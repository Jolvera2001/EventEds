/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./Pages/**/*.{razor,html}",
    "./Shared/**/*.{razor,html}",
    "./wwwroot/index.html",
    "./**/*.{razor,html}"
  ],
  theme: {
    extend: {},
  },
  plugins: [],
}

