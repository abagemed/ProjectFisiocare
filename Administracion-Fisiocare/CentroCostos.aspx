 <%@ Page Language="VB" AutoEventWireup="false" CodeFile="CentroCostos.aspx.vb" Inherits="CentroCostos" %>
<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
<script type="text/javascript" src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.1/jquery.min.js'></script>
    <title>Administración Fisiocare</title>
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; height:870px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
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
                    <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Selectable="False" Text="Zona Medica" Value="Zona Medica">
                    <asp:MenuItem NavigateUrl="~/consultasadminpaq.aspx" Text="Ventas Estimadas" Value="consultasadminpaq.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consulpadmin.aspx" Text="Productos Vendidos" Value="consulpadmin.aspx">
                    </asp:MenuItem>
                   <%-- <asp:MenuItem NavigateUrl="~/concxc.aspx" Text="Clientes CxC" Value="concxc.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consutas Medicas" Value="consultasmed.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/rptweb.aspx" Text="Reportes Facturas" Value="rptweb.aspx">
                    </asp:MenuItem>--%>
                     </asp:MenuItem>
                <asp:MenuItem Text="Camaras" Value="Camaras">
                    <asp:MenuItem NavigateUrl="~/camarasStar.aspx" Text="Camaras Star Medica" Value="Camaras Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/camarasplaza.aspx" Text="Camaras Star plaza" Value="Camaras Star plaza">
                    </asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
            </asp:ScriptManager>
            <br />
        <asp:Panel ID="pnlPrincipal" runat="server">
           <div style="text-align:left">
        LISTA DE PRECIOS SEGUN LA ASEGURADO O INSITUCIÓN<br />
        <asp:DropDownList ID="cmbInstituciones" runat="server" Font-Names="calibri" Font-Size="12pt"
            Width="795px" AutoPostBack="True">
        </asp:DropDownList>
        <asp:Button ID="btnNcosto" runat="server" Text="Nuevo" Width="83px" CommandName="nCosto" /></div>
        <div style="height:250px; width:900px; overflow:auto; text-align:left">
        <%--<asp:GridView ID="gridCostos" runat="server" AutoGenerateColumns="False"
     AutoGenerateEditButton="True" CellPadding="4" ForeColor="#333333" GridLines="None"
     DataKeyNames="idCosto"
     onrowediting="gridCostos_RowEditing"
     onrowcancelingedit="gridCostos_RowCancelingEdit"
     onrowupdating="gridCostos_RowUpdating" Width="870px">
     <RowStyle BackColor="#EFF3FB" />
     <Columns>
         <asp:BoundField DataField="id_servicio" HeaderText="Terapia" ReadOnly="True" />
         <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
         <asp:BoundField DataField="responsable" HeaderText="Responsable" ReadOnly="True" />
         <asp:BoundField DataField="costo" DataFormatString="{0:C}" HeaderText="Costo" />
         
         <asp:TemplateField HeaderText="Activo">
             <EditItemTemplate>
                 <asp:DropDownList ID="ddlPaises" runat="server">
                 </asp:DropDownList>
             </EditItemTemplate>
             <ItemTemplate>
                 <asp:Label ID="Label1" runat="server" Text='<%# Bind("descripcion") %>'></asp:Label>
             </ItemTemplate>
         </asp:TemplateField>
     </Columns>
     <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
     <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
     <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
     <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
     <EditRowStyle BackColor="#2461BF" />
     <AlternatingRowStyle BackColor="White" />
 </asp:GridView>--%>
        <asp:DataGrid ID="gridCostos" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1" DataKeyField="idCosto"
            ForeColor="Black" GridLines="None" PageSize="12" Width="882px">
            <FooterStyle BackColor="Tan" />
            <EditItemStyle Font-Bold="False" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center"
                Mode="NumericPages" />
            <AlternatingItemStyle BackColor="Control" />
            <Columns>
                <asp:BoundColumn DataField="id_servicio" HeaderText="Terapia" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="descripcion" HeaderText="Descripcion"></asp:BoundColumn>
                <asp:BoundColumn DataField="responsable" HeaderText="Responsable" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="costo" DataFormatString="{0:C}" HeaderText="Costo"></asp:BoundColumn>
                <asp:BoundColumn DataField="Status"  HeaderText="Activo"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:Button ID="cmdEditarT" runat="server" CommandName="cmdEditarT" CssClass="boton"
                            Font-Bold="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#000040" Text="Editar" />
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center" />
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#FC0200" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid>
        </div>
        <hr />
        
        <table border="0" cellpadding="0" cellspacing="0" style="width: 100%">
            <tr>
                <td>
                    <table border="0" cellpadding="0" cellspacing="0" style="width: 400px;background-color: #f4f4f4; color:#1B3774">
                        <tr >
                            <td style=" text-align:left">
                    TIPOS DE TERAPIAS</td>
                            <td style=" text-align:right">
                                <asp:Button ID="btnNterapia" runat="server" Text="Nuevo" Width="83px" CommandName="nTerapia" /></td>
                        </tr>
                    </table>
                    <div style="height:380px; width:400; overflow:auto">
                    <asp:DataGrid ID="gridServicios" runat="server" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1"
                        ForeColor="Black" GridLines="None" Style="font-size: 10pt" Width="400px">
                        <FooterStyle BackColor="Tan" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="Control" />
                        <Columns>
                            <asp:BoundColumn DataField="id_servicio" ReadOnly="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="descripcion" HeaderText="Servicio"></asp:BoundColumn>
                            <asp:BoundColumn DataField="codigoSat" HeaderText="CodigoSat"></asp:BoundColumn>
                            <asp:BoundColumn DataField="tipoCosto" HeaderText="TipoCosto"></asp:BoundColumn>
                            <asp:EditCommandColumn CancelText="Cancelar" EditText="Editar" UpdateText="Actualizar"
                                Visible="true">
                                <ItemStyle ForeColor="Red" />
                            </asp:EditCommandColumn>
                        </Columns>
                        <HeaderStyle BackColor="#FC0200" Font-Bold="True" ForeColor="White" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:DataGrid></div></td>
                <td style="width: 450px">
                <table border="0" cellpadding="0" cellspacing="0" style="width: 450px;background-color: #f4f4f4; color:#1B3774">
                        <tr>
                            <td style=" text-align:left">
                                ASEGURADORES E INSTITUCIONES</td>
                            <td style=" text-align:right">
                                <asp:Button ID="btnNaseguradora" runat="server" Text="Nuevo" Width="83px" CommandName="nAseguradora" /></td>
                        </tr>
                    </table>
                    <div style="height:380px; width:430; overflow:auto">
                                        <asp:DataGrid ID="gridInstituciones" runat="server" DataKeyField="idresponsable" AutoGenerateColumns="False"
                        BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1"
                        ForeColor="Black" GridLines="None" Style="font-size: 10pt" Width="430px">
                        <FooterStyle BackColor="Tan" />
                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                        <AlternatingItemStyle BackColor="Control" />
                        <Columns>
                            <asp:BoundColumn DataField="idresponsable" ReadOnly="True"></asp:BoundColumn>
                            <asp:BoundColumn DataField="abreviacion" HeaderText="Abreviación"></asp:BoundColumn>
                            <asp:BoundColumn DataField="descripcion" HeaderText="Aseguradoras e Instituciones"></asp:BoundColumn>
                            <asp:EditCommandColumn CancelText="Cancelar" EditText="Editar" UpdateText="Actualizar"
                                Visible="true">
                                <ItemStyle ForeColor="Red" />
                            </asp:EditCommandColumn>
                        </Columns>
                        <HeaderStyle BackColor="#FC0200" Font-Bold="True" ForeColor="White" />
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:DataGrid></div></td>
            </tr>
        </table>
        </asp:Panel>
        
        <asp:Panel ID="pnlcostos" runat="server" Height="64px" Width="895px" Visible="False">
            <table border="1">
                <tr style="color: #1b3774; background-color: #f4f4f4">
                    <td colspan="6"  style="background-color:#FC0200; color:White">
                        NUEVO COSTO POR ASEGURADORA O INSTITUCION</td>
                </tr>
                <tr style="background-color: #f4f4f4; color:#1B3774">
                    <td>
                        TIPO DE TERAPIA</td>
                    <td>
                        ASEGURADORA E INSTUCIONES</td>
                        <td>
                        COMENTARIO
                        </td>
                    <td style="width: 74px">
                        COSTO</td>
                        <td>
                        ACTIVO</td>
                    <td>
                        CAMBIO DE PRECIO</td>
                </tr>
                <tr>
                    <td>
            <asp:DropDownList ID="cmbterapias" runat="server" Font-Names="calibri" Font-Size="12pt"
            Width="250px" AutoPostBack="True">
            </asp:DropDownList></td>
                    <td>
            <asp:DropDownList ID="cmbaseguradoras" runat="server" Font-Names="calibri" Font-Size="12pt"
            Width="300px" AutoPostBack="True">
            </asp:DropDownList></td>
            <td>
            <asp:TextBox ID="txtcomentario" runat="server" MaxLength="15" Width="100px"></asp:TextBox></td>
                    <td style="width: 74px">
            <asp:TextBox ID="txtcosto" runat="server" Width="60px"></asp:TextBox></td>
            <td>
            <asp:DropDownList ID="cmbActivo" runat="server" Width="50px" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="True">SI</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:DropDownList></td>
            <td>
                <asp:DropDownList ID="cmbcambiop" runat="server" Width="60px" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="True">SI</asp:ListItem>
                            <asp:ListItem Value="False">NO</asp:ListItem>
                        </asp:DropDownList>
            </td>
                </tr>
            </table>
            &nbsp; &nbsp; &nbsp; &nbsp;
            <br />
            <asp:Button ID="Button4" runat="server" Text="Grabar" Width="104px" />
            &nbsp; &nbsp;&nbsp;
            <asp:Button ID="Button3" runat="server" Text="Terminar" Width="104px" />
            </asp:Panel>
         <asp:Panel ID="pnlterapias" runat="server" Height="64px" Width="895px" Visible="False">
            <br />
            <table border="1">
                <tr style="color: #1b3774; background-color: #f4f4f4">
                    <td colspan="6" style="background-color:#FC0200; color:White">
                        NUEVO TIPO DE TERAPIA O SERVICIO</td>
                </tr>
                <tr style="background-color: #f4f4f4; color:#1B3774">
                    <td>
                        ABREVIACION</td>
                    <td>
                        DESCRIPCION</td>
                    <td>
                        CLAVE SAT</td>
                    <td>
                        CLAVE UNIDAD</td>
                    <td>
                        UNIDAD</td>
                    <td>
                        TASA IVA</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtabrevia"  runat="server" MaxLength="5" Width="50px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtservicio" runat="server" Width="300px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtclaveservicio" Text="85121600" runat="server" Width="100px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtclaveunidad" Text="E48" runat="server" Width="100px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtunidad" Text="Unidad de Servicio" runat="server" Width="120px"></asp:TextBox></td>
                    <td>
                        <asp:DropDownList ID="cmbiva" runat="server" Width="50px" Font-Names="Calibri" Font-Size="11pt">
                            <asp:ListItem Value="0.000000">0</asp:ListItem>
                            <asp:ListItem Value="0.160000">16</asp:ListItem>
                            <asp:ListItem Value="Exento">Exento</asp:ListItem>
                        </asp:DropDownList></td>
                </tr>
            </table>
                <br />
                        <asp:Button ID="Button1" runat="server" Text="Grabar" Width="104px" />
                &nbsp; &nbsp;&nbsp;
                <asp:Button ID="Button5" runat="server" Text="Terminar" Width="104px" /></asp:Panel>
          <asp:Panel ID="pnlAseguradora" runat="server" Height="64px" Width="895px" Visible="False">
            <br />
            <table border="1">
                <tr style="color: #1b3774; background-color: #f4f4f4">
                    <td colspan="2" style="background-color:#FC0200; color:White">
                        NUEVA&nbsp; ASEGURADORA O INSTIUCIÓN</td>
                </tr>
                <tr style="background-color: #f4f4f4; color:#1B3774">
                    <td>
                        ABREVIACION</td>
                    <td>
                        DESCRIPCION</td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="txtabreviaAseguradora" runat="server" MaxLength="5" Width="40px"></asp:TextBox></td>
                    <td>
                        <asp:TextBox ID="txtdescripAseguradora" runat="server" Width="463px"></asp:TextBox></td>
                </tr>
            </table>
            &nbsp;<br />
                        <asp:Button ID="Button2" runat="server" Text="Grabar" Width="104px" />
                            &nbsp; &nbsp;
                            <asp:Button ID="Button6" runat="server" Text="Terminar" Width="104px" /></asp:Panel>
        &nbsp; &nbsp; &nbsp;&nbsp;
        <asp:Panel ID="Panelmt" runat="server" BackColor="White" BorderColor="Silver" BorderStyle="Solid"
        BorderWidth="2px" Width="750px">
        <div style="padding-right: 0px; padding-left: 0px; font-size: 11pt; padding-bottom: 3px;
            color: white; padding-top: 3px; border-bottom: white 2px solid; font-family: Calibri; background-color: red">
            ..:: DATOS DE MODIFICACION ::..</div>
        <table border="0" cellpadding="3" cellspacing="0" style="width: 750px; text-align: left">
            <tr>
                <td style="width: 165px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    TERAPIA:</td>
                <td style="width: 15px; color: #ffffff">
                    <asp:TextBox ID="txtmterapia" runat="server" MaxLength="150" Width="163px" CssClass="texto"></asp:TextBox></td>
                    <td style="width: 161px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    COSTO:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtmcosto" runat="server" MaxLength="100" Width="163px"></asp:TextBox></td>
            </tr>
            <tr style="color: #ffffff">
                <td style="width: 165px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    DESCRIPCION:</td>
                <td style="color: #ffffff" colspan="3">
                    <asp:TextBox ID="txtmdescripcion" runat="server" MaxLength="100" Width="300px"></asp:TextBox></td>
                   
                    <asp:TextBox ID="txtidcosto" runat="server" MaxLength="100" Width="30px"></asp:TextBox>
            </tr>
            <tr style="color: #ffffff">
                
                <td style="width: 161px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    RESPONSABLE:</td>
                <td style="width: 400px; color: #ffffff">
                    <asp:TextBox ID="txtmresponsables" runat="server" MaxLength="100" Width="300px"></asp:TextBox></td>
                </tr>
            <tr style="color: #ffffff">
                <td style="width: 150px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    STATUS:</td>
                <td style="width: 10px">
                    <asp:DropDownList ID="cbomactivo" runat="server" Width="80px">
                            <asp:ListItem Value="True">Activo</asp:ListItem>
                            <asp:ListItem Value="False">Inactivo</asp:ListItem>
                    </asp:DropDownList>
                    </td>
                 <td style="width: 200px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    CAMBIAR PRECIO:</td>
                <td style="width: 10px">
                    <asp:DropDownList ID="cbomcambiarp" runat="server" Width="80px">
                            <asp:ListItem Value="True">Activo</asp:ListItem>
                            <asp:ListItem Value="False">Inactivo</asp:ListItem>
                    </asp:DropDownList>
                    </td>
            </tr>
            <tr>
                <td style="width: 165px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    DESCUENTO:</td>
                <td style="width: 15px; color: #ffffff">
                    <asp:TextBox ID="txtmdescuento" runat="server" MaxLength="150" Width="163px" CssClass="texto"></asp:TextBox></td>
                    <td style="width: 161px; color: white; border-bottom: white 2px solid; height: 19px;
                    background-color: red">
                    APOYO:</td>
                <td style="width: 100px">
                    <asp:TextBox ID="txtmapoyo" runat="server" MaxLength="100" Width="163px"></asp:TextBox></td>
            </tr>
        </table>
        <div style="padding-right: 0px; padding-left: 0px; padding-bottom: 3px; padding-top: 3px;
            background-color: red">
            <asp:Button ID="bgmterapia" runat="server" CssClass="boton" ForeColor="#000040" Text="Grabar"
                Width="92px" />&nbsp;
            <asp:Button ID="bgmcancelar" runat="server" CssClass="boton" ForeColor="#000040" Text="Cancelar"
                Width="92px" /></div>
    </asp:Panel>
        <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" TargetControlID="txtcosto" ValidChars="1234567890.">
        </cc1:FilteredTextBoxExtender>
        <cc1:messagebox ID="Messagebox1" runat="server" />
    
    <input id="hfStatus" runat="server" type="hidden" />
      <input id="hfBandera" runat="server" type="hidden" />
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtendermt" runat="server" BackgroundCssClass="modalBackground"
          PopupControlID="Panelmt" TargetControlID="hfStatus">
      </ajaxToolkit:ModalPopupExtender>
    </div>
    </form>
    </center>
</body>
</html>
