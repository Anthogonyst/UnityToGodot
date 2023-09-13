class_name JSONConfigFile


var _configuration := JSONPropertyObject.new()


func set_preprocessor(processor: JSONConfigProcessor) -> void:
	_configuration.set_preprocessor(processor)


func set_postprocessor(processor: JSONConfigProcessor) -> void:
	_configuration.set_postprocessor(processor)


func add_property(name: String, property: JSONProperty,
		required := true, default_value = null):
	_configuration.add_property(name, property, required, default_value)


func add_exclusivity(exclusive_properties: Array,
		one_is_required := false) -> bool:
	return _configuration.add_exclusivity(exclusive_properties, one_is_required)


func add_dependency(main_property: String, dependent_property: String) -> bool:
	return _configuration.add_dependency(main_property, dependent_property)


func get_result():
	return _configuration._get_result()


func has_errors() -> bool:
	return _configuration._has_errors()


func get_errors() -> Array:
	var errors = _configuration._get_errors()

	for error in errors:
		if typeof(error) == TYPE_DICTIONARY:
			error.as_text = JSONProperty._error_as_text(error)

	return errors


func has_warnings() -> bool:
	return _configuration._has_warnings()


func get_warnings() -> Array:
	var warnings = _configuration._get_warnings()

	for warning in warnings:
		if typeof(warning) == TYPE_DICTIONARY:
			warning.as_text = JSONProperty._warning_as_text(warning)

	return warnings


func validate(file_path : String) -> void:
	_configuration._reset()

	var file = File.new()
	var error = file.open(file_path, File.READ)
	if error != OK:
		_configuration._errors.append({
			"error": JSONProperty.Errors.COULD_NOT_OPEN_FILE,
			"code": error,
		})
		return

	var text = file.get_as_text()
	if text == "":
		_configuration._errors.append({
			"error": JSONProperty.Errors.EMPTY_FILE,
		})
		return

	var json = JSON.parse(text)
	error = json.get_error()
	if error != OK:
		_configuration._errors.append({
			"error": JSONProperty.Errors.JSON_PARSING_ERROR,
			"code": error,
			"line": json.get_error_line(),
			"string": json.get_error_string(),
		})
		return

	_configuration._set_variable("dir_path", file_path.get_base_dir())
	_configuration._validate(_configuration, json.get_result())
