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


Public Class FacturacionNC
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Dim _c As Comprobante = New Comprobante()
    Private _comprobante As Comprobante

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'cargaDdConceptoPago()
            'cargaDdMedico()
            'cargaDdConsultorioMedico()
            cargarmetodopago()
            cargaddTipoDePago()
            cargacExportacion()
            'cargacObjeto()
            cargaUCFDI()
            cargaRegimenFiscal()
            cargarTipoRelacion()
            'div_2.Visible = False
            'div_3.Visible = False
        End If
        ocultarPaneles()
        'btn_facturarC.Visible = False
        btn_facturar.Visible = False
    End Sub

    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
        'BotonBuscar.InnerText = "Buscando.."
        Dim strSQL As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos
        If TxbMaterno.Text = "" And TxbPaterno.Text = "" And TxbNombre.Text = "" Then
            Exit Sub
        End If
        strSQL = "SELECT idcliente,paterno+' '+materno+' '+Nombre FROM [" & claseDatos.BaseDatos & "].[dbo].[clientes] " & _
            "where paterno like '%" & TxbPaterno.Text.Trim & "%' and nombre like '%" & TxbNombre.Text.Trim & "%' and materno like '%" & TxbMaterno.Text & "%'"
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
            Exit Sub
        End If
        LstBoxPacientes.Focus()
        LstBoxPacientes.SelectedIndex = 0
    End Sub


    Protected Sub BtnVer_Click()
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        cargaDTGCargosConsulta()
    End Sub

    Protected Sub facturar(sender As Object, e As EventArgs)
        ocultarPanelesNC()
        Dim count As Integer = 0
        Dim listPagos As String = ""
        Dim pacientesv As String = ""
        'Dim fila As String
        For Each row As GridViewRow In dtgCargosConsultas.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then


                If count > 0 Then
                    listPagos += ","
                    pacientesv += ","
                End If
                listPagos += row.Cells(0).Text ' se agrega a una lista todos los filios a facturar ejem: C3243|C5345|C0999 
                pacientesv += row.Cells(1).Text
                count = count + 1  ' para saber cuantos filios de consulta se agregaron a la lista para facturar.
                bandera.Value = 0
                div_input_razon_social.Visible = True
                CargarDatosFacturacion(LstBoxPacientes.SelectedValue)
                LlenarComboRazonSocial(LstBoxPacientes.SelectedValue)
                iniciaComprobante()
                consultasAfacturarG()
                'div_input_razon_social.Visible = False
                div_direccion.Visible = False
                div_cp.Visible = True
                div_ciudad.Visible = False
                div_estado.Visible = False
                div_pais.Visible = False
                div_UPfiscal.Visible = False
                'div_dfacturacion.Visible = False
                div_emisor.Visible = False
                div_dfacturacion.Visible = True
                'pacientesvi.Value = pacientesv
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
                C_facturar.Visible = True
                'CM_facturar.Visible = False
            End If
        Next
        'listPagos += "'''"
    End Sub
    'Protected Sub facturarC(sender As Object, e As EventArgs)
    '    Dim count As Integer = 0
    '    Dim listPagos As String = ""
    '    Dim pacientesv As String = ""
    '    For Each row As GridViewRow In DtgCargosManuales.Rows
    '        If CType(row.FindControl("chk"), CheckBox).Checked Then
    '            If count > 0 Then
    '                listPagos += ","
    '                pacientesv += ","
    '            End If
    '            listPagos += row.Cells(0).Text ' se agrega a una lista todos los filios a facturar ejem: C3243|C5345|C0999 
    '            pacientesv += row.Cells(1).Text
    '            count = count + 1  ' para saber cuantos filios de consulta se agregaron a la lista para facturar.
    '            bandera.Value = 0
    '            div_input_razon_social.Visible = False
    '            CargarDatosFacturacion(LstBoxPacientes.SelectedValue)
    '            LlenarComboRazonSocial(LstBoxPacientes.SelectedValue)
    '            consultasAfacturarC()
    '            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
    '            C_facturar.Visible = False
    '            CM_facturar.Visible = True
    '        End If
    '    Next
    'End Sub

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
        strsql = "SELECT idcliente,elnombre,razonsocial,rfc, email,cp, pais, edo, ciudad, codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + CodigoPaciente + " ORDER BY idCliente"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                dpaciente.Value = dt.Rows(0).Item("elnombre")
                nompaciente.Value = dpaciente.Value
                tbReceptorNombre.Value = dt.Rows(0).Item("razonsocial")
                tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                correo.Value = dt.Rows(0).Item("email")
                cp.Value = dt.Rows(0).Item("cp")
                ciudad.Value = dt.Rows(0).Item("ciudad")
                estado.Value = dt.Rows(0).Item("edo")
                pais.Value = dt.Rows(0).Item("pais")
                If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                    tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                Else
                    tbReceptorRegimenFiscal.SelectedValue = "999"
                End If
                id_razon_social.Value = dt.Rows(0).Item("idcliente")

            End If
        End If
    End Sub

    Sub LlenarComboRazonSocial(CodigoPaciente)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        codigo_paciente.Value = CodigoPaciente
        strSQL = "SELECT idcliente,razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + CodigoPaciente + " ORDER BY idCliente"
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
                div_input_razon_social.Visible = True
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
            'Habilitar bandera para dar de alta el nuevo
            bandera.Value = 1
        Else
            strsql = "SELECT idcliente,razonsocial,rfc, email,cp, pais, edo, ciudad, codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + select_razon_social.SelectedValue
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbReceptorNombre.Value = dt.Rows(0).Item("razonsocial")
                    tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                    correo.Value = dt.Rows(0).Item("email")
                    cp.Value = dt.Rows(0).Item("cp")
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                    estado.Value = dt.Rows(0).Item("edo")
                    pais.Value = dt.Rows(0).Item("pais")
                    If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                        tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                    Else
                        tbReceptorRegimenFiscal.SelectedValue = "999"
                    End If
                    id_razon_social.Value = dt.Rows(0).Item("idcliente")
                End If
            End If
        End If
    End Sub

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
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET razonsocial='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() = 0 Then
                            strsql = "SELECT idCliente, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] " & _
                                      "WHERE razonsocial='" & tbReceptorNombre.Value & "' and rfc='" & tbReceptorRFC.Value & "'  and email='" & correo.Value & "' "
                            If clsdatos.cargatabla(strsql, dt) = 0 Then
                                id_razon_social.Value = dt.Rows(0).Item("idcliente")
                                actualizartotalnc()
                                Facturar33()
                                cargaDTGCargosConsulta()
                            End If

                        Else
                            LblMensajeCritico.Text = clsdatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    Else
                        'Actualizar la razón social
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET razonsocial='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() = 0 Then
                            actualizartotalnc()
                            Facturar33()
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
    Sub actualizartotalnc()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable
        Dim listaFac As String = ""
        Dim count As Integer

        If totalNC.Value = "" Then
            LblMensajeAdvertencia.Text = "No se puede realizar Notas de Credito con valor 0"
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()


        Else
            count = 0
            For Each row As GridViewRow In dtgCargosConsultas.Rows
                If CType(row.FindControl("chk"), CheckBox).Checked Then
                    If count > 0 Then
                        listaFac += ","
                    End If
                    listaFac += "'" + row.Cells(0).Text + "'"
                    count = count + 1
                End If
            Next
            If listaFac = "" Then
                LblMensajeAdvertenciaM.Text = "Favor de Seleccionar facturas"
                PanelAdvertenciaM.Visible = True
                PanelAdvertenciaM.Focus()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
            Else

                strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[factura] SET UPagoRelacion='" & totalNC.Value & "' WHERE idFactura in (" & listaFac & ")"
                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = 0 Then

                Else
                    LblMensajeCriticoM.Text = clsdatos.MensajeError
                    PanelCriticoM.Visible = True
                    PanelCriticoM.Focus()
                End If

            End If


        End If

       

    End Sub
    Private Sub extraerCampos()
        Dim hoy As DateTime = DateTime.Now
        Dim cuenta As Int32 = 0
        Dim contador As Int32 = 0
        Dim cuentau As Int32 = 0
        Dim contadoru As Int32 = 0
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
        _c.CfdiRelacionados.TipoRelacion = tbTipoRelacion.SelectedValue
        _c.Emisor.Nombre = tbEmisorNombre.Value
        _c.Emisor.Rfc = tbEmisorRFC.Value
        _c.Emisor.RegimenFiscal = tbEmisorRegimenFiscal.Value
        _c.Receptor.Nombre = tbReceptorNombre.Value
        _c.Receptor.Rfc = tbReceptorRFC.Value
        _c.Receptor.DomicilioFiscalReceptor = cp.Value
        _c.Receptor.RegimenFiscalReceptor = tbReceptorRegimenFiscal.SelectedValue
        _c.Exportacion = tbExportacion.SelectedValue
        '_c.Receptor.NumRegIdTrib = tbReceptorNumRegIdTrib.Text
        '_c.Receptor.ResidenciaFiscal = tbReceptorResidenciaFiscal.Text
        '_c.Receptor.UsoCFDI = tbReceptorUsoCFDI.SelectedValue

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

        '_c.Impuestos.TotalImpuestosRetenidos = toDecimal(tbImpuestosTotalImpuestosRetenidos.Text)



        '************** -DATOS DEL COMPROBANTE- ************

        'Dim hoy As DateTime = DateTime.Now
        Dim c As New Comprobante()
        Dim total As String
        Dim COIva As String
        Dim stotal, tiva As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal As DataTable
        Dim dtuuid As DataTable
        Dim listafolios, listaFoliosSQL As String


        '    formaPago = ""
        '    total = ""

        '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
        'cargardatosFac(formaPago, total)
        If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, dtuuid, listafolios, listaFoliosSQL) Then

            'Proceso para la verificacion de iva

            COIva = dtiTotal.Rows(0).Item("ivatotal")
            If COIva = "0.00" Then
                total = totalNC.Value
                stotal = totalNC.Value
                tiva = dtiTotal.Rows(0).Item("ivatotal")
                _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)


                Dim t As Traslado = New Traslado()

                t.Base = totalNC.Value
                t.Importe = tiva
                't.Importe = toDecimal(stotal * 0.16)
                t.TipoFactor = "Exento"
                'If tiva = "0.00" Then
                '    t.TasaOCuota = "0.000000"
                'Else
                '    t.TasaOCuota = "0.160000"
                'End If
                t.Impuesto = "002"
                _c.Impuestos.Traslados.Add(t)
            Else
                total = toDecimal(totalNC.Value * 1.16)
                stotal = totalNC.Value
                tiva = Format(CDbl(stotal) * CDbl(0.16), "####0.00")
                _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)


                Dim t As Traslado = New Traslado()

                t.Base = totalNC.Value
                t.Importe = Format(CDbl(stotal) * CDbl(0.16), "####0.00")
                't.Importe = toDecimal(stotal * 0.16)
                t.TipoFactor = "Tasa"
                If tiva = "0.00" Then
                    t.TasaOCuota = "0.000000"
                Else
                    t.TasaOCuota = "0.160000"
                End If
                t.Impuesto = "002"
                _c.Impuestos.Traslados.Add(t)

            End If




            

            
            variasFacturas = True
        Else
            variasFacturas = False
        End If

        'Dim r As CfdiRelacionado = New CfdiRelacionado()
        'r.UUID = "898372CC-6254-44B4-A944-056EE6EDCDAC"
        '_c.CfdiRelacionados.CfdiRelacionado.Add(r)

        _c.Total = total
        _c.Subtotal = stotal
        _c.Descuento = 0



        c.CfdiRelacionados.CfdiRelacionado = New List(Of CfdiRelacionado)()
        Do While contadoru = cuentau
            If variasFacturas = True Then
                For Each filau As DataRow In dtuuid.Rows
                    Dim uuid1 As New CfdiRelacionado()
                    uuid1.UUID = filau.Item("uuid")

                    'c.CfdiRelacionados.CfdiRelacionado.Add(uuid1)
                    _c.CfdiRelacionados.CfdiRelacionado.Add(uuid1)
                Next

            End If
            contadoru = contadoru + 1
        Loop

        '************ -CONCEPTO- *******************


        c.Conceptos = New List(Of Concepto)()
        Do While contador = cuenta

            If variasFacturas = True Then





                For Each fila As DataRow In dtConceptos.Rows
                    Dim concepto1 As New Concepto()
                    concepto1.ClaveProdServ = "84111506"
                    concepto1.ClaveUnidad = "ACT"
                    concepto1.Cantidad = fila.Item("cantidad")
                    concepto1.Unidad = "Actividad"
                    concepto1.NoIdentificacion = "noIdentificacion"

                    concepto1.ObjetoImp = "02"
                    concepto1.Descripcion = fila.Item("Descripcion")
                    concepto1.Importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
                    concepto1.ValorUnitario = fila.Item("costo")
                    c.Conceptos.Add(concepto1)

                    Dim tc As New TrasladoC()
                    'tc.Importe = fila.Item("importeIVA") * fila.Item("cantidad") 'Multiplicar con la cantidad

                    tc.Importe = toDecimal((fila.Item("costo") * 0.16) * fila.Item("cantidad")) 'Multiplicar con la cantidad
                    'tc.TasaOCuota = fila.Item("tasaIVA")
                    'If tc.Importe = "0.00" Then
                    '    tc.TasaOCuota = "0.000000"
                    'Else
                    '    tc.TasaOCuota = "0.160000"
                    'End If


                    If COIva = "0.00" Then
                        tc.TipoFactor = "Exento"
                        tc.Impuesto = "002"
                    Else
                        tc.TipoFactor = "Tasa"
                        tc.Impuesto = "002"
                        If tc.Importe = "0.00" Then
                            tc.TasaOCuota = "0.000000"
                        Else
                            tc.TasaOCuota = "0.160000"
                        End If
                    End If


                    'tc.TipoFactor = "Exento"
                    'tc.Impuesto = "002"
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



        strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from consecutivoFac where idConsecutivoF=2"

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
        tbVersion.Value = "4.0"
        dtpFecha.Value = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))

        'tbNoCertificado.Text = ""
        'numDescuento.Value = 0
        tbMoneda.Value = "MXN"
        tbTipoComprobante.Value = "E"
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


        tbReceptorUsoCFDI.SelectedValue = "6"

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
    Private Sub cargarTipoRelacion()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable

        strSQL = "SELECT clave, Descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatTipoRelacion] " & _
                "where estatus='A' order by CodigoTipoRelacion"

        If funcion.llenadropdown(strSQL, tbTipoRelacion) = 0 Then
            tbTipoRelacion.SelectedValue = 1
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
                LblMensajeAviso.Text = " Factura Realizada con Exito!. E igual Se ha enviado los archivos de la factura (xml y pdf)"
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


        'respuestaTimbrado = serviciotimbrado.TimbrarCFDI("FMD020730PQ5", "VrGwKXeePbE#", stringXML, _c.Serie + _c.Folio.ToString)
        respuestaTimbrado = serviciotimbrado.TimbrarCFDI("MSI1603225B1", "uH9t%yfrR+", stringXML, _c.Serie + _c.Folio.ToString)

        '//Obteniendo la respuesta se valida que haya sido exitosa.

        If respuestaTimbrado.OperacionExitosa Then

            'Guardo el XML timbrado.
            DocumentoXML.LoadXml(respuestaTimbrado.XMLResultado)
            DocumentoXML.Save(rutaXML)

            Dim variasFacturas As Boolean
            Dim dtConceptos, dtTotal As DataTable
            Dim dtsTotal, dtiTotal As DataTable
            Dim dtuuid As DataTable
            Dim listafolios, listaFoliosSQL As String
            Dim strsql As String
            Dim clsdatos As New ClaseDatos

            '''''' Guarda la factura
            Dim foliosConsulta As String
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, dtuuid, listafolios, listaFoliosSQL) Then

                variasFacturas = True
            Else
                variasFacturas = False
            End If
            If variasFacturas = True Then
                foliosConsulta = listaFoliosSQL
            Else

                foliosConsulta = "'" + fconsulta.Value + "'"
            End If



          
            strsql = "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc," & _
            " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad," & _
            " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,CodigoRelacion,UPagoRelacion,IDDocRelacion,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  " & _
            " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "'," & _
            " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & tbReceptorNombre.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'" & respuestaTimbrado.XMLResultado & "', '" & respuestaTimbrado.Timbre.UUID & "'," & _
            " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "','" & _c.CfdiRelacionados.TipoRelacion & "','" & _c.Total & "'," & listafolios & ",'" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() <> 0 Then
                LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
            End If

            '''''' Actualiza el consecutivo 

            strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where  idConsecutivoF=2"
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

    Protected Sub cargaDTGCargosConsulta()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim dt2 As New DataTable



        strSQL = " SELECT A.idFactura,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, fecha,A.serie,A.num_factura," & _
                    "A.importe as importe,A.uuid " & _
                    "FROM factura as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[clientes] as CP on A.idcliente=CP.idcliente " & _
                     "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
                     "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
                    " where A.idCliente=" & LstBoxPacientes.SelectedValue & "  and A.estado='Vigente' and importe > 0 and (A.version=3.3 OR A.version=4.0) "

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            'idcosto.value = dt.Rows(0).Item("idcosto")
            dtgCargosConsultas.DataSource = dt
            dtgCargosConsultas.DataBind()
            'btn_facturarC.Visible = False
            btn_facturar.Visible = True
        Else
            LblMensajeCritico.Text = " Error 9800: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub


    Function consultasAfacturar(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef dt3 As DataTable, ByRef dt4 As DataTable, ByRef dt5 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
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
            strSQL = "select sum(cantidad) as cantidad,'Nota de Crédito que aplica al Comprobante Fiscal con Folio' +' '+serie +' - '+ num_factura +' - '+ uuid as Descripcion,cast((UPagoRelacion) AS decimal(16,2)) as costo,cast((importeIVA) AS decimal(16,2)) as importeIVA FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
            "where idFactura in (" & listaFac & ")" & _
          "group by UPagoRelacion,importeIVA,serie,num_factura,uuid"

            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(((UPagoRelacion)*cantidad)+importeIVA) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                    "where idFactura in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM((UPagoRelacion)*cantidad) AS decimal(16,2)) AS STotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                    "where idFactura in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt3) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importeIVA) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                     "where idFactura in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt4) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select uuid FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                     "where idFactura in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt5) <> 0 Then
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


            strSQL = " select sum(cantidad) as cantidad,serie,num_factura,uuid," & _
                        "CAST(SUM(((importe)*cantidad)+importeIVA) AS decimal(16,2)) AS costoTotal " & _
                        "FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                        "where idFactura in (" & listaFac & ") group by factura.idFactura,factura.serie,num_factura,factura.uuid "



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
    Protected Sub ocultarPanelesNC()
        PanelAdvertenciaM.Visible = False
        PanelCriticoM.Visible = False

    End Sub

End Class