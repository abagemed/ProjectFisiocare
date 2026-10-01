<%@ Page Language="VB" AutoEventWireup="false" CodeFile="clientes.aspx.vb" Inherits="clientes" EnableEventValidation="false" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>
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
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    
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
    alert("LOS CAMPOS DE APELLIDOS Y NOMBRE NO DEBEN DE ESTAR VACIOS");
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
	                                    <small>fisiocp</small>
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
                                <a href="VerCitas.aspx" class="nav-link nav-toggle">
                                    <i class="fa fa-search"></i>
                                    <span class="title">VerCitas</span>
                                    <span class="arrow"></span>
                                </a>
                            </li>
                            
	                        <li class="nav-item">
	                            <a href="#" runat="server" onServerClick="Facturacion" class="nav-link nav-toggle">
                                    <i class="fa fa-pencil"></i>
                                    <span class="title">Facturación</span>
                                    <span class="arrow"></span>
                                </a>
	                            <%--<ul class="sub-menu">
	                                <li class="nav-item">
	                                    <a href="https://facturacioncp.agemed.com.mx" target="_blank" class="nav-link ">
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
                                <div class="page-title">Búsqueda de pacientes</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-search"></i>&nbsp;<a class="parent-item" href="#">Búsqueda de pacientes</a>
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
                                            <header>BÚSQUEDA DE PACIENTES</header>
                                            
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                       
                                        <%--PANEL DE AVISOS--%>
                        <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-success alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
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
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i></button>
                                        <h4>
                                            <asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                        <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi_Click"><i class="fa fa-check"></i> Sí</button>
                                        <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo_Click"><i class="fa fa-close"></i> No</button>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelCritico" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-danger alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label>
                                        </h4>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelAdvertencia" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMensajeAdvertencia" runat="server" Text="Label"></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                                       
                                        <div class="card-body">
                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <asp:LinkButton ID="LinkButton5" runat="server"  OnClientClick="cierraventana();" CssClass="btn btn-block btn-info" ><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</asp:LinkButton>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                    <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-block btn-success"><i class="fa fa-plus-square"></i> Agregar Cliente</asp:LinkButton>
                                                </div>
                                            </div>
                                             <hr />
                                            <div id="buscacliente" runat="server">
                                           <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                     <span>Apellido Paterno</span>
                                                    <asp:TextBox ID="TxbPaterno" runat="server" CssClass="form-control" ToolTip="Apellido paterno"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4 col-md-3 col-sm-12 col-xs-12">
                                                    <span>Apellido Materno</span>
                                                    <asp:TextBox ID="TxbMaterno" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <span>Nombre del Paciente</span>
                                                    <asp:TextBox ID="TxbNombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>

                                            <div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <button type="button" class="btn btn-primary btn-block" runat="server" onserverclick="BtnBuscarPaciente_Click"><i class="fa fa-search"></i> Buscar</button>
                                                </div>
                                            </div>

                                            <div class="row margin-right-10">
                                                <div class="col-md-12">
                                                    <h3>Resultados de la búsqueda</h3>
                                                    <%--<select class="form-control" id="LstBoxPacientes" size="5">

                                                    </select>--%>
                                                     <asp:ListBox ID="LstBoxPacientes" runat="server" CssClass="form-control"></asp:ListBox>
                                                </div>
                                            </div>

                                            <div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <button type="button" class="btn btn-success btn-block" runat="server" onserverclick="BtnVer_Click"><i class="fa fa-info"></i> Ver Paciente</button>
                                                </div>
                                            </div>
                                           </div>
                                        </div>
                                           


                                        <div class="card-body">
                                            


                                            <div class="row">
                                                <div class="col-lg-12">
                                                   <%--<hr />
                                                    <div id="buscacliente" runat="server">

                                                        <div class="row">
                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="card-body">
                                                                    <div class="form-group row">
                                                                        <label class="col-md-5 control-label">Primer Apellido</label>
                                                                        <div class="input-group col-md-7">
                                                                            <span class="input-group-addon"><span class="fa fa-font"></span></span>
                                                                            <asp:TextBox ID="ctxtPaternoB" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-md-5 control-label">Segundo Apellido</label>
                                                                        <div class="input-group col-md-7">
                                                                            <span class="input-group-addon"><span class="fa fa-font"></span></span>
                                                                            <asp:TextBox ID="ctxtMaternoB" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="form-group row">
                                                                        <label class="col-md-5 control-label">Nombre</label>
                                                                        <div class="input-group col-md-7">
                                                                            <span class="input-group-addon"><span class="fa fa-font"></span></span>
                                                                            <asp:TextBox ID="ctxtnombreB" runat="server" CssClass="form-control"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                    <div class="row">
                                                                        <div class="col-md-12">
                                                                            <asp:Button ID="buscarcliente" runat="server" Text="Buscar Paciente" CssClass="btn btn-block btn-success" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-md-6 col-sm-12">
                                                                <div class="card-body" >
                                                                    <asp:DataGrid ID="cgridClientes" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" DataKeyField="idcliente">
                                                                      <Columns>
                                                                          <asp:BoundColumn DataField="elnombre" HeaderText="NOMBRE" >
                                                                          </asp:BoundColumn>
                                                                          <asp:TemplateColumn>
                                                                              <ItemTemplate>
                                                                                  <asp:LinkButton ID="cLinkButton3" runat="server" CssClass="btn btn-block btn-warning"><i class="fa fa-eye"></i> SELECCIONAR</asp:LinkButton>
                                                                              </ItemTemplate>
                                                                          </asp:TemplateColumn>
                                                                      </Columns>
                              
                                                                  </asp:DataGrid>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>--%>

                                                    <hr />        
                                                    <div id="seleccionar" runat="server">
                                                        <asp:Literal ID="Literal1" runat="server"></asp:Literal>
                                                        <asp:DropDownList ID="cmbclientes" runat="server" Visible="true" AutoPostBack="True" CssClass="hidden"></asp:DropDownList>
                                                        
                                                        <div id="masdatos" runat="server" visible="false">
                                                            <div class="form-group row">
                                                                <asp:Label ID="lblelHorario" runat="server" CssClass="col-md-5 control-label"></asp:Label>
                                                            </div>
                                                            

                                                            <asp:DataGrid ID="gridDias" runat="server" visible="false" AutoGenerateColumns="False" CellPadding="2" ShowHeader="False" Style="font-size: 8pt; font-family: Calibri;
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

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label13" runat="server" CssClass="col-md-2 control-label" Text="Duración Terapia"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbDuracion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label14" runat="server" Text="Observaciones" CssClass="input-group col-md-2"></asp:Label>
                                                                <div class="input-group col-md-10">
                                                                    <asp:TextBox ID="txtobservaciones"  runat="server" MaxLength="50" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Label ID="lbllatencion" runat="server" Text="L.Atencion" CssClass="input-group col-md-2"></asp:Label>
                                                                <div class="input-group col-md-10">
                                                                    <asp:DropDownList ID="cmblatencion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="lbldocc" runat="server" Text="Doctor/Consulta" CssClass="input-group col-md-2"></asp:Label>
                                                                <div class="input-group col-md-10">
                                                                    <asp:DropDownList ID="cmbDoctorC" runat="server"  CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="lbltipocm" runat="server" Text="Tipo de Consulta" CssClass="input-group col-md-2"></asp:Label>
                                                                <div class="input-group col-md-10">
                                                                    <asp:DropDownList ID="cmbTipoCM" runat="server"  CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="lblerror" runat="server" CssClass="btn btn-block btn-warning"></asp:Label>
                                                                <div class="input-group col-md-10">
                                                                </div>
                                                            </div>
            
                                                        



                                                            
                                                                    

                                                                    


                                                                
                                                                    
                                                                    


                                                                    
                                                                    


                        
                                                                    
                                                                   
                                                                   
                                                        <asp:Label ID="lbCA" runat="server" Width="24px" Font-Bold="True" Font-Names="Castellar" Visible="False">0</asp:Label>
                                                        <asp:Label ID="lblexistencias" runat="server" Width="24px" Font-Bold="True" Font-Names="Castellar" Visible="False">0</asp:Label>
                                                        <asp:Label ID="lblclieagendado" runat="server" Width="24px" Font-Bold="True" Font-Names="Castellar" Visible="False">0</asp:Label>
                                                        <hr />
                                                        </div>
                                                    </div>


                                                    <div class="row" id="datos" runat="server">
                                                        <div class="col-md-12">
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label15" runat="server" CssClass="col-md-3 control-label" Text="Apellido Paterno" ></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-font fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtPaterno"  runat="server" CssClass="form-control" ></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label16" runat="server" CssClass="col-md-3 control-label" Text="Apellido Materno" ></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-font fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtmaterno" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label17" runat="server" Text="Nombre(s)" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-font fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtnombre" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label25" runat="server" Text="Edad" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-hashtag fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtedad" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label18" runat="server" CssClass="col-md-3 control-label" Text="Sexo"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbsexo" runat="server" Enabled="False" CssClass="form-control" >
                                                                        <asp:ListItem Value="0">--Seleccione--</asp:ListItem>
                                                                        <asp:ListItem Value="H">HOMBRE</asp:ListItem>
                                                                        <asp:ListItem Value="M">MUJER</asp:ListItem>
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="Label34" runat="server" Text="Fecha de nacimiento" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-calendar fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtfechanac" runat="server" placeholder="DD-MM-AAAA" data-mask="99/99/9999" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label27" runat="server" Text="Ocupación" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbocupacion" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>


                                                            <div class="form-group row">
                                                                <asp:Label ID="Label32" runat="server" Text="Ciudad de Origen" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-marker fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtorigen" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label33" runat="server" Text="Dirección" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-signs fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtdireccion" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                
                                                            </div>
                                                         
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label20" runat="server" CssClass="col-md-3 control-label" Text="Celular" ></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-mobile-phone fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtCelular" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label19" runat="server" CssClass="col-md-3 control-label" Text="Teléfono Fijo"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-phone fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtTelefono" runat="server" CssClass="form-control" ></asp:TextBox>
                                                                </div>
                                                            </div>  


                                                            <div class="form-group row">
                                                                <asp:Label ID="Label21" runat="server" CssClass="col-md-3 control-label" Text="Correo"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-envelope fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                
                                                            </div>
                                                            
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label22" runat="server" Text="Remitente" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbDoctores" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="Label31" runat="server" Text="Doctor" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbDoctoresE" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div> 

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label23" runat="server" Text="Cat. Costos" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbCostos" runat="server" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="Label24" runat="server" Text="Activo" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:CheckBox ID="chkActivo" runat="server" Enabled="False" />
                                                                </div>
                                                            </div> 

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label26" runat="server" Text="Fecha Incio" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:TextBox ID="txtFechaIni" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                    <span id="micalendario" runat="server" class="input-group-addon"><span class="fa fa-calendar fa-fw"></span></span>
                                                                </div>
                                                            </div> 
                                                          
                                                        </div>

                                                        
                                                        <hr />

                                                        <div class="col-lg-12">
                                                            <h3 class="text-center">DATOS DE FACTURACIÓN</h3>
                                                        </div>

                                                        <div class="col-lg-12">
                                                            <div class="form-group row">
                                                                <div class="input-group col-md-12">
                                                                    <asp:DropDownList ID="cmbDatosfac" runat="server" AutoPostBack="True" Enabled="False" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-12">
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label3" runat="server" Text="Nombre" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-font fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtRazonSocial" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label4" runat="server" Text="RFC" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-barcode fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtrfc" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label5" runat="server" Text="Calle" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-signs fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtcalle" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label6" runat="server" Text="No. ext" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-home fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtnoext" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label7" runat="server" Text="Colonia" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-pin fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtcolonia" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label8" runat="server" Text="C.P." CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-envelope-open  fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtcp" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label9" runat="server" Text="Municipio" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-marker fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtmunicipio" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label10" runat="server" Text="Localidad" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtciudad" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label11" runat="server" Text="Estado" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-o fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtestado" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                                <asp:Label ID="Label12" runat="server" Text="País" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-map-marker fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtpais" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>

                                                            <div class="form-group row">
                                                                <asp:Label ID="Label1" runat="server" Text="Forma de pago" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <asp:DropDownList ID="cmbformapago" runat="server" Enabled="False" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                                <asp:Label ID="Label2" runat="server" Text="No. de cuenta" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-3">
                                                                    <span class="input-group-addon"><span class="fa fa-hashtag fa-fw"></span></span>
                                                                    <asp:TextBox ID="txtcuenta" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <asp:Label ID="Label35" runat="server" Text="Regimen Fiscal" CssClass="col-md-3 control-label"></asp:Label>
                                                                <div class="input-group col-md-6">
                                                                    <asp:DropDownList ID="cmbregimenfiscal" runat="server" Enabled="False" CssClass="form-control"></asp:DropDownList>
                                                                </div>
                                                            </div>

                                                        </div>

                                                        <div class="col-md-12" id="botones" runat="server">
                                                            <div class="row">
                                                                <div class="col-md-6 col-sm-12">
                                                                    <input id="Button1" class="btn btn-block btn-info" onclick="miventana();" type="button" value="Ver Terapias" />
                                                                
                                                                </div>
                                                                <div class="col-md-6 col-sm-12">
                                                                    <asp:Button ID="lnkModifica" runat="server" Text="Modificar Datos" CssClass="btn btn-block btn-warning" />
                                                                </div>
                                                            </div>
                                                        </div>

                                                       <div id="migif" style="visibility: hidden">
                                                            <img src="imagenes/loader.gif" alt="" />
                                                       </div>


                                                        <div class="col-md-12" id="Gbotones" style="visibility: visible">
                                                            <div class="row">
                                                                <div class="col-md-6 col-sm-12">
                                                                    <asp:Button ID="lnkguardar" runat="server" OnClientClick="javascript:return validanombre();" Text="AGENDAR"  CssClass="btn btn-block btn-info"  />
                                                                </div>
                                                                <div class="col-md-6 col-sm-12">
                                                                    <asp:Button ID="lnkcancelar" runat="server" Text="Cancelar" Visible="False" CssClass="btn btn-block btn-warning" />
                                                                </div>
                                                            </div>
                                                        </div>



                                                         


                                                   

                 
           
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
            FilterMode="InvalidChars" InvalidChars='-, !"#$%/()=?' TargetControlID="txtrfc">
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
            <asp:Label ID="lblfila" runat="server" Text="Label" Visible="true" ForeColor="White"></asp:Label>
            <asp:Label ID="lblposicion" runat="server" Text="Label" Visible="true" ForeColor="White"></asp:Label>
            <asp:Label ID="lblhorario" runat="server" Text="Label" Visible="true" ForeColor="White"></asp:Label>
            <asp:Label ID="lblfecha" runat="server" Text="Label" Visible="true" ForeColor="White"></asp:Label>
            <asp:Label ID="lblid" runat="server" Text="Label" Visible="true" ForeColor="White"></asp:Label>
                                                    <asp:Label ID="lblturno" runat="server" Text="Label" Visible="False"></asp:Label>

                                                </div>
                                            </div>
                                            
                                            <hr />
                                            

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
    <!--ImputMask-->
    <script src="assets/plugins/bootstrap-inputmask/bootstrap-inputmask.min.js" ></script>
    <!-- end js include path -->
    <script>
        $(document).ready(function () {
            $('[data-tooltip="tooltip"]').tooltip();
        })
    </script>

</body>
</html>
