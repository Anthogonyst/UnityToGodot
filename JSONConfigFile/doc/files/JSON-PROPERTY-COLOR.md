# JSONPropertyColor

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [Color](https://docs.godotengine.org/en/stable/classes/class_color.html?highlight=Color)**

Only allows arrays with 3 to 4 elements. They represent a color in RGB or RGBA, with each value being an integer in the range [0, 255]. If the array has only three elements, the transparency would be set to 255.

## Example

In this example, the configuration structure has one required property. The property 'color' must be a color.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Add a 'color' property, which must be a color
json_config_file.add_property("color", JSONPropertyColor.new())

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON: Three elements

This JSON has the required field, which is a color with three elements:

```JSON
{
    "color": [255, 0, 0]
}
```

### Valid JSON: Four elements

This JSON has the required field, which is a color with four elements:

```JSON
{
    "color": [255, 0, 0, 125]
}
```

### Incorrect JSON: Can not be a string

This JSON contains one error. The 'color' property is not the correct type.

```JSON
{
    "color": "red"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.COLOR,
        "context": "color",
        "as_text": "Wrong type: expected 'color', at 'color'."
    }
]
```

### Incorrect JSON: Incorrect number of elements

This JSON contains one error. The 'color' property has not enough elements.

```JSON
{
    "color": [255]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COLOR_WRONG_SIZE,
        "size": 1,
        "context": "color",
        "as_text": "The color is 1 element(s) long, when it should be 3 to 4, at 'color'."
    }
]
```

### Incorrect JSON: Incorrect type of elements

This JSON contains one error. The 'color' property first element is not an integer.

```JSON
{
    "color": [0.5, 0, 0]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COLOR_WRONG_TYPE,
        "context": "color[0]",
        "as_text": "Wrong type: expected 'integer' in the range [0, 255], at 'color[0]'."
    }
]
```

### Incorrect JSON: Out of range element

This JSON contains one error. The 'color' property first element is out of range.

```JSON
{
    "color": [256, 0, 0]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COLOR_OUT_OF_RANGE,
        "value": 256,
        "context": "color[0]",
        "as_text": "256 is out of the range [0, 255], at 'color[0]'."
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
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [COLOR](./ENUMS.md). | Wrong type: expected 'color' |
| COLOR_WRONG_SIZE | The size of the color array is not 3 or 4. | **size -> int:** <br> The input size. | The color is {size} element(s) long, when it should be 3 to 4 |
| COLOR_WRONG_TYPE | The type of one of the color array elements is not an integer. | None. | Wrong type: expected 'integer' in the range [0, 255] |
| COLOR_OUT_OF_RANGE | The value of one of the color array elements is not in the range [0, 256]. | **value -> int:** <br> The input value. | {value} is out of the range [0, 255] |