Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpadminreferencia

    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String

            


            'sql = " select CSERIEDOCUMENTO as SERIE, CFOLIO AS FOLIO, CFECHA AS FECHA, CRAZONSOCIAL AS RAZONSOCIAL, CRFC AS RFC, CREFERENCIA AS REFERENCIA, COBSERVACIONES AS OBSERVACIONES,CNETO AS NETO, CIMPUESTO1 AS IMPUESTO, CDESCUENTOMOV AS DESCUENTO, "
            'sql += vbNewLine & "CTOTAL AS TOTAL"
            'sql += vbNewLine & "from admDocumentos"
            'sql += vbNewLine & "WHERE admDocumentos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admDocumentos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admDocumentos.CIDDOCUMENTODE=4 and admDocumentos.CSERIEDOCUMENTO='C' and admDocumentos.CCANCELADO=0 and admDocumentos.CDEVUELTO=0"
            'sql += vbNewLine & "order by CFOLIO"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)

            'sql = "DECLARE @FechaInicio varchar(30);"
            'sql += vbNewLine & "DECLARE @FechaFin varchar(30);"
            'sql += vbNewLine & "set @FechaInicio='" + txtfecha1.Text + " 00:00:00';"
            'sql += vbNewLine & "set @FechaFin='" + txtfecha2.Text.Trim + " 12:00:00';"
            'sql += vbNewLine & "select CSERIEDOCUMENTO as SERIE, CFOLIO AS FOLIO, CFECHA AS FECHA, CRAZONSOCIAL AS RAZONSOCIAL, CRFC AS RFC,"
            'sql += vbNewLine & "CREFERENCIA AS REFERENCIA, COBSERVACIONES AS OBSERVACIONES,CNETO AS NETO, CIMPUESTO1 AS IMPUESTO, CDESCUENTOMOV AS DESCUENTO,"
            'sql += vbNewLine & "CTOTAL AS TOTAL"
            'sql += vbNewLine & ",(CASE WHEN (select SUM(cneto) from admDocumentos where CSERIEDOCUMENTO='AC'"
            'sql += vbNewLine & "and CIDCLIENTEPROVEEDOR=Doc.CIDCLIENTEPROVEEDOR  AND CFECHA>=@FechaInicio and CFECHA<=@FechaFin) <>0"
            'sql += vbNewLine & "THEN (select SUM(cneto) from admDocumentos where CSERIEDOCUMENTO='AC'"
            'sql += vbNewLine & "and CIDCLIENTEPROVEEDOR=Doc.CIDCLIENTEPROVEEDOR  AND CFECHA>=@FechaInicio and CFECHA<=@FechaFin)"
            'sql += vbNewLine & "ELSE (0.00) END )as  ANTICIPOS"
            'sql += vbNewLine & ",Doc.CIDCLIENTEPROVEEDOR"
            'sql += vbNewLine & "from admDocumentos as Doc"
            'sql += vbNewLine & "WHERE Doc.CFECHA>=@FechaInicio and Doc.CFECHA<=@FechaFin"
            'sql += vbNewLine & "and Doc.CIDDOCUMENTODE=4 and Doc.CSERIEDOCUMENTO='C' and Doc.CCANCELADO=0 and Doc.CDEVUELTO=0"
            'sql += vbNewLine & "order by CFOLIO"
            'gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)
            sql += vbNewLine & "	DECLARE @FechaInicio varchar(30);  	"
            sql += vbNewLine & "	DECLARE @FechaFin varchar(30);  	"
            sql += vbNewLine & "set @FechaInicio='" + txtfecha1.Text + " 00:00:00';"
            sql += vbNewLine & "set @FechaFin='" + txtfecha2.Text.Trim + " 12:00:00';"
            sql += vbNewLine & "	select CSERIEDOCUMENTO as SERIE, CFOLIO AS FOLIO, CAST(FORMAT(cfecha, 'dd-MM-yyyy') as varchar)AS FECHA, CRAZONSOCIAL AS RAZONSOCIAL, CRFC AS RFC, 	"
            sql += vbNewLine & "	CREFERENCIA AS REFERENCIA, COBSERVACIONES AS OBSERVACIONES,CAST(Doc.CNETO AS decimal(16,2)) AS 'NETO',	"
            sql += vbNewLine & "	  CAST(Doc.CIMPUESTO1 AS decimal(16,2)) AS 'IMPUESTO',	"
            sql += vbNewLine & "	   CAST(Doc.CDESCUENTOMOV AS decimal(16,2)) AS 'DESCUENTO',	"
            sql += vbNewLine & "	     CAST(Doc.CTOTAL AS decimal(16,2)) AS 'TOTAL'	"
            sql += vbNewLine & "	            ,(CASE WHEN (select SUM(cneto) from admDocumentos where CSERIEDOCUMENTO='AC' 	"
            sql += vbNewLine & "	            and CIDCLIENTEPROVEEDOR=Doc.CIDCLIENTEPROVEEDOR and CIDCLIENTEPROVEEDOR<>4)<>0 	"
            sql += vbNewLine & "	            THEN (select SUM(cneto) from admDocumentos where CSERIEDOCUMENTO='AC' and CIDCLIENTEPROVEEDOR=Doc.CIDCLIENTEPROVEEDOR and CIDCLIENTEPROVEEDOR<>4)	"
            sql += vbNewLine & "	ELSE (0.00) END )as  ABONOS	"
            sql += vbNewLine & "		"
            sql += vbNewLine & "	from admDocumentos as Doc	"
            sql += vbNewLine & "	WHERE Doc.CFECHA>=@FechaInicio and Doc.CFECHA<=@FechaFin	"
            sql += vbNewLine & "	and Doc.CIDDOCUMENTODE=4 and Doc.CSERIEDOCUMENTO='C' and Doc.CCANCELADO=0 and Doc.CDEVUELTO=0	"
            sql += vbNewLine & "	union all	"
            sql += vbNewLine & "	select 	"
            sql += vbNewLine & "	  CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	     CFolio ,	"
            sql += vbNewLine & "	   CAST(FORMAT(cfecha, 'dd-MM-yyyy') as varchar)AS FECHA,	"
            sql += vbNewLine & "	        CRAZONSOCIAL AS RAZONSOCIAL, CRFC AS RFC, 	"
            sql += vbNewLine & "	    '' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	    '' as Folio ,	"
            sql += vbNewLine & "	     CAST(Doc.CNETO AS decimal(16,2)) AS 'NETO',	"
            sql += vbNewLine & "	  CAST(Doc.CIMPUESTO1 AS decimal(16,2)) AS 'IMPUESTO',	"
            sql += vbNewLine & "	   CAST(Doc.CDESCUENTOMOV AS decimal(16,2)) AS 'DESCUENTO',	"
            sql += vbNewLine & "	     CAST(Doc.CTOTAL AS decimal(16,2)) AS 'TOTAL',	"
            sql += vbNewLine & "	    '' as CSERIEDOCUMENTO	"
            sql += vbNewLine & "	 from admDocumentos as Doc	"
            sql += vbNewLine & "	where CSERIEDOCUMENTO='AC' and CFECHA>=@FechaInicio and CFECHA<=@FechaFin and Doc.CCANCELADO=0 and Doc.CCANCELADO=0 and Doc.CDEVUELTO=0	"
            sql += vbNewLine & "	    union all 	"
            sql += vbNewLine & "	 select 	"
            sql += vbNewLine & "	  'Ciente #Abonos' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	 Count(Cfolio) as Folio 	"
            sql += vbNewLine & "	     ,STUFF(	"
            sql += vbNewLine & "	(SELECT ','+cast(CONVERT(decimal(38,2), CNETO) as varchar)	"
            sql += vbNewLine & "	,'-('+CAST(FORMAT(cfecha, 'dd-MM-yyyy') as varchar)+')/'	"
            sql += vbNewLine & "	    FROM admDocumentos	"
            sql += vbNewLine & "	    where CSERIEDOCUMENTO='AC'and CIDCLIENTEPROVEEDOR=Doc.CIDCLIENTEPROVEEDOR and CFECHA>=@FechaInicio and CFECHA<=@FechaFin	"
            sql += vbNewLine & "	    FOR XML PATH('')),	"
            sql += vbNewLine & "	    1, 1, '') as P2,	"
            sql += vbNewLine & "	       CRAZONSOCIAL AS RAZONSOCIAL,	"
            sql += vbNewLine & "	      CRFC AS RFC, 	"
            sql += vbNewLine & "	    '' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	    '' as Folio ,	"
            sql += vbNewLine & "	   CAST(SUM(Doc.CNETO) AS decimal(16,2)) AS 'NETO',	"
            sql += vbNewLine & "	  CAST(SUM(Doc.CIMPUESTO1) AS decimal(16,2)) AS 'IMPUESTO',	"
            sql += vbNewLine & "	   CAST(SUM(Doc.CDESCUENTOMOV) AS decimal(16,2)) AS 'DESCUENTO',	"
            sql += vbNewLine & "	     CAST(SUM(Doc.CTOTAL) AS decimal(16,2)) AS 'TOTAL',	"
            sql += vbNewLine & "	  '' as CSERIEDOCUMENTO	"
            sql += vbNewLine & "	 from admDocumentos as Doc	"
            sql += vbNewLine & "	where CSERIEDOCUMENTO='AC' and CFECHA>=@FechaInicio and CFECHA<=@FechaFin and Doc.CCANCELADO=0 	"
            sql += vbNewLine & "	group by Doc.CIDCLIENTEPROVEEDOR,Doc.CRAZONSOCIAL,Doc.CRFC	"
            sql += vbNewLine & "		"
            sql += vbNewLine & "	UNION ALL 	"
            sql += vbNewLine & "	   select '# Abonos' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	   Count(Cfolio) as Folio ,	"
            sql += vbNewLine & "	   'Totales Finales' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	   '' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	   '' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	    '' as Folio ,	"
            sql += vbNewLine & "	    '' as CSERIEDOCUMENTO,	"
            sql += vbNewLine & "	  CAST(SUM(Doc.CNETO) AS decimal(16,2)) AS 'NETO',	"
            sql += vbNewLine & "	  CAST(SUM(Doc.CIMPUESTO1) AS decimal(16,2)) AS 'IMPUESTO',	"
            sql += vbNewLine & "	   CAST(SUM(Doc.CDESCUENTOMOV) AS decimal(16,2)) AS 'DESCUENTO',	"
            sql += vbNewLine & "	     CAST(SUM(Doc.CTOTAL) AS decimal(16,2)) AS 'TOTAL',	"
            sql += vbNewLine & "	    '' as Folio 	"
            sql += vbNewLine & "	 from admDocumentos as Doc	"
            sql += vbNewLine & "	where CSERIEDOCUMENTO='AC' and CFECHA>=@FechaInicio and CFECHA<=@FechaFin and Doc.CCANCELADO=0 and Doc.CDEVUELTO=0	"

            gridvadmProductos = funciones.LLenaGridC(sql, gridvadmProductos, hfSucursal.Value)


        Else

        End If

        cmdexcelcm.Visible = True
        gridvadmProductos.Visible = True
        lblIMPLANTES.Visible = True
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
        form.Controls.Add(lblIMPLANTES)
        form.Controls.Add(gridvadmProductos)
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
