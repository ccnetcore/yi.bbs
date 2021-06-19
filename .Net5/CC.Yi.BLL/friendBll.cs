using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.BLL
{
    public partial class friendBll : BaseBll<friend>, IfriendBll
    {
        //同意好友申请
        public async Task<bool> agreeFriend(int friendId)
        {
            var friendData = await CurrentDal.GetEntityById(friendId);
            friendData.is_agree = (short)ViewModel.Enum.AgrFlagEnum.Agree;
            return Db.SaveChanges() > 0;
        }

        //拒绝好友申请与删除好友是同一个操作
        public async Task<bool> delFriend(int friendId)
        {
            var friendData = await CurrentDal.GetEntityById(friendId);
            friendData.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
            return Db.SaveChanges() > 0;
        }
    }
}
