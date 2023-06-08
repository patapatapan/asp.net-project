using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace funplay
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "ProjectHome", action = "ProjectIndex", id = UrlParameter.Optional },
                //defaults: new { controller = "Web", action = "Login", id = UrlParameter.Optional },
                new string[] { "funplay.Controllers" }
            );
        }
    }
}
