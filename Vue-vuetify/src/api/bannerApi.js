import myaxios from '@/utils/myaxios'
export default {
    getBanners() {
        return myaxios({
            url: '/Banner/getBanners',
            method: 'get'
        })
    },
    addBanner(banner) {
        return myaxios({
            url: '/Banner/addBanner',
            method: 'post',
            data: banner
        })
    },
    updateBanner(Banner) {
        return myaxios({
            url: '/Banner/UpdateBanner',
            method: 'post',
            data: Banner
        })
    },
    delBannerList(Ids) {
        return myaxios({
            url: '/Banner/DelBannerList',
            method: 'post',
            data: Ids
        })
    },
}