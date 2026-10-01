Public Class PruebaReporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarConsultorios()

    End Sub

    Private Sub cargarConsultorios()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT CF.CodigoConsultorio, C.descripcionConsultorio " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] as CF " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=CF.CodigoConsultorio " & _
                "where CF.CodigoUsuarioEnlazado='" & Session("codigoUsuario") & "' and CF.CodigoEmpresa='" & Session("codigoEmpresa") & "' "
        If funciones.llenadropdown(strSQL, DdMedico) = 0 Then
            If DdMedico.Items.Count > 1 Then
                DdMedico.Visible = True
                DdMedico.SelectedValue = Session("codigoConsultorio")
            End If
        End If
    End Sub



    Private Sub DdMedico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdMedico.SelectedIndexChanged
        Dim clsDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT FolioConsulta,CodigoPaciente,numeroTurno FROM [Hospitales New].[dbo].[AgAgenda] " & _
                "where CodigoConsultorio='" & DdMedico.SelectedIndex & "' and fechaAgenda='11/01/2017'"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            GdReporte.DataSource = dt
            GdReporte.DataBind()
            GdReporte.Visible = True
        End If
    End Sub

    Private Sub DdMedico_TextChanged(sender As Object, e As EventArgs) Handles DdMedico.TextChanged
        Dim clsDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT FolioConsulta,CodigoPaciente,numeroTurno FROM [Hospitales New].[dbo].[AgAgenda] " & _
                "where CodigoConsultorio='" & DdMedico.SelectedIndex & "' and fechaAgenda='11/01/2017'"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            GdReporte.DataSource = dt
            GdReporte.DataBind()

        End If
    End Sub

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim clsDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT FolioConsulta,CodigoPaciente,numeroTurno FROM [Hospitales New].[dbo].[AgAgenda] " & _
                "where CodigoConsultorio='" & DdMedico.SelectedValue & "' and fechaAgenda>='" & FechaDE.SelectedDate & "'" & _
                 "and fechaAgenda<='" & FechaHasta.Text & "'"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            GdReporte.DataSource = dt
            GdReporte.DataBind()

        End If
    End Sub
End Class