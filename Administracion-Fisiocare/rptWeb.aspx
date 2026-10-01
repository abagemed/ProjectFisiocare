<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptWeb.aspx.vb" Inherits="rptWeb" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>...::Reportes - Hospital de Ortopedia::...</title>
</head>
<body>
<center>
    <form id="form1" runat="server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
                EnableScriptLocalization="True">
            </asp:ScriptManager>
      <div style="width:900px; border: solid 1px #ff0000; text-align:center">
        <img src="imagenes/baner1.png" /><br />
        <asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
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
                <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
                </asp:MenuItem>
            </asp:MenuItem>
            <asp:MenuItem Text="" Value="">
            </asp:MenuItem>
        </Items>
    </asp:Menu>
    <table style="width: 900px">
            <tr>
                <td>
                <br />
                <br />
                    <asp:Label ID="lbReporte" runat="server" Text="TIPO DE REPORTE:" Font-Bold="True" Font-Size="Small"></asp:Label>
                    <asp:DropDownList ID="cboReporte" runat="server" Width="350px" AutoPostBack="True" Font-Size="Medium">
                    </asp:DropDownList><br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cboReporte"
                        ErrorMessage="No!, a seleccionado una opcion" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>
                    <br />
                    <div id="divFecha" runat="server" visible="false">
                        <asp:Label ID="lbInicio" runat="server" Text="INICIO: " Font-Size="Small"></asp:Label>
                        <asp:Image ID="micalendario" runat="server" src="imagenes/Calendar_scheduleHS.png"
                            Visible="true" />
       <cc2:CalendarExtender ID= "fechacita"  runat ="server" PopupButtonID = "micalendario" TargetControlID ="txtinicio"  Format="dd/MM/yyyy" ></cc2:CalendarExtender>
                        <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="72px"></asp:TextBox>
                        
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp;<asp:Label ID="lbFinal" runat="server"
                            Text="FINAL:" Font-Size="Small"></asp:Label>
                        <asp:Image ID="micalendario2" runat="server" src="imagenes/Calendar_scheduleHS.png" Visible="true" />
                        <cc2:CalendarExtender ID= "fechacita2"  runat ="server" PopupButtonID = "micalendario2" TargetControlID ="txtfinal"  Format="dd/MM/yyyy" ></cc2:CalendarExtender>  
                        <asp:TextBox ID="txtFinal" runat="server" Width="72px"></asp:TextBox><br />
                        <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Formato de fecha:dd/mm/yyy" BackColor="WhiteSmoke" Font-Bold="False"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                    </div><br />
                    <div id="divFiltro1" runat="server" visible="false">
                        <asp:Label ID="lbFiltro1" runat="server" Text="FILTRO 1:" Font-Size="Small"></asp:Label>
                        <asp:DropDownList ID="cboFiltro1" runat="server" Width="350px">
                        </asp:DropDownList>
                        </div>
                        <br />
                    <div id="divFiltro2" runat="server" visible="false">
                        <asp:Label ID="lbFiltro2" runat="server" Text="FILTRO 2: " Font-Size="Small"></asp:Label>
                        <asp:DropDownList ID="cboFiltro2" runat="server" Width="350px">
                        </asp:DropDownList>
                    </div>
                    <br />
                        <asp:Button ID="cmGenerar" runat="server" Text="GENERAR REPORTE" />
                    <asp:Button ID="cmdImprimir" runat="server" Text="IMPRIMIR" Visible="False" />
                    <asp:Button ID="cmdexcel" runat="server" Text="EXCEL" Visible="False" /></td>
            </tr>
        </table>
            <asp:HiddenField ID="hfEmpleado" runat="server" />
                            <asp:HiddenField ID="hfusuario" runat="server" />
                            <asp:HiddenField ID="hfIddoctor" runat="server" />
                            <asp:Label ID="lblfecha" runat="server" Visible="False"></asp:Label>
        </div>
    <br />
        <div id="divRpt" style="width:auto" runat="server" visible="false" align="center">
            <asp:GridView ID="gvRpt" runat="server" BorderStyle="Solid"
             BorderWidth="1px" CellPadding="4" Font-Size="Small">
                <RowStyle BackColor="#F0E68C" HorizontalAlign="Left" />
                <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="#FF7F50" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#2461BF" />
                <AlternatingRowStyle BackColor="White" />
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td style=" width:auto; text-align:center; font-weight: bold; background-color: #F0E68C">
                                DESCRIPCION</td>
                        </tr>
                        <tr>
                            <td style="width:auto; color: red; font-weight: bold;">
                                ....::: LO SIENTO!, NO EXISTEN DATOS :::....</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
            &nbsp;
        </div>
    </form>    
</center>
</body>
</html>
