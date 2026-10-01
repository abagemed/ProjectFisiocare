<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="BuscarCitaPaciente.aspx.vb" Inherits="AgeMED.BuscarCitaPaciente" EnableEventValidation="false" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <script>
         function ValSoloLetras(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             if (tecla == 8) return true;
             patron = /[A-Za-z ñÑ]/;
             te = String.fromCharCode(tecla);
             return patron.test(te);
         }
         function ValSoloLetrasEspacioBlanco(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             if (tecla == 8) return true;
             patron = /[A-Za-z ñÑ]/;
             te = String.fromCharCode(tecla);
             return patron.test(te);
         }
         function Hack() {
             if (window.location.pathname != "/Agenda.aspx") {
                 $(".messages-menu").addClass("hidden");
                 $(".notifications-menu").addClass("hidden");
                 $(".tasks-menu").addClass("hidden");
             }
             if ($('.tabla_citas_programadas').length > 0) {
                 $('html, body').animate({
                     scrollTop: $("#box_citas_programadas").offset().top
                 }, 2000);
             }
             else if ($('.tabla_citas_pasadas').length > 0) {
                 $('html, body').animate({
                     scrollTop: $("#box_citas_pasadas").offset().top
                 }, 2000);
             }
         }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
    
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Citas
            <small>Buscar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Agenda.aspx"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active"><i class="fa fa-address-book-o"></i> Ver Citas</li>
        </ol>
    </section>
    <section class="content">
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelCita">
            <ProgressTemplate>
                <%--PRELOADER--%>
                <div class="preloader">
                    <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <div class="row">
            <div class="col-md-12">
                    <div class="box box-info" id="box_BusquedaPaciente" runat ="server">
                        <div class="box-header with-border">
                            <h3 class="box-title">Búsqueda de pacientes</h3> 
                            <div class="box-tools pull-right">
                                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                            </div>                          
                        </div>
                        <div class="box-body">
                        <asp:UpdatePanel ID="UpdatePanelCita" runat="server">
                            <ContentTemplate>
                                <%--PANEL DE AVISOS--%>
                                <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-success alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
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
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMostarDecision" runat="server" Text="Label"></asp:Label></h4>
                                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi_Click"><i class="fa fa-check"></i> Sí</button>
                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo_Click"><i class="fa fa-close"></i> No</button>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelCritico" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
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
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeAdvertencia" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>
                                <!--HACK-->
                                <img src="dist/img/hack.jpg" onload="Hack()" class="hidden"/>
                                <!----------------------------------->
                                <!--NOMBRE-->
                                <div class="row">                                    
                                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                                <asp:TextBox ID="TxbPaterno" runat="server" type="text" CssClass="form-control" onkeypress="return ValSoloLetras(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Apellido paterno"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TxbPaterno_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbPaterno_TextBoxWatermarkExtender" TargetControlID="TxbPaterno" WatermarkText="Apellido Paterno" />
                                            </div>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" BackColor="#FFFFCC" ControlToValidate="TxbPaterno" ErrorMessage="      Dato Obligatorio" ValidationGroup="buscarPaciente" Font-Bold="True" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                                <asp:TextBox ID="TxbMaterno" runat="server" CssClass="form-control" onkeypress="return ValSoloLetras(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Apellido materno"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TxbMaterno_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbMaterno_TextBoxWatermarkExtender" TargetControlID="TxbMaterno" WatermarkText="Apellido Materno" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                                <asp:TextBox ID="TxbNombre" runat="server" CssClass="form-control" onkeypress="return ValSoloLetrasEspacioBlanco(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Nombre"></asp:TextBox>
                                                <ajaxToolkit:TextBoxWatermarkExtender ID="TxbNombre_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbNombre_TextBoxWatermarkExtender" TargetControlID="TxbNombre" WatermarkText="Nombre" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <button type="submit" class="btn btn-primary btn-lg" runat="server" onserverclick="BtnBuscarPaciente_Click" validationgroup="buscarPaciente"><i class="fa fa-search"></i> Buscar</button>

                                <hr>
                                <!--RESULTADOS DE BUSQUEDA-->
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <p>Resultados de búsqueda</p>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                        <div class="form-group">
                                            <div class="input-group col-md-12">
                                                <asp:ListBox ID="LstBoxPacientes" runat="server" CssClass="form-control"></asp:ListBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" BackColor="#FFFFCC" ControlToValidate="LstBoxPacientes" ErrorMessage=" Seleccionar un paciente" ValidationGroup="seleccionPaciente" Font-Bold="True" Font-Size="Medium" ForeColor="#FF3300"></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr>
                                <button type="submit" class="btn btn-success btn-lg" runat="server" onserverclick="BtnVer_Click" validationgroup="seleccionPaciente"><i class="fa fa-eye"></i> Ver</button>


                            </ContentTemplate>
                        </asp:UpdatePanel>
                              </div>  
                    </div>

    <%--------------------- Fin Cambios-------------------------------%>






    <div>
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
            <ContentTemplate>
                <asp:Panel Style="width: 100%" ID="panelError" runat="server" Visible="TRUE">
                    <center>    
                       <div class="box-comment">
                        <table style ="padding :4px 8px 4px 8px;" >
                            <tr>
                                <td><h4><asp:Label ID="LblNomPaciente" runat="server" Text="" ></asp:Label></h4></td>                               
                            </tr>
                        </table>                   
                        </div>
                         </center>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
   
    <div class="box box-success" id="box_citas_programadas">
        <div class="box-header with-border">
            <h3 class="box-title">CITAS PROGRAMADAS</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <%--------------------- Avisos en pantalla-------------------------------%>

                    <asp:Panel ID="PanelAviso2" runat="server" Visible="false">
                        <div class="alert alert-success" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <i class="icon fa fa-check fa-2x ">
                                <asp:Label ID="LblAviso" runat="server" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelDesicion2" runat="server" Visible="false">
                        <div class="alert alert-info" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <p><i class="icon fa fa-hand-o-right fa-2x">
                                <asp:Label ID="LblDesicion" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i></p>
                            <p>
                                <asp:Button ID="BtnSi2" class="btn btn-success" runat="server" Text="   Si   " Width="109px" />
                                <asp:Button ID="BtnNo2" runat="server" Text="   No   " class="btn btn-danger" Width="109px" />
                            </p>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelCritico2" runat="server" Visible="false">
                        <div class="alert alert-danger" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <i class="icon fa fa-ban fa-2x">
                                <asp:Label ID="Lblcritico" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                        </div>
                    </asp:Panel>
                    <asp:Panel ID="PanelAdvertencia2" runat="server" Visible="false">
                        <div class="alert alert-warning" role="alert">
                            <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <i class="icon fa fa-warning fa-2x">
                                <asp:Label ID="LblAdvertencia" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                        </div>
                    </asp:Panel>

                    <%--    ---------------------------------------------------------------------------------------------%>
                    <asp:GridView ID="dtgCitasProgramadas" runat="server"  CssClass="dataTable table-responsive table table-striped tabla_citas_programadas" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:ButtonField HeaderText="Cancelar" Text="Cancelar" ButtonType="Button" CommandName="Cancelar">
                                <ControlStyle CssClass="btn btn-block btn-danger btn-xs" ForeColor="White" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Mover" HeaderText="Reagendar" ShowHeader="True" Text="Mover">
                                <ControlStyle CssClass="btn btn-block btn-primary btn-xs" />
                            </asp:ButtonField>
                            <asp:BoundField DataField="Folio" HeaderText="Folio">
                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoPaciente" HeaderText="Codigo Paciente">
                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Consultorio" HeaderText="Consultorio" />
                            <asp:BoundField DataField="Turno" HeaderText="Turno">
                            <ControlStyle CssClass="hidden-xs" />
                            <HeaderStyle CssClass="hidden-xs" />
                            <ItemStyle CssClass="hidden-xs" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado">
                            <ControlStyle CssClass="hidden-xs" />
                            <HeaderStyle CssClass="hidden-xs" />
                            <ItemStyle CssClass="hidden-xs" />
                            </asp:BoundField>
                        </Columns>
                        <EditRowStyle BackColor="#999999" />
                        <FooterStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#5D7B9D" Font-Bold="True" ForeColor="White" />
                        <PagerStyle BackColor="#284775" ForeColor="White" HorizontalAlign="Center" />
                        <RowStyle BackColor="#F7F6F3" ForeColor="#333333" />
                        <SelectedRowStyle BackColor="#E2DED6" Font-Bold="True" ForeColor="#333333" />
                        <SortedAscendingCellStyle BackColor="#E9E7E2" />
                        <SortedAscendingHeaderStyle BackColor="#506C8C" />
                        <SortedDescendingCellStyle BackColor="#FFFDF8" />
                        <SortedDescendingHeaderStyle BackColor="#6F8DAE" />
                    </asp:GridView>

                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.box-body -->
    </div>


    
    <div class="box box-primary" id="box_citas_pasadas">
        <div class="box-header with-border">
            <h3 class="box-title">CITAS PASADAS</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body" style="display: block;">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="dtgCitas" runat="server" CssClass="dataTable table-responsive table table-striped tabla_citas_pasadas" AutoGenerateColumns="False">
                        <Columns>
                            <asp:BoundField DataField="Folio" HeaderText="Folio">
                            <ControlStyle CssClass="hidden-xs hidden-ms" />
                            <HeaderStyle CssClass="hidden-xs hidden-ms" />
                            <ItemStyle CssClass="hidden-xs hidden-ms" />
                            </asp:BoundField>
                            <asp:BoundField DataField="CodigoPaciente" HeaderText="Codigo Paciente">
                            <ControlStyle CssClass="hidden-xs hidden-ms" />
                            <HeaderStyle CssClass="hidden-xs hidden-ms" />
                            <ItemStyle CssClass="hidden-xs hidden-ms" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Consultorio" HeaderText="Consultorio" />
                            <asp:BoundField DataField="Turno" HeaderText="Turno">
                            <ControlStyle CssClass="hidden-xs " />
                            <HeaderStyle CssClass="hidden-xs" />
                            <ItemStyle CssClass="hidden-xs" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Fecha" HeaderText="Fecha" />
                            <asp:BoundField DataField="Estado" HeaderText="Estado" />
                            <asp:BoundField DataField="Agendo" HeaderText="Agendo">
                            <ControlStyle CssClass="hidden-xs hidden-ms" />
                            <HeaderStyle CssClass="hidden-xs hidden-ms" />
                            <ItemStyle CssClass="hidden-xs hidden-ms" />
                            </asp:BoundField>
                        </Columns>
                    </asp:GridView>
                    <asp:HiddenField ID="Hdpregunta" runat="server" />
                    <asp:HiddenField ID="Hdindex" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.box-body -->
    </div>
                  </div>
              </div>
        </section>

</asp:Content>


