Public Class conciliacion
    Inherits System.Web.UI.Page
    Private banderaImporte As Integer
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            saldo.Value = 0.0
            banderaImporte = 0
            cargar_dropdownlist()
            'cargaFacturas()
            ocultarPaneles()
            HdReferencia.Value = ""
        End If
    End Sub
    'busqueda de pacientes actrualizado 25/02/2018 Antonio Briceño
    Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim strSQL As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos
        If TxbMaterno.Text = "" And TxbPaterno.Text = "" And TxbNombre.Text = "" Then
            Exit Sub
        End If
        strSQL = "SELECT idcliente,paterno+' '+materno+' '+Nombre FROM [" & claseDatos.BaseDatos & "].[dbo].[clientes] " & _
            "where paterno like '%" & TxbPaterno.Text.Trim & "%' and nombre like '%" & TxbNombre.Text.Trim & "%' and materno like '%" & TxbMaterno.Text & "%'"
        resultadoBusqueda = 0
        resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
        If resultadoBusqueda = -1 Then
            LblMensajeCritico.Text = claseDatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
            Exit Sub
        End If
        If resultadoBusqueda = 1 Then
            LblMensajeAdvertencia.Text = "No existe el paciente!"
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
            Exit Sub
        End If
        LstBoxPacientes.Focus()
        LstBoxPacientes.SelectedIndex = 0
    End Sub
    'busqueda de terapias a asignar, actrualizado 25/02/2018 Antonio Briceño
    Sub cargaFacturas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String


        strSQL = "SELECT A.idabono,A.idCita,(CP.paterno+' '+CP.materno+' '+CP.nombre) as CodigoPaciente, fecha,catServicios.descripcion as servicio,A.abono as abono," & _
 "A.importe-A.monto as importe,(CASE A.coaseguro WHEN 0 THEN 'NO' ELSE 'SI' END) AS coaseguro,A.ligaAbono,A.idcliente,A.idcosto as idcosto  " & _
 "FROM abonos as A " & _
 "inner join clientes as CP on A.idcliente=CP.idcliente " & _
 "left JOIN catCostos ON A.idCosto = catCostos.idCosto " & _
 "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " & _
 "where A.idCliente='" & Hdidcliente.Value & "'  and A.facturado='false' and importe > 0"


        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            GdFacturas.DataSource = dt
            GdFacturas.DataBind()

        End If
    End Sub

    'lista de tipos de pagos, actualizado 25/02/2018 Antonio Briceño
    Sub cargar_dropdownlist()
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoFormaPago, descripcion " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] " & _
                "where status='A'"
        funciones.llenadropdown(strSQL, DdTipoPago)


    End Sub


    'buscar abonos asignados, actualizado 28/02/2018 Antonio Briceño
    Protected Sub verReferenciaBancaria(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        If HdfolioIngreso.Value = "" Then

            strSQL = "select idfactura,F.num_factura as factura,F.serie as serie,F.importe as importe,F.importe-(isnull(sum(A.importe),0) - isnull(sum(A.monto),0)) as saldo,F.tipoDePago as tipoDePago ,F.paciente as paciente, F.fecha as fecha from factura F " &
            "inner join abonos as A on A.num_factura=F.num_factura " &
            "where F.num_factura='" & TxtReferencia.Text.Trim & "' group by F.idFactura,F.num_factura,F.serie,F.importe,F.tipoDePago,F.paciente,F.fecha"

        Else

            strSQL = "select idfactura,F.num_factura as factura,F.serie as serie,F.importe as importe,F.importe-(isnull(sum(A.importe),0) - isnull(sum(A.monto),0)) as saldo,F.tipoDePago as tipoDePago ,F.paciente as paciente, F.fecha as fecha from factura F " &
            "inner join abonos as A on A.num_factura=F.num_factura " &
            "where F.idfactura='" & HdfolioIngreso.Value & "' group by F.idFactura,F.num_factura,F.serie,F.importe,F.tipoDePago,F.paciente,F.fecha"


        End If
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                HdReferencia.Value = TxtReferencia.Text
                HdfolioIngreso.Value = dt.Rows(0).Item("idfactura")
                TxtSerie.Text = dt.Rows(0).Item("serie")
                TxtImporte.Text = dt.Rows(0).Item("importe")
                saldo.Value = dt.Rows(0).Item("saldo")
                If TxtImporte.Text = saldo.Value Then
                    saldo.Attributes.CssStyle.Value = "danger"
                Else
                    saldo.Attributes.CssStyle.Value = "succes"
                End If
                DdTipoPago.SelectedValue = dt.Rows(0).Item("tipoDePago")
                observaciones.Value = dt.Rows(0).Item("paciente")
                Fecha.Text = dt.Rows(0).Item("fecha")
                banderaImporte = 1
            Else
                LblMensajeAdvertencia.Text = "No se encontró referencia"
                PanelAdvertencia.Visible = True
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If

        strSQL = "SELECT A.idAbono as idabono,A.idcita as idcita,A.num_factura as factura,A.serie as serie,A.importe as importe,catservicios.descripcion as servicio,F.idfactura  FROM abonos as A " &
       "inner join factura as F on A.num_factura=F.num_factura " &
       "left JOIN catCostos ON A.idCosto = catCostos.idCosto " &
       "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " &
       "where F.idFactura='" & HdfolioIngreso.Value & "'"

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdFacConciliadas.DataSource = dt
                GdFacConciliadas.DataBind()
            Else
                LblMensajeAviso.Text = " La factura no tiene asignación de abonos"
                PanelAvisos.Visible = True
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_referencia').modal('show');</script>", False)
    End Sub
    
    'Busqueda de la factura, actualizado 27/02/2018 Antonio Briceño
    Protected Sub buscarReferenciaBancaria(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String


        If LstBoxPacientes.SelectedValue = "" Then
            strSQL = " select top 500 idFactura, num_factura, serie, importe, fecha, nombre,paciente,idCliente " &
            "from factura where num_factura = '" & TxtReferencia.Text.Trim & "' and cancelada='False' order by fecha desc"

            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    GdBuscarReferencias.DataSource = dt
                    GdBuscarReferencias.DataBind()
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('show');</script>", False)
                Else
                    LblMensajeAdvertencia.Text = "No se encontró factura para asignar"
                    PanelAdvertencia.Visible = True
                    limpiarCampos()
                    GdFacturas.DataBind()
                End If
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
            End If
        Else

            strSQL = " select top 500 idFactura, num_factura, serie, importe, fecha, nombre,paciente,idCliente " &
            "from factura where idCliente=" & LstBoxPacientes.SelectedValue & " and cancelada='False' order by fecha desc"

            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    GdBuscarReferencias.DataSource = dt
                    GdBuscarReferencias.DataBind()
                    ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('show');</script>", False)
                Else
                    LblMensajeAdvertencia.Text = "El paciente no tiene facturas para asignar"
                    PanelAdvertencia.Visible = True
                    limpiarCampos()
                    GdFacturas.DataBind()
                End If
            Else
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
            End If
        End If


        LstBoxPacientes.Items.Clear()


    End Sub
    
    'conciliacion de abonos, actualizado 28/02/2018 Antonio Briceño
    Sub Conciliar(sender As Object, e As EventArgs)
        ocultarPaneles()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim sumaTotal As Double = 0.0
        Dim strSql As String
        If saldo.Value < 0 Then
            Exit Sub
        End If
        strSql = "select Num_factura from [" & clsDatos.BaseDatos & "].[dbo].[factura] " &
                 "where num_factura='" & TxtReferencia.Text.Trim & "' and cancelada='False'"

        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                ' existe la factura,se le asignan a los abonos correspondientes
                strSql = ""
                For Each fila As GridViewRow In GdFacturas.Rows
                    If fila.BackColor = Drawing.Color.DarkTurquoise Then

                        strSql += "update [" & clsDatos.BaseDatos & "].[dbo].[Abonos] set facturado='true',num_factura='" & TxtReferencia.Text.Trim & "',serie='" & TxtSerie.Text.Trim & "' " & _
                                  "where idabono=" & fila.Cells(0).Text & " "
                    End If
                Next
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = 0 Then
                    LblMensajeAviso.Text = " Se realizó correctamente la asignación!"
                    PanelAvisos.Visible = True
                    PanelAvisos.Focus()
                    cargaFacturas()
                Else
                    LblMensajeAdvertencia.Text = "No se ha seleccionado algún abono o existe un error al asignar"
                    PanelAdvertencia.Visible = True
                End If
            Else
            End If
        End If

       
    End Sub

    'Abrir la factura para conciliar, actualizado 28/02/2018 Antonio Briceño
    Private Sub GdBuscarReferencias_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdBuscarReferencias.RowCommand
        If e.CommandName = "Abrir" Then
            Dim index As Integer
            index = CInt(e.CommandArgument)
            TxtReferencia.Text = GdBuscarReferencias.Rows(index).Cells(1).Text
            HdReferencia.Value = GdBuscarReferencias.Rows(index).Cells(1).Text
            Hdidcliente.Value = GdBuscarReferencias.Rows(index).Cells(7).Text
            HdfolioIngreso.Value = GdBuscarReferencias.Rows(index).Cells(0).Text

            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('hide');</script>", False)
            verReferencias()
        End If
    End Sub
    'Ver datos de la factura, actualizado 27/02/2018 Antonio Briceño
    Private Sub verReferencias() ' se agrego para simular el click del boton ver referencias.  
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim dtFactura As New DataTable
        Dim strSQL As String

        cargaFacturas()

        strSQL = "select idfactura,F.num_factura as factura,F.serie as serie,F.importe as importe,F.importe-(isnull(sum(A.importe),0) - isnull(sum(A.monto),0)) as saldo,F.tipoDePago as tipoDePago ,F.paciente as paciente, F.fecha as fecha from factura F " &
        "inner join abonos as A on A.num_factura=F.num_factura " &
        "where F.idFactura='" & HdfolioIngreso.Value & "' group by F.idFactura,F.num_factura,F.serie,F.importe,F.tipoDePago,F.paciente,F.fecha"

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                CargaDatosVerReferencias(dt)
            Else 'si no encontró factura asociada a un abono, que vaya a buscar sólo la factura (por si quedó desasignada y era la única referencia)

                strSQL = "select idfactura,F.num_factura as factura,F.serie as serie,F.importe as importe, F.importe as saldo, F.tipoDePago as tipoDePago ,F.paciente as paciente, F.fecha as fecha " &
                "from factura F " &
                "where F.idFactura='" & HdfolioIngreso.Value & "' "

                If clsDatos.cargatabla(strSQL, dtFactura) = 0 Then
                    If dtFactura.Rows.Count > 0 Then
                        CargaDatosVerReferencias(dtFactura)
                    Else
                        LblMensajeAdvertencia.Text = "No se encontró la factura"
                        PanelAdvertencia.Visible = True
                    End If
                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If

        strSQL = "SELECT A.idAbono as idabono,A.idcita as idcita,A.num_factura as factura,A.serie as serie,A.importe as importe,catservicios.descripcion as servicio,F.idfactura  FROM abonos as A " &
        "inner join factura as F on A.num_factura=F.num_factura " &
        "left JOIN catCostos ON A.idCosto = catCostos.idCosto " &
        "left JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio " &
        "where F.idFactura='" & HdfolioIngreso.Value & "'"

        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdFacConciliadas.DataSource = dt
                GdFacConciliadas.DataBind()
                LblSinFacturas.Text = ""
            Else
                LblSinFacturas.Text = "Sin Abonos Asignados"
                Exit Sub
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If

    End Sub

    'Este método procesará el dataTable recibido para mostrar la info de la factura
    Private Sub CargaDatosVerReferencias(ByVal dt As DataTable)

        Try
            HdfolioIngreso.Value = dt.Rows(0).Item("idfactura")
            TxtSerie.Text = dt.Rows(0).Item("serie")
            TxtImporte.Text = dt.Rows(0).Item("importe")
            saldo.Value = dt.Rows(0).Item("saldo")
            validarSaldocero()

            If TxtImporte.Text = saldo.Value Then
                saldo.Attributes.CssStyle.Value = "danger"
            Else
                saldo.Attributes.CssStyle.Value = "succes"
            End If

            DdTipoPago.SelectedValue = dt.Rows(0).Item("tipoDePago")
            observaciones.Value = dt.Rows(0).Item("paciente")
            Fecha.Text = dt.Rows(0).Item("fecha")
            banderaImporte = 1
        Catch ex As Exception
            LblMensajeCritico.Text = "Error 123:" + ex.Message
            PanelCritico.Visible = True
        End Try
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
  
    'Seleccionar terapias, actualizado 28/02/2018 Antonio Briceño
    Private Sub GdFacturas_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdFacturas.RowCommand
        Dim index As Integer = Convert.ToInt32(e.CommandArgument)
        If e.CommandName = "Des-Seleccionar" Then
            Dim suma As Double = 0.0
            If GdFacturas.Rows(index).BackColor = Drawing.Color.DarkTurquoise Then
                GdFacturas.Rows(index).BackColor = Nothing
                suma = CDbl(saldo.Value) + CDbl(GdFacturas.Rows(index).Cells(6).Text)
                saldo.Value = suma
                CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = True
                CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = False
            End If
        End If
        If saldo.Value = 0 Then
            Exit Sub
        End If
        If e.CommandName = "Seleccionar" Then
            If GdFacturas.Rows(index).BackColor = Nothing Then
                Dim resta As Double
                resta = CDbl(saldo.Value) - CDbl(GdFacturas.Rows(index).Cells(6).Text)
                If resta >= 0 Then
                    GdFacturas.Rows(index).BackColor = Drawing.Color.DarkTurquoise
                    CType(GdFacturas.Rows(index).FindControl("BtnGdSeleccionar"), Button).Visible = False
                    CType(GdFacturas.Rows(index).FindControl("BtnGdDesSeleccionar"), Button).Visible = True
                    saldo.Value = resta
                Else
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
        ocultarPaneles()
        Dim strSql As String
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim funcion As New FuncionesGenerales
        Dim saldoDetFacturas As Double
        ' Dim saldoIngresoBancos As Double
        Dim facturasDesasignadas As String = ""
        Dim errorDesasiganFac As String = ""
        Dim contFacturasAsignadas As Integer = 0
        Dim contFacturasDesasignada As Integer = 0
        For Each fila As GridViewRow In GdFacConciliadas.Rows
            contFacturasAsignadas = contFacturasAsignadas + 1
            If fila.BackColor = Drawing.Color.DarkTurquoise Then
                strSql = "select F.importe-(isnull(sum(A.importe),0) - isnull(sum(A.monto),0)) as saldo from Factura F " & _
                    "inner join abonos as A on A.num_factura=F.num_factura " & _
                    "where F.idFactura=" & fila.Cells(6).Text & " group by F.idFactura,F.importe"

                If clsDatos.cargatabla(strSql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        saldoDetFacturas = dt.Rows(0).Item("saldo") + CDbl(fila.Cells(4).Text)
                        strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[Abonos] set facturado='False',num_factura=NULL,serie=NULL " & _
                                 "where idabono=" & fila.Cells(0).Text & " "
                        clsDatos.cargaComando(strSql)
                        If clsDatos.ejecutar() = 0 Then
                            facturasDesasignadas += fila.Cells(0).Text + " "
                            contFacturasDesasignada = contFacturasDesasignada + 1
                        Else
                            errorDesasiganFac += fila.Cells(0).Text + " "
                        End If
                    Else
                        Exit Sub
                    End If
                Else
                    LblMensajeCritico.Text = "Error 1278: " + clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If

                '
                'aqui estaba elscript de ejecucion de la modificacion del abono
                '
            End If
        Next
        If contFacturasAsignadas = contFacturasDesasignada Then
        End If
        If errorDesasiganFac = "" Then
            LblMensajeAviso.Text = "Lo(s) abonos(s) " + facturasDesasignadas + " se han desasignado"
            PanelAvisos.Visible = True

        Else
            LblMensajeCritico.Text = "No se logró desasignar lo(s) abonos(s) " + errorDesasiganFac
            PanelCritico.Visible = True
        End If
        ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#modal_buscar_referencia').modal('hide');</script>", False)
        verReferencias()
        cargaFacturas()
    End Sub


    Protected Sub limpiarCampos()
        TxtImporte.Text = 0.0
        TxtReferencia.Text = ""
        HdReferencia.Value = ""
        saldo.Value = 0.0
        observaciones.Value = ""
        Fecha.Text = ""

    End Sub
End Class
