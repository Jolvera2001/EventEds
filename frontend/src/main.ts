import { createApp } from 'vue'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
import App from './App.vue'
import './index.css'
import registerComponents from './plugins/PrimeVueInstall'
import router from './router/router'

const app = createApp(App);
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        ripple: true
    }
});

registerComponents(app);
app.use(router)
app.mount('#app');
