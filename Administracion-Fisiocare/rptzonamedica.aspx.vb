Imports System.Data.SqlClient
Imports System.Data
Partial Class rptzonamedica
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
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'D'and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'D' and d.CCANCELADO=0 "
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


            'sql = ""
            'sqlaux = ""
            'sqltotales = ""
            'For i As Integer = 0 To dias
            '    'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
            '    'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
            '    sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            '    sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

            '    'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
            '    'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
            '    sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            '    sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            'Next

            'sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            'sql += vbNewLine & sqlaux & vbNewLine
            'sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            'sql += vbNewLine & "from admProductos p"
            'sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            'sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'E' and d.CCANCELADO=0 "
            'sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            'sql += vbNewLine & ""
            'sql += vbNewLine & "union all"
            'sql += vbNewLine & "select 'TOTALES',"
            'sql += vbNewLine & sqltotales & vbNewLine
            'sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            'sql += vbNewLine & "from admProductos p"
            'sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            'sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'E' and d.CCANCELADO=0 "
            'gridventasCMA = funciones.LLenaGridC(sql, gridventasCMA, hfSucursal.Value)

            'Dim dtC As New DataTable
            'Dim viewC As New DataView
            'Dim dt2C As New DataTable
            'viewC = gridventasCMA.DataSource
            'dt2C = viewC.ToTable
            'dtC.Columns.Add("CLASIFICACIONES")
            'For Each filas As DataRow In dt2C.Rows
            '    dtC.Rows.Add(filas.Item(0).ToString)
            'Next
            'gbaseCMA.DataSource = dtC
            'gbaseCMA.DataBind()

            '''''''''pensiones'''

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'I' and d1.CCANCELADO=0 "
                sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'I' and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'I' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'I' and d.CCANCELADO=0 "
            gridventasPENSIONES = funciones.LLenaGridC(sql, gridventasPENSIONES, hfSucursal.Value)

            Dim dtP As New DataTable
            Dim viewP As New DataView
            Dim dt2P As New DataTable
            viewP = gridventasPENSIONES.DataSource
            dt2P = viewP.ToTable
            dtP.Columns.Add("CLASIFICACIONES")
            For Each filas As DataRow In dt2P.Rows
                dtP.Rows.Add(filas.Item(0).ToString)
            Next
            gbasePENSIONES.DataSource = dtP
            gbasePENSIONES.DataBind()

            '''''fin pensiones''''

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E') and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E' or d1.CSERIEDOCUMENTO = 'I')  and d1.CCANCELADO=0 "
                sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = vbNewLine & "select 'ALTABRISA-VENTAS POR INTERNET' as TOTALES,"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and (d.CSERIEDOCUMENTO = 'D' or d.CSERIEDOCUMENTO = 'E' or d.CSERIEDOCUMENTO = 'I') and d.CCANCELADO=0 "
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
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E') and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and (d1.CSERIEDOCUMENTO = 'D' or d1.CSERIEDOCUMENTO = 'E')  and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = vbNewLine & "select 'ALTABRISA-VENTAS POR INTERNET' as TOTALES,"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and (d.CSERIEDOCUMENTO = 'D' or d.CSERIEDOCUMENTO = 'E'  or d.CSERIEDOCUMENTO = 'I') and d.CCANCELADO=0 "
            gbaseTOTAL2 = funciones.LLenaGridC(sql, gbaseTOTAL2, hfSucursal.Value)

            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'D' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'D'and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & "round(sum(c.cunidades),0) as 'Total Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'D' and d.CCANCELADO=0 "
            gbaseALTA2 = funciones.LLenaGridC(sql, gbaseALTA2, hfSucursal.Value)

            'sql = ""
            'sqlaux = ""
            'sqltotales = ""
            'For i As Integer = 0 To dias
            '    'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
            '    'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
            '    'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            '    'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

            '    'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
            '    'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
            '    'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
            '    'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            '    'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
            '    'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            'Next

            'sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            'sql += vbNewLine & sqlaux & vbNewLine
            'sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            'sql += vbNewLine & "from admProductos p"
            'sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            'sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'E' and d.CCANCELADO=0 "
            'sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            'sql += vbNewLine & ""
            'sql += vbNewLine & "union all"
            'sql += vbNewLine & "select 'TOTALES',"
            'sql += vbNewLine & sqltotales & vbNewLine
            'sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            'sql += vbNewLine & "from admProductos p"
            'sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            'sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'E' and d.CCANCELADO=0 "
            'gbaseCMA2 = funciones.LLenaGridC(sql, gbaseCMA2, hfSucursal.Value)

            ''''''' base pensiones 2''''
            sql = ""
            sqlaux = ""
            sqltotales = ""
            For i As Integer = 0 To dias
                'sqlaux += vbNewLine & " isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqlaux += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1 and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqlaux += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqlaux += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqlaux += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and p1.CIDVALORCLASIFICACION1 = p.CIDVALORCLASIFICACION1  and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqlaux += vbNewLine & "group by p1.CIDVALORCLASIFICACION1  ),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp', "

                'sqltotales += vbNewLine & "isnull( (select  sum(c1.cunidades) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1 left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO  "
                'sqltotales += vbNewLine & "where  c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " Pzas', "
                'sqltotales += vbNewLine & "concat('$',isnull( (select  CONVERT(VARCHAR(30), CONVERT(MONEY,sum(c1.CNETO)- sum(c1.CDESCUENTO1)),1) as Pzas from admProductos p1 left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO "
                'sqltotales += vbNewLine & "left join admClasificacionesValores cs1 on cs1.CIDVALORCLASIFICACION = p1.CIDVALORCLASIFICACION1  left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
                'sqltotales += vbNewLine & "where c1.CFECHA ='" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM/yyyy") + "' and d1.CIDDOCUMENTODE = 4 and d1.CSERIEDOCUMENTO = 'E' and d1.CCANCELADO=0 "
                'sqltotales += vbNewLine & "),0)) as '" + Format(DateAdd(DateInterval.Day, i, CDate(txtfecha1.Text)), "dd/MM") + " $Imp',"

            Next

            sql = "select  isnull(left(cs.CVALORCLASIFICACION,20), 'SINCLASIFICACIONES') as CLASIFICACIONES,"
            sql += vbNewLine & sqlaux & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as 'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where  c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'I' and d.CCANCELADO=0 "
            sql += vbNewLine & "group by cs.CVALORCLASIFICACION,  p.CIDVALORCLASIFICACION1"
            sql += vbNewLine & ""
            sql += vbNewLine & "union all"
            sql += vbNewLine & "select 'TOTALES',"
            sql += vbNewLine & sqltotales & vbNewLine
            sql += vbNewLine & " round(sum(c.cunidades),0) as'Total Pzas', concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(c.CNETO)- sum(c.CDESCUENTO1)), 1)) as 'Total $Imp',  round(((SUM(c.cunidades))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),0) AS 'Estimado Pzas',  concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY,(SUM(c.CNETO)- sum(c.CDESCUENTO1))*(" & CInt(txtNumdias.Text) & "))/(" & dias + 1 & "),1)) AS 'Estimado $Imp'"
            sql += vbNewLine & "from admProductos p"
            sql += vbNewLine & "left join admMovimientos c on p.CIDPRODUCTO=c.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos d on d.CIDDOCUMENTO = c.CIDDOCUMENTO"
            sql += vbNewLine & "left join admClasificacionesValores cs on cs.CIDVALORCLASIFICACION = p.CIDVALORCLASIFICACION1 "
            sql += vbNewLine & "where   c.CFECHA>='" + txtfecha1.Text + " ' and c.CFECHA<='" + txtfecha2.Text.Trim + "' and d.CIDDOCUMENTODE = 4 and d.CSERIEDOCUMENTO = 'I' and d.CCANCELADO=0 "
            gbasePENSIONES2 = funciones.LLenaGridC(sql, gbasePENSIONES2, hfSucursal.Value)

            ''''' fin base pensiones 2'''

        Else

        End If

        cmdexcelcm.Visible = True
        gridventasALTA.Visible = True
        'gridventasCMA.Visible = True
        gridventasPENSIONES.Visible = True
        lblaltabrisa.Visible = True
        'lblcma.Visible = True
        lblpensiones.Visible = True
        lbltotales.Visible = True

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = gridventasALTA.Items.Count - 1

        gridventasALTA.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasALTA.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        gridventasALTA.Items(UltimaFilaGA).Font.Bold = True
        gridventasALTA.Items(UltimaFilaGA).Font.Size = 10

        'Dim UltimaFilaGC As Integer
        'UltimaFilaGC = gridventasCMA.Items.Count - 1

        'gridventasCMA.Items(UltimaFilaGC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridventasCMA.Items(UltimaFilaGC).ForeColor = Drawing.Color.Black
        'gridventasCMA.Items(UltimaFilaGC).Font.Bold = True
        'gridventasCMA.Items(UltimaFilaGC).Font.Size = 10

        ''''PENSIONES'''

        Dim UltimaFilaGP As Integer
        UltimaFilaGP = gridventasPENSIONES.Items.Count - 1

        gridventasPENSIONES.Items(UltimaFilaGP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridventasPENSIONES.Items(UltimaFilaGP).ForeColor = Drawing.Color.Black
        gridventasPENSIONES.Items(UltimaFilaGP).Font.Bold = True
        gridventasPENSIONES.Items(UltimaFilaGP).Font.Size = 10


        Dim UltimaFilaGBP As Integer
        UltimaFilaGBP = gbasePENSIONES.Items.Count - 1

        gbasePENSIONES.Items(UltimaFilaGBP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbasePENSIONES.Items(UltimaFilaGBP).ForeColor = Drawing.Color.Black
        gbasePENSIONES.Items(UltimaFilaGBP).Font.Bold = True
        gbasePENSIONES.Items(UltimaFilaGBP).Font.Size = 10
        gbasePENSIONES.Items(UltimaFilaGBP).HorizontalAlign = HorizontalAlign.Right


        Dim UltimaFilaGBP2 As Integer
        UltimaFilaGBP2 = gbasePENSIONES2.Items.Count - 1

        gbasePENSIONES2.Items(UltimaFilaGBP2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbasePENSIONES2.Items(UltimaFilaGBP2).ForeColor = Drawing.Color.Blue
        gbasePENSIONES2.Items(UltimaFilaGBP2).Font.Bold = True
        gbasePENSIONES2.Items(UltimaFilaGBP2).Font.Size = 10

        '''' FIN PENSIONES'''

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

        'Dim UltimaFilaGBC As Integer
        'UltimaFilaGBC = gbaseCMA.Items.Count - 1

        'gbaseCMA.Items(UltimaFilaGBC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseCMA.Items(UltimaFilaGBC).ForeColor = Drawing.Color.Black
        'gbaseCMA.Items(UltimaFilaGBC).Font.Bold = True
        'gbaseCMA.Items(UltimaFilaGBC).Font.Size = 10
        'gbaseCMA.Items(UltimaFilaGBC).HorizontalAlign = HorizontalAlign.Right

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

        Dim UltimaFilaGBA2 As Integer
        UltimaFilaGBA2 = gbaseALTA2.Items.Count - 1

        gbaseALTA2.Items(UltimaFilaGBA2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseALTA2.Items(UltimaFilaGBA2).ForeColor = Drawing.Color.Blue
        gbaseALTA2.Items(UltimaFilaGBA2).Font.Bold = True
        gbaseALTA2.Items(UltimaFilaGBA2).Font.Size = 10

        'Dim UltimaFilaGBC2 As Integer
        'UltimaFilaGBC2 = gbaseCMA2.Items.Count - 1

        'gbaseCMA2.Items(UltimaFilaGBC2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseCMA2.Items(UltimaFilaGBC2).ForeColor = Drawing.Color.Blue
        'gbaseCMA2.Items(UltimaFilaGBC2).Font.Bold = True
        'gbaseCMA2.Items(UltimaFilaGBC2).Font.Size = 10

        
    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        lblaltabrisa.EnableViewState = False
        'lblcma.EnableViewState = False
        lblpensiones.EnableViewState = False
        lbltotales.EnableViewState = False
        gridventasALTA.EnableViewState = False
        'gridventasCMA.EnableViewState = False
        gridventasPENSIONES.EnableViewState = False
        gridtotales.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        form.Controls.Add(lblaltabrisa)
        form.Controls.Add(gridventasALTA)
        'form.Controls.Add(lblcma)
        'form.Controls.Add(gridventasCMA)
        form.Controls.Add(lblpensiones)
        form.Controls.Add(gridventasPENSIONES)
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
    'Protected Sub gridventasCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasCMA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
    '    Next

    '    For t As Integer = 0 To 1
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
    '    Next
    'End Sub
    Protected Sub gridventasPENSIONES_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasPENSIONES.ItemDataBound
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
    'Protected Sub gbaseCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
    '    e.Item.Cells(0).Font.Bold = True
    '    'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    'End Sub
    Protected Sub gbasePENSIONES_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbasePENSIONES.ItemDataBound
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
    Protected Sub gbaseALTA2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseALTA2.ItemDataBound
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

    'Protected Sub gbaseCMA2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA2.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

    '    Next

    '    For t As Integer = 0 To 1
    '        'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



    '    Next
    'End Sub
    Protected Sub gbasePENSIONES2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbasePENSIONES2.ItemDataBound
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
