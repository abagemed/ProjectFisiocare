<%@ WebHandler Language="VB" Class="VerCitas" %>

Imports System
Imports System.Web
Imports System.Data


Public Class VerCitas : Implements IHttpHandler, IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim opcion As String = context.Request("opcion")
        Dim nombre As String = context.Request("nombre")
        Dim paterno As String = context.Request("paterno")
        Dim materno As String = context.Request("materno")
        Dim id_paciente As String = context.Request("id_paciente")
        Dim id As String = context.Request("id")
        Dim data As String = "[]"
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String = ""
        
        If opcion = 1 Then
            'Buscar paciente
            
            data = BusquedaPaciente(nombre, paterno, materno)
            'data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa""}, {""id"":""2"",""nombre"":""Karla Rosas""}]"
        ElseIf opcion = 2 Then
            'Traer citas programadas
            data = DevolverDatosCitasProgramadas(id_paciente)
            'data = "[{""folio"":""1"",""codigo"":""3"",""consultorio"":""Consultorio 1"",""turno"":""Turno 1"",""fecha"":""03-03-2019"",""estado"":""Finalizado"",""agendo"":""Margarita""},{""folio"":""1"",""codigo"":""32"",""consultorio"":""Consultorio 2"",""turno"":""Turno 2"",""fecha"":""03-03-2019"",""estado"":""Finalizado"",""agendo"":""Margarita""}]"
        ElseIf opcion = 3 Then
            'traer citas pasadas
            data = DevolverDatosCitasPasadas(id_paciente)
            'data = "[{""folio"":""1"",""codigo"":""3"",""consultorio"":""Consultorio 1"",""turno"":""Turno 1"",""fecha"":""03-03-2019"",""estado"":""Finalizado"",""agendo"":""Margarita""},{""folio"":""1"",""codigo"":""32"",""consultorio"":""Consultorio 2"",""turno"":""Turno 2"",""fecha"":""03-03-2019"",""estado"":""Finalizado"",""agendo"":""Margarita""}]"
        ElseIf opcion = 4 Then
            
            strSQL = " insert into [cancelaciones] ( idcita,cancelacion,fecha) " & _
                   "VALUES(" & id & ",'SE CANCELO DESDE VER CITAS',GETDATE()) "
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                'SE cancelo Correctamente 
            End If
            
            
            strSQL = " update agenda set estado='CANCELADO' where idCita='" + id + "'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                'SE Actualizo Correctamente
            End If
            
            data = DevolverDatosCitasProgramadas(id_paciente)
            
            'Cancelar la cita el id es el folio de la cita
            'Puede devolver vacío 
            'data = "[]"
            
            
            
        End If
        
        
        
        context.Response.ContentType = "text/plain"
        context.Response.Write(data)
    End Sub
    
     
    Function BusquedaPaciente(ByRef nombre As String, ByRef paterno As String, ByRef materno As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        
        strSql = "SELECT idCliente,elnombre FROM clientes " & _
           "where paterno like '%" + paterno + "%' and nombre like '%" + nombre + "%' and materno like '%" + materno + "%' order by elnombre"
  
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    
                    cadena += "{""id"":""" & i.Item("idCliente") & """, ""nombre"":""" & i.Item("elnombre") & """}"
                Next
                cadena += "]"
                Data = cadena
            Else
                Data = "{""resp"" : ""1""}"
            End If
        Else
            Data = "{""resp"" : ""1""}"
        
        End If
        Return Data
    End Function
    
    Function DevolverDatosCitasProgramadas(ByRef id_paciente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        


        
        strSql = " SELECT A.idcita as idcita,P.idcliente as idcliente,TP.Nombre as nombreterapista,H.horario as turno,format(A.fecha,'dd/MM/yyyy') as fecha,A.estado as estado   FROM " & _
        "agenda A " & _
        "inner join clientes P on P.idcliente=A.idcliente " & _
        "left join Terapistas TP on TP.columna=A.posicion and TP.turno=A.turno " & _
        "inner join horarios H on H.idhorario=A.idHorario and H.turno=A.turno " & _
        "where A.idcliente  = '" + id_paciente + "' AND estado<>'CANCELADO' and A.fecha>='" & Format(Date.Now, "dd/MM/yyyy") & "' and A.estado <> 'REALIZADO'" & _
        "order by A.fecha Asc "
       
   
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    
                    cadena += "{""folio"":""" & i.Item("idcita") & """, ""codigo"":""" & i.Item("idcliente") & """, ""consultorio"":""" & i.Item("nombreterapista") & """, ""turno"":""" & i.Item("turno") & """,""fecha"":""" & i.Item("fecha") & """,""estado"":""" & i.Item("estado") & """,""agendo"":""" & i.Item("nombreterapista") & """}"
                Next
                cadena += "]"
                Data = cadena
            Else
                Data = "{""resp"" : ""1""}"
            End If
        Else
            Data = "{""resp"" : ""1""}"
        
        End If
        Return Data
    End Function
    
    Function DevolverDatosCitasPasadas(ByRef id_paciente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        


        
        strSql = " SELECT A.idcita as idcita,P.idcliente as idcliente,A.posicion as posicion,H.turno as turno,format(A.fecha,'dd/MM/yyyy') as fecha,A.estado as estado   FROM " & _
        "agenda A " & _
        "inner join clientes P on P.idcliente=A.idcliente " & _
        "left join Terapistas TP on TP.columna=A.posicion and TP.turno=A.turno " & _
        "inner join horarios H on H.idhorario=A.idHorario and H.turno=A.turno " & _
        "where A.idcliente  = '" + id_paciente + "' and A.fecha<='" & Format(Date.Now, "dd/MM/yyyy") & "'" & _
        "order by A.fecha Desc "
       
   
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    
                    cadena += "{""folio"":""" & i.Item("idcita") & """, ""codigo"":""" & i.Item("idcliente") & """, ""consultorio"":""" & i.Item("posicion") & """, ""turno"":""" & i.Item("turno") & """,""fecha"":""" & i.Item("fecha") & """,""estado"":""" & i.Item("estado") & """}"
                Next
                cadena += "]"
                Data = cadena
            Else
                Data = "{""resp"" : ""1""}"
            End If
        Else
            Data = "{""resp"" : ""1""}"
        
        End If
        Return Data
    End Function
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class