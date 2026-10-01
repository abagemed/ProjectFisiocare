<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Diagnostico.aspx.vb" Inherits="AgeMED.Diagnostico" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Diagnósticos
            <small>Agregar, modificar, eliminar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active"><i class="fa fa-stethoscope"></i> Diagnósticos</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Diagnósticos</h3>
                    </div>
                    
                    <div class="box-body">
                        <div style="margin:10px 0px">
                            <button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#alta">
                                Agregar diagnósticos <i class="fa fa-plus"></i>
                            </button>
                        </div>
                        
                        <table runat="server" id="diagnosticos" class="table table-bordered table-striped">
                            <thead>
                                <tr>
                                    <th>Nombre</th>
                                    <th>Editar <i class="fa fa-pencil"></i></th>
                                    <th>Eliminar <i class=" fa fa-trash"></i></th>
                                </tr>
                            </thead>
                            <tbody>
                                <tr>
                                    <td>TORAX INESTEABLE</td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-success" data-toggle="modal" data-target="#editar_1">
                                            Editar <i class="fa fa-pencil"></i>
                                        </button>
                                    </td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-danger" data-toggle="modal" data-target="#eliminar_1">
                                            Eliminar <i class="fa fa-trash"></i>
                                        </button>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>PENE ENORME</td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-success" data-toggle="modal" data-target="#editar_1">
                                            Editar <i class="fa fa-pencil"></i>
                                        </button>
                                    </td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-danger" data-toggle="modal" data-target="#eliminar_1">
                                            Eliminar <i class="fa fa-trash"></i>
                                        </button>
                                    </td>
                                </tr>
                                 <tr>
                                    <td>FRACTURA PENEAL</td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-success" data-toggle="modal" data-target="#editar_1">
                                            Editar <i class="fa fa-pencil"></i>
                                        </button>
                                    </td>
                                    <td>
                                        <button type="button" class="btn btn-block btn-danger" data-toggle="modal" data-target="#eliminar_1">
                                            Eliminar <i class="fa fa-trash"></i>
                                        </button>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
            </div>
        </div>
    </section>



   <!--Modal Alta -->
   <div id="alta" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Nuevo Diágnostico</h4>
                </div>
                <div class="modal-body">
                    <div class="form-body">
                        <div class="form-group">
                            <label class="col-md-2">Nombre</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <input type="text" class="form-control" id="nombre" placeholder="Nombre" name="nombre">
                            </div>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <input type="hidden" name="alta" value="1">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" onclick="submit()">Guardar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!--Modal Alta Usuarios-->
   <div id="editar_1" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Modificar diagnóstico</h4>
                </div>
                <div class="modal-body">
                    <div class="form-body">

                        <div class="form-group">
                            <label class="col-md-2">Nombre</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <input type="text" class="form-control" placeholder="Nombre" name="nombre">
                            </div>
                        </div>
                        
                    </div>
                    <div class="modal-footer">
                        <input type="hidden" name="alta" value="1">
                        <button type="submit" class="btn btn-success" data-dismiss="modal" onclick="submit()">Guardar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <!--Modal Alta Usuarios-->
   <div id="eliminar_1" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Eliminar diagnóstico</h4>
                </div>
                <div class="modal-body">
                    <div class="form-body">
                        <div class="form-group">
                            <label class="col-md-2">Nombre</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <input type="text" class="form-control"  placeholder="Nombre" name="nombre">
                            </div>
                        </div>

                    </div>
                    <div class="modal-footer">
                        <input type="hidden" name="alta" value="1">
                        <button type="submit" class="btn btn-danger" data-dismiss="modal" onclick="submit()">Eliminar</button>
                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>
                </div>

            </div>
        </div>
    </div>
    

    <script>
        
        window.onload = function () {
            
            $(document.getElementById('<%= diagnosticos.ClientID%>')).DataTable({
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
                "ordering": true,
            });

           


        }
</script>
</asp:Content>
