import { createApp } from 'vue';
import App from '@/App.vue';
import { router } from '@/routes';
import { createPinia } from 'pinia';
// import PrimeVue from 'primevue/config';
// import primevueConfig from '../primevue.config';

import './assets/css/app.css';

const app = createApp(App);
const pinia = createPinia();

// app.use(PrimeVue, { ...primevueConfig });
app.use(pinia);
app.use(router);

app.mount('#app');
