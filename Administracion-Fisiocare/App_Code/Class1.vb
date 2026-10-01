Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Data

Public Class miclases
    Public Function conecta(ByVal db As String) As SqlConnection
        Dim miconexion As SqlConnection
        

        miconexion = New SqlConnection("Data Source=198.49.76.114\MSSQLSERVER2012;Initial Catalog=" + db + ";" & _
        "User ID=FisiocareApp; Password=C4r3*Ap+$05")

        '

        Return miconexion
    End Function
    Public Function conecta2(ByVal db As String) As SqlConnection
        Dim miconexion2 As SqlConnection



        miconexion2 = New SqlConnection("Data Source=medsol.ddns.net\COMPAC;Initial Catalog=" + db + ";" & _
        "User ID=FisiocareApp; Password=C4r3*Ap+$05")

        

        Return miconexion2
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

    Public Function LLenaGrid(ByVal miconsulta As String, ByVal migrid As DataGrid, ByVal db As String) As DataGrid
        Dim conexion As SqlConnection = conecta(db)
        Dim odataset As DataSet
        Dim odataAdapter As SqlDataAdapter
        odataAdapter = New SqlDataAdapter(miconsulta, conexion)
        odataAdapter.SelectCommand.CommandTimeout = 240
        odataset = New DataSet
        odataAdapter.Fill(odataset)
        odataAdapter.Dispose()
        odataset.Tables(0).TableName = "mitabla"
        migrid.DataMember = "mitabla"
        migrid.DataSource = odataset.Tables(0).DefaultView
        migrid.DataBind()
        Return migrid
    End Function
    Public Function LLenaGridC(ByVal miconsulta As String, ByVal migrid As DataGrid, ByVal db As String) As DataGrid
        Dim conexion As SqlConnection = conecta2(db)
        Dim odataset As DataSet
        Dim odataAdapter As SqlDataAdapter
        odataAdapter = New SqlDataAdapter(miconsulta, conexion)
        odataset = New DataSet
        odataAdapter.Fill(odataset)
        odataAdapter.Dispose()
        odataset.Tables(0).TableName = "mitabla"
        migrid.DataMember = "mitabla"
        migrid.DataSource = odataset.Tables(0).DefaultView
        migrid.DataBind()
        Return migrid
    End Function
    

    Public Function LLenaGridview(ByVal miconsulta As String, ByVal migrid As GridView, ByVal db As String) As GridView
        Dim conexion As SqlConnection = conecta(db)
        Dim odataset As DataSet
        Dim odataAdapter As SqlDataAdapter
        odataAdapter = New SqlDataAdapter(miconsulta, conexion)
        odataset = New DataSet
        odataAdapter.Fill(odataset)
        odataAdapter.Dispose()
        odataset.Tables(0).TableName = "mitabla"
        migrid.DataMember = "mitabla"
        migrid.DataSource = odataset.Tables(0).DefaultView
        migrid.DataBind()
        Return migrid
    End Function
    Public Sub grabaDatos(ByVal micadena As String, ByVal bd As String)
        Dim conexion As SqlConnection = conecta(bd)
        Dim comando As SqlCommand = conexion.CreateCommand
        comando.CommandText = micadena
        conexion.Open()
        comando.ExecuteNonQuery()
        conexion.Close()
    End Sub
    Public Function leerValor(ByVal consulta As String, ByVal db As String) As String
        Dim conexion As SqlConnection = conecta(db)
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

    Public Function llenacombos(ByVal cmbmilista As DropDownList, ByVal consulta As String, ByVal db As String) As DropDownList
        cmbmilista.Items.Clear()
        Dim conexion As SqlConnection = conecta(db)
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
    Public Function leerValores(ByVal consulta As String, ByVal columnas As Integer, ByVal db As String) As Array
        Dim conexion As SqlConnection = conecta(db)
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

    Public Function llenalista(ByVal consulta As String, ByVal db As String) As String
        Dim conexion As SqlConnection = conecta(db)
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

    Public Function creadataset(ByVal miconsulta As String, ByVal migrid As DataGrid, ByVal db As String) As DataGrid
        Dim conexion As SqlConnection = conecta(db)
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

    Public Function creadatatable(ByVal miconsulta As String, ByVal db As String) As DataTable
        Dim resultado As New DataTable
        Dim conexion As SqlConnection = conecta(db)
        Dim odataAdapter As SqlDataAdapter
        odataAdapter = New SqlDataAdapter(miconsulta, conexion)
        odataAdapter.Fill(resultado)
        Return resultado
    End Function
End Class
