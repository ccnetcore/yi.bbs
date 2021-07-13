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
    public class RecordController : Controller
    {
        private IrecordBll _recordBll;
        private ILogger<RecordController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public RecordController(IrecordBll recordBll, ILogger<RecordController> logger)
        {
            _recordBll = recordBll;
            _logger = logger;
        }


        [HttpGet]
        public async Task<Result> GetRecordsByDiscussId(int discussId)
        {
           var data=  await _recordBll.GetEntities(u => u.discuss.id == discussId && u.is_delete == delFlagNormal).Include(r=>r.user). ToListAsync();
            return Result.Success().SetData(data);
        }


        [HttpGet]
        public async Task<Result> GetRecords()
        {
            var data = await _recordBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result AddRecord(record myRecord)
        {
            _recordBll.Add(myRecord);
            return Result.Success();
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateRecord(record myRecord)
        {
            _recordBll.Update(myRecord);
            return Result.Success();
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result delRecordList(List<int> Ids)
        {
            _recordBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
