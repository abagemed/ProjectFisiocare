Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpadminimplantes

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
            'Dim sql2 As String
            'Dim sql3 As String
            Dim sql4 As String

            'sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO)-SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'NETO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL',"
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
            'sql += vbNewLine & "ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            'sql += vbNewLine & "FROM admCostosHistoricos c "
            'sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            'sql += vbNewLine & "AND c.cIdAlmacen = 0"
            'sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0) as ULTIMO_COSTO,"
            'sql += vbNewLine & "STR(((((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0.0)) +"
            'sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0.0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0.0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0.0)))/2)),30, 2)	 AS PROMEDIO_INVENTARIO,"
            'sql += vbNewLine & "STR((CASE WHEN (ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            'sql += vbNewLine & "FROM admCostosHistoricos c "
            'sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            'sql += vbNewLine & "AND c.cIdAlmacen = 0"
            'sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0)) = 0 OR (((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0)) +"
            'sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0)))/2) = 0 THEN 0 ELSE (ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            'sql += vbNewLine & "FROM admCostosHistoricos c "
            'sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            'sql += vbNewLine & "AND c.cIdAlmacen = 0"
            'sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0))/(((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0)) +"
            'sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            'sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            'sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            'sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0)))/2) END) ,30, 2) AS ROTACION_INVENTARIO"
            'sql += vbNewLine & "from admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            'sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            'sql += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL',"
            'sql += vbNewLine & "'' AS EXISTENCIAS,"
            'sql += vbNewLine & "'' AS UCOSTO,"
            'sql += vbNewLine & "'' AS PINVENTARIO,"
            'sql += vbNewLine & "'' AS RINVENTARIO"
            'sql += vbNewLine & "FROM admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            'sql += vbNewLine & "order by CANTIDAD"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)

            sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO)-SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'NETO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL',"
            sql += vbNewLine & "isnull( (SELECT SUM(cunidades) as Entradas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            sql += vbNewLine & "and cAfectaExistencia=1"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "),0) - isnull( (SELECT SUM(cunidades) as Salidas "
            sql += vbNewLine & "from admProductos p1 "
            sql += vbNewLine & "left join admMovimientos c1 on p1.CIDPRODUCTO=c1.CIDPRODUCTO"
            sql += vbNewLine & "left join admDocumentos d1 on d1.CIDDOCUMENTO = c1.CIDDOCUMENTO "
            sql += vbNewLine & "where  c1.CFECHA>='01/01/2017 00:00:00' and c1.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00'"
            sql += vbNewLine & "and cAfectaExistencia=2"
            sql += vbNewLine & "AND P1.CIDPRODUCTO=admProductos.CIDPRODUCTO"
            sql += vbNewLine & "AND cAfectadoInventario <> 0"
            sql += vbNewLine & "),0) AS EXISTENCIAS,"
            sql += vbNewLine & "ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            sql += vbNewLine & "FROM admCostosHistoricos c "
            sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            sql += vbNewLine & "AND c.cIdAlmacen = 0"
            sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0) as ULTIMO_COSTO,"
            sql += vbNewLine & "STR(((((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0.0)) +"
            sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0.0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0.0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0.0)))/2)),30, 2)	 AS PROMEDIO_INVENTARIO,"
            sql += vbNewLine & "STR((CASE WHEN (ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            sql += vbNewLine & "FROM admCostosHistoricos c "
            sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            sql += vbNewLine & "AND c.cIdAlmacen = 0"
            sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0)) = 0 OR (((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0)) +"
            sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0)))/2) = 0 THEN 0 ELSE (ISNULL((SELECT TOP 1 c.cUltimoCostoH "
            sql += vbNewLine & "FROM admCostosHistoricos c "
            sql += vbNewLine & "WHERE c.cIdProducto = admProductos.cIdProducto"
            sql += vbNewLine & "AND c.cIdAlmacen = 0"
            sql += vbNewLine & "AND c.cFechaCostoH <= '" + txtfecha2.Text.Trim + " 12:00:00' order by c.CFECHACOSTOH desc),0))/(((ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0)) +"
            sql += vbNewLine & "(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cCostoEspecifico ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0 ), 0) + ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cCostoEspecifico ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0) - ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cCostoEspecifico ELSE 0 END )FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0), 0)))/2) END) ,30, 2) AS ROTACION_INVENTARIO,"
            sql += vbNewLine & "CAST((CASE WHEN (ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0             ), 0.0)) = 0 or (((ROUND(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cUnidades ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto            "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0             ), 0.0),5,0))+(((ROUND(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cUnidades ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto            "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0             ), 0.0),5,0))+(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0.0)))-(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0             ), 0.0))))/2) = 0 THEN 0 ELSE (ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0             ), 0.0))/(((ROUND(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cUnidades ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto            "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0             ), 0.0),5,0))+(((ROUND(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) - SUM(CASE WHEN cAfectaExistencia = 2 THEN  cUnidades ELSE 0 END ) "
            sql += vbNewLine & "FROM admMovimientos AS Movto            "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha < '" + txtfecha1.Text + " 00:00:00' AND cAfectadoInventario <> 0             ), 0.0),5,0))+(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 1 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0 ), 0.0)))-(ISNULL((SELECT SUM(CASE WHEN cAfectaExistencia = 2 THEN cUnidades ELSE 0 END ) FROM admMovimientos AS Movto "
            sql += vbNewLine & "LEFT JOIN admProductos p ON Movto.cIdProducto = p.cIdProducto WHERE "
            sql += vbNewLine & "movto.cIdProducto = admProductos.cIdProducto AND movto.cFecha >= '" + txtfecha1.Text + " 00:00:00' AND movto.cFecha <= '" + txtfecha2.Text.Trim + " 12:00:00' AND cAfectadoInventario <> 0             ), 0.0))))/2) END)as decimal(16,2)) as RI_UNIDADES"
            sql += vbNewLine & "from admProductos"
            sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            sql += vbNewLine & "order by CANTIDAD"
            gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)



            sql4 = " select 'IMPLANTES' as TOTALES,"
            sql4 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'TOTAL DE PRODUCTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'TOTAL DE IMPORTES',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'TOTAL DE DESCUENTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'TOTAL DE IMPUESTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL DE VENTAS'"
            sql4 += vbNewLine & "FROM admProductos"
            sql4 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            sql4 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            sql4 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            gridvadmProductostotales = funciones.LLenaGridC(sql4, gridvadmProductostotales, hfSucursal.Value)



            'sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO)-SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'NETO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL',"
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
            'sql += vbNewLine & "),0) AS EXISTENCIAS"
            'sql += vbNewLine & "from admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            'sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO"
            'sql += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            ''sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL',"
            'sql += vbNewLine & "'' AS EXISTENCIAS"
            'sql += vbNewLine & "FROM admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            'sql += vbNewLine & "order by CANTIDAD"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)


            'sql4 = " select 'IMPLANTES' as TOTALES,"
            'sql4 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'TOTAL DE PRODUCTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'TOTAL DE IMPORTES',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'TOTAL DE DESCUENTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'TOTAL DE IMPUESTOS',"
            'sql4 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL DE VENTAS'"
            'sql4 += vbNewLine & "FROM admProductos"
            'sql4 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql4 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql4 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and admDocumentos.CCANCELADO=0 and admProductos.CTIPOPRODUCTO=1"
            'gridvadmProductostotales = funciones.LLenaGridC(sql4, gridvadmProductostotales, hfSucursal.Value)




        Else

        End If

        'Dim UltimaFilaGA As Integer
        'UltimaFilaGA = gridvadmProductos.Items.Count - 1

        'gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridvadmProductos.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        'gridvadmProductos.Items(UltimaFilaGA).Font.Bold = True
        'gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10

        Dim UltimaFilaGT As Integer
        UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

        gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gridvadmProductostotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        gridvadmProductostotales.Items(UltimaFilaGT).Font.Bold = True
        gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

        cmdexcelcm.Visible = True
        gridvadmProductos.Visible = True
        lblIMPLANTES.Visible = True
        'lblcma.Visible = True
        'lblpensiones.Visible = True
        lbltotales.Visible = True
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
        form.Controls.Add(lbltotales)
        form.Controls.Add(gridvadmProductostotales)
        form.Controls.Add(lblIMPLANTES)
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
