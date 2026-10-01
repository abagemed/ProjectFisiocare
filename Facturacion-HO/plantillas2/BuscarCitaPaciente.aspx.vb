Public Class BuscarCitaPaciente
    Inherits System.Web.UI.Page
    Dim modalidad As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        modalidad = Request.QueryString("Modalidad")
        If Request("__EVENTARGUMENT") <> Nothing AndAlso Request("__EVENTARGUMENT") = "move" Then
            Dim clsDatos As New ClaseDatos
            Dim dt, dt2 As New DataTable
            Dim strSql, strSql2 As String
            If LstBoxPacientes.SelectedValue = "" Then
                Exit Sub
            End If
            strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                    "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and fechaAgenda<'" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
                    "order by fechaAgenda desc"
            strSql2 = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                    "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and A.codigoEtapa<>'4' and fechaAgenda>='" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
                    "order by fechaAgenda desc"

            If clsDatos.cargatabla(strSql, dt) = 0 And clsDatos.cargatabla(strSql2, dt2) = 0 Then
                LblNomPaciente.Text = LstBoxPacientes.SelectedItem.ToString
                'LblCodigoPaciente.Text = LstBoxPacientes.SelectedValue.ToString
                dtgCitasProgramadas.DataSource = dt2
                dtgCitas.DataSource = dt
                dtgCitasProgramadas.DataBind()
                dtgCitas.DataBind()
                dtgCitas.Visible = True
            End If
        End If
        TxbPaterno.Focus()
        LstBoxPacientes.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(LstBoxPacientes, "move"))
    End Sub

    Protected Sub BtnVer_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt, dt2 As New DataTable
        Dim strSql, strSql2 As String
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and fechaAgenda<'" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
                "order by fechaAgenda desc"
        strSql2 = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and A.codigoEtapa<>'4' and fechaAgenda>='" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
                "order by fechaAgenda desc"

        If clsDatos.cargatabla(strSql, dt) = 0 And clsDatos.cargatabla(strSql2, dt2) = 0 Then
            LblNomPaciente.Text = LstBoxPacientes.SelectedItem.ToString
            'LblCodigoPaciente.Text = LstBoxPacientes.SelectedValue.ToString
            dtgCitasProgramadas.DataSource = dt2
            dtgCitas.DataSource = dt
            dtgCitasProgramadas.DataBind()
            dtgCitas.DataBind()
            dtgCitas.Visible = True
            dtgCitasProgramadas.Focus()
        End If
    End Sub

    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
        Dim strSQL, strSQL1 As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos
        If Trim(TxbPaterno.Text) <> "" Then
            If Trim(TxbNombre.Text) <> "" Then
                If Trim(TxbMaterno.Text) <> "" Then
                    strSQL1 = " and nombres like '%" & TxbNombre.Text & "%' and SApellido='" & TxbMaterno.Text & "'"
                Else
                    strSQL1 = " and nombres like '%" & TxbNombre.Text & "%'"
                End If
            Else
                If Trim(TxbMaterno.Text) <> "" Then
                    strSQL1 = " and SApellido='" & TxbMaterno.Text & "'"
                Else
                    strSQL1 = ""
                End If
            End If
            strSQL = "SELECT CodigoPaciente,PApellido+' '+SApellido+' '+Nombres FROM [" & claseDatos.BaseDatos & "].[dbo].[Catpacientes]  where PApellido='" & TxbPaterno.Text & "'" & strSQL1
            resultadoBusqueda = 0
            resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
            If resultadoBusqueda = -1 Then
                LblMensajeCritico.Text = funcion.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
                Exit Sub
            End If
            If resultadoBusqueda = 1 Then
                LblMostarDecision.Text = "No existe el paciente. ¿Desea agregarlo?"
                PanelDesicion.Visible = True
                PanelDesicion.Focus()
                Hdpregunta.Value = "0"
                Exit Sub
            End If
            LstBoxPacientes.Focus()
            LstBoxPacientes.SelectedIndex = 0
        Else
            LblMensajeAdvertencia.Text = "El apellido paterno es un dato obligatorio"
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
        End If
    End Sub
   
    Protected Sub TxbMaterno_TextChanged(sender As Object, e As EventArgs) Handles TxbMaterno.TextChanged
    End Sub

    Protected Sub dtgCitas_SelectedIndexChanged(sender As Object, e As EventArgs) Handles dtgCitas.SelectedIndexChanged

    End Sub

    Private Sub dtgCitasProgramadas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dtgCitasProgramadas.RowCommand
        If e.CommandName = "Mover" Then
            Dim index As Integer
            index = CInt(e.CommandArgument)
            Session("codigoConsulta") = dtgCitasProgramadas.Rows(index).Cells(2).Text
            Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
            Session("nombrePaciente") = LstBoxPacientes.SelectedItem.ToString
            If dtgCitasProgramadas.Rows(index).Cells(7).Text = "FINALIZADO" Then
                LblAdvertencia.Text = "  La cita esta finalizada"
                PanelAdvertencia2.Visible = True
                PanelAdvertencia2.Focus()
                Exit Sub
            End If
            Session("modalidad") = "mover"
            Response.Redirect("ReAgendarPaciente.aspx")
        End If
        If e.CommandName = "Cancelar" Then
            LblDesicion.Text = " ¿Esta seguro(a) que desea cancelar esta cita?"
            PanelDesicion2.Visible = True
            PanelDesicion2.Focus()
            Hdpregunta.Value = "1"
            Hdindex.Value = CInt(e.CommandArgument)
        End If
    End Sub

    Private Sub cancelarCita()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set codigoEtapa='4',codigoUsuario='" & Session("codigoUsuario") & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                  "where folioConsulta='" & Session("codigoConsulta") & "' and codigoPaciente='" & Session("codigoPaciente") & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            Session("modalidad") = ""
        Else
            Lblcritico.Text = clsDatos.MensajeError
            PanelCritico2.Visible = True
            PanelCritico2.Focus()
        End If
    End Sub
    Private Sub ActuakizarGRIDCitasProgramadas()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.numeroTurno as Turno,A.FechaAgenda as Fecha,E.descripcion as Estado,(U.nombre+' '+U.PrimerApellido) as Agendo " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                    "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and A.codigoEtapa<>'4' and fechaAgenda>='" & Format(Date.Now, "dd/MM/yyyy") & "' " & _
                    "order by fechaAgenda desc"

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            dtgCitasProgramadas.DataSource = dt
            dtgCitasProgramadas.DataBind()
        End If
    End Sub


    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case Hdpregunta.Value
            Case "0"
                Response.Redirect("DatosPaciente.aspx")

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
        PanelAdvertencia2.Visible = False
        PanelAviso2.Visible = False
        PanelCritico2.Visible = False
        PanelDesicion2.Visible = False


    End Sub

   

    Protected Sub BtnSi2_Click(sender As Object, e As EventArgs) Handles BtnSi2.Click
        Select Case Hdpregunta.Value
            Case "1"
                Session("codigoConsulta") = dtgCitasProgramadas.Rows(Hdindex.Value).Cells(2).Text
                Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
                Session("nombrePaciente") = LstBoxPacientes.SelectedItem.ToString
                If dtgCitasProgramadas.Rows(Hdindex.Value).Cells(7).Text = "FINALIZADO" Then
                    LblAdvertencia.Text = "  La cita esta finalizada"
                    PanelAdvertencia2.Visible = True
                    PanelAdvertencia2.Focus()
                    Exit Sub
                End If
                cancelarCita()
                ActuakizarGRIDCitasProgramadas()
        End Select
    End Sub

    Protected Sub BtnNo2_Click(sender As Object, e As EventArgs) Handles BtnNo2.Click
        Exit Sub
    End Sub

    
End Class