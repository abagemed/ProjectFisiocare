Imports System.Data.SqlClient
Partial Class tratamientos
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'lblfechacita.Text = Context.Items("fecha").ToString.Trim
            'cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, nombre+' '+paterno+' '+materno from clientes order by nombre,paterno,materno ")
            cmbProtocolos = funciones.llenacombos(cmbProtocolos, "select distinct id,nombre from protocolos")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGnueva.Click
        funciones.grabaDatos("update tratamientos set vigente='false' where idcliente='" + hfIdcliente.Value + "'; " & _
        "insert into tratamientos(idcliente,tratamiento,observaciones,vigente,contraindica,comp,crio,lass,para,lassb,elec,maqr,maqh,tral,trac," & _
        "txtcomp,txtcrio,txtlass,txtpara,txtbarr,txtelec,txthomb,txtrodi,txtlumb,txtcerv,idProtocolo) " & _
        "values('" + hfIdcliente.Value + "','" + txtTratamiento.Text + "'," & _
        "'" + txtobservacion.Text + "','true','" + txtContraIndica.Text + "','" + chkComp.Checked.ToString + "','" + chkCrio.Checked.ToString + "'," & _
        "'" + chkLass.Checked.ToString + "','" + chkPara.Checked.ToString + "','" + chklassb.Checked.ToString + "'," & _
        "'" + chkelec.Checked.ToString + "','" + chkMaqr.Checked.ToString + "','" + chkMaqh.Checked.ToString + "'," & _
        "'" + chkTral.Checked.ToString + "','" + chkTrac.Checked.ToString + "','" + txtcomp.Text + "','" + txtcrio.Text + "','" + txtlass.Text + "'," & _
        "'" + txtpara.Text + "','" + txtbarr.Text + "','" + txtelec.Text + "','" + txthomb.Text + "','" + txtrodi.Text + "','" + txtlumb.Text + "'," & _
        "'" + txtcerv.Text + "','" + cmbProtocolos.SelectedValue.ToString + "')")
        gTratamientos = funciones.creadataset("select id,left(cast(tratamiento as varchar),70) as tratamiento,left(cast(observaciones as varchar),70) as observaciones,vigente from tratamientos where idcliente='" + hfIdcliente.Value + "'", gTratamientos)

        llenaDatos()

        btnAnueva.Visible = True
        btnGnueva.Visible = False
        btnmodifica.Visible = True
        btnCancelar.Visible = False
        txtTratamiento.ReadOnly = False
        txtobservacion.ReadOnly = False
        txtContraIndica.ReadOnly = False
        iniciaTextarea()
    End Sub

    Protected Sub gTratamientos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gTratamientos.ItemCommand
        Dim accion As String = e.CommandName
        Select Case accion
            Case "seleccionar"
                funciones.grabaDatos("update tratamientos set vigente='false' where idcliente='" + hfIdcliente.Value + "'; " & _
                "update tratamientos set vigente='true' where id='" + gTratamientos.DataKeys.Item(e.Item.ItemIndex).ToString + "' and idcliente='" + hfIdcliente.Value + "'")
                gTratamientos = funciones.creadataset("select id,left(cast(tratamiento as varchar),70) as tratamiento,left(cast(observaciones as varchar),70) as observaciones,vigente from tratamientos where idcliente='" + hfIdcliente.Value + "'", gTratamientos)

                llenaDatos()

        End Select
    End Sub

    Protected Sub btnmodifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnmodifica.Click
        funciones.grabaDatos("update tratamientos set tratamiento='" + txtTratamiento.Text + "',observaciones='" + txtobservacion.Text + "',contraindica='" + txtContraIndica.Text + "'," & _
        "comp='" + chkComp.Checked.ToString + "',crio='" + chkCrio.Checked.ToString + "',lass='" + chkLass.Checked.ToString + "'," & _
        "para='" + chkPara.Checked.ToString + "',elec='" + chkelec.Checked.ToString + "',maqr='" + chkMaqr.Checked.ToString + "'," & _
        "maqh='" + chkMaqh.Checked.ToString + "',tral='" + chkTral.Checked.ToString + "',trac='" + chkTrac.Checked.ToString + "'," & _
        "lassb='" + chklassb.Checked.ToString + "',txtcomp='" + txtcomp.Text + "',txtcrio='" + txtcrio.Text + "'," & _
        "txtlass='" + txtlass.Text + "',txtpara='" + txtpara.Text + "',txtbarr='" + txtbarr.Text + "', txtelec='" + txtelec.Text + "'," & _
        "txthomb='" + txthomb.Text + "',txtrodi='" + txtrodi.Text + "',txtlumb='" + txtlumb.Text + "'," & _
        "txtcerv='" + txtcerv.Text + "',idProtocolo='" + cmbProtocolos.SelectedValue + "' where id='" + hfIdtratamiento.Value + "'")

        gTratamientos = funciones.creadataset("select id,left(cast(tratamiento as varchar),70) as tratamiento,left(cast(observaciones as varchar),70) as observaciones,vigente from tratamientos where idcliente='" + hfIdcliente.Value + "'", gTratamientos)
    End Sub

    Protected Sub btnAnueva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnueva.Click
        btnAnueva.Visible = False
        btnGnueva.Visible = True
        btnmodifica.Visible = False
        btnCancelar.Visible = True
        txtTratamiento.ReadOnly = False
        txtobservacion.ReadOnly = False
        txtContraIndica.ReadOnly = False
        iniciaTextarea()
        cmbProtocolos.SelectedValue = 0

        iniciaControles()

    End Sub

    Protected Sub btnCancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        btnCancelar.Visible = False
        If gTratamientos.Items.Count = 0 Then
            btnAnueva.Visible = False
            btnGnueva.Visible = True
            btnmodifica.Visible = False
            txtTratamiento.ReadOnly = True
            txtobservacion.ReadOnly = True
            txtContraIndica.ReadOnly = True
            iniciaTextarea()
        Else
            llenaDatos()

            btnAnueva.Visible = True
            btnGnueva.Visible = False
            btnmodifica.Visible = True
            txtTratamiento.ReadOnly = False
            txtobservacion.ReadOnly = False
            txtContraIndica.ReadOnly = False
            iniciaTextarea()
        End If
    End Sub

    Sub llenaDatos()
        Dim valores(,) As String = funciones.leerValores("select tratamiento,observaciones,id,contraindica," & _
                "comp,crio,lass,para,lassb,elec,maqr,maqh,tral,trac,txtcomp,txtcrio,txtlass,txtpara,txtbarr,txtelec,txthomb,txtrodi,txtlumb,txtcerv" & _
                ",idProtocolo from tratamientos where vigente='true' and idcliente='" + hfIdcliente.Value + "'", 25)
        txttratamiento.Text = valores(0, 0)
        txtobservacion.Text = valores(1, 0)
        hfIdtratamiento.Value = valores(2, 0)
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
        txtlumb.Text = valores(22, 0)
        txtcerv.Text = valores(23, 0)
        cmbProtocolos.SelectedValue = valores(24, 0)
    End Sub
    Sub iniciaControles()
        txttratamiento.Text = ""
        txtobservacion.Text = ""
        txtContraIndica.Text = ""
        chkComp.Checked = False
        chkCrio.Checked = False
        chkLass.Checked = False
        chkPara.Checked = False
        chklassb.Checked = False
        chkelec.Checked = False
        chkMaqr.Checked = False
        chkMaqh.Checked = False
        chkTral.Checked = False
        chkTrac.Checked = False
        txtcomp.Text = ""
        txtcrio.Text = ""
        txtlass.Text = ""
        txtpara.Text = ""
        txtbarr.Text = ""
        txtelec.Text = ""
        txtrodi.Text = ""
        txthomb.Text = ""
        txtlumb.Text = ""
        txtcerv.Text = ""
    End Sub

    Protected Sub gSeguimiento_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gSeguimiento.ItemCommand
        Dim Id As String = gSeguimiento.DataKeys.Item(e.Item.ItemIndex).ToString
        Dim valores(,) As String = funciones.leerValores("select observacion,nombre,chc,us,cif,laser,parafina," & _
                "makina,cerv,vectra,crioterapia,lum,dialermia,libres,isotermicos,williams," & _
                "bicicleta,caminadora,polainas,ocupacional,postulares,marcha,ligasa,ligasr,ligasv,ligasn from (SELECT observacion,idTerapista, " & _
                "chc,us,cif,laser,parafina,makina,cerv,vectra,crioterapia,lum,dialermia,libres,isotermicos,williams," & _
                "bicicleta,caminadora,polainas,ocupacional,postulares,marcha,ligasa,ligasr,ligasv,ligasn FROM bitTratamiento " & _
                " WHERE idcita='" + Id + "') as temp left join terapistas on id=idTerapista", 26)
        txtUltima.Text = valores(0, 0)
        If valores(1, 0).ToString.Trim <> "" Then
            lblUltima.Text = valores(1, 0)
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
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub cmbProtocolos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbProtocolos.SelectedIndexChanged
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + cmbProtocolos.SelectedValue + "'"
        txttratamiento.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Dosificación".PadRight(30, " ") + "|" + "Tiempo".PadRight(15, " ") + "|" + Chr(10)
        txttratamiento.Text = txttratamiento.Text + "-".PadRight(105, "-") + Chr(10)
        txtContraIndica.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Precauciones".PadRight(30, " ") + "|" + Chr(10)
        txtContraIndica.Text = txtContraIndica.Text + "-".PadRight(63, "-") + Chr(10)
        txtobservacion.Text = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Intensidad".PadRight(25, " ") + "|" + Chr(10)
        txtobservacion.Text = txtobservacion.Text + "-".PadRight(58, "-") + Chr(10)
        conexion.Open()
        Dim oLeer As SqlDataReader = comando.ExecuteReader
        Do While oLeer.Read
            txttratamiento.Text = txttratamiento.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(1).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(3).ToString.PadRight(15, " ") + "|" + Chr(10)
            txtContraIndica.Text = txtContraIndica.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(4).ToString.PadRight(30, " ") + "|" + Chr(10)
            txtobservacion.Text = txtobservacion.Text + "|" + oLeer.GetValue(0).ToString.PadRight(30, " ") + "|" + oLeer.GetValue(2).ToString.PadRight(25, " ") + "|" + Chr(10)
        Loop
        iniciaTextarea()

        oLeer.Close()
        conexion.Close()
    End Sub

    Protected Sub btnContra_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnContra.Click
        btnTrata.ForeColor = Drawing.Color.Red
        btnContra.ForeColor = Drawing.Color.Gray
        btnObserva.ForeColor = Drawing.Color.Red
        lbltitulo.Text = "CONTRAINDICACIONES"
        txtTratamiento.Visible = False
        txtContraIndica.Visible = True
        txtobservacion.Visible = False
    End Sub

    Protected Sub btnObserva_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnObserva.Click
        btnTrata.ForeColor = Drawing.Color.Red
        btnContra.ForeColor = Drawing.Color.Red
        btnObserva.ForeColor = Drawing.Color.Gray
        lbltitulo.Text = "OBSERVACIONES"
        txtTratamiento.Visible = False
        txtContraIndica.Visible = False
        txtobservacion.Visible = True
    End Sub

    Protected Sub btnTrata_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnTrata.Click
        iniciaTextarea()
        lbltitulo.Text = "TRATAMIENTO"
    End Sub

    Sub iniciaTextarea()
        btnTrata.ForeColor = Drawing.Color.Gray
        btnContra.ForeColor = Drawing.Color.Red
        btnObserva.ForeColor = Drawing.Color.Red
        lbltitulo.Text = "TRATAMIENTO"
        txtTratamiento.Visible = True
        txtContraIndica.Visible = False
        txtobservacion.Visible = False
    End Sub

    Protected Sub TextBox1_TextChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNombusca.TextChanged
        Panel6.Visible = True
        If txtNombusca.Text.Trim <> "" Then
            gBusca = funciones.creadataset("select idcliente,Nombre+' '+paterno+' '+materno as nombre from clientes where paterno + ' ' + materno + ' ' + nombre LIKE '%" + txtNombusca.Text + "%' OR " & _
                         "paterno + ' ' + nombre + ' ' + materno LIKE '%" + txtNombusca.Text + "%' OR nombre + ' ' + materno + ' ' + paterno LIKE '%" + txtNombusca.Text + "%' OR  " & _
                         "nombre + ' ' + paterno + ' ' + materno LIKE '%" + txtNombusca.Text + "%' OR materno + ' ' + paterno + ' ' + nombre LIKE '%" + txtNombusca.Text + "%' OR " & _
                         "materno + ' ' + nombre + ' ' + paterno LIKE '%" + txtNombusca.Text + "%' order by nombre,paterno,materno", gBusca)
        End If
        btnOtro.Visible = True
        If gBusca.Items.Count > 0 Then
            btnVacio.Visible = False
        Else
            btnVacio.Visible = True
        End If
        ModalPopupExtender2.Show()
        Panel1.Focus()
    End Sub

    Protected Sub gBusca_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gBusca.ItemCommand
        cmbProtocolos.Enabled = True
        Panel1.Visible = True
        gTratamientos.Visible = True
        gSeguimiento.Visible = True
        hfidCliente.Value = gBusca.DataKeys(e.Item.ItemIndex).ToString
        iniciaControles()
        txtNombusca.Enabled = False

        gTratamientos = funciones.creadataset("select id,left(cast(tratamiento as varchar),70) as tratamiento,left(cast(observaciones as varchar),70) as observaciones,vigente from tratamientos where idcliente='" + hfIdcliente.Value.Trim + "'", gTratamientos)
        gSeguimiento = funciones.creadataset("select  convert(varchar, fecha, 103) as fecha ,observacion,idcita from bitTratamiento where idcliente='" + hfIdcliente.Value.Trim + "' order by fecha", gSeguimiento)
        If gSeguimiento.Items.Count > 0 Then
            Panel2.Visible = True
        Else
            Panel6.Visible = False
            Panel2.Visible = False
        End If
        btnmodifica.Visible = False
        btnCancelar.Visible = False
        If gTratamientos.Items.Count = 0 Then
            btnAnueva.Visible = False
            btnGnueva.Visible = True
            btnmodifica.Visible = False
            txtTratamiento.ReadOnly = False
            txtobservacion.ReadOnly = False
            txtContraIndica.ReadOnly = False
        Else
            llenaDatos()

            btnAnueva.Visible = True
            btnGnueva.Visible = False
            btnmodifica.Visible = True
            txtTratamiento.ReadOnly = False
            txtobservacion.ReadOnly = False
            txtContraIndica.ReadOnly = False
        End If

        iniciaTextarea()
        
    End Sub

    Protected Sub btnOtro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnOtro.Click
        Panel1.Visible = False
        txtNombusca.Enabled = True
        Panel2.Enabled = False
        cmbProtocolos.Enabled = False
        cmbProtocolos.SelectedValue = "0"
        gTratamientos.Visible = False
        gSeguimiento.Visible = False

    End Sub
End Class
