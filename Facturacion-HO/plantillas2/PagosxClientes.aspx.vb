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


Public Class PagosxClientes
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Dim _c As Comprobante = New Comprobante()
    Private _comprobante As Comprobante

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'cargarmetodopago()
            cargaddTipoDePago()
            'cargaUCFDI()
            cargacuentaempresa()
            cargaRegimenFiscal()
            cargacExportacion()
        End If
        ocultarPaneles()
        btn_facturar.Visible = True
    End Sub

    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
        'BotonBuscar.InnerText = "Buscando.."
        Dim strSQL As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos

        If TxtRazonSocial.Text.Trim = "" And TxtRFC.Text.Trim = "" Then
            Exit Sub
        End If
        If TxtRazonSocial.Text.Trim = "" Then
            strSQL = "SELECT iddatosfac,nombre  FROM [" & claseDatos.BaseDatos & "].[dbo].[catFacturas] " & _
             "where rfc like '%" & TxtRFC.Text.Trim & "%'"
        Else
            strSQL = "SELECT iddatosfac,nombre  FROM [" & claseDatos.BaseDatos & "].[dbo].[catFacturas] " & _
             "where nombre like '%" & TxtRazonSocial.Text.Trim & "%'"
        End If
        resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
        If resultadoBusqueda = -1 Then
            LblMensajeCritico.Text = "Error 982: " + claseDatos.MensajeError
            PanelCritico.Visible = True
            Exit Sub
        End If
        If resultadoBusqueda = 1 Then
            LblMostarDecision.Text = " No existe el Cliente "
            PanelDesicion.Visible = True
            Exit Sub
        End If
        LstBoxPacientes.SelectedIndex = 0



        'If TxbMaterno.Text = "" And TxbPaterno.Text = "" And TxbNombre.Text = "" Then
        '    Exit Sub
        'End If
        'strSQL = "SELECT idcliente,paterno+' '+materno+' '+Nombre FROM [" & claseDatos.BaseDatos & "].[dbo].[clientes] " & _
        '    "where paterno like '%" & TxbPaterno.Text.Trim & "%' and nombre like '%" & TxbNombre.Text.Trim & "%' and materno like '%" & TxbMaterno.Text & "%'"
        'resultadoBusqueda = 0
        'resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
        'If resultadoBusqueda = -1 Then
        '    LblMensajeCritico.Text = claseDatos.MensajeError
        '    PanelCritico.Visible = True
        '    PanelCritico.Focus()
        '    Exit Sub
        'End If
        'If resultadoBusqueda = 1 Then
        '    LblMostarDecision.Text = "No existe el paciente!"
        '    PanelDesicion.Visible = True
        '    PanelDesicion.Focus()
        '    Exit Sub
        'End If
        'LstBoxPacientes.Focus()
        'LstBoxPacientes.SelectedIndex = 0
    End Sub

    Protected Sub BtnVer_Click()
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        cargaDTGCargosConsulta()
    End Sub

    Protected Sub facturar(sender As Object, e As EventArgs)
        Dim count As Integer = 0
        Dim listPagos As String = ""
        Dim pacientesv As String = ""
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
                LlenarComboemisor()
                CargarDatosemisor()
                iniciaComprobante()
                'consultasAfacturarG()
                'div_input_razon_social.Visible = False
                div_direccion.Visible = False
                div_cp.Visible = True
                div_ciudad.Visible = False
                div_estado.Visible = False
                div_pais.Visible = False
                div_UPfiscal.Visible = False
                div_emisor.Visible = False
                div_dfacturacion.Visible = False
                div_buscaremisor.Visible = True
                'pacientesvi.Value = pacientesv
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
        dcpaciente.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""
        codigo_paciente.Value = CodigoPaciente
        ' strsql = "SELECT codigoCliente,RazonSocial,rfc, email, direccion, cp, pais, estado, ciudad, NumCuenta, NomBanco,RFCBanco FROM [" & clsdatos.BaseDatos & "].[dbo].[CatClientes] WHERE CodigoCliente =" + CodigoPaciente + " ORDER BY CodigoCliente"
        'strsql = "SELECT idcliente,elnombre,razonsocial,rfc, email,cp, pais, edo, ciudad, NumCuenta, NomBanco,RFCBanco,codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + CodigoPaciente + " ORDER BY idCliente"
        strsql = "SELECT iddatosfac,nombre,rfc, email,cp, pais, Edo, ciudad, NumCuenta, NomBanco,RFCBanco,codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] WHERE iddatosfac =" + CodigoPaciente + " ORDER BY iddatosfac"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                dcpaciente.Value = dt.Rows(0).Item("nombre")
                'nompaciente.Value = dpaciente.Value
                tbReceptorNombre.Value = dt.Rows(0).Item("nombre")
                tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                correo.Value = dt.Rows(0).Item("email")
                'direccion.Value = dt.Rows(0).Item("direccion")
                cp.Value = dt.Rows(0).Item("cp")
                If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                    tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                Else
                    tbReceptorRegimenFiscal.SelectedValue = "999"
                End If
                ciudad.Value = dt.Rows(0).Item("ciudad")
                estado.Value = dt.Rows(0).Item("Edo")
                pais.Value = dt.Rows(0).Item("pais")

                If Not IsDBNull(dt.Rows(0).Item("NumCuenta")) Then
                    tbPagoCtaOte.Value = dt.Rows(0).Item("NumCuenta")

                Else
                    tbPagoCtaOte.Value = ""
                End If


                If Not IsDBNull(dt.Rows(0).Item("RFCBanco")) Then
                    tbPagoRfcEmisorCtaOte.Value = dt.Rows(0).Item("RFCBanco")
                Else

                    tbPagoRfcEmisorCtaOte.Value = ""
                End If

                If Not IsDBNull(dt.Rows(0).Item("NomBanco")) Then
                    tbPagoNombreBancoOrdenante.Value = dt.Rows(0).Item("NomBanco")
                Else
                    tbPagoNombreBancoOrdenante.Value = ""

                End If




                id_razon_social.Value = dt.Rows(0).Item("iddatosfac")
            End If
        End If
    End Sub
    Sub CargarDatosemisor()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbEmisorNombre.Value = ""
        tbEmisorRegimenFiscal.Value = ""
        tbEmisorRFC.Value = ""
        tbLugarExpedicion.Value = ""
        'tbFolio.Value = ""
        'tbSerie.Value = ""
        'correo.Value = ""
        'direccion.Value = ""
        'cp.Value = ""
        'ciudad.Value = ""
        'estado.Value = ""
        'pais.Value = ""
        'codigo_emisor.Value = 1
        strsql = "SELECT iddatosfacturacion,razonsocial,rfc,regimenfiscal,lugarexpedicion,CAST(CF.consecutivo + 1 AS varchar) as consecutivo, CF.serie FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] AS F " & _
                "inner join [" & clsdatos.BaseDatos & "].[dbo].[ConsecutivoFac] as CF on F.idConsecutivoF = CF.idConsecutivoF  " & _
                "WHERE iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' ORDER BY iddatosfacturacion"




        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                tbEmisorNombre.Value = dt.Rows(0).Item("razonsocial")
                tbEmisorRFC.Value = dt.Rows(0).Item("rfc")
                tbEmisorRegimenFiscal.Value = dt.Rows(0).Item("regimenfiscal")
                tbLugarExpedicion.Value = dt.Rows(0).Item("lugarexpedicion")
                'tbFolio.Value = dt.Rows(0).Item("consecutivo")
                'tbSerie.Value = dt.Rows(0).Item("serie")
                'direccion.Value = dt.Rows(0).Item("direccion")
                'cp.Value = dt.Rows(0).Item("cp")
                'ciudad.Value = dt.Rows(0).Item("ciudad")
                'estado.Value = dt.Rows(0).Item("estado")
                'pais.Value = dt.Rows(0).Item("pais")
                id_emisor.Value = dt.Rows(0).Item("iddatosfacturacion")
            End If
        End If



    End Sub
    Sub LlenarComboemisor()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        'codigo_emisor.Value = CodigoPaciente
        strSQL = "SELECT iddatosfacturacion,razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[Catfacturacion] ORDER BY iddatosfacturacion"
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

    Sub LlenarComboRazonSocial(CodigoPaciente)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        codigo_paciente.Value = CodigoPaciente
        'strSQL = "SELECT codigoCliente,RazonSocial FROM [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] WHERE codigoCliente =" + CodigoPaciente + " ORDER BY CodigoCliente"
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
        tbPagoCtaOte.Value = ""
        tbPagoRfcEmisorCtaOte.Value = ""
        tbPagoNombreBancoOrdenante.Value = ""
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
            'strsql = "SELECT codigoCliente,RazonSocial,rfc, email, direccion, cp, pais, estado, ciudad, NumCuenta, NomBanco,RFCBanco FROM [" & clsdatos.BaseDatos & "].[dbo].[CatClientes] WHERE CodigoCliente =" + select_razon_social.SelectedValue
            'strsql = "SELECT idcliente,razonsocial,rfc, email,cp, pais, edo, ciudad, NumCuenta, NomBanco,RFCBanco,codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[Clientes] WHERE idCliente =" + select_razon_social.SelectedValue
            strsql = "SELECT iddatosfac,nombre,rfc, email,cp, pais, Edo, ciudad, NumCuenta, NomBanco,RFCBanco,codigoRegFiscal FROM [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] WHERE iddatosfac =" + select_razon_social.SelectedValue

            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbReceptorNombre.Value = dt.Rows(0).Item("nombre")
                    tbReceptorRFC.Value = dt.Rows(0).Item("rfc")
                    correo.Value = dt.Rows(0).Item("email")
                    'direccion.Value = dt.Rows(0).Item("direccion")
                    cp.Value = dt.Rows(0).Item("cp")
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                    estado.Value = dt.Rows(0).Item("Edo")
                    pais.Value = dt.Rows(0).Item("pais")
                    If Not IsDBNull(dt.Rows(0).Item("codigoRegFiscal")) Then
                        tbReceptorRegimenFiscal.SelectedValue = dt.Rows(0).Item("codigoRegFiscal")
                    Else
                        tbReceptorRegimenFiscal.SelectedValue = "999"
                    End If

                    If Not IsDBNull(dt.Rows(0).Item("NumCuenta")) Then
                        tbPagoCtaOte.Value = dt.Rows(0).Item("NumCuenta")

                    Else
                        tbPagoCtaOte.Value = ""
                    End If


                    If Not IsDBNull(dt.Rows(0).Item("RFCBanco")) Then
                        tbPagoRfcEmisorCtaOte.Value = dt.Rows(0).Item("RFCBanco")
                    Else

                        tbPagoRfcEmisorCtaOte.Value = ""
                    End If

                    If Not IsDBNull(dt.Rows(0).Item("NomBanco")) Then
                        tbPagoNombreBancoOrdenante.Value = dt.Rows(0).Item("NomBanco")
                    Else
                        tbPagoNombreBancoOrdenante.Value = ""

                    End If

                    id_razon_social.Value = dt.Rows(0).Item("idcliente")
                End If
            End If
        End If
    End Sub
    Sub CambioSelectemisor()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbEmisorNombre.Value = ""
        tbEmisorRFC.Value = ""
        tbEmisorRegimenFiscal.Value = ""
        tbLugarExpedicion.Value = ""
        'tbFolio.Value = ""
        'tbSerie.Value = ""
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
             "inner join [" & clsdatos.BaseDatos & "].[dbo].[ConsecutivoFac] as CF on F.idConsecutivoF = CF.idConsecutivoF  " & _
             "WHERE iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "' ORDER BY iddatosfacturacion"

            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    tbEmisorNombre.Value = dt.Rows(0).Item("razonsocial")
                    tbEmisorRFC.Value = dt.Rows(0).Item("rfc")
                    tbEmisorRegimenFiscal.Value = dt.Rows(0).Item("regimenfiscal")
                    tbLugarExpedicion.Value = dt.Rows(0).Item("lugarexpedicion")
                    'tbFolio.Value = dt.Rows(0).Item("consecutivo")
                    'tbSerie.Value = dt.Rows(0).Item("serie")
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
    Sub CargarDatosRFCCB()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbPagoRfcEmisoCtaDestino.Value = ""



        strsql = "SELECT codigoCuentaBancaria, RFCcuentabanco FROM [" & clsdatos.BaseDatos & "].[dbo].[CatCuentasBancarias] " & _
             "WHERE codigoCuentaBancaria = '" & tbPagoCuentaBeneficiario.SelectedValue & "' and status=1 "

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                tbPagoRfcEmisoCtaDestino.Value = dt.Rows(0).Item("RFCcuentabanco")

            End If
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If


    End Sub
    Sub CambioSelectCuentaB()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        tbPagoRfcEmisoCtaDestino.Value = ""



        strsql = "SELECT codigoCuentaBancaria, RFCcuentabanco FROM [" & clsdatos.BaseDatos & "].[dbo].[CatCuentasBancarias] " & _
             "WHERE codigoCuentaBancaria = '" & tbPagoCuentaBeneficiario.SelectedValue & "' and status=1 "

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                tbPagoRfcEmisoCtaDestino.Value = dt.Rows(0).Item("RFCcuentabanco")
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If

        End If


    End Sub


    Sub FacturarConsultas()

        'aqui vamos a cntinuar mañana 22102025
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If bandera.Value = 1 Then
            'Dar de alta la razón social
            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] SET nombre='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', Edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', NumCuenta='" & tbPagoCtaOte.Value & "', NomBanco='" & tbPagoNombreBancoOrdenante.Value & "', RFCBanco='" & tbPagoRfcEmisorCtaOte.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE iddatosfac = " + id_razon_social.Value
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                strsql = "SELECT iddatosfac, nombre FROM [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] " & _
                          "WHERE nombre='" & tbReceptorNombre.Value & "' and rfc='" & tbReceptorRFC.Value & "'  and email='" & correo.Value & "' "
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    id_razon_social.Value = dt.Rows(0).Item("iddatosfac")
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
            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[catFacturas] SET nombre='" & tbReceptorNombre.Value & "', rfc='" & tbReceptorRFC.Value & "', email='" & correo.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', Edo='" & estado.Value & "', ciudad= '" & ciudad.Value & "', NumCuenta='" & tbPagoCtaOte.Value & "', NomBanco='" & tbPagoNombreBancoOrdenante.Value & "', RFCBanco='" & tbPagoRfcEmisorCtaOte.Value & "', codigoRegFiscal ='" & tbReceptorRegimenFiscal.SelectedValue & "' WHERE iddatosfac = " + id_razon_social.Value
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                Facturar33()
                cargaDTGCargosConsulta()
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
    End Sub
    Private Function getConcepto() As Concepto
        '************ -CONCEPTO- *******************
        Dim c As Concepto = New Concepto()
        c.ClaveProdServ = "84111506"
        c.ClaveUnidad = "ACT"
        c.ValorUnitario = 0
        c.Importe = 0
        c.Cantidad = 1
        c.Descripcion = "Pago"
        c.ObjetoImp = "01"
        Return c
    End Function
    Private Sub extraerCampos()
        Dim hoy As DateTime = DateTime.Now
        Dim cuenta As Int32 = 0
        Dim contador As Int32 = 0
        Dim cuentau As Int32 = 0
        Dim contadoru As Int32 = 0
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos

        _c.Conceptos.Add(getConcepto())
        _c.Version = tbVersion.Value
        _c.Serie = tbSerie.Value
        _c.Folio = tbFolio.Value
        _c.Fecha = dtpFecha.Value
        _c.Moneda = tbMoneda.Value
        _c.TipoDeComprobante = tbTipoComprobante.Value
        _c.LugarExpedicion = tbLugarExpedicion.Value
        _c.Emisor.Nombre = tbEmisorNombre.Value
        _c.Emisor.Rfc = tbEmisorRFC.Value
        _c.Emisor.RegimenFiscal = tbEmisorRegimenFiscal.Value
        _c.Receptor.Nombre = tbReceptorNombre.Value
        _c.Receptor.Rfc = tbReceptorRFC.Value
        _c.Receptor.UsoCFDI = "CP01"
        _c.Receptor.DomicilioFiscalReceptor = cp.Value
        _c.Receptor.RegimenFiscalReceptor = tbReceptorRegimenFiscal.SelectedValue
        _c.Exportacion = tbExportacion.SelectedValue
        _c.Complemento.Pagos.Version = "2.0"



        _c.Total = 0
        _c.Subtotal = 0
        _c.Descuento = 0

        '************** -DATOS DEL COMPROBANTE- ************

        Dim c As New Comprobante()

        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal As DataTable
        Dim listafolios, listaFoliosSQL As String
        Dim totalmonto As String
        Dim totalimpuesto As String
        Dim totalbase As String



        '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 

        If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then


            variasFacturas = True
        Else
            variasFacturas = False
        End If

        Dim strsql As String
        Dim ncuenta As String = ""
        strsql = "SELECT numeroCuenta FROM [" & clsdatos.BaseDatos & "].[dbo].[CatCuentasBancarias] " & _
                "where codigoCuentaBancaria='" & tbPagoCuentaBeneficiario.SelectedValue & "'  and status='1' order by codigoCuentaBancaria"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                ncuenta = dt.Rows(0).Item("numeroCuenta")
            Else
                LblMensajeCritico.Text = "Error en obetener el numero de cuenta"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If


        c.Complemento.Pagos.pagos = New List(Of Pago)()

        Dim pago As Pago = New Pago()
        pago.CadPago = ""
        pago.CertPago = ""


        pago.CtaBeneficiario = ncuenta
        pago.CtaOrdenante = tbPagoCtaOte.Value
        pago.FechaPago = dtpPagoFecha.Value
        pago.FormaDePagoP = tbFormaPago.SelectedValue
        pago.MonedaP = "MXN"
        'pago.Monto = nudPagoMonto.Value
        pago.NomBancoOrdExt = tbPagoNombreBancoOrdenante.Value
        pago.NumOperacion = tbPagoNoOperacion.Value
        pago.RfcEmisorCtaBen = tbPagoRfcEmisoCtaDestino.Value
        pago.RfcEmisorCtaOrd = tbPagoRfcEmisorCtaOte.Value
        pago.SelloPago = ""
        pago.TipoCadPago = ""
        pago.TipoCambioP = "1"

        totalmonto = dtTotal.Rows(0).Item("costoTotal")
        totalimpuesto = dtiTotal.Rows(0).Item("ivatotal")
        totalbase = dtsTotal.Rows(0).Item("STotal")

        pago.Monto = totalmonto
        pago.Montost = totalbase
        pago.Montoiv = totalimpuesto


        _c.Complemento.Pagos.pagos.Add(pago)

        Do While contador = cuenta

            If variasFacturas = True Then

                For Each filad As DataRow In dtConceptos.Rows
                    Dim docrelacionado As New DoctoRelacionado()
                    docrelacionado.Folio = filad.Item("num_factura")
                    docrelacionado.IdDocumento = filad.Item("UUID")
                    docrelacionado.ImpPagado = nudPagoMonto.Value
                    docrelacionado.ImpSaldoAnt = filad.Item("costo")
                    docrelacionado.ImpSaldoInsoluto = filad.Item("costo") - nudPagoMonto.Value
                    docrelacionado.MetodoDePagoDR = filad.Item("CodigoMetodoPago")
                    docrelacionado.MonedaDR = "MXN"
                    docrelacionado.NumParcialidad = 1
                    docrelacionado.Serie = filad.Item("serie")


                    'docrelacionado.TipoCambioDR = 1
                    pago.DoctoRelacionado.Add(docrelacionado)

                    Dim tc As New Traslado()
                    tc.Importe = filad.Item("ivatotal")
                    If filad.Item("ivatotal") = "0.0000" Then
                        tc.TasaOCuota = "0.000000"
                    Else
                        tc.TasaOCuota = "0.160000"
                    End If

                    'tc.TasaOCuota = fila.Item("tasaIVA")
                    tc.TipoFactor = "Tasa"
                    tc.Impuesto = "002"
                    tc.Base = filad.Item("STotal")

                    pago.Impuestos.Traslados.Add(tc)
                Next

            Else

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
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim folio As String = ""
        Dim serie As String = ""


        strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from consecutivoFac where idConsecutivoF=3"

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
        dtpPagoFecha.Value = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("00:00:00"))
        tbMoneda.Value = "XXX"
        tbTipoComprobante.Value = "P"

        Dim totalm As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim dtsTotal, dtiTotal As DataTable
        Dim listafolios, listaFoliosSQL As String

        If consultasAfacturar(dtConceptos, dtTotal, dtsTotal, dtiTotal, listafolios, listaFoliosSQL) Then
            totalm = dtTotal.Rows(0).Item("costoTotal")
            nudPagoMonto.Value = totalm
            variasFacturas = True
        Else
            variasFacturas = False
        End If

        tbFormaPago.SelectedValue = "03"

    End Sub
    Private Sub cargacuentaempresa()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "SELECT codigoCuentaBancaria,numeroCuenta,RFCcuentabanco  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatCuentasBancarias] " & _
                "where status='1' order by codigoCuentaBancaria"
        If funcion.llenadropdown(strSQL, tbPagoCuentaBeneficiario) = 0 Then
            tbPagoCuentaBeneficiario.SelectedValue = 1
            CargarDatosRFCCB()
        Else

            LblMensajeCritico.Text = "Error UsoCFDi 2349: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Private Sub tbFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tbFormaPago.SelectedIndexChanged

        If tbFormaPago.SelectedValue = 1 Then

            tbPagoCuentaBeneficiario.SelectedValue = 2
            tbPagoRfcEmisoCtaDestino.Value = ""
        Else
            tbPagoCuentaBeneficiario.SelectedValue = 1
            CargarDatosRFCCB()

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
        Else

            LblMensajeCritico.Text = "Error FormasDePago 2349: " + clsDatos.MensajeError
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

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dpfx As String = ""
        Dim dcer As String = ""
        Dim passcer As String = ""

        strsql = " SELECT directoriopfx, directoriocer, passwordcer FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                dpfx = dt.Rows(0).Item("directoriopfx")
                dcer = dt.Rows(0).Item("directoriocer")
                passcer = dt.Rows(0).Item("passwordcer")
            Else
                LblMensajeCritico.Text = "Error en los certificados!"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If


        Dim apfx As New String(Server.MapPath(dpfx))
        Dim acer As New String(Server.MapPath(dcer))


        CrearXML.Create(_c, rutaXMLST, apfx, passcer, acer)

        'Hace el timbrado con Folios digitales
        Dim imagen As Image = Image.FromFile(Server.MapPath("/LOGO/logo.jpg"))
        Dim dpaciente As String = dcpaciente.Value
        Dim dobservaciones As String = dcobservaciones.Value
        If Timbrar(rutaXMLST, rutaXMLT, tbUsuarioFoliosDigitales.Value, tbPasswordFoliosDigitales.Value) Then
            Dim CreaPDF = New CreaPDF(rutaXMLT, rutaPDF, imagen, dpaciente, dobservaciones)

            Dim folioFactura As String = _c.Serie + _c.Folio
            Dim Path1 As String
            Dim Path2 As String
            Dim mail As New MailMessage
            Path1 = Server.MapPath("/Facturas/XMLT/" + folioFactura)
            Path2 = Server.MapPath("/Facturas/PDFS/" + folioFactura)


            mail.From = New MailAddress("facturacion@fisiocare.com.mx")

            mail.To.Add(correo.Value)

            mail.Subject = "Complemento de Pago de servicios de Fisiocare"
            mail.Body = "Pago"
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


        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim upac As String = ""
        Dim ppac As String = ""
        Dim codigoc As String = ""
        Dim iddfacturacion As String = ""

        strsql = " SELECT iddatosfacturacion,usuariopac, passwordpac, idConsecutivoF FROM [" & clsdatos.BaseDatos & "].[dbo].[Catfacturacion] WHERE  iddatosfacturacion = '" & selectEmisorNombre.SelectedValue & "'"

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


        '//Obteniendo la respuesta se valida que haya sido exitosa.

        If respuestaTimbrado.OperacionExitosa Then

            'Guardo el XML timbrado.
            DocumentoXML.LoadXml(respuestaTimbrado.XMLResultado)
            DocumentoXML.Save(rutaXML)

            Dim variasFacturas As Boolean
            Dim dtConceptos, dtTotal As DataTable
            Dim dtsTotal, dtiTotal As DataTable
            Dim listafolios, listaFoliosSQL As String
            'Dim strsql As String
            'Dim clsdatos As New ClaseDatos

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
            strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[ConsecutivoFac] set consecutivo = consecutivo + 1 " & _
                               " where idConsecutivoF in (3)  "
            strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[factura] (num_factura,serie,rfc,"
            strsql += vbNewLine & " subtotal,ImporteIva,importe,cantidad,direccion,ciudad,"
            strsql += vbNewLine & " nombre,paciente,idcliente,fecha,xml,uuid,noCertificadoSAT,selloCfD,selloSAT, FechaTimbrado,estado,idcosto,tipoDePago,CodigoMetodoPago,CodigoUCFDI,version,ComplementoPago,IDDocRelacion,codigoRegFiscal,DomicilioFiscalReceptor) VALUES  "
            strsql += vbNewLine & " ('" & _c.Folio & "','" & _c.Serie & "','" & _c.Receptor.Rfc & "','" & _c.Subtotal & "',"
            strsql += vbNewLine & "'0','" & _c.Total & "',1,'conocido','conocido','" & tbReceptorNombre.Value & "','" & dcpaciente.Value & "','" & id_razon_social.Value & "', getdate() ,'" & respuestaTimbrado.XMLResultado & "', '" & respuestaTimbrado.Timbre.UUID & "',"
            strsql += vbNewLine & " '" & respuestaTimbrado.Timbre.NumeroCertificadoSAT & "','" & respuestaTimbrado.Timbre.SelloCFD & "','" & respuestaTimbrado.Timbre.SelloSAT & "','" & respuestaTimbrado.Timbre.FechaTimbrado & "','" & respuestaTimbrado.Timbre.Estado & "','0','0','0','" & _c.Receptor.UsoCFDI & "','" & _c.Version & "','0','" & foliosConsulta & "','" & _c.Receptor.RegimenFiscalReceptor & "','" & _c.Receptor.DomicilioFiscalReceptor & "' )"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() <> 0 Then
                LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                PanelAdvertencia.Visible = True
            End If


            Dim strsqlUF As String

            strsqlUF = "update factura set ComplementoPago = 1 where idFactura in (" & foliosConsulta & ")  "
            clsdatos.cargaComando(strsqlUF)
            clsdatos.ejecutar()

            Dim strsqlU As String

            Dim IDUFactura As String = ""
            strsqlU = "select top 1 idFactura from [" & clsdatos.BaseDatos & "].[dbo].[factura] " & _
                             " order by idFactura desc"
            If clsdatos.cargatabla(strsqlU, dt) = 0 Then

                If dt.Rows.Count > 0 Then
                    IDUFactura = dt.Rows(0).Item("idFactura")

                Else
                    LblMensajeAdvertencia.Text = "Error 1278: " + clsdatos.MensajeError
                    PanelAdvertencia.Visible = True
                    PanelAdvertencia.Focus()

                End If
            End If

            Dim strsqlCC As String
            Dim ncuentab As String = ""
            strsqlCC = "SELECT numeroCuenta FROM [" & clsdatos.BaseDatos & "].[dbo].[CatCuentasBancarias] " & _
                    "where codigoCuentaBancaria='" & tbPagoCuentaBeneficiario.SelectedValue & "'  and status='1' order by codigoCuentaBancaria"

            If clsdatos.cargatabla(strsqlCC, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    ncuentab = dt.Rows(0).Item("numeroCuenta")
                Else
                    LblMensajeCritico.Text = "Error en obetener el numero de cuenta"
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
            End If

            Dim strsqlA As String
            strsqlA = "insert into [" & clsdatos.BaseDatos & "].[dbo].[DetPagos] (IDFactura, FechaPago, FormaDePagoP, MonedaP, Monto, NumOperacion, RfcEmisorCtaOrd, NomBancoOrdExt, CtaOrdenante, RfcEmisorCtaBen, CtaBeneficiario, Estado, Fecha) " & _
         "values('" & IDUFactura & "','" & dtpFecha.Value & "','" & tbFormaPago.SelectedValue & "','MXN','" & nudPagoMonto.Value & "','" & tbPagoNoOperacion.Value & "','" & tbPagoRfcEmisorCtaOte.Value & "','" & tbPagoNombreBancoOrdenante.Value & "','" & tbPagoCtaOte.Value & "','" & tbPagoRfcEmisoCtaDestino.Value & "','" & ncuentab & "','" & respuestaTimbrado.Timbre.Estado & "',getdate()) "


            clsdatos.cargaComando(strsqlA)
            If clsdatos.ejecutar = 0 Then

            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
            End If

            '' '''''' Actualiza el consecutivo para la gestion multiempresas
            'strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where CodigoCF = '" & codigoc & "' "
            'clsdatos.cargaComando(strsql)
            'clsdatos.ejecutar()


            Return True
        Else
            Dim DetalleError As String
            'Si la petición fue erronea muestro el error.
            DetalleError = respuestaTimbrado.CodigoRespuesta
            DetalleError += respuestaTimbrado.MensajeError
            DetalleError += respuestaTimbrado.MensajeErrorDetallado
            LblMensajeCritico.Text = "Error en el timbrado favor de verificar" + DetalleError
            PanelCritico.Visible = True
            PanelCritico.Focus()
            Return False
        End If
    End Function

    Sub enviarFacr(ByRef folioFac As String)
        Dim folioFactura As String = folioFac
        Dim Path As String
        Dim mail As New MailMessage
        Path = Server.MapPath("/Facturas/" + folioFactura)


        mail.From = New MailAddress("facturacion.alainsanchez@agemed.com.mx")

        mail.To.Add(correo.Value)

        mail.Subject = "Factura de servicios de Dr. Alain Sanchez Vazquez"
        mail.Body = "Factura De Servicio de su consulta Medica"
        Try
            Dim FilePath1 As String = Path + ".xml"
            Dim FilePath2 As String = Path + ".pdf"

            'el archivo se adjunta indicándole la ruta 
            mail.Attachments.Add(New Attachment(FilePath1))
            mail.Attachments.Add(New Attachment(FilePath2))

            Dim mailClient As New SmtpClient()

            Dim basicAuthenticationInfo As New NetworkCredential("facturacion.alainsanchez@agemed.com.mx", "Facas2018*")

            mailClient.Host = "mail.agemed.com.mx"

            mailClient.UseDefaultCredentials = True
            mailClient.Credentials = basicAuthenticationInfo
            mailClient.Port = 26

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
    Protected Sub cargaDTGCargosConsulta()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim dt2 As New DataTable

        strSQL = "SELECT F.idFactura,F.Serie,F.num_factura,F.importe,F.rfc,F.nombre,F.fecha,F.UUID,F.CodigoMetodoPago,F.estado " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] as F  " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[catFacturas] as P on F.idcliente=P.iddatosfac " & _
                    "where P.iddatosfac=" & LstBoxPacientes.SelectedValue & " and (version='3.3' or version='4.0') and F.Estado='Vigente' AND F.CodigoMetodoPago='PPD' AND ComplementoPago=0 ORDER BY F.IdFactura desc "


        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            dtgCargosConsultas.DataSource = dt
            dtgCargosConsultas.DataBind()
            btn_facturar.Visible = True
        Else
            LblMensajeCritico.Text = " Error 9800: " + clsDatos.MensajeError
            PanelCritico2.Visible = True
        End If
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
            strSQL = "select  serie,num_factura,UUID,cast((Importe) AS decimal(16,2)) as costo,CodigoMetodoPago,CAST(SUM(Subtotal) AS decimal(16,2)) AS STotal,CAST(SUM(importeIVA) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                     "where IdFactura in (" & listaFac & ") " & _
                     "group by serie,num_factura,UUID,importe,CodigoMetodoPago"
            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importe) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                    "where IdFactura in (" & listaFac & ") "
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(Subtotal) AS decimal(16,2)) AS STotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                    "where IdFactura in (" & listaFac & ") "
            If clsDatos.cargatabla(strSQL, dt3) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(importeIVA) AS decimal(16,2))  AS ivatotal FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] " & _
                     "where IdFactura in (" & listaFac & ") "
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
            strSQL = " select serie,FolioFactura,cast((Total) AS decimal(16,2)) as costo,UUID " & _
                       "FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] " & _
                       "where idFactura in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND Estado='Vigente' group by serie,FolioFactura,Total,UUID"

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
End Class