import myaxios from '@/utils/myaxios'
export default {
    getPlates() {
        return myaxios({
            url: '/Plate/getPlates',
            method: 'get'
        })
    },
    addPlate(plate) {
        return myaxios({
            url: '/Plate/addPlate',
            method: 'post',
            data: plate
        })
    },
    updatePlate(plate) {
        return myaxios({
            url: '/Plate/UpdatePlate',
            method: 'post',
            data: plate
        })
    },
    delPlateList(Ids) {
        return myaxios({
            url: '/Plate/DelPlateList',
            method: 'post',
            data: Ids
        })
    },
}