
Partial Class terapistas
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub cmbsucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbsucursal.SelectedIndexChanged
        cmbTerapistas.Items.Clear()
        If cmbsucursal.SelectedValue <> "0" Then
            cmbTerapistas = funciones.llenacombos(cmbTerapistas, "select id,nombre from terapistas ", cmbsucursal.SelectedValue)
            Panel2.Visible = False
            btnnuevo.Visible = True
        Else
            btnnuevo.Visible = False
            Panel2.Visible = False
        End If
    End Sub

    Protected Sub btnnuevo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevo.Click
        tabla.Visible = False
        Panel1.Visible = True
        lblsuc.Text = "SUCURSAL " + cmbsucursal.Items(cmbsucursal.SelectedIndex).Text
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        funciones.grabaDatos("insert into terapistas(nombre,pass) values('" + txtNombre.Text.Trim + "','" + txtPassN.Text.Trim + "')", cmbsucursal.SelectedValue.Trim)
        txtnombre.Text = ""
        Messagebox1.ShowMessage("SE AGREGO CORRECTAMENTE")
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        cmbTerapistas = funciones.llenacombos(cmbTerapistas, "select id,nombre from terapistas ", cmbsucursal.SelectedValue)
        btnnuevo.Visible = True
        tabla.Visible = True
        Panel1.Visible = False
        Panel2.Visible = False
    End Sub

    Protected Sub cmbTerapistas_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbTerapistas.SelectedIndexChanged
        If cmbTerapistas.SelectedValue <> "0" Then
            Panel2.Visible = True

            txtNombreT.Text = cmbTerapistas.Items(cmbTerapistas.SelectedIndex).Text
            idTerapista.Value = cmbTerapistas.SelectedValue

            cmbActivo.SelectedValue = funciones.leerValor("select activo from terapistas where id='" + idTerapista.Value + "'", cmbsucursal.SelectedValue)
            sucTerapista.Value = cmbsucursal.SelectedValue
        Else
            Panel2.Visible = False
        End If
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        Dim actPass As String = ""
        If txtpass.Text.Trim <> "" Then
            actPass = " ,pass='" + txtpass.Text.Trim + "'"
        End If
        funciones.grabaDatos("update terapistas set activo='" + cmbActivo.SelectedValue + "'" + actPass + " where id='" + cmbTerapistas.SelectedValue + "'", sucTerapista.Value)
        Messagebox1.ShowMessage("Se actualizo correctamente...")
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles Menu1.MenuItemClick
        Dim Valor As String = e.Item.Value.ToString.Trim
        Select Case Valor
            Case "0"
                Server.Transfer("Defadmon.aspx", True)
            Case "1"
                Server.Transfer("centroCostos.aspx", False)
            Case "2"
                Server.Transfer("admUsuarios.aspx", False)
            Case "3"
                Server.Transfer("camaras.aspx", False)
            Case "4"
                Server.Transfer("terapistas.aspx", False)
            Case "5"
                Server.Transfer("admRecibos.aspx", False)
            Case Else
                Server.Transfer(Valor, False)
        End Select
    End Sub
End Class
