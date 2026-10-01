Imports System.Data
Imports System.Data.SqlClient

Partial Class consultascrecibos

    Inherits System.Web.UI.Page
    Dim funciones As New miclases

    Sub llenardatos()
        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbdoctorc.SelectedValue
        If cmbdoctorc.SelectedIndex = 0 Then
            Dim sql As String



            sql = " SELECT idabono as idabono,idcita as idcita,clientes.elnombre as elnombre,catServicios.descripcion as servicio," & _
 "convert(varchar(10),fechaabono,103) as fechaabono," & _
"catResponsables.descripcion as responsable,  " & _
 "AbonosCancelados.abono as abono, AbonosCancelados.importe as importe,CatFormasDePago.descripcion as descripcionpago,motivocancelacion as motivocancelacion," & _
  "convert(varchar(10),fechaactualizacion,103) as fechaactualizacion  FROM AbonosCancelados " & _
 "INNER JOIN clientes ON AbonosCancelados.idCliente = clientes.idCliente  " & _
 "INNER JOIN catCostos ON AbonosCancelados.idCosto = catCostos.idCosto  " & _
 "INNER JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio  " & _
" INNER JOIN  catResponsables ON catResponsables.idresponsable = catCostos.iddatosfac " & _
 "INNER JOIN CatFormasDePago ON AbonosCancelados.idpago = CatFormasDePago.descripcioncorta " & _
 "where AbonosCancelados.fechaabono>='" + txtfecha1.Text + " 00:00:00' and AbonosCancelados.fechaabono<='" + txtfecha2.Text.Trim + " 12:00:00' order by fechaabono desc "
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)

        Else

            Dim sql2 As String

            sql2 = " SELECT idabono as idabono,idcita as idcita,clientes.elnombre as elnombre,catServicios.descripcion as servicio," & _
 "convert(varchar(10),fechaabono,103) as fechaabono," & _
"catResponsables.descripcion as responsable,  " & _
 "AbonosCancelados.abono as abono, AbonosCancelados.importe as importe,CatFormasDePago.descripcion as descripcionpago,motivocancelacion as motivocancelacion," & _
  "convert(varchar(10),fechaactualizacion,103) as fechaactualizacion  FROM AbonosCancelados " & _
 "INNER JOIN clientes ON AbonosCancelados.idCliente = clientes.idCliente  " & _
 "INNER JOIN catCostos ON AbonosCancelados.idCosto = catCostos.idCosto  " & _
 "INNER JOIN  catServicios ON catServicios.id_servicio = catCostos.id_servicio  " & _
" INNER JOIN  catResponsables ON catResponsables.idresponsable = catCostos.iddatosfac " & _
 "INNER JOIN CatFormasDePago ON AbonosCancelados.idpago = CatFormasDePago.descripcioncorta " & _
 "where AbonosCancelados.fechaabono>='" + txtfecha1.Text + " 00:00:00' and AbonosCancelados.fechaabono<='" + txtfecha2.Text.Trim + " 12:00:00' order by fechaabono desc "
            gDatosCM = funciones.LLenaGrid(sql2, gDatosCM, hfSucursal.Value)



        End If
        cmdexcelcm.Visible = True

        gDatosCM.Visible = True

    End Sub
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        llenardatos()
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
        End If


    End Sub
    Protected Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged


        If cmbSucursal.SelectedValue <> "0" Then

            'cmbdoctorc = funciones.llenacombos(cmbdoctorc, "select ID,('DR. '+nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre", cmbSucursal.SelectedValue)

            llenardatos()
        Else

            'cmbdoctorc.Items.Clear()
            gDatosCM.Visible = False
        End If

    End Sub
    Protected Sub cmbdoctorc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbdoctorc.SelectedIndexChanged
        llenardatos()
    End Sub
    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gDatosCM.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(gDatosCM)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=General" & cmbSucursal.SelectedValue & ".xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()
    End Sub
    Protected Sub cmdexcelcm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcelcm.Click
        ExportarAExcel()
    End Sub

End Class
