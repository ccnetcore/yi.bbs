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
    public class VersionController : Controller
    {
        private IversionBll _versionBll;
        private ILogger<VersionController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public VersionController(IversionBll versionBll, ILogger<VersionController> logger)
        {
            _versionBll = versionBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetVersions()
        {
            var data =await  _versionBll.GetEntities(u=>u.is_delete==delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data.Reverse<version>());
        }

        [Authorize(Policy = "版本管理")]
        [HttpPost]
        public Result AddVersion(version myVersion)
        {
            _versionBll.Add(myVersion);
            return Result.Success();
        }

        [Authorize(Policy = "版本管理")]
        [HttpPost]
        public Result UpdateVersion(version myVersion)
        {
            _versionBll.Update(myVersion);
            return Result.Success();
        }

        [Authorize(Policy = "版本管理")]
        [HttpPost]
        public Result delVersionList(List<int> Ids)
        {
            _versionBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
