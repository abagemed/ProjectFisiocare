Partial Class cDoctores
    Inherits System.Web.UI.Page
    Dim funciones As New miclases

    Sub exportaexcel(ByVal migrid As DataGrid)
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Page.EnableViewState = False
        migrid.AllowPaging = False
        migrid.AllowSorting = False
        Dim ESCRITORAEXCEL As New System.IO.StringWriter()
        Dim PASADORHTML As New HtmlTextWriter(ESCRITORAEXCEL)
        migrid.RenderControl(PASADORHTML)
        Response.Write(ESCRITORAEXCEL.ToString)
        Response.End()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            cmbdoctores = funciones.llenacombos(cmbdoctores, "select ID,(nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre,paterno,materno")
            lblfechacita.Text = Context.Items("fecha").ToString.Trim
        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        Try
            Dim auxfe1 As Date = txtfecha1.Text.Trim
            Dim auxfe2 As Date = txtfecha2.Text.Trim
            gridComicion = funciones.creadataset("select clientes.elnombre,abonos.idcliente,abonos.idCosto," & _
            "catServicios.descripcion,count(abonos.idcosto) as cuenta,abonos.importe,abonos.iddoctor from abonos " & _
            "inner join clientes on clientes.idcliente=abonos.idcliente inner join catCostos on abonos.idcosto=catcostos.idcosto " & _
            "inner join catServicios on catCostos.id_servicio=catServicios.id_servicio where abonos.iddoctor='" + cmbdoctores.SelectedValue.Trim + "' and " & _
            "abonos.fecha>='" + Left(auxfe1.ToString.Trim, 10) + "' and abonos.fecha<='" + Left(auxfe2.ToString.Trim, 10) + "' " & _
            "group by clientes.elnombre,abonos.idcliente,catServicios.descripcion,abonos.idCosto,importe,abonos.iddoctor", gridComicion)
        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try
        If gridComicion.Items.Count > 0 Then
            cmdExporta.Enabled = True
        Else
            cmdExporta.Enabled = False
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdExporta.Click
        exportaexcel(gridComicion)
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfechacita.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
End Class
