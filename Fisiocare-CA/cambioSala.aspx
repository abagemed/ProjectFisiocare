<%@ Page Language="VB" AutoEventWireup="false" CodeFile="cambioSala.aspx.vb" Inherits="cambioSala" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<!DOCTYPE html>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
<body class="page-header-fixed sidemenu-closed-hidelogo page-content-white page-md header-white dark-color logo-dark">
    
    <form id="form1" runat="server">
         <asp:Label ID="Label7" runat="server" Text="Label" Visible="false"></asp:Label>
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
                                    <div class="page-title">Cambio de sala</div>
                                </div>
                                <ol class="breadcrumb page-breadcrumb pull-right">
                                    <li><i class="fa fa-file-text-o"></i>&nbsp;<a class="parent-item" href="#">Cambio de sala</a>
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
                                                <header>Cambio de Sala</header>
                                            
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


                                                <div>
                                                    
                                                    <h3 class="text-center"><asp:Label ID="lblNombre" runat="server" Font-Bold="True" Font-Names="Calibri" Font-Size="16px"></asp:Label></h3>

                                                    <div class="row" id="tFecha" runat="server">
                                                        <div class="col-md-3">
                                                            <asp:LinkButton ID="LinkButton1" runat="server" CssClass="btn btn-info btn-block" ValidationGroup="fecha">Cambiar Fecha</asp:LinkButton>
                                                        </div>
                                                        <div class="col-md-6">
                                                            <h3 style="margin:0px" class="text-center"><asp:Label ID="lblfecNom" runat="server" Text="Label"></asp:Label></h3>
                                                        </div>
                                                        <div class="col-md-3">
                                                            <asp:Label ID="lblCuantos" runat="server" Text="Label" Visible="False"></asp:Label>
                                                            <asp:LinkButton ID="lnkCanceladas" runat="server" Font-Bold="True" Font-Names="Calibri" ForeColor="#1A3773" Height="14" Visible="False">0</asp:LinkButton>
                                                        </div>
                                                    </div>


   




    <table id="tHorarios" runat="server" class="table table-bordered table-striped table-hover">
        <tr>
             <%--<td>
               <asp:DataGrid ID="gFijo" runat="server" AutoGenerateColumns="False" BackColor="White"
                    BorderColor="White" BorderStyle="Solid" BorderWidth="2px" CellPadding="4" DataKeyField="idHorario"
                    ForeColor="Black" GridLines="None" Style="font-weight: bold" Width="100px">
                    <FooterStyle BackColor="#CCCC99" />
                    <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="False" Font-Italic="False" Font-Overline="False"
                        Font-Strikeout="False" Font-Underline="False" ForeColor="White" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" Mode="NumericPages" />
                    <AlternatingItemStyle BackColor="#F4F4F4" BorderColor="#1A3773" BorderStyle="Solid"
                        BorderWidth="1px" HorizontalAlign="Center" />
                    <ItemStyle BackColor="White" BorderColor="#1A3773" BorderStyle="Solid" BorderWidth="1px"
                        Font-Names="Calibri" Font-Size="14px" HorizontalAlign="Center" />
                    <Columns>
                        <asp:BoundColumn DataField="horario" HeaderText="HORARIOS">
                            <HeaderStyle Font-Bold="True" Font-Italic="False" Font-Overline="False" Font-Strikeout="False"
                                Font-Underline="False" HorizontalAlign="Center" />
                            <ItemStyle BackColor="#F4F4F4" Font-Bold="True" Font-Italic="False" Font-Overline="False"
                                Font-Strikeout="False" Font-Underline="False" ForeColor="#1A3773" Width="75px" />
                        </asp:BoundColumn>
                    </Columns>
                    <HeaderStyle BackColor="#F4F4F4" Font-Bold="False" Font-Names="calibri" Font-Size="11pt"
                        ForeColor="#1A3773" HorizontalAlign="Center" />
                </asp:DataGrid></td>--%>
            <td>
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
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk7" runat="server" CommandName="lnk7" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl7" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="False">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk8" runat="server" CommandName="lnk8" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl8" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="" HeaderStyle-BackColor="Silver" HeaderStyle-Font-Bold="true" Visible="False">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lnk9" runat="server" CommandName="lnk9" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                             <asp:Label ID="lbl9" runat="server" Visible="False"></asp:Label>
                                                         </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <%--<asp:TemplateColumn HeaderText="&quot;PASANTE&quot;">
                                                             <ItemTemplate>
                                                                  <asp:LinkButton ID="lnk10" runat="server" CommandName="lnk10" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                  <asp:Label ID="lbl10" runat="server" Visible="False"></asp:Label>
                                                             </ItemTemplate>
                                                        </asp:TemplateColumn>--%>
                                                    </Columns>
                                                </asp:DataGrid>
                <asp:DataGrid ID="gridHorariosVespertino" runat="server" AutoGenerateColumns="False" DataKeyField="idHorario" CssClass="table table-bordered table-striped table-hover" >
                                                    <Columns>
                                                        <asp:BoundColumn DataField="horario" HeaderText="VESPERTINO" Visible="True">
                                                        </asp:BoundColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;MARIANA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk1" runat="server" CommandName="lnk1" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl1" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;MARIANA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk2" runat="server" CommandName="lnk2" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl2" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;MELISSA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk3" runat="server" CommandName="lnk3" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl3" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;MELISSA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk4" runat="server" CommandName="lnk4" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl4" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;ALE&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk5" runat="server" CommandName="lnk5" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl5" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;ALE&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk6" runat="server" CommandName="lnk6" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl6" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;SALA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk7" runat="server" CommandName="lnk7" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl7" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;SALA&quot;">
                                                            <ItemTemplate>
                                                                <asp:LinkButton ID="lnk8" runat="server" CommandName="lnk8" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                <asp:Label ID="lbl18" runat="server" Visible="False"></asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                       <asp:TemplateColumn HeaderText="&quot;PASANTE&quot;">
                                                        <ItemTemplate>
                                                             <asp:LinkButton ID="lnk9" runat="server" CommandName="lnk9" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                             <asp:Label ID="lbl9" runat="server" Visible="False"></asp:Label>
                                                         </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="&quot;PASANTE&quot;">
                                                             <ItemTemplate>
                                                                  <asp:LinkButton ID="lnk10" runat="server" CommandName="lnk10" ForeColor="Red">DISPONIBLE</asp:LinkButton>
                                                                  <asp:Label ID="lbl10" runat="server" Visible="False"></asp:Label>
                                                             </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                    </Columns>
                                                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <asp:Panel ID="Panel2" runat="server" BackColor="White" BorderColor="Gray" BorderStyle="Dashed"
        BorderWidth="1px">
        <asp:Calendar ID="calendario" runat="server" BorderColor="#F4F4F4" BorderStyle="Solid"
            BorderWidth="2px" Font-Names="calibri" Font-Size="12pt" Height="298px" ShowGridLines="True"
            Width="368px">
            <OtherMonthDayStyle ForeColor="Silver" VerticalAlign="Middle" />
            <DayStyle BorderColor="Silver" Font-Bold="False" />
            <DayHeaderStyle BackColor="#F4F4F4" ForeColor="#1A3773" />
            <TitleStyle BackColor="#F4F4F4" BorderColor="Red" BorderStyle="Solid" BorderWidth="1px"
                ForeColor="#1A3773" />
        </asp:Calendar>
    </asp:Panel>
    <br />
    <asp:Panel ID="Panel1" runat="server" Width="900px" Visible="False">
   <div style="background-color:#f4f4f4; padding:3; font-family:Calibri; font-size:12pt; color:#1a3773 ">Datos del movimiento</div>
        <br />
        <table border="1" cellpadding="3" cellspacing="0" style="border: solid 1px #f4f4f4; text-align:left; color:#1a3773; font-size:14px; font-family:Calibri">
            <tr>
                <td colspan="2" style="background-color:#f4f4f4; text-align:center">
                    <asp:Label ID="Label4" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="CITA ANTERIOR" Width="126px"></asp:Label></td>
                <td colspan="2" style="background-color:#f4f4f4; text-align:center">
                    <asp:Label ID="Label6" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="NUEVA CITA" Width="126px"></asp:Label></td>
            </tr>
            <tr>
                <td style="width: 63px; background-color:#f4f4f4">
                    <asp:Label ID="Label5" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Fecha" Width="60px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:TextBox ID="lafechaAnte" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                        Width="100px" ReadOnly="True"></asp:TextBox></td>
                <td style="width: 69px; background-color:#f4f4f4">
                    <asp:Label ID="Label2" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Fecha" Width="60px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:TextBox ID="lafecha" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                        BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid" Width="100px" ReadOnly="True"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 63px; background-color:#f4f4f4">
                    <asp:Label ID="Label1" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Horario" Width="60px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:TextBox ID="lahoraAnte" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid"
                        Width="100px" ReadOnly="True"></asp:TextBox></td>
                <td style="width: 69px; background-color:#f4f4f4">
                    <asp:Label ID="Label3" runat="server" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Horario" Width="60px"></asp:Label></td>
                <td style="width: 100px">
                    <asp:TextBox ID="lahora" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                        BorderWidth="1px" MaxLength="50" Style="border-bottom: #e0e0e0 2px solid" Width="100px" ReadOnly="True"></asp:TextBox></td>
            </tr>
        </table>
        <br />
        <asp:Label ID="lblerror" runat="server" Width="367px"></asp:Label><br />
        <br />
         <div style="text-align:right; background-color:red; padding:3px">
        <asp:Button ID="Button1" runat="server" Text="Seleccionar Otro Horario" CssClass="boton" Font-Bold="False" Font-Names="Calibri" Font-Size="12pt" ForeColor="Red" Width="175px" />
             &nbsp; &nbsp;
         <asp:Button ID="lnkGrabar" runat="server" Width="175px" ForeColor="Red" Font-Size="12pt" Text="Guardar Cambios" Font-Bold="False" Font-Names="Calibri" CssClass="boton"/>&nbsp;
    </div>
   </asp:Panel>
    <br />
    <asp:Label ID="lblduracion" runat="server" visible="false"></asp:Label>
    <asp:Label ID="lblposicion" runat="server" visible="false"></asp:Label>
    <asp:Label ID="lblidhorario" runat="server" visible="false"></asp:Label>
    <asp:Label ID="lblidcita" runat="server" Visible="False" Width="164px"></asp:Label>
    <asp:Label ID="lblfecha" runat="server" Visible="False" Width="231px"></asp:Label>
    <asp:Label ID="lblIdcliente" runat="server" visible="false"></asp:Label>
    <asp:Label ID="lblFila" runat="server" Visible="False"></asp:Label><br />
                                                     <asp:Label ID="lblturno" runat="server" visible="false"></asp:Label>
    <ajaxToolkit:messagebox id="Messagebox1" runat="server"></ajaxToolkit:messagebox>
    <input id="hfModal" runat="server" type="hidden" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnableScriptGlobalization="True"
        EnableScriptLocalization="True">
    </asp:ScriptManager>
    <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server" BackgroundCssClass="FondoAplicacion"
        PopupControlID="Panel2" TargetControlID="hfModal">
    </ajaxToolkit:ModalPopupExtender>
    &nbsp;</div>
                                            

                                            
     

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
</body>
</html>
