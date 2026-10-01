<%@ Page Language="VB" AutoEventWireup="false" CodeFile="auxiliar.aspx.vb" Inherits="auxiliar" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>  
    <script language="javascript" type="text/javascript">
    function impventana(){
    xterapia=document.getElementById("miIdterapia").value;
    window.open("impmedios.aspx?x="+xterapia,"nuevo","'toolbar=no,width=100,height=100,scrollbars=no,location=no,menubar=no'");
    }
    </script>  
</head>
<body onload="javascript:if(history.length>0)history.go(+1)">
<center>
    <form id="form1" runat="server">
    <div style="width:500px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/banneraux.png" /><br />
        <br />
        <asp:DataGrid ID="gridTerapias" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1" DataKeyField="idTerapia"
            ForeColor="Black" GridLines="None" Width="490px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="Control" />
            <Columns>
                <asp:BoundColumn DataField="diagnostico" HeaderText="Diagnostico">
                    <ItemStyle Font-Bold="True" Font-Italic="True" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="True" HorizontalAlign="Left" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fechaInicio" HeaderText="Fecha"></asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" Font-Bold="True" ForeColor="Red">| VER |</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#FC0200" Font-Bold="True" ForeColor="White" />
        </asp:DataGrid>
        <asp:Label ID="lblmensaje" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
            Text="Label" Width="490px"></asp:Label><br />
        <br />
        <br />
        <div id="informacion" runat="server">
            &nbsp;<asp:DataGrid ID="gridejercicios" runat="server" AutoGenerateColumns="False"
                BorderColor="Red" BorderStyle="Solid" BorderWidth="1px" Visible="False" Width="495px">
                <Columns>
                    <asp:TemplateColumn HeaderText="MEDIOS FISICOS">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CommandName="medios" Font-Bold="True"
                                ForeColor="Red" Text='<%# Bind("texto") %>'></asp:LinkButton>
                            <asp:Label ID="lblmiidM" runat="server" Text='<%# Bind("id") %>' Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="EJERCICIOS Y RUTINAS">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton2" runat="server" CommandName="ejercicios" Font-Bold="True"
                                ForeColor="Red" Text=""></asp:LinkButton>
                            <asp:Label ID="lblmiidE" runat="server" Text="" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="CONTRAINDICACIONES">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton4" runat="server" CommandName="otros" Font-Bold="True"
                                ForeColor="Red" Text=""></asp:LinkButton>
                            <asp:Label ID="lblmiidO" runat="server" Text="" Visible="False"></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="Gray" ForeColor="White" />
            </asp:DataGrid>
            <img src="imagenes/printer.png" /><asp:Button ID="Button1" runat="server" Font-Size="9pt"
                Text="IMPRIMIR EJERCICIOS" Width="149px" OnClientClick="impventana();" /><br />
        <table border="1" bordercolor="#ffffff" cellpadding="0" cellspacing="1" width="499">
            <tr>
                <td align="center" bordercolor="#7b7b7b" bgcolor="#fc0200">
                    <font color="#FFFFFF">MEDIOS FISICOS</font></td>
            </tr>
            <tr>
                <td bordercolor="#7b7b7b">
                    <textarea id="txtmedios" style="width: 480px; height: 60px;" runat="server"></textarea></td>
            </tr>
        </table>
        <br />
        <table border="1" bordercolor="#ffffff" cellpadding="0" cellspacing="1" width="499">
            <tr>
                <td align="center" bordercolor="#7b7b7b" bgcolor="#fc0200">
                    <font color="#FFFFFF">EJERCICIOS</font></td>
            </tr>
            <tr>
                <td bordercolor="#7b7b7b">
                    <textarea id="txtejercicios" style="width: 480px; height: 60px;" runat="server"></textarea></td>
            </tr>
        </table>
            <asp:Label ID="lblidm" runat="server" Visible="False"></asp:Label><asp:Label ID="lblide"
                runat="server" Visible="False"></asp:Label><asp:Label ID="lblido" runat="server"
                    Visible="False"></asp:Label><br />
        <table border="1" bordercolor="#ffffff" cellpadding="0" cellspacing="1" width="499">
            <tr>
                <td align="center" bordercolor="#7b7b7b" bgcolor="#fc0200">
                    <font color="#FFFFFF">CONTRA INDICACIONES</font></td>
            </tr>
            <tr>
                <td bordercolor="#7b7b7b">
                    <textarea id="txtotros" rows="2" style="width: 480px; height: 60px;" runat="server"></textarea></td>
            </tr>
        </table></div>
        <asp:Label ID="lblIdcliente" runat="server" Text="Label" Visible="False"></asp:Label>
        <input id="miIdterapia" runat="server" type="hidden" /></div>
    </form>
</center>
    &nbsp;
</body>
</html>
