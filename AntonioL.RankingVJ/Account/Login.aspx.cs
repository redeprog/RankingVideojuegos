using AntonioL.BLL;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //txtCorreo.Text = string.Empty;
            //txtContraseña.Text = string.Empty;
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var service = new AuthService();
            var user = service.Login(txtCorreo.Text, txtContraseña.Text);

            if (user != null)
            {
                var ident = service.UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
                HttpContext.Current.GetOwinContext().Authentication.SignIn(new AuthenticationProperties(), ident);
                Response.Redirect("~/Home.aspx");
            }
            else
            {
                lblResultado.Text = "<div class='alert alert-danger'>Credenciales incorrectas.</div>";
            }
        }
    }
}