import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import tailwindcss from '@tailwindcss/vite';
import { resolve } from 'path';
import Components from 'unplugin-vue-components/vite';
import AutoImport from 'unplugin-auto-import/vite';
import { PrimeVueResolver } from 'unplugin-vue-components/resolvers';

// https://vite.dev/config/
export default defineConfig({
	plugins: [
		vue(),
		tailwindcss(),
		AutoImport({
			include: [
				/\.[tj]sx?$/,
				/\.vue$/,
				/\.vue\?vue/,
			],
			dirs: ['src'],
			dts: './src/@types/auto-imports.d.ts',
			eslintrc: {
				enabled: true,
				filepath: './src/shared/.eslint-globals.json',
				globalsPropValue: true,
			},
			imports: ['vue', 'vue-router'],
		}),
		Components({
			dts: './src/@types/components.d.ts',
			dirs: ['src/components'],
			resolvers: [PrimeVueResolver()],
		}),
	],
	server: {
		port: 8080,
		host: true,
	},
	resolve: {
		alias: { '@': resolve(__dirname, './src') },
	},
});
