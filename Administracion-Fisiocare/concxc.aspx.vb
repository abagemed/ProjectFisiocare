Imports System.Data
Imports System.Data.SqlClient

Partial Class concxc
    Inherits System.Web.UI.Page
    Dim funciones As New miclases

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbclientes.SelectedValue
        Dim fechaInicio As String = txtfecha1.Text.Trim + " 00:00:00"
        Dim fechaFin As String = txtfecha2.Text.Trim + " 23:59:59"
        Dim sqlSP As String = "EXEC [FISIOCARE_repClientesCxC] '" + fechaInicio + "', '" + fechaFin + "' "

        If cmbclientes.SelectedIndex = 0 Then

        Else 'eligió cliente
            Dim cliente As String = ", '" + cmbclientes.SelectedItem.Text + "'"
            sqlSP += cliente
        End If

        gDatos = funciones.LLenaGrid(sqlSP, gDatos, hfSucursal.Value)

        cmdexcel.Visible = True
        gtotales.Visible = False
        gDatos.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
        End If
    End Sub

    Protected Sub DropDownList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        Panel1.Visible = False
        Dim columnaQuery As String
        Dim query As String

        If cmbSucursal.SelectedValue <> "0" Then
            If cmbSucursal.SelectedItem.Text = "Gym" Then
                columnaQuery = "elnombre"
            Else
                columnaQuery = "razonSocial"
            End If
            query = "SELECT ROW_NUMBER() over (order by " + columnaQuery + ") as idcliente, " + columnaQuery + " FROM clientes WHERE " + columnaQuery + "  IS NOT NULL AND " + columnaQuery + " <> '' GROUP BY " + columnaQuery + " ORDER BY " + columnaQuery

            cmbclientes = funciones.llenacombos(cmbclientes, query, cmbSucursal.SelectedValue)
        Else

            cmbclientes.Items.Clear()
        End If

        gDatos.Visible = False
        gtotales.Visible = False
    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm

        gDatos.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gDatos)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=General" & cmbclientes.SelectedValue.Trim & "" & cmbSucursal.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub

    Protected Sub cmdexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        ExportarAExcel()
    End Sub

    Protected Sub ExportarAExcelt()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gtotales.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gtotales)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=Totales" & cmbclientes.SelectedValue.Trim & "" & cmbSucursal.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
