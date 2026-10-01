<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="conciliacionbancaria.aspx.vb" Inherits="AgeMED.conciliacionbancaria" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <style>
        .Fecha, .input-group-addon {
            cursor: pointer;
        }
       .scrolling-table-container {
    height: 478px;
    overflow-y: scroll;
    overflow-x: hidden;
}
        .scrolling-table-container2 {
    height: 280px;
    overflow-y: scroll;
    overflow-x: hidden;
}
    </style> 
          
        <script>
            $(function () {
                $('[data-toggle="tooltip"]').tooltip()
            })
        function ValSoloNumeros(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9.]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValNumerosLetras(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9A-Za-z. -]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValNumerosLetrasSim(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9A-Za-z., -%$#?()*+]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        window.onload = function () {

            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }

            $('.input-group').find('.fa-calendar').on('click', function () {
                $(this).parent().siblings('#<%=Fecha.ClientID%>').trigger('focus');
            });

            $("#<%=Fecha.ClientID%>").datepicker({
                format: "dd/mm/yyyy",
                autoclose: true,
                language: 'es',
                todayBtn: "linked",
                todayHighlight: true
            });


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
            });

            $('input[type="checkbox"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });

        }
        function Hack() {

            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }

            $('.input-group').find('.fa-calendar').on('click', function () {
                $(this).parent().siblings('#<%=Fecha.ClientID%>').trigger('focus');
                    });

                        $("#<%=Fecha.ClientID%>").datepicker({
                        format: "dd/mm/yyyy",
                        autoclose: true,
                        language: 'es',
                        todayBtn: "linked",
                        todayHighlight: true
                    });

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
                    });

                    $('input[type="checkbox"]').addClass("flat-red");
                    //Flat red color scheme for iCheck
                    $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                        checkboxClass: 'icheckbox_flat-green',
                        radioClass: 'iradio_flat-green'
                    });
        }




    
           


    </script>



 <asp:UpdatePanel ID="UpdatePanel1" runat="server" >
        <ContentTemplate>
            <section class="content-header">
                <h1>Conciliación
            <small>Bancaria</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="Agenda.aspx"><i class="fa fa-home"></i>Inicio</a></li>
                    <li class="active"><i class="fa fa-exchange"></i>Facturas</li>
                </ol>
            </section>

            <section class="content">
               
                <!--HACK-->
                <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />
                <!----------------------------------->

                <%--    ---------------------------------------------------------------------------------------------%>
                <div class="row">
                    <div class="col-md-12">
                        
                        <div class="box box-info">
                            <div class="box-header with-border">
                                <h3 class="box-title">Conciliación Bancaria</h3>
                            </div>
                            <div class="box-body">
                                 <%--------------------- Avisos en pantalla-------------------------------%>
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
                    <div class="alert alert-info" role="alert">
                        <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <p>
                            <i class="icon fa fa-hand-o-right fa-2x">
                                <asp:Label ID="LblMostarDecision" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                        </p>
                        <p>
                            <asp:Button ID="BtnSi1" class="btn btn-success" runat="server" Text="   Si   " Width="109px" OnClick="BtnSi_Click" />
                            <asp:Button ID="BtnNo1" runat="server" Text="   No   " class="btn btn-danger" Width="109px" OnClick="BtnNo_Click" />
                        </p>
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
                                <h5>Datos Bancarios y Sucursal</h5>
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Sucursal:</label>
                                          <%--  <select class="form-control " id="sucursal" AutoPostBack="True" runat="server">
                                                <option value="1">Fisiocare Starmedica</option>
                                                <option value="2">Fisiocare Campestre</option>
                                                <option value="3">Fisiocare Ho</option>
                                                <option value="4">Fisiocare Anticanceroso</option>
                                                <option value="5">Gym</option>
                                            </select>--%>
                                             <asp:DropDownList  class="form-control " ID="sucursal" runat="server" AutoPostBack="True" >
                                                <asp:ListItem Value="1">Fisiocare Starmedica</asp:ListItem>
                                                <asp:ListItem Value="2">Fisiocare Campestre</asp:ListItem>
                                                <asp:ListItem Value="3">Fisiocare HO</asp:ListItem>
                                                <asp:ListItem Value="4">Fisiocare Anticanceroso</asp:ListItem>
                                                <asp:ListItem Value="5">Gym</asp:ListItem>
                                                <asp:ListItem Value="6">fisiocare Amerimed</asp:ListItem>
                                            </asp:DropDownList>


                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Fecha</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar fa-fw"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="Fecha" CssClass="form-control datepicker Fecha" placeholder="Fecha"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ValidationGroup="gDatos" ControlToValidate="Fecha" ErrorMessage="Dato Obligatorio" ForeColor="red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Importe depositado</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-dollar fa-fw"></i></span>
                                                <asp:TextBox ID="TxtImporte" runat="server" class="form-control " onkeypress="return ValSoloNumeros(event)" placeholder="Importe depositado"></asp:TextBox>
                                                
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ValidationGroup="gDatos" ErrorMessage="Dato obligatorio" ControlToValidate="TxtImporte" ForeColor="Red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Referencia &nbsp; &nbsp;  </label><button class="btn btn-success btn-xs" runat="server" id="editar_Referencia"  visible ="false" onserverclick ="editarReferencia" ><i class="fa fa-pencil"  ></i>  Editar</a></button> &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;  &nbsp; &nbsp;<button class="btn btn-danger btn-xs" runat="server" id="BtnBorrarReferencia"  visible ="false" onserverclick ="borrarReferenciaBancaria" ><i class="fa fa-trash-o fa-xs"></i>  Borrar</a></button>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <asp:TextBox ID="TxtReferencia" runat="server" placeholder="Referencia" class="form-control " MaxLength ="20"  onkeypress="return ValNumerosLetras(event)" style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="gDatos" ErrorMessage="Dato Obligatorio" ControlToValidate="TxtReferencia" ForeColor="red"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="gBuscar" ErrorMessage="Dato Obligatorio" ControlToValidate="TxtReferencia" ForeColor="red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <button type="button" class="btn btn-warning btn-lg btn-block" id="BtnBuscar" runat="server" onserverclick="buscarReferenciaBancaria" data-tooltip="tooltip" title="Buscar Referencias"><i class="fa fa-search"></i></button>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <button type="button" class="btn btn-primary btn-lg btn-block" id="BtnVer" runat="server" onserverclick="verReferenciaBancaria" causesvalidation="true" validationgroup="gBuscar"><i class="fa fa-eye"></i></button>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                            

                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Tipo:</label>
                                            <asp:DropDownList ID="DdTipoPago" runat="server" class="form-control "></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Cuenta:</label>
                                            <asp:DropDownList ID="DdCuenta" runat="server" class="form-control " AutoPostBack="true"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Observaciones:</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <textarea id="observaciones" runat="server" placeholder="Observaciones" class="form-control" rows="1" onkeypress ="return ValNumerosLetrasSim(event)"></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                             <label>&nbsp;</label>
                                            <button type="button" class="btn btn-success btn-lg btn-block" id="BtnGuardar" runat="server" onserverclick="guardarDatosBancarios" causesvalidation="true" validationgroup="gDatos" data-tooltip="tooltip" title="Guardar Referencia"><i class="fa fa-save"></i></button>
                                            
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                           
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                         
                                        </div>
                                    </div>
                                </div>
                                <hr />
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Saldo</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-dollar fa-fw"></i></span>
                                                <input id="saldo" type="text" runat="server" class="form-control " onkeypress="return ValSoloNumeros(event)" placeholder="Saldo disponible a conciliar" readonly />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-10 col-xs-10">
                                        <div class="form-group">
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                    </div>
                                </div>
                                <div class="table-responsive scrolling-table-container2" >
                                    <asp:UpdatePanel ID="UpdatePanelGDFacturas" runat="server"><ContentTemplate>
                                    <asp:GridView ID="GdFacturas" runat="server"   ClientIDMode ="Static" CssClass ="table table-striped  table-hover table-condensed small-top-margin" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField AccessibleHeaderText="IDFactura" DataField="IDFactura" HeaderText="ID" />
                                            <asp:BoundField AccessibleHeaderText="Serie" DataField="Serie" HeaderText="Serie" />
                                            <asp:BoundField AccessibleHeaderText="Folio" DataField="num_factura" HeaderText="Folio" />
                                            <asp:BoundField DataField="Folio" HeaderText="Folio" />
                                            <asp:BoundField AccessibleHeaderText="RFC" DataField="rfc" HeaderText="RFC" />
                                            <asp:BoundField AccessibleHeaderText="Razón Social" DataField="RazonSocial" HeaderText="Razón Social" />
                                            <asp:BoundField AccessibleHeaderText="Total" DataField="importe" HeaderText="Total" />
                                            <asp:BoundField AccessibleHeaderText="Total" DataField="saldo" HeaderText="Saldo" />
                                            <asp:BoundField AccessibleHeaderText="Paciente" DataField="Paciente" HeaderText="Paciente" />
                                            <asp:BoundField AccessibleHeaderText="Fecha" DataField="fecha" HeaderText="Fecha" />
                                            <asp:BoundField AccessibleHeaderText="Tipo de Pago" DataField="tipopago" HeaderText="Tipo de Pago" HtmlEncodeFormatString="False" />
                                            <asp:TemplateField HeaderText="Selección">
                                                    <ItemTemplate>     
                                                        <asp:Button ID="BtnGdSeleccionar" CssClass="btn btn-block btn-primary " runat="server" Text="Seleccionar" CommandName="Seleccionar" CommandArgument="<%# Container.DataItemIndex %>" Visible="true" />
                                                        <asp:Button ID="BtnGDDesSeleccionar" runat="server" CssClass="btn btn-block btn-danger" Text="Des-Seleccionar" CommandName="Des-Seleccionar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            
                                        </Columns>
                                    </asp:GridView>
                                        </ContentTemplate></asp:UpdatePanel>
                                    <asp:HiddenField ID="Hdgfacturas" runat="server" />
                                    <asp:HiddenField ID="HdIndex" runat="server" />
                                    <asp:HiddenField ID="HdfolioIngreso" runat="server" />
                                    <asp:HiddenField ID="HdAporteParcialFac" runat="server" />
                                     <asp:HiddenField ID="HdPregunta" runat="server" />
                                     <asp:HiddenField ID="HdReferencia" runat="server" />
                                     
                                </div>
                                <hr />
                                <button type="button" class="btn btn-lg btn-success btn-block" runat="server" id="btn_conciliar" onserverclick="Conciliar" causesvalidation="true" validationgroup="gDatos"><i class="fa fa-check-square-o"></i>Conciliar</button>
                            </div>
                        </div>
                          
                    </div>
            </section>
            <asp:HiddenField ID="dbsucursal" runat="server" />
               </ContentTemplate>
    </asp:UpdatePanel>

            <!--Modal facturas asignadas-->
            <div id="modal_referencia" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Facturas asignadas a referencia bancaria</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="table-responsive">
                                        <h5>Facturas</h5>
                                        <asp:Label ID="LblSinFacturas" runat="server" Text=""></asp:Label>
                                        <asp:GridView ID="GdFacConciliadas" runat="server" CssClass="table table-bordered table-striped ListaFacturas" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField AccessibleHeaderText="IDFacturaConciliada" DataField="IDFactura" HeaderText="ID" />
                                                <asp:BoundField AccessibleHeaderText="FolioFactura" DataField="FolioFactura" HeaderText="Folio" />
                                                <asp:BoundField AccessibleHeaderText="Importe" DataField="importe" HeaderText="Total" />
                                                <asp:BoundField AccessibleHeaderText="Fecha" DataField="fechaConciliacion" HeaderText="Fecha" />
                                                 <asp:BoundField AccessibleHeaderText="Estado" DataField="estado" HeaderText="Estado Factura" />
                                                  <asp:TemplateField HeaderText="Desasignar">
                                                    <ItemTemplate>     
                                                        <asp:Button ID="BtnSelecFac" CssClass="btn btn-block btn-primary " runat="server" Text="Seleccionar" CommandName="SeleccionarFAC" CommandArgument="<%# Container.DataItemIndex %>" Visible="true" />
                                                        <asp:Button ID="BtnDesSelecFac" runat="server" CssClass="btn btn-block btn-danger" Text="Des-Seleccionar" CommandName="Des-SeleccionarFAC" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>                                               
                                            </Columns>
                                        </asp:GridView>
                                    </div>


                                    <div>
                                        <button type="button" class="btn btn-lg btn-success btn-block" runat="server" data-dismiss="modal" aria-hidden="true" id="BtnDesasignarFAC" onserverclick="Desasignar"><i class="fa fa-window-close-o"></i> Desasignar Facturas</button>

                                   <%--     <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>--%>

                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!--Modal Referencias-->
            <div id="modal_buscar_referencia" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Folios de referencias bancarias</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                       <h5>Referencias Bancarias</h5>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                    <div class="table-responsive scrolling-table-container">                                     
                                        <asp:GridView ID="GdBuscarReferencias" runat="server" CssClass="table table-bordered table-striped ListaFacturas" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField AccessibleHeaderText="Folio" DataField="folioIngreso" HeaderText="Folio" />
                                                <asp:BoundField AccessibleHeaderText="Referencia" DataField="referencia" HeaderText="Referencia" />
                                                <asp:BoundField AccessibleHeaderText="Importe" DataField="importe" HeaderText="Importe" />
                                                <asp:BoundField AccessibleHeaderText="Saldo" DataField="saldo" HeaderText="Saldo" />
                                                <asp:BoundField AccessibleHeaderText="Observaciones" DataField="observaciones" HeaderText="Observaciones" />
                                                <asp:BoundField AccessibleHeaderText="Fecha Operacion" DataField="fechaOperacion" HeaderText="Fecha" />
                                                <asp:ButtonField ButtonType="Button" CommandName="Abrir" HeaderText="Seleccionar" ShowHeader="True" Text="Abrir" >
                                                    <ControlStyle CssClass="btn btn-block btn-primary btn-xs" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <SelectedRowStyle BackColor="LightSteelBlue" />
                                        </asp:GridView>
                                    </div>


                                    <div>
                                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                                    </div>
                                    
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>
    </asp:Content>
