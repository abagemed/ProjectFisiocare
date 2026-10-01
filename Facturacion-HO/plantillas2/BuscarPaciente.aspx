<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="BuscarPaciente.aspx.vb" Inherits="AgeMED.BuscarPaciente" EnableEventValidation="false" %>

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
            }
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Content Header (Page header) -->
    <section class="content-header">
        <h1>Paciente
            <small>Buscar</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="Agenda.aspx"><i class="fa fa-home"></i> Inicio</a></li>
            <li class="active"><i class="fa fa-search"></i> Buscar Paciente</li>
        </ol>
    </section>

    <section class="content">
        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelBusqueda">
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
                         
                    <div class="box-body">
                    <asp:UpdatePanel ID="UpdatePanelBusqueda" runat="server">
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
                        <!------------------------------------->

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
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="LstBoxPacientes" ValidationGroup="seleccionPaciente"><span class="mensaje-error">Por favor, seleccione un paciente</span></asp:RequiredFieldValidator>
                                    </div>
                                </div>
                            </div>

                            
                        </div>
                        <hr>
                        <button type="button" class="btn btn-success btn-lg" runat="server" onserverclick="BtnVer_Click" validationgroup="seleccionPaciente"><i class="fa fa-eye"></i> <span runat="server" id="BtnVerTexto" > Ver</span></button>
                        <asp:HiddenField ID="HdPregunta" runat="server" />
                        <asp:HiddenField ID="Hdfolio" runat="server" />                  
                       
                    </ContentTemplate>
                </asp:UpdatePanel>
                </div>
                </div>   	
            </div>
        </div>
    </section>
</asp:Content>
