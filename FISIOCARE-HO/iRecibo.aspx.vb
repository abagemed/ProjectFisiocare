Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared

Partial Class iRecibo
    Inherits System.Web.UI.Page
    Dim FUNCIONES As New miclases
    Public mireporte As New ReportDocument
    Public clases As New miclases
    Protected Sub btnimprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnimprimir.Click
        Dim valor1, valor2, valor3
        Dim conexion As SqlConnection = FUNCIONES.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim cadreg As String   'catcostos.costo
        Dim caddescuento As String
        Dim cadapoyo As String
        Dim descuento As String
        Dim apoyo As String
        Dim cadobservacion As String
        Dim observacion As String
        cadreg = ""
        comando.CommandText = "select catservicios.descripcion , abonos.abono,catCostos.descuentoc, catCostos.apoyo, abonos.importe,abonos.idcosto,abonos.observacion " & _
            " from abonos inner join catcostos on abonos.idcosto=catcostos.idcosto inner join catservicios " & _
            " on catcostos.id_servicio=catservicios.id_servicio where abonos.idcita='" + lblidcita.Text.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        cadreg = ""
        Do While leer.Read
            cadreg = cadreg & leer.GetValue(5).ToString.Trim & "|" & leer.GetValue(0).ToString.Trim & "|" & Format(leer.GetValue(4), "###,###,###0.00") & "@"
            cadobservacion = cadobservacion & leer.GetValue(6).ToString.Trim & "@"
            caddescuento = caddescuento & " DESCUENTO " & "|" & Format(leer.GetValue(2), "###,###,###0.00") & "@"
            cadapoyo = cadapoyo & " APOYO " & "|" & Format(leer.GetValue(3), "###,###,###0.00") & "@"
            observacion = leer.GetValue(6).ToString.Trim
            apoyo = Format(leer.GetValue(3), "###,###,###0.00")
            descuento = Format(leer.GetValue(2), "###,###,###0.00")
        Loop
        leer.Close()
        conexion.Close()
        '----------------------------
        comando.CommandText = "select idpago, sum(abono) as total from abonos" & _
                              " where idCita = '" + lblidcita.Text.Trim + "'  group by idpago "
        conexion.Open()
        leer = comando.ExecuteReader
        valor1 = 0
        valor2 = 0
        valor3 = 0
        Do While leer.Read
            Select Case leer.GetValue(0).ToString.Trim
                Case "EF"
                    valor1 = valor1 + leer.GetValue(1)
                Case "TC"
                    valor2 = valor2 + leer.GetValue(1)
                Case "TD"
                    valor3 = valor3 + leer.GetValue(1)
            End Select
        Loop
        leer.Close()
        conexion.Close()
        valor1 = Format(valor1, "###,###,###0.00")
        valor2 = Format(valor2, "###,###,###0.00")
        valor3 = Format(valor3, "###,###,###0.00")
        '----------------------------
        'Response.Redirect("popup.aspx?idcita=" + lblrecibo.Text.Trim + "" & _
        '                "&cliente=" + txtcliente.Text + "&fecha=" + txtfecha.Text.Trim + "" & _
        '                "&abono=" + txtabono.Text.Trim + "&importe=" + txtimporte.Text + "&conceptos=" + cadreg.Trim)

        Dim popupScript As String = " window.open('popupC.aspx?idcita=" + lblrecibo.Text.Trim + "&cliente=" + txtcliente.Text + "&fecha=" + txtfecha.Text.Trim + "" & _
                       "&abono=" + txtabono.Text.Trim + "&importe=" + txtabono.Text + "&conceptos=" + cadreg.Trim + "&observacion=" + cadobservacion.Trim + "&descuento=" + caddescuento.Trim + "&apoyo=" + cadapoyo.Trim + "&vobservacion=" + observacion.Trim + "&vdescuento=" + descuento.Trim + "&vapoyo=" + apoyo.Trim + "&val1=" + valor1.ToString + "&val2=" + valor2.ToString + "&val3=" + valor3.ToString + "', 'CustomPopUp', 'width=450, height=400, menubar=no, resizable=no'); "
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "PopupScript", popupScript, True)

    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'For Each impresorasInstaladas As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
            'cmbImpresoras.Items.Add(impresorasInstaladas)
            'Next
            cmbImpresoras.Visible = False

            lblidcita.Text = Context.Items("idcita").ToString.Trim
            lblrecibo.Text = lblidcita.Text.Trim
            'txtterapia.Text = Context.Items("varidterapia").ToString.Trim
            txtcliente.Text = Context.Items("varidcliente").ToString.Trim
            'txtimporte.Text = Context.Items("varimporte").ToString.Trim
            txtfecha.Text = Context.Items("varfecha").ToString.Trim
            'txtabono.Text = Context.Items("varabono").ToString.Trim
            lblfechacita.Text = Context.Items("fecha").ToString.Trim
            lblpagina.Text = Context.Items("pagina").ToString.Trim
            txtcliente.Text = FUNCIONES.leerValor("select elnombre from clientes where idcliente='" + txtcliente.Text.Trim + "'")
            Dim aux1 As Double = FUNCIONES.leerValor("select sum(importe) from abonos where idcita='" + lblidcita.Text.Trim + "'")
            txtimporte.Text = Format(aux1, "###,###0.00")
            Dim aux2 As Double = FUNCIONES.leerValor("select sum(abono) from abonos where idcita='" + lblidcita.Text.Trim + "'")
            txtabono.Text = Format(aux2, "###,###0.00")  'catcostos.costo
            gridConceptos = FUNCIONES.creadataset("select catservicios.descripcion , abonos.importe as costo " & _
            "from abonos inner join catcostos on abonos.idcosto=catcostos.idcosto inner join catservicios " & _
            "on catcostos.id_servicio=catservicios.id_servicio where abonos.idcita='" + lblidcita.Text.Trim + "'", gridConceptos)
        End If
    End Sub

    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        FUNCIONES.grabaDatos("update abonos set reciboPagado='true' where idcita='" + lblidcita.Text.Trim + "'")
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer(lblpagina.Text.Trim, False)
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
    '    Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
    '    mireporte.Load("c:\reporteFisio\ReciboPagoSM.rpt")
    '    Dim conexion As SqlConnection = FUNCIONES.conecta
    '    'With visor_reporte
    '    '    .DisplayGroupTree = False
    '    '    .HasExportButton = False
    '    '    .HasPrintButton = False
    '    '    .DisplayGroupTree = False
    '    '    .HasToggleGroupTreeButton = False
    '    '    .HasZoomFactorList = False
    '    '    .HasViewList = False
    '    '    .DisplayToolbar = False
    '    'End With
    '    Dim odataadapter As New SqlDataAdapter("select catservicios.descripcion , catcostos.costo " & _
    '        "from abonos inner join catcostos on abonos.idcosto=catcostos.idcosto inner join catservicios " & _
    '        "on catcostos.id_servicio=catservicios.id_servicio where abonos.idcita='" + lblidcita.Text.Trim + "'", conexion)

    '    Dim odataset As New DataSet
    '    odataadapter.Fill(odataset)
    '    mireporte.SetDataSource(odataset.Tables(0))


    '    Mivar.Value = txtcliente.Text.Trim
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("cliente").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = txtimporte.Text.Trim
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("costo").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = txtfecha.Text.Trim
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("laFecha").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = lblrecibo.Text.Trim
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("numrecibo").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Dim valor1, valor2, valor3
    '    Dim comando As SqlCommand = conexion.CreateCommand
    '    Dim leer As SqlDataReader
    '    comando.CommandText = "select idpago, sum(abono) as total from abonos" & _
    '                          " where idCita = '" + lblidcita.Text.Trim + "'  group by idpago "
    '    conexion.Open()
    '    leer = comando.ExecuteReader
    '    valor1 = 0
    '    valor2 = 0
    '    valor3 = 0
    '    Do While leer.Read
    '        Select Case leer.GetValue(0).ToString.Trim
    '            Case "EF"
    '                valor1 = valor1 + leer.GetValue(1)
    '            Case "TC"
    '                valor2 = valor2 + leer.GetValue(1)
    '            Case "CH"
    '                valor3 = valor3 + leer.GetValue(1)
    '        End Select
    '    Loop
    '    leer.Close()
    '    conexion.Close()
    '    If valor1 <> 0 Then
    '        valor1 = "EF" + Format(valor1, "###,###,###0.00").ToString
    '    Else
    '        valor1 = ""
    '    End If
    '    If valor2 <> 0 Then
    '        valor2 = "TC" + Format(valor2, "###,###,###0.00").ToString
    '    Else
    '        valor2 = ""
    '    End If
    '    If valor3 <> 0 Then
    '        valor3 = "CH" + Format(valor3, "###,###,###0.00").ToString
    '    Else
    '        valor3 = ""
    '    End If

    '    Mivar.Value = valor1
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("valor1").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = valor2
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("valor2").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = valor3
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("valor3").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    visor_reporte.DataBind()
    '    visor_reporte.ReportSource = mireporte
    '    visor_reporte.PrintMode = PrintMode.Pdf
    '    visor_reporte.Visible = False

    '    'exporta el rpt a pdf
    '    Dim nomArchivo As String = lblrecibo.Text + Replace(txtfecha.Text, "/", "")
    '    nomArchivo.Replace(" ", "")
    '    Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    '    Dim o As CrystalDecisions.Shared.ExportOptions
    '    o = New CrystalDecisions.Shared.ExportOptions
    '    o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    '    o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '    'filedest.DiskFileName = "c:\reporteFisio\reciboPago.pdf"
    '    filedest.DiskFileName = Server.MapPath("Recibos") & "\" + nomArchivo + ".pdf"
    '    o.ExportDestinationOptions = filedest.Clone
    '    mireporte.Export(o)
    '    filedest = Nothing
    '    o = Nothing
    '    mireporte.Close()

    '    'se abre el pdf en acrobat
    '    'Dim Nombre As String
    '    'Nombre = "c:\reporteFisio\reciboPago.pdf"
    '    'Response.Clear()
    '    'Response.ContentType = "application/pdf"
    '    'Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
    '    'Response.WriteFile(Nombre)
    '    'Response.Flush()
    '    'Response.Close()
    '    Response.Write("<script type='text/javascript'>detailedresults=window.open('ImpExReporte.aspx?doc=" & nomArchivo & ".pdf');</script>")

    'End Sub

    'Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
    '    mireporte.Close()
    '    mireporte.Dispose()
    'End Sub
End Class
