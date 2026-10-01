
Imports System.Data

Partial Class login
    Inherits System.Web.UI.Page

    Dim funciones As New miclases
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click

        Dim claseDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT idusuario,password,rol,usuario from usuarios where usuario='" + txtusuario.Text.Trim + "' and password='" + txtpass.Text.Trim + "'"

        If claseDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If txtpass.Text.Trim = dt.Rows(0).Item("password") Then
                    Session.Add("idusuario", dt.Rows(0).Item("idusuario"))
                    Session.Add("usuario", dt.Rows(0).Item("usuario"))
                    Session.Add("passusuario", dt.Rows(0).Item("password"))
                    Session.Add("rol", dt.Rows(0).Item("rol"))
                    Session.Add("FechaAgenda", Format(Date.Now, "dd/MM/yyyy"))
                    'Context.Items.Add("idusuario", dt.Rows(0).Item("idusuario"))
                    'Server.Transfer("default.aspx")
                    Response.Redirect("default.aspx")
                Else
                    Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
                End If
            Else
                Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
            End If
        Else

            Messagebox1.ShowMessage(claseDatos.MensajeError)

        End If





        'Dim idusuario As String = funciones.leerValor("select idusuario,rol from usuarios where usuario='" + txtusuario.Text.Trim + "' and password='" + txtpass.Text.Trim + "'")
        'If idusuario <> 0 Then
        '    Context.Items.Add("idusuario", idusuario)
        '    Server.Transfer("default.aspx")
        'Else
        '    Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
        'End If
    End Sub
End Class