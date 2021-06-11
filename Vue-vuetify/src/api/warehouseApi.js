import myaxios from '@/utils/myaxios'
export default {
    GetWarehousesByUserId() {
        return myaxios({
            url: '/Warehouse/GetWarehousesByUserId',
            method: 'get'
        })
    }
}