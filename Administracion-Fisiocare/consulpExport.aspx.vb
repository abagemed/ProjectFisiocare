Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpExport

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
            'Dim sql2 As String
            'Dim sql3 As String
            Dim sql4 As String

            'sql = " select admProductos.CIDPRODUCTO as ID_PRODUCTO,admProductos.CCODIGOPRODUCTO as CODIGO,admProductos.CNOMBREPRODUCTO as DESCRIPCION,admAgentes.CNOMBREAGENTE as Agente,admDocumentos.CRAZONSOCIAL as Cliente,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql += vbNewLine & "from admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "where admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='C') and admDocumentos.CCANCELADO=0"
            'sql += vbNewLine & "group by admProductos.CIDPRODUCTO,admProductos.CCODIGOPRODUCTO,admProductos.CNOMBREPRODUCTO,admAgentes.CNOMBREAGENTE,admDocumentos.CRAZONSOCIAL"
            'sql += vbNewLine & "union all select '' as ID_PRODUCTO,'' as CODIGO,'TOTALES' as DESCRIPCION,'' as AGENTE,'' as Cliente,"
            'sql += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'CANTIDAD',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'NETO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'DESCUENTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'IMPUESTO',"
            'sql += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL'"
            'sql += vbNewLine & "FROM admProductos"
            'sql += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            'sql += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            'sql += vbNewLine & "left join admAgentes on admAgentes.CIDAGENTE = admDocumentos.CIDAGENTE"
            'sql += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='C') and admDocumentos.CCANCELADO=0"
            'sql += vbNewLine & "order by CANTIDAD"
            'gridvadmProductos = funciones.LLenaGrid(sql, gridvadmProductos, hfSucursal.Value)
            'sql = " 	SELECT  FORMAT(CA.CFECHAABONOCARGO, 'dd-MMM-yyyy', 'en-us') AS FECHA , D.CFOLIO AS FACTURA, D.CSERIEDOCUMENTO	"
            'sql += vbNewLine & "	,Cli.CRAZONSOCIAL as Cliente	"
            'sql += vbNewLine & "	,(0) as Clasificacion	"
            'sql += vbNewLine & "	,CONVERT(decimal(38,2), D.CNETO)AS IMPORTE	"
            'sql += vbNewLine & "	,CONVERT(decimal(38,2), D.CDESCUENTOMOV)  as Descuento	"
            'sql += vbNewLine & "	, (CONVERT(decimal(38,2), D.CNETO-CDESCUENTOMOV)) AS VENTANETA	"
            'sql += vbNewLine & "	, Convert(decimal(38,2), (D.CNETO-CDESCUENTOMOV-(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END))) as Almacen	"
            'sql += vbNewLine & "	,(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END) as Almacen2	"
            'sql += vbNewLine & ",CONVERT(DECIMAL(38,2),((CONVERT(decimal(38,2), D.CNETO)-(CDESCUENTOMOV))+(CONVERT(decimal(38,2), D.CDESCUENTOMOV))-(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)*CUNIDADES) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END))*0.56) as Compra"
            'sql += vbNewLine & "	,CONVERT(decimal(38,2), Round(((D.CNETO-(CDESCUENTOMOV ))*0.03),2)) AS COMISIONAGENTE	"
            'sql += vbNewLine & "	,agt.CNOMBREAGENTE	"
            'sql += vbNewLine & "	,D.CREFERENCIA	AS DOCTOR"
            'sql += vbNewLine & "	,D.COBSERVACIONES AS HOSPITAL	"
            'sql += vbNewLine & "	,(SELECT MIN(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO) as ULTIMOPAGO	"
            'sql += vbNewLine & "	,(CASE WHEN D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO) 	"
            'sql += vbNewLine & "	THEN 'PAGADO' ELSE 'Falta por Pagar' END) as STATUS	"
            'sql += vbNewLine & "	 ,CASE WHEN  CSERIEDOCUMENTO='C' THEN (SELECT CNOMBRECUENTA FROM admCuentasBancarias where CIDCUENTA=(SELECT CIDCUENTA FROM admDocumentos where admDocumentos.CIDDOCUMENTO=CA.CIDDOCUMENTOABONO))  ELSE 'NO1' END AS CIDCUENTABANCARIA	"
            'sql += vbNewLine & "	   FROM admDocumentos D	"
            'sql += vbNewLine & "	inner join admAsocCargosAbonos CA on ca.CIDDOCUMENTOCARGO = D.CIDDOCUMENTO and CFECHAABONOCARGO=(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)	"
            'sql += vbNewLine & "	left join admAgentes agt on agt.CIDAGENTE=d.CIDAGENTE	"
            'sql += vbNewLine & "	left join admCuentasBancarias cb on D.CIDDOCUMENTO=cb.CIDCUENTA 	"
            'sql += vbNewLine & "	INNER JOIN admClientes Cli on D.CIDCLIENTEPROVEEDOR=Cli.CIDCLIENTEPROVEEDOR	"
            'sql += vbNewLine & "	where CSERIEDOCUMENTO='C' and (SELECT CNOMBRECUENTA FROM admCuentasBancarias where CIDCUENTA=(SELECT CIDCUENTA FROM admDocumentos where admDocumentos.CIDDOCUMENTO=CA.CIDDOCUMENTOABONO))='IMPLANTES' AND	"
            'sql += vbNewLine & "	(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)>='" + txtfecha1.Text + "'	"
            'sql += vbNewLine & "	AND (SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)<='" + txtfecha2.Text + "'	"
            'sql += vbNewLine & "	AND D.CCANCELADO=0 and D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)  AND (SELECT MIN(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)>1.00	"
            'sql += vbNewLine & "	order by  cfolio desc	"
            'gridvadmProductos = funciones.LLenaGrid(sql, gridvadmProductos, hfSucursal.Value)

            sql += vbNewLine & "            select "
            sql += vbNewLine & "  [CodigoPaciente]"
            sql += vbNewLine & " ,[Nombres]"
            sql += vbNewLine & " ,[PApellido]"
            sql += vbNewLine & " ,[SApellido]"
            sql += vbNewLine & " ,[Genero]"
            sql += vbNewLine & "  ,[FechaNacimiento]"
            sql += vbNewLine & "  ,Est.Descripcion"
            sql += vbNewLine & "  ,[TelefonoLocal]"
            sql += vbNewLine & "  ,[TelefonoCelular]"
            sql += vbNewLine & " ,[NombreContacto]"
            sql += vbNewLine & "  ,[TelefonoContacto]"
            sql += vbNewLine & " ,[Curp]"
            sql += vbNewLine & "  ,[Email]"
            sql += vbNewLine & "  ,o.descripcion as Ocupacion"
            sql += vbNewLine & "  ,N.nacionalidad"
            sql += vbNewLine & "  ,[Calle]"
            sql += vbNewLine & "   ,[Localidad]"
            sql += vbNewLine & "  ,M.nombreMunicipio as Municipio"
            sql += vbNewLine & "  ,[CodigoPostal]"
            sql += vbNewLine & "  ,[FechaActualizacion]"
            sql += vbNewLine & "  ,[anotaciones]"
            sql += vbNewLine & "  , S.descripcion as '¿Como se entero?', RecomendanteOtro as 'Recomendado por u otro'  "
            sql += vbNewLine & "   ,[RecomendanteOtro]"
            sql += vbNewLine & "   ,[FechaAlta]"
            sql += vbNewLine & "  ,[NSS]"
            sql += vbNewLine & "  FROM [AgemedUrosur].[dbo].CatPacientes P"
            sql += vbNewLine & " inner join [AgemedUrosur].[dbo].CatNacionalidades as N on P.codigoNacionalidad=N.codigoNacionalidad"
            sql += vbNewLine & " inner join [AgemedUrosur].[dbo].CatComoSeEntero as S on S.id=P.comoSeEntero and S.codigoEmpresa=P.CodigoEmpresa"
            sql += vbNewLine & " inner join [AgemedUrosur].[dbo].CatEstadoCivil  Est on P.CodigoEstadoCivil=Est.codigoEstadoCivil"
            sql += vbNewLine & "  INNER join [AgemedUrosur].[dbo].CatOcupaciones O on O.codigoocupacion=P.Codigoocupacion"
            sql += vbNewLine & "  inner join [AgemedUrosur].[dbo].CatMunicipios M on M.codigoMunicipio=P.codigoMunicipio"

            gridvadmProductos = funciones.LLenaGrid(sql, gridvadmProductos, hfSucursal.Value)


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
            'gridvadmProductoscma = funciones.LLenaGrid(sql2, gridvadmProductoscma, hfSucursal.Value)

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
            'gridvadmProductospensiones = funciones.LLenaGrid(sql3, gridvadmProductospensiones, hfSucursal.Value)


            sql4 = " select 'IMPLANTES' as TOTALES,"
            sql4 += vbNewLine & "round(sum(admMovimientos.cunidades),0) AS 'TOTAL DE PRODUCTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.CNETO) AS decimal(16,2)) AS 'TOTAL DE IMPORTES',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.CDESCUENTO1) AS decimal(16,2)) AS 'TOTAL DE DESCUENTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.cimpuesto1) AS decimal(16,2)) AS 'TOTAL DE IMPUESTOS',"
            sql4 += vbNewLine & "CAST(SUM(admMovimientos.ctotal) AS decimal(16,2)) AS 'TOTAL DE VENTAS'"
            sql4 += vbNewLine & "FROM admProductos"
            sql4 += vbNewLine & "left join admMovimientos on admProductos.CIDPRODUCTO=admMovimientos.CIDPRODUCTO "
            sql4 += vbNewLine & "left join admDocumentos on admDocumentos.CIDDOCUMENTO=admMovimientos.CIDDOCUMENTO"
            sql4 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='C') and admDocumentos.CCANCELADO=0"
            gridvadmProductostotales = funciones.LLenaGrid(sql4, gridvadmProductostotales, hfSucursal.Value)




        Else

        End If

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = gridvadmProductos.Items.Count - 1
        If UltimaFilaGA > 1 Then
            gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(255, 255, 204)
            'gridvadmProductos.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
            'gridvadmProductos.Items(UltimaFilaGA).Font.Bold = True
            gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10


            Dim UltimaFilaGT As Integer
            UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

            gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(255, 255, 204)
            'gridvadmProductostotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
            'gridvadmProductostotales.Items(UltimaFilaGT).Font.Bold = True
            gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

            cmdexcelcm.Visible = True
            gridvadmProductos.Visible = True
            lblIMPLANTES.Visible = True
            'lblcma.Visible = True
            'lblpensiones.Visible = True
            lbltotales.Visible = True

        Else
        End If
        '    gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(255, 255, 204)
        ''gridvadmProductos.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        ''gridvadmProductos.Items(UltimaFilaGA).Font.Bold = True
        '    gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10

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
        '    UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

        '    gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(255, 255, 204)
        ''gridvadmProductostotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        ''gridvadmProductostotales.Items(UltimaFilaGT).Font.Bold = True
        '    gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

        '    cmdexcelcm.Visible = True
        '    gridvadmProductos.Visible = True
        '    lblIMPLANTES.Visible = True
        ''lblcma.Visible = True
        ''lblpensiones.Visible = True
        '    lbltotales.Visible = True
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
