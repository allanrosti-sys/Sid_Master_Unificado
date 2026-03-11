import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    port: 5300, // Porta para o servidor de desenvolvimento do Vite
    strictPort: true, // Garante que a porta 5300 seja usada
  }
})
