import myaxios from '@/utils/myaxios'
export default {
    getCollections(pageIndex) {
        return myaxios({
            url: `/Collection/GetCollections?pageIndex=${pageIndex}`,
            method: 'get'
        })
    },
    addCollection(discussId) {
        return myaxios({
            url: `/Collection/AddCollection?discussId=${discussId}`,
            method: 'get'
        })
    },
    delCollection(discussId) {
        return myaxios({
            url: `/Collection/delCollection?discussId=${discussId}`,
            method: 'get',
        })
    },
}