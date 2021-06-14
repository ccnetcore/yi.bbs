import axios from 'axios'
import store from '../store/index'
// import VuetifyDialogPlugin from 'vuetify-dialog/nuxt/index';
const myaxios = axios.create({
        // baseURL:'/'// 
        baseURL: process.env.VUE_APP_BASE_API, // /dev-apis
        timeout: 50000,
        headers: {
            'Authorization': 'Bearer ' + ""
        },
    })
    // 请求拦截器
myaxios.interceptors.request.use(function(config) {
    config.headers.Authorization = 'Bearer ' + store.state.user.token;
    store.dispatch("openLoad");
    return config;
}, function(error) {
    return Promise.reject(error);
});

// 响应拦截器
myaxios.interceptors.response.use(function(response) {
    const resp = response.data
    switch (resp.code) {
        case 200:
            break;
        case 401:
            alert("权限不足");
            break;
        case 403:
            alert("权限不足");
            break;
        case 404:
            console.log("未找到");
            break;
        default:
            console.log("未知原因");
    };
    store.dispatch("closeLoad");
    return resp;
}, function(error) {
    console.log(error.response, error.response.status)
    store.dispatch("closeLoad");
    return Promise.reject(error);
});
export default myaxios