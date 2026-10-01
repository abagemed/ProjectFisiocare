Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpadminProveedores

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
            sql += vbNewLine & "   DECLARE @FechaInicio varchar(30);  	"
            sql += vbNewLine & "DECLARE @FechaFin varchar(30);  	"
            sql += vbNewLine & "set @FechaInicio='" + txtfecha1.Text + " 00:00:00';"
            sql += vbNewLine & "set @FechaFin='" + txtfecha2.Text.Trim + " 12:00:00';"
            sql += vbNewLine & "	SELECT 	"
            sql += vbNewLine & " 	cast(CSERIEDOCUMENTO as VARCHAR)	"
            sql += vbNewLine & "	+cast(CFOLIO as VARCHAR)as SERIEFOLIO	"
            sql += vbNewLine & "	    ,FORMAT(CFECHA, 'dd-MM-yyyy', 'en-us') AS FECHA 	"
            sql += vbNewLine & " 	,FORMAT(CFECHAVENCIMIENTO, 'dd-MM-yyyy', 'en-us') AS FECHAVENCIMIENTO	"
            sql += vbNewLine & "	,D.CREFERENCIA AS FACTURA	"
            sql += vbNewLine & "	,D.CRAZONSOCIAL as Cliente	"
            sql += vbNewLine & "	,D.COBSERVACIONES  AS OBSERVACIONES	"
            sql += vbNewLine & "	,CONVERT(decimal(38,2), D.CDESCUENTOMOV)  as Descuento	"
            sql += vbNewLine & ",CONVERT(decimal(38,2),(D.CTOTAL)) as TotalDocumento	"
            sql += vbNewLine & "	,CONVERT(decimal(38,2),(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO)) as Abonos	"
            sql += vbNewLine & "	,(CASE WHEN D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            sql += vbNewLine & "	THEN 'PAGADO' 	"
            sql += vbNewLine & "	WHEN D.CTOTAL>(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            sql += vbNewLine & "	THEN 'Falta por Pagar '+convert(nvarchar(max),(CONVERT(decimal(38,2),(D.CTOTAL-(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) ))))	"
            sql += vbNewLine & "  Else 'No se Encontro Pago' END) as STATUS	"
            sql += vbNewLine & "   	FROM admDocumentos D	"
            sql += vbNewLine & "	WHERE D.CSERIEDOCUMENTO='S'  and cfecha>=@FechaInicio and cfecha<=@FechaFin "
            'sql += vbNewLine & "	AND D.CCANCELADO=0  AND (CASE WHEN D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            'sql += vbNewLine & "	THEN 'PAGADO' 	"
            'sql += vbNewLine & "	WHEN D.CTOTAL>(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            'sql += vbNewLine & "	THEN 'Falta por Pagar '+convert(nvarchar(max),(CONVERT(decimal(38,2),(D.CTOTAL-(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) ))))	"
            'sql += vbNewLine & " Else 'No se Encontro Pago' END)<>'PAGADO'"
            sql += vbNewLine & "	OR D.CSERIEDOCUMENTO='A'  and cfecha>=@FechaInicio and cfecha<=@FechaFin"
            'sql += vbNewLine & "	 AND D.CCANCELADO=0   	"
            'sql += vbNewLine & "	AND (CASE WHEN D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            'sql += vbNewLine & "	THEN 'PAGADO' 	"
            'sql += vbNewLine & "	WHEN D.CTOTAL>(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) 	"
            'sql += vbNewLine & "	THEN 'Falta por Pagar '+convert(nvarchar(max),(CONVERT(decimal(38,2),(D.CTOTAL-(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOABONO=D.CIDDOCUMENTO) ))))	"
            'sql += vbNewLine & " Else 'No se Encontro Pago' END)	<>'PAGADO'"
            sql += vbNewLine & "	  ORDER BY CSERIEDOCUMENTO	"



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
