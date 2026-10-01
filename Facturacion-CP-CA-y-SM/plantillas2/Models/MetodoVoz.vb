Imports System.Web
Imports System.Speech.Synthesis

Public Class MetodoVoz 
    Implements IHttpModule

    Private WithEvents _context As HttpApplication

    ''' <summary>
    '''  Deberá configurar este módulo en el archivo web.config de su
    '''  web y registrarlo en IIS para poder usarlo. Para obtener más información
    '''  vea el siguiente vínculo: http://go.microsoft.com/?linkid=8101007
    ''' </summary>
#Region "Miembros de IHttpModule"

    Public Sub Dispose() Implements IHttpModule.Dispose

        ' Ponga aquí el código de limpieza

    End Sub

    Public Sub Init(ByVal context As HttpApplication) Implements IHttpModule.Init
        _context = context
    End Sub

#End Region

    Public Sub OnLogRequest(ByVal source As Object, ByVal e As EventArgs) Handles _context.LogRequest

        ' Controla el evento LogRequest para proporcionar una implementación de 
        ' registro personalizado para él

    End Sub

    Sub ConverTexVoz(ByVal text As String)

        Dim audio = CreateObject("SAPI.spvoice")
        audio.volume = 100
        audio.rate = 0
        audio.speak(text)
    End Sub

End Class
