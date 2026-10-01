Imports System.Data
Public Class BloqueosTurnos
    Inherits System.Web.UI.Page
    Dim bloqueo() As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            TxtAgFecha2.Text = Session("FechaAgenda")
            Session("retornarPagina") = ""
            cargaConsultorios()
            cargaDTGturnos()
        End If
    End Sub
    Private Sub cargaConsultorios()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT CF.CodigoConsultorio, (u.nombre+' '+primerApellido+' '+segundoApellido) as DocCabecera " & _
               "FROM [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] as CF " & _
               "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
               "on C.codigoConsultorio=CF.CodigoConsultorio " & _
               "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U " & _
               "on C.idDoctorCabecera=U.codigoUsuario and C.CodigoEmpresa=U.CodigoEmpresa " & _
               "where CF.CodigoUsuarioEnlazado='" & Session("codigoUsuario") & "'  and CF.CodigoEmpresa='" & Session("codigoEmpresa") & "' "
        If funciones.llenadropdown(strSQL, DDconsultorios) <> 0 Then
            LblMensajeCritico.Text = funciones.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
        DDconsultorios.SelectedValue = Session("codigoConsultorio")
    End Sub

    Protected Sub TxtAgFecha2_TextChanged(sender As Object, e As EventArgs) Handles TxtAgFecha2.TextChanged
        actualizar()
    End Sub

    Function Funbloqueos(ByRef consultorio As String, ByRef fecha As Date)
        Dim strSQL As String
        Dim dt As New DataTable
        Dim a() As String
        Funbloqueos = False
        Dim clsDatos As New ClaseDatos
        strSQL = "SELECT turnos AS bloqueos FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgBloqueos] " & _
                 "WHERE codigoConsultorio='" & consultorio & "' AND fecha='" & fecha & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                a = Split(dt.Rows(0).Item("bloqueos"), "|", -1)
                bloqueo = a
                Funbloqueos = True
                HfActualizarOinsertar.Value = "UPDATE"
            Else
                HfActualizarOinsertar.Value = "INSERT"
            End If
        End If
    End Function

    Private Sub DDconsultorios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDconsultorios.SelectedIndexChanged
        actualizar()
    End Sub

    Private Sub cargaDTGturnos()
        Dim blnConBloqueo As Boolean
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = " EXECUTE  [" & clsDatos.BaseDatos & "].[dbo].[genhorarios] " & DDconsultorios.SelectedValue & "," & Session("codigoEmpresa") & ",N'G',1,N'" & CDate(TxtAgFecha2.Text) & "',0"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            dtgTurnos.DataSource = dt
            dtgTurnos.DataBind()
            blnConBloqueo = Funbloqueos(DDconsultorios.SelectedValue, TxtAgFecha2.Text)
            If blnConBloqueo = True Then
                For Each i As String In bloqueo
                    For Each row As GridViewRow In dtgTurnos.Rows
                        If row.Cells(0).Text = i Then
                            row.Cells(2).Text = "Bloqueado"
                            CType(row.Cells(4).Controls(1), CheckBox).Checked = True
                        End If
                    Next
                Next
            End If
            strSQL = "SELECT A.folioConsulta,A.numeroTurno,PApellido+' '+Sapellido+' '+P.nombres AS paciente,P.TelefonoCelular,P.TelefonoLocal,A.codigoPaciente,A.CodigoEtapa " & _
                      "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] AS A " & _
                      "INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] AS P ON A.CodigoPaciente=P.CodigoPaciente AND A.codigoEmpresa=P.CodigoEmpresa " & _
                      "WHERE A.codigoConsultorio='" & DDconsultorios.SelectedValue & "' AND A.fechaAgenda='" & TxtAgFecha2.Text & "' AND A.CodigoEtapa<>'4' AND A.codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                      "ORDER BY A.numeroTurno"
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    For Each fila As DataRow In dt.Rows
                        For Each turno As GridViewRow In dtgTurnos.Rows
                            If fila.Item("numeroTurno") = turno.Cells(0).Text Then
                                turno.Cells(2).Text = fila.Item("paciente")
                                turno.Cells(3).Text = "Cel: " + fila.Item("TelefonoCelular") + vbCr + " Local: " + fila.Item("TelefonoLocal")
                                turno.Cells(5).Text = fila.Item("folioConsulta")
                                turno.Cells(6).Text = fila.Item("codigoPaciente")
                                If fila.Item("CodigoEtapa") = "6" Then
                                    turno.Cells(3).Text = "CONSULTA FINALIZADA"
                                    CType(turno.FindControl("BtnMover"), Button).Visible = False
                                    CType(turno.FindControl("BtnCancelar"), Button).Visible = False
                                    CType(turno.FindControl("chk"), CheckBox).Visible = False
                                    turno.BackColor = Drawing.Color.SteelBlue
                                Else
                                    CType(turno.FindControl("BtnMover"), Button).Visible = True
                                    CType(turno.FindControl("BtnCancelar"), Button).Visible = True
                                    CType(turno.FindControl("chk"), CheckBox).Visible = False
                                End If
                            End If
                        Next
                    Next
                End If
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
        dtgTurnos.Columns(0).Visible = False
        dtgTurnos.Columns(5).Visible = False
        dtgTurnos.Columns(6).Visible = False
    End Sub

    Protected Sub BtnBloquear_Click(sender As Object, e As EventArgs)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim ListBloqueados As String
        Dim count As Integer
        count = 0
        ListBloqueados = ""
        For Each row As GridViewRow In dtgTurnos.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then
                If count > 0 Then
                    ListBloqueados += "|"
                End If
                ListBloqueados += row.Cells(0).Text
                count = count + 1
            End If
        Next
        If HfActualizarOinsertar.Value = "UPDATE" Then
            strSQL = "UPDATE [" & clsDatos.BaseDatos & "].[dbo].[AgBloqueos] set Turnos='" & ListBloqueados & "',FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),codigoUsuario='" & Session("codigoUsuario") & "'  " & _
                     "WHERE codigoConsultorio='" & DDconsultorios.SelectedValue & "' and fecha='" & TxtAgFecha2.Text & "' and codigoEmpresa='" & Session("codigoEmpresa") & "'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                LblMensajeAviso.Text = " Se actualizo la lista de bloqueos"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                actualizar()
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
        If HfActualizarOinsertar.Value = "INSERT" Then
            strSQL = "INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[AgBloqueos] VALUES ('" & DDconsultorios.SelectedValue & "','" & TxtAgFecha2.Text & "','" & ListBloqueados & "','" & Session("codigoEmpresa") & "','1',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),'" & Session("codigoUsuario") & "')"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                LblMensajeAviso.Text = " Se actualizo la lista de bloqueos"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                actualizar()
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
    End Sub

    Protected Sub Button1_Click(sender As Object, e As EventArgs) Handles BtnTodo.Click
        If BtnTodo.Text = "Seleccionar todo" Then
            For Each row As GridViewRow In dtgTurnos.Rows
                If row.Cells(2).Text = "" Or row.Cells(2).Text = "Bloqueado" Then
                    CType(row.FindControl("chk"), CheckBox).Checked = True
                End If
            Next

            BtnTodo.Text = "Deseleccionar todo"
            BtnTodo.CssClass = "btn btn-block btn-warning btn-lg"
        Else
            For Each row As GridViewRow In dtgTurnos.Rows
                CType(row.FindControl("chk"), CheckBox).Checked = False
            Next
            BtnTodo.Text = "Seleccionar todo"
            BtnTodo.CssClass = "btn btn-block btn-success btn-lg"
        End If
    End Sub
    Private Sub actualizar()
        dtgTurnos.Columns(0).Visible = True
        dtgTurnos.Columns(5).Visible = True
        dtgTurnos.Columns(6).Visible = True
        BtnTodo.Text = "Seleccionar todo"
        BtnTodo.CssClass = "btn btn-block btn-success btn-lg"
        cargaDTGturnos()
    End Sub
    Private Sub cancelarCita()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "UPDATE [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set codigoEtapa='4',codigoUsuario='" & Session("codigoUsuario") & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                  "WHERE folioConsulta='" & Session("codigoConsulta") & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            Session("modalidad") = ""
            LblMensajeAviso.Text = " Se realizó la cancelación de la cita"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

  
    Private Sub dtgTurnos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dtgTurnos.RowCommand
      
        If e.CommandName = "BtnCancelar" Then
            LblMostarDecision.Text = " ¿Deseas cancelar la cita?"
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            HdPreguntas.Value = "0"
            HdIndex.Value = CInt(e.CommandArgument)
        End If
        If e.CommandName = "BtnMover" Then
            Dim index As Integer
            index = CInt(e.CommandArgument)
            Session("codigoConsultorio") = DDconsultorios.SelectedValue
            Session("codigoConsulta") = dtgTurnos.Rows(index).Cells(5).Text
            Session("FechaAgenda") = TxtAgFecha2.Text
            Session("nombrePaciente") = dtgTurnos.Rows(index).Cells(2).Text
            Session("codigoPaciente") = dtgTurnos.Rows(index).Cells(6).Text
            Session("modalidad") = "mover"
            Session("retornarPagina") = "BloqueosTurnos.aspx"
            Response.Redirect("ReAgendarPaciente.aspx")
        End If
    End Sub

    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub
    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntas.Value
            Case "0"
                Session("codigoConsulta") = dtgTurnos.Rows(HdIndex.Value.ToString).Cells(5).Text
                cancelarCita()
                actualizar()
        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)

    End Sub
End Class