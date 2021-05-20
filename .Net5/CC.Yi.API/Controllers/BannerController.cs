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
    public class BannerController : BaseController
    {
        private IbannerBll _bannerBll;
        private ILogger<BannerController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public BannerController(IbannerBll bannerBll, ILogger<BannerController> logger)
        {
            _bannerBll = bannerBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> Getbanners()
        {
            var data =await  _bannerBll.GetEntities(u=>u.is_delete==delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }


        [Authorize(Policy = "横幅管理")]
        [HttpPost]
        public Result Addbanner(banner mybanner)
        {
            _bannerBll.Add(mybanner);
            return Result.Success();
        }


        [Authorize(Policy = "横幅管理")]
        [HttpPost]
        public Result Updatebanner(banner mybanner)
        {
            _bannerBll.Update(mybanner);
            return Result.Success();
        }

        [Authorize(Policy = "横幅管理")]
        [HttpPost]
        public Result delbannerList(List<int> Ids)
        {
            _bannerBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
