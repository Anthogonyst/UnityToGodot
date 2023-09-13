class_name JSONPropertyEnum
extends JSONProperty


var _list := []


func set_enum(list: Array):
	for element in list:
		if typeof(element) == TYPE_STRING:
			_list.append(element)


func _validate_type(string) -> void:
	if typeof(string) == TYPE_STRING:
		var valid = false

		for element in _list:
			if element == string:
				valid = true
				break

		if valid:
			_result = string
		else:
			_errors.append({
				"error": Errors.ENUM_NOT_VALID,
				"value": string,
			})
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.ENUM,
		})
