<%@ Page Language="VB" AutoEventWireup="false" CodeFile="camarasStar.aspx.vb" Inherits="camarasStar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>CAMARAS STAR MEDICA</title>
</head>
<body>
     <center>
    <form id="form1" runat="server">
     <div style="width:950px; border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
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
                <asp:MenuItem NavigateUrl="~/encuesta.aspx" Text="Encuestas" Value="encuestas.aspx">
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
    <iframe src ="http://starplaza.serviciomira.com:8000" style="width:950px; height:700px">
</iframe>
    </div>
    </form>
    </center>
</body>
</html>
