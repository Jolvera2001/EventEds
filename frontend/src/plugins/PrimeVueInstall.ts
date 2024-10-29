import {App} from "vue";
import Button from "primevue/button";
import DatePicker from "primevue/datepicker";
import Card from 'primevue/card'
import InputGroup from 'primevue/inputgroup';
import InputGroupAddon from 'primevue/inputgroupaddon';
import InputText from 'primevue/inputtext';
import FloatLabel from 'primevue/floatlabel';
import Textarea from 'primevue/textarea';




export default function registerComponents(app: App) {
    app.component('Button', Button);
    app.component('DatePicker', DatePicker);
    app.component('Card', Card);
    app.component('InputGroup', InputGroup);
    app.component('InputGroupAddon', InputGroupAddon);
    app.component('InputText', InputText);
    app.component('FloatLabel', FloatLabel);
    app.component('Textarea', Textarea);
}