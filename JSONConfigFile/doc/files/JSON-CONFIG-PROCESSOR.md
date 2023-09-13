# JSONConfigProcessor

Is an abstract class from which any custom process must extend. The functionality of this class is a bit advanced, but basically, it serves as a way to create functions to process the user input before or after the property tests.

## Usage

This plugin uses processors as a way to allow the user to execute any custom code during the JSON validation process, in case the functionality provided by this framework is not enough. This page would explain how to use them. But first, let's discuss some behaviour of the validation process.

Each property execute the validation process in three phases:
1. **Preprocess:** The user determines this phase behaviour. However, it can not modify the input data.
2. **Type validation:** This phase executes all the validation tests that we have been discussing in other parts of this documentation.
3. **Postprocess:** The user determines this phase behaviour. This phase can modify the input data.

Whenever an error arises in any of these phases, the process would not execute the following ones. On the other hand, raising warnings would not stop the validation process.

Also, when the process validates a dictionary or [JSONPropertyObject](./JSON-PROPERTY-OBJECT.md), it first executes the preprocess, then its type validation consists on the preprocess, type validation, and postprocess cycle of each of the dictionary properties, and finally it executes the postprocess. The same occurs in [JSONPropertyArray](./JSON-PROPERTY-ARRAY.md).

Finally, on a side note, the process validates the properties of a dictionary in the order they were defined. This fact becomes significant when the custom processes writte and read global variables.

Now we would explain what you can do when using custom processes:
- You can access the property to call its methods, using the 'get_property' method.
- You can raise custom errors and warnings, using the 'add_error' and 'add_warning' methods.
- You can modify the output of a certain property, but only while postprocessing.
- You can set global variables that can be accessed or modify at any point of the validation process, using the 'set_variable', 'has_variable' and 'get_variable' methods.

When creating a new processor, we would need to overwrite the '_preprocess' method, which does not receive the input data and does not return anything, or the '_postprocess' method, which gets the input data as a parameter and returns an output.

## Example: Properties determine the validation process of another property

In this example, the JSON file would contain a 'min' and a 'max' property that would set the minimum and the maximum value of another property 'value':

```JSON
{
    "min": 0,
    "max": 5,
    "value": 2
}
```

To achieve this, we would need three processors in three different scripts. First, the 'set_min' postprocessor:

```GDScript
extends JSONConfigProcessor

# Overwrite the '_postprocess' method
func _postprocess(minimum: int):
    # Set a global variable called 'min'
    set_variable("min", minimum)

    # It is a postprocessor, so it must return a value
    return minimum
```

The 'set_max' postprocessor:

```GDScript
extends JSONConfigProcessor

# Overwrite the '_postprocess' method
func _postprocess(maximum: int):
    # Set a global variable called 'max'
    set_variable("max", maximum)

    # It is a postprocessor, so it must return a value
    return maximum
```

The 'set_range' preprocessor. It is crucial to notice that we need to check if the variables exists. Just in case the input data is non-valid, and the variables are not declared:

```GDScript
extends JSONConfigProcessor

# Overwrite the '_preprocess' method
func _preprocess():
    # Check if the global variable min exists
    if has_variable("min"):
        # Set the integer minimum value
        get_property().set_min_value(get_variable("min"))
    # Check if the global variable max exists
    if has_variable("max"):
        # Set the integer maximum value
        get_property().set_max_value(get_variable("max"))

    # It is a preprocessor, so it does not return a value
```

Now we can declare a new JSON configuration file that would check if 'value' is between 'min' and 'max':

```GDScript
# Create the 'min' property
var min_value = JSONPropertyInteger.new()
min_value.set_postprocessor(preload("res://set_min.gd").new())

# Create the 'max' property
var max_value = JSONPropertyInteger.new()
max_value.set_postprocessor(preload("res://set_max.gd").new())

# Create the 'value' property
var value = JSONPropertyInteger.new()
value.set_preprocessor(preload("res://set_range.gd").new())

# Create the JSON configuration file
var json_config_file = JSONConfigFile.new()
# Add the 'min' property
json_config_file.add_property("min", min_value)
# Add the 'max' property
json_config_file.add_property("max", max_value)
# Add the 'value' property
json_config_file.add_property("value", value)

# Validate input
json_config_file.validate(json_file_path)
```

## Example: Transforming an enum into an integer
In this case, we want the user to introduce an enum. This field would be a string on the final dictionary, but we can transform this field to be an integer that represents a value of a Godot's enum. For this purpose, we must define a postprocessor called 'transform_gender_enum' that looks like this:

```GDScript
extends JSONConfigProcessor


enum Genders{
    MALE,
    FEMALE,
    NON_BINARY
}


func _postprocess(gender: String):
    match gender:
        "MALE":
            return Genders.MALE
        "FEMALE":
            return Genders.FEMALE
        "NON_BINARY":
            return Genders.NON_BINARY

    return -1
```

Then we can create the following JSON configuration file:

```GDScript
# Create the 'gender' property
var gender = JSONPropertyEnum.new()
gender.set_enum(["MALE", "FEMALE", "NON_BINARY"])
gender.set_postprocessor(preload("res://transform_gender_enum.gd").new())

# Create the JSON configuration file
var json_config_file = JSONConfigFile.new()
# Add the 'gender' property
json_config_file.add_property("gender", gender)

# Validate input
json_config_file.validate(json_file_path)
```

Using this method, this JSON file:

```JSON
{
    "gender": "MALE"
}
```

Would generate this dictionary:

```GDScript
{
    "gender": Genders.MALE
}
```

## Example: Primes validation

In this last example, we would raise a custom error. We will check if an integer is prime. For this purpose, we need to create another postprocessor called 'prime_check':

```GDScript
extends JSONConfigProcessor


func _postprocess(integer: int):
    # One is not a prime >:(
    if integer == 1:
        add_error({"error": "This is not a prime"})
        return null

    # A simple prime check
    for i in range(2, sqrt(integer) + 1):
        # If is not prime
        if integer % i == 0:
            add_error({"error": "This is not a prime"})
            return null

    return integer
```

Now we can create the following JSON configuration file:

```GDScript
# Create the 'prime' property
var prime = JSONPropertyInteger.new()
prime.set_postprocessor(preload("res://prime_check.gd").new())

# Create the JSON configuration file
var json_config_file = JSONConfigFile.new()
# Add the 'prime' property
json_config_file.add_property("prime", prime)
	
# Validate input
json_config_file.validate(json_file_path)
```

In this case, this JSON file would not raise any error:

```JSON
{
    "prime": 7
}
```

This file, on the other hand, would raise our custom error:

```JSON
{
    "prime": 4
}
```

Returned error:

```GDScript
[
    {
        "error": "This is not a prime",
        "context": "prime",
        "as_text": "This is not a prime, at 'prime'."
    }
]
```
## Functions

The public methods of this class are:

| Name | Params | Description | Returns |
|-|-|-|-|
| **add_error** | **error -> Dictionary:** <br> Custom error. | Raises a custom error, if you want it to have the 'as_text' property, include your custom error message in an 'error' property. | Nothing. |
| **add_warning** | **warning -> Dictionary:** <br> Custom warning. | Raises a custom warning, if you want it to have the 'as_text' property, include your custom warning message in an 'warning' property. | Nothing. |
| **set_variable** | **name -> String:** <br> Name of the global variable to set. <br> **value -> Variant:** <br> The value assigned to this global variable. | Creates a global variable or modify the value of a previously existing one. | Nothing. |
| **has_variable** | **name -> String:** <br> Name of the global variable to check. | Checks if the global variable exists. | **bool:** <br> If the global variable exists. |
| **get_variable** | **name -> String:** <br> Name of the global variable to get. | Gets the value of the specified global variable. | **Variant:** <br> The value of the global variable. If the variable is undefined, it will return null. |
| **get_property** | None. | Returns the JSON property in which this processor is located. | **JSONProperty:** <br> The JSON property in which this processor is located.