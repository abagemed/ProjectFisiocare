Public Class BuscarExpediente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Request("__EVENTARGUMENT") <> Nothing AndAlso Request("__EVENTARGUMENT") = "move" Then
            Dim clsDatos As New ClaseDatos
            Dim dt As New DataTable
            Dim strSql As String
            If LstBoxPacientes.SelectedValue = "" Then
                Exit Sub
            End If
            strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.FechaAgenda as Fecha,E.descripcion as Estado " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuario " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                    "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and fechaAgenda < ='" & Format(Date.Now, "dd/MM/yyyy") & "' and A.codigoEtapa='6' " & _
                    "order by fechaAgenda desc"
            If clsDatos.cargatabla(strSql, dt) = 0 Then
                LblNomPaciente.Text = LstBoxPacientes.SelectedItem.ToString
                'LblCodigoPaciente.Text = LstBoxPacientes.SelectedValue.ToString
                dtgexpedientes.DataSource = dt
                dtgexpedientes.DataBind()
                dtgexpedientes.Visible = True
            End If
        End If
        TxbPaterno.Focus()
        LstBoxPacientes.Attributes.Add("ondblclick", ClientScript.GetPostBackEventReference(LstBoxPacientes, "move"))
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
                HdPregunta.Value = "0"
                Exit Sub
            End If
            LstBoxPacientes.Focus()
            LstBoxPacientes.SelectedIndex = 0
        Else
            LblMensajeAdvertencia.Text = "Apellido Paterno es un dato obligatorio"
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
        End If
    End Sub

    Private Sub dtgCitasProgramadas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles dtgexpedientes.RowCommand
        If e.CommandName = "Ver" Then
            Dim index As Integer
            index = CInt(e.CommandArgument)
            Session("FolioConsulta") = dtgexpedientes.Rows(index).Cells(2).Text
            Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
            Session("FechaConsulta") = Format(dtgexpedientes.Rows(index).Cells(6).Text.ToString, "dd/MM/yyyy")
            Response.Redirect("consultaEx.aspx")
        End If
        If e.CommandName = "Imprimir" Then
            'Dim index As Integer
            'index = CInt(e.CommandArgument)
            'Session("codigoConsulta") = dtgCitasProgramadas.Rows(index).Cells(2).Text
            'Session("codigoPaciente") = LstBoxPacientes.SelectedValue.ToString
            'Session("nombrePaciente") = LstBoxPacientes.SelectedItem.ToString
            'Session("modalidad") = "mover"
            'Response.Redirect("ReAgendarPaciente.aspx")            

        End If
    End Sub

    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Protected Sub BtnVer_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        strSql = "SELECT A.FolioConsulta as Folio,A.codigoPaciente,C.descripcionConsultorio as Consultorio,A.FechaAgenda as Fecha,E.descripcion as Estado " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on E.codigoEtapa=A.CodigoEtapa and E.codigoEmpresa =A.codigoEmpresa " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=A.codigoUsuario " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and E.codigoEmpresa =A.codigoEmpresa " & _
                "where codigoPaciente='" & LstBoxPacientes.SelectedItem.Value & "' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' and A.status='1' and fechaAgenda < ='" & Format(Date.Now, "dd/MM/yyyy") & "' and A.codigoEtapa='6' " & _
                "order by fechaAgenda desc"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            LblNomPaciente.Text = LstBoxPacientes.SelectedItem.ToString
            'LblCodigoPaciente.Text = LstBoxPacientes.SelectedValue.ToString
            dtgexpedientes.DataSource = dt
            dtgexpedientes.DataBind()
            dtgexpedientes.Visible = True
            dtgexpedientes.Focus()
        End If
    End Sub

    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case HdPregunta.Value
            Case "0"
                Response.Redirect("DatosPaciente.aspx")
        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        Exit Sub
    End Sub
End Class