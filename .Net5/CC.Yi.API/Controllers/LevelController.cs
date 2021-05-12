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
    public class LevelController : Controller
    {
        private IlevelBll _levelBll;
        private ILogger<LevelController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public LevelController(IlevelBll levelBll, ILogger<LevelController> logger)
        {
            _levelBll = levelBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetLevels()
        {
            var data = await _levelBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

        [HttpPost]
        public Result AddLevel(level myLevel)
        {
            _levelBll.Add(myLevel);
            return Result.Success();
        }

        [HttpPost]
        public Result UpdateLevel(level myLevel)
        {
            _levelBll.Update(myLevel);
            return Result.Success();
        }

        [HttpPost]
        public Result delLevelList(List<int> Ids)
        {
            _levelBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
