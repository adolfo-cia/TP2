<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master"CodeBehind="subirNotas.aspx.cs" Inherits="UI.Web.subirNotas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
 <asp:Panel ID="gridPanelDocenteCursos" runat="server">
    <asp:GridView ID="GridViewDocenteCurso" runat="server" DataKeyNames="ID" SkinID="Professional" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridViewDocenteCurso_SelectedIndexChanged">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
          
        <Columns>
                 <asp:BoundField DataField="MAteria" HeaderText="Materia" />
                 <asp:BoundField DataField="COmision" HeaderText="Comision" />
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

<asp:Panel ID="formPanelNotas" runat="server" Visible="false" style="margin-bottom: 71px">
        <br />
        <br />
        <asp:GridView ID="gdvInscripcionesCurso" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" Width="400
            px" HeaderStyle-HorizontalAlign ="Left">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
             <Columns>
                 <asp:BoundField DataField="ALumno" HeaderText="Alumno" />
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
        <asp:Label ID="lblNoInscriptos" runat="server" ForeColor="#FF3300" Text="NO HAY INSCRIPTOS A LA MATERIA" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="lblNota" runat="server" Text="NOTA :" Visible="False"></asp:Label>
        <asp:TextBox ID="txtNota" runat="server" Visible="False" Width="69px"></asp:TextBox>
        <asp:LinkButton ID="lnkSubir" runat="server" OnClick="lnkSubir_Click" >SUBIR</asp:LinkButton>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNota" Display="Dynamic" ErrorMessage="El campo es requerido" ForeColor="Red"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtNota" Display="Dynamic" ErrorMessage="La nota debe ser numero entre 1 y 10" ForeColor="Red" MaximumValue="10" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        </asp:Panel>
                    <asp:Panel ID="gridActionsPanel" runat="server" Height="16px">
                       <asp:LinkButton ID="lnkCargarNota" runat="server" OnClick="lnkCargarNota_Click">Cargar Nota</asp:LinkButton>
                    </asp:Panel>
                    <asp:Panel ID="formActionsPanel" runat="server" Visible="false" Height="16px" Width="1041px">
                        <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click" CausesValidation="False">Aceptar</asp:LinkButton>
                        <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>
                    </asp:Panel>
</asp:Content>