# JSONPropertyArray

**extends [JSONProperty](./JSON-PROPERTY.md)**

**returns: [Array](https://docs.godotengine.org/en/stable/classes/class_array.html?highlight=Array)**

Only allows arrays.

## Usage

Once you have instantiated the 'JSONPropertyArray' class, you can set restrictions on the size of the array via the 'set_min_size' and 'set_max_size' methods. The values that these functions receive as parameters are inclusive. To remove any restriction, you can use the 'remove_min_size' and 'remove_max_size' methods.

You can also determine what kind of elements can be part of this array via the 'set_element_property' method, which receives a 'JSONProperty' object as a parameter. Otherwise, the array would accept any type of data. You can return to the default behavior using the 'remove_element_property'.

Finally, you can determine if you want each element of the array to be unique via the 'set_uniqueness' method and passing 'true' as its first parameter. When the array contains dictionaries, it would only check if the 'unique_key', the second parameter of this method, values of the dictionaries are equal. By default, the 'unique_key' string is empty. In this case, it would compare all the fields of the dictionaries.

## Example

In this example, the configuration structure has one required property. The property 'furniture' must be a list of 2 to 3 pieces of furniture. Each piece of furniture is an object with an 'id' that needs to be unique, and a 'name'.

```GDScript
# Create a JSON configuration file
var json_config_file = JSONConfigFile.new()

# Create a array property, which size must be between 2 to 3
var array_property = JSONPropertyArray.new()
array_property.set_min_size(2)
array_property.set_max_size(3)

var object_property = JSONPropertyObject.new()
# Add a 'id' property, which is a integer
object_property.add_property("id", JSONPropertyInteger.new())
# Add an 'name' property, which is an string
object_property.add_property("name", JSONPropertyString.new())

# Add the element property to the array
array_property.set_element_property(object_property)
# Set the array uniqueness to 'true', and set the 'unique_key' to 'id'
array_property.set_uniqueness(true, "id")

# Add the 'furniture' property
json_config_file.add_property("furniture", array_property)

# Validate input
json_config_file.validate(json_file_path)
```


### Valid JSON

This JSON has the required field, which is a list of 2 to 3 pieces of furniture.:

```JSON
{
    "furniture" : [
        {
            "id": 0,
            "name": "table"
        },
        {
            "id": 1,
            "name": "chair"
        },
        {
            "id": 2,
            "name": "bed"
        }
    ]
}
```

### Incorrect JSON: Wrong type

This JSON contains one error. The 'furniture' property is not the correct type.

```JSON
{
    "furniture": 42
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.ARRAY,
        "context": "furniture",
        "as_text": "Wrong type: expected 'array', at 'furniture'."
    }
]
```

### Incorrect JSON: Can not be smaller than the minimum size

This JSON contains one error. The 'furniture' property size is less than the minimum size.

```JSON
{
    "furniture": [
        {
            "id": 0,
            "name": "table"
        }
    ]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.ARRAY_SMALLER_THAN_MIN,
        "size": 1,
        "min": 2,
        "context": "furniture",
        "as_text": "The array size (1) is smaller than the minimum allowed (2), at 'furniture'."
    }
]
```

### Incorrect JSON: Can not be bigger than the maximum size

This JSON contains one error. The 'furniture' property size is more than the maximum size.

```JSON
{
    "furniture": [
        {
            "id": 0,
            "name": "table"
        },
        {
            "id": 1,
            "name": "chair"
        },
        {
            "id": 2,
            "name": "bed"
        },
        {
            "id": 3,
            "name": "wardrobe"
        }
    ]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.ARRAY_BIGGER_THAN_MAX,
        "size": 4,
        "max": 3,
        "context": "furniture",
        "as_text": "The array size (4) is bigger than the maximum allowed (3), at 'furniture'."
    }
]
```

### Incorrect JSON: Wrong type in one of the elements

This JSON contains one error. One of the array elements is not the correct type.

```JSON
{
    "furniture": [
        {
            "id": 0,
            "name": "table"
        },
        {
            "id": 1,
            "name": "chair"
        },
        "bed"
    ]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.WRONG_TYPE,
        "expected": JSONProperty.Types.OBJECT,
        "context": "furniture[2]",
        "as_text": "Wrong type: expected 'object', at 'furniture[2]'."
    }
]
```

### Incorrect JSON: Two elements have the same id

This JSON contains one error. Two of the objects have the same id.

```JSON
{
    "furniture" : [
        {
            "id": 0,
            "name": "table"
        },
        {
            "id": 1,
            "name": "chair"
        },
        {
            "id": 1,
            "name": "bed"
        }
    ]
}
```

Returned error:

```GDScript
[
    {
        "error": JSONProperty.Errors.ARRAY_TWO_ELEMENTS_ARE_EQUAL,
        "element_1": 1,
        "element_2": 2,
        "key": "id",
        "context": "furniture",
        "as_text": "The array contains two objects with the same 'id': [1] and [2], at 'furniture'.",
    }
]
```

## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **set_preprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute before the validation process. | Sets the process to execute before the validation process. | Nothing. |
| **set_postprocessor** | **processor -> JSONConfigProcessor:** <br> Object that defines the function to execute after the validation process. | Sets the process to execute after the validation process. | Nothing. |
| **set_min_size** | **min_value -> int:** <br> The minimum size allowed for the array. | Sets the minimum size allowed. | Nothing. |
| **remove_min_size** | None. | Removes any minimum size boundary. | Nothing. |
| **set_max_size** | **max_value -> int:** <br> The maximum size allowed for the array. | Sets the maximum size allowed. | Nothing. |
| **remove_max_size** | None. | Removes any maximum size boundary. | Nothing. |
| **set_element_property** | **element_property -> JSONProperty:** <br> Object with the conditions that every element of the array must satisfy. | Sets the conditions that every element of the array must satisfy. | Nothing. |
| **remove_element_property** | None. | Allows the elements of the array to be anything. | Nothing. |
| **set_uniqueness** | **uniqueness -> bool:** <br> Determines if every object of the array must be different from each other. <br> **unique_key -> String (""):** <br> If the array has dictionaries as its elements, 'unique_key' determines which property to use when comparing if two of then are equal. <br><br> **WARNING:** An empty 'unique_key' means that every property of the dictionaries must be the same for them to be equal. | Determines if the elements of the array have to be unique. | Nothing.

## Errors

This class could directly raise any of the following errors:

| Enum value | Description | Params | As text |
|-|-|-|-|
| WRONG_TYPE | The type of the input does not match the expected one. | **expected -> int:** <br> Takes the value [ARRAY](./ENUMS.md). | Wrong type: expected 'array' |
| ARRAY_SMALLER_THAN_MIN | The size of the input is smaller than the minimum. | **size -> int:** <br> The array size. <br> **min -> int:** <br> The minimum size allowed. | The array size ({size}) is smaller than the minimum allowed ({min}) |
| ARRAY_BIGGER_THAN_MAX | The size of the input is bigger than the maximum. | **size -> int:** <br> The array size. <br> **max -> int:** <br> The maximum size allowed. | The array size ({size}) is bigger than the maximum allowed ({max}) |
| ARRAY_TWO_ELEMENTS_ARE_EQUAL | Two elements of the array are equal. | **element_1 -> int:** <br> Index of the first element. <br> **element_2 -> int:** <br> Index of the second element. <br> **key -> String:** <br> The unique key <br><br> **WARNING:** The 'key' field only appears in the error dictionary if it has determined if two dictionaries are equal. | **Without 'key':** <br> The array contains two elements that are equal: \[{element_1}] and \[element_2] <br><br> **With 'key':** <br> The array contains two objects with the same '{key}': \[{element_1}] and \[element_2]
