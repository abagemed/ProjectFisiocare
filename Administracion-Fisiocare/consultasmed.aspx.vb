Imports System.Data
Imports System.Data.SqlClient

Partial Class consultasmed

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbdoctorc.SelectedValue
        Dim idDoctor As Integer
        Dim sql As String
        Dim fechaIni As String = "'" + txtfecha1.Text.Trim + " 00:00:00'"
        Dim fechaFin As String = "'" + txtfecha2.Text.Trim + " 12:00:00'"
        sql = "EXEC [FISIOCARE_repConsultasMedicas]  " + fechaIni + ", " + fechaFin

        If cmbdoctorc.SelectedIndex <> 0 Then
            idDoctor = cmbdoctorc.SelectedItem.Value
            sql += ", " + idDoctor.ToString
        End If

        gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)

        cmdexcelcm.Visible = True

        gDatosCM.Visible = True
        habilitaVerfactura()
    End Sub
   

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)
        End If


    End Sub
    Protected Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        'Panel1.Visible = False
        If cmbSucursal.SelectedValue <> "0" Then

            cmbdoctorc = funciones.llenacombos(cmbdoctorc, "select iddoctorc,(nombre+' '+apaterno+' '+amaterno) as elnombre from catDoctorC order by nombre,apaterno,amaterno", cmbSucursal.SelectedValue)

        Else

            cmbdoctorc.Items.Clear()
        End If
        gDatosCM.Visible = False
    End Sub
    Protected Sub cmbdoctorc_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbdoctorc.SelectedIndexChanged
        gDatosCM.Visible = False
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
    Protected Sub gDatosCM_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gDatosCM.ItemCommand
        Try
            Dim Nombre As String = "c:\reporteFisio\FisioSM\fsm_" + gDatosCM.Items(e.Item.ItemIndex).Cells(10).Text.Trim + ".pdf"
            Response.Clear()
            Response.ContentType = "application/pdf"
            Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
            Response.WriteFile(Nombre)
            Response.Flush()
            Response.Close()
        Catch
            Messagebox1.ShowMessage("NO SE ENCONTRÓ EL ARCHIVO DE LA FACTURA ...")
        End Try
    End Sub
    Sub habilitaVerfactura()
        Dim cuenta As Integer = 0
        Do While cuenta < gDatosCM.Items.Count
            If gDatosCM.Items(cuenta).Cells(11).Text.Trim = "&nbsp;" Then
                CType(gDatosCM.Items(cuenta).Cells(16).Controls(1), LinkButton).Visible = False
            End If
            cuenta = cuenta + 1
        Loop
        If cuenta <> 0 Then
            ' Panel1.Visible = True
        Else
            ' Panel1.Visible = False
        End If
    End Sub
End Class
