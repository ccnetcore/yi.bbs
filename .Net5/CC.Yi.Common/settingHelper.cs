using CC.Yi.Common.Cache;
using CC.Yi.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Common
{
    public static class settingHelper
    {
        public static ICacheWriter CacheWriter { get; set; }

        static settingHelper()
        {
            settingHelper.CacheWriter = new RedisCache();
        }
        public static ICacheWriter getCache()
        {
            return settingHelper.CacheWriter;
        }

        public static int commentPage()
        {
           
            return CacheWriter.GetCache<int>("commentPage");
        }
        public static int discussPage()
        {
            return CacheWriter.GetCache<int>("discussPage");
        }
        public static int commentExperience()
        {
            return CacheWriter.GetCache<int>("commentExperience");
        }
        public static int discussExperience()
        {
            return CacheWriter.GetCache<int>("discussExperience");
        }
        public static string title()
        { 
            return CacheWriter.GetCache<string>("title");
        }

        public static void update(setting data)
        {
            CacheWriter.SetCache<int>("commentPage", data.commentPage);
            CacheWriter.SetCache<int>("discussPage", data.discussPage);
            CacheWriter.SetCache<int>("commentExperience", data.commentExperience);
            CacheWriter.SetCache<int>("discussExperience", data.discussExperience);
            CacheWriter.SetCache<string>("title", data.title);
        }

        public static void Init()
        {
            if (title() == null)
            {
                update(new setting() { title = "暂未设置", discussPage = 10, discussExperience = 3, commentExperience = 1, commentPage = 10 });
            }
        }
    }
}
