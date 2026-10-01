Public Class BuscarPaciente
    Inherits System.Web.UI.Page
    Dim modalidad As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Session("modalidad") = "AG" Then
            BtnVerTexto.InnerText = " Agendar"
        Else
            BtnVerTexto.InnerText = " Ver"
        End If
        If IsPostBack Then
            Exit Sub
        End If
        If Request("__EVENTARGUMENT") <> Nothing AndAlso Request("__EVENTARGUMENT") = "move" Then
            If Session("modalidad") = "AG" Then
                Dim dt2 As New DataTable
                Dim clsDatos As New ClaseDatos
                If LstBoxPacientes.SelectedValue = "" Then
                    Exit Sub
                End If
                Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
                If validarCitasFuturas(dt2) Then
                    If dt2.Rows.Count > 1 Then
                        LblMostarDecision.Text = " El Paciente cuenta con varias citas programadas, ¿desea ir al Buscador de Citas para ver estas?"
                        PanelDesicion.Visible = True
                        PanelDesicion.Focus()
                        HdPregunta.Value = "0"
                        Exit Sub
                    Else
                        LblMostarDecision.Text = " El paciente cuenta con una cita programada el día " + dt2.Rows(0).Item("Fecha") + ", ¿desea mover la cita?"
                        PanelDesicion.Visible = True
                        PanelDesicion.Focus()
                        HdPregunta.Value = "1"
                        Hdfolio.Value = dt2.Rows(0).Item("Folio")
                        Exit Sub
                    End If
                End If
                moverAgregar()
            Else
                Response.Redirect("DatosPaciente.aspx?CodigoPaciente=" + LstBoxPacientes.SelectedValue)
            End If

        End If
        TxbPaterno.Focus()
        LstBoxPacientes.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(LstBoxPacientes, "move"))
    End Sub
    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
        'BotonBuscar.InnerText = "Buscando.."
        Dim strSQL As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos
        If TxbMaterno.Text = "" And TxbPaterno.Text = "" And TxbNombre.Text = "" Then
            Exit Sub
        End If
        strSQL = "SELECT CodigoPaciente,PApellido+' '+SApellido+' '+Nombres FROM [" & claseDatos.BaseDatos & "].[dbo].[Catpacientes] " & _
            "where PApellido like '%" & TxbPaterno.Text.Trim & "%' and nombres like '%" & TxbNombre.Text.Trim & "%' and SApellido like '%" & TxbMaterno.Text & "%'"
        resultadoBusqueda = 0
        resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
        If resultadoBusqueda = -1 Then
            LblMensajeCritico.Text = claseDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
            Exit Sub
        End If
        If resultadoBusqueda = 1 Then
            LblMostarDecision.Text = "No existe el paciente. ¿Desea agregarlo?"
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            HdPregunta.Value = "2"
            Exit Sub
        End If
        LstBoxPacientes.Focus()
        LstBoxPacientes.SelectedIndex = 0
    End Sub
    'Protected Sub LstBoxPacientes_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstBoxPacientes.SelectedIndexChanged
    '    Response.Redirect("DatosPaciente.aspx?CodigoPaciente=" + LstBoxPacientes.SelectedValue)
    'End Sub
    Protected Sub TxbMaterno_TextChanged(sender As Object, e As EventArgs) Handles TxbMaterno.TextChanged
    End Sub
    Function validarCitasFuturas(ByRef dt As DataTable) As Boolean
        Dim strSql As String
        Dim clsDatos As New ClaseDatos
        strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
              "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
              "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
              "where codigoPaciente='" & Session("codigoPaciente") & "' and A.CodigoConsultorio='" & Session("codigoConsultorio") & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and A.codigoEtapa NOT IN ('4','6')  and fechaAgenda>'" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
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
                moverAgregar()
            Case "2"
                Response.Redirect("DatosPaciente.aspx")
            Case "3"

        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        Exit Sub
    End Sub

    Protected Sub BtnVer_Click(sender As Object, e As EventArgs)
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        If Session("modalidad") = "AG" Then
            Dim dt2 As New DataTable
            Dim clsDatos As New ClaseDatos
            If LstBoxPacientes.SelectedValue = "" Then
                Exit Sub
            End If
            Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
            If validarCitasFuturas(dt2) Then
                If dt2.Rows.Count > 1 Then
                    LblMostarDecision.Text = " El Paciente cuenta con varias citas programadas, ¿desea ir al Buscador de Citas para ver estas?"
                    PanelDesicion.Visible = True
                    PanelDesicion.Focus()
                    HdPregunta.Value = "0"
                    Exit Sub
                Else
                    LblMostarDecision.Text = " El paciente cuenta con una cita programada el día " + dt2.Rows(0).Item("Fecha") + ", ¿desea mover la cita?"
                    PanelDesicion.Visible = True
                    PanelDesicion.Focus()
                    HdPregunta.Value = "1"
                    Hdfolio.Value = dt2.Rows(0).Item("Folio")
                    Exit Sub
                End If
            End If
            moverAgregar()
        Else
            Response.Redirect("DatosPaciente.aspx?CodigoPaciente=" + LstBoxPacientes.SelectedValue)
        End If

    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False

    End Sub

    Private Sub moverAgregar()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt, dt1, dt2 As New DataTable
        Dim codigoTipo, codigoCliente As Integer
        strSQL = "SELECT NumeroTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                         " where codigoPaciente='" & LstBoxPacientes.SelectedValue & "'  and codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEtapa<> '4' and fechaAgenda='" & Session("FechaAgenda") & "' " & _
                           "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAdvertencia.Text = "Ya se encuentra agendado el paciente en el turno" + dt.Rows(0).Item("NumeroTurno").ToString
                PanelAdvertencia.Visible = True
                PanelAdvertencia.Focus()
                Exit Sub
            Else
                strSQL = "SELECT count(*) as cont  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                         "where NumeroTurno='" & Session("numeroTurno") & "'  and codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEtapa<> '4' and fechaAgenda='" & Session("FechaAgenda") & "' " & _
                          "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    If dt.Rows(0).Item("cont") > 0 Then
                        LblMensajeAdvertencia.Text = "El turno ya esta ocupado"
                        PanelAdvertencia.Visible = True
                        PanelAdvertencia.Focus()
                        Exit Sub
                    Else
                        ' /////////////////////////////   AGENDAR, MOVER  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                        If Session("modalidad") = "mover" Then
                            '********** MOVER CITA *****************
                            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set fechaAgenda='" & Session("fechaAgenda") & "', numeroTurno='" & Session("numeroTurno") & "',codigoUsuario='" & Session("codigoUsuario") & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                                           "where folioConsulta='" & Hdfolio.Value.ToString & "' and codigoPaciente='" & LstBoxPacientes.SelectedValue & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1' "
                        Else
                            strSQL = "SELECT codigoCliente,codigoTipo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] " & _
                                     "where codigoPaciente=" & Session("codigoPaciente") & " and CodigoEmpresa=" & Session("codigoEmpresa") & " and status=1"
                            If clsDatos.cargatabla(strSQL, dt1) = 0 Then
                                If dt.Rows.Count > 0 Then
                                    codigoCliente = dt1.Rows(0).Item("codigoCliente")
                                    codigoTipo = dt1.Rows(0).Item("codigoTipo")
                                End If
                            End If
                            strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] " & _
                             " where codigoFoliador='1'  and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                                Dim IncrementoFoliador As Integer
                                IncrementoFoliador = dt.Rows(0).Item("consecutivo") + 1
                                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] set consecutivo='" & IncrementoFoliador & "' " & _
                                  "where codigoFoliador='1' and codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                                  "insert into [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda]  " & _
                                  "(FolioConsulta,CodigoPaciente,FechaAgenda,Codigoconsultorio,CodigoEtapa,Codigocliente,CodigoUsuarioAgenda,NumeroTurno,HoraLLegada " & _
                                   ",CodigoModalidad,FechaFinalizacion,CodigoUsuario,CodigoEmpresa,Status,FechaActualizacion,codigoTipo) " & _
                                  "values ('C'+REPLICATE(0,9-LEN(" & IncrementoFoliador & "))+ CAST (" & IncrementoFoliador & " AS varchar)," & LstBoxPacientes.SelectedValue & ",'" & Session("FechaAgenda") & "'," & Session("codigoConsultorio") & ",'1'," & _
                                    " " & codigoCliente & "," & Session("codigoUsuario") & ",'" & Session("numeroTurno") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),'2',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & _
                                    "" & Session("codigoUsuario") & "," & Session("codigoEmpresa") & ",'1',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & codigoTipo & ") "
                            End If
                        End If
                        clsDatos.cargaComando(strSQL)
                        If clsDatos.ejecutar() = 0 Then
                            Session("modalidad") = ""
                            Response.Redirect("Agenda.aspx")
                        Else
                            LblMensajeCritico.Text = "Ya esta ocupado el turno en la agenda :" + clsDatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    End If
                End If
            End If
        End If

    End Sub



End Class