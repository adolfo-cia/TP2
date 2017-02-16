<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.Master" CodeBehind="Comisiones.aspx.cs" Inherits="UI_Web.Comisiones" %>
<asp:Content ID="content4" runat="server" ContentPlaceHolderID="bodyContentPlaceHolder">

    <asp:Panel ID="panelGridComisiones" runat="server">
        <asp:GridView ID="gridComisiones" runat="server" SkinID="Professional" AutoGenerateColumns="False" DataKeyNames="ID" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gridComisiones_SelectedIndexChanged">

            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />

            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripción" />
                <asp:BoundField DataField="AnioEspecialidad" HeaderText="Año de Especialidad">
                <ItemStyle HorizontalAlign="Center" />
                </asp:BoundField>
                <asp:BoundField DataField="idPlan" HeaderText="Plan" />
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

    <asp:Panel ID="panelFormComisiones" runat="server" Visible="false">
        <asp:Label ID="lblDescCom" runat="server" Text="Descripción"></asp:Label>
        <asp:TextBox ID="txtDescCom" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescCom" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblAnioEspecialidad" runat="server" Text="Año de especialidad"></asp:Label>
        <asp:TextBox ID="txtAnioEsp" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtAnioEsp" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtAnioEsp" Display="Dynamic" ErrorMessage="Debe ser un año entre 1 y 5" ForeColor="#FF3300" MaximumValue="5" MinimumValue="1" Type="Integer"></asp:RangeValidator>
        <br />
        <asp:Label ID="lblEspCom" runat="server" Text="Especialidad"></asp:Label>
        <asp:DropDownList ID="ddlEspecialidades" runat="server" OnSelectedIndexChanged="ddlEspecialidades_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlEspecialidades" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblPlanCom" runat="server" Text="Plan"></asp:Label>
        <asp:DropDownList ID="ddlPlanes" runat="server"></asp:DropDownList>
        
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlPlanes" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        
    </asp:Panel>

    <asp:Panel ID="gridActionPanel" runat="server">
        <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
        <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
        <asp:LinkButton ID="lnkBorrar" runat="server" OnClick="lnkBorrar_Click">Eliminar</asp:LinkButton>

    </asp:Panel>

    <asp:Panel ID="formActionPanel" runat="server" Visible="false">
        <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>

    </asp:Panel>

</asp:Content>
