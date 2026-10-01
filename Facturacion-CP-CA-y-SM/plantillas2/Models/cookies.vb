Public Class cookies
    Inherits System.Web.UI.Page
    Private configuracion As String
    Private sesion As String
    Private skin As String
    Private contenido As String
    Private remember As String
    Private mihttpresponse As HttpResponse
    Private mihttprequest As HttpRequest
    ''aqui se setean las propiedades mediante las cuales accederemos a los datos almacenados en las cookies
    '''******* sesion 
    Public Property ssidsesion As String
        Get
            If Not mihttprequest.Cookies(sesion) Is Nothing Then
                Return mihttprequest.Cookies(sesion).Item("idsesion")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(sesion)("idsesion") = Value
        End Set
    End Property
    Public Property ssusuario As String

        Get
            If Not mihttprequest.Cookies(sesion) Is Nothing Then
                Return mihttprequest.Cookies(sesion).Item("usuario")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(sesion)("usuario") = Value
        End Set
    End Property
    Public Property ssidusuario As String

        Get
            If Not mihttprequest.Cookies(sesion) Is Nothing Then
                Return mihttprequest.Cookies(sesion).Item("idusuario")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(sesion)("idusuario") = Value
        End Set
    End Property
    Public Property ssrol As String

        Get
            If Not mihttprequest.Cookies(sesion) Is Nothing Then
                Return mihttprequest.Cookies(sesion).Item("Rol")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(sesion)("rol") = Value
        End Set
    End Property
    '''rememberme
    Public Property rememberpass As String

        Get
            If Not mihttprequest.Cookies(remember) Is Nothing Then
                Return mihttprequest.Cookies(remember).Item("pass")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(remember)("pass") = Value
        End Set
    End Property
    Public Property rememberuser As String

        Get
            If Not mihttprequest.Cookies(remember) Is Nothing Then
                Return mihttprequest.Cookies(remember).Item("usuario")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(remember)("usuario") = Value
        End Set
    End Property
    '''configuracion
    Public Property cnfempresa As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("empresa")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("empresa") = Value
        End Set
    End Property
    Public Property cnffacturacion As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("facturacion")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("facturacion") = Value
        End Set
    End Property
    Public Property cnfregistrooficial As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("registrooficial")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("registrooficial") = Value
        End Set
    End Property
    Public Property cnfcaja As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("caja")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("caja") = Value
        End Set
    End Property
    Public Property cnfpreconsulta As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("preconsulta")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("preconsulta") = Value
        End Set
    End Property
    Public Property cnftopbanner As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("banner")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("banner") = Value
        End Set
    End Property
    Public Property cnfpiepagina As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("piepagina")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("piepagina") = Value
        End Set
    End Property
    Public Property cnfrecetamedica As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("recetamedica")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("recetamedica") = Value
        End Set
    End Property
    Public Property cnflogin As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("login")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("login") = Value
        End Set
    End Property
    Public Property cnflogo As String
        Get
            If Not mihttprequest.Cookies(configuracion) Is Nothing Then
                Return mihttprequest.Cookies(configuracion).Item("logo")
            Else
                Return ""
            End If
        End Get
        Set(ByVal Value As String)
            mihttpresponse.Cookies(configuracion)("logo") = Value
        End Set
    End Property
    '''******* page 
    Public Property mirequest As HttpRequest
        Get
            Return mihttprequest
        End Get
        Set(ByVal Value As HttpRequest)
            mihttprequest = Value
        End Set
    End Property
    Public Property miresponse As HttpResponse
        Get
            Return mihttpresponse
        End Get
        Set(ByVal Value As HttpResponse)
            mihttpresponse = Value
        End Set
    End Property

    ' constructor para setear los nombres de las cookies
    Sub New()
        configuracion = "MDHospitalesCnf"
        sesion = "MDHospitalesSS"
        skin = "MDHospitalesSk"
        contenido = "MDHospitalesCnt"
        remember = "MDRememberme"
    End Sub
    '*** setear datos para cambios de pagina
    Public Sub setTransicion(ByVal Dato As String, ByVal valor As String)
        mihttpresponse.Cookies(contenido)(Dato) = valor
    End Sub
    '***obtener datos para cambio de pagina
    Public Function gettransicion(ByVal dato As String)
        If Not mihttprequest.Cookies(contenido) Is Nothing Then
            Return mihttprequest.Cookies(contenido).Item(dato)
        Else
            Return ""
        End If
    End Function
    '***elimina las cookies al cerrar la sesion
    Public Sub killsesion()
        mihttpresponse.Cookies(sesion).Expires = Date.Now.AddDays(-1)
        mihttpresponse.Cookies(skin).Expires = Date.Now.AddDays(-1)
        mihttpresponse.Cookies(configuracion).Expires = Date.Now.AddDays(-1)
    End Sub
    '*** elimina la cookie para recordar usuario
    Public Sub NotRememberme()
        mihttpresponse.Cookies(remember).Expires = Date.Now.AddDays(-1)
    End Sub
    '*** alarga la vida de la cookie para recordar usuario
    Public Sub Rememberme()
        mihttpresponse.Cookies(remember).Expires = Date.Now.AddDays(100)
    End Sub
End Class
