Imports System.Data
Imports System.Data.SqlClient
Imports System.Security.Cryptography
Partial Class _Default
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim color As Integer
    Dim lasfunciones As New Funciones
    Dim funciones As New miclases
    Dim fun As New miclases
    Sub estableceColor(ByVal fila As String, ByVal posicion As String, ByVal miestado As String)
        'color = color + 10
        'If color >= 50 Then
        ' color = 0
        'End If
        Select Case miestado
            Case "AGENDADO"

                CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)



            Case "REALIZADO"

                CType(gridHorarios.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)



        End Select
    End Sub
    Sub estableceColorVespertino(ByVal fila As String, ByVal posicion As String, ByVal miestado As String)
        'color = color + 10
        'If color >= 50 Then
        ' color = 0
        'End If
        Select Case miestado
            Case "AGENDADO"


                CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0)


            Case "REALIZADO"


                CType(gridHorariosVespertino.Items(fila - 1).Cells(posicion).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 180, 0)


        End Select
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

        'lblCuantos.Text = 0
        'lbAgendados.Text = 0
        'CONSULTA HO
        comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
       " clientes.paterno+' '+clientes.materno+' '+clientes.nombre as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
       "inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
       "and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='M' order by agenda.idhorario"

        'CONSULTA SM/CP
        'comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        '" clientes.nombre+' '+clientes.paterno+' '+clientes.materno as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
        '"inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        '"and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='M' order by agenda.idhorario"
        ''FIN DE CONSULTA
        'comando.Parameters.Add(New SqlParameter("@fecha", SqlDbType.DateTime))
        'comando.Parameters("@fecha").Value = lblfecha.Text.Trim
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

            'lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1
            'lbAgendados.Text = Int(lbAgendados.Text.Trim) + 1
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

                'FISIOHO
                'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 22)
                'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 7
                'If cnombre = "" Then
                '    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22)
                '    'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                '    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 5
                'End If
                'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                ''gridHorarios.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                ''estableceColorLA(val1, val2, latencion)
                'estableceColor(val1, val2, estatus)

                'FISIO SM/CP

                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 12) + ("-") + (latencion)
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 8
                If cnombre = "" Then
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22) + ("-") + (latencion)
                    'CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 5
                End If
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                CType(gridHorarios.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                'gridHorarios.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                'estableceColorLA(val1, val2, latencion)
                estableceColor(val1, val2, estatus)

            Next
        End If

        'deshabilita los disponibles en fechas anteriores al dia de hoy
        Dim auxfecha As Date = lblfecha.Text
        Dim hoy As Date = Now.Date
        If auxfecha < hoy Then
            Dim numCol As Int16 = funciones.negocio("columna_esp")
            Dim cuentaC As Integer = 1
            Dim cuentaF As Integer = 0
            Do While cuentaF < gridHorarios.Items.Count
                cuentaC = 1
                Do While cuentaC <= numCol
                    If CType(gridHorarios.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Text = "DISPONIBLE" Then
                        CType(gridHorarios.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Enabled = True
                    End If
                    cuentaC = cuentaC + 1
                Loop
                cuentaF = cuentaF + 1
            Loop
        End If


        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='AGENDADO'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbAgendados.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='CANCELADO'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbCancelados.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='REALIZADO'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbAtendidos.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CA'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbConfirmados.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO')  and idlatencion='RE'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbEnEspera.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='OP'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbEnEstudio.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CD'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lbdomicilio.Text = leer.GetValue(0).ToString.Trim
        End If
        conexion.Close()

        comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CM'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            lblcmedica.Text = leer.GetValue(0).ToString.Trim
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
        SQL = SQL & "columna =  1 and turno='M' "
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
        SQL = SQL & "columna =  2 and turno='M' "
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
        SQL = SQL & "columna =  3 and turno='M' "
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
        SQL = SQL & "columna =  4 and turno='M' "
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
        SQL = SQL & "columna =  5 and turno='M' "
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
        SQL = SQL & "columna =  6 and turno='M' "
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
        SQL = SQL & "columna =  7 and turno='M' "
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
        SQL = SQL & "columna =  8 and turno='M' "
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
        SQL = SQL & "columna =  9 and turno='M' "
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
        SQL = SQL & "columna =  10 and turno='M' "
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

        'lblCuantos.Text = 0
        'lbAgendados.Text = 0
        comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        " clientes.paterno+' '+clientes.materno+' '+clientes.nombre as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
        "inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        "and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='V' order by agenda.idhorario"
        'consulta sm/cp
        'comando.CommandText = "select agenda.idcliente, agenda.idhorario, agenda.fecha, agenda.duracion, agenda.posicion," & _
        '" clientes.nombre+' '+clientes.paterno+' '+clientes.materno as elnombre, agenda.estado, agenda.idCita, agenda.idlatencion from agenda " & _
        '"inner join clientes on agenda.idcliente=clientes.idcliente where agenda.fecha='" + lblfecha.Text.Trim + "' " & _
        '"and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') AND agenda.turno='V' order by agenda.idhorario"
        'fin de consulta
        'comando.Parameters.Add(New SqlParameter("@fecha", SqlDbType.DateTime))
        'comando.Parameters("@fecha").Value = lblfecha.Text.Trim
        conexion.Open()
        leer = comando.ExecuteReader
        cadregistros = ""
        Do While leer.Read
            cadregistros = cadregistros & leer.GetValue(1).ToString.Trim & "-" & leer.GetValue(4).ToString & "-" & leer.GetValue(3).ToString & "-" & Left(leer.GetValue(5).ToString.Trim, 30) & "-" & leer.GetValue(7).ToString & "-" & leer.GetValue(6).ToString & "-" & leer.GetValue(0).ToString & "-" & leer.GetValue(8).ToString & ","

            'CType(gridHorariosVespertinoVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(1), LinkButton).Text = Left(leer.GetValue(5).ToString.Trim, 30)
            'CType(gridHorariosVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString).Controls(3), Label).Text = leer.GetValue(7).ToString.Trim
            'gridHorariosVespertino.Items(leer.GetValue(1).ToString.Trim).Cells(leer.GetValue(4).ToString.Trim).BackColor = Drawing.Color.FromArgb(128, 0, 0)
            'estableceColor(leer.GetValue(3).ToString.Trim, leer.GetValue(4).ToString.Trim, leer.GetValue(6).ToString.Trim)
            ' lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1

            'lblCuantos.Text = Int(lblCuantos.Text.Trim) + 1
            'lbAgendados.Text = Int(lbAgendados.Text.Trim) + 1
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


                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(cnombre, 1, 22)
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 7
                If cnombre = "" Then
                    CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Text = Mid(funciones.entry(4, cad, "-").ToString.Trim, 1, 22)
                    'CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Enabled = False
                    CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).Font.Size = 5
                End If
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(3), Label).Text = cita & "," & duracion
                CType(gridHorariosVespertino.Items(val1 - 1).Cells(val2).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250)

                'gridHorariosVespertino.Items(val1).Cells(val2).BackColor = Drawing.Color.FromArgb(200, 0, 0)
                'estableceColorLA(val1, val2, latencion)
                estableceColorVespertino(val1, val2, estatus)

            Next
        End If

        'deshabilita los disponibles en fechas anteriores al dia de hoy
        Dim auxfecha As Date = lblfecha.Text
        Dim hoy As Date = Now.Date
        If auxfecha < hoy Then
            Dim numCol As Int16 = funciones.negocio("columna_esp")
            Dim cuentaC As Integer = 1
            Dim cuentaF As Integer = 0
            Do While cuentaF < gridHorariosVespertino.Items.Count
                cuentaC = 1
                Do While cuentaC <= numCol
                    If CType(gridHorariosVespertino.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Text = "DISPONIBLE" Then
                        CType(gridHorariosVespertino.Items(cuentaF).Cells(cuentaC).Controls(1), LinkButton).Enabled = False
                    End If
                    cuentaC = cuentaC + 1
                Loop
                cuentaF = cuentaF + 1
            Loop
        End If

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='CANCELADO'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    'lnkCanceladas.Text = leer.GetValue(0).ToString.Trim
        '    lbCancelados.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='REALIZADO'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lbAtendidos.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CA'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lbConfirmados.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO')  and idlatencion='RE'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lbEnEspera.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='OP'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lbEnEstudio.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CD'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lbdomicilio.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()

        'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and (estado='AGENDADO' or estado='REALIZADO') and idlatencion='CM'"
        'conexion.Open()
        'leer = comando.ExecuteReader
        'If leer.Read Then
        '    lblcmedica.Text = leer.GetValue(0).ToString.Trim
        'End If
        'conexion.Close()



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
        SQL = SQL & "columna =  1 and turno='V' "
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
        SQL = SQL & "columna =  2 and turno='V' "
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
        SQL = SQL & "columna =  3 and turno='V' "
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
        SQL = SQL & "columna =  4 and turno='V' "
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
        SQL = SQL & "columna =  5 and turno='V' "
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
        SQL = SQL & "columna =  6 and turno='V' "
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
        SQL = SQL & "columna =  7 and turno='V' "
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
        SQL = SQL & "columna =  8 and turno='V' "
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
        SQL = SQL & "columna =  9 and turno='V' "
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
        SQL = SQL & "columna =  10 and turno='V' "
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
            gridHorarios = funciones.creadataset("select horario,idhorario from horarios WHERE turno='M'", gridHorarios)
            gridHorariosVespertino = funciones.creadataset("select horario,idhorario from horarios WHERE turno='V'", gridHorariosVespertino)
            'gFijo = funciones.creadataset("select horario,idhorario from horarios2017 ", gFijo)
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
                Dim lafecha As Date = lblfecha.Text
                lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
                calendario.SelectedDate = lblfecha.Text
            Catch ex As Exception
                Try
                    'Dim usuario As String = Context.Items("idusuario").ToString
                    Dim usuario As String = Session("idusuario").ToString
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
            ' llenaAgendaVespertino()
            gridHorariosVespertino.Visible = False
            'Reacomoda()
        End If
        'verificaReporte()
    End Sub
    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub
    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        llenaAgenda()
        'llenaAgendaVespertino()
    End Sub

    Protected Sub gridHorarios_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridHorarios.ItemCommand
        Dim mifila As String = e.Item.ItemIndex.ToString.Trim
        'Agregado para aumentar el numero de columna a mas de 9
        Dim columnav As String

        If e.CommandName = "lnk10" Then
            columnav = Right(e.CommandName, 2)
        Else
            columnav = Right(e.CommandName, 1)
        End If

        Dim columna As String = columnav

        'Dim columna As String = Right(e.CommandName, 1)
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
            Context.Items.Add("turno", "M")
            Server.Transfer("clientes.aspx", True)
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
                    '' modificar de 9 a 10 30052019
                    Do While micolumna2 <= 10 And bandera
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
                hfcita.Value = cita
                Context.Items.Add("fechacita", lblfecha.Text.Trim)
                hffechacita.Value = lblfecha.Text.Trim
                'se deshabilito-inicio
                'Context.Items.Add("condicion", Right(var, Len(var) - 3))
                'hfcondicion.Value = Right(var, Len(var) - 3)
                'fin
                'nuevos valores enviados
                Context.Items.Add("duracion", duracion) 'errrrorrrrrrrrrrrrrrrrrita
                hfduracion.Value = duracion
                Context.Items.Add("posicion", columna)
                hfposicion.Value = columna
                Context.Items.Add("idhorario", gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
                hfidhorario.Value = gridHorarios.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
                Context.Items.Add("turno", "M")
                hfturno.Value = "M"



                If Session("rol") = "TE" Then
                    mpuprolTE.Show()
                Else
                    Server.Transfer("datosCitas.aspx", False)
                End If

            Else

                If Session("rol") = "TE" Then
                    cita = funciones.entry(1, CType(gridHorarios.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                    Context.Items.Add("elidCita", cita)
                    Context.Items.Add("posicion", columna)
                    Context.Items.Add("turno", "M")
                    Server.Transfer("expedientemedico.aspx", False)
                Else


                End If
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
    Protected Sub gridHorariosVespertino_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridHorariosVespertino.ItemCommand
        Dim mifila As String = e.Item.ItemIndex.ToString.Trim
        'Agregado para aumentar el numero de columna a mas de 9
        Dim columnav As String

        If e.CommandName = "lnk10" Then
            columnav = Right(e.CommandName, 2)
        Else
            columnav = Right(e.CommandName, 1)
        End If

        Dim columna As String = columnav

        'Dim columna As String = Right(e.CommandName, 1)
        Dim micolumna As LinkButton = gridHorariosVespertino.Items(mifila).Cells(columna).FindControl("lnk" + columna.ToString.Trim)
        Dim valor As String = micolumna.Text.Trim
        Dim cita, duracion As String
        Dim funciones As New miclases

        If valor = "DISPONIBLE" Then
            Context.Items.Add("duracion", mifila)
            Context.Items.Add("posicion", columna)
            Context.Items.Add("idhorario", gridHorariosVespertino.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
            Context.Items.Add("elhorario", gridHorariosVespertino.Items(e.Item.ItemIndex).Cells(0).Text)
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Context.Items.Add("turno", "V")
            Server.Transfer("clientes.aspx", True)
        Else
            ' If CType(gridHorariosVespertino.Items(mifila).Cells(columna).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(250, 250, 250) Then  'FromArgb(40, 41, 40)
            'If gridHorariosVespertino.Items(mifila).Cells(columna).BackColor = Drawing.Color.FromArgb(200, 0, 0) Then

            If CType(gridHorariosVespertino.Items(mifila).Cells(columna).Controls(1), LinkButton).ForeColor = Drawing.Color.FromArgb(0, 0, 0) Then
                Dim contador As Integer = 0
                Dim cuentaFilas As Integer = gridHorariosVespertino.Items.Count
                Dim micolumna2 As Integer = 1
                Dim columna2 As LinkButton
                Dim var As String = ""
                Dim bandera As Boolean = True
                Do While contador < cuentaFilas
                    bandera = True
                    Do While micolumna2 <= 10 And bandera
                        columna2 = gridHorariosVespertino.Items(contador).Cells(micolumna2).FindControl("lnk" + micolumna2.ToString.Trim)
                        If columna2.Text = "DISPONIBLE" Then
                            bandera = False
                            var = var + " or idHorario='" + (contador + 1).ToString.Trim + "'"
                        End If
                        micolumna2 = micolumna2 + 1
                    Loop
                    micolumna2 = 1
                    contador = contador + 1
                Loop
                cita = funciones.entry(1, CType(gridHorariosVespertino.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                duracion = funciones.entry(2, CType(gridHorariosVespertino.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                Context.Items.Add("elidCita", cita)
                hfcita.Value = cita
                Context.Items.Add("fechacita", lblfecha.Text.Trim)
                hffechacita.Value = lblfecha.Text.Trim
                'Context.Items.Add("condicion", Right(var, Len(var) - 3))
                'hfcondicion.Value = Right(var, Len(var) - 3)
                'nuevos valores enviados
                Context.Items.Add("duracion", duracion) 'errrrorrrrrrrrrrrrrrrrrita
                hfduracion.Value = duracion
                Context.Items.Add("posicion", columna)
                hfposicion.Value = columna
                Context.Items.Add("idhorario", gridHorariosVespertino.DataKeys.Item(e.Item.ItemIndex).ToString.Trim)
                hfidhorario.Value = gridHorariosVespertino.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
                Context.Items.Add("turno", "V")
                hfturno.Value = "V"


                If Session("rol") = "TE" Then
                    mpuprolTE.Show()
                Else
                    Server.Transfer("datosCitas.aspx", False)
                End If

            Else

                If Session("rol") = "TE" Then
                    cita = funciones.entry(1, CType(gridHorariosVespertino.Items(mifila).Cells(columna).Controls(3), Label).Text, ",")
                    Context.Items.Add("elidCita", cita)
                    Context.Items.Add("posicion", columna)
                    Context.Items.Add("turno", "V")
                    Server.Transfer("expedientemedico.aspx", False)
                Else


                End If

            End If
        End If
    End Sub
    Protected Sub lkb2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkb2.Click
        Context.Items.Add("elidCita", hfcita.Value)
        Context.Items.Add("posicion", hfposicion.Value)
        Context.Items.Add("turno", hfturno.Value)
        Server.Transfer("expedientemedico.aspx", False)
    End Sub
    Protected Sub lkb1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lkb1.Click
        Context.Items.Add("elidCita", hfcita.Value)
        Context.Items.Add("fechacita", hffechacita.Value)
        ' Context.Items.Add("condicion", hfcondicion.Value)
        Context.Items.Add("duracion", hfduracion.Value)
        Context.Items.Add("posicion", hfposicion.Value)
        Context.Items.Add("idhorario", hfidhorario.Value)
        Context.Items.Add("turno", hfturno.Value)
        Server.Transfer("datosCitas.aspx", False)
    End Sub
    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        'Select Case Left(Key, 1)
        '   Case "I"
        'End Select
        Dim funciones As New miclases
        funciones.grabaDatos("update clientes set activo='False' where idCliente='" + Key.Trim + "';")

        'Context.Items.Add("fecha", lblfecha.Text.Trim)
        'Server.Transfer("default.aspx", True)
    End Sub

    'Protected Sub lnkCanceladas_Click(ByVal sender As Object, ByVal e As System.EventArgs)
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("canceladas.aspx", False)
    'End Sub

    'Protected Sub lnkCanceladas_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkCanceladas.Click
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("canceladas.aspx", False)
    'End Sub

    Sub verificaReporte()
        Dim hoy As Date = Now.Date
        Dim ultimoReporte As Date = funciones.leerValor("select ultimoReporte from configuracion ")
        Dim auxF As String = String.Format("{0:dd/MM/yyyy}", ultimoReporte)
        If ultimoReporte < hoy Then
            Dim conexion As SqlConnection = funciones.conecta
            Dim comando As SqlCommand = conexion.CreateCommand
            comando.CommandText = "select convert(varchar, fecha, 103) as fecha,nombre+' '+paterno+' '+materno " & _
            "as elnombre,abono, idpago from abonos left join clientes on  " & _
            "abonos.idcliente = clientes.idcliente where abono<>'0' and fecha='" + auxF + "' order by elnombre"
            Dim oLeer As SqlDataReader
            conexion.Open()
            Dim htmlPagos = "<table width='400' border='1' cellspacing='0' cellpadding='5' style=' width:400px; font-family: Calibri; font-size:16px ' > " & _
            "<tr style='background-color:#f4f4f4; color:#1B3774;'><td>Nombre</td><td></td><td style='width: 60px; text-align:center'>Cantidad</td> </tr>"
            oLeer = comando.ExecuteReader
            Dim sEF As Double = 0
            Dim sTC As Double = 0
            Do While oLeer.Read
                htmlPagos = htmlPagos + "<tr><td>" + Left(oLeer.GetValue(1).ToString, 170) + "</td><td>" + oLeer.GetValue(3).ToString + "</td><td style='width: 60px; text-align:center'>" + oLeer.GetValue(2).ToString + "</td> </tr>"
                Select Case oLeer.GetValue(3).ToString
                    Case "EF"
                        sEF = sEF + oLeer.GetValue(2).ToString
                    Case "TC"
                        sTC = sTC + oLeer.GetValue(2).ToString
                End Select
            Loop
            oLeer.Close()
            conexion.Close()
            Dim htmlTotal As String = "<table width='400' border='1' cellspacing='0' cellpadding='5' style=' width:400px; font-family: Calibri; font-size:16px; text-align:center; ' > " & _
            "<tr style='background-color:#f4f4f4; color:#1B3774;'><td>TOTAL</td><td>TC</td><td>EF</td></tr>" & _
            "<tr ><td>" + Format((sTC + sEF), "###,###,###0.00").ToString + "</td><td>" + Format(sTC, "###,###,###0.00").ToString + "</td><td>" + Format(sEF, "###,###,###0.00").ToString + "</td></tr>" & _
            "</table>"
            htmlPagos = htmlTotal + htmlPagos + "</table>"

            Dim Html As String = "<table  border='0' cellspacing='0' cellpadding='2' style=' width:400px; font-family: Calibri; font-size:16px ' >" & _
            "<tr><td style='width: 190px;font-weight:bold'>Campestre - " + auxF + "</td><td style='vertical-align:bottom'><hr style='padding:0' /></td></tr>" & _
            "</table>"
            Html = Html + "<table width='400' border='1' cellspacing='0' cellpadding='5' style=' width:400px; font-family: Calibri; font-size:16px ' > " & _
            "<tr><td style='text-align:left; background-color:#f4f4f4; color:#1B3774; width: 196px;'>N&uacute;mero de Citas Agendadas</td><td style='text-align:center'>" + funciones.leerValor("select count(idcita) from agenda where fecha='" + auxF + "'").ToString + "</td></tr>" & _
            "<tr><td style='text-align:left; background-color:#f4f4f4; color:#1B3774; width: 196px;'>Citas Cerradas</td><td style='text-align:center'>" + funciones.leerValor("select count(idcita) from agenda where fecha='" + auxF + "' and estado='REALIZADO'").ToString + "</td></tr>" & _
            "<tr><td style='text-align:left; background-color:#f4f4f4; color:#1B3774; width: 196px;'>Serv. Realizados</td><td style='text-align:center'>" + funciones.leerValor("select count(idcita) from abonos where fecha='" + auxF + "'").ToString + "</td></tr>" & _
            "<tr><td style='text-align:left; background-color:#f4f4f4; color:#1B3774; width: 196px;'>Citas Canceladas</td><td style='text-align:center'>" + funciones.leerValor("select count(idcita) from agenda where fecha='" + auxF + "' and estado='CANCELADO'").ToString + "</td></tr>" & _
            "<tr><td style='text-align:left; background-color:#f4f4f4; color:#1B3774; width: 196px;'>Citas sin Cerrar</td><td style='text-align:center'>" + funciones.leerValor("select count(idcita) from agenda where fecha='" + auxF + "' and estado='AGENDADO'").ToString + "</td></tr>" & _
            "</table><table border='0' cellspacing='0' cellpadding='0' style='width:400px'><tr><td><hr/></td></tr></table></div><div style='font-family:Calibri; font-size:12px; text-align:left'>TC=Tarjeta de Credito, EF=Efectivo</div>"

            Html = Html + htmlPagos

            comando.CommandText = "select nombre+' '+PATERNO+' '+MATERNO from agenda LEFT JOIN clientes " & _
            "on clientes.idcliente=agenda.idcliente where agenda.estado='AGENDADO' AND FECHA='" + auxF + "'"
            conexion.Open()
            oLeer = comando.ExecuteReader
            Dim htmlSincerrar As String = "<table  border='0' cellspacing='0' cellpadding='2' style=' width:400px; font-family: Calibri; font-size:16px ' >" & _
            "<tr><td style='width: 190px;font-weight:bold'>Citas Sin Cerrar</td><td style='vertical-align:bottom'><hr style='padding:0' /></td></tr>" & _
            "</table>"
            htmlSincerrar = htmlSincerrar + "<table width='400' border='1' cellspacing='0' cellpadding='5' style=' width:400px; font-family: Calibri; font-size:16px ' >" & _
            "<tr style='background-color:#f4f4f4; color:#1B3774;'><td>Nombre</td>"
            Do While oLeer.Read
                htmlSincerrar = htmlSincerrar + "<tr><td>" + Left(oLeer.GetValue(0).ToString, 230) + "</td></tr>"
            Loop
            htmlSincerrar = htmlSincerrar + "</table>"
            oLeer.Close()
            conexion.Close()

            Html = Html + htmlSincerrar

            comando.CommandText = "select nombre+' '+paterno+' '+materno as nombre,cancelacion as motivo " & _
            "from agenda LEFT JOIN cancelaciones on cancelaciones.idcita=agenda.idcita left join clientes " & _
            "on clientes.idcliente=agenda.idcliente where agenda.fecha='" + auxF + "' AND agenda.estado='CANCELADO'"
            conexion.Open()
            oLeer = comando.ExecuteReader
            Dim htmlCancelaciones As String = "<table  border='0' cellspacing='0' cellpadding='2' style=' width:400px; font-family: Calibri; font-size:16px ' >" & _
            "<tr><td style='width: 190px;font-weight:bold'>Cancelaciones</td><td style='vertical-align:bottom'><hr style='padding:0' /></td></tr>" & _
            "</table>"
            htmlCancelaciones = htmlCancelaciones + "<table width='400' border='1' cellspacing='0' cellpadding='5' style=' width:400px; font-family: Calibri; font-size:16px ' >" & _
            "<tr style='background-color:#f4f4f4; color:#1B3774; width:200px'><td>Nombre</td><td style='width: 200px; text-align:center'>Motivo</td> </tr>"
            Do While oLeer.Read
                htmlCancelaciones = htmlCancelaciones + "<tr><td>" + Left(oLeer.GetValue(0).ToString, 170) + "</td><td>" + oLeer.GetValue(1).ToString + "</td> </tr>"
            Loop
            htmlCancelaciones = htmlCancelaciones + "</table>"

            oLeer.Close()
            conexion.Close()

            Html = Html + htmlCancelaciones
            ' Try
            'lasfunciones.enviaCorreoXls("rperez@fisiocare.com.mx,administracion@fisiocare.com.mx,hrivero@zonamedicasureste.com,gbriceno@zonamedicasureste.com", "", "", "Informe Diario Star Medica", Html)
            'funciones.grabaDatos("update configuracion set ultimoReporte='" + String.Format("{0:dd/MM/yyyy}", hoy) + "'")
            'Catch

            'End Try
        End If
    End Sub

    'Pacientes()
    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
        Server.Transfer("clientes.aspx", True)
    End Sub





    Protected Sub Calendar1_SelectionChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles calendario.SelectionChanged
        Try
            Dim lafecha As Date = calendario.SelectedDate
            Dim funciones As New miclases
            lblfecNom.Text = lafecha.Day.ToString + " " + MonthName(lafecha.Month) + " " + lafecha.Year.ToString
            lblfecha.Text = String.Format("{0:dd/MM/yyyy}", lafecha)
            gridHorarios = funciones.creadataset("select horario,idhorario from horarios WHERE turno='M' ", gridHorarios)
            'gridHorariosVespertino = funciones.creadataset("select horario,idhorario from horarios2017 WHERE turno='V' ", gridHorariosVespertino)
            Session("FechaAgenda") = lblfecha.Text
            llenaAgenda()
            'llenaAgendaVespertino()

        Catch
            Messagebox1.ShowMessage("Datos Incorrectos ....")
        End Try
    End Sub

    'Protected Sub LinkButton6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton6.Click
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("canceladas.aspx", False)
    'End Sub
    Protected Sub calendario_VisibleMonthChanged(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MonthChangedEventArgs) Handles calendario.VisibleMonthChanged
        ModalPopupExtender1.Show()
    End Sub

    Protected Sub LinkButton3_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub LinkButton7_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub LinkButton8_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub
    'Protected Sub btfacth_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btfacth.Click
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("facturacionth.aspx", False)
    'End Sub
    'Protected Sub btfactd_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btfactd.Click
    '    Context.Items.Add("fecha", lblfecha.Text.Trim)
    '    Server.Transfer("facturaciontd.aspx", False)
    'End Sub
    Protected Sub ButtonBCitas_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
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
    

    Protected Sub enviarafacturacion(ByVal sender As Object, ByVal e As System.EventArgs)


        Dim usuario As String = Session("usuario").ToString
        Dim password As String = Session("passusuario").ToString

        'inicio de proceso de encriptacion md5
        Dim md5 As MD5CryptoServiceProvider
        Dim bytValue() As Byte
        Dim bytHash() As Byte
        Dim strPassOutput As String
        Dim i As Integer
        strPassOutput = ""

        md5 = New MD5CryptoServiceProvider

        bytValue = System.Text.Encoding.UTF8.GetBytes(password)

        bytHash = md5.ComputeHash(bytValue)
        md5.Clear()

        For i = 0 To bytHash.Length - 1
            strPassOutput &= bytHash(i).ToString("x").PadLeft(2, "0")
        Next

        Dim passencriptado As String = strPassOutput

        Response.Redirect("https://facturacionsm.agemed.com.mx?usuario=" + usuario.ToString + "&contraseña=" + passencriptado.ToString + "&db=" + "fisiocareSM")
        
    End Sub





End Class
