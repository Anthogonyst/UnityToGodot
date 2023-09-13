# Enums

This file contains every enum defined in JSONProperty.

## Types

This enum contains every type of field that this plugin support:

```GDScript
Types {
    BOOL = 0,
    NUMBER = 1,
    INTEGER = 2,
    PERCENTAGE = 3,
    STRING = 4,
    ENUM = 5,
    ARRAY = 6,
    COLOR = 7,
    OBJECT = 8,
    FILE = 9,
    JSON_CONFIG_FILE = 10,
    IMAGE = 11,
}
```

## Errors

This enum contains every error that can be raised:

```GDScript
Errors {
	COULD_NOT_OPEN_FILE = 0,
	COULD_NOT_OPEN_IMAGE = 1,
	EMPTY_FILE = 2,
	JSON_PARSING_ERROR = 3,
	WRONG_TYPE = 4,
	NUMBER_VALUE_LESS_THAN_MIN = 5,
	NUMBER_VALUE_MORE_THAN_MAX = 6,
	PERCENTAGE_LESS_THAN_ZERO = 7,
	PERCENTAGE_MORE_THAN_ONE = 8,
	STRING_SHORTER_THAN_MIN = 9,
	STRING_LONGER_THAN_MAX = 10,
	STRING_DO_NOT_MATCH_PATTERN = 11,
	ENUM_NOT_VALID = 12,
	ARRAY_SMALLER_THAN_MIN = 13,
	ARRAY_BIGGER_THAN_MAX = 14,
	ARRAY_TWO_ELEMENTS_ARE_EQUAL = 15,
	COLOR_WRONG_SIZE = 16,
	COLOR_WRONG_TYPE = 17,
	COLOR_OUT_OF_RANGE = 18,
	OBJECT_MISSING_PROPERTY = 19,
	OBJECT_NON_VALID_PROPERTY = 20,
	OBJECT_ONE_IS_REQUIRED = 21,
	OBJECT_EXCLUSIVITY_ERROR = 22,
	OBJECT_DEPENDENCY_ERROR = 23,
	IMAGE_WRONG_SIZE = 24,
}
```

## Warnings

This enum contains every warning that can be raised:

```GDScript
Warnings {
	IMAGE_WRONG_SIZE = 0,
}
```
