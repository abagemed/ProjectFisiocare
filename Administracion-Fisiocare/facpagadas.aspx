<%@ Page Language="VB" AutoEventWireup="false" CodeFile="facpagadas.aspx.vb" Inherits="facpagadas" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fisiocare</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }</style>
</head>
<body>
    <center>
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
        <br />
        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center">
            <tr>
                <td style="border-right: white 2px solid; width: 223px; background-color: #f4f4f4">
                    Sucursal</td>
                <td style="border-right: white 2px solid; width: 284px; background-color: #f4f4f4">
                    Rango Fechas</td>
                <td style="border-right: white 2px solid; width: 161px; background-color: #f4f4f4">
                    No. de Facturas</td>
                <td rowspan="2" style="width: 161px; background-color: #f4f4f4">
                    <asp:Button ID="Button1" runat="server" CssClass="boton" ForeColor="Red" Height="26px"
                        Text="Consultar" Width="124px" /></td>
            </tr>
            <tr style="border-bottom: #f4f4f4 2px solid">
                <td style="width: 223px">
                    <asp:DropDownList ID="cmbSucursal" runat="server" Font-Names="Calibri" Font-Size="12pt"
                        Width="212px">
                        <asp:ListItem Value="0">--Sucursal--</asp:ListItem>
                        <asp:ListItem Value="fisiocareSM">Star Medica</asp:ListItem>
                        <asp:ListItem Value="fisiocareCP">Campestre</asp:ListItem>
                    </asp:DropDownList><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbSucursal"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0" Width="1px"></asp:CompareValidator></td>
                <td style="width: 284px">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha1" runat="server" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Style="border-bottom: #e0e0e0 2px solid; text-align: center" Width="90px"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha2" runat="server" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Style="border-bottom: #e0e0e0 2px solid; text-align: center" Width="90px"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server" ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
                <td style="border-right: white 2px solid; width: 161px">
                    <asp:TextBox ID="txtnumfactura" runat="server" BorderColor="White" BorderStyle="Solid"
                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid; text-align: center"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" style="text-align: left">
                    Nota:en caso de poner número de factura no se tomará en cuenta el rango de fechas</td>
            </tr>
        </table>
        <asp:DataGrid ID="gFacturas" runat="server" AutoGenerateColumns="False" Width="900px" CellPadding="3">
            <Columns>
                <asp:BoundColumn HeaderText="F.Referencia." DataField="foliofer"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="N.Factura" DataField="numfactura"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Serie" DataField="serie"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="R.Social" DataField="razonsocial"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Fecha.F" DataField="fechafactura"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Serie" DataField="serie"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Serie" DataField="serie"></asp:BoundColumn>
                <asp:BoundColumn DataField="importe" DataFormatString="{0:C}" HeaderText="Importe"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Abono" DataField="abono"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="RFC" DataField="rfc"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Fecha.Mov" DataField="fecha"></asp:BoundColumn>
                <asp:BoundColumn HeaderText="Movimiento" DataField="movimiento"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="Button2" runat="server" Visible="<%# Bind('capcuenta') %>" CssClass="boton" ForeColor="Red" Text=". . ." />
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
            <ItemStyle Height="30px" />
        </asp:DataGrid>
        <asp:Panel ID="Panel1" runat="server" Height="330px" Width="610px" BackColor="White" BorderColor="Silver" BorderStyle="Dotted" BorderWidth="3px">
            <br /><div style=" padding-left:7px; text-align:center">
            <table border="0" cellpadding="4" cellspacing="0" style=" text-align:left; width: 600px">
                <tr>
                    <td style=" background-color:#f4f4f4; color:#1a3773; width: 71px">
                        <asp:Label ID="Label4" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="No. Fac." Width="70px"></asp:Label></td>
                    <td style="width: 252px">
                        <asp:Label ID="lblfac" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="90px"></asp:Label></td>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 77px">
                        <asp:Label ID="Label9" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Facturado" Width="70px"></asp:Label></td>
                    <td style="width: 268px">
                        <asp:Label ID="lblimporte" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="90px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 71px">
                        <asp:Label ID="Label7" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Fecha" Width="70px"></asp:Label></td>
                    <td style="width: 252px">
                        <asp:Label ID="lblfecha" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="90px"></asp:Label></td>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 77px">
                        <asp:Label ID="Label10" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Abonos" Width="70px"></asp:Label></td>
                    <td style="width: 268px">
                        <asp:Label ID="lblabonos" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="90px"></asp:Label></td>
                </tr>
                <tr>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 71px">
                        <asp:Label ID="Label8" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Nombre" Width="70px"></asp:Label></td>
                    <td colspan="3">
                        <asp:Label ID="lblnombre" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="504px"></asp:Label>
                    </td>
                </tr>
            </table>
            <br />
            <table border="0" cellpadding="4" cellspacing="0" style="width: 600px; text-align: left">
                <tr>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 135px">
                        <asp:Label ID="Label1" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Abono Faltante" Width="130px"></asp:Label></td>
                    <td style="width: 415px">
                        <asp:TextBox ID="txtFaltante" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid; text-align:center"
                            Width="218px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 135px">
                        <asp:Label ID="Label2" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Forma de Pago" Width="130px"></asp:Label></td>
                    <td style="width: 415px">
            <asp:DropDownList ID="cmbTipoPago" runat="server" Width="224px">
            </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="cmbTipoPago"
                            ErrorMessage="*" Operator="NotEqual" ValidationGroup="graba" ValueToCompare="0"></asp:CompareValidator></td>
                </tr>
                <tr>
                    <td style="background-color:#f4f4f4; color:#1a3773; width: 135px">
                        <asp:Label ID="Label3" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Num. de Cuenta" Width="130px"></asp:Label></td>
                    <td style="width: 415px">
            <asp:DropDownList ID="cmbCuentas" runat="server" Width="222px">
            </asp:DropDownList>
                        <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="cmbCuentas"
                            ErrorMessage="*" Operator="NotEqual" ValidationGroup="graba" ValueToCompare="0"></asp:CompareValidator></td>
                </tr>
            </table>
            <br />
            </div>
            <br />
            <asp:Button ID="Button3" runat="server" Text="Aceptar" CssClass="boton" ForeColor="Red" ValidationGroup="graba" />
            &nbsp;&nbsp;
            <asp:Button ID="Button4" runat="server" Text="Cancelar" CssClass="boton" ForeColor="Red" /><br />
        </asp:Panel>
        &nbsp;<cc1:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy"
            popupbuttonid="Image1" targetcontrolid="txtfecha1"> </cc1:calendarextender><cc1:calendarextender
                id="CalendarExtender2" runat="server" format="dd/MM/yyyy" popupbuttonid="Image2"
                targetcontrolid="txtfecha2"></cc1:calendarextender><cc1:maskededitextender id="MaskedEditExtender1"
                    runat="server" clearmaskonlostfocus="False" mask="99/99/9999" masktype="Date"
                    promptcharacter=" " targetcontrolid="txtfecha2"></cc1:maskededitextender>
        <cc1:modalpopupextender id="ModalPopupExtender1" runat="server" backgroundcssclass="FondoAplicacion"
            popupcontrolid="Panel1" targetcontrolid="Hidden1"> </cc1:modalpopupextender>
        <input id="hfNumfactura" runat="server" type="hidden" />
                         <input id="Hidden1" runat="server" type="hidden" />
        <input id="hfSucursal" runat="server" type="hidden" />
        <cc1:maskededitextender
                        id="MaskedEditExtender2" runat="server" clearmaskonlostfocus="False" mask="99/99/9999"
                        masktype="Date" promptcharacter=" " targetcontrolid="txtfecha1"></cc1:maskededitextender>
        </div>
    </form>
</body>
</html>
