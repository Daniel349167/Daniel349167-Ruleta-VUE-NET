import { createRouter, createWebHistory } from 'vue-router';
import InicioJuego from '../components/InicioJuego.vue';
import JuegoRuleta from '../components/JuegoRuleta.vue';

const routes = [
  { path: '/', component: InicioJuego },
  { path: '/juego', name: 'JuegoRuleta', component: JuegoRuleta, props: true },
];

const router = createRouter({
  history: createWebHistory(process.env.BASE_URL),
  routes,
});

export default router;
