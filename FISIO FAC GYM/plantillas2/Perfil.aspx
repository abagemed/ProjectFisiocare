<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Perfil.aspx.vb" Inherits="AgeMED.Perfil" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z Ññ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Perfil
            <small>Modificar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-user"></i>Perfil</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Perfil</h3>
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
                        <div class="row">
                            <div class="form-group">
                                <label class="col-md-2" for="codigo">Usuario</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                    <input type="text" class="form-control" id="nombre_usuario" placeholder="Nombre usuario" name="nombre_usuario" readonly runat="server">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2" for="codigo">Nombre</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                    <input type="text" class="form-control" id="nombre" placeholder="Nombre" name="nombre" runat="server" style="text-transform: uppercase;" maxlength="15" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2" for="codigo">Primer apellido</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                    <input type="text" class="form-control" id="pApellido" placeholder="Primer apellido" name="PrimerApellido" runat="server" style="text-transform: uppercase;" maxlength="25" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2" for="codigo">Segundo Apellido</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                    <input type="text" class="form-control" id="sApellido" placeholder="Segundo apellido" name="SegundoApellido" runat="server" style="text-transform: uppercase;" maxlength="25" onkeypress="return ValSoloLetrasEspacioBlanco(event)">
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-md-2" for="codigo">Contraseña</label>
                                <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                    <input type="text" class="form-control" id="pass1" placeholder="****" name="pass1" runat="server" maxlength="20" style ="background-color :LemonChiffon">
                                </div>
                                </div>
                             <div class="form-group">
                                 <label class="col-md-2" for="codigo">Repetir Contraseña</label>
                                 <div class="input-group col-md-8">
                                    <span class="input-group-addon"><i class="fa fa-lock"></i></span>
                                    <input type="text" class="form-control" id="Pass2" placeholder="****" name="pass2" runat="server" maxlength="20"  style ="background-color :LemonChiffon">
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <input type="hidden" name="editar" value="1">
                            <button type="submit" class="btn btn-success" runat="server" onserverclick="BtnGuardar_Click">Guardar</button>
                            <button type="button" class="btn btn-default" runat="server" onserverclick="BtnCancelar_Click">Cancelar</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
