<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Facturacion.aspx.vb" Inherits="AgeMED.Facturacion" EnableEventValidation="false" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<%@ Import Namespace="System.Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
        window.onload = function () {
            $('input[type="checkbox"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });            
        };
       
       

       
        

        function ValSoloNumeros(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValSoloLetras(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z ñÑ1-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z ñÑ1-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function Hack() {
            if (window.location.pathname != "/FacturacionClientes.aspx") {
                $('input[type="checkbox"]').addClass("flat-red");
                //Flat red color scheme for iCheck
                $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                    checkboxClass: 'icheckbox_flat-green',
                    radioClass: 'iradio_flat-green'
                });
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }
            if ($('.tabla_citas_programadas').length > 0) {
                $('html, body').animate({
                    scrollTop: $("#box_citas_programadas").offset().top
                }, 2000);
            }
            else if ($('.tabla_citas_pasadas').length > 0) {
                $('html, body').animate({
                    scrollTop: $("#box_citas_pasadas").offset().top
                }, 2000);
            }
        }
       
    </script>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    <style>
        .Fechas, .input-group-addon{
        cursor:pointer;
    }
    </style>
     <script>
         window.onload = function () {
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=Fechas.ClientID%>').trigger('focus');
             });

             $('.daterange').daterangepicker({
                 "locale": {
                     "format": "DD/MM/YYYY",
                     "separator": " - ",
                     "applyLabel": "Aceptar",
                     "cancelLabel": "Cancelar",
                     "fromLabel": "Desde",
                     "toLabel": "Hasta",
                     "customRangeLabel": "Custom",
                     "daysOfWeek": [
                         "Do",
                         "Lu",
                         "Ma",
                         "Mi",
                         "Ju",
                         "Vi",
                         "Sa"
                     ],
                     "monthNames": [
                         "Enero",
                         "Febrero",
                         "Marzo",
                         "Abril",
                         "Mayo",
                         "Junio",
                         "Julio",
                         "Augosto",
                         "Septiempre",
                         "Octubre",
                         "Noviembre",
                         "Diciembre"
                     ],
                     "firstDay": 1,
                 }
             });
             var grid = document.getElementById('<%= dtgCargosConsultas.ClientID%>');
            var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
            var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

            tbody.removeChild(firstTr); //remove tr's from table

            var newTh = document.createElement('thead'); //creates thead
            newTh.appendChild(firstTr); //puts ths in thead
            grid.insertBefore(newTh, tbody); //puts thead behore tbody
            $(".ListaFacturas").DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
            })

         }
        function Hack() {
            $('.input-group').find('.fa-calendar').on('click', function () {
                $(this).parent().siblings('#<%=Fechas.ClientID%>').trigger('focus');
            });
            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            };

            $('.daterange').daterangepicker({
                "locale": {
                    "format": "DD/MM/YYYY",
                    "separator": " - ",
                    "applyLabel": "Aceptar",
                    "cancelLabel": "Cancelar",
                    "fromLabel": "Desde",
                    "toLabel": "Hasta",
                    "customRangeLabel": "Custom",
                    "daysOfWeek": [
                        "Do",
                        "Lu",
                        "Ma",
                        "Mi",
                        "Ju",
                        "Vi",
                        "Sa"
                    ],
                    "monthNames": [
                        "Enero",
                        "Febrero",
                        "Marzo",
                        "Abril",
                        "Mayo",
                        "Junio",
                        "Julio",
                        "Augosto",
                        "Septiempre",
                        "Octubre",
                        "Noviembre",
                        "Diciembre"
                    ],
                    "firstDay": 1,
                }
            });
            var grid = document.getElementById('<%= dtgCargosConsultas.ClientID%>');
            var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
            var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

            tbody.removeChild(firstTr); //remove tr's from table

            var newTh = document.createElement('thead'); //creates thead
            newTh.appendChild(firstTr); //puts ths in thead
            grid.insertBefore(newTh, tbody); //puts thead behore tbody
            $(".ListaFacturas").DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
            })
        }
</script> 


    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Facturación clientes
            <small>Buscar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="FacturacionClientes.aspx"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-address-book-o"></i>Facturación Cliente</li>
        </ol>
    </section>
    <section class="content">
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelCita">
            <ProgressTemplate>
                <%--PRELOADER--%>
                <div class="preloader">
                    <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info" id="box_BusquedaPaciente" runat="server">
                    <div class="box-header with-border">
                        <h3 class="box-title">Búsqueda de clientes</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanelCita" runat="server">
                            <ContentTemplate>
                                <%--PANEL DE AVISOS--%>
                                <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-success alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
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
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelCritico" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelAdvertencia" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-warning alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeAdvertencia" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <!--HACK-->
                                <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />
                               

                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Rango de fechas:</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar fa-2x"></i>
                                                </div>
                                              <asp:TextBox runat="server" ID="Fechas" CssClass="form-control Fechas daterange input-lg"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3" id="div_2" runat="server">
                                        <div class="form-group">
                                            <label>Clientes:</label>
                                            <div class="input-group col-md-12">
                                                <asp:DropDownList ID="DDTiposDeCargos" class="form-control" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="1038 or A.idcosto=1027 or A.idcosto=1046 or A.idcosto=1044 or A.idcosto=1047 or A.idcosto=1048 or A.idcosto=1049 or A.idcosto=1050 or A.idcosto=1057 or A.idcosto=1058 or A.idcosto=1059 or A.idcosto=1060 or A.idcosto=1061 or A.idcosto=1062 or A.idcosto=1063 or A.idcosto=1064 or A.idcosto=1065 or A.idcosto=1066 or A.idcosto=1067">Pacientes de Publico en General</asp:ListItem>
                                                    <asp:ListItem Value="1039">Pacientes de Millet</asp:ListItem>
                                                    <asp:ListItem Value="1040">pacientes Deplayusa</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3" id="div2" runat="server">
                                        
                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6 " id="div3" >
                                                <%--<div class="form-check">
                                                  <input class="radio-inline radioCaso" type="radio" name="radioOpcion" id="radioDinero"  data-contenedor="contenedor_dinero" />
                                                  <label class="form-check-label" for="flexRadioDefault1">
                                                    Por terapias (#)
                                                  </label>
                                                </div>
                                                <div class="form-check">
                                                  <input class="radio-inline radioCaso" type="radio" name="radioOpcion" id="radioNoFac" data-contenedor="contenedor_numeroFac"/>
                                                  <label class="form-check-label" for="flexRadioDefault2">
                                                    Por monetización ($)
                                                  </label>
                                                </div>--%>

                                                <div class="form-check form-group">
                                                        <asp:CheckBox ID="ChkTerapias" runat="server" AutoPostBack="true" />
                                                        <label class="form-check-label" for="flexRadioDefault1">
                                                            Por terapias (#)
                                                        </label>
                                                </div>
                                                <div class="form-check form-group">
                                                    <asp:CheckBox ID="ChkMonetizacion" runat="server" AutoPostBack="true" />
                                                  <label class="form-check-label" for="flexRadioDefault2">
                                                    Por monetización ($)
                                                  </label>
                                                </div>


                                        </div>
                                        

                                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6"  >
                                                <div class="contenedores " id="div_numeroFac2" runat ="server" visible="false" >
                                                    <label>No de terapias a fucturar</label>
                                                    <div class="input-group col-md-12">
                                                        <asp:TextBox  type="number" min="1" CssClass="form-control" ID="TxtTerapias" runat="server" AutoPostBack="true" onkeypress="return ValSoloNumeros(event)"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="contenedores" id="div_dinero2" runat ="server" visible ="false" >
                                                    <label>Cantidad monetaria</label>
                                                    <div class="input-group col-md-12">
                                                        <asp:TextBox type="number" min="1" value="1" step="any" CssClass="form-control" ID="TxtMonetizacion" runat="server" AutoPostBack="true" onkeypress="return onlyDotsAndNumbers(this,event);"></asp:TextBox>
                                                    </div>
                                                </div>
                                         </div>

                                        <script>
                                            function onlyDotsAndNumbers(txt, event) {
                                                var charCode = (event.which) ? event.which : event.keyCode
                                                if (charCode == 46) {
                                                    if (txt.value.indexOf(".") < 0)
                                                        return true;
                                                    else
                                                        return false;
                                                }

                                                if (txt.value.indexOf(".") > 0) {
                                                    var txtlen = txt.value.length;
                                                    var dotpos = txt.value.indexOf(".");
                                                    //Change the number here to allow more decimal points than 2
                                                    if ((txtlen - dotpos) > 2)
                                                        return false;
                                                }

                                                if (charCode > 31 && (charCode < 48 || charCode > 57))
                                                    return false;

                                                return true;
                                            }
        </script>

                                         <%-- <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6" id="div4" >
                                                <div class="contenedores contenedor_numeroFac" id="contenedor_numeroFac" style="display:none" >
                                                    <label>No de terapias a fucturar</label>
                                                    <div class="input-group col-md-12">
                                                        <input type="number" class="form-control" id="Ndatos" runat="server"  onkeypress="return ValSoloNumeros(event)"/>
                                                    </div>
                                                </div>
                                                <div class="contenedores contenedor_dinero" id="contenedor_dinero" style="display:none">
                                                    <label>Cantidad monetaria</label>
                                                    <div class="input-group col-md-12">
                                                        <input type="number" class="form-control" id="CantMonetaria" runat="server"  onkeypress="return ValSoloNumeros(event)"/>
                                                    </div>
                                                </div>
                                         </div>--%>
                                       
                                    </div>
                                          

                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-12">
                                        <div class="form-group">
                                            <label></label>
                                            <button type="button" class="btn btn-flat btn-info btn-block btn-lg" runat="server" onserverclick="cargaFacturas">
                                                <i class="fa fa-search"></i> Buscar
                                            </button>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4" id="div1" runat="server">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <asp:DropDownList ID="cmbfacturacion" class="form-control" runat="server" AutoPostBack="true">
                                                    <asp:ListItem Value="1">Facturar a Publico en General</asp:ListItem>
                                                    <asp:ListItem Value="2">Facturar a Deplayusa</asp:ListItem>
                                                    <asp:ListItem Value="3">Facturar a Millet</asp:ListItem>

                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                </div>


                                <!--RESULTADOS DE BUSQUEDA-->
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <p>Cliente A Facturar</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <asp:ListBox ID="LstBoxPacientes" runat="server" CssClass="form-control"></asp:ListBox>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>

                <%--------------------- Fin Cambios-------------------------------%>
                <div>
                    <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                        <ContentTemplate>
                            <asp:Panel Style="width: 100%" ID="panelError" runat="server" Visible="TRUE">
                                <center>    
                       <div class="box-comment">
                        <table style ="padding :4px 8px 4px 8px;" >
                            <tr>
                                <td><h4><asp:Label ID="LblNomPaciente" runat="server" Text="" ></asp:Label></h4></td>                               
                            </tr>
                        </table>                   
                        </div>
                         </center>
                            </asp:Panel>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>

                <div class="box box-success" id="box_citas_programadas">
                    <div class="box-header with-border">
                        <h3 class="box-title">Consultas por facturar</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <!-- /.box-header -->
                    <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">                            
                            <ContentTemplate>
                           

                                <%--------------------- Avisos en pantalla-------------------------------%>

                                <asp:Panel ID="PanelAviso2" runat="server" Visible="false">
                                    <div class="alert alert-success" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <i class="icon fa fa-check fa-2x ">
                                            <asp:Label ID="LblAviso" runat="server" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelDesicion2" runat="server" Visible="false">
                                    <div class="alert alert-info" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <p>
                                            <i class="icon fa fa-hand-o-right fa-2x">
                                                <asp:Label ID="LblDesicion" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                                        </p>
                                        <p>
                                            <asp:Button ID="BtnSi2" class="btn btn-success" runat="server" Text="   Si   " Width="109px" />
                                            <asp:Button ID="BtnNo2" runat="server" Text="   No   " class="btn btn-danger" Width="109px" />
                                        </p>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelCritico2" runat="server" Visible="false">
                                    <div class="alert alert-danger" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <i class="icon fa fa-ban fa-2x">
                                            <asp:Label ID="Lblcritico" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                                    </div>
                                </asp:Panel>
                                <asp:Panel ID="PanelAdvertencia2" runat="server" Visible="false">
                                    <div class="alert alert-warning" role="alert">
                                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                        <i class="icon fa fa-warning fa-2x">
                                            <asp:Label ID="LblAdvertencia" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                                    </div>
                                </asp:Panel>

                                <asp:GridView ID="dtgCargosConsultas" runat="server" CssClass="dataTable table-responsive table table-striped tabla_citas_programadas" AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="idabono" HeaderText="IdAbono">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CodigoPaciente" HeaderText="Paciente" HtmlEncode="false">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Consultorio" HeaderText="Médico">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="servicio" HeaderText="Servicio">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Abono" HeaderText="Abono" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Selección">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                <%--<asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>
                                  <asp:GridView ID="DtgCargosManuales" runat="server" CssClass="dataTable table-responsive table table-striped tabla_citas_programadas" AutoGenerateColumns="False" >
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="FolioCargo" HeaderText="Folio">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcionConcepto" HeaderText="Concepto">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="subtotal" HeaderText="Importe" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                         <asp:BoundField DataField="importeIva" HeaderText="IVA" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>                                        
                                        <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                                         <asp:BoundField DataField="observaciones" HeaderText="Anotaciones">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Consultorio" HeaderText="Médico"   >                                              
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="usuario" HeaderText="Usuario">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Selección">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" Checked="true" />
                                                <%--<asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EditRowStyle BackColor="#999999" />
                                    <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                                    <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#E9E7E2" />
                                    <SortedAscendingHeaderStyle BackColor="#506C8C" />
                                    <SortedDescendingCellStyle BackColor="#FFFDF8" />
                                    <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                                </asp:GridView>

                                <hr />
                                <button type="button" class="btn btn-lg btn-success btn-block" runat="server" id="btn_facturar" onserverclick="Facturar"><i class="fa fa-check-square-o"></i>Verificar Consultas</button>
                                            
       
                                
                                 <CR:CrystalReportViewer ID="origen_reporte" runat="server" AutoDataBind="true" />
                  <CR:CrystalReportSource ID="visor_reporte" runat="server"></CR:CrystalReportSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        </div>
                       
                    <!-- /.box-body -->
                   </div>
            </div>
        </div>
    </section>  

                          <!--Modal Facturar -->
            <div id="datos_facturacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Datos de Facturación</h4>
                        </div>                      
                        <asp:UpdatePanel  runat="server">
                            <ContentTemplate>     
                                <div class="modal-body">
                                    <div class="form-body">

                                        <div class="row" id="div_dfacturacion" runat="server" >
                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" >
                                                            <label>VersionCFDI</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="VersionCFDI" id="tbVersion" runat="server">
                                                            </div>
                                                        </div>
                                 <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Comprobante</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Tipo" id="tbTipoComprobante" runat="server"  ></div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Serie</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Serie" id="tbSerie" runat="server"  ></div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Folio</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Folio" id="tbFolio" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Fecha</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Fecha" id="dtpFecha" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Exportacion</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                <asp:DropDownList ID="tbExportacion" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                
                                                    </div>
                                        <div class="row" id="div_emisor" runat="server">
                               <div class="modal-header">
             <h4 class="modal-title">Datos del Emisor</h4>
          </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <label>Emisor</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorNombre" id="tbEmisorNombre" runat="server"  >
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>RFC</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorRFC" id="tbEmisorRFC" runat="server"  ></div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>R.Fiscal</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorRegimenFiscal" id="tbEmisorRegimenFiscal" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>L.Expedicion</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="LugarExpedicion" id="tbLugarExpedicion" runat="server"  ></div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Moneda</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Moneda" id="tbMoneda" runat="server"  ></div>
                                                        </div>
                                
                                                    </div>

                                        <div class="row">

                           <div class="modal-header">
             <h4 class="modal-title">Datos del Receptor</h4>
          </div>



                           <div class="form-group" id="div_select_razon_social" runat="server">
                               <label class="col-md-3">Razón Social</label>
                               <div class="input-group col-md-8">
                                   <asp:DropDownList ID="select_razon_social" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CambioSelectRazonSocial"></asp:DropDownList>
                               </div>
                           </div>
                           <div class="form-group" id="div_input_razon_social" runat="server">
                               <label class="col-md-3">Razón Social</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Razón Social" id="tbReceptorNombre" name="input_razon_social" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">RFC</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="RFC" id="tbReceptorRFC" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Correo</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Correo" id="correo" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_direccion" runat="server">
                               <label class="col-md-3">Dirección</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Dirección" id="direccion" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_cp" runat="server">
                               <label class="col-md-3">CP(Domicilio Fiscal)</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                   <input type="number" class="form-control" placeholder="Código Postal" id="cp" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_ciudad" runat="server">
                               <label class="col-md-3">Ciudad</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Ciudad" id="ciudad" runat="server"  >
                               </div>
                           </div>
                           <div class="form-group" id="div_estado" runat="server">
                               <label class="col-md-3">Estado</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Estado" id="estado" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_pais" runat="server">
                               <label class="col-md-3">País</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="País" id="pais" runat="server">
                               </div>
                           </div>
                                            <%--AB SAT 4.0--%>
                                            <div class="form-group" id="div_regfiscal" runat="server">
                               <label class="col-md-3">Regimen Fiscal</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                   <asp:DropDownList ID="tbReceptorRegimenFiscal" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>  
                               </div>
                           </div>
                               </div>
                                        <div class="row">
                               <div class="modal-header">
             <h4 class="modal-title">Datos del Comprobante</h4>
          </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Forma de Pago</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <asp:DropDownList ID="tbFormaPago" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Metodo de Pago</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                <asp:DropDownList ID="tbMetodoPago" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                   </div>
                                                        </div>

                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>UsoCFDI</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                 <asp:DropDownList ID="tbReceptorUsoCFDI" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                   
                                                                
                                                            </div>
                                                        </div>
                               <%--<div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>L.Expedicion</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="LugarExpedicion" id="Text4" runat="server"  ></div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Moneda</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Moneda" id="Text5" runat="server"  ></div>
                                                        </div>--%>
                                
                                                    </div>

                                         <div class="form-group">
                               <label class="col-md-3">Conceptos por Facturar</label>
                               <div class="input-group col-md-8">
                                   <%--<asp:GridView ID="GdConsultasXpagar" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                       <Columns>
                                           <asp:BoundField DataField="cantidad" HeaderText="cantidad" />
                                           <asp:BoundField DataField="descripcion" HeaderText="Descricion" />
                                           <asp:BoundField DataField="costo" HeaderText="Importe" />
                                       </Columns>
                                   </asp:GridView>--%>
                                    <asp:DataGrid ID="gridDetalles" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateColumn HeaderText="Cantidad" >
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1" runat="server" AutoPostBack="True" Text='<%# Bind("cantidad") %>' enabled="false"  BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("descripcion")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Importe"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox3" runat="server" AutoPostBack="True" Text='<%# Bind("costo")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid> 
                                   <asp:DataGrid ID="gridDetallesC" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateColumn HeaderText="Cantidad" >
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" AutoPostBack="True" Text='<%# Bind("cantidad") %>' enabled="false"  BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("descripcionConcepto")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Importe"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="True" Text='<%# Bind("subtotal")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Referencia"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" AutoPostBack="True" Text='<%# Bind("observaciones")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>                                 
                               </div>
                           </div>
                                         <div class="form-group">
                               <label class="col-md-3">Paciente</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Nombre del paciente!" id="nompaciente" runat="server">
                               </div>
                           </div>
                                        <div class="form-group">
                               <label class="col-md-3">Observaciones</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Observaciones de la Factura si es requerido!" id="dcobservaciones" runat="server">
                               </div>
                           </div>


                                        <div class="row" id="div_UPfiscal" runat="server">
                                <div class="modal-header">
             <h4 class="modal-title">Configuraciones</h4>
          </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>UsuarioFD</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="UsuarioFoliosDigitales" id="tbUsuarioFoliosDigitales" runat="server"  >
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>PasswordFD</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="PasswordFoliosDigitales" id="tbPasswordFoliosDigitales" runat="server"  ></div>
                                                        </div>
                                <%--<div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Subtotal</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="numSubtotal" id="numSubtotal" runat="server"  ></div>
                                                        </div>
                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Descuento</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="numDescuento" id="numDescuento" runat="server"  ></div>
                                                        </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Total</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Total" id="numTotal" runat="server"  ></div>
                                                        </div>--%>



                                </div>
                                    </div>
                                    <div class="modal-footer">
                                        <input type="hidden" id="codigo_paciente" runat="server">
                                        <input type="hidden" id="dpaciente" runat="server">
                                        <input type="hidden" id="idcosto" runat="server">
                                        <input type="hidden" id="id_razon_social" runat="server">
                                        <input type="hidden" id="folio_consulta" runat="server">
                                        <input type="hidden" id="bandera" runat="server">                                     
                                        <button type="button" class="btn btn-success" data-dismiss="modal" runat="server"  id="C_facturar" onserverclick="FacturarConsultas" causesvalidation="true" validationgroup="datosFac"><i class="fa fa-save"></i> Facturar C</button>
                                        
                                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
                                   <asp:HiddenField runat="server" ID="fconsulta" />
                                         </div>
                                </div>                        
                       </ContentTemplate></asp:UpdatePanel> 
                        
                    </div>
                </div>
            </div>

                       
</asp:Content>
