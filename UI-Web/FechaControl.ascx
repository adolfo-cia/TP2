<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="FechaControl.ascx.cs" Inherits="UI_Web.FechaControl" %>
<asp:Label ID="lblDia" runat="server" Text="Dia: "></asp:Label>
<asp:DropDownList ID="ddlDia" runat="server" ClientIDMode="Static"></asp:DropDownList>
<asp:Label ID="lblMes" runat="server" Text="Mes: "></asp:Label>
<asp:DropDownList ID="ddlMes" runat="server" ClientIDMode="Static"></asp:DropDownList>
<asp:Label ID="lblAnio" runat="server" Text="Año: "></asp:Label>
<asp:DropDownList ID="ddlAnio" runat="server" ClientIDMode="Static"></asp:DropDownList>
<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDia" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="ddlMes" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>
<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlAnio" Display="Dynamic" ErrorMessage="Este campo es requerido" ForeColor="#FF3300"></asp:RequiredFieldValidator>