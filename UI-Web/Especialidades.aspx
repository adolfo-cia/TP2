<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Especialidades.aspx.cs" Inherits="UI.Web.Especialidades" %>

<asp:Content ID="Content3" runat="server" ContentPlaceHolderID="bodyContentPlaceHolder">

    <asp:Panel ID="gridPanelEspecialidades" runat="server">
        <asp:GridView ID="gridEspecialidades" runat="server"  SkinID="Professional" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gridEspecialidades_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:CommandField ShowSelectButton="True" />
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

    <asp:Panel ID="EspecialidadesPanel" runat="server" Visible="false">
            <asp:Label ID="lblDescEsp" runat="server" Text="Descripción"></asp:Label>
            <asp:TextBox ID="txtDescEsp" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescEsp" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    </asp:Panel>

    <asp:Panel ID="gridEspecialidadesActionPanel" runat="server">
        <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
        <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click">Eliminar</asp:LinkButton>
    </asp:Panel>

    <asp:Panel ID="formEspecialidadesActionPanel" runat="server" Visible="false">
        <asp:LinkButton ID="btnAceptar" runat="server" OnClick="btnAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton ID="btCancelar" runat="server" OnClick="btCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>


    </asp:Panel>
</asp:Content>
