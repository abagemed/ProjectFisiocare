<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ABCTerapeutas.aspx.vb" Inherits="ABCTerapeutas" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
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
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <!-- DataTables -->
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.css"/>
    <script type="text/javascript">
        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z Ññ.-0-1-2-3-4-5-6-7-8-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValSoloLetrasEspacioBlancoNumeros(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z Ññ.@-_-0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        //columna
        $(".columna").select2({ placeholder: "Seleccione la columna", });
    </script>
    <script language="javascript" type="text/javascript">
    var ventanaCierra;
    function cierraventana(){
    ventanaCierra.close();
    }
    function miventana(){
    ventanaCierra=window.open("terapias.aspx?idcliente="+document.form1.cmbclientes.value,"ordenes","toolbar=no,width=550,height=600,scrollbars=yes");
    }
    </script>
    <style type="text/css">
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }
        .ajax__tab_xp .ajax__tab_tab {
            height: 30px !important;
        }
</style>
</head>
<body onload="javascript:if(history.length>0)history.go(+1)" class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
    <form runat="server" id="Form1">
        <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="false"></asp:Label>
        <div class="page-wrapper">
        <!-- start header -->
		<div class="page-header navbar navbar-fixed-top">
            <div class="page-header-inner ">
                <!-- logo start -->
                <div class="page-logo">
                    <a href="Default.aspx">
                    <span class="logo-default" >Fisiocare</span> </a>
                </div>
                <!-- logo end -->
				<ul class="nav navbar-nav navbar-left in">
					<li><a href="#" class="menu-toggler sidebar-toggler font-size-20"><i class="fa fa-exchange" aria-hidden="true"></i></a></li>
				</ul>
                
                <ul class="nav navbar-nav navbar-left in">
                	<!-- start full screen button -->
                    <li><a href="javascript:;" class="fullscreen-click font-size-20"><i class="fa fa-arrows-alt"></i></a></li>
                    <!-- end full screen button -->
                </ul>
                <!-- start mobile menu -->
                <a href="javascript:;" class="menu-toggler responsive-toggler" data-toggle="collapse" data-target=".navbar-collapse">
                    <span></span>
                </a>
               <!-- end mobile menu -->
                <!-- start header menu -->
                <div class="top-menu">
                    <ul class="nav navbar-nav pull-right">
                        
 						<!-- start manage user dropdown -->
 						<li class="dropdown dropdown-user">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                <img alt="" class="img-circle " src="imagenes/iso.png" />
                            </a>
                            <ul class="dropdown-menu dropdown-menu-default">
                                <li>
                                    <div class="pull-right">
                                      <i class="fa fa-sign-out"></i><asp:Button ID="BtnCerrarSesion" runat="server" Text="Salir" CssClass="btn btn-danger btn-flat" />
                                    </div>
                                </li>
                            </ul>
                        </li>
                        
                    </ul>
                </div>
            </div>
        </div>
        <!-- end header -->
        <!-- start page container -->
        <div class="page-container">
 			<!-- start sidebar menu -->
 			<div class="sidebar-container">
 				<div class="sidemenu-container navbar-collapse collapse fixed-menu">
	                <div id="remove-scroll" class="left-sidemenu">
	                    <ul class="sidemenu  page-header-fixed slimscroll-style" data-keep-expanded="false" data-auto-scroll="true" data-slide-speed="200" style="padding-top: 20px">
	                        <li class="sidebar-toggler-wrapper hide">
	                            <div class="sidebar-toggler">
	                                <span></span>
	                            </div>
	                        </li>
	                        <li class="sidebar-user-panel">
	                            <div class="user-panel">
	                                <div class="pull-left image">
	                                    <img src="imagenes/iso.png" class="img-circle user-img-circle" alt="FisioCare" />
	                                </div>
	                                <div class="pull-left info">
	                                    <p> Administrador</p>
	                                    <small>fisioca</small>
	                                </div>
	                            </div>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="Pacientes" class="nav-link nav-toggle">
	                                <i class="material-icons">accessible</i>
	                                <span class="title">Pacientes</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="Recibos" class="nav-link nav-toggle">
	                                <i class="fa fa-file-text-o"></i>
	                                <span class="title">Recibos</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="Historial" class="nav-link nav-toggle">
	                                <i class="fa fa-history"></i>
	                                <span class="title">Historial</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="Catalogos" class="nav-link nav-toggle">
	                                <i class="fa fa-list-ol"></i>
	                                <span class="title">Catálogos</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="CorteCaja" class="nav-link nav-toggle">
	                                <i class="fa fa-dollar"></i>
	                                <span class="title">Corte de caja</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>
                            <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="BloqueoCitas" class="nav-link nav-toggle">
	                                <i class="fa fa-lock"></i>
	                                <span class="title">Bloqueo de citas</span>
                                	<span class="arrow"></span>
	                            </a>
	                        </li>

                            <li class="nav-item">
                                <a href="VerCitas.aspx" class="nav-link nav-toggle">
                                    <i class="fa fa-search"></i>
                                    <span class="title">VerCitas</span>
                                    <span class="arrow"></span>
                                </a>
                            </li>
                            
	                        <li class="nav-item">
	                            <a href="https://facturacionca.agemed.com.mx" target="_blank" class="nav-link ">
	                                <i class="fa fa-pencil"></i>
	                                <span class="title">Facturación</span>
                                	<span class="arrow"></span>
	                            </a>
	                            <%--<ul class="sub-menu">
	                                <li class="nav-item">
	                                    <a href="https://facturacionca.agemed.com.mx" target="_blank" class="nav-link ">
	                                        <span class="title">Facturación</span>
	                                    </a>
	                                </li>
	                                <li class="nav-item ">
	                                    <a href="#" runat="server" onServerClick="FacturasGeneradas" class="nav-link ">
	                                        <span class="title">Facturas generadas</span>
	                                    </a>
	                                </li>
	                            </ul>--%>
	                        </li>
                            <%--menu configuracion AB26012021--%>

                            <li class="nav-item">
	                            <a href="#" class="nav-link nav-toggle">
	                                <i class="fa fa-cog"></i>
	                                <span class="title">Configuración</span>
                                	<span class="arrow"></span>
	                            </a>
	                            <ul class="sub-menu">
	                                <li class="nav-item ">
	                                    <a href="#" runat="server" onServerClick="CTerapeutas" class="nav-link nav-toggle">
	                                <i class="fa fa-users"></i>
	                                <span class="title">Terapeutas</span>
                                	<span class="arrow"></span>
	                            </a>
	                                </li>
	                            </ul>
	                        </li>
	                        
	                    </ul>
	                </div>
                </div>
            </div>
			 <!-- end sidebar menu -->
			<!-- start page content -->
            <div class="page-content-wrapper">
                <div class="page-content">
                    <div class="page-bar">
                        <div class="page-title-breadcrumb">
                            <div class=" pull-left">
                                <div class="page-title">GESTIÓN DE TERAPEUTAS</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-list-ol"></i>&nbsp;<a class="parent-item" href="#">Terapeutas</a>
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
                                            <header>Gestión de Terapeutas</header>
                                            
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <asp:LinkButton ID="LinkButton5" runat="server"  OnClientClick="cierraventana();" CssClass="btn btn-block btn-info" ><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</asp:LinkButton>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                </div>
                                            </div>
                                            <br />

                                             <%-- .....inicio ab27012021 --%>
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"></asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                                <ContentTemplate>
                                                    <div class="box-body">
                                <%--------------------- Avisos en pantalla-------------------------------%>
                                <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-success alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeAviso" runat="server"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelDesicion" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-info alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi_Click"><i class="fa fa-check"></i>Sí</button>
                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo_Click"><i class="fa fa-close"></i>No</button>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelCritico" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label>></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelAdvertencia" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-warning alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeAdvertencia" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                
                                
                                <div style="margin: 10px 0px" hidden>
                                    <button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#Alta">
                                        Agregar Terapeuta <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                                <asp:GridView ID="GdTerapeutas" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="id" HeaderText="IdTerapeuta" >
                                        <ControlStyle CssClass="hidden-xs hidden-sm" />
                                        <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="columna" HeaderText="# Columna" >
                                        <ControlStyle CssClass="hidden-xs " />
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="turno" HeaderText="Turno" >
                                        <ControlStyle CssClass="hidden-xs hidden-sm" />
                                        <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Button" HeaderText="Editar" Text="Editar" CommandName="Editar" ControlStyle-CssClass="btn btn-block btn-success">
                                            <ControlStyle CssClass="btn btn-block btn-success"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField Visible="false" ButtonType="Button" HeaderText="Borrar" Text="Borrar" CommandName="Borrar" ControlStyle-CssClass="btn btn-block btn-danger">
                                            <ControlStyle CssClass="btn btn-block btn-danger hidden-xs "></ControlStyle>
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                                                    </div>
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

    <!--Modal editar Terapeuta-->
    <div id="Editar_1" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Regresar</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-body">
                                <asp:HiddenField ID="Hdpregunta" runat="server" />
                                <asp:HiddenField ID="HdTerapeutaEdit" runat="server" />
                                <div class="form-group">
                                    <label class="col-md-4">Nombre</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox runat="server" class="form-control" ID="txtNombree" placeholder="Nombre" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombree" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Contraseña</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox class="form-control" runat="server" ID="txtPasse" placeholder="******"  MaxLength="12" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPasse" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                 <div class="form-group">
                                    <label class="col-md-4">Turno</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <%--<select class="form-control" id="selturnoe" name="nivel" runat="server">
                                            <option value="M">Matutino</option>
                                            <option value="V">Vespetino</option>
                                        </select>--%>
                                        <asp:DropDownList ID="selturnoe" class="form-control columna" runat="server" autoPostBack="true">
                                <asp:ListItem Value="M">Matutino</asp:ListItem>
                                <asp:ListItem Value="V">Vespertino</asp:ListItem>
                                </asp:DropDownList>
                                    </div>
                                </div
                              
                                <div class="form-group">
                                   <label  class="col-md-4">Columna</label>
                                   <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-list-ol"></i></span>
                                        <asp:DropDownList ID="secolumnae" class="form-control columna" runat="server" autoPostBack="true">
                                        </asp:DropDownList>
                                   </div>
                                </div>
                                <hr />
                                <div class="form-group">
                                    <label class="col-md-4">Otras Configuraciones</label>
                                    <div class="input-group col-md-8">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:CheckBox ID="ChkBoxActivoe" runat="server"  Text =" Activo"/> 
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                        </div>
                            <div class="modal-footer">
                                <input type="hidden" name="Editar" value="1">
                                <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" onserverclick="BtnGuardarEdit_Click" causesvalidation="true" validationgroup="gDatos">
                                    <i class="fa fa-save"></i> Guardar
                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>
    <!--Modal Nuevo Terapeuta-->
    <div id="Alta" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog modal-lg">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Regresar</h4>
                </div>

                <div class="modal-body">
                    <div class="form-body">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
                            <ContentTemplate>
                        <div class="form-group">
                            <label class="col-md-4">Nombre del Terapeuta</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <asp:TextBox class="form-control" ID="txtNombren" placeholder="Nombre del Terapeuta" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNombren" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Contraseña</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                <asp:TextBox class="form-control" placeholder="******" ID="txtPassn" runat="server"  MaxLength="12" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPassn" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Turno</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                <%--<select class="form-control" runat="server" id="seturnon" name="nivel">
                                     <option value="M">Matutino</option>
                                     <option value="V">Vespertino</option>
                                </select>--%>
                                <asp:DropDownList ID="seturnon" class="form-control columna" runat="server" autoPostBack="true">
                                <asp:ListItem Value="M">Matutino</asp:ListItem>
                                <asp:ListItem Value="V">Vespertino</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="form-group">
                             <label  class="col-md-4">Columna</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-list-ol"></i></span>
                                    <asp:DropDownList ID="secolumnan" class="form-control columna" runat="server" autoPostBack="true">
                                    </asp:DropDownList>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="secolumnan" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                </div>
                        </div>
                        <hr />
                        <div class="form-group">
                             <label class="col-md-4">Otras Configuraciones</label>
                                   <div class="input-group col-md-8">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <asp:CheckBox ID="ChkBoxActivon" runat="server" Text ="Activo" Checked="true"/>
                                            </div>
                                    </div>
                              </div>
                         </div> 
                                <asp:HiddenField ID="HdIdColumna" runat="server" />
                                 </ContentTemplate>
                            </asp:UpdatePanel>
                                                               
                    </div>
                </div>
                <div class="modal-footer">
                            <input type="hidden" name="Alta" value="1">
                            <asp:Button ID="btnGuardarn" runat="server" Text="Guardar" class="btn btn-success btn-block" CausesValidation="true" ValidationGroup="gNuevoDatos" />
                            <button type="button" class="btn btn-default btn-block" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                </div>
            </div>
       </div>
    </div>
                                            <%-- fin ab27012021....--%>
                                                
                                        
                                                <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label><br />
                                              
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end page content -->
           
        </div>
        <!-- end page container -->
        <!-- start footer -->
        <div class="page-footer">
            <div class="page-footer-inner"> 2019 &copy; FisioCare
                <a href="#" target="_top" class="makerCss">MEDSOL SISTEMAS</a>
            </div>
            <div class="scroll-to-top">
                <i class="material-icons">eject</i>
            </div>
        </div>
        <!-- end footer -->
    </div>
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
    <!-- end js include path -->
    <script>
        $(document).ready(function () {
            $('[data-tooltip="tooltip"]').tooltip();
        })
    </script>
    <!-- DataTables -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/pdfmake.min.js"></script>
	<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/vfs_fonts.js"></script>
	<script type="text/javascript" src="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.js"></script>
    <script>
        //CREAR CABECERA
        var grid = document.getElementById('dgridDoctores');
        var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
        var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

        tbody.removeChild(firstTr); //remove tr's from table

        var newTh = document.createElement('thead'); //creates thead
        newTh.appendChild(firstTr); //puts ths in thead
        grid.insertBefore(newTh, tbody); //puts thead before tbody
        $(function () {


            $('#dgridDoctores').DataTable({
                "language": { url: "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json" },
                "ordering": false,
                "paging": false,
                "searching": true,
                "info": false,
                "fixedHeader": true,
                "responsive": true,
                "order": [[1, "asc"]],
            });
        });
    </script>
</body>
</html>
