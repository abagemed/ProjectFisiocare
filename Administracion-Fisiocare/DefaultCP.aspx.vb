Imports System.Data
Imports System.Data.SqlClient
Partial Class DefaultCP
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim color As Integer
    Dim lasfunciones As New Funciones
    Dim funciones As New miclases
    Sub estableceColor(ByVal fila As String, ByVal posicion As String, ByVal miestado As String)
        'color = color + 10
        'If color >= 50 Then
        ' color = 0
        'End If
        Select Case miestado
            Case "AGENDADO"
                'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)
                'gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(200 + color, 0, 0)

                'solo letras
                CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)
                'gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(255, 160, 160)

            Case "REALIZADO"
                'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)
                'gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(46 + color, 137 + color, 46)

                'solo letras
                CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)
                'gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(20, 210, 20)

        End Select
    End Sub

    Sub llenaAgenda()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta("fisiocareCP")
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim bandera As Boolean = True
        Dim cadregistros As String
        Dim cadenafin As String
        Dim cad, cnombre, cita, estatus, duracion As String
        Dim i, val1, val2 As Integer

        lblCuantos.Text = 0
        comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        " clientes.nombre+' '+clientes.paterno+' '+clientes.materno as elnombre, agenda.estado, agenda.idCita from agenda " & _
        "inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        "and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') order by agenda.idhorario"
        'comando.Parameters.Add(New SqlParameter("@fecha", SqlDbType.DateTime))
        'comando.Parameters("@fecha").Value = lblfecha.Text.Trim
        conexion.Open()
        leer = comando.ExecuteReader
        cadregistros = ""
        Do While leer.Read
            cadregistros = cadregistros & leer.GetValue(1).ToString.Trim & "-" & leer.GetValue(4).ToString & "-" & leer.GetValue(3).ToString & "-" & Left(leer.GetValue(5).ToString.Trim, 30) & "-" & leer.GetValue(7).ToString & "-" & leer.GetValue(6).ToString & "-" & leer.GetValue(0).ToString & ","

            'CType(gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(1), LinkButton).Text = Left(leer.GetValue(5).ToString.Trim, 30)
            'CType(gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(3), Label).Text = leer.GetValue(7).ToString.Trim
            'gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString.Trim).BackColor = Drawing.Color.FromArgb(128, 0, 0)
            'estableceColor(leer.GetValue(3).ToString.Trim, leer.GetValue(4).ToString.Trim, leer.GetValue(6).ToString.Trim)
            ' lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1

            lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1
        Loop
        leer.Close()
        conexion.Close()
        If cadregistros.ToString.Trim <> "" Then
            cadregistros = Mid(cadregistros, 1, cadregistros.Length - 1)
            cadenafin = lasfunciones.completalista2(cadregistros, "1")
            ' SE LLENA EL GRID CON LOS OCUPADOS
            cnombre = ""
            Dim cIdcita As String = ""
            i = 0
            For i = 1 To funciones.num_elementos(cadenafin, ",")
                ''cadenafin se compone de idhorario,posicion,duracion,nombre,idcita,status,idCliente
                cad = funciones.entry(i, cadenafin, ",")
                val1 = CInt(funciones.entry(1, cad, "-").ToString.Trim)
                val2 = CInt(funciones.entry(2, cad, "-").ToString.Trim)

                If cIdcita = funciones.entry(7, cad, "-").ToString.Trim Then
                    cnombre = ""
                Else
                    cnombre = funciones.entry(4, cad, "-").ToString.Trim
                End If
                cIdcita = funciones.entry(7, cad, "-").ToString.Trim
                'cnombre = funciones.entry(4, cad, "-").ToString.Trim

                cita = funciones.entry(5, cad, "-").ToString.Trim
                estatus = funciones.entry(6, cad, "-").ToString.Trim
                duracion = funciones.entry(3, cad, "-").ToString.Trim

                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 12)
                If cnombre = "" Then
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22)
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 7
                End If
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                'gridHorarios.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                estableceColor(val1, val2, estatus)

            Next
        End If

        'deshabilita los disponibles en fechas anteriores al dia de hoy
        Dim auxfecha As Date = lblfecha.Text
        Dim hoy As Date = Now.Date

        Dim numCol As Int16 = lasfunciones.negocioCP("columna_esp")
        Dim cuentaC As Integer = 1
        Dim cuentaF As Integer = 0
        Do While cuentaF < gridHorarios.Items.Count
            cuentaC = 1
            Do While cuentaC <= numCol
                If CType(gridHorarios.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Text <> "DISPONIBLE" Then
                    CType(gridHorarios.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Enabled = False
                End If
                cuentaC = cuentaC + 1
            Loop
            cuentaF = cuentaF + 1
        Loop

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='CANCELADO'"
        'comando.Parameters("@fecha").Value = lblfecha.Text.Trim
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lnkCanceladas.Text = leer.GetValue(0).ToString.Trim
        End If

        conexion.Close()

        '** se llenan los bloqueados
        Dim cadena As String = ""
        Dim posicion As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  1 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadena = leer.GetValue(2).ToString.Trim
            If cadena <> "" Then
                For i = 1 To funciones.num_elementos(cadena, "|")
                    posicion = funciones.entry(i, cadena, "|") - 1
                    CType(gridHorarios.Items(posicion).Cells(1).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicion).Cells(1).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicion).Cells(1).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac2 As String = ""
        Dim posicionc2 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  2 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac2 = leer.GetValue(2).ToString.Trim
            If cadenac2 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac2, "|")
                    posicionc2 = funciones.entry(i, cadenac2, "|") - 1
                    CType(gridHorarios.Items(posicionc2).Cells(2).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc2).Cells(2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc2).Cells(2).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac3 As String = ""
        Dim posicionc3 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  3 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac3 = leer.GetValue(2).ToString.Trim
            If cadenac3 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac3, "|")
                    posicionc3 = funciones.entry(i, cadenac3, "|") - 1
                    CType(gridHorarios.Items(posicionc3).Cells(3).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc3).Cells(3).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc3).Cells(3).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac4 As String = ""
        Dim posicionc4 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  4 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac4 = leer.GetValue(2).ToString.Trim
            If cadenac4 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac4, "|")
                    posicionc4 = funciones.entry(i, cadenac4, "|") - 1
                    CType(gridHorarios.Items(posicionc4).Cells(4).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc4).Cells(4).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc4).Cells(4).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac5 As String = ""
        Dim posicionc5 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  5 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac5 = leer.GetValue(2).ToString.Trim
            If cadenac5 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac5, "|")
                    posicionc5 = funciones.entry(i, cadenac5, "|") - 1
                    CType(gridHorarios.Items(posicionc5).Cells(5).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc5).Cells(5).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc5).Cells(5).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac6 As String = ""
        Dim posicionc6 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  6 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac6 = leer.GetValue(2).ToString.Trim
            If cadenac6 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac6, "|")
                    posicionc6 = funciones.entry(i, cadenac6, "|") - 1
                    CType(gridHorarios.Items(posicionc6).Cells(6).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc6).Cells(6).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc6).Cells(6).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac7 As String = ""
        Dim posicionc7 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  7 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac7 = leer.GetValue(2).ToString.Trim
            If cadenac7 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac7, "|")
                    posicionc7 = funciones.entry(i, cadenac7, "|") - 1
                    CType(gridHorarios.Items(posicionc7).Cells(7).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc7).Cells(7).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc7).Cells(7).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac8 As String = ""
        Dim posicionc8 As Integer
        Sql = ""
        Sql = "SELECT "
        Sql = Sql & "fecha,"
        Sql = Sql & "columna, "
        'Sql = Sql & "idDoctor, "
        Sql = Sql & "bloqueos "
        Sql = Sql & "FROM Bloqueos "
        Sql = Sql & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        Sql = Sql & "columna =  8 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = Sql
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac8 = leer.GetValue(2).ToString.Trim
            If cadenac8 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac8, "|")
                    posicionc8 = funciones.entry(i, cadenac8, "|") - 1
                    CType(gridHorarios.Items(posicionc8).Cells(8).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc8).Cells(8).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc8).Cells(8).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac9 As String = ""
        Dim posicionc9 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  9 "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac9 = leer.GetValue(2).ToString.Trim
            If cadenac9 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac9, "|")
                    posicionc9 = funciones.entry(i, cadenac9, "|") - 1
                    CType(gridHorarios.Items(posicionc9).Cells(9).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc9).Cells(9).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc9).Cells(9).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()


    End Sub
    Sub Reacomoda()
        Dim numeroFilas As Integer = gridHorarios.Items.Count
        Dim puntero As Integer = 0
        Dim columnas As Integer = 0
        Dim contador As Integer = 0
        Dim MIRESTA As Integer = 0
        Dim mivar As String = ""
        Do While puntero < numeroFilas
            columnas = 4
            Do While columnas > 0
                mivar = CType(gridHorarios.Items(puntero).Cells(columnas).Controls(1), LinkButton).Text
                Select Case mivar
                    Case ""
                        contador = contador + 1
                    Case "DISPONIBLE"
                        contador = contador + 1
                End Select
                columnas = columnas - 1
            Loop
            MIRESTA = 4 - contador
            If MIRESTA > 0 Then
                If puntero + 1 < numeroFilas Then
                    columnas = 4
                    Do While columnas > 0 And MIRESTA > 0
                        mivar = CType(gridHorarios.Items(puntero + 1).Cells(columnas).Controls(1), LinkButton).Text
                        If mivar = "" Then
                            MIRESTA = MIRESTA - 1
                        End If
                        columnas = columnas - 1
                    Loop
                    If MIRESTA > 0 Then
                        columnas = 4
                        Do While columnas > 0 And MIRESTA > 0
                            mivar = CType(gridHorarios.Items(puntero + 1).Cells(columnas).Controls(1), LinkButton).Text
                            If mivar = "DISPONIBLE" Then
                                CType(gridHorarios.Items(puntero + 1).Cells(columnas).Controls(1), LinkButton).Text = ""
                                MIRESTA = MIRESTA - 1
                            End If
                            columnas = columnas - 1
                        Loop
                    End If
                End If
                If puntero > 0 Then
                    MIRESTA = 4 - contador
                    columnas = 4
                    Do While columnas > 0 And MIRESTA > 0
                        mivar = CType(gridHorarios.Items(puntero - 1).Cells(columnas).Controls(1), LinkButton).Text
                        If mivar = "" Then
                            MIRESTA = MIRESTA - 1
                        End If
                        columnas = columnas - 1
                    Loop
                    If MIRESTA > 0 Then
                        columnas = 4
                        Do While columnas > 0 And MIRESTA > 0
                            mivar = CType(gridHorarios.Items(puntero - 1).Cells(columnas).Controls(1), LinkButton).Text
                            If mivar = "DISPONIBLE" Then
                                CType(gridHorarios.Items(puntero - 1).Cells(columnas).Controls(1), LinkButton).Text = ""
                                MIRESTA = MIRESTA - 1
                            End If
                            columnas = columnas - 1
                        Loop
                    End If
                End If
            End If
            contador = 0
            MIRESTA = 0
            puntero = puntero + 1
        Loop
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            gridHorarios = funciones.LLenaGrid("select horario,idhorario from horarios ", gridHorarios, "fisiocareCP")
            gFijo = funciones.LLenaGrid("select horario,idhorario from horarios ", gFijo, "fisiocareCP")
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
                Dim lafecha As Date = lblfecha.Text
                lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                calendario.SelectedDate = lblfecha.Text
            Catch ex As Exception
                Try
                    Dim hoy As DateTime = DateTime.Now()
                    lblfecha.Text = String.Format("{0:dd/MM/yyyy}", hoy)
                    Dim lafecha As Date = lblfecha.Text
                    lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                    calendario.SelectedDate = lblfecha.Text
                Catch
                    Response.Redirect("login.aspx")
                End Try
            End Try
            llenaAgenda()
            'Reacomoda()
        End If
    End Sub

    Protected Sub gridHorarios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridHorarios.ItemCommand
        Dim mifila As String = e.Item.ItemIndex.ToString.Trim
        Dim columna As String = Right(e.CommandName, 1)
        Dim micolumna As LinkButton = gridHorarios.Items(mifila).Cells(columna).FindControl("lnk" + columna.ToString.Trim)
        Dim valor As String = micolumna.Text.Trim
        Dim cita, duracion As String
        Dim funciones As New miclases

        If valor = "DISPONIBLE" Then
            Context.Items.Add("duracion", mifila)
            Context.Items.Add("posicion", columna)
            Context.Items.Add("idhorario", gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
            Context.Items.Add("elhorario", gridHorarios.Items(e.Item.ItemIndex).Cells(0).Text)
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Server.Transfer("clientesCP.aspx", True)
        Else
            ' If CType(gridHorarios.Items(mifila).Cells(columna).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250) Then  'FromArgb(40, 41, 40)
            'If gridHorarios.Items(mifila).Cells(columna).BackColor = Drawing.Color.FromArgb(200, 0, 0) Then

            If CType(gridHorarios.Items(mifila).Cells(columna).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0) Then
                Dim contador As Integer = 0
                Dim cuentaFilas As Integer = gridHorarios.Items.Count
                Dim micolumna2 As Integer = 1
                Dim columna2 As LinkButton
                Dim var As String = ""
                Dim bandera As Boolean = True
                Do While contador < cuentaFilas
                    bandera = True
                    Do While micolumna2 <= 4 And bandera
                        columna2 = gridHorarios.Items(contador).Cells(micolumna2).FindControl("lnk" + micolumna2.ToString.Trim)
                        If columna2.Text = "DISPONIBLE" Then
                            bandera = False
                            var = var + " or idHorario='" + (contador + 1).ToString.Trim + "'"
                        End If
                        micolumna2 = micolumna2 + 1
                    Loop
                    micolumna2 = 1
                    contador = contador + 1
                Loop
                cita = funciones.entry(1, CType(gridHorarios.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                duracion = funciones.entry(2, CType(gridHorarios.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                Context.Items.Add("elidCita", cita)
                Context.Items.Add("fechacita", lblfecha.Text.Trim)
                Context.Items.Add("condicion", Right(var, Len(var) - 3))
                'nuevos valores enviados
                Context.Items.Add("duracion", duracion) 'errrrorrrrrrrrrrrrrrrrrita
                Context.Items.Add("posicion", columna)
                Context.Items.Add("idhorario", gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
                Server.Transfer("datosCitas.aspx", False)

            Else
                'If CInt(columna.Trim) < 5 Then
                'cita = funciones.entry(1, CType(gridHorarios.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                'duracion = funciones.entry(2, CType(gridHorarios.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                'Context.Items.Add("elidCita", cita)
                'Context.Items.Add("fechacita", lblfecha.Text.Trim)
                'Context.Items.Add("duracion", duracion)
                'Context.Items.Add("posicion", columna)
                'Context.Items.Add("idhorario", gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
                'Server.Transfer("cambiosala.aspx", False)
                'End If
            End If
        End If
    End Sub
    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        'Select Case Left(Key, 1)
        '   Case "I"
        'End Select
        Dim funciones As New miclases
        funciones.grabaDatos("update clientes set activo='False' where idCliente='" + Key.Trim + "';", "fisiocareCP")

        'Context.Items.Add("fecha", lblfecha.Text.Trim)
        'Server.Transfer("Defadmon.aspx", True)
    End Sub

    Protected Sub lnkCanceladas_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub

    Protected Sub lnkCanceladas_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCanceladas.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub

    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        Try
            Dim lafecha As Date = calendario.SelectedDate
            Dim funciones As New miclases
            lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
            lblfecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha)
            gridHorarios = funciones.LLenaGrid("select horario,idhorario from horarios ", gridHorarios, "fisiocareCP")
            llenaAgenda()
        Catch
            Messagebox1.ShowMessage("Datos Incorrectos ....")
        End Try
    End Sub

    Protected Sub LinkButton6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton6.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub
    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub
End Class
