Imports System.Data
Partial Class login
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'validacion y cambio de proceso de login AB13052020
        Dim claseDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT idusuario,password,rol from usuarios where usuario='" + txtusuario.Text.Trim + "' and password='" + txtpass.Text.Trim + "'"

        If claseDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If txtpass.Text.Trim = dt.Rows(0).Item("password") Then
                    Session.Add("idusuario", dt.Rows(0).Item("idusuario"))
                    Session.Add("rol", dt.Rows(0).Item("rol"))
                    Session.Add("FechaAgenda", Format(Date.Now, "dd/MM/yyyy"))
                    'Context.Items.Add("idusuario", dt.Rows(0).Item("idusuario"))
                    'Server.Transfer("default.aspx")
                    If dt.Rows(0).Item("rol") = "TE" Then
                        Response.Redirect("consultaexiscostoaleatorioA4zm.aspx")
                    Else

                        Response.Redirect("Defadmon.aspx")
                    End If
                Else
                    Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
                End If
                Else
                    Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
                End If
        Else

            Messagebox1.ShowMessage(claseDatos.MensajeError)

        End If








        'Dim idusuario As String = funciones.leerValor("select idusuario from usuarios where usuario='" + txtusuario.Text.Trim + "' and password='" + txtpass.Text.Trim + "'", "admFisio")
        'If idusuario <> 0 Then
        '    Context.Items.Add("idusuario", idusuario)
        '    Context.Items.Add("usuario", idusuario)
        '    Server.Transfer("Defadmon.aspx")
        'Else
        '    Messagebox1.ShowMessage("usuario y/o contraseña invalidos")
        'End If
    End Sub
End Class