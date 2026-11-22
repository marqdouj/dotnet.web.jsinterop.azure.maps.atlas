const path = require('path');

module.exports = [
    // library: module
    {
        entry: './tsgen/atlas.js',
        output: {
            path: path.resolve(__dirname, 'wwwroot'),
            filename: 'atlas.js',
            libraryTarget: 'module'
        },
        mode: 'production',
        externalsType: 'var',
        externals: {
            "azure-maps-control": "atlas",
            "azure-maps-animations": "atlas",
            "azure-maps-spatial-io": "atlas",
        },
    },

    // library: window global
    {
        entry: './tsgen/atlas.js',
        output: {
            path: path.resolve(__dirname, 'wwwroot'),
            filename: 'atlasGlobal.js',
            library: 'marqdoujAtlasGlobal',
            libraryTarget: 'window', // Browser global
        },
        mode: 'production',
        externalsType: 'var',
        externals: {
            "azure-maps-control": "atlas",
            "azure-maps-animations": "atlas",
            "azure-maps-spatial-io": "atlas",
        },
    }
];
