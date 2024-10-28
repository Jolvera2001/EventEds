import {App} from "vue";
import Button from "primevue/button";
import DatePicker from "primevue/datepicker";

export default function registerComponents(app: App) {
    app.component('Button', Button);
    app.component('DatePicker', DatePicker)
}