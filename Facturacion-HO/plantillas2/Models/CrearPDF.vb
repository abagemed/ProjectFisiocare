Imports System.Text
Imports System.Threading.Tasks

Imports iTextSharp
Imports iTextSharp.text
Imports iTextSharp.text.pdf
Imports System.IO

Imports System.Xml
Imports System.Globalization

Public Class CreaPDF
    Private _documento As Document
    'Objeto para escribir el pdf
    Private _fuenteTitulos As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA_BOLD, BaseFont.CP1250, BaseFont.NOT_EMBEDDED)
    Private _fuenteContenido As BaseFont = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1250, BaseFont.NOT_EMBEDDED)
    Private _writer As PdfWriter
    Private _cb As PdfContentByte
    Private _comprobante As Comprobante
    'Objeto que contendra la información del documento pdf
    Private xDoc As XmlDocument
    ' Objeto para abrir el archivo xml
    Private _fTitulo As Font
    Private _fSubtitulo As Font
    Private _FNormal As Font
    Private _FNormalBold As Font
    Private _fMediana As Font

    Public Sub New(rutaXML As String, rutaPDF As String, logo As System.Drawing.Image, Npaciente As String, dobservaciones As String)
        'CreateRootAndChildren();

        _fTitulo = New Font(Font.FontFamily.HELVETICA, 10, Font.BOLD)
        _fSubtitulo = New Font(Font.FontFamily.HELVETICA, 9, Font.BOLD)
        _FNormal = New Font(Font.FontFamily.HELVETICA, 8)
        _FNormalBold = New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)
        _fMediana = New Font(Font.FontFamily.HELVETICA, 7)

        LeerArtributosXML(rutaXML)

        'Trabajos con el documento XML
        _documento = New Document(PageSize.LETTER)
        'string nombreDocumento = Path.GetTempFileName() + ".pdf";
        Dim nombreDocumento As String = rutaPDF

        'Creamos el documetno
        _writer = PdfWriter.GetInstance(_documento, New FileStream(nombreDocumento, FileMode.Create))
        _writer.PageEvent = New ITextEvents()

        'Agregamos propiedades al documento
        AgregaPropiedadesDocumento()

        'Abrimos el documento
        _documento.Open()

        If ObtenerTipoComprobante() = "N" Then
            NAgregarTitulo()
            NAgregarTabla(logo)
            NAgregarPercepcionesDeducciones()
            NAgregarMensaje()
            NAgregarDatosFiscales()
        Else


            AgregarLogo(logo)
            AgregarCuadro()
            AgregarDatosEmisorReceptor()
            Agregarpaciente(Npaciente)
            AgregarDatosFactura()
            AgregarCfdiRelacionados()
            'AgregarDatosReceptorEmisor();
            AgregarPagos10()
            AgregarDatosProductos()
            AgregarTotales()
            Agregarobservaciones(dobservaciones)
            AgregarSellos()
        End If
        'Cerramoe el documento
        _documento.Close()

        'Abrimos el archivo .pdf
        'System.Diagnostics.Process.Start(nombreDocumento)
    End Sub



#Region "Leer datos del .xml"

    Private Sub LeerArtributosXML(rutaXML As String)
        'string TipoComprobante = string.Empty;

        xDoc = New XmlDocument()
        'Instancia documento pdf
        _comprobante = New Comprobante()
        'Instancia que contendrá la información para llenar el pdf
        xDoc.Load(rutaXML)

        'TipoComprobante = ObtenerTipoComprobante();  //I - Ingreso; T - Traslado; E - Egreso; N - Nomina; P - Pago

        'if (TipoComprobante == "N")

        ObtenerNodoCfdiComprobante()
        ObtenerNodoCfdisRelacionados()
        ObtenerNodoEmisor()
        ObtenerNodoReceptor()
        ObtenerNodoConceptos()
        ObtenerNodoComplementoDigital()
        ObtenerNodoImpuestos()
        ObtenerNodoPagos()
        'Pagos
        ObtenerNodoNomina()

    End Sub

    Private Function ObtenerTipoComprobante() As String
        Dim TipoComprobante As String = String.Empty

        If xDoc.GetElementsByTagName("cfdi:Comprobante") Is Nothing Then
            Return String.Empty
        End If
        Dim comprobante As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Comprobante")
        If DirectCast(comprobante(0), XmlElement).GetAttribute("TipoDeComprobante") IsNot Nothing Then
            TipoComprobante = DirectCast(comprobante(0), XmlElement).GetAttribute("TipoDeComprobante")
        End If

        Return TipoComprobante
    End Function

    Private Sub ObtenerNodoCfdiComprobante()
        Dim valFloat As Single
        If xDoc.GetElementsByTagName("cfdi:Comprobante") Is Nothing Then
            Return
        End If

        Dim comprobante As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Comprobante")
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Version") IsNot Nothing Then
            _comprobante.Version = DirectCast(comprobante(0), XmlElement).GetAttribute("Version")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Serie") IsNot Nothing Then
            _comprobante.Serie = DirectCast(comprobante(0), XmlElement).GetAttribute("Serie")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Folio") IsNot Nothing Then
            _comprobante.Folio = DirectCast(comprobante(0), XmlElement).GetAttribute("Folio")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Fecha") IsNot Nothing Then
            _comprobante.Fecha = DirectCast(comprobante(0), XmlElement).GetAttribute("Fecha")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Sello") IsNot Nothing Then
            _comprobante.Sello = DirectCast(comprobante(0), XmlElement).GetAttribute("Sello")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("FormaPago") IsNot Nothing Then
            _comprobante.FormaPago = DirectCast(comprobante(0), XmlElement).GetAttribute("FormaPago")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("NoCertificado") IsNot Nothing Then
            _comprobante.NoCertificado = DirectCast(comprobante(0), XmlElement).GetAttribute("NoCertificado")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Certificado") IsNot Nothing Then
            _comprobante.Certificado = DirectCast(comprobante(0), XmlElement).GetAttribute("Certificado")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("CondicionesDePago") IsNot Nothing Then
            _comprobante.CondicionesDePago = DirectCast(comprobante(0), XmlElement).GetAttribute("CondicionesDePago")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("SubTotal") IsNot Nothing Then
            _comprobante.Subtotal = toDecimal(DirectCast(comprobante(0), XmlElement).GetAttribute("SubTotal"))
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Descuento") IsNot Nothing Then
            _comprobante.Descuento = toDecimal(DirectCast(comprobante(0), XmlElement).GetAttribute("Descuento"))
        End If

        If DirectCast(comprobante(0), XmlElement).GetAttribute("Moneda") IsNot Nothing Then
            _comprobante.Moneda = DirectCast(comprobante(0), XmlElement).GetAttribute("Moneda")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("TipoCambio") IsNot Nothing Then
            _comprobante.TipoCambio = toDecimal(DirectCast(comprobante(0), XmlElement).GetAttribute("TipoCambio"))
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Total") IsNot Nothing Then
            _comprobante.Total = toDecimal(DirectCast(comprobante(0), XmlElement).GetAttribute("Total"))

            Dim numaLet As New Numalet()
            Dim mascara As String = "00/100 " + _comprobante.Moneda
            numaLet.MascaraSalidaDecimal = mascara
            numaLet.SeparadorDecimalSalida = "pesos"
            numaLet.ApocoparUnoParteEntera = True
            numaLet.LetraCapital = True
            _comprobante.TotalLetra = numaLet.ToCustomString(_comprobante.Total)
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("TipoDeComprobante") IsNot Nothing Then
            _comprobante.TipoDeComprobante = DirectCast(comprobante(0), XmlElement).GetAttribute("TipoDeComprobante")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("MetodoPago") IsNot Nothing Then
            _comprobante.MetodoPago = DirectCast(comprobante(0), XmlElement).GetAttribute("MetodoPago")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("LugarExpedicion") IsNot Nothing Then
            _comprobante.LugarExpedicion = DirectCast(comprobante(0), XmlElement).GetAttribute("LugarExpedicion")
        End If
        If DirectCast(comprobante(0), XmlElement).GetAttribute("Confirmacion") IsNot Nothing Then
            _comprobante.Confirmacion = DirectCast(comprobante(0), XmlElement).GetAttribute("Confirmacion")
        End If
    End Sub

    Private Sub ObtenerNodoCfdisRelacionados()
        If xDoc.GetElementsByTagName("cfdi:CfdiRelacionados") Is Nothing OrElse xDoc.GetElementsByTagName("cfdi:CfdiRelacionados").Count = 0 Then
            Return
        End If
        Dim CfdiRelacionados As XmlNodeList = xDoc.GetElementsByTagName("cfdi:CfdiRelacionados")
        If DirectCast(CfdiRelacionados(0), XmlElement).GetAttribute("TipoRelacion") IsNot Nothing Then
            _comprobante.CfdiRelacionados.TipoRelacion = DirectCast(CfdiRelacionados(0), XmlElement).GetAttribute("TipoRelacion")
        End If
        If DirectCast(CfdiRelacionados(0), XmlElement).GetElementsByTagName("cfdi:Relacionado") Is Nothing Then
            Return
        End If
        Dim ListaCfdiRelacionados As XmlNodeList = DirectCast(CfdiRelacionados(0), XmlElement).GetElementsByTagName("cfdi:CfdiRelacionado")
        For Each nodo As XmlElement In ListaCfdiRelacionados
            Dim c As New CfdiRelacionado()
            If nodo.GetAttribute("UUID") IsNot Nothing Then
                c.UUID = nodo.GetAttribute("UUID")
            End If
            _comprobante.CfdiRelacionados.CfdiRelacionado.Add(c)
        Next

    End Sub

    Private Sub ObtenerNodoEmisor()
        'Trabajamos con Emisor
        If xDoc.GetElementsByTagName("cfdi:Emisor") Is Nothing Then
            Return
        End If
        Dim emisor As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Emisor")
        If DirectCast(emisor(0), XmlElement).GetAttribute("Rfc") IsNot Nothing Then
            _comprobante.Emisor.Rfc = DirectCast(emisor(0), XmlElement).GetAttribute("Rfc")
        End If
        If DirectCast(emisor(0), XmlElement).GetAttribute("Nombre") IsNot Nothing Then
            _comprobante.Emisor.Nombre = DirectCast(emisor(0), XmlElement).GetAttribute("Nombre")
        End If
        If DirectCast(emisor(0), XmlElement).GetAttribute("RegimenFiscal") IsNot Nothing Then
            _comprobante.Emisor.RegimenFiscal = DirectCast(emisor(0), XmlElement).GetAttribute("RegimenFiscal")
        End If
    End Sub
    'AB SAT 4.0
    Private Sub ObtenerNodoReceptor()
        'Trabajamos con receptor
        If xDoc.GetElementsByTagName("cfdi:Receptor") Is Nothing Then
            Return
        End If
        Dim receptor As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Receptor")
        If DirectCast(receptor(0), XmlElement).GetAttribute("Rfc") IsNot Nothing Then
            _comprobante.Receptor.Rfc = DirectCast(receptor(0), XmlElement).GetAttribute("Rfc")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("Nombre") IsNot Nothing Then
            _comprobante.Receptor.Nombre = DirectCast(receptor(0), XmlElement).GetAttribute("Nombre")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("ResidenciaFiscal") IsNot Nothing Then
            _comprobante.Receptor.ResidenciaFiscal = DirectCast(receptor(0), XmlElement).GetAttribute("ResidenciaFiscal")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("NumRegIdTrib") IsNot Nothing Then
            _comprobante.Receptor.NumRegIdTrib = DirectCast(receptor(0), XmlElement).GetAttribute("NumRegIdTrib")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("UsoCFDI") IsNot Nothing Then
            _comprobante.Receptor.UsoCFDI = DirectCast(receptor(0), XmlElement).GetAttribute("UsoCFDI")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("RegimenFiscalReceptor") IsNot Nothing Then
            _comprobante.Receptor.RegimenFiscalReceptor = DirectCast(receptor(0), XmlElement).GetAttribute("RegimenFiscalReceptor")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("DomicilioFiscalReceptor") IsNot Nothing Then
            _comprobante.Receptor.DomicilioFiscalReceptor = DirectCast(receptor(0), XmlElement).GetAttribute("DomicilioFiscalReceptor")
        End If
    End Sub

    Private Sub ObtenerNodoConceptos()
        Dim valFloat As Single
        Dim c As Concepto

        If xDoc.GetElementsByTagName("cfdi:Conceptos") Is Nothing Then
            Return
        End If
        Dim conceptos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Conceptos")
        If DirectCast(conceptos(0), XmlElement).GetElementsByTagName("cfdi:Concepto") Is Nothing Then
            Return
        End If
        Dim lista As XmlNodeList = DirectCast(conceptos(0), XmlElement).GetElementsByTagName("cfdi:Concepto")

        For Each nodo As XmlElement In lista
            c = New Concepto()
            If nodo.GetAttribute("ClaveProdServ") IsNot Nothing Then
                c.ClaveProdServ = nodo.GetAttribute("ClaveProdServ")
            End If
            If nodo.GetAttribute("NoIdentificacion") IsNot Nothing Then
                c.NoIdentificacion = nodo.GetAttribute("NoIdentificacion")
            End If
            If nodo.GetAttribute("Cantidad") IsNot Nothing Then
                c.Cantidad = toDecimal(nodo.GetAttribute("Cantidad"))
            End If
            If nodo.GetAttribute("ClaveUnidad") IsNot Nothing Then
                c.ClaveUnidad = nodo.GetAttribute("ClaveUnidad")
            End If
            If nodo.GetAttribute("Unidad") IsNot Nothing Then
                c.Unidad = nodo.GetAttribute("Unidad")
            End If
            If nodo.GetAttribute("Descripcion") IsNot Nothing Then
                c.Descripcion = nodo.GetAttribute("Descripcion")
            End If
            If nodo.GetAttribute("ValorUnitario") IsNot Nothing Then
                c.ValorUnitario = toDecimal(nodo.GetAttribute("ValorUnitario"))
            End If
            If nodo.GetAttribute("Importe") IsNot Nothing Then
                c.Importe = toDecimal(nodo.GetAttribute("Importe"))
            End If
            If nodo.GetAttribute("Descuento") IsNot Nothing Then
                c.Descuento = toDecimal(nodo.GetAttribute("Descuento"))
            End If

            Dim lTraslados As New List(Of TrasladoC)()
            Dim lRetenciones As New List(Of RetencionC)()

            If nodo.GetElementsByTagName("cfdi:Impuestos") IsNot Nothing Then
                Dim impuestos As XmlNodeList = nodo.GetElementsByTagName("cfdi:Impuestos")

                If nodo.GetElementsByTagName("cfdi:Traslados") IsNot Nothing Then
                    Dim traslados As XmlNodeList = nodo.GetElementsByTagName("cfdi:Traslados")

                    If nodo.GetElementsByTagName("cfdi:Traslado") IsNot Nothing Then
                        Dim traslado As XmlNodeList = nodo.GetElementsByTagName("cfdi:Traslado")
                        For Each t As XmlElement In traslado
                            Dim tras As New TrasladoC()
                            tras.Base = toFloat(t.GetAttribute("Base"))
                            tras.Importe = toFloat(t.GetAttribute("Importe"))
                            tras.Impuesto = t.GetAttribute("Impuesto")
                            tras.TasaOCuota = toFloat(t.GetAttribute("TasaOCuota"))
                            tras.TipoFactor = t.GetAttribute("TipoFactor")
                            lTraslados.Add(tras)
                        Next
                    End If
                End If

                If nodo.GetElementsByTagName("cfdi:Retenciones") IsNot Nothing Then
                    Dim retenciones As XmlNodeList = nodo.GetElementsByTagName("cfdi:Retenciones")

                    If nodo.GetElementsByTagName("cfdi:Retencion") IsNot Nothing Then
                        Dim retencion As XmlNodeList = nodo.GetElementsByTagName("cfdi:Retencion")
                        For Each r As XmlElement In retencion
                            Dim ret As New TrasladoC()
                            ret.Base = toFloat(r.GetAttribute("Base"))
                            ret.Importe = toFloat(r.GetAttribute("Importe"))
                            ret.Impuesto = r.GetAttribute("Impuesto")
                            ret.TasaOCuota = toFloat(r.GetAttribute("TasaOCuota"))
                            ret.TipoFactor = r.GetAttribute("TipoFactor")
                            lTraslados.Add(ret)
                        Next
                    End If
                End If
            End If

            c.Impuestos.Traslados = lTraslados
            c.Impuestos.Retenciones = lRetenciones
            _comprobante.Conceptos.Add(c)
        Next
    End Sub

    Private Sub ObtenerNodoImpuestos()
        Dim tieneimpuestos As Boolean = False
        Dim valFloat As Single
        Dim indice As Integer = 0
        If xDoc.GetElementsByTagName("cfdi:Impuestos") Is Nothing OrElse xDoc.GetElementsByTagName("cfdi:Impuestos").Count <= 0 Then
            Return
        End If
        Dim impuestos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Impuestos")

        For Each item As XmlElement In impuestos
            If item.ParentNode.Name = "cfdi:Comprobante" Then
                tieneimpuestos = True
                Exit For
            End If
            indice += 1
        Next

        If tieneimpuestos = False Then
            Return
        End If

        If DirectCast(impuestos(indice), XmlElement).ParentNode.Name <> "cfdi:Comprobante" Then
            Return
        End If
        If DirectCast(impuestos(indice), XmlElement).GetAttribute("TotalImpuestosRetenidos") IsNot Nothing OrElse DirectCast(impuestos(indice), XmlElement).GetAttribute("TotalImpuestosRetenidos").Count() > 0 Then
            _comprobante.Impuestos.TotalImpuestosRetenidos = toDecimal(DirectCast(impuestos(indice), XmlElement).GetAttribute("TotalImpuestosRetenidos"))
        End If
        If DirectCast(impuestos(indice), XmlElement).GetAttribute("TotalImpuestosTrasladados") IsNot Nothing Then
            _comprobante.Impuestos.TotalImpuestosTrasladados = toDecimal(DirectCast(impuestos(indice), XmlElement).GetAttribute("TotalImpuestosTrasladados"))
        End If

        If xDoc.GetElementsByTagName("cfdi:Retenciones") IsNot Nothing Then
            Dim retenciones As XmlNodeList = DirectCast(impuestos(indice), XmlElement).GetElementsByTagName("cfdi:Retenciones")

            If retenciones.Count > 0 Then
                Dim listaRetenciones As XmlNodeList = DirectCast(retenciones(0), XmlElement).GetElementsByTagName("cfdi:Retencion")
                For Each retencion As XmlElement In listaRetenciones
                    Dim r As New Retencion()
                    If retencion.GetAttribute("Impuesto") IsNot Nothing Then
                        r.Impuesto = retencion.GetAttribute("Impuesto")
                    End If
                    If retencion.GetAttribute("Importe") IsNot Nothing Then
                        r.Importe = toDecimal(retencion.GetAttribute("Importe"))
                    End If
                    _comprobante.Impuestos.Retenciones.Add(r)
                Next
            End If
        End If


        If xDoc.GetElementsByTagName("cfdi:Traslados") IsNot Nothing Then
            Dim traslados As XmlNodeList = DirectCast(impuestos(indice), XmlElement).GetElementsByTagName("cfdi:Traslados")
            If traslados.Count > 0 Then
                Dim listaTraslados As XmlNodeList = DirectCast(traslados(0), XmlElement).GetElementsByTagName("cfdi:Traslado")
                For Each traslado As XmlElement In listaTraslados
                    Dim t As New Traslado()
                    If traslado.GetAttribute("Impuesto") IsNot Nothing Then
                        t.Impuesto = traslado.GetAttribute("Impuesto")
                    End If
                    If traslado.GetAttribute("TipoFactor") IsNot Nothing Then
                        t.TipoFactor = traslado.GetAttribute("TipoFactor")
                    End If
                    If traslado.GetAttribute("TasaOCuota") IsNot Nothing Then
                        t.TasaOCuota = toDecimal(traslado.GetAttribute("TasaOCuota"))
                    End If
                    If traslado.GetAttribute("Importe") IsNot Nothing Then
                        t.Importe = toDecimal(traslado.GetAttribute("Importe"))
                    End If
                    _comprobante.Impuestos.Traslados.Add(t)
                Next
            End If
        End If


    End Sub

    Private Sub ObtenerNodoComplementoDigital()
        Dim tfDigital As XmlNodeList = xDoc.GetElementsByTagName("tfd:TimbreFiscalDigital")
        If tfDigital.Count <= 0 Then
            Return
        End If

        If DirectCast(tfDigital(0), XmlElement).GetAttribute("version") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.version = DirectCast(tfDigital(0), XmlElement).GetAttribute("version")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("UUID") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.UUID = DirectCast(tfDigital(0), XmlElement).GetAttribute("UUID")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("FechaTimbrado") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.FechaTimbrado = DirectCast(tfDigital(0), XmlElement).GetAttribute("FechaTimbrado")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("SelloCFD") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.SelloCFD = DirectCast(tfDigital(0), XmlElement).GetAttribute("SelloCFD")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("NoCertificadoSAT") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSAT = DirectCast(tfDigital(0), XmlElement).GetAttribute("NoCertificadoSAT")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("SelloSAT") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.SelloSAT = DirectCast(tfDigital(0), XmlElement).GetAttribute("SelloSAT")
        End If
        If DirectCast(tfDigital(0), XmlElement).GetAttribute("RfcProvCertif") IsNot Nothing Then
            _comprobante.Complemento.TimbreFiscalDigital.RfcProvCertif = DirectCast(tfDigital(0), XmlElement).GetAttribute("RfcProvCertif")
        End If
    End Sub

    Private Sub ObtenerNodoPagos()
        If xDoc.GetElementsByTagName("pago20:Pagos") Is Nothing OrElse xDoc.GetElementsByTagName("pago20:Pagos").Count <= 0 Then
            Return
        End If
        Dim pago As XmlNodeList = xDoc.GetElementsByTagName("pago20:Pagos")
        If DirectCast(pago(0), XmlElement).GetElementsByTagName("pago20:Pago") Is Nothing Then
            Return
        End If
        _comprobante.Complemento.Pagos.Version = DirectCast(pago(0), XmlElement).GetAttribute("Version")

        Dim listaPagos As XmlNodeList = DirectCast(pago(0), XmlElement).GetElementsByTagName("pago20:Pago")
        For Each nodo As XmlElement In listaPagos
            Dim p As New Pago()
            If nodo.GetAttribute("FechaPago") IsNot Nothing Then
                p.FechaPago = nodo.GetAttribute("FechaPago")
            End If
            If nodo.GetAttribute("FormaDePagoP") IsNot Nothing Then
                p.FormaDePagoP = nodo.GetAttribute("FormaDePagoP")
            End If
            If nodo.GetAttribute("MonedaP") IsNot Nothing Then
                p.MonedaP = nodo.GetAttribute("MonedaP")
            End If
            If nodo.GetAttribute("TipoCambioP") IsNot Nothing Then
                p.TipoCambioP = toDecimal(nodo.GetAttribute("TipoCambioP"))
            End If
            If nodo.GetAttribute("Monto") IsNot Nothing Then
                p.Monto = toDecimal(nodo.GetAttribute("Monto"))
            End If
            If nodo.GetAttribute("NumOperacion") IsNot Nothing Then
                p.NumOperacion = nodo.GetAttribute("NumOperacion")
            End If
            If nodo.GetAttribute("RfcEmisorCtaOrd") IsNot Nothing Then
                p.RfcEmisorCtaOrd = nodo.GetAttribute("RfcEmisorCtaOrd")
            End If
            If nodo.GetAttribute("NomBancoOrdExt") IsNot Nothing Then
                p.NomBancoOrdExt = nodo.GetAttribute("NomBancoOrdExt")
            End If
            If nodo.GetAttribute("CtaOrdenante") IsNot Nothing Then
                p.CtaOrdenante = nodo.GetAttribute("CtaOrdenante")
            End If
            If nodo.GetAttribute("RfcEmisorCtaBen") IsNot Nothing Then
                p.RfcEmisorCtaBen = nodo.GetAttribute("RfcEmisorCtaBen")
            End If
            If nodo.GetAttribute("CtaBeneficiario") IsNot Nothing Then
                p.CtaBeneficiario = nodo.GetAttribute("CtaBeneficiario")
            End If
            If nodo.GetAttribute("TipoCadPago") IsNot Nothing Then
                p.TipoCadPago = nodo.GetAttribute("TipoCadPago")
            End If
            If nodo.GetAttribute("CertPago") IsNot Nothing Then
                p.CertPago = nodo.GetAttribute("CertPago")
            End If
            If nodo.GetAttribute("CadPago") IsNot Nothing Then
                p.CadPago = nodo.GetAttribute("CadPago")
            End If
            If nodo.GetAttribute("SelloPago") IsNot Nothing Then
                p.SelloPago = nodo.GetAttribute("SelloPago")
            End If
            '#Region "DoctosRelacionados"
            If nodo.GetElementsByTagName("pago20:DoctoRelacionado") IsNot Nothing OrElse nodo.GetElementsByTagName("pago20:DoctoRelacionado").Count > 0 Then
                Dim Doctos As XmlNodeList = nodo.GetElementsByTagName("pago20:DoctoRelacionado")
                If Doctos.Count > 0 Then
                    For Each docto As XmlElement In Doctos
                        Dim dr As New DoctoRelacionado()
                        If docto.GetAttribute("IdDocumento") IsNot Nothing Then
                            dr.IdDocumento = docto.GetAttribute("IdDocumento")
                        End If
                        If docto.GetAttribute("Serie") IsNot Nothing Then
                            dr.Serie = docto.GetAttribute("Serie")
                        End If
                        If docto.GetAttribute("Folio") IsNot Nothing Then
                            dr.Folio = docto.GetAttribute("Folio")
                        End If
                        If docto.GetAttribute("MonedaDR") IsNot Nothing Then
                            dr.MonedaDR = docto.GetAttribute("MonedaDR")
                        End If
                        If docto.GetAttribute("TipoCambioDR") IsNot Nothing Then
                            dr.TipoCambioDR = docto.GetAttribute("TipoCambioDR")
                        End If
                        If docto.GetAttribute("MetodoDePagoDR") IsNot Nothing Then
                            dr.MetodoDePagoDR = docto.GetAttribute("MetodoDePagoDR")
                        End If
                        If docto.GetAttribute("NumParcialidad") IsNot Nothing Then
                            dr.NumParcialidad = docto.GetAttribute("NumParcialidad")
                        End If
                        If docto.GetAttribute("ImpSaldoAnt") IsNot Nothing Then
                            dr.ImpSaldoAnt = toDecimal(docto.GetAttribute("ImpSaldoAnt"))
                        End If
                        If docto.GetAttribute("ImpPagado") IsNot Nothing Then
                            dr.ImpPagado = toDecimal(docto.GetAttribute("ImpPagado"))
                        End If
                        If docto.GetAttribute("ImpSaldoInsoluto") IsNot Nothing Then
                            dr.ImpSaldoInsoluto = toDecimal(docto.GetAttribute("ImpSaldoInsoluto"))
                        End If
                        p.DoctoRelacionado.Add(dr)
                    Next
                End If
            End If
            '#End Region
            '#Region "Impuestos"
            If nodo.GetElementsByTagName("pago20:Impuestos") IsNot Nothing OrElse nodo.GetElementsByTagName("pago20:Impuestos").Count > 0 Then
                Dim impuestos As XmlNodeList = nodo.GetElementsByTagName("pago20:Impuestos")
                For Each impuesto As XmlElement In impuestos
                    Dim i As New Impuestos()
                    If impuesto.GetAttribute("TotalImpuestosRetenidos") IsNot Nothing Then
                        i.TotalImpuestosRetenidos = toDecimal(impuesto.GetAttribute("TotalImpuestosRetenidos"))
                    End If
                    If impuesto.GetAttribute("TotalImpuestosTrasladados") IsNot Nothing Then
                        i.TotalImpuestosRetenidos = toDecimal(impuesto.GetAttribute("TotalImpuestosTrasladados"))
                    End If
                    If impuesto.GetElementsByTagName("pago20:Retenciones") IsNot Nothing OrElse impuesto.GetElementsByTagName("pago20:Retenciones").Count > 0 Then
                        Dim ListaRetenciones As XmlNodeList = impuesto.GetElementsByTagName("pago20:Retencion")
                        For Each retencion As XmlElement In ListaRetenciones
                            Dim r As New Retencion()
                            If retencion.GetAttribute("Impuesto") IsNot Nothing Then
                                r.Impuesto = retencion.GetAttribute("Impuesto")
                            End If
                            If retencion.GetAttribute("Importe") IsNot Nothing Then
                                r.Importe = toDecimal(retencion.GetAttribute("Importe"))
                            End If
                            i.Retenciones.Add(r)
                        Next
                    End If

                    If impuesto.GetElementsByTagName("pago20:Traslados") IsNot Nothing OrElse impuesto.GetElementsByTagName("pago20:Traslados").Count > 0 Then
                        Dim listaTraslados As XmlNodeList = impuesto.GetElementsByTagName("pago20:Traslados")
                        For Each traslado As XmlElement In listaTraslados
                            Dim t As New Traslado()
                            If traslado.GetAttribute("Impuesto") IsNot Nothing Then
                                t.Impuesto = traslado.GetAttribute("Impuesto")
                            End If
                            If traslado.GetAttribute("TipoFactor") IsNot Nothing Then
                                t.TipoFactor = traslado.GetAttribute("TipoFactor")
                            End If
                            If traslado.GetAttribute("TasaOCuota") IsNot Nothing Then
                                t.TasaOCuota = toDecimal(traslado.GetAttribute("TasaOCuota"))
                            End If
                            If traslado.GetAttribute("Importe") IsNot Nothing Then
                                t.Importe = toDecimal(traslado.GetAttribute("Importe"))
                            End If
                            i.Traslados.Add(t)
                        Next
                    End If

                    'p.Impuestos.Add(i)
                Next
            End If
            '#End Region

            _comprobante.Complemento.Pagos.pagos.Add(p)
        Next
    End Sub

    Private Sub ObtenerNodoNomina()
        Dim valFloat As Single

        If xDoc.GetElementsByTagName("cfdi:Complemento") Is Nothing Then
            Return
        End If
        Dim complementos As XmlNodeList = xDoc.GetElementsByTagName("cfdi:Complemento")

        If DirectCast(complementos(0), XmlElement).GetElementsByTagName("nomina12:Nomina").Count <= 0 Then
            Return
        End If
        Dim nomina As XmlNodeList = DirectCast(complementos(0), XmlElement).GetElementsByTagName("nomina12:Nomina")

        If DirectCast(nomina(0), XmlElement).GetAttribute("Version") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Version = DirectCast(nomina(0), XmlElement).GetAttribute("Version")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("TipoNomina") IsNot Nothing Then
            _comprobante.Complemento.Nomina.TipoNomina = DirectCast(nomina(0), XmlElement).GetAttribute("TipoNomina")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("FechaPago") IsNot Nothing Then
            _comprobante.Complemento.Nomina.FechaPago = DirectCast(nomina(0), XmlElement).GetAttribute("FechaPago")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("FechaFinalPago") IsNot Nothing Then
            _comprobante.Complemento.Nomina.FechaFinalPago = DirectCast(nomina(0), XmlElement).GetAttribute("FechaFinalPago")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("FechaInicialPago") IsNot Nothing Then
            _comprobante.Complemento.Nomina.FechaInicialPago = DirectCast(nomina(0), XmlElement).GetAttribute("FechaInicialPago")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("NumDiasPagados") IsNot Nothing Then
            _comprobante.Complemento.Nomina.NumDiasPagados = DirectCast(nomina(0), XmlElement).GetAttribute("NumDiasPagados")
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("TotalPercepciones") IsNot Nothing Then
            Single.TryParse(DirectCast(nomina(0), XmlElement).GetAttribute("TotalPercepciones"), valFloat)
            _comprobante.Complemento.Nomina.TotalPercepciones = valFloat
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("TotalDeducciones") IsNot Nothing Then
            Single.TryParse(DirectCast(nomina(0), XmlElement).GetAttribute("TotalDeducciones"), valFloat)
            _comprobante.Complemento.Nomina.TotalDeducciones = valFloat
        End If
        If DirectCast(nomina(0), XmlElement).GetAttribute("TotalOtrosPagos") IsNot Nothing Then
            Single.TryParse(DirectCast(nomina(0), XmlElement).GetAttribute("TotalOtrosPagos"), valFloat)
            _comprobante.Complemento.Nomina.TotalOtrosPagos = valFloat
        End If

        If DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Emisor") Is Nothing Then
            Return
        End If
        Dim emisor As XmlNodeList = DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Emisor")
        If DirectCast(emisor(0), XmlElement).GetAttribute("Curp") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Emisor.Curp = DirectCast(emisor(0), XmlElement).GetAttribute("Curp")
        End If
        If DirectCast(emisor(0), XmlElement).GetAttribute("RegistroPatronal") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Emisor.RegistroPatronal = DirectCast(emisor(0), XmlElement).GetAttribute("RegistroPatronal")
        End If

        If DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Receptor") Is Nothing Then
            Return
        End If
        Dim receptor As XmlNodeList = DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Receptor")

        If DirectCast(receptor(0), XmlElement).GetAttribute("Curp") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.Curp = DirectCast(receptor(0), XmlElement).GetAttribute("Curp")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("NumSeguridadSocial") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.NumSeguridadSocial = DirectCast(receptor(0), XmlElement).GetAttribute("NumSeguridadSocial")
        End If

        If DirectCast(receptor(0), XmlElement).GetAttribute("FechaInicioRelLaboral") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.FechaInicioRelLaboral = DirectCast(receptor(0), XmlElement).GetAttribute("FechaInicioRelLaboral")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("Antigüedad") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.Antiguedad = DirectCast(receptor(0), XmlElement).GetAttribute("Antigüedad")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("TipoContrato") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.TipoContrato = DirectCast(receptor(0), XmlElement).GetAttribute("TipoContrato")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("TipoJornada") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.TipoJornada = DirectCast(receptor(0), XmlElement).GetAttribute("TipoJornada")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("TipoRegimen") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.TipoRegimen = DirectCast(receptor(0), XmlElement).GetAttribute("TipoRegimen")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("NumEmpleado") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.NumEmpleado = DirectCast(receptor(0), XmlElement).GetAttribute("NumEmpleado")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("RiesgoPuesto") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.RiesgoPuesto = DirectCast(receptor(0), XmlElement).GetAttribute("RiesgoPuesto")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("PeriodicidadPago") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = DirectCast(receptor(0), XmlElement).GetAttribute("PeriodicidadPago")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("Banco") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.Banco = DirectCast(receptor(0), XmlElement).GetAttribute("Banco")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("Departamento") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.Departamento = DirectCast(receptor(0), XmlElement).GetAttribute("Departamento")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("Puesto") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.Puesto = DirectCast(receptor(0), XmlElement).GetAttribute("Puesto")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("CuentaBancaria") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.CuentaBancaria = DirectCast(receptor(0), XmlElement).GetAttribute("CuentaBancaria")
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("SalarioBaseCotApor") IsNot Nothing Then
            Dim val As Single = 0.0F
            Single.TryParse(DirectCast(receptor(0), XmlElement).GetAttribute("SalarioBaseCotApor"), val)
            _comprobante.Complemento.Nomina.Receptor.SalarioBaseCotApor = val
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("SalarioDiarioIntegrado") IsNot Nothing Then
            Dim val1 As Single = 0.0F
            Single.TryParse(DirectCast(receptor(0), XmlElement).GetAttribute("SalarioDiarioIntegrado"), val1)
            _comprobante.Complemento.Nomina.Receptor.SalarioDiarioIntegrado = val1
        End If
        If DirectCast(receptor(0), XmlElement).GetAttribute("ClaveEntFed") IsNot Nothing Then
            _comprobante.Complemento.Nomina.Receptor.ClaveEntFed = DirectCast(receptor(0), XmlElement).GetAttribute("ClaveEntFed")
        End If

        'OBTENGO LAS PERCEPCIONES
        If DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Percepciones") Is Nothing Then
            Return
        End If

        Dim percepciones As XmlNodeList = DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Percepciones")
        If DirectCast(percepciones(0), XmlElement).GetAttribute("TotalExento") IsNot Nothing Then
            Single.TryParse(DirectCast(percepciones(0), XmlElement).GetAttribute("TotalExento"), valFloat)
            _comprobante.Complemento.Nomina.Percepciones.TotalExento = valFloat
        End If
        If DirectCast(percepciones(0), XmlElement).GetAttribute("TotalGravado") IsNot Nothing Then
            Single.TryParse(DirectCast(percepciones(0), XmlElement).GetAttribute("TotalGravado"), valFloat)
            _comprobante.Complemento.Nomina.Percepciones.TotalGravado = valFloat
        End If
        If DirectCast(percepciones(0), XmlElement).GetAttribute("TotalSueldos") IsNot Nothing Then
            Single.TryParse(DirectCast(percepciones(0), XmlElement).GetAttribute("TotalSueldos"), valFloat)
            _comprobante.Complemento.Nomina.Percepciones.TotalSueldos = valFloat
        End If

        If DirectCast(percepciones(0), XmlElement).GetElementsByTagName("nomina12:Percepcion").Count = 0 Then
            Return
        End If
        Dim listaPercepciones As XmlNodeList = DirectCast(percepciones(0), XmlElement).GetElementsByTagName("nomina12:Percepcion")


        Dim p As NPercepcion

        For Each nodo As XmlElement In listaPercepciones
            p = New NPercepcion()
            Single.TryParse(nodo.GetAttribute("ImporteExento"), valFloat)
            p.ImporteExento = valFloat
            Single.TryParse(nodo.GetAttribute("ImporteGravado"), valFloat)
            p.ImporteGravado = valFloat
            p.Concepto = nodo.GetAttribute("Concepto")
            p.Clave = nodo.GetAttribute("Clave")
            p.TipoPercepcion = nodo.GetAttribute("Percepcion")

            _comprobante.Complemento.Nomina.Percepciones.Percepciones.Add(p)
        Next



        'OBTENGO LAS DEDUCCIONES
        If DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Deducciones ") IsNot Nothing Then

            Dim deducciones As XmlNodeList = DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:Deducciones")
            If deducciones.Count > 0 Then

                If DirectCast(deducciones(0), XmlElement).GetAttribute("TotalOtrasDeducciones") IsNot Nothing Then
                    Single.TryParse(DirectCast(deducciones(0), XmlElement).GetAttribute("TotalOtrasDeducciones"), valFloat)
                    _comprobante.Complemento.Nomina.Deducciones.TotalOtrasDeducciones = valFloat
                End If

                If DirectCast(deducciones(0), XmlElement).GetElementsByTagName("nomina12:Deduccion").Count > 0 Then
                    Dim listaDeducciones As XmlNodeList = DirectCast(deducciones(0), XmlElement).GetElementsByTagName("nomina12:Deduccion")
                    Dim d As NDeduccion

                    For Each nodo As XmlElement In listaDeducciones
                        d = New NDeduccion()
                        Single.TryParse(nodo.GetAttribute("Importe"), valFloat)
                        d.Importe = valFloat
                        d.Concepto = nodo.GetAttribute("Concepto")
                        d.Clave = nodo.GetAttribute("Clave")
                        d.TipoDeduccion = nodo.GetAttribute("TipoDeduccion")
                        _comprobante.Complemento.Nomina.Deducciones.Deducciones.Add(d)
                    Next
                End If
            End If
        End If

        'OBTENGO OTROS PAGOS
        If DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:OtrosPagos ") IsNot Nothing Then
            Dim otrosPagos As XmlNodeList = DirectCast(nomina(0), XmlElement).GetElementsByTagName("nomina12:OtrosPagos")
            If otrosPagos.Count > 0 Then
                If DirectCast(otrosPagos(0), XmlElement).GetElementsByTagName("nomina12:OtroPago").Count = 0 Then
                    Return
                End If
                Dim listaOtrosPagos As XmlNodeList = DirectCast(otrosPagos(0), XmlElement).GetElementsByTagName("nomina12:OtroPago")

                For Each nodo As XmlElement In listaOtrosPagos
                    Dim op As New NOtroPago()

                    op.TipoOtroPago = nodo.GetAttribute("TipoOtroPago")


                    op.Clave = nodo.GetAttribute("Clave")
                    op.Concepto = nodo.GetAttribute("Concepto")
                    Single.TryParse(nodo.GetAttribute("Importe"), valFloat)
                    op.Importe = valFloat

                    If nodo.GetAttribute("Concepto") = "Subsidio efectivo" Then
                        If nodo.GetElementsByTagName("nomina12:SubsidioAlEmpleo").Count <> 0 Then
                            Dim ListaSubsidios As XmlNodeList = nodo.GetElementsByTagName("nomina12:SubsidioAlEmpleo")

                            For Each item As XmlElement In ListaSubsidios
                                If item.GetAttribute("SubsidioCausado") IsNot Nothing Then
                                    Dim f As Single
                                    Single.TryParse(item.GetAttribute("SubsidioCausado").ToString(), f)
                                    op.SubsidioAlEmpleo.SubsidioCausado = f
                                End If


                            Next
                        End If
                    End If
                    _comprobante.Complemento.Nomina.OtrosPagos.Add(op)
                Next


            End If
        End If

    End Sub

#End Region

#Region "CatalogosSAT"

    Public Function getFormaPago(clave As String) As String
        If clave = "01" Then
            Return "Efectivo"
        ElseIf clave = "02" Then
            Return "Cheque nominativo"
        ElseIf clave = "03" Then
            Return "Transferencia electrónica de fondos"
        ElseIf clave = "04" Then
            Return "Tarjeta de crédito"
        ElseIf clave = "05" Then
            Return "Monedero electrónico"
        ElseIf clave = "06" Then
            Return "Dinero electrónico"
        ElseIf clave = "08" Then
            Return "Vales de despensa"
        ElseIf clave = "12" Then
            Return "Dación en pago"
        ElseIf clave = "13" Then
            Return "Pago por subrogación"
        ElseIf clave = "14" Then
            Return "Pago por consignación"
        ElseIf clave = "15" Then
            Return "Condonación"
        ElseIf clave = "17" Then
            Return "Compensación"
        ElseIf clave = "23" Then
            Return "Novación"
        ElseIf clave = "24" Then
            Return "Confusión"
        ElseIf clave = "25" Then
            Return "Remisión de deuda"
        ElseIf clave = "26" Then
            Return "Prescripción o caducidad"
        ElseIf clave = "27" Then
            Return "A satisfacción del acreedor"
        ElseIf clave = "28" Then
            Return "Tarjeta de débito"
        ElseIf clave = "29" Then
            Return "Tarjeta de servicios"
        ElseIf clave = "30" Then
            Return "Aplicación de anticipos"
        ElseIf clave = "99" Then
            Return "Por definir"
        Else
            Return " "
        End If
    End Function

    Public Function getImpuesto(clave As String) As String
        If clave = "001" Then
            Return "ISR"
        ElseIf clave = "002" Then
            Return "IVA"
        ElseIf clave = "003" Then
            Return "IEPS"
        Else
            Return ""
        End If
    End Function

    Public Function getMetodoPago(clave As String) As String
        If clave = "PUE" Then
            Return "Pago en una sola exhibición"
        ElseIf clave = "PPD" Then
            Return "Pago en parcialidades o diferido"
        Else
            Return " "
        End If
    End Function
    'AB SAT 4.0
    Public Function getRegimenFiscal(clave As String) As String
        If clave = "601" Then
            Return "General de Ley Personas Morales"
        ElseIf clave = "603" Then
            Return "Personas Morales con Fines no Lucrativos"
        ElseIf clave = "605" Then
            Return "Sueldos y Salarios e Ingresos Asimilados a Salarios"
        ElseIf clave = "606" Then
            Return "Arrendamiento"
        ElseIf clave = "608" Then
            Return "Demás ingresos"
        ElseIf clave = "609" Then
            Return "Consolidación"
        ElseIf clave = "610" Then
            Return "Residentes en el Extranjero sin Establecimiento Permanente en México"
        ElseIf clave = "611" Then
            Return "Ingresos por Dividendos (socios y accionistas)"
        ElseIf clave = "612" Then
            Return "Personas Físicas con Actividades Empresariales y Profesionales"
        ElseIf clave = "614" Then
            Return "Ingresos por intereses"
        ElseIf clave = "616" Then
            Return "Sin obligaciones fiscales"
        ElseIf clave = "620" Then
            Return "Sociedades Cooperativas de Producción que optan por diferir sus ingresos"
        ElseIf clave = "621" Then
            Return "Incorporación Fiscal"
        ElseIf clave = "622" Then
            Return "Actividades Agrícolas, Ganaderas, Silvícolas y Pesqueras"
        ElseIf clave = "623" Then
            Return "Opcional para Grupos de Sociedades"
        ElseIf clave = "624" Then
            Return "Coordinados"
        ElseIf clave = "628" Then
            Return "Hidrocarburos"
        ElseIf clave = "607" Then
            Return "Régimen de Enajenación o Adquisición de Bienes"
        ElseIf clave = "629" Then
            Return "De los Regímenes Fiscales Preferentes y de las Empresas Multinacionales"
        ElseIf clave = "630" Then
            Return "Enajenación de acciones en bolsa de valores"
        ElseIf clave = "615" Then
            Return "Régimen de los ingresos por obtención de premios"
        ElseIf clave = "625" Then
            Return "Régimen de las Actividades Empresariales con ingresos a través de Plataformas Tecnológicas"
        ElseIf clave = "626" Then
            Return "Régimen Simplificado de Confianza"
        Else
            Return " "
        End If
    End Function

    Public Function getPais(clave As String) As String
        If clave = "MEX" Then
            Return "México"
        ElseIf clave = "USA" Then
            Return "Estados Unidos"
        Else
            Return " "
        End If
    End Function

    Public Function getTipoComprobante(clave As String) As String
        If clave = "I" Then
            Return "Ingreso"
        ElseIf clave = "E" Then
            Return "Egreso"
        ElseIf clave = "T" Then
            Return "Traslado"
        ElseIf clave = "N" Then
            Return "Nómina"
        ElseIf clave = "P" Then
            Return "Pago"
        Else
            Return " "
        End If
    End Function
    'AB SAT 4.0
    Public Function getUsoCFDI(clave As String) As String
        If clave = "G01" Then
            Return "Adquisición de mercancias"
        ElseIf clave = "G02" Then
            Return "Devoluciones, descuentos o bonificaciones"
        ElseIf clave = "G03" Then
            Return "Gastos en general"
        ElseIf clave = "I01" Then
            Return "Construcciones"
        ElseIf clave = "I02" Then
            Return "Mobilario y equipo de oficina por inversiones"
        ElseIf clave = "I03" Then
            Return "Equipo de transporte"
        ElseIf clave = "I04" Then
            Return "Equipo de computo y accesorios"
        ElseIf clave = "I05" Then
            Return "Dados, troqueles, moldes, matrices y herramental"
        ElseIf clave = "I06" Then
            Return "Comunicaciones telefónicas"
        ElseIf clave = "I07" Then
            Return "Comunicaciones satelitales"
        ElseIf clave = "I08" Then
            Return "Otra maquinaria y equipo"
        ElseIf clave = "D01" Then
            Return "Honorarios médicos, dentales y gastos hospitalarios."
        ElseIf clave = "D02" Then
            Return "Gastos médicos por incapacidad o discapacidad"
        ElseIf clave = "D03" Then
            Return "Gastos funerales."
        ElseIf clave = "D04" Then
            Return "Donativos."
        ElseIf clave = "D05" Then
            Return "Intereses reales efectivamente pagados por créditos hipotecarios (casa habitación)."
        ElseIf clave = "D06" Then
            Return "Aportaciones voluntarias al SAR."
        ElseIf clave = "D07" Then
            Return "Primas por seguros de gastos médicos."
        ElseIf clave = "D08" Then
            Return "Gastos de transportación escolar obligatoria."
        ElseIf clave = "D09" Then
            Return "Depósitos en cuentas para el ahorro, primas que tengan como base planes de pensiones."
        ElseIf clave = "D10" Then
            Return "Pagos por servicios educativos (colegiaturas)"
        ElseIf clave = "P01" Then
            Return "Por definir"
        ElseIf clave = "S01" Then
            Return "Sin efectos fiscales"
        ElseIf clave = "CP01" Then
            Return "Pagos"
        ElseIf clave = "CN01" Then
            Return "Nomina"
        Else
            Return " "
        End If
    End Function

    Public Function getUnidad(clave As String) As String
        clave = clave.ToUpper()
        If clave = "H87" Then
            Return "Pieza"
        ElseIf clave = "LTR" Then
            Return "Litro"
        ElseIf clave = "KGM" Then
            Return "Kilogramo"
        ElseIf clave = "E48" Then
            Return "Unidad de servicio"
        ElseIf clave = "EA" Then
            Return "Elemento"
        ElseIf clave = "PR" Then
            Return "Par"
        ElseIf clave = "ACT" Then
            Return "Actividad"
        ElseIf clave = "LUB" Then
            Return "Tonelada métrica, aceite lubricante"
        Else
            Return " "
        End If
    End Function

    Public Function getMoneda(clave As String) As String
        If clave = "MXN" Then
            Return "PESO MEXICANO"
        ElseIf clave = "EUR" Then
            Return "EURO"
        ElseIf clave = "USD" Then
            Return "DOLAR AMERICANO"
        Else
            Return " "
        End If
    End Function

    Public Function getTipoRelacion(clave As String) As String
        If clave = "01" Then
            Return "Nota de crédito de los documentos relacionados"
        ElseIf clave = "02" Then
            Return "Nota de débito de los documentos relacionados"
        ElseIf clave = "03" Then
            Return "Devolución de mercancía sobre facturas o traslados previos"
        ElseIf clave = "04" Then
            Return "Sustitución de los CFDI previos"
        ElseIf clave = "05" Then
            Return "Traslados de mercancias facturados previamente"
        ElseIf clave = "06" Then
            Return "Factura generada por los traslados previos"
        ElseIf clave = "07" Then
            Return "CFDI por aplicacion de anticipo"
        Else
            Return ""
        End If
    End Function

#End Region

#Region "Escribir datos en el .pdf"


    Private Sub NAgregarTitulo()
        Dim titulo As New StringBuilder()
        titulo.Append("Recibo pago de nómina ")
        If _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = "02" Then
            titulo.Append("Semanal      del ")
        End If
        If _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = "03" Then
            titulo.Append("Catorcenal      del ")
        End If
        If _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = "04" Then
            titulo.Append("Quincenal      del ")
        End If
        If _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = "05" Then
            titulo.Append("Mensual      del ")
        End If
        If _comprobante.Complemento.Nomina.Receptor.PeriodicidadPago = "10" Then
            titulo.Append("Decenal      del ")
        End If
        Dim fecha As New DateTime()
        DateTime.TryParse(_comprobante.Complemento.Nomina.FechaInicialPago, fecha)
        titulo.Append(fecha.ToShortDateString() + " ")
        titulo.Append("  al ")
        DateTime.TryParse(_comprobante.Complemento.Nomina.FechaFinalPago, fecha)
        titulo.Append(fecha.ToShortDateString() + " ")
        Dim p1 As New Paragraph()

        p1.Alignment = Element.ALIGN_CENTER
        p1.Add(New Phrase(titulo.ToString(), New Font(Font.FontFamily.HELVETICA, 16, Font.BOLD)))

        _documento.Add(p1)
    End Sub

    Private Sub NAgregarTabla(logo As System.Drawing.Image)
        Dim columnas As Single() = {565.0F}
        Dim tablaDatos As New PdfPTable(columnas)
        tablaDatos.SetTotalWidth(columnas)
        tablaDatos.SpacingBefore = 10
        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaDatos.LockedWidth = True

        tablaDatos.AddCell(AgregarDatosLaborales(logo))
        tablaDatos.AddCell(NAgregarEmisor())
        tablaDatos.AddCell(NAgregarPercepcionesDeducciones())


        _documento.Add(tablaDatos)

        Dim colTotalNeto As Single() = {405.0F, 90, 70}
        Dim tablaTotalNeto As New PdfPTable(colTotalNeto)
        'tablaDatos.DefaultCell.Border = Rectangle.NO_BORDER;
        tablaTotalNeto.SetTotalWidth(colTotalNeto)
        tablaTotalNeto.SpacingBefore = 0
        'tablaTotalNeto.HorizontalAlignment = Rectangle.ALIGN_RIGHT;
        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaTotalNeto.LockedWidth = True

        Dim celdab As New PdfPCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 8)))
        celdab.HorizontalAlignment = Rectangle.ALIGN_RIGHT
        celdab.Border = Rectangle.NO_BORDER
        tablaTotalNeto.AddCell(celdab)

        tablaTotalNeto.AddCell(New Phrase("Neto a recibir:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaTotal As New PdfPCell(New Phrase(_comprobante.Total.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        celdaTotal.HorizontalAlignment = Rectangle.ALIGN_RIGHT
        tablaTotalNeto.AddCell(celdaTotal)

        _documento.Add(tablaTotalNeto)
    End Sub

    Private Function AgregarDatosLaborales(logoEmpresa As System.Drawing.Image) As PdfPTable
        Dim columnas As Single() = {120.0F, 60.0F, 100.0F, 60.0F, 80.0F, 70.0F, _
            75.0F}
        Dim tablaDatos As New PdfPTable(columnas)
        tablaDatos.DefaultCell.Border = Rectangle.NO_BORDER
        tablaDatos.SetTotalWidth(columnas)

        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaDatos.LockedWidth = True

        'Agregar logo
        If logoEmpresa IsNot Nothing Then
            Dim imagen As Image = Image.GetInstance(logoEmpresa, BaseColor.BLACK)
            imagen.ScaleToFit(120, 120)
            Dim cell As New PdfPCell(imagen)
            cell.Rowspan = 6
            cell.HorizontalAlignment = Rectangle.ALIGN_CENTER
            cell.Border = Rectangle.NO_BORDER
            tablaDatos.AddCell(cell)
        Else

            Dim celdaVacia As New PdfPCell(New Phrase())
            celdaVacia.Rowspan = 6
            celdaVacia.Border = Rectangle.NO_BORDER
            tablaDatos.AddCell(celdaVacia)
        End If

        tablaDatos.AddCell(New Phrase("Núm.", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.NumEmpleado, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Trabajador", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaNombre As New PdfPCell(New Phrase(_comprobante.Receptor.Nombre, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaNombre.Border = Rectangle.NO_BORDER
        celdaNombre.Colspan = 4
        tablaDatos.AddCell(celdaNombre)
        tablaDatos.AddCell(New Phrase("CURP", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.Curp, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Dias Periodo", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.NumDiasPagados, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Cuota diaria", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.SalarioBaseCotApor.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("R.F.C", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Receptor.Rfc, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Tipo jornada", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        If _comprobante.Complemento.Nomina.Receptor.TipoJornada = "01" Then
            tablaDatos.AddCell(New Phrase("Diurna", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "02" Then
            tablaDatos.AddCell(New Phrase("Nocturna", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "03" Then
            tablaDatos.AddCell(New Phrase("Mixta", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "04" Then
            tablaDatos.AddCell(New Phrase("Por hora", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "05" Then
            tablaDatos.AddCell(New Phrase("Reducida", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "06" Then
            tablaDatos.AddCell(New Phrase("Continuada", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "07" Then
            tablaDatos.AddCell(New Phrase("Partida", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "08" Then
            tablaDatos.AddCell(New Phrase("Por turnos", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "99" Then
            tablaDatos.AddCell(New Phrase("OtraJornada", New Font(Font.FontFamily.HELVETICA, 8)))
        Else
            tablaDatos.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 8)))
        End If


        tablaDatos.AddCell(New Phrase("Integrado", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.SalarioDiarioIntegrado.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("IMSS", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.NumSeguridadSocial, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Horas jornada", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        If _comprobante.Complemento.Nomina.Receptor.TipoJornada = "01" Then
            'DIURNA
            tablaDatos.AddCell(New Phrase("8", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "02" Then
            'NOCTURTO
            tablaDatos.AddCell(New Phrase("7", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoJornada = "03" Then
            'MIXTO
            tablaDatos.AddCell(New Phrase("7.3", New Font(Font.FontFamily.HELVETICA, 8)))
        Else
            tablaDatos.AddCell(New Phrase("", New Font(Font.FontFamily.HELVETICA, 8)))
        End If

        tablaDatos.AddCell(New Phrase("Sub. Causado", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(NObtenerSubsidioCausado().ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))

        'tablaDatos.AddCell(new Phrase("Sub. Empleo", new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)));
        'tablaDatos.AddCell(new Phrase(_templatePDF.nomina.totalPercepciones.ToString("C2"), new Font(Font.FontFamily.HELVETICA, 8)));
        tablaDatos.AddCell(New Phrase("Regimen", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        If _comprobante.Complemento.Nomina.Receptor.TipoRegimen = "02" Then
            tablaDatos.AddCell(New Phrase("Sueldos", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoRegimen = "07" Then
            tablaDatos.AddCell(New Phrase("Asimilados Miembros consejos", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoRegimen = "08" Then
            tablaDatos.AddCell(New Phrase("Asimilados comisionistas", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoRegimen = "09" Then
            tablaDatos.AddCell(New Phrase("Asimilados Honorarios", New Font(Font.FontFamily.HELVETICA, 8)))
        ElseIf _comprobante.Complemento.Nomina.Receptor.TipoRegimen = "10" Then
            tablaDatos.AddCell(New Phrase("Asimilados acciones", New Font(Font.FontFamily.HELVETICA, 8)))
        Else
            tablaDatos.AddCell(New Phrase(" ", New Font(Font.FontFamily.HELVETICA, 8)))
        End If
        tablaDatos.AddCell(New Phrase("Ing. Gravado", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Percepciones.TotalGravado.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))


        tablaDatos.AddCell(New Phrase("Ingreso exento", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Percepciones.TotalExento.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Puesto", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))

        Dim celdaPuesto As New PdfPCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.Puesto, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaPuesto.Border = Rectangle.NO_BORDER
        celdaPuesto.Colspan = 2
        tablaDatos.AddCell(celdaPuesto)

        tablaDatos.AddCell(New Phrase("Departamento", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaDepartamento As New PdfPCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.Departamento, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaDepartamento.Border = Rectangle.NO_BORDER
        celdaDepartamento.Colspan = 2
        tablaDatos.AddCell(celdaDepartamento)


        Return tablaDatos
    End Function

    Private Function NAgregarEmisor() As PdfPTable
        Dim columnas As Single() = {80.0F, 164.0F, 50.0F, 90.0F, 80.0F, 100.0F}
        Dim tablaDatos As New PdfPTable(columnas)
        tablaDatos.DefaultCell.Border = Rectangle.NO_BORDER
        tablaDatos.SetTotalWidth(columnas)
        tablaDatos.SpacingBefore = 5
        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaDatos.LockedWidth = True

        tablaDatos.AddCell(New Phrase("Patrón/Emisor:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaRFC As New PdfPCell(New Phrase(_comprobante.Emisor.Nombre, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaRFC.Border = Rectangle.NO_BORDER
        celdaRFC.Colspan = 5
        tablaDatos.AddCell(celdaRFC)
        tablaDatos.AddCell(New Phrase("Dirección:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaDireccion As New PdfPCell(New Phrase("Direccion", New Font(Font.FontFamily.HELVETICA, 8)))
        celdaDireccion.Border = Rectangle.NO_BORDER
        celdaDireccion.Colspan = 5
        tablaDatos.AddCell(celdaDireccion)
        tablaDatos.AddCell(New Phrase("R.F.C.:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Emisor.Rfc, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("IMSS:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Emisor.RegistroPatronal, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Riesgo Puesto:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.Receptor.RiesgoPuesto, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Regimen fiscal:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaRegimen As New PdfPCell(New Phrase(_comprobante.Emisor.RegimenFiscal, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaRegimen.Border = Rectangle.NO_BORDER
        celdaRegimen.Colspan = 3
        tablaDatos.AddCell(celdaRegimen)
        tablaDatos.AddCell(New Phrase("Fecha de pago:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.Nomina.FechaPago, New Font(Font.FontFamily.HELVETICA, 8)))

        Return tablaDatos

    End Function

    Private Function NAgregarPercepcionesDeducciones() As PdfPTable
        Dim anchoColumnasTablaProductos As Single() = {280.0F, 280.0F}
        Dim tablaProductosPrincipal As New PdfPTable(anchoColumnasTablaProductos)
        tablaProductosPrincipal.DefaultCell.Border = Rectangle.NO_BORDER
        tablaProductosPrincipal.SetTotalWidth(anchoColumnasTablaProductos)
        tablaProductosPrincipal.SpacingBefore = 0
        tablaProductosPrincipal.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductosPrincipal.LockedWidth = True


        'Datos de los productos
        Dim tamanoColumnas As Single() = {190.0F, 90.0F}
        Dim tablaPercepciones As New PdfPTable(tamanoColumnas)
        tablaPercepciones.DefaultCell.Border = Rectangle.NO_BORDER
        tablaPercepciones.SetTotalWidth(tamanoColumnas)
        tablaPercepciones.HorizontalAlignment = Element.ALIGN_LEFT
        tablaPercepciones.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaPercepciones.LockedWidth = True

        Dim celdaTituloPercepciones As New PdfPCell(New Phrase("PERCEPCIONES", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        celdaTituloPercepciones.Colspan = 2
        celdaTituloPercepciones.HorizontalAlignment = Element.ALIGN_CENTER
        celdaTituloPercepciones.Border = Rectangle.NO_BORDER
        tablaPercepciones.AddCell(celdaTituloPercepciones)

        Dim celdaConcepto As New PdfPCell(New Phrase("Concepto", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        celdaConcepto.HorizontalAlignment = Element.ALIGN_LEFT
        celdaConcepto.Right = -1
        tablaPercepciones.AddCell(celdaConcepto)

        Dim celdaImporte As New PdfPCell(New Phrase("Importe", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        celdaImporte.HorizontalAlignment = Element.ALIGN_RIGHT
        tablaPercepciones.AddCell(celdaImporte)

        For Each p As NPercepcion In _comprobante.Complemento.Nomina.Percepciones.Percepciones
            tablaPercepciones.AddCell(New Phrase(p.Concepto, New Font(Font.FontFamily.HELVETICA, 8)))
            Dim importe As New PdfPCell(New Phrase((p.ImporteExento + p.ImporteGravado).ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
            importe.HorizontalAlignment = Element.ALIGN_RIGHT
            importe.Border = Rectangle.RIGHT_BORDER
            'tablaProductos.AddCell(new Phrase(p.importe, new Font(Font.FontFamily.HELVETICA, 8)));
            tablaPercepciones.AddCell(importe)
        Next

        If _comprobante.Complemento.Nomina.OtrosPagos.Count > 0 Then
            tablaPercepciones.AddCell(New Phrase("Otros Pagos", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
            Dim otrosPagos As New PdfPCell()
            otrosPagos.Border = Rectangle.RIGHT_BORDER
            tablaPercepciones.AddCell(otrosPagos)
            For Each p As NOtroPago In _comprobante.Complemento.Nomina.OtrosPagos
                tablaPercepciones.AddCell(New Phrase(p.Concepto, New Font(Font.FontFamily.HELVETICA, 8)))
                Dim importe As New PdfPCell(New Phrase((p.Importe).ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
                importe.HorizontalAlignment = Element.ALIGN_RIGHT
                importe.Border = Rectangle.RIGHT_BORDER
                'tablaProductos.AddCell(new Phrase(p.importe, new Font(Font.FontFamily.HELVETICA, 8)));
                tablaPercepciones.AddCell(importe)
            Next
        End If
        tablaPercepciones.AddCell(New Phrase("Suma de percepciones", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))

        Dim totalPercepciones As Single = _comprobante.Complemento.Nomina.TotalPercepciones
        Dim celdaTotalPercepciones As New PdfPCell(New Phrase(totalPercepciones.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        celdaTotalPercepciones.HorizontalAlignment = Element.ALIGN_RIGHT
        celdaTotalPercepciones.Border = Rectangle.RIGHT_BORDER
        tablaPercepciones.AddCell(celdaTotalPercepciones)

        'Datos de los productos
        Dim tamanoColumnasPercepciones As Single() = {190.0F, 90.0F}
        Dim tablaDeducciones As New PdfPTable(tamanoColumnas)
        tablaDeducciones.DefaultCell.Border = Rectangle.NO_BORDER
        tablaDeducciones.SetTotalWidth(tamanoColumnas)
        tablaDeducciones.HorizontalAlignment = Element.ALIGN_LEFT
        tablaDeducciones.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaDeducciones.LockedWidth = True


        Dim celdaTituloDeducciones As New PdfPCell(New Phrase("DEDUCCIONES", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        celdaTituloDeducciones.Border = Rectangle.NO_BORDER
        celdaTituloDeducciones.HorizontalAlignment = Element.ALIGN_CENTER
        celdaTituloDeducciones.Colspan = 2
        tablaDeducciones.AddCell(celdaTituloDeducciones)

        tablaDeducciones.AddCell(celdaConcepto)
        tablaDeducciones.AddCell(celdaImporte)
        For Each p As NDeduccion In _comprobante.Complemento.Nomina.Deducciones.Deducciones
            tablaDeducciones.AddCell(New Phrase(p.Concepto, New Font(Font.FontFamily.HELVETICA, 8)))
            Dim importe As New PdfPCell(New Phrase(New Phrase((p.Importe).ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8))))
            importe.HorizontalAlignment = Element.ALIGN_RIGHT
            importe.Border = Rectangle.NO_BORDER
            'tablaProductos.AddCell(new Phrase(p.importe, new Font(Font.FontFamily.HELVETICA, 8)));
            tablaDeducciones.AddCell(importe)
        Next

        tablaDeducciones.AddCell(New Phrase("Suma de deducciones", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))

        Dim celdaTotalDeducciones As New PdfPCell(New Phrase(_comprobante.Descuento.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        celdaTotalDeducciones.Border = Rectangle.NO_BORDER
        celdaTotalDeducciones.HorizontalAlignment = Element.ALIGN_RIGHT
        tablaDeducciones.AddCell(celdaTotalDeducciones)

        'tablaDeducciones.FooterRows = 1;

        Dim celdaTitulos As New PdfPCell(tablaPercepciones)
        celdaTitulos.Right = -5
        celdaTitulos.Border = Rectangle.NO_BORDER
        tablaProductosPrincipal.AddCell(celdaTitulos)
        Dim celdaProductos As New PdfPCell(tablaDeducciones)
        celdaProductos.Border = Rectangle.NO_BORDER
        tablaProductosPrincipal.AddCell(celdaProductos)


        Return tablaProductosPrincipal
    End Function

    Private Function NObtenerSubsidioCausado() As Single
        Dim totalSubsidiosCausado As Single = 0.0F
        For Each item As NOtroPago In _comprobante.Complemento.Nomina.OtrosPagos
            If item.Concepto = "Subsidio efectivo" Then
                totalSubsidiosCausado = item.SubsidioAlEmpleo.SubsidioCausado

            End If
        Next
        Return totalSubsidiosCausado
    End Function

    Private Sub NAgregarMensaje()
        Dim columnas As Single() = {565}
        Dim tablaDatos As New PdfPTable(columnas)
        tablaDatos.DefaultCell.Border = Rectangle.NO_BORDER
        tablaDatos.SetTotalWidth(columnas)
        tablaDatos.SpacingBefore = 25
        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaDatos.LockedWidth = True

        Dim celdaLinea As New PdfPCell(New Phrase("___________________________________", New Font(Font.FontFamily.HELVETICA, 8)))
        celdaLinea.Border = Rectangle.NO_BORDER
        celdaLinea.HorizontalAlignment = Rectangle.ALIGN_CENTER
        tablaDatos.AddCell(celdaLinea)
        Dim celdaNombre As New PdfPCell(New Phrase(_comprobante.Receptor.Nombre, New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        celdaNombre.Border = Rectangle.NO_BORDER
        celdaNombre.HorizontalAlignment = Rectangle.ALIGN_CENTER
        tablaDatos.AddCell(celdaNombre)
        tablaDatos.AddCell(New Phrase("El importe de este recibo cubre toda percepción, deducción, subsidio fiscal, descansos y prestaciones legales por mi trabajo a éste patrón (o derivadas de la prestación de servicios independientes a éste Emisor) por el periodo marcado, no quedando cantidad alguna por pagar o prestación por reclamar.", New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Acepto y me doy por pagado en forma y tiempo, segun lo marcado en las Leyes Fiscales y Laborales vigentes, quedando pendiente la recepción del archivo CFDI a mi dirección de correo electrónico, la que me ha sido solicitada y que haré saber en su oportunidad a éste Patrón o Emisor.", New Font(Font.FontFamily.HELVETICA, 8)))

        _documento.Add(tablaDatos)
    End Sub

    Private Sub NAgregarDatosFiscales()
        Dim columnas As Single() = {125.0F, 80.0F, 160.0F, 100.0F, 100.0F}
        Dim tablaDatos As New PdfPTable(columnas)
        tablaDatos.SetTotalWidth(columnas)
        tablaDatos.SpacingBefore = 10
        'tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT;
        tablaDatos.LockedWidth = True

        Dim celdaUI As New PdfPCell(New Phrase("Representación parcial del CFDI correspondiente al UUID/ Folio fiscal:     " + _comprobante.Complemento.TimbreFiscalDigital.UUID, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaUI.Colspan = 5
        celdaUI.HorizontalAlignment = Rectangle.ALIGN_CENTER
        tablaDatos.AddCell(celdaUI)

        'Agregamos el codigo QR al documento
        Dim codigoQR As New StringBuilder()
        codigoQR.Append("?re" + _comprobante.Emisor.Rfc)
        'RFC del Emisor
        codigoQR.Append("&rr" + _comprobante.Receptor.Rfc)
        'RFC del receptor
        codigoQR.Append("&tt" + _comprobante.Total)
        'Total del comprobante 10 enteros y 6 decimales
        codigoQR.Append("&id" + _comprobante.Complemento.TimbreFiscalDigital.UUID)
        'UUID del comprobante
        Dim pdfCodigoQR As New BarcodeQRCode(codigoQR.ToString(), 1, 1, Nothing)
        Dim img As Image = pdfCodigoQR.GetImage()
        img.SpacingAfter = 0.0F
        img.SpacingBefore = 0.0F
        img.BorderWidth = 1.0F
        img.ScaleAbsolute(100.0F, 100.0F)
        'img.ScalePercent(100, 100);
        'img.border
        Dim celdaQR As New PdfPCell(img)
        celdaQR.Rowspan = 5
        celdaQR.FixedHeight = 105
        celdaQR.HorizontalAlignment = Rectangle.ALIGN_CENTER
        celdaQR.VerticalAlignment = Rectangle.ALIGN_MIDDLE

        tablaDatos.AddCell(celdaQR)


        tablaDatos.AddCell(New Phrase("Total:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Total.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))
        Dim ffolio As New Phrase()
        ffolio.Add(New Chunk("Folio: ", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        ffolio.Add(New Chunk(_comprobante.Folio, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(ffolio)

        Dim fMetodoPao As New Phrase()
        fMetodoPao.Add(New Chunk("Metodo de pago: ", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        fMetodoPao.Add(New Chunk(_comprobante.MetodoPago, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(fMetodoPao)


        'tablaDatos.AddCell(new Phrase(_templatePDF.folio, new Font(Font.FontFamily.HELVETICA, 8)));
        'Agregar Totales
        tablaDatos.AddCell(New Phrase("Importe con letra:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        Dim celdaImporteLetra As New PdfPCell(New Phrase(_comprobante.TotalLetra, New Font(Font.FontFamily.HELVETICA, 8)))
        celdaImporteLetra.Colspan = 3
        tablaDatos.AddCell(celdaImporteLetra)
        tablaDatos.AddCell(New Phrase("Fecha y hora de certificación:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.FechaTimbrado, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Lugar" & vbLf & "Fecha de expedición:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.LugarExpedicion + vbLf + _comprobante.Fecha, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Certificado SAT:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSAT, New Font(Font.FontFamily.HELVETICA, 8)))
        tablaDatos.AddCell(New Phrase("Forma de pago:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaDatos.AddCell(New Phrase(_comprobante.FormaPago, New Font(Font.FontFamily.HELVETICA, 8)))

        Dim parrafoSelloSAT As New Phrase()
        parrafoSelloSAT.Add((New Chunk("Sello SAT" & vbLf, New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD))))
        parrafoSelloSAT.Add(New Chunk(_comprobante.Complemento.TimbreFiscalDigital.SelloSAT, New Font(Font.FontFamily.HELVETICA, 8)))
        Dim celdaSelloSAT As New PdfPCell(parrafoSelloSAT)
        celdaSelloSAT.Colspan = 4
        tablaDatos.AddCell(celdaSelloSAT)

        Dim parrafoSelloDE As New Phrase()
        parrafoSelloDE.Add((New Chunk("Sello digital Emisor" & vbLf, New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD))))
        parrafoSelloDE.Add(New Chunk(_comprobante.Complemento.TimbreFiscalDigital.SelloCFD, New Font(Font.FontFamily.HELVETICA, 8)))
        Dim celdaSDE As New PdfPCell(parrafoSelloDE)
        celdaSDE.Colspan = 5
        tablaDatos.AddCell(celdaSDE)

        Dim parrafoCadenaOriginal As New Phrase()
        parrafoCadenaOriginal.Add((New Chunk("Cadena original Complemento de certificado digital del SAT" & vbLf, New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD))))
        Dim cadenaOriginal As New StringBuilder()
        cadenaOriginal.Append("||")
        cadenaOriginal.Append("1.0|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.UUID + "|")
        cadenaOriginal.Append(_comprobante.Fecha.ToString() + "|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.SelloSAT + "|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSAT + "||")
        parrafoCadenaOriginal.Add(New Chunk(cadenaOriginal.ToString(), New Font(Font.FontFamily.HELVETICA, 8)))

        Dim celdaCadenaOriginal As New PdfPCell(parrafoCadenaOriginal)
        celdaCadenaOriginal.Colspan = 5
        tablaDatos.AddCell(celdaCadenaOriginal)

        _documento.Add(tablaDatos)

    End Sub

    Private Sub AgregarLogo(logoEmpresa As System.Drawing.Image)
        If logoEmpresa Is Nothing Then
            Return
        End If
        'Agrego la imagen al documento
        Dim imagen As Image = Image.GetInstance(logoEmpresa, BaseColor.BLACK)
        imagen.ScaleToFit(140, 140)
        imagen.Alignment = Element.ALIGN_RIGHT
        Dim logo As New Chunk(imagen, 1, -115)
        _documento.Add(logo)
    End Sub

    Private Sub AgregarCuadro()
        _cb = _writer.DirectContentUnder
        '_cb.SaveState();
        '_cb.BeginText();
        '_cb.MoveText(1, 1);
        '_cb.SetFontAndSize(_fuenteTitulos, 8);
        '_cb.ShowText("Faustino Rojas Arelano");
        '_cb.EndText();
        '_cb.RestoreState();

        'Agrego cuadro al documento
        _cb.SetColorStroke(BaseColor.BLACK)
        'Color de la linea
        _cb.SetColorFill(BaseColor.WHITE)
        ' Color del relleno
        _cb.SetLineWidth(1.5F)
        'Tamano de la linea
        _cb.Rectangle(408, 625, 195, 160)
        _cb.FillStroke()
    End Sub

    Private Sub AgregarDatosEmisorReceptor()

        'Datos del emisor
        Dim p1 As New Paragraph()
        p1.IndentationLeft = 150.0F
        p1.IndentationRight = 250.0F

        p1.Leading = 9
        p1.Alignment = Element.ALIGN_CENTER
        p1.Add(New Phrase(_comprobante.Emisor.Nombre, _fTitulo))
        p1.Add(vbLf)
        p1.Add(New Phrase(_comprobante.Emisor.Rfc, _FNormal))
        p1.Add(vbLf)
        p1.Add(New Phrase(Convert.ToString("Régimen fiscal: " + _comprobante.Emisor.RegimenFiscal + " - ") & getRegimenFiscal(_comprobante.Emisor.RegimenFiscal), _FNormal))
        p1.Add(vbLf)
        p1.Add(vbLf)
        p1.Add(New Phrase("CLIENTE", _fTitulo))
        p1.Add(vbLf)
        p1.Add(New Phrase(_comprobante.Receptor.Nombre, _FNormal))
        p1.Add(vbLf)
        p1.Add(New Phrase(_comprobante.Receptor.Rfc, _FNormal))
        p1.Add(vbLf)
        p1.Add(New Phrase(Convert.ToString("Uso de CFDI: " + _comprobante.Receptor.UsoCFDI + " - ") & getUsoCFDI(_comprobante.Receptor.UsoCFDI), _FNormal))
        p1.Add(vbLf)
        'AB SAT 4.0
        p1.Add(New Phrase(Convert.ToString("Regimen Fiscal: " + _comprobante.Receptor.RegimenFiscalReceptor + " - ") & getRegimenFiscal(_comprobante.Receptor.RegimenFiscalReceptor), _FNormal))
        p1.Add(vbLf)
        p1.Add(New Phrase(Convert.ToString("Domicilio Fiscal: " + _comprobante.Receptor.DomicilioFiscalReceptor), _FNormal))
        If _comprobante.Receptor.ResidenciaFiscal <> String.Empty Then
            p1.Add(vbLf)
            p1.Add(New Phrase(_comprobante.Receptor.ResidenciaFiscal, New Font(Font.FontFamily.HELVETICA, 8)))
        End If
        If _comprobante.Receptor.NumRegIdTrib <> String.Empty Then
            p1.Add(vbLf)
            p1.Add(New Phrase(_comprobante.Receptor.NumRegIdTrib, New Font(Font.FontFamily.HELVETICA, 8)))
        End If
        p1.Add(vbLf)
        p1.Add(vbLf)
        p1.Add(vbLf)
        p1.Add(vbLf)
        p1.Add(vbLf)
        'p1.Add(vbLf)
        'p1.Add(vbLf)
        'p1.Add(vbLf)
        'p1.Add(vbLf)
        _documento.Add(p1)
    End Sub
    Private Sub Agregarpaciente(dpaciente As String)
        If dpaciente Is Nothing Then
            Return
        End If
        Dim pp As New Paragraph()
        pp.IndentationLeft = 150.0F
        pp.IndentationRight = 250.0F

        pp.Alignment = Element.ALIGN_CENTER
        pp.Add(New Phrase("PACIENTE", _fTitulo))
        pp.Add(vbLf)
        pp.Add(New Phrase(dpaciente, _FNormal))

        'Agrego la imagen al documento
        'Dim imagen As Image = Image.GetInstance(logoEmpresa, BaseColor.BLACK)
        'imagen.ScaleToFit(140, 140)
        'imagen.Alignment = Element.ALIGN_RIGHT
        'Dim logo As New Chunk(imagen, 1, -115)
        _documento.Add(pp)
    End Sub

    Private Sub AgregaPropiedadesDocumento()
        _documento.AddAuthor("Advansys S.A de C.V")
        _documento.AddCreator("Sample application using iTextSharp")
        _documento.AddKeywords("Reporte de Hunter")
        _documento.AddSubject("Document subject - Describing the steps creating a PDF document")
        _documento.AddTitle("Reporte del sistema Hunter")
        _documento.SetMargins(5, 5, 20, 30)
    End Sub

    Private Function toFloat(valor As String) As Single
        Dim val As Single = 0.0F
        Single.TryParse(valor, val)
        Return val
    End Function

    Private Function toDecimal(valor As String) As Decimal
        Dim val As Decimal = 0
        Decimal.TryParse(valor, val)
        Return val
    End Function

    Private Sub AgregarDatosFactura()
        Dim anchoColumnas As Single() = {250.0F}
        Dim tablaDatos As New PdfPTable(anchoColumnas)
        'tablaProductosPrincipal.DefaultCell.Border = Rectangle.NO_BORDER;
        tablaDatos.DefaultCell.UseBorderPadding = False
        tablaDatos.SetTotalWidth(anchoColumnas)
        tablaDatos.DefaultCell.Border = Rectangle.NO_BORDER
        tablaDatos.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER
        tablaDatos.DefaultCell.Padding = 1
        'tablaDatos.DefaultCell.FixedHeight = 13;
        'tablaProductosPrincipal.SpacingBefore = 15;
        tablaDatos.HorizontalAlignment = Element.ALIGN_LEFT
        tablaDatos.LockedWidth = True
        tablaDatos.AddCell(New Phrase("FACTURA NÚM: " + _comprobante.Serie + " " + _comprobante.Folio, _FNormalBold))
        tablaDatos.AddCell(New Phrase("FOLIO FISCAL (UUID): ", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.UUID, _FNormal))
        tablaDatos.AddCell(New Phrase("NO. DE SERIE DEL CERTIFICADO DEL SAT:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSAT, _FNormal))
        tablaDatos.AddCell(New Phrase("NO. DE SERIE DEL CERTIFICADO DEL EMISOR:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.NoCertificado, _FNormal))
        tablaDatos.AddCell(New Phrase("FECHA Y HORA DE CERTIFICACIÓN:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.FechaTimbrado, _FNormal))
        tablaDatos.AddCell(New Phrase("RFC PROVEEDOR DE CERTIFICACIÓN:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.RfcProvCertif, _FNormal))
        tablaDatos.AddCell(New Phrase("FECHA Y HORA DE EMISIÓN DE CFDI:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.Fecha, _FNormal))
        tablaDatos.AddCell(New Phrase("LUGAR DE EXPEDICIÓN:", _FNormalBold))
        tablaDatos.AddCell(New Phrase(_comprobante.LugarExpedicion, _FNormal))
        tablaDatos.WriteSelectedRows(0, -1, 382, 778, _writer.DirectContent)

        'Datos de la factura
        'Paragraph p2 = new Paragraph();
        'p2.IndentationLeft = 403f;

        'p2.SpacingAfter = 18;
        'p2.Leading = 10;
        'p2.Alignment = Element.ALIGN_CENTER;

        ' _documento.Add(p2);

        '/_cb.SaveState();
        '/_cb.BeginText();
        '/_cb.MoveText(1, 1);
        '/_cb.SetFontAndSize(_fuenteTitulos, 8);
        '/_cb.ShowText("Faustino Rojas Arelano");
        '/_cb.EndText();
        '/_cb.RestoreState();

        'PdfContentByte cb = _writer.DirectContent;

        'cb.BeginText();
        'cb.SetFontAndSize(_fuenteTitulos, 8);
        'ColumnText.ShowTextAligned(cb, Element.ALIGN_CENTER, p2, 300, 500,0);
        'cb.EndText();
    End Sub

    Private Sub AgregarCfdiRelacionados()
        If _comprobante.CfdiRelacionados.CfdiRelacionado.Count <= 0 Then
            Return
        End If
        Dim anchoColumnas As Single() = {350.0F, 250.0F}
        Dim tablaCfdisRelacionados As New PdfPTable(anchoColumnas)
        tablaCfdisRelacionados.SetTotalWidth(anchoColumnas)
        tablaCfdisRelacionados.SpacingBefore = 7.0F
        tablaCfdisRelacionados.LockedWidth = True

        Dim ctitulo As New PdfPCell(New Phrase("CFDI´S RELACIONADOS", New Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.WHITE)))
        ctitulo.HorizontalAlignment = Element.ALIGN_CENTER
        ctitulo.VerticalAlignment = Element.ALIGN_CENTER
        ctitulo.Colspan = 2
        ctitulo.BackgroundColor = BaseColor.BLACK
        tablaCfdisRelacionados.AddCell(ctitulo)

        Dim anchoColRelacion As Single() = {250.0F}
        Dim tablaTipoRelacion As New PdfPTable(anchoColRelacion)
        tablaTipoRelacion.SpacingBefore = 0.0F
        tablaTipoRelacion.DefaultCell.Border = Rectangle.NO_BORDER
        tablaTipoRelacion.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER
        tablaTipoRelacion.AddCell(New Phrase("Tipo de relación", _FNormalBold))
        tablaTipoRelacion.AddCell(New Phrase(Convert.ToString(_comprobante.CfdiRelacionados.TipoRelacion + " - ") & getTipoRelacion(_comprobante.CfdiRelacionados.TipoRelacion), _FNormal))
        tablaCfdisRelacionados.AddCell(tablaTipoRelacion)

        Dim anchocol As Single() = {250.0F}
        Dim tablacfdis As New PdfPTable(anchocol)
        tablacfdis.DefaultCell.Border = Rectangle.NO_BORDER
        tablacfdis.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER
        tablacfdis.SpacingBefore = 0.0F
        tablacfdis.AddCell(New Phrase("Folio fiscal", _FNormalBold))

        For Each item As CfdiRelacionado In _comprobante.CfdiRelacionados.CfdiRelacionado
            tablacfdis.AddCell(New Phrase(item.UUID, _FNormal))
        Next

        tablaCfdisRelacionados.AddCell(tablacfdis)
        _documento.Add(tablaCfdisRelacionados)

    End Sub

    Private Sub AgregarPagos10()
        If _comprobante.Complemento.Pagos.pagos.Count <= 0 Then
            Return
        End If
        Dim anchoColumnas As Single() = {600.0F}
        Dim tComplemento As New PdfPTable(anchoColumnas)
        tComplemento.SetTotalWidth(anchoColumnas)
        tComplemento.SpacingBefore = 7.0F
        tComplemento.LockedWidth = True

        Dim ctitulo As New PdfPCell(New Phrase("Complemento pagos V-" + _comprobante.Complemento.Pagos.Version, New Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.WHITE)))
        ctitulo.HorizontalAlignment = Element.ALIGN_CENTER
        ctitulo.VerticalAlignment = Element.ALIGN_CENTER
        ctitulo.BackgroundColor = BaseColor.BLACK
        tComplemento.AddCell(ctitulo)


        Dim acPagos As Single() = {100.0F, 100.0F, 100.0F, 100.0F, 100.0F, 100.0F}
        Dim tPagos As New PdfPTable(acPagos)
        tPagos.DefaultCell.Border = Rectangle.NO_BORDER
        tPagos.SetTotalWidth(acPagos)
        tPagos.SpacingBefore = 7.0F


        For Each pago As Pago In _comprobante.Complemento.Pagos.pagos
            If pago.FechaPago <> String.Empty Then
                tPagos.AddCell(New Paragraph("Fecha de pago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.FechaPago, _FNormal))
            End If
            If pago.FormaDePagoP <> String.Empty Then
                tPagos.AddCell(New Paragraph("Forma de pago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.FormaDePagoP, _FNormal))
            End If
            If pago.MonedaP <> String.Empty Then
                tPagos.AddCell(New Paragraph("Moneda:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.MonedaP, _FNormal))
            End If
            If pago.TipoCambioP <> 0 Then
                tPagos.AddCell(New Paragraph("Tipo de cambio:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.TipoCambioP.ToString("C2"), _FNormal))
            End If
            If pago.Monto <> 0 Then
                tPagos.AddCell(New Paragraph("Monto:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.Monto.ToString("C2"), _FNormal))
            End If
            If pago.NumOperacion <> String.Empty Then
                tPagos.AddCell(New Paragraph("Núm. de operación:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.NumOperacion, _FNormal))
            End If
            If pago.RfcEmisorCtaOrd <> String.Empty Then
                tPagos.AddCell(New Paragraph("RFC Emisor Cta:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.RfcEmisorCtaOrd, _FNormal))
            End If
            If pago.NomBancoOrdExt <> String.Empty Then
                tPagos.AddCell(New Paragraph("NomBancoOrdExt:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.NomBancoOrdExt, _FNormal))
            End If
            If pago.CtaOrdenante <> String.Empty Then
                tPagos.AddCell(New Paragraph("CtaOrdenante:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.CtaOrdenante, _FNormal))
            End If
            If pago.RfcEmisorCtaOrd <> String.Empty Then
                tPagos.AddCell(New Paragraph("RFC Emisor Cta:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.RfcEmisorCtaOrd, _FNormal))
            End If
            If pago.RfcEmisorCtaBen <> String.Empty Then
                tPagos.AddCell(New Paragraph("RfcEmisorCtaBen:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.RfcEmisorCtaBen, _FNormal))
            End If

            If pago.CtaBeneficiario <> String.Empty Then
                tPagos.AddCell(New Paragraph("CtaBeneficiario:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.CtaBeneficiario, _FNormal))
            End If

            If pago.TipoCadPago <> String.Empty Then
                tPagos.AddCell(New Paragraph("TipoCadPago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.TipoCadPago, _FNormal))
            End If

            If pago.CertPago <> String.Empty Then
                tPagos.AddCell(New Paragraph("CertPago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.CertPago, _FNormal))
            End If
            If pago.CadPago <> String.Empty Then
                tPagos.AddCell(New Paragraph("CadPago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.CadPago, _FNormal))
            End If
            If pago.SelloPago <> String.Empty Then
                tPagos.AddCell(New Paragraph("SelloPago:", _FNormalBold))
                tPagos.AddCell(New Paragraph(pago.SelloPago, _FNormal))
            End If
            tPagos.CompleteRow()

            '#Region "Doctos relacionados"
            If pago.DoctoRelacionado.Count > 0 Then
                Dim cdr As New PdfPCell(New Paragraph("- Documentos relacionados", _FNormalBold))
                cdr.Border = Rectangle.NO_BORDER
                cdr.Colspan = 3
                tPagos.AddCell(cdr)
                tPagos.CompleteRow()
            End If


            For Each dr As DoctoRelacionado In pago.DoctoRelacionado

                If dr.IdDocumento <> String.Empty Then
                    tPagos.AddCell(New Paragraph("IdDocumento:", _FNormalBold))
                    Dim cid As New PdfPCell(New Paragraph(dr.IdDocumento, _FNormalBold))
                    cid.Colspan = 3
                    cid.Border = Rectangle.NO_BORDER
                    tPagos.AddCell(cid)
                End If

                If dr.Serie <> String.Empty OrElse dr.Folio <> String.Empty Then
                    tPagos.AddCell(New Paragraph("Folio:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.Serie + " " + dr.Folio, _FNormal))
                End If

                If dr.MonedaDR <> String.Empty Then
                    tPagos.AddCell(New Paragraph("Moneda:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.MonedaDR, _FNormal))
                End If
                If dr.TipoCambioDR <> String.Empty Then
                    tPagos.AddCell(New Paragraph("Tipo de cambio:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.TipoCambioDR, _FNormal))
                End If
                If dr.NumParcialidad <> String.Empty Then
                    tPagos.AddCell(New Paragraph("Núm. de parcialidad:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.NumParcialidad, _FNormal))
                End If
                If dr.ImpSaldoAnt <> 0 Then
                    tPagos.AddCell(New Paragraph("Saldo anterior:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.ImpSaldoAnt.ToString("C2"), _FNormal))
                End If
                If dr.ImpPagado <> 0 Then
                    tPagos.AddCell(New Paragraph("Importe pagado:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.ImpPagado.ToString("C2"), _FNormal))
                End If
                If dr.ImpSaldoInsoluto <> 0 Then
                    tPagos.AddCell(New Paragraph("Saldo insoluto:", _FNormalBold))
                    tPagos.AddCell(New Paragraph(dr.ImpSaldoInsoluto.ToString("C2"), _FNormal))
                End If
                tPagos.CompleteRow()
            Next
            tComplemento.AddCell(tPagos)

            '#End Region

            'If pago.Impuestos.Count > 0 Then
            '    Dim cdr As New PdfPCell(New Paragraph("- Impuestos:", _FNormalBold))
            '    cdr.Colspan = 3
            '    tPagos.AddCell(cdr)
            '    tPagos.CompleteRow()
            'End If
            'For Each ip As Impuestos In pago.Impuestos
            '    For Each r As Retencion In ip.Retenciones
            '        If r.Impuesto <> String.Empty Then
            '            tPagos.AddCell(New Paragraph("Impuesto:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(r.Impuesto, _FNormal))
            '        End If
            '        If r.Importe <> 0 Then
            '            tPagos.AddCell(New Paragraph("Importe:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(r.Importe.ToString("C2"), _FNormal))
            '        End If
            '        tPagos.CompleteRow()
            '    Next
            '    For Each t As Traslado In ip.Traslados
            '        If t.Impuesto <> String.Empty Then
            '            tPagos.AddCell(New Paragraph("Impuesto:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(t.Impuesto, _FNormal))
            '        End If
            '        If t.TipoFactor <> String.Empty Then
            '            tPagos.AddCell(New Paragraph("Tipo factor:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(t.TipoFactor, _FNormal))
            '        End If
            '        If t.TasaOCuota <> 0 Then
            '            tPagos.AddCell(New Paragraph("Tasa:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(t.TasaOCuota.ToString("C2"), _FNormal))
            '        End If
            '        If t.Importe <> 0 Then
            '            tPagos.AddCell(New Paragraph("Importe:", _FNormalBold))
            '            tPagos.AddCell(New Paragraph(t.Importe.ToString("C2"), _FNormal))
            '        End If
            '        tPagos.CompleteRow()
            '    Next

            '    If ip.TotalImpuestosRetenidos > 0 Then
            '        tPagos.AddCell(New Paragraph("Total impuestos retenidos:", _FNormalBold))
            '        tPagos.AddCell(New Paragraph(ip.TotalImpuestosRetenidos.ToString("C2"), _FNormal))
            '        tPagos.CompleteRow()
            '    End If
            '    If ip.TotalImpuestosTrasladados > 0 Then
            '        tPagos.AddCell(New Paragraph("Total impuestos trasladados:", _FNormalBold))
            '        tPagos.AddCell(New Paragraph(ip.TotalImpuestosTrasladados.ToString("C2"), _FNormal))
            '        tPagos.CompleteRow()

            '    End If
            'Next
        Next

        _documento.Add(tComplemento)

    End Sub

    Private Sub AgregarDatosProductos()

        Dim anchoColumnasTablaProductos As Single() = {600.0F}
        Dim tablaProductosPrincipal As New PdfPTable(anchoColumnasTablaProductos)
        'tablaProductosPrincipal.DefaultCell.Border = Rectangle.NO_BORDER;
        tablaProductosPrincipal.SetTotalWidth(anchoColumnasTablaProductos)
        tablaProductosPrincipal.SpacingBefore = 7.0F
        tablaProductosPrincipal.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductosPrincipal.LockedWidth = True


        'Datos de los productos
        Dim tamanoColumnas As Single() = {50.0F, 60.0F, 310.0F, 90.0F, 90.0F}
        Dim tablaProductosTitulos As New PdfPTable(tamanoColumnas)
        'tablaProductosTitulos.DefaultCell.Border = Rectangle.NO_BORDER;
        tablaProductosTitulos.SetTotalWidth(tamanoColumnas)
        tablaProductosTitulos.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductosTitulos.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductosTitulos.LockedWidth = True

        Dim ctitulo As New PdfPCell(New Phrase("CONCEPTOS", New Font(Font.FontFamily.HELVETICA, 7, Font.NORMAL, BaseColor.WHITE)))
        ctitulo.HorizontalAlignment = Element.ALIGN_CENTER
        ctitulo.VerticalAlignment = Element.ALIGN_CENTER
        ctitulo.Colspan = 5
        ctitulo.BackgroundColor = BaseColor.BLACK
        tablaProductosTitulos.AddCell(ctitulo)

        tablaProductosTitulos.AddCell(New Phrase("Cantidad", _FNormalBold))
        tablaProductosTitulos.AddCell(New Phrase("Unidad", _FNormalBold))
        tablaProductosTitulos.AddCell(New Phrase("Descripción", _FNormalBold))
        tablaProductosTitulos.AddCell(New Phrase("Precion unitario", _FNormalBold))
        tablaProductosTitulos.AddCell(New Phrase("Importe", _FNormalBold))



        Dim tamanoColumnasProductos As Single() = {50.0F, 60.0F, 310.0F, 90.0F, 90.0F}
        Dim tablaProductos As New PdfPTable(tamanoColumnas)
        'tablaProductos.SpacingBefore = 1;
        'tablaProductos.DefaultCell.Border = Rectangle.NO_BORDER;
        tablaProductos.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductos.DefaultCell.BorderWidthLeft = 0.1F
        tablaProductos.DefaultCell.BorderWidthRight = 0.0F
        tablaProductos.DefaultCell.BorderWidthBottom = 0.0F
        tablaProductos.DefaultCell.BorderWidthTop = 0.0F
        tablaProductos.SetTotalWidth(tamanoColumnas)
        tablaProductos.HorizontalAlignment = Element.ALIGN_LEFT
        tablaProductos.LockedWidth = True


        For Each c As Concepto In _comprobante.Conceptos
            Dim descripcion As New StringBuilder()

            tablaProductos.AddCell(New Phrase(c.Cantidad.ToString(), _fMediana))
            tablaProductos.AddCell(New Phrase(Convert.ToString(c.ClaveUnidad + " - ") & getUnidad(c.ClaveUnidad), _fMediana))

            descripcion.Append(c.Descripcion + vbLf + "Clave Prod. Serv. " + c.ClaveProdServ)
            descripcion.Append(vbLf)
            If c.NoIdentificacion <> String.Empty Then
                descripcion.Append("No. Identificación - " + c.NoIdentificacion + vbLf)
            End If
            If c.Impuestos.Retenciones.Count > 0 OrElse c.Impuestos.Traslados.Count > 0 Then
                descripcion.Append("Impuestos:" & vbLf)
                If c.Impuestos.Traslados.Count > 0 Then
                    descripcion.Append("  Traslados:" & vbLf)
                    For Each t As TrasladoC In c.Impuestos.Traslados
                        If t.TipoFactor = "Exento" Then
                            descripcion.Append((Convert.ToString("    " + t.Impuesto + " ") & getImpuesto(t.Impuesto)) + " Base -" + t.Base.ToString("C2") + " TipoFactor -" + t.TipoFactor.ToString() + " Impuesto -" + t.Impuesto.ToString() + vbLf)
                        Else
                            descripcion.Append((Convert.ToString("    " + t.Impuesto + " ") & getImpuesto(t.Impuesto)) + " Base -" + t.Base.ToString("C2") + " Tasa -" + t.TasaOCuota.ToString("F6") + " Importe -" + t.Importe.ToString("C2") + vbLf)
                        End If

                    Next
                End If
                If c.Impuestos.Retenciones.Count > 0 Then
                    For Each r As RetencionC In c.Impuestos.Retenciones
                        descripcion.Append("  Retenciones:" & vbLf)
                        descripcion.Append((Convert.ToString("    " + r.Impuesto + " ") & getImpuesto(r.Impuesto)) + " Base " + r.Base.ToString("C2") + " Tasa " + r.TasaOCuota.ToString("F6") + " Importe " + r.Importe.ToString("C2") + vbLf)
                    Next
                End If
            End If
            tablaProductos.AddCell(New Phrase(descripcion.ToString(), _fMediana))
            'tablaProductos.AddCell(new Phrase(c.ValorUnitario, _fMediana));
            'tablaProductos.AddCell(new Phrase(c.Importe, _fMediana));

            Dim celdaValorUnitario As New PdfPCell(New Phrase(c.ValorUnitario.ToString("C2"), _fMediana))
            celdaValorUnitario.HorizontalAlignment = Element.ALIGN_RIGHT
            celdaValorUnitario.BorderWidthLeft = 0.1F
            celdaValorUnitario.BorderWidthRight = 0.0F
            celdaValorUnitario.BorderWidthBottom = 0.0F
            celdaValorUnitario.BorderWidthTop = 0.0F
            'valorUnitario.Border = Rectangle.NO_BORDER;
            tablaProductos.AddCell(celdaValorUnitario)

            Dim importe As New PdfPCell(New Phrase(c.Importe.ToString("C2"), _fMediana))
            importe.HorizontalAlignment = Element.ALIGN_RIGHT
            importe.HorizontalAlignment = Element.ALIGN_RIGHT
            importe.BorderWidthLeft = 0.1F
            importe.BorderWidthRight = 0.0F
            importe.BorderWidthBottom = 0.0F
            importe.BorderWidthTop = 0.0F

            'tablaProductos.AddCell(new Phrase(p.importe, new Font(Font.FontFamily.HELVETICA, 8)));

            tablaProductos.AddCell(importe)
        Next


        Dim celdaTitulos As New PdfPCell(tablaProductosTitulos)
        tablaProductosPrincipal.AddCell(celdaTitulos)
        Dim celdaProductos As New PdfPCell(tablaProductos)
        If _comprobante.TipoDeComprobante = "I" OrElse _comprobante.TipoDeComprobante = "E" Then
            celdaProductos.MinimumHeight = 300
        End If
        tablaProductosPrincipal.AddCell(celdaProductos)
        'tablaProductosPrincipal.WriteSelectedRows(0, -1, 10, 600, _writer.DirectContent);
        _documento.Add(tablaProductosPrincipal)
    End Sub

    Private Sub AgregarTotales()

        Dim anchoColumasTablaTotales As Single() = {400.0F, 200.0F}
        Dim tablaTotales As New PdfPTable(anchoColumasTablaTotales)
        tablaTotales.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaTotales.DefaultCell.Border = Rectangle.NO_BORDER
        tablaTotales.SetTotalWidth(anchoColumasTablaTotales)
        tablaTotales.HorizontalAlignment = Element.ALIGN_CENTER
        tablaTotales.LockedWidth = True

        Dim anchoColumnas As Single() = {130, 70.0F}
        Dim tablaImportes As New PdfPTable(anchoColumnas)
        tablaImportes.DefaultCell.Border = Rectangle.NO_BORDER
        tablaImportes.SetTotalWidth(anchoColumnas)
        tablaImportes.DefaultCell.HorizontalAlignment = Element.ALIGN_RIGHT
        'tablaImportes.HorizontalAlignment = Element.ALIGN_RIGHT;
        tablaImportes.LockedWidth = True

        tablaImportes.AddCell(New Phrase("SUBTOTAL:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaImportes.AddCell(New Phrase(_comprobante.Subtotal.ToString("C"), New Font(Font.FontFamily.HELVETICA, 8)))

        If _comprobante.Descuento > 0 Then
            tablaImportes.AddCell(New Phrase("DESCUENTO:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        End If


        If _comprobante.Impuestos.Traslados.Count > 0 Then
            For Each t As Traslado In _comprobante.Impuestos.Traslados

                If t.TipoFactor = "Exento" Then

                Else
                    tablaImportes.AddCell(New Phrase((Convert.ToString("TRASLADO ") & getImpuesto(t.Impuesto)) + " TASA " + t.TasaOCuota.ToString("F6"), _FNormalBold))
                    tablaImportes.AddCell(New Phrase(t.Importe.ToString("C2"), _FNormal))

                End If

                'tablaImportes.AddCell(New Phrase((Convert.ToString("TRASLADO ") & getImpuesto(t.Impuesto)) + " TASA " + t.TasaOCuota.ToString("F6"), _FNormalBold))
                'tablaImportes.AddCell(New Phrase(t.Importe.ToString("C2"), _FNormal))
            Next
        End If
        If _comprobante.Impuestos.Retenciones.Count > 0 Then
            For Each r As Retencion In _comprobante.Impuestos.Retenciones
                tablaImportes.AddCell(New Phrase(Convert.ToString("RETENCIÓN ") & getImpuesto(r.Impuesto), _FNormalBold))
                tablaImportes.AddCell(New Phrase(r.Importe.ToString("C2"), _FNormal))
            Next
        End If

        tablaImportes.AddCell(New Phrase("TOTAL:", New Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)))
        tablaImportes.AddCell(New Phrase(_comprobante.Total.ToString("C2"), New Font(Font.FontFamily.HELVETICA, 8)))

        Dim columnasLetra As Single() = {120, 280}
        Dim tablaImporteLetra As New PdfPTable(columnasLetra)
        tablaImporteLetra.DefaultCell.Border = Rectangle.NO_BORDER
        tablaImporteLetra.SetTotalWidth(columnasLetra)
        tablaImporteLetra.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        'tablaImportes.HorizontalAlignment = Element.ALIGN_RIGHT;
        tablaImporteLetra.LockedWidth = True


        tablaImporteLetra.AddCell(New Phrase("IMPORTE CON LETRA", _FNormalBold))
        tablaImporteLetra.AddCell(New Phrase(_comprobante.TotalLetra, _FNormal))

        tablaImporteLetra.AddCell(New Phrase("TIPO DE COMPROBANTE", _FNormalBold))
        tablaImporteLetra.AddCell(New Phrase(Convert.ToString(_comprobante.TipoDeComprobante + " ") & getTipoComprobante(_comprobante.TipoDeComprobante), _FNormal))

        If _comprobante.FormaPago <> String.Empty Then
            tablaImporteLetra.AddCell(New Phrase("FORMA DE PAGO", _FNormalBold))
            tablaImporteLetra.AddCell(New Phrase(Convert.ToString(_comprobante.FormaPago + " ") & getFormaPago(_comprobante.FormaPago), _FNormal))
        End If

        If _comprobante.MetodoPago <> String.Empty Then
            tablaImporteLetra.AddCell(New Phrase("MÉTODO DE PAGO", _FNormalBold))
            tablaImporteLetra.AddCell(New Phrase(Convert.ToString(_comprobante.MetodoPago + " ") & getMetodoPago(_comprobante.MetodoPago), _FNormal))
        End If

        tablaImporteLetra.AddCell(New Phrase("MONEDA", _FNormalBold))
        tablaImporteLetra.AddCell(New Phrase(Convert.ToString(_comprobante.Moneda + " ") & getMoneda(_comprobante.Moneda), _FNormal))


        'valorUnitario.Border = Rectangle.NO_BORDER;

        'foreach (ImpuestoCFD i in _comprobante.Impuestos)
        '{
        '    tablaImportes.AddCell(new Phrase(i.impuesto + " " + i.tasa.ToString("F2") + "%:", new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)));
        '    tablaImportes.AddCell(new Phrase(i.importe.ToString("C"), new Font(Font.FontFamily.HELVETICA, 8)));
        '}

        'foreach (RetencionCFD i in _comprobante.retenciones)
        '{
        '    tablaImportes.AddCell(new Phrase("Retencion " + i.impuesto + ": ", new Font(Font.FontFamily.HELVETICA, 8, Font.BOLD)));
        '    tablaImportes.AddCell(new Phrase(i.importe.ToString("C"), new Font(Font.FontFamily.HELVETICA, 8)));
        '}



        tablaTotales.AddCell(tablaImporteLetra)
        tablaTotales.AddCell(tablaImportes)
        _documento.Add(tablaTotales)
    End Sub

    Private Sub Agregarobservaciones(dobservaciones As String)



        If dobservaciones Is Nothing Then
            Return
        End If
        Dim pp As New Paragraph()
        pp.IndentationLeft = 150.0F
        pp.IndentationRight = 250.0F

        pp.Alignment = Element.ALIGN_CENTER
        pp.Add(New Phrase("OBSERVACIONES", _fTitulo))
        pp.Add(vbLf)
        pp.Add(New Phrase(dobservaciones, _FNormal))

        'Agrego la imagen al documento
        'Dim imagen As Image = Image.GetInstance(logoEmpresa, BaseColor.BLACK)
        'imagen.ScaleToFit(140, 140)
        'imagen.Alignment = Element.ALIGN_RIGHT
        'Dim logo As New Chunk(imagen, 1, -115)
        _documento.Add(pp)
    End Sub




    Private Sub AgregarSellos()
        Dim cadenaOriginal As New StringBuilder()
        cadenaOriginal.Append("||")
        cadenaOriginal.Append("1.1|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.UUID + "|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.FechaTimbrado + "|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.SelloCFD + "|")
        cadenaOriginal.Append(_comprobante.Complemento.TimbreFiscalDigital.NoCertificadoSAT + "||")

        Dim anchoColumnas As Single() = {100.0F, 480.0F}
        Dim tablaSellosQR As New PdfPTable(anchoColumnas)
        tablaSellosQR.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaSellosQR.SpacingBefore = 10.0F
        tablaSellosQR.DefaultCell.Border = Rectangle.NO_BORDER
        tablaSellosQR.SetTotalWidth(anchoColumnas)
        'tablaSellosQR.HorizontalAlignment = Element.ALIGN_CENTER;
        tablaSellosQR.LockedWidth = True

        Dim anchoColumnas1 As Single() = {480}
        Dim tablaSellos As New PdfPTable(anchoColumnas1)
        tablaSellos.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT
        tablaSellos.DefaultCell.VerticalAlignment = Element.ALIGN_TOP
        tablaSellos.DefaultCell.Border = Rectangle.NO_BORDER
        tablaSellos.SetTotalWidth(anchoColumnas1)
        tablaSellos.HorizontalAlignment = Element.ALIGN_CENTER
        'tablaSellos.LockedWidth = true;
        tablaSellos.AddCell(New Phrase("SELLO DIGITAL DEL CFDI:", New Font(Font.FontFamily.HELVETICA, 7, Font.BOLD)))
        tablaSellos.AddCell(New Phrase(_comprobante.Complemento.TimbreFiscalDigital.SelloCFD, New Font(Font.FontFamily.HELVETICA, 7)))
        tablaSellos.AddCell(New Phrase("SELLO DIGITAL DEL SAT", New Font(Font.FontFamily.HELVETICA, 7, Font.BOLD)))
        tablaSellos.AddCell(New Phrase(_comprobante.Sello, New Font(Font.FontFamily.HELVETICA, 7)))
        tablaSellos.AddCell(New Phrase("CADENA ORIGINAL DEL COMPLEMENTO DE CERTIFICACIÓN DIGITAL DEL SAT:", New Font(Font.FontFamily.HELVETICA, 7, Font.BOLD)))
        tablaSellos.AddCell(New Phrase(cadenaOriginal.ToString(), New Font(Font.FontFamily.HELVETICA, 7)))

        'Agregamos el codigo QR al documento
        Dim codigoQR As New StringBuilder()
        codigoQR.Append("https://verificacfdi.facturaelectronica.sat.gob.mx/default.aspx")
        'La URL del acceso al servicio que pueda mostrar los datos de la versión pública del comprobante.
        codigoQR.Append("?id=" + _comprobante.Complemento.TimbreFiscalDigital.UUID)
        'Número de folio fiscal del comprobante (UUID).
        codigoQR.Append("&re=" + _comprobante.Emisor.Rfc)
        'RFC del emisor.
        codigoQR.Append("&rr=" + _comprobante.Receptor.Rfc)
        'RFC del receptor.
        codigoQR.Append("&tt=" + _comprobante.Total.ToString())
        'Total del comprobante.
        Dim cuenta As Integer = _comprobante.Sello.Count()
        cuenta = cuenta - 8
        codigoQR.Append("&fe=" + _comprobante.Sello.Substring(cuenta))
        'Ocho últimos caracteres del sello digital del emisor del comprobante..
        Dim pdfCodigoQR As New BarcodeQRCode(codigoQR.ToString(), 1, 1, Nothing)
        Dim img As Image = pdfCodigoQR.GetImage()
        img.SpacingAfter = 0.0F
        img.SpacingBefore = 0.0F
        img.BorderWidth = 1.0F
        'img.ScalePercent(100, 78);
        'img.border

        tablaSellosQR.AddCell(img)
        tablaSellosQR.AddCell(tablaSellos)


        _documento.Add(tablaSellosQR)
    End Sub

#End Region

End Class

#Region "Extensión de la clase pdfPageEvenHelper"
Public Class ITextEvents
    Inherits PdfPageEventHelper

    ' This is the contentbyte object of the writer
    Private cb As PdfContentByte

    ' we will put the final number of pages in a template
    Private headerTemplate As PdfTemplate, footerTemplate As PdfTemplate

    ' this is the BaseFont we are going to use for the header / footer
    Private bf As BaseFont = Nothing

    ' This keeps track of the creation time
    Private PrintTime As DateTime = DateTime.Now


#Region "Fields"
    Private _header As String
#End Region

#Region "Properties"
    Public Property Header() As String
        Get
            Return _header
        End Get
        Set(value As String)
            _header = value
        End Set
    End Property
#End Region


    Public Overrides Sub OnOpenDocument(writer As PdfWriter, document As Document)
        Try
            PrintTime = DateTime.Now
            bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED)
            cb = writer.DirectContent
            headerTemplate = cb.CreateTemplate(100, 100)
            footerTemplate = cb.CreateTemplate(50, 50)

        Catch de As DocumentException

        Catch ioe As System.IO.IOException
        End Try
    End Sub

    Public Overrides Sub OnEndPage(writer As iTextSharp.text.pdf.PdfWriter, document As iTextSharp.text.Document)
        MyBase.OnEndPage(writer, document)

        'iTextSharp.text.Font baseFontNormal = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 10f, iTextSharp.text.Font.NORMAL, iTextSharp.text.BaseColor.BLACK);

        'iTextSharp.text.Font baseFontBig = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12f, iTextSharp.text.Font.BOLD, iTextSharp.text.BaseColor.BLACK);

        'Phrase p1Header = new Phrase("Sample Header Here", baseFontNormal);

        '/Create PdfTable object
        'PdfPTable pdfTab = new PdfPTable(3);

        '/We will have to create separate cells to include image logo and 2 separate strings
        '/Row 1
        'PdfPCell pdfCell1 = new PdfPCell();
        'PdfPCell pdfCell2 = new PdfPCell(p1Header);
        'PdfPCell pdfCell3 = new PdfPCell();
        Dim text As [String] = "Página " + writer.PageNumber.ToString() + " de "


        '/Add paging to header
        '{
        '    cb.BeginText();
        '    cb.SetFontAndSize(bf, 12);
        '    cb.SetTextMatrix(document.PageSize.GetRight(200), document.PageSize.GetTop(45));
        '    cb.ShowText(text);
        '    cb.EndText();
        '    float len = bf.GetWidthPoint(text, 12);
        '    //Adds "12" in Page 1 of 12
        '    cb.AddTemplate(headerTemplate, document.PageSize.GetRight(200) + len, document.PageSize.GetTop(45));
        '}

        'Add paging to footer
        If True Then
            cb.BeginText()
            cb.SetFontAndSize(bf, 9)
            cb.SetTextMatrix(document.PageSize.GetRight(70), document.PageSize.GetBottom(30))
            'cb.MoveText(500,30);
            'cb.ShowText("Este comprobante es una representación impresa de un CFDI");
            cb.ShowText(text)
            cb.EndText()
            Dim len As Single = bf.GetWidthPoint(text, 9)
            cb.AddTemplate(footerTemplate, document.PageSize.GetRight(70) + len, document.PageSize.GetBottom(30))

            Dim anchoColumasTablaTotales As Single() = {600.0F}
            Dim tabla As New PdfPTable(anchoColumasTablaTotales)
            tabla.DefaultCell.Border = Rectangle.NO_BORDER
            tabla.SetTotalWidth(anchoColumasTablaTotales)
            tabla.HorizontalAlignment = Element.ALIGN_CENTER
            tabla.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER
            tabla.LockedWidth = True
            tabla.AddCell(New Phrase("Este documento es una representación impresa de un CFDI", New Font(Font.FontFamily.HELVETICA, 8)))
            tabla.WriteSelectedRows(0, -1, 5, document.PageSize.GetBottom(40), writer.DirectContent)



        End If



        '/Row 2
        'PdfPCell pdfCell4 = new PdfPCell(new Phrase("Sub Header Description", baseFontNormal));
        '/Row 3


        'PdfPCell pdfCell5 = new PdfPCell(new Phrase("Date:" + PrintTime.ToShortDateString(), baseFontBig));
        'PdfPCell pdfCell6 = new PdfPCell();
        'PdfPCell pdfCell7 = new PdfPCell(new Phrase("TIME:" + string.Format("{0:t}", DateTime.Now), baseFontBig));


        '/set the alignment of all three cells and set border to 0
        'pdfCell1.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell2.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell3.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell4.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell5.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell6.HorizontalAlignment = Element.ALIGN_CENTER;
        'pdfCell7.HorizontalAlignment = Element.ALIGN_CENTER;


        'pdfCell2.VerticalAlignment = Element.ALIGN_BOTTOM;
        'pdfCell3.VerticalAlignment = Element.ALIGN_MIDDLE;
        'pdfCell4.VerticalAlignment = Element.ALIGN_TOP;
        'pdfCell5.VerticalAlignment = Element.ALIGN_MIDDLE;
        'pdfCell6.VerticalAlignment = Element.ALIGN_MIDDLE;
        'pdfCell7.VerticalAlignment = Element.ALIGN_MIDDLE;


        'pdfCell4.Colspan = 3;



        'pdfCell1.Border = 0;
        'pdfCell2.Border = 0;
        'pdfCell3.Border = 0;
        'pdfCell4.Border = 0;
        'pdfCell5.Border = 0;
        'pdfCell6.Border = 0;
        'pdfCell7.Border = 0;


        '/add all three cells into PdfTable
        'pdfTab.AddCell(pdfCell1);
        'pdfTab.AddCell(pdfCell2);
        'pdfTab.AddCell(pdfCell3);
        'pdfTab.AddCell(pdfCell4);
        'pdfTab.AddCell(pdfCell5);
        'pdfTab.AddCell(pdfCell6);
        'pdfTab.AddCell(pdfCell7);

        'pdfTab.TotalWidth = document.PageSize.Width - 80f;
        'pdfTab.WidthPercentage = 70;
        '/pdfTab.HorizontalAlignment = Element.ALIGN_CENTER;


        '/call WriteSelectedRows of PdfTable. This writes rows from PdfWriter in PdfTable
        '/first param is start row. -1 indicates there is no end row and all the rows to be included to write
        '/Third and fourth param is x and y position to start writing
        'pdfTab.WriteSelectedRows(0, -1, 40, document.PageSize.Height - 30, writer.DirectContent);
        '/set pdfContent value

        '/Move the pointer and draw line to separate header section from rest of page
        'cb.MoveTo(40, document.PageSize.Height - 100);
        'cb.LineTo(document.PageSize.Width - 40, document.PageSize.Height - 100);
        'cb.Stroke();

        'Move the pointer and draw line to separate footer section from rest of page
        'cb.MoveTo(40, document.PageSize.GetBottom(50));
        'cb.LineTo(document.PageSize.Width - 40, document.PageSize.GetBottom(50));
        'cb.Stroke();
    End Sub

    Public Overrides Sub OnCloseDocument(writer As PdfWriter, document As Document)
        MyBase.OnCloseDocument(writer, document)

        'headerTemplate.BeginText();
        'headerTemplate.SetFontAndSize(bf, 12);
        'headerTemplate.SetTextMatrix(0, 0);
        'headerTemplate.ShowText((writer.PageNumber - 1).ToString());
        'headerTemplate.EndText();

        footerTemplate.BeginText()
        footerTemplate.SetFontAndSize(bf, 9)
        'footerTemplate.MoveText(550,30);
        footerTemplate.SetTextMatrix(0, 0)
        footerTemplate.ShowText((writer.PageNumber - 1).ToString())
        footerTemplate.EndText()


    End Sub
End Class
#End Region

