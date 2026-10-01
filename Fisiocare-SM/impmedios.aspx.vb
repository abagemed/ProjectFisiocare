
Partial Class impmedios
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim aux As String = Request.QueryString("x").ToString
        DataGrid1 = funciones.creadataset("select id, idTerapia, texto, tipo FROM ejercicios " & _
        "where idterapia='1236' ORDER BY tipo ", DataGrid1)
        Dim cRow As Integer = DataGrid1.Items.Count
        Dim cuenta As Integer = 0
        Do While cuenta < cRow
            Select Case DataGrid1.Items(cuenta).Cells(0).Text
                Case Is = "E"
                    DataGrid1.Items(cuenta).Cells(0).Text = "EJERCICIOS Y RUTINAS"
                Case Is = "M"
                    DataGrid1.Items(cuenta).Cells(0).Text = "MEDIOS FISICOS"
                Case Is = "O"
                    DataGrid1.Items(cuenta).Cells(0).Text = "CONTRAINDICACIONES"
            End Select
            cuenta = cuenta + 1
        Loop
    End Sub
End Class
