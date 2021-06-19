import myaxios from '@/utils/myaxios'
export default {
    GetFriends() {
        return myaxios({
            url: '/Friend/GetFriends',
            method: 'get'
        })
    },
    GetFriendsNotice() {
        return myaxios({
            url: '/Friend/GetFriendsNotice',
            method: 'get'
        })
    },
    AddFriend(user2Name) {
        return myaxios({
            url: `/Friend/AddFriend?user2Name=${user2Name}`,
            method: 'post',
        })
    },
    UpdateFriend(friendId) {
        return myaxios({
            url: `/Friend/UpdateFriend?friendId=${friendId}`,
            method: 'post',
        })
    },
    delFriendList(Ids) {
        return myaxios({
            url: '/Friend/delFriendList',
            method: 'post',
            data: Ids
        })
    },
}