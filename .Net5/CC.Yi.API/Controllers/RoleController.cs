using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
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
    [ApiController]
    [Route("[controller]/[action]")]
    public class RoleController : BaseController
    {
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        private readonly ILogger<RoleController> _logger;//处理日志相关文件

        private IroleBll _roleBll;
        public RoleController(ILogger<RoleController> logger, IroleBll roleBll)
        {
            _logger = logger;
            _roleBll = roleBll;
        }
        [Authorize(Policy = "角色管理")]
        [HttpGet]//得到所有角色(有效角色)
        public Result getRoles()
        {
            var data = _roleBll.GetAllEntities().AsNoTracking();
            var mydata = (from r in data
                          where r.is_delete == delFlagNormal
                          select r).ToList();
            return Result.Success().SetData(mydata);
        }

        [HttpGet]//通过角色id得到该角色的权限
        public async  Task<Result> GetActionByRoleId(int roleId)
        {

            var roles = await _roleBll.GetEntities(u => u.id == roleId).Include(u=>u.actions).FirstOrDefaultAsync();
            var actions = roles.actions;
            var mydata = (from r in actions
                          where r.is_delete == delFlagNormal
                          select new { 
                          r.id,
                          r.action_name
                          }).ToList();
            return Result.Success().SetData(mydata);  
        }

        [Authorize(Policy = "角色管理")]
        [HttpPost]//设置角色拥有哪些权限
        public async Task<Result> setAction(setByIds mySetAction)
        {
         var data=   await _roleBll.setAction(mySetAction.id, mySetAction.ids);
            return Result.Success().SetData(data);
        }

        [Authorize(Policy = "角色管理")]
        [HttpPost]//添加角色
        public Result AddRole(role data)
        {
            _roleBll.Add(data);
            return Result.Success();
        }

        [Authorize(Policy = "角色管理")]
        [HttpGet]//根据roleid删除角色(逻辑删除)
        public Result DelRole(int roleId)
        {
            var user = _roleBll.GetEntities(u => u.id == roleId).FirstOrDefault();
            user.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
                _roleBll.Update(user);
            return Result.Success();
        }

        [Authorize(Policy = "角色管理")]
        [HttpPost]//修改角色
        public async Task<Result> UpdateRole(role _role)
        {
            var role = await _roleBll.GetEntities(u => u.id == _role.id).FirstOrDefaultAsync();
            role.role_name = _role.role_name;
            _roleBll.Update(role);
            return Result.Success();
        }

        [Authorize(Policy = "角色管理")]
        [HttpPost]//多角色逻辑删除
        public async Task<Result> DelAllRole(List<int> Ids)
        {
            await _roleBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
