Public Class Usuarios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            CargarGrdUsuarios()
        End If
    End Sub

    Private Sub CargarGrdUsuarios()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoUsuario,(nombre+' '+primerApellido+' '+segundoApellido) as nombre,login,password,rol  " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 "where codigoEmpresa = " & Session("codigoEmpresa") & " And status = 1  "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdUsuarios.DataSource = dt
                GdUsuarios.DataBind()
            Else
                LblMensajeAdvertencia.Text = "No se encontro el usuario, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Private Sub bucarDatos(ByRef codigoUsuario As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT nombre,primerApellido,segundoApellido,especialidad,cedula,login,password,rol  " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 "where codigoEmpresa = " & Session("codigoEmpresa") & " And status = 1 And codigoUsuario ='" & codigoUsuario & "' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                txtLogin.Text = dt.Rows(0).Item("login")
                txtLogin.ReadOnly = True
                txtNombre.Text = dt.Rows(0).Item("nombre")
                txtPaterno.text = dt.Rows(0).Item("primerApellido")
                txtMaterno.text = dt.Rows(0).Item("segundoApellido")
                txtPass.Text = dt.Rows(0).Item("password")
                If Not IsDBNull(dt.Rows(0).Item("cedula")) Then
                    txtEspecialidad.text = dt.Rows(0).Item("especialidad")
                End If
                If Not IsDBNull(dt.Rows(0).Item("cedula")) Then
                    txtCedula.text = dt.Rows(0).Item("cedula")
                End If
                If Not IsDBNull(dt.Rows(0).Item("rol")) Then
                    If dt.Rows(0).Item("rol") = "RX" Then
                        rol1.Value = 1
                    End If
                    If dt.Rows(0).Item("rol") = "RE" Then
                        rol1.Value = 2
                    End If
                    If dt.Rows(0).Item("rol") = "DR" Then
                        rol1.Value = 3
                    End If

                End If

            Else
                LblMensajeAdvertencia.Text = "No se encontro el usuario, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnGuardarEdit_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim rol As String = ""
        If rol1.Value = 1 Then
            rol = "RX"
        End If
        If rol1.Value = 2 Then
            rol = "RE"
        End If
        If rol1.Value = 3 Then
            rol = "DR"
        End If

        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                "set nombre='" & txtNombre.Text.ToUpper & "',primerApellido='" & txtPaterno.Text.ToUpper & "',segundoApellido='" & txtMaterno.Text.ToUpper & "',especialidad='" & txtEspecialidad.Text.ToUpper & "',cedula='" & txtCedula.Text & "',password='" & txtPass.Text & "',rol='" & rol & "' " & _
                "where codigoUsuario='" & HdUsuarioEdit.Value & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status=1"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se realizo el cambio correctamente"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()

        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Protected Sub BtnGuardarNuevo_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim rol As String = ""
        If rol1.Value = 1 Then
            rol = "RX"
        End If
        If rol1.Value = 2 Then
            rol = "RE"
        End If
        If rol1.Value = 3 Then
            rol = "DR"
        End If
        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] (nombre,primerApellido,segundoApellido,especialidad,cedula,login,password,rol,status,codigoEmpresa) " & _
                    "values('" & txtNombre2.Text.ToUpper & "','" & txtPaterno2.Text.ToUpper & "','" & txtMaterno2.Text.ToUpper & "','" & txtEspecialidad2.Text.ToUpper & "','" & txtCedula2.Text & "','" & txtLogin2.Text & "','" & txtPass2.Text & "','" & rol & "',1," & Session("codigoEmpresa") & ")"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se agrego el nuevo usuario"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Agenda.aspx")
    End Sub

    Private Sub GdUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdUsuarios.RowCommand
        If e.CommandName = "Editar" Then
            ocultarPaneles()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#Editar_1').modal('show');</script>", False)
            HdUsuarioEdit.Value = GdUsuarios.Rows(e.CommandArgument).Cells(0).Text
            bucarDatos(GdUsuarios.Rows(e.CommandArgument).Cells(0).Text)
        End If
        If e.CommandName = "Borrar" Then
            ocultarPaneles()
            HdUsuarioEdit.Value = GdUsuarios.Rows(e.CommandArgument).Cells(0).Text
            LblMostarDecision.Text = " ¿Esta seguro(a) que desea borrar el usuario " + GdUsuarios.Rows(e.CommandArgument).Cells(1).Text + "?"
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            Hdpregunta.Value = "0"
        End If
    End Sub
    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case Hdpregunta.Value
            Case "0"
                Dim strSQL As String
                Dim clsDatos As New ClaseDatos
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                "set status=2 " & _
                "where codigoUsuario='" & HdUsuarioEdit.Value & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status=1"
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar = 0 Then
                    ocultarPaneles()
                    CargarGrdUsuarios()
                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If

        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Exit Sub
    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Protected Sub btnGuardar2_Click(sender As Object, e As EventArgs) Handles btnGuardar2.Click
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim rol As String = ""
        If rol1.Value = 1 Then
            rol = "RX"
        End If
        If rol1.Value = 2 Then
            rol = "RE"
        End If
        If rol1.Value = 3 Then
            rol = "DR"
        End If
        strSQL = "SELECT codigoUsuario  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                    "where login='" & txtLogin2.Text & "' and codigoEmpresa=" & Session("codigEmpresa") & " and status=1"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAviso.Text = "El nombre de usuario ya existe"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                Exit Sub
            End If
        End If
        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] (nombre,primerApellido,segundoApellido,especialidad,cedula,login,password,rol,status,codigoEmpresa) " & _
                    "values('" & txtNombre2.Text.ToUpper & "','" & txtPaterno2.Text.ToUpper & "','" & txtMaterno2.Text.ToUpper & "','" & txtEspecialidad2.Text.ToUpper & "','" & txtCedula2.Text & "','" & txtLogin2.Text & "','" & txtPass2.Text & "','" & rol & "',1," & Session("codigoEmpresa") & ")"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se agrego el nuevo usuario"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()
            limpiarvariables()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Protected Sub limpiarvariables()
        txtCedula2.Text = ""
        txtEspecialidad2.Text = ""
        txtLogin2.Text = ""
        txtMaterno2.Text = ""
        txtNombre2.Text = ""
        txtPass2.Text = ""
        txtPaterno2.Text = ""
    End Sub
End Class