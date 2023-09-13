# JSONPropertyEnum

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [String](https://docs.godotengine.org/en/stable/classes/class_string.html?highlight=String)**

Only allows strings from an array of possible values.

## Usage

Once you have instantiated the 'JSONPropertyEnum' class, you must provide an array with the possible values via the 'set_enum' method. Otherwise, it would not allow anything at all.

## Example

In this example, the configuration structure has one required property. The property 'enum' must be a string with the value "FIRST", "SECOND", or "THIRD".

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a enum property, which can only be "FIRST", "SECOND", or "THIRD"
var enum_property = JSONPropertyEnum.new()
enum_property.set_enum(["FIRST", "SECOND", "THIRD"])

# Add a 'enum' property, which can only be "FIRST", "SECOND", or "THIRD"
json_config_file.add_property("enum", enum_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is a string with the value "FIRST".

```JSON
{
    "enum": "FIRST"
}
```


### Incorrect JSON: Wrong type

This JSON contains one error. The 'enum' property is not the correct type.

```JSON
{
    "enum": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.ENUM,
        "context": "enum",
        "as_text": "Wrong type: expected 'string', at 'enum'."
    }
]
```

### Incorrect JSON: Can not be 'FOURTH'

This JSON contains one error. The 'enum' property has a value that is not present in the list of possible values.

```JSON
{
    "enum": "FOURTH"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.ENUM_NOT_VALID,
        "value": "FOURTH",
        "context": "enum",
        "as_text": "'FOURTH' is not in the list of valid values, at 'enum'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_enum** | **list -> Array:** <br> Array of strings with the values that are allowed in this field. | Sets the list of values that are allowed in this field. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [ENUM](./ENUMS.md). | Wrong type: expected 'string' |
| ENUM_NOT_VALID | The input is not present in the list of possible values. | **value -> String:** <br> The input value. | '{value}' is not in the list of valid values |
