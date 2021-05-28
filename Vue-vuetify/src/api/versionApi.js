import myaxios from '@/utils/myaxios'
export default {
    getVersions() {
        return myaxios({
            url: '/Version/getVersions',
            method: 'get'
        })
    },
    addVersion(version) {
        return myaxios({
            url: '/Version/addVersion',
            method: 'post',
            data: version
        })
    },
    updateVersion(Version) {
        return myaxios({
            url: '/Version/UpdateVersion',
            method: 'post',
            data: Version
        })
    },
    delVersionList(Ids) {
        return myaxios({
            url: '/Version/DelVersionList',
            method: 'post',
            data: Ids
        })
    },
}