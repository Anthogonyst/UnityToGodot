class_name JSONProperty


enum Types {
	BOOL,
	NUMBER,
	INTEGER,
	PERCENTAGE,
	STRING,
	ENUM,
	ARRAY,
	COLOR,
	OBJECT,
	FILE,
	JSON_CONFIG_FILE,
	IMAGE,
}

enum Errors {
	COULD_NOT_OPEN_FILE,
	COULD_NOT_OPEN_IMAGE,
	EMPTY_FILE,
	JSON_PARSING_ERROR,
	WRONG_TYPE,
	NUMBER_VALUE_LESS_THAN_MIN,
	NUMBER_VALUE_MORE_THAN_MAX,
	PERCENTAGE_LESS_THAN_ZERO,
	PERCENTAGE_MORE_THAN_ONE,
	STRING_SHORTER_THAN_MIN,
	STRING_LONGER_THAN_MAX,
	STRING_DO_NOT_MATCH_PATTERN,
	ENUM_NOT_VALID,
	ARRAY_SMALLER_THAN_MIN,
	ARRAY_BIGGER_THAN_MAX,
	ARRAY_TWO_ELEMENTS_ARE_EQUAL,
	COLOR_WRONG_SIZE,
	COLOR_WRONG_TYPE,
	COLOR_OUT_OF_RANGE,
	OBJECT_MISSING_PROPERTY,
	OBJECT_NON_VALID_PROPERTY,
	OBJECT_ONE_IS_REQUIRED,
	OBJECT_EXCLUSIVITY_ERROR,
	OBJECT_DEPENDENCY_ERROR,
	IMAGE_WRONG_SIZE,
}

enum Warnings {
	IMAGE_WRONG_SIZE,
}


const PRECISION_ERROR = 0.000001

const MESSAGE_BOOL = "boolean"
const MESSAGE_NUMBER = "number"
const MESSAGE_INTEGER = "integer"
const MESSAGE_PERCENTAGE = "percentage"
const MESSAGE_STRING = "string"
const MESSAGE_ENUM = "string"
const MESSAGE_ARRAY = "array"
const MESSAGE_COLOR = "color"
const MESSAGE_OBJECT = "object"
const MESSAGE_FILE = "file path"
const MESSAGE_JSON_CONFIG_FILE = "JSON configuration file path"
const MESSAGE_IMAGE = "image path"

const MESSAGE_COULD_NOT_OPEN_FILE = "Could not open the file"
const MESSAGE_COULD_NOT_OPEN_IMAGE = "Could not open the image"
const MESSAGE_EMPTY_FILE = "The configuration file can not be empty"
const MESSAGE_JSON_PARSING_ERROR = "JSON parsing error at line %d: \"%s\""
const MESSAGE_WRONG_TYPE = "Wrong type: expected '%s'"
const MESSAGE_NUMBER_VALUE_LESS_THAN_MIN = "%.3f is less than the minimum allowed (%.3f)"
const MESSAGE_NUMBER_VALUE_MORE_THAN_MAX = "%.3f is more than the maximum allowed (%.3f)"
const MESSAGE_INTEGER_VALUE_LESS_THAN_MIN = "%d is less than the minimum allowed (%d)"
const MESSAGE_INTEGER_VALUE_MORE_THAN_MAX = "%d is more than the maximum allowed (%d)"
const MESSAGE_PERCENTAGE_LESS_THAN_ZERO = "%.3f is less than 0"
const MESSAGE_PERCENTAGE_MORE_THAN_ONE = "%.3f is more than 1"
const MESSAGE_STRING_SHORTER_THAN_MIN = "The string length (%s) is shorter than the minimum length allowed (%d)"
const MESSAGE_STRING_LONGER_THAN_MAX = "The string length (%s) is longer than the maximum length allowed (%d)"
const MESSAGE_STRING_DO_NOT_MATCH_PATTERN = "'%s' does not match the specified pattern (%s)"
const MESSAGE_ENUM_NOT_VALID = "'%s' is not in the list of valid values"
const MESSAGE_ARRAY_SMALLER_THAN_MIN = "The array size (%d) is smaller than the minimum allowed (%d)"
const MESSAGE_ARRAY_BIGGER_THAN_MAX = "The array size (%d) is bigger than the maximum allowed (%d)"
const MESSAGE_ARRAY_TWO_ELEMENTS_ARE_EQUAL = "The array contains two elements that are equal: [%d] and [%d]"
const MESSAGE_ARRAY_TWO_KEYS_ARE_EQUAL = "The array contains two objects with the same '%s': [%d] and [%d]"
const MESSAGE_COLOR_WRONG_SIZE = "The color is %d element(s) long, when it should be 3 to 4"
const MESSAGE_COLOR_WRONG_TYPE = "Wrong type: expected 'integer' in the range [0, 255]"
const MESSAGE_COLOR_OUT_OF_RANGE = "%d is out of the range [0, 255]"
const MESSAGE_OBJECT_MISSING_PROPERTY = "The required property '%s' is missing"
const MESSAGE_OBJECT_NON_VALID_PROPERTY = "Unkown property: '%s', this property is not required"
const MESSAGE_OBJECT_ONE_IS_REQUIRED = "One of this properties needs to be specified: %s"
const MESSAGE_OBJECT_EXCLUSIVITY_ERROR = "This properties can not be present at the same time: %s"
const MESSAGE_OBJECT_DEPENDENCY_ERROR = "'%s' property has been specified, but '%s' is missing"
const MESSAGE_IMAGE_WRONG_SIZE = "The image is not the correct size (%d, %d)"

const MESSAGE_WITH_CONTEXT = ", at '%s'."
const MESSAGE_WITHOUT_CONTEXT = "."


var _result
var _errors := []
var _warnings := []
var _public_variables := {}
var _private_variables := {}
var _preprocessor := JSONConfigProcessor.new()
var _postprocessor := JSONConfigProcessor.new()


static func _type_as_text(type: int) -> String:
	match type:
		Types.BOOL:
			return MESSAGE_BOOL
		Types.NUMBER:
			return MESSAGE_NUMBER
		Types.INTEGER:
			return MESSAGE_INTEGER
		Types.PERCENTAGE:
			return MESSAGE_PERCENTAGE
		Types.STRING:
			return MESSAGE_STRING
		Types.ENUM:
			return MESSAGE_ENUM
		Types.ARRAY:
			return MESSAGE_ARRAY
		Types.COLOR:
			return MESSAGE_COLOR
		Types.OBJECT:
			return MESSAGE_OBJECT
		Types.FILE:
			return MESSAGE_FILE
		Types.JSON_CONFIG_FILE:
			return MESSAGE_JSON_CONFIG_FILE
		Types.IMAGE:
			return MESSAGE_IMAGE
		_:
			return "This type message is not defined"


static func _array_as_text(array: Array) -> String:
	var array_as_text = ""

	for i in range(array.size()):
		if i == 0:
			array_as_text = "['" + String(array[0]) + "'"
		elif i == array.size() - 1:
			array_as_text = array_as_text + ", '" + String(array[i]) + "']"
		else:
			array_as_text = array_as_text + ", '" + String(array[i]) + "'"

	return array_as_text


static func _error_as_text(error: Dictionary) -> String:
	var error_as_text

	if error.has("error"):
		match error.error:
			Errors.COULD_NOT_OPEN_FILE:
				error_as_text = MESSAGE_COULD_NOT_OPEN_FILE
			Errors.COULD_NOT_OPEN_IMAGE:
				error_as_text = MESSAGE_COULD_NOT_OPEN_IMAGE
			Errors.EMPTY_FILE:
				error_as_text = MESSAGE_EMPTY_FILE
			Errors.JSON_PARSING_ERROR:
				error_as_text = MESSAGE_JSON_PARSING_ERROR % [error.line, error.string]
			Errors.WRONG_TYPE:
				error_as_text = MESSAGE_WRONG_TYPE % _type_as_text(error.expected)
			Errors.NUMBER_VALUE_LESS_THAN_MIN:
				if typeof(error.value) == TYPE_REAL:
					error_as_text = MESSAGE_NUMBER_VALUE_LESS_THAN_MIN % [error.value, error.min]
				else:
					error_as_text = MESSAGE_INTEGER_VALUE_LESS_THAN_MIN % [error.value, error.min]
			Errors.NUMBER_VALUE_MORE_THAN_MAX:
				if typeof(error.value) == TYPE_REAL:
					error_as_text = MESSAGE_NUMBER_VALUE_MORE_THAN_MAX % [error.value, error.max]
				else:
					error_as_text = MESSAGE_INTEGER_VALUE_MORE_THAN_MAX % [error.value, error.max]
			Errors.PERCENTAGE_LESS_THAN_ZERO:
				error_as_text = MESSAGE_PERCENTAGE_LESS_THAN_ZERO % error.value
			Errors.PERCENTAGE_MORE_THAN_ONE:
				error_as_text = MESSAGE_PERCENTAGE_MORE_THAN_ONE % error.value
			Errors.STRING_SHORTER_THAN_MIN:
				error_as_text = MESSAGE_STRING_SHORTER_THAN_MIN % [error.length, error.min]
			Errors.STRING_LONGER_THAN_MAX:
				error_as_text = MESSAGE_STRING_LONGER_THAN_MAX % [error.length, error.max]
			Errors.STRING_DO_NOT_MATCH_PATTERN:
				error_as_text = MESSAGE_STRING_DO_NOT_MATCH_PATTERN % [error.value, error.pattern]
			Errors.ENUM_NOT_VALID:
				error_as_text = MESSAGE_ENUM_NOT_VALID % error.value
			Errors.ARRAY_SMALLER_THAN_MIN:
				error_as_text = MESSAGE_ARRAY_SMALLER_THAN_MIN % [error.size, error.min]
			Errors.ARRAY_BIGGER_THAN_MAX:
				error_as_text = MESSAGE_ARRAY_BIGGER_THAN_MAX % [error.size, error.max]
			Errors.ARRAY_TWO_ELEMENTS_ARE_EQUAL:
				if error.has("key"):
					error_as_text = MESSAGE_ARRAY_TWO_KEYS_ARE_EQUAL % [error.key, error.element_1, error.element_2]
				else:
					error_as_text = MESSAGE_ARRAY_TWO_ELEMENTS_ARE_EQUAL % [error.element_1, error.element_2]
			Errors.COLOR_WRONG_SIZE:
				error_as_text = MESSAGE_COLOR_WRONG_SIZE % error.size
			Errors.COLOR_WRONG_TYPE:
				error_as_text = MESSAGE_COLOR_WRONG_TYPE
			Errors.COLOR_OUT_OF_RANGE:
				error_as_text = MESSAGE_COLOR_OUT_OF_RANGE % error.value
			Errors.OBJECT_MISSING_PROPERTY:
				error_as_text = MESSAGE_OBJECT_MISSING_PROPERTY % error.property
			Errors.OBJECT_NON_VALID_PROPERTY:
				error_as_text = MESSAGE_OBJECT_NON_VALID_PROPERTY % error.property
			Errors.OBJECT_ONE_IS_REQUIRED:
				error_as_text = MESSAGE_OBJECT_ONE_IS_REQUIRED % _array_as_text(error.properties)
			Errors.OBJECT_EXCLUSIVITY_ERROR:
				error_as_text = MESSAGE_OBJECT_EXCLUSIVITY_ERROR % _array_as_text(error.properties)
			Errors.OBJECT_DEPENDENCY_ERROR:
				error_as_text = MESSAGE_OBJECT_DEPENDENCY_ERROR % [error.main_property, error.dependent_property]
			Errors.IMAGE_WRONG_SIZE:
				error_as_text = MESSAGE_IMAGE_WRONG_SIZE % [error.expected_size[0], error.expected_size[1]]
			_:
				error_as_text = error.error
	else:
		error_as_text = "This error message is not defined"

	if error.has("context"):
		error_as_text = error_as_text + MESSAGE_WITH_CONTEXT % error.context
	else:
		error_as_text = error_as_text + MESSAGE_WITHOUT_CONTEXT

	return error_as_text


static func _warning_as_text(warning: Dictionary) -> String:
	var warning_as_text

	if warning.has("warning"):
		match warning.warning:
			Warnings.IMAGE_WRONG_SIZE:
				warning_as_text = MESSAGE_IMAGE_WRONG_SIZE % [warning.expected_size[0], warning.expected_size[1]]
			_:
				warning_as_text = warning.warning
	else:
		warning_as_text =  "This warning message is not defined"

	if warning.has("context"):
		warning_as_text = warning_as_text + MESSAGE_WITH_CONTEXT % warning.context
	else:
		warning_as_text = warning_as_text + MESSAGE_WITHOUT_CONTEXT

	return warning_as_text


func set_preprocessor(processor: JSONConfigProcessor) -> void:
	_preprocessor = processor


func set_postprocessor(processor: JSONConfigProcessor) -> void:
	_postprocessor = processor


func _get_result():
	return _result


func _has_errors() -> bool:
	return _errors.size() != 0


func _get_errors() -> Array:
	return _errors


func _has_warnings() -> bool:
	return _warnings.size() != 0


func _get_warnings() -> Array:
	return _warnings


func _validate(parent: JSONProperty, property) -> void:
	_reset_result()
	_copy_variables(parent)
	_init_processors()

	_preprocessor._preprocess()

	if not _has_errors():
		_validate_type(property)

	if not _has_errors():
		_result = _postprocessor._postprocess(_get_result())


func _reset() -> void:
	_reset_result()
	_public_variables.clear()
	_private_variables.clear()


func _reset_result() -> void:
	_result = null
	_errors.clear()
	_warnings.clear()


func _copy_variables(parent: JSONProperty) -> void:
	_public_variables = parent._public_variables
	_private_variables = parent._private_variables


func _init_processors() -> void:
	_preprocessor._set_property(self)
	_postprocessor._set_property(self)


func _validate_type(property) -> void:
	_result = property


func _set_variable(name: String, value) -> void:
	_private_variables[name] = value


func _get_variable(name: String):
	if _private_variables.has(name):
		return _private_variables[name]
	else:
		return null


func _get_file_path(file: String) -> String:
	if file.is_abs_path() or _get_variable("dir_path") == null:
		return file
	else:
		return _get_variable("dir_path").plus_file(file)


func _update_context(error_or_warning: Dictionary, context) -> void:
	var index_regex = RegEx.new()
	index_regex.compile("^\\[([0-9]|[1-9]+[0-9]+)\\]")

	if error_or_warning.has("context"):
		if index_regex.search(error_or_warning.context) == null:
			error_or_warning.context = context + "." + error_or_warning.context
		else:
			error_or_warning.context = context + error_or_warning.context
	else:
		error_or_warning.context = context
