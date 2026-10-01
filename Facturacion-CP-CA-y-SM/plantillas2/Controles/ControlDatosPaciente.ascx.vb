Public Class BuscadorPaciente
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim idpaciente, strSQL As String
            Dim dt As New DataTable
            Dim funciones As New FuncionesGenerales
            Dim clsdatos As New ClaseDatos
            idpaciente = Request.QueryString("idPaciente")

            strSQL = "SELECT nombres, papellido,sapellido, direccion,celular,email, genero, dtnacimiento  FROM [dbho].[dbo].[pacientes] where idpaciente=" + idpaciente
            If clsdatos.cargatabla(strSQL, dt) = 0 Then

                txtNombres.Value = dt.Rows(0).Item("nombres")
                TxtPapellido.Value = dt.Rows(0).Item("papellido")
                TxtSapellido.Value = dt.Rows(0).Item("sapellido")
                txtDireccion.Text = dt.Rows(0).Item("direccion")
                txtTelefonoMovil.Value = dt.Rows(0).Item("celular")
                txtEmail.Text = dt.Rows(0).Item("email")
                If CStr(dt.Rows(0).Item("genero")) = "M" Then
                    rbhombre.Checked = True
                    rbrdmujer.Checked = False
                Else
                    rbhombre.Checked = False
                    rbrdmujer.Checked = True

                End If

            End If


        End If
    End Sub

End Class