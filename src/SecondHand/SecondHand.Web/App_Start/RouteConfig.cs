using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SecondHand.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.LowercaseUrls = true;

            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Edit ad",
                url: "advertisements/{id}/edit",
                defaults: new { controller = "Advertisements", action = "Edit" },
                constraints: new { id = @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$" }
            );

            routes.MapRoute(
                name: "Ad details",
                url: "advertisements/{id}",
                defaults: new { controller = "Advertisements", action = "Details" },
                constraints: new { id = @"^[{(]?[0-9A-F]{8}[-]?([0-9A-F]{4}[-]?){3}[0-9A-F]{12}[)}]?$" }
            );

            routes.MapRoute(
                name: "User profile",
                url: "users/{username}",
                defaults: new { controller = "Users", action = "UserProfile" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
