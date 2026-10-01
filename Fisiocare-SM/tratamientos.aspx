<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tratamientos.aspx.vb" Inherits="tratamientos" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
</style>
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/baner1.png" /><br />
        <table width="900px" border="1" cellpadding="0" cellspacing="1" bordercolor="#FFFFFF">
          <tr bordercolor="#7b7b7b" bgcolor="#fc0200">
            <td align="left" style="width: 610px">
                </td>
              <td colspan="2" align="right" style="width: 200px">
                  &nbsp;</td>
          </tr>
      </table>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <br />
        <table border="1" cellpadding="4" cellspacing="0" style="width: 900px; border-style:solid; border-width:2px; border-color:#ece9d8; border-bottom-width:4px;">
            <tr>
                <td style="font-size: 12pt; color: #1a3773; font-family: CALIBRI; background-color: #f4f4f4; width: 471px;">
                    PACIENTE</td>
                <td style="font-size: 12pt; color: #1a3773; font-family: CALIBRI; background-color: #f4f4f4">
                    PROTOCOLOS</td>
            </tr>
            <tr>
                <td style="width: 471px">
                    <asp:TextBox ID="txtNombusca" runat="server" AutoPostBack="True" Width="421px"></asp:TextBox>&nbsp;<asp:Button
                        ID="btnOtro" runat="server" CssClass="boton" ForeColor="Red" Height="22px" Text="..."
                        Width="35px" /></td>
                <td>
                    <asp:DropDownList ID="cmbProtocolos" runat="server" AutoPostBack="True" Width="392px" Enabled="False">
                    </asp:DropDownList></td>
            </tr>
        </table>
                    <asp:DataGrid ID="gTratamientos" DataKeyField="id" runat="server" AutoGenerateColumns="False" Width="900px" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px">
                        <Columns>
                            <asp:BoundColumn DataField="tratamiento" HeaderText="Tratamiento"></asp:BoundColumn>
                            <asp:BoundColumn DataField="observaciones" HeaderText="Observaciones"></asp:BoundColumn>
                            <asp:TemplateColumn HeaderText="Vigente">
                                <ItemTemplate>
                                    <asp:CheckBox ID="CheckBox1" runat="server" Checked='<%# Bind("vigente") %>' Enabled="False" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                                <ItemTemplate>
                                    <asp:LinkButton ID="LinkButton2" runat="server" CommandName="seleccionar">Seleccionar</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>               
                        </Columns>
                        <HeaderStyle BackColor="#F4F4F4" ForeColor="#1B3774" />
                    </asp:DataGrid>
        <hr />
                        <input id="hfIdtratamiento" type="hidden" runat="server" /><asp:Panel ID="Panel1" runat="server" Visible="False" Width="900px">
              <div style="text-align:left; width:890px; padding-bottom:3px">
                    <asp:Button ID="btnTrata" runat="server" CssClass="boton" ForeColor="Gray" Text="Tratamiento" Width="150px" />
                    <asp:Button ID="btnContra" runat="server" CssClass="boton" ForeColor="Red" Text="Contraindicaciones" Width="150px" />
                    <asp:Button ID="btnObserva" runat="server" CssClass="boton" ForeColor="Red" Text="Observaciones" Width="150px" />
                </div> <asp:Label ID="lbltitulo" runat="server" BackColor="#F4F4F4" BorderColor="#ECE9D8" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Calibri" Font-Size="12pt" ForeColor="#1B3774" Width="890px">Tratamiento</asp:Label>
                    <br />
            <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" Visible="False" Height="100px" Width="885px"></asp:TextBox><asp:TextBox ID="txtContraIndica" runat="server" TextMode="MultiLine" Visible="False" Height="100px" Width="885px"></asp:TextBox><asp:TextBox ID="txtTratamiento" runat="server" TextMode="MultiLine" Visible="False" Height="100px" Width="885px"></asp:TextBox>
                            &nbsp;&nbsp;
                <br />
                <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align: left">
                    <tr>
                        <td style="width: 91px">
                                    Equipos Especiales:</td>
                        <td style="width: 185px">
                            <asp:CheckBox ID="chklassb" runat="server" Text="Lasser de Barrido" /></td>
                        <td>
                            <asp:TextBox ID="txtbarr" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 208px">
                            <asp:CheckBox ID="chkelec" runat="server" Text="Electroestimulación" /></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtelec" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 180px">
                            <asp:CheckBox ID="chkMaqr" runat="server" Text="Maquina mov. rodilla" /></td>
                        <td>
                            <asp:TextBox ID="txtrodi" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 91px">
                        </td>
                        <td style="width: 185px">
                            <asp:CheckBox ID="chkMaqh" runat="server" Text="Maquina mov. hombro" /></td>
                        <td>
                            <asp:TextBox ID="txthomb" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 208px">
                            <asp:CheckBox ID="chkTral" runat="server" Text="Tracción lumbar neumática" /></td>
                        <td style="width: 100px">
                            <asp:TextBox ID="txtlumb" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 180px">
                            <asp:CheckBox ID="chkTrac" runat="server" Text="Tracción cervical" /></td>
                        <td>
                            <asp:TextBox ID="txtcerv" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 91px; border-top: solid 2px #f4f4f4 ">
                            Medidas Analgesicas:</td>
                        <td style="width: 185px; border-top: solid 2px #f4f4f4">
                            <asp:CheckBox ID="chkComp" runat="server" Text="Compresas Calientes" /></td>
                        <td style="border-top: solid 2px #f4f4f4">
                            <asp:TextBox ID="txtcomp" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 208px; border-top: solid 2px #f4f4f4">
                            <asp:CheckBox ID="chkCrio" runat="server" Text="Crioterapia" /></td>
                        <td style="width: 100px; border-top: solid 2px #f4f4f4">
                            <asp:TextBox ID="txtcrio" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 180px;border-top: solid 2px #f4f4f4">
                            <asp:CheckBox ID="chkLass" runat="server" Text="Lasser Convencional" /></td>
                        <td style="border-top: solid 2px #f4f4f4">
                            <asp:TextBox ID="txtlass" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 91px">
                        </td>
                        <td style="width: 185px">
                            <asp:CheckBox ID="chkPara" runat="server" Text="Parafina" /></td>
                        <td>
                            <asp:TextBox ID="txtpara" runat="server" MaxLength="50" Width="70px"></asp:TextBox></td>
                        <td style="width: 208px">
                        </td>
                        <td style="width: 100px">
                        </td>
                        <td style="width: 180px">
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
                        <asp:RequiredFieldValidator
            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txttratamiento"
            ErrorMessage="Debe escribir el tratamiento" ValidationGroup="g"></asp:RequiredFieldValidator><br />
            <asp:Button ID="btnAnueva" runat="server" Text="Agegar Nueva Terapia" Width="200px" CssClass="boton" ForeColor="Red" />
                            &nbsp;&nbsp;
            <asp:Button ID="btnGnueva" runat="server" Text="Grabar Terapia Nueva" Width="200px" Visible="False" ValidationGroup="g" CssClass="boton" ForeColor="Red" />&nbsp;
                            &nbsp;
            <asp:Button ID="btnmodifica" runat="server" Text="Grabar Cambios en Terapia" Width="200px" ValidationGroup="g" CssClass="boton" ForeColor="Red" />&nbsp;
                            &nbsp;
            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" Visible="False" Width="200px" CssClass="boton" ForeColor="Red" /><br />
                    <hr />
            <asp:Label ID="Label3" runat="server" BackColor="#F4F4F4" BorderColor="#ECE9D8" BorderStyle="Solid"
                BorderWidth="1px" Font-Bold="True" Font-Names="Calibri" Font-Size="12pt" ForeColor="#1B3774"
                Width="897px">BITACORA DEL PACIENTE</asp:Label>
        <asp:DataGrid ID="gSeguimiento" runat="server" DataKeyField="idcita" AutoGenerateColumns="False" Width="899px" CellPadding="4">
            <Columns>
                <asp:BoundColumn HeaderText="Fecha" DataField="fecha"><HeaderStyle Width="100px" /></asp:BoundColumn>
                <asp:TemplateColumn HeaderText="Observaciones">
                    <ItemTemplate>
                        <asp:LinkButton ID="LinkButton1" runat="server" Text="<%# Bind('observacion') %>"></asp:LinkButton>&nbsp;&nbsp;&nbsp;
                    </ItemTemplate>
                </asp:TemplateColumn>
            </Columns>
            <HeaderStyle BackColor="#F4F4F4" ForeColor="#1B3774" />
        </asp:DataGrid>
                        <input id="Hidden1" type="hidden" runat="server" /> 
                        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel2"
                            TargetControlID="Hidden1" OkControlID="btnOk" BackgroundCssClass="FondoAplicacion">
                        </ajaxToolkit:ModalPopupExtender>
                            &nbsp;<asp:Panel ID="Panel2" runat="server" BackColor="White" Visible="False">
              <asp:Panel ID="Panel5" runat="server" BorderColor="#1B3774" BorderStyle="Solid" BorderWidth="1px">
                  <asp:Label ID="lblUltima" runat="server" BackColor="#F4F4F4" BorderColor="#1B3774"
                      BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Names="Calibri" Font-Size="14pt"
                      ForeColor="#1B3774" Width="695px"></asp:Label>
                        <table border="0" cellpadding="4" cellspacing="0" style="width: 694px; text-align:left">
                            <tr>
                                <td style="width: 44px; text-align:left;">
                                    CHC</td>
                                <td style="width: 79px;text-align:left;">
                                    <asp:TextBox ID="txtchcV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 235px;text-align:left;">
                                    MAQINA MOV. PASIVO</td>
                                <td style="width: 131px;text-align:left;">
                                    <asp:TextBox ID="txtMakinaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px">
                                    LIBRES</td>
                                <td style="width: 160px">
                                    <asp:TextBox ID="txtlibresV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 104px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 44px;text-align:left;">
                                    US</td>
                                <td style="width: 79px;text-align:left;">
                                    <asp:TextBox ID="txtusV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 235px;text-align:left;">
                                    TRACCION CERV.</td>
                                <td style="width: 131px;text-align:left;">
                                    <asp:TextBox ID="txtcervV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px">
                                    ISOTERMICOS</td>
                                <td style="width: 160px">
                                    <asp:TextBox ID="txtIsotermicosV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 104px">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 44px; text-align:left;">
                                    CIF</td>
                                <td style="width: 79px;text-align:left;">
                                    <asp:TextBox ID="txtcifV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 235px; text-align:left;">
                                    VECTRA</td>
                                <td style="width: 131px; text-align:left;">
                                    <asp:TextBox ID="txtvectraV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px;text-align:left;">
                                    WILLIAMS</td>
                                <td style="width: 160px; text-align: left">
                                    <asp:TextBox ID="txtwilliamsV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 104px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 44px;text-align:left;">
                                    LASER</td>
                                <td style="width: 79px;text-align:left;">
                                    <asp:TextBox ID="txtlaserV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 235px;text-align:left;">
                                    CRIOTERAPIA</td>
                                <td style="width: 131px;text-align:left;">
                                    <asp:TextBox ID="txtcrioterapiaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px;text-align:left;">
                                    BICICLETA</td>
                                <td style="width: 160px; text-align: left">
                                    <asp:TextBox ID="txtbicicletaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 104px; text-align: left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 44px">
                                    PARAFINA</td>
                                <td style="width: 79px">
                                    <asp:TextBox ID="txtparafinaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 235px;text-align:left;">
                                    TRACCION LUMB.</td>
                                <td style="width: 131px;text-align:left;">
                                    <asp:TextBox ID="txtLumV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px">
                                    CAMINADORA</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtcaminadoraV" runat="server" MaxLength="50" Width="140px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 44px; height: 32px;">
                                </td>
                                <td style="width: 79px; height: 32px;">
                                </td>
                                <td style="width: 235px; text-align: left; height: 32px;">
                                    DIALERMIA</td>
                                <td style="width: 131px; text-align: left; height: 32px;">
                                    <asp:TextBox ID="txtdialermiaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px; height: 32px;">
                                    POLAINAS</td>
                                <td colspan="2" style="height: 32px">
                                    <asp:TextBox ID="txtPolainasV" runat="server" MaxLength="50" Width="140px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 44px; height: 32px;">
                                    LIGA</td>
                                <td colspan="3" style="height: 32px">
                                    &nbsp; A&nbsp;
                                    <asp:TextBox ID="txtAv" runat="server" Width="35px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; R&nbsp;
                                    <asp:TextBox ID="txtRv" runat="server" Width="35px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; V&nbsp;
                                    <asp:TextBox ID="txtVv" runat="server" Width="35px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; N&nbsp;
                                    <asp:TextBox ID="txtNv" runat="server" Width="35px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px; height: 32px;">
                                    TERAPIA OCUPACIONAL</td>
                                <td colspan="2" style="height: 32px">
                                    <asp:TextBox ID="txtocupacionalV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="4" rowspan="3">
                                    OBSERVACIONES<br />
                                    <asp:TextBox ID="txtUltima" runat="server" Height="78px" TextMode="MultiLine" Width="330px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                                <td style="width: 449px">
                                    POSTULARES</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtpostularesV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 449px">
                                    MARCHA</td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtmarchaV" runat="server" MaxLength="50" Width="65px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td colspan="3" style="text-align:center">
                <asp:Button ID="btnOk" runat="server" Text="OK" Width="113px" CssClass="boton" ForeColor="Red" /></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </asp:Panel>
        </asp:Panel>
        &nbsp;
        <br />
        <asp:Panel ID="Panel6" runat="server" BackColor="White" Height="350px" HorizontalAlign="Center"
            ScrollBars="Vertical" Style="padding-left: 4px; z-index: 100" Width="800px">
            <br />
            <asp:DataGrid ID="gBusca" runat="server" AutoGenerateColumns="False" CellPadding="7"
                DataKeyField="idcliente" Font-Names="Calibri" Font-Size="18pt" HorizontalAlign="center"
                Width="730px">
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre del Paciente">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" Font-Size="16pt" Text='<%# Bind("nombre") %>'></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                <ItemStyle Font-Size="20pt" HorizontalAlign="Left" />
            </asp:DataGrid>
            <div id="btnVacio" runat="server" visible="false">
                <asp:Label ID="Label2" runat="server" Font-Size="12pt" Text="No se encontraron Pacientes" ForeColor="Red"></asp:Label>
                <br />
                <br />
                <asp:Button ID="elbtnVacio" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar"
                    Width="177px" />
            </div>
        </asp:Panel>
        <input id="Hidden2" type="hidden" runat="server" />
        <input id="hfIdcliente" type="hidden" runat="server" />
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server" PopupControlID="panel6" TargetControlID="Hidden2" BackgroundCssClass="FondoAplicacion">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False" Width="103px"></asp:Label></div>
    </form>
    </center>
</body>
</html>
