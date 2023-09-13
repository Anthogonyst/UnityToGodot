# JSONPropertyInteger

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [int](https://docs.godotengine.org/en/stable/classes/class_int.html)**

Only allows integers.

## Usage

Once you have instantiated the 'JSONPropertyInteger' class, you can set the boundaries of the possible values via the 'set_min_value' and 'set_max_value' methods. The values that these functions receive as parameters are inclusive. To remove any boundary you can use the 'remove_min_value' and 'remove_max_value' methods.

## Example

In this example, the configuration structure has one required property. The property 'integer' must be an integer between [0, 10].

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create an integer property, which value must be between [0, 10]
var integer_property = JSONPropertyInteger.new()
integer_property.set_min_value(0)
integer_property.set_max_value(10)

# Add a 'integer' property, which must be an integer between [0, 10]
json_config_file.add_property("integer", integer_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is an integer between [0, 10]:

```JSON
{
    "integer": 5
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'integer' property is not the correct type.

```JSON
{
    "integer": 3.14159
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.INTEGER,
        "context": "integer",
        "as_text": "Wrong type: expected 'integer', at 'integer'."
    }
]
```

### Incorrect JSON: Can not be less than the minimum value

This JSON contains one error. The 'integer' property value is less than the minimum value.

```JSON
{
    "integer": -1
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.NUMBER_VALUE_LESS_THAN_MIN,
        "value": -1,
        "min": 0,
        "context": "integer",
        "as_text": "-1 is less than the minimum allowed (0), at 'integer'."
    }
]
```

### Incorrect JSON: Can not be more than the maximum value

This JSON contains one error. The 'integer' property value is more than the maximum value.

```JSON
{
    "integer": 11
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.NUMBER_VALUE_MORE_THAN_MAX,
        "value": 11,
        "max": 10,
        "context": "integer",
        "as_text": "11 is more than the maximum allowed (10), at 'integer'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_min_value** | **min_value -> int:** <br> The minimum value allowed. | Sets the minimum value allowed. | Nothing. |
| **remove_min_value** | None. | Removes any minimum boundary. | Nothing. |
| **set_max_value** | **max_value -> int:** <br> The maximum value allowed. | Sets the maximum value allowed. | Nothing. |
| **remove_max_value** | None. | Removes any maximum boundary. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [INTEGER](./ENUMS.md). | Wrong type: expected 'integer' |
| NUMBER_VALUE_LESS_THAN_MIN | The value of the input is less than the minimum. | **value -> int:** <br> The input value. <br> **min -> int:** <br> The minimum value allowed. | {value} is less than the minimum allowed ({min}) |
| NUMBER_VALUE_MORE_THAN_MAX | The value of the input is more than the maximum. | **value -> int:** <br> The input value. <br> **max -> int:** <br> The maximum value allowed. | {value} is more than the maximum allowed ({max}) |

