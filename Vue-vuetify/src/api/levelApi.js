import myaxios from '@/utils/myaxios'
export default {
    getLevels() {
        return myaxios({
            url: '/Level/getLevels',
            method: 'get'
        })
    },
    addLevel(level) {
        return myaxios({
            url: '/Level/addLevel',
            method: 'post',
            data: level
        })
    },
    updateLevel(Level) {
        return myaxios({
            url: '/Level/UpdateLevel',
            method: 'post',
            data: Level
        })
    },
    delLevelList(Ids) {
        return myaxios({
            url: '/Level/DelLevelList',
            method: 'post',
            data: Ids
        })
    },
}