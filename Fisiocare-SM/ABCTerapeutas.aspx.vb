Imports System.Data.SqlClient
Imports System.Data
Partial Class ABCTerapeutas
    Inherits System.Web.UI.Page
    Public funciones As New miclases
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            If Session("rol") = "RE" Then
                Server.Transfer("default.aspx", False)
            End If

            Dim funciones As New miclases

            Try
                lblfechacita.Text = Context.Items("fecha").ToString.Trim
            Catch
                lblfechacita.Text = Request.QueryString("fecha").Trim
            End Try

            CargarGrdTerapeutas()
            LlenarComboColumnas()
            LlenarComboColumnasAlta()

        End If
    End Sub


    Private Sub CargarGrdTerapeutas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String

        strSQL = "Select id, Nombre,pass, columna,iif(turno = 'M','MATUTINO','VESPERTINO') as turno  " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] " & _
                 "where codigoEmpresa = 1  And activo = 'True' order by id   "

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdTerapeutas.DataSource = dt
                GdTerapeutas.DataBind()
            Else
                LblMensajeAdvertencia.Text = "No se encontraron datos, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Private Sub GdTerapeutas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdTerapeutas.RowCommand
        If e.CommandName = "Editar" Then
            ocultarPaneles()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#Editar_1').modal('show');</script>", False)
            HdTerapeutaEdit.Value = GdTerapeutas.Rows(e.CommandArgument).Cells(0).Text
            bucarDatos(GdTerapeutas.Rows(e.CommandArgument).Cells(0).Text)
        End If
        If e.CommandName = "Borrar" Then
            ocultarPaneles()
            HdTerapeutaEdit.Value = GdTerapeutas.Rows(e.CommandArgument).Cells(0).Text
            LblMostarDecision.Text = " ¿Está seguro(a) que desea borrar el Terapeuta " + GdTerapeutas.Rows(e.CommandArgument).Cells(1).Text + "?"
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
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] " & _
                "set activo='False' " & _
                "where id='" & HdTerapeutaEdit.Value & "' and codigoEmpresa=1 and activo='True'"
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar = 0 Then
                    ocultarPaneles()
                    CargarGrdTerapeutas()
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
    Protected Sub LlenarComboColumnas()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT IdColumna, Columna FROM [" & clsDatos.BaseDatos & "].[dbo].[CatColumnas] " & _
                " where status='0' and turno='" & selturnoe.SelectedItem.Value & "'"
        If funciones.llenadropdown(strSQL, secolumnae) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarComboColumnasEditar()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT IdColumna, Columna FROM [" & clsDatos.BaseDatos & "].[dbo].[CatColumnas] " & _
                " where IdColumna = '" & secolumnae.SelectedValue & "'"
        If funciones.llenadropdown(strSQL, secolumnae) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Private Sub seturnoe_SelectedIndexChanged(sender As Object, e As EventArgs) Handles selturnoe.SelectedIndexChanged

        LlenarComboColumnas()

    End Sub
    Protected Sub LlenarComboColumnasAlta()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT IdColumna, Columna FROM [" & clsDatos.BaseDatos & "].[dbo].[CatColumnas] " & _
                 " where status='0' and turno='" & seturnon.SelectedItem.Value & "'"
        If funciones.llenadropdown(strSQL, secolumnan) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If

    End Sub
    Private Sub seturnon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles seturnon.SelectedIndexChanged

        LlenarComboColumnasAlta()

    End Sub
    Private Sub bucarDatos(ByRef id As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "Select Nombre, activo, columna, pass, turno  " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] " & _
                 "where codigoEmpresa = 1 And activo = 'True' And id ='" & id & "' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                If Not IsDBNull(dt.Rows(0).Item("Nombre")) Then
                    txtNombree.Text = dt.Rows(0).Item("Nombre")
                End If

                If Not IsDBNull(dt.Rows(0).Item("pass")) Then
                    txtPasse.Text = dt.Rows(0).Item("pass")
                End If

                If Not IsDBNull(dt.Rows(0).Item("turno")) Then
                    If dt.Rows(0).Item("turno") = "M" Then
                        selturnoe.SelectedValue = "M"
                        selturnoe.Enabled = False
                    End If
                    If dt.Rows(0).Item("turno") = "V" Then
                        selturnoe.SelectedValue = "V"
                        selturnoe.Enabled = False
                    End If
                End If

                If Not IsDBNull(dt.Rows(0).Item("columna")) Then
                    secolumnae.SelectedItem.Value = dt.Rows(0).Item("columna")
                    LlenarComboColumnasEditar()
                    secolumnae.Enabled = False
                Else
                    secolumnae.SelectedItem.Value = 1
                    LlenarComboColumnas()
                End If

                If Not IsDBNull(dt.Rows(0).Item("activo")) Then
                    If dt.Rows(0).Item("activo") = "True" Then
                        ChkBoxActivoe.Checked = True
                    Else
                        ChkBoxActivoe.Checked = False
                    End If
                End If

            Else
                LblMensajeAdvertencia.Text = "No se encontró el Terapeuta, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If

    End Sub
    Protected Sub BtnGuardarEdit_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL, activoe As String

        If ChkBoxActivoe.Checked = True Then
            activoe = 1
        Else
            activoe = 0
        End If

        
        Dim turnoe As String = ""
        If selturnoe.SelectedValue = "M" Then
            turnoe = "M"
        End If

        If selturnoe.SelectedValue = "V" Then
            turnoe = "V"
        End If
        
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] " & _
                 "set Nombre='" & txtNombree.Text.ToUpper & "',pass='" & txtPasse.Text & "',turno='" & turnoe & "',activo='" & activoe & "',columna='" & secolumnae.SelectedValue & "' " & _
                 "where id='" & HdTerapeutaEdit.Value & "' and codigoEmpresa=1 and activo='True'"

        clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se realizó el cambio correctamente"
            PanelAvisos.Visible = True
                PanelAvisos.Focus()
            CargarGrdTerapeutas()
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
            End If
       
    End Sub
    Protected Sub btnGuardarn_Click(sender As Object, e As EventArgs) Handles btnGuardarn.Click
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL, activon As String

        If ChkBoxActivon.Checked = True Then
            activon = 1
        Else
            activon = 0
        End If

        Dim turnon As String = ""
        If seturnon.SelectedValue = "M" Then
            turnon = "M"
        End If
        If seturnon.SelectedValue = "V" Then
            turnon = "V"
        End If

        strSQL = "SELECT columna  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatColumnas] " & _
                            "where IdColumna='" & secolumnan.SelectedValue & "' and codigoEmpresa=1"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                HdIdColumna.Value = dt.Rows(0).Item("columna")
            End If
        End If

        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] (Nombre,pass,turno,columna,codigoEmpresa,activo) " & _
                        "values('" & txtNombren.Text.ToUpper & "','" & txtPassn.Text & "','" & turnon & "','" & HdIdColumna.Value & "',1,'" & activon & "' )" & _
        " update [" & clsDatos.BaseDatos & "].[dbo].[CatColumnas] " & _
                     "set status='1' " & _
                    "where IdColumna='" & secolumnan.SelectedValue & "' and codigoEmpresa=1"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se agregó el nuevo terapeuta correctamente"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdTerapeutas()
            limpiarvariables()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If


    End Sub
    Protected Sub limpiarvariables()
        
        txtNombren.Text = ""
        txtPassn.Text = ""
        ChkBoxActivon.Checked = True
        secolumnan.ClearSelection()
        seturnon.ClearSelection()
        LlenarComboColumnasAlta()


    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub
    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
    

    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
        Server.Transfer("clientes.aspx", True)
    End Sub

    Protected Sub Recibos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub Historial(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub Catalogos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub CorteCaja(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub BloqueoCitas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
    End Sub

    Protected Sub FacturasGeneradas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub

    Protected Sub VerCancelados(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub
    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub
    Protected Sub CTerapeutas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("ABCTerapeutas.aspx", False)
    End Sub
End Class