Imports System.Data.SqlClient
Imports System.Data


Public Class ClaseDatos

    Private SqlError As String
    Private servidordb As String
    Private usuariodb As String
    Private contrasenadb As String
    Private basedatosdb As String
    Private con As SqlConnection
    Private tablacomandos As New DataTable

    Public Property Servidor As String
        Get
            Return Me.servidordb
        End Get
        Set(ByVal Value As String)
            Me.servidordb = Value
        End Set
    End Property
    Public Property BaseDatos As String
        Get
            Return Me.basedatosdb
        End Get
        Set(ByVal Value As String)
            Me.basedatosdb = Value
        End Set
    End Property
    Public Property Usuario As String
        Get
            Return Me.usuariodb
        End Get
        Set(ByVal Value As String)
            Me.usuariodb = Value
        End Set
    End Property
    Public Property Password As String
        Get
            Return Me.contrasenadb
        End Get
        Set(ByVal Value As String)
            Me.contrasenadb = Value
        End Set
    End Property
    Public ReadOnly Property MensajeError As String
        Get
            Return Me.SqlError
        End Get
    End Property



    Sub New()

      

        Servidor = "198.49.76.114\MSSQLSERVER2012"
        basedatosdb = "admfisio"
        usuariodb = "FisiocareApp"
        contrasenadb = "C4r3*Ap+$05"
        tablacomandos.Columns.Add("sqlcomando")

    End Sub
    Private Function conectar() As Integer
        con = New SqlClient.SqlConnection
        con.ConnectionString = "Data Source=" & servidordb & ";Initial Catalog=" & basedatosdb & ";" & _
    "User ID=" & usuariodb & "; Password=" & contrasenadb
        Try
            con.Open()
            Return 0
        Catch ex As Exception
            SqlError = ex.Message
            Return -1

        End Try
    End Function
    Private Sub desconectar()
        con.Close()
    End Sub
    Public Function cargatabla(ByVal sqlconsulta As String, ByRef dt As DataTable) As Integer
        Dim sqladaptador As SqlClient.SqlDataAdapter
        dt = New DataTable
        If conectar() <> 0 Then
            Return -1
        End If
        Try
            sqladaptador = New SqlClient.SqlDataAdapter(sqlconsulta, con)
            sqladaptador.Fill(dt)
            desconectar()
            Return 0
        Catch ex As SqlException
            desconectar()
            SqlError = ex.Message
            Return -1
        End Try
    End Function
    Public Sub cargaComando(ByVal sqlinstruc As String)
        Dim dtfila As DataRow
        dtfila.Item(0) = sqlinstruc
        tablacomandos.Rows.Add(dtfila)
    End Sub
    Public Function ejecutar()
        Dim trans As SqlTransaction
        Dim comando As SqlCommand
        If conectar() <> 0 Then ' se abre la conexion
            Return -1
        End If
        trans = con.BeginTransaction()

        Try
            For Each comandos As DataRow In tablacomandos.Rows
                comando = New SqlCommand(comandos.Item(0).ToString, con)
                comando.Transaction = trans
                comando.ExecuteNonQuery()
            Next
            trans.Commit()
            desconectar()
            tablacomandos.Rows.Clear()
            Return 0
        Catch ex As Exception
            SqlError = ex.Message
            trans.Rollback()
            Return -1
        End Try

    End Function
    Public Function insertid(ByVal cadInsert As String) As String
        Dim trans As SqlClient.SqlTransaction
        Dim sqladaptador As SqlClient.SqlDataAdapter
        Dim comando As SqlClient.SqlCommand
        Dim dt As New DataTable

        If conectar() <> 0 Then ' se abre la conexion
            Return "-1"
        End If

        trans = con.BeginTransaction() 'iniciamos transaccion
        cadInsert += vbNewLine & " SELECT SCOPE_IDENTITY() AS [SCOPE_IDENTITY];" 'agregamos el retorno del identity 
        Try
            comando = New SqlClient.SqlCommand(cadInsert, con)
            comando.Transaction = trans
            sqladaptador = New SqlClient.SqlDataAdapter(comando)
            sqladaptador.Fill(dt)
            trans.Commit()
            desconectar()
            Return IIf(IsDBNull(dt.Rows(0).Item(0)), "", dt.Rows(0).Item(0).ToString)
        Catch ex As SqlException
            SqlError = ex.Message
            trans.Rollback()
            Return "-1"
        End Try
    End Function




End Class
