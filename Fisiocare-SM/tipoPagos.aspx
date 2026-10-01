<%@ Page Language="VB" AutoEventWireup="false" CodeFile="tipoPagos.aspx.vb" Inherits="tipoPagos" %>

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
	                            <a href="#" class="nav-link nav-toggle">
	                                <i class="fa fa-pencil"></i>
	                                <span class="title">Facturación</span>
                                	<span class="arrow"></span>
	                            </a>
	                            <ul class="sub-menu">
	                                <li class="nav-item">
	                                    <a href="https://facturacionsm.agemed.com.mx" target="_blank" class="nav-link ">
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

                            <li class="nav-item">
                                <a href="#" class="nav-link nav-toggle">
                                    <i class="fa fa-cog"></i>
                                    <span class="title">Configuración</span>
                                    <span class="arrow"></span>
                                </a>
                                <ul class="sub-menu">
                                    <li class="nav-item ">
                                        <a href="#" runat="server" onserverclick="CTerapeutas" class="nav-link nav-toggle">
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
                                <div class="page-title">Catálogos</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-list-ol"></i>&nbsp;<a class="parent-item" href="#">Catálogos</a>
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
                                            <header>Reporte de pagos</header>
                                            
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
                                            
                                                <div id="seleccionar" runat="server">
                                                    <ajaxToolkit:TabContainer ID="contenedor" runat="server" ActiveTabIndex="0">
                                                         <ajaxToolkit:TabPanel ID="TabPaneldoc" runat="server" HeaderText="TabPaneldoc" CssClass="tab-content">
                                                            <ContentTemplate>
                                                               <div class="form-group row">
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label22" runat="server" Text="Nombre"></asp:Label>
                                                                    </div>
                                                                   <div class="col-md-2">
                                                                        <asp:TextBox ID="dtxtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="idtxtnombre" runat="server" 
                                                                            ControlToValidate="dtxtnombre" Display="Dynamic"
                                                                            ErrorMessage="*" Font-Bold="False" Font-Italic="False"
                                                                            Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True"
                                                                            ValidationGroup="gDatosde" Width="165px">Escribir Nombre</asp:RequiredFieldValidator>
                                                                   </div>
                                                                   <div class="col-md-2">
                                                                        <asp:Label ID="Label23" runat="server" Text="Paterno"></asp:Label>
                                                                   </div>
                                                                   <div class="col-md-2">
                                                                        <asp:TextBox ID="dtxtpaterno" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="idtxtpaterno" runat="server"
                                                                            ControlToValidate="dtxtpaterno" Display="Dynamic"
                                                                            ErrorMessage="*" Font-Bold="False" Font-Italic="False"
                                                                            Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True"
                                                                            ValidationGroup="gDatosde" Width="165px">Escribir Apellido Paterno</asp:RequiredFieldValidator>
                                                                   </div>
                                                                   <div class="col-md-2">
                                                                        <asp:Label ID="Label24" runat="server" Text="Materno"></asp:Label>
                                                                   </div>
                                                                   <div class="col-md-2">
                                                                        <asp:TextBox ID="dtxtmaterno" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator
                                                                    ID="idtxtmaterno" runat="server" ControlToValidate="dtxtmaterno" Display="Dynamic"
                                                                    ErrorMessage="*" Font-Bold="False" Font-Italic="False"
                                                                    Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True"
                                                                    ValidationGroup="gDatosde" Width="165px">Escribir Apellido Materno</asp:RequiredFieldValidator>
                                                                   </div>
                           
                                                                </div>
                                                                <div class="form-group row">
                                                                    <div class="col-md-12">
                                                                       <asp:LinkButton ID="dlnkDocs" runat="server" CssClass="btn btn-success btn-block" ValidationGroup="gDatosde"><i class="fa fa-save"></i> GRABAR</asp:LinkButton>
                                                                   </div>
                                                                </div>


                                                            <div class="table-responsive">
                                                                <asp:DataGrid ID="dgridDoctores" runat="server" AutoGenerateColumns="False"  DataKeyField="iddoctore" CssClass="table table-bordered table-striped table-hover" >
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="nombre" HeaderText="NOMBRE"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="paterno" HeaderText="APELLIDOO PATERNO"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="materno" HeaderText="APELLIDO MATERNO"></asp:BoundColumn>
                                                                        <asp:EditCommandColumn CancelText="Cancelar" EditText="Editar" UpdateText="Actualizar" ButtonType="PushButton">
                                                                        </asp:EditCommandColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </div>
                   
                                                            </ContentTemplate>
                                                            <HeaderTemplate>
                                                                <p>DOCTOR ENVIO</p>
                                                            </HeaderTemplate>
                                                        </ajaxToolkit:TabPanel>


                                                        <ajaxToolkit:TabPanel ID="TabPanel4" runat="server" HeaderText="TabPanel3">
                                                            <ContentTemplate>                                                               
                                                                <div class="form-group row">
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label13" runat="server" Text="Nombre"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="itxtnombre" runat="server" ControlToValidate="txtnombre" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatosdr" Width="165px">Escribir Nombre</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label14" runat="server" Text="Paterno"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtpaterno" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator  ID="itxtpaterno" runat="server" ControlToValidate="txtpaterno" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True"  ValidationGroup="gDatosdr" Width="165px">Escribir Apellido Paterno</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label15" runat="server" Text="Materno"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtmaterno" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="itxtmaterno" runat="server" ControlToValidate="txtmaterno" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatosdr" Width="165px">Escribir Apellido Materno</asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-12">
                                                                        <asp:LinkButton ID="lnkDocs" runat="server" CssClass="btn btn-primary btn-block" ValidationGroup="gDatosdr"><i class="fa fa-save"></i> GRABAR</asp:LinkButton>
                                                                    </div>
                                                                </div>



                                                            <div class="table-responsive">
                                                                <asp:DataGrid ID="gridDoctores" runat="server" AutoGenerateColumns="False" DataKeyField="ID" CssClass="table table-bordered table-striped table-hover">
                                                                    <AlternatingItemStyle BackColor="WhiteSmoke" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="nombre" HeaderText="NOMBRE"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="paterno" HeaderText="APELLIDOO PATERNO"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="materno" HeaderText="APELLIDO MATERNO"></asp:BoundColumn>
                                                                        <asp:EditCommandColumn CancelText="Cancelar"  EditText="Editar" UpdateText="Actualizar" ButtonType="PushButton">
                                                                        </asp:EditCommandColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                                                            </div>
                   
                                                            </ContentTemplate>
                                                            <HeaderTemplate>
                                                                DOCTOR REMITENTE
                                                            </HeaderTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                        <ajaxToolkit:TabPanel ID="TabPanedoc" runat="server" HeaderText="TabPanel3">
                                                            <ContentTemplate>

                                                                <div class="form-group row">
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label16" runat="server" Text="Nombre"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtnombredc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RFnombre" runat="server" ControlToValidate="txtnombredc" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatosdc" Width="165px">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label17" runat="server" Height="20px" Text="Paterno" ></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtapaternodc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RFapaterno" runat="server" ControlToValidate="txtapaternodc" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False"  Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatosdc" Width="165px">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label18" runat="server" Text="Materno"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtamaternodc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        <asp:RequiredFieldValidator ID="RFamaterno" runat="server" ControlToValidate="txtamaternodc" Display="Dynamic" ErrorMessage="*" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatosdc" Width="165px">*</asp:RequiredFieldValidator>
                                                                    </div>
                                                                </div>
                                                                

                                                                <div class="form-group row">
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label19" runat="server" Text="Especialidad"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtespecialidaddc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label20" runat="server" Text="Direccion"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txtdirecciondc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:Label ID="Label21" runat="server" Text="Telefono"></asp:Label>
                                                                    </div>
                                                                    <div class="col-md-2">
                                                                        <asp:TextBox ID="txttelefonodc" runat="server" CssClass="form-control"></asp:TextBox>
                                                                    </div>
                                                                </div>

                                                                <div class="form-group row">
                                                                    <div class="col-md-12">
                                                                        <asp:LinkButton ID="Linkgrabardc" runat="server" CssClass="btn btn-warning btn-block" ValidationGroup="gDatosdc"><i class="fa fa-save"></i> GRABAR</asp:LinkButton>
                                                                    </div>
                                                                </div>

                                                            <div style="overflow:auto; height:450px">
                                                                <asp:DataGrid ID="griddoctorc" runat="server" AutoGenerateColumns="False" DataKeyField="iddoctorc" CssClass="table table-bordered table-striped table-hover">
                                                                    <AlternatingItemStyle BackColor="WhiteSmoke" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="nombre" HeaderText="NOMBRE"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="apaterno" HeaderText="A.PATERNO"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="amaterno" HeaderText="A.MATERNO"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="especialidad" HeaderText="ESPECIALIDAD"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="direccion" HeaderText="DIRECCION"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="telefono" HeaderText="TELEFONO"></asp:BoundColumn>
                                                                        <asp:EditCommandColumn CancelText="Cancelar"  EditText="Editar" UpdateText="Actualizar" ButtonType="PushButton">
                                                                        </asp:EditCommandColumn>
                                                                    </Columns>
                                                                </asp:DataGrid></div>
                   
                                                            </ContentTemplate>
                                                            <HeaderTemplate>
                                                                DOCTOR CONSULTA
                                                            </HeaderTemplate>
                                                        </ajaxToolkit:TabPanel>



                                                        <ajaxToolkit:TabPanel ID="TabPanel5" runat="server" HeaderText="TabPanel3">
                                                            <ContentTemplate>
                                                                <br />
                                                                <div class="form-group row">
                                                                    <div class="col-md-12">
                                                                        <asp:Button ID="btnNuevo" runat="server" Text="Agregar Nuevo" CssClass="btn btn-primary btn-block"  />
                                                                    </div>
                                                                </div>


                                                                <div style="overflow:auto; height:450px">
                                                                <asp:DataGrid ID="gridclientes" runat="server" AutoGenerateColumns="False" DataKeyField="iddatosfac" CssClass="table table-bordered table-striped table-hover">
                                                                    
                                                                    <AlternatingItemStyle BackColor="WhiteSmoke" />
                                                                    <Columns>
                                                                        <asp:BoundColumn DataField="iddatosfac" HeaderText="ID"></asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="nombre" HeaderText="NOMBRE">
                                                                           
                                                                        </asp:BoundColumn>
                                                                        <asp:BoundColumn DataField="rfc" HeaderText="RFC"></asp:BoundColumn>
                                                                        <asp:TemplateColumn>
                                                                            <ItemTemplate>
                                                                                <asp:Button ID="Button2" runat="server" Font-Names="Calibri" Font-Size="12pt" Text="Editar" CssClass="boton" Font-Bold="False" ForeColor="Red" />
                                                                            </ItemTemplate>
                                                                            <ItemStyle HorizontalAlign="Center" />
                                                                        </asp:TemplateColumn>
                                                                    </Columns>
                                                                </asp:DataGrid>
                    
                                                           </div>
                                                            </ContentTemplate>
                                                            <HeaderTemplate>
                                                                CLIENTES FACTURAS
                                                            </HeaderTemplate>
                                                        </ajaxToolkit:TabPanel>
                                                    </ajaxToolkit:TabContainer></div>
                                                <cc1:messagebox ID="Messagebox1" runat="server" />
                                                <asp:ScriptManager ID="ScriptManager1" runat="server">
                                                </asp:ScriptManager>

                                        
                                        <asp:Panel ID="Panel1" runat="server">
                                            <div class="modal-dialog modal-lg" role="document">
					                            <div class="modal-content">
					                                <div class="modal-header">
					                                    <h4 class="modal-title" id="exampleModalLabel">Nuevo</h4>
					                                </div>
					                                <div class="modal-body">
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label3" runat="server" Text="Nombre"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtnombreF" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label8" runat="server" Text="C.P."></asp:Label>
                                                   
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtcp" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label4" runat="server" Text="R.F.C."></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtrfc" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label9" runat="server" Text="Municipio"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtmunicipio" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label5" runat="server" Text="Calle"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtcalle" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label10" runat="server" Text="Localidad"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                               <asp:TextBox ID="txtciudad" runat="server" CssClass="form-control"></asp:TextBox>

                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label6" runat="server" Text="No. Ext."></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtnoext" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label11" runat="server" Text="Estado"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtestado" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label7" runat="server" Height="20px" Text="Colonia"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtcolonia" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label12" runat="server" Text="País"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtpais" runat="server" CssClass="form-control">MEXICO</asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label1" runat="server" Text="Forma de Pago"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:DropDownList ID="cmbformapago" runat="server" Width="230px" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label2" runat="server" Text="No. Cuenta"></asp:Label>
                                                            </div>
                                                            <div class="col-md-4">
                                                                <asp:TextBox ID="txtcuenta" runat="server" CssClass="form-control"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                        <div class="form-group row">
                                                            <div class="col-md-2">
                                                                <asp:Label ID="Label25" runat="server" Text="Regimen Fiscal"></asp:Label>
                                                            </div>
                                                            <div class="col-md-8">
                                                                <asp:DropDownList ID="cmbregimenfiscal" runat="server" Width="400px" CssClass="form-control"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
					                                <div class="modal-footer">
					                                    <asp:Button ID="btnGrabar" runat="server" Text="Grabar Datos" CssClass="btn btn-success"/>
                                                        <asp:Button ID="Button1" runat="server" Text="Salir" CssClass="btn btn-default" />
					                                </div>
					                            </div>
					                        </div>
                                        </asp:Panel>
                                                <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
                                                    FilterMode="InvalidChars" InvalidChars='- *+/!"#$%()~' TargetControlID="txtrfc">
                                                </ajaxToolkit:FilteredTextBoxExtender>
                                                <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
                                                   PopupControlID="Panel1" TargetControlID="Hidden1">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <input id="Hidden1" runat="server" type="hidden" />
                                                <input id="hfBandera" runat="server" type="hidden" />
                                                <input id="hfIdDatos" runat="server" type="hidden" /><br />
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
