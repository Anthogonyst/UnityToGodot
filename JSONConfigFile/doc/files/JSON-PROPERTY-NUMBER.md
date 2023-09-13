# JSONPropertyNumber

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [float](https://docs.godotengine.org/en/stable/classes/class_float.html?highlight=float)**

Only allows real numbers.

## Usage

Once you have instantiated the 'JSONPropertyNumber' class, you can set the boundaries of the possible values via the 'set_min_value' and 'set_max_value' methods. The values that these functions receive as parameters are inclusive. To remove any boundary you can use the 'remove_min_value' and 'remove_max_value' methods.

## Example

In this example, the configuration structure has one required property. The property 'number' must be a real number between [0, 10].

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a number property, which value must be between [0, 10]
var number_property = JSONPropertyNumber.new()
number_property.set_min_value(0)
number_property.set_max_value(10)

# Add a 'number' property, which must be a real number between [0, 10]
json_config_file.add_property("number", number_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is a real number between [0, 10]:

```JSON
{
    "number": 3.14159
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'number' property is not the correct type.

```JSON
{
    "number": "42"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.NUMBER,
        "context": "number",
        "as_text": "Wrong type: expected 'number', at 'number'."
    }
]
```

### Incorrect JSON: Can not be less than the minimum value

This JSON contains one error. The 'number' property value is less than the minimum value.

```JSON
{
    "number": -0.1
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.NUMBER_VALUE_LESS_THAN_MIN,
        "value": -0.1,
        "min": 0.0,
        "context": "number",
        "as_text": "-0.100 is less than the minimum allowed (0.000), at 'number'."
    }
]
```

### Incorrect JSON: Can not be more than the maximum value

This JSON contains one error. The 'number' property value is more than the maximum value.

```JSON
{
    "number": 10.1
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.NUMBER_VALUE_MORE_THAN_MAX,
        "value": 10.1,
        "max": 10.0,
        "context": "number",
        "as_text": "10.100 is more than the maximum allowed (10.000), at 'number'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_min_value** | **min_value -> float:** <br> The minimum value allowed. | Sets the minimum value allowed. | Nothing. |
| **remove_min_value** | None. | Removes any minimum boundary. | Nothing. |
| **set_max_value** | **max_value -> float:** <br> The maximum value allowed. | Sets the maximum value allowed. | Nothing. |
| **remove_max_value** | None. | Removes any maximum boundary. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [NUMBER](./ENUMS.md). | Wrong type: expected 'number' |
| NUMBER_VALUE_LESS_THAN_MIN | The value of the input is less than the minimum. | **value -> float:** <br> The input value. <br> **min -> float:** <br> The minimum value allowed. | {value} is less than the minimum allowed ({min}) |
| NUMBER_VALUE_MORE_THAN_MAX | The value of the input is more than the maximum. | **value -> float:** <br> The input value. <br> **max -> float:** <br> The maximum value allowed. | {value} is more than the maximum allowed ({max}) |
