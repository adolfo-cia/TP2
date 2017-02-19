<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Docentes_cursos.aspx.cs" Inherits="UI_Web.Docentes_cursos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">

     <asp:Panel ID="gridPanel" runat="server">
       <%-- <table style="width:100%;">
            <tr>
                <td class="auto-style1" style="width: 263px">&nbsp;</td>
                <td style="width: 536px">--%>
                    <asp:GridView ID="gridDocenteCurso" runat="server" SkinID="Professional" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="ID" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gridDocenteCurso_SelectedIndexChanged" Width="506px"
                        HeaderStyle-HorizontalAlign ="Left" style="margin-right: 0px">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:BoundField DataField="NombreCompleto" HeaderText="Profesor" />
                            <asp:BoundField DataField="Cargo" HeaderText="Cargo" />
                            <asp:BoundField DataField="DMateria" HeaderText="Materia" />
                            <asp:BoundField DataField="DComision" HeaderText="Comision" />
                            <asp:BoundField DataField="AnioCalendario" HeaderText="Año Calendario" />
                            <asp:CommandField SelectText="Seleccionar" ShowSelectButton="true" />
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>
          <%--      </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="width: 263px">&nbsp;</td>
                <td style="width: 536px">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style1" style="width: 263px">&nbsp;</td>
                <td style="width: 536px">--%>
                    <asp:Panel ID="formPanelDocenteCurso" runat="server" Visible="false">
                        <asp:Label ID="lblDocente" runat="server" Text="Docente:"></asp:Label>
                        <asp:DropDownList ID="ddlDocente" runat="server" AutoPostBack="true">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlDocente" Display="Dynamic" ErrorMessage="El Docente es requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="lblCurso" runat="server" Text="Curso:"></asp:Label>
                        <asp:DropDownList ID="ddlCurso" runat="server">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlCurso" Display="Dynamic" ErrorMessage="El Curso es requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Label ID="lblCargo" runat="server" Text="Cargo:"></asp:Label>
                        <asp:DropDownList ID="ddlCargo" runat="server" ClientIDMode="Static">
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlCargo" Display="Dynamic" ErrorMessage="El cargo es requerido" ForeColor="Red"></asp:RequiredFieldValidator>
                        <br />
                    </asp:Panel>
                    <asp:Panel ID="gridActionsPanel" runat="server">
                        <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
                        <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
                        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click">Eliminar</asp:LinkButton>
                    </asp:Panel>
                    <asp:Panel ID="formActionsPanel" runat="server" Visible="false">
                        <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click">Aceptar</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>
                    </asp:Panel>
    <%--            </td>
                <td>&nbsp;</td>
            </tr>
        </table>--%>
    </asp:Panel>

</asp:Content>
