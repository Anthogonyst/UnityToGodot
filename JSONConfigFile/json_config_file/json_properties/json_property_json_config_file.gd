class_name JSONPropertyJSONConfigFile
extends JSONProperty


var _json_config_file:= JSONConfigFile.new()


func set_json_config_file(json_config_file: JSONConfigFile) -> void:
	_json_config_file = json_config_file


func _validate_type(config) -> void:
	if typeof(config) == TYPE_STRING:
		_json_config_file.validate(_get_file_path(config))

		for error in _json_config_file.get_errors():
			_errors.append(error)

		for warning in _json_config_file.get_warnings():
			_warnings.append(warning)

		_result = _json_config_file.get_result()
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.JSON_CONFIG_FILE,
		})
