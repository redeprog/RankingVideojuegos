using AntonioL.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ
{
    public partial class Home : System.Web.UI.Page
    {
        protected string UserRole = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login.aspx");

            if (!User.IsInRole("Administrador") && !User.IsInRole("Auxiliar"))
                Response.Redirect("~/AccesoDenegado.aspx");

            if (!IsPostBack && Context.User.Identity.IsAuthenticated)
            {
                var userId = Context.User.Identity.GetUserId();
                var context = new ApplicationDbContext();
                var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var roles = userManager.GetRoles(userId);
                if (roles.Count > 0)
                {
                    UserRole = roles[0];
                }
            }

        }
    }
}