<%@ Page Language="VB" AutoEventWireup="false" CodeFile="expedientemedico.aspx.vb" Inherits="expedientemedico" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
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
    </style>

</head>
<body class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
    <form id="form1" runat="server">
    <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="false"></asp:Label>
    <asp:Label ID="lblIdcita" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
    <asp:Label ID="lblIdcliente" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
    <asp:Label ID="hfFechaAgenda" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
        <asp:Label ID="hfposicion" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>
        <asp:Label ID="hfturno" runat="server" Text="Label" Visible="true" CssClass="hidden"></asp:Label>

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
                 <!-- Start Apps Dropdown -->
                 <ul class="nav navbar-nav navbar-left in">
				 	<li class="dropdown dropdown-extended dropdown-notification" >
                            <a href="javascript:;" class="dropdown-toggle app-list-icon font-size-20" data-toggle="dropdown" data-hover="dropdown" data-close-others="true">
                                <i class="fa fa-th" aria-hidden="true"></i>
                            </a>
                            <ul class="dropdown-menu app-icon">
                            	<li class="app-dropdown-header">
                                    <p><span class="bold">Generales</span></p>
                                </li>
                                <li>
                                    <ul class="dropdown-menu-list app-icon-dropdown" data-handle-color="#637283">
										<li>
											<a href="#" runat="server" onServerClick="Pacientes" class="patient-icon">
											<i class="material-icons">accessible</i>
											<span class="block">Pacientes</span>
											</a>
										</li>
										<li>
											<a href="#" runat="server" onServerClick="Recibos" class="email-icon">
											<i class="material-icons">assignment_turned_in</i>
											<span class="block">Recibos</span>
											</a>
										</li>
										<li>
											<a href="#" runat="server" onServerClick="Historial" class="appoint-icon">
											<i class="material-icons">history</i>
											<span class="block">Historial</span>
											</a>
										</li>
										<li>
											<a href="#" runat="server" onServerClick="Catalogos" class="doctor-icon">
											<i class="material-icons">list</i>
											<span class="block">Catálogos</span>
											</a>
										</li>
										<li>
											<a href="#" runat="server" onServerClick="CorteCaja" class="map-icon">
											<i class="material-icons">monetization_on</i>
											<span class="block">Corte de caja</span>
											</a>
										</li>
										<li>
											<a href="#" runat="server" onServerClick="BloqueoCitas" class="payment-icon">
											<i class="material-icons">lock_outline</i>
											<span class="block">Bloqueo de citas</span>
											</a>
										</li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                 </ul>
                 <!-- End Apps Dropdown -->
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
            <%   
                'Response.WriteFile("menu.aspx")
            %>
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
	                                    <small>fisiosm</small>
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
	                            <a href="#" class="nav-link nav-toggle">
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
	                            <a href="#" class="nav-link nav-toggle">
	                                <i class="fa fa-pencil"></i>
	                                <span class="title">Facturación</span>
                                	<span class="arrow"></span>
	                            </a>
	                            <ul class="sub-menu">
	                                <li class="nav-item">
	                                    <a href="https://facturacionho.agemed.com.mx" target="_blank" class="nav-link ">
	                                        <span class="title">Facturación</span>
	                                    </a>
	                                </li>
	                                <li class="nav-item ">
	                                    <a href="#" runat="server" onServerClick="FacturasGeneradas" class="nav-link ">
	                                        <span class="title">Facturas generadas</span>
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
                                <div class="page-title">Expediente Médico</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-search"></i>&nbsp;<a class="parent-item" href="#">Expediente Médico</a>
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
                                            <header>EXPEDIENTE MÉDICO</header>
                                            
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                            <h3>Datos del paciente</h3>
                                            <hr />
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Nombre</label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="nombre" placeholder="Nombre del paciente" class="form-control"/>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Fecha de nacimeinto</label>
                                                <div class="col-sm-3">
                                                    <input type="text" id="fecha_nac" placeholder="24-04-1981" data-mask="99/99/9999" class="form-control"/>
                                                </div>
                                                <label class="col-sm-1 control-label">Edad</label>
                                                <div class="col-sm-2">
                                                    <input type="number" min="1" id="edad" placeholder="Edad" class="form-control"/>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Origen (Ciudad)</label>
                                                <div class="col-sm-3">
                                                    <input type="text" id="origen" placeholder="Origen" class="form-control"/>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Dirección</label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="direccion" placeholder="Dirección" class="form-control"/>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Ocupación</label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="ocupacion" placeholder="Ocupación" class="form-control"/>
                                                </div>
                                            </div>
                                            
                                            <hr />

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <table class="table table-striped table-hover" id="tabla_citas">
                                                        <thead>
                                                            <tr>
                                                                <th>Fecha próxima cita</th>
                                                                <th>Atiende</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                </div>
                                            </div>


                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 class="text-center">Expediente Médico</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Alergías/Padecimientos</label>
                                                <div class="col-sm-9">
                                                    <input type="text" id="alergias" placeholder="Alergías/Padecimientos" class="form-control"/>
                                                </div>
                                            </div>
                                            
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <label>Indicaciones Médicas</label>
                                                    <textarea id="indicaciones_medicas" placeholder="Indicaciones del Medico" class="form-control" rows="5"></textarea>
                                                </div>
                                                <div class="col-sm-6">
                                                    <label>Contra indicaciones</label>
                                                    <textarea id="contra_indicaciones" placeholder="Contra Indicaciones del Medico"  class="form-control" rows="5"></textarea>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-2 control-label">Diagnóstico</label>
                                                <div class="col-sm-6">
                                                    <input type="text" class="select2" id="diagnosticos" onchange="Expediente(4)" />                                                    
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
                                                <label class="col-sm-2 control-label">Protocolo</label>
                                                <div class="col-sm-4">
                                                    <input type="text" class="select2" id="protocolos" onchange="Expediente(7)" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-md-12">
                                                    <table class="table table-striped table-hover" id="tabla_protocolos">
                                                        <thead>
                                                            <tr>
                                                                <th>Protocolo</th>
                                                                <th>Fase</th>
                                                                <th>Fecha</th>
                                                                <th>Borrar</th>
                                                            </tr>
                                                        </thead>
                                                        <tbody></tbody>
                                                    </table>
                                                </div>
                                            </div>

                                        <div id="historial">

                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <h3 class="text-center">Historial</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Fecha tratamiento</label>
                                                <div class="col-sm-3">
                                                    <select class="form-control" id="fecha" onchange="Expediente(3)">
                                                        
                                                    </select>
                                                </div>
                                                <label class="col-sm-3 control-label">Lista de Terapeutas</label>
                                                <div class="col-sm-3">
                                                    <select class="form-control" id="terapista">
                                                        
                                                    </select>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3>EXPLORACIÓN FÍSICA (Objetivo del PX)</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_analgesia" type="checkbox"  />
                                                        <label for="exploracion_analgesia">
                                                           Analgesía
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_propiocepsion" type="checkbox"  />
                                                        <label for="exploracion_propiocepsion">
                                                            Propiocepción
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_desinflamacion" type="checkbox" />
                                                        <label for="exploracion_desinflamacion">
                                                            Desinflamación
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_habilidades_manuales" type="checkbox"  />
                                                        <label for="exploracion_habilidades_manuales">
                                                            Habilidades manuales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_fortalecimiento" type="checkbox"  />
                                                        <label for="exploracion_fortalecimiento">
                                                            Fortalecimiento
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_aumentar_rangos" type="checkbox" />
                                                        <label for="exploracion_aumentar_rangos">
                                                           Aumentar rangos de movilidad
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_reduccion_marcha" type="checkbox"  />
                                                        <label for="exploracion_reduccion_marcha">
                                                            Reducción de la marcha
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="exploracion_reintegracion_deportiva" type="checkbox"  />
                                                        <label for="exploracion_reintegracion_deportiva">
                                                            Reintegración Deportiva
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3>ANALGESÍA</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_laser" type="checkbox" />
                                                        <label for="analgesia_laser">
                                                            Láser
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_ultrasonido" type="checkbox" />
                                                        <label for="analgesia_ultrasonido">
                                                            Ultrasonido
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_traccion_cervical" type="checkbox"  />
                                                        <label for="analgesia_traccion_cervical">
                                                            Tracción cervical
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_electroterapia" type="checkbox"  />
                                                        <label for="analgesia_electroterapia">
                                                            Electroterapia
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_masaje" type="checkbox" />
                                                        <label for="analgesia_masaje">
                                                            Masaje
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_traccion_lumbar" type="checkbox" />
                                                        <label for="analgesia_traccion_lumbar">
                                                            Tracción lumbar
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_tape" type="checkbox"  />
                                                        <label for="analgesia_tape">
                                                            Tape
                                                        </label>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_chc" type="checkbox"  />
                                                        <label for="analgesia_chc">
                                                            CHC
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_parafina" type="checkbox"  />
                                                        <label for="analgesia_parafina">
                                                            Parafina
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_magneto" type="checkbox" />
                                                        <label for="analgesia_magneto">
                                                            Magneto terapia
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_crio" type="checkbox" />
                                                        <label for="analgesia_crio">
                                                            Crio
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_diatermia" type="checkbox" />
                                                        <label for="analgesia_diatermia">
                                                            Diatermia
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_ondas" type="checkbox" />
                                                        <label for="analgesia_ondas">
                                                            Ondas de choque
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_hidroterapia" type="checkbox" />
                                                        <label for="analgesia_hidroterapia">
                                                            Hidroterapia
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_terapia_manual" type="checkbox" />
                                                        <label for="analgesia_terapia_manual">
                                                            Terapia Manual
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_banios_contraste" type="checkbox" />
                                                        <label for="analgesia_banios_contraste">
                                                            Baños de contraste
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_cf" type="checkbox"  />
                                                        <label for="analgesia_cf">
                                                            CF
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_ejercicio" type="checkbox"  />
                                                        <label for="analgesia_ejercicio">
                                                            Ejercicio
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="analgesia_gimnasia" type="checkbox" />
                                                        <label for="analgesia_gimnasia">
                                                            Gimnasia
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3>EJERCICIOS DE ESTIRAMIENTO</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <label class="col-sm-3 control-label">Estiramiento</label>
                                                <div class="col-sm-3">
                                                    <select class="form-control" id="estiramiento_sel" onchange="Cambio()">
                                                        <option value="0" data-tipo="">Seleccionar</option>
                                                        <option value="1" data-tipo="superior">Miembro superior</option>
                                                        <option value="2" data-tipo="columna">Columna</option>
                                                        <option value="3" data-tipo="inferior">Miembro inferior</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div id="estiramiento_contenedor_superior" class="estiramiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_activo_sup" type="checkbox"  />
                                                        <label for="estiramiento_activo_sup">
                                                            Activo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_pasivo_sup" type="checkbox"  />
                                                        <label for="estiramiento_pasivo_sup">
                                                            Pasivo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_cintura_escapular" type="checkbox" />
                                                        <label for="estiramiento_cintura_escapular">
                                                            Cintura escapular
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_extensores_muneca" type="checkbox"  />
                                                        <label for="estiramiento_extensores_muneca">
                                                            Extensores muñeca y dedos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_extensores_codo" type="checkbox"  />
                                                        <label for="estiramiento_extensores_codo">
                                                            Extensores codo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_flexores_muneca" type="checkbox" />
                                                        <label for="estiramiento_flexores_muneca">
                                                            Flexores muñeca y dedos
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_flexores_codo" type="checkbox"  />
                                                        <label for="estiramiento_flexores_codo">
                                                            Flexores codo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_maq_mov_pasiva_hombro" type="checkbox"  />
                                                        <label for="estiramiento_maq_mov_pasiva_hombro">
                                                            Maq. mov. pasiva hombro
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_mano_hombro" type="checkbox" />
                                                        <label for="estiramiento_mano_hombro">
                                                            Mano hombro
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_superior_3_5" type="checkbox"  />
                                                        <label for="estiramiento_superior_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_superior_3_10" type="checkbox"  />
                                                        <label for="estiramiento_superior_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_superior_3_15" type="checkbox" />
                                                        <label for="estiramiento_superior_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_dedos" type="checkbox" />
                                                        <label for="estiramiento_dedos">
                                                            Dedos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label>Otro:</label>
                                                </div>
                                                <div class="col-sm-7">
                                                    <input type="text" class="form-control" id="estiramiento_otro_estiramiento" placeholder="Otro" />
                                                </div>
                                            </div>

                                            </div>

                                            <div id="estiramiento_contenedor_columna" class="estiramiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_activo_columna" type="checkbox"  />
                                                        <label for="estiramiento_activo_columna">
                                                            Activo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_pasivo_columna" type="checkbox"  />
                                                        <label for="estiramiento_pasivo_columna">
                                                            Pasivo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_paravertebrales_dorsales" type="checkbox" />
                                                        <label for="estiramiento_paravertebrales_dorsales">
                                                            Paravertebrales dorsales
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_paravertebrales_lumbares" type="checkbox"  />
                                                        <label for="estiramiento_paravertebrales_lumbares">
                                                            Paravertebrales lumbares
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_cervicales" type="checkbox"  />
                                                        <label for="estiramiento_cervicales">
                                                            Cervicales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_paravertebrales" type="checkbox" />
                                                        <label for="estiramiento_paravertebrales">
                                                            Paravertebrales
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>


                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_columna_3_5" type="checkbox"  />
                                                        <label for="estiramiento_columna_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_columna_3_10" type="checkbox"  />
                                                        <label for="estiramiento_columna_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_columna_3_15" type="checkbox" />
                                                        <label for="estiramiento_columna_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_trapecio" type="checkbox" />
                                                        <label for="estiramiento_trapecio">
                                                            Trapecio
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            </div>

                                            <div id="estiramiento_contenedor_inferior" class="estiramiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_activo_inf" type="checkbox"  />
                                                        <label for="estiramiento_activo_inf">
                                                            Activo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_pasivo_inf" type="checkbox"  />
                                                        <label for="estiramiento_pasivo_inf">
                                                            Pasivo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_cuadriceps" type="checkbox" />
                                                        <label for="estiramiento_cuadriceps">
                                                            Cuadriceps
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_tensor_fascia_lata" type="checkbox"  />
                                                        <label for="estiramiento_tensor_fascia_lata">
                                                            Tensor de la fascia lata
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_isquiotibiales" type="checkbox"  />
                                                        <label for="estiramiento_isquiotibiales">
                                                            Isquiotibiales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_tibial_anterior" type="checkbox" />
                                                        <label for="estiramiento_tibial_anterior">
                                                            Tibial anterior
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_aductores" type="checkbox"  />
                                                        <label for="estiramiento_aductores">
                                                            Aductores
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_tibial_posterior" type="checkbox"  />
                                                        <label for="estiramiento_tibial_posterior">
                                                            Tibial posterior
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_abductores" type="checkbox" />
                                                        <label for="estiramiento_abductores">
                                                            Abductores
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_peroneos" type="checkbox"  />
                                                        <label for="estiramiento_peroneos">
                                                            Peroneos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_rotadores_cadera" type="checkbox"  />
                                                        <label for="estiramiento_rotadores_cadera">
                                                            Rotadores de cadera
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_maq_mov_pasiva_rodilla" type="checkbox" />
                                                        <label for="estiramiento_maq_mov_pasiva_rodilla">
                                                            Maq. mov. pasiva rodilla
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_fascia_plantar" type="checkbox"  />
                                                        <label for="estiramiento_fascia_plantar">
                                                            Fascia plantar
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_maq_mov_pasiva_tobillo" type="checkbox"  />
                                                        <label for="estiramiento_maq_mov_pasiva_tobillo">
                                                            Maq. mov. pasiva tobillo
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_inferior_3_5" type="checkbox"  />
                                                        <label for="estiramiento_inferior_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_inferior_3_10" type="checkbox"  />
                                                        <label for="estiramiento_inferior_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="estiramiento_inferior_3_15" type="checkbox" />
                                                        <label for="estiramiento_inferior_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>                                           

                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3>EJERCICIOS DE FORTALECIMIENTO</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row hidden">
                                                <label class="col-sm-3 control-label">Fortalecimiento</label>
                                                <div class="col-sm-3">
                                                    <select class="form-control" id="fortalecimiento_sel" onchange="Cambio()">
                                                        <option value="0" data-tipo="">Seleccionar</option>
                                                        <option value="1" data-tipo="superior">Miembro superior</option>
                                                        <option value="2" data-tipo="columna">Columna</option>
                                                        <option value="3" data-tipo="inferior">Miembro inferior</option>
                                                    </select>
                                                </div>
                                            </div>

                                            <div id="fortalecimiento_contenedor_superior" class="fortalecimiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_cintura_escapular" type="checkbox" />
                                                        <label for="fortalecimiento_cintura_escapular">
                                                            Cintura escapular
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_ligas" type="checkbox" />
                                                        <label for="fortalecimiento_ligas">
                                                            Ligas
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_polainas" type="checkbox" />
                                                        <label for="fortalecimiento_polainas">
                                                            Polainas
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_isometricas" type="checkbox" />
                                                        <label for="fortalecimiento_isometricas">
                                                            Isométricas
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_sin_peso" type="checkbox" />
                                                        <label for="fortalecimiento_sin_peso">
                                                            Sin peso
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_extensores_muneca" type="checkbox"  />
                                                        <label for="fortalecimiento_extensores_muneca">
                                                            Extensores muñeca y dedos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_extensores_codo" type="checkbox"  />
                                                        <label for="fortalecimiento_extensores_codo">
                                                            Extensores codo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_flexores_muneca" type="checkbox" />
                                                        <label for="fortalecimiento_flexores_muneca">
                                                            Flexores muñeca y dedos
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_flexores_codo" type="checkbox"  />
                                                        <label for="fortalecimiento_flexores_codo">
                                                            Flexores codo
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_maq_mov_pasiva_hombro" type="checkbox"  />
                                                        <label for="fortalecimiento_maq_mov_pasiva_hombro">
                                                            Maq. mov. pasiva hombro
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_mano_hombro" type="checkbox" />
                                                        <label for="fortalecimiento_mano_hombro">
                                                            Mano hombro
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_superior_3_5" type="checkbox"  />
                                                        <label for="fortalecimiento_superior_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_superior_3_10" type="checkbox"  />
                                                        <label for="fortalecimiento_superior_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_superior_3_15" type="checkbox" />
                                                        <label for="fortalecimiento_superior_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_dedos" type="checkbox" />
                                                        <label for="fortalecimiento_dedos">
                                                            Dedos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label>Otro:</label>
                                                </div>
                                                <div class="col-sm-7">
                                                    <input type="text" class="form-control" id="fortalecimiento_otro_estiramiento" placeholder="Otro" />
                                                </div>
                                            </div>

                                            </div>

                                            <div id="fortalecimiento_contenedor_columna" class="fortalecimiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_paravertebrales_dorsales" type="checkbox" />
                                                        <label for="fortalecimiento_paravertebrales_dorsales">
                                                            Paravertebrales dorsales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_williams" type="checkbox" />
                                                        <label for="fortalecimiento_williams">
                                                            Williams
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_mckenzic" type="checkbox" />
                                                        <label for="fortalecimiento_mckenzic">
                                                            Mckenzic
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_core" type="checkbox" />
                                                        <label for="fortalecimiento_core">
                                                            Core
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_klapp" type="checkbox" />
                                                        <label for="fortalecimiento_klapp">
                                                            KLAPP
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_paravertebrales_lumbares" type="checkbox"  />
                                                        <label for="fortalecimiento_paravertebrales_lumbares">
                                                            Paravertebrales lumbares
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_cervicales" type="checkbox"  />
                                                        <label for="fortalecimiento_cervicales">
                                                            Cervicales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_paravertebrales" type="checkbox" />
                                                        <label for="fortalecimiento_paravertebrales">
                                                            Paravertebrales
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_columna_3_5" type="checkbox"  />
                                                        <label for="fortalecimiento_columna_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_columna_3_10" type="checkbox"  />
                                                        <label for="fortalecimiento_columna_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_columna_3_15" type="checkbox" />
                                                        <label for="fortalecimiento_columna_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_trapecio" type="checkbox" />
                                                        <label for="fortalecimiento_trapecio">
                                                            Trapecio
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_ligas_columna" type="checkbox" />
                                                        <label for="fortalecimiento_ligas_columna">
                                                            Ligas
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_isometricas_columna" type="checkbox" />
                                                        <label for="fortalecimiento_isometricas_columna">
                                                            Isométricas
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            </div>

                                            <div id="fortalecimiento_contenedor_inferior" class="fortalecimiento_contenedor">

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_cuadriceps" type="checkbox" />
                                                        <label for="fortalecimiento_cuadriceps">
                                                            Cuadriceps
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_ligas_inf" type="checkbox" />
                                                        <label for="fortalecimiento_ligas_inf">
                                                            Ligas
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_isometricas_inf" type="checkbox" />
                                                        <label for="fortalecimiento_isometricas_inf">
                                                            Isométricas
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_tensor_fascia_lata" type="checkbox"  />
                                                        <label for="fortalecimiento_tensor_fascia_lata">
                                                            Tensor de la fascia lata
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_isquiotibiales" type="checkbox"  />
                                                        <label for="fortalecimiento_isquiotibiales">
                                                            Isquiotibiales
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_tibial_anterior" type="checkbox" />
                                                        <label for="fortalecimiento_tibial_anterior">
                                                            Tibial anterior
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_aductores" type="checkbox"  />
                                                        <label for="fortalecimiento_aductores">
                                                            Aductores
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_tibial_posterior" type="checkbox"  />
                                                        <label for="fortalecimiento_tibial_posterior">
                                                            Tibial posterior
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_abductores" type="checkbox" />
                                                        <label for="fortalecimiento_abductores">
                                                            Abductores
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                             <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_peroneos" type="checkbox"  />
                                                        <label for="fortalecimiento_peroneos">
                                                            Peroneos
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_rotadores_cadera" type="checkbox"  />
                                                        <label for="fortalecimiento_rotadores_cadera">
                                                            Rotadores de cadera
                                                        </label>
                                                    </div>
                                                </div>
                                                 <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_fascia_plantar" type="checkbox"  />
                                                        <label for="fortalecimiento_fascia_plantar">
                                                            Fascia plantar
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_polainas_inf" type="checkbox"  />
                                                        <label for="fortalecimiento_polainas_inf">
                                                            Polainas
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_sin_peso_inf" type="checkbox"  />
                                                        <label for="fortalecimiento_sin_peso_inf">
                                                            Sin peso
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_banco" type="checkbox"  />
                                                        <label for="fortalecimiento_banco">
                                                            Polainas
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_trampolin" type="checkbox"  />
                                                        <label for="fortalecimiento_trampolin">
                                                            Trampolín
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_bossu" type="checkbox"  />
                                                        <label for="fortalecimiento_bossu">
                                                            Bossu
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_inferior_3_5" type="checkbox"  />
                                                        <label for="fortalecimiento_inferior_3_5">
                                                            3/5
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_inferior_3_10" type="checkbox"  />
                                                        <label for="fortalecimiento_inferior_3_10">
                                                            3/10
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="fortalecimiento_inferior_3_15" type="checkbox" />
                                                        <label for="fortalecimiento_inferior_3_15">
                                                            3/15
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>                                           

                                            </div>


                                            <div class="row">
                                                <div class="col-md-12">
                                                    <h3>REACONDICIONAMIENTO</h3>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_bici" type="checkbox" />
                                                        <label for="reacondicionamiento_bici">
                                                            Bici
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label>Tiempo:</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control" id="reacondicionamiento_tiempo_bici" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_caminadora" type="checkbox" />
                                                        <label for="reacondicionamiento_caminadora">
                                                            Caminadora
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-1">
                                                    <label>Tiempo:</label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <input type="text" class="form-control" id="reacondicionamiento_tiempo_caminadora" />
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_reduccion_marcha" type="checkbox"  />
                                                        <label for="reacondicionamiento_reduccion_marcha">
                                                            Reducción de la marcha
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_eliptica" type="checkbox" />
                                                        <label for="reacondicionamiento_eliptica">
                                                            Elíptica
                                                        </label>
                                                    </div>
                                                </div>
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_escaleras" type="checkbox" />
                                                        <label for="reacondicionamiento_escaleras">
                                                            Escaleras
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>

                                            <div class="form-group row">
                                                <div class="col-sm-4">
                                                    <div class="checkbox checkbox-red">
                                                        <input id="reacondicionamiento_pelotas" type="checkbox"  />
                                                        <label for="reacondicionamiento_pelotas">
                                                            Pelotas
                                                        </label>
                                                    </div>
                                                </div>                                              
                                            </div>

                                        </div>



                                            <hr />
                                            <div class="form-group row">
                                                <div class="col-sm-12">
                                                    <label>Observaciones</label>
                                                    <textarea id="observaciones" class="form-control" rows="5"></textarea>
                                                </div>
                                            </div>
                                            <div class="form-group row">
                                                <div class="col-sm-6">
                                                    <label>Atendio</label>
                                                    <input type="text" id="atendio" placeholder="Terapeuta que atendio" class="form-control" readonly/>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-sm-12">
                                                   <button type="button" class="btn btn-block btn-info" onclick="Expediente(2)"><i class="fa fa-save"></i> Guardar</button>
                                                </div>
                                            </div>

                                       
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

        <div class="modal fade" id="modal_protocolos" tabindex="-1" role="dialog" aria-labelledby="Cambios">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                   <h4 class="modal-title" id="titulo_modal">Protocolos</h4>
		           <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                       <span aria-hidden="true">&times;</span>
                   </button>                    
                </div>
                <div class="modal-body">
                    <div class="form-body">
                        <div id="modal_body">
                            <div class="row">
                                <div class="col-md-12 col-sm-12">
                                    <div class="borderBox light bordered">
                                        <div class="borderBox-title tabbable-line">
                                            <div class="caption">
                                                <span class="caption-subject font-dark bold uppercase" id="titulo_protocolo"></span>
                                            </div>
                                            <ul class="nav nav-tabs">
                                                <li class="nav-item">
                                                    <a href="#tab1" data-toggle="tab" class="active"> Tratamiento </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a href="#tab2" data-toggle="tab"> Contraindicación </a>
                                                </li>
                                                <li class="nav-item">
                                                    <a href="#tab3" data-toggle="tab"> Observaciones </a>
                                                </li>
                                            </ul>
                                        </div>
                                        <div class="borderBox-body">
                                            <div class="tab-content">
                                                <div class="tab-pane active" id="tab1">

                                                </div>
                                                <div class="tab-pane" id="tab2">

                                                </div>
                                                <div class="tab-pane" id="tab3">
                                            
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
		            <button type="button" id="boton_modal" class="btn btn-success" data-dismiss="modal">Aceptar<i class="fa fa-check"></i></button>
			        <button type="button" class="btn btn-default" data-dismiss="modal">Cancelar <i class="fa fa-close"></i></button>
                </div>
            </div>
        </div>
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
                    return 'No se encotraron resultados <button type="button" class="btn btn-xs btn-success" onclick="Expediente(11)">¿Desea agregar el diagnóstico?</button>';
                },
            });

            $("#protocolos").select2({
                placeholder: "Seleccionar el protocolo",
                width: null,
                //multiple: true,
                containerCssClass: ':all:',
                minimumInputLength: 4,
                ajax: {
                    url: "./lista_protocolos.ashx",
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
                    return 'No se encotraron resultados';
                },
            });

            Expediente(1);
            //Leer diagnosticos
            Expediente(4);
            //Leer Protocolos
            Expediente(6);
            //Leer Historial de fechas
            Expediente(13);
            //Ocultar paneles
            Cambio();
        });

        function Cambio()
        {
            /*$('.estiramiento_contenedor').hide();
            $('.fortalecimiento_contenedor').hide();
            $('#estiramiento_contenedor_' + $('#estiramiento_sel option:selected').data('tipo')).show();
            $('#fortalecimiento_contenedor_' + $('#fortalecimiento_sel option:selected').data('tipo')).show();*/


            $('.estiramiento_contenedor').hide();
            $('.fortalecimiento_contenedor').hide();
            $('#estiramiento_contenedor_' + $('#estiramiento_sel option:selected').data('tipo')).show();
            $('#fortalecimiento_contenedor_' + $('#estiramiento_sel option:selected').data('tipo')).show();

        }

        function Expediente(opc,id)
        {
            //Empaquetar variables
            var prefijo = 'historial';

            var variable;
            var values = {};

            values['opcion'] = opc;
            values['id'] = id;
            //values['fecha'] = $('#fecha').val();
            //values['terapista'] = $('#terapista').val();
            values['idterapista'] = $('#terapista option:selected').data('id');
            values['tatendio'] = $('#atendio').val();
            values['idcita'] = $('#lblIdcita').html();
            values['turno'] = $('#hfturno').html();
            values['idcliente'] = $('#lblIdcliente').html();
            values['idcitafecha'] = $('#fecha option:selected').data('id');
            values['id_diagnostico'] = $('#diagnosticos').val();
            values['id_protocolo'] = $('#protocolos').val();
            values['fase'] = $('#fase_' + id + ' option:selected').val();
            values['observaciones'] = $('#observaciones').val();
            values['indicaciones_medicas'] = $('#indicaciones_medicas').val();
            values['contra_indicaciones'] = $('#contra_indicaciones').val();
            values['alergias'] = $('#alergias').val();
            values['nombre_diagnostico'] = $('#nombre_diagnostico').val();
           
            //var print = '[{';

            $('#' + prefijo).find(':input, select').each(function (index) {
                if ($(this).is(':checkbox'))
                {
                    values[String($(this).attr('id'))] = $(this).prop('checked') == true ? 1 : 0;
                }
                else
                {
                    values[String($(this).attr('id'))] = $(this).val();
                }
                //print += '""' + $(this).attr('id') + '"" : """& i.Item("' + $(this).attr('id') + '") &""",'
                //console.log('Dim ' + $(this).attr('id') + ' As String = context.Request("' + $(this).attr('id') + '")');
                
            });
            //print += '}]';
            //console.log(print);

            /*
            ultrasonido: $('#ultrasonido').prop('checked') == true ? 1:0,
                    chc: $('#chc').prop('checked') == true ? 1 : 0,
                    electroterapia: $('#electroterapia').prop('checked') == true ? 1 : 0,
                    laser: $('#laser').prop('checked') == true ? 1:0,
                    cf: $('#cf').prop('checked') == true ? 1 : 0,
                    ejercicio: $('#ejercicio').prop('checked') == true ? 1 : 0,
                    masaje: $('#masaje').prop('checked') == true ? 1 : 0,
                    magneto: $('#magneto').prop('checked') == true ? 1 : 0,
                    gimnasia: $('#gimnasia').prop('checked') == true ? 1 : 0,
              */




            $.ajax({
                type: 'POST',
                url: "./expedientemedico.ashx",
                async: false,
                data: values,
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
                            $('#fecha').val(registro.fecha);
                            $('#terapista').val(registro.terapista);
                            $('#atendio').val(registro.atendio);
                            $('#observaciones').val(registro.observaciones);
                            //console.log('KEY: '+ key+' Registro: '+registro);
                            //Variables
                            $.each(registro, function (variable, valor) {
                                //console.log("Variable: " + variable + " Valor: " + valor);
                                if ($('#' + variable).is(':checkbox')) {
                                    //console.log('es check');
                                    $('#'+variable).prop('checked', (valor == 1 ? true : false));
                                }
                                else
                                {
                                    $('#' + variable).val(valor.replace("\\", ""));
                                }
                                
                            });

                            
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

                            $('#terapista').empty();
                            var str3 = registro.terapista;
                            var array3 = str3.split(".");

                            var str4 = registro.idterapista;
                            var array4 = str4.split(".");
                            for (var c = 0; c < array3.length; c++) {
                                if ($('#hfposicion').text() == array4[c]) {
                                    $('#terapista').append('<option value="' + array3[c] + '" selected data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                                else {
                                    $('#terapista').append('<option value="' + array3[c] + '" data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                            }

                        });
                    }
                    if (opc == 2)//Guardar datos
                    {
                        Swal.fire({
                            type: 'success',
                            title: 'Los cambios se guardaron correctamente',
                            showConfirmButton: false,
                            timer: 1800
                        })
                        $("#atendio").val($('#terapista option:selected').text());
                    }
                    if (opc == 3)//Leer datos del expediente según la fecha
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
                            $('#fecha').val(registro.fecha);
                            $('#terapista').val(registro.terapista);
                            $('#atendio').val(registro.atendio);
                            $('#observaciones').val(registro.observaciones);
                            console.log('Opcion 3: KEY: ' + key + ' Registro: ' + registro);
                            //Variables
                            $.each(registro, function (variable, valor) {
                                console.log("Variable: " + variable + " Valor: " + valor);
                                if ($('#' + variable).is(':checkbox')) {
                                    //console.log('es check');
                                    $('#' + variable).prop('checked', (valor == 1 ? true : false));
                                }
                                else {
                                    $('#' + variable).val(valor.replace("\\", ""));
                                }

                            });


                            $('#fecha').empty();
                            var str3 = registro.fecha;
                            var array3 = str3.split(".");

                            var str4 = registro.idfecha;
                            var array4 = str4.split(".");
                            for (var c = 0; c < array3.length; c++) {
                                if (registro.idcitafecha == array4[c]) {
                                    $('#fecha').append('<option value="' + array3[c] + '" selected data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                                else {
                                    $('#fecha').append('<option value="' + array3[c] + '" data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                            }

                            $('#terapista').empty();
                            var str3 = registro.terapista;
                            var array3 = str3.split(".");

                            var str4 = registro.idterapista;
                            var array4 = str4.split(".");
                            for (var c = 0; c < array3.length; c++) {
                                if (registro.atendio == array4[c]) {
                                    $('#terapista').append('<option value="' + array3[c] + '" selected data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                                else {
                                    $('#terapista').append('<option value="' + array3[c] + '" data-id="' + array4[c] + '">' + array3[c] + '</option>');
                                }
                            }
                        });

                      
                    }
                    if (opc == 4)//Guardar y Agregar el diagnostico a la tabla
                    {
                        $("#tabla_diagnosticos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_diagnosticos tbody").append('<tr><td>' + registro.diagnostico + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(5,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }

                    }
                    if (opc == 5)//Guardar y Eliminar el diagnostico a la tabla
                    {

                        $("#tabla_diagnosticos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_diagnosticos tbody").html('<tr><td>' + registro.diagnostico + '</td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(5,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                            });
                        }
                        Swal.fire({
                            type: 'success',
                            title: 'Diagnostico Eliminado',
                            showConfirmButton: false,
                            timer: 1800
                        })

                    }
                    if (opc == 6)//Leer protocolos y llenar el select id=protocolos
                    {
                        /*$("#protocolos").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#protocolos").append('<option value="' + registro.id + '">' + registro.nombre + '</option>');
                            });
                        }*/
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td><select class="form-control" id="fase_' + registro.id + '" onchange="Expediente(10,' + registro.id + ')"><option value="1">Fase 1</option><option value="2">Fase 2</option><option value="3">Fase 3</option><option value="4">Fase 4</option><option value="5">Fase 5</option></select></td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                                $('#fase_' + registro.id).val(registro.fase);
                            });
                        }
                       
                    }
                    if (opc == 7) //Mostrar Modal con la información del protocolo (Aceptar agrega el protocolo, Cancelar no agrega el protocolo)
                    {
                        //Cambiar datos del modal
                        //$("#modal_body").empty();
                        $('#titulo_protocolo').html($('#protocolos option:selected').text());
                        //console.log("porotcolos:" + $('#protocolos option:selected').text());
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
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td><select class="form-control" id="fase_' + registro.id + '" onchange="Expediente(10,' + registro.id + ')"><option value="1">Fase 1</option><option value="2">Fase 2</option><option value="3">Fase 3</option><option value="4">Fase 4</option><option value="5">Fase 5</option></select></td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                                $('#fase_' + registro.id).val(registro.fase);
                            });
                        }
                    }
                    if (opc == 9)//Borrar protocolo 
                    {
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td><select class="form-control" id="fase_' + registro.id + '" onchange="Expediente(10,' + registro.id + ')"><option value="1">Fase 1</option><option value="2">Fase 2</option><option value="3">Fase 3</option><option value="4">Fase 4</option><option value="5">Fase 5</option></select></td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                                $('#fase_' + registro.id).val(registro.fase);
                            });
                        }
                    }
                    if (opc == 10)//Agregar la fase en el ultimo registro
                    {
                        $("#tabla_protocolos tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_protocolos tbody").append('<tr><td>' + registro.protocolo + '</td><td><select class="form-control" id="fase_'+ registro.id +'" onchange="Expediente(10,' + registro.id + ')"><option value="1">Fase 1</option><option value="2">Fase 2</option><option value="3">Fase 3</option><option value="4">Fase 4</option><option value="5">Fase 5</option></select></td><td>' + registro.fecha + '</td><td align="right"><button type="button" class="btn btn-danger" onclick="Expediente(9,' + registro.id + ')"><i class="fa fa-trash"></i> Borrar</button></td></tr>');
                                $('#fase_' + registro.id).val(registro.fase);
                                console.log(registro.fase);
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
                        Expediente(4);
                    }
                    if (opc == 13)//Leer próximas citas
                    {
                        $("#tabla_citas tbody").empty();
                        if (obj.resp == null) {
                            $.each(obj, function (key, registro) {
                                $("#tabla_citas tbody").append('<tr><td>' + registro.fecha + '</td><td>' + registro.atiende + '</td></tr>');
                            });
                        }

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
                                    <button type="button" class="btn btn-primary btn-block" data-dismiss="modal" onclick="Expediente(12)"><i class="fa fa-save"></i> Guardar</button>
					            </div>
					        </div>
					    </div>
					</div>
</body>
</html>
