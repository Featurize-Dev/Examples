/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./**/*.razor"],
    theme: {
        colors: {
            'green': {
                '50': '#ecfffd',
                '100': '#bdfffe',
                '200': '#7bfffe',
                '300': '#31ffff',
                '400': '#00fff6',
                '500': '#00eddd',
                '600': '#00bfb7',
                '700': '#009793',
                '800': '#007776',
                '900': '#005e5d',
                '950': '#003c3d',
            },
            'yellow': {
                '50': '#fcfbea',
                '100': '#f9f4c8',
                '200': '#f5e793',
                '300': '#eed456',
                '400': '#e8bf2b',
                '500': '#d8a71a',
                '600': '#ba8114',
                '700': '#955e13',
                '800': '#7b4a18',
                '900': '#693e1a',
                '950': '#3d200b',
            }
        },
        extend: {}
    },
    plugins: [],
}