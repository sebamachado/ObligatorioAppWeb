<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AddTrip.aspx.cs" Inherits="AddTrip" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Agregar Viaje</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="page-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Agregar Viaje
            </div>
            <table class="table-container">
                <tr>
                    <td class="label-cell">
                        Salida:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_DepartureDate" runat="server" Width="100%" MaxLength="19"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Llegada:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_EstimatedArrivalDate" runat="server" Width="100%" MaxLength="19"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Pasajeros:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_MaxPassengers" runat="server" Width="100%" MaxLength="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Precio del Boleto:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_TicketPrice" runat="server" Width="100%" MaxLength="9"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Número de Andén:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_PlatformNumber" runat="server" Width="100%" MaxLength="2"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Compañía:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_CompanyTrip" runat="server" Width="100%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td class="label-cell">
                        Terminal Destino:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_ArrivalTerminal" runat="server" Width="100%" MaxLength="6"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <table class="table-container">
                <tr>
                    <td class="button-group" colspan="2">
                        <asp:Button ID="btnAddTrip" runat="server" Text="Agregar Viaje" Width="150px" onclick="btnAddTrip_Click" />
                        <asp:Button ID="btnClear" runat="server" Text="Limpiar Formulario" Width="150px" onclick="btnClear_Click" />
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
