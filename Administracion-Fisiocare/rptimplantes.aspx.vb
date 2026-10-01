Imports System.Data.SqlClient
Imports System.Data
Partial Class rptimplantes
    Inherits System.Web.UI.Page
    Dim clase As New miclases
    Dim funciones As New miclases


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        txtfecha1.Text = Request.QueryString("fecha1").Trim
        txtfecha2.Text = Request.QueryString("fecha2").Trim
        txtNumdias.Text = Request.QueryString("numdias").Trim

        lbltitulo.Text = "REPORTE DEL " + txtfecha1.Text + " AL " + txtfecha2.Text + " / NUMERO DE DIAS PROYECTADOS: " + txtNumdias.Text.ToString.Trim

        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then
            Dim sql, sqlaux, sqltotales As String
            Dim dias As Integer = DateDiff(DateInterval.Day, CDate(txtfecha1.Text), CDate(txtfecha2.Text))
            'Dim i = 0
            Dim fechaaux As String
            fechaaux = txtfecha1.Text
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'C' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'C' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            gridventasALTA = funciones.LLenaGridC(sql, gridventasALTA, hfSucursal.Value)

            Dim dt As New DataTable
            Dim view As New DataView
            Dim dt2 As New DataTable
            view = gridventasALTA.DataSource
            dt2 = view.ToTable
            dt.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2.Rows
                dt.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseALTA.DataSource = dt
            gbaseALTA.DataBind()


            'BSN

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'G' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'G' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'G' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'G' and d.CCANCELADO=0 "
            gridventasCMA = funciones.LLenaGridC(sql, gridventasCMA, hfSucursal.Value)

            Dim dtC As New DataTable
            Dim viewC As New DataView
            Dim dt2C As New DataTable
            viewC = gridventasCMA.DataSource
            dt2C = viewC.ToTable
            dtC.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2C.Rows
                dtC.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseCMA.DataSource = dtC
            gbaseCMA.DataBind()



            '''' palakkad ventas '''


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'PS' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'PS' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'PS' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'PS' and d.CCANCELADO=0 "
            gridventasPALAK = funciones.LLenaGridC(sql, gridventasPALAK, hfSucursal.Value)

            Dim dtPL As New DataTable
            Dim viewPL As New DataView
            Dim dt2PL As New DataTable
            viewPL = gridventasPALAK.DataSource
            dt2PL = viewPL.ToTable
            dtPL.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2PL.Rows
                dtPL.Rows.Add(filas.Item(0).ToString)
            Next
            gbasePALAK.DataSource = dtPL
            gbasePALAK.DataBind()


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'PS' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'PS' and d.CCANCELADO=0 "
            gbasePALAK2 = funciones.LLenaGridC(sql, gbasePALAK2, hfSucursal.Value)


            '''' fin de ventas palakkad '''


            '''' INNOMED ventas '''

             sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'IN' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'IN' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'IN' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'IN' and d.CCANCELADO=0 "
            gridventasINNOMED = funciones.LLenaGridC(sql, gridventasINNOMED, hfSucursal.Value)

            Dim dtIN As New DataTable
            Dim viewIN As New DataView
            Dim dt2IN As New DataTable
            viewIN = gridventasINNOMED.DataSource
            dt2IN = viewIN.ToTable
            dtIN.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2IN.Rows
                dtIN.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseINNOMED.DataSource = dtIN
            gbaseINNOMED.DataBind()


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'IN' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'IN' and d.CCANCELADO=0 "
            gbaseINNOMED2 = funciones.LLenaGridC(sql, gbaseINNOMED2, hfSucursal.Value)

            '''' fin de ventas INNOMED '''



            '''''''''''''''''''''''''''''22112018

            '''' ANTICIPOS ventas '''

           sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'AC' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'AC' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'AC' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'AC' and d.CCANCELADO=0 "
            gridventasanticipos = funciones.LLenaGridC(sql, gridventasanticipos, hfSucursal.Value)

            Dim dtAC As New DataTable
            Dim viewAC As New DataView
            Dim dt2AC As New DataTable
            viewAC = gridventasanticipos.DataSource
            dt2AC = viewAC.ToTable
            dtAC.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2AC.Rows
                dtAC.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseanticipos.DataSource = dtAC
            gbaseanticipos.DataBind()


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'AC' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'AC' and d.CCANCELADO=0 "
            gbaseanticipos2 = funciones.LLenaGridC(sql, gbaseanticipos2, hfSucursal.Value)

            '''' fin de ventas ANTICIPOS '''
            '''''''''''''''''''''''''''''end22112018

            'AB 22032021 AGREGAR NUEVA CONSULTA PARA REPORTE DE VENTAS MEDTRONIC

            'MEDTRONIC ventas

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'ME' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'ME' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'ME' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'ME' and d.CCANCELADO=0 "
            gridventasMEDTRONIC = funciones.LLenaGridC(sql, gridventasMEDTRONIC, hfSucursal.Value)

            Dim dtME As New DataTable
            Dim viewME As New DataView
            Dim dt2ME As New DataTable
            viewME = gridventasMEDTRONIC.DataSource
            dt2ME = viewME.ToTable
            dtME.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2ME.Rows
                dtME.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseMEDTRONIC.DataSource = dtME
            gbaseMEDTRONIC.DataBind()


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'ME' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'ME' and d.CCANCELADO=0 "
            gbaseMEDTRONIC2 = funciones.LLenaGridC(sql, gbaseMEDTRONIC2, hfSucursal.Value)



            'MEDTRONIC ventas fin 
            'FIN 22032021


            'AB23032021
            'TOTALES
            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E') and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)- sum(c1.CDESCUENTO2)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'C' or d1.CSERIEDOCUMENTO = 'G' or d1.CSERIEDOCUMENTO = 'IN' or d1.CSERIEDOCUMENTO = 'PS' or d1.CSERIEDOCUMENTO = 'ME')  and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = vbNewLine & "select 'IMPLANTES-BSN-PALAK-INNOM-MEDTRONIC' as TOTALES,"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and (d.CSERIEDOCUMENTO = 'C' or d.CSERIEDOCUMENTO = 'G' or d.CSERIEDOCUMENTO = 'IN' or d.CSERIEDOCUMENTO = 'PS' or d.CSERIEDOCUMENTO = 'ME') and d.CCANCELADO=0 "
            gridtotales = funciones.LLenaGridC(sql, gridtotales, hfSucursal.Value)

            Dim dtT As New DataTable
            Dim viewT As New DataView
            Dim dt2T As New DataTable
            viewT = gridtotales.DataSource
            dt2T = viewT.ToTable
            dtT.Columns.Add("TOTALES")
            For Each filas As DataRow In dt2T.Rows
                dtT.Rows.Add(filas.Item(0).ToString)
            Next
            gbaseTOTAL.DataSource = dtT
            gbaseTOTAL.DataBind()


            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E') and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E')  and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = vbNewLine & "select 'IMPLANTES-BSN-PALAK-INNOM-MEDTRONIC' as TOTALES,"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and (d.CSERIEDOCUMENTO = 'C' or d.CSERIEDOCUMENTO = 'G' or d.CSERIEDOCUMENTO = 'IN' or d.CSERIEDOCUMENTO = 'PS' or d.CSERIEDOCUMENTO = 'ME') and d.CCANCELADO=0 "
            gbaseTOTAL2 = funciones.LLenaGridC(sql, gbaseTOTAL2, hfSucursal.Value)


            'FIN AB23032021

            'END BSN

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'C' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'C' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'C' and d.CCANCELADO=0 "
            gbaseoi = funciones.LLenaGridC(sql, gbaseoi, hfSucursal.Value)

            'Aqui inicia BSN

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION2 = p.CIDVALORCLASIFICACION2  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION2  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION2  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'G' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION2"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1)- sum(c.CDESCUENTO2))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION2 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'G' and d.CCANCELADO=0 "
            gbaseCMA2 = funciones.LLenaGridC(sql, gbaseCMA2, hfSucursal.Value)



        Else

        End If

        cmdexcelcm.Visible = True
        gridventasALTA.Visible = True
        gridventasCMA.Visible = True
        gridventasPALAK.Visible = True
        gridventasINNOMED.Visible = True
        gridventasanticipos.Visible = True
        lblaltabrisa.Visible = True
        lblcma.Visible = True
        lblpalak.Visible = True
        'AB22032021
        lblmedtronic.Visible = True
        lblinnomed.Visible = True
        lblanticipos.Visible = True
        lbltotales.Visible = True

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = gridventasALTA.Items.Count - 1

        gridventasALTA.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasALTA.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        gridventasALTA.Items(UltimaFilaGA).Font.Bold = True
        gridventasALTA.Items(UltimaFilaGA).Font.Size = 10
        Dim UltimaFilaGC As Integer
        UltimaFilaGC = gridventasCMA.Items.Count - 1

        gridventasCMA.Items(UltimaFilaGC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasCMA.Items(UltimaFilaGC).ForeColor = Drawing.Color.Black
        gridventasCMA.Items(UltimaFilaGC).Font.Bold = True
        gridventasCMA.Items(UltimaFilaGC).Font.Size = 10

        Dim UltimaFilaGT As Integer
        UltimaFilaGT = gridtotales.Items.Count - 1

        gridtotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridtotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        gridtotales.Items(UltimaFilaGT).Font.Bold = True
        gridtotales.Items(UltimaFilaGT).Font.Size = 10


        Dim UltimaFilaGBA As Integer
        UltimaFilaGBA = gbaseALTA.Items.Count - 1

        gbaseALTA.Items(UltimaFilaGBA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseALTA.Items(UltimaFilaGBA).ForeColor = Drawing.Color.Black
        gbaseALTA.Items(UltimaFilaGBA).Font.Bold = True
        gbaseALTA.Items(UltimaFilaGBA).Font.Size = 10
        gbaseALTA.Items(UltimaFilaGBA).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBC As Integer
        UltimaFilaGBC = gbaseCMA.Items.Count - 1

        gbaseCMA.Items(UltimaFilaGBC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseCMA.Items(UltimaFilaGBC).ForeColor = Drawing.Color.Black
        gbaseCMA.Items(UltimaFilaGBC).Font.Bold = True
        gbaseCMA.Items(UltimaFilaGBC).Font.Size = 10
        gbaseCMA.Items(UltimaFilaGBC).HorizontalAlign = HorizontalAlign.Right


        Dim UltimaFilaGBT As Integer
        UltimaFilaGBT = gbaseTOTAL.Items.Count - 1

        gbaseTOTAL.Items(UltimaFilaGBT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseTOTAL.Items(UltimaFilaGBT).ForeColor = Drawing.Color.Black
        gbaseTOTAL.Items(UltimaFilaGBT).Font.Bold = True
        gbaseTOTAL.Items(UltimaFilaGBT).Font.Size = 9
        gbaseTOTAL.Items(UltimaFilaGBT).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBT2 As Integer
        UltimaFilaGBT2 = gbaseTOTAL2.Items.Count - 1

        gbaseTOTAL2.Items(UltimaFilaGBT2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseTOTAL2.Items(UltimaFilaGBT2).ForeColor = Drawing.Color.Black
        gbaseTOTAL2.Items(UltimaFilaGBT2).Font.Bold = True
        gbaseTOTAL2.Items(UltimaFilaGBT2).Font.Size = 10

        Dim UltimaFilaoi As Integer
        UltimaFilaoi = gbaseoi.Items.Count - 1

        gbaseoi.Items(UltimaFilaoi).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseoi.Items(UltimaFilaoi).ForeColor = Drawing.Color.Blue
        gbaseoi.Items(UltimaFilaoi).Font.Bold = True
        gbaseoi.Items(UltimaFilaoi).Font.Size = 10

        Dim UltimaFilaGBC2 As Integer
        UltimaFilaGBC2 = gbaseCMA2.Items.Count - 1

        gbaseCMA2.Items(UltimaFilaGBC2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseCMA2.Items(UltimaFilaGBC2).ForeColor = Drawing.Color.Blue
        gbaseCMA2.Items(UltimaFilaGBC2).Font.Bold = True
        gbaseCMA2.Items(UltimaFilaGBC2).Font.Size = 10

        '''' PALAK-INNO ''
        Dim UltimaFilaGP As Integer
        UltimaFilaGP = gridventasPALAK.Items.Count - 1

        gridventasPALAK.Items(UltimaFilaGP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasPALAK.Items(UltimaFilaGP).ForeColor = Drawing.Color.Black
        gridventasPALAK.Items(UltimaFilaGP).Font.Bold = True
        gridventasPALAK.Items(UltimaFilaGP).Font.Size = 10

        Dim UltimaFilaGBP As Integer
        UltimaFilaGBP = gbasePALAK.Items.Count - 1

        gbasePALAK.Items(UltimaFilaGBP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbasePALAK.Items(UltimaFilaGBP).ForeColor = Drawing.Color.Black
        gbasePALAK.Items(UltimaFilaGBP).Font.Bold = True
        gbasePALAK.Items(UltimaFilaGBP).Font.Size = 10
        gbasePALAK.Items(UltimaFilaGBP).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBP2 As Integer
        UltimaFilaGBP2 = gbasePALAK2.Items.Count - 1

        gbasePALAK2.Items(UltimaFilaGBP2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbasePALAK2.Items(UltimaFilaGBP2).ForeColor = Drawing.Color.Blue
        gbasePALAK2.Items(UltimaFilaGBP2).Font.Bold = True
        gbasePALAK2.Items(UltimaFilaGBP2).Font.Size = 10

        '''' INO
        Dim UltimaFilaGI As Integer
        UltimaFilaGI = gridventasINNOMED.Items.Count - 1

        gridventasINNOMED.Items(UltimaFilaGI).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasINNOMED.Items(UltimaFilaGI).ForeColor = Drawing.Color.Black
        gridventasINNOMED.Items(UltimaFilaGI).Font.Bold = True
        gridventasINNOMED.Items(UltimaFilaGI).Font.Size = 10

        Dim UltimaFilaGBI As Integer
        UltimaFilaGBI = gbaseINNOMED.Items.Count - 1

        gbaseINNOMED.Items(UltimaFilaGBI).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseINNOMED.Items(UltimaFilaGBI).ForeColor = Drawing.Color.Black
        gbaseINNOMED.Items(UltimaFilaGBI).Font.Bold = True
        gbaseINNOMED.Items(UltimaFilaGBI).Font.Size = 10
        gbaseINNOMED.Items(UltimaFilaGBI).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBI2 As Integer
        UltimaFilaGBI2 = gbaseINNOMED.Items.Count - 1

        gbaseINNOMED2.Items(UltimaFilaGBI2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseINNOMED2.Items(UltimaFilaGBI2).ForeColor = Drawing.Color.Blue
        gbaseINNOMED2.Items(UltimaFilaGBI2).Font.Bold = True
        gbaseINNOMED2.Items(UltimaFilaGBI2).Font.Size = 10

        '''' AC
        Dim UltimaFilaGAC As Integer
        UltimaFilaGAC = gridventasanticipos.Items.Count - 1

        gridventasanticipos.Items(UltimaFilaGAC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasanticipos.Items(UltimaFilaGAC).ForeColor = Drawing.Color.Black
        gridventasanticipos.Items(UltimaFilaGAC).Font.Bold = True
        gridventasanticipos.Items(UltimaFilaGAC).Font.Size = 10

        Dim UltimaFilaGBAC As Integer
        UltimaFilaGBAC = gbaseanticipos.Items.Count - 1

        gbaseanticipos.Items(UltimaFilaGBAC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseanticipos.Items(UltimaFilaGBAC).ForeColor = Drawing.Color.Black
        gbaseanticipos.Items(UltimaFilaGBAC).Font.Bold = True
        gbaseanticipos.Items(UltimaFilaGBAC).Font.Size = 10
        gbaseanticipos.Items(UltimaFilaGBAC).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBAC2 As Integer
        UltimaFilaGBAC2 = gbaseanticipos.Items.Count - 1

        gbaseanticipos2.Items(UltimaFilaGBAC2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseanticipos2.Items(UltimaFilaGBAC2).ForeColor = Drawing.Color.Blue
        gbaseanticipos2.Items(UltimaFilaGBAC2).Font.Bold = True
        gbaseanticipos2.Items(UltimaFilaGBAC2).Font.Size = 10

        '''' FIN

        'AB22032021 
        Dim UltimaFilaGM As Integer
        UltimaFilaGM = gridventasMEDTRONIC.Items.Count - 1

        gridventasMEDTRONIC.Items(UltimaFilaGM).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasMEDTRONIC.Items(UltimaFilaGM).ForeColor = Drawing.Color.Black
        gridventasMEDTRONIC.Items(UltimaFilaGM).Font.Bold = True
        gridventasMEDTRONIC.Items(UltimaFilaGM).Font.Size = 10

        Dim UltimaFilaGBM As Integer
        UltimaFilaGBM = gbaseMEDTRONIC.Items.Count - 1

        gbaseMEDTRONIC.Items(UltimaFilaGBM).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseMEDTRONIC.Items(UltimaFilaGBM).ForeColor = Drawing.Color.Black
        gbaseMEDTRONIC.Items(UltimaFilaGBM).Font.Bold = True
        gbaseMEDTRONIC.Items(UltimaFilaGBM).Font.Size = 10
        gbaseMEDTRONIC.Items(UltimaFilaGBM).HorizontalAlign = HorizontalAlign.Right

        Dim UltimaFilaGBM2 As Integer
        UltimaFilaGBM2 = gbaseMEDTRONIC.Items.Count - 1

        gbaseMEDTRONIC2.Items(UltimaFilaGBM2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseMEDTRONIC2.Items(UltimaFilaGBM2).ForeColor = Drawing.Color.Blue
        gbaseMEDTRONIC2.Items(UltimaFilaGBM2).Font.Bold = True
        gbaseMEDTRONIC2.Items(UltimaFilaGBM2).Font.Size = 10

    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        lblaltabrisa.EnableViewState = False
        lblcma.EnableViewState = False
        lblinnomed.EnableViewState = False
        lblpalak.EnableViewState = False
        lblanticipos.EnableViewState = False
        lbltotales.EnableViewState = False
        gridventasALTA.EnableViewState = False
        gridventasCMA.EnableViewState = False
        gridventasINNOMED.EnableViewState = False
        'AB22032021
        gridventasMEDTRONIC.EnableViewState = False
        gridventasPALAK.EnableViewState = False
        gridventasanticipos.EnableViewState = False
        gridtotales.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(lblaltabrisa)
        form.Controls.Add(gridventasALTA)
        form.Controls.Add(lblcma)
        form.Controls.Add(gridventasCMA)
        form.Controls.Add(lblinnomed)
        form.Controls.Add(gridventasINNOMED)
        form.Controls.Add(lblmedtronic)
        form.Controls.Add(gridventasMEDTRONIC)
        form.Controls.Add(lblpalak)
        form.Controls.Add(gridventasPALAK)
        form.Controls.Add(lblanticipos)
        form.Controls.Add(gridventasanticipos)
        form.Controls.Add(lbltotales)
        form.Controls.Add(gridtotales)
        pagina.RenderControl(htw)
        Response.Clear()
        Response.Buffer = True
        Response.ContentType = "application/vnd.ms-excel"
        Response.AddHeader("Content-Disposition", "attachment;filename=VentasEstimadas.xls")
        Response.Charset = "UTF-8"
        Response.ContentEncoding = Encoding.Default
        Response.Write(sb.ToString())
        Response.End()

    End Sub
    Protected Sub cmdexcelcm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcelcm.Click
        ExportarAExcel()
    End Sub
    Protected Sub gridventasALTA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasALTA.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1

            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gridventasCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasCMA.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gridtotales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridtotales.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
 
    Protected Sub gbaseALTA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseALTA.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseTOTAL_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseTOTAL.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseTOTAL2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseTOTAL2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10


        Next
    End Sub

    Protected Sub gbaseoi_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseoi.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub
    Protected Sub gbaseCMA2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub
    '''' INNOPALAK
    Protected Sub gridventasINNOMED_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasINNOMED.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gridventasPALAK_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasPALAK.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gridventasanticipos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasanticipos.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gbaseINNOMED_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseINNOMED.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbasePALAK_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbasePALAK.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseanticipos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseanticipos.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseINNOMED2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseINNOMED2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub
    Protected Sub gbasePALAK2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbasePALAK2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub
    Protected Sub gbaseanticipos2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseanticipos2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub

    'AB22032021
    Protected Sub gridventasMEDTRONIC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasMEDTRONIC.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
            e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
        Next

        For t As Integer = 0 To 1
            e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
            e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
        Next
    End Sub
    Protected Sub gbaseMEDTRONIC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseMEDTRONIC.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
        e.Item.Cells(0).Font.Bold = True
        'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    End Sub
    Protected Sub gbaseMEDTRONIC2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseMEDTRONIC2.ItemDataBound
        e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
        e.Item.Cells(0).Font.Bold = True
        e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
        e.Item.Cells(0).Visible = False

        For i As Integer = 0 To 3
            'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

        Next

        For t As Integer = 0 To 1
            'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
            e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



        Next
    End Sub

End Class
