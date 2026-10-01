<%@ Page Language="VB" AutoEventWireup="false" CodeFile="rptWeb.aspx.vb" Inherits="rptWeb" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Página sin título</title>
</head>
<body>
<center>
    <form id="form1" runat="server">
    <div>
        <asp:Image ID="Image1" runat="server" ImageUrl="~/imagenes/baner1.png" />&nbsp;</div>
        <table  style="width: 900px">
            <tr >
                <td align="left" colspan="3" valign="middle" style="background-color: red; height: 21px;">
                    &nbsp;<asp:LinkButton ID="LinkButton5" runat="server" Font-Names="calibri" Font-Size="16px"
                        ForeColor="White" OnClientClick="cierraventana()">| Regresar a Agenda |</asp:LinkButton></td>
          </tr>
      </table>        
        <table style="width: 800px">
            <tr>
                <td>
                    <asp:Label ID="lbReporte" runat="server" Text="REPORTE:" Font-Bold="True" Font-Size="Small"></asp:Label><asp:DropDownList ID="cboReporte" runat="server" Width="420px" AutoPostBack="True">
                    </asp:DropDownList><br />
                    <br />
                    <div id="divFecha" runat="server" visible="false">
                        <asp:Label ID="lbInicio" runat="server" Text="INICIO: " Font-Bold="True" Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="txtInicio" runat="server" MaxLength="10" Width="94px"></asp:TextBox>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        &nbsp; &nbsp; &nbsp;&nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                        <asp:Label ID="lbFinal" runat="server"
                            Text="FINAL:" Font-Bold="True" Font-Size="Small"></asp:Label>
                        <asp:TextBox ID="txtFinal" runat="server" Width="96px"></asp:TextBox><br />
                        <asp:Label ID="Label1" runat="server" Font-Size="Smaller" Text="Formato de fecha:dd/mm/yyy"></asp:Label>
                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    </div><br />
                    <div id="divFiltro1" runat="server" visible="false">
                        <asp:Label ID="lbFiltro1" runat="server" Text="FILTRO 1:" Font-Bold="True" Font-Size="Small"></asp:Label>
                        <asp:DropDownList ID="cboFiltro1" runat="server" Width="350px">
                        </asp:DropDownList>
                        </div>
                        <br />
                    <div id="divFiltro2" runat="server" visible="false">
                        <asp:Label ID="lbFiltro2" runat="server" Text="FILTRO 2: " Font-Bold="True" Font-Size="Small"></asp:Label>
                        <asp:DropDownList ID="cboFiltro2" runat="server" Width="350px">
                        </asp:DropDownList>
                    </div>
                    <br />
                        <asp:Button ID="cmGenerar" runat="server" Text="GENERAR REPORTE" />
                    &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;
                    <asp:Button ID="cmdexcel" runat="server" Text="EXCEL" Visible="False" />
                    <asp:Button ID="cmdImprimir" runat="server" Text="IMPRIMIR" Visible="False" /></td>
            </tr>
        </table>
        <div id="divRpt" style="width: 900px" runat="server" visible="false">
            <asp:GridView ID="gvRpt" runat="server" CellPadding="4" Font-Size="Small" ForeColor="#333333" GridLines="None">
                <RowStyle BackColor="#E0E0E0" HorizontalAlign="Left" ForeColor="#333333" />
                <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                <HeaderStyle BackColor="Red" Font-Bold="True" ForeColor="White" />
                <EditRowStyle BackColor="#999999" />
                <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                <EmptyDataTemplate>
                    <table>
                        <tr>
                            <td style="width: 259px; background-color: #6891c0">
                                DESCRIPCION</td>
                        </tr>
                        <tr>
                            <td style="width: 259px">
                                LO SIENTO NO EXISTEN DATOS</td>
                        </tr>
                    </table>
                </EmptyDataTemplate>
            </asp:GridView>
            &nbsp;
            <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label>
        </div>
    </form>
</center>
</body>
</html>
