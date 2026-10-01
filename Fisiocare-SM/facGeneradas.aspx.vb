Imports System.Collections.Generic
Imports System.IO
Imports com.facturarenlinea.timbrado.RespuestaTFD
Imports com.facturarenlinea.timbrado
Imports System.Xml
Imports CFDI
Partial Class facGeneradas
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim lasfunciones As New Funciones
    Sub facGeneradas()
        Dim condiciona As String = ""
        If cmbMesFac.SelectedValue <> "0" Then
            condiciona = " where datepart(mm, fecha) = '" + cmbMesFac.SelectedValue.Trim + "' and datepart(yyyy, fecha)='" + cmblosaños.SelectedValue + "' and serie='C'"
        Else
            condiciona = " where datepart(yyyy, fecha)='" + cmblosaños.SelectedValue + "' and serie='C'"
        End If
        gFacgeneradas = funciones.creadataset("SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre, SUM(importe) AS importe " & _
       ",fecha as fecha2, (case cancelada when 'false' then 'Vigente' else 'Cancelada' end) as cancelada,validaf FROM factura " + condiciona + " GROUP BY num_factura, fecha, nombre,cancelada,validaf order by num_factura ", gFacgeneradas)
        validaCancelacion()
    End Sub
    Sub facGeneradasFiltro(ByVal filtro As String)
        Dim condiciona As String = ""
        If cmbMesFac.SelectedValue <> "0" Then
            condiciona = " where datepart(mm, fecha) = '" + cmbMesFac.SelectedValue.Trim + "' and datepart(yyyy, fecha)='" + cmblosaños.SelectedValue + "' and serie='C'"
        Else
            condiciona = " where datepart(yyyy, fecha)='" + cmblosaños.SelectedValue + "' and serie='C' "
        End If
        gFacgeneradas = funciones.creadataset("SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre, SUM(importe) AS importe " & _
       ",fecha as fecha2, (case cancelada when 'false' then 'Vigente' else 'Cancelada' end) as cancelada,validaf FROM factura " + condiciona + filtro + " GROUP BY num_factura, fecha, nombre,cancelada,validaf order by num_factura ", gFacgeneradas)
        validaCancelacion()
    End Sub
    Sub validaCancelacion()
        Dim cuenta As Integer = 0
        Do While cuenta < gFacgeneradas.Items.Count
            Dim fecha As Date = gFacgeneradas.Items(cuenta).Cells(1).Text.Trim
            Dim hoy As Date = Date.Now
            Dim xxx = DateDiff(DateInterval.Day, fecha, hoy)
            If DateDiff(DateInterval.Day, fecha, hoy) > 8 And gFacgeneradas.Items(cuenta).Cells(7).Text.Trim = 0 Then
                CType(gFacgeneradas.Items(cuenta).Cells(4).Controls(1), Button).Visible = False
            End If
            If gFacgeneradas.Items(cuenta).Cells(3).Text = "Cancelada" Then
                CType(gFacgeneradas.Items(cuenta).Cells(4).Controls(1), Button).Visible = False
                CType(gFacgeneradas.Items(cuenta).Cells(5).Controls(1), ImageButton).Visible = False
                CType(gFacgeneradas.Items(cuenta).Cells(6).Controls(1), ImageButton).Visible = False
            End If
            cuenta = cuenta + 1
        Loop
    End Sub
    Protected Sub btnfacturar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnfacturar.Click
        Select Case cmbfacgeneradas.SelectedValue
            Case 0
                facGeneradas()
            Case 1
                facGeneradasFiltro(" and cancelada='false' ")
            Case 2
                facGeneradasFiltro(" and cancelada='true' ")
        End Select
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        gFacgeneradas = funciones.creadataset("SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre, SUM(importe) AS importe " & _
               ",fecha as fecha2, (case cancelada when 'false' then 'Vigente' else 'Cancelada' end) as cancelada,validaf FROM factura where num_factura='" + txtnumfactura.Text.ToString + "' and serie='C' GROUP BY num_factura, fecha, nombre,cancelada,validaf order by num_factura ", gFacgeneradas)
        validaCancelacion()
        txtnumfactura.Text = ""
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
            Catch
                Response.Redirect("login.aspx")
            End Try
            cmbMesFac.SelectedValue = Date.Now.Month.ToString
            cmblosaños.SelectedValue = Date.Now.Year.ToString
            facGeneradas()
        End If
    End Sub

    Protected Sub gFacgeneradas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gFacgeneradas.ItemCommand
        Select Case e.CommandName
            Case "imprimir"
                Dim numFac As String = gFacgeneradas.Items(e.Item.ItemIndex).Cells(0).Text.Trim
                Dim Nombre As String
                Nombre = "c:\reporteFisio\fisioSM\fsm_" + numFac + ".pdf"
                Response.Clear()
                Response.ContentType = "application/pdf"
                Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
                Response.WriteFile(Nombre)
                Response.Flush()
                Response.Close()
            Case "mail"
                hfidfac.Value = gFacgeneradas.Items(e.Item.ItemIndex).Cells(0).Text.Trim
                ModalPopupExtender2.Show()
            Case "cancelar"
                txtnumcancelar.Text = gFacgeneradas.Items(e.Item.ItemIndex).Cells(0).Text.Trim
                ModalPopupExtender3.Show()
        End Select
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        Dim Nombre As String
        Nombre = "c:\reporteFisio\fisioSM\fsm_" + hfidfac.Value.Trim + ".pdf"
        Dim xmlArchivo As String = "c:\reporteFisio\FisioSM\fsm_" + hfidfac.Value.Trim + ".xml"
        lasfunciones.enviaCorreoPdf(txtmail.Text, Nombre, xmlArchivo, "Factura", "Fisioterapia y Medicina Deportiva SCP, Sucursal StarMedica Consultorio 722, Merida Yucatan Mexico. Gracias, que tenga un excelente dia.")
        txtmail.Text = ""
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        txtmail.Text = ""
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        'Dim ValidaCodigo As String = ""
        Dim ValidaCodigo() = {""}
        Dim detallecancela As New DetalleCancelacion
        If txtnumcancelar.Text > 0 Then

            Dim uuidCancelar() As String = {funciones.leerValor("select uuid from factura where num_factura='" + txtnumcancelar.Text.ToString.Trim + "' and serie='C'")}

            Dim timbrarFac As New WSTFD ''2015
            Dim cancelacionFac As New RespuestaCancelacion ''2015

            cancelacionFac = timbrarFac.CancelarCFDI("FMD020730PQ5", "VrGwKXeePbE#", "FMD020730PQ5", uuidCancelar, "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", "fmdscp2013")

            detallecancela.CodigoResultado = cancelacionFac.DetallesCancelacion(0).CodigoResultado ''2015

            Dim bandera As Boolean = cancelacionFac.OperacionExitosa


            If bandera = True Then
                'If ValidaCodigo(0).ToString = "201" Then
                'Dim nameFile As String = ""
                'Dim doc As XmlDocument = New XmlDocument()
                'doc.LoadXml(Xml)
                'nameFile = "c:\reporteFisio\canceladasCP\Canceladofcp_" + txtnumcancelar.Text.ToString.Trim + "_201.xml"
                'doc.Save(nameFile)
                ' Else
                'If ValidaCodigo(0).ToString = "202" Then
                'Dim nameFile As String = ""
                'Dim doc As XmlDocument = New XmlDocument()
                ' doc.LoadXml(Xml)
                'nameFile = "c:\reporteFisio\canceladasCP\Canceladofcp_" + txtnumcancelar.Text.ToString.Trim + "_202.xml"
                'doc.Save(nameFile)
                'End If
                'End If
                'Else
                'Messagebox1.ShowMessage("Error en la cancelación ->" & cancelacionFac.MensajeError)
            End If
        End If
        If detallecancela.CodigoResultado.ToString.Trim = "201" Or detallecancela.CodigoResultado.ToString.Trim = "202" Or txtnumcancelar.Text < 0 Then
            funciones.grabaDatos("update factura set capcuenta='false', cancelada='true', sustituidoX='" + txtsustituido.Text.Trim + "' where num_factura='" + txtnumcancelar.Text + "' and serie='C'; " & _
              "update abonos set facturado='false', num_factura=NULL where num_factura='" + txtnumcancelar.Text + "' and serie='C'")

            Dim uuidCancelar() As String = {funciones.leerValor("select uuid from factura where num_factura='" + txtnumcancelar.Text.ToString.Trim + "' and serie='C'")}

            Dim timbrarFac As New WSTFD ''2015

            Dim acusexml As New RespuestaTFD ''2015

            acusexml = timbrarFac.ObtenerAcuseCancelacion("FMD020730PQ5", "VrGwKXeePbE#", uuidCancelar(0).ToString)

            Dim xml

            xml = acusexml.XMLResultado

            If acusexml.CodigoRespuesta = "800" Then
                Dim nameFile As String = ""
                Dim doc As XmlDocument = New XmlDocument()
                doc.LoadXml(xml)
                nameFile = "c:\reporteFisio\canceladasSM\Canceladofsm_" + txtnumcancelar.Text.ToString.Trim + "_201.xml"
                doc.Save(nameFile)

            Else
                Messagebox1.ShowMessage("No se puede obtener el Xml ->" & acusexml.MensajeError)
            End If

        End If

        If detallecancela.CodigoResultado.ToString.Trim = "202" Then
            Messagebox1.ShowMessage("La factura ya ha sido cancelada ante el sat ...")
        Else
            If detallecancela.CodigoResultado.ToString.Trim <> "201" And detallecancela.CodigoResultado.ToString.Trim <> "202" And txtnumcancelar.Text > 8000 Then
                Messagebox1.ShowMessage("La factura no puede ser cancelada en estos momentos intente mas tarde ... codigo de error:" + ValidaCodigo(0).ToString)
            End If
        End If

        Select Case cmbfacgeneradas.SelectedValue
            Case 0
                facGeneradas()
            Case 1
                facGeneradasFiltro(" and cancelada='false' ")
            Case 2
                facGeneradasFiltro(" and cancelada='true' ")
        End Select
        txtsustituido.Text = ""
    End Sub

    'Protected Sub LinkButton8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton8.Click
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("facturacion.aspx", False)
    'End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub



    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Server.Transfer("clientes.aspx", True)
    End Sub

    Protected Sub Recibos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub Historial(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub Catalogos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub CorteCaja(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub BloqueoCitas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
    End Sub

    Protected Sub FacturasGeneradas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub

    Protected Sub VerCancelados(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub

    Protected Sub CTerapeutas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("ABCTerapeutas.aspx", False)
    End Sub
End Class
