Imports Scripting

Public Class pagos
    Inherits System.Web.UI.Page
    Dim _Total As Double = 0
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ocultarPanelesPagos()
        mensaje_pago.InnerText = ""
        If Not IsPostBack Then
            If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
                Response.Redirect("login.aspx")
            End If
            Hffolioconsulta.Value = Session("FolioConsulta")
            LblNombrePaciente.Text = Session("nombrePaciente")
            imprimir_ticket_btn.Visible = False
            listaConceptoPagos()
            cargarConceptoPagos()
            cargarAbonosRealizados()
            leePagoRealizado()
        End If
        cargarpaciente()
    End Sub
    Sub cargarTotalPagos()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT format(sum(cantidad*costo),'N','en-us') as Total " & _
                 " FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                   "WHERE folioConsulta='" & Hffolioconsulta.Value & "' AND codigoempresa = " & Session("codigoEmpresa") & " AND status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Total")) Then
                LblPagoTotal.Text = dt.Rows(0).Item("Total")
                LblPagoTotal2.Text = dt.Rows(0).Item("Total")
                LblPagoTotal3.Text = dt.Rows(0).Item("Total")
            Else
                LblPagoTotal.Text = "0.00"
                LblPagoTotal2.Text = "0.00"
                LblPagoTotal3.Text = "0.00"
            End If
        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True
        End If
        
    End Sub
    Sub cargarConceptoPagos()
        Dim dt3 As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = " SELECT codigoConcepto,servicio,cantidad,descripcion,format(costo,'C','en-us') as costo FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                 "where folioConsulta='" & Hffolioconsulta.Value & "' and codigoempresa = '" & Session("codigoEmpresa") & "'  and status=1"
        If clsdatos.cargatabla(strsql, dt3) = 0 Then
            If dt3.Rows.Count > 0 Then
                registrar_pago.Disabled = False
            Else
                registrar_pago.Disabled = True
            End If
            Page.Session("dt3") = dt3
            GdvConceptoPagos.Columns(0).Visible = True
            GdvConceptoPagos.DataSource = dt3
            GdvConceptoPagos.DataBind()
            'GdvConceptoPagos.Columns(0).Visible = False  ?????????

            GdvConceptoPagos2.Columns(0).Visible = True
            GdvConceptoPagos2.DataSource = dt3
            GdvConceptoPagos2.DataBind()
            GdvConceptoPagos2.Columns(0).Visible = False

            GdvConceptoPagos3.Columns(0).Visible = True
            GdvConceptoPagos3.DataSource = dt3
            GdvConceptoPagos3.DataBind()
            GdvConceptoPagos3.Columns(0).Visible = False

            cargarTotalPagos()
            calcularSaldo()
        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub

    Sub cargarAbonosRealizados()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT Id,FolioConsulta as Folio_Consulta, importe as Importe ,FP.descripcion as Tipo_Pago,Referencia,fechaIngreso as Fecha_Ingreso,(nombre+' '+primerapellido+' '+segundoapellido) as Nombre " & _
                 " FROM [" & clsdatos.BaseDatos & "].[dbo].[IngresosPagosCajas] as I  " & _
                 "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatFormasDePago] as FP on I.codigoFormaPago=FP.codigoFormaPago " & _
                "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=I.codigoUsuario and U.codigoEmpresa=I.codigoEmpresa " & _
                 "where I.folioConsulta='" & Hffolioconsulta.Value & "' and I.codigoempresa = '" & Session("codigoEmpresa") & "' " & _
                 "Order by fechaIngreso desc"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            GdAbonosRealizados.DataSource = dt
            GdAbonosRealizados.DataBind()
            'format (importe,'C','es-mx')
        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub

    
    Private Sub GdvConceptoPagos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvConceptoPagos.RowCommand
        
        Dim fila As Integer = e.CommandArgument
        Dim codigoConcepto As Integer = 0
        If e.CommandName = "borrarConceptoPago" Then

            Try
                GdvConceptoPagos.Columns(0).Visible = True
                codigoConcepto = GdvConceptoPagos.Rows(fila).Cells(0).Text
                'GdvConceptoPagos.Columns(0).Visible = False      ????????????????????
                LblDecisionPagos.Text = "¿Deseas quitar el concepto de pago?"
                PanelDesicionPagos.Visible = True
                HdPreguntasPagos.Value = "0"
                HdIndexPagos.Value = codigoConcepto
            Catch ex As Exception
                LblMensajeAdvertenciaPagos.Text = " Actualice la página, no se pudo borrar el concepto de pago."
                PanelAdvertenciaPagos.Visible = True
            End Try

        End If
    End Sub


    Protected Sub BtnSiPagos_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasPagos.Value
            Case "0"
                eliminarConceptoPago()
                ocultarPanelesPagos()
        End Select
    End Sub

    Protected Sub BtnNoPagos_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasPagos.Value
            Case "0"
                ocultarPanelesPagos()
        End Select
    End Sub

    Sub ocultarPanelesPagos()
        PanelAdvertenciaPagos.Visible = False
        PanelCriticoPagos.Visible = False
        PanelDesicionPagos.Visible = False
    End Sub

    Sub eliminarConceptoPago()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "    update  [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] set status=2 " & _
                     "where folioConsulta='" & Hffolioconsulta.Value & "'  and codigoEmpresa =" & Session("codigoEmpresa") & " and status=1 and codigoConcepto='" & HdIndexPagos.Value & "' "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            If TxtAbono.Text <> "" Then
                LblPagoSaldo.Text = Format(CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text), "##,##0.00")
                LblPagoTotal2.Text = CDbl(TxtAbono.Text)
                LblPagoTotal3.Text = CDbl(TxtAbono.Text)
                If LblPagoSaldo.Text < 0 Then
                    registrar_pago.Disabled = True
                End If
            End If
            HdEliminaConcepto.Value = 1
            cargarConceptoPagos()
            ActualizarIngresoCaja()
            calcularSaldo()
        Else
            LblMensajeCriticoPagos.Text = clsDatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub

    Sub listaConceptoPagos()
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim strsql As String
        LstConceptoPagos.Items.Clear()
        strsql = "SELECT  codigoConcepto,(servicio+'     -     '+ descripcion + '      -     $ ' + convert(varchar,convert(decimal(8,2),costo))) as descripcion   FROM [" & clsDatos.BaseDatos & "].[dbo].[catConceptoPagos] " & _
        "where status = 1  " & _
         "order by descripcion"
        If funciones.llenalistbox(strsql, LstConceptoPagos) = -1 Then
            LblMensajeCriticoPagos.Text = funciones.MensajeError
            PanelCriticoPagos.Visible = True
            PanelCriticoPagos.Focus()
        End If
    End Sub
    Private Sub LstConceptoPagos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstConceptoPagos.SelectedIndexChanged
        Dim dt2 As New DataTable
        Dim band As Boolean = True

        dt2.Columns.Add("codigoConcepto")
        dt2.Columns.Add("cantidad")
        dt2.Columns.Add("servicio")
        dt2.Columns.Add("descripcion")
        dt2.Columns.Add("costo")
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.Columns(1).Visible = True
        GdvConceptoPagos.Columns(2).Visible = True
        For Each fila As GridViewRow In GdvConceptoPagos.Rows
            If fila.Cells(0).Text = LstConceptoPagos.SelectedValue Then
                band = False
                'GdvConceptoPagos.Columns(0).Visible = False    ?????????????????????
                Exit For
            Else
                band = True
                dt2.Rows.Add({fila.Cells(0).Text, fila.Cells(1).Text, fila.Cells(2).Text, fila.Cells(3).Text, fila.Cells(4).Text.ToString})
            End If
        Next
        If band Then
            Dim costo, desc, servicio As String
            Dim cant As Integer
            Dim datos() As String
            datos = Split(LstConceptoPagos.SelectedItem.Text, "-", -1)
            servicio = datos(0).Trim
            desc = datos(1).Trim
            costo = datos(2).Trim
            cant = cantidad.Value
            If costo = 0 Then
                HdConceptoPagoCERO.Value = 1
            End If
            dt2.Rows.Add({LstConceptoPagos.SelectedValue, cant, servicio, desc, costo})
            GdvConceptoPagos.DataSource = dt2
            GdvConceptoPagos.DataBind()
            GdvConceptoPagos.Columns(0).Visible = True
            GdvConceptoPagos.Columns(1).Visible = True
            GdvConceptoPagos.Columns(2).Visible = True
            If TxtAbono.Text <> "" Then
                'LblPagoSaldo.Text = Format(CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text), "##,##0.00")
                Dim xcosto As Double = CDbl(LblPagoTotal.Text)
                LblPagoTotal.Text = xcosto + (costo * cant)

            End If
            guardarConceptoPagos()
            'ActualizarIngresoCaja()
           
            cantidad.Value = 1
        End If
        'cargarConceptoPagos()
    End Sub

    Sub guardarConceptoPagos()
        Dim clsdatos As New ClaseDatos
        Dim strsql, codigos As String
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.Columns(1).Visible = True
        codigos = ""
        For Each fila As GridViewRow In GdvConceptoPagos.Rows
            codigos += fila.Cells(0).Text & ","
        Next
        If codigos.Length > 0 Then
            codigos = codigos.Substring(0, codigos.Length - 1)
            strsql = "  insert into [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] (folioConsulta,codigoConcepto,descripcion,costo,codigoEmpresa,codigoUsuario,fecha,status,cantidad,servicio) " & _
                    " select '" & Hffolioconsulta.Value & "',CP.codigoConcepto,CP.descripcion,CP.costo,codigoEmpresa=" & Session("codigoEmpresa") & ",codigoUsuario=" & Session("codigoUsuario") & ",GETDATE(),1," & cantidad.Value & ",CP.servicio  " & _
                    " from [" & clsdatos.BaseDatos & "].[dbo].[catConceptoPagos] as CP " & _
                    " where CP.status=1  and CP.codigoConcepto in (" & codigos & ") " & _
                    "and CP.codigoConcepto not in ( select codigoConcepto from  [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos]  " & _
                     "where folioConsulta= '" & Hffolioconsulta.Value & "' and codigoEmpresa=1 and status=1)"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then

                'GdvConceptoPagos.DataBind()
                'GdvConceptoPagos.Columns(0).Visible = False
                ActualizarIngresoCaja()
                cargarConceptoPagos()

            Else
                LblMensajeCriticoPagos.Text = clsdatos.MensajeError
                PanelCriticoPagos.Visible = True
                PanelCriticoPagos.Focus()
            End If

        Else
            LblMensajeAdvertenciaPagos.Text = "Seleccione un concepto"
            PanelAdvertenciaPagos.Visible = True
            PanelAdvertenciaPagos.Focus()
            Exit Sub
        End If
    End Sub
    'actualizar ingresaCajas al agregar un concepto de pago
    Sub ActualizarIngresoCaja()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL, pagoRealizado As String
        Dim abono As String = Replace(TxtAbono.Text, ",", "")
        Dim pago As String = Replace(LblPagoTotal.Text, ",", "")
        strSQL = "SELECT format(sum(cantidad*costo),'N','en-us') as Total " & _
                 " FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                   "WHERE folioConsulta='" & Session("folioConsulta") & "' AND codigoempresa = " & Session("codigoEmpresa") & " AND status=1"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Total")) Then
                pago = CDbl(dt.Rows(0).Item("Total").ToString)
            End If
        Else
            LblMensajeCriticoPagos.Text = "ErrorPage 9823: " + clsDatos.MensajeError
            PanelCriticoPagos.Visible = True
        End If
        If TxtAbono.Text = "" Then
            TxtAbono.Text = 0.0
        End If
        If CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text) = 0 And HdEliminaConcepto.Value = 0 And HdConceptoPagoCERO.Value = 0 Then
            If CDbl(LblPagoSaldo.Text) = 0 Then
                pagoRealizado = 1
            Else
                pagoRealizado = 0
            End If
            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set pagoRealizado=" & pagoRealizado & ",codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' " & _
         "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                TxtAbono.Text = LblPagoTotal.Text
                HdEliminaConcepto.Value = 0
                'aqui estoy modifivcando Gerardo 15/11/2017
                Exit Sub
            Else
                LblMensajeAdvertenciaPagos.Text = "ErrorPage 2001: " + clsDatos.MensajeError
                PanelAdvertenciaPagos.Visible = True
                'PanelAdvertenciaPagos.Focus()
                Exit Sub
            End If
        Else
            pagoRealizado = 0
            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' ,importePago='" & pago & "' " & _
         "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                TxtAbono.Text = LblPagoTotal.Text
                HdEliminaConcepto.Value = 0
            Else
                LblMensajeAdvertenciaPagos.Text = "ErrorPage 2001: " + clsDatos.MensajeError
                PanelAdvertenciaPagos.Visible = True
                'PanelAdvertenciaPagos.Focus()
                Exit Sub
            End If
        End If
        'strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set pagoRealizado=" & pagoRealizado & ",codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' ,importePago='" & pago & "' " & _
        ' "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0"
        'clsDatos.cargaComando(strSQL)
        'If clsDatos.ejecutar() = 0 Then
        '    TxtAbono.Text = LblPagoTotal.Text
        '    HdEliminaConcepto.Value = 0
        'Else
        '    LblMensajeAdvertenciaPagos.Text = "ErrorPage 2001: " + clsDatos.MensajeError
        '    PanelAdvertenciaPagos.Visible = True
        '    'PanelAdvertenciaPagos.Focus()
        '    Exit Sub
        'End If
    End Sub
   
    Sub ingresoCaja()

        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim pagado As Integer = 0
        Dim abono As String = Replace(LblPagoTotal2.Text, ",", "")
        Dim pago As String = Replace(LblPagoTotal.Text, ",", "")
        Try
            'Ingreso el pago total o parcial a la tabla IngresosPagosCajas
            If referencia.Value = "" Then
                referencia.Value = "0000000000"
            End If
            strSQL = " insert into [" & clsDatos.BaseDatos & "].[dbo].[IngresosPagosCajas] " & _
                  "(folioConsulta,importe,codigoFormaPago,referencia,fechaIngreso,codigoEmpresa,codigoUsuario) " & _
                   "values('" & Session("folioConsulta") & "','" & abono & "'," & forma_pago.Value & "," & referencia.Value & ",convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20)," & Session("codigoEmpresa") & "," & Session("codigoUsuario") & " ) "
            '-------------- agrega referencia en IngresosBancos en caso de ser tipo de pago diferente de 1.
            If forma_pago.Value <> 1 Then
                Dim observaciones As String
                observaciones = Session("folioConsulta") + " -- " + LblNombrePaciente.Text
                strSQL += " insert into [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco] ( referencia,status,codigoCuentaBancaria,importe,saldo,codigoFormaPago,observaciones " & _
                         ",fechaOperacion,fechaActualizacion,codigoUsuario,CodigoEmpresa) " & _
                         "VALUES(" & referencia.Value & " ,1,1," & abono & "," & abono & "," & forma_pago.Value & ",'" & observaciones & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20),convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20)," & Session("codigoUsuario") & "," & Session("codigoEmpresa") & ")"
            End If
            ' ------------- Fin agregar referencia  ------
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                LblMensajeAvisoPagos.Text = "Se ha registrado el pago. "
                PanelAvisosPagos.Visible = True
                cargarAbonosRealizados()
            Else
                LblMensajeAdvertenciaPagos.Text = "Error al guardar el pago CodeError:134 IngresoCajas" + clsDatos.MensajeError
                PanelAdvertenciaPagos.Visible = True
                Exit Sub
            End If
            ' Sumo los pagos realizados de dicha consulta 
            strSQL = " select sum(importe) as totalPagado,folioConsulta FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosPagosCajas] " & _
                    " where folioConsulta='" & Session("folioConsulta") & "' and codigoEmpresa=" & Session("codigoEmpresa") & " " & _
                    "  group by  folioConsulta"
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    If pago = Format(CDbl(dt.Rows(0).Item("totalPagado")), "#0.00") Then
                        pagado = 1
                        lista_conceptos.Visible = False
                        registrar_pago.Visible = False
                        mensaje_pago.Visible = True
                        mensaje_pago.InnerText = "El pago esta registrado"
                        imprimir_ticket_btn.Visible = True
                        LblPagoSaldo.Text = 0.0
                    Else
                        pagado = 0
                    End If
                    strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' ,codigoFormaPago=" & forma_pago.Value & ",pagoRealizado=" & pagado & ", abono=" & dt.Rows(0).Item("totalPagado") & " " & _
                     "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0 "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        TxtAbono.Text = Format(CDbl(LblPagoTotal.Text - dt.Rows(0).Item("totalPagado")), "##,##0.00")
                        LblPagoTotal2.Text = Format(CDbl(LblPagoTotal.Text - dt.Rows(0).Item("totalPagado")), "##,##0.00")

                    Else
                        LblMensajeAdvertenciaPagos.Text = "Error al guardar el pago CodeError:1234"
                        PanelAdvertenciaPagos.Visible = True
                        'PanelAdvertenciaPagos.Focus()
                        Exit Sub
                    End If
                    Try
                        'Imprimir_Ticket()   este es el bueno para ticket
                    Catch ex As Exception
                        LblMensajeAdvertenciaPagos.Text = "Error al imprimir ticket: " + ex.Message
                        PanelAdvertenciaPagos.Visible = True
                        'PanelAdvertenciaPagos.Focus()
                    End Try
                End If
            End If




        Catch ex As Exception

        End Try
        
    End Sub

    Sub leePagoRealizado()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = " SELECT importePago,Abono  FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja]  " & _
                " WHERE folioConsulta='" & Session("folioConsulta") & "' AND codigoEmpresa=" & Session("codigoEmpresa") & " AND status=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("importePago") = 0 And dt.Rows(0).Item("Abono") = 0 Then
                    Exit Sub
                End If
                If dt.Rows(0).Item("importePago") - dt.Rows(0).Item("Abono") < 0 Then
                    registrar_pago.Disabled = True
                End If
                If dt.Rows(0).Item("importePago") = dt.Rows(0).Item("Abono") Then
                    lista_conceptos.Visible = False
                    registrar_pago.Visible = False
                    mensaje_pago.InnerText = "El pago esta registrado"
                    mensaje_pago.Visible = True
                    TxtAbono.Text = Format(CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                    LblPagoSaldo.Text = 0.0

                    TxtAbono.ReadOnly = True
                    'imprimir_ticket_btn.Visible = True

                Else
                    TxtAbono.Text = Format(CDbl(dt.Rows(0).Item("importePago") - dt.Rows(0).Item("Abono")), "##,##0.00")
                    LblPagoSaldo.Text = Format(CDbl(dt.Rows(0).Item("importePago")- dt.Rows(0).Item("Abono")), "##,##0.00")
                End If

            Else
                'MsgBox("No esta pagado")
            End If

        End If
    End Sub

    Sub cargarpaciente()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = " SELECT  p.CodigoPaciente , p.papellido + ' ' + p.sapellido+ ' ' +p.nombres AS nombre, p.TelefonoCelular AS telefono, p.calle AS direccion  " & _
         " FROM agagenda a " & _
         " INNER JOIN catpacientes p ON p.codigopaciente = a.codigopaciente AND p.CodigoEmpresa = a.CodigoEmpresa " & _
         " WHERE a.folioconsulta = '" & Hffolioconsulta.Value & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                paciente_nombre.InnerText = dt.Rows(0).Item("nombre").trim
                paciente_telefono.InnerText = dt.Rows(0).Item("telefono").trim
                paciente_direccion.InnerText = dt.Rows(0).Item("direccion").trim
                folio_consulta.InnerHtml = "FOLIO CONSULTA: " & Session("folioConsulta")
                fecha_ticket.InnerHtml = Date.Now
            End If
        Else
            'Mensaje de error
        End If

    End Sub

    Sub Imprimir_Ticket()
        ScriptManager.RegisterStartupScript(Me, Page.GetType, "Script", "PrintTicket();", True)
        'No se porque razón

        'TxtAbono.ReadOnly = True
    End Sub

    Private Sub txtAbono_TextChanged(sender As Object, e As EventArgs) Handles TxtAbono.TextChanged
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim StrSQL, Abono As String
        ocultarPanelesPagos()
        StrSQL = "SELECT importePago,pagoRealizado,Abono " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                 "where FolioConsulta='" & Session("folioConsulta") & "'"
        If clsDatos.cargatabla(StrSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("importePago") = 0 Then
                    LblPagoSaldo.Text = Format(CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text), "##,##0.00")
                    LblPagoTotal2.Text = CDbl(TxtAbono.Text)
                    LblPagoTotal3.Text = CDbl(TxtAbono.Text)
                Else
                    'TxtAbono.Text = Format(dt.Rows(0).Item("Abono"), "##,##0.00")
                    LblPagoSaldo.Text = Format(CDbl(dt.Rows(0).Item("ImportePago")) - CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                    If TxtAbono.Text = "" Then
                        Abono = "0.0"
                    Else
                        Abono = TxtAbono.Text
                    End If
                    If 0 <= CDbl(LblPagoSaldo.Text) - CDbl(TxtAbono.Text) Then
                        registrar_pago.Disabled = False
                    Else
                        registrar_pago.Disabled = True
                    End If
                    LblPagoSaldo.Text = Format(CDbl(LblPagoSaldo.Text) - CDbl(TxtAbono.Text), "##,##0.00")
                    LblPagoTotal2.Text = CDbl(TxtAbono.Text)
                    LblPagoTotal3.Text = CDbl(TxtAbono.Text)

                End If
            End If
        Else
            LblMensajeCriticoPagos.Text = "Error txtAbono_TextChanged 126 : " + clsDatos.MensajeError
            PanelCriticoPagos.Visible = True
        End If
    
    End Sub

    Private Sub calcularSaldo()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim StrSQL, Abono As String
        Abono = "0.0"
        StrSQL = "SELECT importePago,pagoRealizado,Abono " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                 "where FolioConsulta='" & Session("folioConsulta") & "'"
        If clsDatos.cargatabla(StrSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                'If dt.Rows(0).Item("PagoRealizado") = 0 Then
                TxtAbono.Text = Format(CDbl(dt.Rows(0).Item("ImportePago")) - CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                LblPagoSaldo.Text = Format(CDbl(dt.Rows(0).Item("ImportePago")) - CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                'End If
                If TxtAbono.Text = "" Then
                    Abono = "0.0"
                Else
                    Abono = TxtAbono.Text
                End If

                LblPagoTotal2.Text = CDbl(Abono)
                LblPagoTotal3.Text = CDbl(Abono)

            End If
        End If
    End Sub


    Private Sub GdAbonosRealizados_RowDataBound(sender As Object, e As GridViewRowEventArgs) Handles GdAbonosRealizados.RowDataBound
        Try
            If e.Row.RowType = DataControlRowType.DataRow Then
                _Total += Convert.ToDecimal(DataBinder.Eval(e.Row.DataItem, "Importe"))
            Else
                If e.Row.RowType = DataControlRowType.Footer Then
                    e.Row.Cells(1).Text = "TOTAL: "
                    e.Row.Cells(1).HorizontalAlign = HorizontalAlign.Right
                    e.Row.Cells(1).Font.Size = 16
                    e.Row.Cells(2).Text = _Total.ToString("C2")
                    e.Row.Cells(2).HorizontalAlign = HorizontalAlign.Left
                    e.Row.Cells(2).Font.Size = 16

                End If
            End If
        Catch ex As Exception
            LblMensajeCriticoPagos.Text = "Error 56 : " + ex.ToString
            PanelCriticoPagos.Visible = True
        End Try
    End Sub

   

    Private Sub GdvConceptoPagos_RowEditing(sender As Object, e As GridViewEditEventArgs) Handles GdvConceptoPagos.RowEditing
        'Se indica que el componente entra en modo de edicion indicando el indice de la fila seleccionada
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.EditIndex = e.NewEditIndex
        'Complementariamente, se recargan los datos del componente para que al momento de entrar en modo de edicion se visualicen los datos de la fila seleccionada
        GdvConceptoPagos.DataSource = Page.Session("dt3")
        'Se enlaza con el origen de datos inidicado anteriormente el cual se almaceno en una variable de sesion.
        GdvConceptoPagos.DataBind()

    End Sub

    Private Sub GdvConceptoPagos_RowUpdating(sender As Object, e As GridViewUpdateEventArgs) Handles GdvConceptoPagos.RowUpdating
        'Manejo de errores
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String = ""
        Dim codigoConcepto As String = ""
        Dim descripcion As String = ""
        Dim costo As Double = 0.0
        Dim index As Integer
        index = CInt(e.RowIndex)
        Try
           
            'Asignar los valores proporcionados por el usuario a los parametros
            'Los valores se encontraran almacenados en la variable e que controla el evento de edicion, recordar que los elementos se enumeran desde 0 a n -1
            codigoConcepto = GdvConceptoPagos.Rows(e.RowIndex).Cells(0).Text
            descripcion = e.NewValues(0).ToString()
            Try
                costo = CDbl(e.NewValues(1).ToString().Trim)
            Catch ex As Exception
                LblMensajeCriticoPagos.Text = " Error 2356: " + ex.Message
                PanelCriticoPagos.Visible = True
            End Try
            'Agregamos los parametros al comando           
            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] set descripcion='" & descripcion & "',costo=" & costo & " " & _
                        "where folioConsulta='" & Session("folioConsulta") & "' and codigoConcepto='" & codigoConcepto & "' and codigoEmpresa=" & Session("codigoEmpresa") & " and status=1"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                cargarConceptoPagos()
                ActualizarIngresoCaja()
                calcularSaldo()
                GdAbonosRealizados.EditIndex = -1
                bindData()
            Else
                LblMensajeCriticoPagos.Text = " Error 2357: " + clsDatos.MensajeError
                PanelCriticoPagos.Visible = True

            End If

            'lblMensaje.Text = "Registro Insertado Exitosamente!!!";
            'Se indica que se termino la edicion de la fila seleccionada
            'Se recarla la tabla con la funcion creada para llenarla
        Catch ex As Exception
            LblMensajeCriticoPagos.Text = " Error 2359: " + ex.Message
            PanelCriticoPagos.Visible = True
            'Se muestra un error al usuario si existiera
        End Try

    End Sub
    Private Sub GdvConceptoPagos_RowCancelingEdit(sender As Object, e As GridViewCancelEditEventArgs) Handles GdvConceptoPagos.RowCancelingEdit
        bindData()
    End Sub
    Protected Sub bindData()
        GdvConceptoPagos.EditIndex = -1
        GdvConceptoPagos.DataSource = Session("dt3")
        GdvConceptoPagos.DataBind()

    End Sub
End Class