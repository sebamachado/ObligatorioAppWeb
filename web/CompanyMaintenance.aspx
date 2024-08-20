<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CompanyMaintenance.aspx.cs" Inherits="CompanyMaintenance" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Compañías</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="page-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Mantenimiento de Compañías
            </div>
<table class="table-container">
    <tr>
        <td class="label-cell">
            Nombre de Compañía:
        </td>
        <td class="input-cell">
            <asp:TextBox ID="txt_nomComp" runat="server" Width="100%"></asp:TextBox>
        </td>
        <td class="button-cell">
            <asp:Button ID="btnSearch" runat="server" Text="Buscar" Width="100%" onclick="btnSearch_Click" />
        </td>
    </tr>
    <tr>
        <td class="label-cell">
            Dirección Matriz:
        </td>
        <td class="input-cell" colspan="2">
            <asp:TextBox ID="txt_dirComp" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
    <tr>
        <td class="label-cell">
            Teléfonos:
        </td>
        <td class="input-cell" colspan="2">
            <asp:TextBox ID="txt_telComp" runat="server" Width="100%"></asp:TextBox>
        </td>
    </tr>
</table>
<table class="table-container">
    <tr>
        <td class="button-group" colspan="2">
            <asp:Button ID="btnCreate" runat="server" Text="Agregar" Width="100px" onclick="btnCreate_Click" />
            <asp:Button ID="btnUpdate" runat="server" Text="Modificar" Width="100px" onclick="btnUpdate_Click" />
            <asp:Button ID="btnDelete" runat="server" Text="Eliminar" Width="100px" onclick="btnDelete_Click" />
            <asp:Button ID="btnClear" runat="server" Text="Limpiar Formulario" Width="180px" onclick="btnClear_Click" />
        </td>
    </tr>
    <tr>
        <td class="error-cell" colspan="2">
            <asp:Label ID="lblError" runat="server"></asp:Label>
        </td>
    </tr>
    <tr>
        <td class="link-cell" colspan="2">
            <asp:LinkButton ID="lknVolver" runat="server" PostBackUrl="~/home.aspx">Volver al Menú</asp:LinkButton>
        </td>
    </tr>
</table>
        </div>
    </form>
</body>
</html>
