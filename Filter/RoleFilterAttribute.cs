using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebBookManagement.BLL;

namespace WebBookManagement.Filter
{
    public class RoleFilterAttribute : ActionFilterAttribute
    {
        //方法之前
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //在Action 执行前执行
            //获取当前的控制器
            string currenturl = filterContext.RouteData.Values["controller"].ToString();
            if (RoleManage.CheckUrl(currenturl))
            {
                //用户路由不对跳转到后台首页页面
                filterContext.Result = new RedirectToRouteResult(new
                    RouteValueDictionary(new { controller = "Admin", action = "Index", area = string.Empty }
                ));
                
            }

        }
    }
}