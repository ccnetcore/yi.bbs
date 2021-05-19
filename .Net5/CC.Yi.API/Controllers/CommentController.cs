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
    public class CommentController : BaseController
    {
        private IcommentBll _commentBll;
        private IdiscussBll _discussBll;
        private Iuser_extraBll _user_extraBll;
        private IuserBll _userBll;
        private ILogger<CommentController> _logger;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public CommentController(IcommentBll commentBll, ILogger<CommentController> logger, IdiscussBll discussBll, IuserBll userBll,Iuser_extraBll user_extraBll )
        {
            _commentBll = commentBll;
            _discussBll = discussBll;
            _user_extraBll = user_extraBll;
            _userBll = userBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetComments()
        {
            var data = await _commentBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

        [HttpGet]
        public async Task<Result> getCommentsByDiscussId(int discussId,int pageIndex)
        {
            if (discussId == 0)
            {
                return Result.Error();
            }
            var discussData = await _discussBll.GetEntities(u => u.id == discussId).Include(u => u.comments).ThenInclude(u => u.user).FirstOrDefaultAsync();
            var commentList = from r in discussData.comments
                              where r.is_delete == delFlagNormal
                              select r;

            var dataFilter = from r in commentList
                             select new
                             {
                                 r.id,
                                 r.time,
                                 r.content,
                                 user = new { r.user?.username, r.user?.id, r.user?.icon }
                             };

            //现在升级一下，开始分页
            int pageSize = settingHelper.commentPage();

            int total = dataFilter.Count();
            var pageData = dataFilter
                         .OrderByDescending(u => u.id)  //降序排序
                         .Skip(pageSize * (pageIndex - 1))
                         .Take(pageSize).AsQueryable();


            return Result.Success().SetData(new { pageData, pageSize, total });
        }

        [Authorize(Policy = "发布评论")]
        //添加评论可以获取经验
        [HttpPost]
        public async Task<Result> AddComment(comment myComment, int discussId)
        {
            if (discussId == 0)
            {
                return Result.Error();
            }
            myComment.time = DateTime.Now;
            myComment.user = await _userBll.GetEntities(u => u.id == _user.id).FirstOrDefaultAsync();

            var discussData = await _discussBll.GetEntities(u => u.id == discussId).Include(u => u.comments).FirstOrDefaultAsync();
            discussData.comments.Add(myComment);

            _discussBll.Update(discussData);
            int Experience = settingHelper.commentExperience();
            //注意，这里经验可以从redis中获取，现在先设置一个1定值
            int level = await _user_extraBll.UpdateExperience(myComment.user.id, Experience, false);
            return Result.Success().SetData(new { level });
        }
    }
}
