<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="ConsultaEx.aspx.vb" Inherits="AgeMED.ConsultaEx" ValidateRequest="false" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=10.2.3600.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">


    <script src="plugins/wheelzoom-master/wheelzoom.js"></script>


    <script>        
        $(document).ready(function() {

           
            //summerNote
            $('.summernote').summernote({ 
                lang: 'es-ES' ,
                toolbar: [
         // [groupName, [list of button]]
         ['fuente',['fontname']],
           ['fontsize', ['fontsize']],
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],  
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
         ['table', ['table']],
          ['imagen', ['picture']],
           ['pantallaCompleta', ['fullscreen','undo','redo','help']]
                ]
            });
           
            //Abrir o cerrar el panel de diagnosticos y pagos
            if($("#<%=Dgdiagnosticos.ClientID%> tr").length > 1)
            {
                $("#box_diagnostico").removeClass("collapsed-box");
            }
            if($("#<%=GdvConceptoPagos.ClientID%> tr").length > 1)
            {
                $("#box_pagos").removeClass("collapsed-box");
            }
            //Plugins
            $(".ListJustificacion").select2({
                placeholder: "Escriba el nombre de la Justificación",
                width: null,
                containerCssClass: ':all:'
            });
            $(".ListMedicamentos").select2({
                placeholder: "Escriba el nombre del Medicamento",
                width: null,
                containerCssClass: ':all:'
            });
            $(".Lstdiagnosticos").select2({
                placeholder: "Escriba el nombre del Diagnóstico",
                width: null,
                containerCssClass: ':all:',
            });
            $(".Listsalnm").select2({
                placeholder: "Escriba la Formula",
                width: null,
                containerCssClass: ':all:'
            });
            $(".LstConceptoPagos").select2({
                placeholder: "Escriba el concepto a cobrar",
                width: null,
                containerCssClass: ':all:'
            });
            $('input[type="radio"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            $( ".estudios" ).select2({ placeholder: "Estudio RX", maximumSelectionSize: 6 } );
            //Plugins
        
            //Guardado de Expediente Médico mediante AJAX
            $('.PadecimientoActual').change(function() {
                console.log( "PadecimientoActual" );
                Guardar();
            });

            $('.Patologicos').change(function() {
                console.log( "Patologicos" );
                Guardar();
            });

            $('.NoPatologicos').change(function() {
                console.log( "NoPatologicos" );
                Guardar();
            });

            $('.Alergias').change(function() {
                console.log( "Alergias" );
                Guardar();
            });

            $('.Exploracion').change(function() {
                console.log( "Exploracion" );
                Guardar();
            });

            $('.Plan').change(function() {
                console.log( "Plan" );
                Guardar();
            });

            $('.TensionArterial').change(function() {
                console.log( "TensionArterial" );
                Guardar();
            });

            $('.FrecuenciaCardiaca').change(function() {
                console.log( "FrecuenciaCardiaca" );
                Guardar();
            });

            $('.Peso').change(function() {
                console.log( "Peso" );
                Guardar();
            });

            $('.Estatura').change(function() {
                console.log( "Estatura" );
                Guardar();
            });

            //Guardado de Justificacion mediante AJAX
            //$('.summernote').change(function() {
            //    console.log( "summernote" );
            //    Guardar();
            //});


            //Funcion para iniciar Plugin de imágenes RX y archivos
            IniciarPluginFileInput();
            console.log("Document ready!");
           
        });


        Sys.WebForms.PageRequestManager.getInstance().add_endRequest(endReq);
        function endReq(sender, args) 
        { 
            console.log("Request!");
        
            if (window.location.pathname != "/Agenda.aspx") {
                $(".messages-menu").addClass("hidden");
                $(".notifications-menu").addClass("hidden");
                $(".tasks-menu").addClass("hidden");
            }
            if($("#<%=Dgdiagnosticos.ClientID%> tr").length > 1)
            {
                $("#box_diagnostico").removeClass("collapsed-box");
            }
            if($("#<%=GdvConceptoPagos.ClientID%> tr").length > 1)
            {
                $("#box_pagos").removeClass("collapsed-box");
            }
        
            //Plugins
           
            //summerNote
            $('.summernote').summernote({
                lang: 'es-ES' ,
                toolbar: [
         // [groupName, [list of button]]
         ['fuente',['fontname']],
           ['fontsize', ['fontsize']],
        ['style', ['bold', 'italic', 'underline', 'clear']],
        ['font', ['strikethrough', 'superscript', 'subscript']],  
        ['color', ['color']],
        ['para', ['ul', 'ol', 'paragraph']],
        ['height', ['height']],
         ['table', ['table']],
          ['imagen', ['picture']],
           ['pantallaCompleta', ['fullscreen','undo','redo','help']]
                ]
            });
                
                                                         
            $(".ListJustificacion").select2({
                placeholder: "Escriba el nombre de la Justificación",
                width: null,
                containerCssClass: ':all:'
            });
            $(".ListMedicamentos").select2({
                placeholder: "Escriba el nombre del Medicamento",
                width: null,
                containerCssClass: ':all:'
            });

            $(".Lstdiagnosticos").select2({
                placeholder: "Escriba el nombre del Diagnóstico",
                width: null,
                containerCssClass: ':all:'
            });

            $(".Listsalnm").select2({
                placeholder: "Escriba la Formula",
                width: null,
                containerCssClass: ':all:'
            });

            $(".LstConceptoPagos").select2({
                placeholder: "Escriba el concepto a cobrar",
                width: null,
                containerCssClass: ':all:'
            });

            $(".estudios").select2({ placeholder: "Estudio RX", maximumSelectionSize: 6 } );

            $('input[type="radio"]').addClass("flat-red");
            //Flat red color scheme for iCheck
            $('input[type="checkbox"].flat-red, input[type="radio"].flat-red').iCheck({
                checkboxClass: 'icheckbox_flat-green',
                radioClass: 'iradio_flat-green'
            });
            //Plugins


            //Guardado de Expediente Médico mediante AJAX
            $('.PadecimientoActual').change(function() {
                console.log( "PadecimientoActual" );
                Guardar();
            });

            $('.Patologicos').change(function() {
                console.log( "Patologicos" );
                Guardar();
            });

            $('.NoPatologicos').change(function() {
                console.log( "NoPatologicos" );
                Guardar();
            });

            $('.Alergias').change(function() {
                console.log( "Alergias" );
                Guardar();
            });

            $('.Exploracion').change(function() {
                console.log( "Exploracion" );
                Guardar();
            });

            $('.Plan').change(function() {
                console.log( "Plan" );
                Guardar();
            });

            $('.TensionArterial').change(function() {
                console.log( "TensionArterial" );
                Guardar();
            });

            $('.FrecuenciaCardiaca').change(function() {
                console.log( "FrecuenciaCardiaca" );
                Guardar();
            });

            $('.Peso').change(function() {
                console.log( "Peso" );
                Guardar();
            });

            $('.Estatura').change(function() {
                console.log( "Estatura" );
                Guardar();
            });

            //Guardado de Justificacion mediante AJAX
            //$('.summernote').change(function() {
            //    console.log( "summernote" );
            //    Guardar();
            //});
                                   

        }
        var prm = Sys.WebForms.PageRequestManager.getInstance();


        function Guardar()
        {
            $.ajax({
                type: 'POST',
                url:"/guardar.ashx",
                data:{
                    PadecimientoActual : $('.PadecimientoActual').val(),
                    Exploracion : $('.Exploracion').val(),
                    Plan : $('.Plan').val(),
                    Patologicos : $('.Patologicos').val(),
                    NoPatologicos : $('.NoPatologicos').val(),
                    Alergias : $('.Alergias').val(),
                    TensionArterial : $('.TensionArterial').val(),
                    FrecuenciaCardiaca : $('.FrecuenciaCardiaca').val(),
                    Peso : $('.Peso').val(),
                    Estatura : $('.Estatura').val(),
                    IMC : $('.IMC').val(),
                    Hffolioconsulta : $('#<%=Hffolioconsulta.ClientID%>').val(),
                    HFCodPaciente : $('#<%=HFCodPaciente.ClientID%>').val(),
                   <%-- summernote : $('.summernote').val(),
                    HFFolioJustificacion : $('#<%=HFFolioJustificacion.ClientID%>').val()--%>
                },
                success: function(data)
                {
                    console.log(data);
                    var obj = data;
                    $('.IMC').val(obj.imc)
                    $('.imc_resultado').empty();
                    $('.imc_resultado').append(obj.resultado);
                }
            
            });

        }

  

        function IniciarPluginFileInput() {
            //+++++++++++++++ cargar imagen +++++++++++++++++++                       
            $("#input-image-1").fileinput({
                language:'es',
                uploadAsync: true,
                browseOnZoneClick: true,
                autoReplace: true,
                initialPreviewAsData: true, // identify if you are sending preview data only and not the raw markup
                initialPreviewFileType: 'image', // image is the default and can be overridden in config below
                initialPreview: <%=ImagenRX.Value.ToString%>,
                initialPreviewConfig:<%=PreviusRX.Value.ToString %>,
                overwriteInitial: false,
                dataType: 'POST',
                uploadUrl: "Handler.ashx",
                deleteUrl: "Handler.ashx",
                allowedFileExtensions: ["jpg", "jpeg", "png", "gif"],
                maxImageWidth: 800,
                maxImageHight: 1500,
                minFileCount:1,
                maxFileCount: 5,
                maxFileSize:200000,
                maxFilePreviewSize: 200000,
                resizeImage: true,
                elErrorContainer: "#errorBlock"
            });
            $("#input-image-1").on('filepreupload', function () {
                $('#kv-success-box').html('');
            });
            $("#input-image-1").on('filezoomshown', function(event, params) {
                wheelzoom(document.querySelectorAll('.file-zoom-detail'));
            });
            $("#input-image-1").on('filezoomprev', function(event, params) {
                setTimeout(
                function() 
                {
                    console.log('File zoom prev 2 segundos despues ');
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.reset'));
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.destroy'));
                    wheelzoom(document.querySelectorAll('.file-zoom-detail'));
                }, 500);

                
            });
            $("#input-image-1").on('filezoomnext', function(event, params) {
                setTimeout(
                function() 
                {
                    console.log('File zoom prev 2 segundos despues ');
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.reset'));
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.destroy'));
                    wheelzoom(document.querySelectorAll('.file-zoom-detail'));
                }, 500);

          
            });
       
            //+++++++++++++++ cargar Video +++++++++++++++++++                       
            $("#input-image-2").fileinput({
                initialPreview: <%=Video.Value.ToString%>,
                language:'es',
                uploadAsync: true,
                browseOnZoneClick: true,
                //autoReplace: true,
                initialPreviewAsData: true, // identify if you are sending preview data only and not the raw markup
                initialPreviewFileType: 'image', // image is the default and can be overridden in config below
                initialPreviewConfig:<%=PreviusVideos.Value.ToString%>,
                overwriteInitial: false,
                dataType: 'POST',
                uploadUrl: "HandlerVideo.ashx",
                deleteUrl: "HandlerVideo.ashx",
                allowedFileExtensions: ["mp4", "pdf", "txt", "jpg", "jpeg", "png", "gif"],
                //maxImageWidth: 800,
                //maxImageHight: 1500,
                minFileCount:1,
                maxFileCount: 5,
                maxFileSize:200000,
                maxFilePreviewSize: 200000,
                resizeImage: true,
                elErrorContainer: "#errorBlock-2"
            });
            $("#input-image-2").on('filepreupload', function () {
                $('#kv-success-box-2').html('');
            });
            $("#input-image-2").on('fileuploaded', function (event, data) {
                $('#kv-success-box-2').append(data.response.link);
            });
            $("#input-image-2").on('filezoomprev', function(event, params) {
                console.log('File zoom prev ');
                setTimeout(
                function() 
                {
                    console.log('File zoom prev 2 segundos despues ');
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.reset'));
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.destroy'));
                    wheelzoom(document.querySelectorAll('.file-zoom-detail'));
                }, 500);

                
            });
            $("#input-image-2").on('filezoomnext', function(event, params) {
                console.log('File zoom next ');

                setTimeout(
                function() 
                {
                    console.log('File zoom prev 2 segundos despues ');
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.reset'));
                    document.querySelector('.file-zoom-detail').dispatchEvent(new CustomEvent('wheelzoom.destroy'));
                    wheelzoom(document.querySelectorAll('.file-zoom-detail'));
                }, 500);

            });            
        }

    

    
        function ValSoloNumeros(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
    </script>

    <style type="text/css">
        .radio label {
            padding-left: 5px;
            padding-right: 10px;
        }
    </style>

    <section class="content">
        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div class="form-group">
                    <button type="button" class="btn btn-lg btn-bitbucket btn-block" runat="server" onserverclick="Lnkbackagenda_Click"><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</button>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="form-group">
                    <button type="button" id="finalizar1" class="btn btn-lg btn-facebook btn-block" runat="server" onserverclick="LnkFinalizar_Click"><i class="fa fa-calendar-check-o"></i> Finalizar Cita</button>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                <div class="form-group">
                    <button type="button" id="cmdimprimirexp" class="btn btn-lg btn-twitter btn-block" runat="server" onserverclick="cmdimprimirexp_Click"><i class="fa fa-print"></i> Imprimir Expediente</button>
                </div>
            </div>
        </div>


        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelPrincipal">
                        <ProgressTemplate>
                            <%--PRELOADER--%>
                            <div class="preloader">
                                <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanelPrincipal" runat="server">
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

                            <asp:HiddenField ID="HdPreguntas" runat="server" />
                            <asp:HiddenField ID="Hffolioconsulta" runat="server" />
                            <asp:HiddenField ID="hfFechaAgenda" runat="server" />
                            <asp:HiddenField ID="HFCodPaciente" runat="server" />
                            <asp:HiddenField ID="HdEliminaPago" runat="server" Value="0" />
                            <asp:HiddenField ID="HdCantidad_a_descontar" runat="server" Value="0" />
                            <asp:HiddenField ID="HFFolioJustificacion" runat="server" Value="0" />

                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <div class="box-header with-border">
                        <h3 class="box-title">Datos del paciente</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="row invoice-info">
                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 invoice-col">
                                <strong>Paciente</strong>
                                <address>
                                    <asp:Label ID="lblpaciente" runat="server"></asp:Label>
                                    <button class="btn btn-success btn-xs" runat="server" id="editar_paciente" onserverclick="DatosPaciente"><i class="fa fa-pencil"></i></button>
                                    <br>
                                    <asp:Label ID="lbledad" runat="server"></asp:Label><br>
                                    <asp:Label ID="Lblfechanacimiento" runat="server"></asp:Label><br>
                                    <asp:Label ID="Lblgenero" runat="server"></asp:Label><br>
                                    <asp:Label ID="lblocupacion" runat="server"></asp:Label><br>
                                    <asp:Label ID="lblestadocivil" runat="server"></asp:Label><br>
                                    <%--PARA COPIAR--%>
                                    <asp:Label ID="Lbldireccion" runat="server"></asp:Label><br>
                                    <%--FIN--%>
                                    <asp:Label ID="Lblnacionalidad" runat="server"></asp:Label><br>
                                </address>
                            </div>
                            <!-- /.col -->
                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 invoice-col">
                                <strong>Información de consulta</strong>
                                <address>
                                    Folio:
                                    <asp:Label ID="Lblfolioconsulta" runat="server"></asp:Label><br>
                                    Agendo:
                                    <asp:Label ID="lblagendo" runat="server"></asp:Label><br>
                                    Atiende:
                                    <asp:Label ID="LblDrAtiende" runat="server"></asp:Label>
                                    <asp:Label ID="lblfagenda" runat="server" Visible="false"></asp:Label><br>
                                    Tipo paciente:
                                      <asp:Label ID="LblTipoPaciente" runat="server"></asp:Label><br>
                                    <strong>Aseguradora:</strong>
                                    <asp:Label ID="LblAseguradora" runat="server"></asp:Label><br>
                                </address>
                                <div>
                                    <strong>Etapa De Consulta</strong>
                                    <address style="align-content: center">
                                        <asp:Label ID="imgEstadoC" runat="server"></asp:Label>
                                        <asp:Label ID="lbEtapaC" runat="server" Font-Italic="False" Font-Names="Calibri" Font-Size="11pt"></asp:Label>
                                        <asp:Label ID="lbidEtapaC" runat="server" Visible="False"></asp:Label>
                                    </address>
                                </div>

                            </div>
                            <!-- /.col -->
                            <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4 invoice-col">

                                <b>Historial de citas</b><br>
                                <div class="container-fluid">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            Fecha: 
                                            <asp:DropDownList ID="DDHfecha" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-lg-12 col-md-12 col-sm-6 col-xs-6">
                                            <div class="radio radiobuttonlist">
                                                <asp:RadioButtonList ID="RbtListExtremidades" runat="server" RepeatDirection="Horizontal">
                                                    <asp:ListItem Selected="True">Derecha</asp:ListItem>
                                                    <asp:ListItem>Izquierda</asp:ListItem>
                                                    <asp:ListItem>Ambos</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </div>
                                        </div>
                                    </div>
                                    <asp:UpdatePanel ID="UpdatePanelEstudiosRX" runat="server">
                                        <ContentTemplate>
                                            <div class="row">
                                                <div class="col-lg-12">
                                                    <asp:ListBox ID="LstEstudiosRX" class="form-control select2 estudios" runat="server" SelectionMode="Multiple"></asp:ListBox>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                    <div class="row" style="margin-top: 20px">
                                        <div class="col-lg-12">
                                            <asp:Button ID="btnEnviarEG" runat="server" CssClass="btn btn- bg-fuchsia-active btn-block" Text="Enviar A Estudios"></asp:Button>
                                        </div>
                                    </div>
                                </div>

                            </div>
                            <!-- /.col -->
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- *************DIAGNOSTICO DE LA CONSULTA********************** -->

        <%--------------------- Inicio del proceso de diagnostico-------------------------------%>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info" id="box_diagnostico">
                    <div class="box-header with-border">
                        <h3 class="box-title">Diagnóstico de la consulta</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelDiagnosticos">
                        <ProgressTemplate>
                            <%--PRELOADER--%>
                            <div class="preloader">
                                <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                    <asp:UpdatePanel ID="UpdatePanelDiagnosticos" runat="server">
                        <ContentTemplate>
                            <%--------------------- Avisos en pantalla para Diagnosticos-------------------------------%>

                            <asp:Panel ID="PanelAvisosdiag" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="alert alert-success alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                            <h4>
                                                <asp:Label ID="LblMensajeAvisodiag" runat="server"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="PanelDesiciondiag" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="alert alert-info alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                            <h4>
                                                <asp:Label ID="LblMostarDecisiondiag" runat="server" Text="Label"></asp:Label></h4>
                                            <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSidiag_Click"><i class="fa fa-check"></i>Sí</button>
                                            <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNodiag_Click"><i class="fa fa-close"></i>No</button>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="PanelCriticodiag" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="alert alert-danger alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                            <h4>
                                                <asp:Label ID="LblMensajeCriticodiag" runat="server" Text="Label"></asp:Label>></h4>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <asp:Panel ID="PanelAdvertenciadiag" runat="server" Visible="false">
                                <div class="row">
                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                        <div class="alert alert-warning alert-dismissable">
                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                            <h4>
                                                <asp:Label ID="LblMensajeAdvertenciadiag" runat="server" Text="Label"></asp:Label></h4>
                                        </div>
                                    </div>
                                </div>
                            </asp:Panel>

                            <%--    ---------------------------------------------------------------------------------------------%>
                            <asp:HiddenField ID="HdPreguntasdiag" runat="server" />
                            <asp:HiddenField ID="HdIndex" runat="server" />
                            <asp:HiddenField ID="HdPreguntasdiagpp" runat="server" />
                            <asp:HiddenField ID="HdIndexpp" runat="server" />
                        </ContentTemplate>
                    </asp:UpdatePanel>

                    <div class="box-body">
                        <div class="row container-fluid">
                            <%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                        <ContentTemplate>
                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 table-responsive">
                                            <div runat="server" id="ultimo_diagnostico">

                                            </div>
                                            
                                            <asp:GridView ID="GvdiagnosticosH" runat="server" Visible="true" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
                                                <Columns>

                                                    <asp:BoundField DataField="codigodiagnostico" HeaderText="cod" Visible="false" InsertVisible="true"></asp:BoundField>
                                                    <asp:BoundField DataField="codigolistadiagnostico" HeaderText="codlist" Visible="false" InsertVisible="true"></asp:BoundField>

                                                    <asp:BoundField DataField="codigoexterno" HeaderText="Código"></asp:BoundField>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                                                    
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <h4>El paciente no tiene Diagnostico</h4>
                                                </EmptyDataTemplate>
                                            </asp:GridView>
                                        </div>
                                            </ContentTemplate>
                                             </asp:UpdatePanel>--%>

                            <div id="contenedor_diagnosticos" runat="server">
                                <h4 class="modal-title">Búsqueda</h4>
                                <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                    <ContentTemplate>
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <asp:ListBox ID="Lstdiagnosticos" runat="server" AutoPostBack="True" CssClass="form-control Lstdiagnosticos select2-single"></asp:ListBox>
                                                </div>
                                            </div>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="Gvdiagnosticosps" runat="server" Visible="False" GridLines="none" AutoGenerateColumns="false" DataKey="codigodiagnostico">
                                                <Columns>
                                                    <asp:BoundField DataField="codigodiagnostico" HeaderText="cod" Visible="false" InsertVisible="true"></asp:BoundField>
                                                    <asp:BoundField DataField="codigolistadiagnostico" HeaderText="codlist" Visible="false" InsertVisible="true"></asp:BoundField>
                                                    <asp:BoundField DataField="folioconsulta" HeaderText="Folio" Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="codigoexterno" HeaderText="Codigo" ItemStyle-Width="10%"></asp:BoundField>
                                                    <asp:BoundField DataField="descripcion" HeaderText="Descripcion"></asp:BoundField>
                                                    <asp:TemplateField ItemStyle-Width="4%" ItemStyle-Wrap="true">
                                                        <ItemTemplate>
                                                            <asp:ImageButton runat="server" ImageUrl="../resource/eliminar.png" Height="1.5em" CommandName="borrar"
                                                                CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>" Style="float: right; padding-right: .2em"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <EmptyDataTemplate>
                                                    <center>
                                                            NO EXISTEN DIAGNÓSTICOS.
                                                        </center>
                                                </EmptyDataTemplate>
                                                <RowStyle BackColor="#E6E6E6" BorderStyle="Solid" BorderColor="White" BorderWidth="2px" />
                                            </asp:GridView>
                                        </div>
                                    </div>

                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 table-responsive">
                                        <!--h4 class="modal-title">Agregados</h4-->
                                        <asp:GridView ID="Dgdiagnosticos" runat="server" Visible="true" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
                                            <Columns>
                                                <asp:BoundField DataField="codigodiagnostico" HeaderText="cod" Visible="false" InsertVisible="true"></asp:BoundField>
                                                <asp:BoundField DataField="codigoexterno" HeaderText="Código"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                                                <asp:BoundField DataField="FechaAgenda" HeaderText="Fecha Consulta"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandName="borrar" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                            class="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h4>No hay diagnósticos asignados</h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>

                </div>
            </div>
        </div>




        <!-- ****************HISTORIAL***************** -->
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Historial</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-minus"></i></button>
                        </div>
                    </div>
                    <div class="box-body">
                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li class="active"><a href="#expediente" data-toggle="tab">Expediente</a></li>
                                <li><a href="#mediciones" data-toggle="tab">Mediciones</a></li>
                                <li><a href="#motivo_consulta" data-toggle="tab">Motivo de consulta</a></li>
                            </ul>

                            <div class="tab-content">
                                <div class="active tab-pane" id="expediente">
                                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelMed">
                                        <ProgressTemplate>
                                            <div class="overlay">
                                                <i class="fa fa-refresh fa-spin"></i>
                                            </div>
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                    <asp:UpdatePanel ID="UpdatePanelExp" runat="server">
                                        <ContentTemplate>
                                            <div class="form-body">
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>Padecimiento Actal</label>
                                                        <asp:TextBox ID="txtPadecimientoActual" runat="server" TextMode="MultiLine" CssClass="form-control PadecimientoActual" MaxLength='1000' onkeyDown="VerificaTextAreaMaxLength(this,event,'1000');" Rows="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>Última Consulta</label>
                                                        <asp:TextBox ID="txtAntecedentes" runat="server" TextMode="MultiLine" CssClass="form-control" ReadOnly="true" Rows="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>Tratamiento Previo</label>
                                                        <asp:TextBox ID="txtTratamientoPrevio" runat="server" TextMode="MultiLine" CssClass="form-control" ReadOnly="true" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>Patológicos (APP)</label>
                                                        <asp:TextBox ID="txtPatologicos" runat="server" TextMode="MultiLine" CssClass="form-control Patologicos" MaxLength='500' onkeyDown="VerificaTextAreaMaxLength(this,event,'500');" Rows="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>No Patológicos (APNP)</label>
                                                        <asp:TextBox ID="txtNoPatologicos" runat="server" TextMode="MultiLine" CssClass="form-control NoPatologicos" MaxLength='500' onkeyDown="VerificaTextAreaMaxLength(this,event,'500');" Rows="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                        <label>Alergías</label>
                                                        <asp:TextBox ID="txtAlergias" runat="server" TextMode="MultiLine" CssClass="form-control Alergias" MaxLength='500' onkeyDown="VerificaTextAreaMaxLength(this,event,'500');" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <label>Exploración</label>
                                                        <asp:TextBox ID="txtExploracion" runat="server" TextMode="MultiLine" CssClass="form-control Exploracion" MaxLength='1000' onkeyDown="VerificaTextAreaMaxLength(this,event,'1000');" Rows="4"></asp:TextBox>
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                        <label>Plan a seguir</label>
                                                        <asp:TextBox ID="txtPlan" runat="server" TextMode="MultiLine" CssClass="form-control Plan" MaxLength='1000' onkeyDown="VerificaTextAreaMaxLength(this,event,'1000');" Rows="4"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>
                                <!-- EXPEDIENTE -->
                                <div class="tab-pane" id="mediciones">
                                    <div class="form-body">
                                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelMed">
                                            <ProgressTemplate>
                                                <div class="overlay">
                                                    <i class="fa fa-refresh fa-spin"></i>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="UpdatePanelMed" runat="server">
                                            <ContentTemplate>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-2">Tensión Arterial</label>
                                                        <div class="col-md-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fa fa-stethoscope fa-fw"></i></span>
                                                                <asp:TextBox ID="TxtTension" CssClass="form-control TensionArterial" placeholder="Tensión Arterial" runat="server" onkeypress="return ValNumerosCE(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-2">Frecuencia Cardíaca</label>
                                                        <div class="col-md-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fa fa-heartbeat fa-fw"></i></span>
                                                                <asp:TextBox ID="Txtfrecuencia" CssClass="form-control FrecuenciaCardiaca" placeholder="Frecuencia Cardíaca" runat="server" onkeypress="return ValNumeros(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-2">Peso (Kg)</label>
                                                        <div class="col-md-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fa fa-balance-scale fa-fw"></i></span>
                                                                <asp:TextBox ID="Txtpeso" CssClass="form-control Peso" placeholder="Peso (Kg)" runat="server" onkeypress="return ValNumerosP(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-2">Estatura (m)</label>
                                                        <div class="col-md-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fa fa-long-arrow-up fa-fw"></i></span>
                                                                <asp:TextBox ID="txttalla" CssClass="form-control Estatura" placeholder="Estatura (m)" runat="server" onkeypress="return ValNumerosP(event)"></asp:TextBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="row">
                                                        <label class="col-md-2">I.M.C. <a class="btn" data-toggle="modal" data-target="#ayuda_imc"><i class="fa fa-question-circle"></i></a></label>
                                                        <div class="col-md-10">
                                                            <div class="input-group">
                                                                <span class="input-group-addon"><i class="fa fa-hashtag fa-fw"></i></span>
                                                                <asp:TextBox ID="TxtImc" CssClass="form-control IMC" value="Índice de Masa Corporal" runat="server" Enabled="False" onkeypress="return ValNumerosP(event)"></asp:TextBox>
                                                            </div>
                                                            <h4 runat="server" id="imc_resultado" class="imc_resultado"></h4>
                                                        </div>
                                                    </div>
                                                </div>
                                                <div class="modal fade" id="ayuda_imc" tabindex="-1" role="dialog" aria-labelledby="myModalLabel">
                                                    <div class="modal-dialog" role="document">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                                <h4 class="modal-title" id="myModalLabel">Índice de Masa Corporal</h4>
                                                            </div>
                                                            <div class="modal-body">
                                                                <div class="table-responsive">
                                                                    <table class="table table-striped">
                                                                        <tbody>
                                                                            <tr>
                                                                                <td><strong><span>Índice de Masa Corporal (IMC)</span></strong></td>
                                                                                <td><strong><span>Clasificación</span></strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Menor a  18</td>
                                                                                <td>Peso bajo. Necesario valorar signos de desnutrición</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>18 a 24.9</td>
                                                                                <td>&nbsp;Normal</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>25 a 26.9</td>
                                                                                <td><strong>&nbsp;Sobrepeso</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Mayor a 27</td>
                                                                                <td><strong>&nbsp;Obesidad</strong></td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>27 a 29.9</td>
                                                                                <td><strong>&nbsp;Obesidad grado I.</strong> Riesgo relativo <strong>alto</strong> para desarrollar enfermedades cardiovasculares</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>30 a 39.9</td>
                                                                                <td><strong>&nbsp;Obesidad grado II.</strong> Riesgo relativo <strong>muy alto</strong> para el desarrollo de enfermedades cardiovasculares</td>
                                                                            </tr>
                                                                            <tr>
                                                                                <td>Mayor a 40</td>
                                                                                <td><strong>Obesidad grado III Extrema o Mórbida.</strong> Riesgo relativo <strong>extremadamente alto</strong> para el desarrollo de enfermedades cardiovasculares</td>
                                                                            </tr>
                                                                        </tbody>
                                                                    </table>
                                                                </div>
                                                            </div>
                                                            <div class="modal-footer">
                                                                <button type="button" class="btn btn-default" data-dismiss="modal">Cerrar <i class="fa fa-close"></i></button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- MEDICIONES -->


                                <div class="tab-pane" id="motivo_consulta">
                                    <div class="form-body">
                                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelHM2">
                                            <ProgressTemplate>
                                                <div class="overlay">
                                                    <i class="fa fa-refresh fa-spin"></i>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <asp:UpdatePanel ID="UpdatePanelHM2" runat="server">
                                            <ContentTemplate>
                                                <!-- script agregado JeVC-->
                                                <script>
                                                    function VerificaTextAreaMaxLength(textBox, e, length) {

                                                        var mLen = textBox["MaxLength"];
                                                        if (null == mLen)
                                                            mLen = length;

                                                        var maxLength = parseInt(mLen);
                                                        if (!checkSpecialKeys(e)) {
                                                            if (textBox.value.length > maxLength - 1) {
                                                                if (window.event)//IE
                                                                    e.returnValue = false;
                                                                else//Firefox
                                                                    e.preventDefault();
                                                            }
                                                        }
                                                    }
                                                    function checkSpecialKeys(e) {
                                                        if (e.keyCode != 8 && e.keyCode != 46 && e.keyCode != 37 && e.keyCode != 38 && e.keyCode != 39 && e.keyCode != 40)
                                                            return false;
                                                        else
                                                            return true;
                                                    }
                                                </script>
                                                <!-- script agregado JeVC-->
                                                <script type="text/javascript">

                                                    function ValNumerosCE(e) {
                                                        tecla = (document.all) ? e.keyCode : e.which;
                                                        if (tecla == 8) return true;
                                                        patron = /[0-9 /-]/;
                                                        te = String.fromCharCode(tecla);
                                                        return patron.test(te);
                                                    }

                                                    function ValNumeros(e) {
                                                        tecla = (document.all) ? e.keyCode : e.which;
                                                        if (tecla == 8) return true;
                                                        patron = /[0-9]/;
                                                        te = String.fromCharCode(tecla);
                                                        return patron.test(te);
                                                    }

                                                    function ValNumerosP(e) {
                                                        tecla = (document.all) ? e.keyCode : e.which;
                                                        if (tecla == 8) return true;
                                                        patron = /[0-9 .]/;
                                                        te = String.fromCharCode(tecla);
                                                        return patron.test(te);
                                                    }

                                                </script>


                                                <ajaxToolkit:TabContainer ID="TabContainer1" runat="server" ForeColor="Black" CssClass="no-border" ActiveTabIndex="0">

                                                    <ajaxToolkit:TabPanel ID="TabPanel2" runat="server">
                                                        <ContentTemplate>
                                                            <%--PANEL DE AVISOS--%>
                                                            <asp:Panel ID="PanelAvisosex" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                        <div class="alert alert-success alert-dismissable">
                                                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                            <h4>
                                                                                <asp:Label ID="LblMensajeAvisoex" runat="server"></asp:Label></h4>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="PanelDesicionex" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                        <div class="alert alert-info alert-dismissable">
                                                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                            <h4>
                                                                                <asp:Label ID="LblMostarDecisionex" runat="server" Text="Label"></asp:Label></h4>
                                                                            <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSiex_Click"><i class="fa fa-check"></i>Sí</button>
                                                                            <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNoex_Click"><i class="fa fa-close"></i>No</button>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="PanelCriticoex" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                        <div class="alert alert-danger alert-dismissable">
                                                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                            <h4>
                                                                                <asp:Label ID="LblMensajeCriticoex" runat="server" Text="Label"></asp:Label>></h4>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <asp:Panel ID="PanelAdvertenciaex" runat="server" Visible="false">
                                                                <div class="row">
                                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                                        <div class="alert alert-warning alert-dismissable">
                                                                            <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                            <h4>
                                                                                <asp:Label ID="LblMensajeAdvertenciaex" runat="server" Text="Label"></asp:Label></h4>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>

                                                            <%--    --------------------------fin de avizos mediciones/Extreme---------------------------------%>

                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                <ContentTemplate>

                                                                    <div class="row">
                                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                            <asp:Image ID="ImgExtremidades" CssClass="img-responsive" runat="server" />
                                                                        </div>
                                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                                            <div class="row container-fluid">
                                                                                <label>Seleccionadas</label>
                                                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 table-responsive">
                                                                                    <asp:DataGrid ID="Dgextremidades" runat="server" AutoGenerateColumns="false" CssClass="table table-striped" GridLines="None">
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="codigoextremidad" HeaderText="cod" Visible="false"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="descripcion" HeaderText="SELECCIONADAS" Visible="false"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="urlimagen" HeaderText="urlimagen" Visible="false"></asp:BoundColumn>
                                                                                            <asp:TemplateColumn Visible="true">
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton ID="LinkButton1" runat="server" CommandName="descripcion">
                                                                                                    </asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                            <asp:TemplateColumn>
                                                                                                <ItemTemplate>
                                                                                                    <asp:LinkButton runat="server" CommandName="borrarex" CommandArgument="<%# CType(Container, DataGridItem).ItemIndex%>"
                                                                                                        class="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                                                                                </ItemTemplate>
                                                                                            </asp:TemplateColumn>
                                                                                        </Columns>
                                                                                    </asp:DataGrid>
                                                                                    <asp:DataGrid ID="DGextremidadesref" runat="server" Visible="false" AutoGenerateColumns="false">
                                                                                        <Columns>
                                                                                            <asp:BoundColumn DataField="codigoextremidad" HeaderText="cod"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="descripcion" HeaderText="descripcion"></asp:BoundColumn>
                                                                                            <asp:BoundColumn DataField="urlimagen" HeaderText="urlimagen" Visible="false"></asp:BoundColumn>
                                                                                        </Columns>
                                                                                    </asp:DataGrid>

                                                                                </div>
                                                                            </div>
                                                                            <div id="contenedor_extremidades" runat="server">
                                                                                <div class="row">
                                                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                                        <label>Seleccionar</label>
                                                                                        <div class="form-group">
                                                                                            <div class="input-group col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                                                <asp:ListBox ID="LstExtremidades" class="form-control" runat="server" Rows="5" AutoPostBack="true"></asp:ListBox>
                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                                <div class="row">
                                                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                                                                        <asp:ImageButton ID="btnAgregarextrem" runat="server" ImageUrl="../Resource/iconguardar.png" CssClass="hidden" />
                                                                                        <button type="button" class="btn btn-primary btn-block" runat="server" onserverclick="Agregar_Extremidad_Click"><i class="fa fa-plus"></i>Agregar</button>
                                                                                    </div>
                                                                                </div>
                                                                            </div>


                                                                        </div>
                                                                    </div>
                                                                </ContentTemplate>
                                                                <Triggers>
                                                                    <asp:AsyncPostBackTrigger ControlID="btnAgregarextrem" EventName="Click" />
                                                                    <asp:AsyncPostBackTrigger ControlID="Dgextremidades" EventName="ItemCommand" />
                                                                    <asp:AsyncPostBackTrigger ControlID="LstExtremidades" EventName="SelectedIndexChanged" />
                                                                </Triggers>

                                                            </asp:UpdatePanel>
                                                            <asp:HiddenField ID="HdPreguntasex" runat="server" />
                                                            <asp:HiddenField ID="HdIndexex" runat="server" />

                                                        </ContentTemplate>
                                                    </ajaxToolkit:TabPanel>
                                                </ajaxToolkit:TabContainer>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- MOTIVO DE CONSULTAs -->
                            </div>
                            <!-- /.tab-content -->
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <!-- ****************HISTORIAL***************** -->


        <!-- ******************DOCUMENTOS************** -->

        <div class="row">
            <div class="col-md-12">
                <div class="box box-info collapsed-box">
                    <div class="box-header with-border">
                        <h3 class="box-title">Documentos</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>

                    <div class="box-body">

                        <div class="nav-tabs-custom">
                            <ul class="nav nav-tabs">
                                <li><a href="#receta_medica" data-toggle="tab">Receta Médica</a></li>
                                <li><a href="#justificacion_medica" data-toggle="tab">Justificación Médica</a></li>
                                <li class="active"><a href="#rayos_x" data-toggle="tab">Rayos X</a></li>
                                <li><a href="#archivos" data-toggle="tab">Archivos</a></li>
                            </ul>

                            <div class="tab-content">
                                <div class="tab-pane" id="receta_medica">
                                    <div class="form-body">
                                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelRECETA">
                                            <ProgressTemplate>
                                                <div class="preloader">
                                                    <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <%--    ------------------------UP principal Recetas----------------------%>
                                        <asp:UpdatePanel ID="UpdatePanelRECETA" runat="server">
                                            <ContentTemplate>
                                                <script type="text/javascript">
                                                    function PrintThisDiv(id) {
                                                        var HTMLContent = document.getElementById(id);

                                                        var Popup = window.open('about:blank', id, 'width=800,height=500');

                                                        Popup.document.writeln('<html><head>');
                                                        Popup.document.writeln('<style type="text/css">');
                                                        Popup.document.writeln('body{font-family: calibri;}');
                                                        Popup.document.writeln('textarea{width: 680px;}');
                                                        Popup.document.writeln('.no-print{display: none;}');
                                                        Popup.document.writeln('</style>');
                                                        Popup.document.writeln('</head><body onload="window.print();">');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:800px; text-align:center ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 130px; height="100px">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td style="width: 369px;" height="100px">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:800px; text-align:center ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 50px; text-align:left">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');
                                                        Popup.document.writeln('<BR/>');
                                                        Popup.document.writeln('<BR/>');
                                                        Popup.document.writeln(HTMLContent.innerHTML);
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('</body></html>');
                                                        Popup.print();
                                                        Popup.close();
                                                    }
                                                </script>

                                                <asp:Panel ID="PanelAvisosReceta" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-success alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeAvisoReceta" runat="server"></asp:Label></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>
                                                <asp:Panel ID="PanelDesicionReceta" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-info alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMostarDecisionReceta" runat="server" Text="Label"></asp:Label></h4>
                                                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSiReceta_Click"><i class="fa fa-check"></i>Sí</button>
                                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNoReceta_Click"><i class="fa fa-close"></i>No</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelCriticoReceta" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-danger alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeCriticoReceta" runat="server" Text="Label"></asp:Label>></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelAdvertenciaReceta" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-warning alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeAdvertenciaReceta" runat="server" Text="Label"></asp:Label></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <div id="contenedor_receta" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label id="lblcantidadReceta" runat="server">Cantidad</label>
                                                            <div class="input-group col-md-12">
                                                                <span id="gaddon" runat="server" class="input-group-addon"><i id="fhashtag" runat="server" class="fa fa-hashtag fa-fw"></i></span>
                                                                <input id="txtUnidades" type="number" runat="server" class="form-control" maxlength="30" placeholder="Cantidad" min="1" value="1" />
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Medicamento</label>
                                                            <div class="form-group">
                                                                <asp:ListBox ID="ListMedicamentos" runat="server" AutoPostBack="True" CssClass="form-control ListMedicamentos select2-single"></asp:ListBox>
                                                                <input id="txtnombrenm" type="text" runat="server" class="form-control" visible="false" maxlength="60" placeholder="Escribe el nombre del nuevo medicamento" />
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label>Formula</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-flask fa-fw"></i></span>
                                                                <input id="txtsalreceta" type="text" runat="server" class="form-control" maxlength="50" placeholder="Formula" readonly />
                                                                <asp:ListBox ID="Listsalnm" runat="server" AutoPostBack="True" Visible="false" CssClass="form-control Listsalnm"></asp:ListBox>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label id="lblobservacionreceta" runat="server">Observaciones</label>
                                                            <asp:TextBox ID="txtObservaReceta" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength='500' onkeyDown="VerificaTextAreaMaxLength(this,event,'500');" Rows="3"></asp:TextBox>
                                                            <input id="txtdosisnm" type="text" runat="server" class="form-control" visible="false" maxlength="60" placeholder="Escribe la dosis recomendada" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label id="lblalergiaReceta" runat="server">Alergías</label>
                                                            <asp:TextBox ID="txtalergiareceta" runat="server" TextMode="MultiLine" CssClass="form-control" MaxLength='500' onkeyDown="VerificaTextAreaMaxLength(this,event,'500');" Rows="3"></asp:TextBox>
                                                            <input id="txtpresentacionnm" type="text" runat="server" class="form-control" visible="false" maxlength="60" placeholder="Escribe la presentación" />
                                                        </div>
                                                        <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                            <label></label>
                                                            <asp:Button ID="btareceta" runat="server" CssClass="btn btn-primary btn-block" Text="Agregar"></asp:Button>
                                                            <asp:Button ID="btnnuevom" runat="server" CssClass="btn btn-warning btn-block" Text="Nuevo Medicamento"></asp:Button>
                                                            <asp:Button ID="btnguardarnm" runat="server" Visible="false" CssClass="btn btn-success btn-block" Text="Guardar Nuevo Medicamento"></asp:Button>
                                                            <asp:Button ID="actualizaReceta" runat="server" Visible="false" CssClass="btn btn-danger btn-block" Text="Cancelar"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                </div>

                                                <asp:HiddenField ID="hfTemporal" runat="server" />
                                                <asp:HiddenField ID="hftemalergia" runat="server" />
                                                <div class="row">
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                    </div>
                                                    <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                    </div>
                                                    <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                                                        <label>Fecha:</label><asp:Label ID="lblfreceta" runat="server" Text="Label" Font-Names="Calibri" Font-Size="11pt"></asp:Label>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12" id="print2">
                                                        <table class="table table-striped table-responsive">
                                                            <tr>
                                                                <td>
                                                                    <label id="LBLDP" runat="server" text="PACIENTE :">
                                                                        <asp:Label ID="lblNombreReceta" runat="server" Font-Bold="True" Font-Names="calibri" Font-Size="11pt" Text="Label"></asp:Label></label>
                                                                    <br />
                                                                    <label id="lblEdi" runat="server" text="EDAD :">
                                                                        <asp:Label ID="lblEdadReceta" runat="server" Text="Label" Font-Names="Calibri" Font-Size="11pt"></asp:Label></label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td>
                                                                    <br />
                                                                    <asp:TextBox ID="txtAplicacionMedica" Visible="false" runat="server" BackColor="Transparent" CssClass="form-control" TextMode="MultiLine" Font-Names="Calibri" Font-Size="11pt" BorderStyle="None" MaxLength='200' onkeyDown="VerificaTextAreaMaxLength(this,event,'200');" Rows="10"></asp:TextBox>
                                                                    <asp:DataGrid ID="gvReceta" runat="server" AutoGenerateColumns="False" GridLines="None" Style="width: 100%; max-height: 100%;" CssClass="table table-striped">
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="CodigoMedicamento" HeaderText="idmedicamento" Visible="false"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="Cantidad" HeaderText="CANT.">
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="descripcion" HeaderText="Indicaciones"></asp:BoundColumn>
                                                                            <asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandName="borrarmedicamento" CommandArgument="<%# CType(Container, DataGridItem).ItemIndex%>" class="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle HorizontalAlign="Center" />
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                    </asp:DataGrid>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>
                                                <asp:HiddenField ID="HdPreguntasRecetaedit" runat="server" />
                                                <asp:HiddenField ID="HdIndexRecetaedit" runat="server" />
                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <a href="javascript:;" onclick="PrintThisDiv('print2')" class="btn btn-info btn-block"><i class="fa fa-print"></i>Imprimir</a>
                                                    </div>
                                                </div>
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- JUSTIFICACION MEDICA -->
                                <div class="tab-pane" id="justificacion_medica">
                                    <div class="form-body">
                                        <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelJUSTIFICACION">
                                            <ProgressTemplate>
                                                <div class="preloader">
                                                    <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                                                </div>
                                            </ProgressTemplate>
                                        </asp:UpdateProgress>
                                        <%--    ------------------------UP principal Recetas----------------------%>
                                        <asp:UpdatePanel ID="UpdatePanelJUSTIFICACION" runat="server">
                                            <ContentTemplate>
                                                <script type="text/javascript">
                                                   
                                                                        
                                                    function PrintThisDivJ(idJ) {
                                                        var HTMLContent = document.getElementById(idJ);

                                                        var textjustificacion = document.getElementById("<%=txtJustificacion.ClientID%>").value
                                                        var textdocjustificacion = document.getElementById("<%=txtdocjustificacion.ClientID%>").value
                                                        var Popup = window.open('about:blank', idJ, 'width=800,height=600');
                                             


                                                        Popup.document.writeln('<html><head>');
                                                        Popup.document.writeln('<style type="text/css">');
                                                        Popup.document.writeln('body{font-family: calibri;}');
                                                        Popup.document.writeln('textarea{width: 680px;}');
                                                        Popup.document.writeln('.no-print{display: none;}');
                                                        Popup.document.writeln('</style>');
                                                        Popup.document.writeln('</head><body onload="window.print();">');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:600px; text-align:justify ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 130px; height="100px">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td style="width: 369px;" height="100px">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:700px; text-align:justify ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 50px; text-align:left">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');

                                                        Popup.document.writeln(HTMLContent.innerHTML);
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:680px; text-align:justify ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 50px; text-align:left">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');
                                                        Popup.document.writeln('<BR/>');
                                                        Popup.document.writeln('<BR/>');
                                                        Popup.document.writeln(textjustificacion);
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('<table border="0" cellpadding="0" cellspacing="0" style=" width:680px; text-align:center ">');
                                                        Popup.document.writeln('<tr>');
                                                        Popup.document.writeln('<td style="width: 50px; text-align:left">');
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('<td>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<Label ID="NDOC" runat="server" Font-Bold="True" Font-Names="calibri" Font-Size="12pt" Text="A T E N T A M E N T E">');
                                                        Popup.document.writeln('A T E N T A M E N T E');
                                                        Popup.document.writeln('</Label>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<Label ID="RDOC" runat="server" Font-Bold="True" Font-Names="calibri" Font-Size="12pt" Text="_">');
                                                        Popup.document.writeln('__________________________');
                                                        Popup.document.writeln('</Label>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln('<br/>');
                                                        Popup.document.writeln(textdocjustificacion);
                                                        Popup.document.writeln('</td>');
                                                        Popup.document.writeln('</tr>');
                                                        Popup.document.writeln('</table>');
                                                        Popup.document.writeln('</body></html>');
                                                        Popup.print();
                                                        Popup.close();
                                                    }
                                                </script>


                                                <asp:Panel ID="PanelAvisosJusti" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-success alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeAvisoJusti" runat="server"></asp:Label></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelDesicionJusti" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-info alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMostarDecisionJusti" runat="server" Text="Label"></asp:Label></h4>
                                                                <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSiJusti_Click"><i class="fa fa-check"></i>Sí</button>
                                                                <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNoJusti_Click"><i class="fa fa-close"></i>No</button>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelCriticoJusti" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-danger alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeCriticoJusti" runat="server" Text="Label"></asp:Label>></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>

                                                <asp:Panel ID="PanelAdvertenciaJusti" runat="server" Visible="false">
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <div class="alert alert-warning alert-dismissable">
                                                                <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                                <h4>
                                                                    <asp:Label ID="LblMensajeAdvertenciaJusti" runat="server" Text="Label"></asp:Label></h4>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </asp:Panel>


                                                <div id="contenedor_justificaciones" runat="server">
                                                    <div class="row">
                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <label>Justificaciones Predefinidas</label>
                                                            <div class="form-group">
                                                                <asp:ListBox ID="ListJustificacion" class="form-control ListJustificacion select2-single" runat="server" AutoPostBack="True"></asp:ListBox>
                                                            </div>
                                                        </div>

                                                        <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                                            <label>Historial de Justificaciones del paciente</label>
                                                            <div class="input-group col-md-12">
                                                                <div class="input-group col-md-12 select2-bootstrap-append">
                                                                    <asp:DataGrid ID="DGjustificaciones" runat="server" GridLines="None" AutoGenerateColumns="False" CssClass="table table-striped">
                                                                        <Columns>
                                                                            <asp:BoundColumn DataField="FolioJustificacion" HeaderText="FolioJustificacion" Visible="False"></asp:BoundColumn>
                                                                            <asp:BoundColumn DataField="descripcionj"></asp:BoundColumn>
                                                                            <asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandName="verjusti" CommandArgument="<%# CType(Container, DataGridItem).ItemIndex%>" class="btn btn-success"><i  class="fa fa-pencil"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="3em" />
                                                                            </asp:TemplateColumn>
                                                                            <asp:TemplateColumn>
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" CommandName="borrarjusti" CommandArgument="<%# CType(Container, DataGridItem).ItemIndex%>" class="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                                                                </ItemTemplate>
                                                                                <ItemStyle Width="3em" />
                                                                            </asp:TemplateColumn>
                                                                        </Columns>
                                                                        <ItemStyle BackColor="#E6E6E6" BorderStyle="Solid" BorderColor="White" BorderWidth="2px" />
                                                                    </asp:DataGrid>
                                                                    <asp:HiddenField ID="HdPreguntasJusti" runat="server" />
                                                                    <asp:HiddenField ID="HdIndexJusti" runat="server" />
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                    <hr />
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <label>Nombre de la Justificación</label>
                                                            <div class="input-group col-md-12">
                                                                <span class="input-group-addon"><i class="fa fa-font fa-fw"></i></span>
                                                                <input id="txtdescripcionj" type="text" runat="server" class="form-control" maxlength="30" placeholder="Nombre de la justificación" />
                                                            </div>
                                                            <hr />
                                                        </div>
                                                    </div>

                                                    <div class=" row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <table class="table table-striped table-responsive">
                                                                <tr id="printj">
                                                                    <td class="col-md-4 ">
                                                                        <asp:Label ID="LBLDPj" runat="server" Text="PACIENTE :"></asp:Label><strong><asp:Label ID="lblNombreJusti" runat="server" Text="Label"></asp:Label></strong>&nbsp&nbsp
                                                                    </td>
                                                                    <td class="col-md-4 ">
                                                                        <asp:Label ID="LBLEPj" runat="server" Text="EDAD :"></asp:Label><strong><asp:Label ID="lblEdadJusti" runat="server" Text="Label"></asp:Label></strong>&nbsp&nbsp
                                                                    </td>
                                                                    <td class="col-md-4 ">
                                                                        <asp:Label ID="LBLFPj" runat="server" Text="FECHA :"></asp:Label><strong><asp:Label ID="lblfjustificacion" runat="server" Text="Label"></asp:Label></strong>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="3" class="col-md-12 ">
                                                                        <asp:TextBox ID="txtJustificacion" Rows="10" runat="server" TextMode="MultiLine" CssClass=" form-control summernote"></asp:TextBox>

                                                                        <%-- +++++++++++++++++++++++++   Aqui Voy  implementar el SummerNote--%>

                                                                        <asp:TextBox ID="txtdocjustificacion" Rows="1" class="form-control" runat="server" BackColor="Transparent"
                                                                            TextMode="MultiLine" BorderStyle="None" onkeyDown="VerificaTextAreaMaxLength(this,event,'1000');"></asp:TextBox>
                                                                    </td>
                                                                </tr>
                                                            </table>

                                                        </div>
                                                        <div>
                                                        </div>

                                                    </div>
                                                    <div class="row">
                                                        <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                            <asp:Button ID="gjustificacion" runat="server" CssClass="btn btn-success btn-block" Text="Grabar Justificación" Visible="false"></asp:Button>
                                                            <asp:Button ID="btagregarnj" runat="server" CssClass="btn btn-warning btn-block" Text="Nueva Justificación"></asp:Button>
                                                            <asp:Button ID="Updatejustificacion" runat="server" CssClass="btn btn-twitter btn-block" Text="Actualizar Justificación" Visible="false"></asp:Button>
                                                            <asp:Button ID="btnuevojustificacion" runat="server" CssClass="btn btn-success btn-block" Text="Guardar Nueva Justificación" Visible="False"></asp:Button>
                                                        </div>
                                                    </div>
                                                    <br />
                                                </div>

                                                <div class="row">
                                                    <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                        <a href="javascript:;" onclick="PrintThisDivJ('printj')" class="btn btn-info btn-block"><i class="fa fa-print"></i>Imprimir</a>
                                                    </div>
                                                </div>

                                                <asp:HiddenField ID="djustificacionc" runat="server" />
                                                <asp:HiddenField ID="foliocj" runat="server" />
                                                <asp:HiddenField ID="djustificacionnuevo" runat="server" />
                                                <asp:HiddenField ID="FJustificacionUpdate" runat="server" />
                                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                    </div>
                                </div>
                                <!-- JUSTIFICACION -->

                                <div class="tab-pane active" id="rayos_x">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-body">
                                                <!-- /.box-header -->
                                                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelDIAGNOSTICOS">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <i class="fa fa-refresh fa-spin"></i>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <input id="input-image-1" name="input-image[]" type="file" multiple="multiple" class="file-loading" accept="image/*">
                                                <div id="errorBlock" class="help-block"></div>
                                                <!-- an example modal dialog to display confirmation of the resized image -->
                                                <div id="kv-success-modal" class="modal fade">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                                <h4 class="modal-title">Se subieron todas las imagenes</h4>
                                                            </div>
                                                            <div id="kv-success-box" class="modal-body">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="tab-pane" id="archivos">
                                    <div class="row">
                                        <div class="col-lg-12">
                                            <div class="form-body">
                                                <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelDIAGNOSTICOS">
                                                    <ProgressTemplate>
                                                        <div class="overlay">
                                                            <i class="fa fa-refresh fa-spin"></i>
                                                        </div>
                                                    </ProgressTemplate>
                                                </asp:UpdateProgress>
                                                <input id="input-image-2" name="input-Videos[]" type="file" multiple="multiple" class="file-loading">
                                                <div id="errorBlock-2" class="help-block"></div>
                                                <!-- an example modal dialog to display confirmation of the resized image -->
                                                <div id="kv-success-modal-2" class="modal fade">
                                                    <div class="modal-dialog">
                                                        <div class="modal-content">
                                                            <div class="modal-header">
                                                                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                                                <h4 class="modal-title">Se subieron todas los archivos</h4>
                                                            </div>
                                                            <div id="kv-success-box-2" class="modal-body">
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                </div>
                                <div id="kvFileinputModal" class="file-zoom-dialog modal fade" tabindex="-1" aria-labelledby="kvFileinputModalLabel" style="display: none;"></div>
                                <asp:HiddenField ID="ImagenRX" runat="server" />
                                <asp:HiddenField ID="PreviusRX" runat="server" />
                                <asp:HiddenField ID="Video" runat="server" />
                                <asp:HiddenField ID="PreviusVideos" runat="server" />
                            </div>
                            <!-- /.tab-content -->
                        </div>

                    </div>
                </div>
            </div>
        </div>

        <!-- ******************DOCUMENTOS************** -->

        <!-- ***************PAGOS************************-->
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info collapsed-box" id="box_pagos">
                    <div class="box-header with-border">
                        <h3 class="box-title">Pagos</h3>
                        <div class="box-tools pull-right">
                            <button type="button" class="btn btn-box-tool" data-widget="collapse"><i class="fa fa-plus"></i></button>
                        </div>
                    </div>
                    <asp:UpdateProgress runat="server" AssociatedUpdatePanelID="UpdatePanelPagos">
                        <ProgressTemplate>
                            <%-- PRELOADER--%>
                            <div class="preloader">
                                <div class="status"><i class="fa fa-spinner fa-pulse fa-5x fa-fw"></i></div>
                            </div>
                        </ProgressTemplate>
                    </asp:UpdateProgress>


                    <div class="box-body">

                        <div class="row" runat="server" id="contenedor_pagos_cantidad">
                            <div class="col-md-2 ">
                                <div class="input-group">
                                    <span class="input-group-addon"><i class="fa fa-cubes"></i></span>
                                    <input type="number" class="form-control" placeholder="Cantidad" name="cantidad" id="cantidad" value="1" runat="server" min="1" onkeypress="return ValSoloNumeros(event)">
                                </div>
                            </div>
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="col-md-10">
                                        <asp:ListBox ID="LstConceptoPagos" runat="server" AutoPostBack="True" CssClass="form-control LstConceptoPagos select2-single"></asp:ListBox>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>


                        <div class="row container-fluid">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 table-responsive">
                                <h4 class="modal-title">Conceptos a pagar</h4>
                                <%--PANEL DE AVISOS--%>
                                <asp:UpdatePanel ID="UpdatePanelPagos" runat="server">
                                    <ContentTemplate>
                                        <asp:Panel ID="PanelAvisosPagos" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <div class="alert alert-success alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                        <h4>
                                                            <asp:Label ID="LblMensajeAvisoPagos" runat="server"></asp:Label></h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelDesicionPagos" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <div class="alert alert-info alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                        <h4>
                                                            <asp:Label ID="LblDecisionPagos" runat="server" Text="Label"></asp:Label></h4>
                                                        <button type="button" class="btn btn-success" runat="server" onserverclick="BtnSiPagos_Click"><i class="fa fa-check"></i>Sí</button>
                                                        <button type="button" class="btn btn-danger" runat="server" onserverclick="BtnNoPagos_Click"><i class="fa fa-close"></i>No</button>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelCriticoPagos" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <div class="alert alert-danger alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                        <h4>
                                                            <asp:Label ID="LblMensajeCriticoPagos" runat="server" Text="Label"></asp:Label>></h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:Panel ID="PanelAdvertenciaPagos" runat="server" Visible="false">
                                            <div class="row">
                                                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                    <div class="alert alert-warning alert-dismissable">
                                                        <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i>Cerrar</button>
                                                        <h4>
                                                            <asp:Label ID="LblMensajeAdvertenciaPagos" runat="server" Text="Label"></asp:Label></h4>
                                                    </div>
                                                </div>
                                            </div>
                                        </asp:Panel>

                                        <asp:HiddenField ID="HdPreguntasPagos" runat="server" />
                                        <asp:HiddenField ID="HdIndexPagos" runat="server" />


                                        <asp:GridView ID="GdvConceptoPagos" runat="server" Visible="true" GridLines="none" AutoGenerateColumns="false" CssClass="table table-striped">
                                            <Columns>
                                                <asp:BoundField DataField="codigoConcepto" HeaderText="idPago" Visible="false" InsertVisible="true"></asp:BoundField>
                                                <asp:BoundField AccessibleHeaderText="Cantidad" DataField="cantidad" HeaderText="Cantidad" />
                                                <asp:BoundField DataField="servicio" HeaderText="Servicio"></asp:BoundField>
                                                <asp:BoundField DataField="descripcion" HeaderText="Descripción"></asp:BoundField>
                                                <asp:BoundField DataField="costo" HeaderText="Costo"></asp:BoundField>
                                                <asp:TemplateField ItemStyle-Wrap="true">
                                                    <ItemTemplate>
                                                        <asp:LinkButton runat="server" CommandName="borrarConceptoPago" CommandArgument="<%# CType(Container, GridViewRow).RowIndex %>"
                                                            CssClass="btn btn-danger"><i  class="fa fa-trash"></i></asp:LinkButton>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <h4>No hay Pagos seleccionados</h4>
                                            </EmptyDataTemplate>
                                        </asp:GridView>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>

                        </div>

                        <div class="row container-fluid">
                            <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12 table-responsive">
                                <table class="table">
                                    <tbody>
                                        <tr>
                                            <td></td>
                                            <td>Total:</td>
                                            <td>
                                                <div class="input-group">
                                                    <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                                                    <asp:UpdatePanel ID="UpdatePanelpagoTotal" runat="server">
                                                        <ContentTemplate>
                                                            <asp:Label ID="LblPagoTotal" class="form-control input-lg" runat="server" Text=""></asp:Label>
                                                        </ContentTemplate>
                                                    </asp:UpdatePanel>
                                                </div>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- ***************FIN PAGOS************************-->

        <div class="row">
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                <div class="form-group">
                    <button type="button" class="btn btn-lg btn-bitbucket btn-block" runat="server" onserverclick="Lnkbackagenda_Click"><i class="fa fa-chevron-circle-left"></i> Regresar a Agenda</button>
                </div>
            </div>
            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                <div class="form-group">
                    <button type="button" id="finalizar2" class="btn btn-lg btn-facebook btn-block" runat="server" onserverclick="LnkFinalizar_Click"><i class="fa fa-calendar-check-o"></i> Finalizar Cita</button>
                </div>
            </div>
            <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12 ">
                <div class="form-group">
                    <button type="button" id="cmdimprimirexp2" class="btn btn-lg btn-twitter btn-block" runat="server" onserverclick="cmdimprimirexp_Click"><i class="fa fa-print"></i> Imprimir Expediente</button>
                </div>
            </div>
        </div>
    </section>
    <CR:CrystalReportSource ID="origen_reporte" runat="server"></CR:CrystalReportSource>
</asp:Content>
