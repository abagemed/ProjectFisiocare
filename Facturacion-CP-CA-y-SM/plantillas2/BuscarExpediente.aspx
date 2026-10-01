<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="BuscarExpediente.aspx.vb" Inherits="AgeMED.BuscarExpediente"   EnableEventValidation="false"%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script>
         function ValSoloLetras(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             if (tecla == 8) return true;
             patron = /[A-Za-zñÑ]/;
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
                if ($('.tabla_expedientes').length > 0) {
                    $('html, body').animate({
                        scrollTop: $("#box_expedientes").offset().top
                    }, 2000);
                }
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Expedientes
           
            <small>Buscar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-address-card-o"></i> Búsqueda Expediente</li>
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
                    <div class="box box-info">
                        <div class="box-header with-border">
                            <h3 class="box-title">Búsqueda de pacientes</h3> 
                            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>                          
                        </div>
                        <div class="box-body" style="display :block;">
                        <asp:UpdatePanel ID="UpdatePanelCita" runat="server">
                            <ContentTemplate>
                               <%--PANEL DE AVISOS--%>
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
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TxbPaterno" ValidationGroup="buscarPaciente"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>
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
                                <button type="button" class="btn btn-primary btn-lg" runat="server" onserverclick="BtnBuscarPaciente_Click" validationgroup="buscarPaciente"><i class="fa fa-search"></i> Buscar</button>
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
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="LstBoxPacientes" ValidationGroup="seleccionPaciente"><span class="mensaje-error">Por favor, selecciona un paciente</span></asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <hr>
                                <button type="button" class="btn btn-success btn-lg" runat="server" onserverclick="BtnVer_Click" validationgroup="seleccionPaciente"><i class="fa fa-eye"></i> Ver</button>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                              </div>  
                    </div>
      





    <%--------------------- Fin Cambioa-------------------------------%>





  
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
   
        <div class="box box-success" id="box_expedientes">
        <div class="box-header with-border">
            <h3 class="box-title">EXPEDIENTES</h3>
            <div class="box-tools pull-right">
                <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
            </div>
        </div>
        <!-- /.box-header -->
        <div class="box-body" style="display: block;">
            <div class ="row">
                <div class ="col-lg-12">
            <asp:UpdatePanel ID="UpdatePanelExpedientes" runat="server">
                <ContentTemplate>
                    <asp:GridView ID="dtgexpedientes" runat="server"   CssClass="dataTable table-responsive table table-striped tabla_expedientes" AutoGenerateColumns="False">
                        <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                        <Columns>
                            <asp:ButtonField HeaderText="Imprimir" Text="Imprimir" ButtonType="Button" CommandName="Imprimir" Visible="False">
                                <ControlStyle CssClass="btn btn-block btn-success btn-xs" ForeColor="White" />
                            </asp:ButtonField>
                            <asp:ButtonField ButtonType="Button" CommandName="Ver" HeaderText="Expediente" ShowHeader="True" Text="Ver">
                                <ControlStyle CssClass="btn btn-block btn-primary btn-xs" />
                            </asp:ButtonField>
                            <asp:BoundField AccessibleHeaderText="Folio" DataField="Folio" HeaderText="Folio">
                                <ControlStyle CssClass="hidden-xs" />
                                <HeaderStyle CssClass="hidden-xs" />
                                <ItemStyle CssClass="hidden-xs" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="Codigo Paciente" DataField="codigoPaciente" HeaderText="Codigo Paciente">
                                <ControlStyle CssClass="hidden-xs hidden-sm" />
                                <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                <ItemStyle CssClass="hidden-xs hidden-sm" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="Consultorio" DataField="Consultorio" HeaderText="Consultorio">
                                <ControlStyle CssClass="hidden-xs hidden-sm" />
                                <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                <ItemStyle CssClass="hidden-xs hidden-sm" />
                            </asp:BoundField>
                            <asp:BoundField AccessibleHeaderText="Fecha" DataField="Fecha" HeaderText="Fecha Consulta" />
                            <asp:BoundField AccessibleHeaderText="Estado" DataField="Estado" HeaderText="Estado" >
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
                      <asp:HiddenField ID="HdPregunta" runat="server" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <!-- /.box-body -->
        </div>
            </div>
    </div>
  </div>
              </div>
        </section>

</asp:Content>
