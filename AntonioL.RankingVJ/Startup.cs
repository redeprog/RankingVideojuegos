﻿using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(AntonioL.RankingVJ.Startup))]
namespace AntonioL.RankingVJ
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login.aspx"),
                ExpireTimeSpan = TimeSpan.FromMinutes(30),           // Tiempo de expiración
                SlidingExpiration = true,                            // Renovación automática
                CookieSecure = CookieSecureOption.SameAsRequest       // Seguridad de la cookie
            });
        }
    }
}