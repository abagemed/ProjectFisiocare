<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ImpExReporte.aspx.vb" Inherits="ImpExReporte" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>ImprimirRecibos</title>
     <script>
    /**
     * Funcion que captura las variables pasados por GET
     * http://www.lawebdelprogramador.com/pagina.html?id=10&pos=3
     * Devuelve un array de clave=>valor
     */
    function getGET()
    {
        // capturamos la url
        var loc = document.location.href;
        // si existe el interrogante
        if(loc.indexOf('?')>0)
        {
            // cogemos la parte de la url que hay despues del interrogante
            var getString = loc.split('?')[1];
            // obtenemos un array con cada clave=valor
            var GET = getString.split('&');
            var get = {};
 
            // recorremos todo el array de valores
            for(var i = 0, l = GET.length; i < l; i++){
                var tmp = GET[i].split('=');
                get[tmp[0]] = unescape(decodeURI(tmp[1]));
            }
            return get;
        }
    }
 
    window.onload = function()
    {
        // Cogemos los valores pasados por get
        var valores=getGET();
        
        if(valores)
        {
            // hacemos un bucle para pasar por cada indice del array de valores
            for(var index in valores)
            {
                document.write('<embed src="Recibos/' + valores[index] + '" id ="embed" width="100%" height="700"></embed>');
               
            }
        }else{
            // no se ha recibido ningun parametro por GET
            document.write('<a href = "ImpExReporte.aspx?variable=PEREZPEREZRAULALFONSO05122014.pdf">asdasdasd</a>');
           
        }
    }
    
         </script>
    <%--<script language="JavaScript">

function cerrar() {
var ventana = window.self;
ventana.opener = window.self;
ventana.close();
}
setTimeout('cerrar()',5000); //5000 = 5 segundos.
</script>--%>

     <script type="text/javascript">
         function imprimir() {
             if (window.print) {
                 window.print();
             } else {
                 alert("La función de impresion no esta soportada por su navegador.");
             }
         }
        </script>

</head>
<body onload="imprimir();">
<%--<embed src="reportes/PEREZPEREZRAULALFONSO05122014.pdf" id = "embed" width="100%" height="700"></embed>
--%>
</body>
</html>
