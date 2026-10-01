Imports System.Data.SqlClient
Partial Class clientesCP
    Inherits System.Web.UI.Page
    Dim fun As New miclases
    Dim lasfunciones As New Funciones
    Sub llenaduracion()
        Dim i As Integer = 0
        Dim cad() As String = {"", "01 Turno (30 min)", "02 Turno (1 hora)", "03 Turno (1 hora 30 min)", "04 Turno (2 horas)", "05 Turno (2 horas 30 min)", "06 Turno (3 horas)", "07 Turno (3 horas 30 min)", "08 Turno (4 horas)", "09 Turno (4 horas 30 min)", "10 Turno (5 horas)"}
        cmbDuracion.Items.Add("--seleccione una opcion--")
        cmbDuracion.Items(0).Value = 0
        For i = 1 To 10
            cmbDuracion.Items.Add(i)
            cmbDuracion.Items(i).Value = i
            cmbDuracion.Items(i).Text = cad(i)
        Next
    End Sub

    Sub llenaDatos()
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta("fisiocareCP")
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        datos.Visible = False
        comando.CommandText = "select paterno,materno,nombre,edad,isnull(sexo,'0') as sexo,email,telefono,celular," & _
        "dtInicio,activo,idDoctor,idCencostos,iddatosfac,razonsocial,rfc,calle,noExt,colonia,cp,municipio," & _
        "ciudad,edo,pais,formadepago,nocuenta from clientes where idCliente='" + cmbclientes.SelectedValue.ToString.Trim + "'"
        conexion.Open()
        leer = comando.ExecuteReader
        If leer.Read Then
            txtPaterno.Text = leer.GetValue(0).ToString.Trim
            txtMaterno.Text = leer.GetValue(1).ToString.Trim
            txtNombre.Text = leer.GetValue(2).ToString.Trim
            txtedad.Text = leer.GetValue(3).ToString
            cmbsexo.SelectedValue = leer.GetValue(4).ToString
            txtemail.Text = leer.GetValue(5).ToString.Trim
            txtTelefono.Text = leer.GetValue(6).ToString.Trim
            txtCelular.Text = leer.GetValue(7).ToString.Trim

            txtFechaIni.Text = leer.GetValue(8).ToString.Trim
            If txtFechaIni.Text.Trim = "" Then
                txtFechaIni.Text = Format("{0:dd/MM/yyyy}", lblfecha.Text.Trim)
            End If

            If leer.GetValue(9).ToString.Trim = "True" Then
                chkActivo.Checked = True
            Else
                txtFechaIni.Text = Format("{0:dd/MM/yyyy}", lblfecha.Text.Trim)
                chkActivo.Checked = False
            End If

            Dim idDoctor As String = leer.GetValue(10).ToString.Trim
            If idDoctor = "" Then
                idDoctor = "0"
            End If
            cmbDoctores.SelectedValue = idDoctor
            cmbCostos.SelectedValue = leer.GetValue(11).ToString

            Dim Iddatosfac As String = leer.GetValue(12).ToString.Trim
            txtRazonSocial.Text = leer.GetValue(13).ToString.Trim
            txtrfc.Text = leer.GetValue(14).ToString.Trim
            txtcalle.Text = leer.GetValue(15).ToString.Trim
            txtnoext.Text = leer.GetValue(16).ToString.Trim
            txtcolonia.Text = leer.GetValue(17).ToString.Trim
            txtcp.Text = leer.GetValue(18).ToString.Trim
            txtmunicipio.Text = leer.GetValue(19).ToString.Trim
            txtciudad.Text = leer.GetValue(20).ToString.Trim
            txtestado.Text = leer.GetValue(21).ToString.Trim
            txtpais.Text = leer.GetValue(22).ToString.Trim
            Dim xxx = leer.GetValue(23).ToString.Trim
            cmbformapago.SelectedValue = leer.GetValue(23).ToString.Trim
            txtcuenta.Text = leer.GetValue(24).ToString.Trim
            'txtDomicilio.Text = leer.GetValue(3).ToString.Trim

            If Iddatosfac = "" Then
                Iddatosfac = "0"
            Else
                If txtRazonSocial.Text.Trim <> "" And txtrfc.Text.Trim <> "" And txtciudad.Text.Trim <> "" And txtemail.Text.Trim <> "" And Iddatosfac <> "0" Then
                    Dim valores(,) As String = funciones.leerValores("select nombre,rfc,calle,NoExt,Colonia," & _
                    "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,email from catfacturas where iddatosfac='" + Iddatosfac + "'", 13, "fisiocareCP")
                    txtRazonSocial.Text = valores(0, 0)
                    txtrfc.Text = valores(1, 0)
                    txtcalle.Text = valores(2, 0)
                    txtnoext.Text = valores(3, 0)
                    txtcolonia.Text = valores(4, 0)
                    txtcp.Text = valores(5, 0)
                    txtmunicipio.Text = valores(6, 0)
                    txtciudad.Text = valores(7, 0)
                    txtestado.Text = valores(8, 0)
                    txtpais.Text = valores(9, 0)
                    cmbformapago.SelectedValue = valores(10, 0)
                    txtcuenta.Text = valores(11, 0)
                    'txtDomicilio.Text = resultados(1, 0)
                End If
            End If
            cmbDatosfac.SelectedValue = Iddatosfac


            datos.Visible = True
            If lblhorario.Text.Trim <> "na" Then
                masdatos.Visible = True
            End If
            cmbDatosfac.Enabled = False
        End If
        leer.Close()
        conexion.Close()
        conexion.Dispose()
    End Sub
    Sub llenafechas()
        Dim funciones As New miclases
        gridDias = funciones.LLenaGrid("select semanas_agendar from semanasAgendar", gridDias, "fisiocareCP")
        Dim aux As DateTime = lblfecha.Text.Trim + " 00:00:00"
        Dim cuenta As Int16 = gridDias.Items.Count
        Dim contador As Int16 = 0
        Dim columnas As Int16 = 0
        Do While contador < cuenta
            columnas = 0
            Do While columnas < 7
                CType(gridDias.Items(contador).Cells(columnas).Controls(1), CheckBox).Text = Format(DateAdd(DateInterval.Day, columnas, aux), "dddd,dd/MM/yy")
                columnas = columnas + 1
            Loop
            aux = DateAdd(DateInterval.Day, 7, aux)
            contador = contador + 1
        Loop
        CType(gridDias.Items(0).Cells(0).Controls(1), CheckBox).Enabled = False
        CType(gridDias.Items(0).Cells(0).Controls(1), CheckBox).Checked = True
        'verificafecha()
    End Sub
    Sub verificafecha()
        Dim clases As New miclases
        Dim cuentaF As Int16 = gridDias.Items.Count
        Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count - 1
        Dim filas As Int16 = 0
        Dim columnas As Int16 = 1
        Dim auxfecha As Date
        Dim auxfecha2 As String
        Do While filas < cuentaF
            Do While columnas < cuentaC
                auxfecha = CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Text
                auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)
                If clases.leerValor("SELECT COUNT(idCita) AS cuentaC FROM agenda WHERE fecha = '" + auxfecha + "' " & _
                "and idhorario='" + lblhorario.Text + "'", "fisiocareCP") >= 4 Then
                    gridDias.Items(filas).Cells(columnas).Enabled = False
                End If
                columnas = columnas + 1
            Loop
            columnas = 0
            filas = filas + 1
        Loop
    End Sub
    Sub grabaEnAgenda(ByVal posicion As String, ByVal lafecha As String)
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta("fisiocareCP")
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim axu = lblhorario.Text.Trim
        comando.CommandText = "insert into agenda(idcliente,idhorario,fecha,duracion,posicion,estado,idDoctor,dtinicio,cobservaciones)" & _
                              "values(@idcliente,@idhorario,@fecha,@duracion,@posicion,'AGENDADO',@idDoctor,@fechaini,@observaciones)"
        comando.Parameters.Add(New SqlParameter("@idcliente", Data.SqlDbType.Int)).Value = cmbclientes.SelectedValue
        comando.Parameters.Add(New SqlParameter("@idhorario", Data.SqlDbType.SmallInt)).Value = lblhorario.Text.Trim
        comando.Parameters.Add(New SqlParameter("@fecha", Data.SqlDbType.VarChar)).Value = lafecha
        comando.Parameters.Add(New SqlParameter("@duracion", Data.SqlDbType.SmallInt)).Value = cmbDuracion.SelectedValue            ' lblfila.Text.Trim
        comando.Parameters.Add(New SqlParameter("@posicion", Data.SqlDbType.SmallInt)).Value = posicion
        comando.Parameters.Add(New SqlParameter("@idDoctor", Data.SqlDbType.Int)).Value = cmbDoctores.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@fechaini", Data.SqlDbType.DateTime)).Value = txtFechaIni.Text.Trim
        comando.Parameters.Add(New SqlParameter("@observaciones", Data.SqlDbType.VarChar)).Value = txtobservaciones.Text.Trim
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
    Sub agendar()
        Dim funciones As New miclases
        Dim cuentaF As Int16 = gridDias.Items.Count
        Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count - 1
        Dim filas As Int16 = 0
        Dim columnas As Int16 = 1
        Dim auxfecha As Date
        Dim auxfecha2 As String
        Dim miposicion As Int16 = 1
        Dim bandera As Boolean = True
        Do While filas < cuentaF
            Do While columnas < cuentaC
                auxfecha = CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Text
                auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)
                If CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Checked = True Then
                    Do While bandera
                        If funciones.leerValor("SELECT COUNT(idCita) AS cuentaC FROM agenda WHERE " & _
                        "idhorario='" + lblhorario.Text.Trim + "' and fecha='" + auxfecha2 + "' and posicion='" + miposicion.ToString + "'", "fisiocareCP") = 0 Then
                            grabaEnAgenda(miposicion.ToString.Trim, auxfecha2)
                            bandera = False
                        Else
                            miposicion = miposicion + 1
                        End If
                    Loop
                End If
                miposicion = 1
                bandera = True
                columnas = columnas + 1
            Loop
            columnas = 0
            filas = filas + 1
        Loop
    End Sub
    Private Function validafechas(ByVal Horario As String, ByVal Columna As String) As String
        Dim funciones As New miclases
        Dim cuentaF As Int16 = gridDias.Items.Count
        Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count
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
        isalas = lasfunciones.negocioCP("numsalas")
        For j = 1 To isalas
            If j.ToString.Trim <> Columna.Trim Then
                cSalas = cSalas + j.ToString.Trim + ","
            End If
        Next
        cSalas = Left(cSalas, Len(cSalas) - 1)
        cSalas = Columna.Trim + "," + cSalas ''' todas las salasy pongo de primero la que di click
        'MsgBox(" # salas " & cSalas)
        '+++++++++++
        Dim PersonaAgendada As String = ""
        Dim auxPersonaAgendada As String = ""
        Dim auxHorario As Integer = Int(Horario) + Int(cmbDuracion.SelectedValue)
        Do While filas < cuentaF
            Do While columnas < cuentaC - 1
                auxfecha = CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Text
                auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)
                If CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Checked = True Then
                    auxPersonaAgendada = funciones.leerValor("select distinct fecha from  ( select idCita, fecha, " & _
                    "idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + cmbclientes.SelectedValue + " and estado='AGENDADO') as horarios " & _
                    "where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
                    "idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')", "fisiocareCP").ToString()
                    If auxPersonaAgendada <> "0" Then
                        PersonaAgendada = PersonaAgendada + auxfecha2.Trim + ","
                    End If
                    selvalida = selvalida + 1
                    '+++++++++++++++++
                    For k = 1 To funciones.num_elementos(cSalas, ",")
                        '++++++++++++++++
                        Columna2 = funciones.entry(k, cSalas, ",")   'idhorario >= " + Horario.Trim + " and " & _''' obtengo la sala que este disponible siempre en la cadena esta la primera sala es donde se dio click
                        consulta = "select agenda.idhorario, agenda.posicion, agenda.duracion from agenda " & _
                        " where agenda.fecha='" + auxfecha2 + "' and " & _
                        " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') order by agenda.idhorario "
                        bandera = False
                        banderaX = False
                        cadregistros = funciones.llenalista(consulta, "fisiocareCP") ''' idhorario-posicion-duracion horarios ocupado

                        'MsgBox("1) fecha: " & auxfecha2.Trim & " columna: " & Columna2.Trim & "  " & " ++ cadregistros: " & cadregistros)
                        If cadregistros.Trim <> "" Then
                            cadenafin = funciones.completalista(cadregistros, "0") ''idhorario-posicion; quita duarions y asigna los horarios que ocupan dos casillas osea mas de media hora
                            'MsgBox("2) *** lista valida: " & cadenafin)
                            For i = 0 To Int(cmbDuracion.SelectedValue) - 1
                                finhorario = CInt(Horario) + i
                                cad = Str(CInt(Horario) + i) + "-" + Columna2 '+ "-" + i.ToString.Trim
                                If funciones.lookup(cad.Trim, cadenafin, ",") > 0 Then
                                    bandera = True ' El horario esta ocupado
                                End If
                                If finhorario > 28 Then
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
                                k = 6
                            Else
                                If bandera = False Then
                                    cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                    k = 6
                                End If
                            End If
                            'MsgBox("3) *** es valido: " & bandera.ToString & "  cadOk = " & cadOk)
                        Else
                            If bandera = False Then ' si esta vacia la sala 
                                cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                k = 6
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
                End If
                columnas = columnas + 1
            Loop
            columnas = 0
            filas = filas + 1
        Loop
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
        Dim cuentaF As Int16 = gridDias.Items.Count
        Dim cuentaC As Int16 = gridDias.Items(0).Cells.Count - 1
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
        cListasalas = lasfunciones.negocioCP("columna_esp")
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
        Dim auxHorario As Integer = Int(Horario) + Int(cmbDuracion.SelectedValue)
        Do While filas < cuentaF
            Do While columnas < cuentaC - 1
                auxfecha = CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Text
                auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)
                If CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Checked = True Then
                    auxPersonaAgendada = funciones.leerValor("select distinct fecha from  ( select idCita, fecha, " & _
                   "idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + cmbclientes.SelectedValue + " and estado='AGENDADO') as horarios " & _
                   "where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
                   "idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')", "fisiocareCP").ToString()
                    If auxPersonaAgendada <> "0" Then
                        PersonaAgendada = PersonaAgendada + auxfecha2.Trim + ","
                    End If
                    selvalida = selvalida + 1
                    '+++++++++++++++++
                    For k = 1 To funciones.num_elementos(cSalas, ",")
                        '++++++++++++++++
                        Columna2 = funciones.entry(k, cSalas, ",")
                        consulta = "select agenda.idhorario, agenda.posicion, agenda.duracion from agenda " & _
                        " where agenda.fecha='" + auxfecha2 + "' and idhorario = " + Horario.Trim + " and " & _
                        " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO') order by agenda.idhorario "
                        bandera = False
                        cadregistros = funciones.llenalista(consulta, "fisiocareCP")
                        If cadregistros.Trim <> "" Then
                            cadenafin = funciones.completalista(cadregistros, "0")
                            For i = 0 To Int(cmbDuracion.SelectedValue) - 1
                                cad = Str(CInt(Horario) + i) + "-" + Columna2 '+ "-" + i.ToString.Trim
                                If funciones.lookup(cad.Trim, cadenafin, ",") > 0 Then
                                    bandera = True ' el horario esta ocupado
                                    'Novalido = True
                                    k = 4
                                End If
                            Next
                        Else
                            If bandera = False Then
                                ' MsgBox("CORRECTO !!! " & auxfecha2 & " posicion " & k.ToString)
                                cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                k = 4
                            End If
                        End If
                    Next '++++++++++++++
                End If
                columnas = columnas + 1
            Loop
            columnas = 0
            filas = filas + 1
        Loop
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
    Sub insertarcliente()
        Dim conexion As SqlConnection = fun.conecta("fisiocareCP")
        Dim comando As SqlCommand = conexion.CreateCommand

        '++ valida que no se pongan caracteres invalidos
        txtPaterno.Text = reemplazar(txtPaterno.Text)
        txtMaterno.Text = reemplazar(txtMaterno.Text)
        txtNombre.Text = reemplazar(txtNombre.Text)

        comando.CommandText = "insert into clientes(paterno,materno,nombre,elnombre,telefono,celular,adeudo," & _
        "idDoctor,email,iddatosfac,dtinicio,activo,edad,sexo,idCenCostos,razonsocial,rfc,calle,NoExt,Colonia," & _
        "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta)" & _
        "values(@paterno,@materno,@nombre,@elnombre,@telefono,@celular,'0',@idDoctor,@email,@iddatosfac," & _
        "@fechainicio,@activo,@edad,@sexo,@idCenCostos,@razonsocial,@rfc,@calle,@NoExt,@Colonia," & _
        "@Cp,@Municipio,@ciudad,@edo,@pais,@formadepago,@nocuenta);SELECT @idCliente= SCOPE_IDENTITY() from clientes "

        comando.Parameters.Add(New SqlParameter("@paterno", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@materno", Data.SqlDbType.VarChar)).Value = txtMaterno.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@nombre", Data.SqlDbType.VarChar)).Value = txtNombre.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@elnombre", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim + " " + txtMaterno.Text.ToUpper.Trim + " " + txtNombre.Text.ToUpper.Trim
        'comando.Parameters.Add(New SqlParameter("@domicilio", Data.SqlDbType.VarChar)).Value = txtDomicilio.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@telefono", Data.SqlDbType.VarChar)).Value = txtTelefono.Text.Trim
        comando.Parameters.Add(New SqlParameter("@celular", Data.SqlDbType.VarChar)).Value = txtCelular.Text.Trim
        comando.Parameters.Add(New SqlParameter("idcliente", Data.SqlDbType.Int)).Direction = Data.ParameterDirection.Output
        comando.Parameters.Add(New SqlParameter("@idDoctor", Data.SqlDbType.Int)).Value = cmbDoctores.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@email", Data.SqlDbType.VarChar)).Value = txtemail.Text.Trim
        comando.Parameters.Add(New SqlParameter("@iddatosfac", Data.SqlDbType.Int)).Value = cmbDatosfac.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@fechainicio", Data.SqlDbType.VarChar)).Value = txtFechaIni.Text
        comando.Parameters.Add(New SqlParameter("@activo", Data.SqlDbType.VarChar)).Value = "false"
        comando.Parameters.Add(New SqlParameter("@edad", Data.SqlDbType.VarChar)).Value = txtedad.Text
        comando.Parameters.Add(New SqlParameter("@sexo", Data.SqlDbType.Char)).Value = cmbsexo.SelectedValue
        comando.Parameters.Add(New SqlParameter("@idCenCostos", Data.SqlDbType.Int)).Value = cmbCostos.SelectedValue
        comando.Parameters.Add(New SqlParameter("@razonsocial", Data.SqlDbType.VarChar)).Value = txtRazonSocial.Text.Trim
        comando.Parameters.Add(New SqlParameter("@rfc", Data.SqlDbType.VarChar)).Value = txtrfc.Text.Trim
        comando.Parameters.Add(New SqlParameter("@calle", Data.SqlDbType.VarChar)).Value = txtcalle.Text.Trim
        comando.Parameters.Add(New SqlParameter("@NoExt", Data.SqlDbType.VarChar)).Value = txtnoext.Text.Trim
        comando.Parameters.Add(New SqlParameter("@colonia", Data.SqlDbType.VarChar)).Value = txtcolonia.Text.Trim
        comando.Parameters.Add(New SqlParameter("@cp", Data.SqlDbType.VarChar)).Value = txtcp.Text.Trim
        comando.Parameters.Add(New SqlParameter("@municipio", Data.SqlDbType.VarChar)).Value = txtmunicipio.Text.Trim
        comando.Parameters.Add(New SqlParameter("@ciudad", Data.SqlDbType.VarChar)).Value = txtciudad.Text.Trim
        comando.Parameters.Add(New SqlParameter("@edo", Data.SqlDbType.VarChar)).Value = txtestado.Text.Trim
        comando.Parameters.Add(New SqlParameter("@pais", Data.SqlDbType.VarChar)).Value = txtpais.Text.Trim
        comando.Parameters.Add(New SqlParameter("@formadepago", Data.SqlDbType.VarChar)).Value = cmbformapago.SelectedValue.ToString
        comando.Parameters.Add(New SqlParameter("@nocuenta", Data.SqlDbType.VarChar)).Value = txtcuenta.Text.Trim
        conexion.Open()
        comando.ExecuteScalar()
        Dim aux As String = comando.Parameters("idcliente").Value
        conexion.Close()
        conexion.Dispose()

        Messagebox1.ShowConfirmation("DESEAS ASIGNAR UNA TERAPIA???", aux, True, True)
    End Sub
    Function reemplazar(ByVal valor As String) As String
        Dim cad As String
        cad = valor
        cad = cad.Replace("-", " ")
        cad = cad.Replace(",", " ")
        cad = cad.Replace("'", " ")
        cad = cad.Replace("*", " ")
        Return cad.Trim
    End Function
    Protected Sub LinkButton2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkguardar.Click
        Dim xcuenta As Integer = 0
        Dim isalas As Integer = 0
        Do While xcuenta < 100000000
            xcuenta = xcuenta + 1
        Loop
        Dim i As Integer
        Dim cFechasValidas As String = ""
        Dim cValor As String = ""
        Dim pos As String
        Dim cuenta As Integer = 0
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta("fisiocareCP")
        Dim comando As SqlCommand = conexion.CreateCommand
        'Dim comando2 As SqlCommand = conexionLocal.CreateCommand

        Select Case lnkguardar.Text.Trim
            Case "GUARDAR"  ' se guardan los datos de un cliente
                Dim auxC As String = funciones.leerValor("select count(idcliente) from clientes " & _
                "where paterno LIKE '%" + txtPaterno.Text.Trim + "%' and materno LIKE '%" + txtmaterno.Text.Trim + "%'" & _
                " and nombre LIKE '%" + txtnombre.Text.Trim + "%'", "fisiocareCP")
                If auxC > 0 Then
                    Messagebox1.ShowConfirmation("Existe un paciente con el mismo nombre desea continuar??..", "C", True, True)
                Else
                    insertarcliente()
                End If
            Case "AGENDAR"  ' se agenda una cita
                If cmbDuracion.SelectedValue <= 0 Then
                    lblerror.Text = "No ha seleccionado la duracion de la terapia !!!"
                    Exit Sub
                End If
                If cmbDoctores.SelectedValue <= 0 Then
                    lblerror.Text = "No ha seleccionado un remitente que envio la cita !!!"
                    Exit Sub
                End If
                '******************************************************************
                ' se debe verificar en q posicion esta posicion = 1 y sacar cuales faltan 2,3,4,5
                'luego validar cada una de las posiciones buscando una libre y en esa se guarde
                '******************************************************************
                pos = lasfunciones.negocioCP("columna_esp").Trim
                If funciones.lookup(lblposicion.Text, pos, ",") > 0 Then
                    ' +++si esta en la agenda especial
                    cFechasValidas = validafechas_esp(lblhorario.Text, lblposicion.Text)
                    If cFechasValidas <> "" Then
                        For i = 1 To funciones.num_elementos(cFechasValidas, "|")
                            cValor = funciones.entry(i, cFechasValidas, "|").Trim
                            grabaEnAgenda(funciones.entry(2, cValor, ",").Trim, funciones.entry(1, cValor, ",").Trim)
                        Next
                        Context.Items.Add("fecha", lblfecha.Text.Trim)
                        Server.Transfer("defaultCP.aspx", False)
                    End If
                Else
                    cFechasValidas = validafechas(lblhorario.Text, lblposicion.Text)
                    If cFechasValidas <> "" Then
                        For i = 1 To funciones.num_elementos(cFechasValidas, "|")
                            cValor = funciones.entry(i, cFechasValidas, "|").Trim
                            grabaEnAgenda(funciones.entry(2, cValor, ",").Trim, funciones.entry(1, cValor, ",").Trim)
                        Next

                        ' si se agenda y esta DESACTIVO se actualiza y se activa
                        If chkActivo.Checked = False Then
                            funciones.grabaDatos("update clientes set activo='True',dtinicio='" + txtFechaIni.Text.Trim + "' where idCliente='" + cmbclientes.SelectedValue + "';", "fisiocareCP")
                        End If

                        funciones.grabaDatos("update clientes set idDoctor='" + cmbDoctores.SelectedValue + "', idCenCostos='" + cmbCostos.SelectedValue + "' where idCliente='" + cmbclientes.SelectedValue + "'", "fisiocareCP")
                        Context.Items.Add("fecha", lblfecha.Text.Trim)
                        Server.Transfer("defaultCP.aspx", False)
                    End If
                End If

            Case "GUARDAR CAMBIOS"  ' se actualizan los datos de la cita
                Dim xxx = cmbDatosfac.SelectedValue
                comando.CommandText = "update clientes set paterno= @paterno,materno=@materno,nombre=@nombre," & _
                "elnombre=@elnombre,telefono=@telefono,celular=@celular,sexo=@sexo,edad=@edad,idDoctor=@idDoctor, " & _
                "email=@email ,iddatosfac=@iddatosfac,activo=@activo,idCenCostos=@idCenCostos,razonsocial=@razonsocial," & _
                "rfc=@rfc,calle=@calle,noext=@NoExt,colonia=@Colonia,cp=@Cp,municipio=@Municipio,ciudad=@ciudad," & _
                "edo=@edo,pais=@pais,formadepago=@formadepago,nocuenta=@nocuenta where idCliente=@idCliente"
                comando.Parameters.Add(New SqlParameter("@paterno", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim
                comando.Parameters.Add(New SqlParameter("@materno", Data.SqlDbType.VarChar)).Value = txtMaterno.Text.ToUpper.Trim
                comando.Parameters.Add(New SqlParameter("@nombre", Data.SqlDbType.VarChar)).Value = txtNombre.Text.ToUpper.Trim
                comando.Parameters.Add(New SqlParameter("@elnombre", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim + " " + txtMaterno.Text.ToUpper.Trim + " " + txtNombre.Text.ToUpper.Trim
                'comando.Parameters.Add(New SqlParameter("@domicilio", Data.SqlDbType.VarChar)).Value = txtDomicilio.Text.ToUpper.Trim
                comando.Parameters.Add(New SqlParameter("@telefono", Data.SqlDbType.VarChar)).Value = txtTelefono.Text.Trim
                comando.Parameters.Add(New SqlParameter("@celular", Data.SqlDbType.VarChar)).Value = txtCelular.Text.Trim
                comando.Parameters.Add(New SqlParameter("@idCliente", Data.SqlDbType.Int)).Value = cmbclientes.SelectedValue.Trim
                comando.Parameters.Add(New SqlParameter("@idDoctor", Data.SqlDbType.Int)).Value = cmbDoctores.SelectedValue.Trim
                comando.Parameters.Add(New SqlParameter("@email", Data.SqlDbType.VarChar)).Value = txtemail.Text.Trim
                comando.Parameters.Add(New SqlParameter("@iddatosfac", Data.SqlDbType.Int)).Value = cmbDatosfac.SelectedValue.Trim
                'comando.Parameters.Add(New SqlParameter("@fechainicio", Data.SqlDbType.DateTime)).Value = txtFechaIni.Text
                comando.Parameters.Add(New SqlParameter("@activo", Data.SqlDbType.VarChar)).Value = chkActivo.Checked
                comando.Parameters.Add(New SqlParameter("@edad", Data.SqlDbType.VarChar)).Value = txtedad.Text
                comando.Parameters.Add(New SqlParameter("@sexo", Data.SqlDbType.Char)).Value = cmbsexo.SelectedValue
                comando.Parameters.Add(New SqlParameter("@idCenCostos", Data.SqlDbType.Int)).Value = cmbCostos.SelectedValue
                comando.Parameters.Add(New SqlParameter("@razonsocial", Data.SqlDbType.VarChar)).Value = txtRazonSocial.Text.Trim
                comando.Parameters.Add(New SqlParameter("@rfc", Data.SqlDbType.VarChar)).Value = txtrfc.Text.Trim
                comando.Parameters.Add(New SqlParameter("@calle", Data.SqlDbType.VarChar)).Value = txtcalle.Text.Trim
                comando.Parameters.Add(New SqlParameter("@NoExt", Data.SqlDbType.VarChar)).Value = txtnoext.Text.Trim
                comando.Parameters.Add(New SqlParameter("@colonia", Data.SqlDbType.VarChar)).Value = txtcolonia.Text.Trim
                comando.Parameters.Add(New SqlParameter("@cp", Data.SqlDbType.VarChar)).Value = txtcp.Text.Trim
                comando.Parameters.Add(New SqlParameter("@municipio", Data.SqlDbType.VarChar)).Value = txtmunicipio.Text.Trim
                comando.Parameters.Add(New SqlParameter("@ciudad", Data.SqlDbType.VarChar)).Value = txtciudad.Text.Trim
                comando.Parameters.Add(New SqlParameter("@edo", Data.SqlDbType.VarChar)).Value = txtestado.Text.Trim
                comando.Parameters.Add(New SqlParameter("@pais", Data.SqlDbType.VarChar)).Value = txtpais.Text.Trim
                comando.Parameters.Add(New SqlParameter("@formadepago", Data.SqlDbType.VarChar)).Value = cmbformapago.SelectedValue.ToString
                comando.Parameters.Add(New SqlParameter("@nocuenta", Data.SqlDbType.VarChar)).Value = txtcuenta.Text.Trim
                conexion.Open()
                comando.ExecuteNonQuery()
                conexion.Close()
                conexion.Dispose()

                Dim idtemp As String = cmbclientes.SelectedValue.Trim
                cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, elnombre from clientes order by elnombre", "fisiocareCP")
                cmbclientes.SelectedValue = idtemp
                txtNombre.ReadOnly = True
                txtPaterno.ReadOnly = True
                txtMaterno.ReadOnly = True
                'txtDomicilio.ReadOnly = True
                txtTelefono.ReadOnly = True
                txtCelular.ReadOnly = True
                txtemail.ReadOnly = True
                txtedad.ReadOnly = True
                cmbsexo.Enabled = False
                cmbDatosfac.Enabled = False
                txtRazonSocial.ReadOnly = True
                txtrfc.ReadOnly = True
                txtcalle.ReadOnly = True
                txtnoext.ReadOnly = True
                txtcolonia.ReadOnly = True
                txtcp.ReadOnly = True
                txtmunicipio.ReadOnly = True
                txtciudad.ReadOnly = True
                txtestado.ReadOnly = True
                txtpais.ReadOnly = True
                cmbformapago.Enabled = False
                txtcuenta.ReadOnly = True

                seleccionar.Visible = True
                lnkcancelar.Visible = False
                botones.Visible = True

                If lblfila.Text.Trim = "na" Then
                    lnkguardar.Visible = False
                    lnkguardar.Text = ""
                Else
                    lnkguardar.Text = "AGENDAR"
                End If
        End Select
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            datos.Visible = False
            busca.Visible = False
            Dim funciones As New miclases
            cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, elnombre from clientes order by elnombre", "fisiocareCP")
            cmbDoctores = funciones.llenacombos(cmbDoctores, "select ID,(nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre,paterno,materno", "fisiocareCP")
            cmbDatosfac = funciones.llenacombos(cmbDatosfac, "select Iddatosfac , nombre from catFacturas where activo='true' order by nombre", "fisiocareCP")
            cmbCostos = funciones.llenacombos(cmbCostos, "select idresponsable,descripcion from catResponsables where activo='true' AND idresponsable<>'111' order by descripcion", "fisiocareCP")
            cmbformapago = funciones.llenacombos(cmbformapago, "select idpago,descripcion from catPagos where activo = 'true' ", "fisiocareCP")
            llenaduracion()
            cmbDuracion.SelectedValue = 2
            lblfila.Text = Context.Items("duracion").ToString.Trim
            lblposicion.Text = Context.Items("posicion").ToString.Trim
            lblhorario.Text = Context.Items("idhorario").ToString.Trim
            lblfecha.Text = Context.Items("fecha").ToString.Trim
            Try
                Literal1.Text = Context.Items("ventaux").ToString.Trim
            Catch
            End Try
            If lblfila.Text.Trim = "na" Then
                lnkguardar.Visible = False
            Else
                cmbDatosfac.Enabled = True
                lblelHorario.Text = Context.Items("elhorario").ToString.Trim
                llenafechas()
            End If
            'llenaDatos()
        Else
            Literal1.Text = ""
        End If
    End Sub

    Protected Sub cmbclientes_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbclientes.SelectedIndexChanged
        llenaDatos()
    End Sub

    Protected Sub lnkcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkcancelar.Click
        Dim mivalor As String = lnkguardar.Text.Trim
        Select Case mivalor
            Case "GUARDAR"
                Context.Items.Add("duracion", lblfila.Text.Trim)
                Context.Items.Add("posicion", lblposicion.Text.Trim)
                Context.Items.Add("idhorario", lblhorario.Text.Trim)
                Context.Items.Add("fecha", lblfecha.Text.Trim)
                Context.Items.Add("elhorario", lblelHorario.Text.Trim)
                Server.Transfer("clientes.aspx")
            Case "GUARDAR CAMBIOS"
                txtNombre.ReadOnly = True
                txtPaterno.ReadOnly = True
                txtMaterno.ReadOnly = True
                'txtDomicilio.ReadOnly = True
                txtTelefono.ReadOnly = True
                txtCelular.ReadOnly = True
                txtemail.ReadOnly = True
                txtedad.ReadOnly = True
                cmbsexo.Enabled = False
                cmbDatosfac.Enabled = False
                txtRazonSocial.ReadOnly = True
                txtrfc.ReadOnly = True
                txtcalle.ReadOnly = True
                txtnoext.ReadOnly = True
                txtcolonia.ReadOnly = True
                txtcp.ReadOnly = True
                txtmunicipio.ReadOnly = True
                txtciudad.ReadOnly = True
                txtestado.ReadOnly = True
                txtpais.ReadOnly = True
                cmbformapago.Enabled = False
                txtcuenta.ReadOnly = True

                seleccionar.Visible = True
                lnkcancelar.Visible = False
                If lblfila.Text.Trim = "na" Then
                    lnkguardar.Visible = False
                    lnkguardar.Text = ""
                Else
                    lnkguardar.Text = "AGENDAR"
                End If
                llenaDatos()
        End Select
        botones.Visible = True
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        datos.Visible = True
        busca.Visible = False
        seleccionar.Visible = False
        lnkguardar.Visible = True
        lnkguardar.Text = "GUARDAR"

        txtNombre.ReadOnly = False
        txtPaterno.ReadOnly = False
        txtMaterno.ReadOnly = False
        'txtDomicilio.ReadOnly = False
        txtTelefono.ReadOnly = False
        txtCelular.ReadOnly = False
        txtedad.ReadOnly = False
        txtemail.ReadOnly = False
        cmbsexo.Enabled = True
        cmbDatosfac.Enabled = True
        txtrazonsocial.ReadOnly = False
        txtrfc.ReadOnly = False
        txtcalle.ReadOnly = False
        txtnoext.ReadOnly = False
        txtcolonia.ReadOnly = False
        txtcp.ReadOnly = False
        txtmunicipio.ReadOnly = False
        txtciudad.ReadOnly = False
        txtestado.ReadOnly = False
        txtpais.ReadOnly = False
        cmbformapago.Enabled = True
        txtcuenta.ReadOnly = False
        'chkActivo.Enabled = True
        'txtFechaIni.ReadOnly = False

        txtNombre.Text = ""
        txtPaterno.Text = ""
        txtMaterno.Text = ""
        'txtDomicilio.Text = ""
        txtTelefono.Text = ""
        txtCelular.Text = ""
        txtemail.Text = ""
        txtedad.Text = ""
        cmbsexo.SelectedValue = "0"
        txtFechaIni.Text = ""
        cmbCostos.SelectedValue = "0"
        cmbDoctores.SelectedValue = "0"
        cmbDatosfac.SelectedValue = "0"
        txtRazonSocial.Text = ""
        txtrfc.Text = ""
        txtciudad.Text = ""

        lnkcancelar.Visible = True
        botones.Visible = False
        'txtfechainicio.Text = Format("{0:dd/MM/yyyy}", Now.Day.ToString + "/" + Now.Month.ToString + "/" + Now.Year.ToString)
        'chkActivo.Checked = True
    End Sub

    Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
        seleccionar.Visible = False
        busca.Visible = True
        datos.Visible = False
    End Sub
    Protected Sub LinkButton2_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton2.Click
        Dim funciones As New miclases
        Dim elpaterno, elmaterno, elnombre As String
        If txtPaternoB.Text = "" Then
            elpaterno = ""
        Else
            elpaterno = Trim(txtPaternoB.Text.ToUpper)
        End If
        If txtMaternoB.Text = "" Then
            elmaterno = ""
        Else
            elmaterno = Trim(txtMaternoB.Text.ToUpper)
        End If
        If txtNombreB.Text = "" Then
            elnombre = ""
        Else
            elnombre = Trim(txtNombreB.Text.ToUpper)
        End If
        gridClientes = funciones.LLenaGrid("select idCliente,elnombre from clientes " & _
        "where paterno like '" + elpaterno + "%' and materno like '" + elmaterno + "%' and nombre " & _
                "like '" + elnombre + "%' order by elnombre", gridClientes, "fisiocareCP")
    End Sub

    Protected Sub gridClientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridClientes.ItemCommand
        seleccionar.Visible = True
        datos.Visible = True
        busca.Visible = False
        cmbclientes.SelectedValue = gridClientes.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
        llenaDatos()
    End Sub
    Protected Sub lnkModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModifica.Click
        lnkguardar.Visible = True
        lnkguardar.Text = "GUARDAR CAMBIOS"
        txtNombre.ReadOnly = False
        txtPaterno.ReadOnly = False
        txtMaterno.ReadOnly = False
        'txtDomicilio.ReadOnly = False
        txtTelefono.ReadOnly = False
        txtCelular.ReadOnly = False
        txtemail.ReadOnly = False
        txtedad.ReadOnly = False
        cmbsexo.Enabled = True
        cmbDatosfac.Enabled = True
        txtRazonSocial.ReadOnly = False
        txtrfc.ReadOnly = False
        txtcalle.ReadOnly = False
        txtnoext.ReadOnly = False
        txtcolonia.ReadOnly = False
        txtcp.ReadOnly = False
        txtmunicipio.ReadOnly = False
        txtciudad.ReadOnly = False
        txtestado.ReadOnly = False
        txtpais.ReadOnly = False
        cmbformapago.Enabled = True
        txtcuenta.ReadOnly = False

        busca.Visible = False
        datos.Visible = True
        seleccionar.Visible = False
        lnkcancelar.Visible = True
        botones.Visible = False
    End Sub

    Protected Sub Messagebox1_NoChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.NoChoosed
        If Key.Trim <> "C" Then
            Context.Items.Add("duracion", lblfila.Text.Trim)
            Context.Items.Add("posicion", lblposicion.Text.Trim)
            Context.Items.Add("idhorario", lblhorario.Text.Trim)
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Context.Items.Add("elhorario", lblelHorario.Text.Trim)
            Server.Transfer("clientes.aspx")
        End If
    End Sub

    Protected Sub Messagebox1_YesChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.YesChoosed
        If Key.Trim <> "C" Then
            Dim ventana As String = "<script language='javascript' type='text/javascript'>" & _
            "window.open('terapias.aspx?idcliente=" + Key + "','terapia','toolbar=no,width=550,height=600,scrollbars=yes')" & _
            "</script>"
            Context.Items.Add("ventaux", ventana)
            Context.Items.Add("duracion", lblfila.Text.Trim)
            Context.Items.Add("posicion", lblposicion.Text.Trim)
            Context.Items.Add("idhorario", lblhorario.Text.Trim)
            Context.Items.Add("elhorario", lblelHorario.Text.Trim)
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Server.Transfer("clientes.aspx")
        Else
            insertarcliente()
        End If
    End Sub

    Protected Sub cmbDatosfac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDatosfac.SelectedIndexChanged
        Dim funciones As New miclases
        If cmbDatosfac.SelectedValue <> "0" Then
            Dim valores(,) As String = funciones.leerValores("select nombre,rfc,calle,NoExt,Colonia," & _
            "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,email from catfacturas where iddatosfac='" + cmbDatosfac.SelectedValue.ToString + "'", 13, "fisiocareCP")
            txtRazonSocial.Text = valores(0, 0)
            txtrfc.Text = valores(1, 0)
            txtcalle.Text = valores(2, 0)
            txtnoext.Text = valores(3, 0)
            txtcolonia.Text = valores(4, 0)
            txtcp.Text = valores(5, 0)
            txtmunicipio.Text = valores(6, 0)
            txtciudad.Text = valores(7, 0)
            txtestado.Text = valores(8, 0)
            txtpais.Text = valores(9, 0)
            cmbformapago.SelectedValue = valores(10, 0)
            txtcuenta.Text = valores(11, 0)
        Else
            txtRazonSocial.Text = txtNombre.Text.Trim + " " + txtPaterno.Text.Trim + " " + txtMaterno.Text.Trim
            txtrfc.Text = "XAXX010101000"
            txtcalle.Text = ""
            txtnoext.Text = ""
            txtcolonia.Text = ""
            txtcp.Text = ""
            txtmunicipio.Text = "Mérida"
            txtciudad.Text = "Mérida"
            txtestado.Text = "Yucatán"
            txtpais.Text = "México"
            cmbformapago.SelectedValue = "0"
            txtcuenta.Text = ""
        End If

    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("defaultCP.aspx", False)
    End Sub
End Class
