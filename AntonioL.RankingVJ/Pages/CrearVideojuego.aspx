<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CrearVideojuego.aspx.cs" Inherits="AntonioL.RankingVJ.Pages.CrearVideojuego" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .detail-container {
            max-width: 500px;
            margin: 0 auto;
            padding: 20px;
            background-color: #f9f9f9;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
            display: flex;
            flex-direction: column;
            align-items: center; /* Centra horizontalmente los hijos */
        }

        .form-group {
            width: 80%; /* Ancho reducido para mejor centrado visual */
            margin-bottom: 15px;
            text-align: left; /* Alinea el texto a la izquierda */
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
            text-align: left;
        }

        .form-control, .form-select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }

        .detail-header {
            text-align: center;
            margin-bottom: 30px;
            width: 100%; /* Ocupa todo el ancho del container */
        }

        .detail-actions {
            text-align: center;
            margin-top: 30px;
            width: 80%; /* Mismo ancho que form-group */
        }

        .text-danger {
            color: #dc3545;
            font-size: 0.875em;
            display: block;
            margin-top: 5px;
        }

        .btn {
            padding: 8px 15px;
            border-radius: 4px;
            cursor: pointer;
            margin-right: 10px;
        }
    </style>
    <div class="detail-container">
        <div class="detail-header">
            <h2>
                <asp:Literal ID="ltlTitulo" runat="server" /></h2>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtNombre">Nombre</asp:Label>
            <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" />
            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNombre" ErrorMessage="Requerido" CssClass="text-danger" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="ddlCompania">Compañía</asp:Label>
            <asp:DropDownList ID="ddlCompania" runat="server" CssClass="form-select"
                DataTextField="Nombre" DataValueField="Id" />
        </div>
        <br />
        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtAnio">Año de Lanzamiento</asp:Label>
            <asp:TextBox ID="txtAnio" runat="server" CssClass="form-control" TextMode="Number" />
            <asp:RangeValidator runat="server" ControlToValidate="txtAnio"
                Type="Integer" MinimumValue="1950" MaximumValue="2100"
                ErrorMessage="Año inválido" CssClass="text-danger" />
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPrecio">Precio</asp:Label>
            <asp:TextBox ID="txtPrecio" runat="server" CssClass="form-control" TextMode="Number" step="0.01" />
            <asp:RegularExpressionValidator runat="server" ControlToValidate="txtPrecio"
                ValidationExpression="^\d+(\.\d{1,2})?$"
                ErrorMessage="Formato inválido (ej: 39.99)" CssClass="text-danger" />
            <%--<asp:RangeValidator runat="server" ControlToValidate="txtPrecio"
            Type="Double" MinimumValue="0" MaximumValue="10000"
            ErrorMessage="Precio inválido" CssClass="text-danger" />--%>
        </div>

        <div class="form-group">
            <asp:Label runat="server" AssociatedControlID="txtPuntaje">Puntaje (0-5)</asp:Label>
            <asp:TextBox ID="txtPuntaje" runat="server" CssClass="form-control" TextMode="Number" step="0.01" />
            <asp:RangeValidator runat="server" ControlToValidate="txtPuntaje"
                Type="Double" MinimumValue="0" MaximumValue="5"
                ErrorMessage="Puntaje debe ser entre 0 y 5" CssClass="text-danger" />
        </div>

        <div class="form-group">
            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
            <asp:HyperLink ID="lnkCancelar" runat="server" NavigateUrl="~/Pages/ListaVideojuegos.aspx"
                Text="Cancelar" CssClass="btn btn-default" />
        </div>
    </div>
</asp:Content>
