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
        public FriendController(IfriendBll friendBll, ILogger<FriendController> logger, IuserBll userBll)
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
            var data = await _friendBll.GetFriends(_user.id).ToListAsync();

            var filterData = data.Select(u => new { u.id, u.time, user = u.user1.id == _user.id ? u.user2 : u.user1 });

            return Result.Success().SetData(filterData);
        }


        [HttpGet]//获取自己有哪些好友通知
        public async Task<Result> GetFriendsNotice()
        {
            var data = await _friendBll.GetFriendsNotice(_user.id).ToListAsync();

            var filterData = data.Select(u => new { u.id, u.time, user = u.user1.id == _user.id ? u.user2 : u.user1 });

            return Result.Success().SetData(filterData);
        }



        [Authorize(Policy = "好友管理")]
        [HttpPost]//添加好友
        public async Task<Result> AddFriend(string user2Name)
        {
            //需要先查询目前是否未好友，并且通知列表是否已经发送

            string msg = "已发送好友请求，等待对方同意";
            bool is_ok = true;

            friend myFriend = new friend
            {
                time = DateTime.Now,
                user1 = await _userBll.GetEntityById(_user.id),
                user2 = await _userBll.GetEntities(u => u.username == user2Name).FirstOrDefaultAsync(),
                is_agree = (short)ViewModel.Enum.AgrFlagEnum.wait
            };

            //user1为请求人
            //user2为被请求人

            //先检测user1的好友是否存在user2
            //再检测user1的好友请求列表是否存在user2，同时user2的还有请求列表中也不能有user1
            var friendData = await _friendBll.GetFriends(_user.id).ToListAsync();//获取到了请求人的所有好友
            foreach (var k in friendData)
            {
                if (k.user1.username == user2Name || k.user2.username == user2Name)
                {
                    msg = "他已成为你的好友，请勿继续添加";
                    is_ok = false;
                    break;
                }
            }

            if (is_ok)
            {
                var friendNotiData1 = await _friendBll.GetFriendsNotice(_user.id).Where(u => u.user1.id == myFriend.user2.id).CountAsync();//获取被请求人为user1的好友请求
                if (friendNotiData1 != 0)
                {
                    msg = "对面已经向你发送了请求，请勿重复发送";
                    is_ok = false;
                }
            }


            if (is_ok)
            {
                var friendNotiData2 = await _friendBll.GetFriendsNotice(myFriend.user2.id).Where(u => u.user1.id == myFriend.user1.id).CountAsync();//获取被请求人为user2的好友请求
                if (friendNotiData2 != 0)
                {
                    msg = "你已经向对方发送了请求，请勿重复发送";
                    is_ok = false;
                }

            }

            if (is_ok)
            {
                _friendBll.Add(myFriend);
                return Result.Success(msg);
            }
            return Result.Error(msg);
        }


        [Authorize(Policy = "好友管理")]
        [HttpPost]//同意好友请求
        public Result UpdateFriend(int friendId)
        {
            _friendBll.agreeFriend(friendId);
            return Result.Success("已同意该好友申请");
        }


        [Authorize(Policy = "好友管理")]
        [HttpPost]//删除好友 或 拒绝好友请求
        public Result delFriendList(List<int> Ids)
        {
            _friendBll.DelListByUpdateList(Ids);
            return Result.Success("已拒绝删除");
        }
    }
}
