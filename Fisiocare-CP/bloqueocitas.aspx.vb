Imports System.Data
Imports System.Data.SqlClient

Partial Class bloqueocitas
    Inherits System.Web.UI.Page

    Dim sqlComando As SqlCommand
    Dim sqlAdaptador As SqlDataAdapter
    Dim tmpTabla As New DataTable


    Public clase As New miclases

    Dim Funciones As New miclases
    Dim Cn As SqlConnection = Funciones.conecta
    Dim adap As SqlDataAdapter
    Dim cmd As SqlCommand
    Dim SQL As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim funciones As New miclases
        If Not (Page.IsPostBack) Then
            cbodoctores = funciones.llenacombos(cbodoctores, "SELECT columna, nombre, turno FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")
            cbodoctores.SelectedIndex = 1
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
                Dim lafecha As Date = lblfecha.Text
                lblFechaNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                calendario.SelectedDate = lblfecha.Text
            Catch ex As Exception
                Try
                    Dim usuario As String = Context.Items("idusuario").ToString
                    Dim hoy As DateTime = DateTime.Now()
                    lblfecha.Text = String.Format("{0:dd/MM/yyyy}", hoy)
                    Dim lafecha As Date = lblfecha.Text
                    lblFechaNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                    calendario.SelectedDate = lblfecha.Text
                Catch
                    Response.Redirect("login.aspx")
                End Try
            End Try
            llenaagenda()
        End If
    End Sub
    Protected Sub cboturno_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboturno.SelectedIndexChanged


        cbodoctores = funciones.llenacombos(cbodoctores, "SELECT columna, nombre, turno FROM Terapistas where activo='True' and turno='" & cboturno.SelectedValue & "' order by columna")
        cbodoctores.SelectedIndex = 1
        llenaagenda()

    End Sub
    Sub llenaagenda()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim cadena As String = ""
        Dim i, posicion As Integer
        Dim fila As Integer = 0
        gridHorarios.Visible = True
        gridHorarios = funciones.creadataset("select horario,idhorario from horarios where turno='" & cboturno.SelectedValue & "'", gridHorarios)
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "agenda.idCliente, "
        SQL = SQL & "agenda.idHorario, "
        SQL = SQL & "agenda.fecha, "
        SQL = SQL & "agenda.duracion, "
        SQL = SQL & "clientes.elnombre, "
        SQL = SQL & "agenda.idcita "
        SQL = SQL & "FROM agenda "
        SQL = SQL & "INNER JOIN clientes ON agenda.idcliente = clientes.idcliente "
        SQL = SQL & "WHERE agenda.fecha = '" & lblfecha.Text.Trim & "' AND "
        SQL = SQL & "posicion='" & cbodoctores.SelectedValue & "' and (agenda.estado = 'AGENDADO' or agenda.estado ='REALIZADO') and turno='" & cboturno.SelectedValue & "'  "
        SQL = SQL & "order by agenda.idhorario"

        'MsgBox(SQL)

        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        Do While leer.Read
            fila = CInt(leer.GetValue(1).ToString.Trim) - 1
            CType(gridHorarios.Items(fila).Cells(2).Controls(1), Label).Text = Left(leer.GetValue(4).ToString.Trim, 30)
            gridHorarios.Items(fila).Cells(3).Text = leer.GetValue(1).ToString.Trim
            CType(gridHorarios.Items(fila).Cells(2).Controls(1), Label).ForeColor = Drawing.Color.DarkGreen
            If leer.GetValue(3) > 1 Then
                Dim cnt As Integer = leer.GetValue(3)
                While cnt > 1
                    cnt = cnt - 1
                    CType(gridHorarios.Items(fila + cnt).Cells(2).Controls(1), Label).Text = Left(leer.GetValue(4).ToString.Trim, 30)
                    gridHorarios.Items(fila + cnt).Cells(3).Text = leer.GetValue(1).ToString.Trim
                    CType(gridHorarios.Items(fila + cnt).Cells(2).Controls(1), Label).ForeColor = Drawing.Color.Black
                    CType(gridHorarios.Items(fila + cnt).Cells(1).Controls(1), CheckBox).Enabled = False
                End While
            End If

            If leer.GetValue(4).ToString.Trim <> "DISPONIBLE" Then
                CType(gridHorarios.Items(fila).Cells(1).Controls(1), CheckBox).Enabled = False
            End If
        Loop
        leer.Close()
        conexion.Close()

        '** se llenan los bloqueados
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' AND "
        SQL = SQL & "columna='" & cbodoctores.SelectedValue & "' and turno='" & cboturno.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadena = leer.GetValue(2).ToString.Trim
            If cadena.Trim <> "" Then
                For i = 1 To funciones.num_elementos(cadena, "|")
                    posicion = funciones.entry(i, cadena, "|") - 1
                    CType(gridHorarios.Items(posicion).Cells(2).Controls(1), Label).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicion).Cells(2).Controls(1), Label).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(2).Controls(1), LinkButton).Enabled = False
                    'CType(gridHorarios.Items(posicion).Cells(2).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()
    End Sub
    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("usuario", hfUsuario.Value)
        Context.Items.Add("fecha", lblfecha.Text)
        Server.Transfer("default.aspx")
    End Sub

    Protected Sub cmdImprimir_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdImprimir.Click
        Dim tRegistros As New DataTable
        Dim IdExploracion As String
        Dim cad As String = ""
        SQL = ""
        SQL = "SELECT bloqueos FROM Bloqueos WHERE fecha='" + lblfecha.Text + "' and columna = '" + cbodoctores.SelectedValue.Trim + "' and turno='" & cboturno.SelectedValue & "'  "
        sqlComando = New SqlCommand(SQL, Cn)
        sqlAdaptador = New SqlDataAdapter(sqlComando)
        sqlAdaptador.Fill(tRegistros)

        SQL = ""
        IdExploracion = ""
        cad = obtenmarcados()
        If tRegistros.Rows.Count > 0 Then
            SQL = "UPDATE Bloqueos SET bloqueos='" & cad.Trim & "' "
            SQL = SQL & "WHERE fecha='" & lblfecha.Text & "' AND columna='" & cbodoctores.SelectedValue.Trim & "' and turno='" & cboturno.SelectedValue & "' "
        Else
            SQL = "INSERT INTO Bloqueos ("
            SQL = SQL & "fecha, columna, bloqueos,turno "
            SQL = SQL & ") VALUES ('"
            SQL = SQL & lblfecha.Text & "', '" & cbodoctores.SelectedValue.Trim & "',"
            SQL = SQL & " '" & cad.Trim & "', '" & cboturno.SelectedValue.Trim & "'"
            SQL = SQL & ")"
        End If
        'MsgBox(obtenmarcados())

        tRegistros = Nothing
        tRegistros = New DataTable
        sqlComando = New SqlCommand(SQL, Cn)
        sqlAdaptador = New SqlDataAdapter(sqlComando)
        sqlAdaptador.Fill(tRegistros)

        Context.Items.Add("usuario", hfUsuario.Value)
        Context.Items.Add("fecha", lblfecha.Text)
        Server.Transfer("default.aspx")
    End Sub
    Function obtenmarcados() As String
        Dim cadena As String = ""
        Dim cuenta As Int16 = gridHorarios.Items.Count
        Dim contador As Int16 = 0
        Do While contador < cuenta
            If CType(gridHorarios.Items(contador).Cells(1).Controls(1), CheckBox).Checked = True Then
                cadena = cadena + gridHorarios.Items(contador).Cells(3).Text.Trim + "|"
            End If
            contador = contador + 1
        Loop
        If cadena.Trim <> "" Then
            cadena = Left(cadena.Trim, Len(cadena.Trim) - 1)
        End If
        Return cadena
    End Function

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        Try
            Dim lafecha As Date = calendario.SelectedDate
            lblFechaNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
            lblfecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha)
            gridHorarios = Funciones.creadataset("select horario,idhorario from horarios where turno='" & cboturno.SelectedValue & "'", gridHorarios)
            llenaagenda()
        Catch
            Messagebox1.ShowMessage("Datos Incorrectos...")
        End Try
    End Sub

    Protected Sub lblFechaNom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lblFechaNom.Click
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub CheckBox2_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles chkSelecciona.CheckedChanged
        Dim elementos As Integer = gridHorarios.Items.Count
        Dim cuenta As Integer = 0
        Do While cuenta < elementos
            If CType(gridHorarios.Items(cuenta).Cells(1).Controls(1), CheckBox).Enabled = True Then
                CType(gridHorarios.Items(cuenta).Cells(1).Controls(1), CheckBox).Checked = chkSelecciona.Checked
            End If
            cuenta = cuenta + 1
        Loop
    End Sub

    Protected Sub cbodoctores_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cbodoctores.SelectedIndexChanged
        llenaagenda()
        chkSelecciona.Checked = False
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
