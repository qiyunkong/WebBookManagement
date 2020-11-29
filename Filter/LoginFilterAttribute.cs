using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBookManagement.BLL;

namespace WebBookManagement.Models
{
    public class LoginFilterAttribute : ActionFilterAttribute
    {
        //方法之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //在Action 执行前执行
           
            var account = UserManage.GetCurrentUser();
            if (account == null )
            {
                //用户不登陆的时候跳转到登录页面
                filterContext.Result = new RedirectToRouteResult(new 
                    RouteValueDictionary(new { controller = "Home", action = "Index", area = string.Empty }
                ));
            }

        }

        
    }
}