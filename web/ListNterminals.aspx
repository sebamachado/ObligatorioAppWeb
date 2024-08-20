<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListNterminals.aspx.cs" Inherits="ListNterminals" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listar Terminales Nacionales</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="page-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Listado de Terminales Nacionales 
            </div>
            <div class="grid-container">
                <asp:GridView ID="gvListNterminals" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />  
                    <Columns> 
                        <asp:BoundField DataField="Id" HeaderText="ID"/>
                        <asp:BoundField DataField="CityName" HeaderText="Ciudad"/>
                        <asp:TemplateField HeaderText="Servicio Taxi">
                            <ItemTemplate>
                                <asp:Label ID="lblTaxiService" runat="server" Text='<%# Eval("TaxiService", "{0}").ToString() == "True" ? "SI" : "NO" %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
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
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </div>
            <div class="link-container">
                <asp:LinkButton ID="lknVolver" runat="server" PostBackUrl="~/ListAllTerminals.aspx">Eleccion Tipo de Terminal</asp:LinkButton>
                <asp:LinkButton ID="LinkButton1" runat="server" PostBackUrl="~/home.aspx"><br>Volver al Menu</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
