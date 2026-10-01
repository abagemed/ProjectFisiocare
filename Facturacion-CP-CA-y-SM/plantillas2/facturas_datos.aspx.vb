Public Class facturas_datos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        cargarpaciente()
        LlenarComboRazonSocial()
        box_razon.Visible = False
        input_razon_social.Visible = False
    End Sub
    Sub cargarpaciente()

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = "SELECT nombres, papellido, sapellido, calle as direccion, codigoPostal, email, municipio FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] where CodigoPaciente =" + Session("codigoPaciente")

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                'Session("codigoPaciente") = dt.Rows(0).Item("CodigoPaciente")
                'nombre.Value = dt.Rows(0).Item("nombres").trim & " " & dt.Rows(0).Item("papellido") & " " & dt.Rows(0).Item("sapellido")
                direccion.Value = dt.Rows(0).Item("direccion")
                cp.Value = dt.Rows(0).Item("codigoPostal")
                municipio.Value = dt.Rows(0).Item("municipio")
                correo.Value = dt.Rows(0).Item("email")
            End If
        End If
    End Sub

    Protected Sub LlenarComboRazonSocial()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT CodigoPaciente, nombres as razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] where CodigoPaciente =" + Session("codigoPaciente")
        If funciones.llenadropdown(strSQL, select_razon_social) = -1 Then
            'Error
            box_razon.Visible = True
            input_razon_social.Visible = True
        End If
    End Sub

    Sub AltaRazonSocial()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] (CodigoPaciente, razonSocial, rfc, email, direccion, cp, pais, estado, ciudad )VALUES('" & Session("codigoPaciente") & "', '" & razon_social_alta.Value & "', '" & rfc_alta.Value & "', '" & correo_alta.Value & "', '" & direccion_alta.Value & "', '" & cp_alta.Value & "', '" & pais_alta.Value & "', '" & estado_alta.Value & "', '" & ciudad_alta.Value & "'  )"
        clsdatos.cargaComando(strsql)
        If clsdatos.ejecutar() = 0 Then
            MsgBox("Alta")
        Else
            MsgBox("Error")
        End If
    End Sub

End Class