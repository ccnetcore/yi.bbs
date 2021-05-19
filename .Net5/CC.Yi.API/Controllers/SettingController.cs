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
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class SettingController : Controller
    {
        private ILogger<SettingController> _logger;
        public SettingController(ILogger<SettingController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public Result getTitle()
        {
          string title= settingHelper.title();
            return Result.Success().SetData(title);
        }

        [HttpGet]
        public Result getSetting()
        {
            setting data = new setting();
            data.commentExperience = settingHelper.commentExperience();
            data.commentPage = settingHelper.commentPage();
            data.discussExperience = settingHelper.discussExperience();
            data.discussPage = settingHelper.discussPage();
            data.title = settingHelper.title();
            return Result.Success().SetData(data);
        }

        [HttpPost]
        public Result UpdateSetting(setting data)
        {
            settingHelper.update(data);
            return Result.Success("更新成功！部分变更请刷新");
        }
    }
}
