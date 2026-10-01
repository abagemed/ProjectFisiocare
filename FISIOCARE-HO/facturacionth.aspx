<%@ Page Language="VB" AutoEventWireup="false" CodeFile="facturacionth.aspx.vb" Inherits="facturacionth" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt" align="center">
        <img src="imagenes/baner2.png" />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <table bgcolor="#fc0200" border="0" cellpadding="0" cellspacing="0" width="900">
            <tr>
                <td align="left" style="width: 701px">
                    <asp:LinkButton ID="LinkButton8" runat="server" Font-Names="calibri" Font-Size="18px"
                        ForeColor="White">Facturas Generadas</asp:LinkButton></td>
                <td style="text-align:right">
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="calibri" Font-Size="18px"
                        ForeColor="White">Agenda</asp:LinkButton></td>
            </tr>
        </table>
        <div id="divOpciones" runat="server">
      <div id="divControles" runat="server" style="text-align: right">  <hr /> 
        <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; background-color:#f4f4f4">
            <tr>
                <td style="text-align:left">
                    <asp:DropDownList ID="cmbclientes" runat="server" Width="429px" Font-Names="calibri" Font-Size="12pt" AutoPostBack="True">
                    </asp:DropDownList></td>
                <td style="width: 448px">
                    <asp:Button ID="Button3" runat="server" CssClass="boton" Font-Names="calibri"
                        Font-Size="11pt" ForeColor="Red" Text="Tx Sin Facturar" Width="130px" />
                    &nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnfacturar" runat="server" CssClass="boton" Enabled="False" Font-Names="calibri"
                        Font-Size="11pt" ForeColor="Red" Text="Facturar Tx" Width="130px" />
                    &nbsp;&nbsp;
                    <asp:Button ID="btnAsigna" runat="server" CssClass="boton" Enabled="False" Font-Names="calibri"
                        Font-Size="11pt" ForeColor="Red" Text="Asignar Tx a Factura" Width="130px" /></td>
            </tr>
        </table>
        <hr />
        <asp:Button ID="cmdexcel" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Visible="False" />
        </div>
        <asp:DataGrid ID="gridFacturas" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4" CellSpacing="1" DataKeyField="idabono"
            ForeColor="Black" GridLines="None" Width="900px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="WhiteSmoke" />
            <Columns>
                <asp:BoundColumn DataField="idcita" HeaderText="# Recibo"></asp:BoundColumn>
                <asp:BoundColumn DataField="elnombre" HeaderText="CLIENTE"></asp:BoundColumn>
                <asp:BoundColumn DataField="descripcion" HeaderText="TERAPIA"></asp:BoundColumn>
                <asp:BoundColumn DataField="fecha" HeaderText="FECHA"></asp:BoundColumn>
                <asp:BoundColumn DataField="abono" DataFormatString="{0:C}" HeaderText="ABONOS">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importe" DataFormatString="{0:C}" HeaderText="IMPORTES">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox1" runat="server" />
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="idcliente" Visible="False"></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="CoAseguro">
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("coaseguro") %>' Enabled="false" />
                    </ItemTemplate>
                    <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Center" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" />
        </asp:DataGrid>
        </div> <div id="lafactura" runat="server" visible="false">
        <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; text-align:left ">
            <tr>
                <td colspan="2" style="background-color:#f4f4f4; color:#1a3773; text-align:right; border-top: solid 4px white">
                       MERIDA,YUCATAN,MEX.,A
                        <asp:TextBox ID="ftxtdia" Style="border-bottom: #e0e0e0 2px solid; text-align:center" runat="server" Width="50px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        DE&nbsp;<asp:DropDownList ID="cmbmes" runat="server" Style="font-size: 9pt; font-family: Arial">
                            <asp:ListItem Value="01">ENERO</asp:ListItem>
                            <asp:ListItem Value="02">FEBRERO</asp:ListItem>
                            <asp:ListItem Value="03">MARZO</asp:ListItem>
                            <asp:ListItem Value="04">ABRIL</asp:ListItem>
                            <asp:ListItem Value="05">MAYO</asp:ListItem>
                            <asp:ListItem Value="06">JUNIO</asp:ListItem>
                            <asp:ListItem Value="07">JULIO</asp:ListItem>
                            <asp:ListItem Value="08">AGOSTO</asp:ListItem>
                            <asp:ListItem Value="09">SEPTIEMBRE</asp:ListItem>
                            <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                            <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                            <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                        </asp:DropDownList>
                        DE<asp:DropDownList ID="cmbaño" runat="server">
                            <asp:ListItem>2009</asp:ListItem>
                            <asp:ListItem>2010</asp:ListItem>
                            <asp:ListItem>2011</asp:ListItem>
                            <asp:ListItem>2012</asp:ListItem>
                            <asp:ListItem>2013</asp:ListItem>
                            <asp:ListItem>2014</asp:ListItem>
                            <asp:ListItem>2015</asp:ListItem>
                            <asp:ListItem>2016</asp:ListItem>
                            <asp:ListItem>2017</asp:ListItem>
                        </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 80px; background-color:#f4f4f4">
                        <asp:Label ID="Label16" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="No. Fac." Width="70px"></asp:Label></td>
                <td style="width: 820px">
                        <asp:TextBox ID="txtNfac" runat="server" Style="border-bottom: #e0e0e0 2px solid" Enabled="False" Width="43px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:TextBox ID="txtNserie" runat="server" Style="border-bottom: #e0e0e0 2px solid" ReadOnly="True" Width="31px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 80px; background-color:#f4f4f4">
                        <asp:Label ID="Label1" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Nombre" Width="70px"></asp:Label></td>
                <td style="width: 820px">
                        <asp:TextBox ID="ftxtnombre" Style="border-bottom: #e0e0e0 2px solid" runat="server" Enabled="False" Width="770px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:DropDownList
                            ID="cmbcliefac" runat="server" AutoPostBack="True" Visible="False" Width="775px">
                        </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="width: 80px; background-color:#f4f4f4">
                        <asp:Label ID="Label2" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Domicilio" Width="70px"></asp:Label></td>
                <td style="width: 820px">
                        <asp:TextBox ID="ftxtdomicilio" runat="server" Enabled="False" Style="border-bottom: #e0e0e0 2px solid"
                            Width="770px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 80px; background-color:#f4f4f4">
                        <asp:Label ID="Label3" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Localidad" Width="70px"></asp:Label></td>
                <td style="width: 820px">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 455px">
                        <asp:TextBox ID="ftxtciudad" runat="server" Enabled="False" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"></asp:TextBox></td>
                            <td style="width: 46px; background-color:#f4f4f4">
                        <asp:Label ID="Label4" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="R.F.C." Width="50px"></asp:Label></td>
                            <td style="width: 142px">
                        <asp:TextBox ID="ftxtrfc" runat="server" Enabled="False" Width="137px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
          <hr />
            <asp:DataGrid ID="gridDetalles" runat="server" AutoGenerateColumns="False" BackColor="White"
                BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1" DataKeyField="idcosto"
                Font-Names="Calibri" Font-Size="14px" ForeColor="Black" GridLines="None" Width="900px">
                <FooterStyle BackColor="Tan" />
                <EditItemStyle Font-Names="9pt" />
                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <Columns>
                    <asp:TemplateColumn HeaderText="Cantidad">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" Font-Names="Calibri" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Font-Size="14px" Style="text-align: center; border-bottom: #e0e0e0 2px solid" Text='<%# Bind("cantidad") %>' Width="61px" OnTextChanged="TextBox1_TextChanged"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                FilterType="Numbers" TargetControlID="TextBox1">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                        <HeaderStyle Width="78px" />
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Font-Names="Calibri" Font-Size="14px" Style="text-align: center; border-bottom: #e0e0e0 2px solid"
                                Text='<%# Bind("descripcion") %>' Width="700px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Importe"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="True" Font-Names="Calibri" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"
                                Font-Size="14px" Style="text-align:right; border-bottom: #e0e0e0 2px solid" Text='<%# Bind("importe") %>' Width="70px" OnTextChanged="TextBox3_TextChanged"></asp:TextBox>
                            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
                                TargetControlID="TextBox3" ValidChars="1234567890,.">
                            </ajaxToolkit:FilteredTextBoxExtender>
                        </ItemTemplate>
                        <HeaderStyle Width="85px" />
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="elnombre" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="domicilio" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="importe" HeaderText="auximporte" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idcliente" Visible="False"></asp:BoundColumn>
                    <asp:BoundColumn DataField="observacion" Visible="False"></asp:BoundColumn>
                </Columns>
                <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" />
            </asp:DataGrid>
            <hr />
            <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:left">
                <tr>
                    <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label5" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Cantidad "
                            Width="70px"></asp:Label></td>
                    <td style="width: 563px">
                                    <asp:TextBox ID="ftxtletras" Style="border-bottom: #e0e0e0 2px solid" runat="server" ReadOnly="True" Width="500px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                    <td style="width: 70px; background-color:#f4f4f4">
                        <asp:Label ID="Label6" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Importe" Width="75px"></asp:Label></td>
                    <td style="width: 85px; text-align:right">
                        $<asp:TextBox ID="ftxtimporte" runat="server" ReadOnly="True" style="border-bottom: #e0e0e0 2px solid; text-align:right" Width="70px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label9" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Paciente" Width="70px"></asp:Label></td>
                    <td style="width: 563px">
                                    <asp:TextBox ID="txtpaciente" Style="border-bottom: #e0e0e0 2px solid" runat="server" Width="500px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                    <td style="width: 70px; background-color:#f4f4f4">
                        <asp:Label ID="Label7" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="I.V.A." Width="75px"></asp:Label></td>
                    <td style="width: 85px; text-align:right">
                        $<asp:TextBox ID="ftxtiva" style="border-bottom: #e0e0e0 2px solid; text-align:right" runat="server" ReadOnly="True" Width="70px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px">0</asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label10" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Observa." Width="70px"></asp:Label></td>
                    <td style="width: 563px">
                                    <asp:TextBox ID="TXTmiobserva" runat="server" Style="border-bottom: #e0e0e0 2px solid" MaxLength="254" Width="500px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                        <asp:CheckBox
                                        ID="chkImp" runat="server" Text="Imp." /></td>
                    <td style="width: 70px; background-color:#f4f4f4">
                        <asp:Label ID="Label8" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="Total" Width="75px"></asp:Label></td>
                    <td style="width: 85px; text-align:right">
                        $<asp:TextBox ID="ftxttotal" style="border-bottom: #e0e0e0 2px solid; text-align:right" runat="server" ReadOnly="True" Width="70px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label11" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="F. Pago" Width="70px"></asp:Label></td>
                    <td style="width: 563px">
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 232px">
                                                <asp:DropDownList ID="cmbClaveMP" runat="server" Width="50px"  Enabled="false">
                                                </asp:DropDownList>
                                                <asp:DropDownList ID="cmbFormaPago" runat="server" AutoPostBack="True" Width="160px">
                                                </asp:DropDownList></td>
                                <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label12" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="N. Cuenta" Width="70px"></asp:Label></td>
                                <td style="width: 270px">
                                                <asp:TextBox ID="txtnumcuenta" runat="server" Width="175px" Style="border-bottom: #e0e0e0 2px solid" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td style="width: 70px;">
                    </td>
                    <td style="width: 85px; text-align: right">
                    </td>
                </tr>
                <tr>
                    <td style="width: 73px; background-color:#f4f4f4">
                        <asp:Label ID="Label13" runat="server" Font-Names="calibri" Font-Size="11pt" ForeColor="#1A3773"
                            Height="20px" Style="border-bottom: #e0e0e0 2px solid" Text="E-m@il" Width="70px"></asp:Label></td>
                    <td style="width: 563px">
                                                <asp:TextBox ID="txtmail" runat="server" Width="500px" Style="border-bottom: #e0e0e0 2px solid" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><asp:CheckBox
                                                    ID="chkMail" runat="server" ValidationGroup="vCorreo" /></td>
                    <td colspan="2" >
            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                    ControlToValidate="txtmail" ErrorMessage="correo invalido" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                    ValidationGroup="vCorreo"></asp:RegularExpressionValidator></td>
                </tr>
                <tr>
                    <td style="width: 73px;">
                        </td>
                    <td colspan="3" style=" text-align:right; padding: 0 0 0 0">
                        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; background-color:red">
                            <tr>
                                <td  style="background-color:White; width: 528px;">
                                </td>
                                <td >
                                    <asp:Button ID="btnFreg" runat="server" Font-Bold="False" Font-Names="Calibri" Font-Size="11pt"
                                        ForeColor="Red" OnClientClick="javascript:this.disabled=true;" Text="Regresar a Buscar"
                                        UseSubmitBehavior="False" Width="120px" CssClass="boton" /></td>
                                <td >
                                    <asp:Button ID="btnFguardar" runat="server" Font-Bold="False" Font-Names="Calibri"
                                        Font-Size="11pt" ForeColor="Red" OnClientClick="javascript:this.disabled=true;"
                                        Text="Generar Factura" UseSubmitBehavior="False" ValidationGroup="vCorreo"
                                        Width="120px" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        <asp:Label ID="lblCondiciones" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblrecibos" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbltempclie" runat="server" Visible="False"></asp:Label><input id="hfModFac"
            runat="server" style="width: 1px" type="hidden" /><input id="hfValFacturar" runat="server"
                style="width: 1px" type="hidden" /><input id="hfidDatosFac" runat="server" style="width: 1px"
                    type="hidden" />
            <asp:Label ID="lblfecha" runat="server" Visible="False"></asp:Label>
            <cr:crystalreportsource id="origen_reporte" runat="server"> </cr:crystalreportsource></div>
        <CR:CrystalReportViewer ID="visor_reporte" runat="server" AutoDataBind="true" />
        <asp:Panel ID="pnlasigna" runat="server" Height="66px" Visible="False" Width="900px" HorizontalAlign="Center">
                <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; text-align:center">
                    <tr>
                        <td>
            <hr />
                        </td>
                        <td style="background-color: #f4f4f4; width: 65px; text-align:left">
                            <asp:Label style="border-bottom: #e0e0e0 2px solid" Font-Names="calibri" Font-Size="16pt" ForeColor="#1A3773" Height="27px" ID="Label14" runat="server" Text="No. Fac." Width="70px" /></td>
                        <td style="width: 97px;">
                            <asp:TextBox ID="txtAfac" Style="border-bottom: #e0e0e0 2px solid;text-align: center" runat="server" Width="53px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px"></asp:TextBox>
                            <asp:TextBox ID="txtAserie" Style="border-bottom: #e0e0e0 2px solid" runat="server" ReadOnly="True" Width="29px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Font-Size="20px"></asp:TextBox></td>
                        <td style="width: 159px;background-color:#f4f4f4">
                <asp:Button ID="Button2" runat="server" CssClass="boton" Font-Size="12pt" ForeColor="Red"
                    Text="Grabar" Width="70px" />
                            &nbsp;&nbsp;
                            <asp:Button ID="Button1" runat="server" CssClass="boton" Font-Size="12pt" ForeColor="Red"
                    Text="Cancelar" Width="70px" /></td>
                    </tr>
                </table>
                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
                    FilterType="Numbers" TargetControlID="txtAfac">
                </ajaxToolkit:FilteredTextBoxExtender>
                <input id="sumimportes" runat="server" type="hidden" /><br />
                </asp:Panel>
            <cc1:messagebox ID="Messagebox1" runat="server" />
        </div>
        
    </form>
    </center>
</body>
</html>
