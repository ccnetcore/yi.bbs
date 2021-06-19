using CC.Yi.Model;
using System;
using System.Collections.Generic;
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
    }
}
