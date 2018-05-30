﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Index",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "EditAd",
                url: "{controller}/{action}/{id1}",
                defaults: new { controller = "Home", action = "EditAd", id = UrlParameter.Optional}
            );

            routes.MapRoute(
                name: "MakePolygraphyOrder",
                url: "{controller}/{action}/{id1}",
                defaults: new { controller = "Home", action = "MakePolygraphyOrder", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Login", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Signup",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Signup", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Logout",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Account", action = "Logout", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Search",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Search", id = UrlParameter.Optional }
            );
        }
    }
}
