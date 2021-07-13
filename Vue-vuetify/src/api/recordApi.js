import myaxios from '@/utils/myaxios'
export default {
    getRecordsByDiscussId(discussId) {
        return myaxios({
            url: `/Record/getRecordsByDiscussId?discussId=${discussId}`,
            method: 'get'
        })
    },

    getRecords() {
        return myaxios({
            url: '/Record/getRecords',
            method: 'get'
        })
    },
    addRecord(record) {
        return myaxios({
            url: '/Record/addRecord',
            method: 'post',
            data: record
        })
    },
    updateRecord(Record) {
        return myaxios({
            url: '/Record/UpdateRecord',
            method: 'post',
            data: Record
        })
    },
    delRecordList(Ids) {
        return myaxios({
            url: '/Record/DelRecordList',
            method: 'post',
            data: Ids
        })
    },
}