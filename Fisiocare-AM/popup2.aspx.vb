
Partial Class popup2
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim funciones As New miclases

        lblcita.Text = Request.QueryString("idcita").ToString.Trim
        lblcliente.Text = Request.QueryString("cliente").ToString.Trim
        lblfecha.Text = Request.QueryString("fecha").ToString.Trim
        lblabono.Text = Request.QueryString("abono").ToString.Trim
        'lblimporte1.Text = Request.QueryString("importe").ToString.Trim
        lbldetalle.Text = Request.QueryString("conceptos").ToString.Trim
        lblobservacion.Text = Request.QueryString("observacion").ToString.Trim
        lbldescuento.Text = Request.QueryString("descuento").ToString.Trim
        lblapoyo.Text = Request.QueryString("apoyo").ToString.Trim
        vobservacion.Text = Request.QueryString("vobservacion").ToString.Trim
        vdescuento.Text = Request.QueryString("vdescuento").ToString.Trim
        vapoyo.Text = Request.QueryString("vapoyo").ToString.Trim
        lblhora.text = Date.Now

        lblimporte2.Text = "Efectivo $: " + Request.QueryString("val1").ToString.Trim
        If Request.QueryString("val1").ToString.Trim <> "0.00" Then
            lblimporte2.Visible = True
        End If
        lblimporte1.Text = "Tarjeta de Credito $: " + Request.QueryString("val2").ToString.Trim
        If Request.QueryString("val2").ToString.Trim <> "0.00" Then
            lblimporte1.Visible = True
        End If
        lblimporte3.Text = "Tarjeta de Debito $: " + Request.QueryString("val3").ToString.Trim
        If Request.QueryString("val3").ToString.Trim <> "0.00" Then
            lblimporte3.Visible = True
        End If

        Dim popupScript As String = " window.print(); "
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "PopupScript", popupScript, True)

        Dim rowCnt As Integer  ' Current row count
        Dim rowCtr As Integer  ' Total number of cells (columns).
        Dim cellCtr As Integer ' Current cell counter.
        Dim cellCnt As Integer
        Dim cad As String

        rowCnt = funciones.num_elementos(lbldetalle.Text, "@") - 1
        cellCnt = 3
        For rowCtr = 1 To rowCnt
            Dim tRow As New TableRow()
            cad = funciones.entry(rowCtr, lbldetalle.Text, "@")
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                'tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr

                tCell.Text = funciones.entry(cellCtr, cad, "|")
                If cellCtr = 1 Then
                    tCell.Width = 30
                End If
                If cellCtr = 2 Then
                    tCell.Width = 150
                End If
                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            Table1.Rows.Add(tRow)
        Next


        Dim rowCntd As Integer  ' Current row count
        Dim rowCtrd As Integer  ' Total number of cells (columns).
        Dim cellCtrd As Integer ' Current cell counter.
        Dim cellCntd As Integer
        Dim cadd As String

        rowCntd = funciones.num_elementos(lbldescuento.Text, "@") - 1
        cellCntd = 2
        For rowCtrd = 1 To rowCntd
            Dim tRowd As New TableRow()
            cadd = funciones.entry(rowCtrd, lbldescuento.Text, "@")
            For cellCtrd = 1 To cellCntd
                Dim tCelld As New TableCell()
                'tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr

                tCelld.Text = funciones.entry(cellCtrd, cadd, "|")
                If cellCtrd = 1 Then
                    tCelld.Width = 80
                End If
                ' Add new TableCell object to row.
                tRowd.Cells.Add(tCelld)
            Next
            ' Add new row to table.
            If vdescuento.Text <> 0 Then
                Table2.Rows.Add(tRowd)
            Else

            End If

        Next

        Dim rowCnta As Integer  ' Current row count
        Dim rowCtra As Integer  ' Total number of cells (columns).
        Dim cellCtra As Integer ' Current cell counter.
        Dim cellCnta As Integer
        Dim cada As String

        rowCnta = funciones.num_elementos(lblapoyo.Text, "@") - 1
        cellCnta = 2
        For rowCtra = 1 To rowCnta
            Dim tRowa As New TableRow()
            cada = funciones.entry(rowCtra, lblapoyo.Text, "@")
            For cellCtra = 1 To cellCnta
                Dim tCella As New TableCell()
                'tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr

                tCella.Text = funciones.entry(cellCtra, cada, "|")
                If cellCtra = 1 Then
                    tCella.Width = 80
                End If
                ' Add new TableCell object to row.
                tRowa.Cells.Add(tCella)
            Next
            ' Add new row to table.
            If vapoyo.Text <> 0 Then
                Table3.Rows.Add(tRowa)
            Else

            End If
        Next

        Dim rowCnto As Integer  ' Current row count
        Dim rowCtro As Integer  ' Total number of cells (columns).
        Dim cellCtro As Integer ' Current cell counter.
        Dim cellCnto As Integer
        Dim cado As String

        rowCnto = funciones.num_elementos(lblobservacion.Text, "@") - 1
        cellCnto = 2
        For rowCtro = 1 To rowCnto
            Dim tRowo As New TableRow()
            cado = funciones.entry(rowCtro, lblobservacion.Text, "@")
            For cellCtro = 1 To cellCnto
                Dim tCello As New TableCell()
                'tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr

                tCello.Text = funciones.entry(cellCtro, cado, "|")
                If cellCtro = 1 Then
                    tCello.Width = 250
                End If
                ' Add new TableCell object to row.
                tRowo.Cells.Add(tCello)
            Next
            ' Add new row to table.
            'If vobservacion.Text = "" Then

            'Else
            '    Table4.Rows.Add(tRowo)
            'End If


            Table4.Rows.Add(tRowo)
        Next

    End Sub
End Class
