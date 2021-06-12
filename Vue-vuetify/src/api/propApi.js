import myaxios from '@/utils/myaxios'
export default {
    getProps() {
        return myaxios({
            url: '/Prop/getProps',
            method: 'get'
        })
    },
    addProp(prop) {
        return myaxios({
            url: '/Prop/addProp',
            method: 'post',
            data: prop
        })
    },
    updateProp(Prop) {
        return myaxios({
            url: '/Prop/UpdateProp',
            method: 'post',
            data: Prop
        })
    },
    delPropList(Ids) {
        return myaxios({
            url: '/Prop/DelPropList',
            method: 'post',
            data: Ids
        })
    },
}