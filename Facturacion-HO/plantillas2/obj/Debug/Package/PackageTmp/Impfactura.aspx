<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Impfactura.aspx.vb" Inherits="AgeMED.Impfactura" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script>
    /**
     * Funcion que captura las variables pasados por GET
     * Devuelve un array de clave=>valor
     */
    function getGET() {
        // capturamos la url
        var loc = document.location.href;
        // si existe el interrogante
        if (loc.indexOf('?') > 0) {
            // cogemos la parte de la url que hay despues del interrogante
            var getString = loc.split('?')[1];
            // obtenemos un array con cada clave=valor
            var GET = getString.split('&');
            var get = {};

            // recorremos todo el array de valores
            for (var i = 0, l = GET.length; i < l; i++) {
                var tmp = GET[i].split('=');
                get[tmp[0]] = unescape(decodeURI(tmp[1]));
            }
            return get;
        }
    }

    window.onload = function () {
        // Cogemos los valores pasados por get
        var valores = getGET();

        if (valores) {
            // hacemos un bucle para pasar por cada indice del array de valores
            for (var index in valores) {
                document.write('<embed src="Facturas/PDFS/' + valores[index] + '" id ="embed" width="100%" height="700"></embed>');
            }
        } else {
            // no se ha recibido ningun parametro por GET
           document.write('<a href = "ImpExReporte.aspx?variable=PEREZPEREZRAULALFONSO05122014.pdf">asdasdasd</a>');
        }
    }
    </script>

</asp:Content>
