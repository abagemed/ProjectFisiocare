Public Class PruebaReporte
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
                Response.Redirect("login.aspx")
            End If

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
            Case 2
                strSql = "Select C.descripcionConsultorio as Doctor, " & _
                "COUNT(CASE  WHEN A.CodigoEtapa='6' then A.CodigoEtapa end) as 'CONSULTAS_REALIZADAS' " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on A.codigoConsultorio=C.codigoConsultorio " & _
                " WHERE fechaAgenda>='" & rangoFechas(0) & "' and fechaAgenda<='" & rangoFechas(1) & "' " & _
                " GROUP BY C.descripcionConsultorio"

                strSql2 = "SELECT C.descripcionConsultorio as Medico,sum(I.importePago) as Importe " & _
                          " FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as I " & _
                          "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=I.codigoConsultorio and C.codigoEmpresa=1 " & _
                          " WHERE fechaIngreso>='" & rangoFechas(0) & "' and fechaIngreso<='" & rangoFechas(1) & "' " & _
                          "group by C.descripcionConsultorio"
            Case 3
                strSql = "SELECT I.FolioConsulta AS 'Folio Consulta', (U.nombre+' '+U.primerApellido+' '+U.segundoApellido) as Cajero,I.fechaIngreso AS 'Fecha', ( '$ ' + format(convert(decimal(8,2),I.importePago),'N','en-us')  ) AS 'Importe',I.Referencia AS 'Referencia',FP.descripcion AS 'Descripción', " & _
                    " (P.nombres+' '+P.pApellido+' '+P.sApellido) as Paciente,C.descripcionConsultorio AS 'Doctor',(CASE I.conciliado WHEN 0 THEN 'No' ELSE 'Si' END) AS 'Conciliado',(CASE pagoRealizado When 0 then 'Sin Pagar' ELSE 'Pagado' end) AS 'Estado' " & _
                    "FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as I " & _
                    "inner join [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A on A.folioConsulta=I.folioConsulta and CodigoEtapa in (6,3) " & _
                    "INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as P on P.codigoPaciente=A.codigoPaciente " & _
                    "INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on I.codigoUsuario=U.codigoUsuario  " & _
                    "INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as FP on FP.codigoFormaPago=I.codigoFormaPago  " & _
                    "INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=I.codigoConsultorio " & _
                    "WHERE fechaingreso>='" & rangoFechas(0) & "' and fechaingreso<='" & rangoFechas(1) & "' "
            Case 4
                strSql = "SELECT (P.papellido+' '+P.sapellido+' '+P.nombres) AS Paciente,(CASE P.genero WHEN 'M' THEN 'Femenino' ELSE 'Masculino' END) as Genero,CDD.descripcion as Diagnostico,D.FechaActualizacion as Fecha, CC.descripcionConsultorio as Consultorio" & _
                    " FROM [" & clsDatos.BaseDatos & "].[dbo].[HmDiagnosticosDet] AS D" & _
                    " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatDiagnosticosDet] as CDD " & _
                    " ON CDD.codigoDiagnostico=D.codigoDiagnostico  and CDD.codigoListaDiagnostico=D.codigoListaDiagnostico and CDD.codigoEmpresa=1 and CDD.status=1" & _
                    " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] AS A" & _
                    " ON D.folioConsulta=A.folioConsulta " & _
                    " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] AS P" & _
                    " ON P.codigoPaciente=A.CodigoPaciente" & _
                    " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] AS CC" & _
                    " ON CC.codigoConsultorio=A.CodigoConsultorio" & _
                    " WHERE D.fechaActualizacion>='" & rangoFechas(0) & "' AND D.fechaActualizacion<='" & rangoFechas(1) & "' " & _
                    " ORDER BY D.fechaActualizacion Desc"
            Case 5
                strSql = "SELECT (nombre+' '+primerApellido+' '+segundoApellido) as Nombre, count(*) as 'Número de consultas' FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                        " INNER JOIN [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U " & _
                        " ON u.codigoUsuario=CodigoUsuarioAtiende WHERE codigoconsultorio = 11 AND codigoEtapa = 6" & _
                        " AND fechaAgenda >= '" & rangoFechas(0) & "' AND fechaAgenda <= '" & rangoFechas(1) & "'" & _
                        " GROUP BY (nombre+' '+primerApellido+' '+segundoApellido)"
            Case 7
                strSql = "select C.folioIngreso,I.referencia,FP.descripcion,I.fechaOperacion,C.folioFactura,F.fechaFactura,'fechaAbono',F.rfc,F.total as Total,C.importe as Abono, (F.total-C.importe) as Saldo " & _
                        "FROM [" & clsDatos.BaseDatos & "].[dbo].[ConciliacionBancaria] as C " & _
                        "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco] as I on I.folioIngreso=C.folioIngreso " & _
                        "inner join  [" & clsDatos.BaseDatos & "].[dbo].[detFacturas] as F on F.idFactura=C.idFactura " & _
                        "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as FP on FP.codigoFormaPago=F.tipoDePago " & _
                        "where fechaFactura>='" & rangoFechas(0) & "'  and fechaFactura<='" & rangoFechas(1) & "' " & _
                        "order by fechaFactura desc"
            Case 8
                strSql = "select C.folioIngreso,I.referencia,FP.descripcion,I.fechaOperacion,C.folioFactura,F.fechaFactura,'fechaAbono',F.rfc,F.total as Total,C.importe as Abono, (F.total-C.importe) as Saldo " & _
                        "FROM [" & clsDatos.BaseDatos & "].[dbo].[ConciliacionBancaria] as C " & _
                        "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosBanco] as I on I.folioIngreso=C.folioIngreso " & _
                        "inner join  [" & clsDatos.BaseDatos & "].[dbo].[detFacturas] as F on F.idFactura=C.idFactura " & _
                        "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as FP on FP.codigoFormaPago=F.tipoDePago " & _
                        "where fechaOperacion>='" & rangoFechas(0) & "'  and fechaOperacion<='" & rangoFechas(1) & "' " & _
                        "order by fechaOperacion desc"
            Case 9
                strSql = "SELECT  F.folioConsulta,F.fechaFactura,C.descripcionConsultorio as Médico,(papellido+' '+sapellido+' '+nombres) as Paciente,DF.razonSocial as Cliente,F.folioFactura as Folio_Factura,F.Serie,F.Total, " & _
                        "sum(case WHEN CP.servicio='CONS' THEN CP.costo ELSE 0 END) as Honorarios, " & _
                        "sum(case WHEN CP.servicio<>'CONS' THEN CP.costo ELSE 0 END) as Insumos, " & _
                        "(case WHEN F.saldo=0 THEN F.total ELSE F.saldo END) as Cobrado " & _
                        "FROM [" & clsDatos.BaseDatos & "].[dbo].[DetFacturas] as F " & _
                        "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as P on F.codigoPaciente=P.codigoPAciente and P.CodigoEmpresa=1 " & _
                        "Inner join [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] as DF on DF.idRazonSocial=F.idRazonSocial " & _
                        "left join [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A on A.folioConsulta=left(f.folioConsulta,10) and A.CodigoEmpresa=1 " & _
                        "left join  [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio and C.CodigoEmpresa=1 " & _
                        "left join [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] as CP on CP.folioConsulta=F.folioConsulta and CP.CodigoEmpresa=1 and CP.status=1 " & _
                         "where fechaFactura>='" & rangoFechas(0) & "'  and fechaFactura<='" & rangoFechas(1) & "' " & _
                        "group by F.folioConsulta,F.fechaFactura,C.descripcionConsultorio,(papellido+' '+sapellido+' '+nombres),DF.razonSocial,F.folioFactura,F.serie,F.total,f.saldo " & _
                        "order by folioFactura"

            Case 10
                strSql = "SELECT " & _
                            "A.FolioConsulta as Consulta,(CASE WHEN f.folioConsulta is null THEN 'SIN FACTURAR' ELSE f.folioconsulta end) as ConsultasFac," & _
                            "C.descripcionConsultorio as Consultorio,(PApellido+' '+SApellido+' '+Nombres) as Paciente,FORMAT(A.FechaAgenda, 'dd/MMM/yyyy', 'en-us') as FechaAgenda, I.importePago," & _
                            "sum(case WHEN PC.servicio='CONS' THEN PC.costo ELSE 0 END) as Honorarios," & _
                            "sum(case WHEN PC.servicio<>'CONS' THEN PC.costo ELSE 0 END) as Insumos, I.Abono," & _
                            "(CASE WHEN I.facturado=1 THEN 'Sí' ELSE 'No' END) as Facturado," & _
                            "(CASE WHEN F.folioFactura is null THEN '' ELSE F.serie+' '+F.folioFactura END) as FolioFac," & _
                            "(CASE WHEN F.fechaFactura is null THEN '' else FORMAT(F.FechaFactura, 'dd/MMM/yyyy', 'en-us')  END) as FechaFac," & _
                            "(CASE WHEN F.rfc is null THEN '' ELSE F.rfc END) as RFC," & _
                            "(CASE WHEN F.estado is null THEN '' ELSE F.estado END) as Estado," & _
                            "(CASE WHEN P.descripcion is null THEN p2.descripcion ELSE P.descripcion END) as FormaPago," & _
                            "(CASE WHEN f.total is null THEN '0.0' ELSE f.total END) as total_facturado " & _
                            "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] as PC on A.folioConsulta=PC.folioConsulta and PC.status=1  " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C on A.codigoConsultorio=C.codigoConsultorio " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[ingresosCaja] as I on A.folioConsulta=I.folioConsulta and PagoRealizado=1 and importePago<>0 " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as CP on CP.codigoPaciente=A.codigoPaciente " & _
                            "left join  [" & clsDatos.BaseDatos & "].[dbo].[detFacturas] as F on I.folioConsulta in (select * from AgemedOrtopediaPruebas.dbo.splitstring(F.folioConsulta)) " & _
                            "left join  [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as P on f.tipoDePago=P.codigoFormaPago " & _
                            "left join   [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as P2 on I.codigoFormaPago=P2.codigoFormaPago " & _
                             "where A.fechaAgenda>='" & rangoFechas(0) & "'  and A.fechaAgenda<='" & rangoFechas(1) & "' " & _
                            "group by A.folioConsulta,F.folioConsulta,C.descripcionConsultorio,(papellido+' '+sapellido+' '+nombres),A.fechaAgenda,I.importePago,I.Abono,I.facturado,F.serie,F.folioFactura, " & _
                             "F.fechaFactura, F.rfc, F.estado, P.descripcion, f.Total, P2.descripcion " & _
                            "order by A.fechaAgenda desc"
            Case 11
                strSql = "select (F.serie+F.folioFactura) as FolioFactura,F.RFC,C.RazonSocial,F.Total as Importe,F.Estado,FP.descripcion as TipoDePago," & _
                            "FechaFactura,folioConsulta as foliosCargosManualesClientes,(U.nombre +' '+primerApellido) as Empleado,CON.descripcionConsultorio as Medico " & _
                            "from   [" & clsDatos.BaseDatos & "].[dbo].[detFacturas]  as F " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as FP on FP.codigoFormaPago=F.tipoDePago " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatClientes] as C on C.codigoCliente=f.idRazonSocial " & _
                            "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on U.codigoUsuario=f.idempleado " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CargosManualesClientes] as CMC on CMC.folioCargo in (select * from AgemedOrtopediaPruebas.dbo.splitstring(F.folioConsulta)) " & _
                            "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as CON on CON.codigoConsultorio=CMC.codigoConsultorio " & _
                            "where fechaFactura>='" & rangoFechas(0) & "'  and fechaFactura<='" & rangoFechas(1) & "' and folioConsulta not like '%C%' " & _
                            "group by F.serie+F.folioFactura,F.rfc,C.RazonSocial,F.total,F.codigoPaciente,F.estado,FP.descripcion,F.conciliado,FechaFactura,F.folioConsulta,(U.nombre +' '+primerApellido),CON.descripcionConsultorio"

        End Select
        If strSql <> "" Then
            If clsDatos.cargatabla(strSql, dt) = 0 Then
                GdReporte.DataSource = dt
                GdReporte.DataBind()
            End If
        End If

        If strSql2 <> "" Then
            Dim i As Integer = 0
            If clsDatos.cargatabla(strSql2, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    Dim datos As String
                    datos = "[['Médico','Ingreso']"
                    i = i + 1
                    For Each fila As DataRow In dt.Rows
                        datos += ",['" + fila.Item("Medico").ToString + "'," + fila.Item("Importe").ToString + "]"
                        i = i + 1
                    Next
                    HdIngresos.Value = datos + "]"
                End If
            End If
        End If


    End Sub

    Private Sub guardarConComillas()
        Dim clsDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable

    End Sub
End Class