import myaxios from '@/utils/myaxios'
export default {
    qqlogin(openid) {
        return myaxios({
            url: `/qq/qqlogin?openid=${openid}`,
            method: 'get'
        })
    },
    qqupdate(openid, userId) {
        return myaxios({
            url: `/qq/qqupdate?openid=${openid}&userId=${userId}`,
            method: 'get'
        })
    }
}