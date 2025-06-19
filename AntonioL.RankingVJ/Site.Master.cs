using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            var ctx = Context.GetOwinContext();
            var authManager = ctx.Authentication;
            authManager.SignOut();
            Response.Redirect("~/Account/Login.aspx");
        }

        public string GetUserRole()
        {
            var user = HttpContext.Current.User;
            if (user.IsInRole("Administrador"))
                return "Administrador";
            else if (user.IsInRole("Auxiliar"))
                return "Auxiliar";
            return string.Empty;
        }
    }
}