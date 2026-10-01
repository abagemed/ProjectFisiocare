
Imports System.Net
Imports System.Data

Partial Class expedientemedico
    Inherits System.Web.UI.Page
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not (Page.IsPostBack) Then
            'realizado.Visible = True
            'cancelado.Visible = False
            'misbotones.Visible = True
            'cmbTipoterapia.Visible = True
            'chkcierre.Visible = True
            'modatos.Visible = False
            If (Context.Items("elidCita") <> "") Then
                lblIdcita.Text = Context.Items("elidCita").ToString.Trim
                hfposicion.Text = Context.Items("posicion").ToString.Trim
                hfturno.Text = Context.Items("turno").ToString.Trim
                hfFechaAgenda.Text = Session("FechaAgenda")

            ElseIf (Context.Request("elidCita") <> "") Then
                lblIdcita.Text = Context.Request("elidCita")
                hfposicion.Text = Context.Request("posicion")
                hfturno.Text = Context.Request("turno")
                hfFechaAgenda.Text = Context.Request("fecha")

            End If

            ''lblIdcliente.Text =



        End If

        'Dim handlerUrl As String = "http://107.161.180.154/admonmaster/expedientemedico.ashx?name=expedientemedico"
        'Dim response As String = (New WebClient()).DownloadString(handlerUrl)

    End Sub

    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
        Server.Transfer("clientes.aspx", True)
    End Sub

    Protected Sub Recibos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub Historial(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub Catalogos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub CorteCaja(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub BloqueoCitas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
    End Sub

    Protected Sub FacturasGeneradas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub

    Protected Sub VerCancelados(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub
    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub
    Protected Sub lnkguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkguardar.Click
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub CTerapeutas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("ABCTerapeutas.aspx", False)
    End Sub

    Protected Sub Facturacion(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim url As String = "https://facturacioncp.agemed.com.mx/?" 'Prod
        'Dim url As String = "http://localhost:5952/login.aspx?" 'Dev
        Dim bd As String = "fisiocareCP"
        Dim idUsuario As String = Session("idusuario")
        Dim pass As String = ""
        obtenerPassword(pass, idUsuario)

        Response.Redirect(url + "idUsuario=" + idUsuario + "&bd=" + bd + "&ValR=" + pass)
    End Sub

    Private Sub obtenerPassword(ByRef pass As String, ByVal idUsuario As String)
        Dim claseDatos As New ClaseDatos
        Dim strSql As String
        Dim dt As New DataTable
        strSql = "SELECT password FROM usuarios WHERE idUsuario = '" + idUsuario + "';"

        If claseDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                pass = dt.Rows(0).Item("password")
            Else
                Messagebox1.ShowMessage("Ocurrió un error al consultar los datos del usuario")
            End If
        Else
            Messagebox1.ShowMessage(claseDatos.MensajeError)
        End If
    End Sub
End Class
