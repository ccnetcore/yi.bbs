using CC.Yi.Common;
using CC.Yi.Common.Cache;
using CC.Yi.Common.Jwt;
using CC.Yi.IBLL;
using CC.Yi.Model;
using CC.Yi.ViewModel;
using Microsoft.AspNetCore.Http;
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
        private IHttpContextAccessor _httpContext;
        public AccountController(IuserBll iuserBll, IHttpContextAccessor httpContext, ILogger<AccountController> logger,IroleBll roleBll)
        {
            _roleBll=roleBll;
            _userBll = iuserBll;
            _logger = logger;
            _httpContext = httpContext;
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

                   return await _userBll.login(data);
                   // //通过查询权限，把所有权限加入进令牌中
                   // List<Claim> claims = new List<Claim>();
                   // claims.Add(new Claim(JwtRegisteredClaimNames.Nbf, $"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}"));
                   // claims.Add(new Claim(JwtRegisteredClaimNames.Exp, $"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"));
                   // claims.Add(new Claim(ClaimTypes.Name, data.username));
                   // claims.Add(new Claim("Id", data.id.ToString()));

                   //var actions = await _userBll.getActionByUserId(data.id);

                   // foreach (var k in actions)
                   // {
                   //     claims.Add(new Claim("action", k.action_name));
                   // }




                   // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey));
                   // var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                   // var token = new JwtSecurityToken(
                   //     issuer: JwtConst.Domain,
                   //     audience: JwtConst.Domain,
                   //     claims: claims,
                   //     expires: DateTime.Now.AddMinutes(30),
                   //     signingCredentials: creds);
                   // _logger.LogInformation("登录成功!");
                   // var tokenData = new JwtSecurityTokenHandler().WriteToken(token);

                 
                   // return Result.Success("登录成功!").SetData(new { token = tokenData,user=new { id=data.id,username=data.username,level=data.user_extra.level,icon=data.icon } });
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


        [HttpGet]//邮箱验证
        public async  Task<Result> Email(string emailAddress)
        {
            emailAddress= emailAddress.Trim().ToLower();
            //先判断邮箱是否被注册使用过，如果被使用过，便不让操作
            if (!await _userBll.mail_exist(emailAddress))
            {
                string code = RandomHelper.GenerateRandomLetter(6);
                code = code.ToUpper();//全部转为大写
                EmailHelper.sendMail("江西服装学院论坛账号注册验证码", code, emailAddress);

                //我要把邮箱和对应的code加进到数据库，还有申请时间
                //设置10分钟过期
                //set不存在便添加，如果存在便替换
                CacheHelper.SetCache<string>(emailAddress, code, DateTime.Now.AddMinutes(10));

                return Result.Success("发送邮件成功，请查看邮箱（可能在垃圾箱）");
            }
            else
            {
                return Result.Error("该邮箱已被注册");
            }
           
          //  邮箱和验证码都要被记住，然后注册时候比对邮箱和验证码是不是都和现在生成的一样
        }

        [HttpPost]//注册
        public async Task<Result> Register(user myUser,string code)
        {
            code = code.Trim().ToUpper();
            //先判断用户名是否存在，如果不存在便开始比对邮箱和验证码
            var user = await _userBll.GetEntities(u => u.username == myUser.username).CountAsync();
            if (user==0)
            {

               string true_code= CacheHelper.GetCache<string>(myUser.email);
                //如果邮箱和验证码比对都成功，再给它往下走，否则直接返回验证码或邮箱错误
                if (true_code == code)//现在表示邮箱和验证码正确，并且用户名也并未注册，即可进入注册
                {
                    user_extra user_Extra = new user_extra();
                    user_Extra.introduction = "暂未简介，什么也没有留下";
                    myUser.user_extra = user_Extra;

                    myUser.ip=_httpContext.HttpContext.Request.Headers["X-Real-IP"].FirstOrDefault();//通过上下文获取ip

                    myUser.icon = "ea017c40-9205-4541-8cd4-f23e036f7795.jpg";//默认头像
                    var data = _userBll.Add(myUser);


                    var roleData = await _roleBll.GetEntities(u => u.role_name == "普通用户").FirstOrDefaultAsync();

                    await _userBll.setRole(data.id, new List<int> { roleData.id });

                    _logger.LogInformation("注册成功!");
                    return Result.Success("注册成功！");
                }
                else
                {
                    _logger.LogInformation("注册成功!");
                    return Result.Error("注册失败！邮箱验证码错误！请重试!");
                }

             
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
