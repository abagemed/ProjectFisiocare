<%@ Page Language="VB" AutoEventWireup="false" CodeFile="datosCitas.aspx.vb" Inherits="datosCitas" EnableEventValidation="false" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692FBEA5521E1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
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
    <script language="javascript" type="text/javascript">
    var ventanaCierra;
    function cierraventana(){
    ventanaCierra.close();
    }
    
    function calculaSaldo(){
    if(document.getElementById("txtimporte").value==""){
       document.getElementById("txtimporte").value=0
    }
    if(document.getElementById("txtabono").value==""){
       document.getElementById("txtabono").value=0
    }
    var1=document.getElementById("txtimporte").value;
    var2=document.getElementById("txtabono").value;
    var3=document.getElementById("txtSaldoA").value;
    document.getElementById("txtSaldoN").value=parseFloat(var1)+parseFloat(var3)-parseFloat(var2);
    }

    function miventana(){
    ventanaCierra=window.open("auxiliar.aspx?idcliente="+document.getElementById("lblIdcliente").innerHTML,"ordenes","toolbar=no,width=550,height=600,scrollbars=yes");
    }  
    function imprimir(){
    varTerapia=document.getElementById("cmbTipoterapia").value
    varIdcliente=document.getElementById("lblIdcliente").innerHTML
    varImporte=document.getElementById("txtimporte").value
    ventanaCierra=window.open("Recibo.aspx?varterapia="+varTerapia+"&varIdcliente="+varIdcliente+"&varImporte="+varImporte,"ordenes","toolbar=no,width=510,height=400,scrollbars=no");
    }  
    </script>
</head>
<body onload="javascript:if(history.length>0)history.go(+1)" class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
    <form id="form1" runat="server">

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
	                            <a href="https://facturacionsm.agemed.com.mx" target="_blank" class="nav-link ">
	                                <i class="fa fa-pencil"></i>
	                                <span class="title">Facturación</span>
                                	<span class="arrow"></span>
	                            </a>
	                           <%-- <ul class="sub-menu">
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
	                            </ul>--%>
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
                                <div class="page-title">Pagos</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-dollar"></i>&nbsp;<a class="parent-item" href="#">Pagos</a>
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
                                            <header>Pagos</header>
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
                                                    <asp:Label ID="lblfechaNom" runat="server"></asp:Label>
                                                    <asp:TextBox ID="TxtFechaCita" runat="server" Visible="False"></asp:TextBox>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="LinkButton2" runat="server" CssClass="btn btn-block btn-success"><i class="fa fa-close"></i> TERMINAR CITA</asp:LinkButton>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-block btn-info"><i class="fa fa-refresh"></i> CAMBIAR HORARIO</asp:LinkButton>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="LinkButton4" runat="server" CssClass="btn btn-block btn-danger"><i class="fa fa-ban"></i> CANCELAR CITA</asp:LinkButton>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:LinkButton ID="btmodatos" runat="server" CssClass="btn btn-block btn-warning"><i class="fa fa-pencil"></i> MODIFICAR DATOS</asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <asp:Label ID="lblNombre" runat="server" Text="Label" CssClass="h3 text-center"></asp:Label>
                                                </div>
                                            </div>

                                            <asp:Label ID="lblexito" runat="server" Text="Operacion Exitosa" Visible="false"></asp:Label>

                                            <hr />
                                            <div class="row" id="modatos" runat="server">
                                                <div class="col-md-3">
                                                    <asp:Label ID="Label7" runat="server" Text="Lugar de Atencion:"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:DropDownList ID="cmblatencion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Button ID="btgmoddatos" runat="server" Text="Actualizar" CssClass="btn btn-success btn-block" />
                                                </div>
                                                <div class="col-md-3">
                                                    <asp:Label ID="lblerror" runat="server"></asp:Label>
                                                    <asp:Label ID="lbCA" runat="server" Visible="False">0</asp:Label>
                                                    <asp:Label ID="lblexistencias" runat="server" Visible="False">0</asp:Label>
                                                </div>
                                            </div>

        
        <hr />
        <div id="realizado" runat="server">
            <div class="row">
                <div class="col-md-6">
                    <asp:Label ID="Label5" runat="server" Text="Doctor Remitente"></asp:Label>
                    <asp:DropDownList ID="cmbDoctores" runat="server" CssClass="form-control"></asp:DropDownList>

                </div>
                <div class="col-md-6">
                    <asp:Label ID="Label6" runat="server" ForeColor="#1A3773" Text="Catálogo de Costos"></asp:Label>
                    <asp:DropDownList ID="cmbcostos" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="lbldocc" runat="server" Text="Doctor/Consulta:_"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbDoctorC" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lbltipocm" runat="server" Text="Tipo de Consulta:_"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbTipoCM" runat="server" CssClass="form-control"></asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label15" runat="server" Text="Terapia"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbTipoterapia" runat="server" AutoPostBack="True" Visible="False" CssClass="form-control"></asp:DropDownList>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label4" runat="server" Text="Observaciones"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtobservacion" runat="server" CssClass="form-control"></asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label1" runat="server" Text="Importe"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtimporte" runat="server" AutoPostBack="True" ReadOnly="True" CssClass="form-control">0.0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label2" runat="server" Text="Abono(Total a Pagar)"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtabono" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
            </div>
           <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label10" runat="server" Text="Subtotal"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtsubtotal" runat="server" ReadOnly="True" CssClass="form-control">0.0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label11" runat="server" Text="Importe IVA"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtiva" runat="server" ReadOnly="True" CssClass="form-control">0</asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:CheckBox ID="chkCoaseguro" runat="server" Text="$ Coaseguro" />
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtmonto" runat="server" CssClass="forn-control"></asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label3" runat="server"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:DropDownList ID="cmbPago" runat="server" CssClass="form-control"> </asp:DropDownList>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label8" runat="server" Text="Cant.Terapias"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtcantidad" runat="server" CssClass="form-control">1</asp:TextBox>
                </div>
                
            </div>
            <br />
             <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label9" runat="server" Text="Clave SAT"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtcsat" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label13" runat="server" Text="Clave Unidad"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtcunidad" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                 
            </div>
            <br />
            <div class="row">
                <div class="col-md-3">
                    <asp:Label ID="Label12" runat="server" Text="Unidad"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtunidad" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="Label14" runat="server" Text="Tasa Iva"></asp:Label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txttasai" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:Label ID="lblaviso" runat="server" Text="seleccione tipo terapia" Visible="False"></asp:Label>
                </div>
            </div>
            <div class="row">
                <div class="col-md-12">
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToValidate="cmbTipoterapia"
                            ErrorMessage="Seleccionar tipo de Terapia" Operator="NotEqual" ValidationGroup="validaterapia" ValueToCompare="0"
                            ></asp:CompareValidator>
                </div>
            </div>
            <br />
            <div class="row">
                <div class="col-md-12">
                    <asp:Button ID="Button1" runat="server" Text="Agregar Terapia" ValidationGroup="validaterapia" CssClass="btn btn-info btn-block" />
                </div>
            </div>
            <br />
            
                       
            <hr />
            <asp:DataGrid ID="gridPagos" runat="server" AutoGenerateColumns="False" DataKeyField="columna5" CssClass="table table-bordered table-striped table-hover">
                <FooterStyle BackColor="Tan" />
                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="Control" />
                <Columns>
                    <asp:BoundColumn DataField="columna" HeaderText="Servicio"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna1" HeaderText="Pago"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna2" HeaderText="Observacion"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna3" HeaderText="Abono"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna4" HeaderText="Importe"></asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Coaseguro">
                        <ItemTemplate>
                            <asp:CheckBox ID="CheckBox2" runat="server" Checked='<%# Bind("columna6") %>' Enabled="False" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="columna7" HeaderText="Monto"></asp:BoundColumn>
                     <asp:BoundColumn DataField="columna8" HeaderText="Cant"></asp:BoundColumn>
                  <asp:BoundColumn DataField="columna9" HeaderText="C.Sat"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna10" HeaderText="subtotal"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna11" HeaderText="iva"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna12" HeaderText="C.Unidad"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna13" HeaderText="Unidad"></asp:BoundColumn>
                    <asp:BoundColumn DataField="columna14" HeaderText="Tasa Iva"></asp:BoundColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton1" runat="server" CommandName="imprmir" Height="14px"
                                ImageUrl="~/imagenes/removecell.png" Width="14px" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
                <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" ForeColor="#1A3773" />
            </asp:DataGrid>
        </div>



        <div id="cancelado" runat="server">
            <h3>MOTIVO DE CANCELACIÓN</h3>
            <div class="row">
                <div class="col-md-12">
                    <textarea id="txtcancelacion" runat="server" class="form-control"></textarea>
                </div>
                <div class="col-md-12">
                     <asp:Label ID="lblIdcita" runat="server" Text="Label"></asp:Label>
                    <asp:Label ID="lblIdcliente" runat="server"></asp:Label>
                </div>
                <div class="col-md-12">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Debe especificar un Motivo" ControlToValidate="txtcancelacion" ValidationGroup="micancelacion"></asp:RequiredFieldValidator>
                </div>
            </div>         
      </div>
                                            
        <div id="misbotones" runat="server">
            
            <table id="TABLE1" language="javascript" onclick="return TABLE1_onclick()" border="0">
                <tr>
                    <td>
                        <asp:Label ID="lblrecibo" runat="server" Visible="false"></asp:Label>
                        <asp:Label ID="txtfecha" runat="server" Text="Recibo" Visible="false"></asp:Label>
                        <asp:CheckBox ID="chkcierre" runat="server" Text="CERRAR TRATAMIENTO (ULTIMA SESION)" Visible="False" />&nbsp;
                        <asp:Label ID="LblBandera" runat="server" Visible ="true"></asp:Label>
                        <asp:Button ID="lnkGrabar" runat="server" ValidationGroup="micancelacion" OnClientClick="javascript:this.disabled=true;" UseSubmitBehavior="False" Text="Grabar Datos" CssClass="btn btn-info"/></td>
                    <CR:CrystalReportViewer ID="visor_reporte" runat="server" AutoDataBind="True" />
                    <CR:CrystalReportSource ID="origen_reporte" runat="server"></CR:CrystalReportSource>
                </tr>
       </table>
       <asp:Label ID="lblturno" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblhorario" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblposicion" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblduracion" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblimportei" runat="server" Text="Label" Visible="False"></asp:Label>
        <input id="tempDoc" style="width: 7px" type="hidden" runat="server" />
        <input id="tempDocc" style="width: 7px" type="hidden" runat="server" />
        <input id="temptipocm" style="width: 7px" type="hidden" runat="server" />
        <input id="tempdatosF" style="width: 7px" type="hidden" runat="server" />
        <input id="templatencion" style="width: 7px" type="hidden" runat="server" />
        <asp:Label ID="lblhorariovalido" runat="server" Text="Label" Visible="False"></asp:Label>
        <input id="hfCortesias" style="width: 1px" type="hidden" runat="server" />
            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
                EnableScriptLocalization="True">
            </asp:ScriptManager>
        <asp:Button ID="LinkButton6" runat="server"  Font-Size="11pt" ForeColor="Red" Text="Regresar a Agenda" Width="133px" Font-Bold="False" Font-Names="calibri" CssClass="boton" Visible="False"/>
        <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
            TargetControlID="txtabono" ValidChars="1234567890.">
        </ajaxToolkit:FilteredTextBoxExtender>
            <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" runat="server"
            TargetControlID="txtimporte" ValidChars="1234567890.">
        </ajaxToolkit:FilteredTextBoxExtender>
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
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
</body>
</html>
