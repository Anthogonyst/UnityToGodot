class_name JSONPropertyImage
extends JSONProperty


var _size = null
var _resizable := true
var _interpolation := Image.INTERPOLATE_BILINEAR


func set_size(width: int, height: int, resizable := true,
		interpolation := Image.INTERPOLATE_BILINEAR) -> void:
	_size = Vector2(width, height)
	_resizable = resizable
	_interpolation = interpolation


func remove_size() -> void:
	_size = null


func _validate_type(image) -> void:
	if image is Image:
		_result = image
	elif typeof(image) == TYPE_STRING:
		var image_path = _get_file_path(image)

		var result = Image.new()
		if File.new().file_exists(image_path):
			var error = result.load(image_path)
			if error != OK:
				_errors.append({
					"error": Errors.COULD_NOT_OPEN_IMAGE,
					"code": error,
				})
			else:
				if _size != null:
					if _equal_sizes(result.get_size(), _size):
						_result = result
					else:
						if _resizable:
							_warnings.append({
								"warning": Warnings.IMAGE_WRONG_SIZE,
								"expected_size": [_size.x, _size.y],
								"size": [result.get_size().x, result.get_size().y],
							})

							result.resize(_size.x, _size.y, _interpolation)
							_result = result
						else:
							_errors.append({
								"error": Errors.IMAGE_WRONG_SIZE,
								"expected_size": [_size.x, _size.y],
								"size": [result.get_size().x, result.get_size().y],
							})
				else:
					_result = result
		else:
			_errors.append({
				"error": Errors.COULD_NOT_OPEN_IMAGE,
				"code": ERR_FILE_NOT_FOUND,
			})
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.IMAGE,
		})

func _equal_sizes(size_1: Vector2, size_2: Vector2) -> bool:
	return (abs(size_1.x - size_2.x) < PRECISION_ERROR
			and abs(size_1.y - size_2.y) < PRECISION_ERROR)
