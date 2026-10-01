Public Class Perfil
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            ocultarPaneles()
            bucarDatos()
        End If
    End Sub

    Private Sub bucarDatos()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT top 1 nombre,primerApellido,segundoApellido,login,password  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 "where codigoUsuario='" & Session("codigoUsuario") & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                nombre_usuario.Value = dt.Rows(0).Item("login")
                nombre.Value = dt.Rows(0).Item("nombre")
                pApellido.Value = dt.Rows(0).Item("primerApellido")
                sApellido.Value = dt.Rows(0).Item("segundoApellido")
                pass1.Value = dt.Rows(0).Item("password")
                Pass2.Value = dt.Rows(0).Item("password")
            Else
                LblMensajeAdvertencia.Text = "No se encontro el usuario, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL, password As String
        If pass1.Value = Pass2.Value Then
            password = pass1.Value
        Else
            LblMensajeAdvertencia.Text = " la contraseña no es correcta, favor de escribir de nuevo"
            PanelAdvertencia.Visible = True
            Exit Sub
        End If
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] set password='" & pass1.Value.Trim & "',nombre='" & nombre.Value.Trim & "',primerApellido ='" & pApellido.Value.Trim & "',segundoApellido='" & sApellido.Value.Trim & "'" & _
                 "where codigoUsuario='" & Session("codigoUsuario") & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se realizo el cambio correctamente"
            PanelAvisos.Visible = True
            bucarDatos()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Agenda.aspx")
    End Sub

    Protected Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
    End Sub



End Class