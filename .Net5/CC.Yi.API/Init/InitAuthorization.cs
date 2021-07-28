using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Init
{
    public static class InitAuthorization
    {
        public static void Init(IServiceCollection services)
        {
            
            services.AddAuthorization(options =>
            {
                //配置基于策略的验证
                options.AddPolicy("板块管理", policy => policy.RequireClaim("action", "板块管理"));
                options.AddPolicy("主题管理", policy => policy.RequireClaim("action", "主题管理"));
                options.AddPolicy("等级管理", policy => policy.RequireClaim("action", "等级管理"));
                options.AddPolicy("设置管理", policy => policy.RequireClaim("action", "设置管理"));
                options.AddPolicy("横幅管理", policy => policy.RequireClaim("action", "横幅管理"));
                options.AddPolicy("版本管理", policy => policy.RequireClaim("action", "版本管理"));
                options.AddPolicy("道具管理", policy => policy.RequireClaim("action", "道具管理"));
                options.AddPolicy("商城管理", policy => policy.RequireClaim("action", "商城管理"));
                options.AddPolicy("类型管理", policy => policy.RequireClaim("action", "类型管理"));

                options.AddPolicy("用户管理", policy => policy.RequireClaim("action", "用户管理"));
                options.AddPolicy("角色管理", policy => policy.RequireClaim("action", "角色管理"));
                options.AddPolicy("权限管理", policy => policy.RequireClaim("action", "权限管理"));
                options.AddPolicy("日志管理", policy => policy.RequireClaim("action", "日志管理"));


                options.AddPolicy("绑定QQ", policy => policy.RequireClaim("action", "绑定QQ"));
                options.AddPolicy("标签管理", policy => policy.RequireClaim("action", "标签管理"));
                options.AddPolicy("收藏管理", policy => policy.RequireClaim("action", "收藏管理"));
                options.AddPolicy("发布主题", policy => policy.RequireClaim("action", "发布主题"));
                options.AddPolicy("发布评论", policy => policy.RequireClaim("action", "发布评论"));
                options.AddPolicy("修改信息", policy => policy.RequireClaim("action", "修改信息"));
                options.AddPolicy("样式管理", policy => policy.RequireClaim("action", "样式管理"));
                options.AddPolicy("点赞管理", policy => policy.RequireClaim("action", "点赞管理"));
                options.AddPolicy("购买道具", policy => policy.RequireClaim("action", "购买道具"));
                options.AddPolicy("使用道具", policy => policy.RequireClaim("action", "使用道具"));
                options.AddPolicy("好友管理", policy => policy.RequireClaim("action", "好友管理"));

            });

        }
    }
}
