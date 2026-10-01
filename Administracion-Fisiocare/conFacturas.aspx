<%@ Page Language="VB" AutoEventWireup="false" CodeFile="conFacturas.aspx.vb" Inherits="conFacturas" %>

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
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; text-align:center; font-family:Calibri; font-size:12pt">
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
                <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultastxf.aspx" Text="Consultas X Facturar" Value="consultastxf.aspx">
                    </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
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
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br /><div style=" width:900px; text-align:left"><table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center">
            <tr>
                <td style="width: 223px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    Sucursal</td>
                <td style="width: 284px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    Rango Fechas</td>
                <td style="width: 161px; background-color: #f4f4f4; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    No. de Facturas</td>
                <td rowspan="2" style="width: 161px; background-color: #f4f4f4">
            <asp:Button ID="Button1" runat="server" Text="Consultar" CssClass="boton" ForeColor="Red" Height="26px" Width="124px" /></td>
            </tr>
            <tr style="border-bottom-width:2px; border-bottom-color:#f4f4f4; border-bottom-style:solid">
                <td style="width: 223px">
                    <asp:DropDownList ID="cmbSucursal" runat="server" Font-Names="Calibri" Font-Size="12pt"
                        Width="212px">
                        <asp:ListItem Value="0">--Sucursal--</asp:ListItem>
                        <asp:ListItem Value="fisiocareSM">Star Medica</asp:ListItem>
                        <asp:ListItem Value="fisiocareCP">Campestre</asp:ListItem>
                        <asp:ListItem Value="Gym">Gym</asp:ListItem>
                        <asp:ListItem Value="fisiocareHO">HO</asp:ListItem>
                        <asp:ListItem Value="fisiocareCA">Anticanceroso</asp:ListItem>
                        <asp:ListItem Value="fisiocarePE">Pensiones</asp:ListItem>
                    </asp:DropDownList><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbSucursal"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0" Width="1px"></asp:CompareValidator></td>
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
                <td style="width: 161px; border-right-width:2px; border-right-color:White; border-right-style:solid">
                    <asp:TextBox ID="txtnumfactura" style="text-align:center; border-bottom: #e0e0e0 2px solid" runat="server" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:left">
                    <br />
                    Nota:en caso de poner número de factura no se tomará en cuenta el rango de fechas</td>
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
        <asp:DataGrid ID="gDatos" runat="server" Width="900px" AutoGenerateColumns="False" CellPadding="3">
            <AlternatingItemStyle BackColor="WhiteSmoke" />
            <Columns>
                <asp:BoundColumn DataField="num_factura" HeaderText="No.Factura"></asp:BoundColumn>
                <asp:BoundColumn DataField="serie" HeaderText="Serie"></asp:BoundColumn>
                <asp:BoundColumn DataField="fecha" HeaderText="FechaFac"></asp:BoundColumn>
                <asp:BoundColumn DataField="nombre" HeaderText="Razon Social"><ItemStyle HorizontalAlign="Left" /> </asp:BoundColumn>
                <asp:BoundColumn DataField="importe" HeaderText="Importe"></asp:BoundColumn>
                <asp:BoundColumn DataField="status" HeaderText="Status"></asp:BoundColumn>
                <asp:BoundColumn DataField="fechapago" HeaderText="FechaPago"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="Button3" runat="server" CommandName="pdf" CssClass="boton" ForeColor="Red"
                            Text="Pdf" Width="50px" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" CommandName="xml" CssClass="boton" ForeColor="Red"
                            Text="Xml" Width="50px" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="Button4" runat="server" visible='<% #bind("cancelada") %>' CommandName="sustituye" CssClass="boton" ForeColor="Red"
                            Text=". . ." Width="42px" />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
        </asp:DataGrid>
        <input id="hfSucursal" runat="server" type="hidden" /><br />
        <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Dotted"
            BorderWidth="3px" Height="175px" Width="610px">
            <br />
            <div style="padding-left: 7px; text-align: center">
                <br />
                <asp:Label ID="Label1" runat="server" BackColor="#F4F4F4" Font-Size="14pt" ForeColor="#1A3773"
                    Text="Sustituido" Width="273px"></asp:Label><br />
                <asp:Label ID="lblSustituye" runat="server" Font-Size="14pt" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                    Width="272px"></asp:Label>&nbsp;</div>
            <br />
            <asp:Button ID="Button3" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar"
                ValidationGroup="graba" /><br />
        </asp:Panel>
        <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
            PopupControlID="Panel1" TargetControlID="Hidden1">
        </cc1:ModalPopupExtender>
        <input id="Hidden1" runat="server" type="hidden" /></div>
    </form></center>
</body>
</html>
