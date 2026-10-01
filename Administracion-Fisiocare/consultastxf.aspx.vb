Imports System.Data
Imports System.Data.SqlClient

Partial Class consultastxf

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim clase As New miclases

    Sub llenardatos()
        Dim sql As String

        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbdoctorc.SelectedValue

        If cmbdoctorc.SelectedValue = 0 Then
            sql = "SELECT idcita,convert(varchar(10),abonos.fecha,103) as fecha, clientes.elnombre,catServicios.descripcion," & _
            "  catServicios.descripcion, " & _
            "abonos.abono, abonos.importe-monto as importe, (abonos.importe-monto - abonos.abono) as Adeuda,abonos.idabono,abonos.fecha as fecha2,abonos.idcliente FROM  " & _
            "abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where facturado='false' and importe<>0 and datepart(yyyy,abonos.fecha)>2015 order by fecha2 asc"
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)
            Dim conexion As SqlConnection = clase.conecta(hfSucursal.Value)
            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(sql, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            tabla.TableName = "mitabla"
            gDatosCM.DataMember = "mitabla"
            gDatosCM.DataSource = tabla.DefaultView
            gDatosCM.DataBind()

            If gDatosCM.Items.Count > 0 Then
                Dim filaTotal As DataRow = tabla.NewRow
                Dim nFilas As Integer = gDatosCM.Items.Count
                Dim nColumnas As Integer = gDatosCM.Items(0).Cells.Count
                Dim cFilas As Integer = 0
                Dim cColumnas As Integer = 5
                Do While cColumnas < nColumnas
                    Dim sumaNvoPacientes As Integer = 0
                    Do While cFilas < nFilas
                        sumaNvoPacientes = sumaNvoPacientes + gDatosCM.Items(cFilas).Cells(cColumnas).Text
                        cFilas = cFilas + 1
                    Loop
                    filaTotal(cColumnas) = sumaNvoPacientes
                    cColumnas = cColumnas + 1
                    cFilas = 0
                Loop

                Dim cuentaF As Integer = gDatosCM.Items.Count
                Dim cuentaC As Integer = gDatosCM.Items(0).Cells.Count
                Dim columna As Integer = 0
                Dim fila As Integer = 0

                Dim cuentaOtros As Integer = 0
                Do While fila < cuentaF
                    Dim nomColum As String = gDatosCM.Items(fila).Cells(0).Text.Trim
                    If nomColum = "0" Then
                        columna = 0
                        Dim filaAux As DataRow = tabla.NewRow
                        Do While columna < cuentaC
                            filaAux(columna) = gDatosCM.Items(fila).Cells(columna).Text.Trim
                            columna = columna + 1
                        Loop
                        tabla.Rows(fila).Delete()
                        tabla.Rows.Add(filaAux)
                        cuentaOtros = cuentaOtros + 1
                    End If
                    fila = fila + 1
                Loop

                tabla.Rows.Add(filaTotal)
                gDatosCM.DataMember = "mitabla"
                gDatosCM.DataSource = tabla.DefaultView
                gDatosCM.DataBind()
                gDatosCM.Items(nFilas).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).ForeColor = Drawing.Color.DarkRed
                gDatosCM.Items(nFilas).Font.Size = 12
                gDatosCM.Items(nFilas).Font.Bold = True
                gDatosCM.Items(nFilas).BorderColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).Cells(4).Text = "TOTALES -------->"
                gDatosCM.Items(nFilas).Cells(4).HorizontalAlign = HorizontalAlign.Right


                cuentaF = gDatosCM.Items.Count - (cuentaOtros + 1)
                Do While cuentaF < gDatosCM.Items.Count - 1
                    gDatosCM.Items(cuentaF).Cells(0).Text = Right(gDatosCM.Items(cuentaF).Cells(0).Text.Trim, gDatosCM.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
                    cuentaF = cuentaF + 1
                Loop
            End If
        Else



            sql = "SELECT idcita,convert(varchar(10),abonos.fecha,103) as fecha, clientes.elnombre,catServicios.descripcion, " & _
            "  catServicios.descripcion, " & _
            "  abonos.abono, abonos.importe-monto as importe, (abonos.importe-monto - abonos.abono) as Adeuda,abonos.idabono,abonos.fecha as fecha2,abonos.idcliente " & _
            " FROM  abonos left JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto left JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where abonos.facturado='false' and importe<>0 and datepart(yyyy,abonos.fecha)>2012 and abonos.idcliente='" + cmbdoctorc.SelectedValue.Trim + "' " & _
            "order by fecha2 asc"
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)
            Dim conexion As SqlConnection = clase.conecta(hfSucursal.Value)
            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(sql, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            tabla.TableName = "mitabla"
            gDatosCM.DataMember = "mitabla"
            gDatosCM.DataSource = tabla.DefaultView
            gDatosCM.DataBind()

            If gDatosCM.Items.Count > 0 Then
                Dim filaTotal As DataRow = tabla.NewRow
                Dim nFilas As Integer = gDatosCM.Items.Count
                Dim nColumnas As Integer = gDatosCM.Items(0).Cells.Count
                Dim cFilas As Integer = 0
                Dim cColumnas As Integer = 5
                Do While cColumnas < nColumnas
                    Dim sumaNvoPacientes As Integer = 0
                    Do While cFilas < nFilas
                        sumaNvoPacientes = sumaNvoPacientes + gDatosCM.Items(cFilas).Cells(cColumnas).Text
                        cFilas = cFilas + 1
                    Loop
                    filaTotal(cColumnas) = sumaNvoPacientes
                    cColumnas = cColumnas + 1
                    cFilas = 0
                Loop

                Dim cuentaF As Integer = gDatosCM.Items.Count
                Dim cuentaC As Integer = gDatosCM.Items(0).Cells.Count
                Dim columna As Integer = 0
                Dim fila As Integer = 0

                Dim cuentaOtros As Integer = 0
                Do While fila < cuentaF
                    Dim nomColum As String = gDatosCM.Items(fila).Cells(0).Text.Trim
                    If nomColum = "0" Then
                        columna = 0
                        Dim filaAux As DataRow = tabla.NewRow
                        Do While columna < cuentaC
                            filaAux(columna) = gDatosCM.Items(fila).Cells(columna).Text.Trim
                            columna = columna + 1
                        Loop
                        tabla.Rows(fila).Delete()
                        tabla.Rows.Add(filaAux)
                        cuentaOtros = cuentaOtros + 1
                    End If
                    fila = fila + 1
                Loop

                tabla.Rows.Add(filaTotal)
                gDatosCM.DataMember = "mitabla"
                gDatosCM.DataSource = tabla.DefaultView
                gDatosCM.DataBind()
                gDatosCM.Items(nFilas).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).ForeColor = Drawing.Color.DarkRed
                gDatosCM.Items(nFilas).Font.Size = 12
                gDatosCM.Items(nFilas).Font.Bold = True
                gDatosCM.Items(nFilas).BorderColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).Cells(4).Text = "TOTALES ------->"
                gDatosCM.Items(nFilas).Cells(4).HorizontalAlign = HorizontalAlign.Right


                cuentaF = gDatosCM.Items.Count - (cuentaOtros + 1)
                Do While cuentaF < gDatosCM.Items.Count - 1
                    gDatosCM.Items(cuentaF).Cells(0).Text = Right(gDatosCM.Items(cuentaF).Cells(0).Text.Trim, gDatosCM.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
                    cuentaF = cuentaF + 1
                Loop
            End If


        End If

        cmdexcelcm.Visible = True

        gDatosCM.Visible = True

    End Sub
    Sub llenardatos2()
        Dim sql As String

        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbdoctorc.SelectedValue

        If cmbdoctorc.SelectedValue = 0 Then
            sql = "SELECT idcita,convert(varchar(10),abonos.fecha,103) as fecha, clientes.elnombre,catServicios.descripcion," & _
            "  catServicios.descripcion, " & _
            "abonos.abono, abonos.importe-monto as importe, (abonos.importe-monto - abonos.abono) as Adeuda,abonos.idabono,abonos.fecha as fecha2,abonos.idcliente FROM  " & _
            "abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio " & _
            " where fecha>='" + txtfecha1.Text + " 00:00:00' and fecha<='" + txtfecha2.Text.Trim + " 12:00:00' AND facturado='false' and importe<>0 order by fecha2 asc"
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)
            Dim conexion As SqlConnection = clase.conecta(hfSucursal.Value)
            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(sql, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            tabla.TableName = "mitabla"
            gDatosCM.DataMember = "mitabla"
            gDatosCM.DataSource = tabla.DefaultView
            gDatosCM.DataBind()

            If gDatosCM.Items.Count > 0 Then
                Dim filaTotal As DataRow = tabla.NewRow
                Dim nFilas As Integer = gDatosCM.Items.Count
                Dim nColumnas As Integer = gDatosCM.Items(0).Cells.Count
                Dim cFilas As Integer = 0
                Dim cColumnas As Integer = 5
                Do While cColumnas < nColumnas
                    Dim sumaNvoPacientes As Integer = 0
                    Do While cFilas < nFilas
                        sumaNvoPacientes = sumaNvoPacientes + gDatosCM.Items(cFilas).Cells(cColumnas).Text
                        cFilas = cFilas + 1
                    Loop
                    filaTotal(cColumnas) = sumaNvoPacientes
                    cColumnas = cColumnas + 1
                    cFilas = 0
                Loop

                Dim cuentaF As Integer = gDatosCM.Items.Count
                Dim cuentaC As Integer = gDatosCM.Items(0).Cells.Count
                Dim columna As Integer = 0
                Dim fila As Integer = 0

                Dim cuentaOtros As Integer = 0
                Do While fila < cuentaF
                    Dim nomColum As String = gDatosCM.Items(fila).Cells(0).Text.Trim
                    If nomColum = "0" Then
                        columna = 0
                        Dim filaAux As DataRow = tabla.NewRow
                        Do While columna < cuentaC
                            filaAux(columna) = gDatosCM.Items(fila).Cells(columna).Text.Trim
                            columna = columna + 1
                        Loop
                        tabla.Rows(fila).Delete()
                        tabla.Rows.Add(filaAux)
                        cuentaOtros = cuentaOtros + 1
                    End If
                    fila = fila + 1
                Loop

                tabla.Rows.Add(filaTotal)
                gDatosCM.DataMember = "mitabla"
                gDatosCM.DataSource = tabla.DefaultView
                gDatosCM.DataBind()
                gDatosCM.Items(nFilas).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).ForeColor = Drawing.Color.DarkRed
                gDatosCM.Items(nFilas).Font.Size = 12
                gDatosCM.Items(nFilas).Font.Bold = True
                gDatosCM.Items(nFilas).BorderColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).Cells(4).Text = "TOTALES -------->"
                gDatosCM.Items(nFilas).Cells(4).HorizontalAlign = HorizontalAlign.Right


                cuentaF = gDatosCM.Items.Count - (cuentaOtros + 1)
                Do While cuentaF < gDatosCM.Items.Count - 1
                    gDatosCM.Items(cuentaF).Cells(0).Text = Right(gDatosCM.Items(cuentaF).Cells(0).Text.Trim, gDatosCM.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
                    cuentaF = cuentaF + 1
                Loop
            End If
        Else



            sql = "SELECT idcita,convert(varchar(10),abonos.fecha,103) as fecha, clientes.elnombre,catServicios.descripcion, " & _
            "  catServicios.descripcion, " & _
            "  abonos.abono, abonos.importe-monto as importe, (abonos.importe-monto - abonos.abono) as Adeuda,abonos.idabono,abonos.fecha as fecha2,abonos.idcliente " & _
            " FROM  abonos left JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto left JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where abonos.facturado='false' and importe<>0 and abonos.idcliente='" + cmbdoctorc.SelectedValue.Trim + "' " & _
            "order by fecha2 asc"
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)
            Dim conexion As SqlConnection = clase.conecta(hfSucursal.Value)
            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(sql, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            tabla.TableName = "mitabla"
            gDatosCM.DataMember = "mitabla"
            gDatosCM.DataSource = tabla.DefaultView
            gDatosCM.DataBind()

            If gDatosCM.Items.Count > 0 Then
                Dim filaTotal As DataRow = tabla.NewRow
                Dim nFilas As Integer = gDatosCM.Items.Count
                Dim nColumnas As Integer = gDatosCM.Items(0).Cells.Count
                Dim cFilas As Integer = 0
                Dim cColumnas As Integer = 5
                Do While cColumnas < nColumnas
                    Dim sumaNvoPacientes As Integer = 0
                    Do While cFilas < nFilas
                        sumaNvoPacientes = sumaNvoPacientes + gDatosCM.Items(cFilas).Cells(cColumnas).Text
                        cFilas = cFilas + 1
                    Loop
                    filaTotal(cColumnas) = sumaNvoPacientes
                    cColumnas = cColumnas + 1
                    cFilas = 0
                Loop

                Dim cuentaF As Integer = gDatosCM.Items.Count
                Dim cuentaC As Integer = gDatosCM.Items(0).Cells.Count
                Dim columna As Integer = 0
                Dim fila As Integer = 0

                Dim cuentaOtros As Integer = 0
                Do While fila < cuentaF
                    Dim nomColum As String = gDatosCM.Items(fila).Cells(0).Text.Trim
                    If nomColum = "0" Then
                        columna = 0
                        Dim filaAux As DataRow = tabla.NewRow
                        Do While columna < cuentaC
                            filaAux(columna) = gDatosCM.Items(fila).Cells(columna).Text.Trim
                            columna = columna + 1
                        Loop
                        tabla.Rows(fila).Delete()
                        tabla.Rows.Add(filaAux)
                        cuentaOtros = cuentaOtros + 1
                    End If
                    fila = fila + 1
                Loop

                tabla.Rows.Add(filaTotal)
                gDatosCM.DataMember = "mitabla"
                gDatosCM.DataSource = tabla.DefaultView
                gDatosCM.DataBind()
                gDatosCM.Items(nFilas).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).ForeColor = Drawing.Color.DarkRed
                gDatosCM.Items(nFilas).Font.Size = 12
                gDatosCM.Items(nFilas).Font.Bold = True
                gDatosCM.Items(nFilas).BorderColor = Drawing.Color.FromArgb(194, 214, 154)
                gDatosCM.Items(nFilas).Cells(4).Text = "TOTALES ------->"
                gDatosCM.Items(nFilas).Cells(4).HorizontalAlign = HorizontalAlign.Right


                cuentaF = gDatosCM.Items.Count - (cuentaOtros + 1)
                Do While cuentaF < gDatosCM.Items.Count - 1
                    gDatosCM.Items(cuentaF).Cells(0).Text = Right(gDatosCM.Items(cuentaF).Cells(0).Text.Trim, gDatosCM.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
                    cuentaF = cuentaF + 1
                Loop
            End If


        End If

        cmdexcelcm.Visible = True

        gDatosCM.Visible = True

    End Sub
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        llenardatos2()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
        End If


    End Sub
    Protected Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged


        If cmbSucursal.SelectedValue <> "0" Then

            cmbdoctorc = funciones.llenacombos(cmbdoctorc, "select idcliente,elnombre from clientes where elnombre<>' ' order by elnombre", cmbSucursal.SelectedValue)
            llenardatos()
        Else

            cmbdoctorc.Items.Clear()
            gDatosCM.Visible = False
        End If

    End Sub
    Protected Sub cmbdoctorc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbdoctorc.SelectedIndexChanged
        llenardatos()
    End Sub
    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gDatosCM.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gDatosCM)
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
