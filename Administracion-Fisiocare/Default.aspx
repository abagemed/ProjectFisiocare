<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="login" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc1" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Administracion Fisiocare</title>
    <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
     <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet" type="text/css" />
	<!-- icons -->

    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/material-design-iconic-font/2.2.0/css/material-design-iconic-font.min.css" />
    <!-- bootstrap -->
	<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- style -->
    <link rel="stylesheet" href="assets/css/pages/extra_pages.css" />
	<!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />




</head>
    <body>
    <div class="limiter">
		<div class="container-login100 page-background">
			<div class="wrap-login100">
				<form class="login100-form validate-form" id="form1" runat="server">
					<span class="login100-form-logo">
						<img alt="" src="imagenes/iso.png">
					</span>
					<span class="login100-form-title p-b-34 p-t-27">
						Administración FisioCare<br />
                        <small>Iniciar sesión</small>
					</span>
					<div class="wrap-input100 validate-input" data-validate = "Escribe el nombre de usuario">
                        <asp:TextBox ID="txtusuario" runat="server" CssClass="input100" placeholder="Usuario"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf207;"></span>
					</div>
					<div class="wrap-input100 validate-input" data-validate="Escribe la contraseña">
                        <asp:TextBox ID="txtpass" runat="server" TextMode="Password" CssClass="input100" placeholder="Contraseña"></asp:TextBox>
						<span class="focus-input100" data-placeholder="&#xf191;"></span>
					</div>
					<div class="container-login100-form-btn">
                        <asp:Button ID="Button1" runat="server" Text="Entrar" CssClass="login100-form-btn" />
					</div>
                    <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
                    <asp:ScriptManager ID="ScriptManager1" runat="server">
                    </asp:ScriptManager>
				</form>
			</div>
		</div>
	</div>

    
    <!-- start js include path -->
     <script type="text/javascript" src="assets/plugins/jquery/jquery.min.js" ></script>
    <!-- bootstrap -->
    <script type="text/javascript" src="assets/plugins/bootstrap/js/bootstrap.min.js" ></script>
    <script type="text/javascript" src="assets/js/pages/extra_pages/extra_pages.js"></script>
    <!-- end js include path -->


</body>
<%--<body><center>
    <form id="form1" runat="server">
    <div style="width:900px; border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
    <img src="imagenes/banerlogin.png" /><br />
        <br />
        <br />
        <table border="0" cellpadding="7" cellspacing="0" style="width: 350px; text-align:left">
            <tr>
                <td style="width: 84px; background-color:#f4f4f4">
                    <asp:Label ID="Label17" runat="server" ForeColor="#1A3773" Height="20px" Style="border-bottom: #e0e0e0 2px solid"
                        Text="Usuario" Width="110px"></asp:Label></td>
                <td style="width: 190px">
                    <asp:TextBox ID="txtusuario" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Style="text-align:center; border-bottom: #e0e0e0 2px solid"
                        Width="200px"></asp:TextBox></td>
            </tr>
            <tr>
                <td style="width: 84px; background-color:#f4f4f4">
                    <asp:Label ID="Label16" runat="server" ForeColor="#1A3773" Height="20px" Style=" border-bottom: #e0e0e0 2px solid"
                        Text="Contraseña" Width="110px"></asp:Label></td>
                <td style="width: 190px">
                    <asp:TextBox ID="txtpass" runat="server" BackColor="White" BorderColor="White"
                        BorderStyle="Solid" BorderWidth="1px" MaxLength="50" Style=" text-align:center;border-bottom: #e0e0e0 2px solid"
                        Width="200px" TextMode="Password"></asp:TextBox></td>
            </tr>
        </table>
      <div style=" width:345px; background-color:Red; border-top: solid 5px white; padding:4px">
        <asp:Button ID="Button1" runat="server" Text="Aceptar" CssClass="boton" ForeColor="Red" /></div>
        <cc1:messagebox id="Messagebox1" runat="server"></cc1:messagebox>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
    </div>
    </form>
    </center>
</body>--%>
</html>
