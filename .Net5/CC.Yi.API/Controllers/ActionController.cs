using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
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
    public class ActionController : BaseController
    {
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        private readonly ILogger<ActionController> _logger;//处理日志相关文件
        private IactionBll _actionBll;
        public ActionController(ILogger<ActionController> logger,IactionBll actionBll)
        {
            _logger = logger;
            _actionBll = actionBll;
        }
        [HttpGet]//得到所有权限(有效权限)
        public async Task<Result> getActions()
        {
            var data = _actionBll.GetAllEntities().AsNoTracking();
            var mydata =await (from r in data
                          where r.is_delete == delFlagNormal
                          select r).ToListAsync();
            return Result.Success().SetData(mydata);
        }

        [HttpPost]//添加权限
        public Result AddAction(action data)
        {
            _actionBll.Add(data);
            return Result.Success();
        }

        [HttpGet]//根据actionid删除权限(逻辑删除)
        public Result DelAction(int actionId)
        {
            var user = _actionBll.GetEntities(u => u.id == actionId).FirstOrDefault();
            user.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
            _actionBll.Update(user);
            return Result.Success();
        }
        [HttpPost]//修改权限
        public async Task<Result> UpdateAction(action _action)
        {
            var action = await _actionBll.GetEntities(u => u.id == _action.id).FirstOrDefaultAsync();
            action.action_name = _action.action_name;
            action.router = _action.router;
            action.icon = _action.icon;
            var flag = _actionBll.Update(action);
            return Result.Success();
        }
        [HttpPost]//多权限逻辑删除
        public async Task<Result> DelAllAction(List<int> Ids)
        {
            await _actionBll.DelListByUpdateList(Ids);
            return Result.Success();  
        }
    }
}
