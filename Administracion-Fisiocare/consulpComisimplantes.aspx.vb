Imports System.Data
Imports System.Data.SqlClient

Partial Class consulpComisimplantes
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then

            Dim sql As String
            Dim sql4 As String

           

            sql += vbNewLine & "DECLARE @FechaInicio varchar(30);"
            sql += vbNewLine & "DECLARE @FechaFin varchar(30);"
            sql += vbNewLine & "set @FechaInicio='" + txtfecha1.Text + " 00:00:00';"
            sql += vbNewLine & "set @FechaFin='" + txtfecha2.Text.Trim + " 12:00:00';"
            sql += vbNewLine & "SELECT  FORMAT(CA.CFECHAABONOCARGO, 'dd-MM-yyyy', 'en-us') AS FECHA , D.CFOLIO AS FACTURA, D.CSERIEDOCUMENTO"
            sql += vbNewLine & ",Cli.CRAZONSOCIAL as Cliente"
            sql += vbNewLine & ",(0) as Clasificacion"
            sql += vbNewLine & ",CONVERT(decimal(38,2), D.CNETO)AS IMPORTE"
            sql += vbNewLine & ",CONVERT(decimal(38,2), D.CDESCUENTOMOV)  as Descuento"
            sql += vbNewLine & ",(CONVERT(decimal(38,2), D.CNETO-CDESCUENTOMOV)) AS VENTANETA"
            sql += vbNewLine & ",Convert(decimal(38,2), (D.CNETO-CDESCUENTOMOV-(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END))) as Almacen	"
            sql += vbNewLine & ",Convert(decimal(38,2),(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END)) as Almacen2	"
            sql += vbNewLine & ",CONVERT(DECIMAL(38,2),((CONVERT(decimal(38,2), D.CNETO)-(CDESCUENTOMOV))+(CONVERT(decimal(38,2), D.CDESCUENTOMOV))-(CASE WHEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7)<>'' THEN (SELECT  SUM((CNETO-CDESCUENTO1)) from admMovimientos WHERE CIDDOCUMENTO=CA.CIDDOCUMENTOCARGO AND CIDALMACEN=7) ELSE 0 END))*0.56) as Compra"
            sql += vbNewLine & ",CONVERT(decimal(38,2), Round(((D.CNETO-CDESCUENTOMOV )*0.03),2)) AS COMISIONAGENTE"
            sql += vbNewLine & ",CONVERT(decimal(38,2),(D.CTOTAL)) as TotalDocument"
            sql += vbNewLine & ",CONVERT(decimal(38,2),(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)) as AbonosPagados"
            sql += vbNewLine & ",agt.CNOMBREAGENTE"
            sql += vbNewLine & ",D.CREFERENCIA"
            sql += vbNewLine & ",D.COBSERVACIONES"
            sql += vbNewLine & ",CONVERT(decimal(38,2),(SELECT MIN(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)) as ULTIMOPAGO"
            sql += vbNewLine & ",(CASE WHEN D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "THEN 'PAGADO' ELSE 'Falta por Pagar' END) as STATUS"
            sql += vbNewLine & ",CASE WHEN  CSERIEDOCUMENTO='C' THEN (SELECT CNOMBRECUENTA FROM admCuentasBancarias where CIDCUENTA=(SELECT CIDCUENTA FROM admDocumentos where admDocumentos.CIDDOCUMENTO=CA.CIDDOCUMENTOABONO))  ELSE (SELECT CNOMBRECUENTA FROM admCuentasBancarias where CIDCUENTA=(SELECT CIDCUENTA FROM admDocumentos where admDocumentos.CIDDOCUMENTO=CA.CIDDOCUMENTOABONO)) END AS CIDCUENTABANCARIA"
            sql += vbNewLine & "FROM admDocumentos D"
            sql += vbNewLine & "inner join admAsocCargosAbonos CA on ca.CIDDOCUMENTOCARGO = D.CIDDOCUMENTO and CFECHAABONOCARGO=(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "left join admAgentes agt on agt.CIDAGENTE=d.CIDAGENTE"
            sql += vbNewLine & "left join admCuentasBancarias cb on D.CIDDOCUMENTO=cb.CIDCUENTA"
            sql += vbNewLine & "INNER JOIN admClientes Cli on D.CIDCLIENTEPROVEEDOR=Cli.CIDCLIENTEPROVEEDOR"
            sql += vbNewLine & "where CSERIEDOCUMENTO='C'   AND"
            sql += vbNewLine & "(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)>=@FechaInicio"
            sql += vbNewLine & "AND (SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)<=@FechaFin"
            sql += vbNewLine & "AND D.CCANCELADO=0 and D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "OR CSERIEDOCUMENTO='IN'  AND"
            sql += vbNewLine & "(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)>=@FechaInicio"
            sql += vbNewLine & "AND (SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)<=@FechaFin"
            sql += vbNewLine & "AND D.CCANCELADO=0 and D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "OR CSERIEDOCUMENTO='PS'  AND"
            sql += vbNewLine & "(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)>=@FechaInicio"
            sql += vbNewLine & "AND (SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)<=@FechaFin"
            sql += vbNewLine & "AND D.CCANCELADO=0 and D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "OR CSERIEDOCUMENTO='ME'  AND"
            sql += vbNewLine & "(SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)>=@FechaInicio"
            sql += vbNewLine & "AND (SELECT MAX(CFECHAABONOCARGO) From admAsocCargosAbonos where CIDDOCUMENTOCARGO=CA.CIDDOCUMENTOCARGO)<=@FechaFin"
            sql += vbNewLine & "AND D.CCANCELADO=0 and D.CTOTAL=(SELECT SUM(CIMPORTECARGO) FROM admAsocCargosAbonos where CIDDOCUMENTOCARGO=ca.CIDDOCUMENTOCARGO)"
            sql += vbNewLine & "order by  CFOLIO asc"
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
            sql4 += vbNewLine & "WHERE admMovimientos.CFECHA>='" + txtfecha1.Text + " 00:00:00' and admMovimientos.CFECHA<='" + txtfecha2.Text.Trim + " 12:00:00' and admMovimientos.CIDDOCUMENTODE=4 and (admDocumentos.CSERIEDOCUMENTO='C') and admDocumentos.CCANCELADO=0"
            gridvadmProductostotales = funciones.LLenaGridC(sql4, gridvadmProductostotales, hfSucursal.Value)




        Else

        End If

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = gridvadmProductos.Items.Count - 1
        If UltimaFilaGA > 1 Then
            gridvadmProductos.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(255, 255, 204)

            gridvadmProductos.Items(UltimaFilaGA).Font.Size = 10


            Dim UltimaFilaGT As Integer
            UltimaFilaGT = gridvadmProductostotales.Items.Count - 1

            gridvadmProductostotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(255, 255, 204)

            gridvadmProductostotales.Items(UltimaFilaGT).Font.Size = 10

            cmdexcelcm.Visible = True
            gridvadmProductos.Visible = True
            lblIMPLANTES.Visible = True

            lbltotales.Visible = True

        Else
        End If

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
