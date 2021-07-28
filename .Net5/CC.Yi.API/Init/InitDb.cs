using CC.Yi.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CC.Yi.API.Init
{
    public static class InitDb
    {
        public static bool Init(DataContext Db)
        {
            if (!Db.Set<user>().Any())
            {
                user initUser = new user
                {
                    username = "cc",
                    password = "123",
                    user_extra = new user_extra(),
                    time = DateTime.Now,
                    roles = new List<role>{
                           new role{
                               role_name="管理员",
                               actions=new List<action>{
                               new action{ action_name="首页",router="/index",icon="mdi-view-dashboard"},
                               new action{action_name="用户管理",router="/admuser",icon="mdi-account-box"},
                               new action{ action_name="角色管理",router="/admrole",icon="mdi-gavel"},
                               new action{ action_name="权限管理",router="/admaction",icon="mdi-lock"},

                               new action{ action_name="板块管理",router="/admplate",icon="mdi-lock"},
                               new action{ action_name="权限管理",router="/action",icon="mdi-lock"},
                               new action{ action_name="等级管理",router="/admlevel",icon="mdi-lock"},
                               new action{ action_name="主题管理",router="/admdiscuss",icon="mdi-lock"},

                               new action{ action_name="我的信息",router="/userinfo",icon="mdi-lock"},
                               new action{ action_name="a-admin",router="",icon="mdi-lock"},

                               new action{ action_name="我的主题",router="/myDiscuss",icon="mdi-lock"},

                               new action{ action_name="标签管理",router="",icon="mdi-lock"},
                               new action{ action_name="收藏管理",router="",icon="mdi-lock"},
                               new action{ action_name="发布主题",router="",icon="mdi-lock"},

                               new action{ action_name="发布评论",router="",icon="mdi-lock"},
                               new action{ action_name="修改信息",router="",icon="mdi-lock"},

                               new action{ action_name="样式管理",router="/theme",icon="mdi-camera"},
                               new action{ action_name="设置管理",router="/admsetting",icon="mdi-lock"},
                               new action{ action_name="横幅管理",router="/Admbanner",icon="mdi-lock"},
                               new action{ action_name="点赞管理",router="",icon="mdi-lock"},
                               new action{ action_name="开源地址",router="/my/https://github.com/jiftcc/yi.bbs",icon="mdi-lock"},
                               new action{ action_name="版本管理",router="/admversion",icon="mdi-lock"},
                               new action{ action_name="版本内容",router="/version",icon="mdi-book-search"},


                               new action{ action_name="我的仓库",router="/warehouse",icon="mdi-home-account"},
                               new action{ action_name="商城",router="/shop",icon="mdi-shopping"},
                               new action{ action_name="道具管理",router="/admprop",icon="mdi-lock"},

                               new action{ action_name="商城管理",router="/admshop",icon="mdi-lock"},
                               new action{ action_name="绑定QQ",router="",icon="mdi-lock"},
                               new action{ action_name="购买道具",router="",icon="mdi-lock"},
                               new action{ action_name="使用道具",router="",icon="mdi-lock"},
                               new action{ action_name="好友管理",router="/friend",icon="mdi-account-group"},

                               new action{ action_name="聊天大厅",router="/chathub",icon="mdi-chat-processing"},
                               new action{ action_name="日志管理",router="/admlog",icon="mdi-lock"},
                               new action{ action_name="d-49",router="",icon="mdi-lock"},
                               new action{ action_name="d-admin",router="",icon="mdi-lock"},
                               new action{ action_name="类型管理",router="/admmytype",icon="mdi-lock"},
                                                        }
                                    },
                           new role{ role_name="l-0"},
                           new role{ role_name="l-1"},
                           new role{ role_name="l-2"},
                           new role{ role_name="l-3"},
                           new role{ role_name="l-4"},
                           new role{ role_name="l-5"},
                           new role{ role_name="l-6"},
                           new role{ role_name="l-7"},
                           new role{ role_name="l-8"},
                           new role{ role_name="l-9"},
                           new role{ role_name="l-10"},
                           new role{ role_name="l-11"},
                           new role{ role_name="l-12"},
                           new role{ role_name="普通用户"}
                                           }
                };
                Db.Set<user>().AddRange(new List<user>() {
                initUser,
                new user(){ username="匿名",password="6d4a6s4fg87qe8gf"},
                new user(){username="游客",password="" }
                }  );


                //-------------------------------------------------------------------------------------添加管理员账户
                List<level> levels = new List<level>
                {
                    new level{num=0, name="小白0",max_char=50,experience=0},
                    new level{num=1, name="小白1",max_char=100,experience=5},
                    new level{num=2, name="小白2",max_char=200,experience=10},
                    new level{num=3, name="小白3",max_char=300,experience=15},
                    new level{num=4, name="小白4",max_char=400,experience=20},
                    new level{num=5, name="小白5",max_char=500,experience=25},
                    new level{num=6, name="小白6",max_char=600,experience=30},
                    new level{num=7, name="小白7",max_char=700,experience=35},
                    new level{num=8, name="小白8",max_char=800,experience=40},
                    new level{num=9, name="小白9",max_char=900,experience=45},
                    new level{num=10, name="小白10",max_char=1000,experience=50},
                    new level{num=11, name="小白11",max_char=1100,experience=55}
                };
                Db.Set<level>().AddRange(levels);
                //---------------------------------------------------------------------------------------添加等级表
                List<mytype> mytypes = new List<mytype> {
                new mytype{ name="闲聊"},
                new mytype{name="原创"},
                new mytype{ name="转载"}

                };

                Db.Set<mytype>().AddRange(mytypes);




               return Db.SaveChanges()>0;
            }
            return false;
        }
    }
}
