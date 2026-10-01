Public Class datosEmpresa
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            bucarDatos()
        End If
    End Sub

    Private Sub bucarDatos()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT descripcion,razonSocial,rfc,calle,municipio,ciudad,estado,pais  " & _
            "FROM [" & clsDatos.BaseDatos & "].[dbo].[catEmpresas] " & _
            "where  codigoEmpresa='" & Session("codigoEmpresa") & "' and status=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                empresa.Value = dt.Rows(0).Item("descripcion")
                razonSocial.Value = dt.Rows(0).Item("razonSocial")
                rfc.Value = dt.Rows(0).Item("rfc")
                direccion.Value = dt.Rows(0).Item("calle")
                estado.Value = dt.Rows(0).Item("estado")
                pais.Value = dt.Rows(0).Item("pais")
                ciudad.Value = dt.Rows(0).Item("ciudad")

            Else
                LblMensajeAdvertencia.Text = "No se encontro la empresa, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
      

        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[catEmpresas] " & _
                "set descripcion='" & empresa.Value & "',razonSocial='" & razonSocial.Value & "',rfc='" & rfc.Value & "',calle='" & direccion.Value & "', " & _
                "ciudad='" & ciudad.Value & "',estado='" & estado.Value & "',pais='" & pais.Value & "' " & _
                "where codigoEmpresa=" & Session("codigoEmpresa") & ""
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se realizo el cambio correctamente"
            PanelAvisos.Visible = True
            bucarDatos()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Agenda.aspx")
    End Sub


End Class