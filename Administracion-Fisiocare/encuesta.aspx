<%@ Page Language="VB" AutoEventWireup="false" CodeFile="encuesta.aspx.vb" Inherits="encuesta" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
     <title>Adminsitración Fisiocare</title>
     <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; height:1200px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner1.png" />
        <asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4" BorderColor="DimGray" BorderStyle="Solid"
            BorderWidth="1px" Font-Bold="True" Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773"
            Orientation="Horizontal" Width="900px">
            <StaticMenuItemStyle VerticalPadding="3px" Width="100px" />
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <DynamicMenuItemStyle BackColor="#F4F4F4" BorderColor="White" BorderStyle="Solid"
                BorderWidth="1px" HorizontalPadding="3px" VerticalPadding="4px" Width="160px" />
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
                    <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Reportes" Value="Reportes">
                    <asp:MenuItem NavigateUrl="~/consultasterapistas.aspx" Text="Agenda-Terapeuta" Value="Agenda-Terapistas">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultaspacientes.aspx" Text="Gestion de Pacientes" Value="Gestion de Pacientes">
                    </asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
        &nbsp;&nbsp;
        <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:center">
            <tr>
                <td style="width: 110px; background-color:#f4f4f4; color:#1a3773">
                    Sucursal</td>
                <td style="width: 325px;  background-color:#f4f4f4; color:#1a3773">
                    Rango de Fechas</td>
                <td style="width: 100px;">
                </td>
                <td colspan="2" rowspan="2" style="background-color:#f4f4f4">
                    <asp:Button ID="Button1" runat="server" Text="Buscar" CssClass="boton" ForeColor="Red" Width="128px" ValidationGroup="val" /></td>
            </tr>
            <tr>
                <td style="width: 110px;">
        <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True">
            <asp:ListItem Value="fisiocaresm">Fisiocare Star</asp:ListItem>
            <asp:ListItem Value="fisiocarecp">Fisiocare Campestre</asp:ListItem>
        </asp:DropDownList></td>
                <td style="width: 325px;">
                    &nbsp;<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
            ErrorMessage="*" Operator="DataTypeCheck" Type="Date" ValidationGroup="val"></asp:CompareValidator><asp:RangeValidator
                ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                    ID="txtfecha1" runat="server" Width="90px"></asp:TextBox><asp:Image ID="Image1" runat="server"
                        ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
        <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
            ErrorMessage="*" Operator="DataTypeCheck" Type="Date" ValidationGroup="val"></asp:CompareValidator><asp:RangeValidator
                ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                    ID="txtfecha2" runat="server" Width="90px"></asp:TextBox><asp:Image ID="Image2" runat="server"
                        ImageUrl="~/imagenes/Calendar_scheduleHS.png" /></td>
                <td style="width: 100px; ">
                </td>
            </tr>
        </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <br />
        <table border="0" cellpadding="4" cellspacing="0" style="width:100%">
            <tr>
                <td style="width: 135px; text-align:left; background-color:#f4f4f4; color:#1a3773">
                    Total de Encuestados</td>
                <td style="width: 676px; text-align:left">
                    <asp:TextBox ID="txtTotalencuesta" runat="server" Width="83px" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>
        <table border="0" cellpadding="0" cellspacing="0" style="width: 900px; text-align:left">
            <tr>
                <td style="width: 200px; vertical-align:top;  padding-top:4px">
                    <asp:Label ID="Label1" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="¿Cuanto espera regularmente para que realizen su terapia?" Width="385px" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    <asp:DataGrid ID="gPregunta1" runat="server" AutoGenerateColumns="False" Width="450px" CellPadding="2">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="De 0 - 10 min."></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="De 11 - 20 min."></asp:BoundColumn>
                            <asp:BoundColumn DataField="op3" HeaderText="De 21 - 30 min."></asp:BoundColumn>
                            <asp:BoundColumn DataField="op4" HeaderText="De 31 - 60 min"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op5" HeaderText="Mas de 1 Hr."></asp:BoundColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle BackColor="#F4F4F4" Font-Size="10pt" ForeColor="#bf3320" Font-Bold="True" HorizontalAlign="Center" />
                    </asp:DataGrid></td>
                
            </tr>
            
            <tr>
            <td></td>
                <td style="width: 200px; vertical-align:top;  padding-top:4px">
                <asp:Label ID="Label7" runat="server" Font-Bold="True"  ForeColor="#bf3320" Height="20px"
                        Text="(10-9) Excelente - (8) Bueno - (7-6) Regular - (5-1) Malo" Width="385px" Font-Size="Small"></asp:Label>
            </td>
            </tr>
            <tr>
                <td style="width: 200px; vertical-align:top;  padding-top:4px">
                    <asp:Label ID="Label2" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="¿Como considera la atención de y presentacion de las terapistas?" Width="385px" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    <asp:DataGrid ID="gPregunta2" runat="server" AutoGenerateColumns="False" Width="450px">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="10"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="9"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op3" HeaderText="8"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op4" HeaderText="7"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op5" HeaderText="6"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op6" HeaderText="5"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op7" HeaderText="4"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op8" HeaderText="3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op9" HeaderText="2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op10" HeaderText="1"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
            <tr>
                <td style="width: 200px; vertical-align:top;  padding-top:4px">
                    <asp:Label ID="Label3" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="¿Como percibe la limpieza del area?" Width="385px" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    <asp:DataGrid ID="gPregunta3" runat="server" AutoGenerateColumns="False" Width="450px">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="10"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="9"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op3" HeaderText="8"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op4" HeaderText="7"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op5" HeaderText="6"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op6" HeaderText="5"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op7" HeaderText="4"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op8" HeaderText="3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op9" HeaderText="2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op10" HeaderText="1"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
            <tr>
                <td style="width: 200px; vertical-align:top; padding-top:4px">
                    <asp:Label ID="Label4" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="¿Como percibe las instalaciones y los equipos con los que contamos?" Width="385px" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    <asp:DataGrid ID="gPregunta4" runat="server" AutoGenerateColumns="False" Width="450px">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="10"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="9"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op3" HeaderText="8"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op4" HeaderText="7"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op5" HeaderText="6"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op6" HeaderText="5"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op7" HeaderText="4"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op8" HeaderText="3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op9" HeaderText="2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op10" HeaderText="1"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
            <tr>
                <td style="width: 200px; vertical-align:top; padding-top:4px">
                    <asp:Label ID="Label5" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="¿Como calificaria la experiencia en general de fisiocare?" Width="385px" Font-Size="Small"></asp:Label>
                    </td>
                    <td>
                    <asp:DataGrid ID="gPregunta5" runat="server" AutoGenerateColumns="False" Width="450px">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="10"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="9"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op3" HeaderText="8"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op4" HeaderText="7"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op5" HeaderText="6"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op6" HeaderText="5"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op7" HeaderText="4"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op8" HeaderText="3"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op9" HeaderText="2"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op10" HeaderText="1"></asp:BoundColumn>
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
            <tr>
                <td style="width: 200px; vertical-align:top;padding-top:4px">
                    <asp:Label ID="Label9" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="Recomendarias a Fisiocare a un familiar o amigo?" Width="385px" Font-Size="Small"></asp:Label>
                        </td>
                    <td>
                        <asp:DataGrid ID="gPregunta7" runat="server" AutoGenerateColumns="False" Width="150px">
                        <Columns>
                            <asp:BoundColumn DataField="op1" HeaderText="SI"></asp:BoundColumn>
                            <asp:BoundColumn DataField="op2" HeaderText="NO"></asp:BoundColumn>    
                        </Columns>
                        <ItemStyle HorizontalAlign="Center" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align:left">
            <tr>
                <td style="width: 100%; vertical-align:top;padding-top:4px">
                    <asp:Label ID="Label6" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Height="20px"
                        Text="Comentarios" Width="300px" Font-Size="Small"></asp:Label>
                    <asp:DataGrid ID="gPregunta6" runat="server" AutoGenerateColumns="False" Width="900px">
                        <Columns>
                            <asp:BoundColumn DataField="idpaciente" HeaderText="IDPac."></asp:BoundColumn>
                            <asp:BoundColumn DataField="paciente" HeaderText="Paciente"></asp:BoundColumn>
                            <asp:BoundColumn DataField="correo" HeaderText="E-Mail"></asp:BoundColumn>
                            <asp:BoundColumn DataField="pregunta6" HeaderText="Comentarios"></asp:BoundColumn>
                            
                        </Columns>
                        <ItemStyle HorizontalAlign="left" />
                        <HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" Font-Bold="True" ForeColor="#bf3320" />
                    </asp:DataGrid></td>
                
            </tr>
        </table>
        <cc1:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy" popupbuttonid="Image1"
            targetcontrolid="txtfecha1"> </cc1:calendarextender>
        <cc1:calendarextender id="CalendarExtender2" runat="server" format="dd/MM/yyyy" popupbuttonid="Image2"
            targetcontrolid="txtfecha2"></cc1:calendarextender>
        <cc1:maskededitextender id="MaskedEditExtender1" runat="server" clearmaskonlostfocus="False"
            mask="99/99/9999" masktype="Date" promptcharacter=" " targetcontrolid="txtfecha2"></cc1:maskededitextender>
        <cc1:maskededitextender id="MaskedEditExtender2" runat="server" clearmaskonlostfocus="False"
            mask="99/99/9999" masktype="Date" promptcharacter=" " targetcontrolid="txtfecha1"></cc1:maskededitextender>
        <br />
            </div>
    </form>
</center>
</body>
</html>
