<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="BloqueosTurnos.aspx.vb" Inherits="AgeMED.BloqueosTurnos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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

    .txtAgFecha2, .input-group-addon{
        cursor:pointer;
    }
    
  </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        window.onload = function () {
            console.log("Cargó Agenda");
            $("#<%=TxtAgFecha2.ClientID%>").datepicker({
                 format: "dd/mm/yyyy",
                 autoclose: true,
                 language: 'es',
                 todayBtn: "linked",
                 todayHighlight: true
             });
             $('#<%=TxtAgFecha2.ClientID%>').on('show', function (e) {
                 if (e.date) {
                     $(this).data('stickyDate', e.date);
                 }
                 else {
                     $(this).data('stickyDate', null);
                 }
             });

             $('#<%=TxtAgFecha2.ClientID%>').on('hide', function (e) {
                 var stickyDate = $(this).data('stickyDate');

                 if (!e.date && stickyDate) {
                     $(this).datepicker('setDate', stickyDate);
                     $(this).data('stickyDate', null);
                 }
             });
            $('.input-group').find('.fa-calendar').on('click', function () {
                $(this).parent().siblings('#<%=TxtAgFecha2.ClientID%>').trigger('focus');
            });

            $('input[type="checkbox"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });

         };



         function Hack() {
             if (window.event.keyCode == 13) return false;
             $("#<%=TxtAgFecha2.ClientID%>").datepicker({
                 format: "dd/mm/yyyy",
                 autoclose: true,
                 language: 'es',
                 todayBtn: "linked",
                 todayHighlight: true
             });
             $('#<%=TxtAgFecha2.ClientID%>').on('show', function (e) {
                 if (e.date) {
                     $(this).data('stickyDate', e.date);
                 }
                 else {
                     $(this).data('stickyDate', null);
                 }
             });

             $('#<%=TxtAgFecha2.ClientID%>').on('hide', function (e) {
                 var stickyDate = $(this).data('stickyDate');

                 if (!e.date && stickyDate) {
                     $(this).datepicker('setDate', stickyDate);
                     $(this).data('stickyDate', null);
                 }
             });
             function validar(e) {
                 tecla = (document.all) ? e.keyCode : e.which;
                 console.log(tecla);
                 if (tecla == 8 || tecla == 13) return false;
                 return false;
             }
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=TxtAgFecha2.ClientID%>').trigger('focus');
             });
             $('input[type="checkbox"]').addClass("flat-red");
             //Flat red color scheme for iCheck
             $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                 checkboxClass: 'icheckbox_flat-green',
                 radioClass: 'iradio_flat-green'
             });

         }

         function validar(e) {
             tecla = (document.all) ? e.keyCode : e.which;
             console.log(tecla);
             if (tecla == 8 || tecla == 13) return false;
             return false;
         }
    </script>

    <section class="content-header">
        <h1>Bloquear
           
            <small>Agenda</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa   fa-calendar-times-o"></i>Bloqueos de horarios</li>
        </ol>
    </section>
    <section class="content">
    <div class="content-fluid ">
    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Bloqueos de Horarios Agenda</h3>
        </div>
        <!-- /.box-header -->
        <!-- form start -->

        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelBloqueos">
            <ProgressTemplate>
                <div class="overlay">
                    <%-- <i class="fa fa-refresh fa-spin"></i>--%>
                </div>
            </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelBloqueos" runat="server">
            <ContentTemplate>
                  <%--------------------- Avisos en pantalla-------------------------------%>
                    
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
                <%--    ---------------------------------------------------------------------------------------------%>
                <div id="infPaciente" class="box-body">
                    <div class ="row">
                        <div class ="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <div class="form-group">
                                <asp:DropDownList ID="DDconsultorios" runat="server" CssClass="form-control input-lg" AutoPostBack="true"></asp:DropDownList>
                            </div>
                        </div>
                       <div class ="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar fa-2x"></i>
                                    </div>
                                    <asp:TextBox runat="server" ID="TxtAgFecha2" CssClass="form-control datepicker txtAgFecha2 input-lg" OnTextChanged="TxtAgFecha2_TextChanged" AutoPostBack="True" onkeypress="return validar(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Button ID="BtnTodo" runat="server" CssClass="btn btn-block btn-success btn-lg" Text="Seleccionar todo" />
                            </div>
                        </div>
                    </div>
                    <div class ="row">
                        <div class ="col-md-12">
                                <div class="scrolling-table-container">
                                    <asp:Panel ID="Panel1" runat="server" ScrollBars="Both" Height="350px">
                                        <asp:GridView ID="dtgTurnos" runat="server" CssClass="dataTable table-responsive table table-stripede"
                                            AutoGenerateColumns="False"  AllowSorting="True" CellPadding="4" ForeColor="#333333" GridLines="None">
                                            <AlternatingRowStyle BackColor="White" ForeColor="#284775" />
                                            <Columns>
                                                 <%-- 0 --%>
                                                <asp:BoundField DataField="idhorario" HeaderText="idhorario" />
                                                 <%-- 1 --%>
                                                <asp:BoundField DataField="horario" HeaderText="Turno" />
                                                 <%-- 2 --%>
                                                <asp:BoundField HeaderText="Paciente" />
                                                 <%-- 3 --%>
                                                <asp:BoundField HeaderText="Teléfonos" />
                                                 <%-- 4 --%>
                                                <asp:TemplateField HeaderText="Bloqueos">
                                                    <ItemTemplate>     
                                                            <asp:CheckBox ID="chk" runat="server"  />
                                                        <asp:Button ID="BtnCancelar" CssClass="btn btn-block btn-danger btn-xs" runat="server" Text="Cancelar" CommandName="BtnCancelar" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                        <asp:Button ID="BtnMover" runat="server" CssClass="btn btn-block btn-primary" Text="Mover" CommandName="BtnMover" CommandArgument="<%# Container.DataItemIndex %>" Visible="false" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                 <%-- 5 --%>
                                                <asp:BoundField HeaderText="Folio Consulta" />
                                                 <%-- 6 --%>
                                                <asp:BoundField HeaderText="CodigoPaciente" />
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
                                    </asp:Panel>
                                </div>
                        </div>
                    </div>
                    
                </div>
                <!-- /.box-body -->
                <div id="boton" class="box-footer">
                    <button type="submit" class="btn btn-primary" runat="server" onserverclick="BtnBloquear_Click" validationgroup="buscarPaciente"><i class="fa fa-check"></i>Aceptar</button>
                </div>
                <asp:HiddenField ID="HfActualizarOinsertar" runat="server" />
                <asp:HiddenField ID="HdPreguntas" runat="server" />
                <asp:HiddenField ID="HdIndex" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        </div>
    </div>
        </section>
</asp:Content>
