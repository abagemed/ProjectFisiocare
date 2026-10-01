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


Public Class FacturacionClientes1
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Dim _c As Comprobante = New Comprobante()
    Private _comprobante As Comprobante

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            cargaDdConceptoPago()
            cargaDdMedico()
            'cargaddTipoDePago()
            cargaDdConsultorioMedico()
            cargarmetodopago()
            cargaddTipoDePago()
            cargacExportacion()
            cargacObjeto()
            cargaUCFDI()
            cargaRegimenFiscal()
            'div_1.Visible = False
            div_2.Visible = True
            div_3.Visible = False
            Datos_especiales.Checked = False
        End If
        ocultarPaneles()
        btn_facturarC.Visible = False
        btn_facturar.Visible = False
    End Sub
    'busqueda de pacientes-general ok AB
    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
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
    'proceso de factura terapias
    Protected Sub facturar(sender As Object, e As EventArgs)

        'validacion de agrupar facturas

        'If Session("AgruparFactura") = 0 Then
        '    Agrupar_Factura.Visible = False
        'Else
        '    Agrupar_Factura.Visible = True
        'End If
        Agrupar_Factura.Visible = True
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
                div_input_razon_social.Visible = true
                CargarDatosFacturacion(LstBoxPacientes.SelectedValue)
                LlenarComboRazonSocial(LstBoxPacientes.SelectedValue)
                LlenarComboemisor()
                CargarDatosemisor()
                iniciaComprobante()
                consultasAfacturarG()
                'div_input_razon_social.Visible = False
                div_direccion.Visible = False
                'AB SAT 4.0
                div_cp.Visible = True
                div_ciudad.Visible = False
                div_estado.Visible = False
                div_pais.Visible = False
                div_UPfiscal.Visible = False
                div_emisor.Visible = False
                div_dfacturacion.Visible = True
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
                C_facturar.Visible = True
                CM_facturar.Visible = False
            Else

            End If
        Next

    End Sub
    'proceso de factura otros cargos AB 01072020
    Protected Sub facturarC(sender As Object, e As EventArgs)

        Agrupar_Factura.Visible = False

        Dim count As Integer = 0
        Dim listPagos As String = ""
        Dim pacientesv As String = ""
        For Each row As GridViewRow In DtgCargosManuales.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then
                If count > 0 Then
                    listPagos += ","
                    pacientesv += ","
                End If
                listPagos += row.Cells(0).Text ' se agrega a una lista todos los filios a facturar ejem: C3243|C5345|C0999 
                pacientesv += row.Cells(1).Text
                count = count + 1  ' para saber cuantos filios de consulta se agregaron a la lista para facturar.
                bandera.Value = 0
                div_input_razon_social.Visible = False
                div_direccion.Visible = False
                'AB sat 4.0
                div_cp.Visible = True
                div_ciudad.Visible = False
                div_estado.Visible = False
                div_pais.Visible = False
                div_UPfiscal.Visible = False
                div_emisor.Visible = False
                div_dfacturacion.Visible = True
                div_buscaremisor.Visible = True
                CargarDatosFacturacion(LstBoxPacientes.SelectedValue)
                LlenarComboRazonSocial(LstBoxPacientes.SelectedValue)
                LlenarComboemisor()
                CargarDatosemisor()
                iniciaComprobanteCM()
                consultasAfacturarC()
                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)
                C_facturar.Visible = False
                CM_facturar.Visible = True
            End If
        Next

    End Sub
    'AB 02/07/2020
    Sub LlenarComboemisor()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT iddatosfacturacion,razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[Catfacturacion] where codigoEmpresa= '1' ORDER BY iddatosfacturacion"
        If funciones.llenadropdown(strSQL, selectEmisorNombre) = -1 Then
            'Error
            div_input_emisor.Visible = True
        Else
            'Se realizó la función, verificar si hay datos
            If selectEmisorNombre.Items.Count = 0 Then
                div_input_emisor.Visible = True
                div_busqueda_emisor.Visible = False
                'div_direccion.Visible = True
                'div_cp.Visible = True
                'div_ciudad.Visible = True
                'div_estado.Visible = True
                'div_pais.Visible = True
                'Habilitar bandera para dar de alta el nuevo
                bandera.Value = 1
            Else
                div_input_emisor.Visible = True
                div_busqueda_emisor.Visible = True
                'selectEmisorNombre.Items.Add("NUEVO")
                bandera.Value = 0
            End If


        End If
    End Sub
    'AB 02/07/2020
    Sub CargarDatosemisor()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbEmisorNombre.Value = ""
        tbEmisorRegimenFiscal.Value = ""
        tbEmisorRFC.Value = ""
        tbLugarExpedicion.Value = ""
        tbFolio.Value = ""
        tbSerie.Value = ""
        'correo.Value = ""
        'direccion.Value = ""
        'cp.Value = ""
        'ciudad.Value = ""
        'estado.Value = ""
        'pais.Value = ""
        'codigo_emisor.Value = 1
        strsql = "SELECT iddatosfacturacion,razonsocial,rfc,regimenfiscal,lugarexpedicion,CAST(CF.consecutivo + 1 AS varchar) as consecutivo, CF.serie FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] AS F " & _
                "inner join [" & clsdatos.BaseDatos & "].[dbo].[ConsecutivoFac] as CF on F.idConsecutivoF  = CF.idConsecutivoF   " & _
                "WHERE iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' and F.codigoEmpresa= '1' ORDER BY iddatosfacturacion"




        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                tbEmisorNombre.Value = dt.Rows(0).Item("razonsocial")
                tbEmisorRFC.Value = dt.Rows(0).Item("rfc")
                tbEmisorRegimenFiscal.Value = dt.Rows(0).Item("regimenfiscal")
                tbLugarExpedicion.Value = dt.Rows(0).Item("lugarexpedicion")
                tbFolio.Value = dt.Rows(0).Item("consecutivo")
                tbSerie.Value = dt.Rows(0).Item("serie")
                'direccion.Value = dt.Rows(0).Item("direccion")
                'cp.Value = dt.Rows(0).Item("cp")
                'ciudad.Value = dt.Rows(0).Item("ciudad")
                'estado.Value = dt.Rows(0).Item("estado")
                'pais.Value = dt.Rows(0).Item("pais")
                id_emisor.Value = dt.Rows(0).Item("iddatosfacturacion")
            End If
        End If


    End Sub
    'AB 02/07/2020
    Sub CambioSelectemisor()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbEmisorNombre.Value = ""
        tbEmisorRFC.Value = ""
        tbEmisorRegimenFiscal.Value = ""
        tbLugarExpedicion.Value = ""
        tbFolio.Value = ""
        tbSerie.Value = ""
        'direccion.Value = ""
        'cp.Value = ""
        'ciudad.Value = ""
        'estado.Value = ""
        'pais.Value = ""
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
            'Habilitar bandera para dar de alta el nuevo
            bandera.Value = 1
        Else
            'strsql = "SELECT iddatosfacturacion,razonsocial,rfc,regimenfiscal,lugarexpedicion FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "'"
            strsql = "SELECT iddatosfacturacion,razonsocial,rfc,regimenfiscal,lugarexpedicion,CAST(CF.consecutivo + 1 AS varchar) as consecutivo, CF.serie FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] AS F " & _
             "inner join [" & clsdatos.BaseDatos & "].[dbo].[ConsecutivoFac] as CF on F.CF.idConsecutivoF  = CF.CF.idConsecutivoF   " & _
             "WHERE iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' and F.codigoEmpresa= '1' ORDER BY iddatosfacturacion"

            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbEmisorNombre.Value = dt.Rows(0).Item("razonsocial")
                    tbEmisorRFC.Value = dt.Rows(0).Item("rfc")
                    tbEmisorRegimenFiscal.Value = dt.Rows(0).Item("regimenfiscal")
                    tbLugarExpedicion.Value = dt.Rows(0).Item("lugarexpedicion")
                    tbFolio.Value = dt.Rows(0).Item("consecutivo")
                    tbSerie.Value = dt.Rows(0).Item("serie")
                    'direccion.Value = dt.Rows(0).Item("direccion")
                    'cp.Value = dt.Rows(0).Item("cp")
                    'ciudad.Value = dt.Rows(0).Item("ciudad")
                    'estado.Value = dt.Rows(0).Item("estado")
                    'pais.Value = dt.Rows(0).Item("pais")
                    id_emisor.Value = dt.Rows(0).Item("iddatosfacturacion")
                End If
            End If
        End If

    End Sub
    'proceso de cargar los datos del paciente AB
    'AB SAT 4.0
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
                If Not IsDBNull(dt.Rows(0).Item("razonsocial")) Then
                    tbReceptorNombre.Value = dt.Rows(0).Item("razonsocial")
                End If
                If Not IsDBNull(dt.Rows(0).Item("rfc")) Then
                    tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                End If
                If Not IsDBNull(dt.Rows(0).Item("email")) Then
                    correo.Value = dt.Rows(0).Item("email")
                End If
                'direccion.Value = dt.Rows(0).Item("direccion")

                If Not IsDBNull(dt.Rows(0).Item("cp")) Then
                    cp.Value = dt.Rows(0).Item("cp")
                End If
                If Not IsDBNull(dt.Rows(0).Item("ciudad")) Then
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                End If
                If Not IsDBNull(dt.Rows(0).Item("edo")) Then
                    estado.Value = dt.Rows(0).Item("edo")
                End If
                If Not IsDBNull(dt.Rows(0).Item("pais")) Then
                    pais.Value = dt.Rows(0).Item("pais")
                End If

                If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                    tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                Else
                    tbReceptorRegimenFiscal.SelectedValue = "999"
                End If

                id_razon_social.Value = dt.Rows(0).Item("idcliente")

            End If
        End If
    End Sub
    'llenar el combo clientes a facturas-pacientes
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
            cp.Disabled = False
            'Habilitar bandera para dar de alta el nuevo
            bandera.Value = 1
        Else
            strsql = "SELECT idcliente,razonsocial,rfc, email,cp, pais, edo, ciudad, codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + select_razon_social.SelectedValue
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbReceptorNombre.Value = dt.Rows(0).Item("razonsocial")
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
                    id_razon_social.Value = dt.Rows(0).Item("idcliente")
                End If
            End If
        End If
    End Sub
    ' facturar terapias AB
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
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET razonsocial='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)

                        If clsdatos.ejecutar() = 0 Then
                            strsql = "SELECT idCliente, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente = " + id_razon_social.Value

                            If clsdatos.cargatabla(strsql, dt) = 0 Then
                                id_razon_social.Value = dt.Rows(0).Item("idcliente")
                                Facturar33()
                                cargaDTGCargosConsulta()
                            End If

                        Else
                            LblMensajeCritico.Text = clsdatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    Else


                        If Datos_especiales.Checked Then
                            'Actualizar la razón social
                            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                            clsdatos.cargaComando(strsql)
                            If clsdatos.ejecutar() = 0 Then
                                Facturar33()
                                cargaDTGCargosConsulta()
                            Else
                                LblMensajeCritico.Text = clsdatos.MensajeError
                                PanelCritico.Visible = True
                                PanelCritico.Focus()
                            End If

                            ''''en caso de los apostrofes
                            Facturar33()
                            cargaDTGCargosConsulta()

                        Else

                            'Actualizar la razón social
                            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET razonsocial='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                            clsdatos.cargaComando(strsql)
                            If clsdatos.ejecutar() = 0 Then
                                Facturar33()
                                cargaDTGCargosConsulta()
                            Else
                                LblMensajeCritico.Text = clsdatos.MensajeError
                                PanelCritico.Visible = True
                                PanelCritico.Focus()
                            End If

                            ''''en caso de los apostrofes
                            Facturar33()
                            cargaDTGCargosConsulta()

                        End If

                        
                    End If
                End If


            End If
        End If
    End Sub
    'Extraer campos de facturacion de terapias
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
        'AB SAT 4.0
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
        Dim stotal, tiva As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal As DataTable
        Dim listafolios, listaFoliosSQL As String


        'proceso de agrupar conceptos y facturar

        If Agrupar_Factura.Checked = True Then
            '++++++++++++++++++++++++  Validar si es del grid-agrupar +++++++++++++++++++++++++++++++ 
            If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then
                total = dtTotal.Rows(0).Item("costoTotal")
                stotal = dtTotal.Rows(0).Item("costoTotal")
                tiva = "0.00"
                _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)


                Dim t As Traslado = New Traslado()
                ' AB SAT 4.0
                t.Base = total
                t.Importe = tiva
                t.TipoFactor = "Tasa"
                If tiva = "0.00" Then
                    t.TasaOCuota = "0.000000"
                Else
                    t.TasaOCuota = "0.160000"
                End If
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

            strsql = " SELECT iddatosfacturacion,CADescripcion, CAClaveProdServ, CAClaveUnidad, CAUnidad FROM [" & clsdatos.BaseDatos & "].[dbo].[CatFacturacion] " & _
                  "WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' and codigoEmpresa= '1'"
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                For Each fila As DataRow In dt.Rows
                    Dim concepto1 As New Concepto()
                    concepto1.ClaveProdServ = fila.Item("CAClaveProdServ")
                    concepto1.ClaveUnidad = fila.Item("CAClaveUnidad")
                    concepto1.Cantidad = 1
                    concepto1.Unidad = fila.Item("CAUnidad")
                    concepto1.NoIdentificacion = "noIdentificacion"
                    ' AB SAT 4.0
                    If Not IsDBNull(fila.Item("Objetoimp")) Then
                        concepto1.ObjetoImp = fila.Item("Objetoimp")
                    Else
                        concepto1.ObjetoImp = "02"
                    End If

                    concepto1.Descripcion = fila.Item("CADescripcion")
                    concepto1.Importe = total
                    concepto1.ValorUnitario = total
                    c.Conceptos.Add(concepto1)

                    Dim tc As New TrasladoC()
                    tc.Importe = tiva
                    tc.TasaOCuota = "0.000000"
                    tc.TipoFactor = "Tasa"
                    tc.Impuesto = "002"
                    tc.Base = total

                    concepto1.Impuestos.Traslados.Add(tc)

                    _c.Conceptos.Add(concepto1)

                Next
            Else
                LblMensajeAdvertencia.Text = "Error no se encontraron conceptos para agrupar: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
                PanelAdvertencia.Focus()
            End If

        Else
            '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 

            If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then
                total = dtTotal.Rows(0).Item("costoTotal")
                stotal = dtsTotal.Rows(0).Item("sTotal")
                tiva = dtiTotal.Rows(0).Item("ivatotal")
                _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)

                Dim t As Traslado = New Traslado()
                ' AB SAT 4.0
                't.Base = dtsTotal.Rows(0).Item("sTotal")

                t.Base = dtsTotal.Rows(0).Item("sTotal")
                t.Importe = tiva
                't.Importe = tiva



                If dtConceptos.Rows(0).Item("tasaIVA") = "Exento" Then

                    t.TipoFactor = "Exento"
                Else
                    t.TipoFactor = "Tasa"
                    If tiva = "0.00" Then
                        t.TasaOCuota = "0.000000"
                    Else
                        t.TasaOCuota = "0.160000"
                    End If
                End If

                't.TipoFactor = "Tasa"
                'If tiva = "0.00" Then
                '    t.TasaOCuota = "0.000000"
                'Else
                '    t.TasaOCuota = "0.160000"
                'End If





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

                        If fila.Item("tasaIVA") = "Exento" Then
                            tc.TipoFactor = "Exento"
                            tc.Impuesto = "002"
                            tc.Base = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad

                        Else

                            tc.Importe = fila.Item("importeIVA") * fila.Item("cantidad") 'Multiplicar con la cantidad
                            tc.TasaOCuota = fila.Item("tasaIVA")
                            tc.TipoFactor = "Tasa"
                            tc.Impuesto = "002"
                            tc.Base = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad

                        End If

                       

                        concepto1.Impuestos.Traslados.Add(tc)

                        _c.Conceptos.Add(concepto1)

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
        End If

    End Sub

    Public Function toDecimal(snumero As String) As Decimal
        Dim numero As Decimal = 0
        Decimal.TryParse(snumero, numero)
        Return numero
    End Function
    'inicio de comprobante de facturacion de terapias
    Public Sub iniciaComprobante()

        Dim banderaError As Boolean = False
        ' Dim fechapdf As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim folio As String = ""
        Dim serie As String = ""



        strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from consecutivoFac where idConsecutivoF=1"

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
        'tbReceptorUsoCFDI.SelectedValue = "6"

    End Sub
    'iniciar comprobante de otros cargos AB 03072020
    Public Sub iniciaComprobanteCM()

        Dim banderaError As Boolean = False

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim folio As String = ""
        Dim serie As String = ""




        Dim hoy As DateTime = DateTime.Now
        'AB SAT 4.0
        tbVersion.Value = "4.0"
        'tbVersion.Value = "3.3"
        dtpFecha.Value = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))


        tbMoneda.Value = "MXN"
        tbTipoComprobante.Value = "I"

        'AB SAT 4.0
        If tbReceptorRFC.Value = "XAXX010101000" Then
            tbReceptorRegimenFiscal.SelectedValue = "616"
            tbReceptorUsoCFDI.SelectedValue = "23"
            cp.Value = tbLugarExpedicion.Value
            cp.Disabled = True
        Else
            tbReceptorUsoCFDI.SelectedValue = "3"
        End If

        'tbReceptorUsoCFDI.SelectedValue = "6"
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
    Private Sub cargacObjeto()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT cobjeto,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatObjeto] " & _
                "where status='1' order by cObjeto"
        If funcion.llenadropdown(strSQL, tbObjeto) = 0 Then
            tbObjeto.SelectedValue = 1
        Else

            LblMensajeCritico.Text = "Error EObjeto 2351: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    'AB FACTURA TERAPIAS, TIMBRADO Y GUARDADO
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
        'AB 16072021
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
    ' TIMBRAR LA FACTURA-TERAPIAS
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



            If Datos_especiales.Checked Then

                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[abonos] set facturado='true', num_factura='" & _c.Folio & "', serie ='" & _c.Serie & "' " & _
                                   " where idabono in (" & listafolios & ")  "
                strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
                strsql += vbNewLine & " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad,"
                strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,CodigoRelacion,UPagoRelacion,IDDocRelacion,tipofactura,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
                strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
                strsql += vbNewLine & " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & dpaciente.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'1', '" & respuestaTimbrado.Timbre.UUID & "',"
                strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "',0,'" & _c.Total & "',0,1,'" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"
                clsdatos.cargaComando(strsql)
            Else
                '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[abonos] set facturado='true', num_factura='" & _c.Folio & "', serie ='" & _c.Serie & "' " & _
                                   " where idabono in (" & listafolios & ")  "
                strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
                strsql += vbNewLine & " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad,"
                strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,CodigoRelacion,UPagoRelacion,IDDocRelacion,tipofactura,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
                strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
                strsql += vbNewLine & " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & tbReceptorNombre.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'" & respuestaTimbrado.XMLResultado & "', '" & respuestaTimbrado.Timbre.UUID & "',"
                strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "',0,'" & _c.Total & "',0,1,'" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"
                clsdatos.cargaComando(strsql)

            End If

            
            If clsdatos.ejecutar() <> 0 Then
                LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
            End If

            '''''' Actualiza el consecutivo 

            strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where idConsecutivoF=1"
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
    ' PROCESO DE ENVIO DE LA FACTURA
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
            mailClient.Port = 587

            mailClient.Send(mail)
            LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
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
    'AB CONSULTA PRINCIPAL DE TERAPIAS
    Protected Sub cargaDTGCargosConsulta()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim dt2 As New DataTable



        strSQL = " SELECT A.idabono,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, " & _
                    "fecha,catServicios.descripcion as servicio,idcita,A.abono as abono,A.importe-A.monto as importe,(CASE A.coaseguro WHEN 0 THEN 'NO' ELSE 'SI' END) AS coaseguro,A.ligaAbono,A.idcliente,A.idAbono,A.idcosto as idcosto,A.importeIVA as importeIva  FROM abonos as A " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[clientes] as CP on A.idcliente=CP.idcliente " & _
                    "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
                    "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
                    "where A.idCliente=" & LstBoxPacientes.SelectedValue & "  and A.facturado='false' and importe > 0"

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            'idcosto.value = dt.Rows(0).Item("idcosto")
            dtgCargosConsultas.DataSource = dt
            dtgCargosConsultas.DataBind()
            btn_facturarC.Visible = False
            btn_facturar.Visible = True
        Else
            LblMensajeCritico.Text = " Error 9800: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub


    'AB FACTURACION DE TERAPIAS
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
            'AB SAT 4.0
            strSQL = "select  abonos.ClaveProdServ,abonos.ClaveUnidad,abonos.Unidad,Descuento,abonos.NoIdentificacion,sum(cantidad) as cantidad,catServicios.descripcion as descripcion,cast((subtotal-monto) AS decimal(16,2)) as costo,cast((importeIVA) AS decimal(16,4)) as importeIVA,abonos.tasaIVA, abonos.Objetoimp FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
            "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
            "where idabono in (" & listaFac & ")" & _
          "group by abonos.subtotal,abonos.importe, abonos.monto,abonos.ClaveProdServ,abonos.ClaveUnidad,abonos.Unidad,catServicios.descripcion,Descuento,abonos.NoIdentificacion,costo,importeIVA,abonos.tasaIVA, abonos.Objetoimp"

            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            '01032021 se agrego cantidad en la operacion de costo total'
            strSQL = "select CAST(SUM(((subtotal-monto)*cantidad)+(importeIVA*cantidad)) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
                    "where idabono in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM((subtotal-monto)*cantidad) AS decimal(16,2)) AS STotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
                    "where idabono in (" & listaFac & ")"
            If clsDatos.cargatabla(strSQL, dt3) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            '01032021 se agrego cantidad en la operacion de iva total'
            strSQL = "select CAST(SUM(importeIVA*cantidad) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
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
    'AB CONSULTA DE CONCEPTOS DE TERAPIAS
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
            strSQL = "select sum(cantidad) as cantidad,catServicios.descripcion as descripcion,cast((subtotal-monto) AS decimal(16,2)) as costo,CAST(SUM(((subtotal-monto)*cantidad)+(importeIVA*cantidad)) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[abonos] " & _
            "left JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
            "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
            "where idabono in (" & listaFac & ") " & _
            "group by abonos.idcosto,importe,catServicios.descripcion,subtotal,monto"
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

    'INICIO DEL PROCESO DE FACTURACION OTROS CARGOS AB 03072020
    'AB SAT 4.0
    Sub FacturarCM()


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
                    ScriptManager.RegisterStartupScript(Me, GetType(Page), "alertaCM", "alert('Favor de ingresar el correo del cliente')", True)
                    correo.Focus()
                Else
                    ScriptManager.RegisterStartupScript(Me, Me.GetType(), "closeModalFacturaCM", "<script>$('#datos_facturacion').modal('hide');</script>", False)
                    If bandera.Value = 1 Then
                        'Dar de alta la razón social
                        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET razonsocial='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                        clsdatos.cargaComando(strsql)

                        If clsdatos.ejecutar() = 0 Then
                            strsql = "SELECT idCliente, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente = " + id_razon_social.Value
                            If clsdatos.cargatabla(strsql, dt) = 0 Then
                                id_razon_social.Value = dt.Rows(0).Item("idcliente")
                                Facturar33CM()
                                cargaDtgCargosManuales()
                            End If

                        Else
                            LblMensajeCritico.Text = clsdatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    Else

                        If Datos_especiales.Checked Then

                            'Actualizar la razón social
                            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[Clientes] SET rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE idCliente = " + id_razon_social.Value
                            clsdatos.cargaComando(strsql)
                            If clsdatos.ejecutar() = 0 Then
                                Facturar33CM()
                                cargaDtgCargosManuales()
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
                                Facturar33CM()
                                cargaDtgCargosManuales()
                            Else
                                LblMensajeCritico.Text = clsdatos.MensajeError
                                PanelCritico.Visible = True
                                PanelCritico.Focus()
                            End If
                        End If



                    End If
                End If
            End If
        End If



        
    End Sub
    'CONCEPTOS A FACTURAR OTROS CARGOS AB03072020
    Function consultasAfacturarCM(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef dt3 As DataTable, ByRef dt4 As DataTable, ByRef dt5 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""
        count = 0
        For Each row As GridViewRow In DtgCargosManuales.Rows
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



            strSQL = "select  ClaveProdServ,ClaveUnidad,Unidad,Descuento,NoIdentificacion, sum(cantidad) as cantidad,descripcionConcepto,cast((subtotal) AS decimal(16,2)) as costo,cast((importeIVA) AS decimal(16,4)) as importeIVA,tasaIVA,Observaciones,cast((importeISR) AS decimal(16,2)) as importeISR,tasaISR,Objetoimp FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                    "where FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 " & _
                    "group by ClaveProdServ,ClaveUnidad,Unidad,descripcionConcepto,Descuento,NoIdentificacion,subtotal,importeIVA,tasaIVA,Observaciones,importeISR,tasaISR,Objetoimp"
            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(subtotal*cantidad+ImporteIva-ImporteIsr) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                    "where FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(subtotal*cantidad) AS decimal(16,2)) AS STotal FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                   "where FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt3) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importeIvA) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                     "where  FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt4) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importeIsr) AS decimal(16,2))  AS isrtotal FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                     "where  FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt5) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            listaFolios = listaFac
            listaFoliosSQL = listaSQL
            Return True
        End If

    End Function
    ' EXTRAER CAMPOS OTROS CARGOS AB03072020
    Private Sub extraerCamposCM()
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
        'AB SAT 4.0
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
        Dim stotal, tiva, tisr As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal, dtisTotal As DataTable
        Dim listafolios, listaFoliosSQL As String


        '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
        'cargardatosFac(formaPago, total)
        If consultasAfacturarCM(dtConceptos, dtTotal, dtsTotal, dtiTotal, dtisTotal, listafolios, listaFoliosSQL) Then
            total = dtTotal.Rows(0).Item("costoTotal")
            stotal = dtsTotal.Rows(0).Item("sTotal")
            tiva = dtiTotal.Rows(0).Item("ivatotal")
            tisr = dtisTotal.Rows(0).Item("isrtotal")
            _c.Impuestos.TotalImpuestosTrasladados = toDecimal(tiva)

            If tisr = "0.00" Then

            Else

                _c.Impuestos.TotalImpuestosRetenidos = toDecimal(tisr)

            End If


            Dim t As Traslado = New Traslado()
            ' AB SAT 4.0
            t.Base = dtsTotal.Rows(0).Item("sTotal")
            t.Importe = tiva
            t.TipoFactor = "Tasa"
            If tiva = "0.00" Then
                t.TasaOCuota = "0.000000"
            Else
                t.TasaOCuota = "0.160000"
            End If
            t.Impuesto = "002"

            _c.Impuestos.Traslados.Add(t)

            '''PRUEBA IVA
            'Dim t As Traslado = New Traslado()
            't.Importe = tiva
            't.TipoFactor = "Tasa"
            't.TasaOCuota = "0.160000"
            't.Impuesto = "002"

            '_c.Impuestos.Traslados.Add(t)

            If tisr = "0.00" Then

            Else
                ''''' ISR

                Dim r As Retencion = New Retencion()
                r.Importe = tisr
                'r.TipoFactor = "Tasa"
                'r.TasaOCuota = "0.000000"
                r.Impuesto = "001"

                _c.Impuestos.Retenciones.Add(r)

                ''' FIN ISR

            End If


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
                    concepto1.ObjetoImp = fila.Item("Objetoimp")
                    concepto1.Descripcion = fila.Item("descripcionConcepto") + "--" + fila.Item("Observaciones")
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

                    If tisr = "0.00" Then

                    Else
                        ''' ISR

                        Dim tr As New RetencionC()
                        tr.Importe = fila.Item("importeISR") * fila.Item("cantidad") 'Multiplicar con la cantidad
                        tr.TasaOCuota = fila.Item("tasaISR")
                        tr.TipoFactor = "Tasa"
                        tr.Impuesto = "001"
                        tr.Base = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad

                        concepto1.Impuestos.Retenciones.Add(tr)

                        '''' FIN ISR

                    End If




                    _c.Conceptos.Add(concepto1)



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
                        concepto1.Descripcion = fila.Item("descripcionConcepto")
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
    'AB03072020 FACTURAR OTROS CARGOS TIMBRADO Y GUARDADO
    Sub Facturar33CM()
        extraerCamposCM()

        Dim rutaXMLST As New String(Server.MapPath("/Facturas/XMLST/" + _c.Serie + _c.Folio + ".xml"))
        'Dim rutaXMLST As String = ".\XMLST\" + _c.Serie + _c.Folio + ".xml" 'Ruta del archivo XMl sin timbrar
        Dim rutaXMLT As New String(Server.MapPath("/Facturas/XMLT/" + _c.Serie + _c.Folio + ".xml"))
        'Dim rutaXMLT As String = ".\XMLT\" + _c.Serie + _c.Folio + ".xml" 'Ruta del archivo XMl timbrado
        Dim rutaPDF As New String(Server.MapPath("/Facturas/PDFS/" + _c.Serie + _c.Folio + ".pdf"))
        'Dim rutaPDF As String = ".\PDFS\" + _c.Serie + _c.Folio + ".pdf" 'Ruta donde guardar el pdf generado

        'Crea el archivo xml en formato 3.2 de acuerdo al anexo 20 del SAT
        Dim CrearXML As CrearXML = New CrearXML()


        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dpfx As String = ""
        Dim dcer As String = ""
        Dim passcer As String = ""
        Dim mailfacturacion As String = ""
        Dim passwordmail As String = ""
        Dim puertomail As String = ""
        Dim servidormail As String = ""

        strsql = " SELECT directoriopfx, directoriocer, passwordcer,mailfacturacion, passwordmail, puertomail, servidormail FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' and codigoEmpresa= '1'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                dpfx = dt.Rows(0).Item("directoriopfx")
                dcer = dt.Rows(0).Item("directoriocer")
                passcer = dt.Rows(0).Item("passwordcer")
                mailfacturacion = dt.Rows(0).Item("mailfacturacion")
                passwordmail = dt.Rows(0).Item("passwordmail")
                puertomail = dt.Rows(0).Item("puertomail")
                servidormail = dt.Rows(0).Item("servidormail")
            Else
                LblMensajeCritico.Text = "Error en los certificados!"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If

        Dim apfx As New String(Server.MapPath(dpfx))
        Dim acer As New String(Server.MapPath(dcer))


        CrearXML.Create(_c, rutaXMLST, apfx, passcer, acer)

        'Dim apfx As New String(Server.MapPath("/FILESSAT/PFX.pfx"))
        'Dim acer As New String(Server.MapPath("/FILESSAT/CER.cer"))


        'CrearXML.Create(_c, rutaXMLST, apfx, "orto2014", acer)

        'Hace el timbrado con Folios digitales
        Dim imagen As Image = Image.FromFile(Server.MapPath("/LOGO/logo.jpg"))
        Dim Npaciente As String = nompaciente.Value
        Dim dobservaciones As String = dcobservaciones.Value
        If TimbrarCM(rutaXMLST, rutaXMLT, tbUsuarioFoliosDigitales.Value, tbPasswordFoliosDigitales.Value) Then
            Dim CreaPDF = New CreaPDF(rutaXMLT, rutaPDF, imagen, Npaciente, dobservaciones)

            Dim folioFactura As String = _c.Serie + _c.Folio
            'Dim estadoFac As String = estado_factura.Value
            Dim Path1 As String
            Dim Path2 As String
            Dim mail As New MailMessage
            Path1 = Server.MapPath("/Facturas/XMLT/" + folioFactura)
            Path2 = Server.MapPath("/Facturas/PDFS/" + folioFactura)


            mail.From = New MailAddress(mailfacturacion)

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

                Dim basicAuthenticationInfo As New NetworkCredential(mailfacturacion, passwordmail)

                mailClient.Host = servidormail

                mailClient.UseDefaultCredentials = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Port = puertomail

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
    End Sub
    'AB 03072020 TIMBRAR FACTURA OTROS CARGOS
    Private Function TimbrarCM(rutaXMLTimbrar As String, rutaXML As String, usuario As String, password As String) As String
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


        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim upac As String = ""
        Dim ppac As String = ""
        Dim codigoc As String = ""
        Dim iddfacturacion As String = ""

        strsql = " SELECT  iddatosfacturacion,usuariopac, passwordpac,  idConsecutivoF FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' and codigoEmpresa= '1'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                iddfacturacion = dt.Rows(0).Item("iddatosfacturacion")
                upac = dt.Rows(0).Item("usuariopac")
                ppac = dt.Rows(0).Item("passwordpac")
                codigoc = dt.Rows(0).Item("idConsecutivoF")
            Else
                LblMensajeCritico.Text = "Error en datos PAC"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If

        respuestaTimbrado = serviciotimbrado.TimbrarCFDI(upac, ppac, stringXML, _c.Serie + _c.Folio.ToString)
        'respuestaTimbrado = serviciotimbrado.TimbrarCFDI("OST141127IEA", "kBMEtFvuTrr#", stringXML, _c.Serie + _c.Folio.ToString)

        '//Obteniendo la respuesta se valida que haya sido exitosa.

        If respuestaTimbrado.OperacionExitosa Then

            'Guardo el XML timbrado.
            DocumentoXML.LoadXml(respuestaTimbrado.XMLResultado)
            DocumentoXML.Save(rutaXML)

            Dim variasFacturas As Boolean
            Dim dtConceptos, dtTotal As DataTable
            Dim dtsTotal, dtiTotal, dtisTotal As DataTable
            Dim listafolios, listaFoliosSQL As String
            'Dim strsql As String
            'Dim clsdatos As New ClaseDatos

            '''''' Guarda la factura
            Dim foliosConsulta As String
            '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
            If consultasAfacturarCM(dtConceptos, dtTotal, dtsTotal, dtiTotal, dtisTotal, listafolios, listaFoliosSQL) Then

                variasFacturas = True
            Else
                variasFacturas = False
            End If
            If variasFacturas = True Then
                foliosConsulta = listaFoliosSQL
            Else

                foliosConsulta = "'" + fconsulta.Value + "'"
            End If

            If Datos_especiales.Checked Then
                strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[CargosManualesClientes] set Facturado='True', num_factura='" & _c.Folio & "', serie ='" & _c.Serie & "' " & _
                               " where FolioCargo in (" & listafolios & ") and codigoEmpresa= '1'"

                strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
                strsql += vbNewLine & " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad,"
                strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,CodigoRelacion,UPagoRelacion,IDDocRelacion,tipofactura,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
                strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
                strsql += vbNewLine & " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & dpaciente.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'1', '" & respuestaTimbrado.Timbre.UUID & "',"
                strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "',0,'" & _c.Total & "',0,2,'" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"

            Else
                strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[CargosManualesClientes] set Facturado='True', num_factura='" & _c.Folio & "', serie ='" & _c.Serie & "' " & _
                               " where FolioCargo in (" & listafolios & ") and codigoEmpresa= '1'"

                strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
                strsql += vbNewLine & " subtotal,ImporteIva,importe ,cantidad,direccion,ciudad,"
                strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,CodigoRelacion,UPagoRelacion,IDDocRelacion,tipofactura,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
                strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
                strsql += vbNewLine & " '" & _c.Impuestos.TotalImpuestosTrasladados & "','" & _c.Total & "',1,'conocido','conocido','" & tbReceptorNombre.Value & "','" & dpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'" & respuestaTimbrado.XMLResultado & "', '" & respuestaTimbrado.Timbre.UUID & "',"
                strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0'," & _c.FormaPago & ",'" & _c.MetodoPago & "','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "',0,'" & _c.Total & "',0,2,'" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "'  )"

            End If

            
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() <> 0 Then
                LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
            End If


          '''''' Actualiza el consecutivo 

            strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where idConsecutivoF=4"
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
            Return False
        End If
    End Function
    
    ' CONCEPTOS A MOSTRAR ANTES DE FACTURAR OTROS CARGOS AB02072020
    Function consultasAfacturarC()
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""

        Dim listaFolios As String
        Dim listaFoliosSQL As String
        count = 0
        For Each row As GridViewRow In DtgCargosManuales.Rows
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
            strSQL = "select sum(cantidad) as cantidad,descripcionConcepto,cast((subtotal) AS decimal(16,2)) as subtotal,observaciones FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] " & _
                    "where FolioCargo in (" & listaFac & ") AND codigoempresa = '1' AND status=1 " & _
                    "group by descripcionConcepto,subtotal,observaciones"


            Dim dt2 As DataTable
            If clsDatos.cargatabla(strSQL, dt2) = 0 Then
                If dt2.Rows.Count > 0 Then
                    gridDetallesC.DataSource = dt2
                    gridDetallesC.DataBind()
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


    Private Sub cargaDdMedico()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT codigoConsultorio,(nombre+' '+primerapellido+' '+segundoapellido) as medico  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
                  "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoEmpresa=C.codigoEmpresa and C.idDoctorCabecera=U.codigoUsuario " & _
                   "where C.codigoEmpresa=" & Session("codigoEmpresa") & " and C.status=1"
        If funcion.llenadropdown(strSQL, DdMedico) = 0 Then
            DdMedico.SelectedValue = 1
        Else
            Lblcritico.Text = "Error DDMedico 2345: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub

    Private Sub cargaDdConsultorioMedico()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT codigoConsultorio,(nombre+' '+primerapellido+' '+segundoapellido) as medico  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
                  "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoEmpresa=C.codigoEmpresa and C.idDoctorCabecera=U.codigoUsuario " & _
                   "where C.codigoEmpresa=" & Session("codigoEmpresa") & " and C.status=1"
        If funcion.llenadropdown(strSQL, DdConsultorioMedico) = 0 Then
            DdConsultorioMedico.SelectedValue = 1
        Else
            Lblcritico.Text = "Error DDConsultorioMedico 2346: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub
    'Private Sub cargaDdConceptoPago()
    '    Dim strSQL, strSQL2 As String
    '    Dim clsDatos As New ClaseDatos
    '    Dim funcion As New FuncionesGenerales
    '    Dim dt As New DataTable
    '    strSQL = "SELECT codigoConcepto,descripcion,ClaveProdServ,ClaveUnidad, Unidad, NoIdentificacion,tasaIVA,tasaISR  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
    '              "where status=1 and codigoEmpresa= '1'"
    '    If funcion.llenadropdown(strSQL, DdConceptoPago) = 0 Then
    '        DdConceptoPago.SelectedValue = 1
    '        strSQL2 = "SELECT codigoConcepto,descripcion,costo as importe,0.0 as comision,tasaIVA as iva,tasaISR as isr,ClaveProdServ,ClaveUnidad, Unidad,NoIdentificacion " & _
    '             "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] where status=1 and codigoConcepto=1 and codigoEmpresa= '1' "
    '        If clsDatos.cargatabla(strSQL2, dt) = 0 Then
    '            TxtImporte.Text = dt.Rows(0).Item("importe")
    '            TxtDescripcion.Text = dt.Rows(0).Item("descripcion")
    '            txtClaveProdServ.Value = dt.Rows(0).Item("ClaveProdServ")
    '            txtClaveUnidad.Value = dt.Rows(0).Item("ClaveUnidad")
    '            txtUnidad.Value = dt.Rows(0).Item("Unidad")
    '            txtNoIdentificacion.Value = dt.Rows(0).Item("NoIdentificacion")

    '            If dt.Rows(0).Item("iva") = 0.0 Then
    '                DdIVA.SelectedValue = 0.0
    '                TxtIVA.Text = 0.0

    '            Else
    '                DdIVA.SelectedValue = 0.16
    '                TxtIVA.Text = CDbl(TxtImporte.Text * 0.16)
    '            End If

    '            If dt.Rows(0).Item("isr") = 0.0 Then
    '                DdISR.SelectedValue = 0.0
    '                TxtISR.Text = 0.0

    '            Else
    '                DdISR.SelectedValue = 0.1
    '                TxtISR.Text = CDbl(TxtImporte.Text * 0.1)
    '            End If

    '        Else
    '            Lblcritico.Text = "Error DdConceptoPago 2342: " + clsDatos.MensajeError
    '            PanelCritico2.Visible = True
    '        End If
    '    Else
    '        Lblcritico.Text = "Error DdConceptoPago 2346: " + clsDatos.MensajeError
    '        PanelCritico2.Visible = True
    '    End If
    'End Sub
    'CARGAR LOS CONCEPTOS DE OTROS CARGOS- AB01072020
    'AB SAT 4.0
    Private Sub cargaDdConceptoPago()
        Dim strSQL, strSQL2 As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT codigoConcepto,descripcion,ClaveProdServ,ClaveUnidad, Unidad, NoIdentificacion,tasaIVA,tasaISR,Objetoimp  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
                  "where status=1"
        If funcion.llenadropdown(strSQL, DdConceptoPago) = 0 Then
            DdConceptoPago.SelectedValue = 1
            strSQL2 = "SELECT codigoConcepto,descripcion,costo as importe,0.0 as comision,tasaIVA as iva,tasaISR as isr,ClaveProdServ,ClaveUnidad, Unidad,NoIdentificacion, round((costo)/1.16,2) AS 'subtotal',(((costo)/1.16)*0.16) AS 'importeiva',Objetoimp  " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] where status=1 and codigoConcepto=1 "
            If clsDatos.cargatabla(strSQL2, dt) = 0 Then

                TxtDescripcion.Text = dt.Rows(0).Item("descripcion")
                txtClaveProdServ.Value = dt.Rows(0).Item("ClaveProdServ")
                txtClaveUnidad.Value = dt.Rows(0).Item("ClaveUnidad")
                txtUnidad.Value = dt.Rows(0).Item("Unidad")
                txtNoIdentificacion.Value = dt.Rows(0).Item("NoIdentificacion")
                tbObjeto.SelectedValue = dt.Rows(0).Item("Objetoimp")

                If dt.Rows(0).Item("iva") = 0.0 Then
                    TxtImporte.Text = CDbl(dt.Rows(0).Item("importe"))
                    DdIVA.SelectedValue = 0.0
                    TxtIVA.Text = 0.0

                Else
                    TxtImporte.Text = CDbl(dt.Rows(0).Item("subtotal"))
                    DdIVA.SelectedValue = 0.16
                    'TxtIVA.Text = CDbl(dt.Rows(0).Item("importeiva"))
                    TxtIVA.Text = CDbl(TxtImporte.Text * 0.16)
                End If

                If dt.Rows(0).Item("isr") = 0.0 Then
                    DdISR.SelectedValue = 0.0
                    TxtISR.Text = 0.0

                Else
                    DdISR.SelectedValue = 0.1
                    TxtISR.Text = CDbl(TxtImporte.Text * 0.1)
                End If

            Else
                Lblcritico.Text = "Error DdConceptoPago 2342: " + clsDatos.MensajeError
                PanelCritico2.Visible = True
            End If
        Else
            Lblcritico.Text = "Error DdConceptoPago 2346: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub
    'Private Sub DdConceptoPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdConceptoPago.SelectedIndexChanged
    '    Dim strSQL As String
    '    Dim clsDatos As New ClaseDatos
    '    Dim dt As New DataTable
    '    strSQL = "SELECT codigoConcepto,descripcion,costo as importe,0.0 as comision,tasaIVA as iva,tasaISR as isr,ClaveProdServ,ClaveUnidad, Unidad,NoIdentificacion " & _
    '             "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] where status=1 and codigoConcepto=" & DdConceptoPago.SelectedValue & " and codigoEmpresa= '1'"
    '    If clsDatos.cargatabla(strSQL, dt) = 0 Then
    '        TxtImporte.Text = dt.Rows(0).Item("importe")
    '        TxtDescripcion.Text = dt.Rows(0).Item("descripcion")
    '        txtClaveProdServ.Value = dt.Rows(0).Item("ClaveProdServ")
    '        txtClaveUnidad.Value = dt.Rows(0).Item("ClaveUnidad")
    '        txtUnidad.Value = dt.Rows(0).Item("Unidad")
    '        txtNoIdentificacion.Value = dt.Rows(0).Item("NoIdentificacion")

    '        If dt.Rows(0).Item("iva") = 0.0 Then
    '            DdIVA.SelectedValue = "0.0"
    '            TxtIVA.Text = 0.0

    '        Else
    '            DdIVA.SelectedValue = CDbl(0.16)
    '            TxtIVA.Text = CDbl(TxtImporte.Text * 0.16)
    '        End If

    '        If dt.Rows(0).Item("isr") = 0.0 Then
    '            DdISR.SelectedValue = "0.0"
    '            TxtISR.Text = 0.0

    '        Else
    '            DdISR.SelectedValue = CDbl(0.1)
    '            TxtISR.Text = CDbl(TxtImporte.Text * 0.1)
    '        End If

    '        ModalPopupExtender1.Show()
    '    Else
    '    End If
    'End Sub

    'AB01072020
    ' SELECCIONAR OTROS CARGOS - CAMBIOS AB01072020
    Private Sub DdConceptoPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdConceptoPago.SelectedIndexChanged
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "SELECT codigoConcepto,descripcion,costo as importe,0.0 as comision,tasaIVA as iva,tasaISR as isr,ClaveProdServ,ClaveUnidad, Unidad,NoIdentificacion, round((costo)/1.16,2) AS 'subtotal',(((costo)/1.16)*0.16) AS 'importeiva',ObjetoImp " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] where status=1 and codigoConcepto=" & DdConceptoPago.SelectedValue & ""
        If clsDatos.cargatabla(strSQL, dt) = 0 Then

            TxtDescripcion.Text = dt.Rows(0).Item("descripcion")
            txtClaveProdServ.Value = dt.Rows(0).Item("ClaveProdServ")
            txtClaveUnidad.Value = dt.Rows(0).Item("ClaveUnidad")
            txtUnidad.Value = dt.Rows(0).Item("Unidad")
            txtNoIdentificacion.Value = dt.Rows(0).Item("NoIdentificacion")
            tbObjeto.SelectedValue = dt.Rows(0).Item("Objetoimp")

            If dt.Rows(0).Item("iva") = 0.0 Then
                TxtImporte.Text = CDbl(dt.Rows(0).Item("importe"))
                DdIVA.SelectedValue = "0.0"
                TxtIVA.Text = 0.0

            Else
                TxtImporte.Text = CDbl(dt.Rows(0).Item("subtotal"))
                DdIVA.SelectedValue = CDbl(0.16)
                'TxtIVA.Text = CDbl(dt.Rows(0).Item("importeiva"))
                TxtIVA.Text = CDbl(TxtImporte.Text * 0.16)
            End If

            If dt.Rows(0).Item("isr") = 0.0 Then
                DdISR.SelectedValue = "0.0"
                TxtISR.Text = 0.0

            Else
                DdISR.SelectedValue = CDbl(0.1)
                TxtISR.Text = CDbl(TxtImporte.Text * 0.1)
            End If

            ModalPopupExtender1.Show()
        Else
        End If
    End Sub



    Private Sub DdTipoDePago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdTipoDePago.SelectedIndexChanged
        If DdTipoDePago.SelectedValue = 1 Or DdTipoDePago.SelectedValue = 98 Or DdTipoDePago.SelectedValue = 99 Then
            TxtReferencia.Visible = False
        Else
            TxtReferencia.Visible = True
        End If
        ModalPopupExtender1.Show()
    End Sub

    Private Sub DdMedico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdMedico.SelectedIndexChanged
        ModalPopupExtender1.Show()
    End Sub

    ''' PARA AGREGAR UN CM AB02072020
    Protected Sub BtnCargosManuales_Click()
        ocultarPaneles()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        If LstBoxPacientes.SelectedValue = "" Then
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] (serieFolio,codigoConcepto,subtotal,comision,importeIva,importeISR,codigoUsuario," & _
                 "codigoEmpresa,fecha,status,codigoCliente,Observaciones,codigoFormaPago,referencia,codigoConsultorio,facturado,conciliado,abono,descripcionConcepto,ClaveProdServ,ClaveUnidad,Unidad,NoIdentificacion,tasaIVA,Descuento,tasaISR,Objetoimp) " & _
                 "values ('CMC'," & DdConceptoPago.SelectedValue & "," & CDbl(TxtImporte.Text.Trim) & ",'0.0'," & CDbl(TxtIVA.Text.Trim) & "," & CDbl(TxtISR.Text.Trim) & ",1," & _
                 "1,getdate(),1," & LstBoxPacientes.SelectedValue & ",'" & TxtAnotaciones.Text.Trim & "',1,'" & TxtReferencia.Text.Trim & "',1,0,0,0,'" & TxtDescripcion.Text.Trim & "','" & txtClaveProdServ.Value & "','" & txtClaveUnidad.Value & "','" & txtUnidad.Value & "','" & txtNoIdentificacion.Value & "',CAST(" & DdIVA.SelectedValue & " AS varchar)+ REPLICATE(0,8-LEN(" & DdIVA.SelectedValue & ")),0,CAST(" & DdISR.SelectedValue & " AS varchar)+ REPLICATE(0,8-LEN(" & DdISR.SelectedValue & ")),'" & tbObjeto.SelectedValue & "')"

        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            cargaDtgCargosManuales()
        Else
            Lblcritico.Text = "Error DdTipoDePago 2348: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub

    ' SELECCIONAR SI SON TERAPIAS O OTRAS CARGOS A FACTURAR
    Private Sub DDTiposDeCargos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDTiposDeCargos.SelectedIndexChanged
        ocultarPaneles()
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        If DDTiposDeCargos.SelectedValue = 0 Then
            DtgCargosManuales.Visible = False
            dtgCargosConsultas.Visible = True
            gridDetallesC.DataBind()
            DtgCargosManuales.DataBind()
            cargaDTGCargosConsulta()
            'btn_facturarC.Visible = False
            'btn_facturar.Visible = True
        Else
            DtgCargosManuales.Visible = True
            dtgCargosConsultas.Visible = False
            gridDetalles.DataBind()
            dtgCargosConsultas.DataBind()
            cargaDtgCargosManuales()
            'btn_facturar.Visible = False
            'btn_facturarC.Visible = True
        End If


    End Sub

    Private Sub DdIVA_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdIVA.SelectedIndexChanged
        If DdIVA.SelectedValue = "0.16" Then
            TxtIVA.Text = CDbl(TxtImporte.Text.Trim) * 0.16
            tbObjeto.SelectedValue = "02"
        Else
            TxtIVA.Text = 0.0
            tbObjeto.SelectedValue = "01"
        End If
        ModalPopupExtender1.Show()
    End Sub
    '''CARGAR CM AB02072020
    Protected Sub cargaDtgCargosManuales()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "SELECT CMC.folioCargo,CMC.descripcionConcepto,CMC.subtotal,CMC.importeIva,CMC.importeIsr,CMC.fecha,CMC.observaciones  " & _
            "FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] as CMC " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[Clientes] as C on C.idcliente=CMC.codigocliente " & _
            " where CMC.codigoCliente = " & LstBoxPacientes.SelectedValue & " And facturado = 0 and CMC.status = 1 and CMC.codigoEmpresa= '1'" & _
            "order by fecha desc"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            DtgCargosManuales.DataSource = dt
            DtgCargosManuales.DataBind()
            btn_facturarC.Visible = True
            btn_facturar.Visible = False
        Else
            LblMensajeCritico.Text = " Error 9801: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
    End Sub

    Private Sub DdConsultorioMedico_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DdConsultorioMedico.SelectedIndexChanged
        ocultarPaneles()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        If DDTiposDeCargos.SelectedValue = 0 Then
            strSQL = "SELECT A.folioConsulta,(CP.papellido+' '+CP.sapellido+' '+CP.nombres) as CodigoPaciente,CC.descripcionConsultorio as Consultorio,fechaAgenda,I.abono  FROM AgAgenda as A " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as CP on A.codigoPaciente=CP.codigoPaciente " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as CC on CC.codigoConsultorio=A.codigoConsultorio " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as I on I.folioConsulta=A.folioConsulta " & _
                "where A.codigoCliente=" & LstBoxPacientes.SelectedValue & " and A.codigoEtapa=6 and I.facturado=0 and importepago > 0 and A.codigoConsultorio=" & DdConsultorioMedico.SelectedValue & " "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    dtgCargosConsultas.DataSource = dt
                    dtgCargosConsultas.DataBind()
                    btn_facturar.Visible = True
                    btn_facturarC.Visible = False
                Else
                    dtgCargosConsultas.DataSource = dt
                    dtgCargosConsultas.DataBind()
                    LblAviso.Text = " No se encontraron registros."
                    PanelAviso2.Visible = True
                End If
                dtgCargosConsultas.DataSource = dt
                dtgCargosConsultas.DataBind()
            Else
                Lblcritico.Text = " Error 9800: " + clsDatos.MensajeError
                PanelCritico2.Visible = True
            End If
        Else
            strSQL = "SELECT CMC.folioCargo,CMC.descripcionConcepto,CMC.subtotal,CMC.importeIva,CMC.fecha,CMC.observaciones,CO.descripcionConsultorio as consultorio,(nombre+' '+primerApellido) as Usuario  " & _
            "FROM [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] as CMC " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on CMC.codigoUsuario=U.CodigoUsuario " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] as C on C.codigoCliente=CMC.codigoCliente " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios]  as CO on CO.codigoConsultorio=CMC.codigoConsultorio " & _
            " where CMC.codigoCliente = " & LstBoxPacientes.SelectedValue & " And facturado = 0 and CMC.codigoConsultorio=" & DdConsultorioMedico.SelectedValue & "" & _
            "order by fecha desc"
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                DtgCargosManuales.DataSource = dt
                DtgCargosManuales.DataBind()
                btn_facturarC.Visible = True
                btn_facturar.Visible = False

            Else
                LblMensajeCritico.Text = " Error 9801: " + clsDatos.MensajeError
                PanelCritico2.Visible = True
            End If
        End If
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
End Class