<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="conciliacion.aspx.vb" Inherits="AgeMED.conciliacion" %>

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
                <h1>Asignacion
            <small>De Facturas</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
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
                                <h3 class="box-title">Busqueda por nombre del paciente</h3>
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
                            <%--<asp:Button ID="BtnSi1" class="btn btn-success" runat="server" Text="   Si   " Width="109px" OnClick="BtnSi_Click" />
                            <asp:Button ID="BtnNo1" runat="server" Text="   No   " class="btn btn-danger" Width="109px" OnClick="BtnNo_Click" />--%>
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
                                <!--NOMBRE-->
                                <div class="row">                           
                            <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="TxbPaterno" runat="server" type="text" CssClass="form-control" onkeypress="return ValSoloLetras(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Apellido paterno"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TxbPaterno_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbPaterno_TextBoxWatermarkExtender" TargetControlID="TxbPaterno" WatermarkText="Apellido Paterno" />
                                    </div>
<%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxbPaterno" ValidationGroup="buscarPaciente"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="TxbMaterno" runat="server" CssClass="form-control" onkeypress="return ValSoloLetras(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Apellido materno"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TxbMaterno_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbMaterno_TextBoxWatermarkExtender" TargetControlID="TxbMaterno" WatermarkText="Apellido Materno" />
                                        <%-- <input type="text" class="form-control" placeholder="Segundo Apellido" name="materno">--%>
                                    </div>
                                </div>
                            </div>
                             <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="TxbNombre" runat="server" CssClass="form-control" onkeypress="return ValSoloLetrasEspacioBlanco(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Nombre"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TxbNombre_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbNombre_TextBoxWatermarkExtender" TargetControlID="TxbNombre" WatermarkText="Nombre" />
                                    </div>
                                </div>
                            </div>
                                     <div class="col-xs-12 col-sm-12 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <button type="button" class="btn btn-primary btn-lg" runat="server" onserverclick="BtnBuscarPaciente_Click" ><span runat="server" id="BotonBuscar"><i class="fa fa-search"></i> Buscar</span></button>
                                    </div>
                                </div>
                            </div>
                        </div>
                                <!--RESULTADOS DE BUSQUEDA-->
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <p>Paciente(s) encontrado(s)</p>
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
                                <hr />
                              <%--  <button type="submit" class="btn btn-success btn-lg" runat="server" onserverclick="BtnVer_Click"><i class="fa fa-eye"></i>Ver</button>--%>
                                
                                <h5>Datos Generales de la factura</h5>
                                <div class="row">
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Fecha</label>
                                            <div class="input-group">
                                                <div class="input-group-addon">
                                                    <i class="fa fa-calendar fa-fw"></i>
                                                </div>
                                                <asp:TextBox runat="server" ID="Fecha" CssClass="form-control datepicker Fecha" placeholder="Fecha" ReadOnly="true"></asp:TextBox>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Importe de la Factura</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-dollar fa-fw"></i></span>
                                                <asp:TextBox ID="TxtImporte" runat="server" class="form-control " onkeypress="return ValSoloNumeros(event)" placeholder="Importe de la factura" ReadOnly="true"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Factura &nbsp; &nbsp;  </label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <asp:TextBox ID="TxtReferencia" runat="server" placeholder="Factura" class="form-control " MaxLength ="20"  onkeypress="return ValNumerosLetras(event)" style="text-transform: uppercase;"></asp:TextBox>
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ValidationGroup="gDatos" ErrorMessage="Dato Obligatorio" ControlToValidate="TxtReferencia" ForeColor="red"></asp:RequiredFieldValidator>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ValidationGroup="gBuscar" ErrorMessage="Dato Obligatorio" ControlToValidate="TxtReferencia" ForeColor="red"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <button type="button" class="btn btn-warning btn-lg btn-block" id="BtnBuscar" runat="server" onserverclick="buscarReferenciaBancaria"><i class="fa fa-search"></i></button>
                                        </div>
                                    </div>
                                    <div class="col-lg-1 col-md-1 col-sm-2 col-xs-2">
                                        <div class="form-group">
                                            <label>&nbsp;</label>
                                            <button type="button" class="btn btn-success btn-lg btn-block" id="BtnVer" runat="server" onserverclick="verReferenciaBancaria" causesvalidation="true" validationgroup="gBuscar"><i class="fa fa-eye"></i></button>
                                        </div>
                                    </div>
                                </div>
                                 <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Paciente:</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <textarea id="observaciones" runat="server" placeholder="Paciente" class="form-control" rows="1" onkeypress ="return ValNumerosLetrasSim(event)" readonly></textarea>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Serie:</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <asp:TextBox ID="TxtSerie" runat="server" class="form-control " onkeypress="return ValSoloNumeros(event)" placeholder="Serie" readonly="true"></asp:TextBox>
                                                
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Tipo:</label>
                                            <asp:DropDownList ID="DdTipoPago" runat="server" class="form-control "></asp:DropDownList>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                    </div>
                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                        <div class="form-group">
                                            <label>Saldo para Asignar</label>
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-dollar fa-fw"></i></span>
                                                <input id="saldo" type="text" runat="server" class="form-control " onkeypress="return ValSoloNumeros(event)" placeholder="Saldo para Asignar" readonly />
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
                                <hr />
                                <div class="table-responsive scrolling-table-container2" >
                                    <asp:UpdatePanel ID="UpdatePanelGDFacturas" runat="server"><ContentTemplate>
                                    <asp:GridView ID="GdFacturas" runat="server"   ClientIDMode ="Static" CssClass ="table table-striped  table-hover table-condensed small-top-margin" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField AccessibleHeaderText="IDAbono" DataField="idabono" HeaderText="IDAbono" />
                                            <asp:BoundField AccessibleHeaderText="Recibo" DataField="idcita" HeaderText="Recibo" />
                                            <asp:BoundField AccessibleHeaderText="Paciente" DataField="CodigoPaciente" HeaderText="Paciente" />
                                            <asp:BoundField AccessibleHeaderText="Fecha" DataField="fecha" HeaderText="Fecha" />
                                            <asp:BoundField AccessibleHeaderText="Servicio" DataField="servicio" HeaderText="Servicio" />
                                            <asp:BoundField AccessibleHeaderText="Abono" DataField="abono" HeaderText="Abono" />
                                            <asp:BoundField AccessibleHeaderText="Importe" DataField="importe" HeaderText="Importe" />
                                            <asp:BoundField AccessibleHeaderText="Coaseguro" DataField="coaseguro" HeaderText="Coaseguro" />
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
                                    <asp:HiddenField ID="Hdidcliente" runat="server" />
                                </div>
                                <hr />
                                <button type="button" class="btn btn-lg btn-success btn-block" runat="server" id="btn_conciliar" onserverclick="Conciliar" causesvalidation="true" validationgroup="gDatos"><i class="fa fa-check-square-o"></i>Asignar</button>
                            </div>
                        </div>
                          
                    </div>
            </section>

               </ContentTemplate>
    </asp:UpdatePanel>

            <!--Modal facturas asignadas-->
            <div id="modal_referencia" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Abonos asignadas a la factura</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="table-responsive">
                                        <h5>Abonos</h5>
                                        <asp:Label ID="LblSinFacturas" runat="server" Text=""></asp:Label>
                                        <asp:GridView ID="GdFacConciliadas" runat="server" CssClass="table table-bordered table-striped ListaFacturas" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField AccessibleHeaderText="IDAbono" DataField="idabono" HeaderText="IDAbono" />
                                                <asp:BoundField AccessibleHeaderText="Recibo" DataField="idcita" HeaderText="Recibo" />
                                                <asp:BoundField AccessibleHeaderText="Factura" DataField="factura" HeaderText="Factura" />
                                                <asp:BoundField AccessibleHeaderText="Serie" DataField="serie" HeaderText="Serie" />
                                                <asp:BoundField AccessibleHeaderText="Importe" DataField="importe" HeaderText="Importe" />
                                                 <asp:BoundField AccessibleHeaderText="Servicio" DataField="servicio" HeaderText="Servicio" />
                                                <asp:BoundField AccessibleHeaderText="IDFactura" DataField="idfactura" HeaderText="IDFactura" />
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
                                        <button type="button" class="btn btn-lg btn-success btn-block" runat="server" data-dismiss="modal" aria-hidden="true" id="BtnDesasignarFAC" onserverclick="Desasignar"><i class="fa fa-window-close-o"></i> Desasignar Abonos</button>

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
                            <h4 class="modal-title">Facturas</h4>
                        </div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <div class="modal-body">
                                       <h5>Facturas Encontradas</h5>
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                    <div class="table-responsive scrolling-table-container">                                     
                                        <asp:GridView ID="GdBuscarReferencias" runat="server" CssClass="table table-bordered table-striped ListaFacturas" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField AccessibleHeaderText="ID" DataField="idfactura" HeaderText="id" />
                                                <asp:BoundField AccessibleHeaderText="Factura" DataField="num_factura" HeaderText="Factura" />
                                                <asp:BoundField AccessibleHeaderText="Serie" DataField="serie" HeaderText="Serie" />
                                                <asp:BoundField AccessibleHeaderText="Importe" DataField="importe" HeaderText="Importe" />
                                                <%--<asp:BoundField AccessibleHeaderText="Saldo" DataField="saldo" HeaderText="Saldo" />--%>
                                                <asp:BoundField AccessibleHeaderText="Fecha" DataField="fecha" HeaderText="Fecha" />
                                                 <asp:BoundField AccessibleHeaderText="Facturado A" DataField="nombre" HeaderText="Facturado A" />
                                                <asp:BoundField AccessibleHeaderText="Paciente" DataField="paciente" HeaderText="Paciente" />
                                                 <asp:BoundField AccessibleHeaderText="IdCliente" DataField="idcliente" HeaderText="IdCliente" />
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
