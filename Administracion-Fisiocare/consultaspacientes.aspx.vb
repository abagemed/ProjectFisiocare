Imports System.Data
Imports System.Data.SqlClient

Partial Class consultaspacientes

    Inherits System.Web.UI.Page
    Dim funciones As New miclases

    Sub llenardatos()
        hfSucursal.Value = cmbSucursal.SelectedValue
        hfclientes.Value = cmbdoctorc.SelectedValue
        If cmbdoctorc.SelectedIndex = 0 Then
            Dim sql As String


            sql = " SELECT clientes.idCliente,COUNT(CASE  WHEN catServicios.id_servicio='TB' then catServicios.id_servicio end) as 'TB', " & _
" COUNT(CASE  WHEN catServicios.id_servicio='CM' then catServicios.id_servicio end) as 'CM',COUNT(CASE  WHEN catServicios.id_servicio='TD' then catServicios.id_servicio end) as 'TD',COUNT(CASE  WHEN catServicios.id_servicio='TH' then catServicios.id_servicio end) as 'TH',COUNT(CASE  WHEN catServicios.id_servicio='HT' then catServicios.id_servicio end) as 'HT',elnombre, convert(varchar(10),dtinicio,103)as finicio, " & _
            " isnull('DR. '+catDoctores.nombre+' '+catDoctores.paterno+' '+catDoctores.materno,'SIN REMITENTE DESCONOCIDO') as doctorr,catResponsables.descripcion as CCostos, " & _
            " (case habCortesia  when 'true' then 'SI' else (case habCortesia  when 'false' then 'NO' else 'SI' end) end) as habCortesia, sexo, edad, telefono, celular,email " & _
            " FROM clientes " & _
            " full join catDoctores on catDoctores.ID = clientes.idDoctor " & _
            " full join catResponsables on catResponsables.idresponsable = clientes.idCenCostos " & _
            " full JOIN abonos ON abonos.idCliente = clientes.idCliente " & _
" full JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
" full JOIN  catServicios ON  catServicios.id_servicio = catCostos.id_servicio " & _
            " WHERE dtinicio >= '" + txtfecha1.Text + " 00:00:00'  AND dtinicio <= '" + txtfecha2.Text.Trim + " 12:00:00' group by clientes.idcliente,elnombre,dtinicio, " & _
" catDoctores.nombre,catDoctores.paterno, catDoctores.materno,catResponsables.descripcion,clientes.habCortesia,clientes.sexo,clientes.edad,clientes.telefono, " & _
" clientes.celular,clientes.email order by finicio"
            gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)

            'sql = "SELECT agenda.idCliente as idcliente,clientes.elnombre as cliente,agenda.idHorario as idhorario," & _
            '" horarios.horario as horario, convert(varchar(10),fecha,103) as fecha, posicion, terapistas.Nombre as terapista, " & _
            '" agenda.estado as estado,agenda.idlatencion as idlatencion,isnull(catLAtencion.atdescripcion,'NO SE APLICO') as latencion " & _
            '" FROM agenda " & _
            '" full join clientes on clientes.idcliente = agenda.idcliente " & _
            '" full join Terapistas on Terapistas.columna = agenda.posicion " & _
            '" full join catLAtencion on catLAtencion.idlatencion = agenda.idlatencion" & _
            '" full join horarios on horarios.idhorario = agenda.idHorario" & _
            '" where fecha>='" + txtfecha1.Text + " 00:00:00' and fecha<='" + txtfecha2.Text.Trim + " 12:00:00'" & _
            '" order by fecha"
            'gDatosCM = funciones.LLenaGrid(sql, gDatosCM, hfSucursal.Value)

        Else

            Dim sql2 As String

            sql2 = " SELECT clientes.idCliente,COUNT(CASE  WHEN catServicios.id_servicio='TB' then catServicios.id_servicio end) as 'TB', " & _
                       " COUNT(CASE  WHEN catServicios.id_servicio='CM' then catServicios.id_servicio end) as 'CM',COUNT(CASE  WHEN catServicios.id_servicio='TD' then catServicios.id_servicio end) as 'TD',COUNT(CASE  WHEN catServicios.id_servicio='TH' then catServicios.id_servicio end) as 'TH',COUNT(CASE  WHEN catServicios.id_servicio='HT' then catServicios.id_servicio end) as 'HT',elnombre, convert(varchar(10),dtinicio,103)as finicio, " & _
            " isnull('DR. '+catDoctores.nombre+' '+catDoctores.paterno+' '+catDoctores.materno,'SIN REMITENTE DESCONOCIDO') as doctorr,catResponsables.descripcion as CCostos, " & _
            " (case habCortesia  when 'true' then 'SI' else (case habCortesia  when 'false' then 'NO' else 'SI' end) end) as habCortesia, sexo, edad, telefono, celular,email " & _
            " FROM clientes " & _
                        " full join catDoctores on catDoctores.ID = clientes.idDoctor " & _
                        " full join catResponsables on catResponsables.idresponsable = clientes.idCenCostos " & _
                        " full JOIN abonos ON abonos.idCliente = clientes.idCliente " & _
" full JOIN catCostos ON abonos.idCosto = catCostos.idCosto " & _
" full JOIN  catServicios ON  catServicios.id_servicio = catCostos.id_servicio " & _
                        " WHERE 'DR. '+catDoctores.nombre+' '+catDoctores.paterno+' '+catDoctores.materno = '" + cmbdoctorc.SelectedItem.Text + "' and dtinicio >= '" + txtfecha1.Text + " 00:00:00'  AND dtinicio <= '" + txtfecha2.Text.Trim + " 12:00:00' group by clientes.idcliente,elnombre,dtinicio, " & _
" catDoctores.nombre,catDoctores.paterno, catDoctores.materno,catResponsables.descripcion,clientes.habCortesia,clientes.sexo,clientes.edad,clientes.telefono, " & _
" clientes.celular,clientes.email order by finicio"
            gDatosCM = funciones.LLenaGrid(sql2, gDatosCM, hfSucursal.Value)

            ' sql2 = "SELECT agenda.idCliente as idcliente,clientes.elnombre as cliente,agenda.idHorario as idhorario," & _
            '" horarios.horario as horario, convert(varchar(10),fecha,103) as fecha, posicion, terapistas.Nombre as terapista, " & _
            '" agenda.estado as estado,agenda.idlatencion as idlatencion,catLAtencion.atdescripcion as latencion " & _
            '" FROM agenda " & _
            '" full join clientes on clientes.idcliente = agenda.idcliente " & _
            '" full join Terapistas on Terapistas.columna = agenda.posicion " & _
            '" full join catLAtencion on catLAtencion.idlatencion = agenda.idlatencion" & _
            '" full join horarios on horarios.idhorario = agenda.idHorario" & _
            '" where terapistas.Nombre = '" + cmbdoctorc.SelectedItem.Text + "' and fecha>='" + txtfecha1.Text + " 00:00:00' and fecha<='" + txtfecha2.Text.Trim + " 12:00:00'" & _
            '" order by fecha"
            ' gDatosCM = funciones.LLenaGrid(sql2, gDatosCM, hfSucursal.Value)

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

            cmbdoctorc = funciones.llenacombos(cmbdoctorc, "select ID,('DR. '+nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre", cmbSucursal.SelectedValue)

            llenardatos()
        Else

            cmbdoctorc.Items.Clear()
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
