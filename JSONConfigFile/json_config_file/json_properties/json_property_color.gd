class_name JSONPropertyColor
extends JSONProperty


func _validate_type(color) -> void:
	if typeof(color) == TYPE_COLOR:
		_result = color
	elif typeof(color) == TYPE_ARRAY:
		if color.size() < 3 or color.size() > 4:
			_errors.append({
				"error": Errors.COLOR_WRONG_SIZE,
				"size": color.size(),
			})
		else :
			var correct_color := true
			var result := Color.black

			for i in range(color.size()):
				if typeof(color[i]) != TYPE_REAL and typeof(color[i]) != TYPE_INT:
					correct_color = false
					_errors.append({
						"error": Errors.COLOR_WRONG_TYPE,
						"context": "[" + String(i) + "]",
					})
					break

				var value := int(color[i])

				if abs(color[i] - value) > PRECISION_ERROR:
					correct_color = false
					_errors.append({
						"error": Errors.COLOR_WRONG_TYPE,
						"context": "[" + String(i) + "]",
					})
					break
				elif value < 0 or value > 255:
					correct_color = false
					_errors.append({
						"error": Errors.COLOR_OUT_OF_RANGE,
						"value": value,
						"context": "[" + String(i) + "]",
					})
					break
				else:
					match i:
						0:
							result.r8 = value
						1:
							result.g8 = value
						2:
							result.b8 = value
						3:
							result.a8 = value

			if correct_color:
				_result = result
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.COLOR,
		})
