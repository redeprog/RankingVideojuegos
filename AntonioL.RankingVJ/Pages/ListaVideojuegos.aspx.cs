using AjaxControlToolkit;
using AntonioL.BLL.Services;
using AntonioL.DAL.Logic;
using AntonioL.DAL.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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

        protected void btnGenerarRanking_Click(object sender, EventArgs e)
        {
            // Lógica para generar el ranking (ejemplo: exportar a Excel, filtrar datos, etc.)
            string resultado = GenerarRanking(); // Método personalizado
            ClientScript.RegisterStartupScript(this.GetType(), "alert", $"alert('{resultado}');", true);
        }

        private string GenerarRanking()
        {
            // Ejemplo: Consultar la base de datos y ordenar por puntaje
            //using (var context = new TuDbContext())
            //{
            //    var ranking = context.Videojuegos
            //        .OrderByDescending(v => v.Puntaje)
            //        .Take(10)
            //        .ToList();

            //    // Procesar los datos (ejemplo: crear un CSV o mostrar en pantalla)
            //return $"Ranking generado con {ranking.Count} videojuegos. Puntaje más alto: {ranking.First().Puntaje}";
            //}
            return $"Ranking generado con videojuegos. Puntaje más alto: ";
        }

        protected void btnGenerarCSV_Click(object sender, EventArgs e)
        {
            int top = 20; // Valor por defecto

            if (!string.IsNullOrEmpty(txtTop.Text))
            {
                if (!int.TryParse(txtTop.Text, out top) || top < 0)
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "alert",
                        "alert('El valor ingresado no es válido. Se usará el top 20 por defecto.');", true);
                    top = 20;
                }
            }

            // Generar y descargar el CSV
            GenerarYDescargarCSV(top);
        }

        private void GenerarYDescargarCSV(int top)
        {
            using (var context = new OL_Gamers_ALEntities())
            {
                // Obtener videojuegos con su puntaje promedio y compañía
                var videojuegos = context.Videojuegos
                    .Include(v => v.Compania)
                    .Select(v => new
                    {
                        v.nombre,
                        Compania = v.Compania.nombre,
                        Puntaje = context.Calificaciones
                            .Where(c => c.id_videojuego == v.id_videojuego)
                            .Average(c => (double?)c.puntuacion) ?? 0,
                        v.id_videojuego
                    })
                    .OrderByDescending(v => v.Puntaje)
                    .ToList();

                // Aplicar top si es mayor que 0
                if (top > 0)
                {
                    videojuegos = videojuegos.Take(top).ToList();
                }

                // Determinar clasificación
                int mitad = top > 0 ? top / 2 : videojuegos.Count / 2;
                var ranking = videojuegos.Select((v, index) => new
                {
                    v.nombre,
                    v.Compania,
                    v.Puntaje,
                    Clasificacion = index < mitad ? "GOTY" : "AAA"
                }).ToList();

                // Generar CSV
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Nombre|Compañía|Puntaje|Clasificación");

                foreach (var item in ranking)
                {
                    sb.AppendLine($"{item.nombre}|{item.Compania}|{item.Puntaje:F2}|{item.Clasificacion}");
                }

                // Descargar archivo
                Response.Clear();
                Response.Buffer = true;
                //Response.AddHeader("content-disposition", $"attachment;filename=ranking_videojuegos_top{(top > 0 ? top : "all")}_{DateTime.Now:yyyyMMdd}.csv");
                Response.AddHeader("content-disposition", $"attachment;filename=ranking_videojuegos_top{(top > 0 ? top.ToString() : "all")}_{DateTime.Now:yyyyMMdd}.csv");
                Response.Charset = "";
                Response.ContentType = "application/text";
                Response.Output.Write(sb.ToString());
                Response.Flush();
                Response.End();
            }
        }
    }
}