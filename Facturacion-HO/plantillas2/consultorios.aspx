<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="consultorios.aspx.vb" Inherits="AgeMED.consultorios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            function ValSoloLetrasEspacioBlanco(e) {
                tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) return true;
                patron = /[A-Za-z Ññ]/;
                te = String.fromCharCode(tecla);
                return patron.test(te);
            }
            $(".estudios").select2({ placeholder: "Lista Diagnostico", maximumSelectionSize: 6 });
            $(".permisos").select2({ placeholder: "Lista permisos", maximumSelectionSize: 10 });
        }
    </script>
    <script type="text/javascript">
        function Hack() {
            if (window.location.pathname != "/consultorios.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }
            $(".estudios").select2({ placeholder: "Lista Diagnostico", maximumSelectionSize: 6 });
            $(".permisos").select2({ placeholder: "Lista permisos", maximumSelectionSize: 10 });

        }
    </script>
    <section class="content-header">
        <h1>Consultorios
            <small>Agregar, modificar, eliminar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-h-square"></i>Consultorios</li>
        </ol>
    </section>
    
    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Consultorios</h3>
                    </div>
                    
                    
                      <%--Avisos en pantalla--%>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
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
                                <div style="margin: 10px 0px">
                                    <button type="button" class="btn btn-block btn-primary" data-toggle="modal" data-target="#Alta" >
                                        Agregar consultorio <i class="fa fa-plus"></i>
                                    </button>
                                </div>
                                <asp:GridView ID="GdConsultorios" runat="server" AutoGenerateColumns="False" class="table table-striped table-bordered">
                                    <Columns>
                                        <asp:BoundField DataField="codigoConsultorio" HeaderText="Codigo Consultorio">
                                            <ControlStyle CssClass="hidden-xs hidden-sm hidden-md" />
                                            <HeaderStyle Wrap="True" CssClass="hidden-xs hidden-sm hidden-md" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm hidden-md" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="descripcionConsultorio" HeaderText="Descripcion" >
                                        <ControlStyle CssClass="hidden-xs" />
                                        <HeaderStyle CssClass="hidden-xs" />
                                        <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="nombre" HeaderText="Doctor de Cabecera" />
                                        <asp:BoundField DataField="idDoctorCabecera" HeaderText="Codigo Doctor Cabecera" >
                                        <ControlStyle CssClass="hidden-xs hidden-sm hidden-md" />
                                        <HeaderStyle CssClass="hidden-xs hidden-sm hidden-md" />
                                        <ItemStyle CssClass="hidden-xs hidden-sm hidden-md" />
                                        </asp:BoundField>
                                        <asp:ButtonField HeaderText="Editar" Text="Editar" CommandName="Editar" ControlStyle-CssClass="btn btn-block btn-success">
                                            <ControlStyle CssClass="btn btn-block btn-success"></ControlStyle>
                                        </asp:ButtonField>
                                        <asp:ButtonField HeaderText="Borrar" Text="Borrar" CommandName="Borrar" ControlStyle-CssClass="btn btn-block btn-danger">
                                            <ControlStyle CssClass="btn btn-block btn-danger hidden-xs"></ControlStyle>
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
    <%--Nuevo Consultorio--%>
    <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="Alta" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Nuevo consultorio</h4>
                        </div>
                        <div class="modal-body">
                            <div class="form-body">
                                <div class="form-group">
                                    <label class="col-md-4">Descripción</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="TxtAltaDescripcion" runat="server" class="form-control" onkeypress="return ValSoloLetrasEspacioBlanco(event)" placeholder="Descripción" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxtAltaDescripcion" Display="Dynamic" SetFocusOnError="True" ValidationGroup="gAlta"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>

                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Dr. de cabecera</label>
                                    <div class="input-group col-md-8">
                                        <asp:DropDownList ID="DdAltaDrCabecera" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Inicio de horario (Matutino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtAltaInicioHorarioM" runat="server" class="form-control" placeholder="8:00:00" Text="8:00:00" ReadOnly="true"></asp:TextBox>
                                        <%--<ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" runat="server" TargetControlID="TxtAltaInicioHorarioM" Mask="99:99" MaskType="Time" InputDirection="RightToLeft" />--%>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Final de horario (Matutino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtAltaFinHorarioM" runat="server" class="form-control" placeholder="13:00:00" Text="13:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Inicio de horario (Vespertino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtAltaInicioHorarioV" runat="server" class="form-control" placeholder="13:00:00" Text="13:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Final de horario (Vespertino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtAltaFinaHorarioV" runat="server" class="form-control" placeholder="19:00:00" Text="19:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Tiempo de cada turno (Min)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtAltaTiempoTurno" runat="server" class="form-control" placeholder="10" Text="10"></asp:TextBox>
                                        <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender2" runat="server" TargetControlID="TxtAltaTiempoTurno" Mask="99" MaskType="Number" InputDirection="RightToLeft" />
                                    </div>
                                    <ajaxToolkit:MaskedEditValidator ID="MaskedEditValidator2" runat="server" MaximumValue="60" MaximumValueMessage="Maximo valos es de 60 min" MinimumValue="1" ValidationGroup="gAlta" ControlExtender="MaskedEditExtender2" ControlToValidate="TxtAltaTiempoTurno" InvalidValueMessage="Minutos incorrectos" EmptyValueMessage="Dato obligatorio"></ajaxToolkit:MaskedEditValidator>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Listas de diagnósticos</label>
                                    <div class="input-group col-md-8">
                                        <asp:ListBox ID="LstAltaListaDiagnosticos" class="form-control select2 estudios" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Permisos</label>
                                    <div class="input-group col-md-8">
                                        <asp:ListBox ID="LstAltaPermisos" class="form-control select2 permisos" runat="server" SelectionMode="Multiple"></asp:ListBox>

                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" data-dismiss="modal" aria-hidden="false" runat="server" onserverclick="btnAltaGuardar_Click" causesvalidation="true" validationgroup="gAlta"><i class="fa fa-save"></i>Guardar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true">Cancelar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <%-- Editar Consultorio--%>
    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div id="Editar" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Editar consultorio</h4>
                        </div>

                        <div class="modal-body">
                            <div class="form-body">
                                <div class="form-group">
                                    <label class="col-md-4">Descripción</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="txtEditarDescripcion" runat="server" class="form-control" onkeypress="return ValSoloLetrasEspacioBlanco(event)" placeholder="Descripción" Style="text-transform: uppercase"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Dr. de cabecera</label>
                                    <div class="input-group col-md-8">
                                        <asp:DropDownList ID="DdEditarDrCabecera" runat="server" class="form-control"></asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Inicio de horario (Matutino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtEditInicioHorarioM" runat="server" class="form-control" placeholder="8:00:00" Text="9:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Final de horario (Matutino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtEditalrFinalHorarioM" runat="server" class="form-control" placeholder="13:00:00" Text="13:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Inicio de horario (Vespertino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtEditarInicioHorarioV" runat="server" class="form-control" placeholder="13:00:00" Text="13:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Final de horario (Vespertino)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtEditFinHorarioV" runat="server" class="form-control" placeholder="19:00:00" Text="19:00:00" ReadOnly="true"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Tiempo de cada turno (Min)</label>
                                    <div class="input-group col-md-8">
                                        <span class="input-group-addon"><i class="fa fa-clock-o"></i></span>
                                        <asp:TextBox ID="TxtEditarTurno" runat="server" class="form-control" placeholder="10" Text="10"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Listas de diagnósticos</label>
                                    <div class="input-group col-md-8">
                                        <asp:ListBox ID="LstEditDiagnosticos" class="form-control select2 estudios" runat="server" SelectionMode="Multiple" ></asp:ListBox>

                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-4">Permisos</label>
                                    <div class="input-group col-md-8">
                                        <asp:ListBox ID="LstEditPermisos" class="form-control select2 permisos" runat="server" SelectionMode="Multiple" ></asp:ListBox>

                                    </div>
                                </div>
                            </div>

                            <div class="modal-footer">
                                <button type="button" class="btn btn-success" data-dismiss="modal" aria-hidden="true" runat="server" onserverclick="btnEditarGuardar_Click"><i class="fa fa-save"></i>Guardar</button>
                                <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true" runat ="server" >Cancelar</button>
                            </div>

                        </div>

                    </div>
                </div>
            </div>
            <!--HACK-->
            <img src="dist/img/hack.jpg" onload="Hack()" class="hidden" />
            <!----------------------------------->
            <asp:HiddenField ID="HdIdConsultorio" runat="server" />
            <asp:HiddenField ID="HdIdDrCabecera" runat="server" />
            <asp:HiddenField ID="HdPregunta" runat="server" />
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
