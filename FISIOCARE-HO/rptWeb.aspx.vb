Imports System.Data
Imports System.Data.SqlClient

Partial Class rptWeb
    Inherits System.Web.UI.Page
    Dim Funciones As New miclases
    Dim Cn As SqlConnection = Funciones.conecta
    Dim adap As SqlDataAdapter
    Dim cmd As SqlCommand
    Dim SQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim hoy As DateTime = DateTime.Now()
        If Not Page.IsPostBack Then
            lblfechacita.Text = Context.Items("fecha").ToString.Trim
            txtInicio.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtFinal.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            cboReporte = Funciones.llenacombos(cboReporte, "SELECT idConsulta, Descripcion FROM ReportesConsultas WHERE estado = 'AC' ORDER BY Descripcion")
        End If
    End Sub

    Protected Sub cboReporte_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboReporte.SelectedIndexChanged
        Dim tConsulta As New DataTable
        Dim vCondicion, vFiltro As String
        vFiltro = ""
        vCondicion = ""
        SQL = ""

        SQL = "SELECT * FROM ReportesConsultas WHERE idconsulta=" & cboReporte.SelectedValue
        cmd = New SqlCommand(SQL, Cn)
        adap = New SqlDataAdapter(cmd)
        Cn.Open()
        adap.Fill(tConsulta)
        Cn.Close()
        vFiltro = tConsulta.Rows(0)("filtro1").ToString
        If vFiltro <> "" Then
            cboFiltro1 = Funciones.llenacombos(cboFiltro1, vFiltro)
            divFiltro1.Visible = True
        Else
            divFiltro1.Visible = False
        End If
        vFiltro = tConsulta.Rows(0)("filtro2").ToString
        If vFiltro <> "" Then
            cboFiltro2 = Funciones.llenacombos(cboFiltro2, vFiltro)
            divFiltro2.Visible = True
        Else
            divFiltro2.Visible = False
        End If
        vFiltro = tConsulta.Rows(0)("fecha1").ToString
        If vFiltro <> "" Then
            divFecha.Visible = True
        Else
            divFecha.Visible = False
        End If
        SQL = ""
    End Sub

    Protected Sub cmGenerar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmGenerar.Click
        Dim tConsulta As New DataTable
        Dim vCondicion, vFiltro1, vCondicion2 As String
        vFiltro1 = ""
        vCondicion = ""
        vCondicion2 = ""
        SQL = ""

        SQL = "SELECT * FROM ReportesConsultas WHERE idconsulta=" & cboReporte.SelectedValue
        cmd = New SqlCommand(SQL, Cn)
        adap = New SqlDataAdapter(cmd)
        Cn.Open()
        adap.Fill(tConsulta)
        Cn.Close()

        If divFiltro1.Visible Then
            If cboFiltro1.SelectedValue <> 0 Then
                vCondicion = " " & tConsulta.Rows(0)("campofiltro1").ToString.Trim & "=" & cboFiltro1.SelectedValue
            Else
                vCondicion = ""
            End If
        End If
        If divFiltro2.Visible Then
            If cboFiltro1.SelectedValue <> 0 Then
                vCondicion = vCondicion & " AND " & tConsulta.Rows(0)("campofiltro2").ToString.Trim & "=" & cboFiltro2.SelectedValue
            End If
        End If
        If divFecha.Visible Then
            If Not divFiltro1.Visible Or cboFiltro1.SelectedValue = "0" Then
                '+++vCondicion = " WHERE " & tConsulta.Rows(0)("fecha1").ToString.Trim & ">='" & txtInicio.Text & "' "
                vCondicion = " " & tConsulta.Rows(0)("fecha1").ToString.Trim & ">='" & txtInicio.Text & "' "
                vCondicion = vCondicion & " AND " & tConsulta.Rows(0)("fecha1").ToString.Trim & "<='" & txtFinal.Text & "' "
            Else
                vCondicion = vCondicion & " AND " & tConsulta.Rows(0)("fecha1").ToString.Trim & ">='" & txtInicio.Text & "' "
                vCondicion = vCondicion & " AND " & tConsulta.Rows(0)("fecha1").ToString.Trim & "<='" & txtFinal.Text & "' "
            End If
            'si tiene la fecha2
            If tConsulta.Rows(0)("fecha2").ToString.Trim <> "" Then
                vCondicion2 = " (" & tConsulta.Rows(0)("fecha2").ToString.Trim & ">='" & txtInicio.Text & "' "
                vCondicion2 = vCondicion2 & " AND " & tConsulta.Rows(0)("fecha2").ToString.Trim & "<='" & txtFinal.Text & "') "
            End If
        End If
        SQL = ""
        SQL = tConsulta.Rows(0)("sqlConsulta").ToString
        SQL = SQL.Replace("?", vCondicion)
        If vCondicion2 <> "" Then
            SQL = SQL.Replace("¿", vCondicion2)
        End If

        'MsgBox(SQL)

        tConsulta = New DataTable
        cmd = New SqlCommand(SQL, Cn)
        adap = New SqlDataAdapter(cmd)
        Cn.Open()
        adap.Fill(tConsulta)
        Cn.Close()
        gvRpt.Caption = cboReporte.SelectedItem.Text
        gvRpt.DataSource = tConsulta
        gvRpt.DataBind()
        divRpt.Visible = True
        If tConsulta.Rows.Count > 0 Then
            cmdImprimir.Visible = True
            cmdexcel.Visible = True
        Else
            cmdImprimir.Visible = False
            cmdexcel.Visible = False
        End If
    End Sub

    Protected Sub cmdexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        ExportarAExcel()
    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form = New HtmlForm
        gvRpt.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gvRpt)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=" & cboReporte.SelectedItem.Text.Trim & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
End Class
