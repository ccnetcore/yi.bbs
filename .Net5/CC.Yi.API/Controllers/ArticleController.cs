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
    public class ArticleController : BaseController
    {
        private IarticleBll _articleBll;
        private ILogger<ArticleController> _logger;
        private IdiscussBll _discussBll;
        private IrecordBll _recordBll;
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public ArticleController(IarticleBll articleBll, IdiscussBll discussBll, ILogger<ArticleController> logger,IrecordBll recordBll)
        {
            _recordBll = recordBll;
            _discussBll = discussBll;
            _articleBll = articleBll;
            _logger = logger;
        }


        [HttpPost]
        public Result setArticleByCache(article articleData)
        {
            var clien=settingHelper.getCache();
            clien.SetCache("Article:"+_user.id+":"+ articleData.id, articleData.content, TimeSpan.FromDays(7));
            return Result.Success("自动保存成功");
        }

        [HttpGet]
        public Result getArticleByCache(int articleId)
        {
            var clien = settingHelper.getCache();
         var data=   clien.GetCache<string>( "Article:" + _user.id + ":" + articleId);
            return Result.Success("读取存档成功").SetData(data);
        }


        [HttpGet]
        public async Task<Result> GetArticlesByDiscussId(int discussId)
        {
            var data = await _articleBll.GetEntities(u => u.discuss.id == discussId && u.is_delete == delFlagNormal).Include(r=>r.children).ThenInclude(u=>u.children).ThenInclude(u => u.children).ThenInclude(u => u.children).ThenInclude(u => u.children).ToListAsync();

           data=datahHandle.artData(data);
            return Result.Success().SetData(data);
        }



        [HttpPost]
        public async Task<Result> AddChildrenArticle(article myArticle, int parentId,int discussId)
        {
            myArticle.id = 0;
            if (discussAction.is_allow(_user, discussId))
            {
                
                //得到父级目录
                var parentArticleData = await _articleBll.GetEntities(u => u.id == parentId).Include(u=>u.children).FirstOrDefaultAsync();

                //父级目录下添加子目录
                parentArticleData.children.Add(myArticle);

                //更新父级目录
                _articleBll.Update(parentArticleData);

                //添加编辑记录
                _recordBll.Add(discussId, $"添加了【{myArticle.name}】目录", _user.id);
                return Result.Success();
            }
            else
            {
                return Result.Error("权限不足！");
            }
        }


        [HttpGet]
        public async Task<Result> GetArticleById(int articleId)
        {
            var data = await _articleBll.GetEntityById(articleId);
            return Result.Success().SetData(data);
        }


        [HttpGet]
        public async Task<Result> GetTitlArticles(int discussId)
        {
            var data = await _articleBll.GetEntities(u => u.discuss.id == discussId && u.is_delete == delFlagNormal).Include(r => r.children).ThenInclude(u => u.children).ThenInclude(u => u.children).ThenInclude(u => u.children).ThenInclude(u => u.children).ToListAsync();
            data = datahHandle.tileArt(data);
           
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
        public async Task<Result> AddArticle(article myArticle, int discussId)
        {
            if (discussAction.is_allow(_user, discussId))
            {
                var myDiscuss = await _discussBll.GetEntities(u => u.id == discussId).FirstOrDefaultAsync();

                myArticle.discuss = myDiscuss;

                myArticle.id = 0;
                _articleBll.Add(myArticle);

                 _recordBll.Add(discussId, $"添加了【{myArticle.name}】目录", _user.id);
                return Result.Success();
            }
            else
            {
                return Result.Error("权限不足！");
            }
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result UpdateArticle(article myArticle, int discussId)
        {
            if (discussAction.is_allow(_user, discussId))
            {
                _articleBll.Update(myArticle);
           _recordBll.Add(discussId, $"更新了【{myArticle.name}】目录", _user.id);
                return Result.Success();
            }
            else
            {
                return Result.Error("权限不足！");
            }
        }

        //      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public async  Task<Result> delArticleList(List<int> Ids, int discussId)
        {
            if (discussAction.is_allow(_user, discussId))
            {

               await _articleBll.DelListByUpdateList(Ids);
                _recordBll.Add(discussId, $"删除了目录", _user.id);
                return Result.Success();
            }
            else
            {
                return Result.Error("权限不足！");
            }

        }
    }
}