/** @type {import('tailwindcss').Config} */
module.exports = {
    mode: "jit",
    content: [
        "./index.html",
        "./**/*.{fs,js,ts,jsx,tsx}",
    ],
    theme: {
        fontFamily: {
            sans: ["Inter", "sans-serif"],
        },
        extend: {},
    },
    plugins: []
}
