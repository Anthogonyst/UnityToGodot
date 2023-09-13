class_name JSONPropertyObject
extends JSONProperty


var _properties := {}
var _properties_in_order := []
var _required_properties := []
var _default_values := {}
var _exclusivity_relations := []
var _dependency_relations := {}


func add_property(name: String, property: JSONProperty,
		required := true, default_value = null) -> void:

	if property != null:
		_properties[name] = property
		_properties_in_order.append(name)

		if required:
			_required_properties.append(name)

		if default_value != null:
			property._reset()
			property._validate(self, default_value)
			if not property._has_errors():
				_default_values[name] = default_value


func add_exclusivity(exclusive_properties: Array,
		one_is_required := false) -> bool:
	for property in exclusive_properties:
		if not _properties.has(property):
			return false

		for required_property in _required_properties:
			if property == required_property:
				return false

	_exclusivity_relations.append({
		"properties": exclusive_properties,
		"one_is_required": one_is_required,
	})
	return true


func add_dependency(main_property: String, dependent_property: String) -> bool:
	if not _properties.has(main_property):
		return false

	for required_property in _required_properties:
		if main_property == required_property:
			return false

	if not _properties.has(dependent_property):
		return false

	for required_property in _required_properties:
		if dependent_property == required_property:
			return false

	if _dependency_relations.has(main_property):
		_dependency_relations[main_property].append(dependent_property)
	else:
		_dependency_relations[main_property] = [dependent_property]

	return true


func _validate_type(object) -> void:
	if typeof(object) == TYPE_DICTIONARY:
		_result = {}

		for property_name in _properties_in_order:
			if object.has(property_name):
				var property = _properties[property_name]

				property._validate(self, object[property_name])

				_result[property_name] = property._get_result()

				for error in property._get_errors():
					_update_context(error, property_name)

					_errors.append(error)

				for warning in property._get_warnings():
					_update_context(warning, property_name)

					_warnings.append(warning)

				if _dependency_relations.has(property_name):
					for dependent_property in _dependency_relations[property_name]:
						if not object.has(dependent_property):
							_errors.append({
								"error": Errors.OBJECT_DEPENDENCY_ERROR,
								"main_property": property_name,
								"dependent_property": dependent_property,
							})

		for key in object.keys():
			if not _properties.has(key):
				_errors.append({
					"error": Errors.OBJECT_NON_VALID_PROPERTY,
					"property": key,
				})

		for required_property in _required_properties:
			if not object.has(required_property):
				_result[required_property] = null

				_errors.append({
					"error": Errors.OBJECT_MISSING_PROPERTY,
					"property": required_property,
				})

		for exclusivity_relation in _exclusivity_relations:
			var properties = []

			for property in exclusivity_relation.properties:
				if _result.has(property):
					properties.append(property)

			if properties.size() == 0:
				if exclusivity_relation.one_is_required:
					_errors.append({
						"error": Errors.OBJECT_ONE_IS_REQUIRED,
						"properties": exclusivity_relation.properties,
					})
			elif properties.size() != 1:
				_errors.append({
					"error": Errors.OBJECT_EXCLUSIVITY_ERROR,
					"properties": properties,
				})

		for key in _default_values.keys():
			if not _result.has(key) or _result[key] == null:
				_result[key] = _default_values[key]

	else:
		_errors.append({
			"error": Errors.WRONG_TYPE,
			"expected": Types.OBJECT,
		})
