import { defineConfig } from 'vite'
import react from '@vitejs/plugin-react'

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [react()],
  server: {
    // Configuração de proxy apenas para dev (npm run dev) na porta 5300
    // Em produção (dotnet run), o próprio ASP.NET serve os arquivos.
    port: 5300,
    proxy: {
      '/api': {
        target: 'http://localhost:5301',
        changeOrigin: true
      }
    }
  }
})