# JSONPropertyJSONConfigFile

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [Dictionary](https://docs.godotengine.org/en/stable/classes/class_dictionary.html?highlight=Dictionary)**

Only allows strings representing a path to a JSON configuration file. This path can be absolute or relative. When the file path is relative, it would be relative to the directory that contains the JSON configuration file that specifies this field. Please, use '/' as a [path delimiter](https://docs.godotengine.org/en/stable/getting_started/step_by_step/filesystem.html?highlight=file%20path#path-delimiter).

## Usage

Once you have instantiated the 'JSONPropertyJSONConfigFile' class, you must determine which JSONConfigFile this property would use via the 'set_json_config_file' method.

## Example

In this example, the configuration structure has one required property. The property 'json_file' must be a path to another JSON configuration file that must contain a 'number' property.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create another JSON configuration file
var number_file = JSONConfigFile.new()
# Add a 'number' property, which must be a number
number_file.add_property("number", JSONPropertyNumber.new())

# Create a JSON configuration file property 
var json_property = JSONPropertyJSONConfigFile.new()
# Set its JSON configuration file to the one created previously
json_property.set_json_config_file(number_file)
	
# Add the 'json_file' property, which is a path to another JSON configuration file
json_config_file.add_property("json_file", json_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field, and also the JSON it points to.

```JSON
{
    "json_file": "json_path.json"
}
```

This is the content of 'json_path.json':

```JSON
{
    "number": 42
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'json_file' property is not the correct type.

```JSON
{
    "json_file": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.JSON_CONFIG_FILE,
        "context": "json_file",
        "as_text": "Wrong type: expected 'JSON configuration file path', at 'json_file'."
    }
]
```

### Incorrect JSON: Missing file

This JSON contains one error. The 'json_file' property indicates a path to a file that does not exist.

```JSON
{
    "json_file": "missing_json_path.json"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COULD_NOT_OPEN_FILE,
        "code": ERR_FILE_NOT_FOUND,
        "context": "json_file",
        "as_text": "Could not open the file, at 'json_file'."
    }
]
```

### Incorrect JSON: Wrong type in the other JSON

This JSON has the required field, but not the JSON it points to.

```JSON
{
    "json_file": "json_path.json"
}
```

This is the content of 'json_path.json':

```JSON
{
    "number": "Not a number"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.NUMBER,
        "context": "json_file.number",
        "as_text": "Wrong type: expected 'number', at 'json_file.number'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_json_config_file** | **json_config_file -> JSONConfigFile:** <br> Object that would validate the file indicated by the path that this field contains. | Sets the JSON Configuration File. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [JSON_CONFIG_FILE](./ENUMS.md). | Wrong type: expected 'JSON configuration file path' |
