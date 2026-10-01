<%@ Page Language="VB" AutoEventWireup="false" CodeFile="impmedios.aspx.vb" Inherits="impmedios" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
     <script language="javascript" type="text/javascript">
    function imprSelec()
       {
  var ficha = document.getElementById('miimpresion');
  var ventimp = window.open(' ', 'popimpr','toolbar=no,width=100,height=100,scrollbars=no,location=no,menubar=no');
  ventimp.document.write( ficha.innerHTML );
  ventimp.document.close();
  ventimp.print( );
  ventimp.close();
  window.close();
} 
    </script>
</head>
<body onload="javascript:imprSelec();">
    <form id="form1" runat="server">
    <div id="miimpresion">
        <asp:DataGrid ID="DataGrid1" runat="server" AutoGenerateColumns="False" Font-Names="Arial"
            Font-Size="12pt" Height="240px" Width="927px">
            <Columns>
                <asp:BoundColumn DataField="tipo" HeaderText="TIPO">
                    <ItemStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="texto" HeaderText="DESCRIPCION"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="Silver" Font-Bold="True" ForeColor="White" />
            <ItemStyle Font-Bold="False" />
        </asp:DataGrid>&nbsp;</div>
    </form>
</body>
</html>
