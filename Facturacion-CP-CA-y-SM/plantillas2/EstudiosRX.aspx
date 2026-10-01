<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="EstudiosRX.aspx.vb" Inherits="AgeMED.EstudiosRX" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

   

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script> window.onload = function () {
     //+++++++++++++++ cargar imagen +++++++++++++++++++                       
     $("#input-image-1").fileinput({
         initialPreview: <%=ImagenRX.Value.ToString%>,
         language:'es',
         uploadAsync: true,
         //autoReplace: true,
         initialPreviewAsData: true, // identify if you are sending preview data only and not the raw markup
         initialPreviewFileType: 'image', // image is the default and can be overridden in config below
         initialPreviewConfig:<%=PreviusRX .Value .ToString %>,
         overwriteInitial: false,
         dataType: 'POST',
         uploadUrl: "Handler.ashx",
         allowedFileExtensions: ["jpg", "jpeg", "png", "gif"],
         // maxImageWidth: 800,
         // maxImageHight: 1500,
         minFileCount:1,
         maxFileCount: 10,
         maxFileSize:400000,
         maxFilePreviewSize:400000,
         resizeImage: true,
         elErrorContainer: "#errorBlock"
     }).on('filepreupload', function () {
         $('#kv-success-box').html('');
     }).on('fileuploaded', function (event, data) {
         $('#kv-success-box').append(data.response.link);
         //$('#kv-success-modal').modal('show');
     }).on('filepredelete', function(event, key) {
         console.log('Key = ' + key);                           
     }).on('filedeleted', function(event, key) {
         console.log('Key = ' + key);
     }).on('filesorted', function(e, params) {
         console.log('File sorted params', params);
     });            
                 
 }     
</script>
    

    <asp:UpdatePanel ID="UpdatePanelDIAGNOSTICOS" runat="server"></asp:UpdatePanel>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:HiddenField ID="ImagenRX" runat="server" />
            <asp:HiddenField ID="CodigoPaciente" runat="server" />
            <asp:HiddenField ID="PreviusRX" runat="server" />
            <asp:HiddenField ID="Video" runat="server" />
            <asp:HiddenField ID="PreviusVideos" runat="server" />
            <%-- \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\ Datos Paciente  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\--%>

            <div class="row">
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <strong>DATOS DEL PACIENTE</strong><br />
                    <label id="lblheadpaciente" class="lblhead">Paciente:</label>
                    <asp:Label ID="lblpaciente" runat="server" Style="padding-left: .2em;"></asp:Label>
                    <br />
                    <label id="lblheadedad" class="lblhead">Edad:</label>
                    <asp:Label ID="lbledad" runat="server" Style="padding-left: .2em;"></asp:Label>
                    <br />
                    <label id="lblheadfecha" class="lblhead">Fecha Nac.:</label>
                    <asp:Label ID="Lblfechanacimiento" runat="server" Style="padding-left: .2em;"></asp:Label>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <strong></strong><br />
                    <label id="lblheadnacionalidad" class="lblhead">Nacionalidad:</label>
                    <asp:Label ID="Lblnacionalidad" runat="server" Style="padding-left: .2em;"></asp:Label>
                    <br />
                    <label id="lblheadocupacion" class="lblhead">Ocupacion:</label>
                    <asp:Label ID="lblocupacion" runat="server" Style="padding-left: .2em;"></asp:Label>
                    <br />
                    <label id="lblheadgenero" class="lblhead">Genero:</label>
                    <asp:Label ID="Lblgenero" runat="server" Style="padding-left: .2em;"></asp:Label>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <strong>INFORMACIÓN CONSULTA</strong><br />
                    <label id="lblheadfolio">Folio Consulta:</label>
                    <asp:Label ID="Lblfolioconsulta" runat="server" Font-Bold="true" ForeColor="DarkRed" Style="margin-left: .5em; font-size: 1.1em;"></asp:Label>
                    <br />
                    <label id="lblheaddr">Atiende:</label>
                    <asp:Label ID="LblDrAtiende" Font-Bold="true" ForeColor="DarkBlue" runat="server"></asp:Label>
                </div>
                <div class="col-lg-3 col-md-3 col-sm-12 col-xs-12">
                    <strong>ESTUDIO SOLICITADO</strong><br />
                    <ul><asp:Label ID="LblEstudioRX" Font-Bold="true" ForeColor="DarkRed" runat="server" Text="Label"></asp:Label></ul>
                    <strong style="font-size:16px"><asp:Label ID="LblLadoExtremidad" runat="server" Text="Lado" CssClass="text-primary"></asp:Label></strong>
                    <asp:Label ID="LblFecha" runat="server" Text="" Style="font-family: Calibri; width: 70%; font-size: 1em"></asp:Label>
                </div>
            </div>
           
            
            <!-- /.box-body ???????????????????????????????????????????????????????????????????????????????????????????????????????????????????? -->
            <div class="box box-default collapsed-box">
                <div class="box-header with-border">
                    <h3 class="box-title">Subir Imagenes</h3>
                    <div class="box-tools pull-right">
                        <button type="button" class="btn btn-box-tool" ><i class="fa fa-plus"></i></button>
                    </div>
                </div>
                <!-- /.box-header -->
                <div class="box-body" style="display: block;">
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
                                    
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
