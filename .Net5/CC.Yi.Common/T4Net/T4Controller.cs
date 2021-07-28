


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
    public class MytypeController : Controller
    {
        private ImytypeBll _mytypeBll;
        private ILogger<MytypeController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public MytypeController(ImytypeBll mytypeBll, ILogger<MytypeController> logger)
        {
            _mytypeBll = mytypeBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetMytypes()
        {
            var data = await _mytypeBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result AddMytype(mytype myMytype)
        {
            _mytypeBll.Add(myMytype);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateMytype(mytype myMytype)
        {
            _mytypeBll.Update(myMytype);
            return Result.Success();
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result delMytypeList(List<int> Ids)
        {
            _mytypeBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}

*/