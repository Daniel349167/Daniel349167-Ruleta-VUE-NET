import { createApp } from 'vue';
import App from './App.vue';
import router from './router';
import 'bootstrap/dist/css/bootstrap.min.css';
import 'bootstrap-vue-next/dist/bootstrap-vue-next.css';
import BootstrapVueNext, { BModal } from 'bootstrap-vue-next'; // Importar el componente específico

const app = createApp(App);
app.component('b-modal', BModal); // Registrar el componente
app.use(router);
app.use(BootstrapVueNext);
app.mount('#app');
