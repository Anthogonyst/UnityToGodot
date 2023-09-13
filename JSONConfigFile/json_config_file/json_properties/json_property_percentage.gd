class_name JSONPropertyPercentage
extends JSONProperty


func _validate_type(number) -> void:
	if typeof(number) == TYPE_REAL || typeof(number) == TYPE_INT:
		var value = float(number)
		if value < 0.0:
			_errors.append({
				"error": Errors.PERCENTAGE_LESS_THAN_ZERO,
				"value": value,
			})
		elif value > 1.0:
			_errors.append({
				"error": Errors.PERCENTAGE_MORE_THAN_ONE,
				"value": value,
			})
		else:
			_result = value
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.PERCENTAGE,
		})
