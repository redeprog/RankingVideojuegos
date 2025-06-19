using AntonioL.BLL.Services;
using AntonioL.DAL.Logic;
using AntonioL.Share.Dtos;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AntonioL.RankingVJ.Pages
{
    public partial class CrearVideojuego : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!User.Identity.IsAuthenticated)
                Response.Redirect("~/Account/Login.aspx");

            if (!User.IsInRole("Administrador") && !User.IsInRole("Auxiliar"))
                Response.Redirect("~/AccesoDenegado.aspx");

            CargarCompanias();

            if (Request.QueryString["id"] != null)
            {
                ltlTitulo.Text = "Actualizar Videojuego";
                CargarVideojuego(Convert.ToInt32(Request.QueryString["id"]));
            }
            else
            {
                ltlTitulo.Text = "Crear Nuevo Videojuego";
            }
        }

        private void CargarCompanias()
        {
            if (!IsPostBack) // Solo cargar en la primera vez
            {
                var service = new VideojuegoService(new UnitOfWork());
                ddlCompania.DataSource = service.GetAllCompanias().ToList();
                ddlCompania.DataBind();
                ddlCompania.Items.Insert(0, new ListItem("-- Seleccione --", ""));
            }
        }

        private void CargarVideojuego(int id)
        {
            var service = new VideojuegoService(new UnitOfWork());
            var videojuego = service.GetById(id);

            txtNombre.Text = videojuego.Nombre;
            if (videojuego.CompaniaId.HasValue)
                ddlCompania.SelectedValue = videojuego.CompaniaId.ToString();

            txtAnio.Text = videojuego.AnioLanzamiento?.ToString();
            txtPrecio.Text = videojuego.Precio?.ToString("0.00", CultureInfo.InvariantCulture);
            txtPuntaje.Text = videojuego.Puntaje?.ToString("0.00", CultureInfo.InvariantCulture);
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            CultureInfo culture = CultureInfo.InvariantCulture;
            var service = new VideojuegoService(new UnitOfWork());
            var videojuegoDto = new VideojuegoDTO
            {
                Nombre = txtNombre.Text,
                CompaniaId = string.IsNullOrEmpty(ddlCompania.SelectedItem.Value) ? (int?)null : Convert.ToInt32(ddlCompania.SelectedItem.Value),
                AnioLanzamiento = string.IsNullOrEmpty(txtAnio.Text) ?
                    (int?)null : Convert.ToInt32(txtAnio.Text),
                Precio = string.IsNullOrEmpty(txtPrecio.Text) ?
                    (decimal?)null : Convert.ToDecimal(txtPrecio.Text, culture),
                Puntaje = string.IsNullOrEmpty(txtPuntaje.Text) ?
                    (decimal?)null : Convert.ToDecimal(txtPuntaje.Text, culture)
            };

            if (Request.QueryString["id"] != null)
            {
                videojuegoDto.Id = Convert.ToInt32(Request.QueryString["id"]);
                service.Update(videojuegoDto, User.Identity.GetUserId());
            }
            else
            {
                service.Create(videojuegoDto, User.Identity.GetUserId());
            }

            Response.Redirect("~/Pages/ListaVideojuegos.aspx");
        }

    }
}