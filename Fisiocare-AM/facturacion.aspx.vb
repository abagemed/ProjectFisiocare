Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.IO
Imports com.facturarenlinea.timbrado.RespuestaTFD
Imports com.facturarenlinea.timbrado
Imports CFDI
Imports ThoughtWorks.QRCode.Codec
Imports System.Xml
Imports System.Data
Imports System.Data.SqlClient
Imports System.Drawing
Imports System.Drawing.ImageFormatConverter
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Partial Class facturacion
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim lasfunciones As New Funciones
    Dim mireporte As New ReportDocument

    Public Function Validapagos(ByVal consulta As String) As String
        Dim valor1 As String = ""
        Dim valor2 As String = ""
        Dim estatus As String = ""
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "select sum(abono) as pago, sum(importe) as importe from abonos " & _
        " where " + lblCondiciones.Text + " group by idCliente "

        conexion.Open()
        Dim sqlread As SqlDataReader = comando.ExecuteReader
        If sqlread.Read Then
            valor1 = sqlread.GetValue(0).ToString.Trim
            valor2 = sqlread.GetValue(1).ToString.Trim
        End If
        sqlread.Close()
        conexion.Close()
        If CDec(valor1.Trim) >= CDec(valor2.Trim) Then
            estatus = "True"
        Else
            estatus = "False"
        End If

        Return estatus
    End Function
    Sub terapiaSinfacturar()
        gridFacturas = funciones.creadataset("SELECT clientes.elnombre, convert(varchar(10),abonos.fecha,103) as fecha, catServicios.descripcion, " & _
            "abonos.abono, abonos.importe-monto as importe,abonos.idabono, abonos.fecha as fecha2,idcita,abonos.idcliente,coaseguro,ligaAbono " & _
            " FROM  abonos left JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto left JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where abonos.facturado='false' and abonos.idcliente='" + cmbclientes.SelectedValue.Trim + "' " & _
            "order by fecha2 asc", gridFacturas)

        gridFacturas.Columns(6).Visible = True
        If gridFacturas.Items.Count > 0 Then
            btnfacturar.Enabled = True
            btnAsigna.Enabled = True
        Else
            btnfacturar.Enabled = False
            btnAsigna.Enabled = False
        End If
    End Sub
    Function generaEimprimeFactura()
        Dim banderaError As Boolean = False
        Dim auxfecha As String = String.Format("{0:dd/MM/yyyy}", ftxtdia.Text.Trim.PadLeft(2, "0") + "/" + cmbmes.SelectedValue.Trim + "/" + cmbaño.SelectedValue + " 12:00:00")
        Dim auxNfac As String = txtNfac.Text.Trim '+ " " + txtNserie.Text.Trim
        Dim clase As New miclases
        Dim cuenta As Int32 = gridDetalles.Items.Count
        Dim contador As Int32 = 0
        Dim auximporte As Decimal
        Dim Sgraba As String = ""
        Dim cpagado As String = ""
        cpagado = Validapagos("")
        Dim comp As New Comprobante()
        comp.folio = txtNfac.Text.Trim
        comp.serie = txtNserie.Text.Trim
        Dim fecha As DateTime = DateTime.Now
        comp.fecha = String.Format("{0}T{1}", fecha.ToString("yyyy-MM-dd"), fecha.ToString("HH:mm:ss"))
        comp.formaDePago = "PAGO EN UNA SOLA EXHIBICION"
        comp.subTotal = Replace(ftxtimporte.Text.Trim, ",", "")
        comp.total = Replace(ftxttotal.Text, ",", "")
        comp.tipoDeComprobante = "ingreso"
        comp.moneda = "MXP"
        comp.tipoCambio = "1.0"
        comp.metodoDePago = cmbClaveMP.Items(cmbClaveMP.SelectedIndex).Text.Trim
        comp.lugarExpedicion = "MÉRIDA,YUCATÁN"

        Dim emisor As New Emisor()
        emisor.rfc = "FMD020730PQ5"
        emisor.nombre = "FISIOTERAPIA Y MEDICINA DEPORTIVA SCP"
        emisor.domicilioFiscal = New DomicilioFiscal
        emisor.domicilioFiscal.calle = "19"
        emisor.domicilioFiscal.noExterior = "88"
        emisor.domicilioFiscal.colonia = "CAMPESTRE"
        emisor.domicilioFiscal.localidad = "MÉRIDA"
        emisor.domicilioFiscal.municipio = "MÉRIDA"
        emisor.domicilioFiscal.estado = "YUCATÁN"
        emisor.domicilioFiscal.pais = "MEXICO"
        emisor.domicilioFiscal.codigoPostal = "97120"
        emisor.regimenFiscal = New RegimenFiscal
        emisor.regimenFiscal.regimen = "REGIMEN GENERAL DE LAS PERSONAS MORALES"

        Dim resultados(,) As String
        If hfidDatosFac.Value = "" Or hfidDatosFac.Value = "0" Then
            resultados = clase.leerValores("select nombre,rfc,calle,noext,colonia,ciudad,cp,municipio," & _
            "edo,pais from clientes where idcliente='" + cmbclientes.SelectedValue + "'", 10)
        Else
            resultados = clase.leerValores("select nombre,rfc,calle,noext,colonia,ciudad,cp,municipio," & _
            "edo,pais from catfacturas where iddatosfac='" + hfidDatosFac.Value.ToString + "'", 10)
        End If

        Dim receptor As New Receptor()
        receptor.rfc = ftxtrfc.Text.Trim
        receptor.nombre = ftxtnombre.Text.Trim
        receptor.domicilio = New Domicilio()
        receptor.domicilio.calle = resultados(2, 0)
        receptor.domicilio.noExterior = resultados(3, 0)
        receptor.domicilio.colonia = resultados(4, 0)
        receptor.domicilio.localidad = resultados(5, 0)
        receptor.domicilio.codigoPostal = resultados(6, 0)
        receptor.domicilio.municipio = resultados(7, 0)
        receptor.domicilio.estado = resultados(8, 0)
        receptor.domicilio.pais = resultados(9, 0)

        comp.conceptos = New List(Of Concepto)()
        Do While contador < cuenta
            If CType(gridDetalles.Items(contador).Cells(2).Controls(1), TextBox).Text.Trim <> "" Then
                auximporte = CType(gridDetalles.Items(contador).Cells(2).Controls(1), TextBox).Text.Trim
            Else
                auximporte = 0
            End If
            Sgraba = Sgraba + "insert into factura(num_factura,serie,fecha,cantidad,idcosto,importe,nombre," & _
            "direccion,ciudad,rfc,capCuenta,paciente,observaciones,idpago,numcuenta,idCliente) " & _
            "values('" + auxNfac + "','" + txtNserie.Text.Trim + "','" + auxfecha + "'," & _
            "'" + CType(gridDetalles.Items(contador).Cells(0).Controls(1), TextBox).Text + "','" + gridDetalles.DataKeys.Item(contador).ToString.Trim + "'," & _
            "'" + auximporte.ToString.Trim + "','" + ftxtnombre.Text.Trim + "'," & _
            "'" + ftxtdomicilio.Text.Trim + "','" + ftxtciudad.Text.Trim + "','" + ftxtrfc.Text.Trim + "'" & _
            ",'" + cpagado + "','" + txtpaciente.Text.Trim + "','" + TXTmiobserva.Text.Trim + "'," & _
            "'" + cmbFormaPago.SelectedValue.Trim + "','" + txtnumcuenta.Text + "','" + cmbclientes.SelectedValue.ToString.Trim + "');"

            Dim concepto As New Concepto()
            Dim cantidad As Decimal = CType(gridDetalles.Items(contador).Cells(0).Controls(1), TextBox).Text
            concepto.cantidad = cantidad.ToString.Trim
            concepto.unidad = "No aplica"
            concepto.noIdentificacion = gridDetalles.DataKeys.Item(contador).ToString.Trim
            concepto.descripcion = CType(gridDetalles.Items(contador).Cells(1).Controls(1), TextBox).Text
            concepto.importe = Decimal.Round(auximporte, 4)
            concepto.valorUnitario = Decimal.Round(auximporte / cantidad, 4).ToString.Trim
            comp.conceptos.Add(concepto)

            contador = contador + 1
        Loop


        Dim iva As New Traslado()
        iva.impuesto = "IVA"
        iva.importe = "0.0000"
        iva.tasa = "0"

        Dim impuestos As New Impuestos()
        impuestos.traslados = New List(Of Traslado)()
        impuestos.totalImpuestosTrasladados = "0.0000"
        impuestos.totalImpuestosRetenidos = "0.0000"
        impuestos.traslados.Add(iva)

        comp.emisor = emisor
        comp.receptor = receptor
        comp.impuestos = impuestos

        Dim Xml
        Dim cer
        Try
            cer = New X509Certificate2(Server.MapPath("fmd020730pq5_1307161754s.pfx"), "fmdscp2013", X509KeyStorageFlags.MachineKeySet)
            Xml = CFDIv32.Serializar(comp, False)

        Catch ex As Exception
            banderaError = True
            Messagebox1.ShowMessage("Error en el sellado del xml para timbrar favor de llamar a informatica " + ex.Message.ToString)
        End Try

        If banderaError = False Then
            Dim cadenaOriginal = CFDIv32.CadenaOriginalSellado(Xml)
            Try
                Dim ar = cer.GetSerialNumber
                Array.Reverse(ar)
                comp.noCertificado = Encoding.UTF8.GetString(ar)
                comp.certificado = Convert.ToBase64String(cer.RawData) 'Certificado igual para la cancelación del cfdi
                comp.sello = CFDIv32.Sello(cadenaOriginal, cer)

                Xml = CFDIv32.Serializar(comp, True)
                CFDIv32.Validar(Xml, True)
                File.WriteAllText(Server.MapPath("mandatorioCP.xml"), Xml)
            Catch ex As Exception
                banderaError = True
                Messagebox1.ShowMessage("Error en el xml para timbrar favor de llamar a informatica" + ex.Message.ToString)
            End Try

            Dim uuid, sellocfd, nocertificadoSat, selloSat, FechaTimbrado As String
            If banderaError = False Then
                Try
                    Dim timbrarFac As New WSTFD
                    Dim timbreFac As New RespuestaTFD

                    'timbreFac = timbrarFac.TimbrarCFDI("DEMO020730PQ5", "vtoGCoHYRd=", Xml, "fsmx_" + comp.folio.ToString)
                    timbreFac = timbrarFac.TimbrarCFDI("FMD020730PQ5", "VrGwKXeePbE#", Xml, "fsm_" + comp.folio.ToString)
                    Xml = timbreFac.XMLResultado
                    If timbreFac.MensajeError.Trim = "" And timbreFac.OperacionExitosa = True And timbreFac.OperacionExitosaSpecified = True Then
                        clase.grabaDatos(Sgraba + "update abonos set facturado='true', num_factura='" + auxNfac + "', serie ='" + txtNserie.Text.Trim + "' " & _
                        "where " + lblCondiciones.Text + "; update consecutivoFac set consecutivo='" + txtNfac.Text.Trim + "'; " & _
                        " update agenda set dtfinal='" + String.Format("{0:dd/MM/yyyy}", Date.Now()) + "' where " + lblrecibos.Text.Trim + ";")
                        CFDIv32.Validar(Xml, True)
                        uuid = timbreFac.Timbre.UUID
                        sellocfd = timbreFac.Timbre.SelloCFD
                        nocertificadoSat = timbreFac.Timbre.NumeroCertificadoSAT
                        selloSat = timbreFac.Timbre.SelloSAT
                        FechaTimbrado = timbreFac.Timbre.FechaTimbrado
                    Else
                        banderaError = True
                        Messagebox1.ShowMessage("No cerrar; Error en el xml de timbrado favor de llamar a informatica; " + timbreFac.MensajeError.ToString.Trim)
                    End If
                Catch ex As Exception
                    banderaError = True
                    Messagebox1.ShowMessage("Error en el timbrado favor de llamar a informatica " + ex.Message.ToString)
                End Try
            End If

            Dim nameFile As String = ""
            If banderaError = False Then
                Try
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(Xml)
                    nameFile = "c:\reporteFisio\fisioSM\fsm_" + comp.folio.ToString + ".xml"
                    doc.Save(nameFile)
                Catch ex As Exception
                    banderaError = True
                    Messagebox1.ShowMessage("Error en el guardado del xml favor de llamar a informatica")
                End Try
            End If

            If banderaError = False Then
                Try
                    Dim cadenaOriComplemento As String = CFDIv32.CadenaOriginalImpresa(Xml)

                    'Dim xmlDoc As New XmlDocument
                    'Dim NodeList As XmlNodeList
                    'Dim Node As XmlNode
                    'Dim bandera As Boolean = True
                    'Dim i As Integer = 0

                    'xmlDoc.Load(nameFile)
                    'NodeList = xmlDoc.ChildNodes
                    'Do While bandera
                    'If NodeList.Item(i).LocalName = "Comprobante" Then
                    'NodeList = NodeList.Item(i).ChildNodes
                    'bandera = False
                    'End If
                    'i = i + 1
                    'Loop
                    'i = 0
                    'bandera = True

                    'Do While bandera

                    'If NodeList.Item(i).LocalName = "Complemento" Then
                    'NodeList = NodeList.Item(i).ChildNodes
                    'bandera = False
                    'End If
                    'i = i + 1
                    'Loop
                    'i = 0

                    'Node = NodeList.Item(0)

                    'Do While i < Node.Attributes.Count
                    'Select Case Node.Attributes.Item(i).LocalName
                    '    Case "UUID"
                    'uuid = Node.Attributes.Item(i).Value.ToString
                    '   Case "selloCFD"
                    'sellocfd = Node.Attributes.Item(i).Value.ToString
                    '    Case "noCertificadoSAT"
                    'nocertificadoSat = Node.Attributes.Item(i).Value.ToString
                    '    Case "selloSAT"
                    'selloSat = Node.Attributes.Item(i).Value.ToString
                    '    Case "FechaTimbrado"
                    'FechaTimbrado = Node.Attributes.Item(i).Value.ToString
                    'End Select
                    'i = i + 1
                    'Loop
                    '*********
                    Dim QRCodeEncoder As New QRCodeEncoder
                    QRCodeEncoder.QRCodeScale = "3"
                    QRCodeEncoder.QRCodeVersion = "7"
                    QRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M

                    Dim imagen As Bitmap
                    Dim auxcad = Split(ftxttotal.Text.ToString.Trim, ".")
                    Dim enteros As String = auxcad(0).ToString.Trim.PadLeft(10, "0")
                    Dim decimales As String = auxcad(1).ToString.Trim.PadRight(6, "0")
                    Dim datos As String = "?re=" + emisor.rfc.Trim + "&rr=" + receptor.rfc.Trim + "&tt=" + enteros + "." + decimales + "&id=" + uuid.Trim
                    'Dim datos As String = "hola mundo"
                    imagen = QRCodeEncoder.Encode(datos)
                    imagen.Save("C:\reporteFisio\archivosFac\RQsm\qrFac" + txtNfac.Text.ToString.Trim + ".jpeg", Imaging.ImageFormat.Jpeg)
                    funciones.grabaDatos("update factura set uuid='" + uuid + "',selloCfD='" + sellocfd + "', noCertificadoSAT='" + nocertificadoSat + "'," & _
                    "selloSAT='" + selloSat + "',fechaTimbrado='" + FechaTimbrado + "' where num_factura='" + txtNfac.Text.Trim + "' and serie='C'")
                    imprimirFactura(txtNfac.Text.Trim, FechaTimbrado, comp.noCertificado.ToString, nocertificadoSat, uuid, cadenaOriComplemento, selloSat, sellocfd, receptor.domicilio.calle.ToString + " " + receptor.domicilio.noExterior.ToString.Trim, receptor.domicilio.colonia, receptor.domicilio.municipio, receptor.domicilio.codigoPostal, receptor.domicilio.localidad, receptor.domicilio.estado)
                Catch ex As Exception
                    Messagebox1.ShowMessage("Error en la impresion del pdf llamar a informatica" + ex.Message.ToString)
                End Try
            End If
        End If
        Return banderaError
    End Function
    Sub imprimirFactura(ByVal queimprimo As String, ByVal fechacer As String, ByVal ceremisor As String, ByVal cersat As String, ByVal uuid As String, ByVal cadoriginal As String, ByVal sellosat As String, ByVal selloCFD As String, ByVal domicilio As String, ByVal colonia As String, ByVal municipio As String, ByVal cp As String, ByVal localidad As String, ByVal estado As String)
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
        mireporte.Load("c:\reporteFisio\facturaFisioSM_CFDI.rpt")
        Dim conexion As SqlConnection = funciones.conecta
        Dim cgrupo As String = ""
        With visor_reporte
            .DisplayGroupTree = True
            .HasExportButton = False
            .HasPrintButton = True
            .DisplayGroupTree = False
            .HasToggleGroupTreeButton = False
            .HasZoomFactorList = True
            .HasViewList = False
            .DisplayToolbar = True
        End With

        '++++++++++++++++++++
        Dim dt As New DataTable
        Dim columna As New DataColumn("cantidad")
        dt.Columns.Add(columna)
        Dim col2 As New DataColumn("descripcion")
        dt.Columns.Add(col2)
        Dim col3 As New DataColumn("importe")
        dt.Columns.Add(col3)
        Dim col4 As New DataColumn("precio")
        dt.Columns.Add(col4)
        'Dim col5 As New DataColumn("img", System.Type.GetType(Of Byte))
        dt.Columns.Add(New DataColumn("img", GetType(Byte())))
        Dim col5 As New DataColumn("observacion")
        dt.Columns.Add(col5)

        Dim fs As FileStream = New FileStream("C:\reporteFisio\archivosFac\RQsm\qrFac" + txtNfac.Text.ToString.Trim + ".jpeg", FileMode.Open)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim imagen(CInt(fs.Length)) As Byte

        br.Read(imagen, 0, CInt(fs.Length))
        br.Close()
        fs.Close()

        Dim cReg As Integer = gridDetalles.Items.Count
        Dim cuenta As Integer = 0
        Do While cuenta < cReg
            If CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Trim <> "" Then
                Dim nFila As DataRow
                nFila = dt.NewRow
                nFila(0) = CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim
                nFila(1) = CType(gridDetalles.Items(cuenta).Cells(1).Controls(1), TextBox).Text.Trim
                nFila(2) = CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Trim
                nFila(3) = Format(CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Trim / CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim, "###,###,###0.00")
                nFila(4) = imagen
                'gridDetalles.Items(cuenta).Cells(5).Text
                If gridDetalles.Items(cuenta).Cells(7).Text.Trim <> "&nbsp;" Then
                    nFila(5) = gridDetalles.Items(cuenta).Cells(7).Text.Trim
                Else
                    nFila(5) = ""
                End If
                dt.Rows.Add(nFila)
            End If
            cuenta = cuenta + 1
        Loop
        mireporte.SetDataSource(dt.DefaultView)

        '--------------datos facturacion
        Mivar.Value = ftxtnombre.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("nombre").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = domicilio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("domicilio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = colonia
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("colonia").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = municipio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("municipio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cp
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cp").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = localidad
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("localidad").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = estado
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("estado").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtrfc.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("rfc").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtpaciente.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()
        '----------------datos de la factura
        Mivar.Value = txtNfac.Text.Trim + " " + txtNserie.Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("recibo").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cmbaño.SelectedItem.Value.ToString.Trim + "-" + cmbmes.SelectedItem.Value + "-" + ftxtdia.Text.Trim.PadLeft(2, "0") + "T00:00:00"
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cmbFormaPago.Items(cmbFormaPago.SelectedIndex).Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("formapago").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cmbClaveMP.Items(cmbClaveMP.SelectedIndex).Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ClaveMP").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtnumcuenta.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("numcuenta").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()
        '-------------------------importes
        Mivar.Value = ftxtimporte.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("importe").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtiva.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("iva").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtimporte.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtletras.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cantidadletras").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        If chkImp.Checked Then
            Mivar.Value = TXTmiobserva.Text.Trim
        Else
            Mivar.Value = ""
        End If
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("observaciones").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = fechacer
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fechacer").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ceremisor
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ceremisor").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cersat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cersat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = uuid
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("uuid").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cadoriginal
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cadenaoriginal").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = sellosat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("sellosat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = selloCFD
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("selloCFD").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '--------------------------------------------------

        visor_reporte.DataBind()
        visor_reporte.ReportSource = mireporte
        visor_reporte.PrintMode = PrintMode.Pdf

        'exporta el rpt a pdf
        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim o As CrystalDecisions.Shared.ExportOptions
        o = New CrystalDecisions.Shared.ExportOptions
        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        filedest.DiskFileName = "c:\reporteFisio\fisioSM\fsm_" + queimprimo.Trim + ".pdf"
        o.ExportDestinationOptions = filedest.Clone
        mireporte.Export(o)
        filedest = Nothing
        o = Nothing

        'se abre el pdf en acrobat
        Dim Nombre As String
        Nombre = "c:\reporteFisio\FisioSM\fsm_" + queimprimo.Trim + ".pdf"
        Dim xmlArchivo As String = "c:\reporteFisio\FisioSM\fsm_" + queimprimo.Trim + ".xml"
        If txtmail.Text.Trim <> "" And chkMail.Checked = True Then
            Try
                lasfunciones.enviaCorreoPdf(txtmail.Text, Nombre, xmlArchivo, "Factura", "Envio de su factura ....")
                funciones.grabaDatos("update clientes set email='" + txtmail.Text.Trim + "' where idcliente='" + gridDetalles.Items(0).Cells(8).Text + "'")
            Catch
            End Try
        End If

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
        Response.WriteFile(Nombre)
        Response.Flush()
        Response.Close()

    End Sub
    Function validaCosecutivoFechas() As Boolean
        Dim contando As Integer = 0
        Dim bandera As Boolean = True
        Dim fecha1 As Date
        Do While contando < gridFacturas.Items.Count And bandera
            fecha1 = gridFacturas.Items(contando).Cells(3).Text
            If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = False Then
                Dim cuenta2 As Integer = 0
                Dim fecha2 As Date
                Do While cuenta2 < gridFacturas.Items.Count And bandera
                    If CType(gridFacturas.Items(cuenta2).Cells(6).Controls(1), CheckBox).Checked = True Then
                        fecha2 = gridFacturas.Items(cuenta2).Cells(3).Text
                        If fecha2 > fecha1 And CType(gridFacturas.Items(cuenta2).Cells(8).Controls(1), CheckBox).Checked = False Then
                            bandera = False
                        End If
                    End If
                    cuenta2 = cuenta2 + 1
                Loop
            End If
            contando = contando + 1
        Loop
        Return bandera
    End Function
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente,elnombre from clientes where elnombre<>' ' order by elnombre")
            cmbFormaPago = funciones.llenacombos(cmbFormaPago, "select idpago,descripcion from catPagos where activo = 'true' ")
            cmbClaveMP = funciones.llenacombos(cmbClaveMP, "select idpago,clave from catPagos order by clave")
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
            Catch

            End Try
        End If
    End Sub
    Sub llenaDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idpago,clave from catpagos where idpago='" + cmbFormaPago.SelectedValue.ToString.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            Dim idclave As String = leer.GetValue(0).ToString.Trim
            If idclave = "" Then
                idclave = "0"
            End If
            cmbClaveMP.SelectedValue = idclave

            cmbClaveMP.Enabled = False
        End If
        leer.Close()
        conexion.Close()
        conexion.Dispose()
    End Sub
    Protected Sub cmbFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFormaPago.SelectedIndexChanged
        If cmbFormaPago.SelectedValue = "0" Then

            cmbClaveMP.SelectedValue = 0
        Else
            llenaDatos()
        End If
    End Sub
    Protected Sub btnfacturar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfacturar.Click
        Dim cuenta As Integer = gridFacturas.Items.Count
        Dim contando As Integer = 0
        Dim cuentaChecks As Int16 = 0
        Dim banderaaux As Boolean
        Dim banderaValida As Boolean = True
        Dim auxobse As Date
        Dim cadcompara As String = ""
        Dim misfun As New miclases
        lblCondiciones.Text = ""
        txtNfac.Text = ""
        txtpaciente.Text = ""
        TXTmiobserva.Text = ""
        lblrecibos.Text = ""
        If validaCosecutivoFechas() = False Or True Then
            Do While contando < cuenta
                If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = True Then
                    lblCondiciones.Text = lblCondiciones.Text + " idabono='" + gridFacturas.DataKeys.Item(contando).ToString + "' OR"
                    If misfun.lookup(gridFacturas.Items(contando).Cells(0).Text, cadcompara, ",") = 0 Then
                        cadcompara = cadcompara + gridFacturas.Items(contando).Cells(0).Text + ","
                        lblrecibos.Text = lblrecibos.Text + " idCita='" + gridFacturas.Items(contando).Cells(0).Text + "' OR"
                    End If
                    If TXTmiobserva.Text.Trim = "" Then
                        auxobse = gridFacturas.Items(contando).Cells(3).Text
                        TXTmiobserva.Text = gridFacturas.Items(contando).Cells(3).Text
                    End If
                    If CDate(gridFacturas.Items(contando).Cells(3).Text) > auxobse Then
                        TXTmiobserva.Text = auxobse.ToString.Trim
                        auxobse = gridFacturas.Items(contando).Cells(3).Text
                    End If
                    '' valida que no se mezclen coaseguradoras con aseguradoras
                    If cuentaChecks = 0 Then
                        banderaaux = CType(gridFacturas.Items(contando).Cells(8).Controls(1), CheckBox).Checked
                        cuentaChecks = cuentaChecks + 1
                    Else
                        If banderaaux <> CType(gridFacturas.Items(contando).Cells(8).Controls(1), CheckBox).Checked Then
                            banderaValida = False
                        End If
                    End If
                    '''''
                End If
                contando = contando + 1
            Loop
            If banderaValida = True Then
                Dim consulta As String = ""
                If lblCondiciones.Text.Trim <> "" Then
                    TXTmiobserva.Text = "TERAPIAS REALIZADAS DEL " + Left(TXTmiobserva.Text.Trim, 10) + " AL " + Left(auxobse.ToString.Trim, 10)
                    divOpciones.Visible = False
                    lafactura.Visible = True
                    lblCondiciones.Text = Left(lblCondiciones.Text, Len(lblCondiciones.Text) - 2)
                    lblrecibos.Text = Left(lblrecibos.Text, Len(lblrecibos.Text) - 2)

                    ftxtdia.Text = Now.Day.ToString
                    cmbmes.SelectedValue = Now.Month.ToString.PadLeft(2, "0")
                    cmbaño.SelectedValue = Now.Year.ToString.Trim

                    Dim resultados(,) As String
                    resultados = funciones.leerValores("SELECT CAST(consecutivo + 1 AS varchar) as num, serie  from consecutivoFac", 2)
                    txtNfac.Text = resultados(0, 0)
                    txtNserie.Text = resultados(1, 0)

                    consulta = "select elnombre,(calle+' '+noext+' '+colonia+' '+cp+' '+municipio+' '+ciudad+' '+edo+' '+pais) as domicilio,temp.cantidad,catservicios.descripcion," & _
                    "temp.importe-temp.monto as importe,temp.idcosto,temp.idcliente, temp.observacion from " & _
                    "(SELECT  idcliente,sum(importe) as importe, sum(monto) as monto, count(idCosto)as cantidad,idcosto,observacion FROM  abonos " & _
                    "WHERE (abonos.idCliente = '" + cmbclientes.SelectedValue.Trim + "') AND " + lblCondiciones.Text + " group by idcliente,idcosto,observacion) as temp " & _
                    "left join clientes on clientes.idcliente=temp.idcliente " & _
                    "left join catcostos on catcostos.idcosto=temp.idcosto " & _
                    "left join catservicios on catservicios.id_servicio=catcostos.id_servicio "
                    gridDetalles = funciones.creadataset(consulta, gridDetalles)
                    lbltempclie.Text = gridDetalles.Items(0).Cells(6).Text
                    txtpaciente.Text = gridDetalles.Items(0).Cells(3).Text

                    resultados = funciones.leerValores("select modfac,email,iddatosfac,razonsocial,rfc,(calle+' '+noext+' '+" & _
                    "colonia+' '+cp+' '+municipio+' '+ciudad+' '+edo+' '+pais) as domicilio,ciudad,formadepago,noCuenta from clientes where idCliente='" + lbltempclie.Text + "'", 9)
                    hfModFac.Value = resultados(0, 0).Trim
                    txtmail.Text = resultados(1, 0).ToLower.Trim
                    hfidDatosFac.Value = resultados(2, 0).Trim

                    Dim cuentaF As Int16 = gridDetalles.Items.Count
                    Dim var As Int16 = 0
                    ftxtimporte.Text = 0.0
                    Do While var < cuentaF
                        CType(gridDetalles.Items(var).Cells(2).Controls(1), TextBox).Text = Format(Convert.ToDouble(CType(gridDetalles.Items(var).Cells(2).Controls(1), TextBox).Text), "###,###,###0.00")
                        ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(CType(gridDetalles.Items(var).Cells(2).Controls(1), TextBox).Text), "###,###,###0.00")
                        CType(gridDetalles.Items(var).Cells(2).Controls(1), TextBox).Enabled = hfModFac.Value
                        CType(gridDetalles.Items(var).Cells(1).Controls(1), TextBox).Enabled = hfModFac.Value
                        gridDetalles.Items(var).Cells(5).Text = CType(gridDetalles.Items(var).Cells(2).Controls(1), TextBox).Text.Trim / CType(gridDetalles.Items(var).Cells(0).Controls(1), TextBox).Text.Trim
                        var = var + 1
                    Loop
                    ftxttotal.Text = ftxtimporte.Text.Trim
                    hfValFacturar.Value = ftxttotal.Text.Trim
                    ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))

                    If hfidDatosFac.Value = "" Or hfidDatosFac.Value = "0" Then
                        ftxtnombre.Text = resultados(3, 0).Trim
                        ftxtrfc.Text = resultados(4, 0).Trim
                        If ftxtrfc.Text.Trim = "" Then
                            ftxtrfc.Text = "XAXX010101000"
                        End If
                        ftxtdomicilio.Text = resultados(5, 0).Trim
                        ftxtciudad.Text = resultados(6, 0).Trim
                        cmbFormaPago.SelectedValue = resultados(7, 0).Trim
                        txtnumcuenta.Text = resultados(8, 0).Trim
                    Else
                        resultados = funciones.leerValores("select nombre,(calle+' '+noext+' '+colonia+' '+cp+' '+municipio+' '+ciudad+' '+edo+' '+pais) as direccion,rfc,ciudad,formadepago,nocuenta from catfacturas where iddatosfac='" + hfidDatosFac.Value + "'", 6)
                        ftxtnombre.Text = resultados(0, 0)
                        ftxtdomicilio.Text = resultados(1, 0)
                        ftxtrfc.Text = resultados(2, 0)
                        If ftxtrfc.Text.Trim = "" Then
                            ftxtrfc.Text = "XAXX010101000"
                        End If
                        ftxtciudad.Text = resultados(3, 0)
                        cmbFormaPago.SelectedValue = resultados(4, 0)
                        txtnumcuenta.Text = resultados(5, 0)
                    End If
                Else
                    Messagebox1.ShowMessage("FAVOR DE SELECCIONAR LO QUE SE VA A FACTURAR")
                End If
            Else
                Messagebox1.ShowMessage("NO SE PUEDE FACTURAR COASEGUROS CON ASEGURADORAS")
            End If
        Else
            Messagebox1.ShowMessage("HAY ABONOS CON FECHAS ANTERIORES SIN FACTURAR")
        End If
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idpago,clave from catpagos where idpago='" + cmbFormaPago.SelectedValue.ToString.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            Dim idclave As String = leer.GetValue(0).ToString.Trim
            If idclave = "" Then
                idclave = "0"
            End If
            cmbClaveMP.SelectedValue = idclave

            cmbClaveMP.Enabled = False
        End If
        leer.Close()
        conexion.Close()
        conexion.Dispose()
    End Sub

    Protected Sub cmbclientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbclientes.SelectedIndexChanged
        If cmbclientes.SelectedValue <> "0" Then
            terapiaSinfacturar()
        End If
    End Sub

    Protected Sub btnFreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFreg.Click
        terapiaSinfacturar()
        txtmail.Text = ""
        chkMail.Checked = False
        lafactura.Visible = False
        divOpciones.Visible = True
    End Sub

    Protected Sub btnFguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFguardar.Click
        Threading.Thread.Sleep(10000)
        Dim bandera As Boolean = True
        Try
            Dim axip As Date = String.Format("{0:dd/MM/yyyy}", ftxtdia.Text.Trim.PadLeft(2, "0") + "/" + cmbmes.SelectedValue.Trim + "/" + cmbaño.SelectedValue + " 12:00:00")
        Catch ex As Exception
            bandera = False
        End Try
        Dim foliofin As Integer = funciones.leerValor("select foliofin from consecutivofac")

        If bandera = True Then
            If Convert.ToDouble(ftxttotal.Text) >= Convert.ToDouble(hfValFacturar.Value) Then
                If txtNfac.Text.Trim <> "" And txtNfac.Text.Trim <> "0" Then
                    If funciones.leerValor("select num_factura from factura where num_factura='" + txtNfac.Text.Trim + " " + txtNserie.Text.Trim + "'") = "0" Then
                        If foliofin >= txtNfac.Text.Trim Then
                            If cmbFormaPago.SelectedValue.Trim <> "0" Then
                                Dim banderaError As Boolean = generaEimprimeFactura()  '* RUTINA QUE GUARDA LA FACTURA
                                If banderaError = False Then
                                    btnFguardar.Visible = False
                                End If
                                'terapiaSinfacturar()
                                'txtmail.Text = ""
                                'chkMail.Checked = False
                                'lafactura.Visible = False
                                'divOpciones.Visible = True
                            Else
                                Messagebox1.ShowMessage("NO HA SELECCIONADO UNA FORMA DE PAGO CORRECTA, VERIFIQUELO !!!")
                            End If
                        Else
                            Messagebox1.ShowMessage("EL SELLO DE HACIENDA SE A VENCIDO FAVOR DE CONTACTAR AL ADMINISTRADOR...")
                        End If
                    Else
                        Messagebox1.ShowMessage("ESTE NUMERO DE FACTURA YA SE DIO DE ALTA")
                    End If
                Else
                    Messagebox1.ShowMessage("DEBE ESCRIBIR EL NUMERO DE FACTURA")
                End If
            Else
                Messagebox1.ShowMessage("EL IMPORTE ES MENOR AL VALOR DE LAS TERAPIAS A FACTURAR")
            End If
        Else
            Messagebox1.ShowMessage("FECHA INCORRECTA")
        End If
    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cReg As Integer = gridDetalles.Items.Count
        Dim cuenta As Integer = 0
        ftxtimporte.Text = 0
        Do While cuenta < cReg
            If CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Trim <> "" And CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim <> "" Then
                CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text = Format(CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim * gridDetalles.Items(cuenta).Cells(5).Text, "###,###,###0.00")
                ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text), "###,###,###0.00")
            End If
            cuenta = cuenta + 1
        Loop
        ftxttotal.Text = ftxtimporte.Text
        ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))
    End Sub

    Protected Sub TextBox3_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cReg As Integer = gridDetalles.Items.Count
        Dim cuenta As Integer = 0
        ftxtimporte.Text = 0

        Do While cuenta < cReg
            If CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Trim <> "" And CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim <> "" Then

                If gridDetalles.Items(cuenta).Cells(5).Text <> Convert.ToDouble(CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Replace(",", "") / Convert.ToDouble(CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim)) Then
                    gridDetalles.Items(cuenta).Cells(5).Text = CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text.Replace(",", "")
                End If
                CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text = Format(CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim * gridDetalles.Items(cuenta).Cells(5).Text, "###,###,###0.00")
                ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(CType(gridDetalles.Items(cuenta).Cells(2).Controls(1), TextBox).Text), "###,###,###0.00")
            End If
            cuenta = cuenta + 1
        Loop
        ftxttotal.Text = ftxtimporte.Text
        ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAsigna.Click
        Dim cuenta As Integer = gridFacturas.Items.Count
        Dim contando As Integer = 0
        lblCondiciones.Text = ""
        txtNfac.Text = ""
        txtpaciente.Text = ""
        sumimportes.Value = 0
        Do While contando < cuenta
            If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = True Then
                lblCondiciones.Text = lblCondiciones.Text + " idabono='" + gridFacturas.DataKeys.Item(contando).ToString + "' OR"
                Dim minum As String = Right(gridFacturas.Items(contando).Cells(5).Text.Trim, gridFacturas.Items(contando).Cells(5).Text.Length - 1)
                sumimportes.Value = Convert.ToDouble(sumimportes.Value) + Convert.ToDouble(minum)
            End If
            contando = contando + 1
        Loop
        If lblCondiciones.Text.Trim = "" Then
            Messagebox1.ShowMessage("FAVOR DE SELECCIONAR LO QUE SE VA A FACTURAR")
        Else
            lbltempclie.Text = gridFacturas.Items(0).Cells(7).Text
            contando = 0
            Do While contando < cuenta
                If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = False Then
                    gridFacturas.Items(contando).Visible = False
                End If
                contando = contando + 1
            Loop
            txtAserie.Text = funciones.leerValor("select serie from consecutivoFac")
            lblCondiciones.Text = Left(lblCondiciones.Text, Len(lblCondiciones.Text) - 2)
            divControles.Visible = False
            gridFacturas.Enabled = False
            pnlasigna.Visible = True
        End If
    End Sub

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        divControles.Visible = True
        pnlasigna.Visible = False
        gridFacturas.Enabled = True
        Dim cuenta As Integer = gridFacturas.Items.Count
        Dim contando As Integer = 0
        Do While contando < cuenta
            If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = False Then
                gridFacturas.Items(contando).Visible = True
            End If
            contando = contando + 1
        Loop
        txtAfac.Text = ""
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Dim auxfac As String = txtAfac.Text.Trim ' + " " + txtAserie.Text.Trim
        Dim val(,) As String
        Dim val1 As Double
        Dim val2 As Double
        Dim diferencia As Double = 0
        val = funciones.leerValores("select num_factura,sum(Importe) from factura where num_factura='" + auxfac + "' and serie='C' " & _
        " group by num_factura", 2)
        If val(0, 0) <> "0" Then
            Dim auxsumAbono As Double = funciones.leerValor("select isnull(sum(importe),0) - isnull(sum(monto),0) from abonos where num_factura='" + auxfac + "' and serie='C'")
            If sumimportes.Value + auxsumAbono <= val(1, 0) Then
                Dim resultados(,) As String
                resultados = funciones.leerValores("select idcliente,capcuenta from (select distinct num_factura," & _
                "idcliente from abonos where num_factura='" + auxfac + "') as temp inner join (select distinct num_factura," & _
                "capcuenta from factura where num_factura='" + auxfac + "' and serie='C') as temp2 on temp.num_factura=temp2.num_factura", 2)
                Try
                    'If resultados(0, 0).Trim = lbltempclie.Text.Trim Then
                    funciones.grabaDatos("update abonos set facturado='true', num_factura='" + auxfac + "', serie='" + txtAserie.Text + "' " & _
                            "where " + lblCondiciones.Text + "")
                    terapiaSinfacturar()
                    Messagebox1.ShowMessage("Datos Guardados")
                    'Else
                    'Messagebox1.ShowMessage("AL CLIENTE NO LE CORRESPONDE LA FACTURA....")
                    'End If
                Catch ex As Exception
                    funciones.grabaDatos("update abonos set facturado='true', num_factura='" + auxfac + "', serie='" + txtAserie.Text + "' " & _
                                    "where " + lblCondiciones.Text + "")
                    terapiaSinfacturar()
                    Messagebox1.ShowMessage("Datos Guardados")
                End Try

                divControles.Visible = True
                pnlasigna.Visible = False
                gridFacturas.Enabled = True
                Dim cuenta As Integer = gridFacturas.Items.Count
                Dim contando As Integer = 0
                Do While contando < cuenta
                    If CType(gridFacturas.Items(contando).Cells(6).Controls(1), CheckBox).Checked = False Then
                        gridFacturas.Items(contando).Visible = True
                    End If
                    contando = contando + 1
                Loop
                txtAfac.Text = ""

            Else
                val1 = funciones.leerValor("select sum(Importe) as importe from factura where num_factura='" + txtAfac.Text.Trim + "' and serie='C' ")
                val2 = funciones.leerValor("select isnull(sum(importe),0) as abonos from abonos where num_factura='" + txtAfac.Text.Trim + "' and serie='C'")
                diferencia = val1 - val2 - sumimportes.Value
                Messagebox1.ShowMessage("EL IMPORTE DE LA FACTURA ES MENOR A LA CANTIDAD QUE ESTAS ASIGNANDO")
            End If
        Else
            Messagebox1.ShowMessage("NO SE ENCONTRO LE NUMERO DE FACTURA....")
        End If
    End Sub

    Protected Sub LinkButton8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton8.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facgeneradas.aspx", False)
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        terapiasSinfacturar()
        cmdexcel.Visible = True
    End Sub

    Sub terapiasSinfacturar()
        If cmbclientes.SelectedValue = 0 Then
            gridFacturas = funciones.creadataset("SELECT clientes.elnombre,catServicios.descripcion," & _
            " convert(varchar(10),abonos.fecha,103) as fecha, catServicios.descripcion, " & _
            "abonos.abono, abonos.importe-monto as importe,abonos.idabono, abonos.fecha as fecha2,idcita,abonos.idcliente,coaseguro,ligaAbono FROM  " & _
            "abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where facturado='false' and importe<>0 and datepart(yyyy,abonos.fecha)>2012 order by fecha2 asc", gridFacturas)
            gridFacturas.Columns(6).Visible = False
            btnfacturar.Enabled = False
            btnAsigna.Enabled = False
        Else
            gridFacturas = funciones.creadataset("SELECT clientes.elnombre, convert(varchar(10),abonos.fecha,103) as fecha, catServicios.descripcion, " & _
            "abonos.abono, abonos.importe-monto as importe,abonos.idabono, abonos.fecha as fecha2,idcita,abonos.idcliente,coaseguro,ligaAbono " & _
            " FROM  abonos left JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto left JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where abonos.facturado='false' and importe<>0 and datepart(yyyy,abonos.fecha)>2012 and abonos.idcliente='" + cmbclientes.SelectedValue.Trim + "' " & _
            "order by fecha2 asc", gridFacturas)
            gridFacturas.Columns(6).Visible = True
        End If
    End Sub

    Protected Sub Page_Unload(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Unload
        mireporte.Close()
        mireporte.Dispose()
    End Sub
    Protected Sub cmdexcel_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcel.Click
        ExportarAExcel()
    End Sub
    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gridFacturas.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gridFacturas)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename= TerapiasSf .xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
End Class
