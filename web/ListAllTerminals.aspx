<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListAllTerminals.aspx.cs" Inherits="ListAllTerminals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listar Terminales</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="home-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Elija el tipo de Terminal 
            </div>
            <div class="home-content">
                <asp:LinkButton ID="lknCompanys" runat="server" CssClass="link-button" PostBackUrl="~/ListNterminals.aspx">Terminales Nacionales</asp:LinkButton>
                <asp:LinkButton ID="lknCategorias" runat="server" CssClass="link-button" PostBackUrl="~/ListIterminals.aspx">Terminales Internacionales</asp:LinkButton>
            </div>
            <div class="link-container">
                <asp:LinkButton ID="lknVolver" runat="server" PostBackUrl="~/home.aspx">Volver al Menu</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
 