<%@ Page Language="VB" AutoEventWireup="false" CodeFile="hpacientes.aspx.vb" Inherits="hpacientes" EnableEventValidation="false" %>

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
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <!-- DataTables -->
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.css"/>


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
                                <div class="page-title">Historial de pacientes</div>
                            </div>
                            <ol class="breadcrumb page-breadcrumb pull-right">
                                <li><i class="fa fa-search"></i>&nbsp;<a class="parent-item" href="#">Historial de pacientes</a>
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
                                        <div class="card-body">

                                            <div class="row">
                                                <div class="col-md-6 col-sm-12">
                                                    <asp:LinkButton ID="LinkButton5" runat="server"  OnClientClick="cierraventana();" CssClass="btn btn-block btn-info" ><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</asp:LinkButton>
                                                </div>
                                                <div class="col-md-6 col-sm-12">
                                                </div>
                                            </div>
                                            <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True" EnableScriptLocalization="True"></asp:ScriptManager>
                                            <hr />
                                            <div id="buscacliente" runat="server">
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-12">
                                                        <div>
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
                                                        <div>
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
                                            </div>
                                            
                                            <hr />
                                            <asp:Panel ID="Panel1" runat="server" Visible="False">
                                            <div class="row">
                                                    <div class="col-md-2">
                                                        <p class="text-info">Ver datos por:</p>
                                                    </div>
                                                    <div class="col-md-10">
                                                        <asp:RadioButtonList ID="OPCIONES" runat="server" AutoPostBack="True" RepeatDirection="Horizontal" >
                                                            <asp:ListItem Value="0" Selected="True">Terapias</asp:ListItem>
                                                            <asp:ListItem Value="1">Facturas</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-12">
                                                    <p>*En ocasiones la suma de los importes y la cantidad facturada no son iguales, esto es debido a que no se agendaron, cerraron o asignaron las terparias correspondientes a la factura. Verificar con la Suc.</p>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label3" runat="server" Text="Total Importes" CssClass="text-info"></asp:Label>
                                                </div>
                                                <div class="col-col-10">
                                                    <asp:Label ID="lblImportes" runat="server" Text="$ 0" CssClass="text-dark"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label1" runat="server" Text="Total Facturado" CssClass="text-info"></asp:Label>
                                                </div>
                                                <div class="col-col-10">
                                                    <asp:Label ID="lblFacturado" runat="server" Text="$ 0" CssClass="text-dark"></asp:Label>
                                                </div>
                                            </div>

                                            <div class="row">
                                                <div class="col-md-2">
                                                    <asp:Label ID="Label2" runat="server" Text="Total Abonado" CssClass="text-info"></asp:Label>
                                                </div>
                                                <div class="col-col-10">
                                                    <asp:Label ID="lblabonos" runat="server" Text="$ 0" CssClass="text-dark"></asp:Label>
                                                </div>
                                            </div>
                                            </asp:Panel>
                                            <hr />
                                            <div class="row" id="dDatos" runat="server" visible="false">
                                                <div class="col-md-12 table-responsive-sm">
                                                    <asp:Label ID="lblnombre" runat="server" CssClass="text-capitalize text-center text-primary h3"></asp:Label>
        
                                                    <asp:DataGrid ID="gridDatos" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-striped table-hover" >
                                                        <FooterStyle BackColor="Tan" />
                                                        <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
                                                        <AlternatingItemStyle BackColor="WhiteSmoke" />
            
                                                        <Columns>
                                                            <asp:BoundColumn DataField="idcita" HeaderText="# Recibo"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="num_factura" HeaderText="FACTURA"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="serie" HeaderText="SERIE"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="elnombre" HeaderText="CLIENTE" Visible="False"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="descripcion" HeaderText="TERAPIA"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="fecha" HeaderText="FECHA"></asp:BoundColumn>
                                                            <asp:BoundColumn DataField="abono" DataFormatString="{0:C}" HeaderText="ABONOS">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundColumn>
                                                            <%--<asp:TemplateColumn HeaderText="Abono">
                                                                <ItemTemplate>
                                                                    <asp:TextBox ID="TextBox1" Font-Size="9pt" runat="server" Text='<% #Bind("abono") %>' Enabled="False" Width="59px" style="text-align: center"></asp:TextBox>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>--%>
                                                            <asp:BoundColumn DataField="importe" DataFormatString="{0:C}" HeaderText="IMPORTES">
                                                                <ItemStyle HorizontalAlign="Center" />
                                                            </asp:BoundColumn>
                                                            <asp:BoundColumn DataField="importefac" HeaderText="FACTURADO" DataFormatString="{0:C}"></asp:BoundColumn>
                                                            <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Button ID="Button1" runat="server" CommandName="verfac" CssClass="btn btn-block btn-info" Text="Ver Factura" />&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                           <%-- <asp:TemplateColumn>
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="ImageButton4" runat="server" CommandName="agregar" Height="20px" ImageUrl="~/imagenes/view_right.png" Width="20px" />
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>--%>
                                                            <asp:BoundColumn DataField="fpago" HeaderText=""></asp:BoundColumn>
                                                        </Columns>        
                                                    </asp:DataGrid>


        <table id="tabla" runat="server" visible="false">
            <tr>
                <td align="center" bgcolor="#fc0200" colspan="6">
                    <asp:Label ID="lblaccion" runat="server" Font-Bold="True" ForeColor="White" Text="Agregar Abono"
                        Width="351px"></asp:Label></td>
            </tr>
            <tr>
                <td bgcolor="#fc0200" style="width: 65px">
                    FECHA</td>
                <td bgcolor="#fc0200" style="width: 230px">
                    TERAPIA</td>
                <td bgcolor="#fc0200" style="width: 75px">
                    ABONO</td>
                <td bgcolor="#fc0200" style="width: 75px">
                    IMPORTE</td>
                <td bgcolor="#fc0200" style="width: 70px" rowspan="2"><asp:ImageButton ID="btnaceptare" runat="server" Height="25px" ImageUrl="~/imagenes/ok.png" Width="22px" ValidationGroup="validaTerapia" />
                    &nbsp;
                    <asp:ImageButton ID="btncancelare" runat="server" Height="24px" ImageUrl="~/imagenes/button_cancel.png" Width="26px" /></td>
            </tr>
            <tr>
                <td style="width: 65px; height: 26px;">
                <img id="micalendario3" runat="server" src="imagenes/Calendar_scheduleHS.png" visible="true" />
                    <asp:TextBox ID="txtfecha" runat="server" Width="70px"></asp:TextBox></td>
                <td style="width: 230px; height: 26px;">
                    <asp:Label ID="lblterapia" runat="server" Text="Label"></asp:Label></td>
                <td style="width: 75px; height: 26px;">
                    <asp:TextBox ID="txtabonos" runat="server" Width="70px"></asp:TextBox></td>
                <td style="width: 75px; height: 26px;">
                    <asp:Label ID="lblimporte" runat="server" Text="Label"></asp:Label></td>
            </tr>
        </table>
        
        </div>
        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" Enabled="True"
            Format="dd/MM/yyyy" PopupButtonID="micalendario3" TargetControlID="txtfecha">
        </ajaxToolkit:CalendarExtender>
        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender3" runat="server" Mask="99/99/9999"
            MaskType="Date" TargetControlID="txtfecha">
        </ajaxToolkit:MaskedEditExtender>
         <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server"
            TargetControlID="txtabonos" ValidChars="1234567890.">
        </ajaxToolkit:FilteredTextBoxExtender>
        <asp:Label ID="lblidabono" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="lblfechacita" runat="server" Text="Label" Visible="False"></asp:Label>
        <asp:Label ID="lblfila" runat="server" Text="Label" Visible="False"></asp:Label>
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
    
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
        var grid = document.getElementById('gridDatos');
        var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
        var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

        tbody.removeChild(firstTr); //remove tr's from table

        var newTh = document.createElement('thead'); //creates thead
        newTh.appendChild(firstTr); //puts ths in thead
        grid.insertBefore(newTh, tbody); //puts thead before tbody
        $(function () {


            $('#gridDatos').DataTable({
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
