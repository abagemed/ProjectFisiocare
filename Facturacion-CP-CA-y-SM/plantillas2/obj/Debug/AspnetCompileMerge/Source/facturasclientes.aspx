<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="facturasclientes.aspx.vb" Inherits="AgeMED.facturasclientes" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
            var grid = document.getElementById('<%= GdFacturas.ClientID%>');
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
                    if (window.location.pathname != "/FacturacionClientes.aspx") {
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
                    var grid = document.getElementById('<%= GdFacturas.ClientID%>');
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
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode ="Conditional">
                            <ContentTemplate> 
       

    <%--------------------- Avisos en pantalla-------------------------------%>
                        <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-success alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                    <h4><asp:Label ID="LblMensajeAviso" runat="server" Text=""></asp:Label></h4>
                  	                </div>
                                </div>
                            </div>
                        </asp:Panel>
            
                         <asp:Panel ID="PanelDesicion" runat="server" Visible="false">
                    <div class="alert alert-info" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                       <p> <i class="icon fa fa-hand-o-right fa-2x"><asp:Label ID="LblMostarDecision" runat="server" Text="Label" style ="font-family: 'Arya', sans-serif; font-size :30px"></asp:Label></i></p>
                        <p>
                            <asp:Button ID="BtnSiGFac" CssClass="btn btn-success" runat="server" Text="Si" Width="109px" />                          
                            <asp:Button ID="BtnNoGFac" runat="server" Text="No" CssClass="btn btn-danger" Width="109px"/>
                        </p>
                    </div>    
                         </asp:Panel>

                        <asp:Panel ID="PanelCritico" runat="server" Visible="false">  
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-danger alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4><asp:Label ID="LblMensajeCritico" runat="server" Text=""></asp:Label>></h4>
                                    </div>
                                </div>
                            </div>                            
                        </asp:Panel>
                        
                        <asp:Panel ID="PanelAdvertencia" runat="server" Visible="false">
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-warning alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4><asp:Label ID="LblMensajeAdvertencia" runat="server" Text="Label" ></asp:Label></h4>
                                    </div>
                                </div>
                            </div> 
                        </asp:Panel>
                       
                <%--    ---------------------------------------------------------------------------------------------%>
    <!--HACK-->
    <img src="dist/img/hack.jpg" onload="Hack()" class="hidden"/>
        <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>
            Facturas
            <small>Empresa</small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="FacturacionClientes.aspx"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active"><i class="fa fa-file-text-o"></i> Facturas</li>
          </ol>
        </section>

        <section class="content">
            <div class="row">
          	    <div class="col-md-12">
          		    <div class="box box-info">
                        <div class="box-header with-border">
                              <h3 class="box-title">Facturas</h3>
                        </div>
                        <div class="box-body">
                            <div class="row">
                                    <div class="col-lg-5 col-md-5 col-sm-12 col-xs-12"  id="div_3" runat="server">
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
                                    <div class="col-lg-5 col-md-5 col-sm-6 col-xs-12" id="div_1" runat="server">
                                        <div class="form-group">
                                            <label>Facturas:</label>
                                            <select class="form-control input-lg" id="tipo_factura" runat="server">
                                                <option value="1">Facturas generadas</option>
                                                <option value="2">Facturas vigentes</option>
				                                <option value="3">Facturas canceladas</option>
                                            </select>
                                        </div>
                                    </div>
                                    <div class="col-lg-2 col-md-2 col-sm-6 col-xs-12"  id="div_2" runat="server">
                                        <div class="form-group">
                                            <label></label>
                                            <button type="button" class="btn btn-flat btn-info btn-block btn-lg" runat="server" onserverclick="cargaFacturas">
                                                <i class="fa fa-search"></i> Buscar
                                            </button>
                                        </div>
                                    </div>
                                </div>
                                <hr />
                            <div class="table-responsive">
                                    <asp:GridView ID="GdFacturas" runat="server" CssClass="table table-bordered table-striped ListaFacturas" AutoGenerateColumns="False">
                                        <Columns>
                                            <asp:BoundField AccessibleHeaderText="IDFactura" DataField="idFactura" HeaderText="ID"  />
                                            <asp:BoundField AccessibleHeaderText="Serie" DataField="Serie" HeaderText="Serie"  />
                                            <asp:BoundField AccessibleHeaderText="Folio" DataField="num_factura" HeaderText="Folio" />
                                            <asp:BoundField AccessibleHeaderText="Folio" DataField="Factura" HeaderText="Factura" />
                                            <asp:BoundField AccessibleHeaderText="RFC" DataField="rfc" HeaderText="RFC" />
                                            <asp:BoundField AccessibleHeaderText="Total" DataField="importe" HeaderText="Total" />
                                            <asp:BoundField AccessibleHeaderText="Alias" DataField="paciente" HeaderText="Paciente" />
                                            <asp:BoundField AccessibleHeaderText="Razón Social" DataField="nombre" HeaderText="Razón Social" />
                                            <asp:BoundField AccessibleHeaderText="Fecha" DataField="fecha" HeaderText="Fecha" />
                                            <asp:BoundField AccessibleHeaderText="Facturó" DataField="version" HeaderText="Version" HtmlEncodeFormatString="False" />
                                            <asp:TemplateField HeaderText="Cancelar">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="cancelar"  CommandName="cancelar" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CssClass="btn btn-danger btn-block">Cancelar <i  class="fa fa-close"></i></asp:LinkButton>
                                                    <asp:LinkButton runat="server" ID="Verificar"  CommandName="Verificar" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CssClass="btn btn-success btn-block">Verificar<i  class="fa fa-check-square-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Enviar">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="enviar" CommandName="enviar"  CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        class="btn btn-info"><i class="fa fa-envelope"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="PDF">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="pdf" CommandName="pdf" PostBackUrl="~/facturasclientes.aspx" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CssClass="btn btn-primary"><i  class="fa fa-file-pdf-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="XML">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="xml" CommandName="xml" PostBackUrl="~/facturasclientes.aspx" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                        CssClass="btn btn-warning"><i  class="fa fa-file-text-o"></i></asp:LinkButton>
                                                </ItemTemplate>
                                                <ItemStyle Wrap="True"></ItemStyle>
                                            </asp:TemplateField>
                                            <asp:BoundField AccessibleHeaderText="CodigoCliente" DataField="idcliente" HeaderText="IdCliente" />
                                            <asp:BoundField AccessibleHeaderText="FolioConsulta" DataField="idcosto" HeaderText="IdCosto" />
                                           <%--  <asp:BoundField AccessibleHeaderText="Facturado" DataField="Facturado" HeaderText="Facturado" />--%>
                                        </Columns>
                                    </asp:GridView> 
                            <asp:HiddenField ID="Hdgfacturas" runat="server" />
                <asp:HiddenField ID="HdIndex" runat="server" />
                                <asp:HiddenField ID="HFolioF" runat="server" />
                                <asp:HiddenField ID="HdTotal" runat="server" />
                                </div>
                        </div>
                    </div>
           	    </div>
            </div>
        </section>

     
            <!--Modal Correo -->
            <div id="correo_facturacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Confirmar correo</h4>
                        </div>
                        <asp:UpdatePanel runat="server" UpdateMode ="Conditional">
                            <ContentTemplate>
                                <div class="modal-body">
                                    <div class="form-body">
                                        <div class="form-group">
                                            <label class="col-md-3">Correo</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Correo" id="correo_confirmar" runat="server">
                                            </div>
                                        </div>
                                    </div>
                                    <div class="modal-footer">
                                        <input type="hidden" id="folio_factura" runat="server">
                                        <input type="hidden" id="serie_factura" runat="server">
                                        <input type="hidden" id="estado_factura" runat="server">
                                        <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" onserverclick="enviarFac"><i class="fa fa-envelope"></i> Enviar</button>
                                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
                                    </div>
                                </div>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

            <!-- Modal Correo -->

            <!--Modal Facturar -->
            <div id="datos_facturacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Datos de Facturación</h4>
                        </div>                      
                        <asp:UpdatePanel  runat="server">
                            <ContentTemplate>     
                                <div class="modal-body">
                                    <div class="form-body">
                                        <div class="form-group" id="div_select_razon_social" runat="server">
                                            <label class="col-md-3">Razón Social</label>
                                            <div class="input-group col-md-8">
                                                <asp:DropDownList ID="select_razon_social" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="CambioSelectRazonSocial"></asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="form-group" id="div_input_razon_social" runat="server">
                                            <label class="col-md-3">Razón Social</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Razón Social" id="input_razon_social" name="input_razon_social" runat="server">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">RFC</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="RFC" id="rfc" runat="server" >
                                            </div>
                                        <asp:RequiredFieldValidator ID="rfRFC" runat="server" ControlToValidate="RFC" Display="Dynamic" SetFocusOnError="True" ValidationGroup="datosFac"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Correo</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Correo" id="correo" runat="server">
                                            </div>
                                        <asp:RegularExpressionValidator ID="rfEmail" runat="server" ControlToValidate="correo"
                                Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="datosFac"><span class="mensaje-error">Por favor, escribe una direcci{on de correo válida</span></asp:RegularExpressionValidator>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Dirección</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Dirección" id="direccion" runat="server">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Código Postal</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                                <input type="number" class="form-control" placeholder="Código Postal" id="cp" runat="server">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Ciudad</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Ciudad" id="ciudad" runat="server">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">Estado</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="Estado" id="estado" runat="server">
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3">País</label>
                                            <div class="input-group col-md-8">
                                                <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                                                <input type="text" class="form-control" placeholder="País" id="pais" runat="server">
                                            </div>
                                        </div>
                                         <div class="form-group">
                               <label class="col-md-3">Consultas pendientes por facturar</label>
                               <div class="input-group col-md-8">
                                   <asp:GridView ID="GdConsultasXpagar" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                       <Columns>
                                           <asp:BoundField DataField="folioConsulta" HeaderText="Folio Consulta" />
                                           <asp:BoundField DataField="FechaAgenda" HeaderText="Fecha" />
                                           <asp:BoundField DataField="ImportePago" HeaderText="Importe" />
                                           <asp:TemplateField HeaderText="Agregar">
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chk" runat="server" />
                                                   <asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                   <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>                                   
                               </div>
                           </div>
                                        <div class="form-group">
                               <label class="col-md-3">Observaciones</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Observaciones de la Factura" id="observacionesf" runat="server">
                               </div>
                           </div>
                                    </div>
                                    <div class="modal-footer">
                                        <input type="hidden" id="codigo_paciente" runat="server">
                                        <input type="hidden" id="id_razon_social" runat="server">
                                        <input type="hidden" id="folio_consulta" runat="server">
                                        <input type="hidden" id="bandera" runat="server">                                     
                                        <button type="button" class="btn btn-success" data-dismiss="modal" runat="server"  onserverclick="Facturar" causesvalidation="true" validationgroup="datosFac"><i class="fa fa-save"></i> Facturar</button>
                                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
                                   <asp:HiddenField runat="server" ID="fconsulta" />
                                         </div>
                                </div>                        
                       </ContentTemplate></asp:UpdatePanel> 
                    </div>
                </div>
            </div>
         
      
            
   <asp:HiddenField ID="HD_IdFactura" runat="server" />
   </ContentTemplate>
                        </asp:UpdatePanel>

</asp:Content>
