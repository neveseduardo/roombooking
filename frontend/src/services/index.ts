import axios, { type AxiosInstance, type InternalAxiosRequestConfig } from 'axios';

const axiosInstance: AxiosInstance = axios.create({ baseURL: import.meta.env.VITE_APP_API_URL ?? '/' });

axiosInstance.interceptors.request.use((config: InternalAxiosRequestConfig): InternalAxiosRequestConfig => {

	if (config.headers['Content-Type'] !== 'multipart/form-data') {
		config.headers['Content-Type'] = 'application/json';
		config.headers['Accept'] = 'application/json';
	}

	return config;
});

export default axiosInstance;
