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


Public Class facturasclientes
    Inherits System.Web.UI.Page
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        limpiarPaneles()
        If Not IsPostBack Then
            cargaFacturas()
            div_3.Visible = False
            div_1.Visible = False
            div_2.Visible = False
        End If
    End Sub

    Sub cargaFacturas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String


        strSQL = "SELECT TOP 5000 F.IDFactura,F.Serie,F.num_factura,(F.Serie+F.num_factura) AS Factura,F.rfc,F.importe,F.paciente, f.nombre,F.fecha," & _
        "F.estado,f.version, f.idCliente,f.idcosto " & _
        "FROM [" & clsDatos.BaseDatos & "].[dbo].[factura] as F  " & _
        "where serie='HO' and version='3.3' or version='4.0' ORDER BY F.IDFactura desc"



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
                                ''AB07012022
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
            ' GdFacturas.Columns(16).Visible = False
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
                                 "where serie='FC' and F.fechaFactura>='" & rangoFechas(0) & "' and F.fechaFactura<='" & rangoFechas(1) & "' " & _
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
                                "where serie='FC' and  F.fechaFactura>='" & rangoFechas(0) & "' and F.fechaFactura<='" & rangoFechas(1) & "' and F.estado='" & estadoFac & "' " & _
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
                                CType(factura.FindControl("Refacturar"), LinkButton).Visible = False
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
                    'Cambios AB22022022'
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
                            If IsDBNull(dt.Rows(0).Item("email")) Then
                                correo_confirmar.Value = "Sin_@_email.com"
                            Else
                                correo_confirmar.Value = dt.Rows(0).Item("email")
                            End If

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
    'Nuevo procedimiento AB06012022'
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
    ''AB22022022
    Sub CancelarFacturaMotivo()
        CancelarFactura()
        cargaFacturas()
    End Sub
    Sub OSRegresar()
        cargaFacturas()
        Response.Redirect("facturasclientes.aspx")
    End Sub
    ''AB22022022
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
        strsql = "select uuid,num_factura,serie,rfc,subtotal from Factura where idFactura='" & factura & "'"

        If clsDatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                Dim nfactura As String = dt.Rows(0).Item("num_factura")
                Dim sfactura As String = dt.Rows(0).Item("serie")
                Dim uuidCancelar() As String = {dt.Rows(0).Item("uuid")}
                Dim uuid As String = dt.Rows(0).Item("uuid")
                Dim rfc As String = dt.Rows(0).Item("rfc")
                Dim Total As String = dt.Rows(0).Item("subtotal")
                Dim ClaveMotivo As String = DdMotivos.SelectedValue


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


                'cancelacionFac = timbrarFac.CancelarCFDIConValidacion("MSI1603225B1", "uH9t%yfrR+", "FMD020730PQ5", listaArreglos.ToArray, "MIIMiQIBAzCCDE8GCSqGSIb3DQEHAaCCDEAEggw8MIIMODCCBu8GCSqGSIb3DQEHBqCCBuAwggbcAgEAMIIG1QYJKoZIhvcNAQcBMBwGCiqGSIb3DQEMAQYwDgQIdAgXrYuiICkCAggAgIIGqJG/DzvVs3kVrNH9lxQ/cJg8GMV5t2X7mV2Nh8eoiy586X9jSAzM33hoFyN4/VQVHmjz4d4zIoXLkiI5ObknkxRwLOMfT/wsIfspbwhH3P0SqJ8fa/2xUnNOKKw3SPoONwgLad5VyF32vnpszZ0mKO2pSFbWMA3Ao9OM4bUl7haoiRS3zDa8xYJAhMLsGOiY8fFVXbTNmGfxRQHnbAd14QoDwXu12mTFCz93XEPGi8JL2Dtghef4fc53QsA7km+apmXoULFZnlMamh+cbonWmZh9N1+voe/Cqh83Nx6jl2uzEkz30URQXARTjafd5rF3XrENv6rEy3acpJHCJWLVagH13ZjrV34LVElADgO7wi/l326ekkNd2Cz05DHDfrBbIvSSxIIuw55d/vFMM5SYhGSQ8Avt4vDmg76zdPM6curC6kTlLjeIQbTNh8aMpIDwzGU1PKIttII3QlmLXwSljlhIvFSo/fZjVWOX/p10GmVsZ588j58EQTt3ITXaB9JDegXYa8sqjizGIyOvSVtYeSDRuKoiqZtoXNv4FXECm4Nsdw31x3qSGBwzjdc+HuDIR2ErnYhgy66Pp16eowLN2ux0pgr6Ghg+ZLerzBOoIRyPqm8eTlbFSyYvjeVKSNkDG0/Yw0qQK/E83RrXGbuvG8rNZu0TGcoh5jpGcxC51Cr/JJF8AZtx7wlHS/OpejiFhn9+u2aTexYrA+6X736SReTowvdAvSiaOFHN7y5mswdf/Ya9T6M9O0Bz0BZS0iPaDpEgAOmX5MOw2orIMteDKpQ4tsS6BrjDMosCJGx+oXgQdmUBrwcqmFagRUFoUY6Tnqi06mIASHvqPfY7ByNaOx92qSbQsyDVyxgL8iM7DtHPoWq+HT2MUSFX75PqTR4pbL0xQamEoXiVPCl0mLwwsZM4PbXTGM2zI1iBKmxAxJ7ww8iMovpTtOGfw1Zwl50kShx6ukLcaqZaSyY1dV2tGqOCJeIASSt/CX9O/avbqNGw28j4zwxyi0kAY1F263a0Ll9r6mVlGL2xay48Y5NRp8c8DWJLgWEBVGHAXiZq1McHAm3eJsfjGN9FBmSkQzbGO7uLUYzrqmzVXX2NR1B0uUIUi+juzDYLwG76sF4yj3K019hO2Jol8kiSjjc+7VSBOMLc6cWbYLWCNIOjRJ5YawjRa4mnqgW2gHBsFigfDr7H9MzoQyKhGCdHZtoEMoto7g6OCoLJjetA1NkJYpVTBJxxf6un/q0MgnZij0755dRSyciZkEvg2/7zsc+ZC8q0GCeHV6G3Pfb+eTIUdf7+gWJ6OwrKSWHYE/MgAf+cC8XBuEN4OZHGoTQst1Ox5iuUw1kNN45UO6KbYug96YCYLb+ql4K/U9s8YjA3Eir/g2MeqZEWjSlkAUxobEDnc8yXS6yRwD7MCQjd+AfLaFmXJzbtvmsWNMsCUTbv6ZwZGqR+/YobRaGXyPbLYyZlsZdSY9B42omgzoJs1edUbZ2ibvp785b4EEyqzMG5lQ8mKTVLjbVMExy6iNGK+FYcK+0K6fHkUt/JBGWcx6fqpt2HNBNc5lX96lNgScHkUnsKj+SaEtKOhJgR9RL856OviksvxsIrhbvG/ZdI2AchM8zDTnb73O6+R/qOXQx2z/mVddZ7s1hyrCJwCw2VrBOiaK+cX5DBzT4pS53DPJq25PXRfDwOu/NyRlApneW5yU6s3JCJ6B4FomcuXuEw7tcimi3Z0nCTIHfdsxJYyqRHEIcC1CSZZYcIdQ02c3LOB90nWwY4tI7Mlu0zvGa+0XpkXrPyTnjdMs76b/nZCekFGWLoJ1MDECH+vfbJTCCbdC/Tk/HQDrjSApfDTSL7VLwDlr7U+SmlwciFtOysFRLalL69mUYM1gOgT76MRwQY+3D/KSiGMzSZezCjiLhTm1OgBD0Q1EWnJe/F7a1ObyHVlkyYv6ztHJodTrZtMWL0doIyIBM7KQwelpKjyfnlPkLTq/q6Ln10FZe1DDRHvPhREf48AvH22EhdH/MD3/NWhTOnP47E8hRnlvzSyMMCXuKhPoVOvtV+9mqAzZDaRXs7FLgWamAtdMPucJtBQeRLY3n4oQWrVUE0ERKlK7verQsyt3CIbnwzVonJZk2VyDMGMrvj3legxUeq8bxQNNmhXMw1gacqa1E6S1G870bDTWANxx3YC83D5WknBfXkmdr2FvaldxpxgdXa58ek+QCUM8j9LNC7yPpy24yw2kzUS+KkMhrEt0g6eVUlg+mDe+GNCMNSbQ/V4IsHJ0qvqDCCBUEGCSqGSIb3DQEHAaCCBTIEggUuMIIFKjCCBSYGCyqGSIb3DQEMCgECoIIE7jCCBOowHAYKKoZIhvcNAQwBAzAOBAigB++kdN8ysAICCAAEggTIh0E9lWDF5z6TTNyHIAi19ROKL34Z1hC7Hd1lsQql5L6w6HQqDjonkpEFwO2DhUgVobNct8S0Xl0P5VY2I2M53pyHhALjVSwAiQow0ctcd9MUPfcqgunQg7zjqqn6RLZiz416vQ0uszFnurY/Tt0oSxpPEePYu/+SRINwpPZNOIgbERAQdX/mobjIH49M3kX10QX91XGh4YZUQtoF1VScHJrof9am+jGgMdvJuaEjqOOkykc69/gw+1JpegA4vJHY+FIbImzHEDDLYePYvMc3GTBUK3u+BaWPCqj/UBhrn98vQKPLh5FCm6+Osa2Szg9h3bG8q5x/XeFP/XjsP+0op9T3Nirs8FF+eoWl/8Cc2T7dRpGDYVU+0f595N9Wu92gWrShg/3mjqzJ/4sSvGO0pULSaahGOQMzByVKLaWXwD5VDn/Ku1TLiw8oIAJjc++/GfNBZOh7Co+KhMJ1Cuq86B4PYT/6KCzcXFuGkd3v08ihCUWoqkMYkK4RaEFYpCeV8Sm/WnE8RyRhFVcfBHYnzfqfyBS8fWOyNILkYP611J7So4Dio1tGPh/g47zyNrn4dZMCvO47j5bR4V8sb+v3KJ9YFuwAI/9hkihOPAvEaghxdOEqBuvgheF3gcfITjSWpYIiihgwfYLApxNE8oe20Zf+RDxSdDHcqtr6j1BLNgWzoIaOJoolTfK9RiBFeM3YQer0885DDR7j5zyk/Se+pIlcWLy28aN1e3eCsO5KG50RQW0qa959jwTd6ToBwGlFc5RVr4R0VckA0NA43N9YK1AyojkWZE5sjfBAQ+n1a1Y/vahKAhNA860yq7FxumxM8ehjm7B6XI8YVcw6C0pCxgpSMog0UZSBG283sOxaHu3X9r3dAOqLE8etxPqJxATZx+lZsS2M9HR4cjpN7vLnBSW62GaO5xD7tv5lAC9B58SVfzaGmXQwVpEFEX/RQIo2PBU3IC3V5kTmZgxFIXnysABFz8MuJiHnmqp4Mn8mo+8/9B0wYnZ1E897ZKYTDqyrSue5dYJFqCxf+80EzT85G9Q/TnzRQlX7e3fxSFglwtU69avMTN0AnH/7tH5Bd+Ua+Et5zR+t8Lsu5Eg178gWlOHwWQNYOb4h1c/JhXYuuP0/Ai4zlgkE0kcshncc6VUPkB0qmPHWO78GtiXnsII/uDgmea50iKxWcD2k3w1oTPf21Re8xc2TNvvuUMRb9GMshjP97jUJRBkSfymoBZh/gMiEfChw/U4IpNeZIy4R9rJAHWzkO3m7C0jKDQ51ZWThNH0kyqDk83sX3GXznkaGdZTsRrelzieodC0CSU0TsrRhM+VSihb78vTBIhZwqM5FGxb+2vATssT83BUjlUJoTEKYIlh8yeQsWBoFKk3u9O9+LnqeWWxkU3vN9Xuun6IGt81JQ59iO9CBxXAGmtyc4FBzc58/8Zb+3iQlBWcCMsnxJp2Xj0UJ2xv6nqHqF2ID8u8obV0gA+J8tSUtUrHkORtdT6XP3bKPGmcmCVRFMoLIyH9B4yWTchnwGPOeomMV/toYmOMuKNgg2LIDPn+CtX8u41G4UQv/P8h973d0hKmV/4AxVe5plTSCXuvq99teeYoFZj75/51xVduOutoC3EBVlGPwuy6jMSUwIwYJKoZIhvcNAQkVMRYEFFIncJQB0w19RnzxeuKFIdUE55bcMDEwITAJBgUrDgMCGgUABBQMFIUmm/CvYIhaj27sEOmN1lH76gQIMuYXVvvzHI4CAggA", "fmdscp2013")
                cancelacionFac = timbrarFac.CancelarCFDI("MSI1603225B1", "uH9t%yfrR+", "FMD020730PQ5", listaArreglos.ToArray, "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", "fmdscp2025")

                For Each UUIDT As WSCFDI33.DetalleCancelacion In cancelacionFac.DetallesCancelacion

                    LblMensajeAdvertencia.Text += UUIDT.CodigoResultado + vbNewLine
                    LblMensajeAdvertencia.Text += UUIDT.MensajeResultado + vbNewLine
                    LblMensajeAdvertencia.Text += UUIDT.UUID + vbNewLine
                    LblMensajeAdvertencia.Text += "Favor de Verificar!"
                    PanelAdvertencia.Visible = True
                    PanelAdvertencia.Focus()
                Next

                ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#MODAvisos').modal('show');</script>", False)

                If cancelacionFac.OperacionExitosa = True Then

                    RespuestaCancelacionDetallada_FEL = cancelacionFac.DetallesCancelacion.ToList()

                    Select Case cancelacionFac.DetallesCancelacion(0).CodigoResultado

                        Case "201"

                            '''''' Guarda la Cancelacion y  '''''' Actualiza el estado
                            strsql = "update abonos set facturado='false', num_factura=NULL where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                            strsql += "update factura set capcuenta='false', cancelada='true', sustituidoX='0', estado = 'Cancelada', clave_motivo_cancelacion='" & ClaveMotivo & "' where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                            clsDatos.cargaComando(strsql)
                            If clsDatos.ejecutar() = 0 Then
                            Else
                                LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                PanelAdvertencia.Visible = True
                            End If


                            Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                            acusexml = timbrarFac.ObtenerAcuseCancelacion("MSI1603225B1", "uH9t%yfrR+", uuidCancelar(0).ToString)
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
                    '''''' Guarda la Cancelacion y  '''''' Actualiza el estado
                    strsql = "update abonos set facturado='false', num_factura=NULL where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                    strsql += "update factura set capcuenta='false', cancelada='true', sustituidoX='0', estado = 'Cancelada', clave_motivo_cancelacion='" & ClaveMotivo & "' where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                    clsDatos.cargaComando(strsql)
                    If clsDatos.ejecutar() = 0 Then
                    Else
                        LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                        PanelAdvertencia.Visible = True
                    End If


                    Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                    acusexml = timbrarFac.ObtenerAcuseCancelacion("MSI1603225B1", "uH9t%yfrR+", uuidCancelar(0).ToString)
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
                    cargaFacturas()
                End If
                Exit Sub
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
        strsql = "select uuid,num_factura,serie,rfc,subtotal from Factura where idFactura='" & factura & "'"

        If clsDatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                Dim nfactura As String = dt.Rows(0).Item("num_factura")
                Dim sfactura As String = dt.Rows(0).Item("serie")
                Dim uuidCancelar() As String = {dt.Rows(0).Item("uuid")}
                Dim uuid As String = dt.Rows(0).Item("uuid")
                Dim rfc As String = dt.Rows(0).Item("rfc")
                Dim Total As String = dt.Rows(0).Item("subtotal")


                Dim timbrarFac As New WSCFDI33.WSCFDI33Client
                Dim cancelacionFac As New RespuestaCancelacion

                Dim listaArreglos As List(Of WSCFDI33.DetalleCFDICancelacion) = New List(Of WSCFDI33.DetalleCFDICancelacion)()

                Dim Arreglo As WSCFDI33.DetalleCFDICancelacion = New WSCFDI33.DetalleCFDICancelacion

                Dim RespuestaCancelacionDetallada_FEL As New List(Of AgeMED.WSCFDI33.DetalleCancelacion)

                Arreglo.RFCReceptor = rfc
                Arreglo.Total = Total
                Arreglo.UUID = uuid

                listaArreglos.Add(Arreglo)


                'cancelacionFac = timbrarFac.CancelarCFDIConValidacion("MSI1603225B1", "uH9t%yfrR+", "FMD020730PQ5", listaArreglos.ToArray, "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", "fmdscp2013")
                cancelacionFac = timbrarFac.CancelarCFDIConValidacion("MSI1603225B1", "uH9t%yfrR+", "FMD020730PQ5", listaArreglos.ToArray, "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", "fmdscp2025")

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

                            '''''' Guarda la Cancelacion y  '''''' Actualiza el estado
                            strsql = "update abonos set facturado='false', num_factura=NULL where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                            strsql += "update factura set capcuenta='false', cancelada='true', sustituidoX='0', estado = 'Cancelada' where num_factura='" & nfactura & "' and serie='" & sfactura & "'"
                            clsDatos.cargaComando(strsql)
                            If clsDatos.ejecutar() = 0 Then
                            Else
                                LblMensajeAdvertencia.Text = "Error :" + clsDatos.MensajeError
                                PanelAdvertencia.Visible = True
                            End If


                            Dim acusexml As New WSCFDI33.RespuestaTFD33 ''2015
                            acusexml = timbrarFac.ObtenerAcuseCancelacion("MSI1603225B1", "uH9t%yfrR+", uuidCancelar(0).ToString)
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

    End Sub
    Protected Sub BtnNoGFac_Click(sender As Object, e As EventArgs)
        cargaFacturas()
    End Sub

    Sub enviarFac()
        Dim folioFactura As String = folio_factura.Value
        Dim seriefactura As String = serie_factura.value
        Dim estadoFac As String = estado_factura.Value
        limpiarPaneles()
        Dim Path1 As String
        Dim Path2 As String
        Dim mail As New MailMessage
        If estadoFac = "Vigente" Then
            Dim folioFacturas As String = seriefactura + folioFactura
            Path1 = Server.MapPath("/Facturas/XMLT/" + folioFacturas)
            Path2 = Server.MapPath("/Facturas/PDFS/" + folioFacturas)
        Else
            Path1 = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
        End If

        mail.From = New MailAddress("facturacion@fisiocare.com.mx")

        mail.To.Add(correo_confirmar.Value)

        mail.Subject = "Factura de servicios de Fisiocare"
        mail.Body = "Facturas"
        Try

            If estadoFac = "Vigente" Then
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
                cargaFacturas()
                LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
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
                cargaFacturas()
                LblMensajeAviso.Text = "Se ha enviado el archivo de cancelacion de la factura (xml)"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
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
            path = Server.MapPath("/Facturas/XMLT/" + serie + folioFactura)

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
            path = Server.MapPath("/Facturas/PDFS/" + serie + folioFactura)
            Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & serie + folioFactura & ".pdf');</script>")

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
    Sub CargarDatosFacturacion(CodigoPaciente)
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        input_razon_social.Value = ""
        rfc.Value = ""
        correo.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""
        codigo_paciente.Value = CodigoPaciente
        strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                input_razon_social.Value = dt.Rows(0).Item("razonsocial")
                rfc.Value = dt.Rows(0).Item("rfc")
                correo.Value = dt.Rows(0).Item("email")
                direccion.Value = dt.Rows(0).Item("direccion")
                cp.Value = dt.Rows(0).Item("cp")
                ciudad.Value = dt.Rows(0).Item("ciudad")
                estado.Value = dt.Rows(0).Item("estado")
                pais.Value = dt.Rows(0).Item("pais")
                codigo_paciente.Value = dt.Rows(0).Item("CodigoPaciente")
                id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
            End If
        End If
    End Sub

    Sub LlenarComboRazonSocial(CodigoPaciente)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        codigo_paciente.Value = CodigoPaciente
        strSQL = "SELECT idRazonSocial, razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"
        If funciones.llenadropdown(strSQL, select_razon_social) = -1 Then
            'Error
            div_input_razon_social.Visible = True
        Else
            'Se realizó la función, verificar si hay datos
            If select_razon_social.Items.Count = 0 Then
                div_input_razon_social.Visible = True
                div_select_razon_social.Visible = False
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
        input_razon_social.Value = ""
        rfc.Value = ""
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
            pais.Value = "MÉXICO"
            estado.Value = "YUCATÁN"
            ciudad.Value = "MÉRIDA"
            'Habilitar bandera para dar de alta el nuevo
            bandera.Value = 1
        Else
            strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE idRazonSocial =" + select_razon_social.SelectedValue
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    input_razon_social.Value = dt.Rows(0).Item("razonsocial")
                    rfc.Value = dt.Rows(0).Item("rfc")
                    correo.Value = dt.Rows(0).Item("email")
                    direccion.Value = dt.Rows(0).Item("direccion")
                    cp.Value = dt.Rows(0).Item("cp")
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                    estado.Value = dt.Rows(0).Item("estado")
                    pais.Value = dt.Rows(0).Item("pais")
                    id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
                End If
            End If
        End If
    End Sub

    Sub Facturar()


        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If bandera.Value = 1 Then
            'Dar de alta la razón social
            strsql = "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] (CodigoPaciente, razonSocial, rfc, email, direccion, cp, pais, estado, ciudad )VALUES('" & codigo_paciente.Value & "', '" & input_razon_social.Value & "', '" & rfc.Value & "', '" & correo.Value & "', '" & direccion.Value & "', '" & cp.Value & "', '" & pais.Value & "', '" & estado.Value & "', '" & ciudad.Value & "'  )"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                strsql = "SELECT idRazonSocial, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] " & _
                          "WHERE CodigoPaciente = '" & codigo_paciente.Value & "' and razonSocial='" & input_razon_social.Value & "' and rfc='" & rfc.Value & "'  and email='" & correo.Value & "' "
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
                    'Timbrado()
                    cargaFacturas()
                    LblMensajeAviso.Text = "Refacturacion"
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                End If

            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        Else
            'Actualizar la razón social
            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] SET razonSocial='" & input_razon_social.Value & "', rfc='" & rfc.Value & "', email='" & correo.Value & "', direccion='" & direccion.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', estado='" & estado.Value & "', ciudad= '" & ciudad.Value & "' WHERE idRazonSocial = " + id_razon_social.Value
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                'Timbrado()
                cargaFacturas()
                LblMensajeAviso.Text = "Refacturacion"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                'cargaAgenda()
                'UpdatePanel1.Update()
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
        cargaFacturas()
    End Sub


    ''********* TIMBRADO

    'Function Timbrado()
    '    Dim banderaError As Boolean = False
    '    ' Dim fechapdf As String
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String
    '    Dim folio As String = ""
    '    Dim serie As String = ""
    '    Dim cuenta As Int32 = 0
    '    Dim contador As Int32 = 0


    '    strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from ConsecutivoFac CodigoCF='1'"

    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            folio = dt.Rows(0).Item("consecutivo")
    '            serie = dt.Rows(0).Item("serie")
    '        Else
    '            LblMensajeCritico.Text = "Error en el consecutivo de folio"
    '            PanelCritico.Visible = True
    '            PanelCritico.Focus()
    '        End If
    '    End If


    '    '********************** FACTURACION ******************************

    '    '*************  -DATOS DEL EMISOR- ******************
    '    Dim emisor As New Emisor()
    '    emisor.rfc = "OST141127IEA"
    '    emisor.nombre = "ORTOPEDIA STAR, S.C.P."
    '    emisor.domicilioFiscal = New DomicilioFiscal
    '    emisor.domicilioFiscal.calle = "26"
    '    emisor.domicilioFiscal.noExterior = "299"
    '    emisor.domicilioFiscal.noInterior = "928"
    '    emisor.domicilioFiscal.colonia = "FRACC. ALTABRISA"
    '    emisor.domicilioFiscal.localidad = "MÉRIDA"
    '    emisor.domicilioFiscal.municipio = "MÉRIDA"
    '    emisor.domicilioFiscal.estado = "YUCATÁN"
    '    emisor.domicilioFiscal.pais = "MEXICO"
    '    emisor.domicilioFiscal.codigoPostal = "97133"
    '    emisor.regimenFiscal = New RegimenFiscal
    '    emisor.regimenFiscal.regimen = "REGIMEN GENERAL DE LEY PERSONAS MORALES"

    '    '************* - DATOS DEL RECEPTOR- ****************
    '    Dim receptor As New Receptor()
    '    receptor.rfc = rfc.Value
    '    receptor.nombre = input_razon_social.Value
    '    receptor.domicilio = New Domicilio()
    '    receptor.domicilio.calle = direccion.Value
    '    receptor.domicilio.localidad = ciudad.Value
    '    receptor.domicilio.codigoPostal = cp.Value
    '    receptor.domicilio.municipio = ciudad.Value
    '    receptor.domicilio.estado = estado.Value
    '    receptor.domicilio.pais = pais.Value

    '    '************** -DATOS DEL COMPROBANTE- ************
    '    Dim hoy As DateTime = DateTime.Now
    '    Dim comp As New Comprobante()
    '    Dim total, formaPago As String
    '    Dim variasFacturas As Boolean
    '    Dim dtConceptos, dtTotal As DataTable
    '    Dim listafolios, listaFoliosSQL As String


    '    formaPago = ""
    '    total = ""
    '    '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
    '    cargardatosFac(formaPago, total)
    '    If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
    '        total = dtTotal.Rows(0).Item("costoTotal")
    '        variasFacturas = True
    '    Else
    '        variasFacturas = False
    '    End If

    '    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '    comp.fecha = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))
    '    fechapdf = comp.fecha
    '    comp.folio = folio
    '    comp.serie = serie
    '    comp.formaDePago = "PAGO EN UNA SOLA EXHIBICION"
    '    comp.subTotal = total
    '    comp.total = total
    '    comp.tipoDeComprobante = "ingreso"
    '    comp.moneda = "MXP"
    '    comp.tipoCambio = "1.0"
    '    comp.metodoDePago = formaPago
    '    comp.lugarExpedicion = "MÉRIDA,YUCATÁN"

    '    '************ -CONCEPTO- *******************

    '    comp.conceptos = New List(Of Concepto)()
    '    Do While contador = cuenta

    '        If variasFacturas = True Then

    '            For Each fila As DataRow In dtConceptos.Rows
    '                Dim concepto1 As New Concepto()
    '                concepto1.cantidad = fila.Item("cantidad")
    '                concepto1.unidad = "No aplica"
    '                concepto1.noIdentificacion = "noIdentificacion"
    '                concepto1.descripcion = fila.Item("descripcion")
    '                concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
    '                concepto1.valorUnitario = fila.Item("costo")
    '                comp.conceptos.Add(concepto1)

    '            Next

    '        Else
    '            strsql = " SELECT codigoConcepto,descripcion,convert(varchar,convert(decimal(8,2),costo)) as costo,cantidad FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
    '           "WHERE folioConsulta='" & fconsulta.Value & "' AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1"
    '            If clsdatos.cargatabla(strsql, dt) = 0 Then
    '                For Each fila As DataRow In dt.Rows
    '                    Dim concepto1 As New Concepto()
    '                    concepto1.cantidad = fila.Item("cantidad")
    '                    concepto1.unidad = "No aplica"
    '                    concepto1.noIdentificacion = "noIdentificacion"
    '                    concepto1.descripcion = fila.Item("descripcion")
    '                    concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
    '                    concepto1.valorUnitario = fila.Item("costo")
    '                    comp.conceptos.Add(concepto1)

    '                Next
    '            Else
    '                'Error en la consulta
    '            End If
    '        End If

    '        contador = contador + 1
    '    Loop

    '    '*************** - CANTIDAD EN LETRAS- *****************
    '    Dim funciones2 As New FuncionesGenerales

    '    Dim tletras As String = " "



    '    tletras = funciones2.monto(Convert.ToDouble(comp.total))

    '    '*************** - IMPUESTOS- *****************
    '    Dim iva As New Traslado()
    '    iva.impuesto = "IVA"
    '    iva.importe = "0.0000"
    '    iva.tasa = "0"

    '    Dim impuestos As New Impuestos()
    '    impuestos.traslados = New List(Of Traslado)()
    '    impuestos.totalImpuestosTrasladados = "0.0000"
    '    impuestos.totalImpuestosRetenidos = "0.0000"
    '    impuestos.traslados.Add(iva)




    '    '************************** TIMBRADO Y RESPUESTA****************************
    '    'Envio al Web Service

    '    comp.emisor = emisor
    '    comp.receptor = receptor
    '    comp.impuestos = impuestos

    '    Dim Xml
    '    Dim cer
    '    Try
    '        cer = New X509Certificate2(Server.MapPath("/OST14112729122014.pfx"), "orto2014", X509KeyStorageFlags.MachineKeySet)
    '        Xml = CFDIv32.Serializar(comp, False)

    '    Catch ex As Exception
    '        banderaError = True
    '        LblMensajeCritico.Text = "Error en el sellado del XML para timbrar favor de llamar a informática " + vbNewLine + ex.Message.ToString
    '        PanelCritico.Visible = True
    '        PanelCritico.Focus()
    '    End Try

    '    If banderaError = False Then
    '        Dim cadenaOriginal = CFDIv32.CadenaOriginalSellado(Xml)

    '        Try
    '            Dim ar = cer.GetSerialNumber
    '            Array.Reverse(ar)
    '            comp.noCertificado = Encoding.UTF8.GetString(ar)
    '            comp.certificado = Convert.ToBase64String(cer.RawData) 'Certificado igual para la cancelación del cfdi
    '            comp.sello = CFDIv32.Sello(cadenaOriginal, cer)

    '            Xml = CFDIv32.Serializar(comp, True)
    '            CFDIv32.Validar(Xml, True)
    '            File.WriteAllText(Server.MapPath("/mandatorioSGO.xml"), Xml)
    '        Catch ex As Exception
    '            banderaError = True
    '            LblMensajeCritico.Text = "Error en el XML para timbrar favor de llamar a informática" + ex.Message.ToString
    '            PanelCritico.Visible = True
    '            PanelCritico.Focus()
    '        End Try

    '        Dim uuid, sellocfd, nocertificadoSat, selloSat, FechaTimbrado As String
    '        If banderaError = False Then
    '            Try

    '                Dim timbrarFac As New AgeMED.WSFEL.WSTFDClient
    '                Dim timbreFac As New RespuestaTFD



    '                timbreFac = timbrarFac.TimbrarCFDI("OST141127IEA", "kBMEtFvuTrr#", Xml, "agemed_" + comp.folio.ToString)


    '                Xml = timbreFac.XMLResultado

    '                If timbreFac.MensajeError.Trim = "" And timbreFac.OperacionExitosa = True Then
    '                    CFDIv32.Validar(Xml, True)
    '                    uuid = timbreFac.Timbre.UUID
    '                    sellocfd = timbreFac.Timbre.SelloCFD
    '                    nocertificadoSat = timbreFac.Timbre.NumeroCertificadoSAT
    '                    selloSat = timbreFac.Timbre.SelloSAT
    '                    FechaTimbrado = timbreFac.Timbre.FechaTimbrado


    '                    '''''' Guarda la factura
    '                    Dim foliosConsulta As String
    '                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
    '                    If variasFacturas = True Then
    '                        foliosConsulta = listaFoliosSQL
    '                    Else

    '                        foliosConsulta = "'" + fconsulta.Value + "'"
    '                    End If

    '                    '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    '                    strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[IngresosCaja] set facturado=1 " & _
    '                            " where folioconsulta in (" & listafolios & ")  "
    '                    strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[DetFacturas] (FolioFactura,Serie,rfc,TipoFactura,"
    '                    strsql += vbNewLine & " OrigenFactura,subtotal,ImporteIva,Total ,"
    '                    strsql += vbNewLine & " IdRazonSocial,CodigoPaciente,Status,fechaFactura,xml,uuid,numerocertificadosat,sellocfd,sellosat,fechatimbrado,estado,IdEmpleado,codigoEmpresa,FolioConsulta,tipoDePago,conciliado,Saldo) VALUES  "
    '                    strsql += vbNewLine & " ('" & folio & "','" & serie & "','" & rfc.Value & "', '1', '1','" & comp.subTotal & "',"
    '                    strsql += vbNewLine & " '" & iva.importe & "','" & comp.total & "','" & id_razon_social.Value & "','" & codigo_paciente.Value & "','1', getdate() , '" & Xml & "', '" & uuid & "',"
    '                    strsql += vbNewLine & " '" & nocertificadoSat & "','" & sellocfd & "','" & selloSat & "','" & FechaTimbrado & "','" & timbreFac.Timbre.Estado & "','" & Session("codigoUsuario") & "','" & Session("codigoEmpresa") & "','" & foliosConsulta & "'," & formaPago & ",'0','" & comp.total & "')"
    '                    clsdatos.cargaComando(strsql)
    '                    If clsdatos.ejecutar() <> 0 Then
    '                        LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
    '                        PanelAdvertencia.Visible = True
    '                    End If

    '                    '''''' Actualiza el consecutivo 

    '                    strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where CodigoCF in (1,2) "
    '                    clsdatos.cargaComando(strsql)
    '                    clsdatos.ejecutar()


    '                Else
    '                    folio = ""
    '                    banderaError = True
    '                    LblMensajeCritico.Text = "No cerrar; Error en el XML de timbrado favor de llamar a informática; " + timbreFac.MensajeErrorDetallado.ToString.Trim
    '                    PanelCritico.Visible = True
    '                    PanelCritico.Focus()
    '                End If
    '            Catch ex As Exception
    '                banderaError = True
    '                LblMensajeCritico.Text = "Error en el timbrado favor de llamar a informática " + ex.Message.ToString
    '                PanelCritico.Visible = True
    '                PanelCritico.Focus()
    '            End Try
    '        End If

    '        Dim nameFile As String = ""
    '        If banderaError = False Then
    '            Try
    '                Dim doc As XmlDocument = New XmlDocument()
    '                doc.LoadXml(Xml)
    '                nameFile = (Server.MapPath("/Facturas/" + comp.folio.ToString + ".xml"))
    '                doc.Save(nameFile)
    '                CFDIv32.Validar(Xml, True)
    '            Catch ex As Exception
    '                banderaError = True
    '                LblMensajeCritico.Text = "Eror en el guradado del XML favor de llamar a informática"
    '                PanelCritico.Visible = True
    '                PanelCritico.Focus()
    '            End Try
    '        End If

    '        If banderaError = False Then
    '            Try
    '                Dim cadenaOriComplemento As String = CFDIv32.CadenaOriginalImpresa(Xml)

    '                Dim QRCodeEncoder As New QRCodeEncoder
    '                QRCodeEncoder.QRCodeScale = "3"
    '                QRCodeEncoder.QRCodeVersion = "7"
    '                QRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M

    '                Dim imagen As Bitmap
    '                Dim auxcad = Split(comp.total, ".")
    '                Dim enteros As String = auxcad(0).ToString.Trim.PadLeft(10, "0")
    '                Dim decimales As String = auxcad(1).ToString.Trim.PadRight(6, "0")
    '                Dim datos As String = "?re=" + emisor.rfc.Trim + "&rr=" + receptor.rfc.Trim + "&tt=" + enteros + "." + decimales + "&id=" + uuid
    '                imagen = QRCodeEncoder.Encode(datos)
    '                imagen.Save(Server.MapPath("/Facturas/" + comp.folio.ToString + ".jpeg"), Imaging.ImageFormat.Jpeg)

    '                imprimirFacturaCfdi(listafolios.ToString, comp.folio.ToString, comp.serie.ToString, receptor.domicilio.calle.ToString, receptor.domicilio.municipio, receptor.domicilio.codigoPostal, receptor.domicilio.localidad, receptor.domicilio.estado, comp.total, tletras, comp.formaDePago, comp.metodoDePago, comp.subTotal, iva.importe, FechaTimbrado, comp.noCertificado.ToString, nocertificadoSat, uuid, cadenaOriComplemento, selloSat, sellocfd)



    '                enviarFacr(comp.folio.ToString)

    '                cargaFacturas()


    '                VisualizarFac(comp.folio.ToString)



    '                'LblMensajeAviso.Text = "Factura enviada "
    '                'PanelAvisos.Visible = True
    '                'PanelAvisos.Focus()
    '                'Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & comp.folio.ToString & ".pdf');</script>")


    '            Catch ex As Exception
    '                LblMensajeAdvertencia.Text = "Error en la impresión del PDF llamar a informática " + ex.Message.ToString
    '                PanelAdvertencia.Visible = True
    '                'PanelAdvertencia.Focus()

    '            End Try

    '        End If
    '    End If

    '    Return banderaError
    'End Function
    'Sub VisualizarFac(ByRef folioFac As String)

    '    Dim folioFactura As String = folioFac

    '    Dim path As String

    '    path = Server.MapPath("/Facturas/" + folioFactura)

    '    Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc= " & folioFactura & " .pdf');</script>")

    'End Sub

    'Sub imprimirFacturaCfdi(ByVal lfolioconsulta As String, ByVal queimprimo As String, ByVal serie As String, ByVal domicilio As String, ByVal municipio As String, ByVal cp As String, ByVal localidad As String, ByVal estado As String, ByVal totalfac As String, ByVal totalletras As String, ByVal formadepago As String, ByVal metodopago As String, ByVal subtotalfac As String, ByVal iva As String, ByVal fechacer As String, ByVal ceremisor As String, ByVal cersat As String, ByVal uuid As String, ByVal cadoriginal As String, ByVal sellosat As String, ByVal selloCFD As String)
    '    Dim mireporte As New ReportDocument
    '    Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
    '    Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
    '    Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String


    '    mireporte.Load(Server.MapPath("/Facturas/Reporte/FacturaCFDI.rpt"))


    '    '++++++++++++++++++++ Detalles de la Factura

    '    Dim columna As New DataColumn("cantidad")
    '    dt.Columns.Add(columna)
    '    Dim col2 As New DataColumn("descripcion")
    '    dt.Columns.Add(col2)
    '    Dim col3 As New DataColumn("importe")
    '    dt.Columns.Add(col3)
    '    Dim col4 As New DataColumn("precio")
    '    dt.Columns.Add(col4)
    '    dt.Columns.Add(New DataColumn("img", GetType(Byte())))


    '    Dim fs As FileStream = New FileStream(Server.MapPath("/Facturas/" + queimprimo.ToString + ".jpeg"), FileMode.Open)
    '    Dim br As BinaryReader = New BinaryReader(fs)
    '    Dim imagen(CInt(fs.Length)) As Byte

    '    br.Read(imagen, 0, CInt(fs.Length))
    '    br.Close()
    '    fs.Close()

    '    Dim variasFacturas As Boolean
    '    Dim dtConceptos, dtTotal As DataTable
    '    Dim listafolios, listaFoliosSQL As String
    '    Dim contador As Int32 = 0
    '    Dim cuenta As Int32 = 0

    '    If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
    '        variasFacturas = True
    '    Else
    '        variasFacturas = False
    '    End If

    '    Do While contador = cuenta

    '        If variasFacturas = True Then

    '            For Each fila As DataRow In dtConceptos.Rows
    '                Dim concepto1 As New Concepto()
    '                Dim nFila As DataRow
    '                nFila = dt.NewRow
    '                nFila(0) = fila.Item("cantidad")
    '                nFila(1) = fila.Item("descripcion")
    '                nFila(2) = fila.Item("costo")
    '                nFila(3) = fila.Item("costo") * fila.Item("cantidad")
    '                nFila(4) = imagen
    '                dt.Rows.Add(nFila)
    '            Next

    '        End If

    '        contador = contador + 1
    '    Loop

    '    mireporte.SetDataSource(dt.DefaultView)


    '    ' -------------- datos folio/serie factura
    '    Mivar.Value = serie + " " + queimprimo
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("recibo").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    '--------------datos facturacion
    '    Mivar.Value = input_razon_social.Value
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("nombre").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = domicilio
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("domicilio").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    'Mivar.Value = colonia
    '    'rpDatos.Add(Mivar)
    '    'mireporte.DataDefinition.ParameterFields("colonia").ApplyCurrentValues(rpDatos)
    '    'rpDatos.Clear()

    '    Mivar.Value = municipio
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("municipio").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = cp
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("cp").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = localidad
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("localidad").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = estado
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("estado").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = pais.Value
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("paisfac").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = rfc.Value
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("rfc").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = Session("nombrePaciente")
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = fechapdf
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()


    '    strsql = "  select descripcion " & _
    '              " from  CatFormasDePago" & _
    '              " where codigoFormaPago = '" & metodopago & "'"

    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            Mivar.Value = dt.Rows(0).Item("descripcion")
    '            rpDatos.Add(Mivar)
    '            mireporte.DataDefinition.ParameterFields("formapago").ApplyCurrentValues(rpDatos)
    '            rpDatos.Clear()
    '        Else
    '            LblMensajeCritico.Text = clsdatos.MensajeError
    '            PanelCritico.Visible = True
    '        End If

    '    End If


    '    strsql = "  select referencia " & _
    '             " from  IngresosPagosCajas" & _
    '             " where folioConsulta =  " & lfolioconsulta & " "

    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            If dt.Rows(0).Item("referencia") = "0" Then
    '                Mivar.Value = ""
    '                rpDatos.Add(Mivar)
    '                mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    '                rpDatos.Clear()
    '            Else
    '                Mivar.Value = dt.Rows(0).Item("referencia")
    '                rpDatos.Add(Mivar)
    '                mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    '                rpDatos.Clear()
    '            End If
    '        Else
    '            Mivar.Value = ""
    '            rpDatos.Add(Mivar)
    '            mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
    '            rpDatos.Clear()
    '        End If

    '    End If

    '    '-----Observaciones de la Factura

    '    Mivar.Value = observacionesf.Value
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("observacionesfac").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()


    '    Mivar.Value = metodopago
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("ClaveMP").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    '-------------------------importes
    '    Mivar.Value = subtotalfac
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("importe").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = iva
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("iva").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = totalfac
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = totalletras
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("cantidadletras").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()


    '    Mivar.Value = fechacer
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("fechacer").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = ceremisor
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("ceremisor").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = cersat
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("cersat").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = uuid
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("uuid").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = cadoriginal
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("cadenaoriginal").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = sellosat
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("sellosat").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    Mivar.Value = selloCFD
    '    rpDatos.Add(Mivar)
    '    mireporte.DataDefinition.ParameterFields("selloCFD").ApplyCurrentValues(rpDatos)
    '    rpDatos.Clear()

    '    '--------------------------------------------------


    '    Try
    '        Dim nomArchivo As String = queimprimo.ToString.Trim
    '        nomArchivo.Replace(" ", "")
    '        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    '        Dim o As CrystalDecisions.Shared.ExportOptions
    '        o = New CrystalDecisions.Shared.ExportOptions
    '        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    '        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '        filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
    '        o.ExportDestinationOptions = filedest.Clone
    '        mireporte.Export(o)
    '        filedest = Nothing
    '        o = Nothing
    '        mireporte.Close()
    '        'Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & nomArchivo & ".pdf');</script>")
    '    Catch ex As Exception
    '        LblMensajeCritico.Text = "Error 1: " + clsdatos.MensajeError
    '        PanelCritico.Visible = True
    '    End Try

    '    'Dim tipoArchivo As String = "pdf"
    '    ' descargarArchivofac(queimprimo, tipoArchivo)

    '    'Dim path As String

    '    'path = Server.MapPath("/Facturas/" + queimprimo.ToString.Trim)


    '    'Try
    '    '    'Limpiamos la salida
    '    '    Response.Clear()
    '    '    'Con esto le decimos al browser que la salida sera descargable
    '    '    Response.ContentType = "application/octet-stream"
    '    '    'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
    '    '    Response.AddHeader("Content-Disposition", "attachment; filename=" + queimprimo.ToString.Trim + ".pdf")
    '    '    '(Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".pdf"))
    '    '    ' Escribimos el fichero a enviar
    '    '    Response.WriteFile(path + ".pdf")
    '    '    ' volcamos el stream 
    '    '    Response.Flush()
    '    '    ' Enviamos todo el encabezado ahora
    '    '    Response.End()
    '    'Catch ex As Exception
    '    '    LblMensajeCritico.Text = ex.Message
    '    '    PanelCritico.Visible = True
    '    '    PanelCritico.Focus()
    '    'End Try

    '    'visor_reporte.DataBind()

    '    'Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
    '    'Dim o As CrystalDecisions.Shared.ExportOptions
    '    'o = New CrystalDecisions.Shared.ExportOptions
    '    'o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
    '    'o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
    '    'filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
    '    'o.ExportDestinationOptions = filedest.Clone
    '    'mireporte.Export(o)
    '    'filedest = Nothing
    '    'o = Nothing






    '    'Dim Nombre As String

    '    'Nombre = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".pdf"))
    '    'Dim xmlArchivo As String = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".xml"))


    '    'Response.Clear()
    '    'Response.ContentType = "application/pdf"
    '    'Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
    '    'Response.WriteFile(Nombre)
    '    'Response.Flush()
    '    'Response.Close()
    'End Sub
    'Sub descargarArchivofac(ByVal queimprimo As String, ByVal tipoArchivo As String)
    '    Dim path As String

    '    path = Server.MapPath("/Facturas/" + queimprimo)


    '    Try
    '        'Limpiamos la salida
    '        Response.Clear()
    '        'Con esto le decimos al browser que la salida sera descargable
    '        Response.ContentType = "application/octet-stream"
    '        'esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
    '        Response.AddHeader("Content-Disposition", "attachment; filename=" + queimprimo + "." + tipoArchivo)
    '        ' Escribimos el fichero a enviar
    '        Response.WriteFile(path + "." + tipoArchivo)
    '        ' volcamos el stream 
    '        Response.Flush()
    '        ' Enviamos todo el encabezado ahora
    '        Response.End()
    '    Catch ex As Exception
    '        LblMensajeCritico.Text = ex.Message
    '        PanelCritico.Visible = True
    '        PanelCritico.Focus()
    '    End Try

    'End Sub
    'Sub enviarFacr(ByRef folioFac As String)
    '    Dim folioFactura As String = folioFac
    '    'Dim estadoFac As String = estado_factura.Value
    '    Dim Path As String
    '    Dim mail As New MailMessage
    '    'If estadoFac = "Vigente" Then
    '    Path = Server.MapPath("/Facturas/" + folioFactura)
    '    'Else
    '    'Path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
    '    'End If

    '    mail.From = New MailAddress("facturas@stargrupoortopedico.com")

    '    mail.To.Add(correo.Value)

    '    mail.Subject = "Factura de servicios de Ortopedia Star"
    '    mail.Body = "Facturas"
    '    Try
    '        Dim FilePath1 As String = Path + ".xml"
    '        Dim FilePath2 As String = Path + ".pdf"

    '        'el archivo se adjunta indicándole la ruta 
    '        mail.Attachments.Add(New Attachment(FilePath1))
    '        mail.Attachments.Add(New Attachment(FilePath2))

    '        Dim mailClient As New SmtpClient()

    '        Dim basicAuthenticationInfo As New NetworkCredential("facturas@stargrupoortopedico.com", "fac2017*")

    '        mailClient.Host = "mail.stargrupoortopedico.com"

    '        mailClient.UseDefaultCredentials = True
    '        mailClient.Credentials = basicAuthenticationInfo
    '        mailClient.Port = 26

    '        mailClient.Send(mail)
    '        LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
    '        PanelAvisos.Visible = True
    '        PanelAvisos.Focus()
    '    Catch ex As Exception
    '        LblMensajeCritico.Text = ex.Message
    '        PanelCritico.Visible = True
    '        PanelCritico.Focus()
    '    End Try
    'End Sub
    Sub cargardatosFac(ByRef formaPago As String, ByRef importe As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT   convert(varchar,convert(decimal(8,2),importePago)) as importePago ,codigoFormaPago FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                   "where folioconsulta='" & fconsulta.Value & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & ""
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                formaPago = dt.Rows(0).Item("codigoFormaPago")
                importe = dt.Rows(0).Item("importePago")
            End If

        End If

    End Sub
    Sub GuardarIngeroCajas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim pago As String = cargarTotalPagos()
        strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado,abono) " & _
                 "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,0," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0,0)"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
        Else
            LblMensajeAdvertencia.Text = clsDatos.MensajeError
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
        End If
        'Dim clsDatos As New ClaseDatos
        'Dim dt As New DataTable
        'Dim strSQL As String
        'Dim pago As String = cargarTotalPagos()
        'strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado) " & _
        '         "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,0," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0)"
        'clsDatos.cargaComando(strSQL)
        'If clsDatos.ejecutar() = 0 Then
        'End If
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
    Sub buscarConsultasAfacturar(ByRef codigoPAciente As String, ByRef folioConsulta As String)
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim dt2 As New DataTable

        strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
            "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
            "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
            "where A.codigoPaciente='" & codigoPAciente & "' and  IC.Facturado=0  and IC.PagoRealizado=0 and A.folioConsulta='" & Session("folioConsulta") & "'" & _
            "order by fechaAgenda desc "
        bandera_facturaCredito = 1 ' si es credito
        If clsDatos.cargatabla(strSQL, dt2) = 0 Then
            If dt2.Rows.Count = 0 Then
                bandera_facturaCredito = 0 ' no es credito
                strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
            "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
            "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
            "where A.codigoPaciente='" & codigoPAciente & "' and IC.PagoRealizado=1 and IC.Facturado=0 " & _
            "order by fechaAgenda desc "
            End If
        End If
        If clsDatos.cargatabla(strSQL, dt2) = 0 Then
            If dt2.Rows.Count > 0 Then
                GdConsultasXpagar.DataSource = dt2
                GdConsultasXpagar.DataBind()
                For Each fila As GridViewRow In GdConsultasXpagar.Rows
                    If fila.Cells(0).Text = folioConsulta Then
                        CType(fila.FindControl("chk"), CheckBox).Checked = True
                        CType(fila.FindControl("chk"), CheckBox).Enabled = False
                        If bandera_facturaCredito = 1 Then
                            fila.BackColor = Color.Salmon
                        Else
                            fila.BackColor = Color.MediumTurquoise
                        End If

                    End If
                Next
            End If
        Else
            LblMensajeAdvertencia.Text = clsDatos.MensajeError
            PanelAdvertencia.Visible = True
        End If
    End Sub
    Function consultasAfacturar(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""
        count = 0
        For Each row As GridViewRow In GdConsultasXpagar.Rows
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
            strSQL = "select sum(cantidad) as cantidad,descripcion,cast((costo) AS decimal(16,2)) as costo FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                    "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 " & _
                    "group by descripcion,costo"
            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(costo*cantidad) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                    "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            listaFolios = listaFac
            listaFoliosSQL = listaSQL
            Return True
        End If

    End Function


End Class