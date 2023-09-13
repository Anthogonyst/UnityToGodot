class_name JSONPropertyInteger
extends JSONProperty


var _min_value = null
var _max_value = null


func set_min_value(min_value: int) -> void:
	_min_value = min_value


func remove_min_value() -> void:
	_min_value = null


func set_max_value(max_value: int) -> void:
	_max_value = max_value


func remove_max_value() -> void:
	_max_value = null


func _validate_type(integer) -> void:
	if typeof(integer) == TYPE_REAL or typeof(integer) == TYPE_INT:
		if abs(integer - int(integer)) < PRECISION_ERROR:
			var value = int(integer)
			if _min_value != null and value < _min_value:
				_errors.append({
					"error": Errors.NUMBER_VALUE_LESS_THAN_MIN,
					"min": _min_value,
					"value": value,
				})
			elif _max_value != null and value > _max_value:
				_errors.append({
					"error": Errors.NUMBER_VALUE_MORE_THAN_MAX,
					"max": _max_value,
					"value": value,
				})
			else:
				_result = value
		else:
			_errors.append({
				"error": Errors.WRONG_TYPE,
				"expected": Types.INTEGER,
			})
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.INTEGER,
		})
