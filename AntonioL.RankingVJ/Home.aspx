<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="AntonioL.RankingVJ.Home" MasterPageFile="~/Site.Master"%>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Bienvenido</h2>
    <p class="lead">Has iniciado sesión correctamente como <%= User.Identity.Name %>.</p>
    <% if (!string.IsNullOrEmpty(UserRole)) { %>
        <p class="text-info">Tu rol es: <strong><%= UserRole %></strong></p>
    <% } %>

    <p class="lead">Para gestionar los Videojuegos con la funcinalidad permitida ir a la opción <a href="/Pages/Listavideojuegos.aspx">Videojuegos</a>.</p>
</asp:Content>
