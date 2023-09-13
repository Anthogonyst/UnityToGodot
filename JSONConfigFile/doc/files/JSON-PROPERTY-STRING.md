# JSONPropertyString

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [String](https://docs.godotengine.org/en/stable/classes/class_string.html?highlight=String)**

Only allows strings.

## Usage

Once you have instantiated the 'JSONPropertyString' class, you can set the boundaries of the possible length of the strings via the 'set_min_length' and 'set_max_length' methods. The values that these functions receive as parameters are inclusive. To remove any boundary you can use the 'remove_min_length' and 'remove_max_length' methods.

This class also allows you to specify if the string must satisfy a pattern. Use the 'set_pattern' method to create this pattern. The pattern is a string that represents a regular expression. You must consider that the regular expression can be in any part of the string to be valid, so you might want to include ^ and $ to delimit the regular expression. To remove any pattern use the 'remove_pattern' method.

## Example

In this example, the configuration structure has one required property. The property 'string' must be a string between [3, 5] characters long that only contains letters.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a string property, which length must be between [3, 5], and that
# only contains letters
var string_property = JSONPropertyString.new()
string_property.set_min_length(3)
string_property.set_max_length(5)
string_property.set_pattern("^[a-zA-Z]+$")

# Add a 'string' property, which must be a string with a length between
# [3, 5], and that only contains letters
json_config_file.add_property("string", string_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, which is a string between [3, 5] characters long that only contains letters.

```JSON
{
    "string": "ABCD"
}
```


### Incorrect JSON: Wrong type

This JSON contains one error. The 'string' property is not the correct type.

```JSON
{
    "string": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.STRING,
        "context": "string",
        "as_text": "Wrong type: expected 'string', at 'string'."
    }
]
```

### Incorrect JSON: Can not be shorter than the minimum length

This JSON contains one error. The 'string' property length is shorter than the minimum length.

```JSON
{
    "string": "AB"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.STRING_SHORTER_THAN_MIN,
        "length": 2,
        "min": 3,
        "context": "string",
        "as_text": "The string length (2) is shorter than the minimum length allowed (3), at 'string'."
    }
]
```


### Incorrect JSON: Can not be longer than the maximum length

This JSON contains one error. The 'string' property value is longer than the maximum length.

```JSON
{
    "string": "ABCDEF"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.STRING_LONGER_THAN_MAX,
        "length": 6,
        "max": 5,
        "context": "string",
        "as_text": "The string length (6) is longer than the maximum length allowed (5), at 'string'."
    }
]
```

### Incorrect JSON: Does not match the regular expression

This JSON contains one error. The 'string' property does not match the regular expression.

```JSON
{
    "string": "ABC4"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.STRING_DO_NOT_MATCH_PATTERN,
        "value": "ABC4",
        "pattern": "^[a-zA-Z]+$",
        "context": "string",
        "as_text": "'ABC4' does not match the specified pattern (^[a-zA-Z]+$), at 'string'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_min_length** | **min_value -> int:** <br> The minimum length allowed for the string. | Sets the minimum length allowed. | Nothing. |
| **remove_min_length** | None. | Removes any minimum length boundary. | Nothing. |
| **set_max_length** | **max_value -> int:** <br> The maximum length allowed for the string. | Sets the maximum length allowed. | Nothing. |
| **remove_max_length** | None. | Removes any maximum length boundary. | Nothing. |
| **set_pattern** | **pattern -> String:** <br> The pattern the string must contain. <br><br> **NOTE:** Check [RegEx](https://docs.godotengine.org/en/stable/classes/class_regex.html?highlight=RegEx#regex) for more information. | Sets the pattern the string must contain. | **int:** <br> The result of compiling the pattern. <br><br> **NOTE:** Check [RegEx.compile](https://docs.godotengine.org/en/stable/classes/class_regex.html?highlight=RegEx#class-regex-method-compile) for more information. |
| **remove_pattern** | None. | Removes any pattern. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [STRING](./ENUMS.md). | Wrong type: expected 'string' |
| STRING_SHORTER_THAN_MIN | The input is shorter than the minimum length. | **length -> int:** <br> The input length. <br> **min -> int:** <br> The minimum lenght allowed. | The string length ({length}) is shorter than the minimum length allowed ({min}) |
| STRING_LONGER_THAN_MAX | The input is longer than the maximum length. | **length -> int:** <br> The input length. <br> **max -> int:** <br> The maximum lenght allowed. | The string length ({length}) is longer than the maximum length allowed ({max}) |
| STRING_DO_NOT_MATCH_PATTERN | The input does not match the pattern. | **value -> String:** <br> The input value. <br> **pattern -> String:** <br> The pattern the input should match. | '{value}' does not match the specified pattern ({pattern}) |
