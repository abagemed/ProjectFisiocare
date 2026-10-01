Imports System.Data.SqlClient
Imports System.Data
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Partial Class terapias
    Inherits System.Web.UI.Page
    Dim fun As New miclases
    Public mireporte As New ReportDocument
    Sub cpaneles(ByVal bandera As Boolean)
        pnleditaM.Visible = bandera
        pnleditaE.Visible = bandera
        pnleditaO.Visible = bandera
        gridejercicios.Enabled = bandera
    End Sub

    Sub cpanelesM(ByVal bandera As Boolean)
        cpaneles(bandera)
        pnlgrabaM.Visible = Not (bandera)
        txtmedios.ReadOnly = bandera
    End Sub

    Sub cpanelesE(ByVal bandera As Boolean)
        cpaneles(bandera)
        pnlgrabaE.Visible = Not (bandera)
        txtejercicios.ReadOnly = bandera
    End Sub

    Sub cpanelesO(ByVal bandera As Boolean)
        cpaneles(bandera)
        pnlgrabaO.Visible = Not (bandera)
        txtotros.ReadOnly = bandera
    End Sub
    Sub llenaejercicios()
        txtmedios.Text = ""
        txtejercicios.Text = ""
        txtotros.Text = ""
        Dim conexion As SqlConnection = fun.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idTerapia,texto,tipo,id from ejercicios where idTerapia='" + lblidTerapia.Text.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        btneditaM.Enabled = False
        btneditaE.Enabled = False
        btneditaO.Enabled = False
        Dim cuenta As Integer = 0
        Dim cuentam As Integer = 0
        Dim cuentae As Integer = 0
        Dim cuentaO As Integer = 0
        gridejercicios = fun.creadataset("select idTerapia,texto,tipo,id from ejercicios " & _
        "where idTerapia='" + lblidTerapia.Text.Trim + "'", gridejercicios)
        Dim aux As Integer = gridejercicios.Items.Count
        Do While leer.Read
            Select Case leer.GetValue(2).ToString.Trim
                Case "M"
                    CType(gridejercicios.Items(cuentam).Cells(0).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentam).Cells(0).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtmedios.Text = leer.GetValue(1).ToString.Trim
                    lblidm.Text = leer.GetValue(3).ToString.Trim
                    cuentam = cuentam + 1
                    btneditaM.Enabled = True
                Case "E"
                    CType(gridejercicios.Items(cuentae).Cells(1).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentae).Cells(1).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtejercicios.Text = leer.GetValue(1).ToString
                    lblide.Text = leer.GetValue(3).ToString.Trim
                    cuentae = cuentae + 1
                    btneditaE.Enabled = True
                Case "O"
                    CType(gridejercicios.Items(cuentaO).Cells(2).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentaO).Cells(2).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtotros.Text = leer.GetValue(1).ToString
                    lblido.Text = leer.GetValue(3).ToString.Trim
                    cuentaO = cuentaO + 1
                    btneditaO.Enabled = True
            End Select
            gridejercicios.Visible = True
            pnlImpejercicios.Visible = True
        Loop
        leer.Close()
        conexion.Close()
        If cuentam < cuentae Then
            If cuentae < cuentaO Then
                cuenta = cuentaO
            Else
                cuenta = cuentae
            End If
        Else
            If cuentam < cuentaO Then
                cuenta = cuentaO
            Else
                cuenta = cuentam
            End If
        End If
        Do While cuenta < aux
            gridejercicios.Items(cuenta).Visible = False
            cuenta = cuenta + 1
        Loop
    End Sub
    Sub limpia()
        txtPoliza.Text = ""
        txtCertificado.Text = ""
        txtNosiniestro.Text = ""
        txtEdad.Text = ""
        txtResponsable.Text = ""
        txtMedico.Text = ""
        'txtDiagnostico.Text = ""
        txtFechainicio.Text = ""
        txtNosesiones.Text = ""
        txtmedios.Text = ""
        txtejercicios.Text = ""
        txtotros.Text = ""
        alergias.Value = ""
        indicaciones_medicas.Value = ""
        contra_indicaciones.Value = ""
    End Sub
    Sub InsertaDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "insert into terapia(idCliente,poliza,certificado,noSiniestro,edad,responsable," & _
        "medico,fechaInicio,noSesiones,muscular,basica,laser,consulta,movimiento,adicional,hospital,IMedicas,CIndicaciones)" & _
        "values(@idcliente,@poliza,@certificado,@nosiniestro,@edad,@responsable,@medico," & _
        "@fechaInicio,@noSesiones,@muscular,@basica,@laser,@consulta,@movimiento,@adicional,@hospital,@IMedicas,@CIndicaciones) " & _
        "SELECT @idTerapia= SCOPE_IDENTITY() from terapia "
        comando.Parameters.Add(New SqlParameter("@idcliente", Data.SqlDbType.Int)).Value = lblIdcliente.Text.Trim
        comando.Parameters.Add(New SqlParameter("@poliza", Data.SqlDbType.VarChar)).Value = txtPoliza.Text.Trim
        comando.Parameters.Add(New SqlParameter("@certificado", Data.SqlDbType.VarChar)).Value = txtCertificado.Text.Trim
        comando.Parameters.Add(New SqlParameter("@noSiniestro", Data.SqlDbType.VarChar)).Value = txtNosiniestro.Text.Trim
        comando.Parameters.Add(New SqlParameter("@edad", Data.SqlDbType.Int)).Value = txtEdad.Text.Trim
        comando.Parameters.Add(New SqlParameter("@responsable", Data.SqlDbType.VarChar)).Value = txtResponsable.Text.Trim
        comando.Parameters.Add(New SqlParameter("@medico", Data.SqlDbType.VarChar)).Value = txtMedico.Text.Trim
        'comando.Parameters.Add(New SqlParameter("@diagnostico", Data.SqlDbType.Text)).Value = txtDiagnostico.Text.Trim
        comando.Parameters.Add(New SqlParameter("@fechaInicio", Data.SqlDbType.DateTime)).Value = txtFechainicio.Text.Trim
        comando.Parameters.Add(New SqlParameter("@noSesiones", Data.SqlDbType.SmallInt)).Value = txtNosesiones.Text.Trim
        comando.Parameters.Add(New SqlParameter("@muscular", Data.SqlDbType.Bit)).Value = chkMuscular.Checked
        comando.Parameters.Add(New SqlParameter("@basica", Data.SqlDbType.Bit)).Value = chkBasica.Checked
        comando.Parameters.Add(New SqlParameter("@laser", Data.SqlDbType.Bit)).Value = chkLaser.Checked
        comando.Parameters.Add(New SqlParameter("@consulta", Data.SqlDbType.Bit)).Value = chkConsulta.Checked
        comando.Parameters.Add(New SqlParameter("@movimiento", Data.SqlDbType.Bit)).Value = chkMovimiento.Checked
        comando.Parameters.Add(New SqlParameter("@adicional", Data.SqlDbType.Bit)).Value = chkAdicional.Checked
        comando.Parameters.Add(New SqlParameter("@hospital", Data.SqlDbType.Bit)).Value = chkHospital.Checked
        comando.Parameters.Add(New SqlParameter("@IMedicas", Data.SqlDbType.Text)).Value = indicaciones_medicas.Value.Trim
        comando.Parameters.Add(New SqlParameter("@CIndicaciones", Data.SqlDbType.Text)).Value = contra_indicaciones.Value.Trim
        comando.Parameters.Add(New SqlParameter("idTerapia", SqlDbType.Int)).Direction = ParameterDirection.Output
        
        conexion.Open()
        comando.ExecuteScalar()
        lblidTerapia.Text = comando.Parameters("idTerapia").Value
        conexion.Close()

        comando.CommandText = "insert into HMAntecedentesMedicos(idCliente, AlegiasPadecimientos, FechaActualizacion)" & _
       "values(@idclientep,@AlegiasPadecimientos,GETDATE()) "
        comando.Parameters.Add(New SqlParameter("@idclientep", Data.SqlDbType.Int)).Value = lblIdcliente.Text.Trim
        comando.Parameters.Add(New SqlParameter("@AlegiasPadecimientos", Data.SqlDbType.Text)).Value = alergias.Value.Trim
        conexion.Open()
        comando.ExecuteScalar()
        conexion.Close()

        comando.CommandText = ""
        If txtmedios.Text.Trim <> "" Then
            comando.CommandText = "insert into ejercicios(idTerapia,texto,tipo) values(@idTerapia1,@texto1,'M')"
            comando.Parameters.Add(New SqlParameter("@texto1", Data.SqlDbType.Text))
            comando.Parameters.Add(New SqlParameter("@idTerapia1", Data.SqlDbType.Int))
            comando.Parameters("@texto1").Value = txtmedios.Text.Trim
            comando.Parameters("@idTerapia1").Value = lblidTerapia.Text.Trim
        End If
        If txtejercicios.Text.Trim <> "" Then
            comando.CommandText = comando.CommandText + ";insert into ejercicios(idTerapia,texto,tipo) values(@idTerapia2,@texto2,'E')"
            comando.Parameters.Add(New SqlParameter("@texto2", Data.SqlDbType.Text))
            comando.Parameters.Add(New SqlParameter("@idTerapia2", Data.SqlDbType.Int))
            comando.Parameters("@texto2").Value = txtejercicios.Text.Trim
            comando.Parameters("@idTerapia2").Value = lblidTerapia.Text.Trim
        End If
        If txtotros.Text.Trim <> "" Then
            comando.CommandText = comando.CommandText + ";insert into ejercicios(idTerapia,texto,tipo) values(@idTerapia3,@texto3,'O')"
            comando.Parameters.Add(New SqlParameter("@texto3", Data.SqlDbType.Text))
            comando.Parameters.Add(New SqlParameter("@idTerapia3", Data.SqlDbType.Int))
            comando.Parameters("@texto3").Value = txtotros.Text.Trim
            comando.Parameters("@idTerapia3").Value = lblidTerapia.Text.Trim
        End If
        If comando.CommandText <> "" Then
            conexion.Open()
            comando.ExecuteNonQuery()
            conexion.Close()
        End If



    End Sub
    Sub LlenaDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select poliza,certificado,noSiniestro,edad,responsable," & _
        "medico,diagnostico,fechaInicio,noSesiones,muscular,basica,consulta,movimiento," & _
        "adicional,hospital,laser,IMedicas,CIndicaciones, HM.AlegiasPadecimientos  from terapia " & _
        "inner join HMAntecedentesMedicos HM on HM.idCliente=terapia.idCliente " & _
        "where idTerapia='" + lblidTerapia.Text.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            txtPoliza.Text = leer.GetValue(0).ToString.Trim
            txtCertificado.Text = leer.GetValue(1).ToString.Trim
            txtNosiniestro.Text = leer.GetValue(2).ToString.Trim
            txtEdad.Text = leer.GetValue(3).ToString.Trim
            txtResponsable.Text = leer.GetValue(4).ToString.Trim
            txtMedico.Text = leer.GetValue(5).ToString.Trim
            'txtDiagnostico.Text = leer.GetValue(6).ToString.Trim
            txtFechainicio.Text = Left(leer.GetValue(7).ToString.Trim, 10)
            txtNosesiones.Text = leer.GetValue(8).ToString.Trim
            chkMuscular.Checked = leer.GetValue(9).ToString.Trim
            chkBasica.Checked = leer.GetValue(10).ToString.Trim
            chkConsulta.Checked = leer.GetValue(11).ToString.Trim
            chkMovimiento.Checked = leer.GetValue(12).ToString.Trim
            chkAdicional.Checked = leer.GetValue(13).ToString.Trim
            chkHospital.Checked = leer.GetValue(14).ToString.Trim
            chkLaser.Checked = leer.GetValue(15).ToString.Trim
            alergias.Value = leer.GetValue(18).ToString.Trim
            indicaciones_medicas.Value = leer.GetValue(16).ToString.Trim
            contra_indicaciones.value = leer.GetValue(17).ToString.Trim

        End If
        leer.Close()
        conexion.Close()
        If txtFechainicio.Text.Trim = "01/01/1900" Then
            txtFechainicio.Text = ""
        End If
        llenaejercicios()
    End Sub
    Sub ActualizarDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "update terapia set idCliente=@idcliente,poliza=@poliza,certificado=@certificado," & _
        "noSiniestro=@nosiniestro,edad=@edad,responsable=@responsable,medico=@medico," & _
        "fechaInicio=@fechaInicio,noSesiones=@noSesiones,muscular=@muscular,basica=@basica,laser=@laser," & _
        "consulta=@consulta,movimiento=@movimiento,adicional=@adicional,hospital=@hospital,IMedicas=@IMedicas,CIndicaciones=@CIndicaciones where idTerapia='" + lblidTerapia.Text.Trim + "'"
        comando.Parameters.Add(New SqlParameter("@idcliente", Data.SqlDbType.Int)).Value = lblIdcliente.Text.Trim
        comando.Parameters.Add(New SqlParameter("@poliza", Data.SqlDbType.VarChar)).Value = txtPoliza.Text.Trim
        comando.Parameters.Add(New SqlParameter("@certificado", Data.SqlDbType.VarChar)).Value = txtCertificado.Text.Trim
        comando.Parameters.Add(New SqlParameter("@noSiniestro", Data.SqlDbType.VarChar)).Value = txtNosiniestro.Text.Trim
        comando.Parameters.Add(New SqlParameter("@edad", Data.SqlDbType.Int)).Value = txtEdad.Text.Trim
        comando.Parameters.Add(New SqlParameter("@responsable", Data.SqlDbType.VarChar)).Value = txtResponsable.Text.Trim
        comando.Parameters.Add(New SqlParameter("@medico", Data.SqlDbType.VarChar)).Value = txtMedico.Text.Trim
        'comando.Parameters.Add(New SqlParameter("@diagnostico", Data.SqlDbType.Text)).Value = txtDiagnostico.Text.Trim
        comando.Parameters.Add(New SqlParameter("@fechaInicio", Data.SqlDbType.DateTime)).Value = txtFechainicio.Text.Trim
        comando.Parameters.Add(New SqlParameter("@noSesiones", Data.SqlDbType.SmallInt)).Value = txtNosesiones.Text.Trim
        comando.Parameters.Add(New SqlParameter("@muscular", Data.SqlDbType.Bit)).Value = chkMuscular.Checked
        comando.Parameters.Add(New SqlParameter("@basica", Data.SqlDbType.Bit)).Value = chkBasica.Checked
        comando.Parameters.Add(New SqlParameter("@laser", Data.SqlDbType.Bit)).Value = chkLaser.Checked
        comando.Parameters.Add(New SqlParameter("@consulta", Data.SqlDbType.Bit)).Value = chkConsulta.Checked
        comando.Parameters.Add(New SqlParameter("@movimiento", Data.SqlDbType.Bit)).Value = chkMovimiento.Checked
        comando.Parameters.Add(New SqlParameter("@adicional", Data.SqlDbType.Bit)).Value = chkAdicional.Checked
        comando.Parameters.Add(New SqlParameter("@hospital", Data.SqlDbType.Bit)).Value = chkHospital.Checked
        comando.Parameters.Add(New SqlParameter("@IMedicas", Data.SqlDbType.Text)).Value = indicaciones_medicas.Value.Trim
        comando.Parameters.Add(New SqlParameter("@CIndicaciones", Data.SqlDbType.Text)).Value = contra_indicaciones.Value.Trim


        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()

        comando.CommandText = "update HMAntecedentesMedicos set AlegiasPadecimientos=@AlegiasPadecimientos where idcliente='" + lblIdcliente.Text.Trim + "'"
        comando.Parameters.Add(New SqlParameter("@AlegiasPadecimientos", Data.SqlDbType.Text)).Value = alergias.Value.Trim


        conexion.Open()
        comando.ExecuteNonQuery()

    End Sub
    Sub EstadoControles(ByVal bandera As Boolean)
        txtPoliza.ReadOnly = Not (bandera)
        txtCertificado.ReadOnly = Not (bandera)
        txtNosiniestro.ReadOnly = Not (bandera)
        txtEdad.ReadOnly = Not (bandera)
        txtResponsable.ReadOnly = Not (bandera)
        txtMedico.ReadOnly = Not (bandera)
        'txtDiagnostico.ReadOnly = Not (bandera)
        If bandera = False Then
            indicaciones_medicas.Disabled = True
            alergias.Disabled = True
            contra_indicaciones.Disabled = True
        Else
            indicaciones_medicas.Disabled = False
            alergias.Disabled = False
            contra_indicaciones.Disabled = False
        End If
        txtFechainicio.ReadOnly = Not (bandera)
        txtNosesiones.ReadOnly = Not (bandera)
        chkMuscular.Enabled = bandera
        chkBasica.Enabled = bandera
        chkConsulta.Enabled = bandera
        chkMovimiento.Enabled = bandera
        chkLaser.Enabled = bandera
        chkAdicional.Enabled = bandera
        chkHospital.Enabled = bandera
        txtmedios.ReadOnly = Not (bandera)
        txtejercicios.ReadOnly = Not (bandera)
        txtotros.ReadOnly = Not (bandera)
        lnkgrabar.Visible = bandera
    End Sub
    Sub validaDatos()
        If txtFechainicio.Text.Trim = "" Then
            txtFechainicio.Text = "01/01/1900"
        End If
        If txtNosesiones.Text.Trim = "" Then
            txtNosesiones.Text = 0
        End If
        If txtEdad.Text.Trim = "" Then
            txtEdad.Text = 0
        End If
    End Sub
    Sub llenaGrid()
        Dim funciones As New miclases
        gridTerapias = funciones.creadataset("select idTerapia, left(fechaInicio,12) as fechaInicio" & _
        " from terapia where idCliente='" + lblIdcliente.Text.Trim + "' order by fechaInicio", gridTerapias)


        '        gridTerapias = funciones.creadataset("select idTerapia,CD.Descripcion as diagnostico, left(fechaInicio,12) as fechaInicio,HD.IdHDiagnostico " & _
        '"from terapia T " & _
        '"inner join Agenda A on A.dtinicio= T.fechaInicio and T.idCliente=a.idCliente " & _
        '"inner join HMDiagnosticos HD on HD.IdCita = A.idCita " & _
        '"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
        '"where T.idCliente='" + lblIdcliente.Text.Trim + "' and HD.Status = 'True' order by fechaInicio", gridTerapias)


        If gridTerapias.Items.Count < 1 Then
            lblmensaje.Text = "NO SE ENCTRARON TERAPIAS PARA ESTE PACIENTE"
            gridTerapias.Visible = False
        Else
            gridTerapias.Visible = True
            lblmensaje.Text = ""
        End If
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            lblIdcliente.Text = Request.QueryString("idcliente").ToString.Trim
            For Each impresorasInstaladas As String In System.Drawing.Printing.PrinterSettings.InstalledPrinters
                cmbImpresoras.Items.Add(impresorasInstaladas)
            Next
            formato.Visible = False
            lblElnombre.Text = fun.leerValor("select elnombre from clientes where idcliente='" + lblIdcliente.Text.Trim + "'")
            txtEdad.Text = fun.leerValor("select edad from clientes where idcliente='" + lblIdcliente.Text.Trim + "'")
            txtMedico.Text = fun.leerValor("SELECT catDoctoresEnvio.nombre+' '+catDoctoresEnvio.paterno+' '+catDoctoresEnvio.materno as elnombre FROM clientes LEFT OUTER JOIN catDoctoresEnvio ON catDoctoresEnvio.iddoctore = clientes.idDoctore where idcliente='" + lblIdcliente.Text.Trim + "'")
            txtResponsable.Text = fun.leerValor("SELECT catResponsables.descripcion FROM clientes LEFT OUTER JOIN catResponsables ON catResponsables.idresponsable = clientes.idCenCostos where idcliente='" + lblIdcliente.Text.Trim + "'")
            llenaGrid()
            ViewState("accion") = "nada"
        End If
    End Sub

    Protected Sub lnkguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkAgregar.Click
        Grid.Visible = False
        formato.Visible = True
        ViewState("accion") = "insertar"
        EstadoControles(True)
        cpaneles(False)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkregresar.Click
        limpia()
        cpanelesM(False)
        cpanelesE(False)
        cpanelesO(False)
        pnlgrabaE.Visible = False
        pnlgrabaM.Visible = False
        pnlgrabaO.Visible = False
        Grid.Visible = True
        formato.Visible = False
        pnlModificar.Visible = False
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkgrabar.Click
        validaDatos()
        Select Case ViewState("accion")
            Case "insertar"
                InsertaDatos()
            Case "actualizar"
                ActualizarDatos()
                pnlModificar.Visible = False
        End Select
        limpia()
        cpanelesM(False)
        cpanelesE(False)
        cpanelesO(False)
        cpanelesM(False)
        cpanelesE(False)
        cpanelesO(False)
        pnlgrabaE.Visible = False
        pnlgrabaM.Visible = False
        pnlgrabaO.Visible = False
        Response.Redirect("terapias.aspx?idcliente=" + lblIdcliente.Text.Trim)
    End Sub
    Protected Sub gridTerapias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridTerapias.ItemCommand
        lblidTerapia.Text = gridTerapias.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        miIdterapia.Value = gridTerapias.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        LlenaDatos()
        Grid.Visible = False
        formato.Visible = True
        ViewState("accion") = "actualizar"
        EstadoControles(False)
        pnlModificar.Visible = True
        cpaneles(True)
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntFicha.Click
        EstadoControles(True)
    End Sub

    Protected Sub bntimprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bntimprimir.Click
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue

        mireporte.Load("c:\reporteFisio\formatoPrincipal.rpt")
        Dim conexion As SqlConnection = fun.conecta()
        Dim odataadapter As New SqlDataAdapter("select elnombre,poliza,certificado,nosiniestro,temp.edad," & _
        "responsable,medico,fechainicio,nosesiones,muscular,basica,laser,consulta,movimiento," & _
        "adicional,hospital " & _
        ",STUFF(" & _
        "(SELECT ', '  + p1.descripcion " & _
        "FROM  HMDiagnosticos as psp " & _
        "INNER JOIN CatDiagnosticos as p1 ON p1.IdDiagnostico = psp.IdDiagnostico and psp.status ='True' " & _
        "WHERE psp.IdCliente = temp.idCliente and psp.status='True' " & _
        "FOR XML PATH('')), " & _
        "1, 2, '') As 'diagnostico'  " & _
        "from (select idcliente,poliza,certificado,noSiniestro,edad,responsable,medico" & _
        ",convert(varchar(10),fechainicio,103) as fechaInicio,noSesiones,muscular,basica,laser," & _
        "consulta,movimiento,adicional,hospital from terapia  where idTerapia='" + lblidTerapia.Text.Trim + "') as temp " & _
        "inner join clientes on clientes.idcliente=temp.idcliente", conexion)
        Dim odataset As New DataSet
        odataadapter.Fill(odataset)
        mireporte.SetDataSource(odataset.Tables(0))
        'mireporte.PrintOptions.PrinterName = cmbImpresoras.SelectedValue.Trim
        'mireporte.PrintToPrinter(1, False, 0, 0)

        Mivar.Value = lblidTerapia.Text + " A"
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("folio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        visor_reporte.DataBind()
        visor_reporte.ReportSource = mireporte

        'exporta el rpt a pdf
        Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        Dim o As CrystalDecisions.Shared.ExportOptions
        o = New CrystalDecisions.Shared.ExportOptions
        o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        filedest.DiskFileName = "c:\reporteFisio\formatoterapia.pdf"
        o.ExportDestinationOptions = filedest.Clone
        mireporte.Export(o)
        filedest = Nothing
        o = Nothing

        'se abre el pdf en acrobat
        Dim Nombre As String
        Nombre = "c:\reporteFisio\formatoterapia.pdf"
        Response.Clear()
        Response.ContentType = "application/pdf"
        Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
        Response.WriteFile(Nombre)
        Response.Flush()
        Response.Close()

    End Sub

    Protected Sub btnnuevoM_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevoM.Click
        cpanelesM(False)
        lblauxedita.Value = "nuevo"
        txtmedios.Text = ""
    End Sub

    Protected Sub btneditaM_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditaM.Click
        cpanelesM(False)
        lblauxedita.Value = "editar"
    End Sub

    Protected Sub btneditaE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditaE.Click
        cpanelesE(False)
        lblauxedita.Value = "editar"
    End Sub
    Protected Sub ctnnuevoE_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevoE.Click
        cpanelesE(False)
        lblauxedita.Value = "nuevo"
        txtejercicios.Text = ""
    End Sub

    Protected Sub btneditaO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btneditaO.Click
        cpanelesO(False)
        lblauxedita.Value = "editar"
    End Sub

    Protected Sub btnnuevoO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevoO.Click
        cpanelesO(False)
        lblauxedita.Value = "nuevo"
        txtotros.Text = ""
    End Sub

    Protected Sub Button8_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button8.Click
        cpanelesM(True)
        llenaejercicios()
    End Sub

    Protected Sub Button7_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button7.Click
        grabadatosE(txtmedios.Text.Trim, lblidm.Text.Trim, "M")
        cpanelesM(True)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        grabadatosE(txtejercicios.Text.Trim, lblide.Text.Trim, "E")
        cpanelesE(True)
    End Sub

    Protected Sub Button11_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button11.Click
        grabadatosE(txtotros.Text.Trim, lblido.Text.Trim, "O")
        cpanelesO(True)
    End Sub
    Sub grabadatosE(ByVal mitexto As String, ByVal miid As String, ByVal mitipo As String)
        Dim conexion As SqlConnection = fun.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Select Case lblauxedita.Value
            Case "editar"
                comando.CommandText = comando.CommandText + ";update ejercicios set texto=@texto1 " & _
                "where id='" + miid + "' and tipo='" + mitipo + "'"
                comando.Parameters.Add(New SqlParameter("@texto1", Data.SqlDbType.Text)).Value = mitexto
            Case "nuevo"
                comando.CommandText = "insert into ejercicios(idTerapia,texto,tipo) values(@idTerapia1,@texto1,'" + mitipo.Trim + "')"
                comando.Parameters.Add(New SqlParameter("@texto1", Data.SqlDbType.Text)).Value = mitexto
                comando.Parameters.Add(New SqlParameter("@idTerapia1", Data.SqlDbType.Int)).Value = lblidTerapia.Text.Trim
        End Select
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        conexion.Dispose()
        llenaejercicios()
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        cpanelesE(True)
        llenaejercicios()
    End Sub

    Protected Sub Button12_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button12.Click
        cpanelesO(True)
        llenaejercicios()
    End Sub

    Protected Sub gridejercicios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridejercicios.ItemCommand
        Select Case e.CommandName
            Case "medios"
                lblidm.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(0).Controls(3), Label).Text
                txtmedios.Text = fun.leerValor("select texto from ejercicios where id='" + lblidm.Text + "'")
            Case "ejercicios"
                lblide.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(1).Controls(3), Label).Text
                txtejercicios.Text = fun.leerValor("select texto from ejercicios where id='" + lblide.Text + "'")
            Case "otros"
                lblido.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(2).Controls(3), Label).Text
                txtotros.Text = fun.leerValor("select texto from ejercicios where id='" + lblido.Text + "'")
        End Select
    End Sub
End Class
