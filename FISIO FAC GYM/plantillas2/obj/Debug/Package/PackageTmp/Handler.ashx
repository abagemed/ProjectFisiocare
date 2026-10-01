<%@ WebHandler Language="VB" Class="Handler" %>

Imports System
Imports System.Web
Imports System.Data
Imports AgeMED

Public Class Handler : Implements IHttpHandler, IReadOnlySessionState
    Private nombreArchivo As String
    Private video As Boolean
    
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        
        Try
           
            Select Case context.Request.HttpMethod
                Case "HEAD"
                Case "GET"
                    If GivenFilename(context) Then
                        DeliverFile(context)
                    End If
                Case "POST"
                    ' ajax calls POST, but it can be a "DELETE" if there is a QueryString on the context
                    If GivenFilename(context) Then
                        DeleteFile(context)
                    Else
                        Uploadfile(context)
                        subirImagenRX(context)
                        
                    End If
                    Return

                Case "PUT"
                Case "DELETE"
                    DeleteFile(context)
                    
                    Return

                Case "OPTIONS"
                Case Else
                    context.Response.ClearHeaders()
                    context.Response.StatusCode = 405
                    Return

            End Select
            
        Catch ex As Exception
            Throw New Exception("Información que deseo incluir", ex)
        End Try

    End Sub
    
    Private Sub Uploadfile(ByVal context As HttpContext)

        Dim i As Integer
        Dim r As New Generic.LinkedList(Of ViewDataUploadFilesResult)
        Dim files As String()
        Dim savedFileName As String = String.Empty
        Dim js As New Script.Serialization.JavaScriptSerializer
        
        Try
            
            If context.Request.Files.Count >= 1 Then
                
                Dim maximumFileSize As Integer = 200000
                
                context.Response.ContentType = "text/plain"
                For i = 0 To context.Request.Files.Count - 1
                    Dim hpf As HttpPostedFile
                    Dim FileName As String
                    hpf = context.Request.Files.Item(i)
            
                    If HttpContext.Current.Request.Browser.Browser.ToUpper = "IE" Then
                        files = hpf.FileName.Split(CChar("\\"))
                        FileName = files(files.Length - 1)
                    Else
                        Dim fecha As String
                        fecha = context.Session("fechaAgenda").ToString
                        fecha = fecha.Replace("/", "")
                        FileName = context.Session("folioConsulta") + "_" + fecha + "_" + hpf.FileName
                        nombreArchivo = FileName                        
                    End If
            
                    If hpf.ContentLength >= 0 And (hpf.ContentLength <= maximumFileSize * 1000 Or maximumFileSize = 0) Then
                        savedFileName = StorageRoot(context)
                        savedFileName = savedFileName & "\" + FileName
                        hpf.SaveAs(savedFileName)
                        
                        r.AddLast(New ViewDataUploadFilesResult(FileName, hpf.ContentLength, hpf.ContentType, savedFileName))
                    
                        Dim uploadedFiles = r.Last
                        Dim jsonObj = js.Serialize(uploadedFiles)
                        context.Response.Write(jsonObj.ToString)
                        'context.Response.ContentType = "application/json"
                        'context.Response.Write("[{""initialPreview"":""../../dist/Radiografias/C000079083_09032017_users_administrator_icon.png"",""append"":true}]")
                       
                        '????????????????????????????????????????????????????????????????????????????????????????????????????
                      
                
                    Else
                        
                        ' File to Big (using IE without ActiveXObject enabled
                        If hpf.ContentLength > maximumFileSize * 6000 Then
                            r.AddLast(New ViewDataUploadFilesResult(FileName, hpf.ContentLength, hpf.ContentType, String.Empty, "maxFileSize"))
                    
                        End If

                        Dim uploadedFiles = r.Last
                        Dim jsonObj = js.Serialize(uploadedFiles)
                        context.Response.Write(jsonObj.ToString)
                        
                    End If
                Next
                
            End If
            
        Catch ex As Exception
            Throw
        End Try

    End Sub
   
    Private Sub DeleteFile(ByVal context As HttpContext)
        Try
            Dim path = StorageRoot(context)
            Dim file = context.Request("f")
            path &= "\" + file
            
            If System.IO.File.Exists(path) Then
                System.IO.File.Delete(path)               
            End If
            borrarImagenRX(context)
        Catch ex As Exception
            Throw
        End Try
    End Sub
    
    

    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property
    
#Region "Generic helpers"
    
    
    Private Function StorageRoot(ByVal context As HttpContext) As String
        Try
            Dim uploadFilesTempBasePath As String = ConfigurationManager.AppSettings("UploadFilesTempBasePath")
            Dim uploadFilesTempPath As String = ConfigurationManager.AppSettings("UploadFilesTempPath")
            Dim initPath As String = uploadFilesTempBasePath & uploadFilesTempPath
            Dim userfolder As String = context.Session("UserFolder") & "/radiografias"
            ' Add the Session Unique Folder Name
            If context.Session("UserFolder") IsNot Nothing Then
                If (initPath.LastIndexOf("\") <> initPath.Length - 1) Then
                    initPath &= "\"
                End If
                initPath &= context.Session("UserFolder") & "/radiografias"
           
            End If
            
            'CheckPath(initPath)
            
            Return initPath
        Catch ex As Exception
            Throw
        End Try
    End Function
    
    Private Sub CheckPath(ByRef serverPath As String)
        Dim initPath As String = String.Empty
        Dim tempPath As String = String.Empty
        Dim folders As String()
        
        Try
            
            folders = serverPath.Split(CChar("\\"))

            ' Save file to a server
            If serverPath.Contains("\\") Then
                initPath = "\\"
            Else
                ' Save file to a local folders  
            End If
            
            For i As Integer = 0 To folders.Length - 1
                If tempPath.Trim = String.Empty And _
                folders(i) <> String.Empty Then
                    tempPath = initPath & folders(i)
                ElseIf tempPath.Trim <> String.Empty And _
                folders(i).Trim <> String.Empty Then
                    tempPath = tempPath & "\" & folders(i)
                    
                    ' Doesn't check if it's a network connection
                    If Not tempPath.Contains("\\") And _
                    Not folders(i).Contains("$") Then
                        
                        If Not System.IO.Directory.Exists(tempPath) Then
                            System.IO.Directory.CreateDirectory(tempPath)
                        End If
 
                    Else
                        If Not System.IO.Directory.Exists(tempPath) Then
                            System.IO.Directory.CreateDirectory(tempPath)
                        End If
                        
                    End If
                    
                End If

            Next
            
            If serverPath.Contains("http://") Then
                serverPath = tempPath & "/"
            Else
                serverPath = tempPath & "\"
            End If
          
            
        Catch ex As Exception
            Throw
        End Try
    End Sub
    
    Private Function GivenFilename(ByVal context As HttpContext) As Boolean
        Try
            Return Not String.IsNullOrEmpty(context.Request("f"))
        Catch ex As Exception
            Throw
        End Try
    End Function
    
    Private Sub DeliverFile(ByVal context As HttpContext)
        Try
            Dim file = context.Request("f")
            Dim filePath = StorageRoot(context) + file
            If System.IO.File.Exists(filePath) Then
                context.Response.AddHeader("Content-Disposition", "attachment; filename=" + file)
                context.Response.ContentType = "application/octet-stream"
                context.Response.ClearContent()
                context.Response.WriteFile(filePath)
               
            Else
                context.Response.StatusCode = 404
            End If
            
        Catch ex As Exception
            Throw
        End Try
    End Sub
    
#End Region

    Private Sub borrarImagenRX(context As HttpContext)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        strSQL = "update A set A.status='2',A.codigoUsuario='" & context.Request("codigoUsuario") & "', A.fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                    "from [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] as E " & _
                   "inner join [" & clsDatos.BaseDatos & "].[dbo].[HmArchivosEstudios] as A on A.folioEstudio=E.folioEstudio and A.status='1' and A.CodigoEmpresa='" & context.Request("codigoEmpresa") & "' " & _
                   "where  E.CodigoPaciente='" & context.Request.QueryString("CodigoPaciente") & "' and A.url='" & context.Request("f") & "'  and A.status='1' and A.CodigoEmpresa='" & context.Request("codigoEmpresa") & "' "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            
        Else
            MsgBox("Error: " + clsDatos.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub

    Private Sub subirImagenRX(context As HttpContext)
        Dim strSQL As String
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
              
        strSQL = "SELECT folioEstudio,folioConsulta,codigoEstudio  FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] " & _
                "where folioconsulta='" & context.Session("folioConsulta") & "'and codigoPaciente='" & context.Session("codigoPaciente") & "' and codigoEstudioGabinete=1 and codigoEmpresa=1 and status=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim folioEstudio As String
                folioEstudio = dt.Rows(0).Item("folioEstudio")
                'insert solo en archivos.
                strSQL = " INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[HmArchivosEstudios] (folioEstudio,tipoArchivo,Url,descripcion,status,codigoUsuario,codigoEmpresa,fechaActualizacion) " & _
                         " VALUES(" & folioEstudio & ",'Imagen','" & nombreArchivo & "',' ',1,1," & context.Session("codigoEmpresa") & ",convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "', 20)) "
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar = 0 Then
                    Exit Sub
                    
                Else
                    MsgBox("error: " + clsDatos.MensajeError + nombreArchivo)
                    
                End If
            Else
                Dim folioEstudio As String = 0
                'Si no tiene Folio de Estudio lo crea en EstudiosdeGabinete.
                strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresdet] " & _
                        "where codigoFoliador = 2 And codigoEmpresa = 1"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    folioEstudio = dt.Rows(0).Item("consecutivo") + 1
                Else
                    MsgBox(clsDatos.MensajeError)
                End If
                
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresdet] set consecutivo=" & folioEstudio & " " & _
                            "where codigoFoliador=2 and codigoEmpresa=1  " & _
                    "insert into [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios]  " & _
                          "(FolioEstudio,FolioConsulta,CodigoEstudioGabinete,CodigoPaciente,usuarioSolicita,codigoUsuario,status,codigoEmpresa,codigoEstudio,fechaActualizacion) " & _
                    "VALUES(" & folioEstudio & ",'" & context.Session("folioConsulta") & "',1,'" & context.Session("CodigoPaciente") & "',0,'" & context.Session("codigoUsuario") & "',1," & context.Session("codigoEmpresa") & ",'" & context.Session("codigoUsuario") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "', 20)) " & _
                    " INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[HmArchivosEstudios] (folioEstudio,tipoArchivo,Url,descripcion,status,codigoUsuario,codigoEmpresa,fechaActualizacion) " & _
                         " VALUES(" & folioEstudio & ",'Imagen','" & nombreArchivo & "',' ',1,1," & context.Session("codigoEmpresa") & ",convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "', 20)) "
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar = 0 Then
                    Exit Sub
                    
                Else
                    MsgBox("error: " + clsDatos.MensajeError + nombreArchivo)
                    
                End If
            End If
            
        End If
    
    End Sub
    
    
   
    

End Class

#Region "local Class"

Public Class ViewDataUploadFilesResult
    Public nombre As String
    Public size As Integer
    Public tipo As String
    Public link As String
    Public delete_url As String
    Public delete_type As String
    Public MSG As String
    Public initialPreview As String
    Public initialPreviewConfig As String
    Public append As String
    Sub New()
        Try

        Catch ex As Exception
            Throw
        End Try
    End Sub

    Sub New(ByVal Name As String, ByVal Length As Integer, ByVal Type As String, ByVal URL As String)
        Try
            nombre = Name
            size = Length
            tipo = Type
            link = "Handler.ashx?f=" + Name
            delete_url = "Handler.ashx?f=" + Name
            delete_type = "POST"
            'initialPreview = Name
            'initialPreviewConfig = Name
            'append = "True"
           
        Catch ex As Exception
            Throw
        End Try
    End Sub
    Sub New(ByVal Name As String, ByVal Length As Integer, ByVal Type As String, ByVal URL As String, ByVal errorMSG As String)
        Try
            nombre = Name
            size = Length
            tipo = Type
            link = "Handler.ashx?f=" + Name
            delete_url = "Handler.ashx?f=" + Name
            delete_type = "POST"
            MSG = errorMSG
            
            'initialPreview = Name
            'initialPreviewConfig = Name
            'append = "True"
            
        Catch ex As Exception
            Throw
        End Try
    End Sub
    
    
End Class


#End Region
