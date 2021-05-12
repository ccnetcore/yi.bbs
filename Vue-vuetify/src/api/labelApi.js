import myaxios from '@/utils/myaxios'
export default {
    getLabelByUserId() {
        return myaxios({
            url: `/Label/getLabelByUserId`,
            method: 'get'
        })
    },
    getDiscussByLabelId(pageIndex, labelId) {
        return myaxios({
            url: `/Label/getDiscussByLabelId?labelId=${labelId}&pageIndex=${pageIndex}`,
            method: 'get'
        })
    },
    getLabels() {
        return myaxios({
            url: '/Label/getLabels',
            method: 'get'
        })
    },
    addLabelByUserId(label) {
        return myaxios({
            url: '/Label/addLabelByUserId',
            method: 'post',
            data: label
        })
    },
    updateLabel(Label) {
        return myaxios({
            url: '/Label/UpdateLabel',
            method: 'post',
            data: Label
        })
    },
    delLabelList(Ids) {
        return myaxios({
            url: '/Label/DelLabelList',
            method: 'post',
            data: Ids
        })
    },
}