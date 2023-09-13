# JSONProperty
**returns: [Variant](https://docs.godotengine.org/en/stable/classes/class_variant.html?highlight=Variant)**

The class that defines what restrictions a given property has. This class itself does not apply any test to the user input, but every other property extends from this one.

## Example

In this example, the configuration structure has one required property. The property 'anything' can be absolutely anything.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Add a 'anything' property, which is can be absolutly anything
json_config_file.add_property("anything", JSONProperty.new())

# Validate input
json_config_file.validate(json_file_path)
```

### Valid JSON

A string is valid:

```JSON
{
    "anything": "This can take any value"
}
```

### Another valid JSON

A 'null' is valid:

```JSON
{
    "anything": null
}
```

### Yet another valid JSON

An object is valid:

```JSON
{
    "anything": {
        "absolutely": "Any kind of value"
    }
}
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
