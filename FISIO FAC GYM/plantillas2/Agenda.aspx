  <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Agenda.aspx.vb" Inherits="AgeMED.Agenda" EnableEventValidation="false" Culture="es-MX" UICulture="es-MX" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Home.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">    
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <link href="dist/css/agenda.css" rel="stylesheet" />

<!--Start of Tawk.to Script-->
<script type="text/javascript">
    var Tawk_API = Tawk_API || {}, Tawk_LoadStart = new Date();
    (function () {
        var s1 = document.createElement("script"), s0 = document.getElementsByTagName("script")[0];
        s1.async = true;
        s1.src = 'https://embed.tawk.to/59f4ac324854b82732ff8721/default';
        s1.charset = 'UTF-8';
        s1.setAttribute('crossorigin', '*');
        s0.parentNode.insertBefore(s1, s0);
    })();
</script>
<!--End of Tawk.to Script-->


     <script type="text/javascript">
         
         window.onload = function () {
             
             console.log("Cargó Agenda");
             $("#<%=TxtAgFecha.ClientID%>").datepicker({
                 format: "dd/mm/yyyy",
                 autoclose: true,
                 language: 'es',
                 todayBtn: "linked",
                 todayHighlight: true
             });
             $('#<%=TxtAgFecha.ClientID%>').on('show', function (e) {
                 if (e.date) {
                     $(this).data('stickyDate', e.date);
                 }
                 else {
                     $(this).data('stickyDate', null);
                 }
             });

             $('#<%=TxtAgFecha.ClientID%>').on('hide', function (e) {
                 var stickyDate = $(this).data('stickyDate');

                 if (!e.date && stickyDate) {
                     $(this).datepicker('setDate', stickyDate);
                     $(this).data('stickyDate', null);
                 }
             });
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=TxtAgFecha.ClientID%>').trigger('focus');

             });

            
         };

        

         function Hack() {
             
             if (window.event.keyCode == 13) return false;
             $("#<%=TxtAgFecha.ClientID%>").datepicker({
                 format: "dd/mm/yyyy",
                 autoclose: true,
                 language: 'es',
                 todayBtn: "linked",
                 todayHighlight: true
             });
             $('#<%=TxtAgFecha.ClientID%>').on('show', function (e) {
                 if (e.date) {
                     $(this).data('stickyDate', e.date);
                 }
                 else {
                     $(this).data('stickyDate', null);
                 }
             });

             $('#<%=TxtAgFecha.ClientID%>').on('hide', function (e) {
                 var stickyDate = $(this).data('stickyDate');

                 if (!e.date && stickyDate) {
                     $(this).datepicker('setDate', stickyDate);
                     $(this).data('stickyDate', null);
                 }
             });
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=TxtAgFecha.ClientID%>').trigger('focus');
             });
             function validar(e) {
                 tecla = (document.all) ? e.keyCode : e.which;
                 console.log(tecla);
                 if (tecla == 8 || tecla == 13) return false;
                 return false;
             }

         }

         function validar(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             console.log(tecla);
             if (tecla == 8 || tecla == 13) return false;
             return false;
         }
    </script>
    
        <script  type="text/javascript">
        function allowDrop(ev) {
            ev.preventDefault();
        }
        function drag(ev) {
            ev.dataTransfer.setData("Text", ev.target.id);
            ev.currentTarget.style.border = "dashed";               
        }
        function drop(ev) {
            ev.preventDefault();            
            var data = ev.dataTransfer.getData("Text");            
            ev.target.appendChild(document.getElementById(data));
            var data2 = ev.target.id;           
            document.getElementById(data).style.color = "blue";
            document.getElementById("<%=rowInicia.ClientID %>").value = data
            document.getElementById("<%=rowFinal.ClientID%>").value = data2
            document.getElementById("<%=BtnActualizar.ClientID%>").click();                 
        }           
    </script>

      
    <asp:UpdatePanel ID="UpdatePanelFechaConsultorio" runat="server">
        <ContentTemplate>
            <style>
                .example-modal .modal {
                    position: relative;
                    top: auto;
                    bottom: auto;
                    right: auto;
                    left: auto;
                    display: block;
                    z-index: 1;
                }

                .example-modal .modal {
                    /*background: transparent !important;*/
                }

                .txtAgFecha, .input-group-addon {
                    cursor: pointer;
                }
            </style>
            
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

                        <%-- %>asp:Panel ID="PanelDesicion" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                    <div class="alert alert-info alert-dismissable">
                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                        <h4>
                                            <asp:Label ID="LblMostarDecision" runat="server" Text=""></asp:Label></h4>
                                        <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSi1_Click"><i class="fa fa-check"></i> Sí</button>
                                        <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNo1_Click"><i class="fa fa-close"></i >No</button>
                                    </div>
                                </div>
                            </div>
                        </--%>
            

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
                        <!--HACK-->
                        <img src="dist/img/hack.jpg" onload="Hack()" class="hidden"/>
                        <!----------------------------------->
                       
                <%--    ---------------------------------------------------------------------------------------------%>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">          
        <ContentTemplate>
             
            <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanel1" DynamicLayout="False">
                        <ProgressTemplate>
                            <%--PRELOADER--%>
                            <div class="preloader">
                                <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
            <!-- Content Header (Page header) -->
            <section class="content-header">
                <h1>Agenda
                    <small>Buscar</small>
                </h1>
                <ol class="breadcrumb">
                    <li><a href="Agenda.aspx"><i class="fa fa-home"></i> Inicio</a></li>
                    <li class="active"><i class="fa fa-calendar"></i> Agenda</li>
                </ol>
            </section>
            <section class="content">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Horarios Agenda</h3>
                    </div>
                    <!-- /.box-header -->
                    <!-- form start -->
                    <div class="box-body">
                         <div class ="row">
                            <div class ="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <div class="">
                                        <asp:DropDownList ID="DDAgendasDoctores" runat="server" OnSelectedIndexChanged="DDAgendasDoctores_SelectedIndexChanged" CssClass="form-control input-lg" AutoPostBack="true" Visible="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class ="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar fa-2x"></i>
                                        </div>
                                        <asp:TextBox runat="server" ID="TxtAgFecha" CssClass="form-control datepicker txtAgFecha input-lg" OnTextChanged="TxtAgFecha_TextChanged" AutoPostBack="True" onkeypress="return validar(event)"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                         </div>
                         <div class="row">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                <asp:Button ID="BtnActualizar" runat="server" Text="Button"  style = "display:none"/>
                                <div class="table-responsive">
                                    <asp:DataGrid runat="server" ID="DGAgenda" CssClass="agendagrid table table-striped" GridLines="None" AutoGenerateColumns="False" AllowSorting="True" AlternatingItemStyle-BackColor="White">
                                    <AlternatingItemStyle BackColor="White" />
                                    <Columns>
                                        <%-- 0 --%>
                                        <asp:BoundColumn HeaderText="idhorario" DataField="idhorario" Visible="false">
                                            <ItemStyle CssClass="headrow" />
                                        </asp:BoundColumn>
                                        <%-- 1--%>
                                        <asp:BoundColumn HeaderText="TURNOS " DataField="horario">
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle CssClass="headrow" Width="15%" VerticalAlign="Middle" />
                                        </asp:BoundColumn>
                                        <%-- 2 --%>
                                        <asp:TemplateColumn HeaderText="PACIENTE">
                                            <ItemTemplate >
                                                <asp:LinkButton ID="lnkpaciente"  runat="server" CommandName="LnkPaciente" ForeColor="Green" OnClick="lnkpaciente_Click"  draggable="true" ondragstart="drag(event)" ondrop="drop(event)" ondragover="allowDrop(event)" CssClass="btn btn-default btn-block btn-lg">DISPONIBLE</asp:LinkButton>
                                                <asp:Label ID="lblidpaciente" runat="server" Visible="False"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle CssClass="itemGrid" />
                                        </asp:TemplateColumn>
                                        <%-- 3 --%>
                                        <asp:TemplateColumn HeaderText="ETAPA">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnk2" CommandName="BtnEstado" runat="server" Visible="False"></asp:LinkButton>                                
                                                <asp:Label ID="lbIdEstado" runat="server" Visible="False"></asp:Label>
                                                <asp:Label ID="lblTime" runat="server" ForeColor="DarkBlue" Visible="False">Min: 123</asp:Label>
                                                <asp:ImageButton ID="ImgEstudioRealizado" runat="server" Height ="2.5em" Width ="5.5em" Visible ="false"  />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Left" VerticalAlign="Middle" />
                                            <ItemStyle CssClass="itemGrid" HorizontalAlign="Left" />
                                        </asp:TemplateColumn>
                                        <%-- 4 --%>
                                        <asp:TemplateColumn HeaderText="CONSULTA" HeaderStyle-CssClass ="hidden-xs hidden-sm" ItemStyle-CssClass ="hidden-xs hidden-sm">
                                            <ItemTemplate>
                                                <asp:Label ID="lblseguimiento" runat="server" CssClass="hidden-xs hidden-sm"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" CssClass="hidden-xs hidden-sm itemGrid" />
                                        </asp:TemplateColumn>
                                        <%-- 5 --%>
                                        <asp:TemplateColumn HeaderText="idagenda" Visible="False" HeaderStyle-CssClass ="hidden hidden-xs" ItemStyle-CssClass ="hidden hidden-xs">
                                            <ItemTemplate>
                                                <asp:Label ID="lbIdAgenda" runat="server"></asp:Label>
                                            </ItemTemplate>
                                            <HeaderStyle CssClass="hidden hidden-xs" />
                                            <ItemStyle CssClass="hidden itemGrid" />
                                        </asp:TemplateColumn>
                                        <%-- 6 --%>
                                        <asp:TemplateColumn HeaderText="COSTO" HeaderStyle-CssClass ="hidden-xs" ItemStyle-CssClass ="hidden-xs" Visible="True">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lbCosto" runat="server" CommandName="LnkCosto"></asp:LinkButton>
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="10%" CssClass="text-center itemGrid" />
                                        </asp:TemplateColumn>
                                        <%-- 7 --%>
                                        <asp:TemplateColumn HeaderText="FACT." HeaderStyle-CssClass ="hidden-xs hidden-sm" ItemStyle-CssClass ="hidden-xs hidden-sm" Visible="False">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgFactura" ImageUrl="~/Resource/impresora.png" Visible="False" runat="server" Height="1.5em" Width="1.5em" CommandName="BtnFacturar" CssClass="hidden-xs hidden-sm" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="5%" CssClass="itemGrid" />
                                        </asp:TemplateColumn>
                                        <%-- 8 --%>
                                        <asp:TemplateColumn HeaderText="REAGENDAR" HeaderStyle-CssClass ="hidden-xs hidden-sm" ItemStyle-CssClass ="hidden-xs hidden-sm" Visible="false">
                                            <ItemTemplate>
                                                <asp:Button ID="BtnReAgendar" runat="server" Text="Reagendar" CssClass="btn btn-block btn-primary hidden-xs hidden-sm" Visible="false" OnClick="BtnReAgendar_Click" CommandName="BtnReAgendar" />
                                            </ItemTemplate>
                                            <HeaderStyle Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <ItemStyle Width="5%" CssClass="hidden-xs hidden-sm itemGrid" />
                           
                                        </asp:TemplateColumn>
                                      </Columns>
                                    <HeaderStyle CssClass="headGrid" />
                                </asp:DataGrid>
                                </div>
                                
                            </div>
                        </div>
                      

                        <ajaxToolkit:ModalPopupExtender ID="mpupetapas" runat="server" PopupControlID="pnletapas" TargetControlID="hffolioconsulta" CancelControlID="BTNcerrar" BackgroundCssClass="modalBackground"></ajaxToolkit:ModalPopupExtender>
                        <asp:HiddenField runat="server" ID="hffolioconsulta" />
                        <asp:HiddenField ID="numEtapa" runat="server" />
                        <asp:HiddenField ID="rowInicia" runat="server"  Value =""/>
                        <asp:HiddenField ID="rowFinal" runat="server"  Value =""  />
                        <asp:Timer ID="Timer1" runat="server" Interval="300000"></asp:Timer>
                </div>

           </section>

            <asp:Panel runat="server" ID="pnletapas" CssClass="panelpp">
                <div class="example-modal">
                    <div class="modal-1">
                        <div class="modal-dialog">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <button id="BTNcerrar" type="button" class="close" data-dismiss="modal" aria-label="Close" data-toggle="tooltip">
                                        <span aria-hidden="true"><i class="fa fa-times"></i></span>
                                    </button>
                                    <h4 class="modal-title">Cambiar Etapas</h4>
                                </div>
                                <div class="modal-body">
                                    <asp:DataGrid ID="dgEtapas" runat="server" CssClass="table no-border" AutoGenerateColumns="False" ShowHeader="False" CellPadding="2" CellSpacing="2" GridLines="none">
                                        <Columns>
                                            <asp:TemplateColumn>
                                                <ItemStyle HorizontalAlign="Center" />
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="Image22" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn>
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lkb2" Text="el estatus" runat="server" Font-Names="Calibri" Font-Size="16px"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:BoundColumn DataField="codigoEtapa" HeaderText="codigoEtapa" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="imagen" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="descripcion" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="colorfondorgb" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="colortexto" Visible="False"></asp:BoundColumn>
                                            <asp:BoundColumn DataField="colorfondo" Visible="False"></asp:BoundColumn>
                                        </Columns>
                                        <ItemStyle />
                                    </asp:DataGrid>
                                </div>

                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </div>
                <!-- /.example-modal -->
            </asp:Panel>



            
            <!--Modal Facturar -->
              <div id="datos_facturacion" class="modal fade" tabindex="-1"  role="dialog" aria-labelledby="myModalLabel" >
    <div class="modal-dialog modal-lg">
       <div class="modal-content">
          <div class="modal-header">
             <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
             <h4 class="modal-title">Datos de Facturación</h4>
          </div>
           <asp:UpdatePanel runat="server">
               <ContentTemplate>
                   <div class="modal-body">
                       <div class="form-body">
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
                                   <input type="text" class="form-control" placeholder="Razón Social" id="input_razon_social" name="input_razon_social" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">RFC</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-barcode fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="RFC" id="rfc" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Correo</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-envelope fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Correo" id="correo" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Dirección</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-marker fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Dirección" id="direccion" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Código Postal</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-pin fa-fw"></i></span>
                                   <input type="number" class="form-control" placeholder="Código Postal" id="cp" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Ciudad</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Ciudad" id="ciudad" runat="server"  >
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Estado</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map-signs fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Estado" id="estado" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">País</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-map fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="País" id="pais" runat="server">
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Consultas pendientes por facturar</label>
                               <div class="input-group col-md-8">
                                   <asp:GridView ID="GdConsultasXpagar" runat="server" CssClass="table table-bordered table-striped" AutoGenerateColumns="False">
                                       <Columns>
                                           <asp:BoundField DataField="folioConsulta" HeaderText="Folio Consulta" />
                                           <asp:BoundField DataField="FechaAgenda" HeaderText="Fecha" />
                                           <asp:BoundField DataField="ImportePago" HeaderText="Importe" />
                                           <asp:TemplateField HeaderText="Agregar">
                                               <ItemTemplate>
                                                   <asp:CheckBox ID="chk" runat="server" />
                                                   <asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                   <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                               </ItemTemplate>
                                           </asp:TemplateField>
                                       </Columns>
                                   </asp:GridView>                                   
                               </div>
                           </div>
                           <div class="form-group">
                               <label class="col-md-3">Observaciones</label>
                               <div class="input-group col-md-8">
                                   <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                   <input type="text" class="form-control" placeholder="Observaciones de la Factura" id="observacionesf" runat="server">
                               </div>
                           </div>
                       </div>
                       <div class="modal-footer">
                           <input type="hidden" id="codigo_paciente" runat="server">
                           <input type="hidden" id="id_razon_social" runat="server">
                           <asp:HiddenField runat="server" ID="fconsulta" />
                           <input type="hidden" id="folio_consulta" runat="server">
                           <input type="hidden" id="foliofac" runat="server">
                           <input type="hidden" id="seriefac" runat="server">
                           <input type="hidden" id="bandera" runat="server">
                           <button type="button" class="btn btn-success" data-dismiss="modal" runat="server" onserverclick="Facturar"><i class="fa fa-save"></i>Facturar</button>
                           <button type="button" class="btn btn-default" data-dismiss="modal" aria-hidden="true"><i class="fa fa-close"></i>Cancelar</button>
                       </div>
                   </div>
                  <CR:CrystalReportViewer ID="origen_reporte" runat="server" AutoDataBind="true" />
                  <CR:CrystalReportSource ID="visor_reporte" runat="server"></CR:CrystalReportSource>


               </ContentTemplate>
           </asp:UpdatePanel>
           
          
       </div>
    </div>
   </div>
               
        
        </ContentTemplate>        
    </asp:UpdatePanel>
   
</asp:Content>


