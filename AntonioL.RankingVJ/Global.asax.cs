using AntonioL.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using System.Web.SessionState;

namespace AntonioL.RankingVJ
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ApplicationDbContext>());
            CrearRoles();
            // Código que se ejecuta al iniciar la aplicación
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void CrearRoles()
        {
            using (var context = new ApplicationDbContext())
            {
                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

                if (!roleManager.RoleExists("Administrador"))
                {
                    var role = new IdentityRole("Administrador");
                    roleManager.Create(role);
                }

                if (!roleManager.RoleExists("Auxiliar"))
                {
                    var role = new IdentityRole("Auxiliar");
                    roleManager.Create(role);
                }
            }
        }
    }
}