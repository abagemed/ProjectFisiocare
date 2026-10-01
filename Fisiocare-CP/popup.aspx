<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popup.aspx.vb" Inherits="popup" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title> </title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div style="width: 392px; height: 257px" id="visor" >
            <table style="width: 435px; height: 154px; font-size: 8pt; font-family: Arial;">
                <tr>
                    <td colspan="4" style="font-size: small">
                        <span style="font-size: 10pt">
                        FISIOCARE CENTRO DE MEDICINA DEPORTIVA Y REHABILITACION<br />
                        Tel StarMedica: (999) 2 85 07 28 &nbsp; Tel Campestre: (999) 9 44 54 27</span></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td style="width: 173px; font-size: small;">
                        <span style="font-size: 9pt">
                        No.</span></td>
                    <td style="width: 208px; font-size: small;">
                        <asp:Label ID="lblcita" runat="server" Text="Label" Width="152px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                    <td style="width: 164px; font-size: 8pt;">
                        Fecha:</td>
                    <td style="width: 213px; font-size: 8pt;">
                        <asp:Label ID="lblfecha" runat="server" Text="Label" Width="153px"></asp:Label></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td style="width: 173px; font-size: small;">
                        <span style="font-size: 9pt">
                        Cliente:</span></td>
                    <td colspan="3" style="font-size: small; width: 213px">
                        <asp:Label ID="lblcliente" runat="server" Text="Label" Width="363px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td style="width: 173px; font-weight: bold; font-size: small;">
                        <span style="font-size: 9pt">
                        concepto</span></td>
                    <td style="width: 208px; font-size: small;">
                    </td>
                    <td style="width: 164px; font-weight: bold; font-size: small;">
                        </td>
                    <td style="width: 213px; font-size: 8pt; font-weight: bold;">
                        importe</td>
                </tr>
                <tr style="font-size: 8pt">
                    <td colspan="4" style="font-size: small; width: 213px;">
                        --------------------------------------------------------------------------------------------------------<br />
                        <asp:Table ID="Table1" runat="server" Font-Size="8pt">
                        </asp:Table>
                        ----
                        ---------------------------------------------------------------------------------------------------</td>
                </tr>
                <tr style="font-size: 8pt">
                    <td style="width: 173px; font-weight: bold; font-size: small;">
                        <span style="font-size: 9pt">
                        Abono:</span></td>
                    <td style="width: 208px; font-size: small;">
                        <asp:Label ID="lblabono" runat="server" Text="Label" Width="149px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                    <td style="width: 164px; font-weight: bold; font-size: 8pt;">
                        <span style="font-size: 9pt">I<span style="font-size: 8pt">mporte Total:</span></span></td>
                    <td style="width: 213px; font-size: 8pt;">
                        <asp:Label ID="lblimporte" runat="server" Text="Label" Width="151px" Font-Names="Arial" Font-Size="8pt"></asp:Label></td>
                </tr>
                <tr style="font-size: 8pt">
                    <td style="width: 173px">
                    </td>
                    <td style="width: 208px">
                    </td>
                    <td style="width: 164px">
                    </td>
                    <td style="width: 200px">
                    </td>
                </tr>
                <tr style="font-size: 8pt">
                    <td colspan="4" style="font-size: small; height: 18px;">
                        <span style="font-size: 8pt">
                        * Gracias por su preferencia !!!</span></td>
                </tr>
            </table>
            <asp:Label ID="lbldetalle" runat="server" Text="Label" Visible="False" Width="43px"></asp:Label><br />
        </div>
    
    </div>
    </form>
</body>
</html>
