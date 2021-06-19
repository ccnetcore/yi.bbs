


/*
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
    public class FriendController : Controller
    {
        private IfriendBll _friendBll;
        private ILogger<FriendController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public FriendController(IfriendBll friendBll, ILogger<FriendController> logger)
        {
            _friendBll = friendBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetFriends()
        {
            var data = await _friendBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result AddFriend(friend myFriend)
        {
            _friendBll.Add(myFriend);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateFriend(friend myFriend)
        {
            _friendBll.Update(myFriend);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result delFriendList(List<int> Ids)
        {
            _friendBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}

*/