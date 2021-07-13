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
        private IwarehouseBll _warehouseBll;
        private IrecordBll _recordBll;
        public DiscussController(ILogger<DiscussController> logger, IdiscussBll discussBll, IuserBll userBll, IplateBll plateBll, Iuser_extraBll user_extraBll, IlabelBll labelBll,IwarehouseBll warehouseBll,IrecordBll recordBll)
        {
            _logger = logger;
            _discussBll = discussBll;
            _plateBll = plateBll;
            _userBll = userBll;
            _labelBll = labelBll;
            _user_extraBll = user_extraBll;
            _warehouseBll = warehouseBll;
            _recordBll = recordBll;
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
                    if (p.is_delete == (short)delFlagNormal)
                    {
                        p.plate = null;
                        discussList.Add(p);
                    }
                }
            }



            return Result.Success().SetData(discussList);
        }

        [HttpGet]//根据用户id得到全部主题(有效),如果没有传入用户，那就默认自己已登录用户
        public async Task<Result> getDiscussByUserId(int userId, int pageIndex)
        {
            if (userId == 0)
            {
                userId = _user.id;
            }
            var myDiscuss = await _discussBll.GetEntities(u => u.user.id == userId && u.is_delete == delFlagNormal).Include(u => u.user).Include(u => u.plate).ToListAsync();
            //数据筛选
            var dataFilter = (from r in myDiscuss
                              select new
                              {
                                  r.id,
                                  r.time,
                                  r.title,
                                  r.type,
                                  r.agree_num,
                                  r.see_num,
                                  r.color,
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


        [HttpGet]//根据板块Id得到主题(有效)，注意：由于道具卡的出现，现在要做特殊处理
        public async Task<Result> getDiscussByPlateId(int plateId, int pageIndex, int orderbyId)
        {
            if (plateId == 0)
            {
                return Result.Error();
            }
            Func<discuss, int> orderbyLambda = r => r.id;//根据条件进行排序操作
            switch (orderbyId)
            {
                case 1: orderbyLambda = r => r.see_num; break;
                case 2: orderbyLambda = r => r.agree_num; break;
            }
            var myPlate = await _plateBll.GetEntities(u => u.id == plateId).Include(u => u.discusses).ThenInclude(u => u.user).FirstOrDefaultAsync();

            var myDiscuss = myPlate.discusses.Where(r => r.is_delete == delFlagNormal && r.is_top == delFlagNormal).OrderByDescending(orderbyLambda);//降序排序，这里地方是所有没置顶的帖子

            var topDiscuss = myPlate.discusses.Where(r => r.is_delete == delFlagNormal &&  r.is_top == (short)ViewModel.Enum.DelFlagEnum.Deleted).OrderByDescending(orderbyLambda).ToList();//这里是放置顶的帖子


            foreach (var k in topDiscuss)//将置顶的帖子加入前缀
            {
                k.type = "置顶][" + k.type;
            }
            //这里修正一下,应该先分页再合并,最后再进行过滤


            int pageSize = settingHelper.discussPage();//每页数量从redis中获取

            int total = myDiscuss.Count();
            var pageData = myDiscuss
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).ToList();
            //查询前面全部没有加进内存中，从这一步加入内存.

            //这里添加置顶的帖子

         

            topDiscuss.AddRange(pageData);
            

            //最后再过滤
            var dataFilter = (from r in topDiscuss
                              select new
                              {
                                  r.id,
                                  r.introduction,
                                  r.time,
                                  r.title,
                                  r.type,
                                  r.see_num,
                                  r.agree_num,
                                  r.color,
                                  user = new { r.user?.id, r.user?.username, r.user?.icon }
                              }).ToList();




            return Result.Success().SetData(new { dataFilter, pageSize, total });
        }

        [HttpGet]
        public async Task<Result> getDiscussByDiscussId(int discussId)
        {
            if (discussId == 0)
            {
                return Result.Error();
            }
            var data = await _discussBll.GetEntities(u => u.id == discussId).Include(u => u.user).FirstOrDefaultAsync();

            data.see_num += 1;
            _discussBll.Update(data);
            //查阅数+1
            return Result.Success().SetData(new { data.id, data.introduction, data.content, data.time, data.title, data.agree_num, user = new { data.user?.id, data.user?.username, data.user?.icon } });
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

            //发帖数+=1
            var myUser = await _userBll.GetEntities(u => u.id == _user.id).Include(u => u.user_extra).FirstOrDefaultAsync();
            myUser.user_extra.num_release += 1;

            data.user = myUser;

            data.labels = await _labelBll.GetEntities(u => Ids.Contains(u.id)).ToListAsync();
            plateData.discusses.Add(data);

            _plateBll.Update(plateData);

            int Experience = settingHelper.discussExperience();
            int level = await _user_extraBll.UpdateExperience(data.user.id, Experience, false);

            _logger.LogInformation(_user.username + "发布了一篇主题。主题Id：" + myModel.data.id+"；主题标题："+myModel.data.title);
            return Result.Success().SetData(new { data.id, level });
        }

        [Authorize(Policy = "主题管理")]
        [HttpPost]
        public async Task<Result> UpdateDiscuss(discuss myDiscuss)
        {

            if (discussAction.is_allow(_user, myDiscuss.id))
            {
                var data = await _discussBll.GetEntities(u => u.id == myDiscuss.id).FirstOrDefaultAsync();
                data.title = myDiscuss.title;
                data.type = myDiscuss.type;
                data.content = myDiscuss.content;
                _discussBll.Update(data);
                return Result.Success();
            }
            else
            {
                return Result.Error("权限不足");
            }
        }

        [Authorize(Policy = "主题管理")]
        [HttpPost]//多逻辑删除
        public async Task<Result> DelDiscussList(List<int> Ids)
        {
            await _discussBll.DelListByUpdateList(Ids);
            return Result.Success();
        }

        [Authorize(Policy = "使用道具")]
        [HttpGet]//使用道具
        public async Task<Result> UpdatePorp(int disucssId, int propId, string color)
        {
            //先往仓库里查是否存在，如果存在，更新数量-1，如果数量就是为1，那直接逻辑删除

            if (await _warehouseBll.setWarehouse(_user.id, propId))
            {
                await _discussBll.setProp(disucssId, propId, color);
                _logger.LogInformation(_user.username + "为主题使用道具。主题Id：" + disucssId + "；道具Id：" + propId);
                return Result.Success("道具使用成功！已从仓库中获取！");
            }

            
            return Result.Error("仓库道具不足，请前往商城中购买!");
        }


    }
}
