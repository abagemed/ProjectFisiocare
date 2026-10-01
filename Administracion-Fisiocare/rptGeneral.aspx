<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptGeneral.aspx.vb" Inherits="rptGeneral" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administración Fisiocare</title>
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <img src="imagenes/baner1.png" /><table bgcolor="#fc0200" border="0" cellpadding="0"
            cellspacing="0" width="900px">
            <tr>
                <td style="height: 16px" colspan="2">
                    &nbsp;&nbsp;
                    <asp:Label ID="lbltitulo" runat="server" Font-Names="calibri" Font-Size="12pt" ForeColor="White"
                        Text="Label"></asp:Label>&nbsp;</td>
            </tr>
        </table>
        <div>
       
            <br />

         <cc1:TabContainer ID="TabContainer1" runat="server" ActiveTabIndex="0" style="text-align: left">
                <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Ocupacion
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="gbaseEncabezado" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="260px">
                         <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="gEncabezado" runat="server" Font-Names="Calibri" Font-Size="10pt">
                             <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                     <asp:Label ID="lblcampestre" runat="server" Font-Bold="True" Font-Size="16px" Text="CAMPESTRE"
                         Visible="False"></asp:Label><table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                             <tr style="vertical-align:top">
                                 <td style="width: 100px">
                     <asp:DataGrid ID="gbaseCP" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Terapias">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                 <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="gridOcupacion" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                 </td>
                             </tr>
                         </table>
                     <asp:Label ID="lblstarmedica" runat="server" Font-Bold="True" Font-Size="16px" Text="STAR MEDICA"
                         Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="gbaseSM" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Terapias">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="gridOcupacionSM" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                     <asp:Label ID="lbls" runat="server" Font-Bold="True" Font-Size="16px" Text="Seniors Gym"
                         Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="DataGrid2" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Mensualidades">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="DataGrid1" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                         <asp:Label ID="lblnho" runat="server" Font-Bold="True" Font-Size="16px" Text="Fisiocare HO"
                         Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="gridbaseho" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Terapias">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="griddatosho" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                         <br />
                         <asp:Label ID="lblnac" runat="server" Font-Bold="True" Font-Size="16px" Text="Fisiocare Anticanceroso"
                         Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="gridbaseac" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Terapias">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="griddatosac" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                         <asp:Label ID="lblnpe" runat="server" Font-Bold="True" Font-Size="16px" Text="Fisiocare Pensiones"
                         Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                     <asp:DataGrid ID="gridbasepe" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                         <Columns>
                             <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                             <asp:BoundColumn DataField="descripcion" HeaderText="Terapias">
                                 <ItemStyle Font-Bold="False" Font-Italic="False" Font-Names="calibri" Font-Overline="False"
                                        Font-Size="8pt" Font-Strikeout="False" Font-Underline="False" />
                             </asp:BoundColumn>
                             <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                         </Columns>
                         <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                         <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                     </asp:DataGrid></td>
                                <td style="width: 100px">
                     <div style="width:620px; overflow:auto;">
                         <asp:DataGrid ID="griddatospe" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                             <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                             <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                         </asp:DataGrid>
                     </div>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel3" runat="server" HeaderText="TabPanel3">
                    <HeaderTemplate>
                        Enviados
                    </HeaderTemplate>
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; vertical-align:top;">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                                    <asp:DataGrid ID="gbaseEncabezadoE" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="260px">
                                        <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                    </asp:DataGrid></td>
                                <td style="width: 100px">
                                    <div style="width:620px; overflow:auto;">
                                        <asp:DataGrid ID="gEncabezadoE" runat="server" Font-Names="Calibri" Font-Size="10pt">
                                            <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                                    <asp:Label ID="lblcampestreE" runat="server" Font-Bold="True" Font-Size="16px" Text="CAMPESTRE"
                                        Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                                    <asp:DataGrid ID="gbaseCPe" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                                        <Columns>
                                            <asp:BoundColumn DataField="nombre"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                                    </asp:DataGrid></td>
                                <td style="width: 100px">
                                    <div style="width:620px; overflow:auto;">
                                        <asp:DataGrid ID="gridOcupacionE" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                                            <HeaderStyle Font-Underline="False" HorizontalAlign="Center" Height="55px" Font-Names="Calibri" Font-Size="10pt" />
                                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                                    <asp:Label ID="lblstarmedicaE" runat="server" Font-Bold="True" Font-Size="16px" Text="STAR MEDICA"
                                        Visible="False"></asp:Label>
                        <table border="0" cellpadding="0" cellspacing="0">
                            <tr style="vertical-align:top">
                                <td style="width: 100px">
                                    <asp:DataGrid ID="gbaseSMe" runat="server"
                                        Font-Size="11pt" AutoGenerateColumns="False" Font-Names="Calibri" Width="260px">
                                        <Columns>
                                            <asp:BoundColumn DataField="nombre"></asp:BoundColumn>
                                        </Columns>
                                        <HeaderStyle Font-Names="Calibri" Font-Size="10pt" Height="55px" HorizontalAlign="Center" />
                                        <ItemStyle Font-Names="Calibri" Font-Size="10.1pt" />
                                    </asp:DataGrid></td>
                                <td style="width: 100px">
                                    <div style="width:620px; overflow:auto;">
                                        <asp:DataGrid ID="gridOcupacionSMe" runat="server"
                                        Font-Size="10pt" Font-Names="Calibri">
                                            <HeaderStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" Height="55px" />
                                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        </asp:DataGrid>
                                    </div>
                                </td>
                            </tr>
                        </table>
                        <br />
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel2" runat="server" HeaderText="TabPanel2" >
                    <HeaderTemplate>
                        Médicos
                    </HeaderTemplate>
                    <ContentTemplate>
                        <asp:DataGrid ID="gMedicos" runat="server" Width="875px">
                            <ItemStyle HorizontalAlign="Center" />
                            <HeaderStyle HorizontalAlign="Center" />
                        </asp:DataGrid>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel4">
                    <ContentTemplate>
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align:left; vertical-align:top">
                            <tr>
                                <td style="height: 18px">
                                </td>
                                <td style="height: 18px">
                                </td>
                            </tr>
                            <tr valign="top" style="vertical-align:middle">
                                <td style="width:260px">
                                    <asp:DataGrid ID="gbaseEncabezadoC" runat="server" Font-Names="Calibri" Font-Size="10pt" Width="260px" AutoGenerateColumns="False">
                                        <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        <Columns>
                                            <asp:BoundColumn DataField="id_servicio"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="descripcion" HeaderText="Descripcion"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="importe"></asp:BoundColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                   
                                </td>
                                <td style="width:620px">
                                    <div style="width:620px; overflow:auto;">
                                        <asp:DataGrid ID="gEncabezadoC" runat="server" Font-Names="Calibri" Font-Size="10pt">
                                            <HeaderStyle Height="55px" HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                            <ItemStyle HorizontalAlign="Center" Font-Names="Calibri" Font-Size="10pt" />
                                        </asp:DataGrid>
                                    </div>
                                  
                                </td>
                            </tr>
                            <tr>
                                <td>
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <HeaderTemplate>
                        Cortesias
                    </HeaderTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel1">
                    <HeaderTemplate>
                        Terapias Doctores
                    </HeaderTemplate>
                    <ContentTemplate>
                        <br />
                    <asp:Label ID="lblteracp" runat="server" Font-Bold="True" forecolor="Red" Font-Size="12px" Text="FISIOCARE CAMPESTRE - TERAPIAS"></asp:Label><br />
                        <br />
                        <asp:DataGrid ID="gTerapiaDocCP" runat="server" Width="875px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <FooterStyle BackColor="White" ForeColor="Black" />
                            <SelectedItemStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid><br />
                        <asp:Label ID="lblterasm" runat="server" Font-Bold="True" forecolor="Red" Font-Size="12px" Text="FISIOCARE STARMEDICA - TERAPIAS"></asp:Label>
                        <br />
                        <br />
                        <asp:DataGrid ID="gTerapiaDocSM" runat="server" Width="875px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3">
                            <FooterStyle BackColor="White" ForeColor="Black" />
                            <SelectedItemStyle BackColor="#000066" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="#006699" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid><br />
                        <asp:Label ID="lblimpcp" runat="server" Font-Bold="True" forecolor="Red" Font-Size="12px" Text="FISIOCARE CAMPESTRE - IMPORTES EN BASE A TERAPIAS"></asp:Label>
                        <br />
                        <br />
                        <asp:DataGrid ID="gTeraImpoCP" runat="server" Width="875px" CellPadding="3" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="arial" Font-Size="Smaller">
                            <FooterStyle BackColor="White" ForeColor="Black" />
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="Teal" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" Font-Names="arial" Font-Size="Small" />
                        </asp:DataGrid><br />
                        <asp:Label ID="lblimpsm" runat="server" Font-Bold="True" forecolor="Red" Font-Size="12px" Text="FISIOCARE STARMEDICA - IMPORTES EN BASE A TERAPIAS"></asp:Label>
                        <br />
                        <br />
                        <asp:DataGrid ID="gTeraImpoSM" runat="server" Width="875px" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="3" Font-Bold="False" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="Smaller" Font-Strikeout="False" Font-Underline="False">
                            <FooterStyle BackColor="White" ForeColor="Black" />
                            <SelectedItemStyle BackColor="#669999" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Left" Mode="NumericPages" />
                            <ItemStyle ForeColor="Black" />
                            <HeaderStyle BackColor="Teal" Font-Bold="True" ForeColor="White" Font-Italic="False" Font-Names="Arial" Font-Overline="False" Font-Size="Small" Font-Strikeout="False" Font-Underline="False" />
                        </asp:DataGrid>
                    </ContentTemplate>
                    </cc1:TabPanel>
            </cc1:TabContainer><br />
        </div>
         <div visible="false" style="visibility:hidden">
            <table border="1" cellpadding="0" cellspacing="0" style="width: 50%; text-align:center">
                <tr style=" background-color:#F4F4F4">
                    <td>
                        Rango de Fechas</td>
                    <td>
            Número de días a proyectar</td>
                </tr>
                <tr>
                    <td>
            <asp:RangeValidator
                ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox ID="txtfecha1" runat="server" Width="90px"></asp:TextBox><asp:Image ID="Image1" runat="server"
            ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                <asp:RangeValidator ID="RangeValidator2"
                            runat="server" ControlToValidate="txtfecha2" ErrorMessage="*" MaximumValue="31/12/2999"
                            MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox ID="txtfecha2" runat="server" Width="90px"></asp:TextBox><asp:Image ID="Image2" runat="server"
            ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
                    <td>
            <asp:TextBox ID="txtNumdias" runat="server" Width="68px" style="text-align:center"></asp:TextBox></td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                <tr>
                    <td style="height: 86px" >
            <table border="1" cellpadding="0" cellspacing="0" style="width: 100%; text-align:center">
                <tr style=" background-color:#F4F4F4; color:#ff0000">
                    <td colspan="4">
                        DATOS CAMPESTRE</td>
                </tr >
                <tr style=" background-color:#F4F4F4">
                    <td>
                        Capacidad Maxima</td>
                    <td>
                        Meta Tx</td>
                    <td>
                        Meta $</td>
                    <td>
                        Número Terapistas</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtCapMaxima" runat="server" Width="86px" style="text-align:center">1,716</asp:TextBox></td>
                    <td >
                        <asp:TextBox ID="txtmetaTx" runat="server" Width="86px" style="text-align:center">679</asp:TextBox></td>
                    <td >
                        <asp:TextBox ID="txtmeta" runat="server" Width="86px" style="text-align:center">176,476</asp:TextBox></td>
                    <td >
                        <asp:TextBox ID="txtnumterapistas" runat="server" Width="86px" style="text-align:center">6</asp:TextBox></td>
                </tr>
            </table>
                    </td>
                    <td style="height: 86px" >
            <table border="1" cellpadding="0" cellspacing="0" style="width: 100%; text-align:center">
                <tr style=" background-color:#F4F4F4; color:#ff0000">
                    <td colspan="4">
                        DATOS STARMEDICA</td>
                </tr>
                <tr style=" background-color:#F4F4F4">
                    <td >
                        Capacidad Maxima</td>
                    <td >
                        Meta Tx</td>
                    <td>
                        Meta $</td>
                    <td >
                        Número Terapistas</td>
                </tr>
                <tr>
                    <td >
                        <asp:TextBox ID="txtcapmaximasm" runat="server" Width="86px" style="text-align:center">1,213</asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtmetaTxsm" runat="server" Width="86px" style="text-align:center">777</asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtmetasm" runat="server" Width="86px" style="text-align:center">209,162</asp:TextBox></td>
                    <td >
                        <asp:TextBox ID="txtnumterapistassm" runat="server" Width="86px" style="text-align:center">6</asp:TextBox></td>
                </tr>
            </table>
                    </td>
                </tr>
                <tr>
                    <td >
                        <table border="1" cellpadding="0" cellspacing="0" style="width: 100%; text-align:center">
                            <tr style="text-align:center">
                                <td colspan="2" style="color: #ff0000; background-color: #f4f4f4">
                                    SENIORS GYM</td>
                            </tr>
                            <tr style="text-align:center">
                                <td style="width: 55px; background-color:#F4F4F4;">
                                META $
                                </td>
                                <td style="width: 100px">
                                    <asp:TextBox ID="txtmetas" runat="server" Width="99px" style="text-align:center">21,350</asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                    <td >
                    </td>
                </tr>
            </table></div>
        <cc1:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="Image1" TargetControlID="txtfecha1">
        </cc1:CalendarExtender>
        <cc1:CalendarExtender ID="CalendarExtender2" runat="server" PopupButtonID="Image2"
            TargetControlID="txtfecha2" Format="dd/MM/yyyy">
        </cc1:CalendarExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender1" runat="server" ClearMaskOnLostFocus="False"
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha2" PromptCharacter=" ">
        </cc1:MaskedEditExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
            TargetControlID="txtNumdias">
        </cc1:FilteredTextBoxExtender>
        <cc1:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="False"
            Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha1" PromptCharacter=" ">
        </cc1:MaskedEditExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender9" runat="server"
            TargetControlID="txtCapMaxima" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
            TargetControlID="txtcapmaximasm" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
            TargetControlID="txtmeta" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender5" runat="server"
            TargetControlID="txtmetasm" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender6" runat="server"
            TargetControlID="txtmetaTx" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender7" runat="server"
            TargetControlID="txtmetaTxsm" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender8" runat="server"
            TargetControlID="txtnumterapistas" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
            TargetControlID="txtnumterapistassm" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender><cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender10" runat="server"
            TargetControlID="txtmetas" ValidChars="1234567890,">
        </cc1:FilteredTextBoxExtender>
        <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
        </div>
    </form>
    </center>
</body>
</html>
