using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TechnoStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(null,
                "",
                new
                {
                    controller = "Technics",
                    action = "List",
                    area="",
                    category = (string)null,
                }
            );


            routes.MapRoute(
                name: "ListCategory",
                "{category}",
                new { controller = "Technics", action = "List" ,area=""}
                );


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}",
                defaults: new { controller = "Technics", action = "List",area=""}
            );
        }
    }
}
