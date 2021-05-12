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
    public partial class user_extraBll : Iuser_extraBll
    {
        //传入提高的经验和用户（该用户要关联权限），返回修改后的等级
        //分两种情况：设置经验为多少，以及添加或者减少多少经验
        public async Task<int> UpdateExperience(int userId, int experienceNum, bool is_set = true)
        {
            user userData = await Db.Set<user>().Where(u => u.id == userId).Include(u => u.user_extra).Include(u => u.roles).FirstOrDefaultAsync();
            if (!is_set)
            {
                experienceNum += userData.user_extra.experience;
            }
            var levelList = Db.Set<level>().OrderBy(u => u.experience).ToList();
            int i;
            for (i = 0; i < levelList.Count(); i++)
            {
                if (levelList[i].experience < experienceNum)
                {
                    var role = await Db.Set<role>().Where(u => u.role_name == "l-" + i.ToString()).FirstOrDefaultAsync();
                    if (role != null)
                    {
                        userData.roles.Add(role);
                    }
                }
                else
                {
                    break;
                }
            }
            userData.user_extra.experience = experienceNum;
            userData.user_extra.level = i;

            Db.Set<user>().Update(userData);
            Db.SaveChanges();
            return i;

        }
    }
}
