<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ListaVideojuegos.aspx.cs" Inherits="AntonioL.RankingVJ.Pages.ListaVideojuegos" MasterPageFile="~/Site.Master" %>

<asp:Content ID="MainContent" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        /* Fondo semitransparente oscuro */
        .modalBackground {
            background-color: rgba(0, 0, 0, 0.7); /* Negro con 70% opacidad */
            position: fixed;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            z-index: 1000;
        }

        /* Contenedor del modal */
        .modalPopup {
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            padding: 20px;
            position: fixed;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            z-index: 1001;
            min-width: 300px;
            box-shadow: 0 5px 15px rgba(0,0,0,0.5);
        }

        /* Estructura del contenido */
        .modal-content {
            display: flex;
            flex-direction: column;
        }

        .modal-header {
            padding: 10px 15px;
            border-bottom: 1px solid #eee;
            font-weight: bold;
            font-size: 1.2em;
        }

        .modal-body {
            padding: 15px;
        }

        .modal-footer {
            padding: 10px 15px;
            border-top: 1px solid #eee;
            text-align: right;
        }

            .modal-footer .btn {
                margin-left: 10px; /* Espacio a la izquierda de cada botón */
            }

            /* O específicamente para estos botones */
            .modal-footer .btn-secondary {
                margin-right: 10px; /* Espacio a la derecha del botón Cancelar */
            }

        .filtros-container {
            background: #f8f9fa;
            padding: 15px;
            border-radius: 5px;
            margin-bottom: 20px;
            display: flex;
            flex-wrap: wrap;
            gap: 15px;
            align-items: flex-end;
        }

        .filtro-group {
            flex: 1;
            min-width: 200px;
        }

        .filtro-actions {
            flex-shrink: 0;
        }
    </style>

    <div class="filtros-container d-flex justify-content-between align-items-center mb-3">
        <h3>CRUD VIDEOJUEGOS - RETO 04</h3>
<%--        <asp:Button
            ID="btnGenerarRanking"
            runat="server"
            CssClass="btn btn-primary"
            Text="Generar Ranking"
            OnClick="btnGenerarRanking_Click" />--%>
        <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-bs-target="#rankingModal">
            Generar Ranking
        </button>
    </div>
    <!-- Contenedor de filtros -->
    <div class="filtros-container">
        <div class="filtro-group">
            <asp:Label runat="server" AssociatedControlID="txtFiltroNombre">Nombre del Videojuego</asp:Label>
            <asp:TextBox ID="txtFiltroNombre" runat="server" CssClass="form-control" placeholder="Filtrar por nombre" />
        </div>

        <div class="filtro-group">
            <asp:Label runat="server" AssociatedControlID="ddlFiltroCompania">Compañía</asp:Label>
            <asp:DropDownList ID="ddlFiltroCompania" runat="server" CssClass="form-select"
                DataTextField="Nombre" DataValueField="Id">
                <asp:ListItem Text="Todas las compañías" Value="" Selected="True" />
            </asp:DropDownList>
        </div>

        <div class="filtro-group">
            <asp:Label runat="server" AssociatedControlID="txtFiltroAnio">Año de Lanzamiento</asp:Label>
            <asp:TextBox ID="txtFiltroAnio" runat="server" CssClass="form-control" TextMode="Number"
                placeholder="Filtrar por año" />
        </div>

        <div class="filtro-actions">
            <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click"
                CssClass="btn btn-primary" />
            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click"
                CssClass="btn btn-secondary" />
        </div>
    </div>


    <asp:GridView ID="gvVideojuegos" runat="server" AutoGenerateColumns="false"
        CssClass="table table-striped table-bordered"
        DataKeyNames="Id" OnRowCommand="gvVideojuegos_RowCommand"
        AllowPaging="true" PageSize="5"
        OnPageIndexChanging="gvVideojuegos_PageIndexChanging" PagerStyle-CssClass="pagination">
        <Columns>
            <%--<asp:BoundField DataField="Id" HeaderText="ID" />--%>
            <asp:TemplateField HeaderText="ID">
                <ItemTemplate>
                    <asp:HyperLink ID="lnkDetalle" runat="server"
                        NavigateUrl='<%# $"~/Pages/DetalleVideojuego.aspx?id={Eval("Id")}" %>'
                        Text='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Nombre" HeaderText="Nombre" />
            <asp:BoundField DataField="CompaniaNombre" HeaderText="Compañía" />
            <asp:BoundField DataField="AnioLanzamiento" HeaderText="Año" />
            <asp:BoundField DataField="Precio" HeaderText="Precio" DataFormatString="{0:C}" />
            <asp:BoundField DataField="Puntaje" HeaderText="Puntaje" DataFormatString="{0:0.00}" />
            <asp:BoundField DataField="FechaActualizacion" HeaderText="Última Actualización" DataFormatString="{0:dd/MM/yyyy HH:mm}" />
            <asp:BoundField DataField="UsuarioActualizacionNombre" HeaderText="Actualizado por" />
            <asp:TemplateField HeaderText="Acciones">
                <ItemTemplate>
                    <asp:LinkButton runat="server" CommandName="CustomEdit" CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-primary" Text="Actualizar" />
                    <asp:LinkButton runat="server" CommandName="CustomDelete" CommandArgument='<%# Eval("Id") %>'
                        CssClass="btn btn-sm btn-danger" Text="Eliminar"
                        Visible='<%# User.IsInRole("Administrador") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>

    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo Videojuego" OnClick="btnNuevo_Click" CssClass="btn btn-success" />

    <!-- Modal de Confirmación -->
    <ajaxToolkit:ModalPopupExtender ID="mpeConfirmar" runat="server"
        TargetControlID="btnFake"
        PopupControlID="pnlConfirmar"
        CancelControlID="btnCancelar"
        BackgroundCssClass="modalBackground"
        PopupDragHandleControlID="modalHeader"
        Drag="true" />
    <asp:Panel ID="pnlConfirmar" runat="server" CssClass="modalPopup" Style="display: none;">
        <div class="modal-content">
            <div class="modal-header">
                <h4>Confirmar Eliminación</h4>
            </div>
            <div class="modal-body">
                ¿Está seguro que desea eliminar este videojuego?
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnConfirmar" runat="server" Text="Eliminar" CssClass="btn btn-danger" OnClick="btnConfirmar_Click" />
                <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
            </div>
        </div>
    </asp:Panel>
    <!-- Modal de Confirmación para Editar -->
    <ajaxToolkit:ModalPopupExtender ID="mpeConfirmarEdicion" runat="server"
        TargetControlID="btnFakeEditar"
        PopupControlID="pnlConfirmarEdicion"
        CancelControlID="btnCancelarEdicion"
        BackgroundCssClass="modalBackground"
        PopupDragHandleControlID="modalHeaderEdicion"
        Drag="true" />
    <asp:Panel ID="pnlConfirmarEdicion" runat="server" CssClass="modalPopup" Style="display: none;">
        <div class="modal-content">
            <div class="modal-header" id="modalHeaderEdicion">
                <h4>Confirmar Actualización</h4>
            </div>
            <div class="modal-body">
                ¿Está seguro que desea actualizar este videojuego?
            </div>
            <div class="modal-footer">
                <asp:Button ID="btnConfirmarEdicion" runat="server" Text="Actualizar" CssClass="btn btn-primary" OnClick="btnConfirmarEdicion_Click" />
                <asp:Button ID="btnCancelarEdicion" runat="server" Text="Cancelar" CssClass="btn btn-secondary" />
            </div>
        </div>
    </asp:Panel>
    <!-- Modal para generar ranking -->
    <div class="modal fade" id="rankingModal" tabindex="-1" aria-labelledby="rankingModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="rankingModalLabel">Generar Ranking de Videojuegos - Reto 06</h5>
                    <button type="button" class="btn-close" data-bs-dismiss="modal" aria-label="Close"></button>
                </div>
                <div class="modal-body">
                    <div class="mb-3">
                        <label for="txtTop" class="form-label">Top deseado (0 para todos):</label>
                        <asp:TextBox ID="txtTop" runat="server" CssClass="form-control" TextMode="Number" min="0"></asp:TextBox>
                        <asp:RegularExpressionValidator ID="revTop" runat="server"
                            ControlToValidate="txtTop"
                            ValidationExpression="^\d+$"
                            ErrorMessage="Solo valores enteros positivos"
                            CssClass="text-danger"
                            Display="Dynamic"></asp:RegularExpressionValidator>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-bs-dismiss="modal">Cancelar</button>
                    <asp:Button ID="btnGenerarCSV" runat="server" CssClass="btn btn-primary"
                        Text="Generar y Descargar" OnClick="btnGenerarCSV_Click" />
                </div>
            </div>
        </div>
    </div>
    <asp:Button ID="btnFake" runat="server" Style="display: none;" />
    <asp:Button ID="btnFakeEditar" runat="server" Style="display: none;" />
</asp:Content>
