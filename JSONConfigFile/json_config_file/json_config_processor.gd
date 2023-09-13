class_name JSONConfigProcessor


var _property


func add_error(error: Dictionary) -> void:
	_property._errors.append(error)


func add_warning(warning: Dictionary) -> void:
	_property._warnings.append(warning)


func set_variable(name: String, value) -> void:
	_property._public_variables[name] = value


func has_variable(name: String) -> bool:
	return _property._public_variables.has(name)


func get_variable(name: String):
	if has_variable(name):
		return _property._public_variables[name]
	else:
		return null


func _set_property(property) -> void:
	_property = property


func get_property():
	return _property


func _preprocess() -> void:
	pass


func _postprocess(value):
	return value
