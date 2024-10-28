import { createApp } from 'vue'
import PrimeVue from 'primevue/config'
import Aura from '@primevue/themes/aura'
import App from './App.vue'
import registerComponents from './plugins/PrimeVueInstall'


const app = createApp(App);
app.use(PrimeVue, {
    theme: {
        preset: Aura,
        ripple: true
    }
});

registerComponents(app);

app.mount('#app');
