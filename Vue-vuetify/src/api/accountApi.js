import myaxios from '@/utils/myaxios'
export default {
    login(username, password) {
        return myaxios({
            url: '/Account/login',
            method: 'post',
            data: {
                username: username,
                password: password
            }
        })
    },
    logout() {
        return myaxios({
            url: '/Account/logout',
            method: 'post',
        })
    },
    logged() {
        return myaxios({
            url: '/Account/logged',
            method: 'post',
        })
    },
    register(user_name, password) {
        return myaxios({
            url: '/Account/register',
            method: 'post',
            data: { user_name: user_name, password: password }
        })
    }

}