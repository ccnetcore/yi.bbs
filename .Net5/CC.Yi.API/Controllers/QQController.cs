using AspNet.Security.OAuth.QQ;
using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QQController : Controller
    {

        private ILogger<PlateController> _logger;
        private IuserBll _userBll;
        public QQController(ILogger<PlateController> logger,IuserBll userBll)
        {
            _userBll = userBll;
            _logger = logger;
        }
        [HttpGet]
        public async Task<Result> qqlogin(string openid)
        {
            // string oauth_consumer_key = "101951505";
            //string p=HttpHelper.HttpGet($"https://graph.qq.com/user/get_user_info?access_token={access_token}&oauth_consumer_key={oauth_consumer_key}&openid={openid}");
            var user = await _userBll.GetEntities(u => u.openid == openid && u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal).Include(u => u.user_extra).FirstOrDefaultAsync();
            if ( user!=null)
            {

                _logger.LogInformation("QQ登录成功");
            return   await _userBll.login(user);

            }
            _logger.LogInformation("QQ登录失败");
            return Result.Error("请注册账号，进入个人信息中绑定");
        }

        [Authorize(Policy = "绑定QQ")]
        [HttpGet]
        public async Task<Result> qqUpdate(string openid,int userId)
        {
            //先判断openid是否被人注册，另外，userId是否存在
            if (await _userBll.GetEntities(u => u.openid == openid).CountAsync() == 0)
            {
               var myUser= await _userBll.GetEntityById(userId);
                myUser.openid = openid;
                _userBll.Update(myUser);
                return Result.Success("QQ绑定成功");
            }
            else
            {
                return Result.Error("该qq账号已被绑定");
            }
        }
    }
}
