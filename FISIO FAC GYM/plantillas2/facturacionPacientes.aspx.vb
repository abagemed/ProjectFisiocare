Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing
Imports System.Collections.Generic
Imports System.Security.Cryptography
Imports System.Security.Cryptography.X509Certificates
Imports System.Drawing
Imports System.Drawing.ImageFormatConverter
Imports System.IO
Imports CFDI
'Imports mx.fel.www
Imports com.facturarenlinea.timbrado.RespuestaTFD
Imports com.facturarenlinea.timbrado
Imports ThoughtWorks.QRCode.Codec
Imports System.Xml

Partial Class facturacionPacientes
    Inherits System.Web.UI.Page
    Dim SQL As String = ""
    Dim funciones As New miclases
    Dim fechapdf As String
    Protected Sub Button11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button11.Click
        lstpacientes.Items.Clear()
        If txtBnombre.Text.Trim <> "" And txtBpaterno.Text.Trim <> "" And txtBmaterno.Text <> "" Then
            SQL = ""
            SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
            SQL = SQL & "WHERE nombres = '" & txtBnombre.Text.Trim & "' and  papellido = '" & txtBpaterno.Text.Trim & "'"
            SQL = SQL & " and sapellido= '" & txtBmaterno.Text.Trim & "'"
            SQL = SQL & "ORDER BY nomcompleto"
            lstpacientes = funciones.llenalista(lstpacientes, SQL)
            If lstpacientes.Items.Count = 0 Then
                SQL = ""
                SQL = " SELECT idpaciente, nomcompleto FROM PACIENTES "
                SQL = SQL & " WHERE nombres LIKE '%" & txtBnombre.Text.Trim & "%' and  papellido LIKE '%" & txtBpaterno.Text.Trim & "%'"
                SQL = SQL & " and sapellido LIKE '%" & txtBmaterno.Text.Trim & "%'"
                SQL = SQL & " ORDER BY nomcompleto"
                lstpacientes = funciones.llenalista(lstpacientes, SQL)
            End If
        End If

        If txtBpaterno.Text.Trim <> "" And txtBmaterno.Text <> "" And lstpacientes.Items.Count = 0 Then
            SQL = ""
            SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
            SQL = SQL & "WHERE papellido = '" & txtBpaterno.Text.Trim & "' and sapellido= '" & txtBmaterno.Text.Trim & "'"
            SQL = SQL & "ORDER BY nomcompleto"
            lstpacientes = funciones.llenalista(lstpacientes, SQL)
            If lstpacientes.Items.Count = 0 Then
                SQL = ""
                SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
                SQL = SQL & "WHERE   papellido LIKE '%" & txtBpaterno.Text.Trim & "%' and sapellido LIKE '%" & txtBmaterno.Text.Trim & "%'"
                SQL = SQL & "ORDER BY nomcompleto"
                lstpacientes = funciones.llenalista(lstpacientes, SQL)
            End If
        End If

        If txtBnombre.Text.Trim <> "" And txtBpaterno.Text.Trim <> "" And lstpacientes.Items.Count = 0 Then
            SQL = ""
            SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
            SQL = SQL & "WHERE nombres = '" & txtBnombre.Text.Trim & "' and  papellido = '" & txtBpaterno.Text.Trim & "'"
            SQL = SQL & "ORDER BY nomcompleto"
            lstpacientes = funciones.llenalista(lstpacientes, SQL)
            If lstpacientes.Items.Count = 0 Then
                SQL = ""
                SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
                SQL = SQL & "WHERE nombres LIKE '%" & txtBnombre.Text.Trim & "%' and  papellido LIKE '%" & txtBpaterno.Text.Trim & "%'"
                SQL = SQL & "ORDER BY nomcompleto"
                lstpacientes = funciones.llenalista(lstpacientes, SQL)
            End If
        End If

        If txtBpaterno.Text.Trim <> "" And lstpacientes.Items.Count = 0 Then
            SQL = ""
            SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
            SQL = SQL & " WHERE papellido = '" & txtBpaterno.Text.Trim & "'"
            SQL = SQL & " ORDER BY nomcompleto"
            lstpacientes = funciones.llenalista(lstpacientes, SQL)
            If lstpacientes.Items.Count = 0 Then
                SQL = ""
                SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
                SQL = SQL & "WHERE papellido LIKE '%" & txtBpaterno.Text.Trim & "%'"
                SQL = SQL & "ORDER BY nomcompleto"
                lstpacientes = funciones.llenalista(lstpacientes, SQL)
            End If
        End If

        If txtBnombre.Text.Trim <> "" And lstpacientes.Items.Count = 0 Then
            SQL = ""
            SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
            SQL = SQL & " WHERE nombres = '" & txtBpaterno.Text.Trim & "'"
            SQL = SQL & " ORDER BY nomcompleto"
            lstpacientes = funciones.llenalista(lstpacientes, SQL)
            If lstpacientes.Items.Count = 0 Then
                SQL = ""
                SQL = "SELECT idpaciente, nomcompleto FROM PACIENTES "
                SQL = SQL & "WHERE nombres LIKE '%" & txtBnombre.Text.Trim & "%'"
                SQL = SQL & "ORDER BY idpaciente"
                lstpacientes = funciones.llenalista(lstpacientes, SQL)
            End If
        End If
        '+lstpacientes.Items.Count.ToString()
        '+lstpacientes = Funciones.llenalista(lstpacientes, SQL)
        '+++++++++++++++++

        lstpacientes.Focus()
    End Sub
    Protected Sub lstpacientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles lstpacientes.SelectedIndexChanged
        lblElpaciente.Text = lstpacientes.Items(lstpacientes.SelectedIndex).Text
        txtpaciente.Text = lblElpaciente.Text
        terapiassinfacturar()
        serviciosFactura.Visible = True
        'hfIdPaciente.Value = lstpacientes.SelectedValue

    End Sub
    Sub terapiassinfacturar()
        Dim sql As String
        sql = " select a.idagenda as folio, p.nomcompleto,convert(varchar(10),a.dtagenda,103) as fecha,"
        sql += vbNewLine & " d.nombre+' '+d.paterno+' '+ d.materno as doctor, sum(c.subtotal) as importe    from agenda a "
        sql += vbNewLine & "  inner join pacientes p on p.idpaciente = a.idpaciente"
        sql += vbNewLine & " inner join CargosConsulta c on c.FolioConsulta = a.idagenda"
        sql += vbNewLine & "  inner join catDoctores d on d.iddoctor = a.idDoctor "
        sql += vbNewLine & "  where p.idpaciente = " + lstpacientes.SelectedValue.Trim + " And a.Facturado = 0 and c.status = 'A' and c.codigoconcepto <> 17 "
        sql += vbNewLine & "  group by a.idagenda, p.nomcompleto,convert(varchar(10),a.dtagenda,103),d.nombre+' '+d.paterno+' '+ d.materno "
        gridFacturas = funciones.creadataset(sql, gridFacturas)
        gridFacturas.Columns(5).Visible = True

    End Sub

    Sub cargossinfacturar()
        Dim sql As String
        sql = " select c.foliocargo as folio, p.nomcompleto,  d.nombre +' '+d.paterno +' '+ d.materno as doctor"
        sql += vbNewLine & " ,c.fechacargo as fecha, c.subtotal as importe "
        sql += vbNewLine & " from CargosManualesPacientes c"
        sql += vbNewLine & " inner join pacientes p on p.idpaciente = c.IdPaciente "
        sql += vbNewLine & " inner join catDoctores d on d.iddoctor = c.IdDoctor "
        sql += vbNewLine & " where c.idpaciente = " + lstpacientes.SelectedValue.Trim + " and c.facturado = 0 and  c.Status = 'a' and c.codigoconcepto <> 17 "

        gridFacturas = funciones.creadataset(sql, gridFacturas)
        gridFacturas.Columns(5).Visible = True

    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim cReg As Integer = gridDetalles.Items.Count
        Dim cuenta As Integer = 0
        ftxtimporte.Text = 0
        Do While cuenta < cReg
            gridDetalles.Items(cuenta).Cells(2).Text = Format(CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim * gridDetalles.Items(cuenta).Cells(5).Text, "###,###,###0.00")
            ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(gridDetalles.Items(cuenta).Cells(2).Text), "###,###,###0.00")
            cuenta = cuenta + 1
        Loop
        ftxttotal.Text = ftxtimporte.Text
        ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))
    End Sub

    'Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
    '    'lbltempclie.Text = "0"
    '    Dim cuenta As Integer = gridFacturas.Items.Count
    '    Dim contando As Integer = 0
    '    Dim auxobse As Date
    '    Dim cadcompara As String = ""
    '    Dim misfun As New miclases
    '    lblCondiciones.Text = ""
    '    txtNfac.Text = ""
    '    txtpaciente.Text = ""
    '    TXTmiobserva.Text = ""
    '    Do While contando < cuenta
    '        If CType(gridFacturas.Items(contando).Cells(5).Controls(1), CheckBox).Checked = True Then
    '            lblCondiciones.Text = lblCondiciones.Text + " idabono='" + gridFacturas.DataKeys.Item(contando).ToString + "' OR"
    '            If misfun.lookup(gridFacturas.Items(contando).Cells(0).Text, cadcompara, ",") = 0 Then
    '                cadcompara = cadcompara + gridFacturas.Items(contando).Cells(0).Text + ","
    '                lblrecibos.Text = lblrecibos.Text + " idCita='" + gridFacturas.Items(contando).Cells(0).Text + "' OR"
    '            End If
    '            If TXTmiobserva.Text.Trim = "" Then
    '                auxobse = gridFacturas.Items(contando).Cells(3).Text
    '                TXTmiobserva.Text = gridFacturas.Items(contando).Cells(3).Text
    '            End If
    '            If CDate(gridFacturas.Items(contando).Cells(3).Text) > auxobse Then
    '                TXTmiobserva.Text = auxobse.ToString.Trim
    '                auxobse = gridFacturas.Items(contando).Cells(3).Text
    '            End If
    '        End If
    '        contando = contando + 1
    '    Loop
    '    Dim consulta As String = ""
    '    If lblCondiciones.Text.Trim <> "" Then
    '        TXTmiobserva.Text = "" '"TERAPIAS REALIZADAS DEL " + Left(TXTmiobserva.Text.Trim, 10) + " AL " + Left(auxobse.ToString.Trim, 10)
    '        datosPaciente.Visible = False
    '        lafactura.Visible = True
    '        lblCondiciones.Text = Left(lblCondiciones.Text, Len(lblCondiciones.Text) - 2)
    '        lblrecibos.Text = Left(lblrecibos.Text, Len(lblrecibos.Text) - 2)

    '        Dim auxDatosfac As String = ""
    '        Dim resultados(,) As String = funciones.leerValores("select idcliente,isnull(datosfacpaciente.idpaciente,0) from pacientes " & _
    '        "left join datosfacpaciente on pacientes.idpaciente=datosfacpaciente.idpaciente " & _
    '        "where pacientes.idpaciente='" + lstpacientes.SelectedValue.ToString + "'", 2)
    '        If resultados(0, 0) = 1 And resultados(1, 0) <> 0 Then
    '            auxDatosfac = "left join datosfacpaciente as datos on datos.idpaciente =  temp.idpaciente"
    '        Else
    '            auxDatosfac = "left join clientes as datos on datos.idcliente = pacientes.idcliente "
    '        End If

    '        consulta = "select rasonsocial,(calle+' '+noext+' '+colonia+' '+cp+' '+datos.municipio+' '+ciudad+' '+estado+' '+pais) as direccion,pacientes.idcliente,temp.idpaciente, temp.idPago,catpagos.descripcion, temp.cantidad,temp.importe,rfc,ciudad " & _
    '        ",formadepago,nocuenta,referencia from (SELECT  idpaciente ,sum(importe) as importe,count(idpago)as cantidad, idpago,referencia FROM  abonos " & _
    '        "WHERE  (abonos.idpaciente = '" + lstpacientes.SelectedValue.Trim + "') AND " + lblCondiciones.Text + " group by idpaciente,idpago,referencia)temp " & _
    '        "left join pacientes on pacientes.idpaciente=temp.idpaciente " & _
    '        "left join catpagos on catpagos.idpago=temp.idpago " & auxDatosfac
    '        gridDetalles = funciones.creadataset(consulta, gridDetalles)

    '        'MsgBox(consulta)

    '        ftxtnombre.Text = gridDetalles.Items(0).Cells(3).Text
    '        ftxtdomicilio.Text = gridDetalles.Items(0).Cells(4).Text
    '        ftxtrfc.Text = gridDetalles.Items(0).Cells(6).Text
    '        ftxtciudad.Text = gridDetalles.Items(0).Cells(7).Text
    '        txtpaciente.Text = lstpacientes.Items(lstpacientes.SelectedIndex).Text 'ftxtnombre.Text.Trim
    '        cmbformapago.SelectedValue = gridDetalles.Items(0).Cells(8).Text
    '        txtnumcuenta.Text = gridDetalles.Items(0).Cells(9).Text
    '        'lbltempclie.Text = gridDetalles.Items(0).Cells(10).Text

    '        Dim cuentaF As Int16 = gridDetalles.Items.Count
    '        Dim var As Int16 = 0
    '        ftxtimporte.Text = 0.0
    '        Do While var < cuentaF
    '            gridDetalles.Items(var).Cells(2).Text = Format(Convert.ToDouble(gridDetalles.Items(var).Cells(2).Text), "###,###,###0.00")
    '            ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(gridDetalles.Items(var).Cells(2).Text), "###,###,###0.00")
    '            var = var + 1
    '        Loop
    '        ftxttotal.Text = ftxtimporte.Text.Trim
    '        ftxtiva.Text = "0.00"
    '        ftxtdia.Text = Now.Day.ToString
    '        cmbmes.SelectedValue = Now.Month.ToString.PadLeft(2, "0")
    '        cmbanio.SelectedValue = Now.Year.ToString.Trim
    '        ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))

    '        resultados = funciones.leerValores("SELECT CAST(consecutivo + 1 AS varchar) as num, serie  from consecutivoFac", 2)
    '        txtNfac.Text = resultados(0, 0)
    '        txtNserie.Text = resultados(1, 0)
    '    Else
    '        Messagebox1.ShowMessage("FAVOR DE SELECCIONAR LO QUE SE VA A FACTURAR")
    '    End If
    'End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'lbltempclie.Text = "0"
        Dim cuenta As Integer = gridFacturas.Items.Count
        Dim contando As Integer = 0
        Dim cadcompara As String = ""
        Dim misfun As New miclases
        lblCondiciones.Text = ""
        txtNfac.Text = ""
        'txtpaciente.Text = ""
        TXTmiobserva.Text = ""
        Do While contando < cuenta
            If CType(gridFacturas.Items(contando).Cells(5).Controls(1), CheckBox).Checked = True Then
                lblCondiciones.Text += "'" + gridFacturas.DataKeys.Item(contando).ToString + "',"
            End If
            contando = contando + 1
        Loop
       
        Dim consulta As String = ""
        If lblCondiciones.Text.Trim <> "" Then
            lblCondiciones.Text = lblCondiciones.Text.Substring(0, lblCondiciones.Text.Length - 1)
            datosPaciente.Visible = False
            lafactura.Visible = True
            If ddlorigen.SelectedValue = "2" Then
                consulta = " select count(cc.codigoconcepto) as cantidad,cc.descripcion , sum(c.subtotal) as importe, '' as observaciones, 0 as importeiva  from cargosconsulta c "
                consulta += vbNewLine & " inner join catconceptos cc on cc.codigoconcepto= c.codigoconcepto where  c.folioconsulta in (" + lblCondiciones.Text + ") and c.status = 'A'"
                consulta += vbNewLine & " group by cc.codigoconcepto, cc.descripcion"

            Else
                consulta = " select count(cc.codigoconcepto) as cantidad,cc.descripcion, sum(c.subtotal) as importe, c.observaciones, sum(c.importeiva) as importeiva from cargosmanualespacientes c "
                consulta += vbNewLine & " inner join catconceptos cc on cc.codigoconcepto= c.codigoconcepto where  c.foliocargo in (" + lblCondiciones.Text + ") and c.status = 'A' "
                consulta += vbNewLine & " group by cc.codigoconcepto, cc.descripcion, c.observaciones"
            End If
            gridDetalles = funciones.creadataset(consulta, gridDetalles)


            Dim cuentaF As Int16 = gridDetalles.Items.Count
            Dim var As Int16 = 0
            ftxtimporte.Text = 0.0
            ftxtiva.Text = 0.0
            Do While var < cuentaF
                gridDetalles.Items(var).Cells(2).Text = Format(Convert.ToDouble(gridDetalles.Items(var).Cells(2).Text), "###,###,###0.00")
                ftxtimporte.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(gridDetalles.Items(var).Cells(2).Text), "###,###,###0.00")
                ftxtiva.Text = Format(Convert.ToDouble(ftxtiva.Text) + Convert.ToDouble(gridDetalles.Items(var).Cells(4).Text), "###,###,###0.00")
                var = var + 1
            Loop
            ftxttotal.Text = Format(Convert.ToDouble(ftxtimporte.Text) + Convert.ToDouble(ftxtiva.Text), "###,###,###0.00")

            ftxtdia.Text = Now.Day.ToString
            cmbmes.SelectedValue = Now.Month.ToString.PadLeft(2, "0")
            cmbanio.SelectedValue = Now.Year.ToString.Trim
            ftxtletras.Text = funciones.monto(Convert.ToDouble(ftxttotal.Text.Trim))

            'resultados = funciones.leerValores("SELECT CAST(consecutivo + 1 AS varchar) as num, serie  from consecutivoFac", 2)
            'txtNfac.Text = resultados(0, 0)
            'txtNserie.Text = resultados(1, 0)
        Else
            Messagebox1.ShowMessage("FAVOR DE SELECCIONAR LO QUE SE VA A FACTURAR")
        End If
    End Sub




    Protected Sub btnCatalogo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCatalogo.Click
        cmbcliefac = funciones.llenacombos(cmbcliefac, "select replace(rfc+razonsocial,' ','') as codigo,razonsocial from rfcclientesfacturacion where status='A' ORDER BY razonsocial")
        ftxtnombre.Visible = False
        btnCatalogo.Visible = False
        btnNcliente.Visible = False
        cmbcliefac.Visible = True
    End Sub

    Protected Sub btnNcliente_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNcliente.Click
        If ftxtnombre.Text = "" Then
            txtdireccion.Text = ""
            txtrfc.Text = "XAXX010101000"
            txtciudad.Text = ""
            txtcp.Text = ""
            txtrazon.Text = ""
            txtCalle.Text = ""
            txtNoExt.Text = ""
            Txtinterior.Text = ""
            txtmunicipio.Text = ""
            txtpais.Text = "MÉXICO"
            txtColonia.Text = ""
            txtrazon.Text = ""
            lblDatosfac.Text = "N"
        Else
            Dim sqlcons As String
            sqlcons = " select * from rfcclientesfacturacion where replace(rfc+razonsocial,' ','') =  '" & ftxtrfc.Text.Replace(" ", "") & ftxtnombre.Text.Replace(" ", "") & "'"
            Dim Cn As SqlConnection = funciones.conecta
            Dim sqlComando = New SqlCommand(sqlcons, Cn)
            sqlComando.CommandText = sqlcons
            Dim sqlAdaptador = New SqlDataAdapter(sqlComando)
            Cn.Open()
            Dim dtresultado As New DataTable
            sqlAdaptador.Fill(dtresultado)
            Cn.Close()


            If dtresultado.Rows.Count > 0 Then
                txtrazon.Text = dtresultado.Rows(0).Item("razonsocial")
                txtrfc.Text = dtresultado.Rows(0).Item("rfc")
                txtCalle.Text = dtresultado.Rows(0).Item("calle")
                txtNoExt.Text = dtresultado.Rows(0).Item("noexterior")
                txtColonia.Text = dtresultado.Rows(0).Item("colonia")
                txtcp.Text = dtresultado.Rows(0).Item("codigopostal")
                txtmunicipio.Text = dtresultado.Rows(0).Item("municipio")
                txtciudad.Text = dtresultado.Rows(0).Item("ciudad")
                txtestado.Text = dtresultado.Rows(0).Item("estado")
                txtpais.Text = dtresultado.Rows(0).Item("pais")
                Txtinterior.Text = dtresultado.Rows(0).Item("nointerior")
                lblDatosfac.Text = "M"
            Else
                txtdireccion.Text = ""
                txtrfc.Text = "XAXX010101000"
                txtciudad.Text = ""
                txtcp.Text = ""
                txtrazon.Text = ""
                txtCalle.Text = ""
                txtNoExt.Text = ""
                Txtinterior.Text = ""
                txtmunicipio.Text = ""
                txtpais.Text = "MÉXICO"
                txtColonia.Text = ""
                txtrazon.Text = ""
                lblDatosfac.Text = "N"
            End If
        End If
        ModalPopupExtender2.Show()
    End Sub

    Protected Sub cmbcliefac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbcliefac.SelectedIndexChanged
        datosfac(cmbcliefac.SelectedValue.ToString)
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click

        ModalPopupExtender1.Show()
    End Sub



    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            lblerrorconcepto.Visible = False
            Button3.Visible = False
            cmbTipoterapia = funciones.llenacombos(cmbTipoterapia, "select codigoconcepto, descripcion from catconceptos where status = 'A' and codigoconcepto <> 17 order By descripcion ")
            ddldoctores = funciones.llenacombos(ddldoctores, "select iddoctor, nombre+' '+paterno+' '+materno as nombre from catdoctores")
            cmbformapago = funciones.llenacombos(cmbformapago, "select idtipopago,descripcion,clave from catTipoPago where activo = 'true'")
            cmbClaveMP = funciones.llenacombos(cmbClaveMP, "select idtipopago,clave from catTipoPago order by clave")
            ' cmbformadepago = funciones.llenacombos(cmbformadepago, "select idtipopago,descripcion from catTipoPago where activo = 'true'")
            cmbcliefac = funciones.llenacombos(cmbcliefac, "select replace(rfc+razonsocial,' ','') as codigo,razonsocial from rfcclientesfacturacion where status='A' ORDER BY razonsocial")
            lbIdagenda.Text = Context.Items("idAgenda").ToString
            Dim idpaciente As String = Context.Items("idpaciente").ToString
            txtfecha.Text = Context.Items("agFecha").ToString
            usuario.Value = Context.Items("usuario").ToString
            empleado.Value = Context.Items("empleado").ToString
            hfIddoctor.Value = Context.Items("idDoctor").ToString
            If idpaciente <> "0" Then
                lstpacientes = funciones.llenalista(lstpacientes, "SELECT idpaciente, nomcompleto FROM PACIENTES where idPaciente='" + idpaciente + "'")
                lstpacientes.SelectedValue = idpaciente
                lstpacientes_SelectedIndexChanged(Nothing, Nothing)
            End If
            ftxtnombre.Visible = False
            btnCatalogo.Visible = False
        End If

    End Sub
    Sub llenaDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idtipopago,clave from catTipoPago where idtipopago='" + cmbformapago.SelectedValue.ToString.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            Dim idclave As String = leer.GetValue(0).ToString.Trim
            If idclave = "" Then
                idclave = "0"
            End If
            cmbClaveMP.SelectedValue = idclave

            cmbClaveMP.Enabled = False
        End If
        leer.Close()
        conexion.Close()
        conexion.Dispose()
    End Sub
    Protected Sub cmbformapago_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbformapago.SelectedIndexChanged
        If cmbformapago.SelectedValue = "0" Then
            cmbClaveMP.SelectedValue = 0
        Else
            llenaDatos()
        End If
    End Sub
    Protected Sub cmbTipoterapia_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTipoterapia.SelectedIndexChanged
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select importesugerido from catconceptos where codigoconcepto='" + cmbTipoterapia.SelectedValue.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            Txtimporte.Value = Math.Round(CDbl(leer.GetValue(0)), 2)
        End If
        conexion.Close()
        ModalPopupExtender1.Show()
    End Sub



    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("usuario", usuario.Value)
        Context.Items.Add("empleado", empleado.Value)
        Context.Items.Add("idDoctor", hfIddoctor.Value)
        Context.Items.Add("fecha", txtfecha.Text)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub btnFreg_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFreg.Click
        terapiassinfacturar()
        datosPaciente.Visible = True
        lafactura.Visible = False

        lafactura.Visible = False
        btnFimp.Visible = False
        btnFguardar.Visible = True
    End Sub


    Protected Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim SQL As String

        ' se guardan los datos del cliente

        If lblDatosfac.Text = "N" Then
            SQL = "insert into rfcclientesfacturacion (razonsocial,rfc,calle,noExterior,nointerior,colonia,codigopostal,municipio,ciudad,estado,pais,status,fechaactualizacion) " & _
            " values ('" + txtrazon.Text + "','" + txtrfc.Text + "','" + txtCalle.Text + "','" + txtNoExt.Text + "','" & Txtinterior.Text & "','" + txtColonia.Text + "','" + txtcp.Text + "'," & _
            "'" + txtmunicipio.Text + "','" + txtciudad.Text.Trim + "','" + txtestado.Text + "','" + txtpais.Text + "','A',getdate()); "
        Else
            SQL = "update rfcclientesfacturacion set razonsocial='" + txtrazon.Text + "',rfc='" + txtrfc.Text + "' " & _
            ",calle ='" + txtCalle.Text + "', noexterior='" + txtNoExt.Text + "', colonia='" + txtColonia.Text + "', municipio='" + txtmunicipio.Text + "' " & _
            ",estado = '" + txtestado.Text + "', pais='" + txtpais.Text + "',fechaactualizacion = getdate()" & _
            ", ciudad='" + txtciudad.Text.Trim + "', codigopostal='" + txtcp.Text + "',nointerior='" + Txtinterior.Text + "' where replace(rfc+razonsocial,' ','') =  '" & ftxtrfc.Text.Replace(" ", "") & ftxtnombre.Text.Replace(" ", "") & "'"
        End If


        comando.CommandText = SQL
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        'cmbcliefac = funciones.llenacombos(cmbcliefac, "select idcliente,rasonsocial from clientes order by rasonsocial")
        ' cmbcliefac.SelectedValue = 1
        ftxtnombre.Visible = False
        btnCatalogo.Visible = False
        btnNcliente.Visible = False
        cmbcliefac.Visible = True
        datosfac(txtrfc.Text.Replace(" ", "") & txtrazon.Text.Replace(" ", ""))






    End Sub

    Protected Sub btnFguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFguardar.Click
        Dim bandera As Boolean = True
        Try
            Dim axip As Date = String.Format("{0:dd/MM/yyyy}", ftxtdia.Text.Trim.PadLeft(2, "0") + "/" + cmbmes.SelectedValue.Trim + "/" + cmbanio.SelectedValue + " 12:00:00")
        Catch ex As Exception
            bandera = False
        End Try
        Dim foliofin As Integer = funciones.leerValor("select foliofin from consecutivofac")

        If bandera = True Then

            'If foliofin >= txtNfac.Text.Trim Then
            If cmbformapago.SelectedValue.Trim <> "0" Then
                '+++imprimirFactura("1"Dim foliofin As Integer = clase.leerValor("select foliofin from consecutivofac")
                'grabafactura("I")  '* RUTINA QUE IMPRIME Y GUARDA LA FACTURA
                Dim banderaerror As Boolean = facturarEimprimir()  '* RUTINA QUE GUARDA LA FACTURA
                If banderaerror = False Then
                    btnFguardar.Visible = False
                End If
            Else
                Messagebox1.ShowMessage("NO HA SELECCIONADO UNA FORMA DE PAGO CORRECTA, VERIFIQUELO !!!")
            End If
            'Else
            '   Messagebox1.ShowMessage("EL SELLO DE HACIENDA SE A VENCIDO FAVOR DE CONTACTAR AL ADMINISTRADOR...")
            'End If

        Else
            Messagebox1.ShowMessage("FECHA INCORRECTA")
        End If
    End Sub

    Function facturarEimprimir()
        Dim banderaError As Boolean = False
        Dim auxfecha As String = String.Format("{0:dd/MM/yyyy}", ftxtdia.Text.Trim.PadLeft(2, "0") + "/" + cmbmes.SelectedValue.Trim + "/" + cmbanio.SelectedValue + " 12:00:00")
        Dim auxNfac As String = txtNfac.Text.Trim '+ " " + txtNserie.Text.Trim
        Dim clase As New miclases
        Dim cuenta As Int32 = gridDetalles.Items.Count
        Dim contador As Int32 = 0
        Dim auximporte As Decimal
        Dim Sgraba As String = ""
        Dim cpagado As String = ""
        Dim comp As New Comprobante()
        Dim sqlcons As String
        Dim Cn As SqlConnection
        Dim sqlComando As SqlCommand
        Dim sqlAdaptador As SqlDataAdapter
        Dim dtresultado As New DataTable


        sqlcons = " select c.prefijo+ right(concat('000000000',c.consecutivo),8) as folio from catfoliadores c where codigofoliador = 4 "

        Cn = funciones.conecta
        sqlComando = New SqlCommand(sqlcons, Cn)
        sqlComando.CommandText = sqlcons
        sqlAdaptador = New SqlDataAdapter(sqlComando)
        Cn.Open()
        sqlAdaptador.Fill(dtresultado)
        Cn.Close()
        txtNfac.Text = dtresultado.Rows(0).Item("folio")


        comp.folio = txtNfac.Text.Trim
        'comp.serie = txtNserie.Text.Trim
        Dim fecha As DateTime = DateTime.Now
        comp.fecha = String.Format("{0}T{1}", fecha.ToString("yyyy-MM-dd"), fecha.ToString("HH:mm:ss"))
        fechapdf = comp.fecha
        comp.formaDePago = "PAGO EN UNA SOLA EXHIBICION"
        comp.subTotal = Replace(ftxtimporte.Text.Trim, ",", "")
        comp.total = Replace(ftxttotal.Text, ",", "")
        comp.tipoDeComprobante = "ingreso"
        comp.moneda = "MXP"
        comp.tipoCambio = "1.0"
        comp.metodoDePago = cmbClaveMP.Items(cmbClaveMP.SelectedIndex).Text.Trim
        comp.lugarExpedicion = "MÉRIDA,YUCATÁN"

        Dim emisor As New Emisor()
        emisor.rfc = "OST141127IEA"
        emisor.nombre = "ORTOPEDIA STAR, S.C.P."
        emisor.domicilioFiscal = New DomicilioFiscal
        emisor.domicilioFiscal.calle = "26"
        emisor.domicilioFiscal.noExterior = "299"
        emisor.domicilioFiscal.noInterior = "928"
        emisor.domicilioFiscal.colonia = "FRACC. ALTABRISA"
        emisor.domicilioFiscal.localidad = "MÉRIDA"
        emisor.domicilioFiscal.municipio = "MÉRIDA"
        emisor.domicilioFiscal.estado = "YUCATÁN"
        emisor.domicilioFiscal.pais = "MEXICO"
        emisor.domicilioFiscal.codigoPostal = "97133"
        emisor.regimenFiscal = New RegimenFiscal
        emisor.regimenFiscal.regimen = "REGIMEN GENERAL DE LEY PERSONAS MORALES"

        sqlcons = " select * from rfcclientesfacturacion where replace(rfc+razonsocial,' ','') =  '" & ftxtrfc.Text.Replace(" ", "") & ftxtnombre.Text.Replace(" ", "") & "'"
        dtresultado = New DataTable
        Cn = funciones.conecta
        sqlComando = New SqlCommand(sqlcons, Cn)
        sqlComando.CommandText = sqlcons
        sqlAdaptador = New SqlDataAdapter(sqlComando)
        Cn.Open()
        sqlAdaptador.Fill(dtresultado)
        Cn.Close()

        Dim receptor As New Receptor()
        receptor.rfc = dtresultado.Rows(0).Item("rfc")
        receptor.nombre = dtresultado.Rows(0).Item("razonsocial")
        receptor.domicilio = New Domicilio()
        receptor.domicilio.calle = dtresultado.Rows(0).Item("calle")
        receptor.domicilio.noExterior = dtresultado.Rows(0).Item("noexterior")
        If dtresultado.Rows(0).Item("nointerior") <> "" Then
            receptor.domicilio.noInterior = dtresultado.Rows(0).Item("nointerior")
        End If
        receptor.domicilio.colonia = dtresultado.Rows(0).Item("colonia")
        receptor.domicilio.localidad = dtresultado.Rows(0).Item("ciudad")
        receptor.domicilio.codigoPostal = dtresultado.Rows(0).Item("codigopostal")
        receptor.domicilio.municipio = dtresultado.Rows(0).Item("municipio")
        receptor.domicilio.estado = dtresultado.Rows(0).Item("estado")
        receptor.domicilio.pais = dtresultado.Rows(0).Item("pais")

        comp.conceptos = New List(Of Concepto)()
        Do While contador < cuenta
            If gridDetalles.Items(contador).Cells(2).Text.Trim <> "" Then
                auximporte = gridDetalles.Items(contador).Cells(2).Text.Trim
            Else
                auximporte = 0
            End If

            Dim concepto As New Concepto()
            Dim cantidad As Decimal = CType(gridDetalles.Items(contador).Cells(0).Controls(1), TextBox).Text
            concepto.cantidad = cantidad.ToString.Trim
            concepto.unidad = "No aplica"
            concepto.noIdentificacion = gridDetalles.DataKeys.Item(contador).ToString.Trim
            concepto.descripcion = gridDetalles.Items(contador).Cells(1).Text
            concepto.importe = Decimal.Round(auximporte, 4)
            concepto.valorUnitario = Decimal.Round(auximporte / cantidad, 4).ToString.Trim
            comp.conceptos.Add(concepto)

            contador = contador + 1
        Loop

        'Sgraba = Sgraba + "insert into factura(num_factura,serie,fecha,saldo,iva,importe,idpaciente,idCliente,observaciones,idtipopago,numcuenta,rfc) " & _
        '                           "values('" + auxNfac.Trim + "','" + txtNserie.Text.Trim + "','" + auxfecha + "'," & _
        '                           "'" + Convert.ToDouble(ftxttotal.Text.Trim).ToString + "','" + Convert.ToDouble(ftxtiva.Text.Trim).ToString + "','" + Convert.ToDouble(ftxttotal.Text.Trim).ToString + "'," & _
        '                           "'" + lstpacientes.SelectedValue.Trim + "','" + cmbcliefac.SelectedValue.Trim + "','" + TXTmiobserva.Text.Trim + "','" + cmbformapago.SelectedValue.Trim + "','" + txtnumcuenta.Text + "','" & ftxtrfc.Text.Trim & "');"


        Dim iva As New Traslado()
        iva.impuesto = "IVA"
        iva.importe = Replace(ftxtiva.Text.Trim, ",", "")
        iva.tasa = "16"

        Dim impuestos As New Impuestos()
        impuestos.traslados = New List(Of Traslado)()
        impuestos.totalImpuestosTrasladados = Replace(ftxtiva.Text.Trim, ",", "")
        impuestos.totalImpuestosRetenidos = "0.0000"
        impuestos.traslados.Add(iva)

        comp.emisor = emisor
        comp.receptor = receptor
        comp.impuestos = impuestos

        Dim Xml
        Dim cer
        Try
            cer = New X509Certificate2(Server.MapPath("OST14112729122014.pfx"), "orto2014", X509KeyStorageFlags.MachineKeySet)
            Xml = CFDIv32.Serializar(comp, False)

        Catch ex As Exception
            banderaError = True
            Messagebox1.ShowMessage("Error en el sellado del xml para timbrar favor de llamar a informatica " + vbNewLine + ex.Message.ToString)
        End Try

        If banderaError = False Then
            Dim cadenaOriginal = CFDIv32.CadenaOriginalSellado(Xml)

            Try
                Dim ar = cer.GetSerialNumber
                Array.Reverse(ar)
                comp.noCertificado = Encoding.UTF8.GetString(ar)
                comp.certificado = Convert.ToBase64String(cer.RawData) 'Certificado igual para la cancelación del cfdi
                comp.sello = CFDIv32.Sello(cadenaOriginal, cer)

                Xml = CFDIv32.Serializar(comp, True)
                CFDIv32.Validar(Xml, True)
                File.WriteAllText(Server.MapPath("mandatorioSGO.xml"), Xml)
            Catch ex As Exception
                banderaError = True
                Messagebox1.ShowMessage("Error en el xml para timbrar favor de llamar a informatica" + ex.Message.ToString)
            End Try

            Dim uuid, sellocfd, nocertificadoSat, selloSat, FechaTimbrado As String
            If banderaError = False Then
                Try
                    'Dim timbrarFac As New WS_TFD
                    'Dim respuesta = timbrarFac.TimbrarCFDPrueba("DEMO060207MQ0XX", "awKBR5$@Vg$", Xml)
                    'Dim respuesta = timbrarFac.TimbrarCFD("OST141127IEA", "kBMEtFvuTrr#", Xml, "orto_" + comp.folio.ToString)
                    'Xml = respuesta(3)
                    Dim timbrarFac As New WSTFD
                    Dim timbreFac As New RespuestaTFD

                    timbreFac = timbrarFac.TimbrarCFDI("OST141127IEA", "kBMEtFvuTrr#", Xml, "orto_" + comp.folio.ToString)
                    'para pruebas
                    'timbreFac = timbrarFac.TimbrarCFDI("DEMO141127IEA", "NJ%CD%FsWh+", Xml, "orto_" + comp.folio.ToString)
                    Xml = timbreFac.XMLResultado
                    'If respuesta(1).ToString.Trim = "" Then
                    If timbreFac.MensajeError.Trim = "" And timbreFac.OperacionExitosa = True And timbreFac.OperacionExitosaSpecified = True Then
                        ' clase.grabaDatos(Sgraba + "  update abonos set lfacturado='true', num_factura='" + auxNfac + "', serie ='" + txtNserie.Text.Trim + "' " & _
                        '" where " + lblCondiciones.Text + "; update consecutivoFac set consecutivo='" + txtNfac.Text.Trim + "'; ")
                        CFDIv32.Validar(Xml, True)
                        uuid = timbreFac.Timbre.UUID
                        sellocfd = timbreFac.Timbre.SelloCFD
                        nocertificadoSat = timbreFac.Timbre.NumeroCertificadoSAT
                        selloSat = timbreFac.Timbre.SelloSAT
                        FechaTimbrado = timbreFac.Timbre.FechaTimbrado
                        Dim sqlguardar As String

                        If ddlorigen.SelectedValue = 2 Then
                            sqlguardar = " insert into consultasfacturadas (folioconsulta,status, foliofactura)"
                            sqlguardar += vbNewLine & " select idagenda, 'A', '" & txtNfac.Text & "' from agenda where idagenda in (" + lblCondiciones.Text + ")"
                            clase.grabaDatos(sqlguardar)
                            clase.grabaDatos("update agenda set facturado = '1' where idagenda in (" + lblCondiciones.Text + ")")
                        Else
                            sqlguardar = " insert into cargosfacturados (foliocargo,status,tipocargo,foliofactura,idempleado) "
                            sqlguardar += vbNewLine & " select foliocargo, 'A','P','" & txtNfac.Text & "','" & empleado.Value & "' from cargosmanualespacientes where foliocargo in (" + lblCondiciones.Text + ")"
                            clase.grabaDatos(sqlguardar)
                            clase.grabaDatos("update cargosmanualespacientes set facturado = '1' where foliocargo in (" + lblCondiciones.Text + ")")
                        End If

                        clase.grabaDatos("update catfoliadores set consecutivo = consecutivo + 1 where codigofoliador in (4,5)")

                        sqlguardar = "INSERT INTO [dbo].[FacturasCab] ([FolioFactura],[Serie],[rfc],[TipoFactura],"
                        sqlguardar += vbNewLine & " [OrigenFactura],[subtotal],[ImporteIva],[ImporteIsr],[Total] ,"
                        sqlguardar += vbNewLine & " [codigoCliente],[Status],[codigoEmpresa],[IdEmpleado],[fechaFactura]) VALUES  "
                        sqlguardar += vbNewLine & " ('" & txtNfac.Text & "','','" & dtresultado.Rows(0).Item("rfc") & "', 'P', '" & IIf(ddlorigen.SelectedValue = 2, "C", "M") & "',"
                        sqlguardar += vbNewLine & " '" & comp.subTotal & "','" & Replace(ftxtiva.Text.Trim, ",", "") & "','0','" & comp.total & "','1','V','1','" & empleado.Value & "', getdate()  )"
                        clase.grabaDatos(sqlguardar)

                        sqlguardar = "INSERT INTO [dbo].[Facturasxml] (foliofactura,serie,xml,uuid,numerocertificadosat,sellocfd, "
                        sqlguardar += vbNewLine & " sellosat,fechatimbrado,estado) values ('" & txtNfac.Text & "','', '" & timbreFac.XMLResultado & "', '" & uuid & "',"
                        sqlguardar += vbNewLine & " '" & nocertificadoSat & "','" & sellocfd & "','" & selloSat & "','" & FechaTimbrado & "','" & timbreFac.Timbre.Estado & "' )"
                        clase.grabaDatos(sqlguardar)
                    Else
                        txtNfac.Text = ""
                        banderaError = True
                        Messagebox1.ShowMessage("No cerrar; Error en el xml de timbrado favor de llamar a informatica; " + timbreFac.MensajeErrorDetallado.ToString.Trim)
                    End If
                Catch ex As Exception
                    banderaError = True
                    Messagebox1.ShowMessage("Error en el timbrado favor de llamar a informatica " + ex.Message.ToString)
                End Try
            End If

            Dim nameFile As String = ""
            If banderaError = False Then
                Try
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(Xml)
                    nameFile = "c:\reporteFisio\orto\" + comp.folio.ToString + ".xml"
                    doc.Save(nameFile)
                    CFDIv32.Validar(Xml, True)
                Catch ex As Exception
                    banderaError = True
                    Messagebox1.ShowMessage("Eror en el guradado del xml favor de llamar a informatica")
                End Try
            End If

            If banderaError = False Then
                Try
                    Dim cadenaOriComplemento As String = CFDIv32.CadenaOriginalImpresa(Xml)

                    Dim QRCodeEncoder As New QRCodeEncoder
                    QRCodeEncoder.QRCodeScale = "3"
                    QRCodeEncoder.QRCodeVersion = "7"
                    QRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M

                    Dim imagen As Bitmap
                    Dim auxcad = Split(ftxttotal.Text.ToString.Trim, ".")
                    Dim enteros As String = auxcad(0).ToString.Trim.PadLeft(10, "0")
                    Dim decimales As String = auxcad(1).ToString.Trim.PadRight(6, "0")
                    Dim datos As String = "?re=" + emisor.rfc.Trim + "&rr=" + receptor.rfc.Trim + "&tt=" + enteros + "." + decimales + "&id=" + uuid.Trim
                    imagen = QRCodeEncoder.Encode(datos)
                    imagen.Save("C:\reporteFisio\archivosFac\RQorto\qrFac" + txtNfac.Text.ToString.Trim + ".jpeg", Imaging.ImageFormat.Jpeg)
                    'funciones.grabaDatos("update factura set uuid='" + uuid + "',selloCfD='" + sellocfd + "', noCertificadoSAT='" + nocertificadoSat + "'," & _
                    ' "selloSAT='" + selloSat + "',fechaTimbrado='" + FechaTimbrado + "' where num_factura='" + txtNfac.Text.Trim + "'")
                    imprimirFacturaCfdi(txtNfac.Text.Trim, FechaTimbrado, comp.noCertificado.ToString, nocertificadoSat, uuid, cadenaOriComplemento, selloSat, sellocfd, receptor.domicilio.calle.ToString + " " + receptor.domicilio.noExterior.ToString.Trim + " " + receptor.domicilio.noInterior, receptor.domicilio.colonia, receptor.domicilio.municipio, receptor.domicilio.codigoPostal, receptor.domicilio.localidad, receptor.domicilio.estado, comp.total)
                    btnFguardar.Visible = False
                Catch ex As Exception
                    Messagebox1.ShowMessage("Error en la impresion del pdf llamar a informatica" + ex.Message.ToString)
                End Try
            End If
        End If
        Return banderaError
    End Function

    Sub imprimirFacturaCfdi(ByVal queimprimo As String, ByVal fechacer As String, ByVal ceremisor As String, ByVal cersat As String, ByVal uuid As String, ByVal cadoriginal As String, ByVal sellosat As String, ByVal selloCFD As String, ByVal domicilio As String, ByVal colonia As String, ByVal municipio As String, ByVal cp As String, ByVal localidad As String, ByVal estado As String, ByVal totalfac As String)
        Dim mireporte As New ReportDocument
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
        mireporte.Load("c:\reporteFisio\FacturaOrtopediaS2016_CFDI.rpt")
        Dim conexion As SqlConnection = funciones.conecta
        Dim cgrupo As String = ""
        With visor_reporte
            .DisplayGroupTree = True
            .HasExportButton = False
            .HasPrintButton = True
            .DisplayGroupTree = False
            .HasToggleGroupTreeButton = False
            .HasZoomFactorList = True
            .HasViewList = False
            .DisplayToolbar = True
        End With

        '++++++++++++++++++++
        Dim dt As New DataTable
        Dim columna As New DataColumn("cantidad")
        dt.Columns.Add(columna)
        Dim col2 As New DataColumn("descripcion")
        dt.Columns.Add(col2)
        Dim col3 As New DataColumn("importe")
        dt.Columns.Add(col3)
        Dim col4 As New DataColumn("precio")
        dt.Columns.Add(col4)
        'Dim col5 As New DataColumn("img", System.Type.GetType(Of Byte))
        dt.Columns.Add(New DataColumn("img", GetType(Byte())))
        Dim col5 As New DataColumn("referencia")
        dt.Columns.Add(col5)

        Dim fs As FileStream = New FileStream("C:\reporteFisio\archivosFac\RQorto\qrFac" + txtNfac.Text.ToString.Trim + ".jpeg", FileMode.Open)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim imagen(CInt(fs.Length)) As Byte

        br.Read(imagen, 0, CInt(fs.Length))
        br.Close()
        fs.Close()

        Dim cReg As Integer = gridDetalles.Items.Count
        Dim cuenta As Integer = 0
        Do While cuenta < cReg
            If gridDetalles.Items(cuenta).Cells(2).Text.Trim <> "" Then
                Dim nFila As DataRow
                nFila = dt.NewRow
                nFila(0) = CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim
                nFila(1) = gridDetalles.Items(cuenta).Cells(1).Text.Trim
                nFila(2) = gridDetalles.Items(cuenta).Cells(2).Text.Trim
                nFila(3) = Format(gridDetalles.Items(cuenta).Cells(2).Text.Trim / CType(gridDetalles.Items(cuenta).Cells(0).Controls(1), TextBox).Text.Trim, "###,###,###0.00")
                nFila(4) = imagen
                If gridDetalles.Items(cuenta).Cells(3).Text.Trim <> "&nbsp;" Then
                    nFila(5) = gridDetalles.Items(cuenta).Cells(3).Text.Trim
                Else
                    nFila(5) = ""
                End If

                dt.Rows.Add(nFila)
            End If
            cuenta = cuenta + 1
        Loop
        mireporte.SetDataSource(dt.DefaultView)

        '--------------datos facturacion
        Mivar.Value = ftxtnombre.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("nombre").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = domicilio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("domicilio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = colonia
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("colonia").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = municipio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("municipio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cp
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cp").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = localidad
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("localidad").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = estado
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("estado").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtrfc.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("rfc").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtpaciente.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()
        '----------------datos de la factura
        Mivar.Value = txtNfac.Text.Trim '+ " " + txtNserie.Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("recibo").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '  Mivar.Value = cmbanio.SelectedItem.Value.ToString.Trim + "-" + cmbmes.SelectedItem.Value + "-" + ftxtdia.Text.Trim.PadLeft(2, "0") + "T00:00:00"
        Mivar.Value = fechapdf
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cmbformapago.Items(cmbformapago.SelectedIndex).Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("formapago").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cmbClaveMP.Items(cmbClaveMP.SelectedIndex).Text
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ClaveMP").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtnumcuenta.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("numcuenta").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()
        '-------------------------importes
        Mivar.Value = ftxtimporte.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("importe").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtiva.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("iva").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = totalfac
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ftxtletras.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cantidadletras").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        If chkImp.Checked Then
            Mivar.Value = TXTmiobserva.Text.Trim
        Else
            Mivar.Value = ""
        End If
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("observaciones").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = fechacer
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fechacer").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ceremisor
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ceremisor").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cersat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cersat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = uuid
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("uuid").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cadoriginal
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cadenaoriginal").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = sellosat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("sellosat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = selloCFD
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("selloCFD").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '--------------------------------------------------

        visor_reporte.DataBind()
        visor_reporte.ReportSource = mireporte
        visor_reporte.PrintMode = PrintMode.Pdf

        'exporta el rpt a pdf
        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim o As CrystalDecisions.Shared.ExportOptions
        o = New CrystalDecisions.Shared.ExportOptions
        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        filedest.DiskFileName = "c:\reporteFisio\orto\" + queimprimo.ToString.Trim + ".pdf"
        o.ExportDestinationOptions = filedest.Clone
        mireporte.Export(o)
        filedest = Nothing
        o = Nothing

        'se abre el pdf en acrobat
        Dim Nombre As String
        Nombre = "c:\reporteFisio\orto\" + queimprimo.ToString.Trim + ".pdf"
        Dim xmlArchivo As String = "c:\reporteFisio\orto\" + queimprimo.Trim + ".xml"
        'If txtmail.Text.Trim <> "" And chkMail.Checked = True Then
        ' Try
        'lasfunciones.enviaCorreoPdf(txtmail.Text, Nombre, xmlArchivo, "Factura", "Envio de su factura ....")
        'funciones.grabaDatos("update clientes set email='" + txtmail.Text.Trim + "' where idcliente='" + gridDetalles.Items(0).Cells(8).Text + "'")
        'Catch
        'End Try
        'End If

        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
        Response.WriteFile(Nombre)
        Response.Flush()
        Response.Close()
    End Sub

    Sub datosfac(ByVal rfc As String)
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand

        comando.CommandText = "select razonsocial,(calle+' '+noexterior+' '+' '+nointerior +' '+colonia+' '+codigopostal+' '+municipio+' '+ciudad+' '+estado+' '+pais) as direccion,ciudad,rfc from rfcclientesfacturacion " & _
        "where replace(rfc+razonsocial,' ','') = '" + rfc + "'"

        conexion.Open()
        Dim sqlread As SqlDataReader = comando.ExecuteReader
        If sqlread.Read Then
            ftxtnombre.Text = sqlread.GetValue(0).ToString.Trim
            ftxtdomicilio.Text = sqlread.GetValue(1).ToString.Trim
            ftxtciudad.Text = sqlread.GetValue(2).ToString.Trim
            ftxtrfc.Text = sqlread.GetValue(3).ToString.Trim

        End If
        sqlread.Close()
        conexion.Close()
        ftxtnombre.Visible = True
        btnCatalogo.Visible = True
        cmbcliefac.Visible = False
        btnNcliente.Visible = True
    End Sub

    Protected Sub agconcepto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles agconcepto.Click
        Dim sqlconcepto As String

        If Txtimporte.Value <> "" And IsNumeric(Txtimporte.Value) And Txtimporte.Value > 0 Then
            lblerrorconcepto.Visible = False
        Else
            lblerrorconcepto.Text = "Importe Invalido"
            lblerrorconcepto.Visible = True
            ModalPopupExtender1.Show()
            Exit Sub
        End If
        If txtcomision.Value <> "" And IsNumeric(txtcomision.Value) Then
            lblerrorconcepto.Visible = False
        Else
            lblerrorconcepto.Text = "Importe de comision Invalido"
            lblerrorconcepto.Visible = True
            ModalPopupExtender1.Show()
            Exit Sub
        End If


        sqlconcepto = " insert into CargosManualesPacientes (foliocargo,codigoconcepto, subtotal, comision, importeiva, ImporteIsr,"
        sqlconcepto += vbNewLine & " status, idempleado,fechacargo,idpaciente,iddoctor,observaciones,Referencia, facturado	)"
        sqlconcepto += vbNewLine & " select c.prefijo+ right(concat('000000000',c.consecutivo),8),'" & cmbTipoterapia.SelectedValue & "','" & Txtimporte.Value & "','" & txtcomision.Value & "'," & txtimporteiva.Value & ",0,'A','" & empleado.Value & "'"
        sqlconcepto += vbNewLine & " ,getdate(),'" & lstpacientes.SelectedValue.Trim & "','" & ddldoctores.SelectedValue.Trim & "','" & Txtobservaciones.Text & "','" & Txtreferencia.Text & "',0  from  CatFoliadores c where c.codigoFoliador = 2"
        sqlconcepto += vbNewLine & vbNewLine & " UPDATE CatFoliadores SET CONSECUTIVO = CONSECUTIVO+1 WHERE codigoFoliador =2"
        funciones.grabaDatos(sqlconcepto)
        cargossinfacturar()

        cmbTipoterapia.SelectedIndex = 0
        Txtimporte.Value = "0.0"
        txtcomision.Value = "0.0"
        txtimporteiva.Value = "0.0"
        ddldoctores.SelectedIndex = 0
        Txtobservaciones.Text = ""
        Txtreferencia.Text = ""
        ddivas.SelectedValue = 1

    End Sub

    Protected Sub ddlorigen_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddlorigen.SelectedIndexChanged
        Select Case ddlorigen.SelectedValue
            Case "1"
                Button3.Visible = True
                cargossinfacturar()
            Case "2"
                Button3.Visible = False
                terapiassinfacturar()
        End Select
    End Sub


    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        txtdireccion.Text = ""
        txtrfc.Text = "XAXX010101000"
        txtciudad.Text = ""
        txtcp.Text = ""
        txtrazon.Text = ""
        txtCalle.Text = ""
        txtNoExt.Text = ""
        Txtinterior.Text = ""
        txtmunicipio.Text = ""
        txtpais.Text = "MÉXICO"
        txtColonia.Text = ""
        txtrazon.Text = ""
        lblDatosfac.Text = "N"
        ModalPopupExtender2.Show()
    End Sub


    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Context.Items.Add("usuario", usuario.Value)
        Context.Items.Add("empleado", empleado.Value)
        Context.Items.Add("idDoctor", hfIddoctor.Value)
        Context.Items.Add("fecha", txtfecha.Text)
        Server.Transfer("facturasGeneradas.aspx")
    End Sub

    Protected Sub ddivas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ddivas.SelectedIndexChanged
        Select Case ddivas.SelectedValue
            Case 1
                txtimporteiva.Value = "0.0"
            Case 2
                If CDbl(Txtimporte.Value) > 0 Then
                    txtimporteiva.Value = Txtimporte.Value * 0.16
                End If
        End Select




        ModalPopupExtender1.Show()
    End Sub

End Class
