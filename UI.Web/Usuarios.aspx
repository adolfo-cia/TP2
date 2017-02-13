<%@ Page Title="Usuarios" Language="C#" MasterPageFile ="~/Site.Master" AutoEventWireup="true" CodeBehind="Usuarios.aspx.cs" StyleSheetTheme="GridViewTheme" Inherits="UI.Web.Usuarios" %>

<%@ Register Src="~/FechaControl.ascx" TagPrefix="uc1" TagName="FechaControl" %>

<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <asp:Panel ID="gridPanel" runat="server">
        <asp:GridView ID="gridView" runat="server" AutoGenerateColumns="False" SkinID="Professional"
        DataKeyNames="ID" CellPadding="4" GridLines="None" Width="541px" OnSelectedIndexChanged="gridView_SelectedIndexChanged" ForeColor="#333333">
        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
        <Columns>
            <asp:BoundField HeaderText="Nombre" DataField="Nombre" />
            <asp:BoundField HeaderText="Apellido" DataField="Apellido" />
            <asp:BoundField HeaderText="Email" DataField="Email" />
            <asp:BoundField HeaderText="Usuario" DataField="NombreUsuario" />
            <asp:BoundField HeaderText="Habilitado" DataField="Habilitado" />
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

<asp:Panel ID="formPanel" Visible="false" runat="server">
    <asp:Label ID="lblNombre" runat="server" Text="Nombre: "></asp:Label>
    <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblApellido" runat="server" Text="Apellido: "></asp:Label>
    <asp:TextBox ID="txtApellido" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre0" runat="server" ControlToValidate="txtApellido" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblEmail" runat="server" Text="Email: "></asp:Label>
    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" Display="Dynamic" ErrorMessage="Debe ingresar un mail válido" ForeColor="#FF3300" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
    <br />
    <asp:Label ID="lblDireccion" runat="server" Text="Direccion: "></asp:Label>
    <asp:TextBox ID="txtDireccion" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre2" runat="server" ControlToValidate="txtDireccion" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblLegajo" runat="server" Text="Legajo: "></asp:Label>
    <asp:TextBox ID="txtLegajo" runat="server"></asp:TextBox>
    <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtLegajo" Display="Dynamic" EnableTheming="True" ErrorMessage="Debe ser un numero entero" ForeColor="#FF3300" MaximumValue="999999" MinimumValue="0" Type="Integer"></asp:RangeValidator>
    <asp:RequiredFieldValidator ID="rfvNombre3" runat="server" ControlToValidate="txtLegajo" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <uc1:FechaControl runat="server" id="FechaControl" />
    <br />
    <asp:Label ID="lblEspecialidad" runat="server" Text="Especialidad: "></asp:Label>
    <asp:DropDownList ID="ddlEspecialidad" runat="server" OnSelectedIndexChanged="ddlEspecialidad_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlEspecialidad" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblIdPlan" runat="server" Text="Plan: "></asp:Label>
    <asp:DropDownList ID="ddlIdPlan" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="ddlIdPlan" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
     <asp:Label ID="lblTipoPersona" runat="server" Text="Tipo Persona: "></asp:Label>
    <asp:DropDownList ID="ddlTipoPersona" runat="server"></asp:DropDownList>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="ddlTipoPersona" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblTelefono" runat="server" Text="Telefono: "></asp:Label>
    <asp:TextBox ID="txtTelefono" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre4" runat="server" ControlToValidate="txtTelefono" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblHabilitado" runat="server" Text="Habilitado: "></asp:Label>
    <asp:CheckBox ID="chkHabilitado" runat="server"></asp:CheckBox>
    <br />
    <asp:Label ID="lblNombreUsuario" runat="server" Text="Usuario: "></asp:Label>
    <asp:TextBox ID="txtNombreUsuario" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvNombre5" runat="server" ControlToValidate="txtNombreUsuario" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <br />
    <asp:Label ID="lblClave" runat="server" Text="Clave: "></asp:Label>
    <asp:TextBox ID="txtClave" TextMode="Password" runat="server"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvClave" runat="server" ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <asp:CustomValidator ID="cvValidadorLongitudClave" runat="server" ControlToValidate="txtClave" Display="Dynamic" ErrorMessage="La clave debe constar de como minimo 8 caracteres" ForeColor="#FF3300" OnServerValidate="cvValidadorLongitudClave_ServerValidate"></asp:CustomValidator>
    <br />
    <asp:Label ID="lblRepetirClave" runat="server" Text="Repetir Clave: "></asp:Label>
    <asp:TextBox ID="txtRepetirClave" runat="server" TextMode="Password"></asp:TextBox>
    <asp:RequiredFieldValidator ID="rfvRepiteClave" runat="server" ControlToValidate="txtRepetirClave" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
    <asp:CompareValidator ID="cvCoinciden" runat="server" ControlToCompare="txtClave" ControlToValidate="txtRepetirClave" ErrorMessage="Las claves no coinciden" ForeColor="#FF3300"></asp:CompareValidator>
    <br />
</asp:Panel>
<asp:Panel ID="gridActionsPanel" runat="server">
    <asp:LinkButton ID="lnkNuevo" runat="server" OnClick="lnkNuevo_Click">Nuevo</asp:LinkButton>
    <asp:LinkButton ID="lnkEditar" runat="server" OnClick="lnkEditar_Click">Editar</asp:LinkButton>
    <asp:LinkButton ID="lnkEliminar" runat="server" OnClick="lnkEliminar_Click">Eliminar</asp:LinkButton>
</asp:Panel>
<asp:Panel ID="formActionsPanel" runat="server" Visible="false">
    <asp:LinkButton ID="lnkAceptar" runat="server" OnClick="lnkAceptar_Click">Aceptar</asp:LinkButton>
    <asp:LinkButton ID="lnkCancelar" runat="server" CausesValidation="False" OnClick="lnkCancelar_Click">Cancelar</asp:LinkButton>
</asp:Panel>
<asp:Panel ID="serializarPanel" runat="server">
    <asp:LinkButton ID="lnkSerializar" runat="server" OnClick="lnkSerializar_Click">Serializar Alumno</asp:LinkButton>
    <asp:LinkButton ID="lnkDeserializar" runat="server" OnClick="lnkDeserializar_Click">Deserializar Alumno</asp:LinkButton>
</asp:Panel>
</asp:Content>

