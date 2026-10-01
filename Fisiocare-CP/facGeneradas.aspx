<%@ Page Language="VB" AutoEventWireup="false" CodeFile="facGeneradas.aspx.vb" Inherits="facGeneradas" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

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
<body class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
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
                                        <a href="#">
                                            <i class="fa fa-sign-out"></i> Salir </a>
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
                                    <div class="page-title">Bloqueo de horarios</div>
                                </div>
                                <ol class="breadcrumb page-breadcrumb pull-right">
                                    <li><i class="fa fa-lock"></i>&nbsp;<a class="parent-item" href="#">Bloqueo de horarios</a>
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
                                                <header>Bloqueo de horarios</header>
                                                <div class="tools">
			                                        <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                                </div>
                                            </div>
                                            <div class="card-body">
                                                <div class="row">
                                                    <div class="col-md-6 col-sm-12">
                                                        <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-block btn-info"><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</asp:LinkButton>
                                                    </div>
                                                    <div class="col-md-6 col-sm-12">
                                                        <a href="https://facturacioncp.agemed.com.mx" target="_blank" class="btn btn-block btn-warning" >Facturación</a>
                                                    </div>
                                                </div>
                                                <br />
                                                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="cmbMesFac" runat="server" CssClass="form-control">
                                                             <asp:ListItem Value="0">TODOS LOS MESES</asp:ListItem>
                                                             <asp:ListItem Value="1">ENERO</asp:ListItem>
                                                             <asp:ListItem Value="2">FEBRERO</asp:ListItem>
                                                             <asp:ListItem Value="3">MARZO</asp:ListItem>
                                                             <asp:ListItem Value="4">ABRIL</asp:ListItem>
                                                             <asp:ListItem Value="5">MAYO</asp:ListItem>
                                                             <asp:ListItem Value="6">JUNIO</asp:ListItem>
                                                             <asp:ListItem Value="7">JULIO</asp:ListItem>
                                                             <asp:ListItem Value="8">AGOSTO</asp:ListItem>
                                                             <asp:ListItem Value="9">SEPTIEMBRE</asp:ListItem>
                                                             <asp:ListItem Value="10">OCTUBRE</asp:ListItem>
                                                             <asp:ListItem Value="11">NOVIEMBRE</asp:ListItem>
                                                             <asp:ListItem Value="12">DICIEMBRE</asp:ListItem>
                                                         </asp:DropDownList>
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="cmblosaños" runat="server" CssClass="form-control">
                                                            <asp:ListItem>2009</asp:ListItem>
                                                            <asp:ListItem>2010</asp:ListItem>
                                                            <asp:ListItem>2011</asp:ListItem>
                                                            <asp:ListItem>2012</asp:ListItem>
                                                            <asp:ListItem>2013</asp:ListItem>
                                                            <asp:ListItem>2014</asp:ListItem>
                                                            <asp:ListItem>2015</asp:ListItem>
                                                            <asp:ListItem>2016</asp:ListItem>
                                                            <asp:ListItem>2017</asp:ListItem>
                                                            <%--<asp:ListItem>2018</asp:ListItem>
                                                            <asp:ListItem>2019</asp:ListItem>--%>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                                <div class="form-group row">
                                                    <div class="col-md-4">
                                                        <asp:DropDownList ID="cmbfacgeneradas" runat="server" CssClass="form-control">
                                                             <asp:ListItem Value="0">Facturas Generadas</asp:ListItem>
                                                             <asp:ListItem Value="1">Facturas </asp:ListItem>
                                                             <asp:ListItem Value="2">Facturas Canceladas</asp:ListItem>
                                                         </asp:DropDownList>

                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnfacturar" runat="server" CssClass="btn btn-block btn-primary" Text="Buscar" />
                                                    </div>
                                                    <div class="col-md-4">
                                                        <asp:Button ID="btnBfac" runat="server" CssClass="btn btn-block btn-warning" Text="Buscar No. Factura" />
                                                    </div>
                                                </div>
         
        
                                                 <div class="table-responsive">
                                                     <asp:DataGrid ID="gFacgeneradas" runat="server"  CssClass="table table-bordered table-striped table-hover">
                                                     <Columns>
                                                         <asp:BoundColumn HeaderText="Factura" DataField="num_factura">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                             <ItemStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                                         <asp:BoundColumn HeaderText="Fecha" DataField="fecha">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                             <ItemStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                                         <asp:BoundColumn HeaderText="Razon Social" DataField="nombre">
                                                             <HeaderStyle HorizontalAlign="Left" />
                                                             <ItemStyle HorizontalAlign="Left" />
                                                         </asp:BoundColumn>
                                                         <asp:BoundColumn HeaderText="Status" DataField="cancelada">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                             <ItemStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                                         <asp:TemplateColumn><ItemStyle Width="70px" HorizontalAlign="Center" />
                                                             <ItemTemplate>
                                                                 <asp:Button ID="Button1" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" CommandName="cancelar" />
                                                             </ItemTemplate>
                                                         </asp:TemplateColumn>
                                                         <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                                                             <ItemTemplate>
                                                                 <asp:ImageButton ID="ImageButton1" runat="server" CommandName="imprimir" Height="20px"
                                                                     ImageUrl="~/imagenes/agt_print.png" Width="22px" />
                                                             </ItemTemplate>
                                                         </asp:TemplateColumn>
                                                         <asp:TemplateColumn><ItemStyle HorizontalAlign="Center" />
                                                             <ItemTemplate>
                                                                 <asp:ImageButton ID="ImageButton2" CommandName="mail" runat="server" Height="30px" ImageUrl="~/imagenes/correos.jpg" Width="33px" />
                                                             </ItemTemplate>
                                                         </asp:TemplateColumn>
                                                         <asp:BoundColumn HeaderText="vf" DataField="validaf" Visible="false">
                                                             <HeaderStyle HorizontalAlign="Center" />
                                                             <ItemStyle HorizontalAlign="Center" />
                                                         </asp:BoundColumn>
                                                     </Columns>
                                                 </asp:DataGrid>
                                                </div>



                                                 <asp:Panel ID="Panel1" runat="server">
                                                    <div class="modal-dialog modal-lg" role="document">
                                                         <div class="modal-content">
                                                             <div class="modal-header">
                                                                 <h4 class="modal-title" id="exampleModalLabel">NÚMERO DE FACTURA</h4>
                                                             </div>
                                                             <div class="modal-body">
                                                                 <div class="form-group row">
                                                                     <div class="col-md-6">
                                                                         <asp:TextBox ID="txtnumfactura" runat="server" CssClass="form-control"></asp:TextBox>
                                                                     </div>
                                                                     <div class="col-md-6">
                                                                         <asp:TextBox ID="TextBox2" runat="server" CssClass="form-control">C</asp:TextBox>
                                                                     </div>
                                                                 </div>

                                                             </div>
                                                             <div class="modal-footer">
                                                                 <asp:Button ID="Button3" runat="server" CssClass="btn btn-primary" Text="Aceptar" />
                                                                 <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="Cancelar" />
                                                             </div>
                                                        </div>
                                                    </div>
                                                 </asp:Panel>
                                                <cc1:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion" PopupControlID="Panel1" TargetControlID="btnBfac"></cc1:ModalPopupExtender>
         
                                                
                                                <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="160px" Width="350px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 300px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                         e-m@il&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtmail" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="285px"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button4" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar" />
                         &nbsp;
                         <asp:Button ID="Button5" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" /></td>
                 </tr>
             </table>


             <input id="hfidfac" runat="server" type="hidden" /></asp:Panel>
         <cc1:ModalPopupExtender ID="ModalPopupExtender2" runat="server" BackgroundCssClass="FondoAplicacion"
             PopupControlID="Panel2" TargetControlID="hfmodalmail">
         </cc1:ModalPopupExtender>
         <input id="hfmodalmail" runat="server" type="hidden" /><br />
         <asp:Panel ID="Panel3" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="240px" Width="370px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 350px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                         NÚMERO FACTURA A CANCELAR&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtnumcancelar" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="163px" Enabled="False"></asp:TextBox>
                         <asp:TextBox ID="TextBox3" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Enabled="False" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="43px">C</asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style="background-color: #f4f4f4; color:#1A3773; font-size:16pt">
                         SUSTITUIDO POR
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtsustituido"
                             ErrorMessage="*" ValidationGroup="cancelar"></asp:RequiredFieldValidator>
                         <asp:TextBox ID="txtsustituido" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px"></asp:TextBox></td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button6" runat="server" CssClass="boton" ForeColor="Red" Text="Aceptar" ValidationGroup="cancelar" />
                         &nbsp;
                         <asp:Button ID="Button7" runat="server" CssClass="boton" ForeColor="Red" Text="Cancelar" /></td>
                 </tr>
             </table>
         </asp:Panel>
         <cc1:ModalPopupExtender ID="ModalPopupExtender3" runat="server" BackgroundCssClass="FondoAplicacion"
             PopupControlID="Panel3" TargetControlID="hfModalcancelar">
         </cc1:ModalPopupExtender>
         <cc2:messagebox id="Messagebox1" runat="server"></cc2:messagebox>
         <input id="hfModalcancelar" runat="server" type="hidden" />
         <asp:Label ID="lblfecha" runat="server" Visible="False"></asp:Label>
                                            
                                           

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
        var grid = document.getElementById('gFacgeneradas');
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
                "searching": false,
                "info": false,
                "fixedHeader": true,
                "responsive": true,
                "order": [[1, "asc"]],
            });
        });
    </script>
</body>
</html>
