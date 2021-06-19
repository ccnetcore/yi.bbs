using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class FriendController : BaseController
    {
        private IfriendBll _friendBll;
        private ILogger<FriendController> _logger;
        private IuserBll _userBll;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public FriendController(IfriendBll friendBll, ILogger<FriendController> logger,IuserBll userBll)
        {
            _friendBll = friendBll;
            _userBll = userBll;
            _logger = logger;
        }
        //is_agr :1代表已经同意，0代表未同意
        //正常好友：is_del为0，is_agr为1 ：未删除，且已经同意
        //好友请求：is_del为0，is_agr为0 ：未删除，等待请求同意
        //同意请求：将好友请求的is_agr：0改为1即可
        //删除好友或者拒绝请求：将好友请求的is_del：0改为1即可

        [HttpGet]//获取自己有哪些好友
        public async Task<Result> GetFriends()
        {
            var data = await _friendBll.GetEntities(u => (u.is_delete == delFlagNormal&& u.is_agree== (short)ViewModel.Enum.AgrFlagEnum.Agree) && (u.user1.id==_user.id || u.user2.id==_user.id)).Include(u=>u.user1).ThenInclude(u=>u.user_extra).Include(u=>u.user2).ThenInclude(u => u.user_extra). ToListAsync();
           
           var filterData= data.Select(u => new { u.id, u.time, user = u.user1.id == _user.id ? u.user2:u.user1 });

            return Result.Success().SetData(filterData);
        }


        [HttpGet]//获取自己有哪些好友通知
        public async Task<Result> GetFriendsNotice()
        {
            var data = await _friendBll.GetEntities(u => (u.is_delete == delFlagNormal && u.is_agree == (short)ViewModel.Enum.AgrFlagEnum.wait) &&  u.user2.id == _user.id).Include(u => u.user1).ThenInclude(u => u.user_extra).Include(u => u.user2).ThenInclude(u => u.user_extra).ToListAsync();

            var filterData = data.Select(u => new { u.id, u.time, user = u.user1.id == _user.id ? u.user2 : u.user1 });

            return Result.Success().SetData(filterData);
        }



        [Authorize(Policy = "好友管理")]
        [HttpPost]//添加好友
        public async Task<Result> AddFriend(string user2Name)
        {
            friend myFriend = new friend
            {
                time = DateTime.Now,
                user1 = await _userBll.GetEntityById(_user.id),
                user2 = await _userBll.GetEntities(u => u.username == user2Name).FirstOrDefaultAsync(),
                is_agree= (short)ViewModel.Enum.AgrFlagEnum.wait
            };
            _friendBll.Add(myFriend);
            return Result.Success();
        }


        [Authorize(Policy = "好友管理")]
        [HttpPost]//同意好友请求
        public Result UpdateFriend(int friendId)
        {
            _friendBll.agreeFriend(friendId);
            return Result.Success();
        }


        [Authorize(Policy = "好友管理")]
        [HttpPost]//删除好友 或 拒绝好友请求
        public Result delFriendList(List<int> Ids)
        {
            _friendBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
