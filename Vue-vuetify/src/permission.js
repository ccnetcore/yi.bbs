import router from './router/index'
import store from './store/index'
// import accountApi from '@/api/accountApi'


router.beforeEach((to, from, next) => {
    const user = store.state.user.user; //获取是有user
    if (!user) { //如果没有登入
        if (to.path == '/login' || to.path == '/register' || to.path == '/reset_password' || to.path == '/qq') {
            next();
        } else {
            next({ path: '/login' });
        }
    } else { //如果有user还要向后端请求是否过期
        store.dispatch("Logged").then(resp => {
            if (!resp.status) //表示已经过期
            {
                store.dispatch("Logout");
                next({ path: '/login' });
            } else {
                next();
            }
        })
    }
})