Imports System
Imports System.Collections.Generic
Imports System.Linq
Imports System.Text
Imports System.Threading.Tasks
Imports System.Xml
Imports System.Xml.Schema
Imports System.Xml.XPath
Imports System.Xml.Xsl
Imports System.IO
Imports System.Windows.Forms
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates



Public Class CrearXML
    Inherits System.Web.UI.Page
    'AB SAT4.0
#Region "variablesGlobales"
    'Inherits System.Web.UI.Page
    Public Const _NamespaceCfdi As String = "http://www.sat.gob.mx/cfd/4"
    Private Const _NamespaceXsi As String = "http://www.w3.org/2001/XMLSchema-instance"
    Private Const _SchemaLocation As String = "http://www.sat.gob.mx/cfd/4 http://www.sat.gob.mx/sitio_internet/cfd/4/cfdv40.xsd"
    Public Const _NamespaceCfdiPagos As String = "http://www.sat.gob.mx/Pagos"
    ' Private Const _ShemaLocationPagos As String = "http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd"
    'Private Const _ShemaLocationPagos As String = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd"
    Private Const _ShemaLocationPagos As String = "http://www.sat.gob.mx/cfd/3 http://www.sat.gob.mx/sitio_internet/cfd/3/cfdv33.xsd http://www.sat.gob.mx/Pagos http://www.sat.gob.mx/sitio_internet/cfd/Pagos/Pagos10.xsd"

    ' Private Const _rutaXslt As String = "\XSLT\cadenaoriginal_3_3.xslt"
    'Dim apfx As New String(Server.MapPath("/FILESSAT/fmd020730pq5_1307161754s.pfx"))
    'Private Const _rutaXslt As String(Server.MapPath("/FILESSAT/CSD_MATRIZ_FMD020730PQ5_20170713_165106s.cer"))


    'Private Const rutaCertificado As String = "\FILESSAT\CSD_MATRIZ_FMD020730PQ5_20170713_165106s.cer"
    'Esta variable de nivel de modulo nos facilita las operaciones en las demas subrutinas
    Private Shared m_xmlDOM As New XmlDocument()
    Private _comprobante As Comprobante
#End Region

    Public Sub Create(c As Comprobante, rutaXML As String, rutaPFX As String, passwordPFX As String, rutaCertificado As String)
        'Dim rutaCertificado As String = "\FILESSAT\CSD_MATRIZ_FMD020730PQ5_20170713_165106s.cer"
        Try
            _comprobante = c
            ObtenerCerticicadoYNoCertificado(rutaCertificado, _comprobante.Certificado, _comprobante.NoCertificado)

            Dim xmlComprobante As XmlElement

            'Inicializamos la variable para que contenga el DOM del CFD
            m_xmlDOM = CrearDOM()


            'creamos el nodo raiz "Comprobante"
            xmlComprobante = CrearNodoComprobante()
            m_xmlDOM.AppendChild(xmlComprobante)
            IndentarNodo(xmlComprobante)
            xmlComprobante.SetAttribute("Sello", ObtenerSello1(rutaPFX, passwordPFX))

            ' _comprobante.Sello = ObtenerSello(rutaPFX, passwordPFX)

            '/Agregamos al XML el nodo Emisor
            'CrearNodoEmisor(xmlComprobante);
            'IndentarNodo(xmlComprobante);

            '/Agregamos al XML el nodo Receptor
            'CrearNodoReceptor(xmlComprobante);
            'IndentarNodo(xmlComprobante);

            '/Agregamos al XML el nodo Conceptos
            'CrearNodoConceptos(xmlComprobante);
            'IndentarNodo(xmlComprobante);

            '/Agregamos al XML el nodo impuestos
            'CrearNodoImpuestos(xmlComprobante);
            'IndentarNodo(xmlComprobante);

            'quitando esta linea no agrega ni certificado ni sello
            'SellarCFD(xmlComprobante, rutaPFX, passwordPFX, rutaCertificado);

            m_xmlDOM.InnerXml = m_xmlDOM.InnerXml.Replace("schemaLocation", "xsi:schemaLocation").ToString()
            'm_xmlDOM.InnerXml = m_xmlDOM.InnerXml.Replace("cfdi", "cfdi:").ToString();
            m_xmlDOM.Save(rutaXML)
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
    End Sub

#Region "Funciones para generar XML"
    Private Shared Function CrearDOM() As XmlDocument


        Dim oDOM As New XmlDocument()
        Try
            Dim Nodo As XmlNode
            Nodo = oDOM.CreateProcessingInstruction("xml", "version='1.0' encoding='utf-8'")
            oDOM.AppendChild(Nodo)
            Nodo = Nothing
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
        Return oDOM


    End Function

    Private Function CrearNodoComprobante() As XmlElement


        Dim Comprobante As XmlElement

        'Comprobante = m_xmlDOM.CreateElement("Comprobante");
        Comprobante = m_xmlDOM.CreateElement("cfdi", "Comprobante", _NamespaceCfdi)
        CrearAtributosComprobante(Comprobante)
        'Agregar CFDIS RELACIONADOS

        Comprobante.AppendChild(CrearNodoEmisor())
        Comprobante.AppendChild(CrearNodoReceptor())
        Try
            If CrearNodoConceptos() IsNot Nothing Then
                Comprobante.AppendChild(CrearNodoConceptos())
            End If
            If CrearNodoImpuestos() IsNot Nothing Then
                Comprobante.AppendChild(CrearNodoImpuestos())
            End If
            'If _comprobante.TipoDeComprobante = "E" Then Comprobante.AppendChild(CrearNodoCfdiRelacionados())
            If _comprobante.TipoDeComprobante = "P" Then Comprobante.AppendChild(CrearNodoPagos())

        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try

        Return Comprobante



    End Function
    Private Function CrearNodoPagos() As XmlElement

        Dim complemento As XmlElement
        complemento = m_xmlDOM.CreateElement("cfdi", "Complemento", _NamespaceCfdi)
        Dim Pagos As XmlElement = m_xmlDOM.CreateElement("pago10", "Pagos", _NamespaceCfdiPagos)
        'Pagos.SetAttribute("xsi:schemaLocation", _ShemaLocationNomina)
        Pagos.SetAttribute("Version", _comprobante.Complemento.Pagos.Version)
        If _comprobante.Complemento.Pagos.Pagos.Count > 0 Then
            For Each pago As Pago In _comprobante.Complemento.Pagos.Pagos
                Dim xpago As XmlElement = m_xmlDOM.CreateElement("pago10", "Pago", _NamespaceCfdiPagos)
                If pago.FechaPago <> String.Empty Then xpago.SetAttribute("FechaPago", pago.FechaPago)
                If pago.FormaDePagoP <> String.Empty Then xpago.SetAttribute("FormaDePagoP", pago.FormaDePagoP)
                If pago.MonedaP <> String.Empty Then xpago.SetAttribute("MonedaP", pago.MonedaP)
                If pago.TipoCambioP <> 0 Then xpago.SetAttribute("TipoCambioP", pago.TipoCambioP.ToString("F2"))
                xpago.SetAttribute("Monto", pago.Monto.ToString("F2"))
                If pago.NumOperacion <> String.Empty Then xpago.SetAttribute("NumOperacion", pago.NumOperacion)
                If pago.RfcEmisorCtaOrd <> String.Empty Then xpago.SetAttribute("RfcEmisorCtaOrd", pago.RfcEmisorCtaOrd)
                If pago.NomBancoOrdExt <> String.Empty Then xpago.SetAttribute("NomBancoOrdExt", pago.NomBancoOrdExt)
                If pago.CtaOrdenante <> String.Empty Then xpago.SetAttribute("CtaOrdenante", pago.CtaOrdenante)
                If pago.RfcEmisorCtaBen <> String.Empty Then xpago.SetAttribute("RfcEmisorCtaBen", pago.RfcEmisorCtaBen)
                If pago.CtaBeneficiario <> String.Empty Then xpago.SetAttribute("CtaBeneficiario", pago.CtaBeneficiario)
                If pago.TipoCadPago <> String.Empty Then xpago.SetAttribute("TipoCadPago", pago.TipoCadPago)
                If pago.CertPago <> String.Empty Then xpago.SetAttribute("CertPago", pago.CertPago)
                If pago.CadPago <> String.Empty Then xpago.SetAttribute("CadPago", pago.CadPago)
                If pago.SelloPago <> String.Empty Then xpago.SetAttribute("SelloPago", pago.SelloPago)
                For Each dr As DoctoRelacionado In pago.DoctoRelacionado
                    Dim xDR As XmlElement = m_xmlDOM.CreateElement("pago10", "DoctoRelacionado", _NamespaceCfdiPagos)
                    If dr.IdDocumento <> String.Empty Then xDR.SetAttribute("IdDocumento", dr.IdDocumento)
                    If dr.Serie <> String.Empty Then xDR.SetAttribute("Serie", dr.Serie)
                    If dr.Folio <> String.Empty Then xDR.SetAttribute("Folio", dr.Folio)
                    If dr.MonedaDR <> String.Empty Then xDR.SetAttribute("MonedaDR", dr.MonedaDR)
                    If dr.TipoCambioDR <> String.Empty Then xDR.SetAttribute("TipoCambioDR", dr.TipoCambioDR)
                    If dr.MetodoDePagoDR <> String.Empty Then xDR.SetAttribute("MetodoDePagoDR", dr.MetodoDePagoDR)
                    If dr.NumParcialidad <> String.Empty Then xDR.SetAttribute("NumParcialidad", dr.NumParcialidad)
                    If dr.ImpSaldoAnt <> 0 Then xDR.SetAttribute("ImpSaldoAnt", dr.ImpSaldoAnt.ToString("F2"))
                    If dr.ImpPagado <> 0 Then xDR.SetAttribute("ImpPagado", dr.ImpPagado.ToString("F2"))
                    If dr.ImpSaldoInsoluto = 0 Then
                        xDR.SetAttribute("ImpSaldoInsoluto", dr.ImpSaldoInsoluto.ToString("F2"))
                    Else
                        xDR.SetAttribute("ImpSaldoInsoluto", dr.ImpSaldoInsoluto.ToString("F2"))

                    End If

                    xpago.AppendChild(xDR)
                Next

                If pago.Impuestos.Retenciones.Count > 0 OrElse _comprobante.Impuestos.Traslados.Count > 0 Then
                    Dim xImpuestos As XmlElement = m_xmlDOM.CreateElement("pago10", "Impuestos", _NamespaceCfdiPagos)
                    If pago.Impuestos.TotalImpuestosRetenidos > 0 Then xImpuestos.SetAttribute("TotalImpuestosRetenidos", pago.Impuestos.TotalImpuestosRetenidos.ToString("F2"))
                    If pago.Impuestos.TotalImpuestosTrasladados > 0 Then xImpuestos.SetAttribute("TotalImpuestosTrasladados", pago.Impuestos.TotalImpuestosTrasladados.ToString("F2"))
                    If pago.Impuestos.Retenciones.Count > 0 Then
                        Dim Retenciones As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retenciones", _NamespaceCfdiPagos)
                        For Each r As Retencion In pago.Impuestos.Retenciones
                            Dim Retencion As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retencion", _NamespaceCfdiPagos)
                            Retencion.SetAttribute("Impuesto", r.Impuesto.ToString())
                            Retencion.SetAttribute("Importe", r.Importe.ToString("F2"))
                            Retenciones.AppendChild(Retencion)
                        Next

                        xImpuestos.AppendChild(Retenciones)
                    End If

                    If pago.Impuestos.Traslados.Count > 0 Then
                        Dim Traslados As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslados", _NamespaceCfdiPagos)
                        For Each t As Traslado In pago.Impuestos.Traslados
                            Dim Traslado As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslado", _NamespaceCfdiPagos)
                            Traslado.SetAttribute("Impuesto", t.Impuesto.ToString())
                            Traslado.SetAttribute("Importe", t.Importe.ToString("F2"))
                            Traslado.SetAttribute("TasaOCuota", t.TasaOCuota.ToString("F6"))
                            Traslado.SetAttribute("TipoFactor", t.TipoFactor.ToString())
                            Traslados.AppendChild(Traslado)
                        Next

                        xImpuestos.AppendChild(Traslados)
                    End If

                    xpago.AppendChild(xImpuestos)
                End If

                Pagos.AppendChild(xpago)
            Next
        End If

        complemento.AppendChild(Pagos)
        Return complemento
    End Function
    'AB SAT 4.0
    Private Sub CrearAtributosComprobante(ByRef Comprobante As XmlElement)

        Try
            If _comprobante.TipoDeComprobante = "P" Then
                'shemaLocation = shemaLocation & " " & _ShemaLocationPagos
                Comprobante.SetAttribute("xmlns:Pago10", _NamespaceCfdiPagos)
            End If
            Comprobante.SetAttribute("xmlns:xsi", _NamespaceXsi)

            If _comprobante.TipoDeComprobante = "P" Then
                Comprobante.SetAttribute("xsi:schemaLocation", _ShemaLocationPagos)
            Else
                Comprobante.SetAttribute("xsi:schemaLocation", _SchemaLocation)
            End If


            Comprobante.SetAttribute("Version", _comprobante.Version)
            If _comprobante.Serie <> String.Empty Then
                Comprobante.SetAttribute("Serie", _comprobante.Serie)
            End If
            If _comprobante.Folio <> String.Empty Then
                Comprobante.SetAttribute("Folio", _comprobante.Folio)
            End If
            Comprobante.SetAttribute("Fecha", _comprobante.Fecha)
            Comprobante.SetAttribute("Sello", "")
            If _comprobante.FormaPago <> String.Empty Then Comprobante.SetAttribute("FormaPago", _comprobante.FormaPago)
            Comprobante.SetAttribute("NoCertificado", _comprobante.NoCertificado)
            Comprobante.SetAttribute("Certificado", _comprobante.Certificado)
            If _comprobante.CondicionesDePago <> String.Empty Then
                Comprobante.SetAttribute("CondicionesDePago", _comprobante.CondicionesDePago)
            End If
            If _comprobante.TipoDeComprobante = "P" Then
                Comprobante.SetAttribute("SubTotal", _comprobante.Subtotal.ToString("0"))
            Else
                Comprobante.SetAttribute("SubTotal", _comprobante.Subtotal.ToString("0.00"))
            End If
            If _comprobante.Descuento > 0 Then
                Comprobante.SetAttribute("Descuento", _comprobante.Descuento.ToString("0.00"))
            End If
            Comprobante.SetAttribute("Moneda", _comprobante.Moneda)
            If _comprobante.TipoCambio > 0 Then
                Comprobante.SetAttribute("TipoCambio", _comprobante.TipoCambio.ToString())
            End If
            If _comprobante.TipoDeComprobante = "P" Then
                Comprobante.SetAttribute("Total", _comprobante.Total.ToString("0"))
            Else
                Comprobante.SetAttribute("Total", _comprobante.Total.ToString("0.00"))
            End If
            Comprobante.SetAttribute("TipoDeComprobante", _comprobante.TipoDeComprobante)
            If _comprobante.Exportacion <> String.Empty Then
                Comprobante.SetAttribute("Exportacion", _comprobante.Exportacion)
            End If
            If _comprobante.MetodoPago <> String.Empty Then
                Comprobante.SetAttribute("MetodoPago", _comprobante.MetodoPago)
            End If
            Comprobante.SetAttribute("LugarExpedicion", _comprobante.LugarExpedicion)
            If _comprobante.Confirmacion <> String.Empty Then
                Comprobante.SetAttribute("Confirmacion", _comprobante.Confirmacion)
            End If
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
    End Sub

    Private Function CrearNodoEmisor() As XmlElement
        Dim Emisor As XmlElement = m_xmlDOM.CreateElement("cfdi", "Emisor", _NamespaceCfdi)
        Emisor.SetAttribute("Rfc", _comprobante.Emisor.Rfc)
        Try
            If _comprobante.Emisor.Nombre <> String.Empty Then
                Emisor.SetAttribute("Nombre", _comprobante.Emisor.Nombre)
            End If
            Emisor.SetAttribute("RegimenFiscal", _comprobante.Emisor.RegimenFiscal)
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
        Return Emisor
    End Function

    Private Function CrearNodoCfdiRelacionados() As XmlElement

        'Dim CfdiRelacionados As XmlElement = m_xmlDOM.CreateElement("cfdi", "CfdiRelacionados", _NamespaceCfdi)
        'CfdiRelacionados.SetAttribute("TipoRelacion", _comprobante.CfdiRelacionados.TipoRelacion)

        If _comprobante.CfdiRelacionados.TipoRelacion.Count > 0 Then
            Dim CfdiRelacionados As XmlElement = m_xmlDOM.CreateElement("cfdi", "CfdiRelacionados", _NamespaceCfdi)

            If _comprobante.CfdiRelacionados.TipoRelacion.Count > 0 Then
                CfdiRelacionados.SetAttribute("TipoRelacion", _comprobante.CfdiRelacionados.TipoRelacion)
            End If
          
            If _comprobante.CfdiRelacionados.CfdiRelacionado.Count > 0 Then
                Dim CfdiRelacionado As XmlElement = m_xmlDOM.CreateElement("cfdi", "CfdiRelacionado", _NamespaceCfdi)
                For Each r As CfdiRelacionado In _comprobante.CfdiRelacionados.CfdiRelacionado
                    ' Dim Retencion As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retencion", _NamespaceCfdi)
                    CfdiRelacionado.SetAttribute("UUID", r.UUID.ToString())

                    ' CfdiRelacionados.AppendChild(CfdiRelacionado)
                Next
                CfdiRelacionados.AppendChild(CfdiRelacionado)
            End If

            'If _comprobante.Impuestos.Traslados.Count > 0 Then
            '    Dim Traslados As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslados", _NamespaceCfdi)
            '    For Each t As Traslado In _comprobante.Impuestos.Traslados
            '        Dim Traslado As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslado", _NamespaceCfdi)
            '        Traslado.SetAttribute("Importe", t.Importe.ToString())
            '        Traslado.SetAttribute("TipoFactor", t.TipoFactor.ToString())
            '        Traslado.SetAttribute("TasaOCuota", t.TasaOCuota.ToString())
            '        Traslado.SetAttribute("Impuesto", t.Impuesto.ToString())


            '        Traslados.AppendChild(Traslado)
            '    Next
            '    Impuestos.AppendChild(Traslados)
            'End If
            Return CfdiRelacionados
        Else
            Return Nothing
        End If




        'Dim CfdiRelacionados As XmlElement = m_xmlDOM.CreateElement("cfdi", "CfdiRelacionados", _NamespaceCfdi)
        'CfdiRelacionados.SetAttribute("TipoRelacion", _comprobante.CfdiRelacionados.TipoRelacion)
        'Try
        '    If _comprobante.CfdiRelacionados.CfdiRelacionado <> String.Empty Then
        '        CfdiRelacionados.SetAttribute("CfdiRelacionado", _comprobante.CfdiRelacionados.CfdiRelacionado)
        '    End If

        'Catch es As System.IO.IOException
        '    MessageBox.Show(es.ToString())

        'End Try
        'Return CfdiRelacionados



        'Dim CfdiRelacionado As XmlElement
        'For Each re As CfdiRelacionado In _comprobante.CfdiRelacionados.CfdiRelacionado
        '    ' CfdiRelacionado = m_xmlDOM.CreateElement("cfdi", "Relacionado", _NamespaceCfdi)
        '    CfdiRelacionado.SetAttribute("UUID", _comprobante.CfdiRelacionados.CfdiRelacionado)

        '    CfdiRelacionados.AppendChild(CfdiRelacionado)

        '    'IndentarNodo(Conceptos);
        '    CfdiRelacionado = Nothing
        'Next

        'Return CfdiRelacionados



    End Function
    'AB SAT 4.0
    Private Function CrearNodoReceptor() As XmlElement
        Dim Receptor As XmlElement = m_xmlDOM.CreateElement("cfdi", "Receptor", _NamespaceCfdi)
        Receptor.SetAttribute("Rfc", _comprobante.Receptor.Rfc)
        Try

            If _comprobante.Receptor.Nombre <> String.Empty Then
                Receptor.SetAttribute("Nombre", _comprobante.Receptor.Nombre)
            End If
            
            If _comprobante.Receptor.DomicilioFiscalReceptor <> String.Empty Then
                Receptor.SetAttribute("DomicilioFiscalReceptor", _comprobante.Receptor.DomicilioFiscalReceptor)
            End If

            If _comprobante.Receptor.ResidenciaFiscal <> String.Empty Then
                Receptor.SetAttribute("ResidenciaFiscal", _comprobante.Receptor.ResidenciaFiscal)
            End If

            If _comprobante.Receptor.NumRegIdTrib <> String.Empty Then
                Receptor.SetAttribute("NumRegIdTrib", _comprobante.Receptor.NumRegIdTrib)
            End If

            If _comprobante.Receptor.RegimenFiscalReceptor <> String.Empty Then
                Receptor.SetAttribute("RegimenFiscalReceptor", _comprobante.Receptor.RegimenFiscalReceptor)
            End If

            Receptor.SetAttribute("UsoCFDI", _comprobante.Receptor.UsoCFDI)
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
        Return Receptor
    End Function

    Private Function CrearNodoConceptos() As XmlElement


        Try
            If _comprobante.Conceptos.Count <= 0 Then
                Return Nothing
            End If
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())

        End Try
        Dim Conceptos As XmlElement = m_xmlDOM.CreateElement("cfdi", "Conceptos", _NamespaceCfdi)
        'IndentarNodo(Conceptos);
        'int c;
        For Each c As Concepto In _comprobante.Conceptos
            Dim Impuestos As XmlElement
            Dim Traslado As XmlElement
            Dim Retencion As XmlElement

            Dim Concepto As XmlElement = m_xmlDOM.CreateElement("cfdi", "Concepto", _NamespaceCfdi)

            Concepto.SetAttribute("ClaveProdServ", c.ClaveProdServ)
            If c.NoIdentificacion <> String.Empty Then
                Concepto.SetAttribute("NoIdentificacion", c.NoIdentificacion)
            End If
            Concepto.SetAttribute("Cantidad", c.Cantidad.ToString())
            Concepto.SetAttribute("ClaveUnidad", c.ClaveUnidad)
            If c.Unidad <> String.Empty Then
                Concepto.SetAttribute("Unidad", c.Unidad)
            End If
            Concepto.SetAttribute("Descripcion", c.Descripcion)
            Concepto.SetAttribute("ValorUnitario", c.ValorUnitario.ToString())
            Concepto.SetAttribute("Importe", c.Importe.ToString())
            If c.Descuento > 0 Then
                Concepto.SetAttribute("Descuento", c.Descuento.ToString())
            End If
            If c.ObjetoImp <> String.Empty Then
                Concepto.SetAttribute("ObjetoImp", c.ObjetoImp)
            End If
            If c.Impuestos.Traslados.Count > 0 OrElse c.Impuestos.Retenciones.Count > 0 Then
                Impuestos = m_xmlDOM.CreateElement("cfdi", "Impuestos", _NamespaceCfdi)
                If c.Impuestos.Traslados.Count > 0 Then
                    Dim Traslados As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslados", _NamespaceCfdi)
                    For Each t As TrasladoC In c.Impuestos.Traslados
                        Traslado = m_xmlDOM.CreateElement("cfdi", "Traslado", _NamespaceCfdi)
                        Traslado.SetAttribute("Base", t.Base.ToString())
                        Traslado.SetAttribute("Impuesto", t.Impuesto)
                        Traslado.SetAttribute("TipoFactor", t.TipoFactor)
                        Traslado.SetAttribute("TasaOCuota", t.TasaOCuota.ToString("F6"))
                        Traslado.SetAttribute("Importe", t.Importe.ToString())
                        Traslados.AppendChild(Traslado)
                    Next
                    Impuestos.AppendChild(Traslados)
                End If

                If c.Impuestos.Retenciones.Count > 0 Then

                    Dim Retenciones As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retenciones", _NamespaceCfdi)
                    For Each r As RetencionC In c.Impuestos.Retenciones
                        Retencion = m_xmlDOM.CreateElement("cfdi", "Retencion", _NamespaceCfdi)
                        Retencion.SetAttribute("Base", r.Base.ToString())
                        Retencion.SetAttribute("Impuesto", r.Impuesto)
                        Retencion.SetAttribute("TipoFactor", r.TipoFactor)
                        Retencion.SetAttribute("TasaOCuota", r.TasaOCuota.ToString("F6"))
                        Retencion.SetAttribute("Importe", r.Importe.ToString())
                        Retenciones.AppendChild(Retencion)
                    Next
                    Impuestos.AppendChild(Retenciones)
                End If
                Concepto.AppendChild(Impuestos)
            End If

            If c.InformacionAduanera.Count > 0 Then
                For Each ia As InformacionAduanera In c.InformacionAduanera
                    Dim InformacionAduanera As XmlElement = m_xmlDOM.CreateElement("cfdi", "InformacionAduanera", _NamespaceCfdi)
                    InformacionAduanera.SetAttribute("NumeroPedimento", ia.NumeroPedimento)
                    InformacionAduanera.AppendChild(InformacionAduanera)
                    Concepto.AppendChild(InformacionAduanera)
                Next
            End If

            If c.CuentaPredial.Numero <> String.Empty Then
                Dim CuentaPredial As XmlElement = m_xmlDOM.CreateElement("cfdi", "CuentaPredial", _NamespaceCfdi)
                CuentaPredial.SetAttribute("Numero", c.CuentaPredial.Numero)
                Conceptos.AppendChild(CuentaPredial)
            End If

            If c.Parte.Count > 0 Then
                For Each p As Parte In c.Parte
                    Dim Parte As XmlElement = m_xmlDOM.CreateElement("cfdi", "Parte", _NamespaceCfdi)
                    Parte.SetAttribute("Descripcion", p.Descripcion)
                    If p.ValorUnitario > 0 Then
                        Parte.SetAttribute("ValorUnitario", p.ValorUnitario.ToString())
                    End If
                    If p.Importe > 0 Then
                        Parte.SetAttribute("Importe", p.Importe.ToString())
                    End If
                    If p.InformacionAduanera.Count > 0 Then
                        For Each ia As InformacionAduanera In p.InformacionAduanera
                            Dim InformacionAduanera As XmlElement = m_xmlDOM.CreateElement("cfdi", "InformacionAduanera", _NamespaceCfdi)
                            InformacionAduanera.SetAttribute("NumeroPedimento", ia.NumeroPedimento)
                            Parte.AppendChild(InformacionAduanera)
                        Next
                    End If
                    Concepto.AppendChild(Parte)
                Next
            End If



            Conceptos.AppendChild(Concepto)
            'IndentarNodo(Conceptos);
            Concepto = Nothing
        Next

        Return Conceptos
    End Function

    Private Function CrearNodoImpuestos() As XmlElement

        If _comprobante.Impuestos.Retenciones.Count > 0 OrElse _comprobante.Impuestos.Traslados.Count > 0 Then
            Dim Impuestos As XmlElement = m_xmlDOM.CreateElement("cfdi", "Impuestos", _NamespaceCfdi)

            If _comprobante.Impuestos.TotalImpuestosRetenidos > 0 Then
                Impuestos.SetAttribute("TotalImpuestosRetenidos", _comprobante.Impuestos.TotalImpuestosRetenidos.ToString())
            End If
            If _comprobante.Impuestos.TotalImpuestosTrasladados >= 0 Then
                Impuestos.SetAttribute("TotalImpuestosTrasladados", _comprobante.Impuestos.TotalImpuestosTrasladados.ToString())
            End If
            If _comprobante.Impuestos.Retenciones.Count > 0 Then
                Dim Retenciones As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retenciones", _NamespaceCfdi)
                For Each r As Retencion In _comprobante.Impuestos.Retenciones
                    Dim Retencion As XmlElement = m_xmlDOM.CreateElement("cfdi", "Retencion", _NamespaceCfdi)
                    Retencion.SetAttribute("Impuesto", r.Impuesto.ToString())
                    Retencion.SetAttribute("Importe", r.Importe.ToString())
                    Retenciones.AppendChild(Retencion)
                Next
                Impuestos.AppendChild(Retenciones)
            End If

            If _comprobante.Impuestos.Traslados.Count > 0 Then
                Dim Traslados As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslados", _NamespaceCfdi)
                For Each t As Traslado In _comprobante.Impuestos.Traslados
                    Dim Traslado As XmlElement = m_xmlDOM.CreateElement("cfdi", "Traslado", _NamespaceCfdi)
                    Traslado.SetAttribute("Base", t.Base.ToString())
                    Traslado.SetAttribute("Importe", t.Importe.ToString())
                    Traslado.SetAttribute("TipoFactor", t.TipoFactor.ToString())
                    Traslado.SetAttribute("TasaOCuota", t.TasaOCuota.ToString())
                    Traslado.SetAttribute("Impuesto", t.Impuesto.ToString())


                    Traslados.AppendChild(Traslado)
                Next
                Impuestos.AppendChild(Traslados)
            End If
            Return Impuestos
        Else
            Return Nothing
        End If

    End Function

    Private Function IndentarNodo(Nodo As XmlNode) As XmlNode
        Nodo.AppendChild(m_xmlDOM.CreateTextNode(Environment.NewLine))
        Return Nodo
    End Function

    Private Function ObtenerSello(rutaPFX As String, passwordPFX As String) As String
        Try
            'X509Certificate2 objCertPfx = new X509Certificate2("C:\SISCOCSNETPACFE\\bin\\Debug\\PKI\\aaa010101aaa__csd_01.pfx", "12345678a");
            Dim objCertPfx As New X509Certificate2(rutaPFX, passwordPFX)
            'Dim objCertPfx As New X509Certificate2(Server.MapPath("/OST14112729122014.pfx"), "orto2014", X509KeyStorageFlags.MachineKeySet)
            Dim lRSA As RSACryptoServiceProvider = DirectCast(objCertPfx.PrivateKey, RSACryptoServiceProvider)
            Dim lhasher As New SHA1CryptoServiceProvider()
            Dim bytesFirmados As Byte() = lRSA.SignData(System.Text.Encoding.UTF8.GetBytes(GetCadenaOriginal(m_xmlDOM.InnerXml)), lhasher)
            Return Convert.ToBase64String(bytesFirmados)
        Catch es As System.IO.IOException
            MessageBox.Show(es.ToString())
            Return String.Empty
        End Try
    End Function
    Private Function ObtenerSello1(rutaPFX As String, passwordPFX As String) As String
        Try
            'X509Certificate2 objCertPfx = new X509Certificate2("C:\SISCOCSNETPACFE\\bin\\Debug\\PKI\\aaa010101aaa__csd_01.pfx", "12345678a");


            Dim privateCert As New X509Certificate2(rutaPFX, passwordPFX, X509KeyStorageFlags.Exportable)
            Dim privateKey As RSACryptoServiceProvider = DirectCast(privateCert.PrivateKey, RSACryptoServiceProvider)
            Dim privateKey1 As New RSACryptoServiceProvider()
            privateKey1.ImportParameters(privateKey.ExportParameters(True))

            Dim bytesFirmados As Byte() = privateKey1.SignData(System.Text.Encoding.UTF8.GetBytes(GetCadenaOriginal(m_xmlDOM.InnerXml)), "SHA256")
            Return Convert.ToBase64String(bytesFirmados)
        Catch ex As System.IO.IOException
            MessageBox.Show(ex.ToString())
            Return String.Empty
        End Try
    End Function

    Private Sub ObtenerCerticicadoYNoCertificado(rutaCertificado As String, ByRef Certificado As String, ByRef NoCertificado As String)
        Try
            Dim objCert As New X509Certificate2()
            'byte[] bRawData = ReadFile("C:\SISCOCSNETPACFE\\bin\\Debug\\PKI\\aaa010101aaa__CSD_01.cer");
            'Private Const _rutaXslt As String = "\XSLT\cadenaoriginal_3_3.xslt"
            Dim bRawData As Byte() = ReadFile(rutaCertificado)
            objCert.Import(bRawData)
            Certificado = Convert.ToBase64String(bRawData)
            NoCertificado = FormatearSerieCert(objCert.SerialNumber)
        Catch excepcion As System.IO.IOException
            Certificado = String.Empty
            NoCertificado = String.Empty
            MessageBox.Show(excepcion.ToString())
        End Try
    End Sub

    Public Function ReadFile(strArchivo As String) As Byte()
        Dim f As New FileStream(strArchivo, FileMode.Open, FileAccess.Read)
        Dim size As Integer = Convert.ToInt64(f.Length)
        Dim data As Byte() = New Byte(size - 1) {}
        size = f.Read(data, 0, size)
        f.Close()
        Return data
    End Function

    Private Function FormatearSerieCert(Serie As String) As String
        Dim Resultado As String = ""
        Dim I As Integer

        For I = 1 To Serie.Length - 1 Step 2
            'Resultado = Serie.Substring(I, 1);
            Resultado = Resultado & Serie.Substring(I, 1)
        Next

        Return Resultado
    End Function
    'AB SAT 4.0
    Public Function GetCadenaOriginal(xmlCFD As String) As String

        'Dim ruta As String = "//SAT//cadenaoriginal_3_3.xslt"

        ' Dim op As System.Windows.Forms.Application.M
        '\XSLT\cadenaoriginal_3_3.xslt
        Try
            Dim _rutaXslt As New String(Server.MapPath("/XSLT/cadenaoriginal_4_0.xslt"))

            Dim xslt As New XslCompiledTransform()
            Dim xmldoc As New XmlDocument()
            Dim navigator As XPathNavigator
            Dim output As New StringWriter()
            xmldoc.LoadXml(xmlCFD)
            navigator = xmldoc.CreateNavigator()
            xslt.Load(_rutaXslt)
            xslt.Transform(navigator, Nothing, output)
            Return output.ToString()
        Catch ex As System.IO.IOException
            MessageBox.Show(ex.ToString())
            Return String.Empty
        End Try
    End Function
#End Region
End Class
