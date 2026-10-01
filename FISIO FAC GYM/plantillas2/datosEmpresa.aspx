<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="datosEmpresa.aspx.vb" Inherits="AgeMED.datosEmpresa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="plugins/bootstrap-fileupload/bootstrap-fileupload.css" rel="stylesheet" />
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
        <h1>Datos de la empresa
            <small>Modificar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active"><i class="fa fa-building"></i> Empresa</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Empresa</h3>
                    </div>
                    <div class="box-body">
                         <%--------------------- Avisos en pantalla-------------------------------%>
                        <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-success alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                    <h4><asp:Label ID="LblMensajeAviso" runat="server"></asp:Label></h4>
                  	                </div>
                                </div>
                            </div>
                        </asp:Panel>            
<%--                         <asp:Panel ID="PanelDesicion" runat="server" Visible="false">
                             <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-info alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4><asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                        <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi_Click"><i class="fa fa-check"></i> Sí</button>
                                        <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo_Click"><i class="fa fa-close"></i> No</button>
                                    </div>
                                </div>
                            </div>
                         </asp:Panel>--%>

                        <asp:Panel ID="PanelCritico" runat="server" Visible="false">  
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-danger alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                        <h4><asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label>></h4>
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
                        <div class="form-body">
                            <div class="form-group">
                                <label class="col-md-3">Empresa</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-building"></i></span>
                                    <input type="text" class="form-control" id="empresa" placeholder="Empresa"  runat ="server" style="text-transform :uppercase;" maxlength="30" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Razón social</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                    <input type="text" class="form-control" placeholder="Razón social" id="razonSocial"  runat ="server" style="text-transform :uppercase;" maxlength="30" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">RFC</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                    <input type="text" class="form-control" placeholder="RFC" id="rfc" runat ="server" style="text-transform :uppercase;" maxlength="15" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Dirección</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
                                    <input type="text" class="form-control" placeholder="Dirección" id="direccion"  runat ="server" style="text-transform :uppercase;" maxlength="40" onkeypress="return ValSoloLetrasEspacioBlancoNumeros(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Pais</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
                                    <input type="text" class="form-control" placeholder="Pais" id="pais"  runat ="server" style="text-transform :uppercase;" maxlength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Estado</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
                                    <input type="text" class="form-control" placeholder="Estado" id="estado" runat ="server" style="text-transform :uppercase;" maxlength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Ciudad</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-map-marker"></i></span>
                                    <input type="text" class="form-control" placeholder="Ciudad" id="ciudad"  runat ="server" style="text-transform :uppercase;" maxlength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>

                             
                            <%--<div class="form-group">
                                <label class="col-md-3">Teléfono</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-phone"></i></span>
                                    <input type="text" class="form-control" placeholder="(9999) 99-00-0">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Correo</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-envelope"></i></span>
                                    <input type="text" class="form-control" placeholder="correo@sitio.com">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-3">Página web</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-link"></i></span>
                                    <input type="text" class="form-control" placeholder="www.sitio.com">
                                </div>
                            </div>--%>

                            <div class="form-group">
                                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                    <div class="fileupload fileupload-new" data-provides="fileupload">
                                        <div class="fileupload-new thumbnail" style="width: 200px; height: 150px;">
                                            <img src="dist/img/logo.png" class="img-responsive" alt="">
                                        </div>
                                        <div class="fileupload-preview fileupload-exists thumbnail" style="max-width: 200px; max-height: 150px; line-height: 20px;"></div>
                                        <div>
                                            <span class="btn btn-primary btn-file">
                                                <span class="fileupload-new"><i class="fa fa-paperclip"></i> Seleccionar imagen</span>
                                                <span class="fileupload-exists"><i class="fa fa-undo"></i> Cambiar</span>
                                                <input type="file" class="default" name="imagen" id="logo" />
                                            </span>
                                            <a href="#" class="btn btn-danger fileupload-exists" data-dismiss="fileupload" onclick="reset_logo()"><i class="fa fa-trash"></i>Borrar</a>
                                        </div>
                                        <script>
                                            function reset_logo() {
                                                var output = [];
                                                output.push('<div class="alert alert-info"><i class="fa fa-check"></i> Formato de archivo correcto</div>');
                                                output.push('<div class="alert alert-info"><i class="fa fa-check"></i> Tamaño de archivo correcto</div>');
                                                document.getElementById('listlogo').innerHTML = output.join('');

                                            }
                                            function handleFileSelect(evt) {
                                                var files = evt.target.files; // FileList object
                                                // files is a FileList of File objects. List some properties.
                                                var output = [];
                                                for (var i = 0, f; f = files[i]; i++) {
                                                    if (f.type == "image/png") {
                                                        output.push('<div class="alert alert-info"><i class="fa fa-check"></i> Formato de archivo correcto</div>');
                                                    }
                                                    else {
                                                        output.push('<div class="alert alert-danger"><i class="fa fa-times"></i> Formato de archivo incorrecto</div>');
                                                    }
                                                    if (f.size < 5000000) {
                                                        output.push('<div class="alert alert-info"><i class="fa fa-check"></i> Tamaño de archivo correcto</div>');
                                                    }
                                                    else {
                                                        output.push('<div class="alert alert-danger"><i class="fa fa-times"></i> Tamaño de archivo incorrecto</div>');
                                                    }
                                                }
                                                if (i >= 1) {
                                                    document.getElementById('listlogo').innerHTML = output.join('');
                                                }
                                            }
                                            document.getElementById('logo').addEventListener('change', handleFileSelect, false);
                                         </script>
                                    </div>
                                </div>
                                <div class="col-lg-8 col-md-8 col-sm-12 col-xs-12">
                                    <div id="listlogo">
                                        <div class="alert alert-info">
                                            <i class="fa fa-exclamation-circle"></i>Formato de archivo: imagen png.
                                       
                                        </div>
                                        <div class="alert alert-info">
                                            <i class="fa fa-exclamation-circle"></i>Tamaño máximo de archivo: 5MB.
                                       
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div>
                            <button type="button" class="btn btn-success btn-block" runat="server" onserverclick="BtnGuardar_Click"><i class="fa fa-save"></i> Guardar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
    <script src="plugins/bootstrap-fileupload/bootstrap-fileupload.js"></script>
</asp:Content>
