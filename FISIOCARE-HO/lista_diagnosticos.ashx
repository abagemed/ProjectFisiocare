<%@ WebHandler Language="VB" Class="lista_diagnosticos" %>

Imports System
Imports System.Web
Imports System.Data

Public Class lista_diagnosticos : Implements IHttpHandler, IReadOnlySessionState
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim buscar As String = context.Request("q").ToUpper
        Dim data As String
        
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
 
        data = "[]"
        
       
        
        strSql = " SELECT IdDiagnostico, Descripcion, Status " & _
        "FROM CatDiagnosticos " & _
        "where descripcion like '%" & buscar & "%' and Status='True ' "
        
        
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
                    cadena += "{""id"":""" & i.Item("IdDiagnostico") & """, ""text"":""" & i.Item("Descripcion") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
             
        End If
        'Return Data
        
       
        'data = "[{""id"":5,""text"":""HOMBRO CONDROMATOSIS SINOVIAL""},{""id"":6,""text"":""" & buscar & """}]"

        'Si no se encuentra se devuelve vacío
        'data = "[]"

        context.Response.ContentType = "application/json; charset=utf-8"
        context.Response.Write(data)
    End Sub
 
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class