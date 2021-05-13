import myaxios from '@/utils/myaxios'
export default {
    getDiscuss() {
        return myaxios({
            url: '/Discuss/getDiscuss',
            method: 'get'
        })
    },
    getDiscussByPlateId(plateId, pageIndex) {
        return myaxios({
            url: `/Discuss/getDiscussByPlateId?plateId=${plateId}&pageIndex=${pageIndex}`,
            method: 'get'
        })
    },
    getDiscussByUserId(pageIndex) {
        return myaxios({
            url: `/Discuss/getDiscussByUserId?pageIndex=${pageIndex}`,
            method: 'get'
        })
    },


    addDiscuss(data, plateId, Ids) {
        return myaxios({
            url: `/Discuss/addDiscuss`,
            method: 'post',
            data: {
                data,
                plateId,
                Ids
            }
        })
    },
    updateDiscuss(data) {
        return myaxios({
            url: '/Discuss/UpdateDiscuss',
            method: 'post',
            data: data
        })
    },
    delDiscussList(Ids) {
        return myaxios({
            url: '/Discuss/DelDiscussList',
            method: 'post',
            data: Ids
        })
    },
    getDiscussByDiscussId(id) {
        return myaxios({
            url: `/Discuss/getDiscussByDiscussId?DiscussId=${id}`,
            method: 'get'
        })
    }
}