<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DetalleVideoJuego.aspx.cs" Inherits="AntonioL.RankingVJ.Pages.DetalleVideoJuego" MasterPageFile="~/Site.Master"%>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .detail-container {
            max-width: 800px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }
        .detail-row {
            display: flex;
            margin-bottom: 15px;
            padding-bottom: 15px;
            border-bottom: 1px solid #eee;
        }
        .detail-label {
            font-weight: bold;
            width: 200px;
        }
        .detail-value {
            flex: 1;
        }
        .detail-header {
            text-align: center;
            margin-bottom: 30px;
        }
        .detail-actions {
            text-align: center;
            margin-top: 30px;
        }
    </style>

    <div class="detail-container">
        <div class="detail-header">
            <h2>Detalles del Videojuego</h2>
        </div>

        <div class="detail-row">
            <div class="detail-label">ID:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlId" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Nombre:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlNombre" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Compañía:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlCompania" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Año de Lanzamiento:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlAnio" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Precio:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlPrecio" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Puntaje:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlPuntaje" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Fecha de Actualización:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlFechaActualizacion" runat="server" />
            </div>
        </div>

        <div class="detail-row">
            <div class="detail-label">Actualizado por:</div>
            <div class="detail-value">
                <asp:Literal ID="ltlUsuarioActualizacion" runat="server" />
            </div>
        </div>

        <div class="detail-actions">
            <asp:HyperLink ID="lnkVolver" runat="server" NavigateUrl="~/Pages/ListaVideojuegos.aspx"
                Text="Volver a la lista" CssClass="btn btn-primary" />
        </div>
    </div>
</asp:Content>
