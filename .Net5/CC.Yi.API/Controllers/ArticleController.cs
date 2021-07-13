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
    public class ArticleController : Controller
    {
        private IarticleBll _articleBll;
        private ILogger<ArticleController> _logger;
        private IdiscussBll _discussBll;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public ArticleController(IarticleBll articleBll,IdiscussBll discussBll,  ILogger<ArticleController> logger)
        {
            _discussBll = discussBll;
            _articleBll = articleBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetArticlesByDiscussId(int discussId)
        {
            var data = await _articleBll.GetEntities(u => u.discuss.id == discussId && u.is_delete==delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

        [HttpGet]
        public async Task<Result> GetArticleById(int articleId)
        {
            var data = await _articleBll.GetEntityById(articleId);
            return Result.Success().SetData(data);
        }

        [HttpGet]
        public async Task<Result> GetArticles()
        {
            var data = await _articleBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public async  Task<Result> AddArticle(article myArticle,int discussId)
        {
          var myDiscuss = await _discussBll.GetEntities(u=>u.id==discussId).FirstOrDefaultAsync();

            myArticle.discuss = myDiscuss;
            _articleBll.Add(myArticle);
            return Result.Success();
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateArticle(article myArticle)
        {
            _articleBll.Update(myArticle);
            return Result.Success();
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result delArticleList(List<int> Ids)
        {
            _articleBll.DelListByUpdateList(Ids);
            return Result.Success();
        }
    }
}