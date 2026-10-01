Imports System.IO

Public Class EstudiosRX
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            Session.Add("UserFolder", Context.Server.MapPath("~/archivos"))
            'Session.Add("UserFolder", "http://107.161.180.154/hortopedia/img")
            CargaDatosPaciente()
            CargaImgRX()
            cargaEstudios()
            folioRX()
        End If
        
    End Sub

    'FileUpload
    Private Sub CargaDatosPaciente()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "SELECT A.codigoPaciente,(P.PApellido+' '+P.SApellido+' '+P.Nombres)as nombre,concat(iif((iif(DATEPART(dayofyear,P.FechaNacimiento) > DATEPART(dayofyear,getdate())  , " & _
        " datediff(YEAR,P.FechaNacimiento, getdate()) -1 ,datediff(YEAR,P.FechaNacimiento, getdate()))) = 0, '', concat((iif(DATEPART(dayofyear,P.FechaNacimiento) > DATEPART(dayofyear,getdate())  , " & _
         " datediff(YEAR,P.FechaNacimiento, getdate()) -1 ,datediff(YEAR,P.FechaNacimiento, getdate()))) ,' años ')) , " & _
         " concat(FLOOR((CAST(DATEDIFF(day, p.FechaNacimiento, GETDATE()) AS float) / 365 - FLOOR(CAST(DATEDIFF(day, p.FechaNacimiento, " & _
         " GETDATE()) AS float) / 365)) * 12) , ' meses ')) as edad," & _
        "P.FechaNacimiento, calle,iif(p.Genero = 'H','MASCULINO','FEMENINO') as Genero, " & _
                "FechaNacimiento,O.descripcion as ocupacion,N.nacionalidad," & _
                "A.folioConsulta,A.CodigoUsuarioAtiende  " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                "inner join  [" & clsDatos.BaseDatos & "].[dbo].[CatPacientes] as P on A.CodigoPaciente=P.CodigoPaciente " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatOcupaciones] as O on P.codigoOcupacion=O.codigoOcupacion " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatNacionalidades] as N  on N.codigoNacionalidad=P.codigoNacionalidad " & _
                 "where folioConsulta='" & Session("folioConsulta") & "' and A.codigoEmpresa='1' and A.status='1' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                lblpaciente.Text = dt.Rows(0).Item("nombre")
                Lblfechanacimiento.Text = dt.Rows(0).Item("FechaNacimiento")
                Lblfolioconsulta.Text = dt.Rows(0).Item("folioConsulta")
                Session("folioConsulta") = dt.Rows(0).Item("folioConsulta")
                Lblnacionalidad.Text = dt.Rows(0).Item("nacionalidad")
                lblocupacion.Text = dt.Rows(0).Item("ocupacion")
                Lblgenero.Text = dt.Rows(0).Item("Genero")
                LblDrAtiende.Text = Session("sesion")
                lbledad.Text = dt.Rows(0).Item("edad")
                CodigoPaciente.Value = dt.Rows(0).Item("codigoPaciente")
                Session("codigoPaciente") = dt.Rows(0).Item("codigoPaciente")
            End If
        Else
            MsgBox("Error: " + clsDatos.MensajeError)
        End If
    End Sub

    Sub CargaImgRX()
        Dim strSql As String
        Dim img As String = ""
        Dim previus As String = ""
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim i As Integer = 1
        strSql = "SELECT  E.folioEstudio,E.folioConsulta,E.CodigoPaciente,A.url,A.Descripcion,A.folioArchivo " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] as E " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[HmArchivosEstudios] as A on A.folioEstudio=E.folioEstudio and A.status='1' " & _
                "where codigoPaciente='" & CodigoPaciente.Value & "' and E.codigoEmpresa='1' and A.tipoArchivo='Imagen' and E.status='1' " & _
                 "order by folioEstudio desc"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                img += "["
                previus = "["
                For Each fila As DataRow In dt.Rows
                    'img += "'" + "http://107.161.180.154/hortopedia/img/" + fila.Item("url") + "'"
                    img += "'../../archivos/radiografias/" + fila.Item("url") + "'"
                    previus += "{caption: " + Chr(34) + fila.Item("url").ToString + Chr(34) + ",size: " + Chr(34) + getTamFile(Session("UserFolder") + "\" + fila.Item("url")) + Chr(34) + ", width: " + Chr(34) + "120px" + Chr(34) + ",url: " + Chr(34) + "Handler.ashx?f=" + fila.Item("url").ToString + "&codigoPaciente=" + CodigoPaciente.Value + "&codigoEmpresa=" + Session("codigoEmpresa").ToString + "&codigoUsuario=" + Session("codigoUsuario").ToString + Chr(34) + ", key: " + i.ToString + "}"
                    If i < dt.Rows.Count Then
                        img += ","
                        previus += ","
                        i = i + 1
                    End If
                Next
                img += "]"
                previus += "]"
                ImagenRX.Value = img
                PreviusRX.Value = previus

            Else
                ImagenRX.Value = "[]"
                PreviusRX.Value = "{}"
            End If
        Else
            MsgBox("Error: " + clsDatos.MensajeError, MsgBoxStyle.Critical)
        End If
    End Sub

    Sub cargaEstudios()
        Dim strSQL, est As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        est = ""
        strSQL = "SELECT (C.zona+C.estudio) as estudio,E.extremidad " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudiosDet] as E " & _
                 "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatEstudios] as C on C.codigoEstudio=E.codigoEstudio and C.codigoestudioGab=1 and C.status=1 " & _
                " where E.folioConsulta='" & Lblfolioconsulta.Text & "' and E.status=1 and E.codigoEmpresa=" & Session("codigoEmpresa") & " "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For x = 0 To dt.Rows.Count - 1
                    est += "<li>" + dt.Rows(x).Item("estudio") + "</li>"
                Next
                LblLadoExtremidad.Text = dt.Rows(0).Item("extremidad")
            Else
                est = "Sin estudio asignado"
                LblLadoExtremidad.Text = ""
            End If
            LblEstudioRX.Text = est
        End If
    End Sub


    Private Sub folioRX()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "SELECT folioEstudio,folioConsulta,codigoEstudio  FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] " & _
               "where folioconsulta='" & Session("folioConsulta") & "'and codigoPaciente='" & Session("codigoPaciente") & "' and codigoEstudioGabinete=1 and codigoEmpresa=1 and status=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count = 0 Then

                Dim folioEstudio As String
                strSQL = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresdet] " & _
                        "where codigoFoliador=2  and codigoEmpresa=1"
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    folioEstudio = dt.Rows(0).Item("consecutivo") + 1
                    strSQL = " update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresdet] set consecutivo=" & folioEstudio & " " & _
                            "where codigoFoliador=2  and codigoEmpresa=1  " & _
                    "insert into [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios]  " & _
                                  "(FolioEstudio,FolioConsulta,CodigoEstudioGabinete,CodigoPaciente,usuarioSolicita,status,codigoEmpresa,codigoUsuario,codigoEstudio,fechaActualizacion) " & _
                "values(" & folioEstudio & ",'" & Session("folioConsulta") & "',1,'" & CodigoPaciente.Value & "',0,'" & Session("codigoUsuario") & "',1," & Session("codigoEmpresa") & ",'" & Session("codigoUsuario") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "', 20)) "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        Exit Sub
                    Else
                        MsgBox(clsDatos.MensajeError)
                    End If
                End If
            Else
                Exit Sub
            End If
        End If
    End Sub


    Public Function getTamFile(ByVal path As String) As String
        Dim fi As New FileInfo(path)
        If fi.Exists Then
            Return fi.Length
        Else
            Return String.Empty
        End If
    End Function

End Class