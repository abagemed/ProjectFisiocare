<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="pagos.aspx.vb" Inherits="AgeMED.pagos" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <style media="print">
                .cabecera_print {
                    font: 8pt Microsoft San Serif;
                    text-align: center;
                    margin-bottom: 3pt;
                }

                    .cabecera_print p {
                        margin: 0;
                    }

                table {
                    font: 8pt Microsoft San Serif;
                }

                .pie_print {
                    font: 8pt Microsoft San Serif;
                    text-align: center;
                }

                .total_print {
                    font: 8pt Microsoft San Serif;
                    text-align: center;
                }

                h2 {
                    text-align: center;
                }

                #cantidad_letras {
                    font: 8pt;
                }
            </style>

            <script>

                function PrintTicket() {
                    $("#cantidad_letras").append("SON: " + NumeroALetras($('#<%=LblPagoTotal2.ClientID%>').html()));
            var divToPrint = document.getElementById("ticket");

            newWin = window.open("");
            newWin.document.write('<html><head><style>');
            newWin.document.write('body{font-size:.8em; font-family: Arial, "Helvetica Neue", Helvetica, sans-serif;}');
            newWin.document.write('@page{ size: auto; margin: 3mm 3mm 3mm 3mm;}');
            newWin.document.write('.cabecera_print {text-align:center;margin-bottom:.3em;}');
            newWin.document.write('p {text-align:center;font-size:.8em; font-family: Arial, "Helvetica Neue", Helvetica, sans-serif;margin-bottom:.3em;-webkit-margin-before: .3em;-webkit-margin-after: .2em;-webkit-margin-start: 0px;-webkit-margin-end: 0px;}');
            newWin.document.write('table {font-size:.8em; font-family: Arial, "Helvetica Neue", Helvetica, sans-serif;}');
            newWin.document.write('h2 {text-align:center}');
            //newWin.document.write('.total_print{font-size:.8em; font-family: Arial, "Helvetica Neue", Helvetica, sans-serif;text-align:center}');
            newWin.document.write('#cantidad_letras{font-size:.8em; font-family: Arial, "Helvetica Neue", Helvetica, sans-serif;text-align:center}');
            newWin.document.write('</style></head><body>');
            newWin.document.write(divToPrint.outerHTML);
            newWin.document.write('</body></html>');
            newWin.print();
            newWin.close();
        }
        window.onload = function () {
            $("#catalogo").DataTable({
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
            $(".ListaPagos").select2({
                allowClear: true,
                placeholder: "Escriba el concepto a cobrar",
                maximunSelectSize: 10
            });
            if ($(".mensaje_pago").text() == "El pago esta registrado") {
                $(".btn-danger").hide();
            }
            $(".forma_pago").change(function () {
                if ($('#<%=forma_pago.ClientID%>').val() != '1') {
                    $('#contenedor_referencia').removeClass('hidden');
                }
                else {
                    $('#contenedor_referencia').addClass('hidden');
                }
            });

        }
        function Hack() {
            $("#catalogo").DataTable({
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
            $(".ListaPagos").select2({
                allowClear: true,
                placeholder: "Escriba el concepto a cobrar",
                maximunSelectSize: 10
            });
            if ($("#mensaje_pago").text() == "El pago esta registrado") {
                $(".btn-danger").hide();
            }

        }
            </script>




            <!--HACK-->
            <script>
                function ValSoloNumeros(e) {
                    tecla = (document.all) ? e.keyCode : e.which;
                    if (tecla == 8) return true;
                    patron = /[0-9]/;
                    te = String.fromCharCode(tecla);
                    return patron.test(te);
                }
            </script>

            <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />


            <section class="content-header">
                <h1>Control
            <small>Pagos</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="Agenda.aspx"><i class="fa fa-home"></i>Inicio</a></li>
                    <li class="active" runat="server"><i class="fa fa-credit-card"></i>Pagos</li>
                </ol>

                <!--HACK-->
            </section>



            <section class="content">
                <div class="row" id="lista_conceptos" runat="server">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header">
                                <h3 class="box-title">Lista de conceptos</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <div class="row">
                                    <div class="col-md-2">
                                        <div class="input-group">
                                            <span class="input-group-addon"><i class="fa fa-cubes"></i></span>
                                            <input type="number" class="form-control" placeholder="Cantidad" name="cantidad" id="cantidad" value="1" runat="server" min="1" onkeypress="return ValSoloNumeros(event)">
                                        </div>
                                    </div>
                                    <div class="col-md-10">
                                        <asp:ListBox ID="LstConceptoPagos" runat="server" AutoPostBack="True" CssClass="form-control ListaPagos select2-single"></asp:ListBox>
                                    </div>
                                </div>
                            </div>
                            <!-- /.box-body -->
                        </div>
                        <!-- /.box -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <div class="box box-info">
                            <div class="box-header">
                                <h3 class="box-title">Pagos</h3>
                            </div>
                            <!-- /.box-header -->
                            <div class="box-body">
                                <%--PANEL DE AVISOS--%>
                        <asp:Panel ID="PanelAvisosPagos" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-success alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMensajeAvisoPagos" runat="server"></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelDesicionPagos" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-info alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblDecisionPagos" runat="server" Text="Label"></asp:Label></h4>
                                        <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSiPagos_Click"><i class="fa fa-check"></i>Sí</button>
                                        <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNoPagos_Click"><i class="fa fa-close"></i>No</button>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelCriticoPagos" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-danger alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMensajeCriticoPagos" runat="server" Text="Label"></asp:Label>></h4>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>

                        <asp:Panel ID="PanelAdvertenciaPagos" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-warning alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMensajeAdvertenciaPagos" runat="server" Text="Label"></asp:Label></h4>
                                    </div>
                                </div>
                            </div>
                        </asp:Panel>
                                <asp:GridView ID="GdvConceptoPagos" runat="server" GridLines="None" AutoGenerateColumns="False" CssClass="table table-striped">
                                    <Columns>
                                        <asp:BoundField DataField="codigoConcepto" HeaderText="idPago" Visible="false" InsertVisible="true"></asp:BoundField>
                                        <asp:BoundField AccessibleHeaderText="Cantidad" DataField="cantidad" HeaderText="Cantidad" />
                                        <asp:BoundField DataField="servicio" HeaderText="Servicio" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                                        <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                        <asp:TemplateField ItemStyle-Wrap="true">
                                            <ItemTemplate>
                                                <asp:LinkButton runat="server" CommandName="borrarConceptoPago" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                    class="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                            </ItemTemplate>

                                            <ItemStyle Wrap="True"></ItemStyle>
                                        </asp:TemplateField>
                                    </Columns>
                                    <EmptyDataTemplate>
                                        <h4>No hay Pagos seleccionados</h4>
                                    </EmptyDataTemplate>
                                </asp:GridView>

                                <h2 class="text-center mensaje_pago" id="mensaje_pago" runat="server"></h2>

                                <div class="row">
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <h3 class="text-center">Total:</h3>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <h3><strong>$
                                            <asp:Label ID="LblPagoTotal" runat="server"></asp:Label></strong></h3>
                                    </div>
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <h3 class="text-center">Abono:</h3>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <div class="input-group" style="margin: 10px 0">
                                            <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                                            <asp:TextBox ID="TxtAbono" runat="server" class="form-control input-lg" placeholder="0.0" onkeypress="return ValSoloNumeros(event)" AutoPostBack="true"></asp:TextBox>
                                            <%--                                    <input type="number" class="form-control input-lg" placeholder="0.0" name="apoyo" id="apoyo"  runat="server" min="1" onkeypress="return ValSoloNumeros(event)">--%>
                                        </div>
                                    </div>
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                </div>

                                <div class="row">
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <h3 class="text-center">Saldo:</h3>
                                    </div>
                                    <div class="col-lg-3 col-md-3 col-sm-6 col-xs-6">
                                        <h3><strong>$
                                            <asp:Label ID="LblPagoSaldo" runat="server"></asp:Label></strong></h3>
                                    </div>
                                    <div class="col-lg-3 col-md-3 hidden-sm hidden-xs"></div>
                                </div>

                            </div>
                            <asp:HiddenField ID="HdPreguntasPagos" runat="server" />
                            <asp:HiddenField ID="HdIndexPagos" runat="server" />
                            <asp:HiddenField ID="Hffolioconsulta" runat="server" />
                        </div>
                        <!-- /.box-body -->
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-12">
                        <button type="button" class="btn btn-block btn-success" id="imprimir_ticket_btn" runat="server" onserverclick="Imprimir_Ticket">
                            Imprimir Ticket <i class="fa fa-print"></i>
                        </button>
                        <button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#modalVenta" id="registrar_pago" runat="server">
                            Registrar pago <i class="fa fa-plus"></i>
                        </button>
                    </div>
                </div>
            </section>



            <!--Modal -->
            <div id="modalVenta" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Pagos</h4>
                        </div>
                        <div class="modal-body">
                            <asp:GridView ID="GdvConceptoPagos2" runat="server" Visible="true" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
                                <Columns>
                                    <asp:BoundField DataField="codigoConcepto" HeaderText="idPago" Visible="false" InsertVisible="true"></asp:BoundField>
                                    <asp:BoundField AccessibleHeaderText="Cantidad" DataField="cantidad" HeaderText="Cantidad" />
                                    <asp:BoundField AccessibleHeaderText="Servicio" DataField="servicio" HeaderText="Servicio" />
                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                                    <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <h4>No hay Pagos seleccionados</h4>
                                </EmptyDataTemplate>
                            </asp:GridView>
                            <h3 class="text-center">Total: <strong>$
                                <asp:Label ID="LblPagoTotal2" runat="server"></asp:Label></strong></h3>
                            <div class="row">
                                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group">
                                        <span>Metodo de Pago</span>
                                        <select class="form-control forma_pago" id="forma_pago" runat="server">
                                            <option value="1" selected>Efectivo</option>
                                            <option value="2">Tarjeta de Crédito/Débito</option>
                                            <option value="5">Cheque</option>
                                            <option value="6">Transferencia</option>
                                            <option value="7">Depósito</option>
                                        </select>

                                    </div>
                                </div>
                                <div class="col-lg-6 col-md-12 col-sm-12 col-xs-12">
                                    <div class="form-group hidden" id="contenedor_referencia">
                                        <span>Referencia</span>
                                        <div class="input-group">
                                            <input type="text" id="referencia" runat="server" placeholder="Referencia" class="form-control">
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div>
                                <button type="button" class="btn btn-primary" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="ingresoCaja"><i class="fa fa-check-circle"></i>Registrar pago</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cerrar</button>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

            <%--TICKET NORMAL
        <div id="ticket" class="hidden">
        <div class="cabecera_print">
            <p><strong>ORTOPEDIA STAR, S.C.P.</strong></p>
            <p>OST141127IEA</p>
            <p>C. 26 No. 299 Int. 928 FRACC. ALTABRISA.</p>
            <p>MÉRIDA, YUCATÁN, MÉXICO. C.P. 97133</p>
        </div>
        <p runat="server" id="folio_consulta"></p>
        <p runat="server" id="fecha_ticket"></p>
        <hr />
        <asp:GridView ID="GdvConceptoPagos3" runat="server" Visible="true" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
            <Columns>
                <asp:BoundField DataField="codigoConcepto" HeaderText="idPago" Visible="false" InsertVisible="true"></asp:BoundField>
                <asp:BoundField AccessibleHeaderText="Cantidad" DataField="cantidad" HeaderText="Cantidad" />
                <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
            </Columns>
            <EmptyDataTemplate>
                <h4>No hay Pagos seleccionados</h4>
            </EmptyDataTemplate>
        </asp:GridView>
        <hr />
        <p class="total_print">Total: <strong> $ <asp:Label ID="LblPagoTotal3" runat="server"></asp:Label></strong></p>
        <p id="cantidad_letras"></p>
        <div class="pie_print">
            <p>Correo: contacto@med-sol.mx</p>
            <p>Tel: (9999) 99 99 99</p>
        </div>
    </div> 
        TICKET NORMAL --%>
            <div id="ticket" class="hidden">
                <div class="cabecera_print">
                    <p><strong>HOSPITAL DE ORTOPEDIA</strong></p>
                    <p>AVENIDA QUETZALCÓATL NO, 104.</p>
                    <p>X 8B Y 8C COL. VERGEL 65</p>
                    <p>MÉRIDA, YUCATÁN. C.P. 97176</p>
                </div>
                <p runat="server" id="folio_consulta"></p>
                <p runat="server" id="fecha_ticket"></p>
                <hr />
                <p><strong>PACIENTE</strong></p>
                <p runat="server" id="paciente_nombre"></p>
                <p runat="server" id="paciente_telefono"></p>
                <p runat="server" id="paciente_direccion"></p>
                <asp:GridView ID="GdvConceptoPagos3" runat="server" Visible="false" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
                    <Columns>
                        <asp:BoundField DataField="codigoConcepto" HeaderText="idPago" Visible="false" InsertVisible="true"></asp:BoundField>
                        <asp:BoundField AccessibleHeaderText="Cantidad" DataField="cantidad" HeaderText="Cantidad" />
                        <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                        <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                    </Columns>
                    <EmptyDataTemplate>
                        <h4>No hay Pagos seleccionados</h4>
                    </EmptyDataTemplate>
                </asp:GridView>
                <h2>Donativo: <strong>$
                    <asp:Label ID="LblPagoTotal3" runat="server"></asp:Label></strong></h2>
                <p id="cantidad_letras"></p>
                <div class="pie_print">
                    <p>Tel: (9999) 83 02 32</p>
                </div>
                <p>Si usted requiere un comprobante fiscal por su donativo, favor de solicitarlo al momento de efectuar su pago</p>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
