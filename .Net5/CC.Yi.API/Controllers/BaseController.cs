using CC.Yi.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace CC.Yi.API.Controllers
{
    public class BaseController : Controller
    {
        public user _user;
        //Response.HttpContext.User.Identity.Name
        public BaseController()
        {
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
      //var myclaim=  HttpContext.AuthenticateAsync().Result.Principal.Claims;
            base.OnActionExecuting(filterContext);
            user myUser = new user() ;
            List<action> actions = new List<action>();

            var identity = (System.Security.Claims.ClaimsIdentity)this.Request.HttpContext.User.Identity;
           
            foreach (var k in identity.Claims)
            {
                switch (k.Type)
                {
                    case ClaimTypes.Name: myUser.username = k.Value;  break;//用户名
                    case "Id": myUser.id = Convert.ToInt32(k.Value);break;//用户id
                    case "action": actions.Add(new action() { action_name = k.Value });break;//用户权限
                }   
            }
            myUser.actions = actions;
            _user = myUser;
            return;
        }

    }
}
