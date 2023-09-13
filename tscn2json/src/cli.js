const convert = require('./');

module.exports = async argv => {
    const input = argv[2];
    const output = argv[3];

    if (!input) {
        console.log('Usage: tscn2json input [output]');
        return;
    }

    try {
        const res = await convert({ input, output });
        if (res) {
            console.log(JSON.stringify(res, null, 2));
        }
    } catch(e) {
        console.error(e);
    }
}