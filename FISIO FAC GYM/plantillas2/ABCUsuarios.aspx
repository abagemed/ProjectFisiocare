<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="ABCUsuarios.aspx.vb" Inherits="AgeMED.Alta_de_Pasiente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title >ABC Usuarios</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate >
        <asp:Label ID="Lbl1" runat="server" Text="Subir fotos a sql"></asp:Label>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Image ID="Image1" runat="server" Height="217px" Width="384px" />
        <asp:Button ID="Button1" runat="server" Text="Button" />
    </ContentTemplate>

</asp:UpdatePanel>
    
</asp:Content>
