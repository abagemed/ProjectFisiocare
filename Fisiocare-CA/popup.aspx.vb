
Partial Class popup
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim funciones As New miclases

        lblcita.Text = Request.QueryString("idcita").ToString.Trim
        lblcliente.Text = Request.QueryString("cliente").ToString.Trim
        lblfecha.Text = Request.QueryString("fecha").ToString.Trim
        lblabono.Text = Request.QueryString("abono").ToString.Trim
        lblimporte.Text = Request.QueryString("importe").ToString.Trim
        lbldetalle.Text = Request.QueryString("conceptos").ToString.Trim

        Dim popupScript As String = " window.print(); "
        Page.ClientScript.RegisterClientScriptBlock(Me.GetType(), "PopupScript", popupScript, True)

        Dim rowCnt As Integer  ' Current row count
        Dim rowCtr As Integer  ' Total number of cells (columns).
        Dim cellCtr As Integer ' Current cell counter.
        Dim cellCnt As Integer
        Dim cad As String

        rowCnt = funciones.num_elementos(lbldetalle.Text, "@") - 1
        cellCnt = 2
        For rowCtr = 1 To rowCnt
            Dim tRow As New TableRow()
            cad = funciones.entry(rowCtr, lbldetalle.Text, "@")
            For cellCtr = 1 To cellCnt
                Dim tCell As New TableCell()
                'tCell.Text = "Row " & rowCtr & ", Cell " & cellCtr
                tCell.Text = funciones.entry(cellCtr, cad, ",")
                If cellCtr = 1 Then
                    tCell.Width = 260
                End If
                ' Add new TableCell object to row.
                tRow.Cells.Add(tCell)
            Next
            ' Add new row to table.
            Table1.Rows.Add(tRow)
        Next

    End Sub
End Class
