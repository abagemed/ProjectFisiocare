
Imports System.Data.Sql
Imports System.Data
Imports System.Data.SqlClient
Partial Class admRecibos
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Public Function catCostos() As DataSet
        Dim myConnection As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
        If cmbSuc.SelectedItem.Text = "Gym" Then
            Dim ad As New SqlDataAdapter("SELECT iddatosfac as responsable,nombre as descripcion FROM catFacturas order by nombre", myConnection)

            Dim ds As New DataSet
            ad.Fill(ds, "Categories")
            Return ds
        Else
            Dim ad As New SqlDataAdapter("SELECT idresponsable as responsable,descripcion FROM catResponsables order by descripcion", myConnection)
            Dim ds As New DataSet
            ad.Fill(ds, "Categories")
            Return ds
        End If
        

    End Function

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        TxtAbono.Attributes.Add("onfocus", "if (this.value=='0') this.value='';")
        TxtAbono.Attributes.Add("onblur", "if (this.value=='') this.value='0';")
        If Not Page.IsPostBack Then
            'validacion de perfil AB 13052020
            If Session("rol") = "" Then
                Response.Redirect("Default.aspx")
            Else
                If Session("rol") = "ADMIN" Then

                Else
                    Response.Redirect("Defadmon.aspx")

                End If
            End If
           
        End If
    End Sub

    Protected Sub DropDownList1_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cuenta As Integer = 0
        Do While cuenta < gRecibos.Items.Count
            Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
            Dim oSqlAdapter As SqlDataAdapter = New SqlDataAdapter("select idcosto,descripcion from catcostos where iddatosfac='" + CType(gRecibos.Items(cuenta).Cells(1).Controls(1), DropDownList).SelectedValue + "'", conexion)
            Dim oDataset As New DataSet
            oSqlAdapter.Fill(oDataset)
            CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataSource = oDataset
            CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataValueField = "idcosto"
            CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataTextField = "descripcion"
            CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataBind()
            If CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).Items.Count > 0 Then
                CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedIndex = 0
                gRecibos.Items(cuenta).Cells(4).Text = funciones.leerValor("select costo from catCostos where idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue + "'", cmbSuc.SelectedValue)
                funciones.grabaDatos("update abonos set importe='" + gRecibos.Items(cuenta).Cells(4).Text.ToString + "', abono='" + CType(gRecibos.Items(cuenta).Cells(3).Controls(1), TextBox).Text.ToString + "'," & _
                "idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue.ToString + "' " & _
                " where idAbono='" + gRecibos.DataKeys.Item(cuenta).ToString + "'", cmbSuc.SelectedValue)
            Else
                gRecibos.Items(cuenta).Cells(4).Text = 0
            End If
            cuenta = cuenta + 1
        Loop
    End Sub
    'validacion de cambio de pago modal AB14052020
    Protected Sub DropDownList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        tipomod.Value = 1
        mpuprolTE.Show()
    End Sub
    'validacion de cambio de pago modal AB13052020
    Protected Sub ListTpago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        tipomod.Value = 3
        mpuprolTE.Show()

    End Sub
    'Guardar cambios AB13052020-14052020
    Protected Sub lkb1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkb1.Click


        Select Case tipomod.Value
            'cambiodeterapia
            Case 1
                Dim cuenta As Integer = 0
                Do While cuenta < gRecibos.Items.Count
                    gRecibos.Items(cuenta).Cells(4).Text = funciones.leerValor("select costo from catCostos where idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue + "'", cmbSuc.SelectedValue)


                    Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
                    Dim comando As SqlCommand = conexion.CreateCommand
                    Dim leer As SqlDataReader
                    comando.CommandText = "select (costo-descuentoc-apoyo) , cambioprecio,ClaveProdServ,tasaIVA, round((costo-descuentoc-apoyo)/1.16,2) AS 'subtotal',round(((costo-descuentoc-apoyo)/1.16)*0.16,2) AS 'iva',ClaveUnidad,Unidad from catcostos left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio where idcosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue + "'"
                    conexion.Open()
                    leer = comando.ExecuteReader
                    If leer.Read Then
                        Dim iva As String
                        Dim subtotal As String
                        'procedimiento de iva

                        Dim csat As String = leer.GetValue(2).ToString.Trim
                        Dim cunidad As String = leer.GetValue(6).ToString.Trim
                        Dim unidad As String = leer.GetValue(7).ToString.Trim
                        Dim tasai As String = leer.GetValue(3).ToString.Trim

                        If leer.GetValue(3).ToString.Trim = "0.160000" Then

                            subtotal = Format(CDbl(leer.GetValue(0).ToString.Trim) / CDbl(1.16), "##,##0.00")
                            iva = Format(CDbl(subtotal) * CDbl(0.16), "##,##0.0000")
                        Else

                            subtotal = leer.GetValue(0).ToString.Trim
                            iva = 0.0


                        End If



                        ''procedimiento de iva

                        'If leer.GetValue(3).ToString.Trim = "0.160000" Then
                        '    txtsubtotal.Text = CDbl(leer.GetValue(4).ToString.Trim)
                        '    txtiva.Text = CDbl(txtsubtotal.Text * 0.16)

                        'Else
                        '    txtsubtotal.Text = leer.GetValue(0).ToString.Trim
                        '    txtiva.Text = 0.0

                        'End If

                        funciones.grabaDatos("update abonos set importe='" + leer.GetValue(0).ToString.Trim + "', abono='" + CType(gRecibos.Items(cuenta).Cells(3).Controls(1), TextBox).Text.ToString + "'," & _
                   "idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue.ToString + "' " & _
                   " ,ClaveProdServ='" + csat + "', ClaveUnidad='" + cunidad + "', Unidad='" + unidad + "', importeIVA='" + iva + "', tasaIVA='" + tasai + "',subtotal='" + subtotal + "'" & _
                   " where idAbono='" + gRecibos.DataKeys.Item(cuenta).ToString + "'", cmbSuc.SelectedValue)



                    End If
                    conexion.Close()



                    funciones.grabaDatos("insert into HmModAbonos( idabono, Tipomodicacion, Observacionmodificacion, FechaModificacion, UsuarioModificacion) " & _
       "values('" + gRecibos.DataKeys.Item(cuenta).ToString + "','Cambio de tipo Terapia-Costo'," & _
       "'" + txtObservacionMod.Text.ToString + "',getdate()," & Session("idusuario") & ")", cmbSuc.SelectedValue)
                    cuenta = cuenta + 1
                Loop
                txtObservacionMod.Text = ""

            Case 2 'cambiodeabono
                Dim cuenta As Integer = 0
                Do While cuenta < gRecibos.Items.Count
                    funciones.grabaDatos("update abonos set importe='" + gRecibos.Items(cuenta).Cells(4).Text.ToString + "', abono='" + CType(gRecibos.Items(cuenta).Cells(3).Controls(1), TextBox).Text.ToString + "'," & _
                    "idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue.ToString + "' " & _
                    " where idAbono='" + gRecibos.DataKeys.Item(cuenta).ToString + "'", cmbSuc.SelectedValue)
                    funciones.grabaDatos("insert into HmModAbonos( idabono, Tipomodicacion, Observacionmodificacion, FechaModificacion, UsuarioModificacion) " & _
       "values('" + gRecibos.DataKeys.Item(cuenta).ToString + "','Cambio del importe del Abono'," & _
       "'" + txtObservacionMod.Text.ToString + "',getdate()," & Session("idusuario") & ")", cmbSuc.SelectedValue)
                    cuenta = cuenta + 1
                Loop
                txtObservacionMod.Text = ""
            Case 3 'tipodepago
                Dim cuenta As Integer = 0
                Do While cuenta < gRecibos.Items.Count
                    gRecibos.Items(cuenta).Cells(4).Text = funciones.leerValor("select costo from catCostos where idCosto='" + CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue + "'", cmbSuc.SelectedValue)

                    funciones.grabaDatos("update abonos set idpago='" + CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).SelectedValue.ToString + "'" & _
                    " where idAbono='" + gRecibos.DataKeys.Item(cuenta).ToString + "'", cmbSuc.SelectedValue)
                    funciones.grabaDatos("insert into HmModAbonos( idabono, Tipomodicacion, Observacionmodificacion, FechaModificacion, UsuarioModificacion) " & _
       "values('" + gRecibos.DataKeys.Item(cuenta).ToString + "','Cambio de Forma de Pago'," & _
       "'" + txtObservacionMod.Text.ToString + "',getdate()," & Session("idusuario") & ")", cmbSuc.SelectedValue)
                    cuenta = cuenta + 1
                Loop
                txtObservacionMod.Text = ""
            Case 4 'eliminar

                funciones.grabaDatos("insert into AbonosCancelados(idabono, idcliente, idcita, idcosto, idpago, fechaabono, abono, importe, fechaactualizacion, motivocancelacion) " & _
               "values('" + txtidabono.Text.Trim + "','" + txtidcliente.Text + "','" + txtRecibo.Text.Trim + "','" + txtidcosto.Text + "','" + txtidpago.Text + "','" + txtfecha.Text + "','" + txtabonoc.Text + "'," & _
               "'" + txtimportec.Text + "',getdate(),'" + txtmotivo.Text.ToString + "')", cmbSuc.SelectedValue)

                funciones.grabaDatos("delete from abonos where idAbono='" + txtidabono.Text + "'", cmbSuc.SelectedValue)

                funciones.grabaDatos("insert into HmModAbonos( idabono, Tipomodicacion, Observacionmodificacion, FechaModificacion, UsuarioModificacion) " & _
       "values('" + txtidabono.Text.Trim + "','Eliminacion de Pago'," & _
       "'" + txtObservacionMod.Text.ToString + "',getdate()," & Session("idusuario") & ")", cmbSuc.SelectedValue)

                gRecibos = funciones.LLenaGrid("select idAbono,convert(varchar, fecha, 103) as fecha,abono,importe from abonos where idcita='" + txtRecibo.Text + "'", gRecibos, cmbSuc.SelectedValue.ToString)
                Dim cuenta As Integer = 0

                Do While cuenta < gRecibos.Items.Count

                    Dim idAbono As String = gRecibos.DataKeys.Item(cuenta).ToString
                    Dim idCosto(,) As String = funciones.leerValores("select idcosto,idcliente from abonos where idAbono='" + idAbono + "'", 2, cmbSuc.SelectedValue.ToString)
                    Dim idPago As String = funciones.leerValor("select idpago from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue.ToString)
                    Dim idResponsable As String = funciones.leerValor("select iddatosfac from catCostos where idCosto='" + idCosto(0, 0) + "'", cmbSuc.SelectedValue.ToString)
                    CType(gRecibos.Items(cuenta).Cells(1).Controls(1), DropDownList).SelectedValue = idResponsable

                    Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
                    Dim oSqlAdapter As SqlDataAdapter = New SqlDataAdapter("select idcosto,descripcion from catcostos where iddatosfac='" + idResponsable + "'", conexion)
                    Dim oDataset As New DataSet
                    oSqlAdapter.Fill(oDataset)
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataSource = oDataset
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataValueField = "idcosto"
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataTextField = "descripcion"
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataBind()
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue = idCosto(0, 0)

                    Dim conexion2 As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
                    Dim oSqlAdapter2 As SqlDataAdapter = New SqlDataAdapter("select idpago,descripcion from catpagos", conexion2)
                    Dim oDataset2 As New DataSet
                    oSqlAdapter2.Fill(oDataset2)
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataSource = oDataset2
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataValueField = "idpago"
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataTextField = "descripcion"
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataBind()
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).SelectedValue = idPago
                    cuenta = cuenta + 1
                Loop

                'If gRecibos.Items.Count = 0 Then
                '    funciones.grabaDatos("delete from agenda where idCita='" + txtRecibo.Text + "'", cmbSuc.SelectedValue)
                '    End If

                txtObservacionMod.Text = ""
        End Select
       

    End Sub
    'Guardar nuevos cambios Agregar Abonos 14052020
    Protected Sub lkb2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkb2.Click
        Dim importes As String = Replace(Replace(lblImporte.Text, "$", ""), ",", "")
        funciones.grabaDatos("insert into abonos(idcliente,fecha,abono,importe,idcita,idcosto,iddoctor,idpago, ClaveProdServ, ClaveUnidad, Unidad, importeIVA, tasaIVA,subtotal,NoIdentificacion, Descuento, cantidad, fechaactualizacion,Objetoimp) " & _
        "values('" + hfidcliente.Value.ToString + "','" + lblfecha.Text + "','" + TxtAbono.Text.ToString + "'," & _
        "'" + importes + "','" + txtRecibo.Text.ToString + "','" + cmbTipoterapiaA.SelectedValue.ToString + "'," & _
        "'" + hfiddoctor.Value.ToString.Trim + "','EF','" + txtcsat.Text.ToString + "','" + txtcunidad.Text.ToString + "','" + txtunidad.Text.ToString + "','" + txtiva.Text.ToString + "','" + txttasai.Text.ToString + "','" + txtsubtotal.Text.ToString + "', " & _
        "0,0,1,getdate(),'02')", cmbSuc.SelectedValue)


        Nidabono.Value = funciones.leerValor("select top 1 idabono from abonos order by idabono desc", cmbSuc.SelectedValue)

        funciones.grabaDatos("insert into HmModAbonos( idabono, Tipomodicacion, Observacionmodificacion, FechaModificacion, UsuarioModificacion) " & _
       "values('" + Nidabono.Value + "','Agrego nuevo pago'," & _
       "'" + txtobservacionAB.Text.ToString + "',getdate()," & Session("idusuario") & ")", cmbSuc.SelectedValue)
        TxtAbono.Text = 0
        txtobservacionAB.Text = ""
    End Sub
    'validacion y modificacion de proceso de la 1 consulta AB14052020
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim bidcita As String = funciones.leerValor("select idcita from abonos where idcita='" + txtRecibo.Text + "'", cmbSuc.SelectedValue.ToString)


        If bidcita = 0 Then
            Messagebox1.ShowMessage("No se encontro el no. de recibo ...")
        Else
            gRecibos = funciones.LLenaGrid("select idAbono,convert(varchar, fecha, 103) as fecha,abono,importe from abonos where idcita='" + txtRecibo.Text + "' and facturado='false'", gRecibos, cmbSuc.SelectedValue.ToString)
            If gRecibos.Items.Count > 0 Then
                Panel1.Visible = False
                Panel2.Visible = True
                Dim cuenta As Integer = 0
                Dim idCliente As Integer = 0
                Do While cuenta < gRecibos.Items.Count

                    Dim idAbono As String = gRecibos.DataKeys.Item(cuenta).ToString
                    Dim idCosto(,) As String = funciones.leerValores("select idcosto,idcliente from abonos where idAbono='" + idAbono + "'", 2, cmbSuc.SelectedValue.ToString)
                    Dim idPago As String = funciones.leerValor("select idpago from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue.ToString)
                    Dim idResponsable As String = funciones.leerValor("select iddatosfac from catCostos where idCosto='" + idCosto(0, 0) + "'", cmbSuc.SelectedValue.ToString)
                    CType(gRecibos.Items(cuenta).Cells(1).Controls(1), DropDownList).SelectedValue = idResponsable

                    Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
                    Dim oSqlAdapter As SqlDataAdapter = New SqlDataAdapter("select idcosto,descripcion from catcostos where iddatosfac='" + idResponsable + "'", conexion)
                    Dim oDataset As New DataSet
                    oSqlAdapter.Fill(oDataset)
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataSource = oDataset
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataValueField = "idcosto"
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataTextField = "descripcion"
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataBind()
                    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue = idCosto(0, 0)

                    Dim conexion2 As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
                    Dim oSqlAdapter2 As SqlDataAdapter = New SqlDataAdapter("select idpago,descripcion from catpagos", conexion2)
                    Dim oDataset2 As New DataSet
                    oSqlAdapter2.Fill(oDataset2)
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataSource = oDataset2
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataValueField = "idpago"
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataTextField = "descripcion"
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).DataBind()
                    CType(gRecibos.Items(cuenta).Cells(5).Controls(1), DropDownList).SelectedValue = idPago

                    idCliente = idCosto(1, 0)
                    cuenta = cuenta + 1
                Loop
                lblPaciente.Text = funciones.leerValor("select nombre+' '+paterno+' '+materno from clientes where idcliente='" + idCliente.ToString + "'", cmbSuc.SelectedValue)
            End If

        End If


       
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        Panel1.Visible = True
        Panel2.Visible = False
    End Sub

    Protected Sub TextBox2_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        'validacion del cambio de costo de abono AB14052020
        tipomod.Value = 2
        mpuprolTE.Show()
    End Sub

    Protected Sub gRecibos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gRecibos.ItemCommand
        'Dim auxDatakey As String = gRecibos.DataKeys.Item(e.Item.ItemIndex).ToString
        'funciones.grabaDatos("delete from abonos where idAbono='" + auxDatakey + "'", cmbSuc.SelectedValue)

        'gRecibos = funciones.LLenaGrid("select idAbono,convert(varchar, fecha, 103) as fecha,abono,importe from abonos where idcita='" + txtRecibo.Text + "'", gRecibos, cmbSuc.SelectedValue.ToString)
        'Dim cuenta As Integer = 0

        'Do While cuenta < gRecibos.Items.Count

        '    Dim idAbono As String = gRecibos.DataKeys.Item(cuenta).ToString
        '    Dim idCosto(,) As String = funciones.leerValores("select idcosto,idcliente from abonos where idAbono='" + idAbono + "'", 2, cmbSuc.SelectedValue.ToString)
        '    Dim idResponsable As String = funciones.leerValor("select iddatosfac from catCostos where idCosto='" + idCosto(0, 0) + "'", cmbSuc.SelectedValue.ToString)
        '    CType(gRecibos.Items(cuenta).Cells(1).Controls(1), DropDownList).SelectedValue = idResponsable

        '    Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
        '    Dim oSqlAdapter As SqlDataAdapter = New SqlDataAdapter("select idcosto,descripcion from catcostos where iddatosfac='" + idResponsable + "'", conexion)
        '    Dim oDataset As New DataSet
        '    oSqlAdapter.Fill(oDataset)
        '    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataSource = oDataset
        '    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataValueField = "idcosto"
        '    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataTextField = "descripcion"
        '    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).DataBind()
        '    CType(gRecibos.Items(cuenta).Cells(2).Controls(1), DropDownList).SelectedValue = idCosto(0, 0)

        '    cuenta = cuenta + 1
        'Loop

        'If gRecibos.Items.Count = 0 Then
        '    funciones.grabaDatos("delete from agenda where idCita='" + txtRecibo.Text + "'", cmbSuc.SelectedValue)
        'End If
        txtnumcancelar.Text = txtRecibo.Text
        txtidabono.Text = gRecibos.DataKeys.Item(e.Item.ItemIndex).ToString
        Dim idAbono As String = gRecibos.DataKeys.Item(e.Item.ItemIndex).ToString
        txtidcliente.Text = funciones.leerValor("select idcliente from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        txtidcosto.Text = funciones.leerValor("select idcosto from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        txtabonoc.Text = funciones.leerValor("select abono from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        txtimportec.Text = funciones.leerValor("select importe from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        txtfecha.Text = funciones.leerValor("select convert(varchar, fecha, 103) as fecha from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        txtidpago.Text = funciones.leerValor("select idpago from abonos where idAbono='" + idAbono + "'", cmbSuc.SelectedValue)
        tipomod.Value = 4
        mpuprolTE.Show()
        'ModalPopupExtender3.Show()
    End Sub
    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click





    End Sub
    Protected Sub btnAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.Click


        Dim bidcita As String = funciones.leerValor("select idcita from abonos where idcita='" + txtRecibo.Text + "'", cmbSuc.SelectedValue.ToString)


        If bidcita = 0 Then
            Messagebox1.ShowMessage("No se encontro el no. de recibo ...")
        Else
            Dim resultados(,) = funciones.leerValores("select idCliente,convert(varchar(10),fecha,103) as fecha,iddoctor,idcosto from abonos where idcita='" + txtRecibo.Text + "'", 4, cmbSuc.SelectedValue)
            lblfecha.Text = resultados(1, 0)
            lblcliente.Text = funciones.leerValor("select elnombre from clientes where idcliente='" + resultados(0, 0) + "'", cmbSuc.SelectedValue)
            hfiddoctor.Value = resultados(2, 0)
            hfidcliente.Value = resultados(0, 0)

            If cmbSuc.SelectedItem.Text = "Gym" Then
                cmbCatCostoA = funciones.llenacombos(cmbCatCostoA, "SELECT iddatosfac as responsable,nombre as descripcion FROM catFacturas order by nombre", cmbSuc.SelectedValue)
            Else
                cmbCatCostoA = funciones.llenacombos(cmbCatCostoA, "SELECT idresponsable,descripcion FROM catResponsables order by descripcion", cmbSuc.SelectedValue)
            End If



            cmbCatCostoA.SelectedValue = funciones.leerValor("select iddatosfac from catcostos where idCosto='" + resultados(3, 0) + "'", cmbSuc.SelectedValue)
            cmbTipoterapiaA = funciones.llenacombos(cmbTipoterapiaA, "select idcosto,descripcion from catcostos where iddatosfac='" + cmbCatCostoA.SelectedValue + "'", cmbSuc.SelectedValue)
            lblImporte.Text = 0
            mpagregarabono.Show()
        End If
        'Try

        '    'ModalPopupExtender1.Show()
        '    Dim resultados(,) = funciones.leerValores("select idCliente,convert(varchar(10),fecha,103) as fecha,iddoctor,idcosto from abonos where idcita='" + txtRecibo.Text + "'", 4, cmbSuc.SelectedValue)
        '    lblfecha.Text = resultados(1, 0)
        '    lblcliente.Text = funciones.leerValor("select elnombre from clientes where idcliente='" + resultados(0, 0) + "'", cmbSuc.SelectedValue)
        '    hfiddoctor.Value = resultados(2, 0)
        '    hfidcliente.Value = resultados(0, 0)

        '    cmbCatCostoA = funciones.llenacombos(cmbCatCostoA, "SELECT idresponsable,descripcion FROM catResponsables order by descripcion", cmbSuc.SelectedValue)
        '    cmbCatCostoA.SelectedValue = funciones.leerValor("select iddatosfac from catcostos where idCosto='" + resultados(3, 0) + "'", cmbSuc.SelectedValue)
        '    cmbTipoterapiaA = funciones.llenacombos(cmbTipoterapiaA, "select idcosto,descripcion from catcostos where iddatosfac='" + cmbCatCostoA.SelectedValue + "'", cmbSuc.SelectedValue)
        '    lblImporte.Text = 0
        '    mpagregarabono.Show()
        'Catch
        '    Messagebox1.ShowMessage("No se encontro el no. de recibo ...")
        'End Try
    End Sub

    Protected Sub cmbCatCostoA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCatCostoA.SelectedIndexChanged
        cmbTipoterapiaA = funciones.llenacombos(cmbTipoterapiaA, "select idcosto,descripcion from catcostos where iddatosfac='" + cmbCatCostoA.SelectedValue + "'", cmbSuc.SelectedValue)
        mpagregarabono.Show()
    End Sub

    Protected Sub cmbTipoterapiaA_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoterapiaA.SelectedIndexChanged
        Dim importe As Double = funciones.leerValor("select costo from catcostos where idcosto='" + cmbTipoterapiaA.SelectedValue + "'", cmbSuc.SelectedValue)
        lblImporte.Text = importe.ToString("C")

          Dim conexion As SqlConnection = funciones.conecta(cmbSuc.SelectedValue.ToString)
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select (costo-descuentoc-apoyo) , cambioprecio,ClaveProdServ,tasaIVA, round((costo-descuentoc-apoyo)/1.16,2) AS 'subtotal',round(((costo-descuentoc-apoyo)/1.16)*0.16,2) AS 'iva',ClaveUnidad,Unidad from catcostos left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio where idcosto='" + cmbTipoterapiaA.SelectedValue.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lblImporte.Text = leer.GetValue(0).ToString.Trim
            TxtAbono.Text = leer.GetValue(0).ToString.Trim
            txtcsat.Text = leer.GetValue(2).ToString.Trim
            txtcunidad.Text = leer.GetValue(6).ToString.Trim
            txtunidad.Text = leer.GetValue(7).ToString.Trim
            txttasai.Text = leer.GetValue(3).ToString.Trim
            If leer.GetValue(1).ToString.Trim = "True" Then

                TxtAbono.ReadOnly = False
                txtcsat.ReadOnly = True
            Else

                TxtAbono.ReadOnly = True
                txtcsat.ReadOnly = True
            End If
            'procedimiento de iva

            If leer.GetValue(3).ToString.Trim = "0.160000" Then

                txtsubtotal.Text = CDbl(leer.GetValue(4).ToString.Trim)
                ' txtiva.Text = CDbl(leer.GetValue(5).ToString.Trim)
                txtiva.Text = CDbl(txtsubtotal.Text * 0.16)
            Else
                txtsubtotal.Text = leer.GetValue(0).ToString.Trim
                txtiva.Text = 0.0

            End If


        End If
        conexion.Close()





        mpagregarabono.Show()
    End Sub

    'Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
    '    Dim importes As String = Replace(Replace(lblImporte.Text, "$", ""), ",", "")
    '    funciones.grabaDatos("insert into abonos(idcliente,fecha,abono,importe,idcita,idcosto,iddoctor,idpago) " & _
    '    "values('" + hfidcliente.Value.ToString + "','" + lblfecha.Text + "','" + TxtAbono.Text.ToString + "'," & _
    '    "'" + importes + "','" + txtRecibo.Text.ToString + "','" + cmbTipoterapiaA.SelectedValue.ToString + "'," & _
    '    "'" + hfiddoctor.Value.ToString.Trim + "','EF')", cmbSuc.SelectedValue)
    '    TxtAbono.Text = 0
    'End Sub
End Class
