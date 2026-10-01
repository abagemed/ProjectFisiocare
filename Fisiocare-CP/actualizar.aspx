<%@ Page Language="VB" AutoEventWireup="false" CodeFile="actualizar.aspx.vb" Inherits="actualizar" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>::FISIOCARE::</title>
    <script language="javascript" type="text/javascript">
    function gif(){
    document.getElementById("gbotones").style.visibility='hidden';
    document.getElementById("migif").style.visibility='visible';
    }
    </script>
</head>
<body><center>
    <form id="form1" runat="server">
     <div style="width:800px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/baner1.png" /><br />
         <table bgcolor="#fc0200" border="0" cellpadding="0" cellspacing="0" width="799">
             <tr>
                 <td align="left" style="height: 16px">
                     <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="True" Font-Italic="True"
                         ForeColor="White" OnClientClick="cierraventana();">| Regresar a Agenda |</asp:LinkButton>
                     &nbsp; &nbsp; &nbsp;
                 </td>
                 <td style="height: 16px">
                     &nbsp;
                 </td>
             </tr>
         </table>
         <br />
         <br />
         <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"></asp:Label><br />
         <br />
         <br />
         <div id="gbotones" style="visibility:visible">
             <asp:DropDownList ID="cmbaño" runat="server" Width="54px">
                 <asp:ListItem>2009</asp:ListItem>
                 <asp:ListItem>2010</asp:ListItem>
                 <asp:ListItem Selected="True">2011</asp:ListItem>
                 <asp:ListItem>2012</asp:ListItem>
             </asp:DropDownList>
             <br />
             <br />
        <asp:Button ID="Button1" runat="server" Text="ACTUALIZAR DATOS" OnClientClick="javascript:gif();" /></div>
        <div id="migif" style="visibility:hidden">
                <img src="imagenes/loader.gif" /><br />
                ...ACTUALIZANDO DATOS FAVOR DE ESPERAR UNOS MINUTOS...</div><br />
         <asp:TextBox ID="TextBox1" runat="server" Height="81px" TextMode="MultiLine"
             Width="623px" Visible="False"></asp:TextBox><br />
         <br />
         <cc1:messagebox ID="Messagebox1" runat="server" />
         <br />
         <br />
         <br />
     </div>
    </form>
    </center>
</body>
</html>
