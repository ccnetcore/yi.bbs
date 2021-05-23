using CC.Yi.Common;
using CC.Yi.IBLL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authorization;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class LabelController : BaseController
    {
        private IlabelBll _labelBll;
        private IuserBll _userBll;
        private IdiscussBll _discussBll;
        private ILogger<LabelController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;

        public LabelController(IlabelBll labelBll, ILogger<LabelController> logger, IuserBll userBll, IdiscussBll discussBll)
        {
            _labelBll = labelBll;
            _userBll = userBll;
            _discussBll = discussBll;
            _logger = logger;
        }

        [Authorize(Policy = "标签管理")]
        [HttpGet]
        //得到用户具有哪些标签
        public async Task<Result> getLabelByUserId(int userId)
        {
            if (userId == 0)
            {
                userId = _user.id;
                    }
            var data = await _labelBll.GetEntities(u => u.user.id == userId && u.is_delete == delFlagNormal).ToListAsync();
            return Result.Success().SetData(data);
        }


        [Authorize(Policy = "标签管理")]
        [HttpGet]
        public async Task<Result> getDiscussByLabelId(int userId, int pageIndex, int labelId)
        {
            if (userId == 0)
            {
                userId = _user.id;
            }
            var data = await _labelBll.GetEntities
                (u => u.user.id == userId && u.is_delete == delFlagNormal&&u.id== labelId).Include(u=>u.discusses).ThenInclude(u=>u.user).Include(u => u.discusses).ThenInclude(u =>u.plate)
                .FirstOrDefaultAsync();
            var discuss = data.discusses;
            var dataFilter = from r in discuss
                             select new
                             {
                                 r.id,
                                 r.introduction,
                                 r.time,
                                 r.title,
                                 user = new { r.user?.icon, r.user?.username },
                                 plate = new { r.plate?.id}
                             };
            int pageSize = 10;//每页数量从redis中获取
            int total = dataFilter.Count();
            var pageData = dataFilter
                         .OrderByDescending(u => u.id)  //降序排序
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).AsQueryable();
            return Result.Success().SetData(new { pageData, pageSize, total });
        }

        [Authorize(Policy = "标签管理")]
        [HttpPost]
        //需要用户id和主题id，将这个标签归属与这个用户
        public async Task<Result> AddLabelByUserId(label myLabel)
        {
            myLabel.user = await _userBll.GetEntities(u => u.id == _user.id).FirstOrDefaultAsync();
            _labelBll.Add(myLabel);
            return Result.Success();
        }

        [Authorize(Policy = "标签管理")]
        [HttpPost]
        //通过LabelId删除标签
        public async Task<Result> delLabelList(List<int> Ids)
        {
            await _labelBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}
