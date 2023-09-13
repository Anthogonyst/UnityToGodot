const fs = require('fs');
const parser = require('./parser');

module.exports = async function convert(options) {
    const { input, inputData, output } = options;

    if (!input && !inputData) {
        throw Error(`Both 'input' and 'inputData' are empty`);
    }

    const rawInput = inputData ? inputData : await readFile(input);
    const rawOutput = parser.parse(rawInput);

    if (!output) {
        return rawOutput;
    }

    await writeFile(output, JSON.stringify(rawOutput, null, 2));
}

async function readFile(filename) {
    return new Promise((resolve, reject) => {
        fs.readFile(filename, (err, data) => {
            if (err) {
                reject(err);
                return;
            }

            resolve(data.toString());
        });
    });
}

async function writeFile(filename, data) {
    return new Promise((resolve, reject) => {
        fs.writeFile(filename, data, err => {
            if (err) {
                reject(err);
                return;
            }

            resolve();
        });
    });
}

