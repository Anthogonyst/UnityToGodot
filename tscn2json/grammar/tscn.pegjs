// PEG grammar for Godot .tscn files

scene
  = descriptor:entity
    entities:(ws entity:entity ws { return entity; })*
    { return { descriptor: descriptor, entities: entities } }

entity
  = begin_entity ws
    type:name ws
    heading:props
    end_entity ws
    props:props
    { return props ?
    	{ type: type, heading: heading, props: props } :
      { type: type, heading: heading }
    }

props
  = props:(
      head:member
      tail:(ws m:member { return m; })*
      {
        var result = {};

        [head].concat(tail).forEach(function(element) {
          result[element.name] = element.value;
        });

        return result;
      }
    )?
    { return props; }

member
  = name:name name_separator value:value {
      return { name: name, value: value };
    }

value
  = false
  / null
  / true
  / number
  / string
  / internal
  / array

// Array
array
  = begin_array
    values:value_list?
    end_array
    { return values !== null ? values : []; }

value_list
  = head:value
    tail:(value_separator v:value { return v; })*
    { return [head].concat(tail); }

// Godot internal types: Color, ExtResource, etc.
internal
  = type:name
    "(" ws
    params:value_list? ws
    ")"
    { return { type: type, params: params } }

// Common
false
  = "false" { return false; }

true
  = "true"  { return true; }

null
  = "null"  { return null; }

begin_array
  = ws "[" ws

end_array
  = ws "]" ws

begin_entity
  = ws "[" ws

end_entity
  = ws "]" ws

name_separator
  = ws "=" ws

value_separator
  = ws "," ws

ws "whitespace"
  = [ \t\n\r]*

// Numbers
number "number"
  = minus? int frac? exp? { return parseFloat(text()); }

decimal_point
  = "."

digit1_9
  = [1-9]

e
  = [eE]

exp
  = e (minus / plus)? DIGIT+

frac
  = decimal_point DIGIT+

int
  = zero / (digit1_9 DIGIT*)

minus
  = "-"

plus
  = "+"

zero
  = "0"

// Strings
name
  = chars:[/_a-z0-9-]i* { return chars.join(''); }

string "string"
  = quotation_mark chars:char* quotation_mark { return chars.join(''); }

char
  = unescaped
  / escape
    sequence:(
        '"'
      / "\\"
      / "/"
      / "b" { return "\b"; }
      / "f" { return "\f"; }
      / "n" { return "\n"; }
      / "r" { return "\r"; }
      / "t" { return "\t"; }
      / "u" digits:$(HEXDIG HEXDIG HEXDIG HEXDIG) {
          return String.fromCharCode(parseInt(digits, 16));
        }
    )
    { return sequence; }

escape
  = "\\"

quotation_mark
  = '"'

unescaped
  = [^\0-\x1F\x22\x5C]

DIGIT
  = [0-9]

HEXDIG
  = [0-9a-f]i