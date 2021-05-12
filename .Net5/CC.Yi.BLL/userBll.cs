using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.BLL
{
    public partial class userBll : BaseBll<user>, IuserBll
    {
        short delFlagNormal = (short)ViewModel.Enum.DelFlagEnum.Normal;
        //得到用户所具有的全部权限：正常权限+特殊权限
        public async Task<List<actionJwt>> getActionByUserId(int id)
        {

            //这里是通过角色来得到的权限
            var user = await CurrentDal.GetEntities(u => u.id == id).Include(u => u.actions).Include(u => u.roles).ThenInclude(u => u.actions).FirstOrDefaultAsync();
            //获取所有去除逻辑删除的角色
            var allRoles = (from r in user.roles
                            where r.is_delete == delFlagNormal
                            select r).ToList();

            //通过有效角色获取所有去除逻辑删除的权限
            var allAction = (from r in allRoles
                             from a in r.actions
                             where a.is_delete == delFlagNormal
                             select a).ToList();
            //这里是获取有效的特殊权限
            var specialAction = (from r in user.actions
                                 where r.is_delete == delFlagNormal
                                 select r).ToList();

            //合并两个权限
            allAction.AddRange(specialAction.AsEnumerable());

            //去重
            var myAllAction = allAction.Distinct().ToList().Select(u => new actionJwt { id = u.id, action_name = u.action_name, router = u.router, icon = u.icon }).ToList();

            return myAllAction;
        }

        //设置用户拥有哪些角色
        public async Task<bool> setRole(int userId,List<int> roleIds)
        {
         var user= await CurrentDal.GetEntities(u => u.id == userId).Include(u => u.roles).FirstOrDefaultAsync();
            user.roles.Clear();
            var allRoles = Db.Set<role>().Where(u => roleIds.Contains(u.id));
            foreach (var k in allRoles)
            {
                user.roles.Add(k);
            }
          return  Db.SaveChanges()>0;
        }


        //设置用户特殊权限
        public async Task<bool> setSpecialAction(int userId, List<int> actionIds)
        {
            var user = await CurrentDal.GetEntities(u => u.id == userId).Include(u => u.actions).FirstOrDefaultAsync();
            user.actions.Clear();
            var allActions = Db.Set<action>().Where(u => actionIds.Contains(u.id));
            foreach (var k in allActions)
            {
                user.actions.Add(k);
            }
            return Db.SaveChanges() > 0;
        }
    }
}
