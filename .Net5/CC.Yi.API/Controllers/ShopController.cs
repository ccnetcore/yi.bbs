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
    public class ShopController : Controller
    {
        private IshopBll _shopBll;
        private ILogger<ShopController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public ShopController(IshopBll shopBll, ILogger<ShopController> logger)
        {
            _shopBll = shopBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetShops()
        {
            var data = await _shopBll.GetEntities(u => u.is_delete == delFlagNormal).Include(u=>u.prop).Select(u=>new { u.id,u.number,u.price,u.discount,  real_price = Math.Floor(u.price*u.discount),prop= new { u.prop.introduction,u.prop.logo,u.prop.name,u.prop.color } }) .ToListAsync();
            return Result.Success().SetData(data);
        }

        [Authorize(Policy = "商城管理")]
        [HttpPost]
        public Result AddShop(shop myShop)
        {
            _shopBll.Add(myShop);
            return Result.Success();
        }

        [Authorize(Policy = "商城管理")]
        [HttpPost]
        public Result UpdateShop(shop myShop)
        {
            _shopBll.Update(myShop);
            return Result.Success();
        }

        [Authorize(Policy = "商城管理")]
        [HttpPost]
        public Result delShopList(List<int> Ids)
        {
            _shopBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}