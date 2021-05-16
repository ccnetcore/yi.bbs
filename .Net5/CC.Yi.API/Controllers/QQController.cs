using AspNet.Security.OAuth.QQ;
using CC.Yi.Common;
using CC.Yi.IBLL;
using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class QQController : Controller
    {

        private ILogger<PlateController> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QQController(ILogger<PlateController> logger, IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _logger = logger;
        }

        [HttpGet("signin-callback")]
        public async Task<IActionResult> Home(string provider, string redirectUrl = "")
        {

            AuthenticateResult authenticateResult = await _httpContextAccessor.HttpContext.AuthenticateAsync(provider);
            if (!authenticateResult.Succeeded) return Redirect(redirectUrl);
            var openIdClaim = authenticateResult.Principal.FindFirst(ClaimTypes.NameIdentifier);
            if (openIdClaim == null || string.IsNullOrWhiteSpace(openIdClaim.Value))
                return Redirect(redirectUrl);


            ClaimsPrincipal principal = authenticateResult.Principal;

            string nickname = principal.FindFirst(ClaimTypes.Name)?.Value;
            string gender = principal.FindFirst(ClaimTypes.Gender)?.Value;
            string picture = principal.FindFirst(QQAuthenticationConstants.Claims.PictureUrl)?.Value;
            string picture_medium = principal.FindFirst(QQAuthenticationConstants.Claims.PictureMediumUrl)?.Value;
            string picture_full = principal.FindFirst(QQAuthenticationConstants.Claims.PictureFullUrl)?.Value;
            string avatar = principal.FindFirst(QQAuthenticationConstants.Claims.AvatarUrl)?.Value;
            string avatar_full = principal.FindFirst(QQAuthenticationConstants.Claims.AvatarFullUrl)?.Value;
            //根据provider，处理用户的基础信息，

            //long id = SaveQQAsync(principal, openIdClaim.Value)


            return Ok("true");
        }
    }
}
