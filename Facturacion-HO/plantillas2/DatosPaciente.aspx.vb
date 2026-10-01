Public Class DatosPaciente
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            Response.Redirect("login.aspx")
        End If
        If Not IsPostBack Then
            Dim strSQL As String
            Dim a() As String
            Dim dt As New DataTable
            Dim funciones As New FuncionesGenerales
            Dim clsdatos As New ClaseDatos
            Session.Add("fechaNacimientoAC", "")
            Session.Add("datosActualizar", "CodigoUsuario='" & Session("codigoUsuario") & "', FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)")
            Session.Add("banderaAC", Session("datosActualizar"))
            Session.Add("codigoPaciente", Request.QueryString("CodigoPaciente"))
            Session.Add("modulo", Request.QueryString("modulo"))
            Session.Add("entidadAC", "")
            Session.Add("municipioAC", "")
            Session.Add("nombresNew", "")
            Session.Add("papellidoNew", "")
            Session.Add("sapellidoNew", "")
            LlenaComboOcupacion()
            LlenaTipoPaciente()
            LlenaCliente()
            LlenarComboEstadoCivil()
            LlenarComboNacionalidad()
            LlenarCombEdoNacimiento()
            LlenarComboEntidad()
            LlenarCombosFechas()
            txtDireccion.Wrap = True
            ' el IF --- si viene de agenda, traigo los datos del paciente, de lo contrario solo cargo campos
            If Session("codigoPaciente") Is Nothing Then
                DDEdoNacimiento.SelectedValue = 31
                DDNacionalidad.SelectedValue = 223
                DDEstadoCivil.SelectedValue = 2
                DDocupacion.SelectedValue = 5
                DDestadosoficial.SelectedValue = 31 'YUCATAN
                LlenarComboMunicipio()
                DDmunicipios.SelectedValue = 50 'MÉRIDA
                LlenarComboLocalidades()
                DDlocalidades.SelectedValue = 1 'MÉRIDA'
                txtNombres.Focus()
                HdModalidad.Value = 1
            Else
                HdModalidad.Value = 2
                strSQL = "SELECT nombres,papellido,sapellido,calle as direccion,telefonocelular,telefonolocal,codigoPostal,nombreContacto, " & _
                     " telefonoContacto,email,genero,fechaNacimiento,curp,codigoOcupacion,codigoEstadoCivil, " & _
                     "CodigoEntidad ,CodigoEntidadNac,codigoNacionalidad,CodigoLocalidad,CodigoMunicipio,municipio,codigoCliente,codigoTipo,anotaciones " & _
                      "FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] where CodigoPaciente =" + Session("codigoPaciente")
                If clsdatos.cargatabla(strSQL, dt) = 0 Then
                    'BtnGuardar.Text = "Actualizar"
                    BotonGuardar.InnerText = "Actualizar"
                    paciente_titulo1.InnerHtml = "Paciente <small>Cambios</small>"
                    paciente_titulo2.InnerHtml = "<i class='fa fa-user'></i> Paciente"
                    paciente_titulo3.InnerText = "Datos del paciente"
                    txtNombres.Value = dt.Rows(0).Item("nombres")
                    TxtPapellido.Value = dt.Rows(0).Item("papellido")
                    TxtSapellido.Value = dt.Rows(0).Item("sapellido")
                    Session.Add("nombres", dt.Rows(0).Item("nombres"))
                    Session.Add("papellido", dt.Rows(0).Item("papellido"))
                    Session.Add("sapellido", dt.Rows(0).Item("sapellido"))
                    Session("nombresNew") = dt.Rows(0).Item("nombres")
                    Session("papellidoNew") = dt.Rows(0).Item("papellido")
                    Session("sapellidoNew") = dt.Rows(0).Item("sapellido")
                    txtDireccion.Text = dt.Rows(0).Item("direccion")
                    txtTelefonoMovil.Value = dt.Rows(0).Item("telefonocelular")
                    txtEmail.Text = dt.Rows(0).Item("email")
                    txtTelefonoMovil.Value = dt.Rows(0).Item("telefonocelular")
                    txtTelefonoFijo.Value = dt.Rows(0).Item("telefonolocal")
                    TxtNombreContacto.Value = dt.Rows(0).Item("nombreContacto")
                    TxtTelefonoContacto.Value = dt.Rows(0).Item("telefonoContacto")
                    Txtcodigopostal.Text = dt.Rows(0).Item("codigoPostal")
                    txtEmail.Text = dt.Rows(0).Item("email")
                    Txtcurp.Value = dt.Rows(0).Item("curp")
                    'LblMunicipioAnti.Text = dt.Rows(0).Item("municipio")
                    DDNacionalidad.SelectedValue = dt.Rows(0).Item("codigoNacionalidad")
                    If DDNacionalidad.SelectedItem.Value = 223 Then
                        DDEdoNacimiento.Visible = True
                    Else
                        DDEdoNacimiento.Visible = False
                    End If
                    DDestadosoficial.SelectedValue = dt.Rows(0).Item("CodigoEntidad")
                    LlenarComboMunicipio()
                    DDmunicipios.SelectedValue = dt.Rows(0).Item("CodigoMunicipio")
                    DDaseguradora.SelectedValue = dt.Rows(0).Item("CodigoCliente")
                    HdCodigoCliente.Value = dt.Rows(0).Item("CodigoCliente")
                    DDTipoPaciente.SelectedValue = dt.Rows(0).Item("CodigoTipo")
                    HdCodigoTipoPaciente.Value = dt.Rows(0).Item("CodigoTipo")
                    If Not IsDBNull(dt.Rows(0).Item("anotaciones")) Then
                        TxtAnotaciones.Text = dt.Rows(0).Item("anotaciones")
                    End If
                    LlenarComboLocalidades()
                    DDlocalidades.SelectedValue = dt.Rows(0).Item("CodigoLocalidad")
                    DDEdoNacimiento.SelectedValue = dt.Rows(0).Item("CodigoEntidadNac")
                    DDocupacion.SelectedValue = dt.Rows(0).Item("codigoOcupacion")
                    DDEstadoCivil.SelectedValue = dt.Rows(0).Item("codigoEstadoCivil")
                    a = Split(dt.Rows(0).Item("fechaNacimiento"), "/", -1)
                    DDdiasfecha.SelectedValue = a(0)
                    DDmesesFecha.SelectedValue = MonthName(a(1), True)
                    DDañosFecha.SelectedValue = a(2)
                    Dim genero As Integer = Asc(dt.Rows(0).Item("genero").ToString)
                    If genero = 72 Then
                        rbhombre.Checked = True
                        rbrdmujer.Checked = False
                    Else
                        rbhombre.Checked = False
                        rbrdmujer.Checked = True
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        If (Session("modulo") = "1") Then
            Response.Redirect("ConsultaEx.aspx")
        End If
        Response.Redirect("Agenda.aspx")
    End Sub
    Protected Sub LlenaComboOcupacion()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoOcupacion,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatOcupaciones] " & _
                  " where codigoEmpresa ='" & Session("codigoEmpresa") & "' and status= '1'"
        If funciones.llenadropdown(strSQL, DDocupacion) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub LlenaTipoPaciente()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoTipo,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatTipoPaciente] " & _
                 "where status = 1 And codigoEmpresa = " & Session("codigoEmpresa") & " " & _
                "order by descripcion"
        If funciones.llenadropdown(strSQL, DDTipoPaciente) = 0 Then
            DDTipoPaciente.SelectedValue = 1
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub LlenaCliente()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoCliente ,RazonSocial  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] " & _
                  "where status = 1 And codigoempresa = " & Session("codigoEmpresa") & " " & _
                  "order by RazonSocial"
        If funciones.llenadropdown(strSQL, DDaseguradora) = 0 Then
            DDaseguradora.SelectedValue = 1
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub LlenarComboEstadoCivil()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoEstadoCivil,descripcion FROM [" & clsDatos.BaseDatos & "].[dbo].[CatEstadoCivil] " & _
                 "where codigoEmpresa='" & Session("codigoEmpresa") & "' and status ='1'"
        If funciones.llenadropdown(strSQL, DDEstadoCivil) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarCombEdoNacimiento()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoEntidad,nombreEntidad FROM [" & clsDatos.BaseDatos & "].[dbo].[CatEntidades] " & _
                 " where status='1'"
        If funciones.llenadropdown(strSQL, DDEdoNacimiento) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarComboNacionalidad()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT CodigoNacionalidad,Nacionalidad  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatNacionalidades] " & _
                 "where status='1'"
        If funciones.llenadropdown(strSQL, DDNacionalidad) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub

    Protected Sub LlenarComboEntidad()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoEntidad,nombreEntidad FROM [" & clsDatos.BaseDatos & "].[dbo].[CatEntidades] " & _
                 " where status='1'"
        If funciones.llenadropdown(strSQL, DDestadosoficial) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarComboMunicipio()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoMunicipio,nombreMunicipio  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatMunicipios] " & _
                  "where codigoEntidad='" & DDestadosoficial.SelectedItem.Value & "' and status='1'" & _
                  "order by nombreMunicipio"
        If funciones.llenadropdown(strSQL, DDmunicipios) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarComboLocalidades()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoLocalidad,nombreLocalidad  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatLocalidades] " & _
                "where codigoEntidad='" & DDestadosoficial.SelectedItem.Value & "' and codigoMunicipio='" & DDmunicipios.SelectedValue & "' and status='1'" & _
                "order by nombreLocalidad"
        If funciones.llenadropdown(strSQL, DDlocalidades) = -1 Then
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
    End Sub
    Protected Sub LlenarCombosFechas()
        Dim nItems = New ListItem()
        Dim fmt As String = "00"
        'Dias del mes
        For i = 1 To 31 Step 1
            nItems.Value = i.ToString(fmt)
            nItems.Text = i.ToString(fmt)
            DDdiasfecha.Items.Add(nItems.ToString)
        Next
        'Meses del año
        For i = 1 To 12 Step 1
            nItems.Value = i.ToString
            nItems.Text = MonthName(i, True)
            DDmesesFecha.Items.Add(nItems.ToString)
        Next
        'Años
        For i = Date.Now.Year To Date.Now.Year - 110 Step -1
            nItems.Value = i
            nItems.Text = i
            DDañosFecha.Items.Add(nItems.ToString)
        Next
    End Sub
    Private Sub DDestadosoficial_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDestadosoficial.SelectedIndexChanged
        Session("entidadAC") = ",CodigoEntidad='" + DDestadosoficial.SelectedItem.Value + "'"
        LlenarComboMunicipio()
        LlenarComboLocalidades()
    End Sub
    Private Sub DDmunicipios_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDmunicipios.SelectedIndexChanged
        Session("municipioAC") = ",CodigoMunicipio='" + DDmunicipios.SelectedItem.Value + "',municipio='" + DDmunicipios.SelectedItem.Text + "'"
        LlenarComboLocalidades()
    End Sub
    Protected Sub BtnGuardar_Click(sender As Object, e As EventArgs)
        Dim strSQL As String
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        If BotonGuardar.InnerText = "Actualizar" Then
            If Session("datosActualizar") = Session("banderaAC") And Session("fechaNacimientoAC") = "" And Session("nombresNew") = Session("nombres") And Session("papellidoNew") = Session("papellido") And Session("sapellidoNew") = Session("sapellido") Then
                If (Session("modulo") = "1") Then
                    Response.Redirect("ConsultaEx.aspx")
                End If
                Exit Sub
            End If
            'Verificar si existe el nombre del paciente en la BD en caso de ser modificado
            Dim nombre, paterno, materno As String
            nombre = Session("nombresNew")
            paterno = Session("papellidoNew")
            materno = Session("sapellidoNew")
            If Session("nombresNew") <> Nothing Then
                If Session("nombresNew") <> Session("nombres") Or Session("papellidoNew") <> Session("papellido") Or Session("sapellidoNew") <> Session("sapellido") Then
                    strSQL = "SELECT codigoPaciente,nombres,papellido,sapellido FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] " & _
                    "where nombres='" & txtNombres.Value.Trim & "' and PApellido='" & TxtPapellido.Value.Trim & "' and SApellido='" & TxtSapellido.Value.Trim & "' and status='1' and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                    If clsDatos.cargatabla(strSQL, dt) = 0 Then
                        If dt.Rows.Count > 0 Then
                            LblMostarDecision.Text = "  El Paciente: " + dt.Rows(0).Item("nombres") + " " + dt.Rows(0).Item("papellido") + " " + dt.Rows(0).Item("sapellido") + " con código:  " + dt.Rows(0).Item("codigoPaciente").ToString + vbCr + "  YA EXISTE  " + vbCr + "Deseas traer la información de este paciente"
                            PanelDesicion.Visible = "true"
                            PanelDesicion.Focus()
                            HdPregunta.Value = "0"
                            HdCodigoPaciente.Value = dt.Rows(0).Item("codigoPaciente").ToString
                            Exit Sub
                        End If
                    End If
                End If
            End If

            'Compartiendo pacientes en actualizar datos  agregar en caso de no compartir "  and CodigoEmpresa='" & Session("codigoEmpresa") & "'  "
            strSQL = "UPDATE [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] SET " & Session("datosActualizar") + Session("fechaNacimientoAC") + Session("municipioAC") + Session("entidadAC") & " " & _
                     "WHERE CodigoPaciente ='" & Session("codigoPaciente") & "' AND status='1' "
            If ValidarFecha() Then
            Else
                Exit Sub
            End If
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                LimpiarVariables()
                LblMensajeAviso.Text = "  Los datos del paciente se han actualizado "
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                Dim modulo, modalidad As String
                modulo = Session("modulo")
                modalidad = Session("modalidad")
                If Session("modulo") = "1" Or Session("modalidad") = "AG" Then
                    strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set Codigocliente =" & HdCodigoCliente.Value & ",codigoTipo =" & HdCodigoTipoPaciente.Value & " " & _
                             "where FolioConsulta ='" & Session("folioConsulta") & "'"
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        If Session("modalidad") = "AG" Then
                            Response.Redirect("Agenda.aspx")
                        End If
                        If (Session("modulo") = "1") Then
                            Response.Redirect("ConsultaEx.aspx")
                        End If
                    Else
                        LblMensajeCritico.Text = clsDatos.MensajeError
                        PanelCritico.Visible = True
                        PanelCritico.Focus()
                    End If
                End If

            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        Else
            If ValidarFecha() Then
            Else
                Exit Sub
            End If
            If Txtcodigopostal.Text = "" Then
                Txtcodigopostal.Text = "000000"
            End If
            strSQL = "SELECT codigoPaciente,nombres,papellido,sapellido FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] " & _
                 "where nombres='" & txtNombres.Value.Trim & "' and PApellido='" & TxtPapellido.Value.Trim & "' and SApellido='" & TxtSapellido.Value.Trim & "' and status='1' and codigoEmpresa='" & Session("codigoEmpresa") & "'"
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    LblMostarDecision.Text = "  El Paciente: " + dt.Rows(0).Item("nombres") + " " + dt.Rows(0).Item("papellido") + " " + dt.Rows(0).Item("sapellido") + " con código:  " + dt.Rows(0).Item("codigoPaciente").ToString + vbCr + "  YA EXISTE  " + vbCr + "Deseas traer la información de este paciente"
                    HdPregunta.Value = "1"
                    PanelDesicion.Visible = "true"
                    PanelDesicion.Focus()
                    HdCodigoPaciente.Value = dt.Rows(0).Item("codigoPaciente").ToString

                Else
                    strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] " & _
                                             " where codigoFoliador='4'  and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                    If clsDatos.cargatabla(strSQL, dt) = 0 Then
                        Dim IncrementoFoliador As Integer
                        IncrementoFoliador = dt.Rows(0).Item("consecutivo") + 1
                        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] set consecutivo='" & IncrementoFoliador & "' " & _
                                      "where codigoFoliador='4' and codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                                "insert into  [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] " & _
                                 "VALUES (" & IncrementoFoliador & ",UPPER('" & txtNombres.Value.Trim & "'),UPPER('" & TxtPapellido.Value.Trim & "'),UPPER('" & TxtSapellido.Value.Trim & "'), CASE WHEN '" & rbhombre.Checked & "'='False' THEN 'M' ELSE 'H' END," & _
                                "'" & DDdiasfecha.SelectedValue + "/" + DDmesesFecha.SelectedItem.Text.TrimEnd(".") + "/" + DDañosFecha.SelectedValue & "'," & DDEstadoCivil.SelectedValue & ",'" & txtTelefonoFijo.Value & "','" & txtTelefonoMovil.Value & "',UPPER('" & TxtNombreContacto.Value.Trim & "'),'" & TxtTelefonoContacto.Value & "'" & _
                                ",UPPER('" & Txtcurp.Value & "'),'" & txtEmail.Text & "'," & DDEdoNacimiento.SelectedValue & "," & DDocupacion.SelectedValue & "," & DDNacionalidad.SelectedValue & ",UPPER('" & txtDireccion.Text.Trim & "')," & DDlocalidades.SelectedItem.Value & ",'" & DDlocalidades.SelectedItem.Text & "'," & DDmunicipios.SelectedItem.Value & ",'" & DDmunicipios.SelectedItem.Text & "'," & _
                                "" & DDestadosoficial.SelectedItem.Value & "," & Txtcodigopostal.Text & ",1," & Session("codigoEmpresa") & "," & Session("codigoUsuario") & ",convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & DDaseguradora.SelectedValue & "," & DDTipoPaciente.SelectedValue & ",'" & TxtAnotaciones.Text & "')"
                        clsDatos.cargaComando(strSQL)
                        If clsDatos.ejecutar() = 0 Then
                            LblMensajeAviso.Text = " Los datos del paciente se han guardado"
                            PanelAvisos.Visible = True
                            PanelAvisos.Focus()
                            If Session("modalidad") = "AG" Then
                                Session("codigoPaciente") = IncrementoFoliador
                                moverAgregar()
                                Exit Sub

                            End If
                            LblMostarDecision.Text = "   ¿Desea agregar otro paciente?"
                            HdPregunta.Value = "2"
                            PanelDesicion.Visible = "true"
                            PanelDesicion.Focus()

                        Else
                            LblMensajeCritico.Text = clsDatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Function ValidarFecha() As Boolean
        Dim fecha, mes As String
        mes = DDmesesFecha.SelectedItem.Text.TrimEnd(".")
        Select Case mes
            Case "ene"
                mes = "1"
            Case "feb"
                mes = "2"
            Case "mar"
                mes = "3"
            Case "abr"
                mes = "4"
            Case "may"
                mes = "5"
            Case "jun"
                mes = "6"
            Case "jul"
                mes = "7"
            Case "ago"
                mes = "8"
            Case "sep"
                mes = "9"
            Case "oct"
                mes = "10"
            Case "nov"
                mes = "11"
            Case "dic"
                mes = "12"
        End Select
        fecha = DDdiasfecha.SelectedValue + "/" + mes + "/" + DDañosFecha.SelectedValue
        If Convert.ToDateTime(fecha) > Date.Now.ToString("dd,MM,yyyy") Then
            LblMensajeAdvertencia.Text = "  Verificar fecha de nacimiento "
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
            Return False
        End If
        Return True
    End Function

    Private Sub limpiar()
        Txtcodigopostal.Text = ""
        Txtcurp.Value = ""
        txtDireccion.Text = ""
        txtEmail.Text = ""
        TxtNombreContacto.Value = ""
        txtNombres.Value = ""
        TxtPapellido.Value = ""
        TxtSapellido.Value = ""
        TxtTelefonoContacto.Value = ""
        txtTelefonoFijo.Value = ""
        txtTelefonoMovil.Value = ""
        DDdiasfecha.SelectedValue = "01"
        DDmesesFecha.SelectedValue = "ene."
        DDañosFecha.SelectedValue = "2016"
        txtNombres.Focus()
    End Sub

    Private Sub LimpiarVariables()
        Session("fechaNacimientoAC") = ""
        Session("datosActualizar") = Session("banderaAC")
        Session("entidadAC") = Nothing
        Session("municipioAC") = Nothing
        Session("nombresNew") = Nothing
        Session("papellidoNew") = Nothing
        Session("sapellidoNew") = Nothing
    End Sub

    Private Sub txtDireccion_TextChanged(sender As Object, e As EventArgs) Handles txtDireccion.TextChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",Calle=UPPER('" + txtDireccion.Text + "')"
        Session("datosActualizar") = update
    End Sub
    Private Sub txtEmail_TextChanged(sender As Object, e As EventArgs) Handles txtEmail.TextChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",Email='" + txtEmail.Text + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDEstadoCivil_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDEstadoCivil.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",codigoEstadoCivil='" + DDEstadoCivil.SelectedValue.ToString + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub Txtcurp_ServerChange(sender As Object, e As EventArgs) Handles Txtcurp.ServerChange
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",Curp=UPPER('" + Txtcurp.Value + "')"
        Session("datosActualizar") = update
    End Sub
    Private Sub txtTelefonoFijo_ServerChange(sender As Object, e As EventArgs) Handles txtTelefonoFijo.ServerChange
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",TelefonoLocal='" + txtTelefonoFijo.Value + "'"
        Session("datosActualizar") = update
    End Sub
    Private Sub txtTelefonoMovil_ServerChange(sender As Object, e As EventArgs) Handles txtTelefonoMovil.ServerChange
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",TelefonoCelular='" + txtTelefonoMovil.Value + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub TxtNombreContacto_ServerChange(sender As Object, e As EventArgs) Handles TxtNombreContacto.ServerChange
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",NombreContacto=UPPER('" + TxtNombreContacto.Value + "')"
        Session("datosActualizar") = update
    End Sub
    Private Sub TxtTelefonoContacto_ServerChange(sender As Object, e As EventArgs) Handles TxtTelefonoContacto.ServerChange
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",TelefonoContacto='" + TxtTelefonoContacto.Value + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDEdoNacimiento_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDEdoNacimiento.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",CodigoEntidadNac='" + DDEdoNacimiento.SelectedValue + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDocupacion_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDocupacion.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",CodigoOcupacion='" + DDocupacion.SelectedValue + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDaseguradora_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDaseguradora.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",codigoCliente='" + DDaseguradora.SelectedValue + "'"
        Session("datosActualizar") = update
        HdCodigoCliente.Value = DDaseguradora.SelectedValue
    End Sub

    Private Sub DDTipoPaciente_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDTipoPaciente.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",codigoTipo='" + DDTipoPaciente.SelectedValue + "'"
        Session("datosActualizar") = update
        HdCodigoTipoPaciente.Value = DDTipoPaciente.SelectedValue
    End Sub

    Private Sub DDNacionalidad_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDNacionalidad.SelectedIndexChanged
        If DDNacionalidad.SelectedItem.Value = 223 Then
            DDEdoNacimiento.Visible = True
            lugar_nac_txt.InnerText = "Lugar de nacimiento"
        Else
            DDEdoNacimiento.Visible = False
            lugar_nac_txt.InnerText = ""
        End If
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",CodigoNacionalidad='" + DDNacionalidad.SelectedItem.Value + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDlocalidades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDlocalidades.SelectedIndexChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",CodigoLocalidad='" + DDlocalidades.SelectedItem .Value + "',Localidad='" + DDlocalidades.SelectedItem.Text + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub Txtcodigopostal_TextChanged(sender As Object, e As EventArgs) Handles Txtcodigopostal.TextChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",CodigoPostal='" + Txtcodigopostal.Text + "'"
        Session("datosActualizar") = update
    End Sub
    Private Sub TxtAnotaciones_TextChanged(sender As Object, e As EventArgs) Handles TxtAnotaciones.TextChanged
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",anotaciones='" + TxtAnotaciones.Text + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub DDdiasfecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDdiasfecha.SelectedIndexChanged
        Session("fechaNacimientoAC") = ",FechaNacimiento='" & DDdiasfecha.SelectedValue + "/" + DDmesesFecha.SelectedItem.Text.TrimEnd(".") + "/" + DDañosFecha.SelectedValue & "'"
    End Sub
    Private Sub DDmesesFecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDmesesFecha.SelectedIndexChanged
        Session("fechaNacimientoAC") = ",FechaNacimiento='" & DDdiasfecha.SelectedValue + "/" + DDmesesFecha.SelectedItem.Text.TrimEnd(".") + "/" + DDañosFecha.SelectedValue & "'"
    End Sub

    Private Sub DDañosFecha_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDañosFecha.SelectedIndexChanged
        Session("fechaNacimientoAC") = ",FechaNacimiento='" & DDdiasfecha.SelectedValue + "/" + DDmesesFecha.SelectedItem.Text.TrimEnd(".") + "/" + DDañosFecha.SelectedValue & "'"
    End Sub

    Private Sub txtNombres_ServerChange(sender As Object, e As EventArgs) Handles txtNombres.ServerChange
        Session("nombresNew") = txtNombres.Value.Trim.ToString
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",Nombres=UPPER('" + txtNombres.Value.Trim.ToString + "')"
        Session("datosActualizar") = update

    End Sub
    Private Sub TxtPapellido_ServerChange(sender As Object, e As EventArgs) Handles TxtPapellido.ServerChange
        Session("papellidoNew") = TxtPapellido.Value.Trim.ToString
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",PApellido=UPPER('" + TxtPapellido.Value.Trim.ToString + "')"
        Session("datosActualizar") = update
    End Sub
    Private Sub TxtSapellido_ServerChange(sender As Object, e As EventArgs) Handles TxtSapellido.ServerChange
        Session("sapellidoNew") = TxtSapellido.Value.Trim.ToString
        Dim update As String
        update = Session("datosActualizar").ToString
        update += ",SApellido=UPPER('" + TxtSapellido.Value.Trim.ToString + "')"
        Session("datosActualizar") = update
    End Sub
    Private Sub rbhombre_CheckedChanged(sender As Object, e As EventArgs) Handles rbhombre.CheckedChanged
        Dim update As String
        Dim gen As String
        update = Session("datosActualizar").ToString
        If rbhombre.Checked = True Then
            gen = "H"
        Else
            gen = "M"
        End If
        update += ",genero='" + gen + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub rbrdmujer_CheckedChanged(sender As Object, e As EventArgs) Handles rbrdmujer.CheckedChanged
        Dim update As String
        Dim gen As String
        update = Session("datosActualizar").ToString
        If rbhombre.Checked = True Then
            gen = "H"
        Else
            gen = "M"
        End If
        update += ",genero='" + gen + "'"
        Session("datosActualizar") = update
    End Sub

    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False

    End Sub

    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case HdPregunta.Value
            Case "0"
                Response.Redirect("DatosPaciente.aspx?CodigoPaciente=" + HdCodigoPaciente.Value.ToString)
            Case "1"
                Response.Redirect("DatosPaciente.aspx?CodigoPaciente=" + HdCodigoPaciente.Value.ToString)
            Case "2"
                limpiar()

        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        Select Case HdPregunta.Value
            Case "0"
                txtNombres.Value = Session("nombres")
                TxtPapellido.Value = Session("papellido")
                TxtSapellido.Value = Session("sapellido")
                LimpiarVariables()
                txtNombres.Focus()
                Exit Sub
            Case "1"
                Exit Sub
            Case "2"
                Response.Redirect("Agenda.aspx")

        End Select
    End Sub
    Private Sub moverAgregar()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt, dt1, dt2 As New DataTable
        Dim codigoCliente, codigoTipo As Integer

        strSQL = "SELECT NumeroTurno  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                         " where codigoPaciente='" & Session("codigoPaciente") & "'  and codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEtapa<> '4' and fechaAgenda='" & Session("FechaAgenda") & "' " & _
                           "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAdvertencia.Text = "Ya se encuentra agendado el paciente en el turno" + dt.Rows(0).Item("NumeroTurno").ToString
                PanelAdvertencia.Visible = True
                PanelAdvertencia.Focus()
                Exit Sub
            Else
                strSQL = "SELECT count(*) as cont  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                         "where NumeroTurno='" & Session("TurnoAgenda") & "'  and codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEtapa<> '4' and fechaAgenda='" & Session("FechaAgenda") & "' " & _
                          "and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    If dt.Rows(0).Item("cont") > 0 Then
                        LblMensajeAdvertencia.Text = "El turno ya esta ocupado"
                        PanelAdvertencia.Visible = True
                        PanelAdvertencia.Focus()
                        Exit Sub
                    Else
                        ' /////////////////////////////   AGENDAR, MOVER  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
                        strSQL = "SELECT codigoCliente,codigoTipo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] " & _
                                     "where codigoPaciente=" & Session("codigoPaciente") & " and CodigoEmpresa=" & Session("codigoEmpresa") & " and status=1"
                        If clsDatos.cargatabla(strSQL, dt1) = 0 Then
                            If dt.Rows.Count > 0 Then
                                codigoCliente = dt1.Rows(0).Item("codigoCliente")
                                codigoTipo = dt1.Rows(0).Item("codigoTipo")
                            End If
                        End If
                        strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] " & _
                             " where codigoFoliador='1'  and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                        If clsDatos.cargatabla(strSQL, dt) = 0 Then
                            Dim IncrementoFoliador As Integer
                            IncrementoFoliador = dt.Rows(0).Item("consecutivo") + 1
                            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] set consecutivo='" & IncrementoFoliador & "' " & _
                              "where codigoFoliador='1' and codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                              "insert into [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda]  " & _
                              "(FolioConsulta,CodigoPaciente,FechaAgenda,Codigoconsultorio,CodigoEtapa,Codigocliente,CodigoUsuarioAgenda,NumeroTurno,HoraLLegada " & _
                               ",CodigoModalidad,FechaFinalizacion,CodigoUsuario,CodigoEmpresa,Status,FechaActualizacion,codigoTipo) " & _
                              "values ('C'+REPLICATE(0,9-LEN(" & IncrementoFoliador & "))+ CAST (" & IncrementoFoliador & " AS varchar)," & Session("codigoPaciente") & ",'" & Session("FechaAgenda") & "'," & Session("codigoConsultorio") & ",'1'," & _
                                "" & codigoCliente & "," & Session("codigoUsuario") & ",'" & Session("numeroTurno") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),'" & HdModalidad.Value & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & _
                                "" & Session("codigoUsuario") & "," & Session("codigoEmpresa") & ",'1',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & codigoTipo & ")"
                        End If
                    End If
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        Session("modalidad") = ""
                        Response.Redirect("Agenda.aspx")
                    Else
                        LblMensajeCritico.Text = clsDatos.MensajeError
                        PanelCritico.Visible = True
                        PanelCritico.Focus()
                    End If
                End If
            End If
        End If


    End Sub

End Class