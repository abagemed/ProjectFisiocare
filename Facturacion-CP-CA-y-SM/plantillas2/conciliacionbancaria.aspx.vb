Public Class conciliacionbancaria
    Inherits System.Web.UI.Page
    Private banderaImporte As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

       
        If Not IsPostBack Then
            cargasucursal()
            saldo.Value = 0.0
            banderaImporte = 0
            cargar_dropdownlist()
            cargaFacturas()
            ocultarPaneles()
            HdReferencia.Value = ""
            'AB01092020
            'Dim selsucursal As String = ""

        End If
    End Sub
    Sub cargasucursal()
        Select Case sucursal.SelectedIndex
            Case "1"
                dbsucursal.Value = "fisiocareSM"
            Case "2"
                dbsucursal.Value = "fisiocareCP"
            Case "3"
                dbsucursal.Value = "fisiocareHO"
            Case "4"
                dbsucursal.Value = "fisiocareCA"
            Case "5"
                dbsucursal.Value = "Gym"
            Case "6"
                dbsucursal.Value = "fisiocareAM"
        End Select
    End Sub
    'AB02092020*
    Sub cargaFacturas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        'strSQL = "SELECT TOP 200 F.IDFactura,F.Serie,F.FolioFactura,(F.Serie+FolioFactura) as Folio,F.rfc,F.Total,Saldo,DF.RazonSocial,(P.papellido+' '+P.sapellido+' '+P.nombres) as Paciente,F.fechaFactura,F.estado,(segundoApellido+' '+primerApellido+' '+nombre) as Facturo " & _
        '             "FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F " & _
        '             " inner join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] as DF  " & _
        '             "On DF.idRazonSocial=F.idRazonSocial " & _
        '             "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as P " & _
        '             "on F.codigoPaciente=P.codigoPaciente and P.codigoEmpresa=1  " & _
        '             "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U  " & _
        '             "on U.codigoUsuario=F.IdEmpleado " & _
        '             "where F.Estado='Vigente' and F.conciliado=0 " & _
        '             "ORDER BY F.fechaFactura desc"


        '        strSQL = "SELECT TOP 500 F.IDFactura,F.Serie,F.FolioFactura,(F.Serie+FolioFactura) as Folio,F.rfc,F.Total,Saldo,(CASE WHEN F.codigoPaciente=0 THEN CMC.RazonSocial ELSE DF.RazonSocial END) as RazonSocial , " & _
        '"(CASE WHEN F.codigoPaciente=0  THEN CMC.Alias ELSE (P.papellido+' '+P.sapellido+' '+P.nombres) END) as Paciente, " & _
        '"F.fechaFactura,F.estado,(segundoApellido+' '+primerApellido+' '+nombre) as Facturo " & _
        '"FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F  " & _
        '"left join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] as DF  On DF.idRazonSocial=F.idRazonSocial " & _
        '"left join [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] as CMC  On CMC.CodigoCliente=F.idRazonSocial " & _
        '"left join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as P on F.codigoPaciente=P.codigoPaciente and P.codigoEmpresa=1  " & _
        '"inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U  on U.codigoUsuario=F.IdEmpleado " & _
        '"where F.Estado='Vigente' and F.conciliado=0 " & _
        '"ORDER BY F.fechaFactura desc"


        strSQL = "SELECT TOP 500 F.IDFactura,F.Serie,F.num_factura,(F.Serie+num_factura) as Folio,F.rfc,F.importe,F.saldo, " & _
"F.nombre as RazonSocial , F.paciente as Paciente, F.fecha,F.estado,FP.descripcion as tipopago " & _
"FROM [" & dbsucursal.Value & "].[dbo].[factura] as F  " & _
"inner join [" & dbsucursal.Value & "].[dbo].[CatFormasDePago] as FP  on FP.codigoFormaPago=F.tipoDePago " & _
"where F.Estado='Vigente' and F.conciliado=0 " & _
"ORDER BY F.fecha desc "

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            GdFacturas.DataSource = dt
            GdFacturas.DataBind()
            'GdFacturas.Columns(0).Visible = False
            'GdFacturas.Columns(1).Visible = False
            'GdFacturas.Columns(2).Visible = False
        End If
    End Sub
    'AB01092020 *
    Sub cargar_dropdownlist()
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        

        'Dim selsucursal As String = ""
        Select Case sucursal.SelectedValue
            Case "1"
                dbsucursal.Value = "fisiocareSM"
            Case "2"
                dbsucursal.Value = "fisiocareCP"
            Case "3"
                dbsucursal.Value = "fisiocareHO"
            Case "4"
                dbsucursal.Value = "fisiocareCA"
            Case "5"
                dbsucursal.Value = "Gym"
            Case "6"
                dbsucursal.Value = "fisiocareAM"
        End Select

        strSQL = "SELECT codigoCuentaBancaria,descripcion  " & _
                "FROM [" & dbsucursal.Value & "].[dbo].[CatCuentasBancarias] " & _
                "where status=1"
        funciones.llenadropdown(strSQL, DdCuenta)

        strSQL = "SELECT codigoFormaPago, descripcion " & _
                "FROM [" & dbsucursal.Value & "].[dbo].[CatFormasDePago] " & _
                "where status='A'"
        funciones.llenadropdown(strSQL, DdTipoPago)

      


    End Sub
    'AB01092020*
    Private Sub sucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles sucursal.SelectedIndexChanged
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        'Dim selsucursal As String = ""
        Select Case sucursal.SelectedValue
            Case "1"
                dbsucursal.value = "fisiocareSM"
            Case "2"
                dbsucursal.value = "fisiocareCP"
            Case "3"
                dbsucursal.value = "fisiocareHO"
            Case "4"
                dbsucursal.value = "fisiocareCA"
            Case "5"
                dbsucursal.Value = "Gym"
            Case "6"
                dbsucursal.Value = "fisiocareAM"
        End Select

        strSQL = "SELECT codigoCuentaBancaria,descripcion  " & _
                "FROM [" & dbsucursal.Value & "].[dbo].[CatCuentasBancarias] " & _
                "where status=1"
        funciones.llenadropdown(strSQL, DdCuenta)

        cargaFacturas()
    End Sub
    'AB01092020*
    Protected Sub guardarDatosBancarios(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT  folioIngreso,codigoCuentaBancaria,importe,saldo,codigoFormaPago,observaciones,fechaOperacion  " & _
                "FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                " where referencia='" & TxtReferencia.Text & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAdvertencia.Text = "Ya existe la referencia " + TxtReferencia.Text.Trim
                PanelAdvertencia.Visible = True
                Exit Sub
            End If
        End If
        strSQL = "insert into [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                 "(referencia,status, codigoCuentaBancaria,importe,saldo,codigoFormaPago,Observaciones,FechaOperacion,fechaActualizacion,codigoUsuario,codigoEmpresa) " & _
                  "values('" & TxtReferencia.Text & "',1," & DdCuenta.SelectedValue & "," & TxtImporte.Text & "," & TxtImporte.Text & "," & DdTipoPago.SelectedValue & ",'" & observaciones.Value & "',convert(varchar(10),'" & Fecha.Text & "',103),getdate(),1," & sucursal.SelectedValue & ") "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAviso.Text = " Se ha guardado la referencia bancaria"
            PanelAvisos.Visible = True
            verReferencias()
            HdReferencia.Value = TxtReferencia.Text.Trim
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String = ""
        Select Case HdPregunta.Value
            Case "0"
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco]  set referencia='" & TxtReferencia.Text.Trim.ToUpper & "',fechaOperacion=convert(varchar(10),'" & Fecha.Text & "',103),observaciones='" & observaciones.Value.ToUpper & "',codigoFormaPago=" & DdTipoPago.SelectedValue & ",codigoUsuario=" & Session("codigoUsuario") & ",fechaActualizacion=getdate(),codigoCuentaBancaria=" & DdCuenta.SelectedValue & " " & _
                          "where folioIngreso='" & HdfolioIngreso.Value & "' and codigoEmpresa=" & Session("codigoEmpresa") & ""
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    HdReferencia.Value = TxtReferencia.Text.Trim
                    LblMensajeAviso.Text = " Se ha guardado los cambios en la referencia bancaria"
                    PanelAvisos.Visible = True
                    verReferencias()
                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If
            Case "1"
                strSQL = "delete FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco] " & _
                        "where  folioIngreso =" & HdfolioIngreso.Value & "  And codigoEmpresa =" & Session("codigoEmpresa") & " and referencia='" & TxtReferencia.Text.Trim & "' "
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar() = 0 Then
                    limpiarCampos()
                    cargaFacturas()
                    LblMensajeAviso.Text = "La referencia  " + TxtReferencia.Text + " se ha borrado"
                    PanelAvisos.Visible = True
                Else
                    LblMensajeCritico.Text = "Error al borrar referencia:  " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If
        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        TxtReferencia.Text = HdReferencia.Value
        Exit Sub
    End Sub
    Protected Sub editarReferencia(sender As Object, e As EventArgs)
        ocultarPaneles()
        If HdReferencia.Value = TxtReferencia.Text.Trim Then
            LblMostarDecision.Text = "Deseas realizar cambio(s) en información de la referencia " + HdReferencia.Value
            PanelDesicion.Visible = True
            HdPregunta.Value = "0"
        Else

            LblMostarDecision.Text = "Deseas cambiar la referencia " + HdReferencia.Value + "  por " + TxtReferencia.Text.Trim
            PanelDesicion.Visible = True
            HdPregunta.Value = "0"

        End If
    End Sub
    'AB21092020*
    Protected Sub verReferenciaBancaria(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        If HdfolioIngreso.Value = "" Then
            strSQL = "SELECT  folioIngreso,codigoCuentaBancaria,importe,saldo,codigoFormaPago,observaciones,fechaOperacion  " & _
               "FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
               " where referencia='" & TxtReferencia.Text.Trim & "'"
        Else
            strSQL = "SELECT  folioIngreso,codigoCuentaBancaria,importe,saldo,codigoFormaPago,observaciones,fechaOperacion  " & _
                   "FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                   " where referencia='" & TxtReferencia.Text.Trim & "' and folioIngreso='" & HdfolioIngreso.Value & "'"

        End If
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                HdReferencia.Value = TxtReferencia.Text
                HdfolioIngreso.Value = dt.Rows(0).Item("folioIngreso")
                'sucursal.SelectedValue = dt.Rows(0).Item("CodigoCuentaBancaria")
                DdCuenta.SelectedValue = dt.Rows(0).Item("CodigoCuentaBancaria")
                TxtImporte.Text = dt.Rows(0).Item("importe")
                saldo.Value = dt.Rows(0).Item("saldo")
                If TxtImporte.Text = saldo.Value Then
                    saldo.Attributes.CssStyle.Value = "danger"
                Else
                    saldo.Attributes.CssStyle.Value = "succes"
                End If
                DdTipoPago.SelectedValue = dt.Rows(0).Item("codigoFormaPago")
                observaciones.Value = dt.Rows(0).Item("observaciones")
                Fecha.Text = dt.Rows(0).Item("fechaOperacion")
                banderaImporte = 1
            Else
                LblMensajeAdvertencia.Text = "No se encontro referencia"
                PanelAdvertencia.Visible = True
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        strSQL = "SELECT C.idFactura,C.Foliofactura,C.importe,C.fechaConciliacion,F.estado " & _
                 " FROM [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria] as C " & _
                 "inner join [" & dbsucursal.Value & "].[dbo].[factura] as F on C.idFactura=F.idFactura " & _
                 " where C.folioIngreso='" & HdfolioIngreso.Value & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdFacConciliadas.DataSource = dt
                GdFacConciliadas.DataBind()
                BtnBorrarReferencia.Visible = False
            Else
                LblMensajeAviso.Text = " La referencia no tiene facturas conciliadas"
                PanelAvisos.Visible = True
                BtnBorrarReferencia.Visible = True
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_referencia').modal('show');</script>", False)
    End Sub
    
    'Ab02092020*
    Protected Sub verfacturasconciliadasBtnBorrar()
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT C.idFactura,C.Foliofactura,C.importe,C.fechaConciliacion,F.estado " & _
                 " FROM [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria] as C " & _
                 "inner join [" & dbsucursal.Value & "].[dbo].[factura] as F on C.idFactura=F.idFactura " & _
                 " where C.folioIngreso='" & HdfolioIngreso.Value & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                BtnBorrarReferencia.Visible = False
            Else
                BtnBorrarReferencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    'AB02092020*
    Protected Sub buscarReferenciaBancaria(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT top 500 folioIngreso ,referencia,importe,saldo,observaciones ,fechaOperacion " & _
                    "FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] where saldo<>0 order by fechaOperacion desc"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdBuscarReferencias.DataSource = dt
                GdBuscarReferencias.DataBind()
            Else
                LblMensajeAdvertencia.Text = "No se encontro referencia"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('show');</script>", False)
    End Sub
    

    Sub Conciliar(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim sumaTotal As Double = 0.0
        Dim strSql As String
        If saldo.Value < 0 Then
            Exit Sub
        End If
        'GdFacturas.Columns(0).Visible = True
        'GdFacturas.Columns(1).Visible = True
        'GdFacturas.Columns(2).Visible = True
        'verifico si ya esta en la base de datos la referencia bancaria
        strSql = "SELECT referencia FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                "where referencia='" & TxtReferencia.Text.Trim & "' and status=1  "
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                ' existe la referencia bancaria, le conciliamos las facturas
                strSql = ""
                For Each fila As GridViewRow In GdFacturas.Rows
                    If fila.BackColor = Drawing.Color.DarkTurquoise Then
                        strSql += "insert into [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria]  " & _
                                       "(folioIngreso,folioFactura,Importe,FechaConciliacion,codigoUsuario,status,codigoEmpresa,idFactura) " & _
                                      " values('" & HdfolioIngreso.Value & "','" & fila.Cells(3).Text & "'," & fila.Cells(7).Text & ", " & _
                                      "getdate(),1,1,1," & fila.Cells(0).Text & ") "
                        strSql += "update [" & dbsucursal.Value & "].[dbo].[factura] set conciliado=1,saldo=0.0 " & _
                                  "where idfactura=" & fila.Cells(0).Text & " and status=1 "
                        'actualizarIngresosCajas(fila.Cells(0).Text)
                    End If
                    If fila.BackColor = Drawing.Color.Salmon Then
                        Dim saldoFac As Double = 0.0
                        strSql += "insert into [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria]  " & _
                                       "(folioIngreso,folioFactura,Importe,FechaConciliacion,codigoUsuario,status,codigoEmpresa,idFactura) " & _
                                      " values('" & HdfolioIngreso.Value & "','" & fila.Cells(3).Text & "'," & HdAporteParcialFac.Value & ", " & _
                                      "getdate(),1,1,1," & fila.Cells(0).Text & ") "
                        saldoFac = CDbl(fila.Cells(7).Text) - CDbl(HdAporteParcialFac.Value)
                        strSql += "update [" & dbsucursal.Value & "].[dbo].[factura] set saldo=" & saldoFac & " " & _
                                 "where idfactura=" & fila.Cells(0).Text & " and status=1 "
                    End If
                Next
                'actualizamos el saldo  de ingresos Bancos
                strSql += "update  [" & dbsucursal.Value & "].[dbo].[IngresosBanco] set saldo='" & saldo.Value & "'   " & _
                    "where referencia='" & TxtReferencia.Text.Trim & "' and folioIngreso='" & HdfolioIngreso.Value & "' "
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = 0 Then
                Else
                    LblMensajeAdvertencia.Text = "Error al insertar dato"
                    PanelAdvertencia.Visible = True
                End If
            Else
                ' se guarda la referencia bancaria
                strSql = "insert into [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                        "(referencia,status, codigoCuentaBancaria,importe,saldo,codigoFormaPago,Observaciones,FechaOperacion,fechaActualizacion,codigoUsuario,codigoEmpresa) " & _
                         "values('" & TxtReferencia.Text.Trim & "',1," & sucursal.SelectedValue & "," & TxtImporte.Text & "," & saldo.Value & "," & DdTipoPago.SelectedValue & ",'" & observaciones.Value & "','" & Fecha.Text & "',getdate(),1,1) "
                clsDatos.cargaComando(strSql)
                clsDatos.ejecutar()
                CapturoFolioIngreso(TxtReferencia.Text.Trim, TxtImporte.Text, 1, Fecha.Text)
                ' se agregan las facturas conciliadas
                strSql = ""
                For Each fila As GridViewRow In GdFacturas.Rows
                    If fila.BackColor = Drawing.Color.DarkTurquoise Then
                        strSql += "insert into [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria]  " & _
                                       "(folioIngreso,folioFactura,Importe,FechaConciliacion,codigoUsuario,status,codigoEmpresa,idFactura) " & _
                                      " values('" & HdfolioIngreso.Value & "','" & fila.Cells(3).Text & "'," & fila.Cells(7).Text & ", " & _
                                      "getdate(),1,1,1," & fila.Cells(0).Text & ") "
                        strSql += "update [" & dbsucursal.Value & "].[dbo].[factura] set conciliado=1 " & _
                                  "where idfactura=" & fila.Cells(0).Text & " and status=1 "
                        'actualizarIngresosCajas(fila.Cells(0).Text)
                    End If
                    If fila.BackColor = Drawing.Color.Salmon Then
                        Dim saldoFac As Double = 0.0
                        strSql += "insert into [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria]  " & _
                                       "(folioIngreso,folioFactura,Importe,FechaConciliacion,codigoUsuario,status,codigoEmpresa,idFactura) " & _
                                      " values('" & HdfolioIngreso.Value & "','" & fila.Cells(3).Text & "'," & HdAporteParcialFac.Value & ", " & _
                                      "getdate(),1,1,1," & fila.Cells(0).Text & ") "
                        saldoFac = CDbl(fila.Cells(7).Text) - CDbl(HdAporteParcialFac.Value)
                        strSql += "update [" & dbsucursal.Value & "].[dbo].[factura] set saldo=" & saldoFac & " " & _
                                 "where idfactura=" & fila.Cells(0).Text & " and status=1 "
                        'actualizarIngresosCajas(fila.Cells(0).Text)
                    End If
                Next
                ' se actualiza el saldo de ingresosbancos
                strSql += "update  [" & dbsucursal.Value & "].[dbo].[IngresosBanco] set saldo='" & saldo.Value & "'   " & _
                   "where referencia='" & TxtReferencia.Text.Trim & "' "
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = 0 Then

                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    Exit Sub
                End If
            End If
        End If
       
        LblMensajeAviso.Text = " Se realizó conciliación"
        BtnBorrarReferencia.Visible = False
        PanelAvisos.Visible = True
        PanelAvisos.Focus()
        cargaFacturas()
    End Sub

  

    'Sub actualizarIngresosCajas(ByRef idFactura As String)
    '    Dim clsDatos As New ClaseDatos
    '    Dim dt As New DataTable
    '    Dim strSql As String
    '    strSql = "SELECT folioConsulta  FROM [" & dbsucursal.Value & "].[dbo].[facturas] as F " & _
    '            "where idFactura='" & idFactura & "'  "
    '    If clsDatos.cargatabla(strSql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            strSql = ""
    '            For Each fila As DataRow In dt.Rows
    '                strSql += " update [" & clsDatos.BaseDatos & "].[dbo].[Ingresoscaja] set conciliado=1,Pagorealizado=1" & _
    '                            "where folioConsulta='" & dt.Rows(0).Item("folioConsulta") & "'"
    '            Next
    '            clsDatos.cargaComando(strSql)
    '            If clsDatos.ejecutar() <> 0 Then
    '                LblMensajeAdvertencia.Text = "Error al actualizar ingresoCaja"
    '                PanelAvisos.Visible = True
    '            End If
    '        End If
    '    End If
    'End Sub

    Sub desConciliar_IngresosCajas(ByRef idFactura As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT folioConsulta  FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F " & _
                "where idFactura='" & idFactura & "'  "
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                strSql = ""
                For Each fila As DataRow In dt.Rows
                    strSql += " update [" & clsDatos.BaseDatos & "].[dbo].[Ingresoscaja] set conciliado=0,Pagorealizado=0" & _
                                "where folioConsulta='" & dt.Rows(0).Item("folioConsulta") & "'"
                Next
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() <> 0 Then
                    LblMensajeAdvertencia.Text = "Error al cambiar el estado del campo conciliacion a cero en la tabla ingresoCaja"
                    PanelAvisos.Visible = True
                End If
            End If
        End If
    End Sub

    Sub CapturoFolioIngreso(ByRef referencia As String, ByRef importe As String, ByRef codigoEmpresa As String, ByRef fechaOperacion As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "select folioIngreso from [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                "where referencia='" & referencia & "' and importe=" & importe & " and codigoEmpresa=1 and fechaOperacion='" & fechaOperacion & "'"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                HdfolioIngreso.Value = dt.Rows(0).Item("folioIngreso")
            End If

        End If

    End Sub
    'AB02092020 *

    Private Sub GdBuscarReferencias_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdBuscarReferencias.RowCommand
        If e.CommandName = "Abrir" Then
            Dim index As Integer
            index = CInt(e.CommandArgument)
            TxtReferencia.Text = GdBuscarReferencias.Rows(index).Cells(1).Text
            HdReferencia.Value = GdBuscarReferencias.Rows(index).Cells(1).Text

            'AGREGUE EL HDFOLIOREFERENCIA PARA CONCILIAR VARIAS REFERENCIAS IGUALES AGREGADA EN CADA CONSULTA COMO METODO DE PAGO.
            HdfolioIngreso.Value = GdBuscarReferencias.Rows(index).Cells(0).Text

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('hide');</script>", False)
            verReferencias()
            verfacturasconciliadasBtnBorrar()
            editar_Referencia.Visible = True
        End If
    End Sub
    'Ab02092020 *
    Private Sub verReferencias() ' se agrego para simular el click del boton ver referencias.  
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        'GdFacturas.Columns(0).Visible = True
        'GdFacturas.Columns(1).Visible = True
        'GdFacturas.Columns(2).Visible = True
        cargaFacturas()
        strSQL = "SELECT  folioIngreso,codigoCuentaBancaria,importe,saldo,codigoFormaPago,observaciones,fechaOperacion  " & _
                "FROM [" & dbsucursal.Value & "].[dbo].[IngresosBanco] " & _
                " where referencia='" & TxtReferencia.Text.Trim & "' and folioIngreso='" & HdfolioIngreso.Value & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Try
                    HdfolioIngreso.Value = dt.Rows(0).Item("folioIngreso")
                    sucursal.SelectedValue = dt.Rows(0).Item("CodigoCuentaBancaria")
                    TxtImporte.Text = dt.Rows(0).Item("importe")
                    saldo.Value = dt.Rows(0).Item("saldo")
                    validarSaldocero()
                    If TxtImporte.Text = saldo.Value Then
                        saldo.Attributes.CssStyle.Value = "danger"
                    Else
                        saldo.Attributes.CssStyle.Value = "succes"
                    End If
                    DdTipoPago.SelectedValue = dt.Rows(0).Item("codigoFormaPago")
                    observaciones.Value = dt.Rows(0).Item("observaciones")
                    Fecha.Text = dt.Rows(0).Item("fechaOperacion")
                    banderaImporte = 1
                Catch ex As Exception
                    LblMensajeCritico.Text = "Error 123:" + ex.Message
                    PanelCritico.Visible = True
                End Try
               
            Else
                LblMensajeAdvertencia.Text = "No se encontro referencia"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        strSQL = "SELECT C.idFactura,C.Foliofactura,C.importe,C.fechaConciliacion,F.estado " & _
                 " FROM [" & dbsucursal.Value & "].[dbo].[ConciliacionBancaria] as C " & _
                 "inner join [" & dbsucursal.Value & "].[dbo].[factura] as F on C.idFactura=F.idFactura " & _
                 " where C.folioIngreso='" & HdfolioIngreso.Value & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdFacConciliadas.DataSource = dt
                GdFacConciliadas.DataBind()
                LblSinFacturas.Text = ""
            Else
                LblSinFacturas.Text = "Sin facturas conciliadas"
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        'ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_referencia').modal('show');</script>", False)

    End Sub

    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Private Sub validarSaldocero()
        If saldo.Value = 0 Then
            btn_conciliar.Disabled = True
        Else
            btn_conciliar.Disabled = False
        End If
    End Sub
  
    'AB21092020*
    Private Sub GdFacturas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdFacturas.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Des-Seleccionar" Then
            Dim suma As Double = 0.0
            If GdFacturas.Rows(index).BackColor = Drawing.Color.DarkTurquoise Then
                GdFacturas.Rows(index).BackColor = Nothing
                suma = CDbl(saldo.Value) + CDbl(GdFacturas.Rows(index).Cells(7).Text)
                saldo.Value = suma
                CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = True
                CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = False
            End If
            If GdFacturas.Rows(index).BackColor = Drawing.Color.Salmon Then
                GdFacturas.Rows(index).BackColor = Nothing
                CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = True
                CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = False
                suma = CDbl(saldo.Value) + CDbl(HdAporteParcialFac.Value)
                saldo.Value = suma
                'agregar la parte que se le agrego a la factura
            End If
        End If
        If saldo.Value = 0 Then
            Exit Sub
        End If
        If e.CommandName = "Seleccionar" Then
            If GdFacturas.Rows(index).BackColor = Nothing Then
                Dim resta As Double
                resta = CDbl(saldo.Value) - CDbl(GdFacturas.Rows(index).Cells(7).Text)
                If resta >= 0 Then
                    GdFacturas.Rows(index).BackColor = Drawing.Color.DarkTurquoise
                    CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = False
                    CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = True
                    saldo.Value = resta
                Else
                    Dim aporteFac As Double = 0.0
                    GdFacturas.Rows(index).BackColor = Drawing.Color.Salmon
                    CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = False
                    CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = True
                    HdAporteParcialFac.Value = CDbl(saldo.Value)
                    saldo.Value = 0
                    'restar a factura
                End If
            End If

        End If
    End Sub

 

    Private Sub TxtReferencia_TextChanged(sender As Object, e As EventArgs) Handles TxtReferencia.TextChanged
        HdfolioIngreso.Value = ""
        saldo.Value = 0.0
    End Sub

    Private Sub GdFacConciliadas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdFacConciliadas.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Des-SeleccionarFAC" Then
            If GdFacConciliadas.Rows(index).BackColor = Drawing.Color.DarkTurquoise Then
                GdFacConciliadas.Rows(index).BackColor = Nothing
                CType(GdFacConciliadas.Rows(index).FindControl("BtnSelecFac"), Button).Visible = True
                CType(GdFacConciliadas.Rows(index).FindControl("BtnDesSelecFac"), Button).Visible = False
            End If
        End If
        If e.CommandName = "SeleccionarFAC" Then
            If GdFacConciliadas.Rows(index).BackColor = Nothing Then
                GdFacConciliadas.Rows(index).BackColor = Drawing.Color.DarkTurquoise
                CType(GdFacConciliadas.Rows(index).FindControl("BtnSelecFac"), Button).Visible = False
                CType(GdFacConciliadas.Rows(index).FindControl("BtnDesSelecFac"), Button).Visible = True
            End If
        End If
    End Sub

    Protected Sub Desasignar()
        Dim strSql As String
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim saldoDetFacturas As Double
        Dim saldoIngresoBancos As Double
        Dim facturasDesasignadas As String = ""
        Dim errorDesasiganFac As String = ""
        Dim contFacturasAsignadas As Integer = 0
        Dim contFacturasDesasignada As Integer = 0
        For Each fila As GridViewRow In GdFacConciliadas.Rows
            contFacturasAsignadas = contFacturasAsignadas + 1
            If fila.BackColor = Drawing.Color.DarkTurquoise Then
                strSql = "select saldo from [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] where idFactura=" & fila.Cells(0).Text & " "
                If clsDatos.cargatabla(strSql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        saldoDetFacturas = dt.Rows(0).Item("saldo") + CDbl(fila.Cells(2).Text)
                    Else
                        Exit Sub
                    End If
                Else
                    LblMensajeCritico.Text = "Error 1278: " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If

                strSql = "select saldo from [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco] where folioIngreso=" & HdfolioIngreso.Value & " and referencia='" & TxtReferencia.Text.Trim & "'"
                If clsDatos.cargatabla(strSql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        saldoIngresoBancos = dt.Rows(0).Item("saldo") + CDbl(fila.Cells(2).Text)
                    Else
                        Exit Sub
                    End If
                Else
                    LblMensajeCritico.Text = "Error 1279: " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If

                strSql = "delete FROM [" & clsDatos.BaseDatos & "].[dbo].[ConciliacionBancaria] " & _
                "where idFactura = " & fila.Cells(0).Text & " And folioIngreso =" & HdfolioIngreso.Value & "  And codigoEmpresa =" & Session("codigoEmpresa") & " and folioFactura='" & fila.Cells(1).Text & "' " & _
                "update [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas]  set conciliado=0, saldo=" & saldoDetFacturas & " " & _
                "where idFactura = " & fila.Cells(0).Text & " And codigoEmpresa = " & Session("codigoEmpresa") & " " & _
                "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco]  set saldo=" & saldoIngresoBancos & " " & _
                "where folioIngreso=" & HdfolioIngreso.Value & "  and codigoEmpresa=" & Session("codigoEmpresa") & ""
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = 0 Then
                    facturasDesasignadas += fila.Cells(1).Text + " "
                    contFacturasDesasignada = contFacturasDesasignada + 1
                    desConciliar_IngresosCajas(fila.Cells(0).Text)
                Else
                    errorDesasiganFac += fila.Cells(1).Text + " "
                End If
            End If
        Next
        If contFacturasAsignadas = contFacturasDesasignada Then
            BtnBorrarReferencia.Visible = True
        End If
        If errorDesasiganFac = "" Then
            LblMensajeAviso.Text = "La(s) factura(s) " + facturasDesasignadas + " se han desasignado"
            PanelAvisos.Visible = True

        Else
            LblMensajeCritico.Text = "No se logro desasignar la(s) factura(s) " + errorDesasiganFac
            PanelCritico.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('hide');</script>", False)
        verReferencias()
        cargaFacturas()
    End Sub
    Protected Sub borrarReferenciaBancaria()
        ocultarPaneles()
        LblMostarDecision.Text = "¿Deseas borrar la siguiente referencia? " + HdReferencia.Value
        PanelDesicion.Visible = True
        HdPregunta.Value = "1"
    End Sub

    Protected Sub limpiarCampos()
        TxtImporte.Text = 0.0
        TxtReferencia.Text = ""
        HdReferencia.Value = ""
        saldo.Value = 0.0
        observaciones.Value = ""
        Fecha.Text = ""
        BtnBorrarReferencia.Visible = False
        editar_Referencia.Visible = False

    End Sub
End Class
