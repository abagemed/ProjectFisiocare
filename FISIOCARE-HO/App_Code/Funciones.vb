Imports Microsoft.VisualBasic

Public Class Funciones
    Dim clases As New miclases
    Public Function completalista2(ByVal cadregistros As String, ByVal connom As String) As String
        Dim cadenafin, cad, cnombre, cita, status, idcliente, latencion As String
        Dim i, j
        Dim val1, val2, val3, z As Integer
        ' SE RECARGA LA LISTA CON LOS Q FALTAN
        cadenafin = ""
        cnombre = ""
        i = 0
        For i = 1 To clases.num_elementos(cadregistros, ",")
            cad = clases.entry(i, cadregistros, ",")
            val1 = CInt(clases.entry(1, cad, "-").ToString.Trim)
            val2 = CInt(clases.entry(2, cad, "-").ToString.Trim)
            val3 = CInt(clases.entry(3, cad, "-").ToString.Trim)
            cnombre = clases.entry(4, cad, "-").ToString.Trim
            cita = clases.entry(5, cad, "-").ToString.Trim
            status = clases.entry(6, cad, "-").ToString.Trim
            idcliente = clases.entry(7, cad, "-").ToString.Trim
            latencion = clases.entry(8, cad, "-").ToString.Trim
            z = val1
            For j = 1 To val3
                If connom = "1" Then                                                            'j
                    cadenafin = cadenafin.Trim + Str(z).Trim & "-" & Str(val2).Trim & "-" & Str(val3).Trim & "-" & cnombre & "-" & cita & "-" & status & "-" & idcliente & "-" & latencion & ","
                Else
                    cadenafin = cadenafin.Trim + Str(z).Trim & "-" & Str(val2).Trim & ","
                End If
                z = z + 1
            Next
        Next
        cadenafin = Mid(cadenafin, 1, cadenafin.Length - 1)
        Return cadenafin
    End Function
    Sub enviaCorreoXls(ByVal elMail As String, ByVal archivo As String, ByVal nameFile As String, ByVal asunto As String, ByVal eltexto As String)
        Dim correo As New System.Net.Mail.MailMessage
        correo.From = New System.Net.Mail.MailAddress("envios@fisiocare.com.mx", "FISIOCARE")
        For Each mail As String In elMail.Split(New Char() {","c})
            correo.To.Add(New System.Net.Mail.MailAddress(mail))
        Next
        correo.Subject = asunto
        correo.Body = eltexto
        correo.IsBodyHtml = True
        correo.Priority = Net.Mail.MailPriority.Normal
        If archivo <> "" Then
            Dim contentAsBytes As Byte() = Encoding.UTF8.GetBytes(archivo)
            Dim memStream As System.IO.MemoryStream = New System.IO.MemoryStream(contentAsBytes)
            Dim streamWriter As System.IO.StreamWriter = New System.IO.StreamWriter(memStream)
            streamWriter.Flush()
            memStream.Position = 0

            Dim thisAttachment As System.Net.Mail.Attachment = New System.Net.Mail.Attachment(memStream, vbNull) '"image/jpeg")
            thisAttachment.ContentDisposition.FileName = nameFile
            correo.Attachments.Add(thisAttachment)
        End If
        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "mail.fisiocare.com.mx"
        smtp.Port = 26
        smtp.Credentials = New System.Net.NetworkCredential("envios@fisiocare.com.mx", "envios123")
        smtp.Send(correo)
    End Sub
    Sub enviaCorreoPdf(ByVal elMail As String, ByVal archivo As String, ByVal xml As String, ByVal asunto As String, ByVal eltexto As String)
        Dim correo As New System.Net.Mail.MailMessage
        correo.From = New System.Net.Mail.MailAddress("facturassm@fisiocare.com.mx", "Fisiocare StarMedica")
        correo.To.Add(elMail)
        correo.Subject = asunto
        correo.Body = eltexto
        correo.IsBodyHtml = False
        correo.Priority = Net.Mail.MailPriority.Normal
        correo.Attachments.Add(New System.Net.Mail.Attachment(archivo))
        correo.Attachments.Add(New System.Net.Mail.Attachment(xml))

        Dim smtp As New System.Net.Mail.SmtpClient
        smtp.Host = "smtp.sendgrid.net"
        smtp.Port = 587
        smtp.Credentials = New System.Net.NetworkCredential("fisiocare.com.mx", "FisicoC@2016!!")
        smtp.Send(correo)
    End Sub
End Class
