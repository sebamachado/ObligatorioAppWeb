<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ListAllCompanies.aspx.cs" Inherits="ListAllCompanies" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>Listar Compañias</title>
    <link href="~/css/styles.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="page-content">
            <div class="image-container">
                <asp:Image ID="Image1" runat="server" CssClass="page-logo" ImageUrl="~/images/logo.jpg" />
            </div>
            <div class="header">
                Listado de Compañias 
            </div>
            <div class="grid-container">
                <asp:GridView ID="gvListCompanies" runat="server" AutoGenerateColumns="False" CellPadding="4" ForeColor="#333333" GridLines="None">
                    <AlternatingRowStyle BackColor="White" />
                    <Columns>
                        <asp:BoundField DataField="Name" HeaderText="Nombre"/>
                        <asp:BoundField DataField="MatrizAddress" HeaderText="Direccion"/>
                        <asp:TemplateField HeaderText="Telefonos" HeaderStyle-CssClass="column-phones">
                            <ItemTemplate>
                                <asp:Literal ID="litPhones" runat="server" Text='<%# Eval("ContactPhone") %>' />
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
                <asp:LinkButton ID="lknVolver" runat="server" PostBackUrl="~/home.aspx">Volver al Menu</asp:LinkButton>
            </div>
        </div>
    </form>
</body>
</html>
