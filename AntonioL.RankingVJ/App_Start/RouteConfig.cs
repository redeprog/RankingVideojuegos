using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Routing;
using Microsoft.AspNet.FriendlyUrls;

namespace AntonioL.RankingVJ
{
    public static class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            var settings = new FriendlyUrlSettings();
            settings.AutoRedirectMode = RedirectMode.Permanent;
            routes.EnableFriendlyUrls(settings);

            // Rutas principales para el menú
            routes.MapPageRoute("Inicio", "", "~/Home.aspx");
            routes.MapPageRoute("VideoJuegos", "videojuegos", "~/Pages/ListaVideojuegos.aspx");
            routes.MapPageRoute("Contacto", "contacto", "~/Contact.aspx");

            routes.MapPageRoute(
                "Login",
                "Login",
                "~/Account/Login.aspx"
            );
        }
    }
}
