<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default"%>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<%@ Register Assembly="System.Web.Extensions, Version=1.0.61025.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35"
    Namespace="System.Web.UI" TagPrefix="asp" %>
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
    <link href="assets/css/pages/typography.css" rel="stylesheet" type="text/css" />
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <!-- DataTables -->
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.css"/>


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
                    <!--img alt="" src="imagenes/logo.png"-->
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
                        <!-- start notification dropdown -->
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="Agendados">
                                <i class="fa fa-calendar mdl-badge mdl-badge--overlap" data-badge="<%= lbAgendados.Text%>" id="not_1"></i>
                                <asp:Label ID="lbAgendados" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="Finalizado">
                                <i class="fa fa-star mdl-badge mdl-badge--overlap" data-badge="<%= lbAtendidos.Text%>"></i>
                                <asp:Label ID="lbAtendidos" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="#" runat="server" onserverclick="VerCancelados" class="dropdown-toggle"  data-tooltip="tooltip" data-placement="bottom" title="Cancelado">
                                <i class="fa fa-close mdl-badge mdl-badge--overlap" data-badge="<%= lbCancelados.Text%>"></i>
                                <asp:Label ID="lbCancelados" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="CA-Cama">
                                <i class="material-icons mdl-badge mdl-badge--overlap" data-badge="<%= lbConfirmados.Text%>">hotel</i>
                                <asp:Label ID="lbConfirmados" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="Re-Reposet">
                                <i class="material-icons mdl-badge mdl-badge--overlap" data-badge="<%= lbEnEspera.Text%>">airline_seat_flat</i>
                                <asp:Label ID="lbEnEspera" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="CD - C.Domicilio">
                                <i class="fa fa-home mdl-badge mdl-badge--overlap" data-badge="<%= lbdomicilio.Text%>"></i>
                                <asp:Label ID="lbdomicilio" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="CM-C. Medica">
                                <i class="fa fa-user-md mdl-badge mdl-badge--overlap" data-badge="<%= lblcmedica.Text%>"></i>
                                <asp:Label ID="lblcmedica" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        <li class="dropdown dropdown-extended dropdown-notification">
                            <a href="javascript:;" class="dropdown-toggle" data-toggle="dropdown" data-hover="dropdown" data-close-others="true" data-tooltip="tooltip" data-placement="bottom" title="OP-Opcional">
                                <i class="material-icons mdl-badge mdl-badge--overlap" data-badge="<%= lbEnEstudio.Text%>">filter_tilt_shift</i>
                                <asp:Label ID="lbEnEstudio" runat="server" Visible="false">0</asp:Label>
                            </a>
                        </li>
                        
                       
                        
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
                            <%--menú configuración mmoreno 05abril2021--%>

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
                   <%-- <div class="page-bar">
                        <div class="page-title-breadcrumb">
                            <div class=" pull-left">
                                <div class="page-title">Agenda</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-calendar"></i>&nbsp;<a class="parent-item" href="#">Agenda</a>
                                </li>
                            </ol>
                        </div>
                    </div>--%>
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-box">
                                        <div class="card-head">
                                            <header>
                                                <asp:Label ID="lblfecNom" runat="server"></asp:Label>
                                                <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="False"></asp:Label>
                                            </header>
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                        <%--    <button type="button" class="mdl-button mdl-js-button mdl-button--raised mdl-js-ripple-effect btn-circle btn-primary" data-toggle="modal" data-target="#modal_opciones">Modal</button>--%>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" ValidationGroup="fecha" CssClass="btn btn-info btn-block btn-lg"><i class="fa fa-calendar"></i> Cambiar Fecha</asp:LinkButton>
                                                </div>
                                            </div>
                                            
                                            <hr />
                                            <asp:ScriptManager ID="ScriptManager2" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True">
                                            </asp:ScriptManager>
                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                <ContentTemplate>
                   
                                                </ContentTemplate>
                                            </asp:UpdatePanel>

                                            <div>

                                                <asp:DataGrid ID="gridHorarios" runat="server" AutoGenerateColumns="False" DataKeyField="idHorario" CssClass="table table-bordered table-striped table-hover" >
                                                    <Columns>
                                                        <asp:BoundColumn DataField="horario" HeaderText="HORARIOS" Visible="True" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk1" runat="server" CommandName="lnk1" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl1" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk2" runat="server" CommandName="lnk2" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl2" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk3" runat="server" CommandName="lnk3" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl3" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk4" runat="server" CommandName="lnk4" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl4" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk5" runat="server" CommandName="lnk5" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl5" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk6" runat="server" CommandName="lnk6" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl6" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk7" runat="server" CommandName="lnk7" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl7" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="false">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk8" runat="server" CommandName="lnk8" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl8" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="false">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lnk9" runat="server" CommandName="lnk9" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                             <asp:Label ID="lbl9" runat="server" Visible="False"></asp:Label>
                                                         </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--<asp:TemplateColumn HeaderText="SALA" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                             <ItemTemplate>
                                                                  <asp:LinkButton ID="lnk10" runat="server" CommandName="lnk10" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                  <asp:Label ID="lbl10" runat="server" Visible="False"></asp:Label>
                                                             </ItemTemplate>
                                                        </asp:TemplateColumn>--%>
                                                    </Columns>
                                                </asp:DataGrid>
                                                <asp:DataGrid ID="gridHorariosVespertino" runat="server" AutoGenerateColumns="False" DataKeyField="idHorario" CssClass="table table-bordered table-striped table-hover" >
                                                    <Columns>
                                                        <asp:BoundColumn DataField="horario" HeaderText="VESPERTINO" Visible="True" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="MARIANA" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk1" runat="server" CommandName="lnk1" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl1" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="MARIANA" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk2" runat="server" CommandName="lnk2" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl2" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="MELISSA" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk3" runat="server" CommandName="lnk3" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl3" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="MELISSA" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk4" runat="server" CommandName="lnk4" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl4" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ALE" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk5" runat="server" CommandName="lnk5" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl5" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ALE" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk6" runat="server" CommandName="lnk6" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl6" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ROGER" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk7" runat="server" CommandName="lnk7" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl7" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="ROGER" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk8" runat="server" CommandName="lnk8" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl18" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="PASANTE" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lnk9" runat="server" CommandName="lnk9" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                             <asp:Label ID="lbl9" runat="server" Visible="False"></asp:Label>
                                                         </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="PASANTE" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true">
                                                             <ItemTemplate>
                                                                  <asp:LinkButton ID="lnk10" runat="server" CommandName="lnk10" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                  <asp:Label ID="lbl10" runat="server" Visible="False"></asp:Label>
                                                             </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
                                                <ajaxToolkit:ModalPopupExtender BackgroundCssClass="FondoAplicacion" PopupControlID="Panel1" ID="ModalPopupExtender1" runat="server" TargetControlID="LinkButton1">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
                                                <asp:Panel ID="Panel1" runat="server">
                                                    <div class="modal-dialog modal-lg" role="document">
					                                    <div class="modal-content">
					                                        <div class="modal-header">
					                                            <h4 class="modal-title" id="exampleModalLabel">Seleccionar fecha</h4>
					                                        </div>
					                                        <div class="modal-body">
                                                                <asp:Calendar ID="calendario" runat="server" BorderColor="#F4F4F4" BorderStyle="Solid"
                                                                    BorderWidth="2px" Font-Names="calibri" Font-Size="12pt" Height="298px" ShowGridLines="True"
                                                                    Width="368px">
                                                                    <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
                                                                    <DayStyle BorderColor="Silver" Font-Bold="False" />
                                                                    <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                                                                    <TitleStyle BackColor="#F4F4F4" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"
                                                                        ForeColor="#1A3773" />
                                                                </asp:Calendar>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                 <ajaxToolkit:ModalPopupExtender ID="mpuprolTE" runat="server" PopupControlID="pnlrolTE" TargetControlID="hfcita" CancelControlID="BTNcerrar" BackgroundCssClass="FondoAplicacion">
                                                </ajaxToolkit:ModalPopupExtender>
                                                <asp:HiddenField runat="server" ID="hfcita" />
                                                <asp:HiddenField runat="server" ID="hffechacita" />
                                                <%--<asp:HiddenField runat="server" ID="hfcondicion" />--%>
                                                <asp:HiddenField runat="server" ID="hfduracion" />
                                                <asp:HiddenField runat="server" ID="hfposicion" />
                                                <asp:HiddenField runat="server" ID="hfidhorario" />
                                                 <asp:HiddenField runat="server" ID="hfturno" />
                                                <asp:Timer ID="Timer1" runat="server" Interval="600000"></asp:Timer>
                                                <asp:Panel runat="server" ID="pnlrolTE" CssClass="panelpp">
                <div class="example-modal">
                    <div class="modal-1">
                        <div class="modal-dialog">
                            <div class="modal-content" dir="ltr">
                                <div class="modal-header">
                                    <h4 class="modal-title">Gestion del Paciente</h4>
                                </div>
                                <div class="modal-body">
                                     <asp:LinkButton ID="lkb1" Text="Administrativo" class="btn btn-success" runat="server" Font-Names="Calibri" Font-Size="16px"></asp:LinkButton>
                                     <asp:LinkButton ID="lkb2" Text="Expediente Medico" class="btn btn-primary" runat="server" Font-Names="Calibri" Font-Size="16px"></asp:LinkButton>
                                 
                                </div>
                                <div class="modal-footer">
					                <button id="BTNcerrar" type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
					            </div>

                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </div>
                <!-- /.example-modal -->
            </asp:Panel>



                                                
    <!---MODAL-->
                 
                  <%-- <div class="modal fade" id="modal_opciones" tabindex="-1" role="dialog" aria-hidden="true">
					    <div class="modal-dialog" role="document">
					        <div class="modal-content">
					            <div class="modal-header">
					                <h4 class="modal-title">Opciones</h4>
					                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
					                    <span aria-hidden="true">&times;</span>
					                </button>
					            </div>
					            <div class="modal-body">
					                <a href="expedientemedico.aspx" class="btn btn-primary">Expediente</a>
                                    <a href="datosCitas.aspx" class="btn btn-success">Gestión Agenda</a>
					            </div>
					            <div class="modal-footer">
					                <button type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
					            </div>
					        </div>
					    </div>
					</div>--%>
                          

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
            <div class="page-footer-inner"> 2020 &copy; FisioCare
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
        var grid = document.getElementById('gridHorarios');
        var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
        var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

        tbody.removeChild(firstTr); //remove tr's from table

        var newTh = document.createElement('thead'); //creates thead
        newTh.appendChild(firstTr); //puts ths in thead
        grid.insertBefore(newTh, tbody); //puts thead before tbody
        $(function () {


            $('#gridHorarios').DataTable({
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
