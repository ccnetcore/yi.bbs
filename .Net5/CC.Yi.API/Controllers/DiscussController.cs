using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class DiscussController : BaseController
    {
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        private readonly ILogger<DiscussController> _logger;//处理日志相关文件

        private IdiscussBll _discussBll;
        private IuserBll _userBll;
        private IplateBll _plateBll;
        private IlabelBll _labelBll;
        private Iuser_extraBll _user_extraBll;
        public DiscussController(ILogger<DiscussController> logger, IdiscussBll discussBll, IuserBll userBll, IplateBll plateBll, Iuser_extraBll user_extraBll,IlabelBll labelBll)
        {
            _logger = logger;
            _discussBll = discussBll;
            _plateBll = plateBll;
            _userBll = userBll;
            _labelBll = labelBll;
            _user_extraBll = user_extraBll;
        }

        [Authorize(Policy = "主题管理")]
        [HttpGet]//得到所有讨论主题(有效)
        public async Task<Result> getDiscuss()
        {
            List<string> myActions = new List<string>();
            Expression<Func<plate, bool>> where = r => r.is_delete == delFlagNormal && myActions.Contains(r.name);
            foreach (var k in _user.actions)
            {
                if (k.action_name == "a-admin")
                {
                    where = r => r.is_delete == delFlagNormal;
                    break;
                }
                else if (k.action_name.Substring(0, 2) == "a-")
                {
                    myActions.Add(k.action_name.Substring(2));
                }
            }
            var data = await _plateBll.GetEntities(where).Include(u => u.discusses).ToListAsync();
            if (data == null)
            {
                return Result.Success();
            }
            //有权限才往下走
            List<discuss> discussList = new List<discuss>();
            foreach (var k in data)
            {
                foreach (var p in k.discusses)
                {
                    p.plate = null;
                    discussList.Add(p);
                }
            }

            return Result.Success().SetData(discussList);
        }

        [HttpGet]//根据用户id得到全部主题(有效)
        public async Task<Result> getDiscussByUserId( int pageIndex)
        {
            //var myPlate = await _plateBll.GetEntities(u =>   == plateId).Include(u => u.discusses).ThenInclude(u => u.user).FirstOrDefaultAsync();
            var myDiscuss = await _discussBll.GetEntities(u => u.user.id == _user.id && u.is_delete == delFlagNormal).Include(u=>u.user).Include(u=>u.plate) .ToListAsync();
            //数据筛选
            var dataFilter = (from r in myDiscuss
                        select new
                        {
                            r.id,
                            r.content,
                            r.time,
                            r.title,
                            r.type,
                            plate = new { r.plate?.id },
                            user = new { r.user?.username, r.user?.icon }
                        }).ToList();
            //现在升级一下，开始分页
            int pageSize = 10;//每页数量从redis中获取

            //LinqHelper.GetPageEntities(dataFilter, pageSize, out int total);

            int total = dataFilter.Count();
            var pageData = dataFilter
                         .OrderByDescending(u => u.id)  //降序排序
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).AsQueryable();


            return Result.Success().SetData(new { pageData, pageSize, total });
        }


        [HttpGet]//根据板块Id得到主题(有效)
        public async Task<Result> getDiscussByPlateId(int plateId, int pageIndex)
        {
            if (plateId == 0)
            {
                return Result.Error();
            }
            var myPlate = await _plateBll.GetEntities(u => u.id == plateId).Include(u => u.discusses).ThenInclude(u => u.user).FirstOrDefaultAsync();
            var myDiscuss = (from r in myPlate.discusses
                             where r.is_delete == delFlagNormal
                             select r).ToList();

            var dataFilter = (from r in myDiscuss
                              select new
                              {
                                  r.id,
                                  r.introduction,
                                  r.time,
                                  r.title,
                                  r.type,
                                  user = new { r.user?.username, r.user?.icon }
                              }).ToList();


            int pageSize = 10;//每页数量从redis中获取

            int total = dataFilter.Count();
            var pageData = dataFilter
                         .OrderByDescending(u => u.id)  //降序排序
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).AsQueryable();


            return Result.Success().SetData(new { pageData, pageSize, total } );
        }

        [HttpGet]
        public async Task<Result> getDiscussByDiscussId(int discussId)
        {
            if (discussId == 0)
            {
                return Result.Error();
            }
            var data = await _discussBll.GetEntities(u => u.id == discussId).Include(u => u.user).FirstOrDefaultAsync();

            return Result.Success().SetData(new { data.id,data.introduction,data.content,data.time,data.title,user=new {data.user?.username,data.user?.icon } });
        }

        [Authorize(Policy = "发布主题")]
        [HttpPost]//添加主题
        public async Task<Result> AddDiscuss(addDiscussModel myModel)
        {
            int plateId = myModel.plateId;
            List<int> Ids = myModel.Ids;
            discuss data = myModel.data;
            if (plateId == 0)
            {
                return Result.Error();
            }
            var plateData = await _plateBll.GetEntities(u => u.id == plateId).Include(u => u.discusses).ThenInclude(u => u.user).Include(u => u.discusses).ThenInclude(u => u.labels).FirstOrDefaultAsync();
            data.time = DateTime.Now;
            data.user = await _userBll.GetEntities(u => u.id == _user.id).FirstOrDefaultAsync();

            data.labels = await _labelBll.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
            plateData.discusses.Add(data);

            _plateBll.Update(plateData);

            //注意，这里经验可以从redis中获取，现在先设置一个1定值
            int level = await _user_extraBll.UpdateExperience(data.user.id, 1, false);

            return Result.Success().SetData(new { data.id, level });
        }

        [Authorize(Policy = "主题管理")]
        [HttpPost]
        public async Task<Result> UpdateDiscuss(discuss myDiscuss)
        {
            var data = await _discussBll.GetEntities(u => u.id == myDiscuss.id).FirstOrDefaultAsync();
            data.title = myDiscuss.title;
            data.type = myDiscuss.type;
            _discussBll.Update(data);
            return Result.Success();
        }

        [Authorize(Policy = "主题管理")]
        [HttpPost]//多逻辑删除
        public async Task<Result> DelDiscussList(List<int> Ids)
        {
            await _discussBll.DelListByUpdateList(Ids);
            return Result.Success();
        }

      
    }
}
