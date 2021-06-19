using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Model
{
    //需要2个用户关联，添加时间，是否同意
    public class friend:baseModel
    {
        public user user1 { get; set; }
        public user user2 { get; set; }

        public DateTime? time { get; set; }

        //判断 is_del 来决定好友是否删除
        public int is_agree { get; set; }//判断该属性，来决定请求列表与好友列表
    }
}
