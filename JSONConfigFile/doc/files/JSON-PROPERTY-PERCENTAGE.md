# JSONPropertyPercentage

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [float](https://docs.godotengine.org/en/stable/classes/class_float.html?highlight=float)**

Only allows real numbers, in the range [0, 1].

## Example

In this example, the configuration structure has one required property. The property 'percentage' must be a percentage.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Add a 'percentage' property, which must be a percentage
json_config_file.add_property("percentage", JSONPropertyPercentage.new())

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is a percentage:

```JSON
{
    "percentage": 0.42
}
```

### Incorrect JSON: Can not be a string

This JSON, on the other hand, contains one error. The 'percentage' property is not the correct type.

```JSON
{
    "percentage": "42%"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.PERCENTAGE,
        "context": "percentage",
        "as_text": "Wrong type: expected 'percentage', at 'percentage'."
    }
]
```

### Incorrect JSON: Can not be less than zero

This JSON contains one error. The 'percentage' property is less than zero.

```JSON
{
    "percentage": -0.1
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.PERCENTAGE_LESS_THAN_ZERO,
        "value": -0.1,
        "context": "percentage",
        "as_text": "-0.1 is less than 0, at 'percentage'."
    }
]
```

### Incorrect JSON: Can not be more than one

This JSON contains one error. The 'percentage' property is more than one.

```JSON
{
    "percentage": 1.1
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.PERCENTAGE_MORE_THAN_ONE,
        "value": 1.1,
        "context": "percentage",
        "as_text": "1.1 is more than 1, at 'percentage'."
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
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [PERCENTAGE](./ENUMS.md). | Wrong type: expected 'percentage' |
| PERCENTAGE_LESS_THAN_ZERO | The value of the input is less than zero. | **value -> float:** <br> The input value. | {value} is less than 0 |
| PERCENTAGE_MORE_THAN_ONE | The value of the input is more than one. | **value -> float:** <br> The input value. | {value} is more than 1 |