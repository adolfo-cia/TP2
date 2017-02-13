<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="UI.Web.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <script type="text/javascript" src="scripts/scripts.js"></script>

<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="Estilos/Estilos.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            height: 176px;
        }
        .auto-style2 {
            height: 176px;
            width: 235px;
        }
        .auto-style3 {
            width: 235px;
        }
        .auto-style4 {
            height: 176px;
            width: 514px;
        }
        .auto-style5 {
            width: 514px;
        }
        .auto-style6 {
            width: 235px;
            height: 140px;
        }
        .auto-style7 {
            width: 514px;
            height: 140px;
        }
        .auto-style8 {
            height: 140px;
        }
    </style>
    
    <link rel="icon" type="image/png" href="/Imagenes/LogoIconoUTN.png" />
</head>
<body>
    <form id="form1" runat="server">
        <table style="width:100%;">
            <tr>
                <td class="auto-style2">
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/Imagenes/utn.gif" />
                </td>
                <td class="auto-style4">
                    <h2 align="center" class="titulo" style="color: rgb(0, 0, 255); font-family: 'Comic Sans MS'; font-style: normal; font-variant: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">Universidad Tecnológica Nacional<br />
                        Facultad Regional Rosario</h2>
                    <p align="center" class="titulo" style="color: rgb(0, 0, 255); font-family: 'Comic Sans MS'; font-size: medium; font-style: normal; font-variant: normal; font-weight: normal; letter-spacing: normal; line-height: normal; orphans: auto; text-indent: 0px; text-transform: none; white-space: normal; widows: 1; word-spacing: 0px; -webkit-text-stroke-width: 0px;">
                        Sistema Academia<br />
                        Módulo de autogestión
                    </p>
                </td>
                <td class="auto-style1"></td>
            </tr>
            <tr>
                <td class="auto-style6"></td>
                <td class="auto-style7">
        <asp:Login ID="loginAcademia" style="vertical-align: middle; text-align: center; width: 400px; margin-left: auto; margin-right:auto;" runat="server" BackColor="#F7F7DE" LoginButtonText="Iniciar sesión" OnAuthenticate="Login1_Authenticate" PasswordRecoveryText="Olvidó su contraseña?" Width="400px" BorderColor="#CCCC99" BorderStyle="Solid" BorderWidth="1px" Font-Names="Verdana" Font-Size="10pt" PasswordRecoveryUrl="~/OlvidoContrasenia.aspx">
            <TitleTextStyle BackColor="#6B696B" Font-Bold="True" ForeColor="#FFFFFF" />
        </asp:Login>
                </td>
                <td class="auto-style8"></td>
            </tr>
            <tr>
                <td class="auto-style3">&nbsp;</td>
                <td class="auto-style5">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </form>
</body>
</html>
