Imports System.Data.SqlClient
Imports System
Imports System.Web
Imports System.Data
Partial Class clientes
    Inherits System.Web.UI.Page
    Dim SQL As String
    Dim fun As New miclases
    Dim funciones As New miclases

    Sub llenaduracion()
        Dim i As Integer = 0
        'Dim cad() As String = {"", "01 Turno (40 min)", "02 Turno (1 hora 20 min)", "03 Turno (2 horas)", "04 Turno (2 horas 40 min)", "05 Turno (3 horas 20 min)", "06 Turno (4 horas)", "07 Turno (4 horas 40 min)", "08 Turno (5 horas 20 min)", "09 Turno (6 horas)", "10 Turno (6 horas 40 min)"}
        'Dim cad() As String = {"", "01 Turno (40 min)", "02 Turno (1 hora 20 min)", "03 Turno (2 horas)", "04 Turno (2 horas 40 min)", "05 Turno (3 horas 20 min)", "06 Turno (4 horas)", "07 Turno (4 horas 40 min)", "08 Turno (5 horas 20 min)", "09 Turno (6 horas)", "10 Turno (6 horas 40 min)"}
        'para fisios y campestre
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
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        datos.Visible = False
        comando.CommandText = "select paterno,materno,nombre,edad,isnull(sexo,'0') as sexo,email,telefono,celular," & _
        "dtInicio,activo,idDoctor,idCencostos,iddatosfac,razonsocial,rfc,calle,noExt,colonia,cp,municipio," & _
        "ciudad,edo,pais,formadepago,nocuenta,iddoctore,domicilio, ciudadorigen, idocupacion, CONVERT(varchar(10),fechanacimiento,103) as fechanacimiento,isnull(codigoRegFiscal,'999') as codigoRegFiscal from clientes where idCliente='" + cmbclientes.SelectedValue.ToString.Trim + "'"
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

            Dim idDoctorE As String = leer.GetValue(25).ToString.Trim
            If idDoctorE = "" Then
                idDoctorE = "0"
            End If
            cmbDoctoresE.SelectedValue = idDoctorE

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
            'AB SAT 4.0
            cmbregimenfiscal.SelectedValue = leer.GetValue(30).ToString.Trim

            If Iddatosfac = "" Then
                Iddatosfac = "0"
            Else
                If txtRazonSocial.Text.Trim <> "" And txtrfc.Text.Trim <> "" And txtciudad.Text.Trim <> "" And txtemail.Text.Trim <> "" And Iddatosfac <> "0" Then
                    Dim valores(,) As String = funciones.leerValores("select nombre,rfc,calle,NoExt,Colonia," & _
                    "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,email,isnull(codigoRegFiscal,'999') as codigoRegFiscal from catfacturas where iddatosfac='" + Iddatosfac + "'", 14)
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
                    cmbregimenfiscal.SelectedValue = valores(13, 0)
                    'txtDomicilio.Text = resultados(1, 0)
                End If
            End If
            cmbDatosfac.SelectedValue = Iddatosfac


            datos.Visible = True
            If lblhorario.Text.Trim <> "na" Then
                masdatos.Visible = True
            End If
            cmbDatosfac.Enabled = False


            ' Programacion AB2019
            txtdireccion.Text = leer.GetValue(26).ToString.Trim
            txtorigen.Text = leer.GetValue(27).ToString.Trim
            txtfechanac.Text = leer.GetValue(29).ToString.Trim

            Dim idocupacion As String = leer.GetValue(28).ToString.Trim
            If idocupacion = "" Then
                idocupacion = "0"
            End If
            cmbocupacion.SelectedValue = idocupacion

        End If
        leer.Close()
        conexion.Close()
        conexion.Dispose()
    End Sub
    Sub llenafechas()
        Dim funciones As New miclases
        gridDias = funciones.creadataset("select semanas_agendar from semanasAgendar", gridDias)
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
    End Sub
   Sub grabaEnAgenda(ByVal posicion As String, ByVal lafecha As String)
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim axu = lblhorario.Text.Trim
        'If posicion > 11 Then
        '    posicion = 11
        'End If
        comando.CommandText = "insert into agenda(idcliente,idhorario,fecha,duracion,posicion,estado,idDoctor,dtinicio,cobservaciones,iddoctorc,idtconsmed,idlatencion,iddoctore,turno)" & _
                              "values(@idcliente,@idhorario,@fecha,@duracion,@posicion,'AGENDADO',@idDoctor,@fechaini,@observaciones,@iddoctorc,@idtconsmed,@idlatencion,@iddoctore,@turno)"
        comando.Parameters.Add(New SqlParameter("@idcliente", Data.SqlDbType.Int)).Value = cmbclientes.SelectedValue
        comando.Parameters.Add(New SqlParameter("@idhorario", Data.SqlDbType.SmallInt)).Value = lblhorario.Text.Trim
        comando.Parameters.Add(New SqlParameter("@fecha", Data.SqlDbType.VarChar)).Value = lafecha
        comando.Parameters.Add(New SqlParameter("@duracion", Data.SqlDbType.SmallInt)).Value = cmbDuracion.SelectedValue            ' lblfila.Text.Trim
        comando.Parameters.Add(New SqlParameter("@posicion", Data.SqlDbType.SmallInt)).Value = posicion
        comando.Parameters.Add(New SqlParameter("@idDoctor", Data.SqlDbType.Int)).Value = cmbDoctores.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@fechaini", Data.SqlDbType.DateTime)).Value = txtFechaIni.Text.Trim
        comando.Parameters.Add(New SqlParameter("@observaciones", Data.SqlDbType.VarChar)).Value = txtobservaciones.Text.Trim
        comando.Parameters.Add(New SqlParameter("@iddoctorc", Data.SqlDbType.Int)).Value = cmbDoctorC.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@idtconsmed", Data.SqlDbType.Int)).Value = cmbTipoCM.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@idlatencion", Data.SqlDbType.VarChar)).Value = cmblatencion.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@iddoctore", Data.SqlDbType.VarChar)).Value = cmbDoctoresE.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@turno", Data.SqlDbType.VarChar)).Value = lblturno.Text.Trim
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
                        "idhorario='" + lblhorario.Text.Trim + "' and fecha='" + auxfecha2 + "' and posicion='" + miposicion.ToString + "' and turno='" + lblturno.Text + "'") = 0 Then
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
        Dim cListasalas As String
        Dim fssalas As String
        '+++++++++++
        fssalas = funciones.negocio("numsalas")
        isalas = funciones.negocio("numsalas")
        cListasalas = funciones.negocio("columna_esp")
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
                    "idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + cmbclientes.SelectedValue + " and estado='AGENDADO' and turno='" + lblturno.Text + "') as horarios " & _
                    "where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
                    "idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')").ToString()
                    If auxPersonaAgendada <> "0" Then
                        PersonaAgendada = PersonaAgendada + auxfecha2.Trim + ","
                    End If
                    selvalida = selvalida + 1
                    '+++++++++++++++++
                    Dim chango As String = funciones.num_elementos(cSalas, ",")
                    For k = 1 To funciones.num_elementos(cSalas, ",")
                        '++++++++++++++++
                        Columna2 = funciones.entry(k, cSalas, ",")   'idhorario >= " + Horario.Trim + " and " & _''' obtengo la sala que este disponible siempre en la cadena esta la primera sala es donde se dio click
                        consulta = "select agenda.idhorario, agenda.posicion, agenda.duracion from agenda " & _
                        " where agenda.fecha='" + auxfecha2 + "' and " & _
                        " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO')  and agenda.turno='" + lblturno.Text + "' order by agenda.idhorario "
                        bandera = False
                        banderaX = False
                        cadregistros = funciones.llenalista(consulta) ''' idhorario-posicion-duracion horarios ocupado

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
                                k = 10

                            Else
                                If bandera = False Then
                                    cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                    k = 10
                                Else
                                    lblerror.Text = "Horario Ocupado!!!"
                                    Exit For
                                End If
                            End If
                            'MsgBox("3) *** es valido: " & bandera.ToString & "  cadOk = " & cadOk)
                        Else
                            If bandera = False Then ' si esta vacia la sala 
                                cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                k = 10
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
            lblerror.Text = "El Horario -- " + cmbDuracion.SelectedItem.Text + " --, Con Fecha " + diasNoDisp + " Esta Ocupado !!!"
        End If
        ' MsgBox("+++ fin Cadena Valida : " & ListaValida & "   +++  dias a agendar: " & selvalida)
        If ListaValida.Trim <> "" And selvalida <> funciones.num_elementos(ListaValida, "|") Then
            lblerror.Text = "(2)Existen fechas ya ocupadas y no se ha podido programar en la agenda (" + diasNoDisp + ") !!!"
            ListaValida = ""
        End If
        'Dim cListasalas As String
        'cListasalas = funciones.negocio("columna_esp")
        If PersonaAgendada >= cListasalas Then
            lblerror.Text = "(3)La persona ya fue agendada en el horario, un horario antes o un horario despues (" + PersonaAgendada + ")"
            ListaValida = ""
        End If
        Return ListaValida
        cmbDoctorC.Visible = False
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
        Dim auxHorario As Integer = Int(Horario) + Int(cmbDuracion.SelectedValue)
        Do While filas < cuentaF
            Do While columnas < cuentaC - 1
                auxfecha = CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Text
                auxfecha2 = String.Format("{0:dd/MM/yyyy}", auxfecha)
                If CType(gridDias.Items(filas).Cells(columnas).Controls(1), CheckBox).Checked = True Then
                    auxPersonaAgendada = funciones.leerValor("select distinct fecha from  ( select idCita, fecha, " & _
                   "idHorario + (duracion - 1) AS Horariosantes, idHorario from agenda where idcliente=" + cmbclientes.SelectedValue + " and estado='AGENDADO'  and turno='" + lblturno.Text + "') as horarios " & _
                   "where fecha='" + auxfecha2.Trim + "' and (Horariosantes='" + (Int(Horario) - 1).ToString + "' or " & _
                   "idhorario='" + Horario + "' or idhorario='" + (auxHorario).ToString + "' or idhorario='" + (auxHorario + 1).ToString + "')").ToString()
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
                        " agenda.posicion = " + Columna2.Trim + " and (agenda.estado='AGENDADO' OR agenda.estado='REALIZADO')  and agenda.turno='" + lblturno.Text + "' order by agenda.idhorario "
                        bandera = False
                        cadregistros = funciones.llenalista(consulta)
                        If cadregistros.Trim <> "" Then
                            cadenafin = funciones.completalista(cadregistros, "0")
                            For i = 0 To Int(cmbDuracion.SelectedValue) - 1
                                cad = Str(CInt(Horario) + i) + "-" + Columna2 '+ "-" + i.ToString.Trim
                                If funciones.lookup(cad.Trim, cadenafin, ",") > 0 Then
                                    bandera = True ' el horario esta ocupado
                                    'Novalido = True
                                    k = 11
                                End If
                            Next
                        Else
                            If bandera = False Then
                                ' MsgBox("CORRECTO !!! " & auxfecha2 & " posicion " & k.ToString)
                                cadOk = cadOk + auxfecha2 + " , " + Columna2.Trim + "|"
                                k = 11
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
        'Dim cListasalas As String
        'cListasalas = funciones.negocio("columna_esp")
        If PersonaAgendada > cListasalas Then
            lblerror.Text = "(3)La persona ya fue agendada en el horario, un horario antes o un horario despues (" + PersonaAgendada + ")"
            cadOk = ""
        End If
        Return cadOk

    End Function
    Sub insertarcliente()
        Dim conexion As SqlConnection = fun.conecta
        Dim comando As SqlCommand = conexion.CreateCommand

        '++ valida que no se pongan caracteres invalidos
        txtPaterno.Text = reemplazar(txtPaterno.Text)
        txtmaterno.Text = reemplazar(txtmaterno.Text)
        txtnombre.Text = reemplazar(txtnombre.Text)

        comando.CommandText = "insert into clientes(paterno,materno,nombre,elnombre,telefono,celular,adeudo," & _
        "idDoctor,email,iddatosfac,dtinicio,activo,edad,sexo,idCenCostos,razonsocial,rfc,calle,NoExt,Colonia," & _
        "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,iddoctore,domicilio, ciudadorigen, idocupacion, fechanacimiento,codigoRegFiscal)" & _
        "values(@paterno,@materno,@nombre,@elnombre,@telefono,@celular,'0',@idDoctor,@email,@iddatosfac," & _
        "@fechainicio,@activo,@edad,@sexo,@idCenCostos,@razonsocial,@rfc,@calle,@NoExt,@Colonia," & _
        "@Cp,@Municipio,@ciudad,@edo,@pais,@formadepago,@nocuenta,@iddoctore,@domicilio,@ciudadorigen,@idocupacion,@fechanacimiento,@regimenfiscal);SELECT @idCliente= SCOPE_IDENTITY() from clientes "

        comando.Parameters.Add(New SqlParameter("@paterno", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@materno", Data.SqlDbType.VarChar)).Value = txtmaterno.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@nombre", Data.SqlDbType.VarChar)).Value = txtnombre.Text.ToUpper.Trim
        comando.Parameters.Add(New SqlParameter("@elnombre", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim + " " + txtmaterno.Text.ToUpper.Trim + " " + txtnombre.Text.ToUpper.Trim
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
        comando.Parameters.Add(New SqlParameter("@iddoctore", Data.SqlDbType.VarChar)).Value = cmbDoctoresE.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@domicilio", Data.SqlDbType.VarChar)).Value = txtdireccion.Text.Trim
        comando.Parameters.Add(New SqlParameter("@ciudadorigen", Data.SqlDbType.VarChar)).Value = txtorigen.Text.Trim
        comando.Parameters.Add(New SqlParameter("@idocupacion", Data.SqlDbType.Int)).Value = cmbocupacion.SelectedValue.Trim
        comando.Parameters.Add(New SqlParameter("@fechanacimiento", Data.SqlDbType.DateTime)).Value = txtfechanac.Text.Trim
        comando.Parameters.Add(New SqlParameter("@regimenfiscal", Data.SqlDbType.VarChar)).Value = cmbregimenfiscal.SelectedValue.ToString

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
    Protected Sub lnkguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkguardar.Click
        Dim xcuenta As Integer = 0
        Dim isalas As Integer = 0
        Do While xcuenta < 15
            xcuenta = xcuenta + 1
        Loop
        Dim i As Integer
        Dim cFechasValidas As String = ""
        Dim cValor As String = ""
        Dim pos As String
        Dim cuenta As Integer = 0
        Dim funciones As New miclases
        Dim conexion As SqlConnection = funciones.conecta
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim conexionLocal As SqlConnection = funciones.conectaLocal
        'Dim comando2 As SqlCommand = conexionLocal.CreateCommand

        Select Case lnkguardar.Text.Trim
            Case "GUARDAR"  ' se guardan los datos de un cliente
                Dim auxC As String = funciones.leerValor("select count(idcliente) from clientes " & _
                "where paterno LIKE '%" + txtPaterno.Text.Trim + "%' and materno LIKE '%" + txtmaterno.Text.Trim + "%'" & _
                " and nombre LIKE '%" + txtnombre.Text.Trim + "%'")
                If auxC > 0 Then
                    Messagebox1.ShowMessage("Existe un paciente con el mismo nombre!...")
                    'desea continuar??..", "C", True, True)
                Else
                    If cmbDoctoresE.SelectedValue <= 0 Then
                        Messagebox1.ShowMessage("No ha seleccionado un Doctor que envio la cita!...")
                        'lblerror.Text = "No ha seleccionado un Doctor que envio la cita !!!"
                        Exit Sub
                    End If

                    If txtfechanac.Text = "" Then
                        Messagebox1.ShowMessage("Fecha de Nacimiento Incorrecto!...")
                        Exit Sub
                    End If


                    insertarcliente()
                End If
            Case "AGENDAR"  ' se agenda una cita

                'comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and idCliente='" + cmbclientes.SelectedValue + "'"
                'conexion.Open()
                'leer = comando.ExecuteReader
                'If leer.Read Then
                '    lblclieagendado.Text = leer.GetValue(0).ToString.Trim
                'End If
                'conexion.Close()
                'Dim cListasalase As String
                'Dim cListasalasn As String
                'cListasalase = funciones.negocio("columna_esp")
                'cListasalasn = funciones.negocio("numsalas")



                'If CInt(lblclieagendado.Text) > 0 And cListasalase > 0 Then
                '    lblerror.Text = "El Paciente esta AGENDADO favor de Verificar!!!"
                '    Exit Sub
                'End If

                If cmbDuracion.SelectedValue <= 0 Then
                    lblerror.Text = "No ha seleccionado la duracion de la terapia !!!"
                    Exit Sub
                End If
                If cmbDoctores.SelectedValue <= 0 Then
                    lblerror.Text = "No ha seleccionado un remitente que envio la cita !!!"
                    Exit Sub
                End If

                If cmbDoctoresE.SelectedValue <= 0 Then
                    lblerror.Text = "No ha seleccionado un Doctor que envio la cita !!!"
                    Exit Sub
                End If

                If cmblatencion.SelectedValue <= "0" Then
                    lblerror.Text = "No ha seleccionado el Lugar de Atencion !!!"
                    Exit Sub
                End If

                comando.CommandText = "select existencias from catlatencion where idlatencion='" + cmblatencion.SelectedValue + "'"
                conexion.Open()
                leer = comando.ExecuteReader
                If leer.Read Then
                    lblexistencias.Text = leer.GetValue(0).ToString.Trim
                End If
                conexion.Close()

                comando.CommandText = "select count(idcliente) from agenda where fecha='" + lblfecha.Text.Trim + "' and estado='AGENDADO' and idlatencion='" + cmblatencion.SelectedValue + "' and idhorario='" + lblhorario.Text.Trim + "' and turno='" + lblturno.Text + "'"
                conexion.Open()
                leer = comando.ExecuteReader
                If leer.Read Then
                    lbCA.Text = leer.GetValue(0).ToString.Trim
                End If
                conexion.Close()

                If CInt(lblexistencias.Text) <= CInt(lbCA.Text) Then
                    lblerror.Text = "Lugar de Atencion '" + cmblatencion.SelectedValue + "' esta AGOTADO !!!"
                    Exit Sub
                End If

                '******************************************************************
                ' se debe verificar en q posicion esta posicion = 1 y sacar cuales faltan 2,3,4,5
                'luego validar cada una de las posiciones buscando una libre y en esa se guarde
                '******************************************************************
                pos = funciones.negocio("columna_esp").Trim
                If funciones.lookup(lblposicion.Text, pos, ",") > 0 Then
                    ' +++si esta en la agenda especial
                    cFechasValidas = validafechas_esp(lblhorario.Text, lblposicion.Text)
                    If cFechasValidas <> "" Then
                        For i = 1 To funciones.num_elementos(cFechasValidas, "|")
                            cValor = funciones.entry(i, cFechasValidas, "|").Trim
                            grabaEnAgenda(funciones.entry(2, cValor, ",").Trim, funciones.entry(1, cValor, ",").Trim)
                        Next
                        Context.Items.Add("fecha", lblfecha.Text.Trim)
                        Server.Transfer("default.aspx", False)
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
                            funciones.grabaDatos("update clientes set activo='True',dtinicio='" + txtFechaIni.Text.Trim + "' where idCliente='" + cmbclientes.SelectedValue + "';")
                        End If

                        funciones.grabaDatos("update clientes set idDoctor='" + cmbDoctores.SelectedValue + "',iddoctore='" + cmbDoctoresE.SelectedValue + "', idCenCostos='" + cmbCostos.SelectedValue + "' where idCliente='" + cmbclientes.SelectedValue + "'")
                        Context.Items.Add("fecha", lblfecha.Text.Trim)
                        Server.Transfer("default.aspx", False)
                    End If
                End If

            Case "GUARDAR CAMBIOS"  ' se actualizan los datos de la cita
                Dim xxx = cmbDatosfac.SelectedValue
                If txtfechanac.Text = "" Then

                    comando.CommandText = "update clientes set paterno= @paterno,materno=@materno,nombre=@nombre," & _
                "elnombre=@elnombre,telefono=@telefono,celular=@celular,sexo=@sexo,edad=@edad,idDoctor=@idDoctor, " & _
                "email=@email ,iddatosfac=@iddatosfac,activo=@activo,idCenCostos=@idCenCostos,razonsocial=@razonsocial," & _
                "rfc=@rfc,calle=@calle,noext=@NoExt,colonia=@Colonia,cp=@Cp,municipio=@Municipio,ciudad=@ciudad," & _
                "edo=@edo,pais=@pais,formadepago=@formadepago,nocuenta=@nocuenta,iddoctore=@iddoctore,domicilio=@domicilio,ciudadorigen=@ciudadorigen,idocupacion=@idocupacion,codigoRegFiscal=@regimenfiscal where idCliente=@idCliente"
                    comando.Parameters.Add(New SqlParameter("@paterno", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@materno", Data.SqlDbType.VarChar)).Value = txtmaterno.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@nombre", Data.SqlDbType.VarChar)).Value = txtnombre.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@elnombre", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim + " " + txtmaterno.Text.ToUpper.Trim + " " + txtnombre.Text.ToUpper.Trim
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
                    comando.Parameters.Add(New SqlParameter("@iddoctore", Data.SqlDbType.VarChar)).Value = cmbDoctoresE.SelectedValue.Trim
                    comando.Parameters.Add(New SqlParameter("@domicilio", Data.SqlDbType.VarChar)).Value = txtdireccion.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@ciudadorigen", Data.SqlDbType.VarChar)).Value = txtorigen.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@idocupacion", Data.SqlDbType.Int)).Value = cmbocupacion.SelectedValue.Trim
                    'comando.Parameters.Add(New SqlParameter("@fechanacimiento", Data.SqlDbType.DateTime)).Value = txtfechanac.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@regimenfiscal", Data.SqlDbType.VarChar)).Value = cmbregimenfiscal.SelectedValue.ToString
                    conexion.Open()
                    comando.ExecuteNonQuery()
                    conexion.Close()
                    conexion.Dispose()

                Else
                    comando.CommandText = "update clientes set paterno= @paterno,materno=@materno,nombre=@nombre," & _
                "elnombre=@elnombre,telefono=@telefono,celular=@celular,sexo=@sexo,edad=@edad,idDoctor=@idDoctor, " & _
                "email=@email ,iddatosfac=@iddatosfac,activo=@activo,idCenCostos=@idCenCostos,razonsocial=@razonsocial," & _
                "rfc=@rfc,calle=@calle,noext=@NoExt,colonia=@Colonia,cp=@Cp,municipio=@Municipio,ciudad=@ciudad," & _
                "edo=@edo,pais=@pais,formadepago=@formadepago,nocuenta=@nocuenta,iddoctore=@iddoctore,domicilio=@domicilio,ciudadorigen=@ciudadorigen,idocupacion=@idocupacion,fechanacimiento=@fechanacimiento,codigoRegFiscal=@regimenfiscal where idCliente=@idCliente"
                    comando.Parameters.Add(New SqlParameter("@paterno", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@materno", Data.SqlDbType.VarChar)).Value = txtmaterno.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@nombre", Data.SqlDbType.VarChar)).Value = txtnombre.Text.ToUpper.Trim
                    comando.Parameters.Add(New SqlParameter("@elnombre", Data.SqlDbType.VarChar)).Value = txtPaterno.Text.ToUpper.Trim + " " + txtmaterno.Text.ToUpper.Trim + " " + txtnombre.Text.ToUpper.Trim
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
                    comando.Parameters.Add(New SqlParameter("@iddoctore", Data.SqlDbType.VarChar)).Value = cmbDoctoresE.SelectedValue.Trim
                    comando.Parameters.Add(New SqlParameter("@domicilio", Data.SqlDbType.VarChar)).Value = txtdireccion.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@ciudadorigen", Data.SqlDbType.VarChar)).Value = txtorigen.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@idocupacion", Data.SqlDbType.Int)).Value = cmbocupacion.SelectedValue.Trim
                    comando.Parameters.Add(New SqlParameter("@fechanacimiento", Data.SqlDbType.DateTime)).Value = txtfechanac.Text.Trim
                    comando.Parameters.Add(New SqlParameter("@regimenfiscal", Data.SqlDbType.VarChar)).Value = cmbregimenfiscal.SelectedValue.ToString
                    conexion.Open()
                    comando.ExecuteNonQuery()
                    conexion.Close()
                    conexion.Dispose()

                End If
                

                Dim idtemp As String = cmbclientes.SelectedValue.Trim
                cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, elnombre from clientes order by elnombre")
                cmbclientes.SelectedValue = idtemp
                txtnombre.ReadOnly = True
                txtPaterno.ReadOnly = True
                txtmaterno.ReadOnly = True
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
                cmbregimenfiscal.Enabled = False
                txtcuenta.ReadOnly = True

                seleccionar.Visible = True
                buscacliente.Visible = True
                lnkcancelar.Visible = False
                botones.Visible = True

                If lblfila.Text.Trim = "na" Then
                    lnkguardar.Visible = False
                    lnkguardar.Text = ""
                Else
                    lnkguardar.Text = "AGENDAR"
                End If

                txtdireccion.ReadOnly = True
                txtorigen.ReadOnly = True
                txtfechanac.ReadOnly = True
                cmbocupacion.Enabled = False
        End Select
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        If Not (Page.IsPostBack) Then
            datos.Visible = False
            busca.Visible = False
            Dim funciones As New miclases
            cmbclientes = funciones.llenacombos(cmbclientes, "select idcliente, elnombre from clientes order by elnombre")
            cmbDoctores = funciones.llenacombos(cmbDoctores, "select ID,(nombre+' '+paterno+' '+materno) as elnombre from catDoctores order by nombre,paterno,materno")
            cmbDoctoresE = funciones.llenacombos(cmbDoctoresE, "select iddoctore,(nombre+' '+paterno+' '+materno) as elnombre from catDoctoresEnvio order by nombre,paterno,materno")
            cmbDoctorC = funciones.llenacombos(cmbDoctorC, "select iddoctorc,(nombre+' '+apaterno+' '+amaterno) as elnombre from catDoctorC order by nombre,apaterno,amaterno")
            cmbTipoCM = funciones.llenacombos(cmbTipoCM, "select idtconsmed,tdescripcion from catTipoCM order by tdescripcion")
            cmblatencion = funciones.llenacombos(cmblatencion, "select idlatencion,atdescripcion from catLAtencion order by idla")
            cmbDatosfac = funciones.llenacombos(cmbDatosfac, "select Iddatosfac , nombre from catFacturas where activo='true' order by nombre")
            cmbCostos = funciones.llenacombos(cmbCostos, "select idresponsable,descripcion from catResponsables where activo='true' AND idresponsable<>'111' order by descripcion")
            cmbformapago = funciones.llenacombos(cmbformapago, "select idpago,descripcion from catPagos where activo = 'true' ")
            cmbocupacion = funciones.llenacombos(cmbocupacion, "select IdOcupacion, descripcion from CatOcupaciones where Status = 'true' ")
            'AB SAT 4.0
            cmbregimenfiscal = funciones.llenacombos(cmbregimenfiscal, "select codigoRegFiscal,(CONVERT(varchar(10), codigoRegFiscal)+'-'+descripcionRegFiscal) as RegimenFiscal from CatRegimenFiscal where status = '1'")
            llenaduracion()

            'cmbDuracion.SelectedValue = 2
            lblfila.Text = Context.Items("duracion").ToString.Trim
            lblposicion.Text = Context.Items("posicion").ToString.Trim
            lblhorario.Text = Context.Items("idhorario").ToString.Trim
            lblfecha.Text = Context.Items("fecha").ToString.Trim
            lblturno.Text = Context.Items("turno").ToString.Trim
            cmblatencion.SelectedValue = "CA"
            If lblhorario.Text = "14" And lblturno.Text = "M" Then
                cmbDuracion.SelectedValue = 1
                cmbDuracion.Enabled = False
            Else

                If lblhorario.Text = "12" And lblturno.Text = "V" Then
                    cmbDuracion.SelectedValue = 1
                    cmbDuracion.Enabled = False
                Else


                    cmbDuracion.SelectedValue = 1
                    cmbDuracion.Enabled = True
                End If

                'cmbDuracion.SelectedValue = 1
                'cmbDuracion.Enabled = True
            End If


            
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
                'Dim conexion As SqlConnection = funciones.conecta
                'Dim comando As SqlCommand = conexion.CreateCommand
                'Dim leer As SqlDataReader
                'comando.CommandText = "select turno from horarios2017 where idhorario='" + lblhorario.Text + "'"
                'conexion.Open()
                'leer = comando.ExecuteReader
                'If leer.Read Then
                '    lblturno.Text = leer.GetValue(0).ToString.Trim
                'End If
                'conexion.Close()
            End If
            'llenaDatos()
        Else
            Literal1.Text = ""
        End If


        'If lblposicion.Text < 11 Then

        cmbDoctorC.Visible = False
        lbldocc.Visible = False
        cmbTipoCM.Visible = False
        lbltipocm.Visible = False
        'End If

    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False

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
                Context.Items.Add("turno", lblturno.Text.Trim)
                Server.Transfer("clientes.aspx")
            Case "GUARDAR CAMBIOS"
                txtnombre.ReadOnly = True
                txtPaterno.ReadOnly = True
                txtmaterno.ReadOnly = True
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
                cmbregimenfiscal.Enabled = False
                txtcuenta.ReadOnly = True

                seleccionar.Visible = True
                buscacliente.Visible = True
                lnkcancelar.Visible = False
                If lblfila.Text.Trim = "na" Then
                    lnkguardar.Visible = False
                    lnkguardar.Text = ""
                Else
                    lnkguardar.Text = "AGENDAR"
                End If
                txtdireccion.ReadOnly = True
                txtorigen.ReadOnly = True
                txtfechanac.ReadOnly = True
                cmbocupacion.Enabled = False

                llenaDatos()
        End Select
        botones.Visible = True
    End Sub
    Protected Sub LinkButton1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton1.Click
        datos.Visible = True
        busca.Visible = False
        seleccionar.Visible = False
        buscacliente.Visible = False
        lnkguardar.Visible = True
        lnkguardar.Text = "GUARDAR"

        txtnombre.ReadOnly = False
        txtPaterno.ReadOnly = False
        txtmaterno.ReadOnly = False
        'txtDomicilio.ReadOnly = False
        txtTelefono.ReadOnly = False
        txtCelular.ReadOnly = False
        txtedad.ReadOnly = False
        txtemail.ReadOnly = False
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
        cmbregimenfiscal.Enabled = True
        txtcuenta.ReadOnly = False
        'chkActivo.Enabled = True
        'txtFechaIni.ReadOnly = False
        txtdireccion.ReadOnly = False
        txtorigen.ReadOnly = False
        txtfechanac.ReadOnly = False
        cmbocupacion.Enabled = True


        txtnombre.Text = ""
        txtPaterno.Text = ""
        txtmaterno.Text = ""
        'txtDomicilio.Text = ""
        txtTelefono.Text = ""
        txtCelular.Text = ""
        txtemail.Text = ""
        txtedad.Text = ""
        cmbsexo.SelectedValue = "0"
        txtFechaIni.Text = ""
        cmbCostos.SelectedValue = "0"
        cmbDoctores.SelectedValue = "0"
        cmbDoctoresE.SelectedValue = "0"
        cmbDatosfac.SelectedValue = "0"
        txtRazonSocial.Text = ""
        txtrfc.Text = ""
        txtciudad.Text = ""

        txtdireccion.Text = ""
        txtorigen.Text = "MÉRIDA"
        txtfechanac.Text = ""
        cmbocupacion.SelectedValue = "0"

        lnkcancelar.Visible = True
        botones.Visible = False
        
        'txtfechainicio.Text = Format("{0:dd/MM/yyyy}", Now.Day.ToString + "/" + Now.Month.ToString + "/" + Now.Year.ToString)
        'chkActivo.Checked = True
    End Sub

    'Protected Sub LinkButton4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton4.Click
    '    seleccionar.Visible = False
    '    buscacliente.Visible = False
    '    busca.Visible = True
    '    datos.Visible = False
    'End Sub
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
        If txtnombreB.Text = "" Then
            elnombre = ""
        Else
            elnombre = Trim(txtnombreB.Text.ToUpper)
        End If
        gridClientes = funciones.creadataset("select idCliente,elnombre from clientes " & _
        "where paterno like '" + elpaterno + "%' and materno like '" + elmaterno + "%' and nombre " & _
                "like '" + elnombre + "%' order by elnombre", gridClientes)
    End Sub

Protected Sub BtnBuscarPaciente_Click(sender As Object, e As EventArgs)

        Dim strSQL As String
        Dim resultadoBusqueda As Integer
        Dim funcion As New FuncionesGenerales
        Dim claseDatos As New ClaseDatos
        If TxbMaterno.Text = "" And TxbPaterno.Text = "" And TxbNombre.Text = "" Then
            strSQL = "SELECT idCliente,elnombre FROM [" & claseDatos.BaseDatos & "].[dbo].[clientes] " & _
             "where elnombre <> '' order by elnombre asc "
            resultadoBusqueda = 0
            resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
            If resultadoBusqueda = -1 Then
                LblMensajeCritico.Text = claseDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
                Exit Sub
            End If

        Else
            strSQL = "SELECT idCliente,elnombre FROM [" & claseDatos.BaseDatos & "].[dbo].[clientes] " & _
            "where paterno like '%" & TxbPaterno.Text.Trim & "%' and nombre like '%" & TxbNombre.Text.Trim & "%' and materno like '%" & TxbMaterno.Text.Trim & "%'"
            resultadoBusqueda = 0
            resultadoBusqueda = funcion.llenalistbox(strSQL, LstBoxPacientes)
            If resultadoBusqueda = -1 Then
                LblMensajeCritico.Text = claseDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
                Exit Sub
            End If
            If resultadoBusqueda = 1 Then
                LblMostarDecision.Text = "No existe el paciente. ¿Desea agregarlo?"
                PanelDesicion.Visible = True
                PanelDesicion.Focus()
                'HdPregunta.Value = "2"
                Exit Sub
            End If
        End If
        
       
        LstBoxPacientes.Focus()
        LstBoxPacientes.SelectedIndex = 0
    End Sub
    Protected Sub BtnVer_Click(sender As Object, e As EventArgs)
        If LstBoxPacientes.SelectedValue = "" Then
            Exit Sub
        End If
        seleccionar.Visible = True

        buscacliente.Visible = True
        datos.Visible = True
        busca.Visible = False
        cmbclientes.SelectedValue = LstBoxPacientes.SelectedValue.ToString
        lblid.Text = LstBoxPacientes.SelectedValue.ToString
        llenaDatos()

        TxbMaterno.Text = ""
        TxbNombre.Text = ""
        TxbPaterno.Text = ""

    End Sub

    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        datos.Visible = True
        busca.Visible = False
        seleccionar.Visible = False
        buscacliente.Visible = False
        lnkguardar.Visible = True
        lnkguardar.Text = "GUARDAR"

        txtnombre.ReadOnly = False
        txtPaterno.ReadOnly = False
        txtmaterno.ReadOnly = False
        'txtDomicilio.ReadOnly = False
        txtTelefono.ReadOnly = False
        txtCelular.ReadOnly = False
        txtedad.ReadOnly = False
        txtemail.ReadOnly = False
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
        cmbregimenfiscal.Enabled = True
        txtcuenta.ReadOnly = False
        'chkActivo.Enabled = True
        'txtFechaIni.ReadOnly = False
        txtdireccion.ReadOnly = False
        txtorigen.ReadOnly = False
        txtfechanac.ReadOnly = False
        cmbocupacion.Enabled = True


        txtnombre.Text = TxbNombre.Text
        txtPaterno.Text = TxbPaterno.Text
        txtmaterno.Text = TxbMaterno.Text
        'txtDomicilio.Text = ""
        txtTelefono.Text = ""
        txtCelular.Text = ""
        txtemail.Text = ""
        txtedad.Text = ""
        cmbsexo.SelectedValue = "0"
        txtFechaIni.Text = ""
        cmbCostos.SelectedValue = "0"
        cmbDoctores.SelectedValue = "0"
        cmbDoctoresE.SelectedValue = "0"
        cmbDatosfac.SelectedValue = "0"
        txtRazonSocial.Text = ""
        txtrfc.Text = ""
        txtciudad.Text = ""

        txtdireccion.Text = ""
        txtorigen.Text = "MÉRIDA"
        txtfechanac.Text = ""
        cmbocupacion.SelectedValue = "0"

        lnkcancelar.Visible = True
        botones.Visible = False
        'Select Case HdPregunta.Value
        '    Case "0"
        '        Response.Redirect("BuscarCitaPaciente.aspx")
        '    Case "1"
        '        Session("modalidad") = "mover"
        '        moverAgregar()
        '    Case "2"
        '        Session.Add("paternoNuevo", TxbPaterno.Text)
        '        Session.Add("maternoNuevo", TxbMaterno.Text)
        '        Session.Add("nombresNuevo", TxbNombre.Text)
        '        If Session("permisoConsFisico") Then
        '            Session.Add("consultorioFis", DdConsultorios.SelectedItem.ToString)
        '            Session.Add("idConsultorioFis", DdConsultorios.SelectedValue.ToString)
        '        End If
        '        Response.Redirect("DatosPaciente.aspx")
        '    Case "3"

        'End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        ''''''verificar validacion de poder agendar mas de una vez al paciente en fechas diferentes-AB17122018
        'Select Case HdPregunta.Value
        '    Case "0"
        '        moverAgregar()
        '    Case "1"
        '        moverAgregar()
        '    Case "2"

        '        Exit Sub

        '    Case "3"

        'End Select

    End Sub

    'Protected Sub buscarcliente_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles buscarcliente.Click
    '    Dim funciones As New miclases
    '    Dim celpaterno, celmaterno, celnombre As String
    '    If ctxtPaternoB.Text = "" Then
    '        celpaterno = ""
    '    Else
    '        celpaterno = Trim(ctxtPaternoB.Text.ToUpper)
    '    End If
    '    If ctxtMaternoB.Text = "" Then
    '        celmaterno = ""
    '    Else
    '        celmaterno = Trim(ctxtMaternoB.Text.ToUpper)
    '    End If
    '    If ctxtnombreB.Text = "" Then
    '        celnombre = ""
    '    Else
    '        celnombre = Trim(ctxtnombreB.Text.ToUpper)
    '    End If
    '    cgridClientes = funciones.creadataset("select idCliente,elnombre from clientes " & _
    '    "where paterno like '" + celpaterno + "%' and materno like '" + celmaterno + "%' and nombre " & _
    '            "like '" + celnombre + "%' order by elnombre", cgridClientes)
    'End Sub

    'Protected Sub gridClientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridClientes.ItemCommand

    '    seleccionar.Visible = False
    '    buscacliente.Visible = False
    '    datos.Visible = True
    '    busca.Visible = False
    '    cmbclientes.SelectedValue = gridClientes.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
    '    llenaDatos()
    'End Sub
    'Protected Sub cgridClientes_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles cgridClientes.ItemCommand
    '    seleccionar.Visible = True

    '    buscacliente.Visible = True
    '    datos.Visible = True
    '    busca.Visible = False
    '    cmbclientes.SelectedValue = cgridClientes.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
    '    lblid.Text = cgridClientes.DataKeys.Item(e.Item.ItemIndex).ToString.Trim
    '    llenaDatos()
    '    cgridClientes.DataBind()
    '    ctxtPaternoB.Text = ""
    '    ctxtMaternoB.Text = ""
    '    ctxtnombreB.Text = ""
    '    'cgridClientes.Refresh()
    'End Sub
    Protected Sub lnkModifica_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkModifica.Click
        lnkguardar.Visible = True
        lnkguardar.Text = "GUARDAR CAMBIOS"
        txtnombre.ReadOnly = False
        txtPaterno.ReadOnly = False
        txtmaterno.ReadOnly = False
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
        cmbregimenfiscal.Enabled = True
        txtcuenta.ReadOnly = False

        busca.Visible = False
        datos.Visible = True
        seleccionar.Visible = True
        lnkcancelar.Visible = True
        botones.Visible = False

        txtdireccion.ReadOnly = False
        txtorigen.ReadOnly = False
        txtfechanac.ReadOnly = False
        cmbocupacion.Enabled = True
    End Sub

    Protected Sub Messagebox1_NoChoosed(ByVal sender As Object, ByVal Key As String) Handles Messagebox1.NoChoosed
        If Key.Trim <> "C" Then
            Context.Items.Add("duracion", lblfila.Text.Trim)
            Context.Items.Add("posicion", lblposicion.Text.Trim)
            Context.Items.Add("idhorario", lblhorario.Text.Trim)
            Context.Items.Add("fecha", lblfecha.Text.Trim)
            Context.Items.Add("elhorario", lblelHorario.Text.Trim)
            Context.Items.Add("turno", lblturno.Text.Trim)
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
            Context.Items.Add("turno", lblturno.Text.Trim)
            Server.Transfer("clientes.aspx")
        Else
            insertarcliente()
        End If
    End Sub

    Protected Sub cmbDatosfac_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbDatosfac.SelectedIndexChanged
        Dim funciones As New miclases
        If cmbDatosfac.SelectedValue <> "0" Then
            Dim valores(,) As String = funciones.leerValores("select nombre,rfc,calle,NoExt,Colonia," & _
            "Cp,Municipio,ciudad,edo,pais,formadepago,nocuenta,email,isnull(codigoRegFiscal,'999') as codigoRegFiscal from catfacturas where iddatosfac='" + cmbDatosfac.SelectedValue.ToString + "'", 14)
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
            cmbregimenfiscal.SelectedValue = valores(13, 0)
        Else
            txtRazonSocial.Text = txtnombre.Text.Trim + " " + txtPaterno.Text.Trim + " " + txtmaterno.Text.Trim
            txtrfc.Text = "XAXX010101000"
            txtcalle.Text = ""
            txtnoext.Text = ""
            txtcolonia.Text = ""
            txtcp.Text = "97120"
            txtmunicipio.Text = "Mérida"
            txtciudad.Text = "Mérida"
            txtestado.Text = "Yucatán"
            txtpais.Text = "México"
            cmbformapago.SelectedValue = "0"
            txtcuenta.Text = ""
            cmbregimenfiscal.SelectedValue = "616"
        End If

    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub




    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
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
