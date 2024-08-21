<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Bienvenidos a Terminal de Montevideo</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <asp:Label ID="lblLastDeploy" runat="server" CssClass="info-text">
            Ultimo despliegue:
        </asp:Label>
        <div class="container">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="home-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="home-content">
                <asp:LinkButton ID="lknCompanys" runat="server" CssClass="link-button" PostBackUrl="~/CompanyMaintenance.aspx">Mantenimiento Compañias</asp:LinkButton>
                <asp:LinkButton ID="lknCategorias" runat="server" CssClass="link-button" PostBackUrl="~/NterminalMaintenance.aspx">Mantenimiento Terminal Nacional</asp:LinkButton>
                <asp:LinkButton ID="lknAgregarPrivado" runat="server" CssClass="link-button" PostBackUrl="~/IterminalMaintenance.aspx">Mantenimiento Terminal Internacional</asp:LinkButton>
                <asp:LinkButton ID="lknListCompanies" runat="server" CssClass="link-button" PostBackUrl="~/ListAllCompanies.aspx">Listado Todas Las Compañias</asp:LinkButton>
                <asp:LinkButton ID="lknListadoComun" runat="server" CssClass="link-button" PostBackUrl="~/AddTrip.aspx">Agregar Viaje</asp:LinkButton>
                <asp:LinkButton ID="lknListadoPrivados" runat="server" CssClass="link-button" PostBackUrl="~/ListAllTerminals.aspx">Listado Terminales</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" CssClass="link-button" PostBackUrl="~/home.aspx">Listado Viajes Terminal Mes/Año (No disponible)</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
