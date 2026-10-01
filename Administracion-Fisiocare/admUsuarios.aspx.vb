Partial Class admUsuarios
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub DropDownList2_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsucursal.SelectedIndexChanged
        Panel1.Visible = False
        If cmbsucursal.SelectedValue <> "0" Then
            cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, elnombre from clientes order by elnombre", cmbsucursal.SelectedValue)
        Else
            cmbclientes.Items.Clear()
        End If
    End Sub

    Protected Sub cmbclientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbclientes.SelectedIndexChanged
        Dim valores(,) As String = funciones.leerValores("select modFac,habCortesia from clientes where idcliente='" + cmbclientes.SelectedValue + "'", 2, cmbsucursal.SelectedValue)
        cmbModfac.SelectedValue = valores(0, 0).ToString.Trim
        cmbCortesias.SelectedValue = valores(1, 0).ToString.Trim
        Panel1.Visible = True
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        funciones.grabaDatos("update clientes set modfac='" + cmbModfac.SelectedValue + "', habCortesia='" + cmbCortesias.SelectedValue + "' where idcliente='" + cmbclientes.SelectedValue + "'", cmbsucursal.SelectedValue)
        Messagebox1.ShowMessage("Datos Actualizados ...")
    End Sub

End Class
