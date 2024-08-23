<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListTripsByTerminalMY.aspx.cs" Inherits="ListTripsByTerminalMY" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listar Viajes Terminal/Mes/Año</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="page-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Listar Viajes Terminal/Mes/Año
            </div>
            <table class="table-container">
                <tr>
                    <td class="label-cell">
                        Código Terminal:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_terminalCode" runat="server" Width="80px" MaxLength="6"></asp:TextBox>
                    </td>
                    <td class="label-cell">
                        Mes:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_month" runat="server" Width="30px" MaxLength="2"></asp:TextBox>
                    </td>
                    <td class="label-cell">
                        Año:
                    </td>
                    <td class="input-cell">
                        <asp:TextBox ID="txt_year" runat="server" Width="30px" MaxLength="4"></asp:TextBox>
                    </td>
                    <td class="button-cell">
                        <asp:Button ID="btnSearch" runat="server" Text="Buscar" Width="100px" OnClick="btnSearch_Click" />
                    </td>
                </tr>
            </table>
            <div class="grid-container">
                <asp:GridView ID="gvListTripTerminalMY" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />  
                    <Columns> 
                        <asp:BoundField DataField="DepartureDate" HeaderText="Fecha/Hora Salida"/>
                        <asp:BoundField DataField="EstimatedArrivalDate" HeaderText="Fecha/Hora Llegada"/>
                        <asp:BoundField DataField="MaxPassengers" HeaderText="Max Pasajeros"/>
                        <asp:BoundField DataField="TicketPrice" HeaderText="Precio Boleto"/>
                        <asp:BoundField DataField="PlatformNumber" HeaderText="Anden"/>
                        <asp:BoundField DataField="ArrivalTerminal.Id" HeaderText="Terminal"/>
                        <asp:BoundField DataField="CompanyTrip.Name" HeaderText="Compañias"/>
                    </Columns>
                    <FooterStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#990000" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#FFCC66" ForeColor="#333333" HorizontalAlign="Center" />
                    <RowStyle BackColor="#FFFBD6" ForeColor="#333333" />
                    <SelectedRowStyle BackColor="#FFCC66" Font-Bold="True" ForeColor="Navy" />
                    <SortedAscendingCellStyle BackColor="#FDF5AC" />
                    <SortedAscendingHeaderStyle BackColor="#4D0000" />
                    <SortedDescendingCellStyle BackColor="#FCF6C0" />
                    <SortedDescendingHeaderStyle BackColor="#820000" />
                </asp:GridView>
            </div>
            <div class="label-container">
                <asp:Button ID="btnClear" runat="server" Text="Limpiar Formulario" Width="180px" onclick="btnClear_Click" /><br/>
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>
            <div class="link-container">
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/home.aspx">Volver al Menu</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
