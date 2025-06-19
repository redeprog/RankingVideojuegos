using AntonioL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ
{
    public partial class RegistroUsuario : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //    txtContraseña.Text = string.Empty;
            //    txtCorreo.Text = string.Empty;

        }

        protected void btnRegistrar_Click(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            var service = new AuthService();
            var result = service.Register(txtCorreo.Text, txtContraseña.Text, ddlRol.SelectedValue);

            if (result.Succeeded)
                lblResultado.Text = "<div class='alert alert-success'>Registro exitoso</div>";
            else
                lblResultado.Text = "<div class='alert alert-danger'>" + string.Join("<br/>", result.Errors) + "</div>";
        }
    }
}