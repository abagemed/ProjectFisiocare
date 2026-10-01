<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="facturas_datos.aspx.vb" Inherits="AgeMED.facturas_datos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script>
        window.onload = function () {
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
            $('#datos_facturacion').modal('show');
        }
        function Hack() {
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
    <!--HACK-->
    <img src="dist/img/hack.jpg" onload="Hack()" class="hidden"/>
        <!-- Content Header (Page header) -->
        <section class="content-header">
          <h1>
            Facturas
            <small>Datos de facturación</small>
          </h1>
          <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active">Datos de facturación</li>
          </ol>
        </section>

        <section class="content">
          <div class="row">
          	<div class="col-md-12">
          		<div class="box box-info">
                    <div class="box-header">
                        <h1 class="text-center">Datos de facturación</h1>
                        <button type="button" class="btn btn-block btn-success btn-flat" data-toggle="modal" data-target="#alta">
                        Agregar <i class="fa fa-plus"></i>
                        </button>

                    </div>
                    <div class="box-body">
                        <div class="table-responsive">
                            <table id="ListaFacturas" class="table table-bordered table-striped ListaFacturas">
                                <thead>
                                    <tr>
                                        <th class="hidden-sm hidden-xs">RFC</th>
                                        <th>Nombre</th>
                                        <th></th>
                                    </tr>
                                </thead>
                                <tbody>
                                    <tr>
                                        <td class="hidden-sm hidden-xs">SOCC8104243X8</td>
                                        <td>Christhian Froilan Sosa Cebalos</td>
                                        <td>
                                            <button type="button" class="btn btn-block btn-success btn-flat">
                                                Editar <i class="fa fa-pencil"></i>
                                            </button>
                                        </td>                                   
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
              </div>
           	</div>
          </div>
        </section>

    <!--Modal Alta -->
   <div id="alta" class="modal fade" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog">
       <div class="modal-content">
          <div class="modal-header">
             <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
             <h4 class="modal-title">Nuevo Registro</h4>
          </div>
           <div class="modal-body">
               <div class="form-body">
                   <div class="form-group">
                       <label class="col-md-3">Razón Social</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Razón Social" id="razon_social_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">RFC</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="RFC" id="rfc_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Correo</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Correo" id="correo_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Dirección</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Dirección" id="direccion_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Código Postal</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                           <input type="number" class="form-control" placeholder="Código Postal" id="cp_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Ciudad</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Ciudad" id="ciudad_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Municipio</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Municipio" id="municipio_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Estado</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Estado" id="estado_alta" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">País</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="País" id="pais_alta" runat="server">
                       </div>
                   </div>
               </div>
               <div class="modal-footer">
                   <input type="hidden" name="alta" value="1">
                   <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" onserverclick="AltaRazonSocial"><i class="fa fa-save"></i> Guardar</button>
                   <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
               </div>
           </div>
          
       </div>
    </div>
   </div>

    <!--Modal Facturar -->
   <div id="datos_facturacion" class="modal fade" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel">
    <div class="modal-dialog">
       <div class="modal-content">
          <div class="modal-header">
             <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
             <h4 class="modal-title">Datos de Facturación</h4>
          </div>
           <div class="modal-body">
               <div class="form-body">
                   <div class="form-group">
                       <label class="col-md-3">Razón Social</label>
                       <div class="input-group col-md-8">  
                           <asp:DropDownList ID="select_razon_social" class="form-control" runat="server" AutoPostBack="True"></asp:DropDownList>
                           <span class="input-group-addon" id="box_razon" runat="server"><i class="fa fa-font fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Razón Social" name="nombre" id="input_razon_social" runat="server">
                       </div>
                   </div>
                   <!--div class="form-group">
                       <label class="col-md-3">Razón Social</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Razón Social" name="nombre" id="nombre" runat="server">
                       </div>
                   </div-->
                   <div class="form-group">
                       <label class="col-md-3">RFC</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="RFC" name="rfc" id="rfc" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Correo</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Correo" name="correo" id="correo" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Dirección</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Dirección" name="direccion" id="direccion" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Código Postal</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                           <input type="number" class="form-control" placeholder="Código Postal" name="cp" id="cp" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Ciudad</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Ciudad" name="ciudad" id="ciudad" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Municipio</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Municipio" name="municipio" id="municipio" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">Estado</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="Estado" name="estado" id="estado" runat="server">
                       </div>
                   </div>
                   <div class="form-group">
                       <label class="col-md-3">País</label>
                       <div class="input-group col-md-8">
                           <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                           <input type="text" class="form-control" placeholder="País" name="pais" id="pais" runat="server">
                       </div>
                   </div>
               </div>
               <div class="modal-footer">
                   <input type="hidden" name="alta" value="1">
                   <button type="button" class="btn btn-success" data-dismiss="modal"><i class="fa fa-save"></i> Facturar</button>
                   <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
               </div>
           </div>
          
       </div>
    </div>
   </div>



</asp:Content>
