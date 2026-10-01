Imports System.Data.SqlClient
Partial Class movimientos
    Inherits System.Web.UI.Page
    Dim clase As New miclases
    Sub mostrarcontroles(ByVal bandera As Boolean)
        pnlbuscar.Visible = bandera
        gridFacturasimP.Columns(7).Visible = bandera
        gridFacturasimP.Columns(8).Visible = bandera
        gridFacturasimP.Columns(9).Visible = bandera
        cmbclientes.Visible = bandera
        lblclientes.visible = bandera
        btncerrar.Visible = Not (bandera)
    End Sub

    '++Sub llenaaños()
    ' cmbaño.Items.Clear()
    'Dim conexion As SqlConnection = clase.conecta
    'Dim odataread As SqlDataReader
    'Dim comando As SqlCommand = conexion.CreateCommand
    '   comando.CommandText = "select distinct datepart(yyyy, fecha), datepart(yyyy, fecha) from factura"
    '   conexion.Open()
    '   odataread = comando.ExecuteReader
    'Dim i As Integer = 0
    '    Do While odataread.Read
    '        cmbaño.Items.Add(odataread.GetValue(1).ToString.Trim)
    '        cmbaño.Items(i).Value = odataread.GetValue(0).ToString.Trim
    '        i = i + 1
    '    Loop
    '    odataread.Close()
    '    conexion.Close()
    '++End Sub

    Sub buscaFacturas(ByVal bandera As Boolean)
        griddetalles.Visible = False
        gridFacturasimP.Visible = False
        Dim condiciona As String = ""

        If opciones.SelectedValue = 0 Then
            condiciona = " where (capCuenta='" + bandera.ToString + "' or capCuenta='True' ) and cancelada='false' "
        Else
            condiciona = " where capCuenta='" + bandera.ToString + "' and cancelada='false' "
        End If

        If cmbclientes.SelectedValue <> "0" Then
            condiciona = condiciona + " and nombre='" + cmbclientes.SelectedValue.Trim + "'"
        End If

        'If cmbMesFac.SelectedValue <> "0" Then
        'condiciona = condiciona + " and datepart(mm, fecha) = '" + cmbMesFac.SelectedValue.Trim + "' and datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        'Else
        'condiciona = condiciona + " and datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        'End If

        '+++ selecciona rango de fechas
        'condiciona = condiciona + " and convert(varchar(10),fecha,101)>= '" + Format(Month(txtfecha1.Text.Trim), "#00") + "/" + Format(Day(txtfecha1.Text.Trim), "#00") + "/" + Format(Year(txtfecha1.Text.Trim), "###0") + " " & _
        '        "' and  convert(varchar(10),fecha,101)<='" + Format(Month(txtfecha2.Text.Trim), "#00") + "/" + Format(Day(txtfecha2.Text.Trim), "#00") + "/" + Format(Year(txtfecha2.Text.Trim), "###0") + "' "

        condiciona = condiciona + " and fecha>='" + txtfecha1.Text.Trim + " 12:00:00' and fecha <='" + txtfecha2.Text.Trim + " 12:00:00'"

        Dim condiciona2 As String = " "
        Select Case opciones.SelectedValue
            Case 0
                'condiciona = condiciona + " or capCuenta='True' "
            Case 1
                condiciona2 &= " WHERE abono=0 "
            Case 2
                'condiciona2 &= " WHERE abono>=importe "
            Case 3
                condiciona2 &= " WHERE abono>0 "
        End Select

        gridFacturasimP = clase.creadataset("select temp.num_factura,fecha,nombre,importe,fecha2,sum(abono) " & _
        "as abono, importeCosto,CASE capcuenta WHEN 'true' THEN 'false'  ELSE 'true' end as capcuenta " & _
        " from(SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre, SUM(importe) " & _
        "AS importe ,fecha as fecha2,capcuenta FROM factura  " + condiciona + " GROUP BY num_factura," & _
        " fecha, nombre,capcuenta) as temp inner join (select num_factura,sum(abono) as abono from abonos group by" & _
        " num_factura ) temp2 on temp2.num_factura=temp.num_factura " & _
        " left join (select sum(importe) as importeCosto,num_factura from abonos group by num_factura) temp3 on temp.num_factura=temp3.num_factura " + condiciona2 + " " & _
        " group by temp.num_factura,fecha,nombre,importe,fecha2,importeCosto,capcuenta order by  temp.num_factura desc ", gridFacturasimP)

        If gridFacturasimP.Items.Count > 0 Then
            gridFacturasimP.Visible = True
        End If
    End Sub
    Sub detallesfactura(ByVal factura As String)
        griddetalles = clase.creadataset("SELECT clientes.elnombre,catServicios.descripcion,abonos.idcliente," & _
            " convert(varchar(10),abonos.fecha,103) as fecha, abonos.abono, abonos.importe,abonos.idabono," & _
            " abonos.fecha as fecha2,cast(idcita as varchar(20)) as idcita FROM  abonos INNER JOIN clientes ON abonos.idCliente = " & _
            "clientes.idCliente INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN" & _
            "  catServicios ON catServicios.id_servicio = catCostos.id_servicio where num_factura=" & _
            "'" + factura + "' order by fecha2 asc", griddetalles)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblfechacita.Text = Context.Items("fecha").ToString.Trim
            '+++ lbldb.Text = Context.Items("db").ToString.Trim
            '+++ lblsucursal.Text = Context.Items("sucursal").ToString.Trim

            'lbldb.Text = "fisocare"
            Dim auxfecha As Date = Now.Date
            Dim aux As Date = "01/" + auxfecha.AddMonths(1).ToString("MM") + "/" + auxfecha.ToString("yyyy")
            txtfecha1.Text = "01" + "/" + auxfecha.ToString("MM") + "/" + auxfecha.ToString("yyyy")
            txtfecha2.Text = aux.AddDays(-1).ToString("dd") + "/" + auxfecha.ToString("MM") + "/" + auxfecha.ToString("yyyy")

            '++llenaaños()
            '++cmbaño.SelectedValue = Now.Year.ToString.Trim
            cmbclientes = clase.llenacombos(cmbclientes, "select distinct nombre,nombre from factura")
            buscaFacturas(False)
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnbuscar.Click
        If opciones.SelectedValue = "0" Or opciones.SelectedValue = "1" Or opciones.SelectedValue = "3" Then
            buscaFacturas(False)
            mostrarcontroles(True)
            gridFacturasimP.Columns(8).Visible = False
        Else
            mostrarcontroles(True)
            buscaFacturas(True)
        End If
    End Sub

    Protected Sub cmbclientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbclientes.SelectedIndexChanged
        If opciones.SelectedValue = "0" Then
            buscaFacturas(False)
        Else
            buscaFacturas(True)
        End If
    End Sub

    Public Function Validapagos(ByVal consulta As String) As String
        Dim valor1 As String = ""
        Dim valor2 As String = ""
        Dim estatus As String = ""
        Dim conexion As SqlConnection = clase.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "select sum(abono) as pago, sum(importe) as importe from abonos " & _
        " where num_factura = '" + consulta.Trim + "' group by num_factura "
        conexion.Open()
        Dim sqlread As SqlDataReader = comando.ExecuteReader
        If sqlread.Read Then
            valor1 = sqlread.GetValue(0).ToString.Trim
            valor2 = sqlread.GetValue(1).ToString.Trim
        End If
        sqlread.Close()
        conexion.Close()
        If CDec(valor1.Trim) >= CDec(valor2.Trim) Then
            estatus = "True"
        Else
            estatus = "False"
        End If

        Return estatus
    End Function

    Protected Sub gridFacturasimP_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridFacturasimP.ItemCommand
        'If opciones.SelectedValue <> "2" Then
        Dim numfac As String = CType(gridFacturasimP.Items(e.Item.ItemIndex).Cells(0).Controls(1), LinkButton).Text
        Select Case e.CommandName
            Case "regresar"
                Dim cRows As Integer = gridFacturasimP.Items.Count
                Dim auxRows As Integer = 0
                Dim consulta As String = "update factura set capcuenta='false' where num_factura='" + numfac + "'"
                clase.grabaDatos(consulta)
                mostrarcontroles(True)
                gridFacturasimP.Columns(7).Visible = False
                buscaFacturas(True)
            Case "verfac"
                Try
                    Dim Nombre As String = "c:\reporteFisio\FisioSM\fsm_" + gridFacturasimP.Items(e.Item.ItemIndex).Cells(1).Text.Trim + ".pdf"
                    Response.Clear()
                    Response.ContentType = "application/pdf"
                    Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
                    Response.WriteFile(Nombre)
                    Response.Flush()
                    Response.Close()
                Catch
                    Messagebox1.ShowMessage("NO SE ENCONTRO EL ARCHIVO DE LA FACTURA ...")
                End Try
            Case "detalles"
                detallesfactura(numfac)
                griddetalles.Visible = True
                mostrarcontroles(False)
                gridFacturasimP = clase.creadataset("select temp.num_factura,fecha,nombre,importe,fecha2" & _
                        ",sum(abono) as abono,importeCosto,CASE capcuenta WHEN 'true' THEN 'false'  ELSE 'true' end as capcuenta" & _
                        "  from(SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre," & _
                        " SUM(importe) AS importe ,fecha as fecha2,capcuenta FROM factura  where num_factura='" + numfac + "'" & _
                        " GROUP BY num_factura, fecha, nombre,capcuenta) as temp inner join (select num_factura,sum(abono) " & _
                        "as abono from abonos where num_factura='" + numfac + "'group by" & _
                        " num_factura ) temp2 on temp2.num_factura=temp.num_factura " & _
                         " left join (select sum(importe) as importeCosto,num_factura from abonos where num_factura='" + numfac + "' group by num_factura) temp3 on temp.num_factura=temp3.num_factura" & _
                        " group by temp.num_factura,fecha,nombre,importe,fecha2,importeCosto,capcuenta order by  fecha2 desc ", gridFacturasimP)
            Case "cancelar"
                btncerrar.Visible = False
                pnlcancela.Visible = True
                gridFacturasimP = clase.creadataset("select temp.num_factura,fecha,nombre,importe,fecha2" & _
                        ",sum(abono) as abono,importeCosto,CASE capcuenta WHEN 'true' THEN 'false'  ELSE 'true' end as capcuenta" & _
                        "  from(SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre," & _
                        " SUM(importe) AS importe ,fecha as fecha2,capcuenta FROM factura  where num_factura='" + numfac + "'" & _
                        " GROUP BY num_factura, fecha, nombre,capcuenta) as temp inner join (select num_factura,sum(abono) " & _
                        "as abono from abonos where num_factura='" + numfac + "'group by" & _
                        " num_factura ) temp2 on temp2.num_factura=temp.num_factura " & _
                         " left join (select sum(importe) as importeCosto,num_factura from abonos where num_factura='" + numfac + "' group by num_factura) temp3 on temp.num_factura=temp3.num_factura" & _
                        " group by temp.num_factura,fecha,nombre,importe,fecha2,importeCosto,capcuenta order by  fecha2 desc ", gridFacturasimP)
        End Select
    End Sub

    Protected Sub gridFacturas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles griddetalles.ItemCommand
        Dim numfac As String = CType(gridFacturasimP.Items(0).Cells(0).Controls(1), LinkButton).Text
        Select Case e.CommandName
            Case "abonar"
                griddetalles.EditItemIndex = e.Item.ItemIndex
                detallesfactura(numfac)
            Case "grabar"

                Dim dGrabo As String = griddetalles.Items(e.Item.ItemIndex).Cells(0).Text
                Dim idabono As String = griddetalles.DataKeys(e.Item.ItemIndex).ToString.Trim
                Dim abono As Decimal = CType(griddetalles.Items(e.Item.ItemIndex).Cells(4).Controls(0), TextBox).Text.Trim
                Dim cRows As Integer = griddetalles.Items.Count
                Dim cuenta As Integer = 0
                Dim suma As Decimal = 0.0
                Do While cuenta < cRows
                    If cuenta <> e.Item.ItemIndex Then
                        Dim valor1 As Double = griddetalles.Items(cuenta).Cells(4).Text
                        suma = suma + valor1
                    Else
                        suma = suma + abono
                    End If
                    cuenta = cuenta + 1
                Loop
                'Dim elimporte As Decimal = gridFacturasimP.Items(0).Cells(5).Text
                Dim elimporte As Decimal = gridFacturasimP.Items(0).Cells(6).Text
                If suma <= elimporte Then
                    clase.grabaDatos("update abonos set abono='" + abono.ToString.Trim + "' where idabono='" + idabono.Trim + "'")
                    If suma >= elimporte Then
                        clase.grabaDatos("update factura set capcuenta='true' where num_factura='" + numfac + "'")
                    Else
                        clase.grabaDatos("update factura set capcuenta='false' where num_factura='" + numfac + "'")
                    End If
                    griddetalles.EditItemIndex = -1
                    detallesfactura(numfac)
                    gridFacturasimP = clase.creadataset("select temp.num_factura,fecha,nombre,importe,fecha2" & _
                        ",sum(abono) as abono,importeCosto,CASE capcuenta WHEN 'true' THEN 'false'  ELSE 'true' end as capcuenta " & _
                        "  from(SELECT num_factura, convert(varchar(10),fecha,103) AS fecha, nombre," & _
                        " SUM(importe) AS importe ,fecha as fecha2,capcuenta FROM factura  where num_factura='" + numfac + "'" & _
                        " GROUP BY num_factura, fecha, nombre,capcuenta) as temp inner join (select num_factura,sum(abono) " & _
                        "as abono from abonos where num_factura='" + numfac + "'group by" & _
                        " num_factura ) temp2 on temp2.num_factura=temp.num_factura " & _
                         " left join (select sum(importe) as importeCosto,num_factura from abonos where num_factura='" + numfac + "' group by num_factura) temp3 on temp.num_factura=temp3.num_factura" & _
                        " group by temp.num_factura,fecha,nombre,importe,fecha2,importeCosto,capcuenta order by  fecha2 desc ", gridFacturasimP)
                Else
                    Messagebox1.ShowMessage("LOS ABONOS NO PUEDEN SER MAYORES AL IMPORTE")
                End If
            Case "cancelar"
                griddetalles.EditItemIndex = -1
                detallesfactura(numfac)

        End Select
    End Sub
    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        'clase.grabaDatos("update factura set cancelada='true',capcuenta='false' where num_factura='" + Key + "'; " & _
        '"update abonos set facturado='false', num_factura=NULL where num_factura='" + Key + "'", lbldb.Text.Trim)
        'buscaFacturas(False)
    End Sub
    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        'Context.Items.Add("db", lbldb.Text.Trim)
        'Context.Items.Add("sucursal", lblsucursal.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub Button3_Click2(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim numfac As String = CType(gridFacturasimP.Items(0).Cells(0).Controls(1), LinkButton).Text.Trim
        clase.grabaDatos("update factura set capcuenta='false', cancelada='true', sustituidoX='" + txtsustituye.Text.Trim + "' where num_factura='" + numfac + "'; " & _
                "update abonos set facturado='false', num_factura=NULL where num_factura='" + numfac + "'")
        pnlcancela.Visible = False
        mostrarcontroles(True)
        If opciones.SelectedValue = "2" Then
            buscaFacturas(True)
        Else
            buscaFacturas(False)
            gridFacturasimP.Columns(8).Visible = False
        End If
        txtsustituye.Text = ""
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        pnlcancela.Visible = False
        mostrarcontroles(True)
        If opciones.SelectedValue = "2" Then
            buscaFacturas(True)
        Else
            buscaFacturas(False)
            gridFacturasimP.Columns(8).Visible = False
        End If
        txtsustituye.Text = ""
    End Sub

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Panel1.Visible = False
        griddetalles.EditItemIndex = -1
        mostrarcontroles(True)
        If opciones.SelectedValue = "2" Then
            buscaFacturas(True)
        Else
            gridFacturasimP.Columns(8).Visible = False
            buscaFacturas(False)
        End If
    End Sub
    Sub exportaexcel(ByVal migrid As DataGrid)
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Page.EnableViewState = False
        migrid.AllowPaging = False
        migrid.AllowSorting = False
        Dim ESCRITORAEXCEL As New System.IO.StringWriter()
        Dim PASADORHTML As New HtmlTextWriter(ESCRITORAEXCEL)
        migrid.RenderControl(PASADORHTML)
        Response.Write(ESCRITORAEXCEL.ToString)
        Response.End()
    End Sub

    Protected Sub lnkExporta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkExporta.Click
        gridFacturasimP.Columns(0).Visible = False
        gridFacturasimP.Columns(1).Visible = True
        gridFacturasimP.Columns(7).Visible = False
        gridFacturasimP.Columns(8).Visible = False
        gridFacturasimP.Columns(9).Visible = False

        exportaexcel(gridFacturasimP)
        gridFacturasimP.Columns(1).Visible = False
        gridFacturasimP.Columns(0).Visible = True
        gridFacturasimP.Columns(7).Visible = True
        gridFacturasimP.Columns(8).Visible = True
        gridFacturasimP.Columns(9).Visible = True
    End Sub

End Class
