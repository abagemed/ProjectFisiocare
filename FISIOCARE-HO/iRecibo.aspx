<%@ Page Language="VB" AutoEventWireup="false" CodeFile="iRecibo.aspx.vb" Inherits="iRecibo" EnableEventValidation="false"%>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
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
</head>
<body onload="javascript:if(history.length>0)history.go(+1)"><center>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>          
    <div style="font-family:Calibri; font-size:12pt;  width:900px; border: solid 1px #ff0000;">
    <img src="imagenes/baner1.png" /><br />
        <table border="0" cellpadding="0" cellspacing="0" width="900px" style="background-color:#fc0200">
            <tr>
                <td align="left" style="width: 344px; height: 16px">
                    <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="False" Font-Italic="False"
                        ForeColor="White" OnClientClick="cierraventana()" Font-Names="Calibri" Font-Size="16px">| Regresar |</asp:LinkButton></td>
                <td align="right" style="height: 16px">
                    &nbsp;
                </td>
            </tr>
        </table>
        <br />
        <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; text-align:left">
            <tr>
                <td colspan="5" style="text-align:center; background-color:#f4f4f4; color:#1a3773">
                    FISIOTERAPIA Y MEDICINA DEPORTIVA
                </td>
            </tr>
            <tr>
                <td style="width: 59px; background-color:#f4f4f4">
                    <asp:Label ID="Label17" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="70px"></asp:Label></td>
                <td style="width: 67px">
                    <asp:Label ID="lblrecibo" runat="server" ForeColor="Black" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="70px"></asp:Label></td>
                <td style="width: 487px">
                </td>
                <td style="width: 70px; background-color:#f4f4f4">
                    <asp:Label ID="Label2" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Fecha" Width="70px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:Label ID="txtfecha" runat="server" ForeColor="Black" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="70px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 59px">
                </td>
                <td style="width: 67px">
                </td>
                <td style="width: 487px">
                                <asp:DataGrid ID="gridConceptos" runat="server" AutoGenerateColumns="False" Width="540px" CellPadding="3">
                                    <Columns>
                                        <asp:BoundColumn DataField="descripcion" HeaderText="CONCEPTO"></asp:BoundColumn>
                                        <asp:BoundColumn DataField="costo" HeaderText="IMPORTE"></asp:BoundColumn>
                                    </Columns>
                                    <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                                </asp:DataGrid></td>
                <td style="width: 70px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 59px; background-color:#f4f4f4">
                    <asp:Label ID="Label6" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Cliente" Width="70px"></asp:Label></td>
                <td colspan="2">
                    <asp:Label ID="txtcliente" runat="server" ForeColor="Black" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="616px"></asp:Label></td>
                <td style="width: 70px; background-color:#f4f4f4">
                    <asp:Label ID="Label4" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Importe" Width="70px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:Label ID="txtimporte" runat="server" ForeColor="Black" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="70px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 59px">
                </td>
                <td style="width: 67px">
                </td>
                <td style="width: 487px">
                </td>
                <td style="width: 70px; background-color:#f4f4f4">
                    <asp:Label ID="Label5" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Abono" Width="70px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:Label ID="txtabono" runat="server" ForeColor="Black" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Recibo" Width="70px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 59px">
                </td>
                <td style="width: 67px">
                </td>
                <td style="width: 487px">
                </td>
                <td style="width: 70px">
                </td>
                <td style="width: 100px">
                </td>
            </tr>
            <tr>
                <td style="width: 59px">
                </td>
                <td style="width: 67px">
                </td>
                <td style="width: 487px">
                </td>
                <td colspan="2" style="background-color:red; text-align:center">
                   <%-- &nbsp;<asp:Button ID="Button2233" runat="server" CssClass="boton"  ForeColor="Red" Text="Imprimir" />--%>
                    <asp:LinkButton ID="btnimprimir" runat="server" Font-Bold="True" Font-Size="14pt"
                                    ForeColor="White" Visible="true">Imprimir!</asp:LinkButton>&nbsp;
                    </td>
            </tr>
        </table>
                                <asp:DropDownList ID="cmbImpresoras" runat="server" Width="185px">
                                </asp:DropDownList>&nbsp;
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
        <asp:Label ID="lblfechacita" runat="server" Text="Label"
            Visible="False"></asp:Label><asp:Label ID="lblidcita" runat="server" Text="Label"
                Visible="False"></asp:Label>
        &nbsp;&nbsp;<asp:Label ID="lblpagina" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lbldetalle" runat="server" Visible="False"></asp:Label>
        &nbsp; &nbsp;
        <CR:CrystalReportSource ID="origen_reporte" runat="server">
        </CR:CrystalReportSource>
        <CR:CrystalReportViewer ID="visor_reporte" runat="server" AutoDataBind="true" />
    </div>
    </form>
    </center>
</body>
</html>
