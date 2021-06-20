using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.IBLL
{
    public partial interface IfriendBll 
    {
        //同意好友申请
        Task<bool> agreeFriend(int friendId);

        //拒绝好友申请
        Task<bool> delFriend(int friendId);

        //得到所有好友
        IQueryable<friend> GetFriends(int id);

        //得到所有请求
        IQueryable<friend> GetFriendsNotice(int id);
    }
}
