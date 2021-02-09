const path = require('path');

module.exports = {

    // bundling mode
    mode: 'production',

    // entry files
    entry: {
        timestampedTextbox: './ts/TimestampedTextbox.ts',
        onchange: './ts/OnChange.ts'
    },

    // output bundles (location)
    output: {
        path: path.resolve(__dirname, 'js'),
        filename: '[name].js',
        libraryTarget: 'var',
        library: ['TimeStampLibrary', '[name]']
    },
    devtool: 'source-map',
    // file resolutions
    resolve: {
        extensions: ['.ts', '.js'],
    },

    // loaders
    module: {
        rules: [
            {
                test: /\.tsx?/,
                exclude: /node_modules/,
                loader: 'ts-loader',
                options: {
                    configFile: 'tsconfig.json'
                }
            }
        ]
    }

};