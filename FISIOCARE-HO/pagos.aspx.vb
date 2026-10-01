Imports System.Data.SqlClient
Partial Class pagos
    Inherits System.Web.UI.Page
    Public funciones As New miclases
    Sub llenaterminadas(ByVal condicion As String)
        gridTerminadas = funciones.creadataset("SELECT abonos.idAbono, clientes.elnombre, " & _
        "(catServicios.descripcion+'-'+catCostos.descripcion) as descripcion,abonos.facturado,abonos.num_factura," & _
        " convert(varchar(10),abonos.fecha,103) as fecha, abonos.abono, abonos.importe,abonos.idcosto,abonos.idCliente,abonos.idCita FROM abonos INNER JOIN clientes ON clientes.idCliente = " & _
        "abonos.idCliente INNER JOIN catCostos ON catCostos.idCosto = abonos.idCosto INNER JOIN catServicios " & _
        "ON catServicios.id_servicio = catCostos.id_servicio WHERE " + condicion + " fecha>='" + txtfecha3.Text.Trim + "' and " & _
        "fecha<='" + txtfecha4.Text.Trim + "' order by clientes.elnombre", gridTerminadas)
        Dim cuentaF As Integer = gridTerminadas.Items.Count
        Dim contando As Integer = 0
        Do While contando < cuentaF
            If gridTerminadas.Items(contando).Cells(6).Text = "True" Then
                gridTerminadas.Items(contando).Cells(6).Text = "Si"
                gridTerminadas.Items(contando).Cells(8).Enabled = False
            Else
                gridTerminadas.Items(contando).Cells(6).Text = "No"
            End If
            contando = contando + 1
        Loop
        If cuentaF > 0 Then
            lblaviso.Visible = False
            gridTerminadas.Visible = True
            elfiltro.Visible = True
        Else
            lblaviso.Visible = True
            gridTerminadas.Visible = False
            elfiltro.Visible = True
        End If
    End Sub
    
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            ' cmbterapias = funciones.llenacombos(cmbterapias, "select idcosto, (catservicios.descripcion+'-'+" & _
            '"catcostos.descripcion) as descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
            '"catservicios.id_servicio=catcostos.id_servicio order By catcostos.id_servicio")
            cmbpacientes = funciones.llenacombos(cmbpacientes, "select idcliente, elnombre from clientes order by elnombre")
            lblfechacita.Text = Context.Items("fecha").ToString.Trim
            Dim hoy As DateTime = DateTime.Now()
            lblfecha1.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            lblfecha2.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            txtfecha3.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha4.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            calendario.SelectedDate = txtfecha3.Text
            calendario2.SelectedDate = txtfecha4.Text
            elfiltro.Visible = False

            Try
                Dim auxi As Date = txtfecha3.Text.Trim
                Dim aux1 As Date = txtfecha4.Text.Trim
                cmbfiltro.SelectedValue = 0
                llenaterminadas("")
            Catch
                Messagebox1.ShowMessage("DATOS INCORRECTOS...")
            End Try
        End If
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub lnkconsulta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkconsulta.Click
        
    End Sub

    Protected Sub gridTerminadas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridTerminadas.ItemCommand
        Dim misfun As New miclases
        Dim resultados As String
        resultados = misfun.leerValor("SELECT iddatosfac from clientes where idcliente = " & gridTerminadas.Items(e.Item.ItemIndex).Cells(12).Text)
        'cmbterapias = funciones.llenacombos(cmbterapias, "select idcosto, (catservicios.descripcion+'-'+" & _
        '"catcostos.descripcion) as descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
        '"catservicios.id_servicio=catcostos.id_servicio where catcostos.iddatosfac = " & resultados.Trim & " order By catcostos.id_servicio")
        'If cmbterapias.Items.Count = 1 Then
        'Dim scriptStr As String = "alert('NO HAY SERVICIOS PARA AGREGAR A LA CITA, VERIFIQUE EL CLIENTE !!...');"
        'ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgBox", scriptStr, True)
        'Else
        lnkconsulta.Visible = False

        Select Case e.CommandName.ToString
            'Case "cambiar"
            '   If gridTerminadas.Items(e.Item.ItemIndex).Cells(5).Text.Trim = "No" Then
            '       elfiltro.Visible = False
            '       gridTerminadas.Visible = False
            '       tabla.Visible = True
            '       lblfecha.Text = gridTerminadas.Items(e.Item.ItemIndex).Cells(0).Text
            '       lblcliente.Text = gridTerminadas.Items(e.Item.ItemIndex).Cells(1).Text
            '       Try
            '           cmbterapias.SelectedValue = gridTerminadas.Items(e.Item.ItemIndex).Cells(7).Text
            '      Catch
            '         cmbterapias.SelectedIndex = 0
            'End Try
            '
            '                   txtabonos.Text = CType(gridTerminadas.Items(e.Item.ItemIndex).Cells(3).Controls(1), TextBox).Text
            '                  txtimporte.Text = CType(gridTerminadas.Items(e.Item.ItemIndex).Cells(4).Controls(1), TextBox).Text
            '                 lblidabono.Text = gridTerminadas.DataKeys(e.Item.ItemIndex).ToString
            '                lblaccion.Text = "MODIFICAR LA TERAPIA"
            '               lblkeAcer.Text = "1"
            '          Else
            '             Dim scriptStr As String = "alert('ESTA TERAPIA A SIDO FACTURADA Y NO SE PUEDE MODIFICAR!!...');"
            '            ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgBox", scriptStr, True)
            '   End If
            '  Case "agregar"
            '     If gridTerminadas.Items(e.Item.ItemIndex).Cells(5).Text.Trim = "No" Then
            '        elfiltro.Visible = False
            '       gridTerminadas.Visible = False
            '      tabla.Visible = True
            '        lblfecha.Text = gridTerminadas.Items(e.Item.ItemIndex).Cells(0).Text
            '        lblcliente.Text = gridTerminadas.Items(e.Item.ItemIndex).Cells(1).Text
            '        cmbterapias.SelectedValue = "0"
            '        txtabonos.Text = 0.0
            '        txtimporte.Text = 0.0
            '        lblidabono.Text = gridTerminadas.DataKeys(e.Item.ItemIndex).ToString
            '        lblaccion.Text = "AGREGAR TERAPIA A UNA CITA"
            '        lblkeAcer.Text = "2"
            '   Else
            '       Dim scriptStr As String = "alert('ESTA TERAPIA A SIDO FACTURADA Y NO SE PUEDE MODIFICAR!!...');"
            '       ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgBox", scriptStr, True)
            'End If
            Case "imprimir"
                Dim conexion As SqlConnection = funciones.conecta
                Dim comando As SqlCommand = conexion.CreateCommand
                Dim leer As SqlDataReader
                comando.CommandText = "select idcita,idcliente,fecha from abonos " & _
                "where idabono='" + gridTerminadas.DataKeys(e.Item.ItemIndex).ToString + "'"
                conexion.Open()
                leer = comando.ExecuteReader
                If leer.Read Then
                    Dim aux As DateTime = leer.GetValue(2).ToString
                    Response.Redirect("paso.aspx?idcita=" + leer.GetValue(0).ToString + "" & _
                    "&varidcliente=" + leer.GetValue(1).ToString + "&varfecha=" + String.Format("{0:dd/MM/yyyy}", aux) + "" & _
                    "&fecha=" + lblfechacita.Text.Trim + "&pagina=pagos.aspx")
                End If
                leer.Close()
                conexion.Close()
            Case "borrar"
                Dim key As String = e.Item.ItemIndex
                If gridTerminadas.Items(e.Item.ItemIndex).Cells(5).Text.Trim = "No" Then
                    Try
                        Messagebox1.ShowConfirmation("DESEAS BORRAR EL ABONO ??", key.Trim, True, True)

                        'misfun.grabaDatos("delete from abonos where idAbono='" + gridTerminadas.DataKeys(e.Item.ItemIndex).ToString + "' ")
                        'num_abonos = misfun.leerValor("select count(idCita) as numero from abonos where idcita='" + gridTerminadas.Items(e.Item.ItemIndex).Cells(13).Text.Trim + "'")
                        'If CInt(num_abonos) = 0 Then
                        ' misfun.grabaDatos("update agenda set estado = 'AGENDADO' where idCita='" + gridTerminadas.Items(e.Item.ItemIndex).Cells(13).Text.Trim + "' ")
                        ' End If

                        'Dim auxi As Date = txtfecha3.Text.Trim
                        'Dim aux1 As Date = txtfecha4.Text.Trim
                        'cmbfiltro.SelectedValue = 0
                        'llenaterminadas("")
                    Catch
                        Messagebox1.ShowMessage("OCURRIERON ERRORES DURANTE EL BORRADO...")
                    End Try
                Else
                    Dim scriptStr As String = "alert('ESTA TERAPIA A SIDO FACTURADA Y NO SE PUEDE ELIMINAR!!...');"
                    ScriptManager.RegisterStartupScript(Me, Me.GetType, "msgBox", scriptStr, True)
                End If
        End Select
        'End If
    End Sub
    Protected Sub cmbprincipal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case cmbprincipal.SelectedValue
            Case 0
                llenaterminadas("")
                contenedor_Paciente.Visible = False
                contenedor_Recibo.Visible = False
                brecibo.Text = ""
            Case 1
                llenaterminadas(" facturado='false' and ")
                contenedor_Paciente.Visible = False
                contenedor_Recibo.Visible = False
                brecibo.Text = ""
            Case 2
                llenaterminadas(" facturado='true' and ")
                contenedor_Paciente.Visible = False
                contenedor_Recibo.Visible = False
                brecibo.Text = ""
            Case 3
                contenedor_Recibo.Visible = True
                contenedor_Paciente.Visible = False
            Case 4
                contenedor_Paciente.Visible = True
                contenedor_Recibo.Visible = False
                brecibo.Text = ""
        End Select
    End Sub
    Protected Sub cmbfiltro_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Select Case cmbfiltro.SelectedValue
            Case 0
                llenaterminadas("")
            Case 1
                llenaterminadas(" facturado='false' and ")
            Case 2
                llenaterminadas(" facturado='true' and ")
        End Select
    End Sub
    Protected Sub cmbpacientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs)

        'llenaterminadas(" abonos.idCliente='" + cmbpacientes.SelectedValue.ToString.Trim + "'  and ")
        gridTerminadas = funciones.creadataset("SELECT abonos.idAbono, clientes.elnombre, " & _
        "(catServicios.descripcion+'-'+catCostos.descripcion) as descripcion,abonos.facturado,abonos.num_factura," & _
        " convert(varchar(10),abonos.fecha,103) as fecha, abonos.abono, abonos.importe,abonos.idcosto,abonos.idCliente,abonos.idCita FROM abonos INNER JOIN clientes ON clientes.idCliente = " & _
        "abonos.idCliente INNER JOIN catCostos ON catCostos.idCosto = abonos.idCosto INNER JOIN catServicios " & _
        "ON catServicios.id_servicio = catCostos.id_servicio WHERE abonos.idCliente='" + cmbpacientes.SelectedValue.ToString.Trim + "'  " & _
        "order by clientes.elnombre", gridTerminadas)
        Dim cuentaF As Integer = gridTerminadas.Items.Count
        Dim contando As Integer = 0
        Do While contando < cuentaF
            If gridTerminadas.Items(contando).Cells(6).Text = "True" Then
                gridTerminadas.Items(contando).Cells(6).Text = "Si"
                gridTerminadas.Items(contando).Cells(8).Enabled = False
            Else
                gridTerminadas.Items(contando).Cells(6).Text = "No"
            End If
            contando = contando + 1
        Loop
        If cuentaF > 0 Then
            lblaviso.Visible = False
            gridTerminadas.Visible = True
            elfiltro.Visible = True
        Else
            lblaviso.Visible = True
            gridTerminadas.Visible = False
            elfiltro.Visible = True
        End If

        contenedor_Paciente.Visible = True

    End Sub
    Protected Sub BtBuscar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btBuscar.Click

        'llenaterminadas(" abonos.idCita='" + brecibo.Text + "'  and ")
        gridTerminadas = funciones.creadataset("SELECT abonos.idAbono, clientes.elnombre, " & _
        "(catServicios.descripcion+'-'+catCostos.descripcion) as descripcion,abonos.facturado,abonos.num_factura," & _
        " convert(varchar(10),abonos.fecha,103) as fecha, abonos.abono, abonos.importe,abonos.idcosto,abonos.idCliente,abonos.idCita FROM abonos INNER JOIN clientes ON clientes.idCliente = " & _
        "abonos.idCliente INNER JOIN catCostos ON catCostos.idCosto = abonos.idCosto INNER JOIN catServicios " & _
        "ON catServicios.id_servicio = catCostos.id_servicio WHERE abonos.idCita='" + brecibo.Text + "'  " & _
        "order by clientes.elnombre", gridTerminadas)
        Dim cuentaF As Integer = gridTerminadas.Items.Count
        Dim contando As Integer = 0
        Do While contando < cuentaF
            If gridTerminadas.Items(contando).Cells(6).Text = "True" Then
                gridTerminadas.Items(contando).Cells(6).Text = "Si"
                gridTerminadas.Items(contando).Cells(8).Enabled = False
            Else
                gridTerminadas.Items(contando).Cells(6).Text = "No"
            End If
            contando = contando + 1
        Loop
        If cuentaF > 0 Then
            lblaviso.Visible = False
            gridTerminadas.Visible = True
            elfiltro.Visible = True
        Else
            lblaviso.Visible = True
            gridTerminadas.Visible = False
            elfiltro.Visible = True
        End If

        contenedor_Recibo.Visible = True
    End Sub
    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed

        'Dim misfun As New miclases
        'Dim num_abonos As String
        'Try
        'misfun.grabaDatos("delete from abonos where idAbono='" + gridTerminadas.DataKeys(Key).ToString + "' ")
        'num_abonos = misfun.leerValor("select count(idCita) as numero from abonos where idcita='" + gridTerminadas.Items(Key).Cells(13).Text.Trim + "'")
        'If CInt(num_abonos) = 0 Then
        ' misfun.grabaDatos("update agenda set estado = 'AGENDADO' where idCita='" + gridTerminadas.Items(Key).Cells(13).Text.Trim + "' ")
        'End If
        'Dim auxi As Date = txtfecha3.Text.Trim
        'Dim aux1 As Date = txtfecha4.Text.Trim
        'cmbfiltro.SelectedValue = 0
        'llenaterminadas("")
        'Catch
        ' Messagebox1.ShowMessage("OCURRIERON ERRORES DURANTE EL BORRADO...")
        ' End Try

    End Sub
    Protected Sub Messagebox1_NoChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.NoChoosed
        ' Messagebox1.ShowMessage("FALSE           DATOS INCORRECTOS FAVOR DE VERIFICAR !!! ")
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        Dim lafecha1 As Date = calendario.SelectedDate
        Dim lafecha2 As Date = calendario2.SelectedDate
        lblfecha1.Text = lafecha1.Day.ToString + " " + MonthName(lafecha1.Month)
        lblfecha2.Text = lafecha2.Day.ToString + " " + MonthName(lafecha2.Month)
        txtfecha3.Text = String.Format("{0:dd/MM/yyyy}", lafecha1)
        txtfecha4.Text = String.Format("{0:dd/MM/yyyy}", lafecha2)
        elfiltro.Visible = False

        Try
            Dim auxi As Date = txtfecha3.Text.Trim
            Dim aux1 As Date = txtfecha4.Text.Trim
            'cmbfiltro.SelectedValue = 0
            'llenaterminadas("")
            Select Case cmbprincipal.SelectedValue
                Case 0
                    llenaterminadas("")
                    contenedor_Paciente.Visible = False
                    contenedor_Recibo.Visible = False
                    brecibo.Text = ""
                Case 1
                    llenaterminadas(" facturado='false' and ")
                    contenedor_Paciente.Visible = False
                    contenedor_Recibo.Visible = False
                    brecibo.Text = ""
                Case 2
                    llenaterminadas(" facturado='true' and ")
                    contenedor_Paciente.Visible = False
                    contenedor_Recibo.Visible = False
                    brecibo.Text = ""
                Case 3
                    llenaterminadas(" abonos.idCita='" + brecibo.Text + "'  and ")
                    contenedor_Recibo.Visible = True
                    contenedor_Paciente.Visible = False
                Case 4
                    llenaterminadas(" abonos.idCliente='" + cmbpacientes.SelectedValue.ToString.Trim + "'  and ")
                    contenedor_Paciente.Visible = True
                    contenedor_Recibo.Visible = False
                    brecibo.Text = ""
            End Select
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS...")
        End Try
    End Sub

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario2.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Calendar1_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario2.VisibleMonthChanged
        ModalPopupExtender1.Show()
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

End Class
