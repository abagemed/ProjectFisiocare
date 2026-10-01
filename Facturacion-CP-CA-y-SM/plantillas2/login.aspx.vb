Public Class login
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        PanelCritico.Visible = False
        If Not IsPostBack Then
            If Not (Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing) Then
                Response.Redirect("Agenda.aspx")
            End If
        End If
    End Sub

    Protected Sub BtnIniciarSesion_Click(sender As Object, e As EventArgs)
        Dim claseDatos As New ClaseDatos
        Dim clsmetodoVoz As New MetodoVoz
        Dim strSql As String
        Dim texto As String
        Dim dt As New DataTable
        strSql = "SELECT U.codigoUsuario,U.codigoEmpresa,U.nombre+' '+U.primerApellido as nomempleado,U.especialidad,U.password,U.tipo,U.rol, " & _
                 " C.CodigoConsultorio " & _
                 "FROM [" & claseDatos.BaseDatos & "].[dbo].[CatUsuarios] as U " & _
                  "inner join [" & claseDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] as C on U.codigoUsuario=C.CodigoUsuarioEnlazado and U.codigoEmpresa=C.CodigoEmpresa " & _
                  "where  C.status='1' and U.status='1' and U.login='" & TxtUsuario.Text & "'"
        If claseDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If TxtPass.Text = dt.Rows(0).Item("password") Then
                    Session.Add("sesion", dt.Rows(0).Item("nomempleado"))
                    Session.Add("especialidad", dt.Rows(0).Item("especialidad"))
                    Session.Add("codigoEmpresa", dt.Rows(0).Item("codigoEmpresa"))
                    Session.Add("codigoConsultorio", dt.Rows(0).Item("codigoConsultorio"))
                    Session.Add("codigoUsuario", dt.Rows(0).Item("codigoUsuario"))
                    Session.Add("FechaAgenda", Format(Date.Now, "dd/MM/yyyy"))
                    Session.Add("rol", dt.Rows(0).Item("rol"))
                    Session.Add("tipo", dt.Rows(0).Item("tipo"))
                    Session.Add("modalidad", "")
                    Session.Add("retornarPagina", "")

                    If Session("codigoUsuario") = "5" Then
                        Session.Add("foto", "~/dist/img/5.jpg")
                    ElseIf Session("codigoUsuario") = "4" Then
                        Session.Add("foto", "~/dist/img/4.jpg")
                    ElseIf Session("codigoUsuario") = "7" Then
                        Session.Add("foto", "~/dist/img/7.jpg")
                    ElseIf Session("codigoUsuario") = "55" Then
                        Session.Add("foto", "~/dist/img/55.jpg")
                    ElseIf Session("especialidad") = "RECEPCION" Then
                        Session.Add("foto", "~/dist/img/recepcion.jpg")
                    Else
                        Session.Add("foto", "~/dist/img/user.jpg")
                    End If
                    'If dt.Rows(0).Item("rol") = "DR" Then
                    '    Dim MyString As String = dt.Rows(0).Item("nomempleado")
                    '    Dim Newstring = MyString.Remove(0, 4)
                    '    texto = "Bienvenido Doctor " + NewString
                    '    clsmetodoVoz.ConverTexVoz(texto)
                    'Else
                    '    texto = "Bienvenido " + dt.Rows(0).Item("nomempleado")
                    '    clsmetodoVoz.ConverTexVoz(texto)
                    'End If

                    Response.Redirect("Agenda.aspx")
                Else
                    LblMensajeAviso.Text = "Usuario o contraseña incorrecto"
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                End If
            Else
                LblMensajeAviso.Text = "Usuario o contraseña incorrecto"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
            End If
        Else
            LblMensajeCritico.Text = claseDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If

    End Sub



End Class