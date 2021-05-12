import router from './router/index'
// import accountApi from '@/api/accountApi'
router.beforeEach((to, from, next) => {
    const user = localStorage.getItem("user") //获取是否登入
    if (!user) { //如果没有登入
        if (to.path == '/login' || to.path == '/register' || to.path == '/reset_password') {
            next()
        } else {
            next({ path: '/login' })
        }
    } else {
        //如果已经登入，就要询问后端会话是否存在
        // accountApi.logged().then(response => {
        //     // const resp = response.data;
        //     // if (resp.flag) { //如果后端会话也存在，那就不管
        next()
            //         // } else { //如果后端会话不存在，清除前端存储并且跳转到登入界面
            //         //     localStorage.clear()
            //         //     alert("登入已超时，请重新登入")
            //         //     next({ path: '/login' })
            //         // }
            // })
    }
})