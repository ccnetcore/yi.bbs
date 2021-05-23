﻿using CC.Yi.Common;
using CC.Yi.Common.Jwt;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : BaseController
    {
        private IuserBll _userBll;
        private IroleBll _roleBll;
        private ILogger<AccountController> _logger;
        public AccountController(IuserBll iuserBll, ILogger<AccountController> logger,IroleBll roleBll)
        {
            _roleBll=roleBll;
            _userBll = iuserBll;
            _logger = logger;
        }

        [HttpPost]//验证登录
        public async Task<Result> login(user myUser)
        {
            var data = await _userBll.GetEntities(u => u.username == myUser.username && u.is_delete == (short)ViewModel.Enum.DelFlagEnum.Normal).Include(u=>u.user_extra).FirstOrDefaultAsync();

            if (data != null)
            {
                if (data.password == myUser.password)
                {
                    //HttpContext.Session.SetString("loginId", JsonHelper.ToString(_info_user));
                    _logger.LogInformation("登录成功!");

                    //通过查询权限，把所有权限加入进令牌中
                    List<Claim> claims = new List<Claim>();
                    claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
                    claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"));
                    claims.Add(new Claim(ClaimTypes.Name, data.username));
                    claims.Add(new Claim("Id", data.id.ToString()));

                   var actions = await _userBll.getActionByUserId(data.id);

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
                    _logger.LogInformation("登录成功!");
                    var tokenData = new JwtSecurityTokenHandler().WriteToken(token);

                 
                    return Result.Success("登录成功!").SetData(new { token = tokenData,user=new { id=data.id,username=data.username,level=data.user_extra.level,icon=data.icon } });
                }
            }

            _logger.LogInformation("登录失败!");
            return Result.Error("登入失败，用户名或者密码错误！");
        }
        [HttpPost]//退出登入
        public Result logOut()
        {
            _logger.LogInformation("已退出登录!");

            return Result.Success("成功退出！");
        }

        [HttpPost]//注册
        public async Task<Result> Register(user myUser)
        {
            var user = await _userBll.GetEntities(u => u.username == myUser.username).CountAsync();
            if (user==0)
            {
                user_extra user_Extra = new user_extra();
                myUser.user_extra = user_Extra;
                var data = _userBll.Add(myUser);
               var roleData= await _roleBll.GetEntities(u => u.role_name == "普通用户").FirstOrDefaultAsync();
                
                await _userBll.setRole(data.id, new List<int> { roleData.id});
    
                _logger.LogInformation("注册成功!");
                return Result.Success("注册成功！");
            }
            else
            {
                _logger.LogInformation("注册失败!");
                return  Result.Error("注册失败！当前用户已被注册！");
            }
        }

        [HttpPost]//判断是否有有登录
        public Result Logged()
        {
            if (_user.id==0)
            {
                return Result.Error("登录超时！请重新登录");
            }
            else
            {
                return Result.Success();
            }
        
        }

        
    }
}
