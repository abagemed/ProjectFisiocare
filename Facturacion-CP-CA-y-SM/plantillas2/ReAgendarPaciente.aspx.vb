Public Class ReAgendarPaciente1
    Inherits System.Web.UI.Page
    Dim bloqueo() As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Not IsPostBack Then
            TxtAgFecha2.Text = Session("FechaAgenda")
            LblNombrePaciente.Text = Session("nombrePaciente")
            LblCodigoPaciente.Text = Session("codigoPaciente")
            cargaConsultorios()
            cargaTurnos()
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
    Private Sub cargaTurnos()
        Dim strSQL, incremento As String
        Dim Horario1, Horario2 As TimeSpan
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim array(65) As String
        Dim BlnBloqueos As Boolean
        strSQL = "SELECT inicioMat,finalMat,inicioVesp,finalVesp,tiempoTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] " & _
                 "where codigoConsultorio='" & DDconsultorios.SelectedValue & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            Horario1 = dt.Rows(0).Item("inicioMat")
            incremento = "00:" + dt.Rows(0).Item("tiempoTurno") + ":00"
            Horario2 = dt.Rows(0).Item("inicioMat") + TimeSpan.Parse(incremento)
            For i As Integer = 1 To 65
                array(i) = i.ToString + "    " + Horario1.ToString + " - " + Horario2.ToString
                Horario1 = Horario2
                Horario2 = Horario2 + TimeSpan.Parse(incremento)
            Next
            BlnBloqueos = Funbloqueos(DDconsultorios.SelectedValue, TxtAgFecha2.Text)
            If BlnBloqueos = True Then
                strSQL = "SELECT numeroTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                     "where  fechaAgenda='" & TxtAgFecha2.Text & "' and codigoConsultorio='" & DDconsultorios.SelectedValue & "' and codigoEtapa<>'4' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    For i As Integer = 1 To 65
                        For Each j As DataRow In dt.Rows
                            If i = j.Item("numeroTurno") Then
                                array(i) = "No Disponible"
                            End If
                        Next
                    Next
                    For i As Integer = 1 To 65
                        For Each j As String In bloqueo
                            If j = i.ToString Then
                                array(i) = "No Disponible"
                            End If
                        Next
                        If array(i) <> "No Disponible" Then
                            Dim listItem As New ListItem(array(i), i)
                            DDturnos.Items.Add(listItem)
                        End If
                    Next
                End If
            Else
                strSQL = "SELECT numeroTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                     "where  fechaAgenda='" & TxtAgFecha2.Text & "' and codigoConsultorio='" & DDconsultorios.SelectedValue & "' and codigoEtapa<>'4' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    For i As Integer = 1 To 65
                        For Each j As DataRow In dt.Rows
                            If i = j.Item("numeroTurno") Then
                                array(i) = "No Disponible"
                            End If
                        Next
                        If array(i) <> "No Disponible" Then
                            Dim listItem As New ListItem(array(i), i)
                            DDturnos.Items.Add(listItem)
                        End If
                    Next
                End If
            End If
        End If
    End Sub
    Protected Sub TxtAgFecha2_TextChanged(sender As Object, e As EventArgs) Handles TxtAgFecha2.TextChanged
        DDturnos.Items.Clear()
        cargaTurnos()
    End Sub

    Function Funbloqueos(ByRef consultorio As String, ByRef fecha As Date)
        Dim strSQL As String
        Dim dt As New DataTable
        Dim a() As String
        Funbloqueos = False
        Dim clsDatos As New ClaseDatos
        strSQL = "select turnos as bloqueos from  [" & clsDatos.BaseDatos & "].[dbo].[AgBloqueos] " & _
                 "where codigoConsultorio='" & consultorio & "' and fecha='" & fecha & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                a = Split(dt.Rows(0).Item("bloqueos"), "|", -1)
                bloqueo = a
                Funbloqueos = True
            End If
        End If
    End Function

    Private Sub DDconsultorios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDconsultorios.SelectedIndexChanged
        DDturnos.Items.Clear()
        cargaTurnos()

    End Sub

    Protected Sub BtnReAgendar_Click(sender As Object, e As EventArgs)
        If DDturnos.SelectedValue = "" Then
            LblMensajeAdvertencia.Text = "No existe algun turno disponible"
            PanelAdvertencia.Visible = True
            'PanelAdvertencia.Focus()
            Exit Sub
        End If
        Dim dt2 As New DataTable
        '*********   Si viene la solicitud de otra pagina, trae un tipo de modalidad  ******************
        If Session("modalidad") = "" Then
            '////////////////  valida si el paciente cuenta con citas futuras programadas \\\\\\\\\\\\\\\\\
            If validarCitasFuturas(dt2) Then
                If dt2.Rows.Count > 1 Then
                    LblMensajeDecision.Text = " El paciente cuenta con varias citas programadas, ¿desea ir al Buscador de Citas para ver estas?"
                    PanelDesicion.Visible = True
                    PanelDesicion.Focus()
                    HdPregunta.Value = "0"
                Else
                    LblMensajeDecision.Text = " El Paciente cuenta con una cita programada el día " + dt2.Rows(0).Item("Fecha") + ", ¿desea mover la cita?"
                    PanelDesicion.Visible = True
                    PanelDesicion.Focus()
                    HdPregunta.Value = "1"
                End If
            End If
        End If
        moverAgenda()
    End Sub

    Function validarCitasFuturas(ByRef dt As DataTable) As Boolean
        Dim strSql As String
        Dim clsDatos As New ClaseDatos
        strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
              "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
              "where codigoPaciente='" & Session("codigoPaciente") & "' and A.Codigoconsultorio='" & DDconsultorios.SelectedValue & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and A.codigoEtapa<>'4'  and fechaAgenda>'" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
              "order by fechaAgenda desc"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Return True
                Exit Function
            End If
        End If
        Return False
    End Function

    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case HdPregunta.Value
            Case "0"
                Response.Redirect("BuscarCitaPaciente.aspx")
            Case "1"
                Session("modalidad") = "mover"
                moverAgenda()
        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        Exit Sub
    End Sub

    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Protected Sub moverAgenda()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        '/////////////////  Valida si el paciente ya se encuentra agendado  \\\\\\\\\\\\\\\\\\\\\\\\\\
        strSQL = "SELECT NumeroTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                 " where codigoPaciente='" & LblCodigoPaciente.Text & "'  and codigoConsultorio='" & DDconsultorios.SelectedValue & "' and codigoEtapa<> '4' and fechaAgenda='" & TxtAgFecha2.Text & "' " & _
                   "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAdvertencia.Text = " Ya se encuentra agendado el paciente en el turno" + dt.Rows(0).Item("NumeroTurno").ToString
                PanelAdvertencia.Visible = True
                PanelAdvertencia.Focus()
            Else
                '///////////////////////  Valida si el turno se encuentra ocupado  \\\\\\\\\\\\\\\\\\\\\\\\\\
                strSQL = "SELECT count(*) as cont  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                         "where NumeroTurno='" & DDturnos.SelectedValue & "'  and codigoConsultorio='" & DDconsultorios.SelectedValue & "' and codigoEtapa<> '4' and fechaAgenda='" & TxtAgFecha2.Text & "' " & _
                          "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    If dt.Rows(0).Item("cont") > 0 Then
                        LblMensajeAdvertencia.Text = "  El turno ya esta ocupado"
                        PanelAdvertencia.Visible = True
                        PanelAdvertencia.Focus()
                        Exit Sub
                    Else
                        '////////////// Mover, Cancelar , Reagendar Citam \\\\\\\\\\\\\\\\
                        '********** MOVER CITA *****************
                        If Session("modalidad") = "mover" Then
                            strSQL = "update [" & clsDatos.BaseDatos & " ].[dbo].[AgAgenda] set fechaAgenda='" & TxtAgFecha2.Text & "', numeroTurno='" & DDturnos.SelectedValue & "',codigoUsuario='" & Session("codigoUsuario") & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                                        "where folioConsulta='" & Session("codigoConsulta") & "' and codigoPaciente='" & LblCodigoPaciente.Text & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                        Else
                            '**************************  CANCELAR CITA  *****************************
                            If Session("modalidad") = "cancelar" Then
                                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set codigoEtapa='4',codigoUsuario='" & Session("codigoUsuario") & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                                         "where folioConsulta='" & Session("codigoConsulta") & "' and codigoPaciente='" & LblCodigoPaciente.Text & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                            Else

                                '******************* REAGENDAR CITA **************************
                                strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] " & _
                                  " where codigoFoliador='1'  and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                                    Dim IncrementoFoliador As Integer
                                    IncrementoFoliador = dt.Rows(0).Item("consecutivo") + 1
                                    strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] set consecutivo='" & IncrementoFoliador & "' " & _
                                        "where codigoFoliador='1' and codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                                        "insert into [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda]  " & _
                                        "([FolioConsulta],[CodigoPaciente],[FechaAgenda],[Codigoconsultorio],[CodigoEtapa],[Codigocliente],[CodigoUsuarioAgenda],[NumeroTurno]" & _
                                         ",[HoraLLegada],[CodigoModalidad],[FechaFinalizacion],[CodigoUsuario],[CodigoEmpresa],[Status],[FechaActualizacion]) " & _
                                        "values ('C'+REPLICATE(0,9-LEN(" & IncrementoFoliador & "))+ CAST (" & IncrementoFoliador & " AS varchar)," & LblCodigoPaciente.Text & ",'" & TxtAgFecha2.Text & "'," & DDconsultorios.SelectedValue & ",'1'," & _
                                          "'1'," & Session("codigoUsuario") & ",'" & DDturnos.SelectedValue & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),'2',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & _
                                          "" & Session("codigoUsuario") & "," & Session("codigoEmpresa") & ",'1',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20))"
                                End If
                            End If
                        End If
                        clsDatos.cargaComando(strSQL)
                        If clsDatos.ejecutar() = 0 Then
                            Session("modalidad") = ""
                            If Session("retornarPagina") <> "" Then
                                Response.Redirect(Session("retornarPagina"))
                            Else
                                Response.Redirect("Agenda.aspx")
                            End If

                        Else
                            LblMensajeCritico.Text = clsDatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
End Class