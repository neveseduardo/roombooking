import { defineConfig } from 'vite'
import vue from '@vitejs/plugin-vue'
import tailwindcss from '@tailwindcss/vite'
import { resolve } from 'path';

// https://vite.dev/config/
export default defineConfig({
	plugins: [
		vue(),
		tailwindcss(),
	],
	server: {
		port: 8080,
		host: true,
	},
	resolve: {
		alias: { '@': resolve(__dirname, './src') },
	},
})
