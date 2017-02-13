<%@ Page Title="Home" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="UI_Web.Default" %>
<asp:Content ID="Content2" ContentPlaceHolderID="bodyContentPlaceHolder" runat="server">
    <table style="width:100%;">
        <tr>
            <td style="width: 394px; height: 7px;"></td>
            <td style="height: 7px">
    <asp:Panel ID="menuPanel" runat="server" CssClass="panelCss" BorderStyle="None" Width="204px">
        <asp:HyperLink ID="hlkCargarNotas" runat="server" NavigateUrl="~/subirNotas.aspx">Cargar Notas</asp:HyperLink>
        <asp:HyperLink ID="hlkInscripcionCurso" runat="server" NavigateUrl="~/AlumnoInscripciones.aspx">Inscribirse a Cursos</asp:HyperLink>
        <asp:HyperLink ID="hlkUsuarios" runat="server" NavigateUrl="~/Usuarios.aspx">Administrar Usuarios</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlkEspecialidades" runat="server" NavigateUrl="~/Especialidades.aspx">Administrar Especialidades</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlkPlanes" runat="server" NavigateUrl="~/Planes.aspx">Administrar Planes</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlkMaterias" runat="server" NavigateUrl="~/Materias.aspx">Administrar Materias</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlkComisiones" runat="server" NavigateUrl="~/Comisiones.aspx">Administrar Comisiones</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="hlkCursos" runat="server" NavigateUrl="~/Cursos.aspx">Administrar Cursos</asp:HyperLink>
        <br />
        <br />
        <br />
        <br />
        <br />
    </asp:Panel>
            </td>
            <td style="height: 7px"></td>
        </tr>
        </table>
</asp:Content>
