<%@ Page Language="VB" AutoEventWireup="false" CodeFile="DefaultSM.aspx.vb" Inherits="DefaultSM"%>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>FISIOCARE</title>
</head>
<body onload="javascript:if(history.length>0)history.go(+1)">
<center>
    <form id="form1" runat="server">
    <div style="width:1250px; border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner2SM.png" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
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
                </asp:MenuItem>
                <asp:MenuItem Text="Camaras" Value="Camaras">
                    <asp:MenuItem NavigateUrl="~/camarasStar.aspx" Text="Camaras Star Medica" Value="Camaras Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/camarasplaza.aspx" Text="Camaras Star plaza" Value="Camaras Star plaza">
                    </asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
                        <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"></asp:Label>
            <hr />
            <table border="0" cellpadding="0" cellspacing="0" style="font-size: 18px; width: 1250px;
                color: #1A3773; font-family: Calibri; background-color: #f4f4f4">
                <tr>
                    <td align="center" style="width: 311px; text-align:center" rowspan="2">
                        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="False" Font-Names="Calibri"
                            Font-Size="16px" ForeColor="Red" ValidationGroup="fecha" CssClass="boton" Width="145px">Cambiar Fecha</asp:LinkButton></td>
                    <td align="center" style="text-align: center; width: 391px;" rowspan="2">
                        <asp:Label ID="lblfecNom" runat="server" Font-Size="27pt" ForeColor="Red" Text="Label"></asp:Label>&nbsp;&nbsp;&nbsp;
                        &nbsp;&nbsp;</td>
                    <td align="left" style="text-align: left; width: 74px;">
                        </td>
                    <td align="left" style=" text-align: left; width: 171px;">
                        <asp:LinkButton ID="LinkButton6" runat="server" Font-Bold="False" Font-Names="Calibri"
                            ForeColor="#1A3773" Height="14" Enabled="False">Citas Canceladas</asp:LinkButton></td>
                    <td align="right" style="text-align:right; width: 48px;">
                        <asp:LinkButton ID="lnkCanceladas" runat="server" Font-Bold="False" Font-Names="Calibri"
                            ForeColor="#1A3773" Height="14" Enabled="False">0</asp:LinkButton></td>
                    <td align="right" style="text-align: right; width: 39px;">
                        &nbsp;&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="left" style="text-align: left; width: 74px;">
                        </td>
                    <td align="left" style="text-align: left; width: 171px;">
                        Total de Citas del día</td>
                    <td align="right" style=" text-align:right; width: 48px;">
                        <asp:Label ID="lblCuantos" runat="server" Text="Label"></asp:Label></td>
                    <td align="right" style="text-align: right; width: 39px;">
                    </td>
                </tr>
            </table><hr />
        <table border="0" id="tHorarios" runat="server" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td style=" border: solid 1px red">
                    <asp:DataGrid ID="gFijo" runat="server" AutoGenerateColumns="False" BackColor="White" CellPadding="4" DataKeyField="idHorario"
                ForeColor="Black" Style="font-weight: bold" Width="100px" GridLines="None" BorderColor="White" BorderStyle="Solid" BorderWidth="2px">
                        <FooterStyle BackColor="#CCCC99" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="#F4F4F4" BorderStyle="Solid" BorderColor="#1A3773" BorderWidth="1px" HorizontalAlign="Center" />
                        <ItemStyle BackColor="White" Font-Names="Calibri" Font-Size="14px" HorizontalAlign="Center" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px" />
                        <Columns>
                            <asp:BoundColumn DataField="horario" HeaderText="HORARIOS">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle BackColor="#F4F4F4" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="#1A3773" Width="75px" />
                            </asp:BoundColumn>
                        </Columns>
                        <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" HorizontalAlign="Center" Font-Names="calibri" Font-Size="11pt" />
                    </asp:DataGrid></td>
                <td style="border: solid 1px red">
                    <asp:DataGrid ID="gridHorarios" runat="server" AutoGenerateColumns="False" BackColor="White" CellPadding="4" DataKeyField="idHorario"
                ForeColor="Black" Style="font-weight: bold" Width="1145px" GridLines="None" BorderColor="White" BorderStyle="Solid" BorderWidth="2px">
                        <FooterStyle BackColor="#CCCC99" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                    Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                        <AlternatingItemStyle BackColor="#F4F4F4" BorderStyle="Solid" BorderColor="#1A3773" BorderWidth="1px" HorizontalAlign="Center" />
                        <ItemStyle BackColor="White" Font-Names="Calibri" Font-Size="14px" HorizontalAlign="Center" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px" />
                        <Columns>
                            <asp:BoundColumn DataField="horario" HeaderText="HORARIOS" Visible="False">
                                <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Center" />
                                <ItemStyle BackColor="#F4F4F4" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                            Font-Strikeout="False" Font-Underline="False" ForeColor="#1A3773" Width="75px" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;CRISTINA&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk1" runat="server" CommandName="lnk1" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl1" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;CRISTINA&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk2" runat="server" CommandName="lnk2" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl2" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;LUCELY&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk3" runat="server" CommandName="lnk3" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl3" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;LUCELY&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk4" runat="server" CommandName="lnk4" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl4" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;CLARIZA&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk5" runat="server" CommandName="lnk5" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl5" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;CLARIZA&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk6" runat="server" CommandName="lnk6" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl6" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="SALA &quot;RUBEN&quot;">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk7" runat="server" CommandName="lnk7" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl7" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SALA &quot;RUBEN&quot;" Visible="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnk8" runat="server" CommandName="lnk8" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                            <asp:Label ID="lbl8" runat="server" Visible="False"></asp:Label>
                        </ItemTemplate>
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" />
                    </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="A.CONSULTA">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnk9" runat="server" CommandName="lnk9" ForeColor="Blue">DISPONIBLE</asp:LinkButton>
                        <asp:Label ID="lbl9" runat="server" Visible="False"></asp:Label>
                    </ItemTemplate>
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" ForeColor="Blue" />
                </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" HorizontalAlign="Center" Font-Names="calibri" Font-Size="11pt" />
            </asp:DataGrid></td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
        </asp:ScriptManager>
        <ajaxToolkit:ModalPopupExtender BackgroundCssClass="FondoAplicacion" PopupControlID="Panel1" ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButton1">
        </ajaxToolkit:ModalPopupExtender>
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
        <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="Gray" BorderStyle="Dashed" BorderWidth="1px">
            <asp:Calendar ID="calendario" runat="server" BorderColor="#F4F4F4" BorderStyle="Solid"
                BorderWidth="2px" Font-Names="calibri" Font-Size="12pt" Height="298px" ShowGridLines="True"
                Width="368px">
                <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
                <DayStyle BorderColor="Silver" Font-Bold="False" />
                <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                <TitleStyle BackColor="#F4F4F4" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"
                    ForeColor="#1A3773" />
            </asp:Calendar>
        </asp:Panel>
    </div>
    </form>
</center>
</body>
</html>
