<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Usuarios.aspx.vb" Inherits="AgeMED.Usuarios" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script >
         function ValSoloLetrasEspacioBlanco(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             if (tecla == 8) return true;
             patron = /[A-Za-z Ññ]/;
             te = String.fromCharCode(tecla);
             return patron.test(te);
         }
         function ValSoloLetrasEspacioBlancoNumeros(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             if (tecla == 8) return true;
             patron = /[A-Za-z Ññ0-9]/;
             te = String.fromCharCode(tecla);
             return patron.test(te);
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Usuarios
            <small>Agregar, modificar, eliminar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-users"></i>Usuarios</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Usuarios</h3>
                    </div>
                    <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                        <ContentTemplate>
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
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-info alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi_Click"><i class="fa fa-check"></i>Sí</button>
                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo_Click"><i class="fa fa-close"></i>No</button>
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
                                                    <asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label>></h4>
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
                                <%--    ---------------------------------------------------------------------------------------------%>
                                
                                    <!--HACK-->
                                    <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />
                                    <!----------------------------------->
                                
                                
                                <div style="margin: 10px 0px">
                                    <button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#Alta">
                                        Agregar usuario <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                                <asp:GridView ID="GdUsuarios" runat="server" class="table table-bordered table-striped" AutoGenerateColumns="False">
                                    <Columns>
                                        <asp:BoundField DataField="codigoUsuario" HeaderText="Código Usuario" >
                                        <ControlStyle CssClass="hidden-xs hidden-sm" />
                                        <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Nombre" />
                                        <asp:BoundField DataField="login" HeaderText="Usuario" >
                                        <ControlStyle CssClass="hidden-xs " />
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="rol" HeaderText="Tipo de Cuenta" >
                                        <ControlStyle CssClass="hidden-xs hidden-sm" />
                                        <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Button" HeaderText="Editar" Text="Editar" CommandName="Editar" ControlStyle-CssClass="btn btn-block btn-success">
                                            <ControlStyle CssClass="btn btn-block btn-success"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField ButtonType="Button" HeaderText="Borrar" Text="Borrar" CommandName="Borrar" ControlStyle-CssClass="btn btn-block btn-danger">
                                            <ControlStyle CssClass="btn btn-block btn-danger hidden-xs "></ControlStyle>
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                        </asp:ButtonField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>
    </section>
    <!--Modal editar Usuario-->
    <div id="Editar_1" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Editar Usuario</h4>
                </div>
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                    <ContentTemplate>
                        <div class="modal-body">
                            <div class="form-body">
                                <asp:HiddenField ID="Hdpregunta" runat="server" />
                                <asp:HiddenField ID="HdUsuarioEdit" runat="server" />
                                <div class="form-group">
                                    <label class="col-md-4" for="codigo">Login</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                        <asp:TextBox class="form-control" ID="txtLogin" placeholder="Login" runat="server"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLogin" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Contraseña</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox class="form-control" runat="server" ID="txtPass" placeholder="******"  MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtPass" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Nombre</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox runat="server" class="form-control" ID="txtNombre" placeholder="Nombre" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtNombre" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Apellido Paterno</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox class="form-control" ID="txtPaterno" placeholder="Paterno" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtPaterno" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Apellido Materno</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox class="form-control" ID="txtMaterno" placeholder="Materno" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Especialidad</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox class="form-control" ID="txtEspecialidad" placeholder="Especialidad" runat="server" Style="text-transform: uppercase;" MaxLength="30" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEspecialidad" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Cedula</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                        <asp:TextBox class="form-control" ID="txtCedula" placeholder="Cedula" runat="server" Style="text-transform: uppercase;" MaxLength="25" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)"></asp:TextBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCedula" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Nivel de usuario</label>
                                    <div class="input-group col-md-8">
                                        <select class="form-control" id="rol1" name="nivel" runat="server">
                                            <option value="1">Radiologo</option>
                                            <option value="2">Recepcionista</option>
                                            <option value="3">Doctor</option>
                                        </select>

                                    </div>
                                </div>
                            </div>
                            <div class="modal-footer">
                                <input type="hidden" name="Editar" value="1">
                                <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" onserverclick="BtnGuardarEdit_Click" causesvalidation="true" validationgroup="gDatos">
                                    <i class="fa fa-save">Guardar</i>

                                </button>
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                            </div>
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
        </div>
    </div>

    <!--Modal Nuevo Usuario-->
    <div id="Alta" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Nuevo usuario</h4>
                </div>

                <div class="modal-body">

                    <div class="form-body">
                        <div class="form-group">
                            <label class="col-md-4" for="codigo">Login de usuario</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                <asp:TextBox ID="txtLogin2" runat="server" class="form-control" placeholder="Nombre de usuario" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtLogin2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Contraseña</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                <asp:TextBox class="form-control" placeholder="******" ID="txtPass2" runat="server"  MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)" ></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="txtPass2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Nombre</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <asp:TextBox class="form-control" ID="txtNombre2" placeholder="Nombre" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtNombre2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Apellido Paterno</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <asp:TextBox class="form-control" ID="txtPaterno2" placeholder="Paterno" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtPaterno2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Apellido Materno</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                <asp:TextBox class="form-control" ID="txtMaterno2" placeholder="Materno" runat="server" Style="text-transform: uppercase;" MaxLength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMaterno2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Especialidad</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                <asp:TextBox class="form-control" ID="txtEspecialidad2" placeholder="Especialidad" runat="server" Style="text-transform: uppercase;" MaxLength="30" onkeypress="return ValSoloLetrasEspacioBlanco(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtEspecialidad2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Cedula</label>
                            <div class="input-group col-md-8">
                                <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                <asp:TextBox class="form-control" ID="txtCedula2" placeholder="Cedula" runat="server" Style="text-transform: uppercase;" MaxLength="25" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtCedula2" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gNuevoDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="form-group">
                            <label class="col-md-4">Nivel de usuario</label>
                            <div class="input-group col-md-8">
                                <select class="form-control" name="nivel">
                                    <option value="1">Radiologo</option>
                                    <option value="2">Recepcionista</option>
                                    <option value="3">Doctor</option>
                                </select>

                            </div>
                        </div>
                    </div>

                    <div class="modal-footer">
                        <input type="hidden" name="Alta" value="1">
                        <asp:Button ID="btnGuardar2" runat="server" Text="Guardar" class="btn btn-success" CausesValidation="true" ValidationGroup="gNuevoDatos" />
                        <%--                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnGuardarNuevo_Click" causesvalidation="true" validationgroup="gDatos"><i class="fa fa-save"></i>Guardar</span></button>--%>
                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                    </div>

                </div>

            </div>
        </div>
    </div>

   
    <script>

        function Hack() {
            var grid = document.getElementById('<%= GdUsuarios.ClientID%>');
            var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
            var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

            tbody.removeChild(firstTr); //remove tr's from table

            var newTh = document.createElement('thead'); //creates thead
            newTh.appendChild(firstTr); //puts ths in thead
            grid.insertBefore(newTh, tbody); //puts thead behore tbody
            $(document.getElementById('<%= GdUsuarios.ClientID%>')).DataTable({
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

      
        window.onload = function () {
           
            var grid = document.getElementById('<%= GdUsuarios.ClientID%>');
            var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
            var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

            tbody.removeChild(firstTr); //remove tr's from table

            var newTh = document.createElement('thead'); //creates thead
            newTh.appendChild(firstTr); //puts ths in thead
            grid.insertBefore(newTh, tbody); //puts thead behore tbody
            $(document.getElementById('<%= GdUsuarios.ClientID%>')).DataTable({
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

            function GetTable() {
                var array = [];
                var headers = [];
                var data = [];
                $('#<%= GdUsuarios.ClientID%> th').each(function (index, item) {
                    headers[index] = $(item).text();
                });
                data.push(headers);
                $('#<%= GdUsuarios.ClientID%> tr').has('td').each(function () {
                    var arrayItem = [];
                    $('td', $(this)).each(function (index, item) {
                        if (index != 0)
                        {
                            arrayItem[index] = parseInt($(item).text(),10);
                        }
                        else
                        {
                            arrayItem[index] = $(item).text();
                        }
                        
                    });
                    data.push(arrayItem);
                });
                console.log(data);
                return data;
            }
          
        }
</script>




</asp:Content>
