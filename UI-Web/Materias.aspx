<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Materias.aspx.cs" Inherits="UI_Web.Materias" %>
<asp:Content ID="Content1" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridMateriasPanel" runat="server">
        <asp:GridView ID="gridMaterias" runat="server" DataKeyNames="ID" SkinID="Professional" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="gridMaterias_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
            <Columns>
                <asp:BoundField DataField="Descripcion" HeaderText="Descripcion" />
                <asp:BoundField DataField="HorasSemanales" HeaderText="Horas Semanales" />
                <asp:BoundField DataField="HorasTotales" HeaderText="Horas Totales" />
                <asp:BoundField HeaderText="Plan" DataField="DPlan" />
                <asp:BoundField HeaderText="Especialidad" DataField="DEspecialidad" />
                <asp:CommandField SelectText="Seleccionar" ShowSelectButton="True" />
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
    <asp:Panel ID="materiasPanel" runat="server" Visible="false">
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripcion: "></asp:Label>
        <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescripcion" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblHS" runat="server" Text="Horas semanales: "></asp:Label>
        <asp:TextBox ID="txtHorasSemanales" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtHorasSemanales" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtHorasSemanales" Display="Dynamic" ErrorMessage="Debe ser un entero mayor a 0" ForeColor="#FF3300" MaximumValue="999999" MinimumValue="1"></asp:RangeValidator>
        <br />
        <asp:Label ID="lblHT" runat="server" Text="Horas totales: "></asp:Label>
        <asp:TextBox ID="txtHorasTotales" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtHorasTotales" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="RangeValidator2" runat="server" ControlToValidate="txtHorasTotales" Display="Dynamic" ErrorMessage="Debe ser un entero mayor a 0" ForeColor="#FF3300" MaximumValue="9999999" MinimumValue="1"></asp:RangeValidator>
        <br />
        <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad: "></asp:Label>
        <asp:DropDownList ID="ddlEspecialidad" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEspecialidad" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblPlan" runat="server" Text="Plan: "></asp:Label>
        <asp:DropDownList ID="ddlPlan" runat="server" AutoPostBack="true">
        </asp:DropDownList>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlPlan" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    </asp:Panel>
    <asp:Panel ID="gridMateriasActionPanel" runat="server">
        <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
        <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
        <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click">Eliminar</asp:LinkButton>
    </asp:Panel>
    <asp:Panel ID="formActionPanel" runat="server" Visible="false">
        <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click">Aceptar</asp:LinkButton>
        <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click" CausesValidation="False">Cancelar</asp:LinkButton>
    </asp:Panel>
</asp:Content>
