<%@ Page Language="VB" AutoEventWireup="false" CodeFile="terapistas.aspx.vb" Inherits="terapistas" %>
<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Adminsitración Fisiocare</title>
</head>
<body><center>
    <form id="form1" runat="server">
     <div style="width:900px; height:860px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner1.png" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
            BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
            Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773" Orientation="Horizontal"
            Width="900px">
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
         <br />
        <table border="1" cellpadding="4" runat="server" id="tabla" cellspacing="0" style="width: 895px; text-align: left; border-width:1px; border-color:#efefef; border-style:solid">
            <tr>
                <td style="color: #1b3774; background-color: #f4f4f4; width:400px">
                    SUCURSAL</td>
                <td rowspan="4" style="text-align:center" align="center" >
                    <asp:Panel ID="Panel2" runat="server" Visible="False" Width="450px">
                        &nbsp;<br />
                        <table border="0" cellpadding="2" cellspacing="0" style="width: 450px">
                            <tr style="text-align:left">
                                <td >
                                    Nombre<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombreT"
                            ErrorMessage="*" ValidationGroup="g"></asp:RequiredFieldValidator></td>
                                <td>
                        <asp:TextBox ID="txtNombreT" runat="server" Width="330px"></asp:TextBox></td>
                            </tr>
                            <tr style="text-align:left">
                                <td>
                                    Password<asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtpass"
                                        ErrorMessage="*" ValidationGroup="g"></asp:RequiredFieldValidator></td>
                                <td>
                                    <asp:TextBox ID="txtpass" runat="server" TextMode="Password"></asp:TextBox></td>
                            </tr>
                            <tr style="text-align:left">
                                <td>
                                    Confirma Pass</td>
                                <td>
                                    <asp:TextBox ID="txtpass2" runat="server" TextMode="Password"></asp:TextBox>
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtpass"
                                        ControlToValidate="txtpass2" ErrorMessage="No coinciden los pass" ValidationGroup="g"></asp:CompareValidator></td>
                            </tr>
                            <tr style="text-align:left">
                                <td>
                                    Activo</td>
                                <td>
                        <asp:DropDownList ID="cmbActivo" runat="server" Width="47px" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="True">SI</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:DropDownList></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <asp:Button ID="Button3" runat="server" Text="Modificar" Width="188px" ValidationGroup="g" /></asp:Panel>
                    &nbsp;
                    <input id="idTerapista" runat="server" style="width: 29px" type="hidden" />
                    <input id="sucTerapista" runat="server" style="width: 29px" type="hidden" /></td>
            </tr>
            <tr>
                <td style="width:400px">
        <asp:DropDownList ID="cmbsucursal" runat="server" AutoPostBack="True" Width="257px" Font-Names="Calibri" Font-Size="11pt">
            <asp:ListItem Value="0">---Seleccione---</asp:ListItem>
            <asp:ListItem Value="fisiocarecp">CAMPESTRE</asp:ListItem>
            <asp:ListItem Value="fisiocaresm">STAR MEDICA</asp:ListItem>
        </asp:DropDownList>
                    <asp:Button ID="btnnuevo" runat="server" Text="Agregar Terapista" Visible="False"
                        Width="139px" /></td>
            </tr>
            <tr>
                <td style="color: #1b3774; background-color: #f4f4f4; width:400px">
                    Terapistas</td>
            </tr>
            <tr>
                <td style="width:400px; vertical-align:top">
        <asp:DropDownList ID="cmbTerapistas" runat="server" AutoPostBack="True" Width="400px" Font-Names="Calibri" Font-Size="11pt">
        </asp:DropDownList></td>
            </tr>
        </table>
        &nbsp;&nbsp;<cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
                    <br />
    
    <asp:Panel ID="Panel1" runat="server" style="text-align:center" Visible="False" Width="450px">
        <asp:Label ID="lblsuc" runat="server" BackColor="#F4F4F4" ForeColor="#1B3774" Width="455px"></asp:Label><br />
        &nbsp;<table border="0" cellpadding="2" cellspacing="0" style="width: 450px">
            <tr style="text-align:left">
                <td >
                    Nombre<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtNombre"
                        ErrorMessage="*" ValidationGroup="n"></asp:RequiredFieldValidator></td>
                <td style="color: #000000">
                    <asp:TextBox ID="txtNombre" runat="server" Width="330px"></asp:TextBox></td>
            </tr>
            <tr style="text-align:left; color: #000000;">
                <td>
                    Password<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtPassN"
                        ErrorMessage="*" ValidationGroup="n"></asp:RequiredFieldValidator></td>
                <td>
                    <asp:TextBox ID="txtPassN" runat="server" TextMode="Password"></asp:TextBox></td>
            </tr>
            <tr style="text-align:left">
                <td>
                    Confirma Pass</td>
                <td>
                    <asp:TextBox ID="txtPass2N" runat="server" TextMode="Password"></asp:TextBox>
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToCompare="txtPassN"
                        ControlToValidate="txtPass2N" ErrorMessage="No coinciden los pass"
                        ValidationGroup="n"></asp:CompareValidator></td>
            </tr>
        </table>
                        <br />
                        <asp:Button ID="Button1" runat="server" Text="Agregar Terpista" Width="140px" ValidationGroup="n" />
        &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Button ID="Button2" runat="server" Text="Terminar" Width="140px" /></asp:Panel>
    
    </div>
    </form></center>
</body>
</html>
