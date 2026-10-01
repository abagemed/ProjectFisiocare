<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="DatosPaciente.aspx.vb" Inherits="AgeMED.DatosPaciente" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 34%;
        }

        .auto-style2 {
            height: 0.6em;
        }

        .auto-style3 {
            height: 0.4em;
        }

        input[type="text"], textarea {
            background-color: #faffbd;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="dist/css/registro.css" rel="stylesheet" />

    <script type="text/javascript">
        //$(document).ready(function () {
        //    $('#TxtTelefonoContacto').mask('(000) 000-00-00');
        //});

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
            patron = /[A-Za-zñÑ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }

        function ValSoloTelLocal(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }

        function ValSoloTelCelular(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }

        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z Ññ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValSoloLetrasEspacioBlancoNumero(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z Ññ0-9]/;
            te = String.fromCharCode(tecla); 
            return patron.test(te);
        }


        window.onload = function () {
            $(".sel_estado").select2({
                allowClear: true,
                placeholder: "Estado",
                maximunSelectSize: 10
            });
            $(".sel_municipio").select2({
                allowClear: true,
                placeholder: "Municipio",
                maximunSelectSize: 10
            });
            $(".sel_colonia").select2({
                allowClear: true,
                placeholder: "Colonia",
                maximunSelectSize: 10
            });
            $(".sel_nacionalidad").select2({
                allowClear: true,
                placeholder: "Nacionalidad",
                maximunSelectSize: 10
            });
            $(".sel_Aseguradora").select2({
                allowClear: true,
                placeholder: "Aseguradora",
                maximunSelectSize: 10
            });
            $(".sel_lugar_nacimiento").select2({
                allowClear: true,
                placeholder: "Colonia",
                maximunSelectSize: 10
            });
            $('.form-control').keydown(function (e) {
                if (e.which === 13) {
                    var index = $('.form-control').index(this) + 1;
                    $('.form-control').eq(index).focus();
                }
            });
        }

        function Hack() {
            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }
            $(".sel_estado").select2({
                allowClear: true,
                placeholder: "Estado",
                maximunSelectSize: 10
            });
            $(".sel_municipio").select2({
                allowClear: true,
                placeholder: "Municipio",
                maximunSelectSize: 10
            });
            $(".sel_colonia").select2({
                allowClear: true,
                placeholder: "Colonia",
                maximunSelectSize: 10
            });
            $(".sel_nacionalidad").select2({
                allowClear: true,
                placeholder: "Nacionalidad",
                maximunSelectSize: 10
            });
            $(".sel_Aseguradora").select2({
                allowClear: true,
                placeholder: "Aseguradora",
                maximunSelectSize: 10
            });
            $(".sel_lugar_nacimiento").select2({
                allowClear: true,
                placeholder: "Colonia",
                maximunSelectSize: 10
            });
            $('.form-control').keydown(function (e) {
                if (e.which === 13) {
                    var index = $('.form-control').index(this) + 1;
                    $('.form-control').eq(index).focus();
                }
            });
        }

    </script>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
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
            <!--HACK-->
            <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />
            <%--    ---------------------------------------------------------------------------------------------%>
            <section class="content-header">
                <h1 runat="server" id="paciente_titulo1">Paciente
           
                    <small>Nuevo</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="Agenda.aspx"><i class="fa fa-home"></i>Inicio</a></li>
                    <li class="active" runat="server" id="paciente_titulo2"><i class="fa fa-user-plus"></i>Nuevo Paciente</li>
                </ol>
            </section>


            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <section class="content">
                        <div class="row">
                            <div class="col-md-12">
                                <form id="pacienteForm" class="pacienteForm">
                                    <div class="box box-info">
                                        <div class="box-header with-border">
                                            <h3 class="box-title" runat="server" id="paciente_titulo3">Paciente Nuevo</h3>
                                        </div>

                                        <div class="box-body">
                                            <!--NOMBRE-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                            <input id="TxtPapellido" type="text" runat="server" class="form-control" style="text-transform: uppercase;"
                                                                maxlength="25" onkeypress="return ValSoloLetrasEspacioBlanco(event)" autocomplete="on" placeholder="Primer Apellido" />
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfPaterno" runat="server" ControlToValidate="TxtPapellido" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                            <input id="TxtSapellido" type="text" runat="server" class="form-control" style="text-transform: uppercase;" maxlength="25" onkeypress="return ValSoloLetrasEspacioBlanco(event)" placeholder="Segundo Apellido" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                            <input id="txtNombres" type="text" runat="server" class="form-control" style="text-transform: uppercase;" maxlength="30" onkeypress="return ValSoloLetrasEspacioBlanco(event)" placeholder="Nombre(s)" />
                                                        </div>
                                                        <asp:RequiredFieldValidator ID="rfNombre" runat="server" ControlToValidate="txtNombres" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gDatos"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr>
                                            <!--NACIMIENTO-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span>Fecha de Nacimiento</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12 date">
                                                            <span class="input-group-addon"><i class="fa fa-calendar fa-fw"></i></span>
                                                            <asp:DropDownList ID="DDdiasfecha" class="form-control" Style="padding: 6px 8px;" runat="server" Width="60px"></asp:DropDownList>
                                                            <asp:DropDownList ID="DDmesesFecha" class="form-control" Style="padding: 6px 8px;" runat="server" Width="71px"></asp:DropDownList>
                                                            <asp:DropDownList ID="DDañosFecha" class="form-control" Style="padding: 6px 8px;" runat="server" Width="70px"></asp:DropDownList>
                                                            <!--<input class="form-control" type="text" name="fecha" id="fecha" placeholder="Fecha de nacimiento" readonly>-->
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span>Nacionalidad</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDNacionalidad" class="form-control sel_nacionalidad" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span runat="server" id="lugar_nac_txt">Lugar de nacimiento</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDEdoNacimiento" class="form-control sel_lugar_nacimiento" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr>

                                            <!--DATOS GENERALES-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:RadioButton ID="rbhombre" runat="server" GroupName="Genero" Text="HOMBRE " Checked="True" />
                                                            <asp:RadioButton ID="rbrdmujer" runat="server" GroupName="Genero" Text="MUJER" Style="margin-left: 1em;" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-barcode"></i></span>
                                                            <input id="Txtcurp" type="text" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)" style="text-transform: uppercase;" placeholder="CURP" maxlength="18" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr>

                                            <!--DATOS GENERALES-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                                                            <asp:TextBox ID="txtDireccion" class="form-control" runat="server" Font-Names="Calibri" onkeydown="return (event.keyCode!=13)" MaxLength="150" Rows="2" placeholder="Dirección"></asp:TextBox>
                                                        </div>                                                     

                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                                            <asp:TextBox ID="Txtcodigopostal" class="form-control" runat="server" onkeypress="return ValSoloNumeros(event)" onkeydown="return (event.keyCode!=13)" MaxLength="5" placeholder="Código Postal"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDestadosoficial" class="form-control sel_estado" runat="server" AutoPostBack="True"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDmunicipios" class="form-control sel_municipio" runat="server" AutoPostBack="True"></asp:DropDownList></td>
                              	
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDlocalidades" class="form-control sel_colonia" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>


                                            </div>
                                            <hr>

                                            <!--ESTADO CIVIL-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span>Estado Civil</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDEstadoCivil" class="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span>Ocupación</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDocupacion" class="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <span>Tipo de Paciente</span>
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <asp:DropDownList ID="DDTipoPaciente" class="form-control" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr>

                                            <!--TELEFONO-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-phone fa-fw"></i></span>
                                                            <input id="txtTelefonoFijo" type="text" runat="server" class="form-control" maxlength="10" onkeypress="return ValSoloTelLocal(event)" placeholder="Teléfono" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-mobile fa-fw"></i></span>
                                                            <input id="txtTelefonoMovil" type="text" runat="server" class="form-control" maxlength="10" onkeypress="return ValSoloTelCelular(event)" placeholder="Teléfono celular" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                                            <asp:TextBox ID="txtEmail" runat="server" class="form-control" onkeydown="return (event.keyCode!=13)" MaxLength="50" placeholder="Correo electrónico"></asp:TextBox></td>
                                 
                                                        </div>
                                                        <asp:RegularExpressionValidator ID="rfEmail" runat="server" ControlToValidate="txtEmail"
                                                            Display="Dynamic" ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="gDatos"><span class="mensaje-error">Por favor, escribe una direcci{on de correo válida</span></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                            <hr>


                                            <!--FAMILIAR-->
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-user fa-fw"></i></span>
                                                            <input id="TxtNombreContacto" type="text" runat="server" class="form-control" style="text-transform: uppercase; font-family: Calibri;"
                                                                maxlength="50" onkeypress="return ValSoloLetrasEspacioBlanco(event)" placeholder="Persona de contacto" />
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group">
                                                        <div class="input-group col-md-12">
                                                            <span class="input-group-addon"><i class="fa fa-phone fa-fw"></i></span>
                                                            <input id="TxtTelefonoContacto" type="text" runat="server" class="form-control" style="font-family: Calibri;"
                                                                maxlength="10" onkeypress="return ValSoloTelCelular(event)" placeholder="Teléfono" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-xs-12 col-sm-12 col-md-8 col-lg-8 ">
                                                    <div class="form-group ">
                                                        <div class="input-group  col-md-12 ">
                                                            <span class="input-group-addon"><i class="fa fa fa-yelp fa-fw"></i></span>
                                                            <asp:DropDownList ID="DDaseguradora" class="form-control sel_Aseguradora" runat="server"></asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                                    <div class="form-group ">
                                                        <div class="input-group  col-md-12 ">
                                                            <span class="input-group-addon"><i class="fa fa-pencil fa-fw"></i></span>
                                                            <asp:TextBox ID="TxtAnotaciones" runat="server" CssClass="form-control" MaxLength="45" placeholder="Anotaciones" onkeypress="return ValSoloLetrasEspacioBlancoNumero(event)"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>

                                            </div>
                                            <div class="row">
                                                <div class="col-lg-8 col-md-8 col-sm-6 col-xs-6">
                                                    <button type="button" class="btn btn-success btn-lg btn-block" runat="server" onserverclick="BtnGuardar_Click" causesvalidation="true" validationgroup="gDatos"><i class="fa fa-save"></i><span runat="server" id="BotonGuardar">Guardar</span></button>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-6 col-xs-6">
                                                    <button type="button" class="btn btn-default btn-lg btn-block" runat="server" onserverclick="BtnCancelar_Click" ><i class="fa fa-close"></i>Cancelar</button>
                                                </div>
                                            </div>


                                        </div>
                                    </div>

                                </form>
                            </div>
                        </div>
                    </section>
                </ContentTemplate>
            </asp:UpdatePanel>

            <asp:HiddenField ID="HdCodigoPaciente" runat="server" />
            <asp:HiddenField ID="HdPregunta" runat="server" />
            <asp:HiddenField ID="HdModalidad" runat="server" />
            <asp:HiddenField ID="HdCodigoCliente" runat="server" />
            <asp:HiddenField ID="HdCodigoTipoPaciente" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
