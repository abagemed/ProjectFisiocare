<%@ Page Language="VB" AutoEventWireup="false" CodeFile="pagos.aspx.vb" Inherits="pagos" %>

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
    <!-- DataTables -->
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.css"/>

</head>
<body onload="javascript:if(history.length>0)history.go(+1)" class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
    <form id="form1" runat="server">  
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
                                    <div class="page-title">Recibos</div>
                                </div>
                                <ol class="breadcrumb page-breadcrumb pull-right">
                                    <li><i class="fa fa-file-text-o"></i>&nbsp;<a class="parent-item" href="#">Recibos</a>
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
                                                <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"></asp:ScriptManager>
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-12">
                                                        <asp:LinkButton ID="LinkButton5" runat="server"  OnClientClick="cierraventana();" CssClass="btn btn-block btn-info" ><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-6 col-sm-12">
                                                    </div>
                                                </div>
                                                <br />
                                            
                                                <div class="row">
                                                    <div class="col-sm-3 text-bold">
                                                        <asp:Label ID="Label1" runat="server" Text="Período del Reporte"></asp:Label>
                                                        <asp:Label ID="lblfecha1" runat="server" Text="Label" CssClass="txt-info small"></asp:Label>&nbsp; <asp:Label ID="Label2" runat="server" Text="al"></asp:Label>&nbsp; 
                                                        <asp:Label ID="lblfecha2" runat="server" Text="Label" CssClass="txt-info small"></asp:Label>
                                                    </div>
                                                    <div class="col-sm-3">
                                                        <asp:LinkButton ID="lnkconsulta" runat="server" ValidationGroup="validacion" CssClass="btn btn-success btn-block"><i class="fa fa-calendar"></i> Modificar Fechas</asp:LinkButton>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-12 text-bold">
                                                        <asp:TextBox ID="txtfecha3" runat="server" Width="95px" Visible="False"></asp:TextBox>
                                                        <asp:TextBox ID="txtfecha4" runat="server" Width="95px" Visible="False"></asp:TextBox>
                                                        <asp:Label ID="lblaviso" runat="server" Text="NO SE ENCONTRARON REGISTROS" Visible="False" CssClass="txt-danger"></asp:Label>
                                            
                                                    </div>
                                                </div>

                                                <hr />
                                                
                                                

                                                <div class="form-group row">
                                                <label class="col-sm-3 control-label">Tipo de Consulta</label>
                                                <div class="col-sm-3">
                                                     <asp:DropDownList ID="cmbprincipal" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbprincipal_SelectedIndexChanged" CssClass="form-control">
                                                                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                                                                    <asp:ListItem Value="1">SIN FACTURAR</asp:ListItem>
                                                                    <asp:ListItem Value="2">FACTURADO</asp:ListItem>
                                                                    <asp:ListItem Value="3">RECIBO</asp:ListItem>
                                                                    <asp:ListItem Value="4">PACIENTE</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                            </div>

                                               <div id="contenedor_Facturacion" class="fortalecimiento_contenedor" style="display:none">
                                                      <div class="row" id="elfiltro" runat="server">
                                                    <div class="col-md-12">
                                                        <div class="form-group row">
                                                            <asp:Label ID="Label13" runat="server" CssClass="col-md-3 control-label" Text="General" Width="80px"></asp:Label>
                                                            <div class="input-group col-md-3">
                                                                <asp:DropDownList ID="cmbfiltro" runat="server" AutoPostBack="True" OnSelectedIndexChanged="cmbfiltro_SelectedIndexChanged" CssClass="form-control">
                                                                    <asp:ListItem Value="0">TODOS</asp:ListItem>
                                                                    <asp:ListItem Value="1">SIN FACTURAR</asp:ListItem>
                                                                    <asp:ListItem Value="2">FACTURADO</asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                </div>

                                                <div id="contenedor_Recibo" runat="server" class="fortalecimiento_contenedor" visible="false">
                                                      <div class="form-group row">
                                                        <label class="col-sm-3 control-label">Recibo</label>
                                                        <div class="col-sm-3">
                                                        <%--<input type="text" class="form-control" id="brecibo" runat="server" placeholder="Escribe el numero de recibo!" />--%>
                                                            <asp:TextBox ID="brecibo" runat="server" CssClass="form-control"></asp:TextBox>
                                                        </div>
                                                        <div class="col-sm-3">
                                                        <asp:Button ID="btBuscar" runat="server" CssClass="btn btn-primary" Text="Buscar" />
                                                        </div>
                                                     </div>
                                                </div>
                                                <div id="contenedor_Paciente" runat="server" class="fortalecimiento_contenedor"  visible="false">
                                                      <div class="form-group row">
                                                        <label class="col-sm-3 control-label">Paciente</label>
                                                        <div class="col-sm-3">
                                                        <asp:DropDownList ID="cmbpacientes"  runat="server"  AutoPostBack="True" OnSelectedIndexChanged="cmbpacientes_SelectedIndexChanged" CssClass="form-control">
                                                                    
                                                        </asp:DropDownList>
                                                        </div>
                                                     </div>
                                                </div>

                                            
          
        <div class="row margin-top-20 table-responsive">
                
            <asp:DataGrid ID="gridTerminadas" runat="server" DataKeyField="idabono" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" >
                <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                <AlternatingItemStyle BackColor="WhiteSmoke" />
                <Columns>
                     <asp:BoundColumn DataField="idcita" HeaderText="Recibo"></asp:BoundColumn>
                    <asp:BoundColumn DataField="fecha" HeaderText="Fecha"></asp:BoundColumn>
                    <asp:BoundColumn DataField="elnombre" HeaderText="Cliente" ReadOnly="True">
                        <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                        <ItemStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                            Font-Underline="False" HorizontalAlign="Left" />
                    </asp:BoundColumn>
                    <asp:TemplateColumn HeaderText="Terapia">
                        <EditItemTemplate>
                
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" Text='<% #Bind("descripcion")%>' Enabled="False" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Abono">
                        <EditItemTemplate>
                            &nbsp;
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" Text='<% #Bind("abono") %>' Enabled="False" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Importe">
                        <EditItemTemplate>
                            &nbsp;
                        </EditItemTemplate>
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<% #Bind("importe") %>' Enabled="False" CssClass="form-control"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="facturado" HeaderText="Facturado"></asp:BoundColumn>
                    <asp:BoundColumn DataField="num_factura" HeaderText="Factura"></asp:BoundColumn>
                    <asp:BoundColumn DataField="idcosto" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" ForeColor="Red" CommandName="cambiar" style="font-size: 8pt">CAMBIAR</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton3" runat="server" CommandName="imprimir" Height="20px"
                                ImageUrl="~/imagenes/agt_print.png" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton4" runat="server" CommandName="agregar" Height="20px"
                                ImageUrl="~/imagenes/view_right.png" Width="20px" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="idCliente" Visible="False"></asp:BoundColumn>
                    <asp:TemplateColumn Visible="False">
                        <ItemTemplate>
                            <asp:ImageButton ID="ImageButton5" runat="server" CommandName="borrar" ImageUrl="~/imagenes/page_delete.png" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="idCita" Visible="False"></asp:BoundColumn>
                </Columns>
            </asp:DataGrid>
            <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
                MaskType="Date" TargetControlID="txtfecha3">
            </ajaxToolkit:MaskedEditExtender>
            
            <asp:Panel ID="Panel1" runat="server" style="text-align:center">
                <div class="modal-dialog modal-lg" role="document">
					<div class="modal-content">
					    <div class="modal-header">
					        <h4 class="modal-title" id="exampleModalLabel">Rango de fechas</h4>
					    </div>
					    <div class="modal-body">
                            <div class="form-group row">
                                <div class="col-md-6">
                                    <h3>INICIO</h3>
                                    <asp:Calendar ID="calendario" runat="server" BorderColor="#F4F4F4" BorderStyle="Solid"
                                        BorderWidth="10px" Font-Names="calibri" Font-Size="12pt" Height="200px" ShowGridLines="True">
                                        <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
                                        <DayStyle BorderColor="Silver" Font-Bold="False" />
                                        <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                                        <TitleStyle BackColor="#F4F4F4" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px"
                                            ForeColor="#1A3773" />
                                    </asp:Calendar>

                                </div>
                                <div class="col-md-6">
                                    <h3>FIN</h3>
                                    <asp:Calendar ID="calendario2" runat="server" BorderColor="#F4F4F4" BorderStyle="Solid"
                                        BorderWidth="10px" Font-Names="calibri" Font-Size="12pt" Height="200px" ShowGridLines="True">
                                        <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
                                        <DayStyle BorderColor="Silver" Font-Bold="False" />
                                        <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                                        <TitleStyle BackColor="#F4F4F4" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px"
                                            ForeColor="#1A3773" />
                                    </asp:Calendar>
                                </div>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-md-12">
                                <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                            </div>
                        </div>
                    </div>
                </div>
               
            </asp:Panel>
                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" Mask="99/99/9999"
                        MaskType="Date" TargetControlID="txtfecha4">
                    </ajaxToolkit:MaskedEditExtender>
            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion" PopupControlID="Panel1" TargetControlID="lnkconsulta">
            </ajaxToolkit:ModalPopupExtender>
            <asp:Label ID="lblidabono" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblfechacita" runat="server" Visible="False"></asp:Label>
            <asp:Label ID="lblkeAcer" runat="server" Visible="False"></asp:Label><cc1:messagebox id="Messagebox1"
                runat="server"></cc1:messagebox>
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
        var grid = document.getElementById('gridTerminadas');
        var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
        var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

        tbody.removeChild(firstTr); //remove tr's from table

        var newTh = document.createElement('thead'); //creates thead
        newTh.appendChild(firstTr); //puts ths in thead
        grid.insertBefore(newTh, tbody); //puts thead before tbody
        $(function () {


            $('#gridTerminadas').DataTable({
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
