Imports Microsoft.VisualBasic
Imports System
Imports System.Data
Imports System.Configuration
Imports System.Xml
Imports System.Web
Imports System.Web.Security
Imports System.Web.UI
Imports System.Web.UI.HtmlControls
Imports System.Web.UI.WebControls
Imports System.Web.UI.WebControls.WebParts
Imports System.Xml.XmlLinkedNode
Public Class classPost
    Dim _Inputs As New System.Collections.Specialized.NameValueCollection()
    Dim _Url As String = ""
    Dim _Method As String = "post"
    Dim _FormName As String = "form1"

    Public Property url() As String
        Get
            Return _Url
        End Get
        Set(ByVal value As String)
            _Url = value
        End Set
    End Property
    Public Sub add(ByVal name As String, ByVal value As String)
        _Inputs.Add(name, value)
    End Sub
    Public Sub post()
        System.Web.HttpContext.Current.Response.Clear()
        System.Web.HttpContext.Current.Response.Write("")
        System.Web.HttpContext.Current.Response.Write(String.Format("<html><head></head><body onload=\'document.{0}.submit()\'>", _FormName))
        System.Web.HttpContext.Current.Response.Write(String.Format("<center><form target=\'_blank\' name=\'{0}\' method=\'{1}\' action=\'{2}\' >", _FormName, _Method, url))
        Dim i = 0
        For i = 0 To i < _Inputs.Keys.Count
            System.Web.HttpContext.Current.Response.Write(String.Format("<input name=\'{0}\' type=\'hidden\' value=\'{1}\'>", _Inputs.Keys(i), _Inputs(_Inputs.Keys(i))))
            System.Web.HttpContext.Current.Response.Write("</form></center></body></html>")
            i = i + 1
        Next
    End Sub
End Class
