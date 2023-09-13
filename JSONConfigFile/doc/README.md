# JSON Configuration File

## Introduction

JSON Configuration File is a plugin for Godot that aims to aid reading user input via a JSON file. It uses three types of classes for this purpose:

* [**JSONConfigFile:**](./files/JSON-CONFIG-FILE.md) The main class of this plugin. This class would read the user input file, detecting its potential errors. This class also returns the dictionary obtained. It works by adding properties of the desired JSON structure via code.
* [**JSONProperty:**](./files/JSON-PROPERTY.md) The class that defines what restrictions a given property has. This class itself does not apply any test to the user input, but every other property extends from this one. Here it is the list with the twelve types of properties based on this class, alongside the input they expect:
    * [**JSONPropertyBool:**](./files/JSON-PROPERTY-BOOL.md) Booleans.
    * [**JSONPropertyNumber:**](./files/JSON-PROPERTY-NUMBER.md) Real numbers.
    * [**JSONPropertyInteger:**](./files/JSON-PROPERTY-INTEGER.md) Integers.
    * [**JSONPropertyPercentage:**](./files/JSON-PROPERTY-PERCENTAGE.md) Real numbers, in the range [0, 1].
    * [**JSONPropertyString:**](./files/JSON-PROPERTY-STRING.md) Strings.
    * [**JSONPropertyEnum:**](./files/JSON-PROPERTY-ENUM.md) Strings from an array of possible values.
    * [**JSONPropertyArray:**](./files/JSON-PROPERTY-ARRAY.md) Arrays.
    * [**JSONPropertyColor:**](./files/JSON-PROPERTY-COLOR.md) Arrays with 3 to 4 elements. They represent a color in RGB or RGBA, with each value being an integer in the range [0, 255].
    * [**JSONPropertyObject:**](./files/JSON-PROPERTY-OBJECT.md) Dictionaries.
    * [**JSONPropertyFile:**](./files/JSON-PROPERTY-FILE.md) Strings representing a path to a file.
    * [**JSONPropertyJSONConfigFile:**](./files/JSON-PROPERTY-JSON-CONFIG-FILE.md) Strings representing a path to a JSON configuration file.
    * [**JSONPropertyImage:**](./files/JSON-PROPERTY-IMAGE.md) Strings representing a path to an image file.
* [**JSONConfigProcessor:**](./files/JSON-CONFIG-PROCESSOR.md) The abstract class from which any custom process must extend. The functionality of this class is a bit advanced, but basically, it serves as a way to create functions to process the user input before or after the property tests.
