<%@ Page Language="VB" AutoEventWireup="false" CodeFile="clientesCP.aspx.vb" Inherits="clientesCP" EnableEventValidation="false" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>FISIOCARE</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript">
    var ventanaCierra;
    function cierraventana(){
    ventanaCierra.close();
    }
    function miventana(){
    ventanaCierra=window.open("terapias.aspx?idcliente="+document.form1.cmbclientes.value,"ordenes","toolbar=no,width=650,height=600,scrollbars=yes");
    }
    function vacio(q) {   
        for ( i = 0; i < q.length; i++ ) {   
                if ( q.charAt(i) != " " ) {   
                        return true;   
                }   
        }   
        return false;
    }   

    function validanombre(){
    document.getElementById("Gbotones").style.visibility='hidden';
    document.getElementById("migif").style.visibility='visible';
    if (vacio(document.getElementById("txtpaterno").value)==false || vacio(document.getElementById("txtmaterno").value)==false || vacio(document.getElementById("txtnombre").value)==false){
    alert("LOS CAMPOS PATERNO, MATERNO Y NOMBRE NO DEBEN DE ESTAR VACIOS");
     document.getElementById("Gbotones").style.visibility='visible';
    document.getElementById("migif").style.visibility='hidden'
    return false;
    }
    else{
    return true;
    }
    }
    </script>
</head>
<body onload="javascript:if(history.length>0)history.go(+1)">
<center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Arial; font-size:9pt" align="center">
        <img src="imagenes/baner1.png" /><br />
      <table bgcolor="#fc0200" border="0" cellpadding="0" cellspacing="0" width="900px">
                <tr>
                    <td align="left" style="height: 16px">
                        <asp:LinkButton ID="LinkButton5" runat="server" ForeColor="White" Font-Names="calibri" Font-Size="16px" OnClientClick="cierraventana();">| Regresar a Agenda |</asp:LinkButton>&nbsp;                  &nbsp;<asp:LinkButton ID="LinkButton1" runat="server" Font-Names="calibri" Font-Size="16px"
                            ForeColor="White" Visible="False">| Agregar Cliente |</asp:LinkButton>
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="LinkButton4" runat="server" ForeColor="White" Font-Names="calibri" Font-Size="16px" Visible="False">| Buscar Cliente |</asp:LinkButton>&nbsp;
                  </td>
                    <td style="height: 16px">&nbsp;
                  </td>
                </tr>
      </table>
      <hr />        
        <div id="seleccionar" runat="server">
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
        <table border="0" cellpadding="3" cellspacing="0" style="width: 806px; text-align:center; border-color:#f4f4f4; border-width:1px; border-style:solid ">
          <tr style="background-color:#f4f4f4">
            <td style="width: 147px; text-align:left">
                <asp:Label ID="Label27" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                    Text="SELECCIONAR CLIENTE"></asp:Label></td>
              <td style=" background-color:White; text-align:left">
                <asp:DropDownList ID="cmbclientes" runat="server" Width="648px" AutoPostBack="True">
            </asp:DropDownList></td>
          </tr>
        </table>
        <hr />
        <div id="masdatos" runat="server" visible="false">
            <asp:Label ID="lblelHorario" runat="server" BackColor="#F4F4F4" BorderColor="Silver"
                BorderStyle="Solid" BorderWidth="1px" ForeColor="#1A3773" Width="798px" Font-Bold="False" Font-Names="calibri" Font-Size="12pt"></asp:Label>
            <asp:DataGrid ID="gridDias" runat="server" AutoGenerateColumns="False" CellPadding="2" ShowHeader="False" Style="font-size: 8pt; font-family: Calibri;
                text-align: left" Width="800px" BackColor="#F4F4F4" BorderColor="WhiteSmoke" BorderStyle="Solid" BorderWidth="1px" GridLines="Horizontal">
                <ItemStyle BackColor="#F4F4F4" BorderColor="#E0E0E0" BorderStyle="Solid" BorderWidth="1px" />
                <Columns>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                <asp:CheckBox ID="chk1" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk2" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                <asp:CheckBox ID="chk3" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk4" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                <asp:CheckBox ID="chk5" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:CheckBox ID="chk6" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                <asp:CheckBox ID="chk7" runat="server" ForeColor="#1A3773" Font-Names="arial" Font-Size="8pt" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="semanas_agendar" HeaderText="semanas" Visible="False"></asp:BoundColumn>
                </Columns>
                <AlternatingItemStyle BackColor="White" />
            </asp:DataGrid><br />
            
            <table border="0" cellpadding="3" cellspacing="0" style="width:800px;font-family:Calibri; font-size:11pt;border-color:#efefef; border-style:solid; border-width:1px">
                <tr align="center" style="background-color: #f4f4f4; text-align:center">
                    <td >
                        <asp:Label ID="Label13" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Duración Terapia" Width="118px" ForeColor="#1A3773"></asp:Label></td>
                    <td style="width: 158px; background-color:White; text-align:left">
                        <asp:DropDownList ID="cmbDuracion" runat="server" Width="154px">
                    </asp:DropDownList></td>
                    <td colspan="2" style="width: 106px">
                        <asp:Label ID="Label14" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Observaciones" Width="98px"></asp:Label></td>
                    <td colspan="1" style=" background-color:White; text-align:left">
                        <asp:TextBox ID="txtobservaciones" Style="border-bottom: #e0e0e0 2px solid" runat="server" MaxLength="50" Width="397px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                </tr>
                <tr>
                </tr>
                <tr>
                    <td style="text-align: center;" colspan="5">
                        <asp:Label ID="lblerror" runat="server" ForeColor="#1B3774" Font-Bold="True"></asp:Label>
                        &nbsp;&nbsp;&nbsp;</td>
                </tr>
            </table><hr />
            </div>
            
        </div>
        <div id="datos" runat="server">         
            <table  border="0" cellpadding="3" cellspacing="0" style="width:800px;font-family:Calibri; font-size:11pt;border-color:#efefef; border-style:solid; border-width:1px">
                <tr align="left" style="background-color: #f4f4f4; text-align:left">
                    <td style="width: 110px;">
                        <asp:Label ID="Label15" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Apellido Paterno" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtPaterno"  runat="server" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                            ReadOnly="True" Width="280px" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label20" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Celular" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtCelular" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="10" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="285px"></asp:TextBox></td>
                </tr>
                <tr align="left" style="background-color: #f4f4f4; text-align:left">
                    <td style="width: 110px">
                        <asp:Label ID="Label16" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Apellido Materno" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtmaterno" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="50" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="280px"></asp:TextBox></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label21" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="e-mail" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtemail" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                            BorderWidth="1px" MaxLength="150" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="285px"></asp:TextBox></td>
                </tr>
                <tr align="left" style="background-color: #f4f4f4; text-align:left">
                    <td style="width: 110px">
                        <asp:Label ID="Label17" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Nombre(s)" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtnombre" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="50" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="280px"></asp:TextBox></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label22" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Remitente" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:DropDownList ID="cmbDoctores" runat="server" Width="290px">
                        </asp:DropDownList></td>
                </tr>
                <tr align="left" style="background-color: #f4f4f4; text-align:left">
                    <td style="width: 110px">
                        <asp:Label ID="Label25" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Edad" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtedad" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                            BorderWidth="1px" MaxLength="10" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="280px"></asp:TextBox></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label23" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Cat. Costos" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:DropDownList ID="cmbCostos" runat="server" Width="290px">
                            
                        </asp:DropDownList></td>
                    </tr>
                <tr align="left" style="background-color: #f4f4f4; text-align:left">
                    <td style="width: 110px">
                        <asp:Label ID="Label18" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Sexo" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:DropDownList ID="cmbsexo" runat="server" Enabled="False" Width="285px">
                            <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                            <asp:ListItem Value="H">HOMBRE</asp:ListItem>
                            <asp:ListItem Value="M">MUJER</asp:ListItem>
                        </asp:DropDownList></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label24" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Activo" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:CheckBox ID="chkActivo" runat="server" Enabled="False" />&nbsp;</td>
                </tr>
                <tr align="left" style="background-color: #f4f4f4; text-align: left">
                    <td style="width: 110px">
                        <asp:Label ID="Label19" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Telefono Fijo" Width="110px"></asp:Label></td>
                    <td style="width: 300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtTelefono" runat="server" BackColor="White" BorderColor="White"
                            BorderStyle="Solid" BorderWidth="1px" MaxLength="10" ReadOnly="True" Style="border-bottom: #e0e0e0 2px solid"
                            Width="280px"></asp:TextBox></td>
                    <td style="width: 80px">
                        <asp:Label ID="Label26" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                            Text="Fecha Inicio" Width="75px"></asp:Label></td>
                    <td style="width:300px; background-color:White; text-align:left">
                        <asp:TextBox ID="txtFechaIni" runat="server" ReadOnly="True" Width="80px"  Style="border-bottom: #e0e0e0 2px solid" BackColor="White" BorderColor="White" BorderStyle="Solid" BorderWidth="1px"></asp:TextBox><img
                            id="micalendario" runat="server" src="imagenes/Calendar_scheduleHS.png" visible="true" /></td>
                </tr>
                <tr align="center">
                    <td colspan="4">
                        &nbsp; &nbsp;
                    </td>
                </tr>
                <tr align="center" style="background-color: #f4f4f4; text-align:center; color:White; color:#1a3773;">
                    <td colspan="4" style="border-top: solid 1px silver; border-bottom: solid 1px silver">
                    DATOS DE FACTURACION</td>
                </tr>
               
                <tr align="center" style="text-align:center">
                    <td colspan="4" style="height: 15px">
                        <asp:DropDownList ID="cmbDatosfac" runat="server" AutoPostBack="True" Enabled="False"
                            Width="795px" >
                        </asp:DropDownList></td>
                </tr>
                <tr align="left" style="text-align:left">
                    <td colspan="4">
                        <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                            <tr>
                                <td style="width: 61px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label3" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Nombre" Width="60px"></asp:Label></td>
                                <td style="width: 178px">
                                    <asp:TextBox ID="txtRazonSocial" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"
                                        Width="370px" ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 62px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label8" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="C.P." Width="60px"></asp:Label></td>
                                <td style="width: 165px">
                                    <asp:TextBox ID="txtcp" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid" Width="270px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 61px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label4" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="R.F.C." Width="60px"></asp:Label></td>
                                <td style="width: 178px">
                                    <asp:TextBox ID="txtrfc" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid" Width="370px" ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 62px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label9" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Municipio" Width="60px"></asp:Label></td>
                                <td style="width: 165px">
                                    <asp:TextBox ID="txtmunicipio" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"
                                        Width="270px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 61px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label5" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Calle" Width="60px"></asp:Label></td>
                                <td style="width: 178px">
                                    <asp:TextBox ID="txtcalle" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid" Width="370px" ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 62px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label10" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Localidad" Width="60px"></asp:Label></td>
                                <td style="width: 165px">
                                    <asp:TextBox ID="txtciudad" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"
                                        Width="270px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 61px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label6" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="No. Ext." Width="60px"></asp:Label></td>
                                <td style="width: 178px">
                                    <asp:TextBox ID="txtnoext" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid" Width="370px" ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 62px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label11" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Estado" Width="60px"></asp:Label></td>
                                <td style="width: 165px">
                                    <asp:TextBox ID="txtestado" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"
                                        Width="270px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td style="width: 61px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label7" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="Colonia" Width="60px"></asp:Label></td>
                                <td style="width: 178px">
                                    <asp:TextBox ID="txtcolonia" runat="server" BackColor="White" BorderColor="White"
                                        BorderStyle="Solid" BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid"
                                        Width="370px" ReadOnly="True"></asp:TextBox></td>
                                <td style="width: 62px; color: #1a3773; background-color: #f4f4f4; text-align: left">
                                    <asp:Label ID="Label12" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                        Text="País" Width="60px"></asp:Label></td>
                                <td style="width: 165px">
                                    <asp:TextBox ID="txtpais" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                        BorderWidth="1px" Style="border-bottom: #e0e0e0 2px solid" Width="270px" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                        </table>
                        <br />
                        <table border="0" cellpadding="0" cellspacing="0" style="width: 769px">
                            <tr>
                                <td style="width: 158px; height: 18px">
                                    <asp:Label ID="Label1" runat="server" BackColor="#F4F4F4" ForeColor="#1A3773" Text="Forma de Pago"
                                        Width="230px"></asp:Label></td>
                                <td style="width: 428px; height: 18px">
                                    <asp:Label ID="Label2" runat="server" BackColor="#F4F4F4" BorderColor="#E0E0E0" ForeColor="#1A3773"
                                        Text="No. Cuenta" Width="155px"></asp:Label></td>
                            </tr>
                            <tr>
                                <td style="width: 158px">
                                    <asp:DropDownList ID="cmbformapago" runat="server" Width="230px" Enabled="False">
                                    </asp:DropDownList></td>
                                <td style="width: 428px">
                                    <asp:TextBox ID="txtcuenta" runat="server" ReadOnly="True"></asp:TextBox></td>
                            </tr>
                        </table>
                        <hr />
                    </td>
                </tr>
                <tr align="center" bordercolor="#7b7b7b">
                    <td colspan="4" style="border-left-color: #7b7b7b; border-bottom-color: #7b7b7b;
                        color: white; border-top-color: #7b7b7b; background-color: red;
                        border-right-color: #7b7b7b;">
                       
                        <table border="0" cellpadding="3" cellspacing="0" style="width: 100%">
                                <tr>
                                    <td style="text-align:left">
                                     <div id="botones" runat="server">
                            &nbsp;<input id="Button1" class="boton" onclick="miventana();" type="button" style="font-size:9pt; color:Red; height:20px; width:130px; font-weight:normal; visibility:hidden " value="Ver Terapias" />
                            &nbsp; &nbsp; &nbsp;&nbsp;
                            
                            <asp:Button ID="lnkModifica" runat="server" Font-Bold="False" Font-Size="9pt" ForeColor="Red" Text="Modificar Datos" Width="130px" CssClass="boton" Height="20px" Visible="False" />
                            
                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style=" border-top: solid 4px white; text-align:right"> 
                                         <table border="0" cellpadding="0" cellspacing="0" style="width:100%; text-align:right">
                                             <tr>
                                                 <td style="width: 469px"> <div id="migif" style="visibility: hidden">
                            <img src="imagenes/loader.gif" /></div></td>
                                                 <td style="width: 277px">
                                                 <div id="Gbotones" style="visibility: visible">
                          <asp:Button ID="lnkguardar" runat="server" Font-Bold="False" Font-Size="9pt" ForeColor="Red"
                                OnClientClick="javascript:return validanombre();" Text="AGENDAR" Width="130px" CssClass="boton" Height="20px" />&nbsp;&nbsp;&nbsp; 
                        <asp:Button ID="lnkcancelar" runat="server" Font-Bold="False" Font-Size="9pt" ForeColor="Red"
                                Text="CANCELAR" Visible="False" Width="130px" CssClass="boton" Height="20px" /></div>
                                                 </td>
                                             </tr>
                                         </table>
                                      </td>
                                </tr>
                            </table>
                    </td>
                </tr>
            </table>
        </div>
      <div id="busca" runat="server">
            <table border="0" cellpadding="0" cellspacing="0" style="width: 800px">
                              <tr>
                                  <td style=" vertical-align:top">
                  <table border="0" cellpadding="3" cellspacing="0"  style="width: 350px">
                    <tr >
                      <td style="width: 73px; background-color:#f4f4f4"><asp:Label ID="Label28" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                              Text="Apellido Paterno" Width="110px"></asp:Label></td>
                      <td style="text-align:left">
                          <asp:TextBox ID="txtPaternoB" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                              BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                              Width="220px"></asp:TextBox></td>
                    </tr>
                      <tr>
                          <td style="width: 73px;background-color:#f4f4f4">
                              <asp:Label ID="Label29" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                  Text="Apellido Materno" Width="110px"></asp:Label></td>
                          <td style="text-align:left"> 
                              <asp:TextBox ID="txtMaternoB" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                  BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                                  Width="220px"></asp:TextBox></td>
                      </tr>
                      <tr>
                          <td style="width: 73px;background-color:#f4f4f4">
                              <asp:Label ID="Label30" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                                  Text="Nombre(s)" Width="110px"></asp:Label></td>
                          <td style="text-align:left">
                              <asp:TextBox ID="txtnombreB" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                                  BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                                  Width="220px"></asp:TextBox></td>
                      </tr>
                    <tr>
                      <td colspan="2" style="text-align:center">
                          <br />
                          <asp:Button ID="LinkButton2" runat="server" Font-Bold="False" Font-Size="9pt" ForeColor="Red"
                                Text="Buscar" Width="130px" CssClass="boton" Height="20px" /></td>
                    </tr>
          </table>
                                  </td>
                                  <td style="width: 100px">
                          <asp:DataGrid ID="gridClientes" runat="server" AutoGenerateColumns="False"
                              BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="2" CellSpacing="1"
                              ForeColor="Black" GridLines="None" Width="450px" DataKeyField="idcliente">
                              <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                              <AlternatingItemStyle BackColor="#F4F4F4" />
                              <Columns>
                                  <asp:BoundColumn DataField="elnombre" HeaderText="NOMBRE" >
                                      <ItemStyle Font-Names="calibri"  Font-Size="14px" />
                                  </asp:BoundColumn>
                                  <asp:TemplateColumn>
                                      <ItemTemplate>
                                          <asp:LinkButton ID="LinkButton3" runat="server" ForeColor="Red">| VER |</asp:LinkButton>
                                      </ItemTemplate>
                                  </asp:TemplateColumn>
                              </Columns>
                              <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" />
                              <ItemStyle Font-Bold="False" />
                          </asp:DataGrid></td>
                              </tr>
                          </table>
      </div>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy" PopupButtonID="micalendario"
            TargetControlID="txtFechaini" Enabled="False">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFechaini">
        </ajaxToolkit:MaskedEditExtender>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" runat="server"
            FilterMode="InvalidChars" InvalidChars='-, !"#$%&/()=?' TargetControlID="txtrfc">
        </ajaxToolkit:FilteredTextBoxExtender>
<cc1:messagebox ID="Messagebox1" runat="server" />
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
            FilterType="Numbers" TargetControlID="txtTelefono">
        </ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
            FilterType="Numbers" TargetControlID="txtCelular">
        </ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" runat="server"
            FilterType="Numbers" TargetControlID="txtedad">
        </ajaxToolkit:FilteredTextBoxExtender>
            <asp:Label ID="lblfila" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblposicion" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblhorario" runat="server" Text="Label" Visible="False"></asp:Label>
            <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"></asp:Label></div>
    </form>
</center>

</body>
</html>
