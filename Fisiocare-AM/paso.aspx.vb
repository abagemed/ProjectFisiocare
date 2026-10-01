
Partial Class paso
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Context.Items.Add("idcita", Request.QueryString("idcita").ToString.Trim)
        'Context.Items.Add("varidterapia", Request.QueryString("varidterapia").ToString.Trim)
        Context.Items.Add("varidcliente", Request.QueryString("varidcliente").ToString.Trim)
        'Context.Items.Add("varimporte", Request.QueryString("varimporte").ToString.Trim)
        Context.Items.Add("varfecha", Request.QueryString("varfecha").ToString.Trim)
        'Context.Items.Add("varabono", Request.QueryString("varabono").ToString.Trim)
        Context.Items.Add("fecha", Request.QueryString("fecha").ToString.Trim)
        Context.Items.Add("factura", Request.QueryString("factura").ToString.Trim)
        Context.Items.Add("pagina", Request.QueryString("pagina").ToString.Trim)
        'Context.Items.Add("val1", Request.QueryString("val1").ToString.Trim)
        'Context.Items.Add("val2", Request.QueryString("val2").ToString.Trim)
        'Context.Items.Add("val3", Request.QueryString("val3").ToString.Trim)
        Server.Transfer("iRecibo.aspx", True)
    End Sub
End Class
