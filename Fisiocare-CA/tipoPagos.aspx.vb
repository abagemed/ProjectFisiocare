Imports System.Data.SqlClient
Imports System.Data
Partial Class tipoPagos
    Inherits System.Web.UI.Page
    Public funciones As New miclases
    Sub llenaDoctoresenvio()
        dgridDoctores = funciones.creadataset("select iddoctore,nombre,paterno,materno from catDoctoresEnvio ", dgridDoctores)
        Dim cuenta As Integer = 0
        Do While cuenta < dgridDoctores.Items.Count
            CType(dgridDoctores.Items(cuenta).Cells(3).Controls(0), Button).CssClass = "boton"
            CType(dgridDoctores.Items(cuenta).Cells(3).Controls(0), Button).ForeColor = Drawing.Color.Red
            CType(dgridDoctores.Items(cuenta).Cells(3).Controls(0), Button).Font.Bold = False
            cuenta = cuenta + 1
        Loop
    End Sub
    Sub llenaDoctores()
        gridDoctores = funciones.creadataset("select ID,nombre,paterno,materno from catDoctores ", gridDoctores)
        Dim cuenta As Integer = 0
        Do While cuenta < gridDoctores.Items.Count
            CType(gridDoctores.Items(cuenta).Cells(3).Controls(0), Button).CssClass = "boton"
            CType(gridDoctores.Items(cuenta).Cells(3).Controls(0), Button).ForeColor = Drawing.Color.Red
            CType(gridDoctores.Items(cuenta).Cells(3).Controls(0), Button).Font.Bold = False
            cuenta = cuenta + 1
        Loop
    End Sub
    Sub llenaDoctorc()
        griddoctorc = funciones.creadataset("select iddoctorc,nombre,apaterno,amaterno,especialidad,direccion,telefono from catDoctorC ", griddoctorc)
        Dim cuenta As Integer = 0
        Do While cuenta < griddoctorc.Items.Count
            CType(griddoctorc.Items(cuenta).Cells(6).Controls(0), Button).CssClass = "boton"
            CType(griddoctorc.Items(cuenta).Cells(6).Controls(0), Button).ForeColor = Drawing.Color.Red
            CType(griddoctorc.Items(cuenta).Cells(6).Controls(0), Button).Font.Bold = False
            cuenta = cuenta + 1
        Loop
    End Sub
    Sub llenaclifac()
        gridclientes = funciones.creadataset("select iddatosfac,nombre,direccion,rfc,ciudad from catFacturas where activo='true' ", gridclientes)
    End Sub
   
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Dim funciones As New miclases
            llenaclifac()
            llenaDoctoresenvio()
            llenaDoctores()
            llenaDoctorc()
            funciones.llenacombos(cmbformapago, "select idpago,descripcion from catPagos where activo = 'true' ")
            funciones.llenacombos(cmbregimenfiscal, "select codigoRegFiscal,(CONVERT(varchar(10), codigoRegFiscal)+'-'+descripcionRegFiscal) as RegimenFiscal from CatRegimenFiscal where status = '1'")
            Try
                lblfechacita.Text = Context.Items("fecha").ToString.Trim
            Catch
                lblfechacita.Text = Request.QueryString("fecha").Trim
            End Try
            Try
                Dim pestaña As Int16 = Request.QueryString("pag")
                contenedor.ActiveTabIndex = pestaña
            Catch ex As Exception
                contenedor.ActiveTabIndex = 0
            End Try
        End If
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
    Protected Sub dlnkDocs_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles dlnkDocs.Click
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "insert into catDoctoresEnvio(nombre,paterno,materno) values(@nombre,@paterno,@materno)"
        comando.Parameters.Add(New SqlParameter("@nombre", SqlDbType.VarChar)).Value = dtxtnombre.Text.Trim
        comando.Parameters.Add(New SqlParameter("@paterno", SqlDbType.VarChar)).Value = dtxtpaterno.Text.Trim
        comando.Parameters.Add(New SqlParameter("@materno", SqlDbType.VarChar)).Value = dtxtmaterno.Text.Trim
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        limpiaCamposenvio()
        llenaDoctoresenvio()
        contenedor.ActiveTabIndex = 0
        'Response.Redirect("tipoPagos.aspx?pag=3&fecha=" + lblfechacita.Text.Trim)
    End Sub
    Protected Sub limpiaCamposenvio()

        dtxtpaterno.Text = ""
        dtxtmaterno.Text = ""
        dtxtnombre.Text = ""

    End Sub
    Protected Sub lnkaseguradora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkDocs.Click
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "insert into catDoctores(nombre,paterno,materno) values(@nombre,@paterno,@materno)"
        comando.Parameters.Add(New SqlParameter("@nombre", SqlDbType.VarChar)).Value = txtnombre.Text.Trim
        comando.Parameters.Add(New SqlParameter("@paterno", SqlDbType.VarChar)).Value = txtpaterno.Text.Trim
        comando.Parameters.Add(New SqlParameter("@materno", SqlDbType.VarChar)).Value = txtmaterno.Text.Trim
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        limpiaCamposremitente()
        llenaDoctores()
        contenedor.ActiveTabIndex = 1
        'Response.Redirect("tipoPagos.aspx?pag=3&fecha=" + lblfechacita.Text.Trim)
    End Sub
    Protected Sub limpiaCamposremitente()

        txtpaterno.Text = ""
        txtmaterno.Text = ""
        txtnombre.Text = ""

    End Sub
    Protected Sub Linkgrabardc_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Linkgrabardc.Click
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = "insert into catDoctorC(nombre,apaterno,amaterno,especialidad,direccion,telefono) values(@nombre,@paterno,@materno,@especialidad,@direccion,@telefono)"
        comando.Parameters.Add(New SqlParameter("@nombre", SqlDbType.VarChar)).Value = txtnombredc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@paterno", SqlDbType.VarChar)).Value = txtapaternodc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@materno", SqlDbType.VarChar)).Value = txtamaternodc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@especialidad", SqlDbType.VarChar)).Value = txtespecialidaddc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@direccion", SqlDbType.VarChar)).Value = txtdirecciondc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@telefono", SqlDbType.VarChar)).Value = txttelefonodc.Text.Trim
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        llenaDoctorc()
        limpiaCampos()
        contenedor.ActiveTabIndex = 2

        'Response.Redirect("tipoPagos.aspx?pag=3&fecha=" + lblfechacita.Text.Trim)
    End Sub
    Protected Sub limpiaCampos()
        
        txtapaternodc.Text = ""
        txtamaternodc.Text = ""
        txtnombredc.Text = ""
        txttelefonodc.Text = ""
        txtespecialidaddc.Text = ""
        txtdirecciondc.Text = ""

    End Sub
    Protected Sub dgridDoctores_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgridDoctores.CancelCommand
        dgridDoctores.EditItemIndex = -1
        llenaDoctoresenvio()
        contenedor.ActiveTabIndex = 0
    End Sub
    Protected Sub gridDoctores_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridDoctores.CancelCommand
        gridDoctores.EditItemIndex = -1
        llenaDoctores()
        contenedor.ActiveTabIndex = 1
    End Sub
    Protected Sub griddoctorc_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles griddoctorc.CancelCommand
        griddoctorc.EditItemIndex = -1
        llenaDoctorc()
        contenedor.ActiveTabIndex = 3
    End Sub
    Protected Sub dgridDoctores_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgridDoctores.EditCommand
        dgridDoctores.EditItemIndex = e.Item.ItemIndex
        llenaDoctoresenvio()
        Dim cuenta As Integer = 0
        Do While cuenta < dgridDoctores.Items.Count
            Try
                CType(dgridDoctores.Items(cuenta).Cells(3).Controls(2), Button).CssClass = "boton"
                CType(dgridDoctores.Items(cuenta).Cells(3).Controls(2), Button).ForeColor = Drawing.Color.Red
                CType(dgridDoctores.Items(cuenta).Cells(3).Controls(2), Button).Font.Bold = False
            Catch
            End Try
            cuenta = cuenta + 1
        Loop
        contenedor.ActiveTabIndex = 0
    End Sub
    Protected Sub gridDoctores_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridDoctores.EditCommand
        gridDoctores.EditItemIndex = e.Item.ItemIndex
        llenaDoctores()
        Dim cuenta As Integer = 0
        Do While cuenta < gridDoctores.Items.Count
            Try
                CType(gridDoctores.Items(cuenta).Cells(3).Controls(2), Button).CssClass = "boton"
                CType(gridDoctores.Items(cuenta).Cells(3).Controls(2), Button).ForeColor = Drawing.Color.Red
                CType(gridDoctores.Items(cuenta).Cells(3).Controls(2), Button).Font.Bold = False
            Catch
            End Try
            cuenta = cuenta + 1
        Loop
        contenedor.ActiveTabIndex = 1
    End Sub
    Protected Sub griddoctorc_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles griddoctorc.EditCommand
        griddoctorc.EditItemIndex = e.Item.ItemIndex
        llenaDoctorc()
        Dim cuenta As Integer = 0
        Do While cuenta < griddoctorc.Items.Count
            Try
                CType(griddoctorc.Items(cuenta).Cells(6).Controls(2), Button).CssClass = "boton"
                CType(griddoctorc.Items(cuenta).Cells(6).Controls(2), Button).ForeColor = Drawing.Color.Red
                CType(griddoctorc.Items(cuenta).Cells(6).Controls(2), Button).Font.Bold = False
            Catch
            End Try
            cuenta = cuenta + 1
        Loop
        contenedor.ActiveTabIndex = 2
    End Sub
    Protected Sub dgridDoctores_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles dgridDoctores.UpdateCommand
        Try
            Dim elnombre As String = CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim elpaterno As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            Dim elmaterno As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            Dim elid As String = dgridDoctores.DataKeys.Item(e.Item.ItemIndex).ToString
            funciones.grabaDatos("update catDoctoresEnvio set nombre='" + elnombre.Trim + "', paterno='" + elpaterno.Trim + "'," & _
            " materno='" + elmaterno.Trim + "' where iddoctore='" + elid.Trim + "'")
            dgridDoctores.EditItemIndex = -1
            llenaDoctoresenvio()
            contenedor.ActiveTabIndex = 0
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try
    End Sub

    Protected Sub gridDoctores_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridDoctores.UpdateCommand
        Try
            Dim elnombre As String = CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim elpaterno As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            Dim elmaterno As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            Dim elid As String = gridDoctores.DataKeys.Item(e.Item.ItemIndex).ToString
            funciones.grabaDatos("update catDoctores set nombre='" + elnombre.Trim + "', paterno='" + elpaterno.Trim + "'," & _
            " materno='" + elmaterno.Trim + "' where ID='" + elid.Trim + "'")
            gridDoctores.EditItemIndex = -1
            llenaDoctores()
            contenedor.ActiveTabIndex = 1
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try
    End Sub
    Protected Sub griddoctorc_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles griddoctorc.UpdateCommand
        Try
            Dim elnombre As String = CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim elpaterno As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            Dim elmaterno As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text
            Dim laespecialidad As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text
            Dim ladireccion As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text
            Dim eltelefono As String = CType(e.Item.Cells(5).Controls(0), TextBox).Text
            Dim elid As String = griddoctorc.DataKeys.Item(e.Item.ItemIndex).ToString
            funciones.grabaDatos("update catDoctorC set nombre='" + elnombre.Trim + "', apaterno='" + elpaterno.Trim + "'," & _
            " amaterno='" + elmaterno.Trim + "' , especialidad='" + laespecialidad.Trim + "', direccion='" + ladireccion.Trim + "', telefono='" + eltelefono.Trim + "' where iddoctorc='" + elid.Trim + "'")
            griddoctorc.EditItemIndex = -1
            llenaDoctorc()
            contenedor.ActiveTabIndex = 2
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try
    End Sub
    Protected Sub gridclientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridclientes.ItemCommand
        'Try
        hfIdDatos.Value = gridclientes.DataKeys.Item(e.Item.ItemIndex).ToString
        Dim valores(,) As String = funciones.leerValores("select nombre,rfc,calle,NoExt,Colonia," & _
        "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,email,isnull(codigoRegFiscal,'999') as codigoRegFiscal from catfacturas where iddatosfac='" + hfIdDatos.Value.ToString + "'", 14)
        txtnombreF.Text = valores(0, 0)
        txtrfc.Text = valores(1, 0)
        txtcalle.Text = valores(2, 0)
        txtnoext.Text = valores(3, 0)
        txtcolonia.Text = valores(4, 0)
        txtcp.Text = valores(5, 0)
        txtmunicipio.Text = valores(6, 0)
        txtciudad.Text = valores(7, 0)
        txtestado.Text = valores(8, 0)
        txtpais.Text = valores(9, 0)
        If txtpais.Text.Trim = "" Then
            txtpais.Text = "MEXICO"
        End If
        cmbformapago.SelectedValue = valores(10, 0)
        txtcuenta.Text = valores(11, 0)
        cmbregimenfiscal.SelectedValue = valores(13, 0)
        ModalPopupExtender1.Show()
        hfBandera.Value = "0"
        'Catch
        'Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        'End Try
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        txtnombreF.Text = ""
        txtrfc.Text = ""
        txtcalle.Text = ""
        txtnoext.Text = ""
        txtcolonia.Text = ""
        txtcp.Text = ""
        txtmunicipio.Text = ""
        txtciudad.Text = ""
        txtestado.Text = ""
        txtpais.Text = "MEXICO"
        cmbformapago.SelectedValue = "0"
        cmbregimenfiscal.SelectedValue = "616"
        txtcuenta.Text = ""
        hfBandera.Value = "1"
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub btnGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGrabar.Click
        Select Case hfBandera.Value
            Case "0"
                funciones.grabaDatos("update catfacturas set nombre='" + txtnombreF.Text.Trim + "', rfc='" + txtrfc.Text.Trim + "'," & _
                "calle='" + txtcalle.Text.Trim + "',Noext='" + txtnoext.Text.Trim + "',colonia='" + txtcolonia.Text.Trim + "',cp='" + txtcp.Text.Trim + "'," & _
                "municipio='" + txtmunicipio.Text.Trim + "',ciudad='" + txtciudad.Text.Trim + "',edo='" + txtestado.Text.Trim + "'," & _
                "pais='" + txtpais.Text.Trim + "', formadepago='" + cmbformapago.SelectedValue.ToString + "',nocuenta='" + txtcuenta.Text.Trim + "',codigoRegFiscal='" + cmbregimenfiscal.SelectedValue.ToString + "' " & _
                "where iddatosfac='" + hfIdDatos.Value.ToString + "'")
            Case "1"
                funciones.grabaDatos("insert into catfacturas(nombre,rfc,calle,NoExt,Colonia,Cp,Municipio,ciudad,edo,pais," & _
                "formadepago,nocuenta,activo) values('" + txtnombreF.Text.Trim + "','" + txtrfc.Text.Trim + "','" + txtcalle.Text.Trim + "'," & _
                "'" + txtnoext.Text.Trim + "','" + txtcolonia.Text.Trim + "','" + txtcp.Text.Trim + "','" + txtmunicipio.Text.Trim + "'," & _
                "'" + txtciudad.Text.Trim + "','" + txtestado.Text.Trim + "','" + txtpais.Text.Trim + "','" + cmbformapago.SelectedValue.ToString + "'," & _
                "'" + txtcuenta.Text.Trim + "','true','" + cmbregimenfiscal.SelectedValue.ToString + "')")
        End Select
        llenaclifac()
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