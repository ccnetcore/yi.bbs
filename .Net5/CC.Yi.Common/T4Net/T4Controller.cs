


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
    public class PropController : Controller
    {
        private IpropBll _propBll;
        private ILogger<PropController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public PropController(IpropBll propBll, ILogger<PropController> logger)
        {
            _propBll = propBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetProps()
        {
            var data = await _propBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result AddProp(prop myProp)
        {
            _propBll.Add(myProp);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateProp(prop myProp)
        {
            _propBll.Update(myProp);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result delPropList(List<int> Ids)
        {
            _propBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}

*/