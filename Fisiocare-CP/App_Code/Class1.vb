Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class miclases
    Public Function negocio(ByVal cad As String) As String
        Dim valor As String
        valor = ""
        If cad = "columna_esp" Then
            valor = "9"
        End If
        If cad = "numsalas" Then
            valor = "8"
        End If
        If cad = "sucursal" Then
            valor = "Fisiocare y Medicina del Deporte (Suc. Campestre)"
        End If
        Return valor
    End Function

    Public Function cadenalocal() As String
        ' ++ Dim cadenal As String = "'server=localhost\sqlexpress;database=fisiocarenew;uid=sa;pwd=12345678'"
        'Dim cadenal As String = "'server=192.168.0.8\sqlexpress;database=fisiocareCP;uid=sa;pwd=ZxCv123'"

        'Return cadenal
    End Function
    Public Function cadenaremoto() As String
        'Dim cadenaR As String = "'server=192.168.0.8\SQLEXPRESS;database=111;uid=sa;pwd=ZxCv123'"
        'Return cadenaR
    End Function

    Public Function baselocal() As String
        'Dim cadenaLocal As String = "'server=localhost\SQLEXPRESS;database=fisiolocal;uid=sa;pwd=12345678'"
        'Return cadenaLocal
    End Function
    Public Function conectaLocal() As SqlConnection
        Dim miconexion As SqlConnection
        'miconexion = New SqlConnection("Data Source=localhost\SQLEXPRESS;Initial Catalog=fisiolocal;" & _
        '"User ID=sa; Password=12345678")

        'Return miconexion
    End Function

    Public Function conecta() As SqlConnection
        Dim miconexion As SqlConnection
      

        miconexion = New SqlConnection("Data Source=198.49.76.114\MSSQLSERVER2012;Initial Catalog=fisiocareCP;" & _
        "User ID=FisiocareApp; Password=C4r3*Ap+$05")

        Return miconexion
    End Function
    Public Function conectaR() As SqlConnection
        Dim miconexion As SqlConnection
       
    End Function
    Public Function creatabla(ByVal numcolumnas As Integer)
        Dim tabla As New DataTable
        Dim i As Integer = 0
        Dim nomcolumna As String = "columna"
        Do While i < numcolumnas
            Dim columna As New DataColumn(nomcolumna)
            tabla.Columns.Add(columna)
            i = i + 1
            nomcolumna = "columna" + i.ToString.Trim
        Loop
        Return tabla
    End Function
    Public Function creadataset(ByVal miconsulta As String, ByVal migrid As DataGrid) As DataGrid
        Dim conexion As SqlConnection = conecta()
        Dim odataset As DataSet
        Dim odataAdapter As SqlDataAdapter
        odataAdapter = New SqlDataAdapter(miconsulta, conexion)
        odataset = New DataSet
        odataAdapter.Fill(odataset)
        odataset.Tables(0).TableName = "mitabla"
        migrid.DataMember = "mitabla"
        migrid.DataSource = odataset.Tables(0).DefaultView
        migrid.DataBind()
        Return migrid
    End Function
    Public Function llenacombos(ByVal cmbmilista As DropDownList, ByVal consulta As String) As DropDownList
        cmbmilista.Items.Clear()
        Dim conexion As SqlConnection = conecta()
        Dim odataread As SqlDataReader
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = consulta
        conexion.Open()
        odataread = comando.ExecuteReader
        Dim i As Integer = 1
        cmbmilista.Items.Add("--seleccione una opcion--")
        cmbmilista.Items(0).Value = 0
        Do While odataread.Read
            cmbmilista.Items.Add(odataread.GetValue(1).ToString.Trim)
            cmbmilista.Items(i).Value = odataread.GetValue(0).ToString.Trim
            i = i + 1
        Loop
        odataread.Close()
        conexion.Close()
        Return cmbmilista
    End Function
    
    Public Sub grabaDatos(ByVal micadena As String)
        Dim conexion As SqlConnection = conecta()
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = micadena
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
    End Sub

    Public Sub grabaDatosLocal(ByVal micadena As String)
        Dim conexion As SqlConnection = conectaLocal()
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = micadena
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
    End Sub

    Function monto(ByVal valor As Double) As String
        Dim M As Integer
        Dim numitem(12) As String, numescr(12) As String
        Dim millon1 As String, millon As String, mill As String
        Dim mil As String, pesos As String
        Dim stvalor, stint, stcentav, xmonto As String
        Dim pos, centav, longi, tope, i, j As Integer

        stvalor = Str(valor)
        pos = InStr(stvalor, ".")
        If pos > 0 Then
            stint = Mid$(stvalor, 1, pos - 1)
            stcentav = Mid$(stvalor, pos + 1, Len(stvalor))
            If Len(stcentav) = 1 Then
                stcentav = stcentav & "0"
            End If
        Else
            stint = stvalor
            stcentav = "0"
        End If
        centav = Val(stcentav)
        xmonto = Trim$(stint)
        longi = Len(xmonto)
        i = 1
        For j = longi To 1 Step -1
            numitem(i) = Mid$(xmonto, j, 1)
            i = i + 1
        Next

        Try
            M = Int(Math.Log(valor) / Math.Log(10))
        Catch ex As Exception
            M = 0
        End Try
        tope = longi
        For i = 1 To tope Step 3
            If numitem(i) = "0" Then
                numescr(i) = ""
            ElseIf numitem(i) = "1" Then
                numescr(i) = IIf(M = 10, "", "UN ")
            ElseIf numitem(i) = "2" Then
                numescr(i) = "DOS "
            ElseIf numitem(i) = "3" Then
                numescr(i) = "TRES "
            ElseIf numitem(i) = "4" Then
                numescr(i) = "CUATRO "
            ElseIf numitem(i) = "5" Then
                numescr(i) = "CINCO "
            ElseIf numitem(i) = "6" Then
                numescr(i) = "SEIS "
            ElseIf numitem(i) = "7" Then
                numescr(i) = "SIETE "
            ElseIf numitem(i) = "8" Then
                numescr(i) = "OCHO "
            ElseIf numitem(i) = "9" Then
                numescr(i) = "NUEVE "
            End If
            If numitem(i) = "1" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "ONCE "
                numescr(i) = ""
            ElseIf numitem(i) = "2" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "DOCE "
                numescr(i) = ""
            ElseIf numitem(i) = "3" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "TRECE "
                numescr(i) = ""
            ElseIf numitem(i) = "4" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "CATORCE "
                numescr(i) = ""
            ElseIf numitem(i) = "5" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "QUINCE "
                numescr(i) = ""
            ElseIf numitem(i) > "5" And numitem(i + 1) = "1" Then
                numescr(i + 1) = "DIECI"
            End If
            If numitem(i + 1) = "1" And numitem(i) = "0" Then
                numescr(i + 1) = "DIEZ "
            ElseIf numitem(i + 1) = "2" And numitem(i) = "0" Then
                numescr(i + 1) = "VEINTE "
            ElseIf numitem(i + 1) = "3" And numitem(i) = "0" Then
                numescr(i + 1) = "TREINTA "
            ElseIf numitem(i + 1) = "4" And numitem(i) = "0" Then
                numescr(i + 1) = "CUARENTA "
            ElseIf numitem(i + 1) = "5" And numitem(i) = "0" Then
                numescr(i + 1) = "CINCUENTA "
            ElseIf numitem(i + 1) = "6" And numitem(i) = "0" Then
                numescr(i + 1) = "SESENTA "
            ElseIf numitem(i + 1) = "7" And numitem(i) = "0" Then
                numescr(i + 1) = "SETENTA "
            ElseIf numitem(i + 1) = "8" And numitem(i) = "0" Then
                numescr(i + 1) = "OCHENTA "
            ElseIf numitem(i + 1) = "9" And numitem(i) = "0" Then
                numescr(i + 1) = "NOVENTA "
            ElseIf numitem(i + 1) = "2" Then
                numescr(i + 1) = "VEINTI"
            ElseIf numitem(i + 1) = "3" Then
                numescr(i + 1) = "TREINTA Y "
            ElseIf numitem(i + 1) = "4" Then
                numescr(i + 1) = "CUARENTA Y "
            ElseIf numitem(i + 1) = "5" Then
                numescr(i + 1) = "CINCUENTA Y "
            ElseIf numitem(i + 1) = "6" Then
                numescr(i + 1) = "SESENTA Y "
            ElseIf numitem(i + 1) = "7" Then
                numescr(i + 1) = "SETENTA Y "
            ElseIf numitem(i + 1) = "8" Then
                numescr(i + 1) = "OCHENTA Y "
            ElseIf numitem(i + 1) = "9" Then
                numescr(i + 1) = "NOVENTA Y "
            ElseIf numitem(i + 1) = "0" Then
                numescr(i + 1) = ""
            End If
            If numitem(i + 2) = "0" Then
                numescr(i + 2) = ""
            ElseIf numitem(i + 2) = "1" Then
                If (numitem(i + 1) + numitem(i) <> "00") Then
                    numescr(i + 2) = "CIENTO "
                Else
                    numescr(i + 2) = "CIEN "
                End If
            ElseIf numitem(i + 2) = "2" Then
                numescr(i + 2) = "DOSCIENTOS "
            ElseIf numitem(i + 2) = "3" Then
                numescr(i + 2) = "TRESCIENTOS "
            ElseIf numitem(i + 2) = "4" Then
                numescr(i + 2) = "CUATROCIENTOS "
            ElseIf numitem(i + 2) = "5" Then
                numescr(i + 2) = "QUINIENTOS "
            ElseIf numitem(i + 2) = "6" Then
                numescr(i + 2) = "SEISCIENTOS "
            ElseIf numitem(i + 2) = "7" Then
                numescr(i + 2) = "SETECIENTOS "
            ElseIf numitem(i + 2) = "8" Then
                numescr(i + 2) = "OCHOCIENTOS "
            ElseIf numitem(i + 2) = "9" Then
                numescr(i + 2) = "NOVECIENTOS "
            End If
        Next
        mil = ""
        mill = ""
        millon1 = ""
        millon = ""
        If longi < 4 Then
            mill = ""
            mil = ""
        ElseIf longi < 7 Then
            mill = ""
            mil = "MIL "
        ElseIf longi = 7 Then
            If numitem(7) <= "1" Then
                If Val(Mid$(xmonto, 2, 6)) = 0 Then
                    mill = "MILLON DE "
                    mil = ""
                ElseIf Val(Mid$(xmonto, 2, 3)) = 0 Then
                    mill = "MILLON "
                    mil = ""
                Else
                    mill = "MILLON "
                    mil = "MIL "
                End If
            Else
                If Val(Mid$(xmonto, 2, 6)) = 0 Then
                    mill = "MILLONES DE "
                    mil = ""
                ElseIf Val(Mid$(xmonto, 2, 3)) = 0 Then
                    mill = "MILLONES "
                    mil = ""
                Else
                    mill = "MILLONES "
                    mil = "MIL "
                End If
            End If
        ElseIf longi = 9 Then
            If Val(Mid$(xmonto, 4, 6)) = 0 Then
                mill = "MILLONES DE "
                mil = ""
            ElseIf Val(Mid$(xmonto, 7, 3)) = 0 Then
                mill = "MILLONES "
                mil = "MIL "
            ElseIf Val(Mid$(xmonto, 4, 3)) = 0 Then
                mill = "MILLONES "
                mil = ""
            Else
                mill = "MILLONES "
                mil = "MIL "
            End If
        ElseIf longi = 10 Then
            millon = "MILLONES "
            millon1 = "MIL "
        Else
            If Val(Mid$(xmonto, 3, 6)) = 0 Then
                mill = "MILLONES DE "
                mil = ""
            ElseIf Val(Mid$(xmonto, 6, 3)) = 0 Then
                mill = "MILLONES "
                mil = "MIL "
            ElseIf Val(Mid$(xmonto, 3, 3)) = 0 Then
                mill = "MILLONES "
                mil = ""
            Else
                mill = "MILLONES "
                mil = "MIL "
            End If
        End If

        If centav > 0 Then
            pesos = "PESOS " & stcentav & " /100 M.N"
        Else
            pesos = "PESOS 00/100 M.N"
        End If
        monto = numescr(10) & millon1 & numescr(9) & numescr(8) & _
        numescr(7) & millon & mill & numescr(6) & _
        numescr(5) & numescr(4) & mil & millon1 & _
        numescr(3) & numescr(2) & numescr(1) & pesos

    End Function
    Public Function leerValor(ByVal consulta As String) As String
        Dim conexion As SqlConnection = conecta()
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim resultado As String = ""
        comando.CommandText = consulta
        conexion.Open()
        Dim leer As SqlDataReader = comando.ExecuteReader
        If leer.Read Then
            resultado = leer.GetValue(0).ToString.Trim
        Else
            resultado = "0"
        End If
        If resultado = "" Then
            resultado = "0"
        End If
        leer.Close()
        conexion.Close()

        Return resultado
    End Function
    Public Function llenalista(ByVal consulta As String) As String
        Dim conexion As SqlConnection = conecta()
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim leer As SqlDataReader
        Dim cadregistros As String

        comando.CommandText = consulta
        conexion.Open()
        leer = comando.ExecuteReader
        cadregistros = ""
        Do While leer.Read
            cadregistros = cadregistros & leer.GetValue(0).ToString.Trim & "-" & leer.GetValue(1).ToString & "-" & leer.GetValue(2).ToString & ","
        Loop
        leer.Close()
        conexion.Close()
        If cadregistros.Trim <> "" Then
            cadregistros = Mid(cadregistros, 1, cadregistros.Length - 1)
        End If
        Return cadregistros.Trim
    End Function
    Public Function completalista(ByVal cadregistros As String, ByVal connom As String) As String
        Dim cadenafin, cad, cnombre, cita, status As String
        Dim i, j
        Dim val1, val2, val3, z As Integer
        ' SE RECARGA LA LISTA CON LOS Q FALTAN
        cadenafin = ""
        cnombre = ""
        i = 0
        For i = 1 To num_elementos(cadregistros, ",")
            cad = entry(i, cadregistros, ",")
            val1 = CInt(entry(1, cad, "-").ToString.Trim)
            val2 = CInt(entry(2, cad, "-").ToString.Trim)
            val3 = CInt(entry(3, cad, "-").ToString.Trim)
            cnombre = entry(4, cad, "-").ToString.Trim
            cita = entry(5, cad, "-").ToString.Trim
            status = entry(6, cad, "-").ToString.Trim
            z = val1
            For j = 1 To val3
                If connom = "1" Then                                                            'j
                    cadenafin = cadenafin.Trim + Str(z).Trim & "-" & Str(val2).Trim & "-" & Str(val3).Trim & "-" & cnombre & "-" & cita & "-" & status & ","
                Else
                    cadenafin = cadenafin.Trim + Str(z).Trim & "-" & Str(val2).Trim & ","
                End If
                z = z + 1
            Next
        Next
        cadenafin = Mid(cadenafin, 1, cadenafin.Length - 1)
        Return cadenafin
    End Function
    Public Function leerValores(ByVal consulta As String, ByVal columnas As Integer) As Array
        Dim conexion As SqlConnection = conecta()
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim filas As Integer = 0
        Dim resultado(columnas - 1, filas) As String
        comando.CommandText = consulta
        conexion.Open()
        Dim leer As SqlDataReader = comando.ExecuteReader
        Do While leer.Read
            ReDim Preserve resultado(columnas - 1, filas)
            Dim auxcolum As Integer = 0
            Do While auxcolum < columnas
                resultado(auxcolum, filas) = leer.GetValue(auxcolum).ToString.Trim
                auxcolum = auxcolum + 1
            Loop
            filas = filas + 1
        Loop
        leer.Close()
        conexion.Close()
        conexion.Dispose()
        Return resultado
    End Function
    Public Function lookup(ByVal valor As String, ByVal cadena As String, ByVal separador As String) As Integer
        Dim i, cont, posicion
        Dim letra, palabra As String

        posicion = 0
        cont = 0
        palabra = ""
        cadena = cadena.ToString.Trim & separador
        For i = 1 To cadena.Length
            letra = Mid(cadena, i, 1)
            If letra.ToString.Trim <> separador Then
                palabra = palabra & letra
            End If
            If letra.ToString.Trim = separador Then
                cont = cont + 1
                If palabra.Trim = valor.Trim Then
                    posicion = cont
                End If
                palabra = ""
            End If
        Next
        Return posicion
    End Function
    Public Function num_elementos(ByVal cadena As String, ByVal separador As String) As Integer
        Dim i, cont
        Dim letra As String

        cont = 0
        cadena = cadena.ToString.Trim & separador
        For i = 1 To cadena.Length
            letra = Mid(cadena, i, 1)
            If letra.ToString.Trim = separador Then
                cont = cont + 1
            End If
        Next
        Return cont
    End Function
    Public Function entry(ByVal pos As Integer, ByVal cadena As String, ByVal separador As String) As String
        Dim cad As String
        Dim i, cont
        Dim letra, palabra As String

        cad = ""
        cont = 0
        palabra = ""
        cadena = cadena.ToString.Trim & separador
        For i = 1 To cadena.Length
            letra = Mid(cadena, i, 1)
            If letra.ToString.Trim <> separador Then
                palabra = palabra & letra
            End If
            If letra.ToString.Trim = separador Then
                cont = cont + 1
                If cont = pos Then
                    cad = palabra
                End If
                palabra = ""
            End If
        Next
        Return cad
    End Function
    Public Function llenaListbox(ByVal listabox As ListBox, ByVal consulta As String) As ListBox
        listabox.Items.Clear()
        Dim conexion As SqlConnection = conecta()
        Dim odataread As SqlDataReader
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = consulta
        conexion.Open()
        odataread = comando.ExecuteReader
        Dim i As Integer = 0
        Do While odataread.Read
            listabox.Items.Add(odataread.GetValue(1).ToString.Trim)
            listabox.Items(i).Value = odataread.GetValue(0).ToString.Trim
            i = i + 1
        Loop
        odataread.Close()
        conexion.Close()
        Return listabox
    End Function
End Class
