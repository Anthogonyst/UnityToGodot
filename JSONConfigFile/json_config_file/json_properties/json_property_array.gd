class_name JSONPropertyArray
extends JSONProperty


var _min_size = null
var _max_size = null
var _element_property := JSONProperty.new()
var _uniqueness := false
var _unique_key := ""


func set_min_size(min_size: int) -> void:
	_min_size = min_size


func remove_min_size() -> void:
	_min_size = null


func set_max_size(max_size: int) -> void:
	_max_size = max_size


func remove_max_size() -> void:
	_max_size = null


func set_element_property(element_property: JSONProperty) -> void:
	_element_property = element_property


func remove_element_property() -> void:
	_element_property = JSONProperty.new()


func set_uniqueness(uniqueness: bool, unique_key := "") -> void:
	_uniqueness = uniqueness
	_unique_key = unique_key


func _validate_type(array) -> void:
	if typeof(array) == TYPE_ARRAY:
		if _min_size != null and array.size() < _min_size:
			_errors.append({
				"error": Errors.ARRAY_SMALLER_THAN_MIN,
				"min": _min_size,
				"size": array.size(),
			})
		elif _max_size != null and array.size() > _max_size:
			_errors.append({
				"error": Errors.ARRAY_BIGGER_THAN_MAX,
				"max": _max_size,
				"size": array.size(),
			})
		else :
			_result = []

			for i in range(array.size()):
				_element_property._validate(self, array[i])

				_result.append(_element_property._get_result())

				var index = "[" + String(i) + "]"

				for error in _element_property._get_errors():
					_update_context(error, index)

					_errors.append(error)

				for warning in _element_property._get_warnings():
					_update_context(warning, index)

					_warnings.append(warning)

			if _uniqueness and not _has_errors():
				for i in range(0, _get_result().size()):
					for j in range(1 + i, _get_result().size()):
						var value_1 = _get_result()[i]
						var value_2 = _get_result()[j]

						if _are_equal(value_1, value_2):
							var error = {
								"error": Errors.ARRAY_TWO_ELEMENTS_ARE_EQUAL,
								"element_1": i,
								"element_2": j,
							}

							if typeof(value_1) == TYPE_DICTIONARY:
								if typeof(value_2) == TYPE_DICTIONARY:
									if _can_apply_unique_key(value_1, value_2):
										error.key = _unique_key

							_errors.append(error)
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.ARRAY,
		})


func _are_equal(value_1, value_2) -> bool:
	if typeof(value_1) != typeof(value_2):
		return false

	if typeof(value_1) == TYPE_REAL:
		return _are_equal_floats(value_1, value_2)
	elif typeof(value_1) == TYPE_ARRAY:
		return _are_equal_arrays(value_1, value_2)
	elif typeof(value_1) == TYPE_DICTIONARY:
		return _are_equal_dictionaries(value_1, value_2)
	else:
		return value_1 == value_2


func _are_equal_floats(float_1 : float, float_2 : float) -> bool:
	return abs(float_1 - float_2) < PRECISION_ERROR


func _are_equal_arrays(array_1 : Array, array_2 : Array) -> bool:
	if array_1.size() != array_2.size():
		return false

	for i in array_1.size():
		if not _are_equal(array_1[i], array_2[i]):
			return false

	return true


func _are_equal_dictionaries(dic_1 : Dictionary, dic_2 : Dictionary) -> bool:
	if _can_apply_unique_key(dic_1, dic_2):
		var unique_key = _unique_key
		_unique_key = ""

		var result = _are_equal(dic_1[unique_key], dic_2[unique_key])

		_unique_key = unique_key
		return result

	if dic_1.keys().size() != dic_2.keys().size():
		return false

	for key in dic_1.keys():
		if dic_2.has(key):
			if not _are_equal(dic_1[key], dic_2[key]):
				return false
		else:
			return false

	return true

func _can_apply_unique_key(dic_1 : Dictionary, dic_2 : Dictionary) -> bool:
	return not _unique_key.empty() and dic_1.has(_unique_key) and dic_2.has(_unique_key)
