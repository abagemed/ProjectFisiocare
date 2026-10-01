Imports System.Data.SqlClient
Partial Class auxiliar
    Inherits System.Web.UI.Page
    Dim fun As New miclases
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            lblIdcliente.Text = Request.QueryString("idcliente").ToString.Trim
            Dim funciones As New miclases
            gridTerapias = funciones.creadataset("select idTerapia,substring(diagnostico,1,30) as diagnostico, left(fechaInicio,12) as fechaInicio" & _
            " from terapia where idCliente='" + lblIdcliente.Text.Trim + "' order by fechaInicio", gridTerapias)
            If gridTerapias.Items.Count < 1 Then
                lblmensaje.Text = "NO SE ENCTRARON TERAPIAS PARA ESTE PACIENTE"
                gridTerapias.Visible = False
            Else
                gridTerapias.Visible = True
                lblmensaje.Text = ""
            End If
            informacion.Visible = False
        End If
    End Sub

    Protected Sub gridTerapias_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridTerapias.ItemCommand
        miIdterapia.Value = gridTerapias.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        llenaEjercicios(miIdterapia.Value.Trim)
        informacion.Visible = True
    End Sub
    Sub llenaEjercicios(ByVal lblIdterapia As String)
        txtmedios.Value = ""
        txtejercicios.Value = ""
        txtotros.Value = ""
        Dim conexion As SqlConnection = fun.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select idTerapia,texto,tipo,id from ejercicios where idTerapia='" + lblIdterapia.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        Dim cuenta As Integer = 0
        Dim cuentam As Integer = 0
        Dim cuentae As Integer = 0
        Dim cuentaO As Integer = 0
        gridejercicios = fun.creadataset("select idTerapia,texto,tipo,id from ejercicios " & _
        "where idTerapia='" + lblIdterapia.Trim + "'", gridejercicios)
        Dim aux As Integer = gridejercicios.Items.Count
        Do While leer.Read
            Select Case leer.GetValue(2).ToString.Trim
                Case "M"
                    CType(gridejercicios.Items(cuentam).Cells(0).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentam).Cells(0).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtmedios.Value = leer.GetValue(1).ToString.Trim
                    lblidm.Text = leer.GetValue(3).ToString.Trim
                    cuentam = cuentam + 1
                Case "E"
                    CType(gridejercicios.Items(cuentae).Cells(1).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentae).Cells(1).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtejercicios.Value = leer.GetValue(1).ToString
                    lblide.Text = leer.GetValue(3).ToString.Trim
                    cuentae = cuentae + 1
                Case "O"
                    CType(gridejercicios.Items(cuentaO).Cells(2).Controls(1), LinkButton).Text = Left(leer.GetValue(1).ToString.Trim, 12)
                    CType(gridejercicios.Items(cuentaO).Cells(2).Controls(3), Label).Text = leer.GetValue(3).ToString.Trim
                    txtotros.Value = leer.GetValue(1).ToString
                    lblido.Text = leer.GetValue(3).ToString.Trim
                    cuentaO = cuentaO + 1
            End Select
            gridejercicios.Visible = True
        Loop
        leer.Close()
        conexion.Close()
        If cuentam < cuentae Then
            If cuentae < cuentaO Then
                cuenta = cuentaO
            Else
                cuenta = cuentae
            End If
        Else
            If cuentam < cuentaO Then
                cuenta = cuentaO
            Else
                cuenta = cuentam
            End If
        End If
        Do While cuenta < aux
            gridejercicios.Items(cuenta).Visible = False
            cuenta = cuenta + 1
        Loop
    End Sub

    Protected Sub gridejercicios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridejercicios.ItemCommand
        Select Case e.CommandName
            Case "medios"
                lblidm.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(0).Controls(3), Label).Text
                txtmedios.Value = fun.leerValor("select texto from ejercicios where id='" + lblidm.Text + "'")
            Case "ejercicios"
                lblide.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(1).Controls(3), Label).Text
                txtejercicios.Value = fun.leerValor("select texto from ejercicios where id='" + lblide.Text + "'")
            Case "otros"
                lblido.Text = CType(gridejercicios.Items(e.Item.ItemIndex).Cells(2).Controls(3), Label).Text
                txtotros.Value = fun.leerValor("select texto from ejercicios where id='" + lblido.Text + "'")
        End Select
    End Sub
End Class
