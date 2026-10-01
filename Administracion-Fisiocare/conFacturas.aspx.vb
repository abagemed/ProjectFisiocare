Partial Class conFacturas
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        hfSucursal.Value = cmbSucursal.SelectedValue
        If txtnumfactura.Text.Trim = "" Then
            gDatos = funciones.LLenaGrid("select num_factura as num_factura,factura.serie as serie,convert(varchar(10),factura.fecha,103) as fecha," & _
            " nombre,(case cancelada  when 'true' then 'CANCELADA' else (case pagada when 'false' then 'ADEUDA' else 'PAGADA' end) end) as status," & _
            " (case cancelada when 'true' then 'false' else (case pagada when 'false' then 'true' else 'false' end) end) as pagada, " & _
            " isnull((select top 1 convert(varchar(10),fecha,103)  from admfisio.dbo.facturas f1 where  f1.numFactura  = factura.num_factura and f1.serie = factura.serie " & _
            " order by fecha desc),'SIN_MOV') as FechaPago,sum(factura.importe) as importe, cancelada from factura " & _
            " where factura.fecha>='" + txtfecha1.Text + " 12:00:00' and factura.fecha<='" + txtfecha2.Text.Trim + " 12:00:00' " & _
            " group by num_factura,cancelada,factura.serie,factura.fecha,nombre,pagada", gDatos, hfSucursal.Value)
        Else
            gDatos = funciones.LLenaGrid("select num_factura as num_factura,factura.serie as serie,convert(varchar(10),factura.fecha,103) as fecha," & _
            " nombre,(case cancelada  when 'true' then 'CANCELADA' else (case pagada when 'false' then 'ADEUDA' else 'PAGADA' end) end) as status," & _
            " (case cancelada when 'true' then 'false' else (case pagada when 'false' then 'true' else 'false' end) end) as pagada, " & _
            " isnull((select top 1 convert(varchar(10),fecha,103)  from admfisio.dbo.facturas f1 where  f1.numFactura  = factura.num_factura and f1.serie = factura.serie COLLATE SQL_Latin1_General_CP1_CI_AS " & _
            " order by fecha desc),'SIN_MOV') as FechaPago,sum(factura.importe) as importe, cancelada from factura " & _
            " where num_factura=" + txtnumfactura.Text.Trim + "  group by num_factura,cancelada,factura.serie,factura.fecha,nombre,pagada", gDatos, hfSucursal.Value)
        End If
        gDatos.Visible = True
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
        End If
        gDatos.Visible = False
    End Sub

    Protected Sub gDatos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gDatos.ItemCommand
        Try
            Dim factura As String = gDatos.Items(e.Item.ItemIndex).Cells(0).Text.Trim
            If e.CommandName <> "sustituye" Then
                Dim filename As String = ""
                Select Case cmbSucursal.SelectedValue
                    Case "fisiocareSM"
                        Dim serie As String = e.Item.ItemIndex.ToString.Trim
                        serie = gDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim
                        Select Case serie
                            Case "C"
                                filename = "C:\reporteFisio\fisioSM\fsm_" + factura + "." + e.CommandName
                                
                            Case "TH"
                                filename = "C:\reporteFisio\fisioSM\fsmTH_" + factura + "." + e.CommandName
                                
                            Case "DS"
                                filename = "C:\reporteFisio\fisioSM\fsmDS_" + factura + "." + e.CommandName
                              
                        End Select
                        'filename = "C:\reporteFisio\fisioSM\fsm_" + factura + "." + e.CommandName
                    Case "fisiocareCP"

                        Dim serie As String = e.Item.ItemIndex.ToString.Trim
                        serie = gDatos.Items(e.Item.ItemIndex).Cells(1).Text.Trim
                        Select Case serie
                            Case "A"
                                filename = "C:\reporteFisio\fisioCP\fcp_" + factura + "." + e.CommandName
                                
                            Case "HT"
                                filename = "C:\reporteFisio\fisioCP\fcpHT_" + factura + "." + e.CommandName
                                
                            Case "DC"
                                filename = "C:\reporteFisio\fisioCP\fcpDC_" + factura + "." + e.CommandName
                               
                        End Select

                        ' filename = "C:\reporteFisio\fisioCP\fcp_" + factura + "." + e.CommandName
                    Case "Gym"
                        filename = "C:\reporteFisio\Fisiogym\" + factura + "." + e.CommandName
                    Case "fisiocareHO"
                        filename = "C:\reporteFisio\FisioHO\fh_" + factura + "." + e.CommandName
                    Case "fisiocareCA"
                        filename = "C:\reporteFisio\FisioCA\fca_" + factura + "." + e.CommandName
                    Case "fisiocarePE"
                        filename = "C:\reporteFisio\FisioPE\fpe_" + factura + "." + e.CommandName
                End Select


                If (Not String.IsNullOrEmpty(filename)) Then

                    Dim toDownload = New System.IO.FileInfo(filename)

                    If (toDownload.Exists) Then

                        Response.Clear()
                        Response.AddHeader("Content-Disposition", "attachment; filename=" + toDownload.Name)
                        Response.AddHeader("Content-Length", toDownload.Length.ToString())
                        Response.ContentType = "application/octet-stream"
                        Response.WriteFile(filename)
                        Response.End()
                    Else
                        Messagebox1.ShowMessage("No se encontró el archivo...")
                    End If
                Else
                    Messagebox1.ShowMessage("No se encontró el archivo...")
                End If
            Else
                lblSustituye.Text = funciones.leerValor("select sustituidoX from factura where num_factura='" + factura + "'", cmbSucursal.SelectedValue)
                ModalPopupExtender1.Show()
            End If
        Catch
            Messagebox1.ShowMessage("No se encontró el archivo...")
        End Try
    End Sub
End Class
