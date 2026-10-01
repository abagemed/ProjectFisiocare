<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Ayuda.aspx.vb" Inherits="AgeMED.Ayuda" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
   <style>
       .video-container {
           position:relative;
           padding-bottom:25%;
           padding-top:25px;
           height:0;
           overflow:hidden;
       }

       .video-container iframe, .video-container object, .video-container embed {
           position:absolute;
           top:0;
           left:0;
           width:95%;   
           height:95%; 
           max-height :301.5px;
           max-width:402px;
       }
   </style>
    <div class="row"  >
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 video-container">
            <iframe  src="https://www.youtube.com/embed/_1SYEKWVFDo" frameborder="0" gesture="media" allowfullscreen></iframe>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 video-container">
            <iframe src="https://www.youtube.com/embed/MuH2ipVVp7g" frameborder="0" gesture="media" allowfullscreen></iframe>
        </div>
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 video-container">
            <iframe  src="https://www.youtube.com/embed/Y9eKi181h9w" frameborder="0" gesture="media" allowfullscreen></iframe>
        </div>
           
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 video-container">
           <iframe  src="https://www.youtube.com/embed/idhJaCKlGeE" frameborder="0" gesture="media" allowfullscreen></iframe>
        </div>  
        <div class="col-lg-4 col-md-4 col-sm-6 col-xs-12 video-container">
            <iframe  src="https://www.youtube.com/embed/NTmLSKHp5_E" frameborder="0" gesture="media" allowfullscreen></iframe>
        </div>      
    </div>
    <hr />
    
</asp:Content>
