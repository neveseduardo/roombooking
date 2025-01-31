import { createApp } from 'vue';
import App from '@/App.vue';
import { router } from '@/routes';
import { createPinia } from 'pinia';
import './assets/css/app.css';

const app = createApp(App);
const pinia = createPinia();

app.use(pinia);
app.use(router);

app.mount('#app');
