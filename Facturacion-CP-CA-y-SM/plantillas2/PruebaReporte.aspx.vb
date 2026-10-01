Public Class PruebaReporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
            '    Response.Redirect("login.aspx")
            'End If

            cargaNomReportes()
            HdIngresos.Value = "0"
        End If
       
    End Sub

    Sub cargaNomReportes()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT codigoSQL,descripcion " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatReportesDet]"
        DdConsulta.Items.Add("Seleccionar")
        If funciones.llenadropdown(strSQL, DdConsulta) = 0 Then

        End If
    End Sub
    

    Private Sub BtnAceptar_Click(sender As Object, e As EventArgs) Handles BtnAceptar.Click
        Dim clsDatos As New ClaseDatos
        Dim strSql, strSql2 As String
        Dim dt As New DataTable
        Dim rangoFechas
        strSql = ""
        strSql2 = ""
        rangoFechas = Split(Fechas.Text, " - ")

        Select Case DdConsulta.SelectedValue
            Case 1
                'Nada
            Case 10
      


                strSql = "SELECT idcita as RECIBO,clientes.elnombre as PACIENTE,catServicios.descripcion AS TERAPIA, convert(varchar(10),abonos.fecha,103) as FECHA,  " & _
"abonos.abono AS ABONO, abonos.importe-monto as IMPORTE, " & _
"(CASE coaseguro WHEN 0 THEN 'NO' ELSE 'SI' END) AS COASEGURO FROM abonos " & _
"INNER JOIN clientes ON abonos.idCliente = clientes.idCliente " & _
"INNER JOIN catCostos ON abonos.idCosto = catCostos.idCosto INNER JOIN  catServicios ON " & _
"catServicios.id_servicio = catCostos.id_servicio where abonos.fecha>='" & rangoFechas(0) & "' and abonos.fecha<='" & rangoFechas(1) & "' AND facturado='false' and importe<>0 and datepart(yyyy,abonos.fecha)>2012 order by fecha asc"

             
        End Select
        If strSql <> "" Then
            If clsDatos.cargatabla(strSql, dt) = 0 Then
                GdReporte.DataSource = dt
                GdReporte.DataBind()
            End If
        End If



    End Sub
End Class