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


Public Class facturaspagos
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        limpiarPaneles()
        If Not IsPostBack Then
            cargaFacturas()
        End If
    End Sub

    Sub cargaFacturas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT F.IDFactura,F.Serie,F.num_factura,(F.Serie+F.num_factura) AS Factura,F.rfc,F.importe,F.nombre, " & _
                "F.paciente,F.fecha,F.estado,f.idCliente,f.idcosto " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] as F  " & _
                "where serie in ('PH') and (version='3.3' or version='4.0') " & _
                "ORDER BY F.IDFactura desc "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            GdFacturas.DataSource = dt
            GdFacturas.DataBind()
            GdFacturas.Columns(0).Visible = True
            GdFacturas.Columns(1).Visible = True
            GdFacturas.Columns(2).Visible = True
            GdFacturas.Columns(14).Visible = True
            GdFacturas.Columns(15).Visible = True
            'GdFacturas.Columns(16).Visible = True
            If dt.Rows.Count > 0 Then
                For Each fila As DataRow In dt.Rows
                    For Each factura As GridViewRow In GdFacturas.Rows
                        If fila.Item("num_factura") = factura.Cells(2).Text Then
                            If fila.Item("estado") = "Vigente" Then
                                CType(factura.FindControl("Cancelar"), LinkButton).Visible = True
                                CType(factura.FindControl("Verificar"), LinkButton).Visible = False
                            Else
                                CType(factura.FindControl("Cancelar"), LinkButton).Visible = False
                                ''AB09022022
                                CType(factura.FindControl("Verificar"), LinkButton).Visible = False
                                CType(factura.FindControl("enviar"), LinkButton).Visible = True
                                CType(factura.FindControl("pdf"), LinkButton).Enabled = False
                                CType(factura.FindControl("xml"), LinkButton).Visible = True



                                'If fila.Item("Facturado") = "1" Then


                                '    CType(factura.FindControl("Refacturar"), LinkButton).Visible = False

                                'Else
                                '    CType(factura.FindControl("Refacturar"), LinkButton).Enabled = True
                                'End If




                            End If
                        End If
                    Next
                Next
            End If
            GdFacturas.Columns(0).Visible = False
            GdFacturas.Columns(1).Visible = False
            GdFacturas.Columns(2).Visible = False
            GdFacturas.Columns(14).Visible = False
            GdFacturas.Columns(15).Visible = False
            'GdFacturas.Columns(16).Visible = False
        End If
    End Sub

    Protected Sub cargaFacturas(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL, estadoFac As String
        Dim rangoFechas
        GdFacturas.Columns(0).Visible = True
        GdFacturas.Columns(1).Visible = True
        GdFacturas.Columns(2).Visible = True
        rangoFechas = Split(Fechas.Text, " - ")
        If tipo_factura.Value = 1 Then
            strSQL = "SELECT F.IDFactura,F.Serie,F.FolioFactura,(F.Serie+F.FolioFactura) AS Factura,F.rfc,F.Total,P.RazonSocial, " & _
                "P.alias,F.fechaFactura,F.estado,(segundoApellido+' '+primerApellido+' '+nombre) as Facturo,codigoCliente,F.FolioConsulta AS FolioConsulta " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F  " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] as P on F.idRazonSocial=P.codigocliente and P.codigoEmpresa=1  " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U  on U.codigoUsuario=F.IdEmpleado " & _
                                 "where serie in ('PH')  and F.fechaFactura>='" & rangoFechas(0) & "' and F.fechaFactura<='" & rangoFechas(1) & "' " & _
                                 "ORDER BY F.fechaFactura desc"
        Else
            If tipo_factura.Value = 2 Then
                estadoFac = "Vigente"
            Else
                estadoFac = "Cancelada"
            End If
            strSQL = "SELECT F.IDFactura,F.Serie,F.FolioFactura,(F.Serie+F.FolioFactura) AS Factura,F.rfc,F.Total,P.RazonSocial, " & _
                "P.alias,F.fechaFactura,F.estado,(segundoApellido+' '+primerApellido+' '+nombre) as Facturo,codigoCliente,F.FolioConsulta AS FolioConsulta " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F  " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] as P on F.idRazonSocial=P.codigocliente and P.codigoEmpresa=1  " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U  on U.codigoUsuario=F.IdEmpleado " & _
                                "where serie in ('PH') and  F.fechaFactura>='" & rangoFechas(0) & "' and F.fechaFactura<='" & rangoFechas(1) & "' and F.estado='" & estadoFac & "' " & _
                                "ORDER BY F.fechaFactura desc"
        End If
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            GdFacturas.DataSource = dt
            GdFacturas.DataBind()
            If dt.Rows.Count > 0 Then
                For Each fila As DataRow In dt.Rows
                    For Each factura As GridViewRow In GdFacturas.Rows
                        If fila.Item("folioFactura") = factura.Cells(2).Text Then
                            If fila.Item("estado") = "Vigente" Then
                                CType(factura.FindControl("Cancelar"), LinkButton).Visible = True
                                CType(factura.FindControl("ReFacturar"), LinkButton).Visible = False
                            Else
                                CType(factura.FindControl("Cancelar"), LinkButton).Visible = False
                                CType(factura.FindControl("Refacturar"), LinkButton).Visible = True
                                CType(factura.FindControl("enviar"), LinkButton).Visible = True
                                CType(factura.FindControl("pdf"), LinkButton).Visible = False
                                CType(factura.FindControl("xml"), LinkButton).Visible = True
                            End If


                        End If
                    Next
                Next
            End If

        End If
        GdFacturas.Columns(0).Visible = False
        GdFacturas.Columns(1).Visible = False
        GdFacturas.Columns(2).Visible = False
    End Sub


    Protected Sub GdFacturas_SelectedIndexChanged(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles GdFacturas.RowCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        If e.CommandArgument = "" Then
            cargaFacturas()
        Else
            GdFacturas.Columns(0).Visible = True
            GdFacturas.Columns(1).Visible = True
            GdFacturas.Columns(2).Visible = True
            GdFacturas.Columns(14).Visible = True
            GdFacturas.Columns(15).Visible = True
            'GdFacturas.Columns(16).Visible = True
            Dim fila As Integer = e.CommandArgument
            Dim codgfacturas As Integer = GdFacturas.Rows.Item(fila).Cells(0).Text()
            Dim FolioFac As String = GdFacturas.Rows.Item(fila).Cells(2).Text
            Dim serie As String = GdFacturas.Rows.Item(fila).Cells(1).Text
            Dim Nombre_paciente As String = GdFacturas.Rows.Item(fila).Cells(6).Text
            Dim codigo_paciente_aux As String = GdFacturas.Rows.Item(fila).Cells(14).Text
            Dim folioConsulta As String = GdFacturas.Rows.Item(fila).Cells(15).Text
            HD_IdFactura.Value = GdFacturas.Rows.Item(fila).Cells(0).Text
            HdTotal.Value = GdFacturas.Rows.Item(fila).Cells(5).Text
            Dim estadoFac As String
            Dim tipoArchivo As String = e.CommandName




            If CType(GdFacturas.Rows.Item(fila).FindControl("Cancelar"), LinkButton).Visible Then
                estadoFac = "Vigente"
            Else
                estadoFac = "Cancelada"
            End If
            Select Case e.CommandName
                Case "cancelar"
                    'Cambios AB09022022'
                    HdIndex.Value = codgfacturas
                    HFolioF.Value = FolioFac
                    cargaDdMotivo_Cancelacion()
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#cancelacion').modal('show');</script>", False)
                    'CancelarFactura()
                Case "Verificar"
                    HdIndex.Value = codgfacturas
                    HFolioF.Value = FolioFac
                    VerificarCancelacion()

                Case "enviar"
                    serie_factura.Value = serie
                    folio_factura.Value = FolioFac
                    estado_factura.Value = estadoFac
                   strsql = "select factura.IdFactura,factura.idcliente,Clientes.email as email from Factura" & _
                     " full join Clientes on Factura.idcliente=Clientes.idCliente " & _
                     "where Factura.IDFactura=  '" & GdFacturas.Rows.Item(fila).Cells(0).Text() & "'"

                    If clsdatos.cargatabla(strsql, dt) = 0 Then
                        If dt.Rows.Count > 0 Then
                            correo_confirmar.Value = dt.Rows(0).Item("email")
                        Else
                            LblMensajeCritico.Text = "No se puede obtener en mail"
                            PanelCritico.Visible = True
                            PanelCritico.Focus()

                        End If
                    End If
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#correo_facturacion').modal('show');</script>", False)

                    'enviarFac(FolioFac, estadoFac)
                Case "xml"
                    descargarArchivoxml(FolioFac, serie, tipoArchivo, estadoFac)
                Case "pdf"
                    descargarArchivopdf(FolioFac, serie, tipoArchivo, estadoFac)
            End Select
        End If
        GdFacturas.Columns(0).Visible = False
        GdFacturas.Columns(1).Visible = False
        GdFacturas.Columns(2).Visible = False
        GdFacturas.Columns(14).Visible = False
        GdFacturas.Columns(15).Visible = False
        'GdFacturas.Columns(16).Visible = False
    End Sub
    'Nuevo procedimiento AB09022022'
    Private Sub cargaDdMotivo_Cancelacion()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        strSQL = "EXEC FISIOCARE_cargaMotivoCancelacion"

        If funcion.llenadropdown(strSQL, DdMotivos) = 0 Then
            DdMotivos.SelectedValue = "02"
        Else
            LblMensajeCritico.Text = "Error FormasDePago 202201: " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub BtnSiGFac_Click(sender As Object, e As EventArgs)
        Select Case Hdgfacturas.Value
            Case "0"

                CancelarFactura()
                cargaFacturas()
        End Select
    End Sub
    ''AB09022022
    Sub CancelarFacturaMotivo()
        CancelarFactura()
    End Sub
    ''AB09022022
    Private Sub CancelarFactura()
        Dim strsql As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable

        Dim factura As String
        Dim foliofcancelar As String
        factura = HdIndex.Value
        foliofcancelar = HFolioF.Value
        Dim ValidaCodigo() = {""}
        Dim Valida() = {""}
        Dim detallecancela As New DetalleCancelacion
        limpiarPaneles()
        strsql = "select uuid, IDDocRelacion,rfc,subtotal from factura where IDFactura='" & factura & "'"

        If clsDatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then




                Dim foliocc As String = dt.Rows(0).Item("IDDocRelacion")



                Dim uuidCancelar() As String = {dt.Rows(0).Item("uuid")}
                Dim uuid As String = dt.Rows(0).Item("uuid")
                Dim rfc As String = dt.Rows(0).Item("rfc")
                Dim Total As String = dt.Rows(0).Item("subtotal")
                Dim ClaveMotivo As String = DdMotivos.SelectedValue



                strsql = "select iddatosfacturacion,rfc,passwordcer,usuariopac,passwordpac, pemcancelacion from CatFacturacion where iddatosfacturacion='1'"

                If clsDatos.cargatabla(strsql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then


                        Dim urfc As String = dt.Rows(0).Item("rfc")

                        Dim upac As String = dt.Rows(0).Item("usuariopac")

                        Dim ppac As String = dt.Rows(0).Item("passwordpac")

                        Dim pcer As String = dt.Rows(0).Item("passwordcer")

                        Dim pemc As String = dt.Rows(0).Item("pemcancelacion")


                        Dim timbrarFac As New WSCFDI33.WSCFDI33Client
                        Dim cancelacionFac As New RespuestaCancelacion


                        Dim listaArreglos As List(Of WSCFDI33.DetalleCFDICancelacion) = New List(Of WSCFDI33.DetalleCFDICancelacion)()

                        Dim Arreglo As WSCFDI33.DetalleCFDICancelacion = New WSCFDI33.DetalleCFDICancelacion

                        Dim RespuestaCancelacionDetallada_FEL As New List(Of AgeMED.WSCFDI33.DetalleCancelacion)

                        Arreglo.RFCReceptor = rfc
                        Arreglo.Total = Total
                        Arreglo.UUID = uuid
                        Arreglo.Motivo = ClaveMotivo
                        listaArreglos.Add(Arreglo)

                        cancelacionFac = timbrarFac.CancelarCFDI(upac, ppac, urfc, listaArreglos.ToArray, pemc, pcer)


                        'For Each UUIDT As WSCFDI33.DetalleCancelacion In cancelacionFac.DetallesCancelacion

                        '    LblMensajeAdvertencia.Text += UUIDT.CodigoResultado + vbNewLine
                        '    LblMensajeAdvertencia.Text += UUIDT.MensajeResultado + vbNewLine
                        '    LblMensajeAdvertencia.Text += UUIDT.UUID + vbNewLine
                        '    LblMensajeAdvertencia.Text += "Favor de Verificar!"
                        '    PanelAdvertencia.Visible = True
                        '    PanelAdvertencia.Focus()
                        'Next

                        If cancelacionFac.OperacionExitosa = True Then

                            RespuestaCancelacionDetallada_FEL = cancelacionFac.DetallesCancelacion.ToList()

                            Select Case cancelacionFac.DetallesCancelacion(0).CodigoResultado

                                Case "201"


                                    '''''' Actualiza el estado de facturas clientes consultas
                                    Dim folioString As String = foliocc
                                    Dim foliosArray() As String = Split(folioString, ",")
                                    Dim LastNonEmpty As Integer = -1
                                    For i As Integer = 0 To foliosArray.Length - 1
                                        If i <> foliosArray.Count Then
                                            LastNonEmpty += 1
                                            foliosArray(LastNonEmpty) = foliosArray(i)
                                            strsql = "update factura set ComplementoPago = '0' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                                            clsDatos.cargaComando(strsql)
                                            If clsDatos.ejecutar() = 0 Then
                                            Else
                                                LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                                PanelAdvertencia.Visible = True
                                            End If
                                        End If
                                    Next
                                    ReDim Preserve foliosArray(LastNonEmpty)
                                    '''''' Guarda la Cancelacion
                                    strsql = "update factura set estado = 'Cancelada' where Idfactura='" & factura & "' "
                                    strsql += "update DetPagos set estado = 'Cancelada',clave_motivo_cancelacion='" & ClaveMotivo & "' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                                    clsDatos.cargaComando(strsql)
                                    If clsDatos.ejecutar() = 0 Then
                                    Else
                                        LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                        PanelAdvertencia.Visible = True
                                    End If

                                    Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                                    acusexml = timbrarFac.ObtenerAcuseCancelacion(upac, ppac, uuidCancelar(0).ToString)
                                    Dim xml
                                    xml = acusexml.XMLResultado
                                    If acusexml.CodigoRespuesta = "800" Then
                                        Dim nameFile As String = ""
                                        Dim doc As XmlDocument = New XmlDocument()
                                        doc.LoadXml(xml)
                                        nameFile = (Server.MapPath("/Facturas/Canceladas/" & foliofcancelar & ".xml"))
                                        'nameFile = "c:\reporteFisio\canceladasOrto\CanceladoOrto_" + txtnumcancelar.Text.ToString.Trim + "_201.xml"
                                        doc.Save(nameFile)
                                        cargaFacturas()
                                        LblMensajeAviso.Text = "La factura se ha cancelado"
                                        PanelAvisos.Visible = True
                                        PanelAvisos.Focus()

                                    Else
                                        LblMensajeCritico.Text = "Factura se canceló pero no se pudo obtener el Xml :" & acusexml.MensajeError
                                        PanelCritico.Visible = True
                                        PanelCritico.Focus()
                                        Exit Sub
                                    End If
                                Case Else
                            End Select

                        Else
                            '''''' Actualiza el estado de facturas clientes consultas
                            Dim folioString As String = foliocc
                            Dim foliosArray() As String = Split(folioString, ",")
                            Dim LastNonEmpty As Integer = -1
                            For i As Integer = 0 To foliosArray.Length - 1
                                If i <> foliosArray.Count Then
                                    LastNonEmpty += 1
                                    foliosArray(LastNonEmpty) = foliosArray(i)
                                    strsql = "update factura set ComplementoPago = '0' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                                    clsDatos.cargaComando(strsql)
                                    If clsDatos.ejecutar() = 0 Then
                                    Else
                                        LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                        PanelAdvertencia.Visible = True
                                    End If
                                End If
                            Next
                            ReDim Preserve foliosArray(LastNonEmpty)
                            '''''' Guarda la Cancelacion
                            strsql = "update factura set estado = 'Cancelada',clave_motivo_cancelacion='" & ClaveMotivo & "' where Idfactura='" & factura & "' "
                            strsql += "update DetPagos set estado = 'Cancelada' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                            clsDatos.cargaComando(strsql)
                            If clsDatos.ejecutar() = 0 Then
                            Else
                                LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                PanelAdvertencia.Visible = True
                            End If

                            Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                            acusexml = timbrarFac.ObtenerAcuseCancelacion(urfc, ppac, uuidCancelar(0).ToString)
                            Dim xml
                            xml = acusexml.XMLResultado
                            If acusexml.CodigoRespuesta = "800" Then
                                Dim nameFile As String = ""
                                Dim doc As XmlDocument = New XmlDocument()
                                doc.LoadXml(xml)
                                nameFile = (Server.MapPath("/Facturas/Canceladas/" & foliofcancelar & ".xml"))
                                'nameFile = "c:\reporteFisio\canceladasOrto\CanceladoOrto_" + txtnumcancelar.Text.ToString.Trim + "_201.xml"
                                doc.Save(nameFile)
                                cargaFacturas()
                                LblMensajeAviso.Text = "La factura se ha cancelado"
                                PanelAvisos.Visible = True
                                PanelAvisos.Focus()

                            Else
                                LblMensajeCritico.Text = "Factura se canceló pero no se pudo obtener el Xml :" & acusexml.MensajeError
                                PanelCritico.Visible = True
                                PanelCritico.Focus()
                                Exit Sub
                            End If
                            cargaFacturas()
                        End If

                        Exit Sub
                    Else
                        LblMensajeCritico.Text = clsDatos.MensajeError
                        PanelCritico.Visible = True
                        PanelCritico.Focus()
                    End If
                End If
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If

    End Sub

    Private Sub VerificarCancelacion()
        Dim strsql As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable

        Dim factura As String
        Dim foliofcancelar As String
        factura = HdIndex.Value
        foliofcancelar = HFolioF.Value
        Dim ValidaCodigo() = {""}
        Dim Valida() = {""}
        Dim detallecancela As New DetalleCancelacion
        limpiarPaneles()
        strsql = "select uuid, IDDocRelacion,rfc,subtotal from factura where IDFactura='" & factura & "'"

        If clsDatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then




                Dim foliocc As String = dt.Rows(0).Item("IDDocRelacion")



                Dim uuidCancelar() As String = {dt.Rows(0).Item("uuid")}
                Dim uuid As String = dt.Rows(0).Item("uuid")
                Dim rfc As String = dt.Rows(0).Item("rfc")
                Dim Total As String = dt.Rows(0).Item("subtotal")



                strsql = "select iddatosfacturacion,rfc,passwordcer,passwordpac, pemcancelacion from CatFacturacion where iddatosfacturacion='1'"

                If clsDatos.cargatabla(strsql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then


                        Dim urfc As String = dt.Rows(0).Item("rfc")

                        Dim ppac As String = dt.Rows(0).Item("passwordpac")

                        Dim pcer As String = dt.Rows(0).Item("passwordcer")

                        Dim pemc As String = dt.Rows(0).Item("pemcancelacion")


                        Dim timbrarFac As New WSCFDI33.WSCFDI33Client
                        Dim cancelacionFac As New WSCFDI33.RespuestaCancelacion


                        Dim listaArreglos As List(Of WSCFDI33.DetalleCFDICancelacion) = New List(Of WSCFDI33.DetalleCFDICancelacion)()

                        Dim Arreglo As WSCFDI33.DetalleCFDICancelacion = New WSCFDI33.DetalleCFDICancelacion

                        Dim RespuestaCancelacionDetallada_FEL As New List(Of AgeMED.WSCFDI33.DetalleCancelacion)

                        Arreglo.RFCReceptor = rfc
                        Arreglo.Total = Total
                        Arreglo.UUID = uuid

                        listaArreglos.Add(Arreglo)

                        cancelacionFac = timbrarFac.CancelarCFDIConValidacion(urfc, ppac, urfc, listaArreglos.ToArray, pemc, pcer)


                        For Each UUIDT As WSCFDI33.DetalleCancelacion In cancelacionFac.DetallesCancelacion

                            LblMensajeAdvertencia.Text += UUIDT.CodigoResultado + vbNewLine
                            LblMensajeAdvertencia.Text += UUIDT.MensajeResultado + vbNewLine
                            LblMensajeAdvertencia.Text += UUIDT.UUID + vbNewLine
                            LblMensajeAdvertencia.Text += "Favor de Verificar!"
                            PanelAdvertencia.Visible = True
                            PanelAdvertencia.Focus()
                        Next

                        If cancelacionFac.OperacionExitosa = True Then

                            RespuestaCancelacionDetallada_FEL = cancelacionFac.DetallesCancelacion.ToList()

                            Select Case cancelacionFac.DetallesCancelacion(0).CodigoResultado

                                Case "201"


                                    '''''' Actualiza el estado de facturas clientes consultas
                                    Dim folioString As String = foliocc
                                    Dim foliosArray() As String = Split(folioString, ",")
                                    Dim LastNonEmpty As Integer = -1
                                    For i As Integer = 0 To foliosArray.Length - 1
                                        If i <> foliosArray.Count Then
                                            LastNonEmpty += 1
                                            foliosArray(LastNonEmpty) = foliosArray(i)
                                            strsql = "update factura set ComplementoPago = '0' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                                            clsDatos.cargaComando(strsql)
                                            If clsDatos.ejecutar() = 0 Then
                                            Else
                                                LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                                PanelAdvertencia.Visible = True
                                            End If
                                        End If
                                    Next
                                    ReDim Preserve foliosArray(LastNonEmpty)
                                    '''''' Guarda la Cancelacion
                                    strsql = "update factura set estado = 'Cancelada' where Idfactura='" & factura & "' "
                                    strsql += "update DetPagos set estado = 'Cancelada' where Idfactura in ('" & foliosArray(LastNonEmpty) & "')"
                                    clsDatos.cargaComando(strsql)
                                    If clsDatos.ejecutar() = 0 Then
                                    Else
                                        LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                        PanelAdvertencia.Visible = True
                                    End If

                                    Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                                    acusexml = timbrarFac.ObtenerAcuseCancelacion(urfc, ppac, uuidCancelar(0).ToString)
                                    Dim xml
                                    xml = acusexml.XMLResultado
                                    If acusexml.CodigoRespuesta = "800" Then
                                        Dim nameFile As String = ""
                                        Dim doc As XmlDocument = New XmlDocument()
                                        doc.LoadXml(xml)
                                        nameFile = (Server.MapPath("/Facturas/Canceladas/" & foliofcancelar & ".xml"))
                                        'nameFile = "c:\reporteFisio\canceladasOrto\CanceladoOrto_" + txtnumcancelar.Text.ToString.Trim + "_201.xml"
                                        doc.Save(nameFile)
                                        cargaFacturas()
                                        LblMensajeAviso.Text = "La factura se ha cancelado"
                                        PanelAvisos.Visible = True
                                        PanelAvisos.Focus()

                                    Else
                                        LblMensajeCritico.Text = "Factura se cancelo pero no se pudo obtener el Xml :" & acusexml.MensajeError
                                        PanelCritico.Visible = True
                                        PanelCritico.Focus()
                                        Exit Sub
                                    End If
                                Case Else
                            End Select

                        Else
                            cargaFacturas()
                        End If

                        Exit Sub
                    Else
                        LblMensajeCritico.Text = clsDatos.MensajeError
                        PanelCritico.Visible = True
                        PanelCritico.Focus()
                    End If
                End If
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If

    End Sub
    Protected Sub BtnNoGFac_Click(sender As Object, e As EventArgs)
        cargaFacturas()
    End Sub

    Sub enviarFac()

        Dim folioFactura As String = folio_factura.Value
        Dim seriefactura As String = serie_factura.Value
        Dim estadoFac As String = estado_factura.Value
        limpiarPaneles()
        Dim Path1 As String
        Dim Path2 As String
        Dim Path As String
        Dim mail As New MailMessage
        If estadoFac = "Vigente" Then
            If HD_IdFactura.Value > 0 Then

                Dim folioFacturas As String = seriefactura + folioFactura
                Path1 = Server.MapPath("/Facturas/XMLT/" + folioFacturas)
                Path2 = Server.MapPath("/Facturas/PDFS/" + folioFacturas)
            Else
                Path = Server.MapPath("/Facturas/" + folioFactura)
            End If


        Else
            Path1 = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
        End If

        mail.From = New MailAddress("facturacion@fisiocare.com.mx")

        mail.To.Add(correo_confirmar.Value)

        mail.Subject = "Factura de pago de servicios"
        mail.Body = "Factura de pago de servicio"
        Try

            If estadoFac = "Vigente" Then

                If HD_IdFactura.Value > 0 Then

                    Dim FilePath1 As String = Path1 + ".xml"
                    Dim FilePath2 As String = Path2 + ".pdf"

                    mail.Attachments.Add(New Attachment(FilePath1))
                    mail.Attachments.Add(New Attachment(FilePath2))

                    Dim mailClient As New SmtpClient()

                    Dim basicAuthenticationInfo As New NetworkCredential("facturacion@fisiocare.com.mx", "fac2018*")

                    mailClient.Host = "mail.fisiocare.com.mx"

                    mailClient.UseDefaultCredentials = True
                    mailClient.Credentials = basicAuthenticationInfo
                    mailClient.Port = 26

                    mailClient.Send(mail)
                    LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                    cargaFacturas()
                    Response.Redirect("facturaspagos.aspx")

                Else
                    Dim FilePath1 As String = Path + ".xml"
                    Dim FilePath2 As String = Path + ".pdf"

                    mail.Attachments.Add(New Attachment(FilePath1))
                    mail.Attachments.Add(New Attachment(FilePath2))

                    Dim mailClient As New SmtpClient()

                    Dim basicAuthenticationInfo As New NetworkCredential("facturacion@fisiocare.com.mx", "fac2018*")

                    mailClient.Host = "mail.fisiocare.com.mx"

                    mailClient.UseDefaultCredentials = True
                    mailClient.Credentials = basicAuthenticationInfo
                    mailClient.Port = 26

                    mailClient.Send(mail)
                    LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                    cargaFacturas()
                    Response.Redirect("facturaspagos.aspx")

                End If


            Else
                Dim FilePath1 As String = Path1 + ".xml"
                mail.Attachments.Add(New Attachment(FilePath1))


                Dim mailClient As New SmtpClient()

                Dim basicAuthenticationInfo As New NetworkCredential("facturacion@fisiocare.com.mx", "fac2018*")

                mailClient.Host = "mail.fisiocare.com.mx"

                mailClient.UseDefaultCredentials = True
                mailClient.Credentials = basicAuthenticationInfo
                mailClient.Port = 26

                mailClient.Send(mail)
                LblMensajeAviso.Text = "Se ha enviado el archivo de cancelacion de la factura (xml)"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                cargaFacturas()
                Response.Redirect("facturaspagos.aspx")
            End If



        Catch ex As Exception
            LblMensajeCritico.Text = "Envio de factura : " + ex.Message
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End Try


    End Sub
    Sub descargarArchivoxml(ByVal folioFactura As String, ByVal serie As String, ByVal tipoArchivo As String, ByVal estadoFac As String)

        Dim path As String

        If estadoFac = "Vigente" Then
            If HD_IdFactura.Value > 0 Then
                path = Server.MapPath("/Facturas/XMLT/" + serie + folioFactura)

            Else
                path = Server.MapPath("/Facturas/" + folioFactura)
            End If

        Else
            path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
        End If
        Try
            'Limpiamos la salida
            Response.Clear()
            'Con esto le decimos al browser que la salida sera descargable
            Response.ContentType = "application/octet-stream"
            'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
            Response.AddHeader("Content-Disposition", "attachment; filename=" + serie + folioFactura + "." + tipoArchivo)
            ' Escribimos el fichero a enviar
            Response.WriteFile(path + "." + tipoArchivo)
            ' volcamos el stream 
            Response.Flush()
            ' Enviamos todo el encabezado ahora
            Response.End()
        Catch ex As Exception
            LblMensajeCritico.Text = ex.Message
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End Try


    End Sub
    Sub descargarArchivopdf(ByVal folioFactura As String, ByVal serie As String, ByVal tipoArchivo As String, ByVal estadoFac As String)

        Dim path As String
        If estadoFac = "Vigente" Then
            If HD_IdFactura.Value > 0 Then
                path = Server.MapPath("/Facturas/PDFS/" + serie + folioFactura)
                If Session("Movil") = 1 Then
                    ' Limpiamos la salida
                    Response.Clear()
                    'Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream"
                    ' esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=Facturas/PDFS/" & serie + folioFactura & ".pdf")
                    ' Escribimos el fichero a enviar 
                    Response.WriteFile("Facturas/PDFS/" & serie + folioFactura & ".pdf")
                    ' volcamos el stream 
                    Response.Flush()
                    ' Enviamos todo el encabezado ahora
                    Response.End()

                Else
                    Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & serie + folioFactura & ".pdf');</script>")

                End If


            Else
                path = Server.MapPath("/Facturas/" + folioFactura)
                If Session("Movil") = 1 Then
                    ' Limpiamos la salida
                    Response.Clear()
                    'Con esto le decimos al browser que la salida sera descargable
                    Response.ContentType = "application/octet-stream"
                    ' esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                    Response.AddHeader("Content-Disposition", "attachment; filename=Facturas/" & serie + folioFactura & ".pdf")
                    ' Escribimos el fichero a enviar 
                    Response.WriteFile("Facturas/" & folioFactura & ".pdf")
                    ' volcamos el stream 
                    Response.Flush()
                    ' Enviamos todo el encabezado ahora
                    Response.End()

                Else
                    Response.Write("<script type='text/javascript'>detailedresults=window.open('ImpfacturaAntiguas.aspx?doc=" & folioFactura & ".pdf');</script>")

                End If

            End If
        Else
            LblMensajeAdvertencia.Text = "La Factura  " & serie + folioFactura & " es cancelada"
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If


    End Sub

    Sub limpiarPaneles()
        LblMensajeAdvertencia.Text = ""
        LblMensajeAviso.Text = ""
        LblMensajeCritico.Text = ""
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelDesicion.Visible = False
    End Sub


    '**********************Facturas********************************************
    'Sub CargarDatosFacturacion(CodigoPaciente)
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String
    '    'Limpiar Campos
    '    input_razon_social.Value = ""
    '    rfc.Value = ""
    '    correo.Value = ""
    '    direccion.Value = ""
    '    cp.Value = ""
    '    ciudad.Value = ""
    '    estado.Value = ""
    '    pais.Value = ""
    '    codigo_paciente.Value = CodigoPaciente
    '    strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"

    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            input_razon_social.Value = dt.Rows(0).Item("razonsocial")
    '            rfc.Value = dt.Rows(0).Item("rfc")
    '            correo.Value = dt.Rows(0).Item("email")
    '            direccion.Value = dt.Rows(0).Item("direccion")
    '            cp.Value = dt.Rows(0).Item("cp")
    '            ciudad.Value = dt.Rows(0).Item("ciudad")
    '            estado.Value = dt.Rows(0).Item("estado")
    '            pais.Value = dt.Rows(0).Item("pais")
    '            codigo_paciente.Value = dt.Rows(0).Item("CodigoPaciente")
    '            id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
    '        End If
    '    End If
    'End Sub

    'Sub LlenarComboRazonSocial(CodigoPaciente)
    '    Dim strSQL As String
    '    Dim clsDatos As New ClaseDatos
    '    Dim funciones As New FuncionesGenerales
    '    codigo_paciente.Value = CodigoPaciente
    '    strSQL = "SELECT idRazonSocial, razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"
    '    If funciones.llenadropdown(strSQL, select_razon_social) = -1 Then
    '        'Error
    '        div_input_razon_social.Visible = True
    '    Else
    '        'Se realizó la función, verificar si hay datos
    '        If select_razon_social.Items.Count = 0 Then
    '            div_input_razon_social.Visible = True
    '            div_select_razon_social.Visible = False
    '            'Habilitar bandera para dar de alta el nuevo
    '            bandera.Value = 1
    '        Else
    '            div_input_razon_social.Visible = False
    '            div_select_razon_social.Visible = True
    '            select_razon_social.Items.Add("NUEVO")
    '            bandera.Value = 0
    '        End If


    '    End If
    'End Sub

    'Sub CambioSelectRazonSocial()
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String
    '    'Limpiar Campos
    '    input_razon_social.Value = ""
    '    rfc.Value = ""
    '    correo.Value = ""
    '    direccion.Value = ""
    '    cp.Value = ""
    '    ciudad.Value = ""
    '    estado.Value = ""
    '    pais.Value = ""
    '    If select_razon_social.SelectedValue = "NUEVO" Then
    '        'Agregar Nuevo
    '        div_input_razon_social.Visible = True
    '        div_select_razon_social.Visible = False
    '        pais.Value = "MÉXICO"
    '        estado.Value = "YUCATÁN"
    '        ciudad.Value = "MÉRIDA"
    '        'Habilitar bandera para dar de alta el nuevo
    '        bandera.Value = 1
    '    Else
    '        strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE idRazonSocial =" + select_razon_social.SelectedValue
    '        If clsdatos.cargatabla(strsql, dt) = 0 Then
    '            If dt.Rows.Count > 0 Then
    '                input_razon_social.Value = dt.Rows(0).Item("razonsocial")
    '                rfc.Value = dt.Rows(0).Item("rfc")
    '                correo.Value = dt.Rows(0).Item("email")
    '                direccion.Value = dt.Rows(0).Item("direccion")
    '                cp.Value = dt.Rows(0).Item("cp")
    '                ciudad.Value = dt.Rows(0).Item("ciudad")
    '                estado.Value = dt.Rows(0).Item("estado")
    '                pais.Value = dt.Rows(0).Item("pais")
    '                id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
    '            End If
    '        End If
    '    End If
    'End Sub

    'Sub Facturar()


    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String
    '    Dim dt As New DataTable

    '    If bandera.Value = 1 Then
    '        'Dar de alta la razón social
    '        strsql = "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] (CodigoPaciente, razonSocial, rfc, email, direccion, cp, pais, estado, ciudad )VALUES('" & codigo_paciente.Value & "', '" & input_razon_social.Value & "', '" & rfc.Value & "', '" & correo.Value & "', '" & direccion.Value & "', '" & cp.Value & "', '" & pais.Value & "', '" & estado.Value & "', '" & ciudad.Value & "'  )"
    '        clsdatos.cargaComando(strsql)
    '        If clsdatos.ejecutar() = 0 Then
    '            strsql = "SELECT idRazonSocial, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] " & _
    '                      "WHERE CodigoPaciente = '" & codigo_paciente.Value & "' and razonSocial='" & input_razon_social.Value & "' and rfc='" & rfc.Value & "'  and email='" & correo.Value & "' "
    '            If clsdatos.cargatabla(strsql, dt) = 0 Then
    '                id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
    '                'Timbrado()
    '                cargaFacturas()
    '                LblMensajeAviso.Text = "Refacturacion"
    '                PanelAvisos.Visible = True
    '                PanelAvisos.Focus()
    '            End If

    '        Else
    '            LblMensajeCritico.Text = clsdatos.MensajeError
    '            PanelCritico.Visible = True
    '            PanelCritico.Focus()
    '        End If
    '    Else
    '        'Actualizar la razón social
    '        strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] SET razonSocial='" & input_razon_social.Value & "', rfc='" & rfc.Value & "', email='" & correo.Value & "', direccion='" & direccion.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', estado='" & estado.Value & "', ciudad= '" & ciudad.Value & "' WHERE idRazonSocial = " + id_razon_social.Value
    '        clsdatos.cargaComando(strsql)
    '        If clsdatos.ejecutar() = 0 Then
    '            'Timbrado()
    '            cargaFacturas()
    '            LblMensajeAviso.Text = "Refacturacion"
    '            PanelAvisos.Visible = True
    '            PanelAvisos.Focus()
    '            'cargaAgenda()
    '            'UpdatePanel1.Update()
    '        Else
    '            LblMensajeCritico.Text = clsdatos.MensajeError
    '            PanelCritico.Visible = True
    '            PanelCritico.Focus()
    '        End If
    '    End If
    '    cargaFacturas()
    'End Sub


    ' ''********* TIMBRADO

    ''Function Timbrado()
    ''    Dim banderaError As Boolean = False
    ''    ' Dim fechapdf As String
    ''    Dim dt As New DataTable
    ''    Dim clsdatos As New ClaseDatos
    ''    Dim strsql As String
    ''    Dim folio As String = ""
    ''    Dim serie As String = ""
    ''    Dim cuenta As Int32 = 0
    ''    Dim contador As Int32 = 0


    ''    strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from ConsecutivoFac CodigoCF='1'"

    ''    If clsdatos.cargatabla(strsql, dt) = 0 Then
    ''        If dt.Rows.Count > 0 Then
    ''            folio = dt.Rows(0).Item("consecutivo")
    ''            serie = dt.Rows(0).Item("serie")
    ''        Else
    ''            LblMensajeCritico.Text = "Error en el consecutivo de folio"
    ''            PanelCritico.Visible = True
    ''            PanelCritico.Focus()
    ''        End If
    ''    End If


    ''    '********************** FACTURACION ******************************

    ''    '*************  -DATOS DEL EMISOR- ******************
    ''    Dim emisor As New Emisor()
    ''    emisor.rfc = "OST141127IEA"
    ''    emisor.nombre = "ORTOPEDIA STAR, S.C.P."
    ''    emisor.domicilioFiscal = New DomicilioFiscal
    ''    emisor.domicilioFiscal.calle = "26"
    ''    emisor.domicilioFiscal.noExterior = "299"
    ''    emisor.domicilioFiscal.noInterior = "928"
    ''    emisor.domicilioFiscal.colonia = "FRACC. ALTABRISA"
    ''    emisor.domicilioFiscal.localidad = "MÉRIDA"
    ''    emisor.domicilioFiscal.municipio = "MÉRIDA"
    ''    emisor.domicilioFiscal.estado = "YUCATÁN"
    ''    emisor.domicilioFiscal.pais = "MEXICO"
    ''    emisor.domicilioFiscal.codigoPostal = "97133"
    ''    emisor.regimenFiscal = New RegimenFiscal
    ''    emisor.regimenFiscal.regimen = "REGIMEN GENERAL DE LEY PERSONAS MORALES"

    ''    '************* - DATOS DEL RECEPTOR- ****************
    ''    Dim receptor As New Receptor()
    ''    receptor.rfc = rfc.Value
    ''    receptor.nombre = input_razon_social.Value
    ''    receptor.domicilio = New Domicilio()
    ''    receptor.domicilio.calle = direccion.Value
    ''    receptor.domicilio.localidad = ciudad.Value
    ''    receptor.domicilio.codigoPostal = cp.Value
    ''    receptor.domicilio.municipio = ciudad.Value
    ''    receptor.domicilio.estado = estado.Value
    ''    receptor.domicilio.pais = pais.Value

    ''    '************** -DATOS DEL COMPROBANTE- ************
    ''    Dim hoy As DateTime = DateTime.Now
    ''    Dim comp As New Comprobante()
    ''    Dim total, formaPago As String
    ''    Dim variasFacturas As Boolean
    ''    Dim dtConceptos, dtTotal As DataTable
    ''    Dim listafolios, listaFoliosSQL As String


    ''    formaPago = ""
    ''    total = ""
    ''    '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
    ''    cargardatosFac(formaPago, total)
    ''    If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
    ''        total = dtTotal.Rows(0).Item("costoTotal")
    ''        variasFacturas = True
    ''    Else
    ''        variasFacturas = False
    ''    End If

    ''    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ''    comp.fecha = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))
    ''    fechapdf = comp.fecha
    ''    comp.folio = folio
    ''    comp.serie = serie
    ''    comp.formaDePago = "PAGO EN UNA SOLA EXHIBICION"
    ''    comp.subTotal = total
    ''    comp.total = total
    ''    comp.tipoDeComprobante = "ingreso"
    ''    comp.moneda = "MXP"
    ''    comp.tipoCambio = "1.0"
    ''    comp.metodoDePago = formaPago
    ''    comp.lugarExpedicion = "MÉRIDA,YUCATÁN"

    ''    '************ -CONCEPTO- *******************

    ''    comp.conceptos = New List(Of Concepto)()
    ''    Do While contador = cuenta

    ''        If variasFacturas = True Then

    ''            For Each fila As DataRow In dtConceptos.Rows
    ''                Dim concepto1 As New Concepto()
    ''                concepto1.cantidad = fila.Item("cantidad")
    ''                concepto1.unidad = "No aplica"
    ''                concepto1.noIdentificacion = "noIdentificacion"
    ''                concepto1.descripcion = fila.Item("descripcion")
    ''                concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
    ''                concepto1.valorUnitario = fila.Item("costo")
    ''                comp.conceptos.Add(concepto1)

    ''            Next

    ''        Else
    ''            strsql = " SELECT codigoConcepto,descripcion,convert(varchar,convert(decimal(8,2),costo)) as costo,cantidad FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
    ''           "WHERE folioConsulta='" & fconsulta.Value & "' AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1"
    ''            If clsdatos.cargatabla(strsql, dt) = 0 Then
    ''                For Each fila As DataRow In dt.Rows
    ''                    Dim concepto1 As New Concepto()
    ''                    concepto1.cantidad = fila.Item("cantidad")
    ''                    concepto1.unidad = "No aplica"
    ''                    concepto1.noIdentificacion = "noIdentificacion"
    ''                    concepto1.descripcion = fila.Item("descripcion")
    ''                    concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
    ''                    concepto1.valorUnitario = fila.Item("costo")
    ''                    comp.conceptos.Add(concepto1)

    ''                Next
    ''            Else
    ''                'Error en la consulta
    ''            End If
    ''        End If

    ''        contador = contador + 1
    ''    Loop

    ''    '*************** - CANTIDAD EN LETRAS- *****************
    ''    Dim funciones2 As New FuncionesGenerales

    ''    Dim tletras As String = " "



    ''    tletras = funciones2.monto(Convert.ToDouble(comp.total))

    ''    '*************** - IMPUESTOS- *****************
    ''    Dim iva As New Traslado()
    ''    iva.impuesto = "IVA"
    ''    iva.importe = "0.0000"
    ''    iva.tasa = "0"

    ''    Dim impuestos As New Impuestos()
    ''    impuestos.traslados = New List(Of Traslado)()
    ''    impuestos.totalImpuestosTrasladados = "0.0000"
    ''    impuestos.totalImpuestosRetenidos = "0.0000"
    ''    impuestos.traslados.Add(iva)




    ''    '************************** TIMBRADO Y RESPUESTA****************************
    ''    'Envio al Web Service

    ''    comp.emisor = emisor
    ''    comp.receptor = receptor
    ''    comp.impuestos = impuestos

    ''    Dim Xml
    ''    Dim cer
    ''    Try
    ''        cer = New X509Certificate2(Server.MapPath("/OST14112729122014.pfx"), "orto2014", X509KeyStorageFlags.MachineKeySet)
    ''        Xml = CFDIv32.Serializar(comp, False)

    ''    Catch ex As Exception
    ''        banderaError = True
    ''        LblMensajeCritico.Text = "Error en el sellado del XML para timbrar favor de llamar a informática " + vbNewLine + ex.Message.ToString
    ''        PanelCritico.Visible = True
    ''        PanelCritico.Focus()
    ''    End Try

    ''    If banderaError = False Then
    ''        Dim cadenaOriginal = CFDIv32.CadenaOriginalSellado(Xml)

    ''        Try
    ''            Dim ar = cer.GetSerialNumber
    ''            Array.Reverse(ar)
    ''            comp.noCertificado = Encoding.UTF8.GetString(ar)
    ''            comp.certificado = Convert.ToBase64String(cer.RawData) 'Certificado igual para la cancelación del cfdi
    ''            comp.sello = CFDIv32.Sello(cadenaOriginal, cer)

    ''            Xml = CFDIv32.Serializar(comp, True)
    ''            CFDIv32.Validar(Xml, True)
    ''            File.WriteAllText(Server.MapPath("/mandatorioSGO.xml"), Xml)
    ''        Catch ex As Exception
    ''            banderaError = True
    ''            LblMensajeCritico.Text = "Error en el XML para timbrar favor de llamar a informática" + ex.Message.ToString
    ''            PanelCritico.Visible = True
    ''            PanelCritico.Focus()
    ''        End Try

    ''        Dim uuid, sellocfd, nocertificadoSat, selloSat, FechaTimbrado As String
    ''        If banderaError = False Then
    ''            Try

    ''                Dim timbrarFac As New AgeMED.WSFEL.WSTFDClient
    ''                Dim timbreFac As New RespuestaTFD



    ''                timbreFac = timbrarFac.TimbrarCFDI("OST141127IEA", "kBMEtFvuTrr#", Xml, "agemed_" + comp.folio.ToString)


    ''                Xml = timbreFac.XMLResultado

    ''                If timbreFac.MensajeError.Trim = "" And timbreFac.OperacionExitosa = True Then
    ''                    CFDIv32.Validar(Xml, True)
    ''                    uuid = timbreFac.Timbre.UUID
    ''                    sellocfd = timbreFac.Timbre.SelloCFD
    ''                    nocertificadoSat = timbreFac.Timbre.NumeroCertificadoSAT
    ''                    selloSat = timbreFac.Timbre.SelloSAT
    ''                    FechaTimbrado = timbreFac.Timbre.FechaTimbrado


    ''                    '''''' Guarda la factura
    ''                    Dim foliosConsulta As String
    ''                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    ''                    If variasFacturas = True Then
    ''                        foliosConsulta = listaFoliosSQL
    ''                    Else

    ''                        foliosConsulta = "'" + fconsulta.Value + "'"
    ''                    End If

    ''                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''                    strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[IngresosCaja] set facturado=1 " & _
    ''                            " where folioconsulta in (" & listafolios & ")  "
    ''                    strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[DetFacturas] (FolioFactura,Serie,rfc,TipoFactura,"
    ''                    strsql += vbNewLine & " OrigenFactura,subtotal,ImporteIva,Total ,"
    ''                    strsql += vbNewLine & " IdRazonSocial,CodigoPaciente,Status,fechaFactura,xml,uuid,numerocertificadosat,sellocfd,sellosat,fechatimbrado,estado,IdEmpleado,codigoEmpresa,FolioConsulta,tipoDePago,conciliado,Saldo) VALUES  "
    ''                    strsql += vbNewLine & " ('" & folio & "','" & serie & "','" & rfc.Value & "', '1', '1','" & comp.subTotal & "',"
    ''                    strsql += vbNewLine & " '" & iva.importe & "','" & comp.total & "','" & id_razon_social.Value & "','" & codigo_paciente.Value & "','1', getdate() , '" & Xml & "', '" & uuid & "',"
    ''                    strsql += vbNewLine & " '" & nocertificadoSat & "','" & sellocfd & "','" & selloSat & "','" & FechaTimbrado & "','" & timbreFac.Timbre.Estado & "','" & Session("codigoUsuario") & "','" & Session("codigoEmpresa") & "','" & foliosConsulta & "'," & formaPago & ",'0','" & comp.total & "')"
    ''                    clsdatos.cargaComando(strsql)
    ''                    If clsdatos.ejecutar() <> 0 Then
    ''                        LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
    ''                        PanelAdvertencia.Visible = True
    ''                    End If

    ''                    '''''' Actualiza el consecutivo 

    ''                    strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where CodigoCF in (1,2) "
    ''                    clsdatos.cargaComando(strsql)
    ''                    clsdatos.ejecutar()


    ''                Else
    ''                    folio = ""
    ''                    banderaError = True
    ''                    LblMensajeCritico.Text = "No cerrar; Error en el XML de timbrado favor de llamar a informática; " + timbreFac.MensajeErrorDetallado.ToString.Trim
    ''                    PanelCritico.Visible = True
    ''                    PanelCritico.Focus()
    ''                End If
    ''            Catch ex As Exception
    ''                banderaError = True
    ''                LblMensajeCritico.Text = "Error en el timbrado favor de llamar a informática " + ex.Message.ToString
    ''                PanelCritico.Visible = True
    ''                PanelCritico.Focus()
    ''            End Try
    ''        End If

    ''        Dim nameFile As String = ""
    ''        If banderaError = False Then
    ''            Try
    ''                Dim doc As XmlDocument = New XmlDocument()
    ''                doc.LoadXml(Xml)
    ''                nameFile = (Server.MapPath("/Facturas/" + comp.folio.ToString + ".xml"))
    ''                doc.Save(nameFile)
    ''                CFDIv32.Validar(Xml, True)
    ''            Catch ex As Exception
    ''                banderaError = True
    ''                LblMensajeCritico.Text = "Eror en el guradado del XML favor de llamar a informática"
    ''                PanelCritico.Visible = True
    ''                PanelCritico.Focus()
    ''            End Try
    ''        End If

    ''        If banderaError = False Then
    ''            Try
    ''                Dim cadenaOriComplemento As String = CFDIv32.CadenaOriginalImpresa(Xml)

    ''                Dim QRCodeEncoder As New QRCodeEncoder
    ''                QRCodeEncoder.QRCodeScale = "3"
    ''                QRCodeEncoder.QRCodeVersion = "7"
    ''                QRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M

    ''                Dim imagen As Bitmap
    ''                Dim auxcad = Split(comp.total, ".")
    ''                Dim enteros As String = auxcad(0).ToString.Trim.PadLeft(10, "0")
    ''                Dim decimales As String = auxcad(1).ToString.Trim.PadRight(6, "0")
    ''                Dim datos As String = "?re=" + emisor.rfc.Trim + "&rr=" + receptor.rfc.Trim + "&tt=" + enteros + "." + decimales + "&id=" + uuid
    ''                imagen = QRCodeEncoder.Encode(datos)
    ''                imagen.Save(Server.MapPath("/Facturas/" + comp.folio.ToString + ".jpeg"), Imaging.ImageFormat.Jpeg)

    ''                imprimirFacturaCfdi(listafolios.ToString, comp.folio.ToString, comp.serie.ToString, receptor.domicilio.calle.ToString, receptor.domicilio.municipio, receptor.domicilio.codigoPostal, receptor.domicilio.localidad, receptor.domicilio.estado, comp.total, tletras, comp.formaDePago, comp.metodoDePago, comp.subTotal, iva.importe, FechaTimbrado, comp.noCertificado.ToString, nocertificadoSat, uuid, cadenaOriComplemento, selloSat, sellocfd)



    ''                enviarFacr(comp.folio.ToString)

    ''                cargaFacturas()


    ''                VisualizarFac(comp.folio.ToString)



    ''                'LblMensajeAviso.Text = "Factura enviada "
    ''                'PanelAvisos.Visible = True
    ''                'PanelAvisos.Focus()
    ''                'Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & comp.folio.ToString & ".pdf');</script>")


    ''            Catch ex As Exception
    ''                LblMensajeAdvertencia.Text = "Error en la impresión del PDF llamar a informática " + ex.Message.ToString
    ''                PanelAdvertencia.Visible = True
    ''                'PanelAdvertencia.Focus()

    ''            End Try

    ''        End If
    ''    End If

    ''    Return banderaError
    ''End Function
    ''Sub VisualizarFac(ByRef folioFac As String)

    ''    Dim folioFactura As String = folioFac

    ''    Dim path As String

    ''    path = Server.MapPath("/Facturas/" + folioFactura)

    ''    Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc= " & folioFactura & " .pdf');</script>")

    ''End Sub

    ''Sub imprimirFacturaCfdi(ByVal lfolioconsulta As String, ByVal queimprimo As String, ByVal serie As String, ByVal domicilio As String, ByVal municipio As String, ByVal cp As String, ByVal localidad As String, ByVal estado As String, ByVal totalfac As String, ByVal totalletras As String, ByVal formadepago As String, ByVal metodopago As String, ByVal subtotalfac As String, ByVal iva As String, ByVal fechacer As String, ByVal ceremisor As String, ByVal cersat As String, ByVal uuid As String, ByVal cadoriginal As String, ByVal sellosat As String, ByVal selloCFD As String)
    ''    Dim mireporte As New ReportDocument
    ''    Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
    ''    Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
    ''    Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
    ''    Dim dt As New DataTable
    ''    Dim clsdatos As New ClaseDatos
    ''    Dim strsql As String


    ''    mireporte.Load(Server.MapPath("/Facturas/Reporte/FacturaCFDI.rpt"))


    ''    '++++++++++++++++++++ Detalles de la Factura

    ''    Dim columna As New DataColumn("cantidad")
    ''    dt.Columns.Add(columna)
    ''    Dim col2 As New DataColumn("descripcion")
    ''    dt.Columns.Add(col2)
    ''    Dim col3 As New DataColumn("importe")
    ''    dt.Columns.Add(col3)
    ''    Dim col4 As New DataColumn("precio")
    ''    dt.Columns.Add(col4)
    ''    dt.Columns.Add(New DataColumn("img", GetType(Byte())))


    ''    Dim fs As FileStream = New FileStream(Server.MapPath("/Facturas/" + queimprimo.ToString + ".jpeg"), FileMode.Open)
    ''    Dim br As BinaryReader = New BinaryReader(fs)
    ''    Dim imagen(CInt(fs.Length)) As Byte

    ''    br.Read(imagen, 0, CInt(fs.Length))
    ''    br.Close()
    ''    fs.Close()

    ''    Dim variasFacturas As Boolean
    ''    Dim dtConceptos, dtTotal As DataTable
    ''    Dim listafolios, listaFoliosSQL As String
    ''    Dim contador As Int32 = 0
    ''    Dim cuenta As Int32 = 0

    ''    If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
    ''        variasFacturas = True
    ''    Else
    ''        variasFacturas = False
    ''    End If

    ''    Do While contador = cuenta

    ''        If variasFacturas = True Then

    ''            For Each fila As DataRow In dtConceptos.Rows
    ''                Dim concepto1 As New Concepto()
    ''                Dim nFila As DataRow
    ''                nFila = dt.NewRow
    ''                nFila(0) = fila.Item("cantidad")
    ''                nFila(1) = fila.Item("descripcion")
    ''                nFila(2) = fila.Item("costo")
    ''                nFila(3) = fila.Item("costo") * fila.Item("cantidad")
    ''                nFila(4) = imagen
    ''                dt.Rows.Add(nFila)
    ''            Next

    ''        End If

    ''        contador = contador + 1
    ''    Loop

    ''    mireporte.SetDataSource(dt.DefaultView)


    ''    ' -------------- datos folio/serie factura
    ''    Mivar.Value = serie + " " + queimprimo
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("recibo").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    '--------------datos facturacion
    ''    Mivar.Value = input_razon_social.Value
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("nombre").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = domicilio
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("domicilio").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    'Mivar.Value = colonia
    ''    'rpDatos.Add(Mivar)
    ''    'mireporte.DataDefinition.ParameterFields("colonia").ApplyCurrentValues(rpDatos)
    ''    'rpDatos.Clear()

    ''    Mivar.Value = municipio
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("municipio").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = cp
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("cp").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = localidad
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("localidad").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = estado
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("estado").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = pais.Value
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("paisfac").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = rfc.Value
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("rfc").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = Session("nombrePaciente")
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = fechapdf
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()


    ''    strsql = "  select descripcion " & _
    ''              " from  CatFormasDePago" & _
    ''              " where codigoFormaPago = '" & metodopago & "'"

    ''    If clsdatos.cargatabla(strsql, dt) = 0 Then
    ''        If dt.Rows.Count > 0 Then
    ''            Mivar.Value = dt.Rows(0).Item("descripcion")
    ''            rpDatos.Add(Mivar)
    ''            mireporte.DataDefinition.ParameterFields("formapago").ApplyCurrentValues(rpDatos)
    ''            rpDatos.Clear()
    ''        Else
    ''            LblMensajeCritico.Text = clsdatos.MensajeError
    ''            PanelCritico.Visible = True
    ''        End If

    ''    End If


    ''    strsql = "  select referencia " & _
    ''             " from  IngresosPagosCajas" & _
    ''             " where folioConsulta =  " & lfolioconsulta & " "

    ''    If clsdatos.cargatabla(strsql, dt) = 0 Then
    ''        If dt.Rows.Count > 0 Then
    ''            If dt.Rows(0).Item("referencia") = "0" Then
    ''                Mivar.Value = ""
    ''                rpDatos.Add(Mivar)
    ''                mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    ''                rpDatos.Clear()
    ''            Else
    ''                Mivar.Value = dt.Rows(0).Item("referencia")
    ''                rpDatos.Add(Mivar)
    ''                mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    ''                rpDatos.Clear()
    ''            End If
    ''        Else
    ''            Mivar.Value = ""
    ''            rpDatos.Add(Mivar)
    ''            mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    ''            rpDatos.Clear()
    ''        End If

    ''    End If

    ''    '-----Observaciones de la Factura

    ''    Mivar.Value = observacionesf.Value
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("observacionesfac").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()


    ''    Mivar.Value = metodopago
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("ClaveMP").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    '-------------------------importes
    ''    Mivar.Value = subtotalfac
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("importe").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = iva
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("iva").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = totalfac
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = totalletras
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("cantidadletras").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()


    ''    Mivar.Value = fechacer
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("fechacer").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = ceremisor
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("ceremisor").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = cersat
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("cersat").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = uuid
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("uuid").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = cadoriginal
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("cadenaoriginal").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = sellosat
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("sellosat").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    Mivar.Value = selloCFD
    ''    rpDatos.Add(Mivar)
    ''    mireporte.DataDefinition.ParameterFields("selloCFD").ApplyCurrentValues(rpDatos)
    ''    rpDatos.Clear()

    ''    '--------------------------------------------------


    ''    Try
    ''        Dim nomArchivo As String = queimprimo.ToString.Trim
    ''        nomArchivo.Replace(" ", "")
    ''        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    ''        Dim o As CrystalDecisions.Shared.ExportOptions
    ''        o = New CrystalDecisions.Shared.ExportOptions
    ''        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    ''        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    ''        filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
    ''        o.ExportDestinationOptions = filedest.Clone
    ''        mireporte.Export(o)
    ''        filedest = Nothing
    ''        o = Nothing
    ''        mireporte.Close()
    ''        'Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & nomArchivo & ".pdf');</script>")
    ''    Catch ex As Exception
    ''        LblMensajeCritico.Text = "Error 1: " + clsdatos.MensajeError
    ''        PanelCritico.Visible = True
    ''    End Try

    ''    'Dim tipoArchivo As String = "pdf"
    ''    ' descargarArchivofac(queimprimo, tipoArchivo)

    ''    'Dim path As String

    ''    'path = Server.MapPath("/Facturas/" + queimprimo.ToString.Trim)


    ''    'Try
    ''    '    'Limpiamos la salida
    ''    '    Response.Clear()
    ''    '    'Con esto le decimos al browser que la salida sera descargable
    ''    '    Response.ContentType = "application/octet-stream"
    ''    '    'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
    ''    '    Response.AddHeader("Content-Disposition", "attachment; filename=" + queimprimo.ToString.Trim + ".pdf")
    ''    '    '(Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".pdf"))
    ''    '    ' Escribimos el fichero a enviar
    ''    '    Response.WriteFile(path + ".pdf")
    ''    '    ' volcamos el stream 
    ''    '    Response.Flush()
    ''    '    ' Enviamos todo el encabezado ahora
    ''    '    Response.End()
    ''    'Catch ex As Exception
    ''    '    LblMensajeCritico.Text = ex.Message
    ''    '    PanelCritico.Visible = True
    ''    '    PanelCritico.Focus()
    ''    'End Try

    ''    'visor_reporte.DataBind()

    ''    'Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    ''    'Dim o As CrystalDecisions.Shared.ExportOptions
    ''    'o = New CrystalDecisions.Shared.ExportOptions
    ''    'o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    ''    'o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    ''    'filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
    ''    'o.ExportDestinationOptions = filedest.Clone
    ''    'mireporte.Export(o)
    ''    'filedest = Nothing
    ''    'o = Nothing






    ''    'Dim Nombre As String

    ''    'Nombre = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".pdf"))
    ''    'Dim xmlArchivo As String = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".xml"))


    ''    'Response.Clear()
    ''    'Response.ContentType = "application/pdf"
    ''    'Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
    ''    'Response.WriteFile(Nombre)
    ''    'Response.Flush()
    ''    'Response.Close()
    ''End Sub
    ''Sub descargarArchivofac(ByVal queimprimo As String, ByVal tipoArchivo As String)
    ''    Dim path As String

    ''    path = Server.MapPath("/Facturas/" + queimprimo)


    ''    Try
    ''        'Limpiamos la salida
    ''        Response.Clear()
    ''        'Con esto le decimos al browser que la salida sera descargable
    ''        Response.ContentType = "application/octet-stream"
    ''        'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
    ''        Response.AddHeader("Content-Disposition", "attachment; filename=" + queimprimo + "." + tipoArchivo)
    ''        ' Escribimos el fichero a enviar
    ''        Response.WriteFile(path + "." + tipoArchivo)
    ''        ' volcamos el stream 
    ''        Response.Flush()
    ''        ' Enviamos todo el encabezado ahora
    ''        Response.End()
    ''    Catch ex As Exception
    ''        LblMensajeCritico.Text = ex.Message
    ''        PanelCritico.Visible = True
    ''        PanelCritico.Focus()
    ''    End Try

    ''End Sub
    ''Sub enviarFacr(ByRef folioFac As String)
    ''    Dim folioFactura As String = folioFac
    ''    'Dim estadoFac As String = estado_factura.Value
    ''    Dim Path As String
    ''    Dim mail As New MailMessage
    ''    'If estadoFac = "Vigente" Then
    ''    Path = Server.MapPath("/Facturas/" + folioFactura)
    ''    'Else
    ''    'Path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
    ''    'End If

    ''    mail.From = New MailAddress("facturacion.alainsanchez@agemed.com.mx")

    ''    mail.To.Add(correo.Value)

    ''    mail.Subject = "Factura de servicios de Ortopedia Star"
    ''    mail.Body = "Facturas"
    ''    Try
    ''        Dim FilePath1 As String = Path + ".xml"
    ''        Dim FilePath2 As String = Path + ".pdf"

    ''        'el archivo se adjunta indicándole la ruta 
    ''        mail.Attachments.Add(New Attachment(FilePath1))
    ''        mail.Attachments.Add(New Attachment(FilePath2))

    ''        Dim mailClient As New SmtpClient()

    ''        Dim basicAuthenticationInfo As New NetworkCredential("facturacion.alainsanchez@agemed.com.mx", "Facas2018*")

    ''        mailClient.Host = "mail.agemed.com.mx"

    ''        mailClient.UseDefaultCredentials = True
    ''        mailClient.Credentials = basicAuthenticationInfo
    ''        mailClient.Port = 26

    ''        mailClient.Send(mail)
    ''        LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
    ''        PanelAvisos.Visible = True
    ''        PanelAvisos.Focus()
    ''    Catch ex As Exception
    ''        LblMensajeCritico.Text = ex.Message
    ''        PanelCritico.Visible = True
    ''        PanelCritico.Focus()
    ''    End Try
    ''End Sub
    'Sub cargardatosFac(ByRef formaPago As String, ByRef importe As String)
    '    Dim clsDatos As New ClaseDatos
    '    Dim dt As New DataTable
    '    Dim strSQL As String
    '    strSQL = "SELECT   convert(varchar,convert(decimal(8,2),importePago)) as importePago ,codigoFormaPago FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
    '               "where folioconsulta='" & fconsulta.Value & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & ""
    '    If clsDatos.cargatabla(strSQL, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            formaPago = dt.Rows(0).Item("codigoFormaPago")
    '            importe = dt.Rows(0).Item("importePago")
    '        End If

    '    End If

    'End Sub
    'Sub GuardarIngeroCajas()
    '    Dim clsDatos As New ClaseDatos
    '    Dim dt As New DataTable
    '    Dim strSQL As String
    '    Dim pago As String = cargarTotalPagos()
    '    strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado,abono) " & _
    '             "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,0," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0,0)"
    '    clsDatos.cargaComando(strSQL)
    '    If clsDatos.ejecutar() = 0 Then
    '    Else
    '        LblMensajeAdvertencia.Text = clsDatos.MensajeError
    '        PanelAdvertencia.Visible = True
    '        PanelAdvertencia.Focus()
    '    End If
    '    'Dim clsDatos As New ClaseDatos
    '    'Dim dt As New DataTable
    '    'Dim strSQL As String
    '    'Dim pago As String = cargarTotalPagos()
    '    'strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado) " & _
    '    '         "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,0," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0)"
    '    'clsDatos.cargaComando(strSQL)
    '    'If clsDatos.ejecutar() = 0 Then
    '    'End If
    'End Sub
    'Function cargarTotalPagos() As String
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String
    '    strsql = "SELECT sum(cantidad * costo) as Total " & _
    '             " FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
    '               "where folioConsulta='" & Session("folioConsulta") & "' and codigoempresa = " & Session("codigoEmpresa") & "  and status=1"
    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If Not IsDBNull(dt.Rows(0).Item("Total")) Then
    '            Return dt.Rows(0).Item("Total").ToString
    '        Else
    '            Return "0.0"
    '        End If

    '    Else
    '        LblMensajeCritico.Text = clsdatos.MensajeError
    '        PanelCritico.Visible = True
    '        Return "0.0"
    '    End If
    'End Function
    'Sub buscarConsultasAfacturar(ByRef codigoPAciente As String, ByRef folioConsulta As String)
    '    Dim clsDatos As New ClaseDatos
    '    Dim strSQL As String
    '    Dim dt2 As New DataTable

    '    strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
    '        "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
    '        "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
    '        "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
    '        "where A.codigoPaciente='" & codigoPAciente & "' and  IC.Facturado=0  and IC.PagoRealizado=0 and A.folioConsulta='" & Session("folioConsulta") & "'" & _
    '        "order by fechaAgenda desc "
    '    bandera_facturaCredito = 1 ' si es credito
    '    If clsDatos.cargatabla(strSQL, dt2) = 0 Then
    '        If dt2.Rows.Count = 0 Then
    '            bandera_facturaCredito = 0 ' no es credito
    '            strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
    '        "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
    '        "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
    '        "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
    '        "where A.codigoPaciente='" & codigoPAciente & "' and IC.PagoRealizado=1 and IC.Facturado=0 " & _
    '        "order by fechaAgenda desc "
    '        End If
    '    End If
    '    If clsDatos.cargatabla(strSQL, dt2) = 0 Then
    '        If dt2.Rows.Count > 0 Then
    '            GdConsultasXpagar.DataSource = dt2
    '            GdConsultasXpagar.DataBind()
    '            For Each fila As GridViewRow In GdConsultasXpagar.Rows
    '                If fila.Cells(0).Text = folioConsulta Then
    '                    CType(fila.FindControl("chk"), CheckBox).Checked = True
    '                    CType(fila.FindControl("chk"), CheckBox).Enabled = False
    '                    If bandera_facturaCredito = 1 Then
    '                        fila.BackColor = Color.Salmon
    '                    Else
    '                        fila.BackColor = Color.MediumTurquoise
    '                    End If

    '                End If
    '            Next
    '        End If
    '    Else
    '        LblMensajeAdvertencia.Text = clsDatos.MensajeError
    '        PanelAdvertencia.Visible = True
    '    End If
    'End Sub
    'Function consultasAfacturar(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
    '    Dim clsDatos As New ClaseDatos
    '    Dim strSQL As String
    '    Dim listaFac As String = ""
    '    Dim count As Integer
    '    Dim listaSQL As String = ""
    '    count = 0
    '    For Each row As GridViewRow In GdConsultasXpagar.Rows
    '        If CType(row.FindControl("chk"), CheckBox).Checked Then
    '            If count > 0 Then
    '                listaFac += ","
    '                listaSQL += ","
    '            End If
    '            listaFac += "'" + row.Cells(0).Text + "'"
    '            listaSQL += row.Cells(0).Text
    '            count = count + 1
    '        End If
    '    Next
    '    If listaFac = "" Then
    '        Return False
    '    Else
    '        strSQL = "select sum(cantidad) as cantidad,descripcion,cast((costo) AS decimal(16,2)) as costo FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
    '                "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 " & _
    '                "group by descripcion,costo"
    '        If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
    '            LblMensajeAdvertencia.Text = clsDatos.MensajeError
    '            PanelAdvertencia.Visible = True
    '        End If
    '        strSQL = "select CAST(SUM(costo*cantidad) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
    '                "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 "
    '        If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
    '            LblMensajeAdvertencia.Text = clsDatos.MensajeError
    '            PanelAdvertencia.Visible = True
    '        End If
    '        listaFolios = listaFac
    '        listaFoliosSQL = listaSQL
    '        Return True
    '    End If

    'End Function


End Class