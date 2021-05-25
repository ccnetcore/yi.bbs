import myaxios from '@/utils/myaxios'
export default {
    login(username, password) {
        return myaxios({
            url: '/Account/login',
            method: 'post',
            data: {
                username,
                password
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
    register(username, password, email, code) {
        return myaxios({
            url: `/Account/register?code=${code}`,
            method: 'post',
            data: { username, password, email }
        })
    },
    email(emailAddress) {
        return myaxios({
            url: `/Account/email?emailAddress=${emailAddress}`,
            method: 'get',
        })
    }

}