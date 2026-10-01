Imports System.Security.Cryptography.X509Certificates
Imports System.Collections.Generic
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml
Imports AgeMED.WSCFDI33.RespuestaTFD33
Imports AgeMED.WSCFDI33
Imports System.Net
Imports System.Net.Mail
Imports System.Drawing
Imports System.Drawing.ImageFormatConverter
Imports System.Windows
Imports ThoughtWorks.QRCode.Codec
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Xml.Schema
Imports System.Xml.XPath
Imports System.Xml.Xsl


Public Class Facturacion
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Dim _c As Comprobante = New Comprobante()
    Private _comprobante As Comprobante

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargarmetodopago()
            cargaddTipoDePago()
            cargacExportacion()
            'cargacObjeto()
            cargaRegimenFiscal()
            cargaUCFDI()
            div1.Visible = False
            cmbfacturacion.Enabled = False
            LimpiarPaneles()
        End If
        ocultarPaneles()
    End Sub

    Sub cargaFacturas()
        cargaDTGCargosConsulta()
    End Sub

    Protected Sub facturar(sender As Object, e As EventArgs)
        Dim count As Integer = 0
        Dim listPagos As String = ""

        CargarDatosFacturacion(LstBoxPacientes.SelectedValue)
        LlenarComboRazonSocial(LstBoxPacientes.SelectedValue)
        div_input_razon_social.Visible = False
        div_input_razon_social.Visible = False
        div_direccion.Visible = False
        'AB SAT 4.0
        div_cp.Visible = True
        div_ciudad.Visible = False
        div_estado.Visible = False
        div_pais.Visible = False
        div_UPfiscal.Visible = False
        div_emisor.Visible = False
        div_dfacturacion.Visible = False
        iniciaComprobante()
        consultasAfacturarG()

        For Each row As GridViewRow In dtgCargosConsultas.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then

                If count > 0 Then
                    listPagos += ","
                End If

                listPagos += row.Cells(0).Text ' se agrega a una lista todos los filios a facturar ejem: C3243|C5345|C0999 
                count = count + 1  ' para saber cuantos filios de consulta se agregaron a la lista para facturar.
                bandera.Value = 0
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
                C_facturar.Visible = True
            End If
        Next


    End Sub

    '**********************Facturas********************************************
    Sub CargarDatosFacturacion(CodigoPaciente)
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbReceptorNombre.Value = ""
        tbReceptorRFC.Value = ""
        correo.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""
        codigo_paciente.Value = CodigoPaciente
        strsql = "SELECT iddatosfac,nombre,nombre,rfc, email,cp, pais, edo, ciudad, codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] WHERE iddatosfac =" + CodigoPaciente + " ORDER BY iddatosfac"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                dpaciente.Value = dt.Rows(0).Item("nombre")
                nompaciente.Value = dpaciente.Value
                tbReceptorNombre.Value = dt.Rows(0).Item("nombre")
                tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                correo.Value = dt.Rows(0).Item("email")
                    'direccion.Value = dt.Rows(0).Item("direccion")
                cp.Value = dt.Rows(0).Item("cp")
                ciudad.Value = dt.Rows(0).Item("ciudad")
                estado.Value = dt.Rows(0).Item("edo")
                pais.Value = dt.Rows(0).Item("pais")
                If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                    tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                Else
                    tbReceptorRegimenFiscal.SelectedValue = "999"
                End If
                id_razon_social.Value = dt.Rows(0).Item("iddatosfac")

                End If
            End If
    End Sub

    Sub LlenarComboRazonSocial(CodigoPaciente)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        codigo_paciente.Value = CodigoPaciente
        strSQL = "SELECT iddatosfac,nombre FROM [" & clsDatos.BaseDatos & "].[dbo].[catFacturas] WHERE iddatosfac =" + CodigoPaciente + " ORDER BY iddatosfac"

        If funciones.llenadropdown(strSQL, select_razon_social) = -1 Then
            'Error
            div_input_razon_social.Visible = True
        Else
            'Se realizó la función, verificar si hay datos
            If select_razon_social.Items.Count = 0 Then
                div_input_razon_social.Visible = True
                div_select_razon_social.Visible = False
                div_direccion.Visible = True
                div_cp.Visible = True
                div_ciudad.Visible = True
                div_estado.Visible = True
                div_pais.Visible = True
                'Habilitar bandera para dar de alta el nuevo
                bandera.Value = 1
            Else
                div_input_razon_social.Visible = False
                div_select_razon_social.Visible = True
                select_razon_social.Items.Add("NUEVO")
                bandera.Value = 0
            End If


        End If
    End Sub

    Sub CambioSelectRazonSocial()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbReceptorNombre.Value = ""
        tbReceptorRFC.Value = ""
        correo.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""

        If select_razon_social.SelectedValue = "NUEVO" Then
            'Agregar Nuevo
            div_input_razon_social.Visible = True
            div_select_razon_social.Visible = False
            div_direccion.Visible = True
            div_cp.Visible = True
            div_ciudad.Visible = True
            div_estado.Visible = True
            div_pais.Visible = True
            pais.Value = "MÉXICO"
            estado.Value = "YUCATÁN"
            ciudad.Value = "MÉRIDA"
            tbReceptorRegimenFiscal.SelectedValue = "999"
            cp.Disabled = False
            'Habilitar bandera para dar de alta el nuevo
            bandera.Value = 1
        Else
            strsql = "SELECT iddatosfac,nombre,rfc, email,cp, pais, edo, ciudad, codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[CcatFacturas] WHERE iddatosfac =" + select_razon_social.SelectedValue
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbReceptorNombre.Value = dt.Rows(0).Item("nombre")
                    tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                    correo.Value = dt.Rows(0).Item("email")
                    'direccion.Value = dt.Rows(0).Item("direccion")
                    cp.Value = dt.Rows(0).Item("cp")
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                    estado.Value = dt.Rows(0).Item("edo")
                    pais.Value = dt.Rows(0).Item("pais")
                    If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                        tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                    Else
                        tbReceptorRegimenFiscal.SelectedValue = "999"
                    End If
                    id_razon_social.Value = dt.Rows(0).Item("iddatosfac")
                End If
            End If
        End If
    End Sub
    'AB SAT 4.0
    Sub FacturarConsultas()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If (tbReceptorRegimenFiscal.SelectedValue = "999") Then
            ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaRegimenFiscal", "alert('Favor de seleccionar el Regimen Fiscal')", True)
            correo.Focus()
        Else

            If cp.Value = "" Then
                ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaDomicilioFiscal", "alert('Favor de ingresar el Codigo Postal(DomicilioFiscal)')", True)
                correo.Focus()
            Else
                If (correo.Value = "") Then
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alerta", "alert('Favor de ingresar el correo del cliente')", True)
                    correo.Focus()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closeModalFactura", "<script>$('#datos_facturacion').modal('hide');</script>", False)
                    If bandera.Value = 1 Then
                        'Dar de alta la razón social
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] SET nombre='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE iddatosfac = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() = 0 Then
                            strsql = "SELECT iddatosfac, nombre FROM [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] WHERE iddatosfac = " + id_razon_social.Value

                            If clsdatos.cargatabla(strsql, dt) = 0 Then
                                id_razon_social.Value = dt.Rows(0).Item("iddatosfac")
                                'Timbrado()
                                Facturar33()
                                'LblMensajeAviso.Text = "Facturacion Realizada con Exito"
                                'PanelAvisos.Visible = True
                                'PanelAvisos.Focus()
                                cargaDTGCargosConsulta()
                            End If

                        Else
                            LblMensajeCritico.Text = clsdatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    Else
                        'Actualizar la razón social
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] SET nombre='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE iddatosfac = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() = 0 Then
                            'Timbrado()
                            Facturar33()
                            'LblMensajeAviso.Text = "Facturacion Realizada con Exito!"
                            'PanelAvisos.Visible = True
                            'PanelAvisos.Focus()
                            cargaDTGCargosConsulta()
                        Else
                            LblMensajeCritico.Text = clsdatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    End If
                End If


            End If
        End If
    End Sub

    Private Sub extraerCampos()
        Dim hoy As DateTime = DateTime.Now
        Dim cuenta As Int32 = 0
        Dim contador As Int32 = 0
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        _c.Version = tbVersion.Value
        _c.Serie = tbSerie.Value
        _c.Folio = tbFolio.Value
        _c.Fecha = dtpFecha.Value
        _c.FormaPago = tbFormaPago.SelectedValue
        '_c.NoCertificado = tbNoCertificado.Text
        '_c.Descuento = numDescuento.Value
        _c.Moneda = tbMoneda.Value
        _c.TipoCambio = "0.00"
        _c.TipoDeComprobante = tbTipoComprobante.Value
        _c.MetodoPago = tbMetodoPago.SelectedValue
        _c.LugarExpedicion = tbLugarExpedicion.Value
        ''new CfdiRelacionados()  //Opcional=            //public CfdiRelacionados CfdiRelacionados ;
        _c.Emisor.Nombre = tbEmisorNombre.Value
        _c.Emisor.Rfc = tbEmisorRFC.Value
        _c.Emisor.RegimenFiscal = tbEmisorRegimenFiscal.Value
        _c.Receptor.Nombre = tbReceptorNombre.Value
        _c.Receptor.Rfc = tbReceptorRFC.Value
        '_c.Receptor.NumRegIdTrib = tbReceptorNumRegIdTrib.Text
        '_c.Receptor.ResidenciaFiscal = tbReceptorResidenciaFiscal.Text
        ' _c.Receptor.UsoCFDI = tbReceptorUsoCFDI.SelectedValue
        '_c.Impuestos.TotalImpuestosRetenidos = toDecimal(tbImpuestosTotalImpuestosRetenidos.Text)

        'AB SAT 4.0
        _c.Receptor.DomicilioFiscalReceptor = cp.Value
        _c.Receptor.RegimenFiscalReceptor = tbReceptorRegimenFiscal.SelectedValue
        _c.Exportacion = tbExportacion.SelectedValue

        strsql = "SELECT CodigoUCFDI,ClaveUCFDI FROM [" & clsdatos.BaseDatos & "].[dbo].[CatUsoCFDI] " & _
                "where CodigoUCFDI=" & tbReceptorUsoCFDI.SelectedValue & " and estatus='A'"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                _c.Receptor.UsoCFDI = dt.Rows(0).Item("ClaveUCFDI")
            Else
                LblMensajeAdvertencia.Text = "No se encontro Uso CFDI, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
        End If

        '************** -DATOS DEL COMPROBANTE- ************

        'Dim hoy As DateTime = DateTime.Now
        Dim c As New Comprobante()
        Dim total As String
        Dim stotal, tiva As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal As DataTable
        Dim listafolios, listaFoliosSQL As String


        '    formaPago = ""
        '    total = ""

        '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
        'cargardatosFac(formaPago, total)
        If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then
            total = dtTotal.Rows(0).Item("costoTotal")
            stotal = dtsTotal.Rows(0).Item("sTotal")
            tiva = dtiTotal.Rows(0).Item("ivatotal")
            _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)

            Dim t As Traslado = New Traslado()
            ' AB SAT 4.0
            t.Base = dtsTotal.Rows(0).Item("sTotal")
            t.Importe = tiva
            t.TipoFactor = "Tasa"
            t.TasaOCuota = "0.000000"
            t.Impuesto = "002"

            _c.Impuestos.Traslados.Add(t)

            variasFacturas = True
        Else
            variasFacturas = False
        End If

        _c.Total = total
        _c.Subtotal = stotal
        _c.Descuento = 0


        '************ -CONCEPTO- *******************

        c.Conceptos = New List(Of Concepto)()

        Do While contador = cuenta
            If variasFacturas = True Then
                For Each fila As DataRow In dtConceptos.Rows
                    Dim concepto1 As New Concepto()
                    concepto1.ClaveProdServ = fila.Item("ClaveProdServ")
                    concepto1.ClaveUnidad = fila.Item("ClaveUnidad")
                    concepto1.Cantidad = fila.Item("cantidad")
                    concepto1.Unidad = fila.Item("Unidad")
                    concepto1.NoIdentificacion = "noIdentificacion"


                    ' AB SAT 4.0
                    If Not IsDBNull(fila.Item("Objetoimp")) Then
                        concepto1.ObjetoImp = fila.Item("Objetoimp")
                    Else
                        concepto1.ObjetoImp = "02"
                    End If

                    concepto1.Descripcion = fila.Item("descripcion")
                    concepto1.Importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
                    concepto1.ValorUnitario = fila.Item("costo")
                    c.Conceptos.Add(concepto1)

                    Dim tc As New TrasladoC()
                    tc.Importe = fila.Item("importeIVA") * fila.Item("cantidad") 'Multiplicar con la cantidad
                    tc.TasaOCuota = fila.Item("tasaIVA")
                    tc.TipoFactor = "Tasa"
                    tc.Impuesto = "002"
                    tc.Base = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad

                    concepto1.Impuestos.Traslados.Add(tc)
                    _c.Conceptos.Add(concepto1)

                    'Dim t As Traslado = New Traslado()
                    't.Importe = fila.Item("importeIVA") * fila.Item("cantidad") 'Multiplicar con la cantidad
                    't.TipoFactor = "Tasa"
                    't.TasaOCuota = fila.Item("tasaIVA")
                    't.Impuesto = "002"
                    '_c.Impuestos.Traslados.Add(t)
                Next

            Else
                strsql = " SELECT codigoConcepto,descripcion,convert(varchar,convert(decimal(8,2),costo)) as costo,cantidad FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
               "WHERE folioConsulta='" & fconsulta.Value & "' AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1"
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    For Each fila As DataRow In dt.Rows
                        Dim concepto1 As New Concepto()
                        concepto1.Cantidad = fila.Item("cantidad")
                        concepto1.Unidad = "No aplica"
                        concepto1.NoIdentificacion = "noIdentificacion"
                        concepto1.Descripcion = fila.Item("descripcion")
                        concepto1.Importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
                        concepto1.ValorUnitario = fila.Item("costo")
                        c.Conceptos.Add(concepto1)

                    Next
                Else
                    'Error en la consulta
                End If
            End If
            contador = contador + 1
        Loop
    End Sub

    Public Function toDecimal(snumero As String) As Decimal
        Dim numero As Decimal = 0
        Decimal.TryParse(snumero, numero)
        Return numero
    End Function

    Public Sub iniciaComprobante()
        Dim banderaError As Boolean = False
        ' Dim fechapdf As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim folio As String = ""
        Dim serie As String = ""

        strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from ConsecutivoFac where idConsecutivoF=1"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                tbFolio.Value = dt.Rows(0).Item("consecutivo")
                tbSerie.Value = dt.Rows(0).Item("serie")
            Else
                LblMensajeCritico.Text = "Error en el consecutivo de folio"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If

        Dim hoy As DateTime = DateTime.Now
        ' AB SAT 4.0
        tbVersion.Value = "4.0"
        'tbVersion.Value = "3.3"
        dtpFecha.Value = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))

        'tbNoCertificado.Text = ""
        'numDescuento.Value = 0
        tbMoneda.Value = "MXN"
        tbTipoComprobante.Value = "I"
        tbLugarExpedicion.Value = "97120"

        ''+++++++DATOS DE EMISOR++++++++++'
        'tbEmisorNombre.Value = "ORTOPEDIA STAR, S.C.P."
        'tbEmisorRegimenFiscal.value = "601"
        'tbEmisorRFC.Value = "OST141127IEA"

        tbEmisorNombre.Value = "FISIOTERAPIA Y MEDICINA DEPORTIVA"
        tbEmisorRegimenFiscal.Value = "601"
        tbEmisorRFC.Value = "FMD020730PQ5"

        'Dim total, formaPago As String
        'cargardatosFac(formaPago, total)

        'tbFormaPago.SelectedValue = formaPago

        'If tbFormaPago.SelectedValue = 99 Then

        '    tbMetodoPago.SelectedValue = "PPD"
        'Else
        '    tbMetodoPago.SelectedValue = "PUE"

        'End If

        'AB SAT 4.0
        If tbReceptorRFC.Value = "XAXX010101000" Then
            tbReceptorRegimenFiscal.SelectedValue = "616"
            tbReceptorUsoCFDI.SelectedValue = "23"
            cp.Value = tbLugarExpedicion.Value
            cp.Disabled = True
        Else
            tbReceptorUsoCFDI.SelectedValue = "3"
        End If

        'tbReceptorUsoCFDI.SelectedValue = "P01"
    End Sub

    Private Sub cargarmetodopago()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT ClaveMP, Descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[Catmetodopago] " & _
                "where estatus='A' order by CodigoMetodoPago"

        If funcion.llenadropdown(strSQL, tbMetodoPago) = 0 Then
            tbMetodoPago.SelectedValue = 1
            'TxtReferencia.Visible = False
        Else
            LblMensajeCritico.Text = "Error MetodoPago 2349: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Private Sub cargaddTipoDePago()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT clave,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] " & _
                "where status='A' order by clave"

        If funcion.llenadropdown(strSQL, tbFormaPago) = 0 Then
            tbFormaPago.SelectedValue = 1
            'TxtReferencia.Visible = False
        Else
            LblMensajeCritico.Text = "Error FormasDePago 2349: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Private Sub tbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbFormaPago.SelectedIndexChanged

        If tbFormaPago.SelectedValue = 99 Then
            tbMetodoPago.SelectedValue = "PPD"
        Else
            tbMetodoPago.SelectedValue = "PUE"
        End If
    End Sub
    'AB SAT 4.0
    Private Sub tbReceptorRegimenFiscal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbReceptorRegimenFiscal.SelectedIndexChanged

        If tbReceptorRegimenFiscal.SelectedValue = "616" Then

            tbReceptorUsoCFDI.SelectedValue = 23
        Else
            tbReceptorUsoCFDI.SelectedValue = 3
            cp.Disabled = False

        End If


    End Sub

    'AB SAT 4.0
    Private Sub cargaUCFDI()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT CodigoUCFDI,ClaveUCFDI+' - '+descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsoCFDI] " & _
                "where estatus='A' order by CodigoUCFDI"
        If funcion.llenadropdown(strSQL, tbReceptorUsoCFDI) = 0 Then
            If tbReceptorRFC.Value = "XAXX010101000" Then
                tbReceptorUsoCFDI.SelectedValue = 23
            Else
                tbReceptorUsoCFDI.SelectedValue = 3
            End If

            'TxtReferencia.Visible = False
        Else

            LblMensajeCritico.Text = "Error UsoCFDi 2349: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    'AB SAT 4.0
    Private Sub cargaRegimenFiscal()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT codigoRegFiscal,(CONVERT(varchar(10), codigoRegFiscal)+'-'+descripcionRegFiscal) as RegimenFiscal  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatRegimenFiscal] " & _
                "where status='1' order by codigoRegFiscal"
        If funcion.llenadropdown(strSQL, tbReceptorRegimenFiscal) = 0 Then
            tbReceptorRegimenFiscal.SelectedValue = 616

        Else

            LblMensajeCritico.Text = "Error RegimenFiscal 2350: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    'AB SAT 4.0
    Private Sub cargacExportacion()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT cExportacion,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatExportacion] " & _
                "where status='1' order by cExportacion"
        If funcion.llenadropdown(strSQL, tbExportacion) = 0 Then
            tbExportacion.SelectedValue = 1
        Else

            LblMensajeCritico.Text = "Error Exportacion 2351: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    'AB SAT 4.0
    'Private Sub cargacObjeto()
    '    Dim strSQL As String
    '    Dim clsDatos As New ClaseDatos
    '    Dim funcion As New FuncionesGenerales
    '    Dim dt As New DataTable
    '    strSQL = "SELECT cobjeto,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatObjeto] " & _
    '            "where status='1' order by cObjeto"
    '    If funcion.llenadropdown(strSQL, tbObjeto) = 0 Then
    '        tbObjeto.SelectedValue = 1
    '    Else

    '        LblMensajeCritico.Text = "Error EObjeto 2351: " + clsDatos.MensajeError
    '        PanelCritico.Visible = True
    '        PanelCritico.Focus()
    '    End If
    'End Sub
    'AB SAT 4.0
    Sub Facturar33()
        extraerCampos()
        Dim rutaXMLST As New String(Server.MapPath("/Facturas/XMLST/" + _c.Serie + _c.Folio + ".xml"))
        'Dim rutaXMLST As String = ".\XMLST\" + _c.Serie + _c.Folio + ".xml" 'Ruta del archivo XMl sin timbrar
        Dim rutaXMLT As New String(Server.MapPath("/Facturas/XMLT/" + _c.Serie + _c.Folio + ".xml"))
        'Dim rutaXMLT As String = ".\XMLT\" + _c.Serie + _c.Folio + ".xml" 'Ruta del archivo XMl timbrado
        Dim rutaPDF As New String(Server.MapPath("/Facturas/PDFS/" + _c.Serie + _c.Folio + ".pdf"))
        'Dim rutaPDF As String = ".\PDFS\" + _c.Serie + _c.Folio + ".pdf" 'Ruta donde guardar el pdf generado

        'Crea el archivo xml en formato 3.2 de acuerdo al anexo 20 del SAT
        Dim CrearXML As CrearXML = New CrearXML()

        Dim apfx As New String(Server.MapPath("/FILESSAT/PFX.pfx"))
        Dim acer As New String(Server.MapPath("/FILESSAT/CER.cer"))

        'CrearXML.Create(_c, rutaXMLST, apfx, "orto2014", acer)
        'CrearXML.Create(_c, rutaXMLST, apfx, "fmdscp2013", acer)
        'ACTUALIZACION AB15072021
        CrearXML.Create(_c, rutaXMLST, apfx, "fmdscp2025", acer)

        'Hace el timbrado con Folios digitales
        Dim imagen As Image = Image.FromFile(Server.MapPath("/LOGO/logo.jpg"))
        Dim Npaciente As String = nompaciente.Value
        Dim dobservaciones As String = dcobservaciones.Value

        If Timbrar(rutaXMLST, rutaXMLT, tbUsuarioFoliosDigitales.Value, tbPasswordFoliosDigitales.Value) Then
            Dim CreaPDF = New CreaPDF(rutaXMLT, rutaPDF, imagen, Npaciente, dobservaciones)

            Dim folioFactura As String = _c.Serie + _c.Folio
            'Dim estadoFac As String = estado_factura.Value
            Dim Path1 As String
            Dim Path2 As String
            Dim mail As New MailMessage
            'If estadoFac = "Vigente" Then
            Path1 = Server.MapPath("/Facturas/XMLT/" + folioFactura)
            Path2 = Server.MapPath("/Facturas/PDFS/" + folioFactura)
            'Else
            'Path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
            'End If

            mail.From = New MailAddress("facturacion@fisiocare.com.mx")

            mail.To.Add(correo.Value)

            mail.Subject = "Factura de servicios de Fisiocare"
            mail.Body = "Facturas"

            Try
                Dim FilePath1 As String = Path1 + ".xml"
                Dim FilePath2 As String = Path2 + ".pdf"

                'el archivo se adjunta indicándole la ruta 
                mail.Attachments.Add(New Attachment(FilePath1))
                mail.Attachments.Add(New Attachment(FilePath2))

                Dim mailClient As New SmtpClient()

                Dim basicAuthenticationInfo As New NetworkCredential("facturacion@fisiocare.com.mx", "fac2018*")

                mailClient.Host = "mail.fisiocare.com.mx"

                mailClient.UseDefaultCredentials = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Port = 26

                mailClient.Send(mail)
                LblMensajeAviso.Text = " Factura Realizada con Exito! E igual Se ha enviado los archivos de la factura (xml y pdf)"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
            Catch ex As Exception
                LblMensajeCritico.Text = ex.Message
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End Try

        End If
        'cargaAgenda()
        'UpdatePanel1.Update()
    End Sub
    Private Function Timbrar(rutaXMLTimbrar As String, rutaXML As String, usuario As String, password As String) As String
        'En el proyecto se agrego una referencia de servicio apuntando al WS de Timbrado de FOLIOS_DIGITALES, a la cual se llamo WSFD
        'La URL es la siguiente.
        '   //http://www.fel.mx/WSTimbrado33/WSCFDI33.svc?WSDL

        '    //Se instancia el WS de Timbrado.
        Dim serviciotimbrado = New WSCFDI33.WSCFDI33Client
        '   //Se instancia la Respuesta del WS de Timbrado.
        Dim respuestaTimbrado = New WSCFDI33.RespuestaTFD33

        '  //Se carga el XML desde archivo.
        Dim DocumentoXML = New XmlDocument()
        ' //La direccion se sustituira dependiendo de donde se leera el XML.
        DocumentoXML.Load(rutaXMLTimbrar)

        '//Variable string que contiene el contenido del XML.
        Dim stringXML = String.Empty
        stringXML = DocumentoXML.OuterXml

        '//Se realiza la petición al WebService, almacenando la respuesta en el objeto RespuestaTFD (RespuestaTimbrado_FOLIOS_DIGITALES)
        '//Los parametros son usuario,password,cadenaXML,referencia
        '//Los datos de acceso se deben solicitar a FOLIOS_DIGITALES.


        respuestaTimbrado = serviciotimbrado.TimbrarCFDI("MSI1603225B1", "uH9t%yfrR+", stringXML, _c.Serie + _c.Folio.ToString)

        '//Obteniendo la respuesta se valida que haya sido exitosa.

        If respuestaTimbrado.OperacionExitosa Then

            'Guardo el XML timbrado.
            DocumentoXML.LoadXml(respuestaTimbrado.XMLResultado)
            DocumentoXML.Save(rutaXML)

            Dim variasFacturas As Boolean
            Dim dtConceptos, dtTotal As DataTable
            Dim dtsTotal, dtiTotal As DataTable
            Dim listafolios, listaFoliosSQL As String
            Dim strsql As String
            Dim clsdatos As New ClaseDatos

            '''''' Guarda la factura
            Dim foliosConsulta As String
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then

                variasFacturas = True
            Else
                variasFacturas = False
            End If
            If variasFacturas = True Then
                foliosConsulta = listaFoliosSQL
            Else

                foliosConsulta = "'" + fconsulta.Value + "'"
            End If


            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[abonos] set facturado='true', num_factura='" & _c.Folio & "', serie ='" & _c.Serie & "' " & _
                               " where idabono in (" & listafolios & ")  "
            strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
            strsql += vbNewLine & " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad,"
            strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
            strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
            strsql += vbNewLine & " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & tbReceptorNombre.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'" & respuestaTimbrado.XMLResultado & "', '" & respuestaTimbrado.Timbre.UUID & "',"
            strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "','" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"
            clsdatos.cargaComando(strsql)

            If clsdatos.ejecutar() <> 0 Then
                LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
            End If

            '''''' Actualiza el consecutivo 

            strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where idConsecutivoF in (1) "
            clsdatos.cargaComando(strsql)
            clsdatos.ejecutar()

            Return True
        Else
            Dim DetalleError As String
            'Si la petición fue erronea muestro el error.
            DetalleError = respuestaTimbrado.CodigoRespuesta
            DetalleError += respuestaTimbrado.MensajeError
            DetalleError += respuestaTimbrado.MensajeErrorDetallado
            'banderaError = True
            LblMensajeCritico.Text = "Error en el timbrado favor de verificar" + DetalleError
            PanelCritico.Visible = True
            PanelCritico.Focus()
            'cargaAgenda()
            'UpdatePanel1.Update()
            ' MessageBox.Show(DetalleError)
            Return False
        End If
    End Function

    Sub enviarFacr(ByRef folioFac As String)
        Dim folioFactura As String = folioFac
        'Dim estadoFac As String = estado_factura.Value
        Dim Path As String
        Dim mail As New MailMessage
        'If estadoFac = "Vigente" Then
        Path = Server.MapPath("/Facturas/" + folioFactura)
        'Else
        'Path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
        'End If

        mail.From = New MailAddress("facturas@stargrupoortopedico.com")

        mail.To.Add(correo.Value)

        mail.Subject = "Factura de servicios de Ortopedia Star"
        mail.Body = "Facturas"
        Try
            Dim FilePath1 As String = Path + ".xml"
            Dim FilePath2 As String = Path + ".pdf"

            'el archivo se adjunta indicándole la ruta 
            mail.Attachments.Add(New Attachment(FilePath1))
            mail.Attachments.Add(New Attachment(FilePath2))

            Dim mailClient As New SmtpClient()

            Dim basicAuthenticationInfo As New NetworkCredential("facturas@stargrupoortopedico.com", "fac2017*")

            mailClient.Host = "mail.stargrupoortopedico.com"

            mailClient.UseDefaultCredentials = True
            mailClient.Credentials = basicAuthenticationInfo
            mailClient.Port = 26

            mailClient.Send(mail)
            LblMensajeAviso.Text = "Se han enviado los archivos de la factura (xml y pdf)"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
        Catch ex As Exception
            LblMensajeCritico.Text = ex.Message
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End Try
    End Sub

    Function cargarTotalPagos() As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT sum(cantidad * costo) as Total " & _
                 " FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                   "where folioConsulta='" & Session("folioConsulta") & "' and codigoempresa = " & Session("codigoEmpresa") & "  and status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Total")) Then
                Return dt.Rows(0).Item("Total").ToString
            Else
                Return "0.0"
            End If

        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            Return "0.0"
        End If
    End Function

    Protected Sub cargaDTGCargosConsulta()
        LimpiarPaneles()
        Dim strSQL, strSQL2 As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim dt3 As New DataTable
        Dim rangoFechas
        rangoFechas = Split(Fechas.Text, " - ")

        If ChkMonetizacion.Checked = True And TxtMonetizacion.Text <> "" Then
            strSQL2 = " SELECT A.idabono,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, " & _
                 "fecha,catServicios.descripcion as servicio,A.abono as abono,A.importe-A.monto as importe,A.idcliente,A.idAbono,A.idcosto as idcosto FROM abonos as A " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[clientes] as CP on A.idcliente=CP.idcliente " & _
                 "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
                 "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
                 "where (A.idcosto= " + DDTiposDeCargos.SelectedValue.Trim + ") and fecha>='" & rangoFechas(0) & "' and fecha<='" & rangoFechas(1) & "' and A.facturado='false' and importe > 0"

            If clsDatos.cargatabla(strSQL2, dt3) = 0 Then
                Dim valorOptimo As Double
                Dim mejorValor As Double
                Dim suma As Double
                Dim dtMochila As New DataTable
                dtMochila.Columns.Add("idabono")
                dtMochila.Columns.Add("CodigoPaciente")
                dtMochila.Columns.Add("fecha")
                dtMochila.Columns.Add("servicio")
                dtMochila.Columns.Add("abono")
                dtMochila.Columns.Add("importe")
                dtMochila.Columns.Add("idcliente")
                dtMochila.Columns.Add("idAbono")
                dtMochila.Columns.Add("idcosto")
                Dim dtFinal As New DataTable

                mejorValor = TxtMonetizacion.Text
                valorOptimo = 0.0

                For i = 0 To dt3.Rows.Count - 1
                    suma = 0
                    For j = i To dt3.Rows.Count - 1
                        suma = suma + dt3.Rows(j).Item("abono")
                        If mejorValor >= suma Then
                            dtMochila.Rows.Add(dt3.Rows(j).Item("idabono"), dt3.Rows(j).Item("CodigoPaciente"), dt3.Rows(j).Item("fecha"), dt3.Rows(j).Item("servicio"), dt3.Rows(j).Item("abono"), dt3.Rows(j).Item("importe"), dt3.Rows(j).Item("idcliente"), dt3.Rows(j).Item("idAbono"), dt3.Rows(j).Item("idcosto"))
                            If valorOptimo < suma Then
                                valorOptimo = suma
                                dtFinal.Clear()
                                dtFinal = dtMochila.Copy()

                            End If
                        Else
                            dtMochila.Clear()
                            Exit For
                        End If
                    Next
                Next
                Dim dif As Double
                dif = mejorValor - valorOptimo

                If dif = 0 Then
                    LblMensajeAviso.Text = "Se encontró la cantidad exacta $" + CStr(mejorValor)
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                Else
                    LblMensajeAdvertencia.Text = "UPSSSS te faltó $" + CStr(Format(dif, "##,##0.00"))
                    PanelAdvertencia.Visible = True
                    PanelAdvertencia.Focus()
                    Exit Sub
                End If

                dtgCargosConsultas.DataSource = dtFinal
                dtgCargosConsultas.DataBind()
            End If
        Else
            If ChkTerapias.Checked = True And TxtTerapias.Text <> "" Then
                Dim NBuscar As Integer
                NBuscar = CInt(TxtTerapias.Text)

                strSQL = " SELECT TOP " & NBuscar & " A.idabono,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, " & _
                      "fecha,catServicios.descripcion as servicio,A.abono as abono,A.importe-A.monto as importe,A.idcliente,A.idAbono,A.idcosto as idcosto FROM abonos as A " & _
                      "inner join [" & clsDatos.BaseDatos & "].[dbo].[clientes] as CP on A.idcliente=CP.idcliente " & _
                      "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
                      "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
                      "where (A.idcosto= " + DDTiposDeCargos.SelectedValue.Trim + ") and fecha>='" & rangoFechas(0) & "' and fecha<='" & rangoFechas(1) & "' and A.facturado='false' and importe > 0 ORDER BY NEWID()"
            Else
                strSQL = " SELECT A.idabono,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, " & _
                      "fecha,catServicios.descripcion as servicio,A.abono as abono,A.importe-A.monto as importe,A.idcliente,A.idAbono,A.idcosto as idcosto FROM abonos as A " & _
                      "inner join [" & clsDatos.BaseDatos & "].[dbo].[clientes] as CP on A.idcliente=CP.idcliente " & _
                      "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
                      "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
                      "where (A.idcosto= " + DDTiposDeCargos.SelectedValue.Trim + ") and fecha>='" & rangoFechas(0) & "' and fecha<='" & rangoFechas(1) & "' and A.facturado='false' and importe > 0"
            End If

            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                dtgCargosConsultas.DataSource = dt
                dtgCargosConsultas.DataBind()
            Else
                LblMensajeCritico.Text = " Error 9800: " + clsDatos.MensajeError
                PanelCritico2.Visible = True
                Exit Sub
            End If
        End If

        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos

        strSQL = "SELECT iddatosfac,nombre FROM [" & claseDatos.BaseDatos & "].[dbo].[catFacturas] " & _
            "where iddatosfac=" + cmbfacturacion.SelectedValue.Trim + ""
        resultadoBusqueda = 0
        resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)

        If resultadoBusqueda = -1 Then
            LblMensajeCritico.Text = claseDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
            Exit Sub
        End If
        If resultadoBusqueda = 1 Then
            LblMostarDecision.Text = "No existe el paciente!"
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            'HdPregunta.Value = "2"
            Exit Sub
        End If

        LstBoxPacientes.Focus()
        LstBoxPacientes.SelectedIndex = 0
    End Sub

    Function consultasAfacturar(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef dt3 As DataTable, ByRef dt4 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""
        count = 0

        For Each row As GridViewRow In dtgCargosConsultas.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then
                If count > 0 Then
                    listaFac += ","
                    listaSQL += ","
                End If
                listaFac += "'" + row.Cells(0).Text + "'"
                listaSQL += row.Cells(0).Text
                count = count + 1
            End If
        Next

        If listaFac = "" Then
            Return False
        Else
            strSQL = "select  abonos.ClaveProdServ,abonos.ClaveUnidad,abonos.Unidad,Descuento,abonos.NoIdentificacion,sum(cantidad) as cantidad,catServicios.descripcion as descripcion,cast((importe-monto) AS decimal(16,2)) as costo,cast((importeIVA) AS decimal(16,2)) as importeIVA,abonos.tasaIVA, abonos.Objetoimp FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
            "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
            "where idabono in (" & listaFac & ")" & _
          "group by abonos.importe, abonos.monto,abonos.ClaveProdServ,abonos.ClaveUnidad,abonos.Unidad,catServicios.descripcion,Descuento,abonos.NoIdentificacion,costo,importeIVA,abonos.tasaIVA, abonos.Objetoimp"

            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importe-monto*cantidad+importeIVA) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
                    "where idabono in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importe-monto*cantidad) AS decimal(16,2)) AS STotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
                    "where idabono in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt3) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importeIVA) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
                     "where idabono in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt4) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            listaFolios = listaFac
            listaFoliosSQL = listaSQL
            Return True
        End If
    End Function

    Function consultasAfacturarG()
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""
        Dim listaFolios As String
        Dim listaFoliosSQL As String
        count = 0

        For Each row As GridViewRow In dtgCargosConsultas.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then
                If count > 0 Then
                    listaFac += ","
                    listaSQL += ","
                End If
                listaFac += "'" + row.Cells(0).Text + "'"
                listaSQL += row.Cells(0).Text
                count = count + 1
            End If
        Next

        If listaFac = "" Then
            Return False
        Else
            strSQL = "select sum(cantidad) as cantidad,catServicios.descripcion as descripcion,cast((importe-monto) AS decimal(16,2)) as costo FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
            "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
            "where idabono in (" & listaFac & ") " & _
            "group by abonos.idcosto,importe,monto,catServicios.descripcion"
            'Dim dt1 As DataTable
            'If clsDatos.cargatabla(strSQL, dt1) = 0 Then
            '    If dt1.Rows.Count > 0 Then
            '        GdConsultasXpagar.DataSource = dt1
            '        GdConsultasXpagar.DataBind()
            '    End If
            'Else
            '    LblMensajeAdvertencia.Text = clsDatos.MensajeError
            '    PanelAdvertencia.Visible = True
            'End If
            Dim dt2 As DataTable

            If clsDatos.cargatabla(strSQL, dt2) = 0 Then
                If dt2.Rows.Count > 0 Then
                    gridDetalles.DataSource = dt2
                    gridDetalles.DataBind()
                End If
            Else
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If

            listaFolios = listaFac
            listaFoliosSQL = listaSQL
            Return True
        End If
    End Function

    Sub llenaDatosf()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "select iddfac,idcosto from catCostos where idcosto ='" + DDTiposDeCargos.SelectedValue.ToString.Trim + "' "

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim idclave As String = dt.Rows(0).Item("iddfac")

                If idclave = "" Then
                    idclave = "0"
                End If
                cmbfacturacion.SelectedValue = idclave

                cmbfacturacion.Enabled = False

            Else
                LblMensajeCritico.Text = "Error en el dato de facturación"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
    End Sub

    Private Sub DDTiposDeCargos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDTiposDeCargos.SelectedIndexChanged
        ocultarPaneles()

        If DDTiposDeCargos.SelectedValue = "1038 or A.idcosto=1027" Then
            cmbfacturacion.SelectedValue = 1
        Else
            llenaDatosf()
        End If

        cargaDTGCargosConsulta()
    End Sub

    Protected Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAdvertencia2.Visible = False
        PanelAviso2.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelCritico2.Visible = False
        PanelDesicion.Visible = False
        PanelCritico2.Visible = False
    End Sub

    Private Sub ChkMonetizacion_CheckedChanged(sender As Object, e As EventArgs) Handles ChkMonetizacion.CheckedChanged
        If ChkMonetizacion.Checked = True Then
            div_dinero2.Visible = True
            div_numeroFac2.Visible = False
            ChkTerapias.Checked = False
        Else
            div_dinero2.Visible = False
        End If
    End Sub

    Private Sub ChkTerapias_CheckedChanged(sender As Object, e As EventArgs) Handles ChkTerapias.CheckedChanged
        If ChkTerapias.Checked = True Then
            div_numeroFac2.Visible = True
            div_dinero2.Visible = False
            ChkMonetizacion.Checked = False
        Else
            div_numeroFac2.Visible = False
        End If
    End Sub

    Sub LimpiarPaneles()
        PanelAdvertencia.Visible = False
        PanelCritico.Visible = False
        PanelAvisos.Visible = False
    End Sub
End Class