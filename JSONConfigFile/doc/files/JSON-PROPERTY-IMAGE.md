# JSONPropertyImage

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [Image](https://docs.godotengine.org/en/stable/classes/class_image.html?highlight=image)**

Only allows strings representing a path to an image. This path can be absolute or relative. When the file path is relative, it would be relative to the directory that contains the JSON configuration file that specifies this field. Please, use '/' as a [path delimiter](https://docs.godotengine.org/en/stable/getting_started/step_by_step/filesystem.html?highlight=file%20path#path-delimiter). Also, check Godot's [supported image formats](https://docs.godotengine.org/en/latest/getting_started/workflow/assets/importing_images.html#supported-image-formats).

## Usage

Once you have instantiated the 'JSONPropertyImage' class, you can determine the size of the image via the 'set_size' method. By default, the images would be resizable and rescaled using [Image.INTERPOLATE_BILINEAR](https://docs.godotengine.org/en/stable/classes/class_image.html?highlight=Image#enum-image-interpolation). That means that when the image size is different, this property will raise a warning. If you want it to raise an error, set the third parameter in 'set_size' to 'false'.

## Example

In this example, the configuration structure has one required property. The property 'image' must be a path to an image which size should be 64x64.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create an image property
var image_property = JSONPropertyImage.new()
image_property.set_size(64, 64)
	
# Add the 'image' property, which is a path to an image
json_config_file.add_property("image", image_property)

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

This JSON has the required field.

```JSON
{
    "image": "image.png"
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'image' property is not the correct type.

```JSON
{
    "image": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.IMAGE,
        "context": "image",
        "as_text": "Wrong type: expected 'image path', at 'image'."
    }
]
```

### Incorrect JSON: Missing image

This JSON contains one error. The 'image' property indicates a path to an image that does not exist.

```JSON
{
    "image": "missing_image.png"
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.COULD_NOT_OPEN_IMAGE,
        "code": ERR_FILE_NOT_FOUND,
        "context": "image",
        "as_text": "Could not open the image, at 'image'."
    }
]
```

### Semi-incorrect JSON: Wrong size image

This JSON contains one warning. The 'image' property indicates a path to an image which size is not the desired one.

```JSON
{
    "image": "32x32_image.png"
}
```

Returned warning:

```GDScript
[
    {
        "warning": Warnings.IMAGE_WRONG_SIZE,
		"expected_size": [64, 64],
		"size": [32, 32],
        "context": "image",
        "as_text": "The image is not the correct size (64, 64), at 'image'."
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_size** | **width -> int:** <br> Width of the image. <br> **height -> int:** <br> Height of the image. <br> **resizable -> bool(true):** <br> Determines if the image is resizable. If it is, it will raise a warning whenever the size of the input image is different than this size, and then it will resize the image. If it is not, it will raise an error instead. <br> **interpolation -> int(Image.INTERPOLATE_BILINEAR):** <br> The interpolation technic to apply whenever the size of the input image is different, and it must be resized. <br><br> **NOTE:** Check [Image.Interpolation](https://docs.godotengine.org/en/stable/classes/class_image.html?highlight=Image#enum-image-interpolation) for more information. | Sets the recommended, or required, size of the image. | Nothing. |

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [IMAGE](./ENUMS.md). | Wrong type: expected 'file path' |
| COULD_NOT_OPEN_IMAGE | The image path leads to a nonexistent image or an error occurred when loading the image. | **code -> int:** <br> Error code returned by [Image.load()](https://docs.godotengine.org/en/stable/classes/class_image.html?highlight=Image#class-image-method-load). | Could not open the image |
IMAGE_WRONG_SIZE | The image has not the correct size. | **expected_size -> array:** <br> Array with two integers representing the expected size of the image. <br> **size -> array:** <br> Array with two integers representing the actual size of the image. | The image is not the correct size ({expected_size})

## Warnings

This class could directly raise any of the following warnings:

| Enum value | Description | Params | As text |
|-|-|-|-|
IMAGE_WRONG_SIZE | The image has not the correct size. | **expected_size -> array:** <br> Array with two integers representing the expected size of the image. <br> **size -> array:** <br> Array with two integers representing the actual size of the image. | The image is not the correct size ({expected_size})
