<%@ Page Language="VB" AutoEventWireup="false" CodeFile="popupC.aspx.vb" Inherits="popupC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title> </title>
    </head>
<body onload="setTimeout('window.close()', 2000)">
    <form id="form1" runat="server">
        <div style="font-size: 8pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px;">
            <div style="text-align: center">
            <asp:Image ID="Image1" runat="server" Height="46px" ImageUrl="~/imagenes/encabezado.PNG"
                Width="112px" />
            </div>
            <br />
            <%--<div style="font-size: 6pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: center;">
                <strong>FISIOTERAPIA - HOSPITAL DE ORTOPEDIA</strong>
            </div>--%>
            <div style="font-size: 6pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: center;">
                <strong>Fisioterapia Y Medicina Deportiva S.C.P</strong>
            </div>
            <div style="font-size: 10pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: center;">
                <strong>Sucursal Anticanceroso</strong>
            </div>
            <br />
            <div style="font-size: 10pt; line-height: 9pt; font-family: consolas; letter-spacing:0px">
            <strong>Ticket No.:</strong> <asp:Label ID="lblcita" runat="server" Text="Label" Width="80px" Font-Names="consolas" Font-Size="10pt" Height="1px"></asp:Label>
            <br />
            <strong>Fecha:</strong>  <asp:Label ID="lblfecha" runat="server" Text="Label" Width="70px" Font-Names="consolas" Font-Size="10pt" Height="1px"></asp:Label>
            <br />
            <br />
            <strong>Paciente:<asp:Label ID="lblcliente" runat="server" Text="Label" Width="200px" Font-Names="consolas" Font-Size="8pt" Height="4px"></asp:Label></strong> 
            </div>
            <br />
            ============================================<br />
            <!-- 1 PRUEBA DE DETALLES&nbsp; $231.12<br />
            2 PRUEBA DE DETALLES&nbsp; $221.12<br />  -->
            <br />
                        <asp:Table ID="Table1" runat="server" Font-Size="9pt" Font-Bold="true" Font-Names="consolas">
                        </asp:Table>
                        <br />
            <asp:Table ID="Table2" runat="server" Font-Size="8pt" Font-Bold="true" Font-Names="consolas">
                        </asp:Table>
                        <br />
                        <asp:Table ID="Table3" runat="server" Font-Size="8pt" Font-Bold="true" Font-Names="consolas">
                        </asp:Table>
                        <br />
            ============================================<br />
        <div style="font-size: 10pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: left;">
            <strong>Observaciones:</strong><asp:Label ID="Label1" runat="server" Text="" Width="118px" Font-Bold="true" Font-Names="consolas" Font-Size="10pt"></asp:Label><br />
            </div>
            <asp:Table ID="Table4" runat="server" Font-Size="8pt" Font-Bold="true" Font-Names="consolas">
                        </asp:Table>
                        <br />
            <div style="font-size: 10pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: right;">
            <strong>TOTAL $</strong><asp:Label ID="lblabono" runat="server" Text="Label" Width="118px" Font-Bold="true" Font-Names="consolas" Font-Size="10pt"></asp:Label><br />
            </div>
            <br />
            <div style="font-size: 7pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: left;">
            <asp:Label ID="lblimporte1" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="208px" Visible="False"></asp:Label><br />
                <br />
            <asp:Label ID="lblimporte2" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="207px" Visible="False"></asp:Label><br />
                <br />
            <asp:Label ID="lblimporte3" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="207px" Visible="False"></asp:Label><br />
                <br />
                <asp:Label ID="vdescuento" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="207px" Visible="False"></asp:Label><br />
                <br />
            <asp:Label ID="vapoyo" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="207px" Visible="False"></asp:Label><br />
                <asp:Label ID="vobservacion" runat="server" Font-Names="consolas" Font-Size="7pt"
                Height="1px" Text="Label" Width="207px" Visible="False"></asp:Label><br />
                </div>
            <br />
            <br />
<div style="font-size: 8pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: center;">
            Gracias por su preferencia<asp:Label ID="lbldetalle" runat="server" Font-Names="consolas" Font-Size="5pt"
                Height="1px" Text="Label" Width="150px" Visible="False"></asp:Label>
    <asp:Label ID="lblobservacion" runat="server" Font-Names="consolas" Font-Size="5pt"
                Height="1px" Text="Label" Width="150px" Visible="False"></asp:Label>
     <asp:Label ID="lbldescuento" runat="server" Font-Names="consolas" Font-Size="5pt"
                Height="1px" Text="Label" Width="150px" Visible="False"></asp:Label>
    <asp:Label ID="lblapoyo" runat="server" Font-Names="consolas" Font-Size="5pt"
                Height="1px" Text="Label" Width="150px" Visible="False"></asp:Label>
    </div>
            <br />
            <div style="font-size: 6pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: right;">
            Fecha/Hora de Impresion<asp:Label ID="lblhora" runat="server" Font-Names="consolas" Font-Size="6pt"
                Height="1px" Text="Label" Width="150px" Visible="True"></asp:Label>
</div>
            <br />
            <div style="font-size: 6pt; line-height: 9pt; font-family: consolas; letter-spacing: 0px; text-align: right;">
            Reimpresion
    </div>
        </div>
    </form>
</body>
</html>
