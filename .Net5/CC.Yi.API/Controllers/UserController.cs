using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : BaseController
    {

        private readonly ILogger<UserController> _logger;//处理日志相关文件

        private IuserBll _userBll;
        private IHttpContextAccessor _httpContext;
        public UserController(ILogger<UserController> logger, IHttpContextAccessor httpContext, IuserBll userBll)
        {
            _logger = logger;
            _userBll = userBll;
            _httpContext = httpContext;
        }

        [Authorize(Policy = "用户管理")]
        [HttpGet]//得到所有有效用户
        public async Task<Result> getAllUser()
        {
            var data = await _userBll.GetAllEntities().AsNoTracking().Where(u => u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal).ToListAsync();

            return Result.Success().SetData(data); 
        }

        [HttpGet]//得到用户所具有的全部权限：正常权限+特殊权限
        public async Task<Result> getActionByUserId(int userId)
        {
            if (userId == 0)
            {
                userId = _user.id;
            }
            var data = await _userBll.getActionByUserId(userId);
            return Result.Success().SetData(data);
        }
        [HttpGet]//得到用户所具有的特殊权限
        public async Task<Result> getSpecialAction(int userId)
        {
            var data = await _userBll.GetEntities(u => u.id == userId).Include(u => u.actions).FirstOrDefaultAsync();
            var mydata = (from u in data.actions
                          where u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal
                          select new { u.id, u.action_name }).ToList();
            return Result.Success().SetData(mydata); 
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//添加用户
        public Result addUser(user _user)
        {
            _user.user_extra = new user_extra();
            _userBll.Add(_user);
            return Result.Success();
        }

        [HttpGet]//得到该id的用户(如果没有传值，就返回当前用户的值)
        public async Task<Result> getUserByUserId(int userId)
        {
            if (userId == 0)
            {
                userId = _user.id;
            }
            var data = await _userBll.GetEntities(u => u.id == userId).Include(u=>u.user_extra). FirstOrDefaultAsync();
          
            return Result.Success().SetData(new { data.id,data.email,data.icon,data.nick,data.username,user_extra = new {data.user_extra.introduction ,data.user_extra.experience,data.user_extra.level,data.user_extra.num_release,data.user_extra.num_reply} });
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//给用户设置角色
        public async Task<Result> setRole(setByIds mySetRole)
        {
            await _userBll.setRole(mySetRole.id, mySetRole.ids);
            return Result.Success();
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//给用户设置特殊权限
        public async Task<Result> setSpecialAction(setByIds mySetSpecialAction)
        {
            await _userBll.setSpecialAction(mySetSpecialAction.id, mySetSpecialAction.ids);
            return Result.Success();
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//多用户逻辑删除
        public async Task<Result> DelAllUser(List<int> Ids)
        {
            await _userBll.DelListByUpdateList(Ids);
            return Result.Success(); 
        }

        [Authorize(Policy = "用户管理")]
        [HttpGet]//根据userid删除用户(逻辑删除)
        public Result DelUser(int userId)
        {
            var user = _userBll.GetEntities(u => u.id == userId).FirstOrDefault();
            user.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                _userBll.Update(user);
            return Result.Success(); 
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//修改用户
        public async Task<Result> UpdateUser(user _user)
        {
            var user = await _userBll.GetEntities(u => u.id == _user.id).FirstOrDefaultAsync();
            user.username = _user.username;
            user.ip = _user.ip;
            user.password = _user.password;
            user.icon = _user.icon;
            user.nick = _user.nick;
            user.openid = _user.openid;
            user.email = _user.email;
            _userBll.Update(user);
            return Result.Success(); 
        }

        [Authorize(Policy = "修改信息")]
        [HttpPost]
        public async Task<Result> TryUpdateUser(userTry myUserTry)
        {
            var user = await _userBll.GetEntities(u => u.id == _user.id).Include(u=>u.user_extra).FirstOrDefaultAsync();
            if (myUserTry.password !="" && myUserTry.password!=null )//表示没有输入原密码，直接修改其他属性
            {
                if (user.password != myUserTry.password)
                {
                    return Result.Error("原密码错误");
                }
                user.password = myUserTry.password_new;
            }
            user.nick = myUserTry.nick;
            user.username = myUserTry.username;
            user.icon = myUserTry.icon;
            user.user_extra.introduction = myUserTry.user_extra.introduction;
            _userBll.Update(user);
            _logger.LogInformation(_user.username + "成功修改信息");
            return Result.Success("修改成功");

        }

     
        [HttpGet]//通过Id得到该用户有哪些角色
        public async Task<Result> getRoleByuserId(int userId)
        {
            if (userId == 0)
            {
                userId = _user.id;
            }
            var user = await _userBll.GetEntities(u => u.id == userId).Include(u => u.roles).FirstOrDefaultAsync();
            var roles = (from u in user.roles
                         where u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal
                         select new { u.id, u.role_name }).ToList();
            return Result.Success().SetData(roles);
        }

        [Authorize(Policy = "用户管理")]
        [HttpPost]//批量设置权限
        public async Task<Result> setRoleList(setRoleList mydata)
        {
            await _userBll.setRoleList(mydata);
            return Result.Success();
        }
    }
}
