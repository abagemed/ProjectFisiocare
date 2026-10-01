<%@ WebHandler Language="VB" Class="terapias" %>

Imports System
Imports System.Web
Imports System.Data


Public Class terapias : Implements IHttpHandler, IReadOnlySessionState
    Public Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest
        Dim opcion As String = context.Request("opcion")
        Dim id As String = context.Request("id")
        Dim idcita As String = context.Request("idcita")
        Dim fecha As String = context.Request("fecha")
        Dim observaciones As String = context.Request("observaciones")
        Dim alergias As String = context.Request("alergias")
        Dim indicaciones_medicas As String = context.Request("indicaciones_medicas")
        Dim contra_indicaciones As String = context.Request("contra_indicaciones")
        Dim idcitafecha As String = context.Request("idcitafecha")
        Dim idcliente As String = context.Request("idcliente")
        Dim idterapia As String = context.Request("idterapia")
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String = ""
        Dim ultrasonido As String = context.Request("ultrasonido")
        Dim chc As String = context.Request("chc")
        Dim electroterapia As String = context.Request("electroterapia")
        Dim laser As String = context.Request("laser")
        Dim cf As String = context.Request("cf")
        Dim ejercicio As String = context.Request("ejercicio")
        Dim masaje As String = context.Request("masaje")
        Dim magneto As String = context.Request("magneto")
        Dim gimnasia As String = context.Request("gimnasia")
        Dim id_diagnostico As String = context.Request("id_diagnostico")
        Dim idH_diagnostico As String = context.Request("id")
        Dim id_protocolo As String = context.Request("id_protocolo")
        Dim idH_protocolo As String = context.Request("id")
        Dim nombre_diagnostico As String = context.Request("nombre_diagnostico")
        Dim fase As String = context.Request("fase")
        Dim data As String = ""
        
        If opcion = 1 Then
            'Leer datos
            data = devolverDatospaciente(idcita)
            
        ElseIf opcion = 2 Then
            'Guardar datos terapias
            
            'IMedicas, CIndicaciones, AlegiasPadecimientos
            
            strSQL = " select  idTerapia FROM [Terapia]  where idcliente=" & idcliente & " "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'update
                    strSQL = "update  [Terapia]  set IMedicas='" & indicaciones_medicas & "',CIndicaciones='" & contra_indicaciones & "' ,AlegiasPadecimientos='" & alergias & "' where idcliente=" & idcliente & " "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                Else
                    'insert
                    strSQL = " insert into [Terapia] (idcliente, muscular, basica, laser, consulta, movimiento, adicional, hospital,IMedicas, CIndicaciones, AlegiasPadecimientos) " & _
                    "VALUES(" & idcliente & ",'False','False','False','False','False','False','False','" & indicaciones_medicas & "','" & contra_indicaciones & "','" & alergias & "') "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            
            'Guardar datos tratamientos
            strSQL = " select idtratamiento FROM [HMTratamientos]  where idcliente=" & idcliente & " and idcita =" & idcitafecha & "  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'update
                    strSQL = "update  [HMTratamientos]  set ultrasonido=" & ultrasonido & ",chc=" & chc & " ,electroterapia=" & electroterapia & " ,laser=" & laser & " ,cf=" & cf & " ,ejercicio=" & ejercicio & " ,masaje=" & masaje & " ,magneto=" & magneto & " ,gimnasia=" & gimnasia & ",observaciones='" & observaciones & "'   where idcliente=" & idcliente & " and idcita =" & idcitafecha & "  "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                Else
                    'insert
                    strSQL = " insert into [HMTratamientos] (idcliente,idcita,ultrasonido, chc, electroterapia, laser, cf, ejercicio, masaje, magneto, gimnasia, observaciones) " & _
                    "VALUES(" & idcliente & "," & idcita & "," & ultrasonido & "," & chc & "," & electroterapia & "," & laser & "," & cf & "," & ejercicio & "," & masaje & "," & magneto & "," & gimnasia & ",'" & observaciones & "') "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            
            
            data = "[]"
            
        ElseIf opcion = 3 Then
            
            'Leer datos del historial
            data = devolverDatoshistorial(idcitafecha)
            
            
        ElseIf opcion = 4 Then
            'Guardar el diagnostico y devolver todos los diagnosticos registrados
            
            'strSQL = " select  IdHDiagnostico FROM [HMDiagnosticos]  where IdDiagnostico=" & id_diagnostico & " and idcita =" & idcita & " and Status='True'  "
            
            strSQL = " select idTerapia,CD.Descripcion as diagnostico, left(fechaInicio,12) as fechaInicio,HD.IdHDiagnostico " & _
"from terapia T " & _
"inner join Agenda A on A.dtinicio= T.fechaInicio and T.idCliente=a.idCliente " & _
"inner join HMDiagnosticos HD on HD.IdCita = A.idCita " & _
"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
"where T.idCliente=" & idcliente & "  and HD.IdDiagnostico=" & id_diagnostico & " and HD.Status='True' order by fechaInicio"
            
            
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'mensaje que ya existe
                    data = "[]"
                Else
                    'insert
                    strSQL = " insert into [HMDiagnosticos] (IdDiagnostico,Idcliente, FechaActualizacion, Status) " & _
                    "VALUES(" & id_diagnostico & "," & idcliente & ",GETDATE(),'True') "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        'mensaje de guardado correctamente
                        data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            data = devolverDatosdiagnosticos((idcita), (idcliente))
        
        ElseIf opcion = 5 Then
            'Eliminar el diagnostico y devolver todos los diagnosticos registrados
            
            strSQL = "    update HMDiagnosticos  set Status = 'False', FechaActualizacion = getdate() " & _
                 "where  IdHDiagnostico=" & idH_diagnostico & " and Status='True'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverDatosdiagnosticos((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            'data = "[{""id"":""1"",""diagnostico"":""hombro"",""fecha"":""20-03-2018""}]"
        ElseIf opcion = 6 Then
            'Devolver la lista de protocolos guardados en el historial
            
            'data = llenarprotocolos()
            data = devolverprotocolosguardados((idcita), (idcliente))
            ' data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
           
        ElseIf opcion = 7 Then
            'Devolver la incormación del protocolo
            'id_protocolo
            
            
            data = devolverDatosprotocolos(id_protocolo)
           
        ElseIf opcion = 8 Then
            'Guardar el protocolo en el expediente médico del paciente
            'id_protocolo
            
            Dim strSqlp As String
            Dim dtp As New DataTable
            Dim Tratamientop As String
            Dim contraindicacionp As String
            Dim observacionesp As String
            Dim TratamientoM As String
            Dim contraindicacionM As String
            Dim observacionesM As String
       
            strSqlp = " select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + id_protocolo + "' "
        
        
            If clsDatos.cargatabla(strSqlp, dtp) = 0 Then
                If dtp.Rows.Count > 0 Then
              
                    Tratamientop = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Dosificación".PadRight(30, " ") + "|" + "Tiempo".PadRight(15, " ") + "|" + Chr(10)
                    Tratamientop = Tratamientop + "-".PadRight(105, "-") + Chr(10)
                    contraindicacionp = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Precauciones".PadRight(30, " ") + "|" + Chr(10)
                    contraindicacionp = contraindicacionp + "-".PadRight(63, "-") + Chr(10)
                    observacionesp = "|" + "Equipo/Ejercicio".PadRight(30, " ") + "|" + "Intensidad".PadRight(25, " ") + "|" + Chr(10)
                    observacionesp = observacionesp + "-".PadRight(58, "-") + Chr(10)

                  
                    For Each F As DataRow In dtp.Rows
                  
                        Tratamientop = Tratamientop + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("dosificacion").ToString.PadRight(30, " ") + "|" + F.Item("tiempo").ToString.PadRight(15, " ") + "|" + Chr(10)
                        contraindicacionp = contraindicacionp + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("precauciones").ToString.PadRight(30, " ") + "|" + Chr(10)
                        observacionesp = observacionesp + "|" + F.Item("equipoejercicio").ToString.PadRight(30, " ") + "|" + F.Item("intensidad").ToString.PadRight(25, " ") + "|" + Chr(10)
       
                    Next
                    
                    TratamientoM = Tratamientop
                    contraindicacionM = contraindicacionp
                    observacionesM = observacionesp

                End If
            Else
                data = "[]"
            End If
            
            strSQL = " select idHProtocolo FROM [HMProtocolos]  where IdProtocolo=" & id_protocolo & " and idcliente=" & idcliente & " and idcita =" & idcita & "  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    'mensaje que ya existe
                    data = "[]"
                Else
                    'insert
                    strSQL = " insert into [HMProtocolos] (idcliente, idcita, idProtocolo, tratamiento, contraindicaciones, observaciones,fase, Status,FechaActualizacion) " & _
                    "VALUES(" & idcliente & "," & idcita & "," & id_protocolo & ",'" & TratamientoM & "','" & contraindicacionM & "','" & observacionesM & "','" & fase & "','True',GETDATE()) "
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = data = "[]"
                    Else
                        'error
                        data = "{""resp"" : ""1""}"
                    End If
                End If
            End If
            
            
            data = devolverprotocolosguardados((idcita), (idcliente))
           
           
        
        ElseIf opcion = 9 Then
            'Eliminar protocolo de la tabla y devolver la tabla con la información
            
             
            strSQL = "    update HMProtocolos  set Status = 'False', FechaActualizacion = getdate() " & _
                 "where  IdHProtocolo=" & idH_protocolo & " and Status='True'"
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar() = 0 Then
                data = devolverprotocolosguardados((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            'data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
            
            
            
        ElseIf opcion = 10 Then
            
            strSQL = " select idHProtocolo FROM [HMProtocolos]  where IdProtocolo=" & id_protocolo & " and idcliente=" & idcliente & " and idcita =" & idcita & "  "
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    strSQL = " update HMProtocolos  set Fase = " & fase & ", FechaActualizacion = getdate() " & _
                         "where  IdHProtocolo=" & idH_protocolo & " and Status='True'"
                    clsDatos.cargaComando(strSQL)
                    If clsDatos.ejecutar() = 0 Then
                        data = devolverprotocolosguardados((idcita), (idcliente))
                    Else
                        data = "[]"
                    End If
                Else
                  
                End If
            End If
            data = devolverprotocolosguardados((idcita), (idcliente))
            
    
            'Cambiar la fase del protocolo con la fecha actual y devolver la tabla con la información
            'data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""}]"
        ElseIf opcion = 11 Then
            'La opción 11 no devuelve nada solo abre el modal en la vista del usuario para agregar el diagnostico nuevo
            data = "[]"
        ElseIf opcion = 12 Then
            'Se guarda el diagnosito nuevo en la tabla
            
            
            strSQL = " insert into [" & clsDatos.BaseDatos & "].[dbo].[CatDiagnosticos] (Descripcion, Status) VALUES('" & nombre_diagnostico & "','True')"
            clsDatos.cargaComando(strSQL)
           If clsDatos.ejecutar() = 0 Then
                data = devolverDatosdiagnosticos((idcita), (idcliente))
            Else
                data = "[]"
            End If
            
            
            'data = "[{""id"":""1"", ""nombre"":""Nuevo""}]"
        End If
        context.Response.ContentType = "text/plain"
        context.Response.Write(data)
    End Sub
    
    Function devolverDatospaciente(ByRef idcita As String) As String
        Dim strSql As String
        Dim strSql2 As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        Dim Fechas As String
        Dim icita As String
  
        strSql = " select P.idcliente as Id,(P.paterno+' '+P.materno+' '+P.nombre) as paciente,P.edad as edad,p.domicilio as direccion, p.ciudadorigen as origen, CO.descripcion as ocupacion,p.fechanacimiento as fechanacimiento, " & _
        "T.IMedicas as IMedicas, T.CIndicaciones as CIndicaciones,T.AlegiasPadecimientos as AlegiasPadecimientos, " & _
        "A.Fecha as FechaAgenda, isnull(HT.ultrasonido,0) as ultrasonido,isnull(HT.chc,0) as chc,isnull(HT.electroterapia,0) as electroterapia,isnull(HT.laser,0) as laser, " & _
        "isnull(HT.cf,0) as cf,isnull(HT.ejercicio,0) as ejercicio,isnull(HT.masaje,0) as masaje,isnull(HT.magneto,0) as magneto, " & _
        "isnull(HT.gimnasia,0) as gimnasia,isnull(HT.observaciones,'') as observaciones " & _
        "from agenda A " & _
        "inner join clientes P on P.idcliente=A.idcliente " & _
        "left join terapia T on T.idcliente=A.idcliente " & _
        "left join HMTratamientos HT on HT.idcita=A.idcita " & _
        "left join CatOcupaciones CO on CO.IdOcupacion=P.idocupacion " & _
        "where A.idCita='" + idcita + "' "
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                
                strSql2 = "SELECT A.idcita as idcita,format(A.fecha,'dd/MM/yyyy') as fecha  FROM agenda A " & _
                " where A.idcliente = '" & dt.Rows(0).Item("Id") & "' " & _
                " order by A.fecha desc "
                If clsDatos.cargatabla(strSql2, dt2) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim FechaA As String
                        Dim IdcitaA As String
                        Dim count2 As Integer = 1
                        For Each F As DataRow In dt2.Rows
                            If count2 > 1 Then
                                FechaA += "."
                                IdcitaA += "."
                            End If
                            count2 = count2 + 1
                            If Not IsDBNull(F.Item("fecha")) Then
                                FechaA += F.Item("fecha")
                            End If
                            If Not IsDBNull(F.Item("idcita")) Then
                                IdcitaA += F.Item("idcita").ToString
                            End If
                            
                            
                            
                        Next
                        Fechas = FechaA
                        icita = IdcitaA
                    End If
                Else
                    'FechaA
                End If
                
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                For Each i As DataRow In dt.Rows
                    If count > 1 Then
                        cadena += ","
                    End If
                    count = count + 1
                    cadena += "{""id"":""" & i.Item("Id") & """,""nombre"":""" & i.Item("paciente") & """,""fecha_nac"":""" & i.Item("fechanacimiento") & """,""edad"":""" & i.Item("edad") & """,""origen"":""" & i.Item("origen") & """,""ocupacion"":""" & i.Item("ocupacion") & """,""direccion"":""" & i.Item("direccion") & """,""alergias"":""" & i.Item("AlegiasPadecimientos") & """, ""indicaciones_medicas"":""" & i.Item("IMedicas") & """, ""contra_indicaciones"":""" & i.Item("CIndicaciones") & """ , ""fecha"":""" & Fechas & """, ""idfecha"":""" & icita & """,""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            'data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa Cebalos"",""fecha_nac"":""24/04/1981"",""edad"":""37"",""origen"":""Mérida"",""ocupacion"":""Máster de la web"",""direccion"":""Vergel III"",""alergias"":""a los hombres"", ""indicaciones_medicas"":""Tomar viagra"", ""contra_indicaciones"":""No tomar viagra si tomó alcohol"",""id_diagnosticos"":""1.2.3"", ""diagnosticos"":""HOMBRO CONDROMATOSIS.HOMBRO.TOBILLO"", ""protocolo"":""3"",""fase"":""4"", ""fecha"":""20/02/2019.24/02/2019.28/02/2019"",""ultrasonido"":""1"", ""chc"":""1"", ""electroterapia"":""0"", ""laser"":""1"", ""cf"":""1"", ""ejercicio"":""1"",""masaje"":""0"",""magneto"":""0"",""gimnasia"":""0"",""observaciones"":""Esta muy guapo el paciente"" }]"
        End If
        Return Data
    End Function
    
    Function devolverDatoshistorial(ByRef idcitafecha As String) As String
        Dim strSql As String
        'Dim strSql2 As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        ' Dim Fechas As String
        'Dim icita As String
  
        
       
        
        strSql = " SELECT idtratamiento, idcliente, idcita, ultrasonido, chc, electroterapia, laser, cf, ejercicio, masaje, magneto, gimnasia, observaciones " & _
        "FROM HMTratamientos " & _
        "where idcita='" + idcitafecha + "' "
        
        
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
                    cadena += "{""id"":""" & i.Item("idtratamiento") & """, ""ultrasonido"":""" & i.Item("ultrasonido") & """, ""chc"":""" & i.Item("chc") & """, ""electroterapia"":""" & i.Item("electroterapia") & """, ""laser"":""" & i.Item("laser") & """, ""cf"":""" & i.Item("cf") & """, ""ejercicio"":""" & i.Item("ejercicio") & """,""masaje"":""" & i.Item("masaje") & """,""magneto"":""" & i.Item("magneto") & """,""gimnasia"":""" & i.Item("gimnasia") & """,""observaciones"":""" & i.Item("observaciones") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            ' data = "[{""id"":""1"",""nombre"":""Christhian Froilan Sosa Cebalos"",""fecha_nac"":""24/04/1981"",""edad"":""37"",""origen"":""Mérida"",""ocupacion"":""Máster de la web"",""direccion"":""Vergel III"",""alergias"":""a los hombres"", ""indicaciones_medicas"":""Tomar viagra"", ""contra_indicaciones"":""No tomar viagra si tomo alcohol"",""id_diagnosticos"":""1.2.3"", ""diagnosticos"":""HOMBRO CONDROMATOSIS.HOMBRO.TOBILLO"", ""protocolo"":""3"",""fase"":""4"",""fecha"":""20/02/2019.24/02/2019.28/02/2019"",""ultrasonido"":""0"", ""chc"":""0"", ""electroterapia"":""0"", ""laser"":""1"", ""cf"":""1"", ""ejercicio"":""1"",""masaje"":""1"",""magneto"":""1"",""gimnasia"":""1"",""observaciones"":""Esta muy guapo el paciente"" }]"
            
        End If
        Return Data
    End Function
  
    Function devolverDatosdiagnosticos(ByRef idcita As String, ByRef idcliente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
  
        
       
        
        'strSql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as Descripcion , FORMAT(A.Fecha , 'dd MMMM yyyy') as FechaAgenda    " & _
        '"from agenda A  " & _
        '"inner join HMDiagnosticos HD on HD.IdCita = A.idCita " & _
        '"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
        '"where A.idCliente  = 1 and HD.Status = 'True'"
        
        strSql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as Descripcion , " & _
 "isnull(FORMAT(HD.FechaActualizacion , 'dd MMMM yyyy'),FORMAT(A.Fecha , 'dd MMMM yyyy')) as fechaagenda " & _
 "from HMDiagnosticos HD " & _
 "left join agenda A  on A.IdCita = HD.idCita " & _
 "inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
 "where HD.idCliente  = '" + idcliente + "'  and HD.Status = 'True'"
        
        
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
                    cadena += "{""id"":""" & i.Item("IdHDiagnostico") & """, ""diagnostico"":""" & i.Item("Descripcion") & """, ""fecha"":""" & i.Item("FechaAgenda") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            
            'Data = "[{""id"":""1"",""diagnostico"":""hombro"",""fecha"":""20-03-2018""},{""id"":""2"",""diagnostico"":""hombro2"",""fecha"":""22-02-2018""},{""id"":""3"",""diagnostico"":""hombro3"",""fecha"":""25-02-2018""}]"
        
            
        End If
        Return Data
    End Function
    
    'Function llenarprotocolos() As String
    '    Dim strSql As String
    '    Dim dt As New DataTable
    '    Dim dt2 As New DataTable
    '    Dim clsDatos As New ClaseDatos
    '    Dim Data As String = ""
  
        
       
    '    strSql = " SELECT  distinct id, nombre " & _
    '  "FROM protocolos "
        
            
        
    '    If clsDatos.cargatabla(strSql, dt) = 0 Then
    '        If dt.Rows.Count > 0 Then
    '            Dim cadena As String
    '            Dim count As Integer = 1
    '            cadena = "["
    '            For Each i As DataRow In dt.Rows
    '                If count > 1 Then
    '                    cadena += ","
    '                End If
    '                count = count + 1
    '                cadena += "{""id"":""" & i.Item("id") & """, ""nombre"":""" & i.Item("nombre") & """}"
    '            Next
    '            cadena += "]"
    '            Data = cadena

    '        End If
    '    Else
            
    '        Data = "[{""id"":""0"",""nombre"":""Seleccionar Protocolo""}]"
        
    '        'data = "[{""id"":""0"",""nombre"":""Seleccionar Protocolo""},{""id"":""1"",""nombre"":""Protocolo 1""},{""id"":""2"",""nombre"":""Protocolo 2""},{""id"":""3"",""nombre"":""Protocolo 3""}]"
        
    '    End If
        
    '    Return Data
    'End Function
    
    Function devolverDatosprotocolos(ByRef id_protocolo As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
        Dim Tratamiento As String
        Dim contraindicacion As String
        Dim observaciones As String
        Dim TratamientoM As String
        Dim contraindicacionM As String
        Dim observacionesM As String
       
       
        
        
        strSql = " select equipoejercicio,dosificacion,intensidad,tiempo,precauciones from protocolos where id='" + id_protocolo + "' "
        
        
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
              
                Tratamiento = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Dosificación</th><th>Tiempo</th></tr></thead><tbody>"
               
                
                contraindicacion = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Precauciones</th></tr></thead><tbody>"
                'contraindicacion = contraindicacion + "-".PadRight(63, "-") + Chr(10)
                
                observaciones = "<table class='table table-striped table-hover'><thead><tr><th>Equipo/Ejercicio</th><th>Intensidad</th></tr></thead><tbody>"
                'observaciones = observaciones + "-".PadRight(58, "-") + Chr(10)

                Dim count2 As Integer = 1
                For Each F As DataRow In dt.Rows
                  
                    Tratamiento = Tratamiento + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("dosificacion") + "</td><td>" + F.Item("tiempo") + "</td></tr>"
                    contraindicacion = contraindicacion + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("precauciones") + "</td></tr>"
                    observaciones = observaciones + "<tr><td>" + F.Item("equipoejercicio") + "</td><td>" + F.Item("intensidad") + "</td><td>"
       
                Next
                TratamientoM = Tratamiento + "</tbody></table>"
                contraindicacionM = contraindicacion + "</tbody></table>"
                observacionesM = observaciones + "</tbody></table>"
               
                
                Dim cadena As String
                Dim count As Integer = 1
                cadena = "["
                TratamientoM = Replace(TratamientoM, Chr(9), " ")
                TratamientoM = Replace(TratamientoM, Chr(10), " ")
                TratamientoM = Replace(TratamientoM, Chr(13), " ")
                TratamientoM = Replace(TratamientoM, Chr(160), " ")
                
                contraindicacionM = Replace(contraindicacionM, Chr(9), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(10), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(13), " ")
                contraindicacionM = Replace(contraindicacionM, Chr(160), " ")
                
                observacionesM = Replace(observacionesM, Chr(9), " ")
                observacionesM = Replace(observacionesM, Chr(10), " ")
                observacionesM = Replace(observacionesM, Chr(13), " ")
                observacionesM = Replace(observacionesM, Chr(160), " ")
                
                
                cadena += "{""tab1"":""" & TratamientoM & """,""tab2"":""" & contraindicacionM & """,""tab3"":""" & observacionesM & """}"
                'Next
                cadena += "]"
                Data = cadena

            End If
        Else
            'data = "[{""target1"":""Información del protocolo"",""target2"":""Información para la pestaña 2"",""target3"":""Pestaña 3""}]"    
        End If
        Return Data
    End Function
    
    Function devolverprotocolosguardados(ByRef idcita As String, ByRef idcliente As String) As String
        Dim strSql As String
        Dim dt As New DataTable
        Dim dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim Data As String = ""
  
        
       
        
        'strSql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as Descripcion , FORMAT(A.Fecha , 'dd MMMM yyyy') as FechaAgenda    " & _
        '"from agenda A  " & _
        '"inner join HMDiagnosticos HD on HD.IdCita = A.idCita " & _
        '"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
        '"where A.idCliente  = (select idCliente from agenda where Idcita ='" + idcita + "' and HD.Status = 'True')"
        
        strSql = " select HDP.idHProtocolo as IdHProtocolo ,HDP.idProtocolo as IdProtocolo , PR.nombre as Descripcion , " & _
        "FORMAT(A.Fecha , 'dd MMMM yyyy') as FechaAgenda " & _
        "from agenda A  " & _
        "inner join HMProtocolos HDP on HDP.IdCita = A.idCita " & _
        "inner join protocolos PR on PR.id = HDP.idProtocolo " & _
        "where A.idCliente  = (select idCliente from agenda where Idcita ='" + idcita + "' and HDP.Status = 'True') " & _
        "group by HDP.idHProtocolo,PR.nombre,HDP.idProtocolo,a.fecha"
        
        
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
                    cadena += "{""id"":""" & i.Item("IdHProtocolo") & """, ""protocolo"":""" & i.Item("Descripcion") & """,""fase"":""FASE 1"", ""fecha"":""" & i.Item("FechaAgenda") & """}"
                Next
                cadena += "]"
                Data = cadena

            End If
        Else
            
            'Data = "[{""id"":""1"",""protocolo"":""PARALISIS FACIAL"",""fase"":""FASE 1"",""fecha"":""20-03-2018""},{""id"":""2"",""protocolo"":""PARALISIS GLUTEAL"",""fase"":""FASE 3"",""fecha"":""20-03-2018""}]"
       
        
            
        End If
        Return Data
    End Function
    
    Public ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class