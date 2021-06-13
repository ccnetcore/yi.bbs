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
    public class ShopController : BaseController
    {
        private IshopBll _shopBll;
        private IpropBll _propBll;
        private IuserBll _userBll;
        private IwarehouseBll _warehouseBll;
        private ILogger<ShopController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public ShopController(IshopBll shopBll, IpropBll propBll, IwarehouseBll warehouseBll, IuserBll userBll, ILogger<ShopController> logger)
        {
            _shopBll = shopBll;
            _propBll = propBll;
            _warehouseBll = warehouseBll;
            _userBll = userBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetShops()
        {
            var data = await _shopBll.GetEntities(u => u.is_delete == delFlagNormal).Include(u => u.prop).Select(u => new { u.id, u.number, u.price, u.discount, real_price = Math.Floor(u.price * u.discount), prop = new { u.prop.introduction, u.prop.logo, u.prop.name, u.prop.color } }).ToListAsync();
            return Result.Success().SetData(data);
        }

        //[Authorize(Policy = "商城管理")]
        [HttpPost]
        public async Task<Result> AddShop(shop myShop, int propId)
        {
            myShop.prop = await _propBll.GetEntityById(propId);
            _shopBll.Add(myShop);
            return Result.Success();
        }

        //[Authorize(Policy = "商城管理")]
        [HttpPost]
        public async Task<Result> UpdateShop(shop myShop, int propId)
        {
            myShop.prop = await _propBll.GetEntityById(propId);
            _shopBll.Update(myShop);
            return Result.Success();
        }

        //[Authorize(Policy = "商城管理")]
        [HttpPost]
        public Result delShopList(List<int> Ids)
        {
            _shopBll.DelListByUpdateList(Ids);
            return Result.Success();
        }

        [HttpGet]// 购买道具，0:最先判断自己的金币是否足够 1：商城数量-1，并记住该道具,如果只剩下1个，直接删除。  2：仓库+1，并添加该道具,如果不存在，直接添加一个数量为1
        public async Task<Result> BuyShop(int shopId)
        {
            //先判断金币是否足够
            var shopData = await _shopBll.GetEntities(u => u.id == shopId).Include(u => u.prop).FirstOrDefaultAsync();
            var propData = shopData.prop;//这个为购买的道具
            if (shopData.number == 1)
            {
                shopData.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
            }
            shopData.number -= 1;
            _shopBll.Update(shopData);
            //另外还需要仓库中添加
            var warehouseData = await _warehouseBll.GetEntities(u => u.user.id == _user.id && u.prop.id == propData.id && u.is_delete == delFlagNormal).FirstOrDefaultAsync();
           
            if (warehouseData == null)
            {
                var userData =await _userBll.GetEntityById(_user.id);
                _warehouseBll.Add(new warehouse { number = 1, prop = propData, user = userData });
            }
            else
            {
                warehouseData.number += 1;
                _warehouseBll.Update(warehouseData);
            }
            return Result.Success("购买道具成功，已自动放入仓库");
        }
    }
}