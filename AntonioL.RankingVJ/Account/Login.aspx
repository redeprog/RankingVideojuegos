<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AntonioL.RankingVJ.Login" MasterPageFile="~/Site.Master"%>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        body {
            background-color: #f5f5f5;  Fondo gris claro 
            /*min-height: 100vh;*/
            /*display: flex;*/
            align-items: center;
        }
        
        .login-card {
            max-width: 400px;
            width: 100%;
            margin: 0 auto;
            border-radius: 10px;
            box-shadow: 0 5px 20px rgba(0,0,0,0.1);
            overflow: hidden;
        }
        
        .card-header {
            background-color: #343a40;
            color: white;
            text-align: center;
            padding: 20px;
        }
        
        .card-body {
            padding: 30px;
            background-color: white;
            display: flex;
            flex-direction: column;
            align-items: center;
        }
        
        .btn-login {
            width: 100%;
            padding: 10px;
            font-weight: 600;
        }
        
        .divider {
            text-align: center;
            margin: 20px 0;
            position: relative;
        }
        
        .divider::before {
            content: "";
            position: absolute;
            top: 50%;
            left: 0;
            right: 0;
            height: 1px;
            background-color: #eee;
            z-index: 1;
        }
        
        .divider span {
            background-color: white;
            position: relative;
            z-index: 2;
            padding: 0 15px;
            color: #6c757d;
        }
    </style>
    <div class="container">
        <div class="login-card">
            <div class="card-header">
                <h3>Iniciar Sesión</h3>
            </div>
            <div class="card-body">
                <asp:ValidationSummary runat="server" CssClass="alert alert-danger" />
                <asp:Label ID="lblResultado" runat="server" /><br />
                <div class="mb-3">
                    <asp:TextBox ID="txtCorreo" runat="server" CssClass="form-control" Placeholder="Correo electrónico" />
                    <asp:RequiredFieldValidator ControlToValidate="txtCorreo" runat="server" ErrorMessage="Correo requerido" CssClass="text-danger" />
                </div>
                <div class="mb-3">
                    <asp:TextBox ID="txtContraseña" runat="server" TextMode="Password" CssClass="form-control" Placeholder="Contraseña" />
                    <asp:RequiredFieldValidator ControlToValidate="txtContraseña" runat="server" ErrorMessage="Contraseña requerida" CssClass="text-danger" />
                </div>
                <div class="d-grid gap-2 mb-3">
                    <asp:Button ID="btnLogin" runat="server" Text="Ingresar" OnClick="btnLogin_Click" CssClass="btn btn-primary" />
                </div>
                <div class="text-center">
                    <p class="mb-2">¿No tienes una cuenta?</p>
                    <asp:HyperLink ID="lnkRegistro" runat="server" 
                        NavigateUrl="~/Account/RegistroUsuario.aspx" 
                        CssClass="text-muted small">
                        Regístrate aquí
                    </asp:HyperLink>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
