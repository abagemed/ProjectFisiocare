<%@ Page Language="VB" AutoEventWireup="false" CodeFile="terapias.aspx.vb" Inherits="terapias" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>FISIOCARE</title>
    <!-- google font -->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet" type="text/css" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/material-design-iconic-font/2.2.0/css/material-design-iconic-font.min.css" />
    <!-- bootstrap -->
	<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Material Design Lite CSS -->
	<link href="assets/plugins/material/material.min.css" rel="stylesheet" />
	<link href="assets/css/material_style.css" rel="stylesheet" />
	<!-- Theme Styles -->
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css" />
	<link href="assets/css/theme-color.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/pages/formlayout.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/pages/typography.css" rel="stylesheet" type="text/css" />
    <!--select2-->
    <link href="assets/plugins/select2/select2.css" rel="stylesheet" type="text/css" />
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <style>
        .select2-container-multi .select2-choices {
            border: 1px solid #d2d6de;
        }
        .ajax__calendar_container
        {
            z-index:99999
        }
    </style>
    <script language="javascript" type="text/javascript">
    function impventana(){
    xterapia=document.getElementById("miIdterapia").value;
    window.open("impmedios.aspx?x="+xterapia,"nuevo","'toolbar=no,width=100,height=100,scrollbars=no,location=no,menubar=no'");
    }
    </script>  

    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
    <link href="/aspnet_client/System_Web/2_0_50727/CrystalReportWebFormViewer3/css/default.css"
        rel="stylesheet" type="text/css" />
</head>
<body class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark" onload="javascript:if(history.length>0)history.go(+1)">

    <form id="form1" runat="server">

         





        
        

        <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="page-bar">
                        <div class="page-title-breadcrumb">
                            <div class=" pull-left">
                                <div class="page-title">Terapias</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-search"></i>&nbsp;<a class="parent-item" href="#">Terapias</a>
                                </li>
                            </ol>
                        </div>
                    </div>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-box">
                                        <div class="card-head">
                                            <header><asp:Label ID="lblElnombre" runat="server" Text="Label" CssClass="h3"></asp:Label></header>
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            
                                            <!--BOTÓN VER-->
                                            <asp:Panel ID="pnlModificar" runat="server" Visible="False" CssClass="m-b-20">
                                                <asp:Button ID="bntFicha" runat="server" Text="ModificarFicha" CssClass="btn btn-info" />
                                                <asp:Button ID="bntimprimir" runat="server" Text="Imprimir" CssClass="btn btn-success" />
                                                <asp:DropDownList ID="cmbImpresoras" runat="server" CssClass="form-control" Visible="False"></asp:DropDownList>
                                            </asp:Panel>

                                            

                                            <div id="formato" runat="server"> 

                                                <div class="form-group row">
                                                    <asp:Label ID="Label14" runat="server" Text="Poliza" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtPoliza" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <asp:Label ID="Label1" runat="server" Text="Certificado" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtCertificado" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                     <asp:Label ID="Label2" runat="server" Text="No. Siniestro" CssClass="col-sm-3 control-label"></asp:Label>                    
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtNosiniestro" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <asp:Label ID="Label3" runat="server" Text="Edad Paciente" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtEdad" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <asp:Label ID="Label4" runat="server" Text="Responsable Gastos" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtResponsable" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                     <asp:Label ID="Label5" runat="server" Text="Medico Refiere" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                        <asp:TextBox ID="txtMedico" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <%-- <div class="form-group row">
                                                    <asp:Label ID="Label6" runat="server" Text="Diagnostico" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                       <asp:TextBox ID="txtDiagnostico" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtDiagnostico" ErrorMessage="Se requiere este campo" Style="font-weight: bold; font-size: 8pt; font-family: Arial" ValidationGroup="validacion"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>--%>

                                                <div class="form-group row">
                                                    <asp:Label ID="Label7" runat="server" Text="Fecha Inicio" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                       <asp:TextBox ID="txtFechainicio" runat="server" CssClass="form-control"></asp:TextBox>
                                                       <img id="micalendario" runat="server" src="imagenes/Calendar_scheduleHS.png" visible="true" />
                                                       <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtFechainicio"
                                                            ErrorMessage="*" MaximumValue="31/12/2999" MinimumValue="01/01/1999" Type="Date"
                                                            ValidationGroup="validacion"></asp:RangeValidator>
                                                        <asp:RequiredFieldValidator
                                                            ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFechainicio"
                                                            ErrorMessage="Se requiere este campo" Style="font-weight: bold; font-size: 8pt;
                                                            font-family: Arial" ValidationGroup="validacion"></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                     <asp:Label ID="Label8" runat="server" Text="No. Sesiones" CssClass="col-sm-3 control-label"></asp:Label>
                                                    <div class="col-sm-9">
                                                       <asp:TextBox ID="txtNosesiones" runat="server" CssClass="form-control"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-sm-3 control-label">Alergías/Padecimientos</label>
                                                    <div class="col-sm-9">
                                                        <input type="text" id="alergias" runat="server" placeholder="Alergías/Padecimientos" class="form-control"/>
                                                    </div>
                                                </div>
                                            
                                                <div class="form-group row">
                                                    <div class="col-sm-6">
                                                        <label>Indicaciones Médicas</label>
                                                        <textarea id="indicaciones_medicas"  runat="server" placeholder="Indicaciones del Medico" class="form-control" rows="5"></textarea>
                                                    </div>
                                                    <div class="col-sm-6">
                                                        <label>Contra indicaciones</label>
                                                         <textarea id="contra_indicaciones"  runat="server"  placeholder="Contra Indicaciones del Medico" class="form-control" rows="5"></textarea>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <label class="col-sm-4 control-label">Diagnóstico</label>
                                                    <div class="col-sm-6">
                                                        <input type="text" class="select2" id="diagnosticos" onchange="terapias(4)" />                                                    
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-12">
                                                        <table class="table table-striped table-hover" id="tabla_diagnosticos">
                                                            <thead>
                                                                <tr>
                                                                    <th>Diagnóstico</th>
                                                                    <th>Fecha</th>
                                                                    <th>Borrar</th>
                                                                </tr>
                                                            </thead>
                                                            <tbody></tbody>
                                                        </table>
                                                    </div>
                                                </div>

                                                <div class="form-group row">
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkBasica" runat="server"  Text="Terapia Básica" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkAdicional" runat="server" Text="Area Adicional" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkLaser" runat="server"  Text="Barrido Laser" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkMovimiento" runat="server" Text="Maq. de Movimiento" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkHospital" runat="server"  Text="Terapia Hospital" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkConsulta" runat="server"  Text="Consulta" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="chkMuscular" runat="server"  Text="Terapia Muscular"  />
                                                    </div>
                                                </div>
                                                
                                                <div class="row">
                                                    <asp:Panel ID="pnlImpejercicios" runat="server" Visible="False">
                                                        <asp:Button ID="Button1" runat="server" OnClientClick="impventana();" Text="IMPRIMIR EJERCICIOS" CssClass="btn btn-primary"  />
                                                    </asp:Panel>
                                                </div>

                                                <div class="modal-footer">
                                                    <asp:LinkButton ID="lnkgrabar" runat="server" CssClass="btn btn-success" ValidationGroup="validacion">GUARDAR DATOS</asp:LinkButton>
                                                    <asp:LinkButton ID="lnkregresar" runat="server" CssClass="btn btn-primary" ValidationGroup="validaDatos">REGRESAR</asp:LinkButton>
                                                </div>
                                                   
                                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"></asp:ScriptManager>
                                                <input id="miIdterapia" runat="server" type="hidden" />
      
           
                                              <table width="600" runat="server" visible="false" border="0" cellpadding="4" cellspacing="0" bordercolor="#FFFFFF" style=" width:600px">
          
                                                   <tr align="center">
                                                       <td colspan="3">
                                                           <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                               <ContentTemplate>
                                                        <asp:DataGrid ID="gridejercicios" runat="server" Width="495px" AutoGenerateColumns="False" Visible="False" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px">
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="MEDIOS FISICOS">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton1" Text='<%# Bind("texto") %>' runat="server" Font-Bold="True" ForeColor="Red" CommandName="medios"></asp:LinkButton>
                                                                        <asp:Label ID="lblmiidM" Text='<%# Bind("id") %>'  runat="server"  Visible="False"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="EJERCICIOS Y RUTINAS">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton2" Text='' runat="server" Font-Bold="True" ForeColor="Red" CommandName="ejercicios"></asp:LinkButton>
                                                                        <asp:Label ID="lblmiidE" runat="server" Text='' Visible="False"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="CONTRAINDICACIONES">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton ID="LinkButton4" Text='' runat="server" Font-Bold="True" ForeColor="Red" CommandName="otros"></asp:LinkButton>
                                                                        <asp:Label ID="lblmiidO" runat="server" Text='' Visible="False"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                            <HeaderStyle BackColor="Gray" ForeColor="White" />
                                                        </asp:DataGrid>&nbsp;
                                                                   <br />
                                                                   <table border="1" cellpadding="1" cellspacing="1" style="width: 499px">
                                                                       <tr>
                                                                           <td style="width: 203px; background-color:Red">
                                                                           <strong><font color="#ffffff">
                                                      MEDIOS FISICOS<br />
                
                                                    </font></strong>
                                                       <asp:Panel ID="pnleditaM" runat="server" Visible="False">
                                                            <img src="imagenes/page_white_edit.png" /><asp:Button ID="btneditaM" runat="server"
                                                                Font-Size="8pt" Text="Editar" OnClick="btneditaM_Click" />
                                                            <img src="imagenes/page_add.png" /><asp:Button ID="btnnuevoM" runat="server" Font-Size="8pt"
                                                                Text="Agregar" /></asp:Panel> <asp:Panel ID="pnlgrabaM" runat="server" Visible="False">
                                                            <img src="imagenes/bullet_disk.png" /><asp:Button ID="Button7" runat="server" Font-Size="8pt"
                                                                Text="Grabar" ValidationGroup="grabam" />
                                                            <img src="imagenes/cancel.png" /><asp:Button ID="Button8" runat="server" Font-Size="8pt"
                                                                Text="Cancelar" Width="56px" />
                                                        </asp:Panel>
                                                                           </td>
                                                                           <td style="width: 100px"><asp:TextBox ID="txtmedios" runat="server" Height="91px" TextMode="MultiLine" Width="286px"></asp:TextBox><br />
                                                        <asp:Label ID="lblidm" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmedios"
                                                            ErrorMessage="Debe Poner Datos" ValidationGroup="grabam" Width="162px"></asp:RequiredFieldValidator></td>
                                                                       </tr>
                                                                       <tr>
                                                                           <td style="width: 203px; background-color:Red">
                                                                           <strong><font color="#ffffff">
                                                      EJERCICIOS Y RUTINAS<br />
                                                        <asp:Panel ID="pnleditaE" runat="server" Visible="False">
                                                            <img src="imagenes/page_white_edit.png" /><asp:Button ID="btneditaE" runat="server"
                                                                Font-Size="8pt" Text="Editar" />
                                                            <img src="imagenes/page_add.png" /><asp:Button ID="btnnuevoE" runat="server" Font-Size="8pt"
                                                                Text="Agregar" /></asp:Panel>
                                                        <asp:Panel ID="pnlgrabaE" runat="server" Visible="False">
                                                            <img src="imagenes/bullet_disk.png" /><asp:Button ID="Button5" runat="server" Font-Size="8pt"
                                                                Text="Grabar" ValidationGroup="grabae" />
                                                            <img src="imagenes/cancel.png" /><asp:Button ID="Button6" runat="server" Font-Size="8pt"
                                                                Text="Cancelar" Width="55px" /></asp:Panel>
                                                    </font></strong>
                                                                           </td>
                                                                           <td style="width: 100px">
                                                        <asp:TextBox ID="txtejercicios" runat="server" Height="103px" TextMode="MultiLine" Width="286px"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtejercicios"
                                                            ErrorMessage="Debe Poner Datos" ValidationGroup="grabae" Width="224px"></asp:RequiredFieldValidator><asp:Label ID="lblide" runat="server" Visible="False"></asp:Label></td>
                                                                       </tr>
                                                                       <tr>
                                                                           <td style="width: 203px; background-color:Red">
                                                                           <strong><span style="color: #ffffff">CONTRAINDICACIONES<br />
                                                            <asp:Panel ID="pnleditaO" runat="server" Visible="False">
                                                                <img src="imagenes/page_white_edit.png" /><asp:Button ID="btneditaO" runat="server"
                                                                    Font-Size="8pt" Text="Editar" />
                                                                <img src="imagenes/page_add.png" /><asp:Button ID="btnnuevoO" runat="server" Font-Size="8pt"
                                                                    Text="Agregar" /></asp:Panel>
                                                            <asp:Panel ID="pnlgrabaO" runat="server" Visible="False">
                                                                <img src="imagenes/bullet_disk.png" /><asp:Button ID="Button11" runat="server" Font-Size="8pt"
                                                                    Text="Grabar" ValidationGroup="grabao" />
                                                                <img src="imagenes/cancel.png" /><asp:Button ID="Button12" runat="server" Font-Size="8pt"
                                                                    Text="Cancelar" Width="56px" /></asp:Panel>
                                                        </span></strong>
                                                                           </td>
                                                                           <td style="width: 100px">
                                                        <asp:TextBox ID="txtotros" runat="server" Height="96px" TextMode="MultiLine" Width="286px"></asp:TextBox><br />
                                                        <asp:Label ID="lblido" runat="server" Visible="False"></asp:Label><asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtotros"
                                                            ErrorMessage="Debe Poner Datos" ValidationGroup="grabao" Width="198px"></asp:RequiredFieldValidator></td>
                                                                       </tr>
                                                                   </table>
                                                               </ContentTemplate>
                                                           </asp:UpdatePanel>
                                                       </td>
                                                   </tr>
          
                                              </table> 

                                            
                                            
                                            </div><!--ID FORMATO-->
                                            
                                            
                                            
                                            
                                            <div id="Grid" runat="server">
                                                      <CR:CrystalReportSource ID="origen_reporte" runat="server">
                                                      </CR:CrystalReportSource>
                                                      <CR:CrystalReportViewer ID="visor_reporte" runat="server" AutoDataBind="true" />
                                                      <input id="lblauxedita" type="hidden" runat="server" /><br />
                                                    <asp:DataGrid ID="gridTerapias" runat="server" AutoGenerateColumns="False" BackColor="White"
                                                        BorderColor="#F4F4F4" BorderWidth="1px" CellPadding="3" CellSpacing="1" DataKeyField="idTerapia"
                                                        ForeColor="Black" GridLines="None" Width="490px" Font-Bold="False">
                                                        <FooterStyle BackColor="Tan" />
                                                        <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                        <AlternatingItemStyle BackColor="White" />
                                                        <Columns>
                                                            <%--<asp:BoundColumn DataField="diagnostico" HeaderText="Diagnostico">
                                                                <ItemStyle HorizontalAlign="Left" />
                                                            </asp:BoundColumn>--%>
                                                            <asp:BoundColumn DataField="fechaInicio" HeaderText="Fecha"></asp:BoundColumn>
                                                            <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:LinkButton ID="LinkButton3" runat="server" CssClass="btn btn-warning">Ver</asp:LinkButton>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <HeaderStyle BackColor="#F4F4F4" Font-Bold="True" ForeColor="#1A3773" />
                                                        <ItemStyle BackColor="#F4F4F4" />
                                                    </asp:DataGrid>
                                                      <asp:Label ID="lblmensaje" runat="server" BackColor="Red" Font-Bold="True" ForeColor="White"
                                                          Text="Label" Width="490px"></asp:Label>
                                                <asp:LinkButton ID="lnkAgregar" runat="server" ValidationGroup="validaDatos" CssClass="btn btn-success">Agregar Terapia</asp:LinkButton>

                                            </div>

        
                                        
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        
        
       

        
        
        
        
        <asp:Label ID="lblIdcliente" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
      <asp:Label ID="lblidTerapia" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
            PopupButtonID="micalendario" TargetControlID="txtFechainicio">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtFechainicio">
        </ajaxToolkit:MaskedEditExtender>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
            FilterType="Numbers" TargetControlID="txtNosesiones">
        </ajaxToolkit:FilteredTextBoxExtender>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
            FilterType="Numbers" TargetControlID="txtEdad">
        </ajaxToolkit:FilteredTextBoxExtender>
   
    </form>
    <!-- start js include path -->
    <script type="text/javascript" src="assets/plugins/jquery/jquery.min.js" ></script>
	<script type="text/javascript" src="assets/plugins/popper/popper.min.js" ></script>
    <script type="text/javascript" src="assets/plugins/jquery-blockui/jquery.blockui.min.js" ></script>
	<script type="text/javascript" src="assets/plugins/jquery-slimscroll/jquery.slimscroll.js"></script>
    <!-- bootstrap -->
    <script type="text/javascript" src="assets/plugins/bootstrap/js/bootstrap.min.js" ></script>
    <!-- Common js-->
	<script type="text/javascript" src="assets/js/app.js" ></script>
    <script type="text/javascript" src="assets/js/layout.js" ></script>
	<script type="text/javascript" src="assets/js/theme-color.js" ></script>
	<!-- Material -->
	<script type="text/javascript" src="assets/plugins/material/material.min.js"></script>
    <!--ImputMask-->
    <script src="assets/plugins/bootstrap-inputmask/bootstrap-inputmask.min.js" ></script>
    <!--select2-->
    <script type="text/javascript" src="assets/plugins/select2/select2.js"></script>
    <!-- end js include path -->
    <!--sweetalert2-->
    <script type="text/javascript" src="https://cdn.jsdelivr.net/npm/sweetalert2@8.3.0/dist/sweetalert2.all.min.js"></script>
    <script>
        $(document).ready(function () {
            $('[data-tooltip="tooltip"]').tooltip();

            $("#diagnosticos").select2({
                placeholder: "Seleccionar el diagnóstico",
                width: null,
                //multiple: true,
                containerCssClass: ':all:',
                minimumInputLength: 4,
                ajax: {
                    url: "./lista_diagnosticos.ashx",
                    dataType: "json",
                    data: function (term) {
                        return {
                            q: term
                        };
                    },
                    results: function (data) {
                        return {
                            results: data
                        };
                    },
                },
                cache: true,
                formatNoMatches: function () {
                    return 'No se encotraron resultados <button type="button" class="btn btn-xs btn-success" onclick="terapias(11)">¿Desea agregar el diagnóstico?</button>';
                },
            });




            //Expediente(1);
            //Leer diagnosticos
           terapias(4);
            //Leer Protocolos
            //Expediente(6);
        })

        function terapias(opc,id)
        {            
            $.ajax({
                type: 'POST',
                url: "./terapias.ashx",
                async: false,
                data: {
                    opcion: opc,
                    id: id,
                    fecha: $('#fecha').val(),
                    idcita: $('#lblIdcita').html(),
                    idcliente: $('#lblIdcliente').html(),
                    idterapia: $('#lblidTerapia').html(),
                    idcitafecha: $('#fecha option:selected').data('id'),
                    id_diagnostico: $('#diagnosticos').val(),
                    id_protocolo: $('#protocolos option:selected').val(),
                    fase: $('#protocolos option:selected').val(),
                    observaciones: $('#observaciones').val(),
                    indicaciones_medicas: $('#indicaciones_medicas').val(),
                    contra_indicaciones: $('#contra_indicaciones').val(),
                    alergias: $('#alergias').val(),
                    ultrasonido: $('#ultrasonido').prop('checked') == true ? 1:0,
                    chc: $('#chc').prop('checked') == true ? 1 : 0,
                    electroterapia: $('#electroterapia').prop('checked') == true ? 1 : 0,
                    laser: $('#laser').prop('checked') == true ? 1:0,
                    cf: $('#cf').prop('checked') == true ? 1 : 0,
                    ejercicio: $('#ejercicio').prop('checked') == true ? 1 : 0,
                    masaje: $('#masaje').prop('checked') == true ? 1 : 0,
                    magneto: $('#magneto').prop('checked') == true ? 1 : 0,
                    gimnasia: $('#gimnasia').prop('checked') == true ? 1 : 0,
                    nombre_diagnostico: $('#nombre_diagnostico').val()
                    
                },
                success: function (data) {
                    console.log("success! " + data);
                    var obj = JSON.parse(data);

                    if (opc == 1) //Leer los datos
                    {
                        var diagnosticos = new Array();
                        $.each(obj, function (key, registro) {
                            $('#lblIdcliente').html(registro.id);
                            $('#nombre').val(registro.nombre);
                            $('#fecha_nac').val(registro.fecha_nac);
                            $('#edad').val(registro.edad);
                            $('#origen').val(registro.origen);
                            $('#ocupacion').val(registro.ocupacion);
                            $('#direccion').val(registro.direccion);
                            $('#alergias').val(registro.alergias);
                            $('#indicaciones_medicas').val(registro.indicaciones_medicas);
                            $('#contra_indicaciones').val(registro.contra_indicaciones);
                            $('#protocolo').val(registro.protocolo);
                            $('#fase').val(registro.fase);
                            $('#fecha').val(registro.fecha);
                            $('#observaciones').val(registro.observaciones);
                            $('#ultrasonido').prop('checked', (registro.ultrasonido == 1 ? true : false));
                            $('#chc').prop('checked', (registro.chc == 1 ? true : false));
                            $('#electroterapia').prop('checked', (registro.electroterapia == 1 ? true : false));
                            $('#laser').prop('checked', (registro.laser == 1 ? true : false));
                            $('#cf').prop('checked', (registro.cf == 1 ? true : false));
                            $('#ejercicio').prop('checked', (registro.ejercicio == 1 ? true : false));
                            $('#masaje').prop('checked', (registro.masaje == 1 ? true : false));
                            $('#magneto').prop('checked', (registro.magneto == 1 ? true : false));
                            $('#gimnasia').prop('checked', (registro.gimnasia == 1 ? true : false));

                            /*var str1 = registro.id_diagnosticos;
                            var array1 = str1.split(".");
                            var str2 = registro.diagnosticos;
                            var array2 = str2.split(".");
                            for (var c = 0; c < array1.length; c++) {
                                diagnosticos.push({ id: array1[c], text: array2[c] });
                            }*/

                            $('#fecha').empty();
                            var str3 = registro.fecha;
                            var array3 = str3.split(".");

                            var str4 = registro.idfecha;
                            var array4 = str4.split(".");
                            for (var c = 0; c < array3.length; c++) {
                                if ($('#hfFechaAgenda').html() == array3[c])
                                {
                                    $('#fecha').append('<option value="' + array3[c] + '" selected data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                                else
                                {
                                    $('#fecha').append('<option value="' + array3[c] + '" data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                            }

                        });
                        //$('#diagnosticos').select2('data', diagnosticos).trigger('change');
                    }
                    if (opc == 2)//Guardar datos
                    {
                        Swal.fire({
                            type: 'success',
                            title: 'Los cambios se guardaron correctamente',
                            showConfirmButton: false,
                            timer: 1800
                        })
                       
                    }
                    if (opc == 3)//Leer datos del expediente según la fecha
                    {
                        $.each(obj, function (key, registro) {
                            $('#observaciones').val(registro.observaciones);
                            $('#ultrasonido').prop('checked', (registro.ultrasonido == 1 ? true : false));
                            $('#chc').prop('checked', (registro.chc == 1 ? true : false));
                            $('#electroterapia').prop('checked', (registro.electroterapia == 1 ? true : false));
                            $('#laser').prop('checked', (registro.laser == 1 ? true : false));
                            $('#cf').prop('checked', (registro.cf == 1 ? true : false));
                            $('#ejercicio').prop('checked', (registro.ejercicio == 1 ? true : false));
                            $('#masaje').prop('checked', (registro.masaje == 1 ? true : false));
                            $('#magneto').prop('checked', (registro.magneto == 1 ? true : false));
                            $('#gimnasia').prop('checked', (registro.gimnasia == 1 ? true : false));

                        });
                    }
                    if (opc == 4)//Guardar y Agregar el diagnostico a la tabla
                    {
                        $("#tabla_diagnosticos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_diagnosticos tbody").append('<tr><td>' + registro.diagnostico + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="terapias(5,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }

                    }
                    if (opc == 5)//Guardar y Eliminar el diagnostico a la tabla
                    {
                        $("#tabla_diagnosticos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_diagnosticos tbody").append('<tr><td>' + registro.diagnostico + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="terapias(5,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }

                    }
                    if (opc == 6)//Leer protocolos y llenar el select id=protocolos
                    {
                        $("#protocolos").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#protocolos").append('<option value="' + registro.id + '">' + registro.nombre + '</option>');
                            });
                        }
                    }
                    if (opc == 7) //Mostrar Modal con la información del protocolo (Aceptar agrega el protocolo, Cancelar no agrega el protocolo)
                    {
                        //Cambiar datos del modal
                        //$("#modal_body").empty();
                        $('#titulo_protocolo').html($('#protocolos option:selected').text());
                        console.log("porotcolos:" + $('#protocolos option:selected').text());
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tab1").html('<h3>Tratamiento</h3><p>' + registro.tab1 + '</p>');
                                $('#tab2').html('<h3>Contra Indicaciones</h3><p>' + registro.tab2+'</p>');
                                $('#tab3').html('<h3>Observaciones</h3><p>' + registro.tab3 + '</p>');
                            });
                        }
                        $('#boton_modal').attr('onClick', 'Expediente(8,'+$('#protocolos option:selected').val()+')');
                        $('#modal_protocolos').modal();
                    }
                    if (opc == 8)//Guardar protocolo
                    {
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td>' + registro.fase + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }
                    }
                    if (opc == 9)//Borrar protocolo 
                    {
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td>' + registro.fase + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }
                    }
                    if (opc == 10)//Agregar la fase en el ultimo registro
                    {
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td>' + registro.fase + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }
                    }
                    if (opc == 11) //Abrir el modal para agregar el diagnostico
                    {
                        $("#diagnosticos").select2('close');
                        $('#diagnostico_modal').modal();
                    }
                    if (opc == 12) //Guardar el diagnostico nuevo en la tabla
                    {
                        console.log("Opcion 12");
                        var obj = JSON.parse(data);
                        $("#diagnosticos").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                console.log('registro' + registro.nombre);
                                $('#diagnosticos').append('<option value="' + registro.id + '" selected>' + registro.nombre + '</option>');
                                $('#diagnosticos').val(registro.id);
                            });
                        }
                        terapias(4);
                    }
                }
            });
        }
        </script>

        <div class="modal fade" id="diagnostico_modal" tabindex="-1" role="dialog" aria-hidden="true">
					    <div class="modal-dialog" role="document">
					        <div class="modal-content">
					            <div class="modal-header">
					                <h4 class="modal-title">Agregar Diagnóstico</h4>
					                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					                    <span aria-hidden="true">&times;</span>
					                </button>
					            </div>
					            <div class="modal-body">
					                <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-4">Nombre</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                                <input type="text" id="nombre_diagnostico" class="form-control" style="text-transform:uppercase;" onkeyup="javascript:this.value=this.value.toUpperCase();" />
                                            </div>
                                        </div>
                                    </div>
					            </div>
					            <div class="modal-footer">
                                    <button type="button" class="btn btn-primary btn-block" data-dismiss="modal" onclick="terapias(12)"><i class="fa fa-save"></i> Guardar</button>
					            </div>
					        </div>
					    </div>
					</div>

       

</body>
</html>
