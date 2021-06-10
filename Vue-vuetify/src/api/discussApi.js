import myaxios from '@/utils/myaxios'
export default {
    getDiscuss() {
        return myaxios({
            url: '/Discuss/getDiscuss',
            method: 'get'
        })
    },
    getDiscussByPlateId(plateId, pageIndex, orderbyId) {
        return myaxios({
            url: `/Discuss/getDiscussByPlateId?plateId=${plateId}&pageIndex=${pageIndex}&orderbyId=${orderbyId}`,
            method: 'get'
        })
    },
    getDiscussByUserId(userId, pageIndex) {
        if (userId == undefined) {
            userId = 0
        }
        return myaxios({
            url: `/Discuss/getDiscussByUserId?userId=${userId}&pageIndex=${pageIndex}`,
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
    },
    UpdatePorp(disucssId, propId, color) {
        color = color.replace("#", "%23"); //颜色代码替换
        return myaxios({
            url: `/Discuss/UpdatePorp?disucssId=${disucssId}&propId=${propId}&color=${color}`,
            method: 'get'
        })
    }
}