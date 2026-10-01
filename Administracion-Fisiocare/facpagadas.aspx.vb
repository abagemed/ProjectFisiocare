
Partial Class facpagadas
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Sub llenaFacturas()
        'hfSucursal.Value = cmbSucursal.SelectedValue
        'If txtnumfactura.Text.Trim = "" Then
        'gFacturas = funciones.leerValores("select *  from factura", "admfisio")
        'Else
        'gFacturas = funciones.LLenaGrid("select num_factura + '-' + serie as numfactura,convert(varchar(10),fecha,103) as fecha," & _
        '"nombre,(case cancelada  when 'true' then 'CANCELADA' else (case capcuenta when 'false' then 'ADEUDA' else 'PAGADA' end) end) as status," & _
        '"(case cancelada when 'true' then 'false' else (case capCuenta when 'false' then 'true' else 'false' end) end) as capcuenta,sum(importe) as importe,CAST(num_factura AS int) as tempfac from factura " & _
        '"where num_factura=" + txtnumfactura.Text.Trim + "  group by num_factura,cancelada,serie,fecha,nombre,capcuenta order by tempfac", gFacturas, hfSucursal.Value)
        'End If
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        llenaFacturas()
    End Sub

    Protected Sub gFacturas_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gFacturas.ItemCommand
        hfNumfactura.Value = Left(gFacturas.Items(e.Item.ItemIndex).Cells(0).Text, gFacturas.Items(e.Item.ItemIndex).Cells(0).Text.Length - 2)
        lblfac.Text = gFacturas.Items(e.Item.ItemIndex).Cells(0).Text
        lblfecha.Text = gFacturas.Items(e.Item.ItemIndex).Cells(1).Text
        lblnombre.Text = gFacturas.Items(e.Item.ItemIndex).Cells(2).Text
        Dim importe As Double = funciones.leerValor("select sum(importe) as importe from factura  where num_factura='" + hfNumfactura.Value + "' group by idCliente ", hfSucursal.Value)
        Dim abonos As Double = funciones.leerValor("select sum(abono) as pago from abonos  where num_factura='" + hfNumfactura.Value + "' group by idCliente ", hfSucursal.Value)
        Dim resta As Double = importe - abonos

        lblimporte.Text = importe.ToString("C")
        lblabonos.Text = abonos.ToString("C")
        txtFaltante.Text = resta.ToString("C")

        ModalPopupExtender1.Show()
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", Date.Now)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", Date.Now)
            cmbCuentas = funciones.llenacombos(cmbCuentas, "select numCuenta, nomBanco +' - '+ cast(numcuenta as varchar) from catCuentas", "admFisio")
            cmbTipoPago = funciones.llenacombos(cmbTipoPago, "select id,nomPago from catTipodePagos", "admFisio")
        End If
    End Sub
    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        funciones.grabaDatos("update factura set capcuenta='true' where num_factura='" + hfNumfactura.Value + "'", hfSucursal.Value)
        Dim monto As String = Replace(Replace(lblabonos.Text, "$", ""), ",", "")
        Dim fecha As String = String.Format("{0:dd/MM/yyyy}", Date.Now)
        funciones.grabaDatos("insert into facturasPagadas(cuenta,formadePago,fecha,monto) values('" + cmbCuentas.SelectedValue.ToString + "','" + cmbTipoPago.SelectedValue.ToString + "','" + fecha + "','" + monto.ToString + "')", "admFisio")
        cmbCuentas.SelectedValue = 0
        cmbTipoPago.SelectedValue = 0
        llenaFacturas()
    End Sub
End Class
