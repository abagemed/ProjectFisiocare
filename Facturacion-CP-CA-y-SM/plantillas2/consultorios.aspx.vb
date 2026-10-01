Public Class consultorios
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            limpiarpanel()
            ocultarpaneles()
            cargaGridConsultorios()
            CargaDropDoctorCabecera()
            CargaDropEditDoctorCabecera()
            cargaListaDeDiagnosticos()
            cargaListaPermisos()
        End If
    End Sub

    Sub cargaListaDeDiagnosticos()  ' ++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT codigoListaDiagnostico,descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatDiagnosticoslistas] " & _
                 "where codigoEmpresa = 1 And status = 1 "
        If funciones.llenalistbox(strSql, LstEditDiagnosticos) = 0 And funciones.llenalistbox(strSql, LstAltaListaDiagnosticos) = 0 Then
        End If

    End Sub

    Sub insertListaDiagnosticosEditar()      '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "delete [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosListasDiagnosticos] " & _
                      "where codigoConsultorio = " & HdIdConsultorio.Value & " And codigoEmpresa = " & Session("codigoEmpresa") & " And status = 1 "
        clsDatos.cargaComando(strSql)
        clsDatos.ejecutar()
        For Each li As ListItem In LstEditDiagnosticos.Items
            If li.Selected = True Then
                strSql = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosListasDiagnosticos] " & _
                        "(codigoConsultorio ,codigoListaDiagnostico,codigoEmpresa,status,fechaActualizacion,CodigoUsuario) " & _
                        "values(" & HdIdConsultorio.Value & "," & li.Value & "," & Session("codigoEmpresa") & ",1,getdate()," & Session("codigoUsuario") & ")"
                clsDatos.cargaComando(strSql)
                clsDatos.ejecutar()
            End If
        Next
    End Sub
    Sub insertListaDiagnosticosAlta()      '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        For Each li As ListItem In LstAltaListaDiagnosticos.Items
            If li.Selected = True Then
                strSql = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosListasDiagnosticos] " & _
                        "(codigoConsultorio ,codigoListaDiagnostico,codigoEmpresa,status,fechaActualizacion,CodigoUsuario) " & _
                        "values(" & HdIdConsultorio.Value & "," & li.Value & "," & Session("codigoEmpresa") & ",1,getdate()," & Session("codigoUsuario") & ")"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = 0 Then
                Else
                    LblMensajeCritico.Text = "Error 105 : " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
            End If
        Next
    End Sub

    Sub leeListaDiagnosticos()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT codigoListaDiagnostico  " & _
                 "FROM[" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosListasDiagnosticos] " & _
                 "where codigoConsultorio=" & HdIdConsultorio.Value & " and CodigoEmpresa=" & Session("codigoEmpresa") & ""
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For x = 0 To dt.Rows.Count - 1
                    For Each li As ListItem In LstEditDiagnosticos.Items
                        If li.Value = dt.Rows(x).Item("codigoListaDiagnostico") Then
                            li.Selected = True
                        End If
                    Next
                Next
            End If
        End If
    End Sub

    Sub cargaListaPermisos()  ' ++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "  SELECT codigoUsuario,(nombre+' '+primerApellido+' '+segundoApellido) as descripcion " & _
                 "  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 " where codigoEmpresa =" & Session("codigoEmpresa") & " And status = 1 " & _
                 " order by descripcion "
        If funciones.llenalistbox(strSql, LstEditPermisos) = 0 And funciones.llenalistbox(strSql, LstAltaPermisos) = 0 Then
        End If

    End Sub

    Sub insertListaPermisosEditar()      '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = " delete FROM [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] " & _
                "where codigoConsultorio =" & HdIdConsultorio.Value & " And codigoEmpresa =" & Session("codigoEmpresa") & " And status = 1 "
        clsDatos.cargaComando(strSql)
        If clsDatos.ejecutar() <> 0 Then
            LblMensajeCritico.Text = "Error 102 : " + clsDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
        For Each li As ListItem In LstEditPermisos.Items
            If li.Selected = True Then
                strSql = "  insert into [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] " & _
                        "(codigoConsultorio,CodigoUsuarioEnlazado,CodigoEmpresa,status,fechaActualizacion,codigoUsuario) " & _
                        "values(" & HdIdConsultorio.Value & "," & li.Value & "," & Session("codigoEmpresa") & ",1,getdate()," & Session("codigoUsuario") & ") "
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() <> 0 Then
                    LblMensajeCritico.Text = "Error 102 : " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
            End If
        Next
    End Sub

    Sub insertListaPermisosAlta()      '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        For Each li As ListItem In LstAltaPermisos.Items
            If li.Selected = True Then
                strSql = "  insert into [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] " & _
                        "(codigoConsultorio,CodigoUsuarioEnlazado,CodigoEmpresa,status,fechaActualizacion,codigoUsuario) " & _
                        "values(" & HdIdConsultorio.Value & "," & li.Value & "," & Session("codigoEmpresa") & ",1,getdate()," & Session("codigoUsuario") & ") "
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() <> 0 Then
                    LblMensajeCritico.Text = "Error 102 : " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
            End If
        Next
    End Sub

    Sub leeListaPermisos()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "  SELECT codigoUsuarioEnlazado,(nombre+' '+primerApellido+' '+segundoApellido) as descripcion  " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] as CC " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U " & _
                "on U.codigoEmpresa=CC.codigoEmpresa and U.codigoUsuario=CC.codigoUsuarioEnlazado and U.status=1  " & _
                " where CC.codigoConsultorio = " & HdIdConsultorio.Value & " And CC.CodigoEmpresa = " & Session("codigoEmpresa") & " And CC.status = 1"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For x = 0 To dt.Rows.Count - 1
                    For Each li As ListItem In LstEditPermisos.Items
                        If li.Value = dt.Rows(x).Item("codigoUsuarioEnlazado") Then
                            li.Selected = True
                        End If
                    Next
                Next
            End If
        End If

    End Sub
    Protected Sub CargaDropDoctorCabecera()
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoUsuario,(nombre+' '+primerApellido +' '+segundoApellido) as nombre  " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 "where codigoEmpresa=" & Session("codigoEmpresa") & " and rol='DR' and status=1"
        If funcion.llenadropdown(strSQL, DdAltaDrCabecera) <> 0 Then
            LblMensajeCritico.Text = "Error: " + funcion.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Protected Sub CargaDropEditDoctorCabecera()
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoUsuario,(nombre+' '+primerApellido +' '+segundoApellido) as nombre  " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] " & _
                 "where codigoEmpresa=" & Session("codigoEmpresa") & " and rol='DR' and status=1"
        If funcion.llenadropdown(strSQL, DdEditarDrCabecera) <> 0 Then
            LblMensajeCritico.Text = "Error: " + funcion.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub cargaGridConsultorios()
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoConsultorio,idDoctorCabecera,descripcionConsultorio,(U.nombre+' '+U.primerApellido+' '+U.segundoApellido) as nombre " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios]  as U on C.IdDoctorCabecera=U.codigoUsuario " & _
                "where C.codigoEmpresa =" & Session("codigoEmpresa") & " And C.status = 1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            GdConsultorios.DataSource = dt
            GdConsultorios.DataBind()
        Else
            LblMensajeCritico.Text = "Error: " + funcion.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnSi_Click()
        If HdPregunta.Value = 0 Then
            Dim clsDatos As New ClaseDatos
            Dim funcion As New FuncionesGenerales
            Dim dt As New DataTable
            Dim strSQL As String
            ocultarpaneles()
            strSQL = "  update [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] set status=2 " & _
                      "where codigoEmpresa=" & Session("codigoEmpresa") & " and codigoConsultorio=" & HdIdConsultorio.Value & " and status=1 " & _
                    "update [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] set status=2 " & _
                    "where codigoEmpresa=" & Session("codigoEmpresa") & " and codigoConsultorio=" & HdIdConsultorio.Value & " and status=1 "

            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                LblMensajeAviso.Text = " Se ha borrado el consultorio"
                PanelAvisos.Visible = True
                cargaGridConsultorios()
            Else
                LblMensajeCritico.Text = "Error: " + clsDatos.MensajeError
                PanelCritico.Visible = True
            End If
        End If

    End Sub
    Protected Sub BtnNo_Click()
        ocultarpaneles()
        Exit Sub
    End Sub

    Private Sub GdConsultorios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdConsultorios.RowCommand
        ocultarpaneles()
        limpiarpanel()
        If e.CommandName = "Editar" Then
            HdIdConsultorio.Value = GdConsultorios.Rows(e.CommandArgument).Cells(0).Text
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#Editar').modal('show');</script>", False)
            HdIdDrCabecera.Value = GdConsultorios.Rows(e.CommandArgument).Cells(3).Text
            buscarDatos(GdConsultorios.Rows(e.CommandArgument).Cells(0).Text)
        End If
        If e.CommandName = "Borrar" Then
            HdIdConsultorio.Value = GdConsultorios.Rows(e.CommandArgument).Cells(0).Text
            LblMostarDecision.Text = " ¿Esta seguro(a) que desea borrar el consultorio " + GdConsultorios.Rows(e.CommandArgument).Cells(1).Text
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            HdPregunta.Value = "0"
        End If

    End Sub

    Protected Sub buscarDatos(ByRef CodigoConsultorio As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT descripcionConsultorio,idDoctorCabecera,inicioMat,finalMat,inicioVesp,finalVesp,tiempoTurno " & _
                  "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
                   "where C.codigoEmpresa=" & Session("codigoEmpresa") & " and C.codigoConsultorio=" & HdIdConsultorio.Value & " and C.status=1"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            leeListaDiagnosticos()
            leeListaPermisos()
            Try
                DdEditarDrCabecera.SelectedValue = dt.Rows(0).Item("idDoctorCabecera")
            Catch ex As Exception
                DdEditarDrCabecera.ClearSelection()
            End Try

            txtEditarDescripcion.Text = dt.Rows(0).Item("descripcionConsultorio")
            TxtEditInicioHorarioM.Text = dt.Rows(0).Item("inicioMat").ToString
            TxtEditalrFinalHorarioM.Text = dt.Rows(0).Item("finalMat").ToString
            TxtEditarInicioHorarioV.Text = dt.Rows(0).Item("inicioVesp").ToString
            TxtEditFinHorarioV.Text = dt.Rows(0).Item("finalVesp").ToString
            TxtEditarTurno.Text = dt.Rows(0).Item("tiempoTurno")
        Else
            LblMensajeCritico.Text = "Error: " + clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub btnEditarGuardar_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        insertListaDiagnosticosEditar()
        insertListaPermisosEditar()
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] " & _
                 "set descripcionConsultorio='" & txtEditarDescripcion.Text.ToUpper.Trim & "',idDoctorCabecera='" & DdEditarDrCabecera.SelectedValue & "',inicioMat='" & TxtEditInicioHorarioM.Text & "',finalMat='" & TxtEditalrFinalHorarioM.Text & "' " & _
                 ",inicioVesp='" & TxtEditarInicioHorarioV.Text & "',finalVesp='" & TxtEditFinHorarioV.Text & "',tiempoTurno=" & TxtEditarTurno.Text & ",tipoConsultorio='OR' " & _
                 "where codigoEmpresa=" & Session("codigoEmpresa") & " and codigoConsultorio=" & HdIdConsultorio.Value & " and status=1"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAviso.Text = " Se ha guardado los cambios"
            PanelAvisos.Visible = True
            cargaGridConsultorios()
        Else
            LblMensajeCritico.Text = "Error: " + clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub


    Protected Sub btnAltaGuardar_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] " & _
                 "(CodigoEmpresa,descripcionConsultorio,idDoctorCabecera,inicioMat,finalMat,inicioVesp,finalVesp, " & _
                 "tiempoTurno,numeroTurnos,tipoConsultorio,status,fechaActualizacion) " & _
                 "VALUES(" & Session("codigoEmpresa") & ",'" & TxtAltaDescripcion.Text.ToUpper.Trim & "','" & DdAltaDrCabecera.SelectedValue & "', " & _
                        "'" & TxtAltaInicioHorarioM.Text & "','" & TxtAltaFinHorarioM.Text & "','" & TxtAltaInicioHorarioV.Text & "' , " & _
                        " '" & TxtAltaFinaHorarioV.Text & "'," & TxtAltaTiempoTurno.Text & ",0,'OR',1, convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20) ) "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            strSQL = "SELECT codigoConsultorio  FROM [AgemedDEMO].[dbo].[CatConsultorios] " & _
                        "where codigoEmpresa=" & Session("codigoEmpresa") & " and descripcionConsultorio='" & TxtAltaDescripcion.Text.ToUpper.Trim & "' and status=1"
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    HdIdConsultorio.Value = dt.Rows(0).Item("codigoConsultorio")
                    insertListaDiagnosticosAlta()
                    insertListaPermisosAlta()
                End If
            End If
            limpiarpanel()
            LblMensajeAviso.Text = " Se ha guardado los cambios"
            PanelAvisos.Visible = True
            cargaGridConsultorios()
        Else
            LblMensajeCritico.Text = "Error: " + clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Private Sub ocultarpaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Private Sub limpiarpanel()
        LstEditDiagnosticos.ClearSelection()
        LstEditPermisos.ClearSelection()
        DdEditarDrCabecera.ClearSelection()
        DdAltaDrCabecera.ClearSelection()
        DdEditarDrCabecera.ClearSelection()
        TxtAltaDescripcion.Text = ""
    End Sub


End Class