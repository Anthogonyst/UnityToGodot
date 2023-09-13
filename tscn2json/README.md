tscn2json - tool to convert [Godot](https://godotengine.org) .tscn files to json format, with command line and programmatic interfaces. Based on parser, generated with [PEG.js](https://pegjs.org), grammar can be found here: [grammar/tscn.pegjs](grammar/tscn.pegjs)
## Installation
To use the tscn2json command, install globally:

`npm install -g tscn2json`

To use the JavaScript API, install locally:

`npm install --save tscn2json`

## Command Line usage
Usage is simple: `tscn2json input [output]`

`input` - input tscn file, `output` - output json file, if omitted, result will be printed to `stdout`.

Example: ```tscn2json scene.tscn parsed-scene.json```
## Programmatic usage
It's pretty easy to use it. `tscn2json` module exports one async function, you just have to include it and call with `options` object:
```javascript
const convert = require('tscn2json');

(async () => {
  await convert({
    input: 'scene.tscn',
    output: 'parsed-scene.json'
  });
})();
```
Options can be
  * `options.input` - input file name
  * `options.inputData` - instead of file name, you can provide string with tscn data
  * `options.output` - output file name, if omitted, resulted json will be returned

Here example with return json data:
```javascript
const res = await convert({ input: 'scene.tscn' });
```