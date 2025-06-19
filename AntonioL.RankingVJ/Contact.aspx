<%@ Page Title="Contact" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="AntonioL.RankingVJ.Contact" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main aria-labelledby="title">
        <h2 id="title"><%: Title %>.</h2>
        <h3>El Desarrollador</h3>
        <address>
            Floridablanca, Santander<br />
            Autopista<br />
            <abbr title="Phone">P:</abbr>
            +573138756155
        </address>

        <address>
            <strong>Support:</strong>   <a href="mailto:Support@example.com">lopezchantonio@gmail.com</a><br />
            <strong>Marketing:</strong> <a href="mailto:Marketing@example.com">atony100@hotmail.com</a>
        </address>
    </main>
</asp:Content>
