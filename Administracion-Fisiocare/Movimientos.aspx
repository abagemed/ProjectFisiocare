<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Movimientos.aspx.vb" Inherits="Movimientos" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
    <title>...:: Movimientos Bancarios ::...</title>
     <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
     <script type="text/javascript" src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js'></script>
    
</head>
<body style="font-family:Calibri; font-size:12pt"><center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; text-align:center">
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
                <asp:MenuItem NavigateUrl="~/consultastxf.aspx" Text="Consultas  X Facturar" Value="consultastxf.aspx">
                    </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
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
        <cc1:toolkitscriptmanager id="ToolkitScriptManager1" runat="server"></cc1:toolkitscriptmanager>
        <br />
        <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4">
                    <asp:Label ID="Label6" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Fecha" Width="60px"></asp:Label></td>
                <td style="width: 603px; text-align: left">
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
                        <tr>
                            <td style="width: 165px">
                            <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                    <asp:TextBox ID="txtfecha" runat="server" style="text-align:center; border-bottom: #e0e0e0 2px solid" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="81px"></asp:TextBox>
                    <asp:RangeValidator ID="validaFecha" runat="server" ErrorMessage="*" MinimumValue="01/01/2000"
                        Type="Date" ControlToValidate="txtfecha"></asp:RangeValidator>
                       <%-- <asp:CompareValidator ID="CompareValidator5" runat="server" ControlToValidate="txtfecha"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator>--%>
                        <%--<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtfecha"
                        ErrorMessage="*"></asp:RequiredFieldValidator>--%></td>
                            <td style="width: 67px; background-color:#f4f4f4"><asp:Label ID="Label8" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                    Text="Importe Dep." Width="100px"></asp:Label>
                                </td>
                            <td style="width: 430px">
                                &nbsp;<asp:TextBox ID="Txtidep" runat="server" style="text-align:center; border-bottom: #e0e0e0 2px solid" AutoPostBack="True" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px" Width="130px" Font-Bold="True" Font-Size="16pt"  >0</asp:TextBox>
                                <%--<asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtidep"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%></td>
                        </tr>
                    </table>
                </td>
                <td colspan="2" style="text-align: center">
                <asp:Label ID="lblfolioingreso" runat="server"
                 Font-Bold="True" Font-Italic="False" Font-Names="Trebuchet MS" Font-Size="14pt" ForeColor="#000066"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4">
                    <asp:Label ID="Label3" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Referencia" Width="80px"></asp:Label>
                    </td>
                <td style="width: 603px; text-align: left">
                  <asp:TextBox ID="txtReferencia" style="text-align:center; border-bottom: #e0e0e0 2px solid" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"  Font-Bold="True" ForeColor="#C00000"></asp:TextBox>
                    
                    <asp:Button ID="Btnbusfol" runat="server" Text="Buscar FR"  CssClass="boton" ForeColor="Red"/>
                    <%--<asp:Button ID="ButtonAD" runat="server" CommandName="cmdAD" CssClass="boton"
        Font-Bold="False" Font-Names="Calibri" Font-Size="9pt" ForeColor="#000040" Text="H.Diagnosticos" />--%>
                    </td>
                <td style="text-align:center; color:#1A3773; background-color:#f4f4f4" colspan="2">
                    DISPONIBLE DEL DEPOSITO</td>
            </tr>
            <tr>
                <td style="text-align:left; width: 88px; background-color:#f4f4f4">
                    <asp:Label ID="Label15" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Sucursal" Width="80px"></asp:Label>
                    </td>
                <td style="width: 603px; text-align: left">
                    <asp:DropDownList ID="cmbSucursal" runat="server" AutoPostBack="True" Font-Names="Calibri" Font-Size="12pt"
            Width="266px">
                        <asp:ListItem Value="0">--Sucursal--</asp:ListItem>
                        <asp:ListItem Value="fisiocareSM">Star Medica</asp:ListItem>
                        <asp:ListItem Value="fisiocareCP">Campestre</asp:ListItem>
                        <asp:ListItem Value="fisiocareHO">HO</asp:ListItem>
                        <asp:ListItem Value="fisiocareAM">Amerimed</asp:ListItem>
                        <asp:ListItem Value="fisiocareCA">Anticanceroso</asp:ListItem>
                        <asp:ListItem Value="Gym">Gym</asp:ListItem>
                    </asp:DropDownList>
                    <%--<asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbSucursal"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%>
                   </td>
                <td style="text-align:center" colspan="2">
                    <asp:Label ID="lblAuximporte" runat="server" Font-Names="calibri" Font-Size="16pt" Font-Bold="True" ForeColor="#C00000"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4">
                    <asp:Label ID="Label2" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Tipo" Width="80px"></asp:Label></td>
                <td style="width: 603px; text-align:left">
                    <asp:DropDownList ID="cmbTipo" runat="server" Font-Names="Calibri" Font-Size="12pt"
            Width="266px">
                    </asp:DropDownList>
                    <%--<asp:CompareValidator ID="CompareValidator3" runat="server" ControlToValidate="cmbTipo"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%></td>
                <td style="width: 65px; text-align:left">
                    </td>
                <td style="width: 244px">
                    <asp:Label ID="tempPendiente" runat="server" Visible="False">0</asp:Label>
                    <asp:Label ID="tempImporte" runat="server" Visible="False">0</asp:Label></td>
            </tr>
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4">
                    
                    <asp:Label ID="Label4" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Observa." Width="80px"></asp:Label>
                    </td>
                <td colspan="3" style="text-align: left">
                    <asp:TextBox ID="txtobserva" Style="border-bottom: #e0e0e0 2px solid" runat="server" Width="600px"  BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    
                </td>
            </tr>
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4; height: 33px;">
                    
                    <asp:Label ID="Label1" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Cuenta" Width="80px"></asp:Label>
                    </td>
                <td colspan="3" style="text-align: left; height: 33px;">
                    <asp:DropDownList ID="cmbCuentas" runat="server" Font-Names="Calibri" Font-Size="12pt"
            Width="266px" AutoPostBack="True">
                    </asp:DropDownList>
                    <%--<asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="cmbCuentas"
                        ErrorMessage="*" Operator="NotEqual" ValueToCompare="0"></asp:CompareValidator>--%>
                    </td>
            </tr>
            <tr>
                <td style="width: 88px; text-align: left; background-color:#f4f4f4">
                    <asp:Label ID="Label5" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="No. factura" Width="80px"></asp:Label></td>
                <td style="width: 603px; text-align: left">
                <asp:TextBox ID="txtNumFac" Style="border-bottom: #e0e0e0 2px solid; text-align:center" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    </td>
                <td style="width: 100px;">
                    </td>
                <td style="width: 65px;">
               
                    </td>
            </tr>
            </table>
            <table border="0" cellpadding="5" cellspacing="0" style="width: 100%">
            <tr>
            <td style="width: 180px; text-align: Center">
             <asp:Label ID="Label7" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Rango de Fecha" Width="180px"></asp:Label>
            </td>
            <td style="width: 300px; text-align: left">
            <asp:Label ID="Label9" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Clientes" Width="300px"></asp:Label>
            </td>
            <td style="width: 100px; text-align: left">
            
            </td>
            </tr>
            <tr>
            <td style="width: 180px; text-align: left">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="txtfecha1"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator1" runat="server" ControlToValidate="txtfecha1" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha1" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:Image ID="Image2" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtfecha2"
                        ErrorMessage="*" Operator="DataTypeCheck" Type="Date"></asp:CompareValidator><asp:RangeValidator
                            ID="RangeValidator2" runat="server" ControlToValidate="txtfecha2" ErrorMessage="*"
                            MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date" ValidationGroup="val"></asp:RangeValidator><asp:TextBox
                                ID="txtfecha2" runat="server" Width="90px" style="text-align:center; border-bottom: #e0e0e0 2px solid" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox>
                    <asp:Image ID="Image3" runat="server"
                                    ImageUrl="~/imagenes/Calendar_scheduleHS.png" />
                                    <cc1:calendarextender
                                    id="CalendarExtender2" runat="server" format="dd/MM/yyyy" popupbuttonid="Image2"
                                    targetcontrolid="txtfecha1">
        </cc1:calendarextender><cc1:calendarextender id="CalendarExtender3" runat="server"
            format="dd/MM/yyyy" popupbuttonid="Image3" targetcontrolid="txtfecha2">
        </cc1:calendarextender><cc1:maskededitextender id="MaskedEditExtender2" runat="server"
            clearmaskonlostfocus="False" mask="99/99/9999" masktype="Date" promptcharacter=" "
            targetcontrolid="txtfecha2">
        </cc1:maskededitextender><cc1:maskededitextender id="MaskedEditExtender3" runat="server"
            clearmaskonlostfocus="False" mask="99/99/9999" masktype="Date" promptcharacter=" "
            targetcontrolid="txtfecha1">
        </cc1:maskededitextender>
                                    </td>
                                    <td style="width: 300px; text-align: left">
                                    <asp:DropDownList ID="cmbclientes" runat="server" AutoPostBack="True" Width="300px" Font-Names="Calibri" Font-Size="9pt">
                                    </asp:DropDownList>
                                    </td>
                                    <td style="width: 100px; text-align: left">
                                     <asp:Button ID="btnAgregarf" runat="server" Text="BuscarFactura" CssClass="boton" ForeColor="Red" Font-Size="9pt"/>
                                    </td>
                                    </tr>
                                    </table>
          
           <table border="0" cellpadding="4" cellspacing="0" style="width: 100%">
            <tr>
            <td colspan="4" style="text-align: left">
        <asp:DataGrid ID="gridFacturas" runat="server" Width="900px" DataKeyField="num_factura" Font-Size="Small" AutoGenerateColumns="False" BackColor="White">
                    <AlternatingItemStyle BackColor="#E0E9F2" />
                    <Columns>
                        <asp:TemplateColumn HeaderText="Factura">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtfactura"   Font-Size="Small" Enabled="false" runat="server" Text='<%# Bind("num_factura") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lfactura"   Font-Size="Small" runat="server" Text='<%# Bind("num_factura") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Serie">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtserie"  Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("serie") %>' Width="40px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lserie" runat="server" Text='<%# Bind("serie") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Fecha">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtfecha" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("fecha") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lfecha" runat="server" Text='<%# Bind("fecha") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Nombre">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtnombre" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("nombre") %>' Width="120px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lnombre" runat="server" Text='<%# Bind("nombre") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Rfc">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtrfc" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("rfc") %>' Width="100px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lrfc" runat="server" Text='<%# Bind("rfc") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Status">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtstatus" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("status") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lstatus" runat="server" Text='<%# Bind("status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Importe">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtimporte" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("importe") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="limporte" runat="server" Text='<%# Bind("importe") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="Abonado">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtabonado" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("abonado") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="labonado" runat="server" Text='<%# Bind("abonado") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="adeuda">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtadeuda" Font-Size="X-Small" Enabled="false" runat="server" Text='<%# Bind("adeuda") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="ladeuda" runat="server" Text='<%# Bind("adeuda") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="XAbonar">
                            <EditItemTemplate>
                                <asp:TextBox ID="txtxabonar"  BackColor="AliceBlue"  Font-Bold="true" runat="server" Text='<%# Bind("adeuda") %>' Width="60px"></asp:TextBox>
                            </EditItemTemplate>
                            <ItemTemplate>
                                <asp:Label ID="lxabonar" runat="server" Text='<%# Bind("adeuda") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Left"  BackColor="LightYellow" />
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemTemplate>
                                <asp:LinkButton ID="LbtnAbonar" runat="server">Abonar</asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <HeaderStyle BackColor="#6791C0" ForeColor="White"  Font-Size="Small" />
                </asp:DataGrid>
        
        
        
        <input id="hfSucursal" runat="server" type="hidden" />
        <input id="hfclientes" runat="server" type="hidden" />
        
            </td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center; background:#c4c4c4; color:#1A3773; font-weight:bold;">
              Facturas Conciliadas Ahora    
                </td> </tr> 
            <tr>
                <td colspan="4" style="text-align: left">
                    <br />
                    <asp:DataGrid ID="gFacturas" runat="server" Width="890px" AutoGenerateColumns="False">
                        <Columns>
                           <asp:BoundColumn DataField="columna0" HeaderText="#Fac"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna1" HeaderText="Serie"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna2" HeaderText="Fecha"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna3" HeaderText="R.Social"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna4" HeaderText="RFC"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna5" HeaderText="Pendiente"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna6" HeaderText="Abono"></asp:BoundColumn>
                            <asp:BoundColumn DataField="columna7" HeaderText="Movimiento"></asp:BoundColumn>
                            <asp:TemplateColumn>
                                <ItemTemplate>
                                    <asp:Button ID="Button12" runat="server" CssClass="boton" Font-Bold="True" Font-Size="14pt"
                                        ForeColor="Red" Text="   X   " />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns><ItemStyle HorizontalAlign="Center" /><HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" ForeColor="#1A3773" />
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td colspan="4" style="text-align:center; background:#c4c4c4; color:#1A3773; font-weight:bold;">
              Facturas Conciliadas Anteriormente    
                </td> </tr> 
             <tr>
                <td colspan="4" style="text-align: left">
                    <br />
                    <asp:DataGrid ID="DgFacturasCN" runat="server" Width="890px" AutoGenerateColumns="False">
                        <Columns>
                        <asp:BoundColumn DataField="idCF" HeaderText="CF" Visible="false"></asp:BoundColumn>
                        <asp:BoundColumn DataField="folioIngreso" HeaderText="Folio"></asp:BoundColumn>
                        <asp:BoundColumn DataField="folioref" HeaderText="Referencia"></asp:BoundColumn>
                            <asp:BoundColumn DataField="numfactura" HeaderText="Factura."></asp:BoundColumn>
                            <asp:BoundColumn DataField="Serie" HeaderText="Serie"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Fecha" HeaderText="Fecha Conc."></asp:BoundColumn>
                            <asp:BoundColumn DataField="razonsocial" HeaderText="R. Social"></asp:BoundColumn>
                            <asp:BoundColumn DataField="TotalAbonado" HeaderText="Total Fact"></asp:BoundColumn>
                            <asp:BoundColumn DataField="Movimiento" HeaderText="Mov.Factura"></asp:BoundColumn>
                            <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="cmdEditarT" runat="server" CommandName="cmdEditarT" CssClass="boton"
                            Font-Bold="False" Font-Names="Calibri" Font-Size="14pt" ForeColor="Red" Text="X" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                            <%--
                            <asp:BoundColumn DataField="Asignado" HeaderText="Abonado"></asp:BoundColumn>
                            <asp:BoundColumn DataField="SaldoFactura" HeaderText="Saldo Fact"></asp:BoundColumn>--%>
                            
                            </Columns><ItemStyle HorizontalAlign="Center" /><HeaderStyle HorizontalAlign="Center" BackColor="#F4F4F4" ForeColor="#1A3773" />
                    </asp:DataGrid></td>
            </tr>
        </table>
        
        <cc1:calendarextender id="CalendarExtender1" runat="server" format="dd/MM/yyyy"
            popupbuttonid="Image1" targetcontrolid="txtfecha"> </cc1:calendarextender>
        <cc1:maskededitextender id="MaskedEditExtender1" runat="server" mask="99/99/9999"
            masktype="Date" targetcontrolid="txtfecha"></cc1:maskededitextender>
                    <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
                    <input id="hfStatus" runat="server" type="hidden" />
                  
                    <input id="hfBandera" runat="server" type="hidden" />
                    <input id="hfsaldo" runat="server" type="hidden" />
                    &nbsp;
                    <cc1:ModalPopupExtender ID="ModalPopupExtenderfolio" runat="server" BackgroundCssClass="FondoAplicacion"
          PopupControlID="Panelfolio" TargetControlID="hfStatus">
     </cc1:ModalPopupExtender>
                  
        <br />
                    <asp:Button ID="Button2" runat="server" Text="Aceptar" Width="159px" CssClass="boton" ForeColor="Red" ValidationGroup="gDatos2" BackColor="CornflowerBlue" BorderColor="Maroon" Font-Bold="True"/>
                    
                    
                    <asp:Panel ID="Panelfolio" runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid"
        BorderWidth="2px" Width="750px">
        <div style="padding-right: 0px; padding-left: 0px; font-size: 15pt; padding-bottom: 3px;
            color: white; padding-top: 3px; border-bottom: white 2px solid; font-family: Calibri; background-color: #6891c0; text-align: center;">
            FOLIOS DE REFENCIAS BANCARIAS -------------------------
            <asp:LinkButton ID="LinkButton9" runat="server" Font-Names="Calibri" Font-Size="15pt"
                                                ForeColor="Firebrick" Font-Bold="False">Salir</asp:LinkButton>
            </div>
            <div style="overflow:auto; height:300px; width:725px; padding-bottom: 0px; vertical-align:middle ">
        <asp:DataGrid ID="gridfolio" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="Silver" BorderWidth="1px" CellPadding="4" CellSpacing="1" DataKeyField="folioingreso"
            Font-Names="Calibri" Font-Size="11pt" ForeColor="Black" Font-Bold="true" GridLines="None" Width="700px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="WhiteSmoke" />
            <Columns>
<asp:BoundColumn DataField="FolioIngreso" HeaderText="FolioI" ></asp:BoundColumn>
<asp:BoundColumn DataField="folioRef" HeaderText="Referencia" ></asp:BoundColumn>
<asp:BoundColumn DataField="codigocuentabancaria" HeaderText="Cuenta"></asp:BoundColumn>
<asp:BoundColumn DataField="importe" HeaderText="Importe"></asp:BoundColumn>
<asp:BoundColumn DataField="saldo" HeaderText="Saldo"></asp:BoundColumn>
<asp:BoundColumn DataField="tipoPago" HeaderText="TPago"></asp:BoundColumn>
<asp:BoundColumn DataField="fechaoperacion" HeaderText="Fecha"></asp:BoundColumn>
<asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="cmdefolio" runat="server" CommandName="cmdEditar" CssClass="boton"
                            Font-Bold="False" Font-Names="Calibri" Font-Size="12pt" ForeColor="#000040" Text="Aplicar"  />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="updatefolio" runat="server" CommandName="updatecf" CssClass="boton"
                            Font-Bold="False" Font-Names="Calibri" Font-Size="12pt" ForeColor="#000040" Text="Regresar $"  Visible="false" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
                
</Columns>
            <HeaderStyle BackColor="#6891C0" Font-Bold="False" ForeColor="White" />
        </asp:DataGrid>
        </div>
       </asp:Panel>
                    </div>
                    <%--<input id="hfIdusuario" runat="server" style="width: 41px" type="hidden" />--%>
                    
    </form>
</center>
</body>
</html>