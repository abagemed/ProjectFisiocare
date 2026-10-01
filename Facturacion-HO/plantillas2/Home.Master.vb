Imports System.Object
Imports System.Web.UI.UpdatePanelTrigger
Imports System.Web.UI.UpdatePanelControlTrigger
Imports System.Web.UI.AsyncPostBackTrigger
Public Class Site1
    Inherits System.Web.UI.MasterPage

    Public _porcentaje As String
    Public _idAgenda As String
    Public _Ingresos As String

    Public Property contCanceladas() As String
        Get
            Return LblContCanceladas.Text
        End Get
        Set(valor As String)
            LblContCanceladas.Text = valor
        End Set
    End Property
    Public Property PacientesCancelados() As String
        Get
            Return ListBoxPacientesCancelados.Text
        End Get
        Set(value As String)
            ListBoxPacientesCancelados.Items.Add(New ListItem(value))
        End Set
    End Property

    Public Property contConfirmadas()
        Get
            Return LblContConfirmadas.Text
        End Get
        Set(valor)
            LblContConfirmadas.Text = valor
        End Set
    End Property

    Public Property contEnEspera()
        Get
            Return LblContEspera.Text
        End Get
        Set(value)
            LblContEspera.Text = value
        End Set
    End Property

    Public Property conEnEstudio()
        Get
            Return LblContEstudio.Text
        End Get
        Set(value)
            LblContEstudio.Text = value
        End Set
    End Property

    Public Property contAgendados()
        Get
            Return LblContAgendados.Text
        End Get
        Set(value)
            LblContAgendados.Text = value
        End Set
    End Property

    Public Property contFinalizados()
        Get
            Return LblContFinalizadas.Text
        End Get
        Set(value)
            LblContFinalizadas.Text = value
        End Set
    End Property

    Public Property sumaHonorarios()
        Get
            Return LblHonorarios.Text
        End Get
        Set(value)
            LblHonorarios.Text = value
        End Set
    End Property
    Public Property sumaInsumos()
        Get
            Return LblInsumos.Text
        End Get
        Set(value)
            LblInsumos.Text = value
        End Set
    End Property
    Public Property porcentajeMedicoMeta()
        Get
            Return LblPorcentajeMedicoMeta.Text
        End Get
        Set(value)
            LblPorcentajeMedicoMeta.Text = value
        End Set
    End Property

    Public Property porcentajeCompletadoMedico()
        Get
            Return LblPorcentajeCompletadoMedico.Text
        End Get
        Set(value)
            LblPorcentajeCompletadoMedico.Text = value
        End Set
    End Property

    Public Property porcentajeBarra()
        Get
            Return LblPorcentajeBarra.Text
        End Get
        Set(value)
            LblPorcentajeBarra.Text = value
        End Set
    End Property
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("sesion") = "" Then
            '    Response.Redirect("login.aspx")
            'Else
            'LblUsuario.Text = Session("sesion")
            'LblUsuarioBarraLateral.Text = Session("sesion")
            'LblUsuarioCuadro.Text = Session("sesion")
            'LblUsuarioPuesto.Text = Session("especialidad")
            '_idAgenda = Session("codigoConsultorio")
            'ImgUsuario1.ImageUrl = Session("foto")
            'ImgUsuario2.ImageUrl = Session("foto")
            'ImgUsuario3.ImageUrl = Session("foto")

            'LblUsuarioEmpresa.Text = Session("Empresa")

            'End If
        End If

    End Sub

    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        System.Web.Security.FormsAuthentication.SignOut()
        Session.Abandon()
        Response.Redirect("login.aspx")
    End Sub
    Public Sub borrarListaPacientesCancelados()
        ListBoxPacientesCancelados.Items.Clear()
    End Sub




End Class