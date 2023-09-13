class_name JSONPropertyString
extends JSONProperty


var _min_length = null
var _max_length = null
var _pattern := RegEx.new()


func _init() -> void:
	# warning-ignore:return_value_discarded
	remove_pattern()


func set_min_length(min_length: int) -> void:
	_min_length = min_length


func remove_min_length() -> void:
	_min_length = null


func set_max_length(max_length: int) -> void:
	_max_length = max_length


func remove_max_length() -> void:
	_max_length = null


func set_pattern(pattern: String) -> int:
	return _pattern.compile(pattern)


func remove_pattern() -> void:
	# warning-ignore:return_value_discarded
	_pattern.compile("")


func _validate_type(string) -> void:
	if typeof(string) == TYPE_STRING:
		if _min_length != null and string.length() < _min_length:
			_errors.append({
				"error": Errors.STRING_SHORTER_THAN_MIN,
				"min": _min_length,
				"length": string.length(),
			})
		elif _max_length != null and string.length() > _max_length:
			_errors.append({
				"error": Errors.STRING_LONGER_THAN_MAX,
				"max": _max_length,
				"length": string.length(),
			})
		elif _pattern.search(string) == null:
			_errors.append({
				"error": Errors.STRING_DO_NOT_MATCH_PATTERN,
				"pattern": _pattern.get_pattern(),
				"value": string,
			})
		else:
			_result = string
	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.STRING,
		})
