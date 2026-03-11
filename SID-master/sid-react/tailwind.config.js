/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      colors: {
        brand: '#004669', // Exemplo de cor institucional
        brandLight: '#005f8f'
      }
    },
  },
  plugins: [],
}