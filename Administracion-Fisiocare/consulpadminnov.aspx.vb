Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpadminnov

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
           

            sql = " SELECT"
            sql += vbNewLine & "CIDPRODUCTO AS IDPRODUCTO,"
            sql += vbNewLine & "CCODIGOPRODUCTO AS CODIGO,"
            sql += vbNewLine & "CNOMBREPRODUCTO AS DESCRIPCION,"
            sql += vbNewLine & "isnull( (SELECT SUM(cunidades) as Entradas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='24/09/2019 12:00:00' "
            sql += vbNewLine & "and cAfectaExistencia=1"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=P.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='24/09/2019 12:00:00'"
            sql += vbNewLine & "and cAfectaExistencia=2"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=P.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "),0) AS EXISTENCIAS"
            sql += vbNewLine & "FROM"
            sql += vbNewLine & "admProductos P"
            sql += vbNewLine & " left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & "WHERE"
            sql += vbNewLine & " NOT EXISTS(SELECT"
            sql += vbNewLine & " M.CIDPRODUCTO"
            sql += vbNewLine & "FROM"
            sql += vbNewLine & "admMovimientos M"
            sql += vbNewLine & "WHERE M.CFECHA>='" + txtfecha1.Text + " 00:00:00' and M.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and M.CIDPRODUCTO = P.CIDPRODUCTO) AND P. CTIPOPRODUCTO=1 AND P.CSTATUSPRODUCTO=1"
            sql += vbNewLine & "order by EXISTENCIAS"
            gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)

            'PARA CLASIFICACIONES
            'sql += vbNewLine & "cs1.CVALORCLASIFICACION AS CLASIFICACION,"
        Else

        End If
        cmdexcelcm.Visible = True
        gridvadmProductos.Visible = True

    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)

        End If
    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gridvadmProductos.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        'form.Controls.Add(lbltotales)
        'form.Controls.Add(gridvadmProductostotales)
        form.Controls.Add(lblaltabrisa)
        form.Controls.Add(gridvadmProductos)
        'form.Controls.Add(lblcma)
        'form.Controls.Add(gridvadmProductoscma)
        'form.Controls.Add(lblpensiones)
        'form.Controls.Add(gridvadmProductospensiones)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=General" & cmbSucursal.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub cmdexcelcm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcelcm.Click
        ExportarAExcel()
    End Sub
End Class
