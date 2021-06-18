using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.BLL
{
    public partial class discussBll : BaseBll<discuss>, IdiscussBll
    {
        public async Task<bool> setProp(int disucssId, int propId, string color = "#212121")
        {
            var myDiscuss = await CurrentDal.GetEntities(u => u.id == disucssId).Include(u => u.user).Include(u=>u.labels). FirstOrDefaultAsync();


            //{ title: "置顶卡", method: "1" },
            //{ title: "色彩卡", method: "2" },
            //{ title: "匿名卡", method: "3" },
            //{ title: "加密卡", method: "4" },
            switch (propId)
            {
                case 1: myDiscuss.is_top = 1; break;//置顶将置顶标识变为1
                case 2: myDiscuss.color = color; break;//色彩直接更换即可
                case 3: myDiscuss.is_anonymous = 1; myDiscuss.user = await Db.Set<user>().Where(u => u.username == "匿名").FirstOrDefaultAsync();myDiscuss.labels = null; break;//匿名将作者更换，同时将标签清空
                case 4: myDiscuss.is_encryption = 1; break;//暂时放一下
            }
            CurrentDal.Update(myDiscuss);
            return Db.SaveChanges() > 0;
        }
    }
}
