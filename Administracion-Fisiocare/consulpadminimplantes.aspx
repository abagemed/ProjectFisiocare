<%@ Page Language="VB" AutoEventWireup="false" CodeFile="consulpadminimplantes.aspx.vb" Inherits="consulpadminimplantes" %>


<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>Fisiocare</title>
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
    <div style="width:1100px; border: solid 1px #ff0000; text-align:center; font-family:Calibri; font-size:10pt">
    <img src="imagenes/bannerio.png" width="1100" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
        BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
        Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773" Orientation="Horizontal"
        Width="1100px">
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
                <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Selectable="False" Text="Med Solution" Value="Med Solution">
                    <asp:MenuItem NavigateUrl="~/consultasadminpaq.aspx" Text="Ventas Estimadas ZonaMedica" Value="consultasadminpaq.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultasadminpaqio.aspx" Text="Ventas Estimadas Implantes" Value="consultasadminpaqio.aspx">
                    </asp:MenuItem>
                    <%--<asp:MenuItem NavigateUrl="~/consulpadmin.aspx" Text="Productos Vendidos 2018" Value="consulpadmin.aspx">
                    </asp:MenuItem>--%>
                    <asp:MenuItem NavigateUrl="~/consulpadminimplantes.aspx" Text="Productos Vendidos-General" Value="consulpadminimplantes.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consulpadminnov.aspx" Text="Productos NO vendidos" Value="consulpadminnov.aspx">
                    </asp:MenuItem>
                    <%--<asp:MenuItem NavigateUrl="~/consulpadminbsn.aspx" Text="Productos Vendidos BSN" Value="consulpadminbsn.aspx">
                    </asp:MenuItem>--%>
                   <%-- <asp:MenuItem NavigateUrl="~/consulpadmin2017.aspx" Text="Productos Vendidos 2017" Value="consulpadmin2017.aspx">
                    </asp:MenuItem>--%>
                     <asp:MenuItem NavigateUrl="~/consultaexiscostoadmin.aspx" Text="Ventas-Existencias-Costo" Value="consultaexiscostoadmin.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consulpadminreferencia.aspx" Text="Facturas-Referencias" Value="consulpadminreferencia.aspx">
                    </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultaCC.aspx" Text="Ventas Cliente" Value="consultaCC.aspx">
                    </asp:MenuItem>
                </asp:MenuItem>
            <asp:MenuItem Text="" Value="">
            </asp:MenuItem>
        </Items>
    </asp:Menu>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <strong><span style="font-size: 16pt; font-family: Trebuchet MS">
        REPORTE DE PRODUCTOS
            VEDIDOS</span></strong>
        <br />
        <br /><div style=" width:1100px; text-align:left"><table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center">
            <tr>
                <td style="width: 150px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"><strong>Producto</strong></span></td>
                <td style="width: 284px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"><strong>
                    Rango Fechas</strong></span></td>
                <%--<td style="width: 161px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <span style="font-size: 12pt"></span></td>--%>
                <td rowspan="2" style="width: 161px; background-color: #f4f4f4">
            <asp:Button ID="Btncm" runat="server" Text="Consultar" CssClass="boton" ForeColor="Red" Height="26px" Width="124px" />
            
            <asp:Button ID="cmdexcelcm" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Visible="False" />
            
            </td>
            </tr>
            <tr style="border-bottom-width:2px; border-bottom-color:#f4f4f4; border-bottom-style:solid">
                <td style="width: 150px">
                    <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
                        Width="160px">
                        <asp:ListItem Value="adMed_Solution_de_sure2018">--Todos los productos--</asp:ListItem>
                        <%--<asp:ListItem Value="rentaszm">ZonaMedica Altabrisa</asp:ListItem>
                        <asp:ListItem Value="rentaszm">ZonaMedica CMA</asp:ListItem>--%>
                        
                    </asp:DropDownList></td>
                <td style="width: 284px">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha1" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha2" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
               <%-- <td></td>--%>
            </tr>
            
        </table>
            <br />
            </div>
        
         <cc1:calendarextender
                                    id="CalendarExtender1" runat="server" format="dd/MM/yyyy" popupbuttonid="Image1"
                                    targetcontrolid="txtfecha1">
        </cc1:calendarextender><cc1:calendarextender id="CalendarExtender2" runat="server"
            format="dd/MM/yyyy" popupbuttonid="Image2" targetcontrolid="txtfecha2">
        </cc1:calendarextender><cc1:maskededitextender id="MaskedEditExtender1" runat="server"
            clearmaskonlostfocus="False" mask="99/99/9999" masktype="Date" promptcharacter=" "
            targetcontrolid="txtfecha2">
        </cc1:maskededitextender><cc1:maskededitextender id="MaskedEditExtender2" runat="server"
            clearmaskonlostfocus="False" mask="99/99/9999" masktype="Date" promptcharacter=" "
            targetcontrolid="txtfecha1">
        </cc1:maskededitextender>
        <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
        <hr />
        <div style="width:1100px; overflow:auto; text-align: center;">
        <asp:Label ID="lbltotales" runat="server" Font-Bold="True" Font-Size="16px" Text="TOTAL GENERAL"
                         Visible="False"></asp:Label>
                         <asp:DataGrid ID="gridvadmProductostotales" runat="server"
                                        Font-Size="12pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="AliceBlue" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="black" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>
                         </div>
                         <hr />
                         <br />
                          <div style="overflow:auto; text-align: center;">
        <asp:Label ID="lblIMPLANTES" runat="server" Font-Bold="True" Font-Size="16px" Text="GENERAL"
                         Visible="False"></asp:Label>
                         <asp:DataGrid ID="gridvadmProductos" runat="server" HorizontalAlign="Center"
                                        Font-Size="12pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="AliceBlue" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="black" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>
                         <br>
                         <%--<asp:Label ID="lblcma" runat="server" Font-Bold="True" Font-Size="16px" Text="ZONA MEDICA CENTRO"
                         Visible="False"></asp:Label>--%>
                         <%--<asp:DataGrid ID="gridvadmProductoscma" runat="server"
                                        Font-Size="12pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="AliceBlue" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="black"/>
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>--%>
                         <br>
                         <%--<asp:Label ID="lblpensiones" runat="server" Font-Bold="True" Font-Size="16px" Text="ZONA MEDICA PENSIONES"
                         Visible="False"></asp:Label>--%>
                         <%--<asp:DataGrid ID="gridvadmProductospensiones" runat="server"
                                        Font-Size="12pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="AliceBlue" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="black" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>--%>
                         <br>
                     </div>
        
        <input id="hfSucursal" runat="server" type="hidden" />
        <input id="hfclientes" runat="server" type="hidden" />
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
            PopupControlID="Panel1" TargetControlID="Hidden1">
        </cc1:ModalPopupExtender>
        <input id="Hidden1" runat="server" type="hidden" /></div>
    </form></center>
</body>
</html>
