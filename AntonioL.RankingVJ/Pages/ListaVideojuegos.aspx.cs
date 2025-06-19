using AntonioL.BLL.Services;
using AntonioL.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ.Pages
{
    public partial class ListaVideojuegos : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login.aspx");

            if (!User.IsInRole("Administrador") && !User.IsInRole("Auxiliar"))
                Response.Redirect("~/AccesoDenegado.aspx");

            if (!IsPostBack)
            {
                CargarFiltros();
                CargarVideojuegos();
            }
        }

        private void CargarFiltros()
        {
            var service = new VideojuegoService(new UnitOfWork());

            // Cargar compañías para el dropdown de filtro
            var companias = service.GetAllCompanias().ToList();
            ddlFiltroCompania.DataSource = companias;
            ddlFiltroCompania.DataBind();
            ddlFiltroCompania.Items.Insert(0, new ListItem("Todas las compañías", ""));
        }

        private void CargarVideojuegos()
        {
            var service = new VideojuegoService(new UnitOfWork());

            // Obtener valores de filtro
            string filtroNombre = txtFiltroNombre.Text.Trim();
            string filtroCompaniaId = ddlFiltroCompania.SelectedValue;
            string filtroAnio = txtFiltroAnio.Text.Trim();

            // Aplicar filtros
            var videojuegos = service.GetAll()
                .Where(v => string.IsNullOrEmpty(filtroNombre) || v.Nombre.Contains(filtroNombre))
                .Where(v => string.IsNullOrEmpty(filtroCompaniaId) || v.CompaniaId.ToString() == filtroCompaniaId)
                .Where(v => string.IsNullOrEmpty(filtroAnio) || v.AnioLanzamiento.ToString() == filtroAnio)
                .ToList();

            //gvVideojuegos.DataSource = service.GetAll().ToList();
            gvVideojuegos.DataSource = videojuegos;
            gvVideojuegos.DataBind();
        }

        protected void gvVideojuegos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvVideojuegos.PageIndex = e.NewPageIndex;
            CargarVideojuegos();
        }

        protected void gvVideojuegos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "CustomEdit")
            {
                //Response.Redirect($"~/Pages/CrearVideojuego.aspx?id={e.CommandArgument}");
                ViewState["IdAEditar"] = e.CommandArgument;
                mpeConfirmarEdicion.Show();
            }
            else if (e.CommandName == "CustomDelete")
            {
                ViewState["IdAEliminar"] = e.CommandArgument;
                mpeConfirmar.Show();
            }
        }

        protected void btnConfirmar_Click(object sender, EventArgs e)
        {
            var service = new VideojuegoService(new UnitOfWork());
            service.Delete(Convert.ToInt32(ViewState["IdAEliminar"]));
            CargarVideojuegos();
            mpeConfirmar.Hide();
        }

        protected void btnConfirmarEdicion_Click(object sender, EventArgs e)
        {
            if (ViewState["IdAEditar"] != null)
            {
                Response.Redirect($"~/Pages/CrearVideojuego.aspx?id={ViewState["IdAEditar"]}");
            }
            mpeConfirmarEdicion.Hide();
        }

        protected void btnCancelarEdicion_Click(object sender, EventArgs e)
        {
            mpeConfirmarEdicion.Hide();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/CrearVideojuego.aspx");
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarVideojuegos();
        }
        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtFiltroNombre.Text = "";
            ddlFiltroCompania.SelectedIndex = 0;
            txtFiltroAnio.Text = "";
            CargarVideojuegos();
        }
    }
}