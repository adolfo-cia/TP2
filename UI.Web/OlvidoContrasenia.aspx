<%@ Page Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="OlvidoContrasenia.aspx.cs" Inherits="UI.Web.OlvidoContrasenia" %>
<asp:Content ID="Content4" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">

    <asp:Panel ID="Panel1" runat="server">
    <asp:Label ID="lblUsuario" runat="server" Text="Nombre de Usuario:  "></asp:Label>
    <asp:TextBox ID="txtUsuario" runat="server"></asp:TextBox>
</asp:Panel>
<asp:Panel ID="Panel2" runat="server">
    <asp:LinkButton ID="lnkEnviar" runat="server" OnClick="lnkEnviar_Click">Enviar Mail        </asp:LinkButton>
    <asp:LinkButton ID="lnkCancelar" runat="server" OnClick="lnkCancelar_Click">Cancelar</asp:LinkButton>
</asp:Panel>

</asp:Content>