<%@ Page Language="VB" AutoEventWireup="false" CodeFile="facGeneradasth.aspx.vb" Inherits="facGeneradasth" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
</head>
<body><center>
    <form id="form1" runat="server">
     <div style="width:900px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt" align="center">
        <img src="imagenes/baner2.png" />&nbsp;<table bgcolor="#fc0200" border="0" cellpadding="0"
            cellspacing="0" width="900">
            <tr>
                <td align="left" style="width: 701px">
                    <asp:LinkButton ID="LinkButton8" runat="server" Font-Names="calibri" Font-Size="18px"
                        ForeColor="White">Facturación</asp:LinkButton></td>
                <td style="text-align: right">
                    <asp:LinkButton ID="LinkButton1" runat="server" Font-Names="calibri" Font-Size="18px"
                        ForeColor="White">Agenda</asp:LinkButton></td>
            </tr>
        </table>
         <asp:ScriptManager ID="ScriptManager1" runat="server">
         </asp:ScriptManager>
         <hr />
         <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; background-color: #f4f4f4">
             <tr>
                 <td style="text-align: left">
                     <asp:DropDownList ID="cmbMesFac" runat="server" Font-Names="calibri" Font-Size="12pt"
                        >
                         <asp:ListItem Value="0">TODOS LOS MESES</asp:ListItem>
                         <asp:ListItem Value="1">ENERO</asp:ListItem>
                         <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                         <asp:ListItem Value="3">MARZO</asp:ListItem>
                         <asp:ListItem Value="4">ABRIL</asp:ListItem>
                         <asp:ListItem Value="5">MAYO</asp:ListItem>
                         <asp:ListItem Value="6">JUNIO</asp:ListItem>
                         <asp:ListItem Value="7">JULIO</asp:ListItem>
                         <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                         <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                         <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                         <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                         <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                     </asp:DropDownList>&nbsp;<asp:DropDownList ID="cmblosaños" runat="server" Font-Names="calibri" Font-Size="12pt">
                         <asp:ListItem>2009</asp:ListItem>
                         <asp:ListItem>2010</asp:ListItem>
                         <asp:ListItem>2011</asp:ListItem>
                         <asp:ListItem>2012</asp:ListItem>
                         <asp:ListItem>2013</asp:ListItem>
                         <asp:ListItem>2014</asp:ListItem>
                         <asp:ListItem>2015</asp:ListItem>
                         <asp:ListItem>2016</asp:ListItem>
                         <asp:ListItem>2017</asp:ListItem>
                         <asp:ListItem>2018</asp:ListItem>
                         <asp:ListItem>2019</asp:ListItem>
                     </asp:DropDownList></td>
                 <td style="width: 448px">
                 </td>
             </tr>
             <tr>
                 <td style="text-align: left; border-top-color:White; border-top-width:4px; border-top-style:solid">
                     <asp:DropDownList ID="cmbfacgeneradas" runat="server" Font-Names="calibri"
                         Font-Size="12pt" Width="429px">
                         <asp:ListItem Value="0">Facturas Generadas</asp:ListItem>
                         <asp:ListItem Value="1">Facturas </asp:ListItem>
                         <asp:ListItem Value="2">Facturas Canceladas</asp:ListItem>
                     </asp:DropDownList></td>
                 <td style="width: 448px; border-top-color:White; border-top-width:4px; border-top-style:solid">
                     <asp:Button ID="btnfacturar" runat="server" CssClass="boton" Font-Names="calibri"
                         Font-Size="11pt" ForeColor="Red" Text="Buscar" Width="130px" />&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnBfac" runat="server" CssClass="boton" Font-Names="calibri"
                         Font-Size="11pt" ForeColor="Red" Text="Buscar No. Factura" Width="130px" /></td>
             </tr>
         </table>
         <hr />
         <div style="overflow: auto; height:500px">
             &nbsp;<asp:DataGrid ID="gFacgeneradas" runat="server" AutoGenerateColumns="False" Width="880px" CellPadding="4">
             <Columns>
                 <asp:BoundColumn HeaderText="Factura" DataField="num_factura">
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundColumn>
                 <asp:BoundColumn HeaderText="Fecha" DataField="fecha">
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundColumn>
                 <asp:BoundColumn HeaderText="Razon Social" DataField="nombre">
                     <HeaderStyle HorizontalAlign="Left" />
                     <ItemStyle HorizontalAlign="Left" />
                 </asp:BoundColumn>
                 <asp:BoundColumn HeaderText="Status" DataField="cancelada">
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundColumn>
                 <asp:TemplateColumn><ItemStyle Width="70px" HorizontalAlign="Center" />
                     <ItemTemplate>
                         <asp:Button ID="Button1" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" CommandName="cancelar" />
                     </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>
                         <asp:ImageButton ID="ImageButton1" runat="server" CommandName="imprimir" Height="20px"
                             ImageUrl="~/imagenes/agt_print.png" Width="22px" />
                     </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                     <ItemTemplate>
                         <asp:ImageButton ID="ImageButton2" CommandName="mail" runat="server" Height="30px" ImageUrl="~/imagenes/correos.jpg" Width="33px" />
                     </ItemTemplate>
                 </asp:TemplateColumn>
                 <asp:BoundColumn HeaderText="vf" DataField="validaf" Visible="false">
                     <HeaderStyle HorizontalAlign="Center" />
                     <ItemStyle HorizontalAlign="Center" />
                 </asp:BoundColumn>
             </Columns>
             <AlternatingItemStyle BackColor="WhiteSmoke" />
             <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
         </asp:DataGrid></div>
         <asp:Panel ID="Panel1" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="160px" Width="350px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 300px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                     NÚMERO DE FACTURA
                     </td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtnumfactura" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Style="border-bottom: #e0e0e0 2px solid; text-align:center" Font-Size="18pt" Width="163px"></asp:TextBox>
                         <asp:TextBox ID="TextBox2" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Enabled="False" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="43px">TH</asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button3" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar" />
                         &nbsp;
                         <asp:Button ID="Button2" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" /></td>
                 </tr>
             </table>
         </asp:Panel>
         <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
             PopupControlID="Panel1" TargetControlID="btnBfac">
         </cc1:ModalPopupExtender>
         <br />
         <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="160px" Width="350px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 300px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                         e-m@il&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtmail" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="285px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button4" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar" />
                         &nbsp;
                         <asp:Button ID="Button5" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" /></td>
                 </tr>
             </table>
             <input id="hfidfac" runat="server" type="hidden" /></asp:Panel>
         <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="FondoAplicacion"
             PopupControlID="Panel2" TargetControlID="hfmodalmail">
         </cc1:ModalPopupExtender>
         <input id="hfmodalmail" runat="server" type="hidden" /><br />
         <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="240px" Width="370px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 350px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                         NÚMERO FACTURA A CANCELAR&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtnumcancelar" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="163px" Enabled="False"></asp:TextBox>
                         <asp:TextBox ID="TextBox3" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Enabled="False" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="43px">TH</asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style="background-color: #f4f4f4; color:#1A3773; font-size:16pt">
                         SUSTITUIDO POR
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsustituido"
                             ErrorMessage="*" ValidationGroup="cancelar"></asp:RequiredFieldValidator>
                         <asp:TextBox ID="txtsustituido" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button6" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar" ValidationGroup="cancelar" />
                         &nbsp;
                         <asp:Button ID="Button7" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" /></td>
                 </tr>
             </table>
         </asp:Panel>
         <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="FondoAplicacion"
             PopupControlID="Panel3" TargetControlID="hfModalcancelar">
         </cc1:ModalPopupExtender>
         <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
         <input id="hfModalcancelar" runat="server" type="hidden" />
         <asp:Label ID="lblfecha" runat="server" Visible="False"></asp:Label></div>
    </form>
    </center>
</body>
</html>
