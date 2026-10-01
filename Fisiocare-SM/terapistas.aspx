<%@ Page Language="VB" AutoEventWireup="false" CodeFile="terapistas.aspx.vb" Inherits="terapistas" %>
<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Fisiocare</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
            function botonProtocolo() {
                document.getElementById("divBtnProtocolo").style.visibility='hidden';
                document.getElementById("migifP").style.visibility='visible';
            }
            
            function finTratamiento() {
                document.getElementById("divbtnTratamiento").style.visibility='hidden';
                document.getElementById("migifT").style.visibility='visible';
            }
            
    </script>
    
</head>
<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Calibri; font-size:10pt" align="center">
        <img src="imagenes/baner1.png" /><br />
        <table width="900px" border="1" cellpadding="0" cellspacing="1" bordercolor="#FFFFFF">
          <tr bordercolor="#7b7b7b" bgcolor="#fc0200">
            <td align="left" style="width: 610px">
                &nbsp;</td>
              <td colspan="2" align="right" style="width: 200px">
                  &nbsp;</td>
          </tr>
      </table>
        <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
        </ajaxToolkit:ToolkitScriptManager>
        <br />
        <table border="0" cellpadding="4" cellspacing="0" style="width: 100%; text-align:left; vertical-align:top">
            <tr>
                <td style="width: 399px">
                    <asp:Label ID="Label5" runat="server" Font-Size="12pt" Text="Usuario"></asp:Label></td>
                <td style="width: 479px">
                        <asp:Label ID="lblContraseña" runat="server" Font-Size="12pt" Text="Contraseña" Visible="False"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 399px">
        <asp:DropDownList ID="cmbterapistas" runat="server" AutoPostBack="True" Width="395px" Font-Names="Calibri" Font-Size="18pt">
        </asp:DropDownList></td>
                <td style="width: 479px">
                    <asp:Panel ID="Panel3" runat="server" Visible="False" Width="450px">
                        <asp:TextBox ID="txtpass" runat="server" Width="293px" TextMode="Password" AutoPostBack="True" Font-Names="Calibri" Font-Size="18pt"></asp:TextBox><asp:Label ID="lblIncorrecto" runat="server" ForeColor="Red"></asp:Label></asp:Panel>
                </td>
            </tr>
        </table>
        <asp:Panel ID="Panel4" runat="server" Visible="False" Width="900px" style="text-align:left;">
            <table border="0" cellpadding="4" cellspacing="0" style="width:900px; text-align:left">
                <tr>
                    <td style="WIDTH: 400px">
                        <asp:Label ID="Label3" runat="server" Font-Size="12pt" Text="Paciente"></asp:Label></td>
                    <td style="">
                        <asp:Label ID="lblselfecha" runat="server" Font-Size="12pt" Text="Seleccionar fecha de Terapia"
                            Visible="False"></asp:Label></td>
                    <td>
                        </td>
                </tr>
                <tr>
                    <td style="WIDTH: 400px">
                        <asp:TextBox ID="txtNombusca" runat="server" Width="390px" AutoPostBack="True" Font-Names="Calibri" Font-Size="18pt"></asp:TextBox></td>
                    <td style="">
                        <asp:DropDownList ID="cmbFechas" runat="server" Width="395px" AutoPostBack="True" Font-Names="Calibri" Font-Size="18pt" Visible="False">
                        </asp:DropDownList></td>
                    <td>
                    <asp:Button ID="btnOtro" runat="server" Height="33px" Text="..." Width="40px" Visible="False" CssClass="boton" ForeColor="Red" /></td>
                </tr>
            </table>
                        </asp:Panel>
        <br />
        <asp:Panel ID="Panel1" runat="server" Visible="False">
            <asp:Label ID="Label4" runat="server" Text="INDICACIONES DEL TRATAMIENTO" BackColor="#F4F4F4" BorderColor="#1B3774" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True" Font-Size="12pt" ForeColor="#1B3774" Width="899px"></asp:Label>
        <table border="0" cellpadding="3" cellspacing="0" style="width: 900px; border-top:inset 1px #f4f4f4;">
            <tr>
                <td style=" text-align:center; vertical-align:top; border-left:inset 1px #f4f4f4;">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                <div style="text-align:left; width:890px; padding-bottom:3px">
                    <asp:Button ID="btnTrata" runat="server" CssClass="boton" ForeColor="Gray" Text="Tratamiento" Width="150px" />
                    <asp:Button ID="btnContra" runat="server" CssClass="boton" ForeColor="Red" Text="Contra Indicaciones" Width="150px" />
                    <asp:Button ID="btnObserva" runat="server" CssClass="boton" ForeColor="Red" Text="Observaciones" Width="150px" />
                </div>    
                    <asp:Label ID="lbltitulo" runat="server" BackColor="#F4F4F4" BorderColor="#ECE9D8" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Calibri" Font-Size="12pt" ForeColor="#1B3774" Width="890px">Tratamiento</asp:Label>
                    <asp:TextBox ID="txtvisible" runat="server" Height="150px" ReadOnly="True" TextMode="MultiLine"
                        Width="885px" Wrap="False"></asp:TextBox><br />
            <asp:TextBox ID="txtobservacion" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox><asp:TextBox ID="txtContraIndica" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox><asp:TextBox ID="txtTratamiento" runat="server" TextMode="MultiLine" Visible="False"></asp:TextBox>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    &nbsp;
                    <br />
                    <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align: left">
                        <tr>
                            <td style="width: 91px">
                                Equipos Especiales:</td>
                            <td style="width: 185px">
                                <asp:CheckBox ID="chklassb" runat="server" Enabled="False" Text="Lasser de Barrido" /></td>
                            <td>
                                <asp:TextBox ID="txtbarr" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 208px">
                                <asp:CheckBox ID="chkelec" runat="server" Enabled="False" Text="Electroestimulación" /></td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtelec" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 180px">
                                <asp:CheckBox ID="chkMaqr" runat="server" Enabled="False" Text="Maquina mov. rodilla" /></td>
                            <td>
                                <asp:TextBox ID="txtrodi" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px">
                                </td>
                            <td style="width: 185px">
                                <asp:CheckBox ID="chkMaqh" runat="server" Enabled="False" Text="Maquina mov. hombro" /></td>
                            <td>
                                <asp:TextBox ID="txthomb" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 208px">
                                <asp:CheckBox ID="chkTral" runat="server" Enabled="False" Text="Tracción lumbar neumática" /></td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtlumbD" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 180px">
                                <asp:CheckBox ID="chkTrac" runat="server" Enabled="False" Text="Tracción cervical" /></td>
                            <td>
                                <asp:TextBox ID="txtcervD" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px">
                                Medidas Analgesicas:</td>
                            <td style="width: 185px">
                                <asp:CheckBox ID="chkComp" runat="server" Enabled="False" Text="Compresas Calientes" /></td>
                            <td>
                                <asp:TextBox ID="txtcomp" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 208px">
                                <asp:CheckBox ID="chkCrio" runat="server" Enabled="False" Text="Crioterapia" /></td>
                            <td style="width: 100px">
                                <asp:TextBox ID="txtcrio" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                            <td style="width: 180px">
                                <asp:CheckBox ID="chkLass" runat="server" Enabled="False" Text="Lasser Convencional" /></td>
                            <td>
                                <asp:TextBox ID="txtlass" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td style="width: 91px">
                                </td>
                            <td style="width: 185px">
                                <asp:CheckBox ID="chkPara" runat="server" Enabled="False" Text="Parafina" /></td>
                            <td>
                                <asp:TextBox ID="txtpara" runat="server" MaxLength="50" ReadOnly="True" Width="70px"></asp:TextBox></td>
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
            </td>
            </tr>
        </table>
        <hr style="width:900px" />
            <asp:Label ID="lblUltima" runat="server" BackColor="#F4F4F4" BorderColor="#1B3774" BorderStyle="Solid"
                        BorderWidth="1px" Font-Names="Calibri" Font-Size="14pt" ForeColor="#1B3774" Width="898px" Font-Bold="True">Ultima Sesión</asp:Label>
            <asp:Panel ID="Panel5" runat="server" BorderColor="#1B3774" BorderStyle="Solid" BorderWidth="1px">
                <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:left">
                    <tr>
                        <td style="width: 80px">
                            CHC</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtchcV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            MAQINA MOV. PASIVO</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtMakinaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            DIALERMIA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtdialermiaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            US</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtusV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            TRACCION CERV.</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtcervV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            LIBRES</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtlibresV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            CIF</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtcifV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            VECTRA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtvectraV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            ISOTERMICOS</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtIsotermicosV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            LASER</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtlaserV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            CRIOTERAPIA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtcrioterapiaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            WILLIAMS</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtwilliamsV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            PARAFINA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtparafinaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            TRACCION LUMB.</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtLumV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            BICICLETA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtbicicletaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            CAMINADORA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtcaminadoraV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            POLAINAS</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtPolainasV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                            TERP. OCUPA.</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtocupacionalV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td style="width: 80px">
                            POSTULARES
                        </td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtmarchaV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 130px">
                            MARCHA</td>
                        <td style="width: 185px">
                                    <asp:TextBox ID="txtpostularesV" runat="server" MaxLength="50" Width="175px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 80px">
                        </td>
                        <td style="width: 185px">
                        </td>
                    </tr>
                </table>
                <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:left; vertical-align:top">
                    <tr>
                        <td style="width: 165px; vertical-align:top">
                            LIGAS</td>
                        <td style="width: 605px; vertical-align:top">
                        A&nbsp;
                                    <asp:TextBox ID="txtAv" runat="server" Width="35px" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; R&nbsp;
                                    <asp:TextBox ID="txtRv" runat="server" Width="35px" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; V&nbsp;
                                    <asp:TextBox ID="txtVv" runat="server" Width="35px" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox>
                                    &nbsp; &nbsp; N&nbsp;
                                    <asp:TextBox ID="txtNv" runat="server" Width="35px" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                        <td style="width: 97px; vertical-align:top">
                            OBSERVACIONES</td>
                        <td style="width: 406px">
                                    <asp:TextBox ID="txtUltima" runat="server" Height="75px" TextMode="MultiLine" Width="400px" ReadOnly="True" Font-Names="Calibri" Font-Size="10pt"></asp:TextBox></td>
                    </tr>
                </table>
                <br />
            <asp:Button ID="btnleido" runat="server" Text="He leido las indicaciones" Width="223px" Visible="False" CssClass="boton" ForeColor="Red" Height="37px" /></asp:Panel>
        <input id="hfidcita" type="hidden" runat="server" /><input id="hffecha" type="hidden" runat="server" /></asp:Panel>
        <asp:Panel ID="Panel2" runat="server" Visible="False" Width="899px">
            <asp:Label ID="lblfecha" runat="server" BackColor="#F4F4F4" BorderColor="#ECE9D8" BorderStyle="Solid"
                BorderWidth="1px" Font-Names="Calibri" Font-Size="12pt" ForeColor="#1B3774" Width="898px"></asp:Label><br />
            <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:left">
                <tr>
                    <td style="width: 80px">
                        CHC</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtchc" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow:auto; resize: none;" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        MAQUINA MOV. PASIVO</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtmakina" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        DIALERMIA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtdialermia" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        US</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtus" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        TRACCION CERV.</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtcerv" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        LIBRES</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtlibres" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        CIF</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtcif" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        VECTRA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtvectra" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        ISOTERMICOS</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtisotermicos" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        LASER</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtlaser" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        CRIOTERAPIA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtcrioterapia" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        WILLIAMS</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtwilliams" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        PARAFINA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtparafina" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        TRACCION LUMB.</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtlumb" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        BICICLETA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtbicicleta" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        CAMINADORA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtcaminadora" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        POLAINA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtpolainas" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                        TERP. OCUPA.</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtOcupacional" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                </tr>
                <tr>
                    <td style="width: 80px">
                        POSTULARES</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtpostulares" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 134px">
                        MARCHA</td>
                    <td style="width: 180px">
                        <asp:TextBox ID="txtMarcha" runat="server" BorderColor="LightSteelBlue" BorderStyle="Solid"
                            BorderWidth="1px" Font-Names="calbri" Font-Size="10pt" Height="17px" MaxLength="10"
                            Style="overflow: auto; resize: none" TextMode="MultiLine" Width="175px"></asp:TextBox></td>
                    <td style="width: 80px">
                    </td>
                    <td style="width: 180px">
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="4" cellspacing="0" style="width: 900px; text-align:left; vertical-align:top">
                <tr>
                    <td style="width: 165px; vertical-align:top">
                        LIGAS</td>
                    <td style="width: 605px; vertical-align:top">
                        A&nbsp;
                        <asp:TextBox ID="txtA" runat="server" Font-Names="Calibri" Font-Size="10pt" ReadOnly="True"
                            Width="35px"></asp:TextBox>
                        &nbsp; &nbsp; R&nbsp;
                        <asp:TextBox ID="txtR" runat="server" Font-Names="Calibri" Font-Size="10pt" ReadOnly="True"
                            Width="35px"></asp:TextBox>
                        &nbsp; &nbsp; V&nbsp;
                        <asp:TextBox ID="txtV" runat="server" Font-Names="Calibri" Font-Size="10pt" ReadOnly="True"
                            Width="35px"></asp:TextBox>
                        &nbsp; &nbsp; N&nbsp;
                        <asp:TextBox ID="txtN" runat="server" Font-Names="Calibri" Font-Size="10pt" ReadOnly="True"
                            Width="35px"></asp:TextBox></td>
                    <td style="width: 97px; vertical-align:top">
                        OBSERVACIONES</td>
                    <td style="width: 406px">
                        <asp:TextBox ID="txtObsesion" runat="server" Font-Names="Calibri" Font-Size="10pt" Height="75px" TextMode="MultiLine" Width="400px" Style="overflow: auto; resize: none"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="4" style="vertical-align: top; text-align:center">
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtObsesion"
                ErrorMessage="Favor de Escribir una Observación" ValidationGroup="g"></asp:RequiredFieldValidator><br />
                <div id="migifT" style="visibility: hidden">
                            <img src="imagenes/loader.gif" /></div>
                <div id="divbtnTratamiento"><asp:Button ID="Button2" runat="server" Text="Terminar Sesión" Width="221px" ValidationGroup="g" CssClass="boton" ForeColor="Red" OnClientClick="javascript:finTratamiento();" Height="32px" /> </div></td>
                </tr>
            </table>
            <br />
</asp:Panel>
                    <asp:Panel ID="Panel6" runat="server" HorizontalAlign="Center" style="padding-left:4px; z-index:100" BackColor="White" Width="800px" ScrollBars="Vertical" Height="350px">
            <br />
            <asp:DataGrid ID="gBusca" runat="server" DataKeyField="idcliente" AutoGenerateColumns="False" CellPadding="7"
                Width="730px" Font-Names="Calibri" Font-Size="18pt" HorizontalAlign="center">
                <Columns>
                    <asp:TemplateColumn HeaderText="Nombre del Paciente">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" Text='<%# Bind("nombre") %>' runat="server" Font-Size="16pt"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                <ItemStyle HorizontalAlign="Left" Font-Size="20pt" />
            </asp:DataGrid><div runat="server" visible="false" id="divProtocolos">
                <asp:Label ID="Label1" runat="server" BackColor="#F4F4F4" Font-Size="14pt" ForeColor="#1A3773"
                    Text="PROTOCOLOS" Width="524px"></asp:Label><br />
                        <asp:DropDownList ID="cmbProtocolos" runat="server" AutoPostBack="True" 
                            Width="526px" Font-Names="Calibri" Font-Size="18pt">
                        </asp:DropDownList>
                <br />
                <asp:Label ID="lblAvisoProtocolo" runat="server" ForeColor="Red" Text="Debe Elegir un Protocolo"
                    Visible="False"></asp:Label><br /><div id="migifP" style="visibility: hidden">
                            <img src="imagenes/loader.gif" /><br />
                        &nbsp;</div><div id="divBtnProtocolo">
                <asp:Button ID="btnProtocolo" runat="server" Text="Aceptar" ValidationGroup="protocolo" Width="210px" CssClass="boton" ForeColor="Red" OnClientClick="javascript:botonProtocolo();" Height="45px" />&nbsp;
                <asp:Button ID="Button3" runat="server" Text="Cancelar" ValidationGroup="protocolo" Width="210px" CssClass="boton" ForeColor="Red" Height="45px" /><br /></div>
                       </div> <br />
                        <div id="btnVacio" runat="server" visible="false">
                            <asp:Label ID="Label2" runat="server" Font-Size="12pt" Text="No se encontraron Pacientes"></asp:Label>
                            <br />
                            <br />
                        <asp:Button ID="elbtnVacio" runat="server" Text="Aceptar" Width="177px" CssClass="boton" ForeColor="Red" />
                        </div>
                        </asp:Panel>          
        <input id="hfModal" runat="server" type="hidden" />
        <input id="hfidCliente" runat="server" type="hidden" />&nbsp;
        <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="hfModal" BackgroundCssClass="FondoAplicacion" PopupControlID="Panel6">
        </ajaxToolkit:ModalPopupExtender>
        <br />
        <asp:Label ID="lblaviso" runat="server" BackColor="#F4F4F4" BorderColor="#ECE9D8"
            BorderStyle="Solid" BorderWidth="1px" Font-Names="Calibri" Font-Size="12pt" ForeColor="Red"
            Visible="False" Width="289px">No se encontraron fechas agendadas ....!!!!</asp:Label><cc1:messagebox
                id="Messagebox1" runat="server"></cc1:messagebox>
        </div>
    </form></center>
</body>
</html>
