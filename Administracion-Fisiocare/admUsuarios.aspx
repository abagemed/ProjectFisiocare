<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admUsuarios.aspx.vb" Inherits="admUsuarios" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administración Fisiocare</title>
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; height:860px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner1.png" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
            BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
            Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773" Orientation="Horizontal"
            Width="900px">
            <StaticMenuItemStyle VerticalPadding="3px" Width="100px" />
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <DynamicMenuItemStyle BackColor="#F4F4F4" BorderColor="White" BorderStyle="Solid"
                BorderWidth="1px" HorizontalPadding="3px" VerticalPadding="4px" Width="250px" />
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <Items>
                <asp:MenuItem NavigateUrl="~/Defadmon.aspx" Text="Reporte de Ocupaci&#243;n" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Selectable="False" Text="Agenda" Value="Agenda">
                    <asp:MenuItem NavigateUrl="~/CentroCostos.aspx" Text="Centro de Costos" Value="1"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/admUsuarios.aspx" Text="Administraci&#243;n Clientes"
                        Value="2"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/admRecibos.aspx" Text="Administrar Recibos" Value="5"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/terapistas.aspx" Text="Terapistas" Value="4"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/DefaultSM.aspx" Text="Agenda Star Medica" Value="Agenda Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/DefaultCP.aspx" Text="Agenda Campestre" Value="Agenda Campestre">
                    </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Selectable="False" Text="Contabilidad" Value="Contabilidad">
                    <asp:MenuItem NavigateUrl="~/Movimientos.aspx" Text="Movimientos" Value="bancos.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/conFacturas.aspx" Text="Consultas Facturas" Value="confacturas.aspx">
                    </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Camaras" Value="Camaras">
                    <asp:MenuItem NavigateUrl="~/camarasStar.aspx" Text="Camaras Star Medica" Value="Camaras Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/camarasplaza.aspx" Text="Camaras Star plaza" Value="Camaras Star plaza">
                    </asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        <br />
        <table border="1" cellpadding="4" cellspacing="0" style="width: 895px; text-align: left; border-width:1px; border-color:#efefef; border-style:solid">
            <tr>
                <td style="color: #1b3774; background-color: #f4f4f4; width:400px">
                    SUCURSAL</td>
                <td rowspan="4" style="text-align:center" >
        <asp:Panel ID="Panel1" runat="server" Visible="False" Width="400px">
            <table border="0" cellpadding="4" cellspacing="0" style="width: 100%">
                <tr>
                    <td style=" text-align:left">
            PERMITIR MODIFICAR FACTURA</td>
                    <td>
                        <asp:DropDownList ID="cmbModfac" runat="server" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="True">SI</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
                <tr>
                    <td style=" text-align:left">
                        AUTORIZAR CORTESIAS</td>
                    <td>
                        <asp:DropDownList ID="cmbCortesias" runat="server" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="True">SI</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
            <br />
            <asp:Button ID="Button1" runat="server" Text="Actualizar" Width="163px" />
            <br />
            <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
        </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="width:400px">
        <asp:DropDownList ID="cmbsucursal" runat="server" AutoPostBack="True" Width="257px" Font-Names="Calibri" Font-Size="11pt">
            <asp:ListItem Value="0">---Seleccione---</asp:ListItem>
            <asp:ListItem Value="fisiocarecp">CAMPESTRE</asp:ListItem>
            <asp:ListItem Value="fisiocaresm">STAR MEDICA</asp:ListItem>
        </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="color: #1b3774; background-color: #f4f4f4; width:400px">
                    CLIENTE</td>
            </tr>
            <tr>
                <td style="width:400px">
        <asp:DropDownList ID="cmbclientes" runat="server" AutoPostBack="True" Width="400px" Font-Names="Calibri" Font-Size="11pt">
        </asp:DropDownList></td>
            </tr>
        </table>
        &nbsp;&nbsp;<br />
    
    </div>
    </form>
    </center>
</body>
</html>
