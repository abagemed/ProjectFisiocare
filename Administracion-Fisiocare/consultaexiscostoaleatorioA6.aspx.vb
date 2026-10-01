Imports System.Data
Imports System.Data.SqlClient

Partial Class consultaexiscostoaleatorioA6

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
            'Dim sql2 As String
            'Dim sql3 As String
            'Dim sql4 As String

            'sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "isnull( (SELECT SUM(cunidades) as Entradas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=1"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=2"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) AS EXISTENCIAS,"
            'sql += vbNewLine & "CAST("
            'sql += vbNewLine & "(case isnull((SELECT SUM(cunidades) as Entradas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=1"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=2"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0)  when '0' then '0' else (case round(sum(admMovimientos.cunidades),0)/(isnull( (SELECT SUM(cunidades) as Entradas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=1"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & " And cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=2"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0))  when '0' then '0' else round(sum(admMovimientos.cunidades),0)/(isnull( (SELECT SUM(cunidades) as Entradas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=1"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=2"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0)) end) end)AS decimal(16,2)) as RINVENTARIO,"
            'sql += vbNewLine & "ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            'sql += vbNewLine & "FROM admCostosHistoricos c "
            'sql += vbNewLine & " WHERE c.cIdProducto = admProductos.cIdProducto"
            'sql += vbNewLine & "AND c.cIdAlmacen = 0 "
            'sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0) as ULTIMOCOSTO"
            'sql += vbNewLine & "from admProductos"
            'sql += vbNewLine & "left join admMovimientos on admMovimientos.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 AND admProductos.cTipoProducto=1"
            'sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admProductos.CPRECIO1,admProductos.CPRECIO2,admProductos.CPRECIO3"
            'sql += vbNewLine & "order by RINVENTARIO"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)


            'sql = "Select [CIDPRODUCTO]"
            'sql += vbNewLine & ",[CCODIGOPRODUCTO]"
            'sql += vbNewLine & ",[CNOMBREPRODUCTO]"
            'sql += vbNewLine & ",[CTIPOPRODUCTO]"
            'sql += vbNewLine & ",[CCONTROLEXISTENCIA]"
            'sql += vbNewLine & ",[CMETODOCOSTEO]"
            'sql += vbNewLine & ",[CSTATUSPRODUCTO]"
            'sql += vbNewLine & ",[CIDUNIDADBASE]"
            'sql += vbNewLine & ",[CPRECIO1]"
            'sql += vbNewLine & ",[CPRECIO2]"
            'sql += vbNewLine & ",[CPRECIO3]"
            'sql += vbNewLine & ",[CFECHAALTAPRODUCTO]"
            'sql += vbNewLine & ",isnull( (SELECT SUM(cunidades) as Entradas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=1"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            'sql += vbNewLine & "from admProductos p1 "
            'sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            'sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            'sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            'sql += vbNewLine & "and cAfectaExistencia=2"
            'sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            'sql += vbNewLine & "AND cAfectadoInventario <> 0"
            'sql += vbNewLine & "),0) AS EXISTENCIAS"
            'sql += vbNewLine & "FROM [adMed_Solution_de_Sure2018].[dbo].[admProductos]"
            'sql += vbNewLine & "where CSTATUSPRODUCTO = 1 And CTIPOPRODUCTO = 1 "
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)


            sql = " select TOP 10 admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            sql += vbNewLine & "isnull( (SELECT SUM(cunidades) as Entradas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            sql += vbNewLine & "and cAfectaExistencia=1"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "and P1.CTIPOPRODUCTO=1"
            sql += vbNewLine & "and P1.CSTATUSPRODUCTO = 1"
            sql += vbNewLine & "and  (c1.CIDALMACEN=6)"
            sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            sql += vbNewLine & "and cAfectaExistencia=2"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "and P1.CTIPOPRODUCTO=1"
            sql += vbNewLine & "and P1.CSTATUSPRODUCTO = 1"
            sql += vbNewLine & "and  (c1.CIDALMACEN=6)"
            sql += vbNewLine & "),0) AS EXISTENCIAS,"
            sql += vbNewLine & "ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            sql += vbNewLine & "FROM admCostosHistoricos c "
            sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            sql += vbNewLine & "AND (c.CIDALMACEN=6)"
            sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0) as ULTIMO_COSTO,"
            sql += vbNewLine & "ISNULL((SELECT TOP 1 C.CCOSTOH "
            sql += vbNewLine & "FROM admCostosHistoricos  AS C "
            sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            sql += vbNewLine & "AND (c.CIDALMACEN=6)"
            sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00'"
            sql += vbNewLine & "ORDER BY C.CFECHACOSTOH DESC), 0) AS COSTOPROMEDIO"
            sql += vbNewLine & ",(admProductos.CPRECIO1 * 1.16) AS PRECIOVENTA"
            sql += vbNewLine & "from admProductos"
            sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            sql += vbNewLine & "where admProductos.CTIPOPRODUCTO = 1"
            sql += vbNewLine & "and admProductos.CSTATUSPRODUCTO = 1"
            sql += vbNewLine & "and  (admMovimientos.CIDALMACEN=6)"
            sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admProductos.CPRECIO1"
            sql += vbNewLine & "order by NEWID()"
            gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)

            'sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql += vbNewLine & "from admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='D') and admDocumentos.CCANCELADO=0"
            'sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            'sql += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql += vbNewLine & "FROM admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='D') and admDocumentos.CCANCELADO=0"
            'sql += vbNewLine & "order by CANTIDAD"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)

            'sql2 = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql2 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql2 += vbNewLine & "from admProductos"
            'sql2 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql2 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql2 += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='E') and admDocumentos.CCANCELADO=0"
            'sql2 += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            'sql2 += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,"
            'sql2 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql2 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql2 += vbNewLine & "FROM admProductos"
            'sql2 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql2 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql2 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='E') and admDocumentos.CCANCELADO=0"
            'sql2 += vbNewLine & "order by CANTIDAD"
            'gridvadmProductoscma = funciones.LLenaGridC(sql2, gridvadmProductoscma, hfSucursal.Value)

            'sql3 = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql3 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql3 += vbNewLine & "from admProductos"
            'sql3 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql3 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql3 += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='P') and admDocumentos.CCANCELADO=0"
            'sql3 += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            'sql3 += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,"
            'sql3 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql3 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql3 += vbNewLine & "FROM admProductos"
            'sql3 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql3 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql3 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='P') and admDocumentos.CCANCELADO=0"
            'sql3 += vbNewLine & "order by CANTIDAD"
            'gridvadmProductospensiones = funciones.LLenaGridC(sql3, gridvadmProductospensiones, hfSucursal.Value)


            'sql4 = " select 'ALTABRISA-CENTRO-PENSIONES' as TOTALES,"
            'sql4 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'TOTAL DE PRODUCTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'TOTAL DE IMPORTES',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'TOTAL DE DESCUENTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'TOTAL DE IMPUESTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL DE VENTAS'"
            'sql4 += vbNewLine & "FROM admProductos"
            'sql4 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql4 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql4 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='D' or admDocumentos.CSERIEDOCUMENTO = 'E' or admDocumentos.CSERIEDOCUMENTO = 'P') and admDocumentos.CCANCELADO=0"
            'gridvadmProductostotales = funciones.LLenaGridC(sql4, gridvadmProductostotales, hfSucursal.Value)




        Else

        End If

        'Dim UltimaFilaGA As Integer
        'UltimaFilaGA = gridvadmProductos.Items.Count - 1

        'gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductos.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        'gridvadmProductos.Items(UltimaFilaGA).Font.Bold = True
        'gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10

        'Dim UltimaFilaGC As Integer
        'UltimaFilaGC = gridvadmProductoscma.Items.Count - 1

        'gridvadmProductoscma.Items(UltimaFilaGC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductoscma.Items(UltimaFilaGC).ForeColor = Drawing.Color.Black
        'gridvadmProductoscma.Items(UltimaFilaGC).Font.Bold = True
        'gridvadmProductoscma.Items(UltimaFilaGC).Font.Size = 10


        'Dim UltimaFilaGP As Integer
        'UltimaFilaGP = gridvadmProductospensiones.Items.Count - 1

        'gridvadmProductospensiones.Items(UltimaFilaGP).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductospensiones.Items(UltimaFilaGP).ForeColor = Drawing.Color.Black
        'gridvadmProductospensiones.Items(UltimaFilaGP).Font.Bold = True
        'gridvadmProductospensiones.Items(UltimaFilaGP).Font.Size = 10


        'Dim UltimaFilaGT As Integer
        'UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

        'gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductostotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        'gridvadmProductostotales.Items(UltimaFilaGT).Font.Bold = True
        'gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

        cmdexcelcm.Visible = True
        gridvadmProductos.Visible = True
        lblaltabrisa.Visible = True
        'lblcma.Visible = True
        'lblpensiones.Visible = True
        'lbltotales.Visible = True
    End Sub


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim hoy As DateTime = DateTime.Now()
            txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)

        End If
    End Sub

    Protected Sub ExportarAExcel()
        Dim sb As StringBuilder = New StringBuilder()
        Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        Dim pagina As Page = New Page
        Dim form As New HtmlForm
        gridvadmProductos.EnableViewState = False
        pagina.EnableEventValidation = False
        pagina.DesignerInitialize()
        pagina.Controls.Add(form)
        'form.Controls.Add(lbltotales)
        'form.Controls.Add(gridvadmProductostotales)
        form.Controls.Add(lblaltabrisa)
        form.Controls.Add(gridvadmProductos)
        'form.Controls.Add(lblcma)
        'form.Controls.Add(gridvadmProductoscma)
        'form.Controls.Add(lblpensiones)
        'form.Controls.Add(gridvadmProductospensiones)
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
