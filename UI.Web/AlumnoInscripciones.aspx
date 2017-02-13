<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AlumnoInscripciones.aspx.cs" Inherits="UI.Web.AlumnoInscripciones" %>

<asp:Content ID="Inscripciones" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanelInscripciones" runat="server">
        <asp:GridView ID="gdvAlumno_Incripcion" runat="server" SkinID="Professional" AutoGenerateColumns="False" OnSelectedIndexChanged="gdvIncripcion_SelectedIndexChanged" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" Width="400
            px" HeaderStyle-HorizontalAlign ="Left">
         <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                 <asp:BoundField DataField="MAteria" HeaderText="Materia" />
                 <asp:BoundField DataField="COmision" HeaderText="Comision" />
                 <asp:BoundField DataField="Condicion" HeaderText="Condicion" />
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
   </asp:Panel>
    <asp:Panel ID="formPanelInscripcion" runat="server" Visible="false" style="margin-bottom: 71px">
        <br />
        <br />
        <asp:GridView ID="gdvInscripcionesCurso" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" Width="400
            px" HeaderStyle-HorizontalAlign ="Left">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             <Columns>
                 <asp:BoundField DataField="IdCurso" HeaderText="Curso" />
                 <asp:BoundField DataField="MAte" HeaderText="Materia" />
                 <asp:BoundField DataField="COmi" HeaderText="Comision" />
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
        <asp:Label ID="lblNoMateria" runat="server" ForeColor="#FF3300" Text="NO SE PUEDE INSCRIBIR A NINGUNA MATERIA" Visible="False"></asp:Label>
        <br />
        </asp:Panel>
                    <asp:Panel ID="gridActionsPanel" runat="server">
                        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click">Eliminar Inscripcion</asp:LinkButton>
                        <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Inscribirse</asp:LinkButton>
                    </asp:Panel>
                    <asp:Panel ID="formActionsPanel" runat="server" Visible="false">
                        <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click">Aceptar</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>
                    </asp:Panel>
</asp:Content>