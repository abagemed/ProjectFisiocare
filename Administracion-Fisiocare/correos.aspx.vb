Imports System.Data.SqlClient
Partial Class correos
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Dim contenidoCorreo As String = "<img src='cid:Imagen1' />"
        Dim contenidoCorreo As String = "<table border='0' cellpadding='0' cellspacing='0' style='width: 100%; text-align:center'>" & _
        "<tr><td ><a href='http://www.zonamedicasureste.com'><img src='cid:Imagen1' /></a></td></tr>" & _
        "</table> <br /> " & _
        "<p align='center' class='MsoNormal' style='margin: 0cm 0cm 0pt; text-align: center'>" & _
        "<span style='font-size: 16pt'><a href='http://www.zonamedicasureste.com/productos.php'><img src='cid:Imagen3' /></a>" & _
        "<p align='center' class='MsoNormal' style='margin: 0cm 0cm 0pt; text-align: center'>" & _
        "<span style='font-size: 16pt'><a href='https://www.google.com.mx/maps/place/Zona+M%C3%A9dica/@21.0153083,-89.5872239,17z/data=!4m6!1m3!3m2!1s0x8f567726d46371f3:0xd5925a840a7aa397!2sZona+M%C3%A9dica!3m1!1s0x8f567726d46371f3:0xd5925a840a7aa397'><img src='cid:Imagen4' /></a>" & _
        "<br>" & _
        "<p align='center' class='MsoNormal' style='margin: 0cm 0cm 0pt; text-align: center'>" & _
        "<span style='font-size: 16pt'><a href='https://www.facebook.com/ZonaMedicaSureste/'><img src='cid:Imagen2' /></a>" & _
        "<br>" & _
        "<p align='center' class='MsoNormal' style='margin: 0cm 0cm 0pt; text-align: center'>" & _
        "<span style='font-size: 16pt'><a href='http://www.zonamedicasureste.com/contacto.php'><img src='cid:Imagen5' /></a>" & _
        "<br>" & _
        "<?xml  namespace='' ns='urn:schemas-microsoft-com:office:office' prefix='o' ?><o:p></o:p></span></p>" & _
        "<p align='center' class='MsoNormal' style='margin: 0cm 0cm 0pt; text-align: left'>" & _
        "</td></tr><tr><td sytle='font-size:5pt'>Si desea dar de baja este correo y no seguir recibiendo nuestras promociones favor de dar click en el siguiente enlace <a href='www.zonamedicasureste.com/bajacorreo.php'>Baja</a><td></tr>" & _
        "</td></tr><tr><td sytle='font-size:5pt'>,Y si desea conocer nuestras politicas de seguridad y privacidad de datos favor de dar click en el siguiente enlace <a href='www.zonamedicasureste.com/politicas.php'>Ver</a> o marque el 254-10-20/926-38-80 <td></tr>"

        enviaCorreo("ti@med-sol.mx", contenidoCorreo)
        'enviaCorreo("fcamara@sureste.com", contenidoCorreo)
        'enviaCorreo("hrivero@med-sol.mx", contenidoCorreo)
        'enviaCorreo("gvaldez@med-sol.mx", contenidoCorreo)
        Dim conexion As SqlConnection = funciones.conecta("dbcorreos")
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "select correo from clientesmd where bloque='" + txtbloque.Text.Trim + "'"
        'comando.CommandText = "select correo from correos where activo='true'"
        'comando.CommandText = "select correo from pcorreos where activo='true'"
        conexion.Open()
        Dim oread As SqlDataReader = comando.ExecuteReader
        Dim cuenta As Integer = 0
        Do While oread.Read
            cuenta = cuenta + enviaCorreo(oread.GetValue(0).ToString.Trim, contenidoCorreo)
        Loop
        oread.Close()
        conexion.Close()
        Label1.Text = "Se enviaron un total de " + cuenta.ToString
    End Sub

    Function enviaCorreo(ByVal elMail As String, ByVal eltexto As String) As Integer
        Try
            Dim correo As New System.Net.Mail.MailMessage
            correo.From = New System.Net.Mail.MailAddress("informacion@zonamedicasureste.com", "Zona Medica", System.Text.Encoding.UTF8)
            'correo.From = New System.Net.Mail.MailAddress("info@fisiocare.com.mx", "FISIOCARE", System.Text.Encoding.UTF8)
            correo.To.Add(elMail)
            correo.Subject = "Ahorra el 15% en mes de Abril"
            correo.SubjectEncoding = System.Text.Encoding.UTF8
            correo.Body = eltexto
            correo.Priority = Net.Mail.MailPriority.Normal

            Dim html As System.Net.Mail.AlternateView
            html = System.Net.Mail.AlternateView.CreateAlternateViewFromString(eltexto, Nothing, "text/html")
            Dim imagen As System.Net.Mail.LinkedResource
            imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\marketingzonamedia.JPG"))
            'imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\marketingFisiocare.JPG"))
            imagen.ContentId = "Imagen1"
            html.LinkedResources.Add(imagen)
            '''''''----------comentar esto cuando se envia desde fisiocare
            imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\face.JPG"))
            imagen.ContentId = "Imagen2"
            html.LinkedResources.Add(imagen)
            '''''''----------
            '''''''----------comentar esto cuando se envia desde fisiocare
            imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\productos.JPG"))
            imagen.ContentId = "Imagen3"
            html.LinkedResources.Add(imagen)
            '''''''----------
            '''''''----------comentar esto cuando se envia desde fisiocare
            imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\sucursales.JPG"))
            imagen.ContentId = "Imagen4"
            html.LinkedResources.Add(imagen)
            '''''''----------
            '''''''----------comentar esto cuando se envia desde fisiocare
            imagen = New System.Net.Mail.LinkedResource(HttpContext.Current.Server.MapPath("imgCorreos\correo.JPG"))
            imagen.ContentId = "Imagen5"
            html.LinkedResources.Add(imagen)
            '''''''----------

            correo.AlternateViews.Add(html)

            'correo.Attachments.Add(New System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("imgCorreos\marketingFisiocare.JPG")))
            correo.Attachments.Add(New System.Net.Mail.Attachment(HttpContext.Current.Server.MapPath("imgCorreos\marketingzonamedia.JPG")))
            Dim smtp As New System.Net.Mail.SmtpClient
            smtp.UseDefaultCredentials = False
            smtp.Credentials = New System.Net.NetworkCredential("informacion@zonamedicasureste.com", "zonam123")
            'smtp.Credentials = New System.Net.NetworkCredential("info@fisiocare.com.mx", "info123")
            smtp.Port = 587
            'smtp.Port = 25
            'smtp.Host = "mail.fisiocare.com.mx"
            smtp.Host = "mail.zonamedicasureste.com"
            smtp.DeliveryMethod = Net.Mail.SmtpDeliveryMethod.Network
            smtp.Send(correo)

            Return 1
        Catch
            Return 0
        End Try
    End Function
End Class
