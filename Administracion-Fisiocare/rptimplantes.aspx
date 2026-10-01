<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptimplantes.aspx.vb" Inherits="rptimplantes" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Reporte de Ventas Estimadas...::: Implantes & BSN ::::...</title>
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:1200px; border: solid 1px #0404B4; font-family:Calibri; font-size:11pt;" align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <img src="imagenes/bannerio.png" width="1200px"/>
        <table bgcolor="#5882FA" border="0" cellpadding="0"
            cellspacing="0" width="1200px">
            <tr>
                <td style="height: 16px" colspan="2">
                    &nbsp;&nbsp;
                    <asp:Label ID="lbltitulo" runat="server" Font-Names="calibri" Font-Size="12pt" ForeColor="White"
                        Text="Label"></asp:Label>&nbsp;</td>
            </tr>
        </table>
        <div style="text-align: center">
        <strong><span style="font-size: 16pt; font-family: Trebuchet MS">
        REPORTE DE VENTAS (IMPLANTES-BSN-INNOMED-PALAKKAD-ANTICIPOS-MEDTRONIC)</span></strong>
            <br />
            <asp:Button ID="cmdexcelcm" runat="server" Text="Exportar a Excel" CssClass="boton" ForeColor="Black" Font-Bold="True" />
        <div id="divprincipal"  visible="false" style="visibility:hidden">
        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align: center" >
            <tr style="border-bottom-width:2px; border-bottom-color:#f4f4f4; border-bottom-style:solid">
                <td style="width: 150px">
                    <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="10pt"
                        Width="160px" Visible="False">
                        <asp:ListItem Value="adMed_Solution_de_sure2018">--Todas las Tiendas--</asp:ListItem>
                        </asp:DropDownList></td>
                <td style="width: 300px">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator>
                            <asp:TextBox
                                ID="txtfecha1" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox>
                    <asp:Image ID="Image1" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" Visible="False" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha2" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Visible="False"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" Visible="False" /></td>
                <td> 
                <asp:TextBox ID="txtNumdias" runat="server" Width="68px" style="text-align:center" Visible="False"></asp:TextBox>
               </td>
               
            </tr>
            
        </table>
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
            <div style="width:1200px; overflow:auto;">
        <asp:Label ID="lbltotales" runat="server" Font-Bold="True" Font-Size="16px" Text="RESUMEN"
                         Visible="False"></asp:Label>
                         <table  border="0" cellpadding="0" cellspacing="0" style="width: 1200px; overflow:auto;">
                            <tr style="vertical-align:top; overflow:auto;">
                                <td style="width: 180px; overflow:auto;">
                                <asp:DataGrid ID="gbaseTOTAL" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                     <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseTOTAL2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="#003399">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridtotales" runat="server"
                                        Font-Size="9pt" Font-Names="Calibri" BackColor="#CCFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="9pt" />
                         </asp:DataGrid>
                         </div></td>
                            
                            </tr>
                        </table>
        </div>
        <br />
        <hr />
            <div style="width:1200px; overflow:auto;">
        <asp:Label ID="lblaltabrisa" runat="server" Font-Bold="True" Font-Size="16px" Text="IMPLANTES"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseALTA" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseoi" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasALTA" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFFCC" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black" AutoGenerateColumns="true" >
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                             <PagerStyle BackColor="Olive" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                 Font-Strikeout="False" Font-Underline="False" ForeColor="Red" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                         <br>
                        <asp:Label ID="lblcma" runat="server" Font-Bold="True" Font-Size="16px" Text="BSN"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseCMA" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td><td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseCMA2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasCMA" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                          <br>
                         <asp:Label ID="lblinnomed" runat="server" Font-Bold="True" Font-Size="16px" Text="INNOMED"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseINNOMED" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseINNOMED2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasINNOMED" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                         <br>
                         <asp:Label ID="lblpalak" runat="server" Font-Bold="True" Font-Size="16px" Text="PALAKKAD"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbasePALAK" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbasePALAK2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasPALAK" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                <br>
                         <asp:Label ID="lblanticipos" runat="server" Font-Bold="True" Font-Size="16px" Text="ANTICIPOS"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseanticipos" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseanticipos2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasanticipos" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                <br>
                         <asp:Label ID="lblmedtronic" runat="server" Font-Bold="True" Font-Size="16px" Text="MEDTRONIC"
                         Visible="False"></asp:Label>
                         <table border="0" cellpadding="0" cellspacing="0" style="width: 1200px">
                            <tr style="vertical-align:top">
                                <td style="width: 180px">
                                <asp:DataGrid ID="gbaseMEDTRONIC" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="180px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width: 250px; overflow:auto;">
                                <asp:DataGrid ID="gbaseMEDTRONIC2" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="250px" ForeColor="Black">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" ForeColor="DarkRed" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                     </asp:DataGrid></td>
                                <td style="width:765px; overflow:auto;">
                     <div style="width:765px; overflow:auto;">
                         <asp:DataGrid ID="gridventasMEDTRONIC" runat="server"
                                        Font-Size="8pt" Font-Names="Calibri" BackColor="#FFFF99" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" ForeColor="Black">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" BackColor="Silver" Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" ForeColor="MidnightBlue" />
                             <ItemStyle HorizontalAlign="left" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                         </div>
                                </td>
                            </tr>
                        </table>
                     </div>

        
        </div>
        
        <input id="hfSucursal" runat="server" type="hidden" />
        <input id="hfclientes" runat="server" type="hidden" /><br />
    </form>
    </center>
</body>
</html>
