<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="FacturacionNC.aspx.vb" Inherits="AgeMED.FacturacionNC" EnableEventValidation="false" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>


<%@ Import Namespace="System.Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    
    <script>
        window.onload = function () {
            $('input[type="checkbox"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            
        };

        function ValSoloLetras(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z ñÑ1-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z ñÑ1-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        function Hack() {
            if (window.location.pathname != "/FacturacionClientes.aspx") {
                $('input[type="checkbox"]').addClass("flat-red");
                //Flat red color scheme for iCheck
                $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                    checkboxClass: 'icheckbox_flat-green',
                    radioClass: 'iradio_flat-green'
                });
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
        <h1>Facturación clientes
            <small>Buscar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="FacturacionClientes.aspx"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-address-book-o"></i>Facturación Cliente</li>
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
                <div class="box box-info" id="box_BusquedaPaciente" runat="server">
                    <div class="box-header with-border">
                        <h3 class="box-title">Búsqueda de clientes</h3>
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
                                                    <asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label></h4>
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
<%--                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TxbPaterno" ValidationGroup="buscarPaciente"><span class="mensaje-error">Este campo es obligatorio</span></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="TxbMaterno" runat="server" CssClass="form-control" onkeypress="return ValSoloLetras(event)" Style="text-transform: uppercase" BackColor="White" MaxLength="20" ToolTip="Apellido materno"></asp:TextBox>
                                        <ajaxToolkit:TextBoxWatermarkExtender ID="TxbMaterno_TextBoxWatermarkExtender" runat="server" BehaviorID="TxbMaterno_TextBoxWatermarkExtender" TargetControlID="TxbMaterno" WatermarkText="Apellido Materno" />
                                        <%-- <input type="text" class="form-control" placeholder="Segundo Apellido" name="materno">--%>
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
                        <button type="button" class="btn btn-primary btn-lg" runat="server" onserverclick="BtnBuscarPaciente_Click" ><span runat="server" id="BotonBuscar"><i class="fa fa-search"></i> Buscar</span></button>

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

                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <button type="submit" class="btn btn-success btn-lg" runat="server" onserverclick="BtnVer_Click"><i class="fa fa-eye"></i>Ver</button>
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
                        <h3 class="box-title">Consultas por facturar</h3>
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
                                        <p>
                                            <i class="icon fa fa-hand-o-right fa-2x">
                                                <asp:Label ID="LblDesicion" runat="server" Text="Label" Style="font-family: 'Arya', sans-serif; font-size: 30px"></asp:Label></i>
                                        </p>
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
                               
                                <asp:GridView ID="dtgCargosConsultas" runat="server" CssClass="dataTable table-responsive table table-striped tabla_citas_programadas" AutoGenerateColumns="False">
                                    <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                    <Columns>
                                        <asp:BoundField DataField="idFactura" HeaderText="IdFactura">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="CodigoPaciente" HeaderText="Paciente" HtmlEncode="false">
                                            <ControlStyle CssClass="hidden-xs hidden-sm" />
                                            <HeaderStyle CssClass="hidden-xs hidden-sm" />
                                            <ItemStyle CssClass="hidden-xs hidden-sm" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Consultorio" HeaderText="Médico">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="fecha" HeaderText="Fecha" />
                                        <asp:BoundField DataField="serie" HeaderText="Serie">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="num_factura" HeaderText="Factura">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="Abono" HeaderText="Abono" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="importe" HeaderText="Importe" DataFormatString="{0:C}">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" HorizontalAlign="Right" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="uuid" HeaderText="UUID">
                                            <ControlStyle CssClass="hidden-xs" />
                                            <HeaderStyle CssClass="hidden-xs" />
                                            <ItemStyle CssClass="hidden-xs" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Selección">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chk" runat="server" />
                                                <%--<asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
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

                                <hr />
                                <button type="button" class="btn btn-lg btn-success btn-block" runat="server" id="btn_facturar" onserverclick="Facturar"><i class="fa fa-check-square-o"></i>Verificar Consultas</button>
                               </div>
                       
                    <!-- /.box-body -->
                                </div>
            </div>
        </div>
              </section>              
                       
                                 <CR:CrystalReportViewer ID="origen_reporte" runat="server" AutoDataBind="true" />
                  <CR:CrystalReportSource ID="visor_reporte" runat="server"></CR:CrystalReportSource>
                            </ContentTemplate>
                        </asp:UpdatePanel>

                     

                          <!--Modal Facturar -->
            <div id="datos_facturacion" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                <div class="modal-dialog  modal-lg">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                            <h4 class="modal-title">Datos de Facturación</h4>
                        </div>                      
                        <asp:UpdatePanel  runat="server">
                            <ContentTemplate>     
                                <div class="modal-body">
                                    <asp:Panel ID="PanelCriticoM" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-danger alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeCriticoM" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                <asp:Panel ID="PanelAdvertenciaM" runat="server" Visible="false">
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                            <div class="alert alert-warning alert-dismissable">
                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                <h4>
                                                    <asp:Label ID="LblMensajeAdvertenciaM" runat="server" Text="Label"></asp:Label></h4>
                                            </div>
                                        </div>
                                    </div>
                                </asp:Panel>

                                    <div class="form-body">

                                        <div class="row" id="div_dfacturacion" runat="server" >
                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12" >
                                                            <label>VersionCFDI</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="VersionCFDI" id="tbVersion" runat="server">
                                                            </div>
                                                        </div>
                                 <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Comprobante</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Tipo" id="tbTipoComprobante" runat="server"  ></div>
                                                        </div>
                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Serie</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Serie" id="tbSerie" runat="server"  ></div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Folio</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Folio" id="tbFolio" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Fecha</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Fecha" id="dtpFecha" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                                            <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Exportacion</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                <asp:DropDownList ID="tbExportacion" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                
                                                    </div>
                                        <div class="row" id="div_emisor" runat="server">
                               <div class="modal-header">
             <h4 class="modal-title">Datos del Emisor</h4>
          </div>
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <label>Emisor</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorNombre" id="tbEmisorNombre" runat="server"  >
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>RFC</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorRFC" id="tbEmisorRFC" runat="server"  ></div>
                                                        </div>

                                                        <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>R.Fiscal</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="EmisorRegimenFiscal" id="tbEmisorRegimenFiscal" runat="server"  >
                                                                
                                                            </div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>L.Expedicion</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="LugarExpedicion" id="tbLugarExpedicion" runat="server"  ></div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Moneda</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Moneda" id="tbMoneda" runat="server"  ></div>
                                                        </div>
                                
                                                    </div>

                                        <div class="row">

                           <div class="modal-header">
             <h4 class="modal-title">Datos del Receptor</h4>
          </div>



                           <div class="form-group" id="div_select_razon_social" runat="server">
                               <label class="col-md-3">Razón Social</label>
                               <div class="input-group col-md-8">
                                   <asp:DropDownList ID="select_razon_social" class="form-control" runat="server" AutoPostBack="True" OnSelectedIndexChanged="CambioSelectRazonSocial"></asp:DropDownList>
                               </div>
                           </div>
                           <div class="form-group" id="div_input_razon_social" runat="server">
                               <label class="col-md-3">Razón Social</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Razón Social" id="tbReceptorNombre" name="input_razon_social" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">RFC</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="RFC" id="tbReceptorRFC" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Correo</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Correo" id="correo" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_direccion" runat="server">
                               <label class="col-md-3">Dirección</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Dirección" id="direccion" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_cp" runat="server">
                               <label class="col-md-3">CP(Domicilio Fiscal)</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                   <input type="number" class="form-control" placeholder="Código Postal" id="cp" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_ciudad" runat="server">
                               <label class="col-md-3">Ciudad</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Ciudad" id="ciudad" runat="server"  >
                               </div>
                           </div>
                           <div class="form-group" id="div_estado" runat="server">
                               <label class="col-md-3">Estado</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Estado" id="estado" runat="server">
                               </div>
                           </div>
                           <div class="form-group" id="div_pais" runat="server">
                               <label class="col-md-3">País</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="País" id="pais" runat="server">
                               </div>
                           </div>
                                            <div class="form-group" id="div_regfiscal" runat="server">
                               <label class="col-md-3">Regimen Fiscal</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                   <asp:DropDownList ID="tbReceptorRegimenFiscal" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>  
                               </div>
                           </div>
                               </div>
                                        <div class="row">
                               <div class="modal-header">
             <h4 class="modal-title">Datos del Comprobante</h4>
          </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                            <label>Forma de Pago</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                <asp:DropDownList ID="tbFormaPago" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                            <label>Metodo de Pago</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                <asp:DropDownList ID="tbMetodoPago" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>
                                                        </div>
                                                        </div>

                                                        <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                            <label>UsoCFDI</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                 <asp:DropDownList ID="tbReceptorUsoCFDI" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>    
                                                            </div>
                                                        </div>
                                            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                            <label>TipoRelacion</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                                                 <asp:DropDownList ID="tbTipoRelacion" runat="server" class="form-control" AutoPostBack="true"></asp:DropDownList>    
                                                            </div>
                                                        </div>
                               <%--<div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>L.Expedicion</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="LugarExpedicion" id="Text4" runat="server"  ></div>
                                                        </div>
                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Moneda</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Moneda" id="Text5" runat="server"  ></div>
                                                        </div>--%>
                                
                                                    </div>

                                         <div class="form-group">
                               <label class="col-md-3">Conceptos por Facturar</label>
                               <div class="input-group col-md-12">
                                   <%--<asp:GridView ID="GdConsultasXpagar" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                       <Columns>
                                           <asp:BoundField DataField="cantidad" HeaderText="cantidad" />
                                           <asp:BoundField DataField="descripcion" HeaderText="Descricion" />
                                           <asp:BoundField DataField="costo" HeaderText="Importe" />
                                       </Columns>
                                   </asp:GridView>--%>
                                    <asp:DataGrid ID="gridDetalles" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateColumn HeaderText="Cantidad"  ItemStyle-Width="40px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox1"  Width="40px" runat="server" AutoPostBack="True" Text='<%# Bind("cantidad") %>' enabled="false"  BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="serie" ItemStyle-Width="40px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox2"  Width="40px" runat="server" Text='<%# Bind("serie")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Factura" ItemStyle-Width="60px">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox3" Width="60px" runat="server" AutoPostBack="True" Text='<%# Bind("num_factura")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                     <asp:TemplateColumn HeaderText="UUID" ItemStyle-Width="250px">
                        <ItemTemplate>
                            <asp:TextBox ID="guuid" Width="250px" runat="server" AutoPostBack="True" Text='<%# Bind("uuid")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Total" ItemStyle-Width="60px"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" Width="60px" runat="server" AutoPostBack="True" Text='<%# Bind("costoTotal")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid> 
                                   <asp:DataGrid ID="gridDetallesC" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                <Columns>
                    <asp:TemplateColumn HeaderText="Cantidad" >
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox4" runat="server" AutoPostBack="True" Text='<%# Bind("cantidad") %>' enabled="false"  BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Descripcion">
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox5" runat="server" Text='<%# Bind("descripcionConcepto")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Importess"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox6" runat="server" AutoPostBack="True" Text='<%# Bind("subtotal")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Referencia"><ItemStyle HorizontalAlign="Right" />
                        <ItemTemplate>
                            <asp:TextBox ID="TextBox7" runat="server" AutoPostBack="True" Text='<%# Bind("observaciones")%>' enabled="false" BorderWidth="0"></asp:TextBox>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid>                                 
                               </div>
                           </div>
                                          <div class="form-group">
                               <label class="col-md-3">Total NC</label>
                               <div class="input-group col-md-4">
                                   <span class="input-group-addon"><i class="fa fa-font fa-dollar"></i></span>
                                   <input type="text" class="form-control" placeholder="00.00" id="totalNC" runat="server">
                               </div>
                           </div>
                                         <div class="form-group">
                               <label class="col-md-3">Paciente</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Nombre del paciente!" id="nompaciente" runat="server">
                               </div>
                           </div>
                                        <div class="form-group">
                               <label class="col-md-3">Observaciones</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Observaciones de la Factura si es requerido!" id="dcobservaciones" runat="server">
                               </div>
                           </div>


                                        <div class="row" id="div_UPfiscal" runat="server">
                                <div class="modal-header">
             <h4 class="modal-title">Configuraciones</h4>
          </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>UsuarioFD</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="UsuarioFoliosDigitales" id="tbUsuarioFoliosDigitales" runat="server"  >
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>PasswordFD</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="PasswordFoliosDigitales" id="tbPasswordFoliosDigitales" runat="server"  ></div>
                                                        </div>
                                <%--<div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Subtotal</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="numSubtotal" id="numSubtotal" runat="server"  ></div>
                                                        </div>
                                <div class="col-lg-2 col-md-2 col-sm-12 col-xs-12">
                                                            <label>Descuento</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="numDescuento" id="numDescuento" runat="server"  ></div>
                                                        </div>
                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Total</label>
                                                            <div class="input-group col-md-12">
                                                                 <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Total" id="numTotal" runat="server"  ></div>
                                                        </div>--%>



                                </div>
                                    </div>
                                    <div class="modal-footer">
                                        <input type="hidden" id="codigo_paciente" runat="server">
                                        <input type="hidden" id="dpaciente" runat="server">
                                        <input type="hidden" id="idcosto" runat="server">
                                        <input type="hidden" id="id_razon_social" runat="server">
                                        <input type="hidden" id="folio_consulta" runat="server">
                                        <input type="hidden" id="bandera" runat="server">                                     
                                        <button type="button" class="btn btn-success" data-dismiss="modal" runat="server"  id="C_facturar" onserverclick="FacturarConsultas" causesvalidation="true" validationgroup="datosFac"><i class="fa fa-save"></i> Facturar C</button>
                                      <%--  <button type="button" class="btn btn-success" data-dismiss="modal" runat="server"  id="CM_facturar" onserverclick="FacturarCM" causesvalidation="true" validationgroup="datosFac"><i class="fa fa-save"></i> Facturar CM</button>--%>
                                        <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i> Cancelar</button>
                                   <asp:HiddenField runat="server" ID="fconsulta" />
                                         </div>
                                </div>                        
                       </ContentTemplate></asp:UpdatePanel> 
                    </div>
                </div>
            </div>

                       
</asp:Content>
