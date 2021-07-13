﻿


/*
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
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        public ArticleController(IarticleBll articleBll, ILogger<ArticleController> logger)
        {
            _articleBll = articleBll;
            _logger = logger;
        }

        [HttpGet]
        public async Task<Result> GetArticles()
        {
            var data = await _articleBll.GetEntities(u => u.is_delete == delFlagNormal).AsNoTracking().ToListAsync();
            return Result.Success().SetData(data);
        }

//      [Authorize(Policy = "板块管理")]
        [HttpPost]
        public Result AddArticle(article myArticle)
        {
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

*/