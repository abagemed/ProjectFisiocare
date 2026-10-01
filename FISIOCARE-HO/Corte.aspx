<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Corte.aspx.vb" Inherits="Corte" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="ajaxToolkit" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
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
    <script type="text/javascript" language="javascript">
     function txtVisible(){   
        var cmb = document.getElementById("cmbExporta").value;
        if (cmb == "1"){
          document.getElementById("txtEmail").style.display = "inline";
        }
        else{
          document.getElementById("txtEmail").style.display = "none";
       }
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
</head>
<body class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
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
                                <div class="page-title">Corte de caja</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-dollar"></i>&nbsp;<a class="parent-item" href="#">Corte de caja</a>
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
                                            <header>Corte de Caja</header>
                                            
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
                                                <div class="col-md-8 text-bold">
                                                    <asp:Label ID="Label1" runat="server" Text="Corte de caja del"></asp:Label>
                                                    <asp:Label ID="lblfecha1" runat="server" Text="Label" CssClass="txt-info"></asp:Label>&nbsp; <asp:Label ID="Label2" runat="server" Text="al"></asp:Label>&nbsp; 
                                                    <asp:Label ID="lblfecha2" runat="server" Text="Label" CssClass="txt-info"></asp:Label>
                                                </div>
                                                <div class="col-md-4">
                                                    <asp:LinkButton ID="btnFechas" runat="server" ValidationGroup="validacion" CssClass="btn btn-success btn-block"><i class="fa fa-calendar"></i> Modificar Fechas</asp:LinkButton>
                                                </div>
                                            </div>
                                            <br />
                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="lblturno" runat="server" Text="TURNO:"></asp:Label>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:DropDownList ID="cboturno" runat="server" CssClass="form-control" AutoPostBack="True">
                                                        <asp:ListItem Value="0">General</asp:ListItem>
                                                        <asp:ListItem Value="M">Matutino</asp:ListItem>
                                                        <asp:ListItem Value="V">Vespertino</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-md-2">
                                                    <asp:Label ID="lbBuscaPaciente" runat="server" Text="TERAPISTA:"></asp:Label>
                                                </div>
                                                <div class="col-md-3">
                                                   
                                                     <asp:DropDownList ID="cboterapistas" runat="server" CssClass="form-control" AutoPostBack="True"></asp:DropDownList>
                                                </div>
                                            
                                            </div>
                                            <hr />
                                            <div class="row">
                                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                            <div class="text-center">
                                            <div id="total_efectivo" runat="server" visible="false"><h4 class="modal-title" id="">Total en Efectivo: $<asp:Label ID="tefectivo" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label></h4></div>
                                            <div id="total_tarjeta_debito" runat="server" visible="false"><h4 class="modal-title" id="">Total en Tarjeta Debito: $<asp:Label ID="tdebito" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label></h4></div>
                                            <div id="total_tarjeta_credito" runat="server" visible="false"><h4 class="modal-title" id="">Total en Tarjeta Credito: $<asp:Label ID="tcredito" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label></h4></div>
                                            <div id="total_transferencia" runat="server" visible="false"><h4 class="modal-title" id="">Total en Transferencia: $<asp:Label ID="ttrasnferencia" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label></h4></div>
                                            <div id="total_general" runat="server" visible="false"><h4 class="modal-title" id="">Total: $<asp:Label ID="ttotal" runat="server" Text="" ForeColor="Black" Font-Bold="true"></asp:Label></h4></div>
                                            </div> 
                                            </div>
                                                 </div>
                                            <div class="table-responsive">
                                                 <asp:DataGrid ID="DataGrid1" runat="server" CssClass="table table-bordered table-striped table-hover" >
                                                 </asp:DataGrid>
                                                 <ajaxToolkit:messagebox id="Messagebox1" runat="server"></ajaxToolkit:messagebox>
                                             </div>
                                            <asp:TextBox ID="txtfecha2" runat="server" Width="85px" Visible="False"></asp:TextBox>
                                            <asp:TextBox ID="txtFecha" runat="server" Width="85px" Visible="False"></asp:TextBox>
                                            <asp:Panel ID="Panel1" runat="server" Style="text-align: center">

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
                                                                     BorderWidth="10px" Font-Names="calibri" Font-Size="12pt" Height="252px" ShowGridLines="True">
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
                                                                         BorderWidth="10px" Font-Names="calibri" Font-Size="12pt" Height="252px" ShowGridLines="True">
                                                                         <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
                                                                         <DayStyle BorderColor="Silver" Font-Bold="False" />
                                                                         <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
                                                                         <TitleStyle BackColor="#F4F4F4" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px"
                                                                             ForeColor="#1A3773" />
                                                                     </asp:Calendar>
                                                                </div>
                                                            </div>
                                                            <div class="form-group row">
                                                                <div class="col-md-12">
                                                                    <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>

                                                
                                             </asp:Panel>
                                            <input id="hfpx" type="hidden" runat="server" /><br />
                                            <asp:Label ID="lblaviso" runat="server" Font-Names="Calibri" Font-Size="16px" ForeColor="Red" Text="No se encontraron abonos" Visible="False" Width="613px"></asp:Label>
                                            <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label>
                                            <cc1:maskededitextender id="MaskedEditExtender1" runat="server" mask="99/99/9999" masktype="Date" targetcontrolid="txtFecha"></cc1:maskededitextender>
                                            <cc1:maskededitextender id="Maskededitextender2" runat="server" mask="99/99/9999" masktype="Date" targetcontrolid="txtFecha2"></cc1:maskededitextender>
                                            <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" PopupControlID="Panel1" TargetControlID="btnFechas" BackgroundCssClass="FondoAplicacion"></cc1:ModalPopupExtender>
                                            <cr:crystalreportsource id="origen_reporte" runat="server"></cr:crystalreportsource>
                                            <cr:crystalreportviewer id="visor_reporte" runat="server" autodatabind="true"></cr:crystalreportviewer>
                                            <div style="font-family:Calibri; font-size:12px; text-align:left">&nbsp;</div>
     
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
    <!-- DataTables -->
    <script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/pdfmake.min.js"></script>
	<script type="text/javascript" src="https://cdnjs.cloudflare.com/ajax/libs/pdfmake/0.1.32/vfs_fonts.js"></script>
	<script type="text/javascript" src="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.js"></script>
    <script>
        //CREAR CABECERA
        var grid = document.getElementById('DataGrid1');
        var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
        var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

        tbody.removeChild(firstTr); //remove tr's from table

        var newTh = document.createElement('thead'); //creates thead
        newTh.appendChild(firstTr); //puts ths in thead
        grid.insertBefore(newTh, tbody); //puts thead before tbody
        $(function () {


            $('#DataGrid1').DataTable({
                "language": { url: "//cdn.datatables.net/plug-ins/1.10.19/i18n/Spanish.json" },
                "ordering": false,
                "paging": false,
                "searching": true,
                "info": false,
                "fixedHeader": true,
                "responsive": true,
                "order": [[1, "asc"]],
                dom: 'Bfrtip',
                buttons: [
					{
					    extend: 'excel',
					    text: 'Excel <i class="fa fa-file-excel-o"></i>',
					    messageTop: ' Total Efectivo:' + $('#<%=tefectivo.ClientID%>').text() + '  Total Tarjeta Debito: ' + $('#<%=tdebito.ClientID%>').text() + '   Total Tarjeta Credito: ' + $('#<%=tcredito.ClientID%>').text() + '   Total Transferencia: ' + $('#<%=ttrasnferencia.ClientID%>').text() + '    Total General: ' + $('#<%=ttotal.ClientID%>').text()
                     },
                    {
                        extend: 'pdf',
                        text: 'PDF <i class="fa fa-file-pdf-o"></i>',
                        messageTop: ' Total Efectivo:' + $('#<%=tefectivo.ClientID%>').text() + '  Total Tarjeta Debito: ' + $('#<%=tdebito.ClientID%>').text() + '  Total Tarjeta Credito: ' + $('#<%=tcredito.ClientID%>').text() + '   Total Transferencia: ' + $('#<%=ttrasnferencia.ClientID%>').text() + '  Total General: ' + $('#<%=ttotal.ClientID%>').text()
                    },
                    {
                        extend: 'print',
                        text: 'Imprimir <i class="fa fa-print"></i>',
                        messageTop: 'Corte de Caja </br> Total Efectivo:' + $('#<%=tefectivo.ClientID%>').text() + '</br> Total Tarjeta Debito: ' + $('#<%=tdebito.ClientID%>').text() + '</br> Total Tarjeta Credito: ' + $('#<%=tcredito.ClientID%>').text() + '</br> Total Transferencia: ' + $('#<%=ttrasnferencia.ClientID%>').text() + '</br> Total General: ' + $('#<%=ttotal.ClientID%>').text()
                    },
                ]
            });
        });
    </script>
</body>
</html>
tefectivo
ttotal
tdebito