<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cDoctores.aspx.vb" Inherits="cDoctores" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
     <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body>
    <center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/baner1.png" /><br />
        <table bgcolor="#fc0200" border="0" cellpadding="0" cellspacing="0" width="900px">
            <tr>
                <td align="left" style="height: 16px">
                    <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="False" Font-Italic="False"
                        ForeColor="White" OnClientClick="cierraventana();" Font-Names="Calibri" Font-Size="16px">| Regresar a Agenda |</asp:LinkButton>&nbsp;
                </td>
                <td style="height: 16px">&nbsp;
                    
                </td>
            </tr>
        </table>
        <br />
        <br />
        <table width="600" border="1" cellspacing="0" cellpadding="0">
          <tr>
            <td width="246" align="left">
                &nbsp;<asp:TextBox ID="txtfecha1" runat="server" Width="85px"></asp:TextBox><asp:Image ID="calendario1" runat="server" ImageUrl="~/imagenes/Calendar_scheduleHS.png" />&nbsp;
        <asp:TextBox ID="txtfecha2" runat="server" Width="69px"></asp:TextBox><asp:Image ID="calendario2" runat="server" ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
            <td width="246">&nbsp;
        </td>
            <td width="100" rowspan="2" align="center" valign="middle">
        <asp:LinkButton ID="LinkButton1" runat="server" Font-Bold="True" ForeColor="Red">CONSULTAR</asp:LinkButton>
                &nbsp;</td>
          </tr>
          <tr>
            <td colspan="2" align="left">
                &nbsp;<asp:DropDownList ID="cmbdoctores" runat="server" Width="474px">
        </asp:DropDownList></td>
          </tr>
        </table>
      <br />
        <asp:DataGrid ID="gridComicion" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1"
            ForeColor="Black" GridLines="None" Style="font-size: 8pt" Width="601px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="Control" />
            <Columns>
                <asp:BoundColumn DataField="elnombre" HeaderText="PACIENTE"></asp:BoundColumn>
                <asp:BoundColumn DataField="descripcion" HeaderText="TERAPIA"></asp:BoundColumn>
                <asp:BoundColumn DataField="cuenta" HeaderText="CANTIDAD"></asp:BoundColumn>
                <asp:BoundColumn DataField="importe" HeaderText="IMPORTE"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#FC0200" Font-Bold="False" ForeColor="White" />
        </asp:DataGrid>
        <asp:Button ID="cmdExporta" runat="server" Enabled="False" Font-Size="9pt" Text="EXPORTAR A EXCEL"
            Width="142px" /><br />
        <cc1:messagebox ID="Messagebox1" runat="server" />
        <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label><asp:Label
            ID="lblpagina" runat="server" Visible="False"></asp:Label><br />
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
            PopupButtonID="calendario1" TargetControlID="txtfecha1">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtfecha1">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy"
            PopupButtonID="calendario2" TargetControlID="txtfecha2">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtfecha2">
        </ajaxToolkit:MaskedEditExtender>
      
        </div>
    </form>
</center>
</body>
</html>
