import myaxios from '@/utils/myaxios'
export default {
    getShops() {
        return myaxios({
            url: '/Shop/getShops',
            method: 'get'
        })
    },
    addShop(shop, propId) {
        return myaxios({
            url: `/Shop/addShop?propId=${propId}`,
            method: 'post',
            data: shop
        })
    },
    updateShop(Shop, propId) {
        return myaxios({
            url: `/Shop/UpdateShop?propId=${propId}`,
            method: 'post',
            data: Shop
        })
    },
    delShopList(Ids) {
        return myaxios({
            url: '/Shop/DelShopList',
            method: 'post',
            data: Ids
        })
    },
    BuyShop(shopId) {
        return myaxios({
            url: `/Shop/BuyShop?shopId=${shopId}`,
            method: 'get'
        })
    }
}