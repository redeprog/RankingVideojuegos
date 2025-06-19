using AntonioL.BLL.Services;
using AntonioL.DAL.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ.Pages
{
    public partial class DetalleVideoJuego : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login.aspx");

            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    CargarDetalles(Convert.ToInt32(Request.QueryString["id"]));
                }
                else
                {
                    // Si no hay ID, redirigir a la lista
                    Response.Redirect("~/Pages/ListaVideojuegos.aspx");
                }
            }
        }

        private void CargarDetalles(int id)
        {
            var service = new VideojuegoService(new UnitOfWork());
            var serviceCia = new CompaniaService(new UnitOfWork());

            var videojuego = service.GetById(id);

            if (videojuego == null)
            {
                // Si no se encuentra el videojuego, redirigir a la lista
                Response.Redirect("~/Pages/ListaVideojuegos.aspx");
                return;
            }

            if (videojuego.CompaniaId > 0)
            {
                var compania = serviceCia.GetById((int)videojuego.CompaniaId);
                videojuego.CompaniaNombre = compania.Nombre;
            }

            ltlId.Text = videojuego.Id.ToString();
            ltlNombre.Text = videojuego.Nombre;
            ltlCompania.Text = videojuego.CompaniaNombre ?? "No especificado";
            ltlAnio.Text = videojuego.AnioLanzamiento?.ToString() ?? "No especificado";
            ltlPrecio.Text = videojuego.Precio?.ToString("C", CultureInfo.CurrentCulture) ?? "No especificado";
            ltlPuntaje.Text = videojuego.Puntaje?.ToString("0.00") ?? "No especificado";
            ltlFechaActualizacion.Text = videojuego.FechaActualizacion.ToString("dd/MM/yyyy HH:mm");
            ltlUsuarioActualizacion.Text = videojuego.UsuarioActualizacionNombre ?? "Desconocido";
        }
    }
}