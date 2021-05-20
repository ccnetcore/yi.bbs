import myaxios from '@/utils/myaxios'
export default {
    getAgrees(discussId) {
        return myaxios({
            url: `/Agree/getAgrees?discussId=${discussId}`,
            method: 'get'
        })
    },
}