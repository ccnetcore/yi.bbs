import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import VuetifyDialog from 'vuetify-dialog'
import 'vuetify-dialog/dist/vuetify-dialog.css'
import signalr from './utils/signalR'


// import { mavonEditor } from "mavon-editor";
// import "mavon-editor/dist/css/index.css";

import store from './store/index'
import "./permission"

// Vue.component("mavon-editor", mavonEditor);
Vue.use(signalr);

Vue.config.productionTip = false
Vue.use(VuetifyDialog, {
    context: {
        vuetify
    }
});

new Vue({
    router,
    store,
    vuetify,
    render: function(h) { return h(App) }
}).$mount('#app')