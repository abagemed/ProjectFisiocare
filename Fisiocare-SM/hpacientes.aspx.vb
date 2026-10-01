Imports System.Data.SqlClient
Partial Class hpacientes
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim fun As New miclases
    Dim funciones As New miclases
    Sub llenagrid(ByVal condicion As String)
        gridDatos = funciones.creadataset("SELECT clientes.elnombre, convert(varchar(10),abonos.fecha,103) as fecha, catServicios.descripcion, " & _
            "abonos.abono, abonos.importe - abonos.monto as importe,abonos.idabono, abonos.fecha as fecha2,idcita,num_factura FROM  abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
            "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
            "catServicios.id_servicio = catCostos.id_servicio where abonos.idcliente='" + lblfila.Text.ToString + "' " & _
            " " + condicion + " order by fecha2 desc", gridDatos)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Dim funciones As New miclases
            lblfechacita.Text = Context.Items("fecha").ToString.Trim

        End If
    End Sub

    Protected Sub OPCIONES_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles OPCIONES.SelectedIndexChanged
        Select Case OPCIONES.SelectedValue.Trim
            Case 0
                porAbonos()
            Case 1
                porFactura()
        End Select
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
    Sub porAbonos()
        gridDatos.Columns(4).Visible = True
        gridDatos.Columns(8).Visible = False
        gridDatos = funciones.creadataset("SELECT clientes.elnombre, convert(varchar(10),abonos.fecha,103) as fecha, IIF(abonos.abonoConciliacion > 0, 'ABONO POR CONCILIACIÓN', catServicios.descripcion) AS descripcion, " & _
        "IIF(abonos.abonoConciliacion > 0, abonos.abonoConciliacion, abonos.abono) as abono, abonos.importe - abonos.monto as importe,abonos.idabono, abonos.fecha as fecha2,idcita,num_factura,serie, 0 as importefac, CatFormasDePago.descripcion as fpago FROM  abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
        "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
        "catServicios.id_servicio = catCostos.id_servicio " & _
        " inner join CatFormasDePago on CatFormasDePago.descripcioncorta = abonos.idpago " & _
        "where abonos.idcliente='" + lblfila.Text.ToString + "' " & _
        " order by fecha2 desc, idcita desc, num_factura desc", gridDatos)
        habilitaVerfactura()
        habilitaVereditar()
    End Sub
    Sub porFactura()
        gridDatos.Columns(4).Visible = False
        gridDatos.Columns(8).Visible = True
        gridDatos = funciones.creadataset("select elnombre,fecha,' ' as descripcion,sum(IIF(abonoConciliacion > 0, abonoConciliacion, abono)) as abono,sum(importe) as importe,idabono,fecha2,idcita,num_factura,serie,importefac, '' as fpago from " & _
        "(SELECT clientes.elnombre, convert(varchar(10),factura.fecha,103) as fecha, ' ' as descripcion, " & _
        "abonos.abono, sum(abonos.importe) - sum(abonos.monto) as importe, sum(abonoConciliacion) as abonoConciliacion,' ' as idabono, factura.fecha as fecha2,' ' as idcita,factura.num_factura,factura.serie,importefac FROM  abonos INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
        "INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
        "catServicios.id_servicio = catCostos.id_servicio left join (select num_factura,fecha,sum(importe) as importefac,serie from factura where idcliente='" + lblfila.Text.ToString + "'  group by serie,num_factura,fecha )" & _
        " as factura on abonos.num_factura=factura.num_factura where abonos.idcliente='" + lblfila.Text.ToString + "' " & _
        " group by elnombre,factura.fecha,catServicios.descripcion,abono,importe,idabono,factura.fecha,idcita,factura.num_factura,factura.serie,importefac ) " & _
        "as temp group by elnombre,fecha,descripcion,idabono,fecha2,idcita,num_factura,serie,importefac order by fecha2 desc,num_factura desc", gridDatos)
        habilitaVerfactura()
        habilitaVereditar()
    End Sub
    Sub totales()
        Dim valores(,) As String = funciones.leerValores("select isnull(sum(IIF(abonoConciliacion > 0, abonoConciliacion, abono)),0.0),isnull(sum(importe),0.0)-isnull(sum(monto),0.0) from abonos where idcliente='" + lblfila.Text.ToString + "'", 2)
        lblabonos.Text = "$ " + Format(Convert.ToDouble(valores(0, 0)), "###,###,###0.00").ToString.Trim
        lblImportes.Text = "$ " + Format(Convert.ToDouble(valores(1, 0)), "###,###,###0.00")
        Dim facturado As String = funciones.leerValor("select isnull(sum(factura.importe),0.0) from factura where idcliente='" + lblfila.Text.ToString + "' and cancelada='False'")
        lblFacturado.Text = "$ " + Format(Convert.ToDouble(facturado), "###,###,###0.00").ToString.Trim

    End Sub

    Protected Sub gridDatos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridDatos.ItemCommand



        Select Case e.CommandName.ToString
            Case "verfac"
                Dim serie As String = e.Item.ItemIndex.ToString.Trim
                serie = gridDatos.Items(e.Item.ItemIndex).Cells(2).Text.Trim
                Select Case serie
                    Case "C"
                        'Dim Nombre As String = "c:\reporteFisio\FisioSM\fsm_" + gridDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf"
                        Dim Nombre As String = "C:\inetpub\vhosts\agemed.com.mx\facturacionsm.agemed.com.mx\Facturas\PDFS\C  " + gridDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf"
                        Dim informacion As String = "C  " + gridDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf "
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("Content-disposition", "attachment; filename=" & informacion)
                        Response.WriteFile(Nombre)
                        Response.Flush()
                        Response.Close()
                    Case "TH"
                        Dim Nombre As String = "c:\reporteFisio\FisioSM\fsmTH_" + gridDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf"
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
                        Response.WriteFile(Nombre)
                        Response.Flush()
                        Response.Close()
                    Case "DS"
                        Dim Nombre As String = "c:\reporteFisio\FisioSM\fsmDS_" + gridDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf"
                        Response.Clear()
                        Response.ContentType = "application/pdf"
                        Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
                        Response.WriteFile(Nombre)
                        Response.Flush()
                        Response.Close()
                End Select
            Case "agregar"
                gridDatos.Visible = False
                tabla.Visible = True
                txtfecha.Text = gridDatos.Items(e.Item.ItemIndex).Cells(5).Text
                lblterapia.Text = gridDatos.Items(e.Item.ItemIndex).Cells(4).Text
                txtabonos.Text = gridDatos.Items(e.Item.ItemIndex).Cells(6).Text
                lblimporte.Text = gridDatos.Items(e.Item.ItemIndex).Cells(7).Text
                lblidabono.Text = gridDatos.Items(e.Item.ItemIndex).Cells(1).Text
        End Select


    End Sub
    Protected Sub btncancelare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btncancelare.Click
        gridDatos.Visible = True
        tabla.Visible = False
    End Sub
    Protected Sub btnaceptare_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnaceptare.Click
        Try
            Dim miabono As Double = Convert.ToDouble(txtabonos.Text)
            
            funciones.grabaDatos("update abonos set abono='" + miabono.ToString + "',fecha='" + txtfecha.Text.Trim + "', fechaactualizacion=getdate() where idabono='" + lblidabono.Text.Trim + "'")
            
            tabla.Visible = False
            gridDatos.Visible = True
            Select Case OPCIONES.SelectedValue.Trim
                Case 0
                    porAbonos()
                    totales()
                Case 1
                    porFactura()
                    totales()
            End Select
            Messagebox1.ShowMessage("DATOS GUARDADOS")
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try


     
    End Sub
    Sub habilitaVereditar()
        Dim cuenta As Integer = 0
        Do While cuenta < gridDatos.Items.Count
            If gridDatos.Items(cuenta).Cells(6).Text < gridDatos.Items(cuenta).Cells(7).Text Then
                CType(gridDatos.Items(cuenta).Cells(10).Controls(1), ImageButton).Visible = False
            Else
                CType(gridDatos.Items(cuenta).Cells(10).Controls(1), ImageButton).Visible = False
            End If
            cuenta = cuenta + 1
        Loop
        If cuenta <> 0 Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub
    Sub habilitaVerfactura()
        Dim cuenta As Integer = 0
        Do While cuenta < gridDatos.Items.Count
            If gridDatos.Items(cuenta).Cells(1).Text.Trim = "&nbsp;" Then
                CType(gridDatos.Items(cuenta).Cells(9).Controls(1), Button).Visible = False
            End If
            cuenta = cuenta + 1
        Loop
        If cuenta <> 0 Then
            Panel1.Visible = True
        Else
            Panel1.Visible = False
        End If
    End Sub
    Protected Sub buscarcliente_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles buscarcliente.Click
        Dim funciones As New miclases
        Dim celpaterno, celmaterno, celnombre As String
        If ctxtPaternoB.Text = "" Then
            celpaterno = ""
        Else
            celpaterno = Trim(ctxtPaternoB.Text.ToUpper)
        End If
        If ctxtMaternoB.Text = "" Then
            celmaterno = ""
        Else
            celmaterno = Trim(ctxtMaternoB.Text.ToUpper)
        End If
        If ctxtnombreB.Text = "" Then
            celnombre = ""
        Else
            celnombre = Trim(ctxtnombreB.Text.ToUpper)
        End If
        cgridClientes = funciones.creadataset("select idCliente,elnombre from clientes " & _
        "where paterno like '" + celpaterno + "%' and materno like '" + celmaterno + "%' and nombre " & _
                "like '" + celnombre + "%' order by elnombre", cgridClientes)
    End Sub
    Protected Sub cgridClientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles cgridClientes.ItemCommand

        lblfila.Text = cgridClientes.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        llenaDatos()
        cgridClientes.DataBind()
        ctxtPaternoB.Text = ""
        ctxtMaternoB.Text = ""
        ctxtnombreB.Text = ""
    End Sub
    Sub llenaDatos()

        porAbonos()
        totales()
        If gridDatos.Items.Count > 0 Then
            dDatos.Visible = True
        Else
            dDatos.Visible = False
        End If


        Dim misfunciones As New miclases
        Dim conexion As SqlConnection = misfunciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idCliente,elnombre from clientes where idcliente='" + lblfila.Text.ToString + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lblnombre.Text = leer.GetValue(1).ToString.Trim
        End If
        leer.Close()
        conexion.Close()

    End Sub




    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
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

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub

    Protected Sub CTerapeutas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("ABCTerapeutas.aspx", False)
    End Sub
End Class
