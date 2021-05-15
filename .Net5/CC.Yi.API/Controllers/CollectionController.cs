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
    public class CollectionController : BaseController
    {
        private IcollectionBll _collectionBll;
        private IdiscussBll _discussBll;
        private IuserBll _userBll;
        private ILogger<CollectionController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public CollectionController(IcollectionBll collectionBll, ILogger<CollectionController> logger,IdiscussBll discussBll,IuserBll userBll)
        {
            _collectionBll = collectionBll;
            _discussBll = discussBll;
            _userBll = userBll;
            _logger = logger;
        }


        //得到用户的收藏
        [HttpGet]
        public async Task<Result> GetCollections(int pageIndex)
        {
            var data =await  _collectionBll.GetEntities(u=>u.is_delete==delFlagNormal && u.user.id==_user.id).Include(u=>u.discuss).ThenInclude(u=>u.user).Include(u => u.discuss).ThenInclude(u =>u.plate).ToListAsync();

            var discussList =(from k in data
                              select k.discuss).ToList();
            int pageSize = 10;//每页数量从redis中获取

            int total = discussList.Count();
            var pageData = discussList
                         .OrderByDescending(u => u.id)  //降序排序
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).ToList();
            var dataFilter = from u in pageData
                             select new
                             {
                                 u.id,
                                 u.content,
                                 u.introduction,
                                 u.time,
                                 u.type,
                                 u.title,
                                 user=new { 
                                 u.user?.id,
                                 u.user?.username,
                                 u.user?.icon
                                 },
                                 plate = new { u.plate?.id }
                             };

            return Result.Success().SetData(new { pageData= dataFilter, pageSize, total });
        }

        //添加收藏,要包含主题id
        [HttpGet]
        public async Task<Result> AddCollection(int discussId)
        {
            //先判断是否存在
            if (_collectionBll.GetEntities(u => u.discuss.id == discussId && u.user.id == _user.id && u.is_delete == delFlagNormal).Count() > 0)
            {
                return Result.Error("你已经收藏，无需重复收藏");
            }
            collection mycollection = new collection();
            mycollection.time = DateTime.Now;
            mycollection.discuss = await _discussBll.GetEntityById(discussId);
            mycollection.user = await _userBll.GetEntityById(_user.id);
            _collectionBll.Add(mycollection);
            return Result.Success("收藏成功，可在 我的收藏 中查看");
        }

        //删除收藏
        [HttpGet]
        public async Task<Result> delCollection(int discussId)
        {
         var data= await   _collectionBll.GetEntities(u => u.discuss.id == discussId && u.user.id == _user.id && u.is_delete == delFlagNormal).FirstOrDefaultAsync();
            data.is_delete = (short)ViewModel.Enum.DelFlagEnum.Deleted;
            _collectionBll.Update(data);
            return Result.Success();
        }
    }
}
