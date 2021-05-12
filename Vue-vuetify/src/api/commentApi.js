import myaxios from '@/utils/myaxios'
export default {
    getComments() {
        return myaxios({
            url: '/Comment/getComments',
            method: 'get'
        })
    },
    getCommentsByDiscussId(discussId, pageIndex) {
        return myaxios({
            url: `/Comment/getCommentsByDiscussId?discussId=${discussId}&pageIndex=${pageIndex}`,
            method: 'get'
        })
    },
    addComment(Comment, discussId) {
        return myaxios({
            url: `/Comment/addComment?discussId=${discussId}`,
            method: 'post',
            data: Comment
        })
    },
    updateComment(Comment) {
        return myaxios({
            url: '/Comment/UpdateComment',
            method: 'post',
            data: Comment
        })
    },
    delCommentList(Ids) {
        return myaxios({
            url: '/Comment/DelCommentList',
            method: 'post',
            data: Ids
        })
    },
}