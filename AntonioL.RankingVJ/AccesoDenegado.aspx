<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AccesoDenegado.aspx.cs" Inherits="AntonioL.RankingVJ.AccesoDenegado"  MasterPageFile="~/Site.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container text-center mt-5">
        <div class="alert alert-danger" role="alert">
            <h4 class="alert-heading">Acceso Denegado</h4>
            <p>No tienes permiso para acceder a esta sección.</p>
            <hr>
            <a href="Login.aspx" class="btn btn-primary">Volver al Login</a>
        </div>
    </div>
</asp:Content>
