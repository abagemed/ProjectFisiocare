<%@ Page Language="VB" AutoEventWireup="false" CodeFile="concxc.aspx.vb" Inherits="concxc" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>Fisiocare</title>
    <style type="text/css">
        .FondoAplicacion {
            background-color: Gray;
            filter: alpha(opacity=70);
            opacity: 0.7;
        }
    </style>
</head>
<body>
    <center>
        <form id="form1" runat="server">
            <div style="width: 1100px; border: solid 1px #ff0000; text-align: center; font-family: Calibri; font-size: 12pt">
                <img src="imagenes/baner1.png" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
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
                        <asp:MenuItem NavigateUrl="~/Defadmon.aspx" Text="Reporte de Ocupaci&#243;n" Value="0"></asp:MenuItem>
                        <asp:MenuItem NavigateUrl="~/encuesta.aspx" Text="Encuestas" Value="encuestas.aspx"></asp:MenuItem>
                        <asp:MenuItem Selectable="False" Text="Agenda" Value="Agenda">
                            <asp:MenuItem NavigateUrl="~/CentroCostos.aspx" Text="Centro de Costos" Value="1"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/admUsuarios.aspx" Text="Administraci&#243;n Clientes"
                                Value="2"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/admRecibos.aspx" Text="Administrar Recibos" Value="5"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/terapistas.aspx" Text="Terapistas" Value="4"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/DefaultSM.aspx" Text="Agenda Star Medica" Value="Agenda Star Medica"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/DefaultCP.aspx" Text="Agenda Campestre" Value="Agenda Campestre"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Selectable="False" Text="Contabilidad" Value="Contabilidad">
                            <asp:MenuItem NavigateUrl="~/Movimientos.aspx" Text="Movimientos" Value="bancos.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/conFacturas.aspx" Text="Consultas Facturas" Value="confacturas.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/consultastxf.aspx" Text="Consultas X Facturar" Value="consultastxf.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx"></asp:MenuItem>
                            <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx"></asp:MenuItem>
                        </asp:MenuItem>
                        <asp:MenuItem Text="" Value=""></asp:MenuItem>
                    </Items>
                </asp:Menu>
                <asp:ScriptManager ID="ScriptManager1" runat="server">
                </asp:ScriptManager>
                <br />
                <strong><span style="font-size: 16pt; font-family: Trebuchet MS">REPORTE DE CUENTAS X COBRAR (FACTURAS-CLIENTES)</span></strong>
                <br />
                <br />
                <div style="width: 1100px; text-align: left">
                    <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center">
                        <tr>
                            <td style="width: 150px; background-color: #f4f4f4; border-right-width: 2px; border-right-color: White; border-right-style: solid">Sucursal</td>
                            <td style="width: 284px; background-color: #f4f4f4; border-right-width: 2px; border-right-color: White; border-right-style: solid">Rango Fechas</td>
                            <td style="width: 161px; background-color: #f4f4f4; border-right-width: 2px; border-right-color: White; border-right-style: solid">Cliente</td>
                            <td rowspan="2" style="width: 161px; background-color: #f4f4f4">
                                <asp:Button ID="Button1" runat="server" Text="Consultar" CssClass="boton" ForeColor="Red" Height="26px" Width="124px" />
                                <%--<asp:Button ID="Btntotales" runat="server" Text="ConsTotales" CssClass="boton" ForeColor="Red" Height="26px" Width="124px" />--%>
                                <asp:Button ID="cmdexcel" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Visible="False" />
                                <%--<asp:Button ID="cmdexcelt" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Visible="False" />--%>
            </td>
                        </tr>
                        <tr style="border-bottom-width: 2px; border-bottom-color: #f4f4f4; border-bottom-style: solid">
                            <td style="width: 150px">
                                <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
                                    Width="150px">
                                    <asp:ListItem Value="0">--Sucursal--</asp:ListItem>
                                    <asp:ListItem Value="fisiocareSM">Star Medica</asp:ListItem>
                                    <asp:ListItem Value="fisiocareCP">Campestre</asp:ListItem>
                                    <asp:ListItem Value="fisiocareHO">HO</asp:ListItem>
                                    <asp:ListItem Value="fisiocareAM">Amerimed</asp:ListItem>
                                    <asp:ListItem Value="fisiocareCA">Anticanceroso</asp:ListItem>
                                    <asp:ListItem Value="Gym">Gym</asp:ListItem>
                                </asp:DropDownList><asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbSucursal"
                                    ErrorMessage="*" Operator="NotEqual" ValueToCompare="0" Width="1px"></asp:CompareValidator></td>
                            <td style="width: 284px">
                                <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
                                    ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                                        ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                                        MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                            ID="txtfecha1" runat="server" Width="90px" Style="text-align: center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Image ID="Image1" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                                <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
                                    ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                                        ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                                        MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                            ID="txtfecha2" runat="server" Width="90px" Style="text-align: center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                                <asp:Image ID="Image2" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
                            <td>
                                <asp:DropDownList ID="cmbclientes" runat="server" AutoPostBack="True" Width="250px" Font-Names="Calibri" Font-Size="11pt">
                                </asp:DropDownList>
                            </td>
                        </tr>

                    </table>
                    <br />
                </div>

                <cc1:CalendarExtender
                    ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1"
                    TargetControlID="txtfecha1">
                </cc1:CalendarExtender>
                <cc1:CalendarExtender ID="CalendarExtender2" runat="server"
                    Format="dd/MM/yyyy" PopupButtonID="Image2" TargetControlID="txtfecha2">
                </cc1:CalendarExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server"
                    ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" PromptCharacter=" "
                    TargetControlID="txtfecha2">
                </cc1:MaskedEditExtender>
                <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server"
                    ClearMaskOnLostFocus="False" Mask="99/99/9999" MaskType="Date" PromptCharacter=" "
                    TargetControlID="txtfecha1">
                </cc1:MaskedEditExtender>
                <cc2:messagebox ID="Messagebox1" runat="server"></cc2:messagebox>
                <div style="width: 1100px; overflow: auto;">
                    <asp:DataGrid ID="gDatos" runat="server" Width="1400px" Font-Size="Small" AutoGenerateColumns="False" CellPadding="3">
                        <AlternatingItemStyle BackColor="WhiteSmoke" />
                        <Columns>
                            <asp:BoundColumn DataField="num_factura" HeaderText="NO.FAC."></asp:BoundColumn>
                            <asp:BoundColumn DataField="serie" HeaderText="SERIE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="idcliente" HeaderText="ID.CLIE"></asp:BoundColumn>
                            <asp:BoundColumn DataField="paciente" HeaderText="PACIENTE">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="razonSocial" HeaderText="RAZÓN SOCIAL">
                                <ItemStyle HorizontalAlign="Left" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="rfc" HeaderText="RFC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="uuid" HeaderText="UUID"></asp:BoundColumn>

                            <asp:BoundColumn DataField="fechaFactura" HeaderText="FECHA FAC."></asp:BoundColumn>
                            <asp:BoundColumn DataField="fechaAbono" HeaderText="FECHA ABONO."></asp:BoundColumn>
                            <asp:BoundColumn DataField="importeAbonos" HeaderText="IMPORTE ABONOS"></asp:BoundColumn>
                            <asp:BoundColumn DataField="ImporteFactura" HeaderText="IMPORTE FACTURA"></asp:BoundColumn>
                            <asp:BoundColumn DataField="importeConciliado" HeaderText="IMPORTE CONCILIADO"></asp:BoundColumn>
                            <asp:BoundColumn DataField="saldoConciliar" HeaderText="SALDO CONCILIAR">
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundColumn>
                            <asp:BoundColumn DataField="FechaConciliacion" HeaderText="FECHA CONCIL."></asp:BoundColumn>
                            <asp:BoundColumn DataField="statusfac" HeaderText="STATUS">
                                <ItemStyle Font-Bold="true" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                    </asp:DataGrid>
                </div>
                <br />
                <asp:DataGrid ID="gtotales" runat="server" Width="1100px" AutoGenerateColumns="False" CellPadding="3">
                    <AlternatingItemStyle BackColor="WhiteSmoke" />
                    <Columns>
                        <asp:BoundColumn DataField="nombre" HeaderText="RAZON SOCIAL/CLIENTE">
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="totalimporte" HeaderText="T.IMPORTE"></asp:BoundColumn>
                        <asp:BoundColumn DataField="totalabono" HeaderText="T.ABONADO"></asp:BoundColumn>
                        <asp:BoundColumn DataField="saldo" HeaderText="ADEUDA"></asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                </asp:DataGrid>

                <input id="hfSucursal" runat="server" type="hidden" />
                <input id="hfclientes" runat="server" type="hidden" /><br />
                <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Dotted"
                    BorderWidth="3px" Height="175px" Width="610px">
                    <br />
                    <div style="padding-left: 7px; text-align: center">
                        <br />
                        <asp:Label ID="Label1" runat="server" BackColor="#F4F4F4" Font-Size="14pt" ForeColor="#1A3773"
                            Text="Sustituido" Width="273px"></asp:Label><br />
                        <asp:Label ID="lblSustituye" runat="server" Font-Size="14pt" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Width="272px"></asp:Label>&nbsp;
                    </div>
                    <br />
                    <asp:Button ID="Button3" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar"
                        ValidationGroup="graba" /><br />
                </asp:Panel>
                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
                    PopupControlID="Panel1" TargetControlID="Hidden1">
                </cc1:ModalPopupExtender>
                <input id="Hidden1" runat="server" type="hidden" />
            </div>
        </form>
    </center>
</body>
</html>
