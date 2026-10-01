Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Imports System.IO

Partial Class Corte
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim fCorreos As New Funciones
    Public mireporte As New ReportDocument
    Sub buscar()

        tefectivo.Text = "0"
        tdebito.Text = "0"
        tcredito.Text = "0"
        ttrasnferencia.Text = "0"
        ttotal.Text = "0"

        DataGrid1 = funciones.creadataset(laConsulta, DataGrid1)



        If DataGrid1.Items.Count > 0 Then
            Dim filas As Integer = 0
            Dim columnas As Integer = 4
            Dim sumas As Integer = 0
            Do While filas < DataGrid1.Items.Count

                If DataGrid1.Items(filas).Cells(4).Text = "EFECTIVO" Then
                    sumas = sumas + DataGrid1.Items(filas).Cells(6).Text
                    tefectivo.Text = Format(sumas, "###,###,###0.00")
                End If

                filas = filas + 1
            Loop
        End If

        If DataGrid1.Items.Count > 0 Then
            Dim filas As Integer = 0
            Dim columnas As Integer = 4
            Dim sumas As Integer = 0
            Do While filas < DataGrid1.Items.Count




                If DataGrid1.Items(filas).Cells(4).Text = "TARJETA DE CREDITO" Then
                    sumas = sumas + DataGrid1.Items(filas).Cells(6).Text
                    tcredito.Text = Format(sumas, "###,###,###0.00")
                End If


                filas = filas + 1
            Loop
        End If
        If DataGrid1.Items.Count > 0 Then
            Dim filas As Integer = 0
            Dim columnas As Integer = 4
            Dim sumas As Integer = 0
            Do While filas < DataGrid1.Items.Count


                If DataGrid1.Items(filas).Cells(4).Text = "TARJETA DE DEBITO" Then
                    sumas = sumas + DataGrid1.Items(filas).Cells(6).Text
                    tdebito.Text = Format(sumas, "###,###,###0.00")
                End If


                filas = filas + 1
            Loop
        End If
        If DataGrid1.Items.Count > 0 Then
            Dim filas As Integer = 0
            Dim columnas As Integer = 4
            Dim sumas As Integer = 0
            Do While filas < DataGrid1.Items.Count


                If DataGrid1.Items(filas).Cells(4).Text = "TRANSFERENCIA ELECTRONICA DE FONDO" Then
                    sumas = sumas + DataGrid1.Items(filas).Cells(6).Text
                    ttrasnferencia.Text = Format(sumas, "###,###,###0.00")
                End If

                filas = filas + 1
            Loop
        End If

        If DataGrid1.Items.Count > 0 Then
            Dim filas As Integer = 0
            Dim columnas As Integer = 4
            Dim sumas As Integer = 0
            Do While filas < DataGrid1.Items.Count



                sumas = sumas + DataGrid1.Items(filas).Cells(6).Text
                ttotal.Text = Format(sumas, "###,###,###0.00")


                filas = filas + 1
            Loop
        End If

        If tefectivo.Text > "0" Then
            total_efectivo.Visible = True
        Else
            total_efectivo.Visible = False
        End If
        If tdebito.Text > "0" Then
            total_tarjeta_debito.Visible = True
        Else
            total_tarjeta_debito.Visible = False
        End If
        If tcredito.Text > "0" Then
            total_tarjeta_credito.Visible = True
        Else
            total_tarjeta_credito.Visible = False
        End If
        If tcredito.Text > "0" Then
            total_tarjeta_credito.Visible = True
        Else
            total_tarjeta_credito.Visible = False
        End If
        If ttrasnferencia.Text > "0" Then
            total_transferencia.Visible = True
        Else
            total_transferencia.Visible = False
        End If
        If ttotal.Text > "0" Then
            total_general.Visible = True
        Else
            total_general.Visible = False
        End If
    End Sub
    Function laConsulta() As String
        

        Dim fecha1 As Date = txtFecha.Text.Trim
        Dim fecha2 As Date = txtfecha2.Text.Trim


        If cboturno.SelectedIndex = "0" Then


            Dim consulta As String = " SELECT I.idcita AS 'Recibo',FORMAT(I.fecha, 'dd/MMM/yyyy', 'es-mx') as 'Fecha'," & _
"P.idCliente as IdPaciente,  (P.nombre+' '+P.paterno+' '+P.materno) as Paciente,CP.descripcion AS 'Forma de pago',C.descripcion, ( '$ ' + format(convert(decimal(8,2),I.abono),'N','en-us')  ) AS 'Importe Pago', " & _
"I.serie+'-'+I.num_factura as 'Factura' , " & _
"(S.descripcion) As 'Concepto de pago' " & _
"FROM [abonos] as I " & _
"INNER JOIN  clientes AS P ON I.idCliente = P.idCliente " & _
"INNER JOIN catCostos C ON C.idCosto = I.idCosto " & _
"INNER JOIN catServicios S ON S.id_servicio = C.id_servicio " & _
"inner join agenda A on I.idCita  = A.idCita " & _
"inner join CatFormasDePago CP on CP.descripcioncorta = I.idpago " & _
"WHERE I.fecha>='" + txtFecha.Text + "' and I.fecha<='" + txtfecha2.Text + "' AND (A.turno='M' or A.turno='V') " & _
"order by fecha"



            Return consulta

        Else

            If cboturno.SelectedIndex = "0" And cboterapistas.SelectedIndex = "0" Then


                Dim consulta As String = " SELECT I.idcita AS 'Recibo',FORMAT(I.fecha, 'dd/MMM/yyyy', 'es-mx') as 'Fecha'," & _
"P.idCliente as IdPaciente,  (P.nombre+' '+P.paterno+' '+P.materno) as Paciente,CP.descripcion AS 'Forma de pago', C.descripcion, ( '$ ' + format(convert(decimal(8,2),I.abono),'N','en-us')  ) AS 'Importe Pago', " & _
"I.serie+'-'+I.num_factura as 'Factura' , " & _
"(S.descripcion) As 'Concepto de pago' " & _
"FROM [abonos] as I " & _
"INNER JOIN  clientes AS P ON I.idCliente = P.idCliente " & _
"INNER JOIN catCostos C ON C.idCosto = I.idCosto " & _
"INNER JOIN catServicios S ON S.id_servicio = C.id_servicio " & _
"inner join agenda A on I.idCita  = A.idCita " & _
"inner join CatFormasDePago CP on CP.descripcioncorta = I.idpago " & _
"WHERE I.fecha>='" + txtFecha.Text + "' and I.fecha<='" + txtfecha2.Text + "' AND (A.turno='M' or A.turno='V') " & _
"order by fecha"



                Return consulta
            Else

                If cboterapistas.SelectedIndex = "0" Then


                    Dim consulta As String = " SELECT I.idcita AS 'Recibo',FORMAT(I.fecha, 'dd/MMM/yyyy', 'es-mx') as 'Fecha'," & _
"P.idCliente as IdPaciente,  (P.nombre+' '+P.paterno+' '+P.materno) as Paciente,CP.descripcion AS 'Forma de pago', C.descripcion, ( '$ ' + format(convert(decimal(8,2),I.abono),'N','en-us')  ) AS 'Importe Pago', " & _
"I.serie+'-'+I.num_factura as 'Factura' , " & _
"(S.descripcion) As 'Concepto de pago' " & _
"FROM [abonos] as I " & _
"INNER JOIN  clientes AS P ON I.idCliente = P.idCliente " & _
"INNER JOIN catCostos C ON C.idCosto = I.idCosto " & _
"INNER JOIN catServicios S ON S.id_servicio = C.id_servicio " & _
"inner join agenda A on I.idCita  = A.idCita " & _
"inner join CatFormasDePago CP on CP.descripcioncorta = I.idpago " & _
"WHERE I.fecha>='" + txtFecha.Text + "' and I.fecha<='" + txtfecha2.Text + "' AND A.turno='" & cboturno.SelectedValue & "' " & _
"order by fecha"


                    Return consulta
                Else


                    Dim consulta As String = " SELECT I.idcita AS 'Recibo',FORMAT(I.fecha, 'dd/MMM/yyyy', 'es-mx') as 'Fecha'," & _
"P.idCliente as IdPaciente,  (P.nombre+' '+P.paterno+' '+P.materno) as Paciente,CP.descripcion AS 'Forma de pago', C.descripcion, ( '$ ' + format(convert(decimal(8,2),I.abono),'N','en-us')  ) AS 'Importe Pago', " & _
"I.serie+'-'+I.num_factura as 'Factura' , " & _
"(S.descripcion) As 'Concepto de pago' " & _
"FROM [abonos] as I " & _
"INNER JOIN  clientes AS P ON I.idCliente = P.idCliente " & _
"INNER JOIN catCostos C ON C.idCosto = I.idCosto " & _
"INNER JOIN catServicios S ON S.id_servicio = C.id_servicio " & _
"inner join agenda A on I.idCita  = A.idCita " & _
"inner join CatFormasDePago CP on CP.descripcioncorta = I.idpago " & _
"WHERE I.fecha>='" + txtFecha.Text + "' and I.fecha<='" + txtfecha2.Text + "' AND A.posicion='" & cboterapistas.SelectedValue & "' and A.turno='" & cboturno.SelectedValue & "' " & _
"order by fecha"

                    Return consulta


                End If

            End If

        End If




    End Function
    Protected Sub cboterapistas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboterapistas.SelectedIndexChanged

        buscar()

    End Sub
    Protected Sub cboturno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboturno.SelectedIndexChanged
        cboterapistas = funciones.llenacombos(cboterapistas, "SELECT columna, nombre FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")

        buscar()

    End Sub
    
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFechas.Click
        buscar()
    End Sub

    'Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnImprimir.Click
    '    Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
    '    Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
    '    mireporte.Load("c:\reporteFisio\CortedeCaja.rpt")
    '    Dim conexion As SqlConnection = funciones.conecta
    '    With visor_reporte
    '        .DisplayGroupTree = False
    '        .HasExportButton = False
    '        .HasPrintButton = False
    '        .DisplayGroupTree = False
    '        .HasToggleGroupTreeButton = False
    '        .HasZoomFactorList = False
    '        .HasViewList = False
    '        .DisplayToolbar = False
    '    End With
    '    Dim odataadapter As New SqlDataAdapter("select convert(varchar, fecha, 103) as fecha,nombre+' '+paterno+' '+materno " & _
    '        "as elnombre,abono, idpago from abonos left join clientes on  " & _
    '        "abonos.idcliente = clientes.idcliente where abono<>'0' and fecha='" + txtFecha.Text + "' order by elnombre", conexion)

    '    Dim odataset As New DataSet
    '    odataadapter.Fill(odataset)
    '    mireporte.SetDataSource(odataset.Tables(0))

    '    Mivar.Value = txtFecha.Text
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Dim total As Double = funciones.leerValor("select sum(abono) from abonos where abono<>'0' and fecha='" + txtFecha.Text + "'")
    '    Mivar.Value = Format(total, "###,###,###0.00").ToString
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    visor_reporte.DataBind()
    '    visor_reporte.ReportSource = mireporte

    '    'exporta el rpt a pdf
    '    Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    '    Dim o As CrystalDecisions.Shared.ExportOptions
    '    o = New CrystalDecisions.Shared.ExportOptions
    '    o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    '    o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '    filedest.DiskFileName = "c:\reporteFisio\cortedecaja.pdf"
    '    o.ExportDestinationOptions = filedest.Clone
    '    mireporte.Export(o)
    '    filedest = Nothing
    '    o = Nothing

    '    'se abre el pdf en acrobat
    '    Dim Nombre As String
    '    Nombre = "c:\reporteFisio\cortedecaja.pdf"
    '    Response.Clear()
    '    Response.ContentType = "application/pdf"
    '    Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
    '    Response.WriteFile(Nombre)
    '    Response.Flush()
    '    Response.Close()
    'End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then

            cboterapistas = funciones.llenacombos(cboterapistas, "SELECT columna, nombre FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")
            'cbodoctores.SelectedIndex = 1

            Try
                lblfechacita.Text = Context.Items("fecha").ToString.Trim
            Catch
                lblfechacita.Text = Request.QueryString("fecha").Trim
            End Try
            Dim hoy As DateTime = DateTime.Now()
            lblfecha1.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            lblfecha2.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            txtFecha.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            calendario.SelectedDate = txtFecha.Text
            calendario2.SelectedDate = txtfecha2.Text
            buscar()
            'buscar2()

        End If
        'cboterapistas = funciones.llenacombos(cboterapistas, "SELECT columna, nombre FROM Terapistas where activo='True' order by columna")
        'cbodoctores.SelectedIndex = 1
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    'Protected Sub Button2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnexportar.Click
    '    If cmbExporta.Value = "1" And txtEmail.Text = "" Then
    '        Messagebox1.ShowMessage("Favor de poner el correo electronico")
    '        txtEmail.Attributes.Add("style", "display:inline")
    '    Else
    '        Dim responsePage As HttpResponse = Response
    '        Dim sb As New StringBuilder()
    '        Dim sw As New StringWriter(sb)
    '        Dim htw As New HtmlTextWriter(sw)
    '        Dim pageToRender As New Page()
    '        Dim form As New HtmlForm()
    '        Dim dGrid As New DataGrid

    '        dGrid = DataGrid1
    '        form.Controls.Add(dGrid)
    '        pageToRender.Controls.Add(form)
    '        responsePage.Clear()
    '        responsePage.Buffer = True
    '        responsePage.AddHeader("Content-Disposition", "attachment;filename=informe.xls")
    '        responsePage.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"
    '        responsePage.Charset = "UTF-8"
    '        responsePage.ContentEncoding = Encoding.Default
    '        pageToRender.RenderControl(htw)
    '        responsePage.Write(sw.ToString())
    '        File.WriteAllText("C:\reporteFisio\informe.xls", sw.ToString)
    '        If cmbExporta.Value = "1" And txtEmail.Text <> "" Then
    '            fCorreos.enviaCorreoXls(txtEmail.Text, "C:\reporteFisio\informe.xls", "informe.xls", "informe", "")
    '            Messagebox1.ShowMessage("Informacion enviada")
    '        End If
    '        responsePage.End()
    '    End If
    'End Sub

    'Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
    '    e.Item.Cells(0).Width = "600"
    '    e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
    'End Sub

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario2.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario2_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario2.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim lafecha1 As Date = calendario.SelectedDate
            Dim lafecha2 As Date = calendario2.SelectedDate
            lblfecha1.Text = lafecha1.Day.ToString + " " + MonthName(lafecha1.Month)
            lblfecha2.Text = lafecha2.Day.ToString + " " + MonthName(lafecha2.Month)
            txtFecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha1)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", lafecha2)
            buscar()
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS...")
        End Try
    End Sub



    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
        Server.Transfer("clientes.aspx", True)
    End Sub

    Protected Sub Recibos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub Historial(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub Catalogos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub CorteCaja(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub BloqueoCitas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
    End Sub

    Protected Sub FacturasGeneradas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub

    Protected Sub VerCancelados(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub
    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub

End Class
