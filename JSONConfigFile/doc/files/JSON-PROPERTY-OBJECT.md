# JSONPropertyObject

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [Dictionary](https://docs.godotengine.org/en/stable/classes/class_dictionary.html?highlight=Dictionary)**

Only allows dictionaries.

## Usage

First, once you have instantiated the 'JSONPropertyObject' class, you can add new properties by using the 'add_property' method, which requires the property name and a 'JSONProperty' object as its parameters. By default, any new property is obligatory. If you want to make it optional, you must include 'false' as the third parameter. The fourth parameter is the default value that the property would take if the input does not specify this property. Any default value must pass the tests of this property. Otherwise, it would not be accepted.

After adding all the properties, you can create exclusivity and dependency relationships between them. You can not establish these relations with obligatory properties, so every property must be optional.

You can add exclusivity relations with the 'add_exclusivity' method. This method accepts an array of properties names. Two properties of this array can not be present simultaneously in any input. If you set the second parameter of this method to 'true', the input data must contain one of these properties.

To add a dependency relationship, you must use the method 'add_dependency'. This method takes two parameters, the first one is the name of the main property, and the second one is the name of the dependent property. When the main property is present, the dependent property must be present too.

## Example: Adding two properties

In this example, the configuration structure has one required property. The property 'person' must be an object with two properties: 'name' and 'age'.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a object property
var object_property = JSONPropertyObject.new()
# Add a 'name' property, which is a string
object_property.add_property("name", JSONPropertyString.new())
# Add an 'age' property, which is an integer
object_property.add_property("age", JSONPropertyInteger.new())

# Add a 'person' property
json_config_file.add_property("person", object_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required fields with its corresponding types:

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42
    }
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'person' property is not the correct type.

```JSON
{
    "person": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.OBJECT,
        "context": "person",
        "as_text": "Wrong type: expected 'object', at 'person'."
    }
]
```

### Incorrect JSON: 'person' property contains errors

This JSON contains multiple errors in its 'person' property. It is missing the 'name' property, the 'age' value is not the correct type, and the structure does not specify its last property:

```JSON
{
    "person": {
        "age": "Not a number",
        "unrequired_property": 42
    }
}
```

Returned errors:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.INTEGER,
        "context": "person.age",
        "as_text": "Wrong type: expected 'integer', at 'person.age'."
    },
    {
        "error": JSONProperty.Errors.OBJECT_NON_VALID_PROPERTY,
        "property": "unrequired_property",
        "context": "person",
        "as_text": "Unkown property: 'unrequired_property', this property is not required, at 'person'."
    },
    {
        "error": JSONProperty.Errors.OBJECT_MISSING_PROPERTY,
        "property": "name",
        "context": "person",
        "as_text": "The required property 'name' is missing, at 'person'."
    }
]
```

## Example: Adding two exclusive properties

In this example, the configuration structure has one required property. The property 'person' must be an object with two properties: 'student' and 'employee'. Both can not appear in the same input data, but one of these properties must always be present.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a object property
var object_property = JSONPropertyObject.new()
# Add a 'name' property, which is a string
object_property.add_property("name", JSONPropertyString.new())
# Add an 'age' property, which is an integer
object_property.add_property("age", JSONPropertyInteger.new())
# Add a 'student' property, which is a string
object_property.add_property("student", JSONPropertyString.new(), false)
# Add a 'employee' property, which is a string
object_property.add_property("employee", JSONPropertyString.new(), false)

# Add an exclusivity relationship, at least one property must be present
object_property.add_exclusivity(["student", "employee"], true)

# Add a 'person' property
json_config_file.add_property("person", object_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required fields with its corresponding types:

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42,
        "student": "University Name"
    }
}
```

### Incorrect JSON: Exclusive properties missing

This JSON contains one error in its 'person' property. It is missing one of the exclusive properties:

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42
    }
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.OBJECT_ONE_IS_REQUIRED,
        "properties": ["student", "employee"],
        "context": "person",
        "as_text": "One of this properties needs to be specified: ['student', 'employee'], at 'person'."
    }
]
```

### Incorrect JSON: Two exclusive properties are present

This JSON contains one error in its 'person' property. Both of the exclusive properties are present:

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42,
        "student": "University Name",
        "employee": "Company Name"
    }
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.OBJECT_EXCLUSIVITY_ERROR,
        "properties": ["student", "employee"],
        "context": "person",
        "as_text": "This properties can not be present at the same time: ['student', 'employee'], at 'person'."
    }
]
```

## Example: Adding a main and a dependent property

In this example, the configuration structure has one required property. The property 'person' must be an object with two properties: 'street' and 'city'. If the 'street' is present, the 'city' must also be specified.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a object property
var object_property = JSONPropertyObject.new()
# Add a 'name' property, which is a string
object_property.add_property("name", JSONPropertyString.new())
# Add an 'age' property, which is an integer
object_property.add_property("age", JSONPropertyInteger.new())
# Add a 'street' property, which is a string
object_property.add_property("street", JSONPropertyString.new(), false)
# Add a 'city' property, which is a string
object_property.add_property("city", JSONPropertyString.new(), false)

# Add a dependency relationship, with 'street' as the main property and
# 'city' as the dependent property 
object_property.add_dependency("street", "city")

# Add a 'person' property
json_config_file.add_property("person", object_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON: None of the optional properties are present.

This JSON has none of the optional properties in its 'person' property.

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42
    }
}
```

### Valid JSON: Only the dependent property is specified.

This JSON has specified only the dependent property in its 'person' property.

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42,
        "city": "Big Ville"
    }
}
```

**Tip:** If you want to make both properties mutually dependent, you must add another dependency:

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a object property
var object_property = JSONPropertyObject.new()
# Add a 'name' property, which is a string
object_property.add_property("name", JSONPropertyString.new())
# Add an 'age' property, which is an integer
object_property.add_property("age", JSONPropertyInteger.new())
# Add a 'street' property, which is a string
object_property.add_property("street", JSONPropertyString.new(), false)
# Add a 'city' property, which is a string
object_property.add_property("city", JSONPropertyString.new(), false)

# Add a dependency relationship, with 'street' as the main property and
# 'city' as the dependent property 
object_property.add_dependency("street", "city")
# Add a dependency relationship, with 'city' as the main property and
# 'street' as the dependent property 
object_property.add_dependency("city", "street")

# Add a 'person' property
json_config_file.add_property("person", object_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON: Both the main and the dependent property are specified.

This JSON has specified both the main and the dependent property in its 'person' property.

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42,
        "street": "Long Avenue",
        "city": "Big Ville"
    }
}
```

### Incorrect JSON

This JSON contains one error in its 'person' property. It is missing the dependent property:

```JSON
{
    "person": {
        "name": "Mr. Example",
        "age": 42,
        "street": "Long Avenue"
    }
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.OBJECT_DEPENDENCY_ERROR,
        "main_property": "street",
        "dependent_property": "city",
        "context": "person",
        "as_text": "'street' property has been specified, but 'city' is missing, at 'person'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **add_property** | **name -> String:** <br> Name of the property. <br> **property -> JSONProperty:** <br> Object with the conditions of the property. <br> **required -> bool (true):** <br> Determines if the property is obligatory. <br> **default_value -> Variant (null):** <br> If the property is not present or if it does not meet the conditions, this is the value the result would have. | Adds a new property to the object. | Nothing. |
| **add_exclusivity** | **exclusive_properties -> Array:** <br> Array with the names of the exclusive properties. <br> **one_is_required -> bool (false):** <br> Determines if one of the exclusive properties must be present. | Two properties of the array of exclusive properties cannot be present simultaniously. <br><br> **WARNING**: The exclusive properties array cannot contain properties not yet defined or any required property. | **bool:** <br> If the exclusive properties have been succesfully added. |
| **add_dependency** | **main_property -> String:** <br> Name of the main property. <br> **dependent_property -> String:** <br> Name of the dependent property. | If the main property is present, the dependent property must be present too. <br><br> **WARNING**: Neither the main nor the dependent property can be properties not yet defined or any required property. | **bool:** <br> If the dependency have been succesfully added. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [OBJECT](./ENUMS.md). | Wrong type: expected 'object' |
OBJECT_MISSING_PROPERTY | The input does not specify a required property. | **property -> String:** <br> Name of the required property. | The property '{property}' has not been specified |
OBJECT_NON_VALID_PROPERTY | The input specifies a property not defined by the structure. | **property -> String:** <br> Name of the non-valid property. | The property '{property}' is not a valid one |
OBJECT_ONE_IS_REQUIRED | If the structure defines an array of exclusive properties, and it specifies that at least one must be present, this error arises whenever none of these properties are present. | **properties -> Array:** <br> List of names of the exclusive properties. | One of this properties needs to be specified: {properties} |
OBJECT_EXCLUSIVITY_ERROR | The input specifies two properties present in the same array of exclusive properties. | **properties -> Array:** <br> List of the exclusive properties specified in the input. | This properties can not be present at the same time: {properties} |
OBJECT_DEPENDENCY_ERROR | The main property is present in the input but not the dependent one | **main_property -> String:** <br> Name of the main property. <br> **dependent_property -> String:** <br> Name of the dependent property. | '{main_property}' property has been specified, but '{dependent_property}' is missing |
