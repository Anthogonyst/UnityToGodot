# JSONPropertyBool

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [bool](https://docs.godotengine.org/en/stable/classes/class_bool.html?highlight=bool)**

Only allows booleans.

## Example

In this example, the configuration structure has one required property. The property 'bool' must be a boolean.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Add a 'bool' property, which must be a boolean
json_config_file.add_property("bool", JSONPropertyBool.new())

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is a boolean:

```JSON
{
    "bool": true
}
```

### Incorrect JSON

This JSON contains one error. The 'bool' property is not the correct type.

```JSON
{
    "bool": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.BOOL,
        "context": "bool",
        "as_text": "Wrong type: expected 'boolean', at 'bool'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [BOOL](./ENUMS.md). | Wrong type: expected 'boolean' |
