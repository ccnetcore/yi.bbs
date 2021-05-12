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
    public partial class roleBll : BaseBll<role>, IBLL.IroleBll
    {
        //设置角色拥有哪些权限
        public async Task<bool> setAction(int roleid,List<int> actionids)
        {
            
         var role= await CurrentDal.GetEntities(u => u.id == roleid).Include(u => u.actions).FirstOrDefaultAsync();
            role.actions.Clear();
            var allActions =Db.Set<action>().Where(u => actionids.Contains(u.id));
            foreach (var k in allActions)
            {
                role.actions.Add(k);
            }
          return  Db.SaveChanges()>0;
        }
    }
}
