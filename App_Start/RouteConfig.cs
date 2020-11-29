using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebBookManagement
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces:new string[] {"WebBookManagement.Controllers"}
            );
            /*
            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "WebBookManagement.Controllers" },
               constraints: new { year = @"\d{4}", month = @"\d{4}", day = @"\d{2}" }
           );
           */
        }
    }
}
