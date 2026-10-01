Imports System.Data
Imports System.Data.SqlClient
Partial Class cambioSala
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim funciones As New miclases
    Dim lasfunciones As New Funciones
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String = ""
        Dim dt As New DataTable
        strSQL = "SELECT nombre,columna " & _
                 " FROM [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] where turno='M' " & _
                 " order by columna asc"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    gridHorarios.Columns(CInt(row.Item("columna").ToString)).HeaderText = row.Item("nombre").ToString
                    'Console.WriteLine(row.Item("nombre").ToString + " ---" + row.Item("columna").ToString)
                Next
            End If

        Else

        End If
        strSQL = "SELECT nombre,columna " & _
                 " FROM [" & clsDatos.BaseDatos & "].[dbo].[Terapistas] where turno='V' " & _
                 " order by columna asc"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For Each row As DataRow In dt.Rows
                    gridHorariosVespertino.Columns(CInt(row.Item("columna").ToString)).HeaderText = row.Item("nombre").ToString
                Next
            End If

        Else

        End If
        If Not Page.IsPostBack Then
            lblfecha.Text = Context.Items("fecha").ToString.Trim
            lafechaAnte.Text = lblfecha.Text
            lahoraAnte.Text = funciones.leerValor("select horario from horarios2017 where idhorario=(select idhorario from agenda where idcita='" + Context.Items("elidCita").ToString.Trim + "')")
            calendario.SelectedDate = lblfecha.Text
            Dim lafecha As Date = lblfecha.Text
            lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
            lblidcita.Text = Context.Items("elidCita").ToString.Trim
            gridHorarios = funciones.creadataset("select horario,idhorario from horarios2017 WHERE turno='M' ", gridHorarios)
            gridHorariosVespertino = funciones.creadataset("select horario,idhorario from horarios2017 WHERE turno='V' order by horario ", gridHorariosVespertino)
            'gFijo = funciones.creadataset("select horario,idhorario from horarios2017 ", gFijo)
            Dim valores(,) As String = funciones.leerValores("select agenda.idCliente, clientes.nombre + ' ' + clientes.paterno + ' ' + clientes.materno AS nombre " & _
            ",duracion from agenda left join clientes on agenda.idCliente=clientes.idCliente where idCita='" + lblidcita.Text + "' and agenda.estado='AGENDADO'", 3)
            lblIdcliente.Text = valores(0, 0)
            lblNombre.Text = "CAMBIAR HORARIO DE " + valores(1, 0)
            lblduracion.Text = valores(2, 0)
            llenaAgenda()
            llenaAgendaVespertino()
        End If
    End Sub

    Sub llenaAgenda()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim bandera As Boolean = True
        Dim cadregistros As String
        Dim cadenafin As String
        Dim cad, cnombre, cita, estatus, duracion As String
        Dim latencion As String
        Dim i, val1, val2 As Integer


        comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        " clientes.nombre+' '+clientes.paterno+' '+clientes.materno as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
        "inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        "and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='M' order by agenda.idhorario"
        comando.Parameters.Add(New SqlParameter("@fecha", SqlDbType.DateTime))
        comando.Parameters("@fecha").Value = lblfecha.Text.Trim
        conexion.Open()
        leer = comando.ExecuteReader
        cadregistros = ""
        Do While leer.Read
            cadregistros = cadregistros & leer.GetValue(1).ToString.Trim & "-" & leer.GetValue(4).ToString & "-" & leer.GetValue(3).ToString & "-" & Left(leer.GetValue(5).ToString.Trim, 30) & "-" & leer.GetValue(7).ToString & "-" & leer.GetValue(6).ToString & "-" & leer.GetValue(0).ToString & "-" & leer.GetValue(8).ToString & ","

            'CType(gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(1), LinkButton).Text = Left(leer.GetValue(5).ToString.Trim, 30)
            'CType(gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(3), Label).Text = leer.GetValue(7).ToString.Trim
            'gridHorarios.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString.Trim).BackColor = Drawing.Color.FromArgb(128, 0, 0)
            'estableceColor(leer.GetValue(3).ToString.Trim, leer.GetValue(4).ToString.Trim, leer.GetValue(6).ToString.Trim)
            ' lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1


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
                latencion = funciones.entry(8, cad, "-").ToString.Trim


                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 12)
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 8
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                If cnombre = "" Then
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22)
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 7
                End If
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                'gridHorarios.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                estableceColor(val1, val2, estatus, latencion)
                'estableceColor(val1, val2, estatus)


            Next
        End If


        '** se llenan los bloqueados
        Dim cadena As String = ""
        Dim posicion As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  1  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  2  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  3  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  4  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  5  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  6  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  7  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  8  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
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
        SQL = SQL & "columna =  9  and turno='M' "
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

        Dim cadenac10 As String = ""
        Dim posicionc10 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  10  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac10 = leer.GetValue(2).ToString.Trim
            If cadenac10 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac10, "|")
                    posicionc10 = funciones.entry(i, cadenac10, "|") - 1
                    CType(gridHorarios.Items(posicionc10).Cells(10).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc10).Cells(10).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc10).Cells(10).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac11 As String = ""
        Dim posicionc11 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  11  and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac11 = leer.GetValue(2).ToString.Trim
            If cadenac11 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac11, "|")
                    posicionc11 = funciones.entry(i, cadenac11, "|") - 1
                    CType(gridHorarios.Items(posicionc11).Cells(11).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc11).Cells(11).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc11).Cells(11).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac12 As String = ""
        Dim posicionc12 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  12 and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac12 = leer.GetValue(2).ToString.Trim
            If cadenac12 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac12, "|")
                    posicionc12 = funciones.entry(i, cadenac12, "|") - 1
                    CType(gridHorarios.Items(posicionc12).Cells(12).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc12).Cells(12).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc12).Cells(12).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac13 As String = ""
        Dim posicionc13 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  13 and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac13 = leer.GetValue(2).ToString.Trim
            If cadenac13 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac13, "|")
                    posicionc13 = funciones.entry(i, cadenac13, "|") - 1
                    CType(gridHorarios.Items(posicionc13).Cells(13).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc13).Cells(13).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc13).Cells(13).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac14 As String = ""
        Dim posicionc14 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  14 and turno='M' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac14 = leer.GetValue(2).ToString.Trim
            If cadenac14 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac14, "|")
                    posicionc14 = funciones.entry(i, cadenac14, "|") - 1
                    CType(gridHorarios.Items(posicionc14).Cells(14).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorarios.Items(posicionc14).Cells(14).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(posicionc14).Cells(14).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorarios.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

    End Sub
    Sub llenaAgendaVespertino()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim bandera As Boolean = True
        Dim cadregistros As String
        Dim cadenafin As String
        Dim cad, cnombre, cita, estatus, duracion As String
        Dim latencion As String
        Dim i, val1, val2 As Integer


        comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        " clientes.nombre+' '+clientes.paterno+' '+clientes.materno as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
        "inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        "and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='V' order by agenda.idhorario"
        comando.Parameters.Add(New SqlParameter("@fecha", SqlDbType.DateTime))
        comando.Parameters("@fecha").Value = lblfecha.Text.Trim
        conexion.Open()
        leer = comando.ExecuteReader
        cadregistros = ""
        Do While leer.Read
            cadregistros = cadregistros & leer.GetValue(1).ToString.Trim & "-" & leer.GetValue(4).ToString & "-" & leer.GetValue(3).ToString & "-" & Left(leer.GetValue(5).ToString.Trim, 30) & "-" & leer.GetValue(7).ToString & "-" & leer.GetValue(6).ToString & "-" & leer.GetValue(0).ToString & "-" & leer.GetValue(8).ToString & ","

            'CType(gridHorariosVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(1), LinkButton).Text = Left(leer.GetValue(5).ToString.Trim, 30)
            'CType(gridHorariosVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(3), Label).Text = leer.GetValue(7).ToString.Trim
            'gridHorariosVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString.Trim).BackColor = Drawing.Color.FromArgb(128, 0, 0)
            'estableceColor(leer.GetValue(3).ToString.Trim, leer.GetValue(4).ToString.Trim, leer.GetValue(6).ToString.Trim)
            ' lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1


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
                latencion = funciones.entry(8, cad, "-").ToString.Trim

                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 12)
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                If cnombre = "" Then
                    CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22)
                    CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 7
                End If
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                'gridHorariosVespertino.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                'estableceColorVespertino(val1, val2, estatus)
                estableceColorVespertino(val1, val2, estatus, latencion)

            Next
        End If


        '** se llenan los bloqueados
        Dim cadena As String = ""
        Dim posicion As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  1  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadena = leer.GetValue(2).ToString.Trim
            If cadena <> "" Then
                For i = 1 To funciones.num_elementos(cadena, "|")
                    posicion = funciones.entry(i, cadena, "|") - 1
                    CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac2 As String = ""
        Dim posicionc2 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  2  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac2 = leer.GetValue(2).ToString.Trim
            If cadenac2 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac2, "|")
                    posicionc2 = funciones.entry(i, cadenac2, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc2).Cells(2).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc2).Cells(2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc2).Cells(2).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac3 As String = ""
        Dim posicionc3 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  3  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac3 = leer.GetValue(2).ToString.Trim
            If cadenac3 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac3, "|")
                    posicionc3 = funciones.entry(i, cadenac3, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc3).Cells(3).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc3).Cells(3).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc3).Cells(3).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac4 As String = ""
        Dim posicionc4 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  4  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac4 = leer.GetValue(2).ToString.Trim
            If cadenac4 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac4, "|")
                    posicionc4 = funciones.entry(i, cadenac4, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc4).Cells(4).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc4).Cells(4).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc4).Cells(4).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac5 As String = ""
        Dim posicionc5 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  5  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac5 = leer.GetValue(2).ToString.Trim
            If cadenac5 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac5, "|")
                    posicionc5 = funciones.entry(i, cadenac5, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc5).Cells(5).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc5).Cells(5).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc5).Cells(5).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac6 As String = ""
        Dim posicionc6 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  6  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac6 = leer.GetValue(2).ToString.Trim
            If cadenac6 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac6, "|")
                    posicionc6 = funciones.entry(i, cadenac6, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc6).Cells(6).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc6).Cells(6).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc6).Cells(6).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac7 As String = ""
        Dim posicionc7 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  7  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac7 = leer.GetValue(2).ToString.Trim
            If cadenac7 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac7, "|")
                    posicionc7 = funciones.entry(i, cadenac7, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc7).Cells(7).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc7).Cells(7).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc7).Cells(7).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac8 As String = ""
        Dim posicionc8 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  8  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac8 = leer.GetValue(2).ToString.Trim
            If cadenac8 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac8, "|")
                    posicionc8 = funciones.entry(i, cadenac8, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc8).Cells(8).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc8).Cells(8).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc8).Cells(8).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
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
        SQL = SQL & "columna =  9  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac9 = leer.GetValue(2).ToString.Trim
            If cadenac9 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac9, "|")
                    posicionc9 = funciones.entry(i, cadenac9, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc9).Cells(9).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc9).Cells(9).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc9).Cells(9).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac10 As String = ""
        Dim posicionc10 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  10  and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac10 = leer.GetValue(2).ToString.Trim
            If cadenac10 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac10, "|")
                    posicionc10 = funciones.entry(i, cadenac10, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc10).Cells(10).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc10).Cells(10).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc10).Cells(10).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac11 As String = ""
        Dim posicionc11 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  11 and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac11 = leer.GetValue(2).ToString.Trim
            If cadenac11 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac11, "|")
                    posicionc11 = funciones.entry(i, cadenac11, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc11).Cells(11).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc11).Cells(11).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc11).Cells(11).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac12 As String = ""
        Dim posicionc12 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  12 and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac12 = leer.GetValue(2).ToString.Trim
            If cadenac12 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac12, "|")
                    posicionc12 = funciones.entry(i, cadenac12, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc12).Cells(12).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc12).Cells(12).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc12).Cells(12).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac13 As String = ""
        Dim posicionc13 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  13 and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac13 = leer.GetValue(2).ToString.Trim
            If cadenac13 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac13, "|")
                    posicionc13 = funciones.entry(i, cadenac13, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc13).Cells(13).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc13).Cells(13).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc13).Cells(13).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

        Dim cadenac14 As String = ""
        Dim posicionc14 As Integer
        SQL = ""
        SQL = "SELECT "
        SQL = SQL & "fecha,"
        SQL = SQL & "columna, "
        'Sql = Sql & "idDoctor, "
        SQL = SQL & "bloqueos "
        SQL = SQL & "FROM Bloqueos "
        SQL = SQL & "WHERE fecha = '" & lblfecha.Text.Trim & "' And "
        SQL = SQL & "columna =  14 and turno='V' "
        'Sql = Sql & "idDoctor='" & cboDoctores.SelectedValue & "' "
        comando.CommandText = SQL
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            cadenac14 = leer.GetValue(2).ToString.Trim
            If cadenac14 <> "" Then
                For i = 1 To funciones.num_elementos(cadenac14, "|")
                    posicionc11 = funciones.entry(i, cadenac14, "|") - 1
                    CType(gridHorariosVespertino.Items(posicionc14).Cells(14).Controls(1), LinkButton).Text = "BLOQUEADO"
                    CType(gridHorariosVespertino.Items(posicionc14).Cells(14).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(posicionc14).Cells(14).Controls(1), LinkButton).ForeColor = Drawing.Color.Gray
                    'CType(gridHorariosVespertino.Items(posicion).Cells(1).Controls(1), CheckBox).Checked = True
                Next
            End If
        End If
        leer.Close()
        conexion.Close()

    End Sub

    Sub estableceColor(ByVal fila As String, ByVal posicion As String, ByVal miestado As String, ByVal latencion As String)
        'color = color + 10
        'If color >= 50 Then
        ' color = 0
        'End If
        Select Case miestado
            Case "AGENDADO"
                If latencion = "THOS" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).Text = "<i class='fa fa-2x fa-handshake-o'> " & CType(gridHorarios.Items(fila - 1).Cells(fila).Controls(1), LinkButton).Text & "</i>"

                End If
                If latencion = "PROEJ" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(255, 229, 204)
                End If

                If latencion = "TP" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorarios.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(147, 148, 210)
                End If

                CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)

            Case "REALIZADO"
                If latencion = "THOS" Then
                    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 250)
                ElseIf latencion = "PROEJ" Then
                    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(153, 76, 0)
                ElseIf latencion = "TP" Then
                    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(147, 148, 210)
                Else
                    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)
                End If
                'Case "AGENDADO"


                '    'solo letras
                '    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)


                'Case "REALIZADO"


                '    'solo letras
                '    CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)


        End Select
    End Sub
    Sub estableceColorVespertino(ByVal fila As String, ByVal posicion As String, ByVal miestado As String, ByVal latencion As String)
        'color = color + 10
        'If color >= 50 Then
        ' color = 0
        'End If
        Select Case miestado
            Case "AGENDADO"
                If latencion = "THOS" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorariosVespertino.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                End If
                If latencion = "PROEJ" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorariosVespertino.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(255, 229, 204)
                End If
                If latencion = "TP" Then
                    'CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).BackColor = Drawing.Color.FromArgb(143, 210, 243)
                    gridHorariosVespertino.Items(fila - 1).Cells(posicion).BackColor = Drawing.Color.FromArgb(147, 148, 210)
                End If
                CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)


            Case "REALIZADO"
                If latencion = "THOS" Then
                    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 250)
                ElseIf latencion = "PROEJ" Then
                    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(153, 76, 0)
                ElseIf latencion = "TP" Then
                    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(147, 148, 210)
                Else
                    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)
                End If
                'Case "AGENDADO"


                '    'solo letras
                '    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)


                'Case "REALIZADO"

                '    'solo letras

                '    CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)

        End Select
    End Sub

    Protected Sub gridHorarios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridHorarios.ItemCommand
        Dim mifila As String = e.Item.ItemIndex.ToString.Trim
        'Agragado para aumentar el numero de columna a mas de 9 (Cambio de sala)
        Dim columnav As String

        'If e.CommandName = "lnk10" Then
        '    columnav = Right(e.CommandName, 2)
        'Else
        '    columnav = Right(e.CommandName, 1)
        'End If

        If e.CommandName = "lnk10" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk11" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk12" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk13" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk14" Then
            columnav = Right(e.CommandName, 2)
        Else
            columnav = Right(e.CommandName, 1)
        End If

        Dim columna As String = columnav

        'Dim columna As String = Right(e.CommandName, 1)


        lblFila.Text = mifila
        lblposicion.Text = columna
        lahora.Text = gridHorarios.Items(e.Item.ItemIndex).Cells(0).Text
        lblidhorario.Text = gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        lblturno.Text = "M"
        tHorarios.Visible = False
        Panel1.Visible = True
        lafecha.Text = lblfecha.Text
        tFecha.Visible = False
    End Sub
    Protected Sub gridHorariosVespertino_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridHorariosVespertino.ItemCommand
        Dim mifila As String = e.Item.ItemIndex.ToString.Trim
        'Agragado para aumentar el numero de columna a mas de 9 (Cambio de sala)
        Dim columnav As String

        If e.CommandName = "lnk10" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk11" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk12" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk13" Then
            columnav = Right(e.CommandName, 2)
        ElseIf e.CommandName = "lnk14" Then
            columnav = Right(e.CommandName, 2)
        Else
            columnav = Right(e.CommandName, 1)
        End If

        Dim columna As String = columnav

        'If e.CommandName = "lnk10" Then
        '    columnav = Right(e.CommandName, 2)
        'Else
        '    columnav = Right(e.CommandName, 1)
        'End If

        'Dim columna As String = columnav

        'Dim columna As String = Right(e.CommandName, 1)


        lblFila.Text = mifila
        lblposicion.Text = columna
        lahora.Text = gridHorariosVespertino.Items(e.Item.ItemIndex).Cells(0).Text
        lblidhorario.Text = gridHorariosVespertino.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        lblturno.Text = "V"
        tHorarios.Visible = False
        Panel1.Visible = True
        lafecha.Text = lblfecha.Text
        tFecha.Visible = False
    End Sub

    Protected Sub lnkGrabar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkGrabar.Click
        lblerror.Text = ""
        Dim cFechasValidas As String = ""
        Dim pos As String
        Dim i As Integer
        Dim cValor As String = ""
        pos = funciones.negocio("columna_esp").Trim
        If funciones.lookup(lblposicion.Text, pos, ",") > 0 Then
            ' +++si esta en la agenda especial
            cFechasValidas = validafechas_esp(lblidhorario.Text, lblposicion.Text)
            If cFechasValidas <> "" Then
                For i = 1 To funciones.num_elementos(cFechasValidas, "|")
                    cValor = funciones.entry(i, cFechasValidas, "|").Trim
                    grabaEnAgenda(funciones.entry(2, cValor, ",").Trim, funciones.entry(1, cValor, ",").Trim)
                Next
                Context.Items.Add("fecha", lblfecha.Text.Trim)
                Server.Transfer("default.aspx", False)
            End If
        Else
            cFechasValidas = validafechas(lblidhorario.Text, lblposicion.Text)
            If cFechasValidas <> "" Then
                For i = 1 To funciones.num_elementos(cFechasValidas, "|")
                    cValor = funciones.entry(i, cFechasValidas, "|").Trim
                    grabaEnAgenda(funciones.entry(2, cValor, ",").Trim, funciones.entry(1, cValor, ",").Trim)
                Next
            End If
        End If
        If lblerror.Text = "" Then
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Server.Transfer("default.aspx", False)
        End If
    End Sub
    Private Function validafechas(ByVal Horario As String, ByVal Columna As String) As String
        Dim funciones As New miclases
        'Dim cuentaF As Int16 = gridDias.Items.Count
        'Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count
        Dim filas As Int16 = 0
        Dim columnas As Int16 = 0
        Dim auxfecha As Date
        Dim auxfecha2 As String
        Dim bandera As Boolean = False
        Dim Ocupado As Boolean = False
        Dim selvalida As Integer = 0
        Dim finhorario As Integer = 0
        Dim cad, cadregistros, cadenafin, consulta As String
        Dim i, j, k, isalas As Integer
        Dim cSalas As String = ""
        Dim Columna2 As String = ""
        Dim cadOk As String = ""
        Dim CadX As String = ""
        Dim CadXOk As String = ""
        Dim banderaX As Boolean = False
        Dim ListaValida As String = ""
        Dim diasNoDisp As String = ""
        '+++++++++++
        isalas = funciones.negocio("numsalas")
        For j = 1 To isalas
            If j.ToString.Trim <> Columna.Trim Then
                cSalas = cSalas + j.ToString.Trim + ","
            End If
        Next
        cSalas = Left(cSalas, Len(cSalas) - 1)
        cSalas = Columna.Trim + "," + cSalas ''' todas las salas y pongo de primero la que di click
        'MsgBox(" # salas " & cSalas)
        '+++++++++++
        Dim PersonaAgendada As String = ""
        Dim auxPersonaAgendada As String = ""
        Dim auxHorario As Integer = Int(Horario) + Int(lblduracion.Text)

        auxfecha = lblfecha.Text
        auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)

        'auxPersonaAgendada = funciones.leerValor("select distinct fecha from  ( select idCita, fecha, " & _
        '"idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + lblIdcliente.Text + " and estado='AGENDADO') as horarios " & _
        '"where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
        '"idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')").ToString()
        'If auxPersonaAgendada <> "0" Then
        'PersonaAgendada = PersonaAgendada + auxfecha2.Trim + ","
        'End If
        selvalida = selvalida + 1
        '+++++++++++++++++
        For k = 1 To funciones.num_elementos(cSalas, ",")
            '++++++++++++++++
            Columna2 = funciones.entry(k, cSalas, ",")   'idhorario >= " + Horario.Trim + " and " & _''' obtengo la sala que este disponible siempre en la cadena esta la primera sala es donde se dio click
            consulta = "select agenda.idhorario, agenda.posicion, agenda.duracion from agenda " & _
            " where agenda.fecha='" + auxfecha2 + "' and " & _
            " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') and agenda.turno='" + lblturno.Text + "' order by agenda.idhorario "
            bandera = False
            banderaX = False
            cadregistros = funciones.llenalista(consulta) ''' idhorario-posicion-duracion horarios ocupado

            'MsgBox("1) fecha: " & auxfecha2.Trim & " columna: " & Columna2.Trim & "  " & " ++ cadregistros: " & cadregistros)
            If cadregistros.Trim <> "" Then
                cadenafin = funciones.completalista(cadregistros, "0") ''idhorario-posicion; quita duarions y asigna los horarios que ocupan dos casillas osea mas de media hora
                'MsgBox("2) *** lista valida: " & cadenafin)
                For i = 0 To Int(lblduracion.Text) - 1
                    finhorario = CInt(Horario) + i
                    cad = Str(CInt(Horario) + i) + "-" + Columna2 '+ "-" + i.ToString.Trim
                    If funciones.lookup(cad.Trim, cadenafin, ",") > 0 Then
                        bandera = True ' El horario esta ocupado
                    End If
                    If finhorario > 32 Then
                        bandera = True
                    End If
                    If i = 0 Then ' se checa q haya alguna sala ocupada antes ***
                        CadX = Str(CInt(Horario) + 1) + "-" + Columna2
                        If funciones.lookup(CadX.Trim, cadenafin, ",") <= 0 Then
                            banderaX = True
                            CadXOk = auxfecha2 + " , " + Columna2.Trim + "|" 'CadX
                        End If
                    End If
                Next
                If bandera = False And banderaX = True Then 'checa si hay en alguna sala lugar
                    '++cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                    cadOk = CadXOk
                    k = 9
                Else
                    If bandera = False Then
                        cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                        k = 9
                    End If
                End If
                'MsgBox("3) *** es valido: " & bandera.ToString & "  cadOk = " & cadOk)
            Else
                If bandera = False Then ' si esta vacia la sala 
                    cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                    k = 9
                End If
            End If
        Next
        'si es mas de 1 se corta
        If funciones.num_elementos(cadOk, "|") > 1 Then
            cadOk = funciones.entry(1, cadOk, "|")
        End If
        'MsgBox("1) " & cadOk)
        'se checa q se encontro en el dia un lugar libre
        If cadOk <> "" Then
            ListaValida = ListaValida + cadOk + "|"
            cadOk = ""
        Else
            diasNoDisp = diasNoDisp + auxfecha2.ToString.Trim
        End If

        If ListaValida.Trim <> "" Then
            ListaValida = Left(ListaValida, Len(ListaValida) - 1)
        Else
            lblerror.Text = "(1)Existen fechas ya ocupadas y no se ha podido programar en la agenda(" + diasNoDisp + ") !!!"
        End If
        ' MsgBox("+++ fin Cadena Valida : " & ListaValida & "   +++  dias a agendar: " & selvalida)
        If ListaValida.Trim <> "" And selvalida <> funciones.num_elementos(ListaValida, "|") Then
            lblerror.Text = "(2)Existen fechas ya ocupadas y no se ha podido programar en la agenda (" + diasNoDisp + ") !!!"
            ListaValida = ""
        End If
        If PersonaAgendada <> "" Then
            lblerror.Text = "(3)La persona ya fue agendada en el horario, un horario antes o un horario despues (" + PersonaAgendada + ")"
            ListaValida = ""
        End If
        Return ListaValida
    End Function
    Private Function validafechas_esp(ByVal Horario As String, ByVal Columna As String) As String
        Dim funciones As New miclases
        'Dim cuentaF As Int16 = gridDias.Items.Count
        'Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count - 1
        Dim filas As Int16 = 0
        Dim columnas As Int16 = 0
        Dim auxfecha As Date
        Dim auxfecha2 As String
        Dim bandera As Boolean = False
        Dim selvalida As Integer = 0
        Dim cad, cadregistros, cadenafin, consulta As String
        Dim i, j, k As Integer
        Dim cSalas As String = ""
        Dim cListasalas As String
        Dim Columna2 As String = ""
        Dim cadOk As String = ""
        '+++++++++++ este es para las salas especiales
        cListasalas = funciones.negocio("columna_esp")
        For j = 1 To funciones.num_elementos(cListasalas, ",")
            If funciones.entry(j, cListasalas, ",") <> Columna.Trim Then
                cSalas = cSalas + j.ToString.Trim + ","
            Else
                cSalas = funciones.entry(j, cListasalas, ",")
            End If
        Next
        cSalas = Left(cSalas, Len(cSalas) - 1)
        cSalas = Columna.Trim + "," + cSalas
        '+++++++++++
        Dim PersonaAgendada As String = ""
        Dim auxPersonaAgendada As String = ""
        Dim auxHorario As Integer = Int(Horario) + Int(lblduracion.Text.Trim)

        auxfecha = lblfecha.Text
        auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)

        'auxPersonaAgendada = funciones.leerValor("select distinct fecha from  ( select idCita, fecha, " & _
        '"idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + lblIdcliente.Text + " and estado='AGENDADO') as horarios " & _
        '"where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
        '"idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')").ToString()
        'If auxPersonaAgendada <> "0" Then
        'PersonaAgendada = PersonaAgendada + auxfecha2.Trim + ","
        'End If
        selvalida = selvalida + 1
        '+++++++++++++++++
        For k = 1 To funciones.num_elementos(cSalas, ",")
            '++++++++++++++++
            Columna2 = funciones.entry(k, cSalas, ",")
            consulta = "select agenda.idhorario, agenda.posicion, agenda.duracion from agenda " & _
            " where agenda.fecha='" + auxfecha2 + "' and idhorario = " + Horario.Trim + " and " & _
            " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') and agenda.turno='" + lblturno.Text + "' order by agenda.idhorario "
            bandera = False
            cadregistros = funciones.llenalista(consulta)
            If cadregistros.Trim <> "" Then
                cadenafin = funciones.completalista(cadregistros, "0")
                For i = 0 To Int(lblduracion.Text) - 1
                    cad = Str(CInt(Horario) + i) + "-" + Columna2 '+ "-" + i.ToString.Trim
                    If funciones.lookup(cad.Trim, cadenafin, ",") > 0 Then
                        bandera = True ' el horario esta ocupado
                        'Novalido = True
                        k = 10
                    End If
                Next
            Else
                If bandera = False Then
                    ' MsgBox("CORRECTO !!! " & auxfecha2 & " posicion " & k.ToString)
                    cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                    k = 10
                End If
            End If
        Next '++++++++++++++

        'MsgBox("cadena cadOk : " & cadOk & "  selecciob " & selvalida.ToString & "  buscados " & funciones.num_elementos(cadOk, "|").ToString)
        If cadOk.Trim <> "" Then
            cadOk = Left(cadOk, Len(cadOk) - 1)
        End If
        If selvalida <> funciones.num_elementos(cadOk, "|") Then
            lblerror.Text = "Existen fechas ya ocupadas y no se ha podido programar en la agenda !!!"
            cadOk = ""
        End If
        If PersonaAgendada <> "" Then
            lblerror.Text = "(3)La persona ya fue agendada en el horario, un horario antes o un horario despues (" + PersonaAgendada + ")"
            cadOk = ""
        End If
        Return cadOk
    End Function

    Sub grabaEnAgenda(ByVal posicion As String, ByVal lafecha As String)
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim axu = lblidhorario.Text.Trim
        comando.CommandText = "update agenda set idhorario=@idhorario,fecha=@fecha,posicion=@posicion,turno=@turno where idCita='" + lblidcita.Text + "'"
        comando.Parameters.Add(New SqlParameter("@idhorario", Data.SqlDbType.SmallInt)).Value = lblidhorario.Text.Trim
        comando.Parameters.Add(New SqlParameter("@fecha", Data.SqlDbType.VarChar)).Value = lafecha
        comando.Parameters.Add(New SqlParameter("@duracion", Data.SqlDbType.SmallInt)).Value = lblduracion.Text            ' lblfila.Text.Trim
        comando.Parameters.Add(New SqlParameter("@posicion", Data.SqlDbType.SmallInt)).Value = posicion
        comando.Parameters.Add(New SqlParameter("@turno", Data.SqlDbType.VarChar)).Value = lblturno.Text

        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
        conexion.Dispose()
        '-----------+++ conexion local (replica)  ++++-----------
        'Dim conexionLocal As SqlConnection = funciones.conectaLocal
        'Dim comando2 As SqlCommand = conexionLocal.CreateCommand
        'comando2.CommandText = "insert into agenda(idcliente,idhorario,fecha,duracion,posicion,estado,idDoctor,dtinicio,cobservaciones)" & _
        '                      "values(@idcliente,@idhorario,@fecha,@duracion,@posicion,'AGENDADO',@idDoctor,@fechaini,@observaciones)"
        'comando2.Parameters.Add(New SqlParameter("@idcliente", Data.SqlDbType.Int)).Value = cmbclientes.SelectedValue
        'comando2.Parameters.Add(New SqlParameter("@idhorario", Data.SqlDbType.SmallInt)).Value = lblhorario.Text.Trim
        'comando2.Parameters.Add(New SqlParameter("@fecha", Data.SqlDbType.VarChar)).Value = lafecha
        'comando2.Parameters.Add(New SqlParameter("@duracion", Data.SqlDbType.SmallInt)).Value = cmbDuracion.SelectedValue            ' lblfila.Text.Trim
        'comando2.Parameters.Add(New SqlParameter("@posicion", Data.SqlDbType.SmallInt)).Value = posicion
        'comando2.Parameters.Add(New SqlParameter("@idDoctor", Data.SqlDbType.Int)).Value = cmbDoctores.SelectedValue.Trim
        'comando2.Parameters.Add(New SqlParameter("@fechaini", Data.SqlDbType.VarChar)).Value = txtFechaIni.Text.Trim
        'comando2.Parameters.Add(New SqlParameter("@observaciones", Data.SqlDbType.VarChar)).Value = txtobservaciones.Text.Trim
        'conexionLocal.Open()
        'comando2.ExecuteNonQuery()
        'conexionLocal.Close()
        'conexionLocal.Dispose()
        '--------------------------------------
    End Sub


    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        tHorarios.Visible = True
        tFecha.Visible = True
        Panel1.Visible = False
        lblerror.Text = ""
    End Sub

    Protected Sub calendario_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        Dim auxfecha As Date = calendario.SelectedDate
        Dim hoy As Date = Now.Date
        If auxfecha < hoy Then
            calendario.SelectedDate = lblfecha.Text.Trim
            Messagebox1.ShowMessage("no se pueden seleccinar horarios anteriores a la fecha de hoy ...")
            gridHorarios = funciones.creadataset("select horario,idhorario from horarios2017 ", gridHorarios)
            llenaAgenda()
        Else
            Try
                Dim lafecha As Date = calendario.SelectedDate
                Dim funciones As New miclases
                lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                lblfecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha)
                gridHorarios = funciones.creadataset("select horario,idhorario from horarios2017 where turno='M' ", gridHorarios)
                gridHorariosVespertino = funciones.creadataset("select horario,idhorario from horarios2017 where turno='V' ", gridHorariosVespertino)
                llenaAgenda()
                llenaAgendaVespertino()
            Catch
                Messagebox1.ShowMessage("Datos Incorrectos :( ")
            End Try
        End If
    End Sub

    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
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



End Class
