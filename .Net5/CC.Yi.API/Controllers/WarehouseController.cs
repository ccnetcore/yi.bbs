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
    public class WarehouseController : BaseController
    {
        private IwarehouseBll _warehouseBll;
        private ILogger<WarehouseController> _logger;
        public WarehouseController(IwarehouseBll warehouseBll, ILogger<WarehouseController> logger)
        {
            _warehouseBll = warehouseBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetWarehousesByUserId()
        {
            var data = await _warehouseBll.GetEntities(u => u.user.id == _user.id && u.is_delete== (short)ViewModel.Enum.DelFlagEnum.Normal).Include(r => r.prop).Select(u => new
            {
                u.number,
                prop = new
                {
                    u.prop.name,
                    u.prop.introduction,
                    u.prop.logo,
                    u.prop.color
                }
            }).ToListAsync();
            return Result.Success().SetData(data);
        }
    }
}
