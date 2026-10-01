<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consultasbancos.aspx.vb" Inherits="consultasbancos" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>Ingresos/Egresos - Fisiocare</title>
    <style type="text/css">
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }</style>
</head>
<body><center>
    <form id="form123" runat="server">
    <div style="width:1200px; border: solid 1px #0404B4; text-align:center; font-family:Calibri; font-size:9pt">
    <img src="imagenes/bannerzma.png" width="1200px" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
        BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
        Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773" Orientation="Horizontal"
        Width="1200px">
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
                <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultastxf.aspx" Text="Consultas  X Facturar" Value="consultastxf.aspx">
                    </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Selectable="False" Text="Zona Medica" Value="Zona Medica">
                    <asp:MenuItem NavigateUrl="~/consultasadminpaq.aspx" Text="Ventas Estimadas ZM" Value="consultasadminpaq.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultasadminpaqio.aspx" Text="Ventas Estimadas IO" Value="consultasadminpaqio.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consulpadmin.aspx" Text="admProductos Vendidos" Value="consulpadmin.aspx">
                    </asp:MenuItem>
                   <%-- <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consutas Medicas" Value="consultasmed.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
                    </asp:MenuItem>--%>
                     </asp:MenuItem>
            <asp:MenuItem Text="" Value="">
            </asp:MenuItem>
        </Items>
    </asp:Menu>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <strong><span style="font-size: 16pt; font-family: Trebuchet MS">
        REPORTE DE INGRESOS/EGRESOS (FISIOTERAPIA Y MEDICINA DEPORTIVA SCP)</span></strong>
        <br />
        <br /><div style=" width:800px; text-align:left">
        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center">
            <tr>
                <td style="width: 120px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"><strong>Empresa</strong></span></td>
                    <td style="width: 120px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"><strong>Año</strong></span></td>
                    <td style="width: 120px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"><strong>Mes</strong></span></td>
                <td rowspan="2" style="width: 161px; background-color: #f4f4f4">
            <asp:Button ID="Btncm" runat="server" Text="Consultar" CssClass="boton" ForeColor="Red" Height="26px" Width="124px" />
            
            <asp:Button ID="cmdexcelcm" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Visible="False" />
            
            </td>
            </tr>
            <tr style="border-bottom-width:2px; border-bottom-color:#f4f4f4; border-bottom-style:solid">
                <td style="width: 120px">
                    <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
                        Width="160px">
                        <asp:ListItem Value="ctFisioterapia_y_Medicina_Deportiva_S">FISIOCARE</asp:ListItem>
                        <%--<asp:ListItem Value="rentaszm">ZonaMedica Altabrisa</asp:ListItem>
                        <asp:ListItem Value="rentaszm">ZonaMedica CMA</asp:ListItem>--%>
                        
                    </asp:DropDownList></td>
                     <td style="width: 120px">
                    <asp:DropDownList ID="cmbaño" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
                        Width="160px">
                        <asp:ListItem Value="2017">2017</asp:ListItem>
                         <asp:ListItem Value="2018">2018</asp:ListItem>
                    </asp:DropDownList></td>
                    <td style="width: 120px">
                    <asp:DropDownList ID="cmbmes" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
                        Width="160px">
                        <asp:ListItem Value="saldo1">ENERO</asp:ListItem>
                         <asp:ListItem Value="saldo2">FEBRERO</asp:ListItem>
                          <asp:ListItem Value="saldo3">MARZO</asp:ListItem>
                           <asp:ListItem Value="saldo4">ABRIL</asp:ListItem>
                            <asp:ListItem Value="saldo5">MAYO</asp:ListItem>
                             <asp:ListItem Value="saldo6">JUNIO</asp:ListItem>
                              <asp:ListItem Value="saldo7">JULIO</asp:ListItem>
                               <asp:ListItem Value="saldo8">AGOSTO</asp:ListItem>
                                <asp:ListItem Value="saldo9">SEPTIEMBRE</asp:ListItem>
                                 <asp:ListItem Value="saldo10">OCTUBRE</asp:ListItem>
                                  <asp:ListItem Value="saldo11">NOVIEMBRE</asp:ListItem>
                                   <asp:ListItem Value="saldo12">DICIEMBRE</asp:ListItem>
                        
                    </asp:DropDownList></td>
              </tr>
        </table>
            <br />
            </div>
        <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
        <div style="width:1200px; overflow:auto;">
        <asp:Label ID="Label1" runat="server" Font-Bold="True" Font-Size="16px" Text="INGRESOS - FISIOTERAPIA Y MEDICINA DEPORTIVA SCP."
                         Visible="False"></asp:Label>
                         <table  border="0" cellpadding="0" cellspacing="0" style="overflow:auto;">
                            <tr style="vertical-align:top; overflow:auto;">
                                <td>
                                
                     <asp:DataGrid ID="gbaseingresos" runat="server" Width="220px"
                                        Font-Size="10pt" Font-Names="Calibri" BackColor="#CCFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="Right" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     
                     </td>
                     <td>
                                <asp:DataGrid ID="ialtabrisa" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td>
                                <asp:DataGrid ID="ithospital" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                         <td>
                                <asp:DataGrid ID="icampestre" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="igym" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="ihidroterapia" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="itdomicilio" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="iadministracion" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="icorporativo" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="iho" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="ianticanceroso" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="ipensiones" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                <asp:DataGrid ID="isocios" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td>
                                
                     <asp:DataGrid ID="itotales" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri" BackColor="#CCFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="Right" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     
                     </td>
                            
                            </tr>
                             <tr style="vertical-align:top; overflow:auto;">
                                <td>
                   <asp:DataGrid ID="gbaseegresos" runat="server" Width="220px"
                                        Font-Size="10pt" Font-Names="Calibri" BackColor="#CCFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="20px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="Right" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </td>
                     <td>
                                <asp:DataGrid ID="ealtabrisa" runat="server" Font-Names="Calibri" Font-Size="10pt" ForeColor="#003399">
                         <HeaderStyle Height="20px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     </tr>
                        </table>
        </div>
        <br />
        <hr />
        <%--<div style="width:1200px; overflow:auto;">
        <asp:Label ID="lbltotales" runat="server" Font-Bold="True" Font-Size="16px" Text="RESUMEN"
                         Visible="False"></asp:Label>
                         <table  border="0" cellpadding="0" cellspacing="0" style="width: 1200px; overflow:auto;">
                            <tr style="vertical-align:top; overflow:auto;">
                                <td style="width: 180px; overflow:auto;">
                                <asp:DataGrid ID="gbaseTOTAL" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseTOTAL2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridtotales" runat="server"
                                        Font-Size="9pt" Font-Names="Calibri" BackColor="#CCFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="9pt" />
                         </asp:DataGrid>
                         </div></td>
                            
                            </tr>
                        </table>
        </div>
        <br />
        <hr />
        <div style="width:1200px; overflow:auto;">
        <asp:Label ID="lblaltabrisa" runat="server" Font-Bold="True" Font-Size="16px" Text="ZONA MEDICA ALTABRISA"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseALTA" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseALTA2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasALTA" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" AutoGenerateColumns="true" >
                             <HeaderStyle Font-Underline="False" HorizontalAlign="center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                         <br>
                         <asp:Label ID="lblcma" runat="server" Font-Bold="True" Font-Size="16px" Text="ZONA MEDICA CENTRO"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseCMA" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseCMA2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasCMA" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                         <br>
                         <br>
                         
                     </div>
        
        <br />--%>
        
        <input id="hfSucursal" runat="server" type="hidden" />
        <input id="hfclientes" runat="server" type="hidden" /><br />
        
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
            PopupControlID="Panel1" TargetControlID="Hidden1">
        </cc1:ModalPopupExtender>
        <input id="Hidden1" runat="server" type="hidden" /></div>
    </form></center>
</body>
</html>
