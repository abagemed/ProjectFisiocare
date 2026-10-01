<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="ReAgendarPaciente.aspx.vb" Inherits="AgeMED.ReAgendarPaciente1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ MasterType VirtualPath="~/Home.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=TxtAgFecha2.ClientID%>').trigger('focus');
             });
            
         };

        

        function validar(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            console.log(tecla);
            if (tecla == 8 || tecla == 13) return false;
            return false;
        }

        function Hack() {
            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }
             if (window.event.keyCode == 13) return false;
             $("#<%=TxtAgFecha2.ClientID%>").datepicker({
                 format: "dd/mm/yyyy",
                 autoclose: true,
                 language: 'es',
                 todayBtn: "linked",
                 todayHighlight: true
             });
             $('.input-group').find('.fa-calendar').on('click', function () {
                 $(this).parent().siblings('#<%=TxtAgFecha2.ClientID%>').trigger('focus');
             });
         }

         
    </script>

    <style>
    .txtAgFecha2, .input-group-addon{
        cursor:pointer;
    }
    
  </style>

     <section class="content-header">
        <h1>Reagendar
           
            <small>Paciente</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa  fa-calendar"></i> ReAgendarPaciente</li>
        </ol>
    </section>
    <section class="content">
    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Re-Agendar Paciente</h3>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
               <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelReAgenda">
                   <ProgressTemplate>
                       <div class="preloader">
                           <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                       </div>
                   </ProgressTemplate>
        </asp:UpdateProgress>
        <asp:UpdatePanel ID="UpdatePanelReAgenda" runat="server">
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
                                    <asp:Label ID="LblMensajeDecision" runat="server" Text=""></asp:Label></h4>
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
                <!-- PANEL AVISOS-->

                <!--HACK-->
                <img src="dist/img/hack.jpg" onload="Hack()" class="hidden"/>



                <div id="infPaciente" class="box-body">
                    <div class="row ">
                        <div class ="col-md-6"> <td><h4><asp:Label ID="LblNombrePaciente" runat="server" Text="Nombre de paciente"></asp:Label></h4></td></div>
                        <div class="col-md-6"> <td><h4><asp:Label ID="LblCodigoPaciente" runat="server" Text="0000000"></asp:Label></h4></td></div>
                    </div>
                    <div class="row ">
                        <div class ="col-md-6">
                             <h5>CONSULTORIO</h5>
                                <asp:DropDownList ID="DDconsultorios" runat="server" CssClass="form-control input-lg"  AutoPostBack ="true"></asp:DropDownList>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <div class="form-group">
                                <h5>FECHA</h5>
                                <div class="input-group">
                                    <div class="input-group-addon">
                                        <i class="fa fa-calendar fa-2x"></i>
                                    </div>
                                    <asp:TextBox runat="server" ID="TxtAgFecha2" CssClass="form-control datepicker txtAgFecha input-lg" OnTextChanged="TxtAgFecha2_TextChanged" AutoPostBack="True" onkeypress="return validar(event)"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-md-6">
                            <h5>FECHA</h5>
                            <%--asp:TextBox runat="server" ID="TxtAgFecha2" CssClass="form-control datepicker txtAgFecha input-lg" OnTextChanged="TxtAgFecha2_TextChanged" AutoPostBack="True" onkeypress="return validar(event)"></%--asp:TextBox>
                                <asp:Image ID="calendario" runat="server" CssClass="icono" ImageUrl="../Resource/calendar.png" Width="2em"  />                                                      
                                <asp:TextBox ID="TxtAgFecha2" runat="server" BorderStyle ="None" BackColor ="Transparent" Enabled ="false" OnTextChanged ="TxtAgFecha2_TextChanged" AutoPostBack ="true"  ></asp:TextBox--%>                          
                        </div>
                    </div>
                    <div class ="row">
                        <div class="col-md-12">
                            <h5>TURNOS DISPONIBLES</h5>
                                <asp:DropDownList ID="DDturnos" runat="server" CssClass="form-control " ></asp:DropDownList>
                        </div>
                    </div>
                    
                    <%--ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server" PopupButtonID="calendario"
                        TargetControlID="TxtAgFecha2" Format="dd/MM/yyyy" StartDate="08/14/2016" EndDate ="1/1/2019"/ --%>
                </div>
                <!-- /.box-body -->
                <div id="boton" class="box-footer">
                <button type="button" class="btn btn-primary" runat="server" onserverclick="BtnReAgendar_Click" validationgroup="buscarPaciente"><i class="fa fa-check"></i>Aceptar</button>
                    
                </div>
                <asp:HiddenField ID="HdPregunta" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
        <!-- /.box-footer -->
        <%-- </form>--%>
    </div>
        </section>

</asp:Content>
