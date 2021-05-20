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
    public class AgreeController : BaseController
    {
        private IagreeBll _agreeBll;
        private IuserBll _userBll;
        private ILogger<AgreeController> _logger;
        private IdiscussBll _discussBll;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public AgreeController(IagreeBll agreeBll, IdiscussBll discussBll, IuserBll userBll, ILogger<AgreeController> logger)
        {
            _agreeBll = agreeBll;
            _discussBll = discussBll;
            _userBll = userBll;
            _logger = logger;
        }


       
        [Authorize(Policy = "点赞管理")]
        [HttpGet]
        public async Task<Result> GetAgrees(int discussId)
        {
            string msg = "";
            var data = await _agreeBll.GetEntities(u => u.is_delete == delFlagNormal && u.discuss.id == discussId && u.user.id == _user.id).FirstOrDefaultAsync();
            var discussData = await _discussBll.GetEntityById(discussId);
            //如果发现该用户并未给该主题点赞，即向主题点赞数+1，并给自己添加一个点赞的数据
            if (data == null)
            {

                discussData.agree_num += 1;
                //_discussBll.Update(discussData);

                var userData = await _userBll.GetEntityById(_user.id);

                _agreeBll.Add(new agree { discuss = discussData, user = userData });
                msg = "点赞成功，点赞人数+1";
            }
            else
            //否则用户已经点赞，现在要取消，给主题点赞数-1，并且删除自己点赞的记录
            {

                discussData.agree_num -= 1;
                _discussBll.Update(discussData);
                await _agreeBll.DelListByUpdateList(new List<int>() { data.id });
                msg = "已取消点赞，点赞人数-1";
            }

            return Result.Success(msg).SetData(discussData.agree_num);
        }
    }
}
