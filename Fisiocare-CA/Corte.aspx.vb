Imports System.Data
Imports System.Data.SqlClient
Imports System.Data.Sql
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Imports System.IO
Imports System.Collections.Generic
Imports System.Windows.Forms

Partial Class Corte
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim fCorreos As New Funciones
    Public mireporte As New ReportDocument

    Sub buscar()
        DataGrid1 = funciones.creadataset(laConsulta, DataGrid1)

        Dim dic As New Dictionary(Of String, Decimal) 'para guardar totales dinámicamente

        If DataGrid1.Items.Count > 0 Then
            Dim contador As Integer = 0
            Dim monto As Decimal
            Dim formaPago As String
            Dim montoStr As String

            Do While contador < DataGrid1.Items.Count
                formaPago = DataGrid1.Items(contador).Cells(4).Text
                montoStr = DataGrid1.Items(contador).Cells(5).Text
                montoStr = montoStr.Replace("$", "").Replace(",", "").Trim
                monto = Decimal.Parse(montoStr)

                If dic.ContainsKey(formaPago) Then 'si existe, tomar el valor y actualizar
                    Dim acumulado As Decimal
                    acumulado = dic(formaPago)
                    acumulado += monto
                    dic(formaPago) = acumulado

                Else 'si no existe, agregar
                    dic.Add(formaPago, monto)
                End If

                contador += 1
            Loop
            ArmarTotales(dic)
        Else

        End If
    End Sub

    Sub ArmarTotales(ByVal dic As Dictionary(Of String, Decimal))
        Dim montoDiccionario As Decimal
        Dim formaPagoDiccionario As String

        Dim textoLbl As String
        Dim monto As Decimal
        Dim montoFormateado As String

        Dim montoTotal As Decimal = 0
        Dim montoTotalFormateado As String

        'Ordenamos el listado para que siempre venga en el mismo orden
        Dim dicOrder As List(Of String) = New List(Of String)

        For Each dicRow As KeyValuePair(Of String, Decimal) In dic
            dicOrder.Add(dicRow.Key)
        Next

        dicOrder.Sort()

        For Each key As String In dicOrder
            montoDiccionario = dic.Item(key)
            formaPagoDiccionario = key

            'Obtenemos texto y monto
            textoLbl = System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(formaPagoDiccionario.ToLower())
            textoLbl = "  Total en " + textoLbl + ": $"

            monto = montoDiccionario
            montoTotal += monto
            montoFormateado = Format(monto, "###,###,###0.00")

            'creamos div
            CreaDivLabelYMonto(textoLbl, montoFormateado)
        Next

        'leyenda y monto total
        montoTotalFormateado = Format(montoTotal, "###,###,###0.00")
        CreaDivLabelYMonto("  Total: $", montoTotalFormateado)
    End Sub

    Sub CreaDivLabelYMonto(ByVal textoLbl As String, ByVal montoFormateado As String)
        'Creamos div y labels
        Dim contenido As String
        Dim div As New HtmlGenericControl("div")

        contenido = textoLbl + "<span style=""color:Black;font-weight:bold"">" + montoFormateado + "</span>"
        Dim h4Leyenda As New HtmlGenericControl("h4")
        h4Leyenda.Attributes("class") = "modal-title"
        h4Leyenda.InnerHtml = contenido

        'lo agregamos al div y luego al divContainer
        div.Controls.Add(h4Leyenda)
        divContainer.Controls.Add(div)
    End Sub

    Function laConsulta() As String
        Dim fecha1 As Date = txtFecha.Text.Trim
        Dim fecha2 As Date = txtfecha2.Text.Trim
        Dim consulta As String = "EXEC FISIOCARE_Rep_CorteCaja '" + fecha1 + "', '" + fecha2 + "' "
        Dim turno As String
        Dim terapista As String

        If cboturno.SelectedIndex = "0" Then 'ningún filtro aplicado

        Else
            If cboterapistas.SelectedIndex = "0" Then 'sólo filtro de turno
                turno = ", '" + cboturno.SelectedValue + "' "

                consulta += turno
            Else 'filtro de turno y terapista
                turno = ", '" + cboturno.SelectedValue + "' "
                terapista = ", '" + cboterapistas.SelectedValue + "' "

                consulta += turno + terapista
            End If
        End If

        Return consulta
    End Function
    Protected Sub cboterapistas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboterapistas.SelectedIndexChanged
        buscar()
    End Sub

    Protected Sub cboturno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboturno.SelectedIndexChanged
        cboterapistas = funciones.llenacombos(cboterapistas, "SELECT columna, nombre FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")
        buscar()
    End Sub
   
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFechas.Click
        buscar()
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cboterapistas = funciones.llenacombos(cboterapistas, "SELECT columna, nombre FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")

            Try
                lblfechacita.Text = Context.Items("fecha").ToString.Trim
            Catch
                lblfechacita.Text = Request.QueryString("fecha").Trim
            End Try

            Dim hoy As DateTime = DateTime.Now()
            lblfecha1.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            lblfecha2.Text = hoy.Day.ToString + " " + MonthName(hoy.Month)
            txtFecha.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            calendario.SelectedDate = txtFecha.Text
            calendario2.SelectedDate = txtfecha2.Text
            buscar()
        End If
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario2_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario2.SelectionChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario2_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario2.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Try
            Dim lafecha1 As Date = calendario.SelectedDate
            Dim lafecha2 As Date = calendario2.SelectedDate
            lblfecha1.Text = lafecha1.Day.ToString + " " + MonthName(lafecha1.Month)
            lblfecha2.Text = lafecha2.Day.ToString + " " + MonthName(lafecha2.Month)
            txtFecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha1)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", lafecha2)
            buscar()
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS...")
        End Try
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
