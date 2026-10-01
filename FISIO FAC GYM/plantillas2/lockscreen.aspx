<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="lockscreen.aspx.vb" Inherits="AgeMED.lockscreen" %>

<!DOCTYPE html>
<html>
<head>
  <meta charset="utf-8">
  <meta http-equiv="X-UA-Compatible" content="IE=edge">
  <title>AdminLTE 2 | Lockscreen</title>
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

  <!-- HTML5 Shim and Respond.js IE8 support of HTML5 elements and media queries -->
  <!-- WARNING: Respond.js doesn't work if you view the page via file:// -->
  <!--[if lt IE 9]>
  <script src="https://oss.maxcdn.com/html5shiv/3.7.3/html5shiv.min.js"></script>
  <script src="https://oss.maxcdn.com/respond/1.4.2/respond.min.js"></script>
  <![endif]-->
    <script src="Jquery/jquery-2.1.1.min.js" type="text/javascript"></script>
    <script src="ionic/js/ionic.min.js" type="text/javascript"></script>
    <script src="ionic/js/ionic.bundle.min.js" type="text/javascript"></script>
    <script src="almacenarlocal.js" type="text/javascript"></script>
</head>
    <script>
    $(document).ready(function(){                                                        
        /*Obtener datos almacenados*/
        var nombre = localStorage.getItem("Nombre");         
               
        /*Mostrar datos almacenados*/      
        document.getElementById("<%=txtUsuario.ClientID%>").text= nombre;                
 
});
 </script>
<body class="hold-transition lockscreen">
<!-- Automatic element centering -->
<div class="lockscreen-wrapper">
  <div class="lockscreen-logo">
    <a href="../../login.aspx"><b>Ege</b>MED</a>
  </div>
  <!-- User name -->
  <div class="lockscreen-name">
     <asp:Label ID="txtUsuario" runat="server" text="Gerardo Valdez"></asp:Label>
  </div>

  <!-- START LOCK SCREEN ITEM -->
  <div class="lockscreen-item">
    <!-- lockscreen image -->
    <div class="lockscreen-image">
      <img src="../../dist/img/user1-128x128.jpg" alt="User Image">
    </div>
    <!-- /.lockscreen-image -->

    <!-- lockscreen credentials (contains the form) -->
    <form class="lockscreen-credentials">
      <div class="input-group">
        <input type="password" class="form-control" placeholder="password">

        <div class="input-group-btn">
          <button type="button" class="btn"><i class="fa fa-arrow-right text-muted"></i></button>
        </div>
      </div>
    </form>
    <!-- /.lockscreen credentials -->

  </div>
  <!-- /.lockscreen-item -->
  <div class="help-block text-center">
    Ingresa tu password para inicia sesión
  </div>
  <div class="text-center">
    <a href="login.aspx">Si eres un diferente usuario</a>
  </div>
  <div class="lockscreen-footer text-center">
    MedSolution del Sureste A.C.  &copy; 2014-2016 <b><a href="http://med-sol.mx" target="_blank" class="text-black">MedSol</a></b><br>
    Todos los derechos reservados.
  </div>
</div>
<!-- /.center -->

<!-- jQuery 2.2.0 -->
<script src="../../plugins/jQuery/jQuery-2.2.0.min.js"></script>
<!-- Bootstrap 3.3.6 -->
<script src="../../bootstrap/js/bootstrap.min.js"></script>
</body>
</html>
