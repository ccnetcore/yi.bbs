using System;
using System.Collections.Generic;
using System.Text;

namespace CC.Yi.Model
{
    public class collection:baseModel
    {
        public DateTime time { get; set; }//收藏时间

       public  user user { get; set;}  //这个收藏是谁的
       public discuss discuss { get; set; }//这个收藏的主题是谁的
    }
}
