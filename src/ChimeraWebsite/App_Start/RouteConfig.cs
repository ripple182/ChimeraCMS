using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace ChimeraWebsite
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


            routes.MapRoute(
               name: "Default",
               url: "{friendlyURL}",
               namespaces: new[] { "ChimeraWebsite.Controllers" },
               defaults: new { controller = "Home", action = "Index", friendlyURL = UrlParameter.Optional }
           );

            routes.MapRoute(
              name: "SearchProducts",
              url: "SearchProducts/{action}",
              namespaces: new[] { "ChimeraWebsite.Controllers" },
              defaults: new { controller = "SearchProducts", action = "Active" }
          );

            routes.MapRoute(
             name: "ViewProduct",
             url: "ViewProduct/{action}",
             namespaces: new[] { "ChimeraWebsite.Controllers" },
             defaults: new { controller = "ViewProduct", action = "Details" }
         );

            routes.MapRoute(
            name: "ShoppingCart",
            url: "ShoppingCart/{action}",
            namespaces: new[] { "ChimeraWebsite.Controllers" },
            defaults: new { controller = "ShoppingCart", action = "ViewCart" }
 );
              routes.MapRoute(
            name: "Order",
            url: "Order/{action}",
            namespaces: new[] { "ChimeraWebsite.Controllers" },
            defaults: new { controller = "Order", action = "InitCheckout" }
        );

              routes.MapRoute(
             name: "PageExit",
             url: "PageExit/{action}",
             namespaces: new[] { "ChimeraWebsite.Controllers" },
             defaults: new { controller = "PageExit", action = "Exit" }
         );

        }
    }
}