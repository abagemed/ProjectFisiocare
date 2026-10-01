<%@ Page Language="VB" AutoEventWireup="false" CodeFile="correos.aspx.vb" Inherits="correos" %>

<%@ Register Assembly="CKEditor.NET" Namespace="CKEditor.NET" TagPrefix="CKEditor" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
<center>
    <form id="form1" runat="server">
   <div style="width:900px; border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner1.png" /><br />
       <br />
       <asp:Button ID="Button1" runat="server" Text="Enivar Correo" Width="199px" />&nbsp;<br />
       TOTAL DE CORREOS ENVIADOS&nbsp;
       <asp:Label ID="Label1" runat="server" Font-Bold="True"></asp:Label><br />
       Bloque de envio:
       <asp:TextBox ID="txtbloque" runat="server" BackColor="White" BorderColor="White"
           BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" ForeColor="#C00000" Style="border-bottom: #e0e0e0 2px solid;
           text-align: center"></asp:TextBox><br />
       <table border="0" cellpadding="0" cellspacing="0" style="width: 100%; text-align:center">
           <tr>
               <td >
                   <img src="imgCorreos/marketingzonamedia.JPG" /></td>
           </tr>
           <tr>
               <td >
                   <img src="imgCorreos/zonamedica.JPG" /></td>
           </tr>
           <tr>
               <td >
                   <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                       <span style="font-size: 16pt"><?xml
                           namespace="" ns="urn:schemas-microsoft-com:office:office" prefix="o" ?><o:p></o:p></span></p>
                   <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                       <span style="font-size: 16pt">Calle 26 Núm. 210 Loc. 4 por 7 y 15 Fracc. Altabrisa<o:p></o:p></span></p>
                   <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                       <span style="font-size: 16pt">Frente al hospital Star Medica<o:p></o:p></span></p>
                   <p align="center" class="MsoNormal" style="margin: 0cm 0cm 0pt; text-align: center">
                       <span style="font-size: 16pt">Mérida Yucatán <b>Tel. (999) 2541020</b><o:p></o:p></span></p>
               </td>
           </tr>
       </table>
       <br />
       <br />
    
    </div>
    </form>
 </center>
</body>
</html>
