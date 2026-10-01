<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="login.aspx.vb" Inherits="AgeMED.login" %>

<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>AgeMED | Agenda Médica</title>
    <!-- Tell the browser to be responsive to screen width -->
    <meta content="width=device-width, initial-scale=1, maximum-scale=1, user-scalable=no" name="viewport">
    <!-- Bootstrap 3.3.6 -->
    <link rel="stylesheet" href="../../bootstrap/css/bootstrap.min.css">
    <!-- Font Awesome -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.5.0/css/font-awesome.min.css">
    <!-- Ionicons -->
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/ionicons/2.0.1/css/ionicons.min.css">
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css">
    <!-- iCheck -->
    <link rel="stylesheet" href="../../plugins/iCheck/square/blue.css">

    <script>
        function ValSoloLetrasEspacioBlanco(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[A-Za-z ñÑ]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        
    </script>
</head>
<body class="hold-transition login-page" style="background: url('dist/img/bg.jpg') no-repeat center">
    <div class="login-box">               
        <div class="login-logo">
            <a href="#">
                <img src="dist/img/logo.png" />
            </a>    
        </div>
        <p align="center"><strong>Bienvenido a la Agenda Médica, por favor inicie su sesión.</strong></p>
        <!-- /.login-logo -->
        <div class="login-box-body" style=" box-shadow: 0 4px 8px 0 rgba(0, 0, 0, 0.2), 0 6px 20px 0 rgba(0, 0, 0, 0.19);">
              <%--------------------- Avisos en pantalla-------------------------------%>                   
                    <asp:Panel ID="PanelAvisos" runat="server" Visible="false">
                            <div class="row">
            	                <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                	                <div class="alert alert-warning alert-dismissable">
                                    <button type="button" class="close" data-dismiss="alert"><i class="fa fa-close"></i> </button>
                                    <p><asp:Label ID="LblMensajeAviso" runat="server"></asp:Label></p>
                  	                </div>
                                </div>
                            </div>
                        </asp:Panel>
                        <asp:Panel ID="PanelCritico" runat="server" Visible="false">                              
                    <div class="alert alert-danger" role="alert">
                      <button type="button" class="close"  data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                        <i class="icon fa fa-ban"><asp:Label ID="LblMensajeCritico" runat="server" Text="Label"></asp:Label></i>
                    </div>
                   </asp:Panel>         
                       
                <%--    ---------------------------------------------------------------------------------------------%>
            <p class="login-box-msg">Inicio de Sesión</p>
            <form runat="server">
                <div class="form-group has-feedback">
                    <asp:TextBox ID="TxtUsuario" runat="server" CssClass="form-control" placeholder="Usuario" MaxLength="20"  onkeypress="return ValSoloLetrasEspacioBlanco(event)" ></asp:TextBox>
                    <span class="glyphicon glyphicon-user form-control-feedback"></span>
                </div>
                <div class="form-group has-feedback">
                    <asp:TextBox ID="TxtPass" runat="server" class="form-control" placeholder="Contraseña" TextMode="Password" MaxLength="20"></asp:TextBox>
                    <span class="glyphicon glyphicon-lock form-control-feedback"></span>
                </div>
                <div class="row">
                    <div class="col-xs-8 hidden">
                        <div class="checkbox icheck">
                            <label>
                                <input type="checkbox">
                                Recordarme
           
                            </label>
                        </div>
                    </div>
                    <!-- /.col -->
                    <div class="col-xs-12">
                        <button type="button" class="btn btn-success btn-block btn-flat" id="BtnIniciarSesion" runat="server" onserverclick="BtnIniciarSesion_Click">Iniciar <i class="fa fa-chevron-circle-right"></i></button>
                    </div>
                    <!-- /.col -->
                </div>
            </form>

            <div class="social-auth-links text-center hidden">
                <p>- OR -</p>
                <a href="#" class="btn btn-block btn-social btn-facebook btn-flat"><i class="fa fa-facebook"></i>Siguenos en 
        Facebook</a>
                <a href="#" class="btn btn-block btn-social btn-google btn-flat"><i class="fa fa-google-plus"></i>Siguenos en 
        Google+</a>
            </div>
            <!-- /.social-auth-links -->

            <!--a href="#">Olvide mi password</!--a><br>
            <a href="register.html" class="text-center">Registrate</a-->

        </div>
        <!-- /.login-box-body -->
    </div>
    <!-- /.login-box -->

    <!-- jQuery 2.2.0 -->
    <script src="../../plugins/jQuery/jQuery-2.2.0.min.js"></script>
    <!-- Bootstrap 3.3.6 -->
    <script src="../../bootstrap/js/bootstrap.min.js"></script>
    <!-- iCheck -->
    <script src="../../plugins/iCheck/icheck.min.js"></script>
    <script src="Jquery/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="ionic/js/ionic.min.js" type="text/javascript"></script>
    <script src="ionic/js/ionic.bundle.min.js" type="text/javascript"></script>
    <script src="almacenarlocal.js" type="text/javascript"></script>
    <script>
        $(function () {
            $('input').iCheck({
                checkboxClass: 'icheckbox_square-blue',
                radioClass: 'iradio_square-blue',
                increaseArea: '20%' // optional
            });
        });
        $(document).ready(function () {
            $("<%=BtnIniciarSesion.ClientID%>").click(function () {
                /*Captura de datos escrito en los inputs*/
                var nom = document.getElementById("TxtUsuario").textContent;

                /*Guardando los datos en el LocalStorage*/
                localStorage.setItem("Nombre", nom);

            });
        });
</script>
</body>
</html>
