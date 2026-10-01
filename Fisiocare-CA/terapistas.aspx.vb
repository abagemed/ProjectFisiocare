Imports System.Data.SqlClient

Partial Class terapistas
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cmbterapistas = funciones.llenacombos(cmbterapistas, "select id,Nombre from terapistas where activo='true'")
            cmbProtocolos = funciones.llenacombos(cmbProtocolos, "select distinct id,nombre from protocolos")
        End If
    End Sub

    Protected Sub cmbterapistas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbterapistas.SelectedIndexChanged
        Panel1.Visible = False
        Panel2.Visible = False
        Panel3.Visible = False
        Panel4.Visible = False
        lblaviso.Visible = False
        lblContraseña.Visible = False
        txtObsesion.Text = ""
        txtNombusca.Text = ""
        If cmbterapistas.SelectedValue <> "0" Then
            Panel3.Visible = True
            lblContraseña.Visible = True
        End If
        txtNombusca.Text = ""
        txtNombusca.Enabled = True
        btnOtro.Visible = False
        lblselfecha.Visible = False
        cmbFechas.Visible = False
    End Sub


    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnleido.Click
        Panel1.Visible = False
        Panel2.Visible = True
        lblfecha.Text = "Tratamientos - " + hffecha.Value
        txtchc.Text = txtchcV.Text
        txtus.Text = txtusV.Text
        txtcif.Text = txtcifV.Text
        txtlaser.Text = txtlaserV.Text
        txtparafina.Text = txtparafinaV.Text
        txtmakina.Text = txtMakinaV.Text
        txtcerv.Text = txtcervV.Text
        txtvectra.Text = txtvectraV.Text
        txtcrioterapia.Text = txtcrioterapiaV.Text
        txtlumb.Text = txtLumV.Text
        txtdialermia.Text = txtdialermiaV.Text
        txtlibres.Text = txtlibresV.Text
        txtisotermicos.Text = txtIsotermicosV.Text
        txtwilliams.Text = txtwilliamsV.Text
        txtbicicleta.Text = txtbicicletaV.Text
        txtcaminadora.Text = txtcaminadoraV.Text
        txtpolainas.Text = txtPolainasV.Text
        txtOcupacional.Text = txtocupacionalV.Text
        txtpostulares.Text = txtpostularesV.Text
        txtMarcha.Text = txtmarchaV.Text
        txtA.Text = txtAv.Text
        txtR.Text = txtRv.Text
        txtV.Text = txtVv.Text
        txtN.Text = txtNv.Text
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        funciones.grabaDatos("insert into bitTratamiento(idTerapista,observacion,idcita,fecha,idcliente," & _
        "chc,us,cif,laser,parafina,makina,cerv,vectra,crioterapia,lum,dialermia,libres,isotermicos,williams," & _
        "bicicleta,caminadora,polainas,ocupacional,postulares,marcha,ligasa,ligasr,ligasv,ligasn)  " & _
        "values('" + cmbterapistas.SelectedValue + "','" + txtObsesion.Text + "','" + hfidcita.Value + "','" + hffecha.Value + "','" + hfidCliente.Value + "'" & _
        ",'" + txtchc.Text + "','" + txtus.Text + "','" + txtcif.Text + "','" + txtlaser.Text + "','" + txtparafina.Text + "'," & _
        "'" + txtmakina.Text + "','" + txtcerv.Text + "','" + txtvectra.Text + "','" + txtcrioterapia.Text + "'," & _
        "'" + txtlumb.Text + "','" + txtdialermia.Text + "','" + txtlibres.Text + "','" + txtisotermicos.Text + "'," & _
        "'" + txtwilliams.Text + "','" + txtbicicleta.Text + "','" + txtcaminadora.Text + "','" + txtpolainas.Text + "'," & _
        "'" + txtOcupacional.Text + "','" + txtpostulares.Text + "','" + txtMarcha.Text + "','" + txtA.Text + "'," & _
        "'" + txtR.Text + "','" + txtV.Text + "','" + txtN.Text + "'); " & _
        " update agenda set enSesion='false' where idcita='" + hfidcita.Value + "'")
        Panel2.Visible = False
        txtObsesion.Text = ""
        Panel1.Visible = False
        txtNombusca.Text = ""
        txtNombusca.Enabled = True
        lblselfecha.Visible = False
        cmbFechas.Visible = False
        btnOtro.Visible = False
        Messagebox1.ShowMessage("Los datos fueron grabados ...")
    End Sub
    Protected Sub txtNombusca_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombusca.TextChanged
        If Page.IsPostBack Then
            If txtNombusca.Text.Trim <> "" Then
                gBusca = funciones.creadataset("select idcliente,Nombre+' '+paterno+' '+materno as nombre from clientes where paterno + ' ' + materno + ' ' + nombre LIKE '%" + txtNombusca.Text + "%' OR " & _
                             "paterno + ' ' + nombre + ' ' + materno LIKE '%" + txtNombusca.Text + "%' OR nombre + ' ' + materno + ' ' + paterno LIKE '%" + txtNombusca.Text + "%' OR  " & _
                             "nombre + ' ' + paterno + ' ' + materno LIKE '%" + txtNombusca.Text + "%' OR materno + ' ' + paterno + ' ' + nombre LIKE '%" + txtNombusca.Text + "%' OR " & _
                             "materno + ' ' + nombre + ' ' + paterno LIKE '%" + txtNombusca.Text + "%' order by nombre,paterno,materno", gBusca)
            End If
            Panel1.Visible = False
            Panel2.Visible = False
            txtNombusca.Enabled = False
            btnOtro.Visible = True
            If gBusca.Items.Count > 0 Then
                btnVacio.Visible = False
            Else
                btnVacio.Visible = True
            End If
            ModalPopupExtender1.Show()
            Panel1.Focus()
        End If
    End Sub

    Protected Sub txtpass_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpass.TextChanged
        lblIncorrecto.Text = ""
        If funciones.leerValor("select nombre from terapistas where id='" + cmbterapistas.SelectedValue + "' and pass='" + txtpass.Text + "'") <> "0" Then
            Panel3.Visible = False
            lblContraseña.Visible = False
            Panel4.Visible = True
        Else
            lblIncorrecto.Text = "Password Incorrecto"
        End If
    End Sub

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOtro.Click
        Panel1.Visible = False
        Panel2.Visible = False
        txtNombusca.Enabled = True
        btnOtro.Visible = False
        lblselfecha.Visible = False
        cmbFechas.Visible = False
        lblaviso.Visible = False
    End Sub

    Protected Sub cmbFechas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbFechas.SelectedIndexChanged
        If cmbFechas.SelectedValue <> "0" Then
            hfidcita.Value = cmbFechas.SelectedValue
            hffecha.Value = cmbFechas.Items(cmbFechas.SelectedIndex).Text.Trim
            btnleido.Visible = True
            If Panel2.Visible = False Then
                Panel1.Visible = True
            Else
                lblfecha.Text = "Tratamientos - " + hffecha.Value
            End If
        Else
            btnleido.Visible = False
            Panel1.Visible = False
        End If
    End Sub
    Protected Sub btnContra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContra.Click
        txtvisible.Text = txtContraIndica.Text
        btnTrata.ForeColor = Drawing.Color.Red
        btnContra.ForeColor = Drawing.Color.Gray
        btnObserva.ForeColor = Drawing.Color.Red
        lbltitulo.Text = "CONTRA INDICACIONES"
    End Sub
    Protected Sub btnTrata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrata.Click
        txtvisible.Text = txtTratamiento.Text
        btnTrata.ForeColor = Drawing.Color.Gray
        btnContra.ForeColor = Drawing.Color.Red
        btnObserva.ForeColor = Drawing.Color.Red
        lbltitulo.Text = "TRATAMIENTO"
    End Sub

    Protected Sub btnObserva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObserva.Click
        txtvisible.Text = txtobservacion.Text
        btnTrata.ForeColor = Drawing.Color.Red
        btnContra.ForeColor = Drawing.Color.Red
        btnObserva.ForeColor = Drawing.Color.Gray
        lbltitulo.Text = "OBSERVACIONES"
    End Sub

    Protected Sub gBusca_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gBusca.ItemCommand
        hfidCliente.Value = gBusca.DataKeys(e.Item.ItemIndex).ToString
        cmbFechas = funciones.llenacombos(cmbFechas, "select idcita,convert(varchar,fecha,103) from agenda where idcliente='" + hfidCliente.Value + "' and enSesion='true' order by fecha")
        If cmbFechas.Items.Count > 1 Then
            If funciones.leerValor("select tratamiento from tratamientos where vigente='true' and " & _
            " idcliente='" + hfidCliente.Value + "'") = "0" Then
                ModalPopupExtender1.Show()
                gBusca.Visible = False
                divProtocolos.Visible = True
                Exit Sub
            End If
            llenaLosdatosIni()
            txtNombusca.Text = CType(gBusca.Items(e.Item.ItemIndex).Cells(0).Controls(1), LinkButton).Text
        Else
            lblaviso.Visible = True
        End If
    End Sub

    Protected Sub cmbProtocolos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProtocolos.SelectedIndexChanged
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + cmbProtocolos.SelectedValue + "'"
        txtTratamiento.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Dosificación".PadRight(30, " ") + "|" + "Tiempo".PadRight(15, " ") + "|" + Chr(10)
        txtTratamiento.Text = txtTratamiento.Text + "-".PadRight(105, "-") + Chr(10)
        txtContraIndica.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Precauciones".PadRight(30, " ") + "|" + Chr(10)
        txtContraIndica.Text = txtContraIndica.Text + "-".PadRight(63, "-") + Chr(10)
        txtobservacion.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Intensidad".PadRight(25, " ") + "|" + Chr(10)
        txtobservacion.Text = txtobservacion.Text + "-".PadRight(58, "-") + Chr(10)
        conexion.Open()
        Dim oLeer As SqlDataReader = comando.ExecuteReader
        Do While oLeer.Read
            txtTratamiento.Text = txtTratamiento.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(1).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(3).ToString.PadRight(15, " ") + "|" + Chr(10)
            txtContraIndica.Text = txtContraIndica.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(4).ToString.PadRight(30, " ") + "|" + Chr(10)
            txtobservacion.Text = txtobservacion.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(2).ToString.PadRight(25, " ") + "|" + Chr(10)
        Loop

        ModalPopupExtender1.Show()

    End Sub

    Protected Sub Button1_Click2(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnProtocolo.Click
        lblAvisoProtocolo.Visible = False
        'Threading.Thread.Sleep(3000)
        If cmbProtocolos.SelectedValue <> "0" Then
            funciones.grabaDatos("insert into tratamientos(idcliente,tratamiento,observaciones,vigente,contraindica,comp,crio,lass,para,lassb,elec,maqr,maqh,tral,trac," & _
           "txtcomp,txtcrio,txtlass,txtpara,txtbarr,txtelec,txthomb,txtrodi,txtlumb,txtcerv,idProtocolo) " & _
           "values('" + hfidCliente.Value + "','" + txtTratamiento.Text + "'," & _
           "'" + txtobservacion.Text + "','true','" + txtContraIndica.Text + "','" + chkComp.Checked.ToString + "','" + chkCrio.Checked.ToString + "'," & _
           "'" + chkLass.Checked.ToString + "','" + chkPara.Checked.ToString + "','" + chklassb.Checked.ToString + "'," & _
           "'" + chkelec.Checked.ToString + "','" + chkMaqr.Checked.ToString + "','" + chkMaqh.Checked.ToString + "'," & _
           "'" + chkTral.Checked.ToString + "','" + chkTrac.Checked.ToString + "','" + txtcomp.Text + "','" + txtcrio.Text + "','" + txtlass.Text + "'," & _
           "'" + txtpara.Text + "','" + txtbarr.Text + "','" + txtelec.Text + "','" + txthomb.Text + "','" + txtrodi.Text + "','" + txtlumb.Text + "'," & _
           "'" + txtcerv.Text + "','" + cmbProtocolos.SelectedValue.ToString + "')")
            gBusca.Visible = False
            divProtocolos.Visible = True
            cmbProtocolos.SelectedValue = 0
            llenaLosdatosIni()
            gBusca.Visible = True
            divProtocolos.Visible = False
        Else
            lblAvisoProtocolo.Visible = True
            ModalPopupExtender1.Show()
        End If
    End Sub

    Sub llenaLosdatosIni()
        Panel2.Visible = False
        Panel1.Visible = False
        lblselfecha.Visible = False
        cmbFechas.Visible = False
        txtObsesion.Text = ""
        btnleido.Visible = False
        cmbFechas.Items.Clear()

        cmbFechas = funciones.llenacombos(cmbFechas, "select idcita,convert(varchar,fecha,103) from agenda where idcliente='" + hfidCliente.Value + "' and enSesion='true' order by fecha")
        If cmbFechas.Items.Count > 1 Then
            cmbFechas.Visible = True
            lblselfecha.Visible = True
            lblaviso.Visible = False
            Dim valores(,) As String = funciones.leerValores("select tratamiento,observaciones,id,contraindica," & _
           "comp,crio,lass,para,lassb,elec,maqr,maqh,tral,trac,txtcomp,txtcrio,txtlass,txtpara,txtbarr,txtelec,txthomb,txtrodi,txtlumb,txtcerv" & _
           " from tratamientos where vigente='true' and idcliente='" + hfidCliente.Value + "'", 24)
            Try
                txtTratamiento.Text = valores(0, 0)
                txtobservacion.Text = valores(1, 0)
                txtContraIndica.Text = valores(3, 0)
                chkComp.Checked = valores(4, 0)
                chkCrio.Checked = valores(5, 0)
                chkLass.Checked = valores(6, 0)
                chkPara.Checked = valores(7, 0)
                chklassb.Checked = valores(8, 0)
                chkelec.Checked = valores(9, 0)
                chkMaqr.Checked = valores(10, 0)
                chkMaqh.Checked = valores(11, 0)
                chkTral.Checked = valores(12, 0)
                chkTrac.Checked = valores(13, 0)
                txtcomp.Text = valores(14, 0)
                txtcrio.Text = valores(15, 0)
                txtlass.Text = valores(16, 0)
                txtpara.Text = valores(17, 0)
                txtbarr.Text = valores(18, 0)
                txtelec.Text = valores(19, 0)
                txthomb.Text = valores(20, 0)
                txtrodi.Text = valores(21, 0)
                txtlumbD.Text = valores(22, 0)
                txtcervD.Text = valores(23, 0)
            Catch

            End Try
            txtvisible.Text = txtTratamiento.Text

            valores = funciones.leerValores("select observacion,nombre,chc,us,cif,laser,parafina," & _
            "makina,cerv,vectra,crioterapia,lum,dialermia,libres,isotermicos,williams," & _
            "bicicleta,caminadora,polainas,ocupacional,postulares,marcha,ligasa,ligasr,ligasv,ligasn from (SELECT observacion,idTerapista, " & _
            "chc,us,cif,laser,parafina,makina,cerv,vectra,crioterapia,lum,dialermia,libres,isotermicos,williams," & _
            "bicicleta,caminadora,polainas,ocupacional,postulares,marcha,ligasa,ligasr,ligasv,ligasn FROM bitTratamiento " & _
            " WHERE (fecha = (SELECT MAX(fecha) AS FechaMaxima FROM bitTratamiento where idCliente='" + hfidCliente.Value + "')) " & _
            "AND (idCliente = '" + hfidCliente.Value + "')) as temp left join terapistas on id=idTerapista", 26)
            txtUltima.Text = valores(0, 0)
            Try
                If valores(1, 0).ToString.Trim <> "" Then
                    lblUltima.Text = "Ultima Sesión - " + valores(1, 0)
                End If
                txtchcV.Text = valores(2, 0)
                txtusV.Text = valores(3, 0)
                txtcifV.Text = valores(4, 0)
                txtlaserV.Text = valores(5, 0)
                txtparafinaV.Text = valores(6, 0)
                txtMakinaV.Text = valores(7, 0)
                txtcervV.Text = valores(8, 0)
                txtvectraV.Text = valores(9, 0)
                txtcrioterapiaV.Text = valores(10, 0)
                txtLumV.Text = valores(11, 0)
                txtdialermiaV.Text = valores(12, 0)
                txtlibresV.Text = valores(13, 0)
                txtIsotermicosV.Text = valores(14, 0)
                txtwilliamsV.Text = valores(15, 0)
                txtbicicletaV.Text = valores(16, 0)
                txtcaminadoraV.Text = valores(17, 0)
                txtPolainasV.Text = valores(18, 0)
                txtocupacionalV.Text = valores(19, 0)
                txtpostularesV.Text = valores(20, 0)
                txtmarchaV.Text = valores(21, 0)
                txtAv.Text = valores(22, 0)
                txtRv.Text = valores(23, 0)
                txtVv.Text = valores(24, 0)
                txtNv.Text = valores(25, 0)
                lblUltima.Text = lblUltima.Text + " - No. Sesiones " + funciones.leerValor("select count(idCliente) from agenda where idCliente='" + hfidCliente.Value + "' and estado = 'REALIZADO'").ToString
            Catch
                lblUltima.Text = "Ultima Sesión"
                txtchcV.Text = ""
                txtusV.Text = ""
                txtcifV.Text = ""
                txtlaserV.Text = ""
                txtparafinaV.Text = ""
                txtMakinaV.Text = ""
                txtcervV.Text = ""
                txtvectraV.Text = ""
                txtcrioterapiaV.Text = ""
                txtLumV.Text = ""
                txtdialermiaV.Text = ""
                txtlibresV.Text = ""
                txtIsotermicosV.Text = ""
                txtwilliamsV.Text = ""
                txtbicicletaV.Text = ""
                txtcaminadoraV.Text = ""
                txtPolainasV.Text = ""
                txtocupacionalV.Text = ""
                txtpostularesV.Text = ""
                txtmarchaV.Text = ""
                txtAv.Text = ""
                txtRv.Text = ""
                txtVv.Text = ""
                txtNv.Text = ""
            End Try
        Else
            lblaviso.Visible = True
        End If
    End Sub

    Protected Sub elbtnVacio_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles elbtnVacio.Click

    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        gBusca.Visible = True
        divProtocolos.Visible = False
    End Sub
End Class