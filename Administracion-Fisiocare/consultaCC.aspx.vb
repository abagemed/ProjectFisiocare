Imports System.Data
Imports System.Data.SqlClient

Partial Class consultaCC

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
            'Dim sql2 As String
            'Dim sql3 As String
            'Dim sql4 As String

            sql = " select   d.CIDCLIENTEPROVEEDOR as 'IdCliente',d.CRAZONSOCIAL as 'Cliente',round(sum(c.cunidades),0) as 'TotalPzas',"
            sql += vbNewLine & "concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'TotalImp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by d.CRAZONSOCIAL,d.CIDCLIENTEPROVEEDOR"
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select '','',round(sum(c.cunidades),0) as 'Total Pzas',"
            sql += vbNewLine & "concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)


        Else

        End If

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = gridvadmProductos.Items.Count - 1

        gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridvadmProductos.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        gridvadmProductos.Items(UltimaFilaGA).Font.Bold = True
        gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10

        'Dim UltimaFilaGC As Integer
        'UltimaFilaGC = gridvadmProductoscma.Items.Count - 1

        'gridvadmProductoscma.Items(UltimaFilaGC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductoscma.Items(UltimaFilaGC).ForeColor = Drawing.Color.Black
        'gridvadmProductoscma.Items(UltimaFilaGC).Font.Bold = True
        'gridvadmProductoscma.Items(UltimaFilaGC).Font.Size = 10


        'Dim UltimaFilaGP As Integer
        'UltimaFilaGP = gridvadmProductospensiones.Items.Count - 1

        'gridvadmProductospensiones.Items(UltimaFilaGP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductospensiones.Items(UltimaFilaGP).ForeColor = Drawing.Color.Black
        'gridvadmProductospensiones.Items(UltimaFilaGP).Font.Bold = True
        'gridvadmProductospensiones.Items(UltimaFilaGP).Font.Size = 10


        'Dim UltimaFilaGT As Integer
        'UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

        'gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductostotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        'gridvadmProductostotales.Items(UltimaFilaGT).Font.Bold = True
        'gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

        cmdexcelcm.Visible = True
        gridvadmProductos.Visible = True
        lblIMPLANTES.Visible = True
        'lblcma.Visible = True
        'lblpensiones.Visible = True
        'lbltotales.Visible = True
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
        form.Controls.Add(lblIMPLANTES)
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

    Protected Sub gridvadmProductos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridvadmProductos.ItemCommand
        Dim id As String = gridvadmProductos.DataKeys.Item(e.Item.ItemIndex).ToString
        idcliente.Value = id


        Select Case e.CommandName
            Case "editar"

                If cmbSucursal.SelectedIndex = "0" Then

                    If id = "0" Then
                        Dim sql As String

                        sql = " select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
                        sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total Imp'"
                        sql += vbNewLine & "from admProductos p"
                        sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
                        sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
                        sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
                        sql += vbNewLine & "where c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
                        sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
                        sql += vbNewLine & " union all"
                        sql += vbNewLine & "select 'TOTALES',"
                        sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp'"
                        sql += vbNewLine & "from admProductos p"
                        sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
                        sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
                        sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
                        sql += vbNewLine & "where c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
                        gridresultado = funciones.LLenaGridC(sql, gridresultado, hfSucursal.Value)
                    Else
                        Dim sql As String

                        sql = " select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
                        sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total Imp'"
                        sql += vbNewLine & "from admProductos p"
                        sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
                        sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
                        sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
                        sql += vbNewLine & "where d.CIDCLIENTEPROVEEDOR='" & id & "' and c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
                        sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
                        sql += vbNewLine & " union all"
                        sql += vbNewLine & "select 'TOTALES',"
                        sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp'"
                        sql += vbNewLine & "from admProductos p"
                        sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
                        sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
                        sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
                        sql += vbNewLine & "where d.CIDCLIENTEPROVEEDOR='" & id & "' and  c.CFECHA>='" + txtfecha1.Text + " 00:00:00' and c.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
                        gridresultado = funciones.LLenaGridC(sql, gridresultado, hfSucursal.Value)
                    End If





                Else

                End If

                Dim UltimaFilaGA As Integer
                UltimaFilaGA = gridresultado.Items.Count - 1

                gridresultado.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gridresultado.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
                gridresultado.Items(UltimaFilaGA).Font.Bold = True
                gridresultado.Items(UltimaFilaGA).Font.Size = 10


                cmdexcel.Visible = True
                gridresultado.Visible = True

                Dim conexion As SqlConnection = funciones.conecta(hfSucursal.Value)
                Dim comando As SqlCommand = conexion.CreateCommand
                comando.CommandText = "select CRAZONSOCIAL as 'Cliente'" & _
                           "from admClientes " & _
                           "where CIDCLIENTEPROVEEDOR='" + id + "'"
                conexion.Open()
                Dim sqlread As SqlDataReader = comando.ExecuteReader
                If sqlread.Read Then
                    lbldescripcion.Text = sqlread.GetValue(0).ToString.Trim
                End If
                sqlread.Close()
                conexion.Close()

                lbldescripcion.Visible = True
                lbltitulo.Visible = True

                ModalPopupExtender2.Show()
        End Select

    End Sub
    Protected Sub cmdexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        ExportarAExcelR()
    End Sub
    Protected Sub ExportarAExcelR()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gridresultado.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(lbldescripcion)
        form.Controls.Add(gridresultado)
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
End Class
