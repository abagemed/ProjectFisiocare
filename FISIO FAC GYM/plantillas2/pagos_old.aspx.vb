Imports Scripting

Public Class pagos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPanelesPagos()
        mensaje_pago.InnerText = ""
        If Not IsPostBack Then
            If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
                Response.Redirect("login.aspx")
            End If
            Hffolioconsulta.Value = Session("FolioConsulta")
            imprimir_ticket_btn.Visible = False
            listaConceptoPagos()
            cargarConceptoPagos()
            leePagoRealizado()
            calcularSaldo()
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
                LblPagoSaldo.Text = dt.Rows(0).Item("Total")

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
        'strsql = "SELECT Abono  FROM [" & clsdatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
        '        "where folioConsulta='" & Hffolioconsulta.Value & "' AND codigoempresa = " & Session("codigoEmpresa") & " AND status=1"
        'If clsdatos.cargatabla(strsql, dt) = 0 Then
        '    If Not IsDBNull(dt.Rows(0).Item("Abono")) Then
        '        LblPagoSaldo.Text = dt.Rows(0).Item("Abono")
        '        TxtAbono.Text = CDbl(LblPagoTotal.Text) - CDbl(LblPagoSaldo.Text)
        '    Else
        '        LblPagoSaldo.Text = "0"

        '    End If
        'Else
        '    LblMensajeCriticoPagos.Text = clsdatos.MensajeError
        '    PanelCriticoPagos.Visible = True

        'End If
    End Sub
    Sub cargarConceptoPagos()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = " SELECT codigoConcepto,servicio,cantidad,descripcion,format(costo,'C','en-us') as costo FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                 "where folioConsulta='" & Hffolioconsulta.Value & "' and codigoempresa = '" & Session("codigoEmpresa") & "'  and status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            GdvConceptoPagos.Columns(0).Visible = True
            GdvConceptoPagos.DataSource = dt
            GdvConceptoPagos.DataBind()
            GdvConceptoPagos.Columns(0).Visible = False

            GdvConceptoPagos2.Columns(0).Visible = True
            GdvConceptoPagos2.DataSource = dt
            GdvConceptoPagos2.DataBind()
            GdvConceptoPagos2.Columns(0).Visible = False

            GdvConceptoPagos3.Columns(0).Visible = True
            GdvConceptoPagos3.DataSource = dt
            GdvConceptoPagos3.DataBind()
            GdvConceptoPagos3.Columns(0).Visible = False


            cargarTotalPagos()
            'calcularSaldo()
        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub
    Private Sub GdvConceptoPagos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvConceptoPagos.RowCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim fila As Integer = e.CommandArgument
        Dim codigoConcepto As Integer = GdvConceptoPagos.Rows.Item(fila).Cells(0).Text()
        If e.CommandName = "borrarConceptoPago" Then
            LblDecisionPagos.Text = "¿Deseas quitar el concepto de pago?"
            PanelDesicionPagos.Visible = True

            HdPreguntasPagos.Value = "0"
            HdIndexPagos.Value = codigoConcepto
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
            cargarConceptoPagos()
            ActualizarIngresoCaja()
            If TxtAbono.Text <> "" Then
                LblPagoSaldo.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
                LblPagoTotal2.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
                LblPagoTotal3.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
            End If
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
        Dim dt As New DataTable
        Dim band As Boolean = True
        dt.Columns.Add("codigoConcepto")
        dt.Columns.Add("cantidad")
        dt.Columns.Add("servicio")
        dt.Columns.Add("descripcion")
        dt.Columns.Add("costo")
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.Columns(1).Visible = True
        GdvConceptoPagos.Columns(2).Visible = True
        For Each fila As GridViewRow In GdvConceptoPagos.Rows
            If fila.Cells(0).Text = LstConceptoPagos.SelectedValue Then
                band = False
                GdvConceptoPagos.Columns(0).Visible = False
                Exit For
            Else
                band = True
                dt.Rows.Add({fila.Cells(0).Text, fila.Cells(1).Text, fila.Cells(2).Text, fila.Cells(3).Text, fila.Cells(4).Text.ToString})
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
            'servicio = LstConceptoPagos.SelectedItem.Text.Substring(0, InStr(LstConceptoPagos.SelectedItem.Text, "-") - 1)
            'desc = LstConceptoPagos.SelectedItem.Text.Substring(InStr(LstConceptoPagos.SelectedItem.Text, "-"), InStr(LstConceptoPagos.SelectedItem.Text, "-") - 1)
            'costo = LstConceptoPagos.SelectedItem.Text.Substring(InStr(LstConceptoPagos.SelectedItem.Text, "-") - 2)
            'cant = cantidad.Value
            'servicio = "xxxxx"  '  fata****************************************************
            dt.Rows.Add({LstConceptoPagos.SelectedValue, cant, servicio, desc, costo})
            GdvConceptoPagos.DataSource = dt
            GdvConceptoPagos.DataBind()
            GdvConceptoPagos.Columns(0).Visible = False
            GdvConceptoPagos.Columns(1).Visible = True
            GdvConceptoPagos.Columns(2).Visible = True
            guardarConceptoPagos()
            ActualizarIngresoCaja()
            If TxtAbono.Text <> "" Then
                LblPagoSaldo.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
                LblPagoTotal2.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
                LblPagoTotal3.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
            End If
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
                cargarConceptoPagos()
                'GdvConceptoPagos.DataBind()
                'GdvConceptoPagos.Columns(0).Visible = False
                ActualizarIngresoCaja()
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
        Dim strSQL As String
        Dim abono As String = Replace(LblPagoTotal2.Text, ",", "")
        Dim pago As String = Replace(LblPagoTotal.Text, ",", "")
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' ,importePago='" & pago & "',codigoFormaPago=1, abono='" & pago & "' " & _
                 "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0"

        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then

        Else
            LblMensajeAdvertenciaPagos.Text = clsDatos.MensajeError
            PanelAdvertenciaPagos.Visible = True
            'PanelAdvertenciaPagos.Focus()
            Exit Sub
        End If
    End Sub
    'Registrar Pagos, pago realizado
    Sub ingresoCaja()

        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim abono As String = Replace(LblPagoTotal2.Text, ",", "")
        Dim pago As String = Replace(LblPagoTotal.Text, ",", "")
        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set codigoUsuario=" & Session("codigoUsuario") & ",fechaIngreso='" & Session("FechaAgenda") & "' ,importePago=" & pago & ",codigoFormaPago=1,pagoRealizado=1, abono=" & abono & " " & _
                 "where folioconsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & " and Pagorealizado=0"

        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            lista_conceptos.Visible = False
            registrar_pago.Visible = False
            mensaje_pago.Visible = True
            mensaje_pago.InnerText = "El pago esta registrado"
            imprimir_ticket_btn.Visible = True

        Else
            LblMensajeAdvertenciaPagos.Text = "Error al guardar el pago CodeError:1234"
            PanelAdvertenciaPagos.Visible = True
            'PanelAdvertenciaPagos.Focus()
            Exit Sub
        End If
        Try
            Imprimir_Ticket()
        Catch ex As Exception
            LblMensajeAdvertenciaPagos.Text = "Error al imprimir ticket: " + ex.Message
            PanelAdvertenciaPagos.Visible = True
            'PanelAdvertenciaPagos.Focus()
        End Try
    End Sub

    Sub leePagoRealizado()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = " SELECT importePago,importePago,Abono  FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja]  " & _
                " WHERE folioConsulta='" & Session("folioConsulta") & "' AND FechaIngreso='" & Session("FechaAgenda") & "' AND codigoEmpresa=" & Session("codigoEmpresa") & " AND status=1 AND pagoRealizado=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                lista_conceptos.Visible = False
                registrar_pago.Visible = False
                mensaje_pago.InnerText = "El pago esta registrado"
                mensaje_pago.Visible = True
                TxtAbono.Text = Format(CDbl(dt.Rows(0).Item("importePago")) - CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                LblPagoSaldo.Text = Format(CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                'TxtAbono.Enabled = False
                TxtAbono.ReadOnly = True
                imprimir_ticket_btn.Visible = True

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
        GdvConceptoPagos.Columns(5).Visible = False
        TxtAbono.ReadOnly = True
    End Sub

    Private Sub TxtAbono_TextChanged(sender As Object, e As EventArgs) Handles TxtAbono.TextChanged
        If TxtAbono.Text = "" Then
            TxtAbono.Text = 0.0
        End If
        LblPagoSaldo.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
        LblPagoTotal2.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
        LblPagoTotal3.Text = CDbl(LblPagoTotal.Text) - CDbl(TxtAbono.Text)
    End Sub

    Private Sub calcularSaldo()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim StrSQL, apoyo As String
        apoyo = "0.0"
        StrSQL = "SELECT importePago,pagoRealizado,Abono " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                 "where FolioConsulta='" & Session("folioConsulta") & "'"
        If clsDatos.cargatabla(StrSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("PagoRealizado") = 1 Then
                    LblPagoSaldo.Text = Format(dt.Rows(0).Item("Abono"), "##,##0.00")
                    TxtAbono.Text = Format(CDbl(dt.Rows(0).Item("ImportePago")) - CDbl(dt.Rows(0).Item("Abono")), "##,##0.00")
                End If
                If TxtAbono.Text = "" Then
                    apoyo = "0.0"
                Else
                    apoyo = TxtAbono.Text
                End If
                LblPagoSaldo.Text = CDbl(LblPagoTotal.Text) - CDbl(apoyo)
                LblPagoTotal2.Text = CDbl(LblPagoTotal.Text) - CDbl(apoyo)
                LblPagoTotal3.Text = CDbl(LblPagoTotal.Text) - CDbl(apoyo)

            End If
        End If

    End Sub


End Class