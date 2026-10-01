Public Class AltaEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Dim strSQL As String
            Dim a() As String
            Dim dt As New DataTable
            Dim funciones As New FuncionesGenerales
            Dim clsdatos As New ClaseDatos


            LlenarComboPais()
            LlenarComboEstado()
            LlenarComboMunicipio()
            LlenarComboCiudad()

            DDPais.SelectedValue = 223 'Mexicana
            'DDEstado.SelectedValue = 31 'Yucatan
            ' DDMunicipio.SelectedValue = 50 'Merida 50
            'DDCiudad.SelectedValue = ""
        End If
    End Sub
    Protected Sub LlenarComboPais()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "Select CodigoNacionalidad,Nacionalidad  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatNacionalidades] " &
                 "where status='1'"
        If funciones.llenadropdown(strSQL, DDPais) = -1 Then
            MsgBox(funciones.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub
    Protected Sub LlenarComboEstado()
        DDEstado.SelectedValue = 31 'Yucatan
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoEntidad,nombreEntidad FROM [" & clsDatos.BaseDatos & "].[dbo].[CatEntidades] " &
                 " where status='1'"
        If funciones.llenadropdown(strSQL, DDEstado) = -1 Then
            MsgBox(funciones.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub
    Protected Sub LlenarComboMunicipio()

        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoMunicipio, nombreMunicipio FROM [" & clsDatos.BaseDatos & "].[dbo].[CatMunicipios] " &
            "where codigoEntidad='" & DDEstado.SelectedValue & "' and status='1'"
        'strSQL = "Select codigoMunicipio, nombreMunicipio FROM [" & clsDatos.BaseDatos & "].[dbo].[CatMunicipios]" &
        '   "where codigoEntidad ='31' and status='1'"
        If funciones.llenadropdown(strSQL, DDMunicipio) = -1 Then
            MsgBox(funciones.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub
    Protected Sub LlenarComboCiudad()
        'DDMunicipio.SelectedValue = 50 'Merida
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "Select codigoLocalidad,nombreLocalidad FROM [" & clsDatos.BaseDatos & "].[dbo].[CatLocalidades] " &
        "where codigoEntidad='" & DDEstado.SelectedValue & "' and codigoMunicipio='" & DDMunicipio.SelectedValue & "' and status='1'"
        'strSQL = "Select codigoLocalidad, nombreLocalidad FROM [" & clsDatos.BaseDatos & "].[dbo].[CatLocalidades]" &
        '   "where codigoEntidad ='31' and codigoMunicipio='50' and status='1'"
        If funciones.llenadropdown(strSQL, DDCiudad) = -1 Then
            MsgBox(funciones.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub
    Private Sub DDEstado_CambioDeEstado(sender As Object, e As EventArgs) Handles DDEstado.SelectedIndexChanged

        LlenarComboMunicipio()
        LlenarComboCiudad()
    End Sub

    Private Sub DDMunicipio_CambioDeMunicipio(sender As Object, e As EventArgs) Handles DDMunicipio.SelectedIndexChanged

        LlenarComboCiudad()
    End Sub

End Class