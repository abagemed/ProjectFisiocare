Imports System.Data.SqlClient
Imports System.Data

Partial Class canceladas
    Inherits System.Web.UI.Page
    Sub llenaGrid()
        Dim misfunciones As New miclases
        misfunciones.creadataset("select agenda.idcita,clientes.elnombre from agenda inner join " & _
        "clientes on clientes.idcliente=agenda.idcliente where agenda.estado='CANCELADO' and agenda.fecha='" + lblfecha.Text.Trim + "'", gridCancelaciones)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
                seccion.Visible = False
                llenaGrid()
            Catch ex As Exception
                Server.Transfer("default.aspx", False)
            End Try
        End If
    End Sub

    Protected Sub gridCancelaciones_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCancelaciones.ItemCommand
        Dim misfunciones As New miclases
        Dim conexion As SqlConnection = misfunciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        comando.CommandText = "select cancelacion from cancelaciones where idcita='" + gridCancelaciones.DataKeys(e.Item.ItemIndex).ToString.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            seccion.Visible = True
            txtcancelacion.Value = leer.GetValue(0).ToString.Trim
        End If
        leer.Close()
        conexion.Close()
    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
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
