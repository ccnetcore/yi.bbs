import Vue from 'vue'
import App from './App.vue'
import router from './router'
import vuetify from './plugins/vuetify';
import VuetifyDialog from 'vuetify-dialog'
import 'vuetify-dialog/dist/vuetify-dialog.css'
import signalr from './utils/signalR'
// import hljs from 'highlight.js' //导入代码高亮文件
// import 'highlight.js/styles/googlecode.css'


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

// Vue.directive('highlight', function(el) {
//     let highlight = el.querySelectorAll('code,pre');
//     highlight.forEach((block) => {
//         if (block) {
//             hljs.highlightBlock(block);
//         }
//     })
// })

new Vue({
    router,
    store,
    vuetify,
    render: function(h) { return h(App) }
}).$mount('#app')