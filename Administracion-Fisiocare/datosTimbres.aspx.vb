Imports mx.fel.www
Partial Class datosTimbres
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim timbrarFac As New WS_TFD
        Dim respuesta = timbrarFac.ConsultarCreditos("SGO060207MQ0", "ZRg2jwDD#")
        Label1.Text = respuesta(0)
    End Sub
End Class
