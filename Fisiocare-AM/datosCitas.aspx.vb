Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Imports Microsoft.VisualBasic
Imports System

Partial Class datosCitas
    Inherits System.Web.UI.Page
    Dim FUNCIONES As New miclases
    Public mireporte As New ReportDocument
    Public clases As New miclases
    Sub llenaDatos(ByVal idCita As String)
        Dim misfunciones As New miclases
        Dim conexion As SqlConnection = misfunciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select clientes.paterno,clientes.materno,clientes.nombre,agenda.fecha," & _
        "clientes.adeudo, clientes.idcliente,agenda.idDoctor,agenda.iddoctorc, agenda.idtconsmed,clientes.idCenCostos,clientes.habCortesia,agenda.idlatencion from agenda inner join " & _
        "clientes on clientes.idcliente=agenda.idcliente where agenda.idCita='" + idCita + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lblNombre.Text = leer.GetValue(2).ToString.Trim + " " + leer.GetValue(0).ToString.Trim + " " + leer.GetValue(1).ToString.Trim
            Dim hoy As DateTime = leer.GetValue(3).ToString.Trim
            lblfechaNom.Text = hoy.Day.ToString + " de " + MonthName(hoy.Month) + " del " + hoy.Year.ToString
            TxtFechaCita.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            'txtfecha.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            'txtSaldoA.Text = leer.GetValue(4).ToString.Trim
            'txtSaldoN.Text = txtSaldoA.Text
            lblIdcliente.Text = leer.GetValue(5).ToString.Trim
            tempDoc.Value = leer.GetValue(6).ToString.Trim
            tempDocc.Value = leer.GetValue(7).ToString.Trim
            temptipocm.Value = leer.GetValue(8).ToString.Trim
            tempdatosF.Value = leer.GetValue(9).ToString.Trim
            hfCortesias.Value = leer.GetValue(10).ToString.Trim
            templatencion.value = leer.GetValue(11).ToString.Trim
        End If
        leer.Close()
        conexion.Close()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtabono.Attributes.Add("onfocus", "if (this.value=='0') this.value='';")
        txtabono.Attributes.Add("onblur", "if (this.value=='') this.value='0'")
        If Not (Page.IsPostBack) Then
            'realizado.Visible = True
            'cancelado.Visible = False
            'misbotones.Visible = True
            'cmbTipoterapia.Visible = True
            'chkcierre.Visible = True
            'modatos.Visible = False
            lblIdcita.Text = Context.Items("elidCita").ToString.Trim
            lblrecibo.Text = lblIdcita.Text.Trim
            'txtfecha.Text = Context.Items("varfecha").ToString.Trim
            lblposicion.Text = Context.Items("posicion").ToString.Trim
            lblhorario.Text = Context.Items("idhorario").ToString.Trim
            lblduracion.Text = Context.Items("duracion").ToString.Trim
            llenaDatos(Context.Items("elidCita").ToString.Trim)
            'ViewState("micondicion") = Context.Items("condicion").ToString.Trim
            lblturno.Text = Context.Items("turno").ToString.Trim
            Dim misfunciones As New miclases
            realizado.Visible = True
            cancelado.Visible = False
            misbotones.Visible = True
            cmbTipoterapia.Visible = True
            chkcierre.Visible = True
            modatos.Visible = False
            Dim condicion As String = " and idResponsable<>'111'"
            If hfCortesias.Value = "True" Then
                condicion = ""
            End If
            cmbcostos = misfunciones.llenacombos(cmbcostos, "select idResponsable , descripcion from catResponsables where activo='true' " + condicion + " order by descripcion")
            If tempdatosF.Value.Trim = "" Then
                tempdatosF.Value = 0
            End If
            cmbcostos.SelectedValue = tempdatosF.Value.Trim
            'cmbclientes.Enabled = False

            cmbTipoterapia = misfunciones.llenacombos(cmbTipoterapia, "select idcosto,catServicios.descripcion+ ' - ' +substring(catCostos.descripcion,Charindex('$',catCostos.descripcion),50) as descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
             "catservicios.id_servicio=catcostos.id_servicio where catcostos.activo='true' and catcostos.iddatosfac = " & tempdatosF.Value.Trim & " order By catcostos.id_servicio")

            cmbDoctores = misfunciones.llenacombos(cmbDoctores, "select ID,(nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre,paterno,materno")
            If tempDoc.Value.Trim = "" Then
                tempDoc.Value = 0
            End If
            cmbDoctores.SelectedValue = tempDoc.Value.Trim

            cmbDoctorC = misfunciones.llenacombos(cmbDoctorC, "select iddoctorc,(nombre+' '+apaterno+' '+amaterno) as elnombre from catDoctorC order by nombre,apaterno,amaterno")
            If tempDocc.Value.Trim = "" Then
                tempDocc.Value = 0
            End If
            cmbDoctorC.SelectedValue = tempDocc.Value.Trim

            cmbTipoCM = misfunciones.llenacombos(cmbTipoCM, "select idtconsmed,tdescripcion from catTipoCM order by tdescripcion")
            If temptipocm.Value.Trim = "" Then
                temptipocm.Value = 0
            End If
            cmbTipoCM.SelectedValue = temptipocm.Value.Trim

            cmbPago = misfunciones.llenacombos(cmbPago, "select idPago,descripcion from catPagos where activo = 'true' order by descripcion")
            cmbPago.SelectedValue = "EF"

        End If




        If lblposicion.Text < 9 Then

            cmbDoctorC.Visible = False
            lbldocc.Visible = False
            cmbTipoCM.Visible = False
            lbltipocm.Visible = False

        End If
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Dim misfunciones As New miclases
        realizado.Visible = True
        cancelado.Visible = False
        misbotones.Visible = True
        cmbTipoterapia.Visible = True
        chkcierre.Visible = True
        modatos.Visible = False
        Dim condicion As String = " and idResponsable<>'111'"
        If hfCortesias.Value = "True" Then
            condicion = ""
        End If
        cmbcostos = misfunciones.llenacombos(cmbcostos, "select idResponsable , descripcion from catResponsables where activo='true' " + condicion + " order by descripcion")
        If tempdatosF.Value.Trim = "" Then
            tempdatosF.Value = 0
        End If
        cmbcostos.SelectedValue = tempdatosF.Value.Trim
        'cmbclientes.Enabled = False

        cmbTipoterapia = misfunciones.llenacombos(cmbTipoterapia, "select idcosto, catcostos.descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
         "catservicios.id_servicio=catcostos.id_servicio where catcostos.activo='true' and catcostos.iddatosfac = " & tempdatosF.Value.Trim & " order By catcostos.id_servicio")

        cmbDoctores = misfunciones.llenacombos(cmbDoctores, "select ID,(nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre,paterno,materno")
        If tempDoc.Value.Trim = "" Then
            tempDoc.Value = 0
        End If
        cmbDoctores.SelectedValue = tempDoc.Value.Trim

        cmbDoctorC = misfunciones.llenacombos(cmbDoctorC, "select iddoctorc,(nombre+' '+apaterno+' '+amaterno) as elnombre from catDoctorC order by nombre,apaterno,amaterno")
        If tempDocc.Value.Trim = "" Then
            tempDocc.Value = 0
        End If
        cmbDoctorC.SelectedValue = tempDocc.Value.Trim

        cmbTipoCM = misfunciones.llenacombos(cmbTipoCM, "select idtconsmed,tdescripcion from catTipoCM order by tdescripcion")
        If temptipocm.Value.Trim = "" Then
            temptipocm.Value = 0
        End If
        cmbTipoCM.SelectedValue = temptipocm.Value.Trim

        cmbPago = misfunciones.llenacombos(cmbPago, "select idPago,descripcion from catPagos where activo = 'true' order by descripcion")
        cmbPago.SelectedValue = "EF"

    End Sub
    Protected Sub btmodatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmodatos.Click
        Dim misfunciones As New miclases
        realizado.Visible = False
        modatos.Visible = True
        cancelado.Visible = False
        misbotones.Visible = False
        cmbTipoterapia.Visible = False
        chkcierre.Visible = False

        cmblatencion = misfunciones.llenacombos(cmblatencion, "select idlatencion,atdescripcion from catLAtencion order by idla")
        If templatencion.Value.Trim = "" Then
            templatencion.Value = 0
        End If
        cmblatencion.SelectedValue = templatencion.Value.Trim
    End Sub
    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        realizado.Visible = False
        cancelado.Visible = True
        misbotones.Visible = True
        chkcierre.Visible = True
        modatos.Visible = False
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub lnkGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGrabar.Click
        Dim misfunciones As New miclases
        Dim i As Integer = 0
        Dim cadena, cad As String
        Dim bandera As String
        Dim conexion As SqlConnection = FUNCIONES.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader


        If cancelado.Visible = True Then
            misfunciones.grabaDatos("insert into cancelaciones(idcita,cancelacion,fecha) " & _
            "values('" + lblIdcita.Text.Trim + "','" + txtcancelacion.Value.Trim + "','" + TxtFechaCita.Text.Trim + "')")

            If chkcierre.Checked Then
                Dim auxdtFinal As String = misfunciones.leerValor("select fecha from agenda where idcliente='" + lblIdcliente.Text.Trim + "' and estado='REALIZADO' order by fecha desc")
                auxdtFinal = Left(auxdtFinal, 10)
                misfunciones.grabaDatos("update agenda set estado='CANCELADO' , dtFinal ='" + auxdtFinal + "' where idCita='" + lblIdcita.Text.Trim + "'; " & _
                "update clientes set activo='False', dtFinal ='" + auxdtFinal + "' where idCliente='" + lblIdcliente.Text.Trim + "';")
            Else
                misfunciones.grabaDatos("update agenda set estado='CANCELADO' where idCita='" + lblIdcita.Text.Trim + "';")
            End If
            '------++graba en la replica local---------------
            'misfunciones.grabaDatosLocal("update agenda set estado='CANCELADO' where idCita='" + lblIdcita.Text.Trim + "';")
            'misfunciones.grabaDatosLocal("insert into cancelaciones(idcita,cancelacion,fecha) " & _
            '"values('" + lblIdcita.Text.Trim + "','" + txtcancelacion.Value.Trim + "','" + TxtFechaCita.Text.Trim + "')")
            '------------------------------------------------

            Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
            Server.Transfer("default.aspx", False)
        End If
        If realizado.Visible = True Then
            Dim cuenta As Int16 = gridPagos.Items.Count
            Dim auxA As Double = 0.0
            Dim auxI As Double = 0.0
            Dim auxiAbono As Double
            Dim auxiImporte, auxiMonto, auxivaMonto As Double
            Dim ligaCoaseguro As Integer = 0

            comando.CommandText = "select idCita from abonos where idCita='" + lblIdcita.Text.Trim + "'"
            conexion.Open()
            leer = comando.ExecuteReader
            If leer.Read Then
                Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
                Server.Transfer("default.aspx", False)
                Exit Sub

            End If
            conexion.Close()





            If cuenta > 0 Then
                Dim contador As Int16 = 0
                Do While contador < cuenta
                    auxiAbono = gridPagos.Items(contador).Cells(3).Text.Trim
                    auxiImporte = gridPagos.Items(contador).Cells(4).Text.Trim

                    If CType(gridPagos.Items(contador).Cells(5).Controls(1), CheckBox).Checked Then
                        auxiMonto = gridPagos.Items(contador).Cells(6).Text.Trim


                        If gridPagos.Items(contador).Cells(10).Text.Trim = 0 Then
                            auxivaMonto = 0
                        Else
                            auxivaMonto = CDbl(gridPagos.Items(contador).Cells(6).Text.Trim * 0.16)
                        End If



                        'comando.CommandText = "insert into abonos(idcliente,fecha,abono,idCita,importe,idcosto,idDoctor,idPago,coaseguro,iddoctorc,idtconsmed,ClaveProdServ, ClaveUnidad, Unidad, importeIVA, tasaIVA, NoIdentificacion, Descuento, cantidad,fechaactualizacion) " & _
                        '"values('" + lblIdcliente.Text.Trim + "','" + TxtFechaCita.Text.Trim + "'," & _
                        '"'" + auxiAbono.ToString.Trim + "','" + lblIdcita.Text.Trim + "','" + auxiMonto.ToString.Trim + "'," & _
                        '"'" + gridPagos.DataKeys(contador).ToString.Trim + "','" + cmbDoctores.SelectedValue.Trim + "'," & _
                        '"'" + gridPagos.Items(contador).Cells(1).Text.Trim + "','true','" + cmbDoctorC.SelectedValue.Trim + "','" + cmbTipoCM.SelectedValue.Trim + "','" + gridPagos.Items(contador).Cells(8).Text.Trim + "','E48','Unidad de Servicio','0','0.000000','0','0','" + gridPagos.Items(contador).Cells(7).Text.Trim + "',getdate());SELECT @idAbono= SCOPE_IDENTITY() from clientes"
                        'comando.Parameters.Add(New SqlParameter("idAbono", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
                        'conexion.Open()
                        comando.CommandText = "insert into abonos(idcliente,fecha,abono,idCita,importe,idcosto,idDoctor,idPago,coaseguro,iddoctorc,idtconsmed,ClaveProdServ, ClaveUnidad, Unidad, importeIVA, tasaIVA, NoIdentificacion, Descuento, cantidad,fechaactualizacion,subtotal,Objetoimp) " & _
                        "values('" + lblIdcliente.Text.Trim + "','" + TxtFechaCita.Text.Trim + "'," & _
                        "'" + auxiAbono.ToString.Trim + "','" + lblIdcita.Text.Trim + "','" + auxiMonto.ToString.Trim + "'," & _
                        "'" + gridPagos.DataKeys(contador).ToString.Trim + "','" + cmbDoctores.SelectedValue.Trim + "'," & _
                        "'" + gridPagos.Items(contador).Cells(1).Text.Trim + "','true','" + cmbDoctorC.SelectedValue.Trim + "','" + cmbTipoCM.SelectedValue.Trim + "','" + gridPagos.Items(contador).Cells(8).Text.Trim + "','" + gridPagos.Items(contador).Cells(11).Text.Trim + "','" + gridPagos.Items(contador).Cells(12).Text.Trim + "','" + auxivaMonto.ToString.Trim + "','" + gridPagos.Items(contador).Cells(13).Text.Trim + "','0','0','" + gridPagos.Items(contador).Cells(7).Text.Trim + "',getdate(),'" + auxiMonto.ToString.Trim + "','02');SELECT @idAbono= SCOPE_IDENTITY() from clientes"
                        comando.Parameters.Add(New SqlParameter("idAbono", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
                        conexion.Open()
                        comando.ExecuteScalar()
                        ligaCoaseguro = comando.Parameters("idAbono").Value
                        conexion.Close()
                        conexion.Dispose()
                        auxiAbono = 0.0

                        misfunciones.grabaDatos("update agenda set idDoctorc='" & cmbDoctorC.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")
                        misfunciones.grabaDatos("update agenda set idtconsmed='" & cmbTipoCM.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

                    End If

                    'misfunciones.grabaDatos("insert into abonos(idcliente,fecha,abono,idCita,importe,idcosto,idDoctor,idPago,observacion,ligaAbono,monto,iddoctorc,idtconsmed,ClaveProdServ, ClaveUnidad, Unidad, importeIVA, tasaIVA, NoIdentificacion, Descuento, cantidad,fechaactualizacion) " & _
                    '"values('" + lblIdcliente.Text.Trim + "','" + TxtFechaCita.Text.Trim + "'," & _
                    '"'" + auxiAbono.ToString.Trim + "','" + lblIdcita.Text.Trim + "','" + auxiImporte.ToString.Trim + "'," & _
                    '"'" + gridPagos.DataKeys(contador).ToString.Trim + "','" + cmbDoctores.SelectedValue.Trim + "'," & _
                    '"'" + gridPagos.Items(contador).Cells(1).Text.Trim + "','" + gridPagos.Items(contador).Cells(2).Text.Trim + "'," & _
                    '"'" + ligaCoaseguro.ToString + "','" + auxiMonto.ToString + "','" + cmbDoctorC.SelectedValue.Trim + "','" + cmbTipoCM.SelectedValue.Trim + "','" + gridPagos.Items(contador).Cells(8).Text.Trim + "','E48','Unidad de Servicio','0','0.000000','0','0','" + gridPagos.Items(contador).Cells(7).Text.Trim + "',getdate())")
                    misfunciones.grabaDatos("insert into abonos(idcliente,fecha,abono,idCita,importe,idcosto,idDoctor,idPago,observacion,ligaAbono,monto,iddoctorc,idtconsmed,ClaveProdServ, ClaveUnidad, Unidad, importeIVA, tasaIVA, NoIdentificacion, Descuento, cantidad,fechaactualizacion,subtotal,Objetoimp) " & _
                    "values('" + lblIdcliente.Text.Trim + "','" + TxtFechaCita.Text.Trim + "'," & _
                    "'" + auxiAbono.ToString.Trim + "','" + lblIdcita.Text.Trim + "','" + auxiImporte.ToString.Trim + "'," & _
                    "'" + gridPagos.DataKeys(contador).ToString.Trim + "','" + cmbDoctores.SelectedValue.Trim + "'," & _
                    "'" + gridPagos.Items(contador).Cells(1).Text.Trim + "','" + gridPagos.Items(contador).Cells(2).Text.Trim + "'," & _
                    "'" + ligaCoaseguro.ToString + "','" + auxiMonto.ToString + "','" + cmbDoctorC.SelectedValue.Trim + "','" + cmbTipoCM.SelectedValue.Trim + "','" + gridPagos.Items(contador).Cells(8).Text.Trim + "','" + gridPagos.Items(contador).Cells(11).Text.Trim + "','" + gridPagos.Items(contador).Cells(12).Text.Trim + "','" + gridPagos.Items(contador).Cells(10).Text.Trim + "','" + gridPagos.Items(contador).Cells(13).Text.Trim + "','0','0','" + gridPagos.Items(contador).Cells(7).Text.Trim + "',getdate(),'" + gridPagos.Items(contador).Cells(9).Text.Trim + "','02')")

                    misfunciones.grabaDatos("update agenda set idDoctorc='" & cmbDoctorC.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")
                    misfunciones.grabaDatos("update agenda set idtconsmed='" & cmbTipoCM.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

                    auxA = auxA + gridPagos.Items(contador).Cells(3).Text.Trim
                    auxI = auxI + gridPagos.Items(contador).Cells(4).Text.Trim
                    contador = contador + 1


                Loop
                'misfunciones.grabaDatos("update clientes set adeudo='" + txtSaldoA.Text.Trim + "', ultimoPago=" & _
                '"'" + txtfecha.Text.Trim + "' where idcliente='" + lblIdcliente.Text.Trim + "';" & _
                If chkcierre.Checked Then
                    Dim auxdtFinal As Date = TxtFechaCita.Text
                    misfunciones.grabaDatos("update agenda set estado='REALIZADO' , dtFinal ='" + String.Format("{0:dd/MM/yyyy}", auxdtFinal) + "' where idCita='" + lblIdcita.Text.Trim + "'; " & _
                                            "update clientes set activo='False', dtFinal ='" + String.Format("{0:dd/MM/yyyy}", auxdtFinal) + "' where idCliente='" + lblIdcliente.Text.Trim + "';")
                Else
                    misfunciones.grabaDatos("update agenda set estado='REALIZADO' where idCita='" + lblIdcita.Text.Trim + "'")
                End If
                If auxA > 0.0 Then
                    ImprimirR()
                    'salir()
                Else
                    'ImprimirR()
                    salir()
                End If
            Else
                Messagebox1.ShowMessage("DEBE ELEGIR AL MENOS UN TIPO DE TERAPIA")
            End If
        End If
    End Sub

    Protected Sub LinkButton6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton6.Click
        Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub cmbTipoterapia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoterapia.SelectedIndexChanged
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select (costo-descuentoc-apoyo) , cambioprecio,ClaveProdServ,tasaIVA, round((costo-descuentoc-apoyo)/1.16,2) AS 'subtotal',round(((costo-descuentoc-apoyo)/1.16)*0.16,2) AS 'iva',ClaveUnidad,Unidad from catcostos left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio where idcosto='" + cmbTipoterapia.SelectedValue.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            txtimporte.Text = leer.GetValue(0).ToString.Trim
            txtabono.Text = leer.GetValue(0).ToString.Trim
            txtcsat.Text = leer.GetValue(2).ToString.Trim
            txtcunidad.Text = leer.GetValue(6).ToString.Trim
            txtunidad.Text = leer.GetValue(7).ToString.Trim
            txttasai.Text = leer.GetValue(3).ToString.Trim
            If leer.GetValue(1).ToString.Trim = "True" Then
                txtimporte.ReadOnly = False
                txtabono.ReadOnly = False
                txtcsat.ReadOnly = True
            Else
                txtimporte.ReadOnly = True
                txtabono.ReadOnly = True
                txtcsat.ReadOnly = True
            End If
            'procedimiento de iva

            If leer.GetValue(3).ToString.Trim = "0.160000" Then
                txtsubtotal.Text = CDbl(leer.GetValue(4).ToString.Trim)
                txtiva.Text = CDbl(txtsubtotal.Text * 0.16)

            Else
                txtsubtotal.Text = leer.GetValue(0).ToString.Trim
                txtiva.Text = 0.0

            End If


        End If
        conexion.Close()
    End Sub
    Protected Sub txtimporte_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtimporte.TextChanged
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select (costo-descuentoc-apoyo) , cambioprecio,ClaveProdServ,tasaIVA, round((costo-descuentoc-apoyo)/1.16,2) AS 'subtotal',round(((costo-descuentoc-apoyo)/1.16)*0.16,2) AS 'iva',ClaveUnidad,Unidad from catcostos left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio where idcosto='" + cmbTipoterapia.SelectedValue.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then

            'procedimiento de iva

            If leer.GetValue(3).ToString.Trim = "0.160000" Then
                txtabono.Text = txtimporte.Text
                'txtsubtotal.Text = CDbl(txtimporte.Text / 1.16)
                txtsubtotal.Text = Format(CDbl(txtimporte.Text) / CDbl(1.16), "####0.00")
                'txtiva.Text = CDbl(txtsubtotal.Text * 0.16)
                txtiva.Text = Format(CDbl(txtsubtotal.Text) * CDbl(0.16), "####0.0000")

            Else

                txtsubtotal.Text = txtimporte.Text
                txtabono.Text = txtimporte.Text
                txtiva.Text = 0.0
            End If


        End If
        conexion.Close()
    End Sub
    Protected Sub Messagebox1_NoChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.NoChoosed
        Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        Context.Items.Add("varidterapia", cmbTipoterapia.SelectedValue.Trim)
        Context.Items.Add("varidcliente", lblIdcliente.Text.Trim)
        Context.Items.Add("varimporte", txtimporte.Text.Trim)
        Context.Items.Add("varfecha", TxtFechaCita.Text.Trim)
        Context.Items.Add("varabono", txtabono.Text.Trim)
        Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
        Context.Items.Add("idcita", lblIdcita.Text.Trim)
        Server.Transfer("recibo.aspx", False)
    End Sub

    Protected Sub gridPagos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridPagos.ItemCommand
        gridPagos.Items(e.Item.ItemIndex).Visible = False
        Dim otabla As New DataTable
        Dim funciones As New miclases
        otabla = funciones.creatabla(15)
        Dim cuenta As Int16 = gridPagos.Items.Count
        Dim contador As Int16 = 0
        Do While contador < cuenta
            If gridPagos.Items(contador).Visible = True Then
                Dim aFila As DataRow = otabla.NewRow
                aFila(0) = gridPagos.Items(contador).Cells(0).Text
                aFila(1) = gridPagos.Items(contador).Cells(1).Text
                aFila(2) = gridPagos.Items(contador).Cells(2).Text
                aFila(3) = gridPagos.Items(contador).Cells(3).Text
                aFila(4) = gridPagos.Items(contador).Cells(4).Text
                aFila(5) = gridPagos.DataKeys(contador).ToString
                aFila(6) = CType(gridPagos.Items(contador).Cells(5).Controls(1), CheckBox).Checked.ToString
                aFila(7) = gridPagos.Items(contador).Cells(6).Text
                aFila(8) = gridPagos.Items(contador).Cells(7).Text
                aFila(9) = gridPagos.Items(contador).Cells(8).Text
                aFila(10) = gridPagos.Items(contador).Cells(9).Text
                aFila(11) = gridPagos.Items(contador).Cells(10).Text
                aFila(12) = gridPagos.Items(contador).Cells(11).Text
                aFila(13) = gridPagos.Items(contador).Cells(12).Text
                aFila(14) = gridPagos.Items(contador).Cells(13).Text
                otabla.Rows.Add(aFila)
                'Else
                '   Dim aux As Double = Convert.ToDouble(txtSaldoA.Text) - (Convert.ToDouble(gridPagos.Items(contador).Cells(2).Text) - Convert.ToDouble(gridPagos.Items(contador).Cells(1).Text))
                '  txtSaldoA.Text = aux
            End If
            contador = contador + 1
        Loop
        otabla.TableName = "mitabla"
        gridPagos.DataMember = "mitabla"
        gridPagos.DataSource = otabla.DefaultView
        gridPagos.DataBind()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If cmbTipoterapia.SelectedValue <> "0" Then
            If txtabono.Text.Trim = "" Then
                txtabono.Text = "0.0"
            End If
            If txtmonto.Text.Trim = "" Or chkCoaseguro.Checked = False Then
                txtmonto.Text = "0.0"
            End If
            Dim auxAbono As Double = Convert.ToDouble(txtabono.Text)
            Dim auxMonto As Double = Convert.ToDouble(txtmonto.Text)
            'Dim aux As Double = (Convert.ToDouble(txtimporte.Text) - Convert.ToDouble(txtabono.Text)) + Convert.ToDouble(txtSaldoA.Text)
            If (auxMonto <> 0 And chkCoaseguro.Checked = True) Or (chkCoaseguro.Checked = False) Then
                Dim otabla As New DataTable
                Dim funciones As New miclases
                otabla = funciones.creatabla(15)
                Dim cuenta As Int16 = gridPagos.Items.Count
                If Convert.ToDouble(txtmonto.Text) <= Convert.ToDouble(txtimporte.Text.Trim) Then
                    If cuenta > 0 Then
                        Dim contador As Int16 = 0
                        Do While contador < cuenta
                            Dim aFila As DataRow = otabla.NewRow
                            aFila(0) = gridPagos.Items(contador).Cells(0).Text
                            aFila(1) = gridPagos.Items(contador).Cells(1).Text
                            aFila(2) = gridPagos.Items(contador).Cells(2).Text
                            aFila(3) = gridPagos.Items(contador).Cells(3).Text
                            aFila(4) = gridPagos.Items(contador).Cells(4).Text
                            aFila(5) = gridPagos.DataKeys(contador).ToString
                            aFila(6) = CType(gridPagos.Items(contador).Cells(5).Controls(1), CheckBox).Checked.ToString
                            aFila(7) = gridPagos.Items(contador).Cells(6).Text
                            aFila(8) = gridPagos.Items(contador).Cells(7).Text
                            aFila(9) = gridPagos.Items(contador).Cells(8).Text
                            aFila(10) = gridPagos.Items(contador).Cells(9).Text
                            aFila(11) = gridPagos.Items(contador).Cells(10).Text
                            aFila(12) = gridPagos.Items(contador).Cells(11).Text
                            aFila(13) = gridPagos.Items(contador).Cells(12).Text
                            aFila(14) = gridPagos.Items(contador).Cells(13).Text
                            otabla.Rows.Add(aFila)
                            contador = contador + 1
                        Loop
                    End If
                    Dim aFila2 As DataRow = otabla.NewRow
                    aFila2(0) = cmbTipoterapia.Items(cmbTipoterapia.SelectedIndex).Text
                    aFila2(1) = cmbPago.SelectedValue.Trim
                    aFila2(2) = txtobservacion.Text
                    aFila2(3) = Format(auxAbono, "###,###,###0.00")
                    aFila2(4) = txtimporte.Text.Trim
                    aFila2(5) = cmbTipoterapia.SelectedValue.Trim
                    aFila2(6) = chkCoaseguro.Checked.ToString
                    aFila2(7) = Format(auxMonto, "###,###,###0.00")
                    aFila2(8) = txtcantidad.Text
                    aFila2(9) = txtcsat.Text
                    aFila2(10) = txtsubtotal.Text
                    aFila2(11) = txtiva.Text
                    aFila2(12) = txtcunidad.Text
                    aFila2(13) = txtunidad.Text
                    aFila2(14) = txttasai.Text
                    otabla.Rows.Add(aFila2)
                    otabla.TableName = "mitabla"
                    gridPagos.DataMember = "mitabla"
                    gridPagos.DataSource = otabla.DefaultView
                    gridPagos.DataBind()
                    txtabono.Text = 0.0
                    txtimporte.Text = 0.0
                    chkCoaseguro.Checked = False
                    txtmonto.Text = 0.0
                    txtcsat.Text = 0
                    txtcunidad.Text = 0
                    txtunidad.Text = 0
                    txtsubtotal.Text = 0
                    txtiva.Text = 0
                    txttasai.Text = 0
                    txtcantidad.Text = 1
                    'txtSaldoA.Text = aux
                    cmbTipoterapia.SelectedValue = 0
                    txtobservacion.Text = ""
                    cmbPago.SelectedValue = "EF"
                    lblaviso.Visible = False
                Else
                    Messagebox1.ShowMessage("EL IMPORTE NO PUEDE SER MENOR AL MONTO DEL COASEGURO ...!!")
                End If
            Else
                Messagebox1.ShowMessage("EL COASEGURO NO PUEDE SER CERO ...!!")
            End If
        Else
            lblaviso.Visible = True
        End If
    End Sub

    Protected Sub cmbcostos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcostos.SelectedIndexChanged
        Dim misfunciones As New miclases
        tempdatosF.Value = cmbcostos.SelectedValue.Trim
        cmbTipoterapia.Items.Clear()
        If tempdatosF.Value <> "111" Then
            misfunciones.grabaDatos("update clientes set idcenCostos='" + cmbcostos.SelectedValue.Trim + "', idDoctor = '" + cmbDoctores.SelectedValue.Trim + "' where idcliente='" + lblIdcliente.Text.Trim + "';")
        End If
        misfunciones.grabaDatos("update agenda set idDoctor='" & cmbDoctores.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

        misfunciones.grabaDatos("update agenda set idDoctorc='" & cmbDoctorC.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

        'cmbTipoterapia = misfunciones.llenacombos(cmbTipoterapia, "select idcosto, catcostos.descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
        ' "catservicios.id_servicio=catcostos.id_servicio where catcostos.activo='true' and catcostos.iddatosfac = " & tempdatosF.Value.Trim & " order By catcostos.id_servicio")

        cmbTipoterapia = misfunciones.llenacombos(cmbTipoterapia, "select idcosto,catServicios.descripcion+ ' - ' +substring(catCostos.descripcion,Charindex('$',catCostos.descripcion),50) as descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
         "catservicios.id_servicio=catcostos.id_servicio where catcostos.activo='true' and catcostos.iddatosfac = " & tempdatosF.Value.Trim & " order By catcostos.id_servicio")

    End Sub
    'Protected Sub BtnUpPac_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnUpPac.Click
    '    Dim misfunciones As New miclases
    '    tempdatosF.Value = cmbcostos.SelectedValue.Trim
    '    cmbTipoterapia.Items.Clear()
    '    If tempdatosF.Value <> "111" Then
    '        misfunciones.grabaDatos("update clientes set idcenCostos='" + cmbcostos.SelectedValue.Trim + "', idDoctor = '" + cmbDoctores.SelectedValue.Trim + "' where idcliente='" + lblIdcliente.Text.Trim + "';")
    '    End If
    '    misfunciones.grabaDatos("update agenda set idDoctor='" & cmbDoctores.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

    '    misfunciones.grabaDatos("update agenda set idDoctorc='" & cmbDoctorC.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")

    '    cmbTipoterapia = misfunciones.llenacombos(cmbTipoterapia, "select idcosto, catcostos.descripcion,catcostos.id_servicio from catCostos inner join catServicios on " & _
    '     "catservicios.id_servicio=catcostos.id_servicio where catcostos.activo='true' and catcostos.iddatosfac = " & tempdatosF.Value.Trim & " order By catcostos.id_servicio")
    'End Sub
    Protected Sub btgmoddatos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btgmoddatos.Click
        Dim misfunciones As New miclases
        Dim funciones As New miclases
        Dim conexion As SqlConnection = Funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        If cmblatencion.SelectedValue <= "0" Then
            lblerror.Text = "No ha seleccionado el Lugar de Atencion"
            Exit Sub
        End If

        comando.CommandText = "select existencias from catlatencion where idlatencion='" + cmblatencion.SelectedValue + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lblexistencias.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + TxtFechaCita.Text.Trim + "' and estado='AGENDADO' and idlatencion='" + cmblatencion.SelectedValue + "' and idhorario='" + lblhorario.Text.Trim + "' and turno='" + lblturno.Text + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbCA.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        If CInt(lblexistencias.Text) > CInt(lbCA.Text) Then

            misfunciones.grabaDatos("update agenda set idlatencion='" & cmblatencion.SelectedValue.Trim & "' where idCita='" + lblIdcita.Text.Trim + "';")
            lblerror.Text = "Lugar de Atencion '" + cmblatencion.SelectedValue + "' ACTUALIZADO !!!"
        Else
            lblerror.Text = "Lugar de Atencion '" + cmblatencion.SelectedValue + "' esta AGOTADO !!!"

        End If



    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Context.Items.Add("elidCita", lblIdcita.Text)
        Context.Items.Add("fecha", TxtFechaCita.Text)
        Server.Transfer("cambiosala.aspx", False)
    End Sub
    Sub ImprimirR()
        Dim valor1, valor2, valor3
        Dim conexion As SqlConnection = FUNCIONES.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim cadreg As String   'catcostos.costo
        Dim caddescuento As String
        Dim cadapoyo As String
        Dim descuento As String
        Dim apoyo As String
        Dim cadobservacion As String
        Dim observacion As String
        cadreg = ""
        comando.CommandText = "select catservicios.descripcion , abonos.abono, catCostos.descuentoc, catCostos.apoyo, abonos.importe,abonos.idcosto,abonos.observacion   " & _
            " from abonos inner join catcostos on abonos.idcosto=catcostos.idcosto inner join catservicios " & _
            " on catcostos.id_servicio=catservicios.id_servicio where abonos.idcita='" + lblIdcita.Text.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        cadreg = ""
        
        Do While leer.Read
            cadreg = cadreg & leer.GetValue(5).ToString.Trim & "|" & leer.GetValue(0).ToString.Trim & "|" & Format(leer.GetValue(4), "###,###,###0.00") & "@"
            cadobservacion = cadobservacion & leer.GetValue(6).ToString.Trim & "@"
            caddescuento = caddescuento & " DESCUENTO " & "|" & Format(leer.GetValue(2), "###,###,###0.00") & "@"
            cadapoyo = cadapoyo & " APOYO " & "|" & Format(leer.GetValue(3), "###,###,###0.00") & "@"
            observacion = leer.GetValue(6).ToString.Trim
            apoyo = Format(leer.GetValue(3), "###,###,###0.00")
            descuento = Format(leer.GetValue(2), "###,###,###0.00")
        Loop
        
        leer.Close()
        conexion.Close()
        '----------------------------
        comando.CommandText = "select idpago, sum(abono) as total from abonos" & _
                              " where idCita = '" + lblIdcita.Text.Trim + "'  group by idpago "
        conexion.Open()
        leer = comando.ExecuteReader
        valor1 = 0
        valor2 = 0
        valor3 = 0
        Do While leer.Read
            Select Case leer.GetValue(0).ToString.Trim
                Case "EF"
                    valor1 = valor1 + leer.GetValue(1)
                Case "TC"
                    valor2 = valor2 + leer.GetValue(1)
                Case "TD"
                    valor3 = valor3 + leer.GetValue(1)
            End Select
        Loop
        leer.Close()
        conexion.Close()
        valor1 = Format(valor1, "###,###,###0.00")
        valor2 = Format(valor2, "###,###,###0.00")
        valor3 = Format(valor3, "###,###,###0.00")

        Dim aux2 As Double = FUNCIONES.leerValor("select sum(abono) from abonos where idcita='" + lblIdcita.Text.Trim + "'")
        txtabono.Text = Format(aux2, "###,###0.00")  'catcostos.costo
        '----------------------------
        'Response.Redirect("popup.aspx?idcita=" + lblrecibo.Text.Trim + "" & _
        '                "&cliente=" + txtcliente.Text + "&fecha=" + txtfecha.Text.Trim + "" & _
        '                "&abono=" + txtabono.Text.Trim + "&importe=" + txtimporte.Text + "&conceptos=" + cadreg.Trim)
        LblBandera.Text = 0
        Dim popupScript As String = " window.open('popup2.aspx?idcita=" + lblrecibo.Text.Trim + "&cliente=" + lblNombre.Text + "&fecha=" + TxtFechaCita.Text.Trim + "" & _
                        "&abono=" + txtabono.Text.Trim + "&importe=" + txtabono.Text + "&conceptos=" + cadreg.Trim + "&observacion=" + cadobservacion.Trim + "&descuento=" + caddescuento.Trim + "&apoyo=" + cadapoyo.Trim + "&vobservacion=" + observacion.Trim + "&vdescuento=" + descuento.Trim + "&vapoyo=" + apoyo.Trim + "&val1=" + valor1.ToString + "&val2=" + valor2.ToString + "&val3=" + valor3.ToString + "', 'CustomPopUp', 'width=350, height=350, menubar=no, resizable=no'); "
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "PopupScript", popupScript, True)

        realizado.Visible = False
        misbotones.Visible = False
        LinkButton2.Visible = False
        LinkButton1.Visible = False
        LinkButton4.Visible = False
        btmodatos.Visible = False
        lblexito.Visible = True

    End Sub
    Sub salir()
        Context.Items.Add("fecha", TxtFechaCita.Text.Trim)
        Server.Transfer("default.aspx", False)
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

    Protected Sub Facturacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = "https://facturacioncp.agemed.com.mx/?" 'Prod
        'Dim url As String = "http://localhost:5952/login.aspx?" 'Dev
        Dim bd As String = "fisiocareAM"
        Dim idUsuario As String = Session("idusuario")
        Dim pass As String = ""
        obtenerPassword(pass, idUsuario)

        Response.Redirect(url + "idUsuario=" + idUsuario + "&bd=" + bd + "&ValR=" + pass)
    End Sub

    Private Sub obtenerPassword(ByRef pass As String, ByVal idUsuario As String)
        Dim claseDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT password FROM usuarios WHERE idUsuario = '" + idUsuario + "';"

        If claseDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                pass = dt.Rows(0).Item("password")
            Else
                Messagebox1.ShowMessage("Ocurrió un error al consultar los datos del usuario")
            End If
        Else
            Messagebox1.ShowMessage(claseDatos.MensajeError)
        End If
    End Sub
End Class
