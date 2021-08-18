using CC.Yi.Common;
using CC.Yi.Common.Jwt;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

            //排序
            allAction= allAction.OrderBy(u=>u.sort).ToList();

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


        //批量设置用户拥有哪些角色
        public async Task<bool> setRoleList(setRoleList mydata)
        {
            //先得到所有用户

            var userList = await CurrentDal.GetEntities(u => mydata.userIds.Contains(u.id)).Include(u=>u.roles).ToListAsync();

            var allRoles =await Db.Set<role>().Where(u => mydata.roleIds.Contains(u.id)).ToListAsync();

            foreach (var k in userList)
            {
                k.roles = allRoles;
            }
            return Db.SaveChanges() > 0;
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


        //已经从数据库中查找到的数据data  这里是登录操作
        public async Task<Result> login(user data)
        {
            //通过查询权限，把所有权限加入进令牌中
            List<Claim> claims = new List<Claim>();
            claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"));
            claims.Add(new Claim(ClaimTypes.Name, data.username));
            claims.Add(new Claim("Id", data.id.ToString()));

            var actions = await getActionByUserId(data.id);

            foreach (var k in actions)
            {
                claims.Add(new Claim("action", k.action_name));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: JwtConst.Domain,
                audience: JwtConst.Domain,
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);
            var tokenData = new JwtSecurityTokenHandler().WriteToken(token);
            return Result.Success("登录成功!").SetData(new { token = tokenData, user = new { id = data.id, username = data.username, level = data.user_extra.level, icon = data.icon } });

        }


        public async Task<bool> mail_exist(string mail)//大于0表示邮箱已经被注册
        {
            return await CurrentDal.GetEntities(u => u.email == mail.Trim().ToLower()).CountAsync() > 0;
        }
    }
}
