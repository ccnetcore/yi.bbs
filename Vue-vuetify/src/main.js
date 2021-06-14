import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import VuetifyDialog from 'vuetify-dialog'
import 'vuetify-dialog/dist/vuetify-dialog.css'
import store from './store/index'
import "./permission"

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