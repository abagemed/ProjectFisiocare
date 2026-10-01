<%@ Page Language="VB" AutoEventWireup="false" CodeFile="movimientos.aspx.vb" Inherits="movimientos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
</head>
<body>
<center>
    <form id="form2" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/baner1.png" /><br />
        <table width="900px" border="1" cellpadding="0" cellspacing="1" bordercolor="#FFFFFF">
          <tr bordercolor="#7b7b7b" bgcolor="#fc0200">
            <td align="left" style="width: 610px">
                <asp:LinkButton ID="LinkButton5" runat="server" Font-Bold="False" Font-Italic="False"
                    ForeColor="White" OnClientClick="cierraventana()" Font-Names="Calibri" Font-Size="16px">| Regresar a Agenda |</asp:LinkButton>
                </td>
              <td colspan="2" align="right" style="width: 200px">
                  <asp:Label ID="lblsucursal" runat="server" Font-Bold="True" Font-Size="12pt" ForeColor="White"></asp:Label>&nbsp;</td>
          </tr>
      </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <asp:Panel ID="pnlbuscar" runat="server" Width="800px">
            <table border="1" cellpadding="0" cellspacing="2" style="width: 800px">
                <tr>
                    <td style="width: 410px">
                    </td>
                    <td style="width: 130px; color: #1a3773; background-color: #f4f4f4">
                        INCIO</td>
                    <td style="width: 130px; color: #1a3773; background-color: #f4f4f4">
                        FIN</td>
                    <td rowspan="2" style="width: 130px">
                        <asp:Button ID="btnbuscar" runat="server" Text="Buscar" Width="97px" Font-Size="9pt" ForeColor="Red" /></td>
                </tr>
                <tr>
                    <td style="width: 410px">
        <asp:RadioButtonList ID="opciones" runat="server" Width="410px" RepeatDirection="Horizontal" BorderColor="#EFEFEF" BorderStyle="Solid" BorderWidth="2px" Font-Bold="False" BackColor="#F4F4F4" ForeColor="#1A3773">
            <asp:ListItem Selected="True" Value="0">TODAS</asp:ListItem>
            <asp:ListItem Value="1">POR COBRAR</asp:ListItem>
            <asp:ListItem Value="3">PAGOS PARCIALES</asp:ListItem>
            <asp:ListItem Value="2">PAGADAS</asp:ListItem>
        </asp:RadioButtonList></td>
                    <td style="width: 130px">
                        <asp:TextBox ID="txtfecha1" runat="server" Width="100px"></asp:TextBox></td>
                    <td style="width: 130px">
                        <asp:TextBox ID="txtfecha2" runat="server" Width="100px"></asp:TextBox></td>
                </tr>
            </table>
            &nbsp;
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" ClearMaskOnLostFocus="False"
                Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha1">
            </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" ClearMaskOnLostFocus="False"
                Mask="99/99/9999" MaskType="Date" TargetControlID="txtfecha2">
            </ajaxToolkit:MaskedEditExtender>
            <hr />
            <br />
            <asp:LinkButton ID="lnkExporta" runat="server">Exporta a Excel</asp:LinkButton></asp:Panel>
<div>
    <table border="0" cellpadding="0" cellspacing="0" style="width: 897px">
        <tr>
            <td align="left" style="width: 667px">
                <asp:Panel ID="btncerrar" runat="server" Visible="False" Width="125px">
                    <img src="imagenes/door_out.png" /><asp:Button ID="Button1" runat="server" Font-Size="9pt"
                        ForeColor="Red" Text="Salir" Width="61px" /></asp:Panel>
                </td>
            <td align="right" style="width: 115px">
            </td>
        </tr>
        <tr>
            <td style="width: 667px" align="left">
                <asp:Label ID="lblclientes" runat="server" Text="Cliente :"></asp:Label>
        <asp:DropDownList ID="cmbclientes" runat="server" Width="665px" AutoPostBack="True">
        </asp:DropDownList></td>
            <td align="right" style="width: 115px">
                </td>
        </tr>
    </table>
</div>
        <asp:DataGrid ID="gridFacturasimP" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1" ForeColor="Black"
            GridLines="None" Width="900px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="Control" />
            <Columns>
                <asp:TemplateColumn HeaderText="FACTURA">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton2" runat="server" CommandName="detalles" Font-Bold="True"
                            ForeColor="Red" Text='<%# Bind("num_factura") %>'>LinkButton</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="num_factura" HeaderText="No.FACTURA" Visible="False">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="fecha" HeaderText="FECHA">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="nombre" HeaderText="CLIENTE">
                    <ItemStyle Height="30px" Width="350px" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="abono" DataFormatString="{0:C}" HeaderText="ABONO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importeCosto" DataFormatString="{0:C}" HeaderText="IMPORTES">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importe" DataFormatString="{0:C}" HeaderText="FACTURADO">
                    <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                        Font-Underline="False" HorizontalAlign="Right" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" CommandName="cancelar" Font-Bold="False"
                            Font-Names="Calibri" Font-Size="12px" ForeColor="Red">CANCELAR</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn Visible="False">
                    <ItemTemplate>
                        <img src="imagenes/arrow_rotate_anticlockwise.png" /><asp:LinkButton ID="LinkButton4"
                            runat="server" CommandName="regresar" Font-Names="Calibri" Font-Size="12px" ForeColor="Black">Reg. Factura</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton3" runat="server" CommandName="verfac" Font-Names="Calibri"
                            Font-Size="12px" ForeColor="#1B3774">Ver Factura</asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#FC0200" Font-Bold="False" ForeColor="White" />
        </asp:DataGrid><br />
        <asp:DataGrid ID="griddetalles" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1" DataKeyField="idabono"
            ForeColor="Black" GridLines="None" Visible="False" Width="900px">
            <FooterStyle BackColor="Tan" />
            <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <AlternatingItemStyle BackColor="Control" />
            <Columns>
                <asp:BoundColumn DataField="idcita" HeaderText="# Recibo" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="elnombre" HeaderText="CLIENTE" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="descripcion" HeaderText="TERAPIA" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="fecha" HeaderText="FECHA" ReadOnly="True"></asp:BoundColumn>
                <asp:BoundColumn DataField="abono" HeaderText="ABONO" DataFormatString="{0:C}">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:BoundColumn DataField="importe" HeaderText="IMPORTE" DataFormatString="{0:C}" ReadOnly="True">
                    <ItemStyle HorizontalAlign="Center" />
                </asp:BoundColumn>
                <asp:TemplateColumn>
                    <ItemTemplate>
                        <img src="imagenes/money.png" /><asp:Button ID="Button2" runat="server" Text="Editar Abono" CommandName="abonar" Font-Size="8pt" Width="81px" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/imagenes/bullet_disk.png" CommandName="grabar" />
                        &nbsp;
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/imagenes/cancel.png" CommandName="cancelar" />
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn DataField="idcliente" Visible="False"></asp:BoundColumn>
            </Columns>
            <HeaderStyle BackColor="#FC0200" Font-Bold="False" ForeColor="White" />
        </asp:DataGrid>
        <asp:Panel ID="Panel1" runat="server" Visible="False" Width="800px">
            &nbsp; &nbsp;
        </asp:Panel>
        <asp:Panel ID="pnlcancela" runat="server" Visible="False" Width="557px">
            <asp:Label ID="Label2" runat="server" Text="SUSTITUIDAD POR : "></asp:Label>
            <asp:TextBox ID="txtsustituye" runat="server" MaxLength="75" Width="287px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtsustituye"
                ErrorMessage="*" ValidationGroup="cancel"></asp:RequiredFieldValidator><br />
            <br />
            <img src="imagenes/bullet_disk.png" /><asp:Button ID="Button3" runat="server" Font-Size="9pt"
                ForeColor="Red" Text="Grabar Cancelacion" ValidationGroup="cancel" Width="121px" />
            <img src="imagenes/cancel.png" /><asp:Button ID="Button5" runat="server" Font-Size="9pt"
                ForeColor="Red" Text="Cancelar" Width="68px" /></asp:Panel>
            <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lbldb" runat="server" Visible="False"></asp:Label>
            <input id="tempidcliente" type="hidden" runat="server" />
            <cc1:messagebox ID="Messagebox1" runat="server" />
    </div>
    </form>
</center>

</body>
</html>
