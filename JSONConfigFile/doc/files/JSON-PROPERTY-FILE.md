# JSONPropertyFile

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [File](https://docs.godotengine.org/en/stable/classes/class_file.html?highlight=file)**

Only allows strings representing a path to a file. This path can be absolute or relative. When the file path is relative, it would be relative to the directory that contains the JSON configuration file that specifies this field. Please, use '/' as a [path delimiter](https://docs.godotengine.org/en/stable/getting_started/step_by_step/filesystem.html?highlight=file%20path#path-delimiter).

## Usage

Once you have instantiated the 'JSONPropertyFile' class, you can determine how to open the file via the 'set_mode_flag' method. It would be [File.READ](https://docs.godotengine.org/en/stable/classes/class_file.html?highlight=File.READ#enumerations) by default.

## Example

In this example, the configuration structure has one required property. The property 'file' must be a path to another file.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()
	
# Add the 'file' property, which is a path to a file
json_config_file.add_property("file", JSONPropertyFile.new())

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field.

```JSON
{
    "file": "file.txt"
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'file' property is not the correct type.

```JSON
{
    "file": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.FILE,
        "context": "file",
        "as_text": "Wrong type: expected 'file path', at 'file'."
    }
]
```

### Incorrect JSON: Missing image

This JSON contains one error. The 'file' property indicates a path to a file that does not exist.

```JSON
{
    "file": "missing_file.txt"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COULD_NOT_OPEN_IMAGE,
        "code": ERR_FILE_NOT_FOUND,
        "context": "file",
        "as_text": "Could not open the file, at 'file'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_mode_flag** | **mode_flag -> int:** <br> The flag that determines how to open the file. <br><br> **NOTE:** Check [File.ModeFlags](https://docs.godotengine.org/en/stable/classes/class_file.html?highlight=File#enum-file-modeflags) for more information. | Determines how to open the file. | Nothing.

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [FILE](./ENUMS.md). | Wrong type: expected 'file path' |
| COULD_NOT_OPEN_FILE | The file path leads to a nonexistent file or an error occurred when opening the file. | **code -> int:** <br> Error code returned by [File.open()](https://docs.godotengine.org/en/stable/classes/class_file.html?highlight=File#class-file-method-open). | Could not open the file |
