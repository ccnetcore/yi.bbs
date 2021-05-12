import Vue from 'vue'
import Vuex from 'vuex'
import home from './modules/home'
import user from './modules/user'
Vue.use(Vuex);

//实例化
const store = new Vuex.Store({
    modules: {
        home,
        user
    }
})
export default store