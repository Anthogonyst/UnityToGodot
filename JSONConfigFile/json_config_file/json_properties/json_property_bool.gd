class_name JSONPropertyBool
extends JSONProperty


func _validate_type(boolean) -> void:
	if typeof(boolean) == TYPE_BOOL:
		_result = boolean
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.BOOL,
		})
