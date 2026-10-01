Imports System.Data
Imports System.Data.SqlClient
Imports System.IO
Imports System.Collections.Generic

Partial Class Movimientos

    Inherits System.Web.UI.Page
    Dim clase As New miclases
    Dim funciones As New miclases
    Dim sqlComando As SqlCommand
    Dim SQL As String
    Dim sqlAdaptador As SqlDataAdapter
    Dim Cn As SqlConnection = funciones.conecta("admFisio")
    Dim dtCatFormaPago As New DataTable

    Sub llenafolioserie()
        lblfolioingreso.Text = funciones.leerValor("select prefijo + right(concat('000000000',consecutivo),9) as folioingreso from catfoliadores where codigoFoliador=1", "admFisio")
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            LlenarCatalogoFormasPago()
            cmbCuentas = funciones.llenacombos(cmbCuentas, "select codigocuentabancaria, nomBanco +' - '+ cast(numcuenta as varchar) from catCuentas", "admFisio")
            cmbTipo = funciones.llenacombos(cmbTipo, "select id,nomPago from catTipodePagos", "admFisio")
            Dim hoy As Date = Now.Date
            validaFecha.MaximumValue = hoy.ToString("dd") + "/" + hoy.ToString("MM") + "/" + hoy.ToString("yyyy")

            Dim hoy2 As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy2)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy2)
        End If
        Button2.Visible = False
        Panelfolio.Visible = False
    End Sub

    Sub LlenarCatalogoFormasPago()
        dtCatFormaPago = New DataTable
        Dim conexion As SqlConnection = funciones.conecta("admFisio")
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim query As String
        query = "EXEC [ADMFISIO_catTipodePagos] 1"

        sqlComando = New SqlCommand
        sqlAdaptador = New SqlDataAdapter
        Dim dtFormasPagos As New DataTable
        
        sqlAdaptador = New SqlDataAdapter(query, Cn)
        sqlAdaptador.Fill(dtFormasPagos)
        dtCatFormaPago = dtFormasPagos
        'sqlAdaptador.Dispose()
    End Sub

    Sub terapiaSinfacturar()
        If cmbSucursal.SelectedValue > "0" Then
            hfSucursal.Value = cmbSucursal.SelectedValue

            If cmbclientes.SelectedIndex = 0 Then
                gridFacturas = funciones.creadataset("select fact.num_factura, fact.serie,convert(varchar(10),fact.fecha,103) as fecha, fact.nombre,fact.rfc,fact.status, convert(decimal(15,2),fact.importe,2) as Importe, " & _
                     " convert(decimal(15,2),sum(isnull (facturas.abono,0)),2) as  Abonado, " & _
                    " case fact.status WHEN 'CANCELADA' THEN 0 ELSE (convert(decimal(15,2),fact.importe - sum(isnull (facturas.abono,0)),2)) END  as Adeuda " & _
                     " from (select num_factura ,serie ,fecha,idcliente, " & _
                    " nombre,rfc,(case cancelada  when 'true' then 'CANCELADA' else (case pagada when 'false' then 'ADEUDA' else 'PAGADA' end) end) as status, " & _
                    " sum(importe) as importe from " & hfSucursal.Value & ".dbo.factura " & _
                    " group by num_factura, serie, fecha,nombre,rfc,idcliente,cancelada,pagada) fact " & _
                    " full join  admFisio.dbo.facturas on facturas.numfactura=fact.num_factura and facturas.serie=fact.serie COLLATE DATABASE_DEFAULT " & _
                     " where fact.num_factura='" + txtNumFac.Text.Trim + "' and fact.status='ADEUDA' " & _
                     " group by fact.num_factura, fact.serie, fact.nombre,fact.rfc ,fact.idcliente, fact.fecha, fact.importe,fact.status " & _
                     " order by fact.nombre", gridFacturas, cmbSucursal.SelectedValue)

                gridFacturas.Visible = True

            Else

                gridFacturas = funciones.creadataset("select fact.num_factura, fact.serie,convert(varchar(10),fact.fecha,103) as fecha, fact.nombre,fact.rfc,fact.status, convert(decimal(15,2),fact.importe,2) as Importe, " & _
         " convert(decimal(15,2),sum(isnull (facturas.abono,0)),2) as  Abonado, " & _
        " case fact.status WHEN 'CANCELADA' THEN 0 ELSE (convert(decimal(15,2),fact.importe - sum(isnull (facturas.abono,0)),2)) END  as Adeuda " & _
         " from (select num_factura ,serie ,fecha,idcliente, " & _
        " nombre,rfc,(case cancelada  when 'true' then 'CANCELADA' else (case pagada when 'false' then 'ADEUDA' else 'PAGADA' end) end) as status, " & _
        " sum(importe) as importe from " & hfSucursal.Value & ".dbo.factura " & _
        " group by num_factura, serie, fecha,nombre,rfc,idcliente,cancelada,pagada) fact " & _
        " full join  admFisio.dbo.facturas on facturas.numfactura=fact.num_factura and facturas.serie=fact.serie COLLATE DATABASE_DEFAULT " & _
         " where fact.fecha>='" + txtfecha1.Text + " 12:00:00' and fact.fecha<='" + txtfecha2.Text.Trim + " 12:00:00'  and fact.rfc='" & hfclientes.Value & "' and fact.status='ADEUDA' " & _
         " group by fact.num_factura, fact.serie, fact.nombre,fact.rfc ,fact.idcliente, fact.fecha, fact.importe,fact.status " & _
         " order by fact.nombre", gridFacturas, cmbSucursal.SelectedValue)

                gridFacturas.Visible = True
            End If

        Else
            Messagebox1.ShowMessage("Favor de Seleccionar una Sucursal...")
        End If
    End Sub

    Protected Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        If cmbSucursal.SelectedValue <> "0" Then
            If cmbSucursal.SelectedItem.Text = "Gym" Then
                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
            Else
                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
            End If
        Else
            cmbclientes.Items.Clear()
        End If
        gridFacturas.Visible = False
    End Sub

    Protected Sub cmbclientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbclientes.SelectedIndexChanged
        If cmbSucursal.SelectedValue <> "0" Then
            If cmbSucursal.SelectedItem.Text = "Gym" Then
                Dim conexion As SqlConnection = funciones.conecta(cmbSucursal.SelectedValue)
                Dim comando As SqlCommand = conexion.CreateCommand
                comando.CommandText = "select  rfc " & _
                " from factura where paciente='" + cmbclientes.SelectedItem.Text + "'"
                conexion.Open()
                Dim sqlread As SqlDataReader = comando.ExecuteReader

                If sqlread.Read Then
                    hfclientes.Value = sqlread.GetValue(0).ToString.Trim
                End If
                sqlread.Close()
                conexion.Close()
            Else
                Dim conexion As SqlConnection = funciones.conecta(cmbSucursal.SelectedValue)
                Dim comando As SqlCommand = conexion.CreateCommand
                comando.CommandText = "select  rfc " & _
                " from factura where paciente='" + cmbclientes.SelectedItem.Text + "'"
                conexion.Open()
                Dim sqlread As SqlDataReader = comando.ExecuteReader

                If sqlread.Read Then
                    hfclientes.Value = sqlread.GetValue(0).ToString.Trim
                End If
                sqlread.Close()
                conexion.Close()
            End If
        Else
            cmbclientes.Items.Clear()
        End If
        terapiaSinfacturar()
        gridFacturas.DataBind()
        gridFacturas.Visible = True
    End Sub

    Protected Sub btnAgregarf_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregarf.Click
        terapiaSinfacturar()
    End Sub

    Protected Sub gridFacturas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridFacturas.ItemCommand
        Select Case CType(gridFacturas.Items(e.Item.ItemIndex).Cells(10).Controls(1), LinkButton).Text
            Case "Abonar"
                gridFacturas.EditItemIndex = e.Item.ItemIndex
                terapiaSinfacturar()

                CType(gridFacturas.Items(e.Item.ItemIndex).Cells(10).Controls(1), LinkButton).Text = "Aceptar"
                CType(gridFacturas.Items(e.Item.ItemIndex).Cells(9).Controls(1), TextBox).BackColor = Drawing.Color.YellowGreen

                txtNumFac.Text = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(0).Controls(1), TextBox).Text
                txtNumFac.Enabled = True
            Case "Aceptar"
                If txtReferencia.Text > "" Then
                    If cmbSucursal.SelectedValue > "0" Then
                        If CDbl(lblAuximporte.Text) >= CDbl(CType(gridFacturas.Items(e.Item.ItemIndex).Cells(9).Controls(1), TextBox).Text) Then
                            If Txtidep.Text > 0 Then

                                Dim abonos As Double = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(7).Controls(1), TextBox).Text
                                Dim oTabla As New DataTable
                                oTabla = funciones.creatabla(8)
                                Dim cuenta As Int16 = 0
                                Dim bandera As Boolean = True

                                Dim auxSaldo As Double = lblAuximporte.Text
                                txtobserva.Text = ""

                                Do While cuenta < gFacturas.Items.Count
                                    Dim aFila As DataRow = oTabla.NewRow

                                    aFila(0) = gFacturas.Items(cuenta).Cells(0).Text
                                    aFila(1) = gFacturas.Items(cuenta).Cells(1).Text
                                    aFila(2) = gFacturas.Items(cuenta).Cells(2).Text
                                    aFila(3) = gFacturas.Items(cuenta).Cells(3).Text
                                    txtobserva.Text = txtobserva.Text + gFacturas.Items(cuenta).Cells(3).Text + ", "
                                    aFila(4) = gFacturas.Items(cuenta).Cells(4).Text
                                    aFila(5) = gFacturas.Items(cuenta).Cells(5).Text
                                    Dim ImporteFac As Double = gFacturas.Items(cuenta).Cells(5).Text
                                    aFila(6) = gFacturas.Items(cuenta).Cells(6).Text
                                    aFila(7) = gFacturas.Items(cuenta).Cells(7).Text
                                    oTabla.Rows.Add(aFila)
                                    If gFacturas.Items(cuenta).Cells(0).Text = txtNumFac.Text.Trim Then
                                        bandera = False
                                    End If
                                    cuenta = cuenta + 1
                                Loop

                                If bandera = True Then
                                    Dim aFila2 As DataRow = oTabla.NewRow
                                    aFila2(0) = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(0).Controls(1), TextBox).Text
                                    aFila2(1) = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(1).Controls(1), TextBox).Text
                                    aFila2(2) = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(2).Controls(1), TextBox).Text
                                    aFila2(3) = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(3).Controls(1), TextBox).Text
                                    txtobserva.Text = txtobserva.Text + CType(gridFacturas.Items(e.Item.ItemIndex).Cells(3).Controls(1), TextBox).Text + ", "
                                    aFila2(4) = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(4).Controls(1), TextBox).Text
                                    Dim importeFac As Double = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(6).Controls(1), TextBox).Text
                                    importeFac = importeFac - abonos
                                    Dim apimporte As Double = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(9).Controls(1), TextBox).Text
                                    auxSaldo = apimporte - importeFac
                                    aFila2(5) = Format(importeFac, "###,###,###0.00")

                                    If auxSaldo >= 0 Then
                                        aFila2(6) = Format(importeFac, "###,###,###0.00")
                                        aFila2(7) = "Pagar"
                                    Else
                                        aFila2(6) = Format(importeFac + auxSaldo, "###,###,###0.00")
                                        aFila2(7) = "Abonar"
                                    End If

                                    oTabla.Rows.Add(aFila2)
                                    Dim ap2importe As Double = CType(gridFacturas.Items(e.Item.ItemIndex).Cells(9).Controls(1), TextBox).Text
                                    Dim ap3importe As Double = lblAuximporte.Text
                                    lblAuximporte.Text = ap3importe - ap2importe

                                    If lblAuximporte.Text <= 0 Then

                                        lblAuximporte.Text = 0
                                    End If

                                    oTabla.Columns(0).ColumnName = "columna0"
                                    oTabla.TableName = "mitabla"
                                    gFacturas.DataMember = "mitabla"
                                    gFacturas.DataSource = oTabla.DefaultView
                                    gFacturas.DataBind()

                                    gridFacturas.DataBind()
                                    terapiaSinfacturar()
                                    CType(gridFacturas.Items(e.Item.ItemIndex).Cells(10).Controls(1), LinkButton).Visible = False
                                End If
                                Button2.Visible = True
                            End If
                        Else
                            Messagebox1.ShowMessage("La Disponibilidad en Depósito es menor que la del movimiento")

                        End If
                    Else
                        Messagebox1.ShowMessage("Favor de Seleccionar una Sucursal...")
                    End If
                Else
                    Messagebox1.ShowMessage("Favor de aplicar en No.Referencia")
                End If
        End Select
    End Sub

    Protected Sub gFacturas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gFacturas.ItemCommand
        Dim eNumfac As String = gFacturas.Items(e.Item.ItemIndex).Cells(0).Text
        Dim oTabla As New DataTable
        oTabla = funciones.creatabla(8)
        Dim cuenta As Int16 = 0
        Dim auxSaldo As Double = CDbl(tempImporte.Text) + CDbl(tempPendiente.Text)
        txtobserva.Text = ""

        Do While cuenta < gFacturas.Items.Count
            If eNumfac <> gFacturas.Items(cuenta).Cells(0).Text Then
                Dim aFila As DataRow = oTabla.NewRow
                aFila(0) = gFacturas.Items(cuenta).Cells(0).Text
                aFila(1) = gFacturas.Items(cuenta).Cells(1).Text
                aFila(2) = gFacturas.Items(cuenta).Cells(2).Text
                aFila(3) = gFacturas.Items(cuenta).Cells(3).Text
                aFila(4) = gFacturas.Items(cuenta).Cells(4).Text
                aFila(5) = gFacturas.Items(cuenta).Cells(5).Text
                txtobserva.Text = txtobserva.Text + gFacturas.Items(cuenta).Cells(3).Text + ", "
                Dim ImporteFac As Double = gFacturas.Items(cuenta).Cells(6).Text
                Dim importep As Double = gFacturas.Items(cuenta).Cells(5).Text
                'Dim apimporte As Double = txtImporte.Text
                auxSaldo = auxSaldo - ImporteFac

                If ImporteFac = importep Then
                    aFila(6) = ImporteFac
                    aFila(7) = "Pagar"
                Else
                    aFila(6) = ImporteFac
                    aFila(7) = "Abonar"
                    'ImporteFac = importep
                    'aFila(7) = "Pagar"
                End If
                oTabla.Rows.Add(aFila)
                Button2.Visible = True
            End If
            cuenta = cuenta + 1
        Loop
        'Dim ap2importe As Double = txtImporte.Text
        lblAuximporte.Text = auxSaldo

        terapiaSinfacturar()

        If lblAuximporte.Text <= 0 Then
            'btnAgregar.Enabled = False
            lblAuximporte.Text = 0
        End If
        oTabla.Columns(0).ColumnName = "columna0"
        oTabla.TableName = "mitabla"
        gFacturas.DataMember = "mitabla"
        gFacturas.DataSource = oTabla.DefaultView
        gFacturas.DataBind()
    End Sub

    Protected Sub gFacturas_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gFacturas.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(0).Text = "# Fac."
            e.Item.Cells(1).Text = "Serie"
            e.Item.Cells(2).Text = "Fecha"
            e.Item.Cells(3).Text = "R.Social"
            e.Item.Cells(4).Text = "RFC"
            e.Item.Cells(5).Text = "Pendiente"
            e.Item.Cells(6).Text = "Abono"
            e.Item.Cells(7).Text = "Movimiento"

            e.Item.Cells(0).Width = 40
            e.Item.Cells(1).Width = 40
            e.Item.Cells(2).Width = 80
            e.Item.Cells(3).Width = 300
            e.Item.Cells(0).Width = 90
        End If
    End Sub

    Protected Sub txtidep_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Txtidep.TextChanged
        'btnAgregar.Enabled = True
        Dim oTabla As New DataTable
        Dim auximporte As Double
        'Dim aimporte As Double
        'Dim atxtimporte As Double = txtImporte.text
        Dim auxtxtImporte As Double = Txtidep.Text
        tempImporte.Text = Txtidep.Text
        auximporte = tempPendiente.Text + auxtxtImporte
        'aimporte = auximporte - atxtimporte
        Dim auxSaldo As Double = auximporte
        txtobserva.Text = ""
        calcula(auxSaldo)
    End Sub

    Protected Sub cmbCuentas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCuentas.SelectedIndexChanged
        If txtReferencia.Text > "" Then
            If txtfecha.Text > "" Then
                If cmbTipo.SelectedValue > 0 Then
                    If Txtidep.Text > 0 Then

                        sqlComando = New SqlCommand
                        sqlAdaptador = New SqlDataAdapter
                        Dim dtfolio As New DataTable
                        SQL = "select folioref from IngresosBancos "
                        SQL += vbNewLine & " where folioref= '" & txtReferencia.Text.Trim & "' and importe= '" & Txtidep.Text.Trim & "' and tipoPago= '" & cmbTipo.SelectedValue.ToString & "'"
                        sqlAdaptador = New SqlDataAdapter(SQL, Cn)
                        sqlAdaptador.Fill(dtfolio)

                        If dtfolio.Rows.Count = 0 Then
                            Dim conexion As SqlConnection = funciones.conecta("admFisio")
                            Dim comando As SqlCommand = conexion.CreateCommand
                            comando.CommandText = "select prefijo + right(concat('000000000',consecutivo + 1),9)  as folioingreso FROM CatFoliadores"
                            conexion.Open()
                            Dim sqlread As SqlDataReader = comando.ExecuteReader
                            If sqlread.Read Then
                                lblfolioingreso.Text = sqlread.GetValue(0)
                            End If
                            sqlread.Close()
                            conexion.Close()

                            funciones.grabaDatos("insert into IngresosBancos(FolioIngreso,folioRef,status,fechaOperacion,fechaIngreso,sucursal,CodigoCuentaBancaria," & _
                                  "tipoPago,Observaciones, importe, saldo) " & _
                                   "values('" + lblfolioingreso.Text + "','" + txtReferencia.Text.Trim + "','A','" + txtfecha.Text.Trim + "',getdate(),'" + Right(cmbSucursal.SelectedValue, 2) + "'" & _
                                   ",'" + cmbCuentas.SelectedValue.ToString + "','" + cmbTipo.SelectedValue.ToString + "','" + txtobserva.Text.Trim + "'" & _
                                   ",'" & Format(CDbl(Txtidep.Text), "##,##0.00") & "','" & Format(CDbl(lblAuximporte.Text), "##,##0.00") & "')", "admFisio")
                            funciones.grabaDatos("update catfoliadores set consecutivo = consecutivo +1 where codigoFoliador = 1", "admFisio")

                            cmbCuentas.Enabled = False
                            txtReferencia.Enabled = False
                            llenafolioserie()
                        Else
                            Messagebox1.ShowMessage("El folio ya existe con los mismo datos(Referencia, Deposito y Tipo de pago)")
                            cmbCuentas.SelectedValue = 0
                        End If
                    Else
                        Messagebox1.ShowMessage("El importe del deposito esta en $ 0")
                        cmbCuentas.SelectedValue = 0
                    End If
                Else
                    Messagebox1.ShowMessage("Favor de aplitar el Tipo de pago")
                    cmbCuentas.SelectedValue = 0
                End If
            Else
                Messagebox1.ShowMessage("Favor de aplicar Fecha de Deposito")
                cmbCuentas.SelectedValue = 0
            End If
        Else
            Messagebox1.ShowMessage("Favor de aplicar en No.Referencia")
            cmbCuentas.SelectedValue = 0
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        funciones.grabaDatos("update IngresosBancos set saldo='" & Format(CDbl(lblAuximporte.Text), "##,##0.00") & "' where FolioIngreso='" & lblfolioingreso.Text.Trim & "'", "admFisio")
        Dim cuenta As Integer = 0
        Dim serieFac As String

        Do While cuenta < gFacturas.Items.Count
            serieFac = gFacturas.Items(cuenta).Cells(1).Text.Trim

            funciones.grabaDatos("insert into facturas(folioingreso,folioRef,numFactura,serie,razonsocial,rfc,movimiento,fechafactura,importe,abono,fecha,fechaconciliacion,status) " & _
            "values('" & lblfolioingreso.Text.Trim & "','" + txtReferencia.Text.Trim + "','" + gFacturas.Items(cuenta).Cells(0).Text.ToString + "','" + serieFac + "'" & _
            ",'" + gFacturas.Items(cuenta).Cells(3).Text + "','" + gFacturas.Items(cuenta).Cells(4).Text + "','" + gFacturas.Items(cuenta).Cells(7).Text + "','" + gFacturas.Items(cuenta).Cells(2).Text + "','" + Replace(gFacturas.Items(cuenta).Cells(5).Text.ToString, ",", "") + "'" & _
            ",'" + Replace(gFacturas.Items(cuenta).Cells(6).Text.ToString, ",", "") + "'" & _
            ",'" + txtfecha.Text.Trim + "',getdate(),'A')", "admFisio")

            Try
                Select Case cmbSucursal.SelectedValue.Trim
                    Case "fisiocareCP"
                        If serieFac = "A" Then 'facturas normales, no complementos de pagos
                            ValidaInsertarAbonoConciliado(cuenta) 'validar si se debe insertar abono conciliado (si cliente es de aseguradora)
                        End If
                    Case "fisiocareCA"
                        If serieFac = "CA" Then 'facturas normales, no complementos de pagos
                            ValidaInsertarAbonoConciliado(cuenta) 'validar si se debe insertar abono conciliado (si cliente es de aseguradora)
                        End If
                    Case "fisiocareSM"
                        If serieFac = "C" Then 'facturas normales, no complementos de pagos
                            ValidaInsertarAbonoConciliado(cuenta) 'validar si se debe insertar abono conciliado (si cliente es de aseguradora)
                        End If
                    Case "fisiocareHO"
                        If serieFac = "HO" Then 'facturas normales, no complementos de pagos
                            ValidaInsertarAbonoConciliado(cuenta) 'validar si se debe insertar abono conciliado (si cliente es de aseguradora)
                        End If
                End Select
            Catch
                Messagebox1.ShowMessage("Ocurrió un error al insertar el abono por conciliación .... Favor de notificar a sistemas.")
                Console.WriteLine("Ocurrió un error al insertar el abono por conciliación.. Folio Ingreso > " + lblfolioingreso.Text.Trim)
            End Try

            If gFacturas.Items(cuenta).Cells(7).Text.Trim = "Pagar" Then
                funciones.grabaDatos("update factura set pagada='True', capcuenta='true' where num_factura='" + gFacturas.Items(cuenta).Cells(0).Text + "'", cmbSucursal.SelectedValue.Trim)
            End If

            cuenta = cuenta + 1
        Loop

        If lblAuximporte.Text < 0 Then
            lblAuximporte.Text = 0
        End If

        Dim dt As New DataTable
        DgFacturasCN.DataSource = dt
        DgFacturasCN.DataBind()
        Dim dtgf As New DataTable
        gridFacturas.DataSource = dtgf
        gridFacturas.DataBind()
        llenarconciliados(txtReferencia.Text)

        txtfecha.Text = ""
        'txtImporte.Text = 0
        Txtidep.Text = 0
        cmbSucursal.SelectedValue = 0
        cmbCuentas.SelectedValue = 0
        cmbTipo.SelectedValue = 0
        txtReferencia.Text = ""
        txtReferencia.Text = ""
        txtobserva.Text = ""
        'txtNumFac.Text = ""
        lblAuximporte.Text = ""
        lblfolioingreso.Text = ""

        gFacturas.DataSource = ""
        gFacturas.DataBind()
        gridFacturas.DataSource = ""
        gridFacturas.DataBind()

        cmbCuentas.Enabled = True
        txtReferencia.Enabled = True
        'btnAgregar.Enabled = False

        Messagebox1.ShowMessage("Movimiento Realizado con Éxito ....")
    End Sub

    Sub ValidaInsertarAbonoConciliado(ByVal indice As Integer)
        'validar si es cliente de aseguradora
        Dim numFact As String = gFacturas.Items(indice).Cells(0).Text.ToString
        Dim sucursal As String = cmbSucursal.SelectedValue.Trim
        Dim idAseguradora As String

        Dim conexion As SqlConnection = funciones.conecta(sucursal)
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim query As String
        query = "SELECT TOP 1 ab.idAbono, ab.idCliente, ab.idCita, ab.idCosto, ab.facturado, ab.num_factura, ab.serie, ab.reciboPagado, "
        query += "  ab.idDoctor, ab.coaseguro, ab.monto, ab.ligaAbono, ab.iddoctorc, ab.idtconsmed, ab.ClaveProdServ, ab.ClaveUnidad, "
        query += "  ab.unidad, costo.iddatosfac, resp.abreviacion, resp.descripcion "
        query += "FROM abonos ab "
        query += "INNER JOIN catCostos costo ON ab.idCosto = costo.idCosto "
        query += "INNER JOIN catResponsables resp ON costo.iddatosfac = resp.idresponsable "
        query += "WHERE ab.num_factura = '" + numFact + "' "
        query += "ORDER BY ab.idAbono ASC; "

        comando.CommandText = query
        conexion.Open()
        Dim sqlread As SqlDataReader = comando.ExecuteReader

        If sqlread.Read Then
            idAseguradora = sqlread.GetValue(17)
        Else
            Exit Sub
        End If

        If idAseguradora <> "200" Then 'Particular = 200, cualquier otra es aseguradora
            'insert en nuevo campo para abono conciliacion
            Dim abonoConciliacion As String = Replace(gFacturas.Items(indice).Cells(6).Text.ToString, ",", "")
            Dim descripcionCorta As String = ObtenerCatFormaPagoById(cmbTipo.SelectedValue.ToString)
            Dim observacion As String = txtobserva.Text

            If observacion.Length > 49 Then
                observacion = observacion.Substring(0, 49)
            End If

            Dim query2 As String
            query2 = "INSERT INTO abonos(idcliente, fecha, abono, idCita, importe, monto, idcosto, facturado, num_factura, serie, idDoctor, observacion, idPago, coaseguro, iddoctorc, idtconsmed, ClaveProdServ, ClaveUnidad, Unidad, cantidad, abonoConciliacion) "
            query2 += "VALUES(" + sqlread.GetValue(1).ToString + ", getdate(), 0, 0, 0, 0, " + sqlread.GetValue(3).ToString + ", '" + sqlread.GetValue(4).ToString + "','" + sqlread.GetValue(5).ToString + "','" + sqlread.GetValue(6).ToString + "', " + sqlread.GetValue(8).ToString + ", "
            query2 += "'" + observacion + "', '" + descripcionCorta + "', '" + sqlread.GetValue(9).ToString + "', " + sqlread.GetValue(12).ToString + ", "
            query2 += "" + sqlread.GetValue(13).ToString + ", '" + sqlread.GetValue(14).ToString + "', '" + sqlread.GetValue(15) + "', '" + sqlread.GetValue(16) + "', "
            query2 += "1, " + abonoConciliacion + ")"

            sqlread.Close()
            conexion.Close()
            funciones.grabaDatos(query2, sucursal)
        End If
    End Sub

    Function ObtenerCatFormaPagoById(ByVal idFormaPago As String) As String
        Dim descripcionCorta As String = "EF"
        Dim id As String

        LlenarCatalogoFormasPago()

        For Each row As DataRow In dtCatFormaPago.Rows
            id = row("id")

            If id = idFormaPago Then
                descripcionCorta = row("descripcionCorta")
                Exit For
            End If
        Next row

        Return descripcionCorta
    End Function

    Sub calcula(ByVal auxsaldo As Double)
        Dim cuenta As Int16 = 0
        Dim oTabla As New DataTable
        oTabla = funciones.creatabla(8)
        Dim bandera As Boolean = True

        Do While cuenta < gFacturas.Items.Count
            If auxsaldo > 0 Then
                Dim aFila As DataRow = oTabla.NewRow
                aFila(0) = gFacturas.Items(cuenta).Cells(0).Text
                aFila(1) = gFacturas.Items(cuenta).Cells(1).Text
                aFila(2) = gFacturas.Items(cuenta).Cells(2).Text
                aFila(3) = gFacturas.Items(cuenta).Cells(3).Text
                aFila(4) = gFacturas.Items(cuenta).Cells(4).Text
                aFila(5) = gFacturas.Items(cuenta).Cells(5).Text
                txtobserva.Text = txtobserva.Text + gFacturas.Items(cuenta).Cells(3).Text + ", "
                Dim ImporteFac As Double = gFacturas.Items(cuenta).Cells(5).Text
                'Dim apimporte As Double = txtImporte.Text
                auxsaldo = auxsaldo - ImporteFac

                If auxsaldo >= 0 Then
                    aFila(6) = ImporteFac
                    aFila(7) = "Pagar"
                Else
                    aFila(6) = ImporteFac
                    aFila(7) = "Abonar"
                End If
                oTabla.Rows.Add(aFila)

                If gFacturas.Items(cuenta).Cells(1).Text = txtNumFac.Text.Trim Then
                    bandera = False
                End If
            End If
            cuenta = cuenta + 1
        Loop
        'Dim ap2importe As Double = txtImporte.Text
        lblAuximporte.Text = auxsaldo

        If lblAuximporte.Text <= 0 Then
            'btnAgregar.Enabled = False
            lblAuximporte.Text = 0
        End If

        oTabla.Columns(0).ColumnName = "columna0"
        oTabla.TableName = "mitabla"
        gFacturas.DataMember = "mitabla"
        gFacturas.DataSource = oTabla.DefaultView
        gFacturas.DataBind()
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles Menu1.MenuItemClick
        Dim Valor As String = e.Item.Value.ToString.Trim
        Select Case Valor
            Case "0"
                Server.Transfer("Defadmon.aspx", True)
            Case "1"
                Server.Transfer("centroCostos.aspx", False)
            Case "2"
                Server.Transfer("admUsuarios.aspx", False)
            Case "3"
                Server.Transfer("camaras.aspx", False)
            Case "4"
                Server.Transfer("terapistas.aspx", False)
            Case "5"
                Server.Transfer("admRecibos.aspx", False)
            Case Else
                Server.Transfer(Valor, False)
        End Select
    End Sub

    Sub llenafolios()
        gridfolio = funciones.creadataset("select folioingreso,folioRef, codigocuentabancaria, importe, saldo, tipoPago, fechaoperacion from ingresosbancos WHERE folioRef='" + txtReferencia.Text.Trim + "'", gridfolio, "admFisio")
    End Sub
    Sub llenafoliost()
        gridfolio = funciones.creadataset("select folioingreso,folioRef, codigocuentabancaria, importe, saldo, tipoPago, fechaoperacion from ingresosbancos", gridfolio, "admFisio")
    End Sub

    Protected Sub Btnbusfol_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btnbusfol.Click
        If txtReferencia.Text = "" Then
            llenafoliost()
        Else
            llenafolios()
        End If
        hfBandera.Value = "0"
        ModalPopupExtenderfolio.Show()
        Panelfolio.Visible = True
    End Sub

    Protected Sub gridfolio_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridfolio.ItemCommand
        If e.CommandName = "cmdEditar" Then
            Dim conexion As SqlConnection = funciones.conecta("admFisio")
            Dim comando As SqlCommand = conexion.CreateCommand
            comando.CommandText = "select  FolioIngreso, folioref, CodigoCuentaBancaria, importe, saldo,tipoPago, Observaciones," & _
            " fechaOperacion,sucursal from ingresosbancos where folioingreso='" + gridfolio.Items(e.Item.ItemIndex).Cells(0).Text + "'"
            conexion.Open()
            Dim sqlread As SqlDataReader = comando.ExecuteReader

            If sqlread.Read Then
                lblfolioingreso.Text = sqlread.GetValue(0).ToString.Trim
                txtReferencia.Text = sqlread.GetValue(1).ToString.Trim
                cmbCuentas.SelectedValue = sqlread.GetValue(2).ToString.Trim
                Txtidep.Text = sqlread.GetValue(3).ToString.Trim
                lblAuximporte.Text = sqlread.GetValue(4).ToString.Trim
                cmbTipo.SelectedValue = sqlread.GetValue(5).ToString.Trim
                txtobserva.Text = sqlread.GetValue(6).ToString.Trim
                txtfecha.Text = Left(sqlread.GetValue(7).ToString.Trim, 10)
                'cmbSucursal.SelectedValue = sqlread.GetValue(8).ToString.Trim

                Dim serie As String = e.Item.ItemIndex.ToString.Trim
                serie = sqlread.GetValue(8).ToString.Trim
                Select Case serie
                    Case "SM"
                        cmbSucursal.SelectedValue = "fisiocareSM"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                    Case "CP"
                        cmbSucursal.SelectedValue = "fisiocareCP"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                    Case "ym"
                        cmbSucursal.SelectedValue = "Gym"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                    Case "HO"
                        cmbSucursal.SelectedValue = "fisiocareHO"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)


                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                    Case "CA"
                        cmbSucursal.SelectedValue = "fisiocareCA"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                    Case "AM"
                        cmbSucursal.SelectedValue = "fisiocareAM"

                        If cmbSucursal.SelectedValue <> "0" Then
                            If cmbSucursal.SelectedItem.Text = "Gym" Then
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            Else
                                cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                            End If
                        Else
                            cmbclientes.Items.Clear()
                        End If
                        gridFacturas.Visible = False
                End Select

                tempImporte.Text = sqlread.GetValue(4).ToString.Trim
                txtfecha.Enabled = False
                Txtidep.Enabled = False
                'btnAgregar.Visible = True
                'lbliabono.Visible = True
                ' txtImporte.Visible = True
                cmbCuentas.Enabled = False
                Dim dt As New DataTable
                DgFacturasCN.DataSource = dt
                DgFacturasCN.DataBind()
                llenarconciliados(txtReferencia.Text)
                ' btnAgregar.Enabled = True
            End If
            sqlread.Close()
            conexion.Close()
        Else
            If e.CommandName = "updatecf" Then

                Dim conexion As SqlConnection = funciones.conecta("admFisio")
                Dim comando As SqlCommand = conexion.CreateCommand
                Dim id As String = gridfolio.Items(e.Item.ItemIndex).Cells(0).Text

                funciones.grabaDatos("update ingresosbancos set saldo= (SELECT saldo FROM ingresosbancos WHERE FolioIngreso='" + id + "') + '" + hfsaldo.Value + "' where FolioIngreso='" + id + "'", "admFisio")

                Messagebox1.ShowMessage("Movimiento Realizado con Exito ....")

                hfsaldo.Value = ""

                comando.CommandText = "select  FolioIngreso, folioref, CodigoCuentaBancaria, importe, saldo,tipoPago, Observaciones," & _
            " fechaOperacion,sucursal from ingresosbancos where folioingreso='" + id + "'"
                conexion.Open()

                Dim sqlread As SqlDataReader = comando.ExecuteReader

                If sqlread.Read Then
                    lblfolioingreso.Text = sqlread.GetValue(0).ToString.Trim
                    txtReferencia.Text = sqlread.GetValue(1).ToString.Trim
                    cmbCuentas.SelectedValue = sqlread.GetValue(2).ToString.Trim
                    Txtidep.Text = sqlread.GetValue(3).ToString.Trim
                    lblAuximporte.Text = sqlread.GetValue(4).ToString.Trim
                    cmbTipo.SelectedValue = sqlread.GetValue(5).ToString.Trim
                    txtobserva.Text = sqlread.GetValue(6).ToString.Trim
                    txtfecha.Text = Left(sqlread.GetValue(7).ToString.Trim, 10)
                    'cmbSucursal.SelectedValue = sqlread.GetValue(8).ToString.Trim

                    Dim serie As String = e.Item.ItemIndex.ToString.Trim
                    serie = sqlread.GetValue(8).ToString.Trim
                    Select Case serie
                        Case "SM"
                            cmbSucursal.SelectedValue = "fisiocareSM"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                        Case "CP"
                            cmbSucursal.SelectedValue = "fisiocareCP"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                        Case "ym"
                            cmbSucursal.SelectedValue = "Gym"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                        Case "HO"
                            cmbSucursal.SelectedValue = "fisiocareHO"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                        Case "CA"
                            cmbSucursal.SelectedValue = "fisiocareCA"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                        Case "AM"
                            cmbSucursal.SelectedValue = "fisiocareAM"

                            If cmbSucursal.SelectedValue <> "0" Then
                                If cmbSucursal.SelectedItem.Text = "Gym" Then
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                Else
                                    cmbclientes = funciones.llenacombos(cmbclientes, "select ROW_NUMBER() over (order by paciente) as idfactura,paciente, nombre,rfc from factura where paciente  is not null and paciente <> '' and nombre  is not null and nombre <> '' and rfc is not null and rfc <> '' group by paciente,rfc,nombre order by paciente", cmbSucursal.SelectedValue)
                                End If
                            Else
                                cmbclientes.Items.Clear()
                            End If
                            gridFacturas.Visible = False
                    End Select
                    tempImporte.Text = sqlread.GetValue(4).ToString.Trim
                    txtfecha.Enabled = False
                    Txtidep.Enabled = False
                    cmbCuentas.Enabled = False
                    Dim dt As New DataTable
                    DgFacturasCN.DataSource = dt
                    DgFacturasCN.DataBind()
                    llenarconciliados(txtReferencia.Text)
                End If
                sqlread.Close()
                conexion.Close()
            End If
        End If
    End Sub

    Public Sub llenarconciliados(ByVal folio As String)
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        strsql = "select f.idCF,isnull(f.FolioIngreso,'SIN FOLIO') as FolioIngreso,f.folioRef,format(f.fecha,'dd/MM/yyyy') as fecha, f.numfactura as numfactura,f.serie as 'Serie',"
        strsql += vbNewLine & " f.razonSocial as razonsocial"
        strsql += vbNewLine & " , sum(f.abono) as 'TotalAbonado',f.movimiento as 'Movimiento' from  facturas f"
        strsql += vbNewLine & " where f.folioRef = '" & folio & "' and f.status = 'A'"
        strsql += vbNewLine & " group by f.idCF,f.FolioIngreso, f.folioRef,f.razonSocial,f.fecha, f.numfactura,f.serie, f.abono,f.movimiento order by f.idCF"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                DgFacturasCN.DataSource = dt
                DgFacturasCN.DataBind()
            End If
        Else
            Messagebox1.ShowMessage("Error: " & clsdatos.MensajeError)
        End If
    End Sub

    Protected Sub DgFacturasCN_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DgFacturasCN.ItemCommand
        If e.CommandName = "cmdEditarT" Then

            Dim id As String = DgFacturasCN.Items(e.Item.ItemIndex).Cells(0).Text
            Dim eNumfolioRef As String = DgFacturasCN.Items(e.Item.ItemIndex).Cells(2).Text
            Dim factura As String = DgFacturasCN.Items(e.Item.ItemIndex).Cells(3).Text
            Dim serie As String = DgFacturasCN.Items(e.Item.ItemIndex).Cells(4).Text
            Dim abono As Double = DgFacturasCN.Items(e.Item.ItemIndex).Cells(7).Text
            hfsaldo.Value = abono
            Dim movimiento As String = DgFacturasCN.Items(e.Item.ItemIndex).Cells(8).Text
            Dim conexion As SqlConnection = funciones.conecta("admFisio")
            Dim comando As SqlCommand = conexion.CreateCommand

            gridfolio = funciones.creadataset("select folioingreso,folioRef, codigocuentabancaria, importe, saldo, tipoPago, fechaoperacion from ingresosbancos WHERE folioRef='" + eNumfolioRef + "'", gridfolio, "admFisio")

            hfBandera.Value = "0"
            ModalPopupExtenderfolio.Show()
            Panelfolio.Visible = True

            Dim cuenta As Integer = 0
            Do While cuenta < gridfolio.Items.Count
                CType(gridfolio.Items(cuenta).Cells(8).Controls(1), Button).Visible = True
                CType(gridfolio.Items(cuenta).Cells(7).Controls(1), Button).Visible = False
                cuenta = cuenta + 1
            Loop

            If movimiento = "Abonar" Then
                'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")
            Else
                Dim Valor As String = serie

                Select Case Valor
                    Case "C"
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")
                    Case "DS"
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")
                    Case "TH"
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareSM")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case "A"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")
                    Case "HT"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")
                    Case "DC"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "fisiocareCP")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case "S"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "gym")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "gym")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case "CA"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareCA")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareCA")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case "HO"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareHO")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareHO")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case "AM"
                        funciones.grabaDatos("update factura set Pagada ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareAM")
                        funciones.grabaDatos("update factura set capCuenta ='False' where num_factura ='" + factura + "' and serie='" + serie + "'", "FisiocareAM")
                        'funciones.grabaDatos("update facturas set status='C' where idCF='" + id + "'", "admFisio")
                        funciones.grabaDatos("delete from facturas where idCF='" + id + "'", "admFisio")

                    Case Else
                        Messagebox1.ShowMessage("No Existe Serie ....")
                End Select
            End If
            llenarconciliados(txtReferencia.Text)
            conexion.Open()
        End If
    End Sub
End Class
