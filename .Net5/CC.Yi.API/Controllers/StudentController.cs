//using CC.Yi.Common;
//using CC.Yi.Common.Cache;
//using CC.Yi.Common.Jwt;
//using CC.Yi.IBLL;
//using CC.Yi.Model;
//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Caching.Distributed;
//using Microsoft.Extensions.Logging;
//using Microsoft.IdentityModel.Tokens;
//using ServiceStack.Redis;
//using System;
//using System.Collections.Generic;
//using System.IdentityModel.Tokens.Jwt;
//using System.Linq;
//using System.Security.Claims;
//using System.Text;
//using System.Threading.Tasks;

//namespace CC.Yi.API.Controllers
//{
//    [ApiController]
//    [Route("[controller]/[action]")]
//    public class StudentController : ControllerBase
//    {
//        private readonly ILogger<StudentController> _logger;//处理日志相关文件

//        private IstudentBll _studentBll;
//        public StudentController(ILogger<StudentController> logger, IstudentBll studentBll)
//        {
//            _logger = logger;
//            _logger.LogInformation("现在你进入了StudentController控制器");
//            _studentBll = studentBll;
//        }

//        [HttpGet]
//        public Result GetReids()
//        {
//            var data = CacheHelper.CacheWriter.GetCache<string>("key01");
//            return Result.Success(data);
//        }

//        //发送令牌
//        [HttpGet]
//        public Result Login(string role)
//        {
//            string userName = "admin";
//            var claims = new[]
//                   {
//                    new Claim(JwtRegisteredClaimNames.Nbf,$"{new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds()}") ,
//                    new Claim (JwtRegisteredClaimNames.Exp,$"{new DateTimeOffset(DateTime.Now.AddMinutes(30)).ToUnixTimeSeconds()}"),
//                    new Claim(ClaimTypes.Name, userName),
//                   new Claim(ClaimTypes.Role,role)

//                };
//            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtConst.SecurityKey));
//            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

//            var token = new JwtSecurityToken(
//                 issuer: JwtConst.Domain,
//                    audience: JwtConst.Domain,
//                claims: claims,
//                expires: DateTime.Now.AddMinutes(30),
//                signingCredentials: creds);

//            var tokenData = new JwtSecurityTokenHandler().WriteToken(token);
//            return Result.Success("欢迎你！管理员!").SetData(new { token = tokenData });
//        }

//        [HttpGet]
//        [Authorize(Policy = "myadmin")]//基于策略的验证
//        public Result MyAdmin()
//        {
//            return Result.Success("欢迎你！管理员!");
//        }

//        [HttpGet]
//        [Authorize(Roles = "user")]//基于角色的验证
//        public Result MyUser()
//        {
//            return Result.Success("欢迎你！游客！");
//        }

//        #region
//        //下面，经典的 增删改查 即为简易--Yi意框架
//        //注意：请确保你的数据库中存在合理的数据
//        #endregion
//        [HttpGet]
//        public async Task<Result> GetTest()//查
//        {
//            _logger.LogInformation("调用查方法");
//            var data =await  _studentBll.GetAllEntities().ToListAsync();
//            return Result.Success().SetData(data);
//        }
//        [HttpGet]
//        public Result AddTest()//增
//        {
//            _logger.LogInformation("调用增方法");
//            List<student> students = new List<student>() { new student { name = "学生a" }, new student { name = "学生d" } };
//            if (_studentBll.Add(students))
//            {
//                return Result.Success();
//            }
//            else
//            {
//                return Result.Error();
//            }
//        }
//        [HttpGet]
//        public Result RemoveTest()//删
//        {
//            _logger.LogInformation("调用删方法");
//            if (_studentBll.Delete(u => u.name == "学生a"))
//            {
//                return Result.Success();
//            }
//            else
//            {
//                return Result.Error();
//            }
//        }
//        [HttpGet]
//        public  Result UpdateTest()//改
//        {
//            _logger.LogInformation("调用改方法");
//            if (_studentBll.Update(new student { id = 2, name = "学生a" }, "name"))
//            {
//                return Result.Success();
//            }
//            else
//            {
//                return Result.Error();
//            }

//        }
//    }
//}
