using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace cinema
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
            name: "Filmy",
            url: "Film/{nazwa}/{id}",
            defaults: new { controller = "Filmy", action = "Film" },
            constraints: new { id = @"[\w& ]+" }
             );

            routes.MapRoute(
            name: "SEanse",
            url: "Seans/{id}",
            defaults: new { controller = "Seanse", action = "Index" },
            constraints: new { id = @"[\w& ]+" }
             );

            routes.MapRoute(
           name: "Film2",
           url: "Filmy",
           defaults: new { controller = "Filmy", action = "Filmy" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
