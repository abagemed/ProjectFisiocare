Imports System
Imports System.Web
Imports System.Web.Services
Imports System.Data
Imports AgeMED

Public Class guardar
    Implements System.Web.IHttpHandler, IReadOnlySessionState



    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Dim PadecimientoActual As String = context.Request("PadecimientoActual")
        Dim Exploracion As String = context.Request("Exploracion")
        Dim Plan As String = context.Request("Plan")
        Dim Patologicos As String = context.Request("Patologicos")
        Dim NoPatologicos As String = context.Request("NoPatologicos")
        Dim Alergias As String = context.Request("Alergias")
        Dim TensionArterial As String = context.Request("TensionArterial")
        Dim FrecuenciaCardiaca As String = context.Request("FrecuenciaCardiaca")
        Dim Peso As String = context.Request("Peso")
        Dim Estatura As String = context.Request("Estatura")
        Dim IMC As String = ""
        Dim resultado As String = ""
        Dim Hffolioconsulta As String = context.Request("Hffolioconsulta")
        Dim HFCodPaciente As String = context.Request("HFCodPaciente")
        ' Dim Justificacion As String = context.Request("summernote").ToString
        'Dim HFfolioJustificacion As String = context.Request("HFFolioJustificacion")
        Dim strsql As String
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable


        If Val(Estatura) > 0 And (Peso) <> "" Then
            IMC = String.Format("{0:00.00}", (Val(Peso)) / (Val(Estatura) * Val(Estatura)))

            If IMC < 15.99 Then
                resultado = "Infrapeso: Delgadez Severa"
            ElseIf IMC > 16 And IMC < 16.99 Then
                resultado = "Infrapeso: Delgadez Moderada"
            ElseIf IMC > 17 And IMC < 18.49 Then
                resultado = "Infrapeso: Delgadez Aceptable"
            ElseIf IMC > 18.5 And IMC < 24.99 Then
                resultado = "Peso normal"
            ElseIf IMC > 25 And IMC < 29.99 Then
                resultado = "Sobrepeso"
            ElseIf IMC > 30 And IMC < 34.99 Then
                resultado = "Obesidad grado 1"
            ElseIf IMC > 35 And IMC < 39.99 Then
                resultado = "Obesidad grado 2"
            ElseIf IMC > 40 Then
                resultado = "Obesidad grado 3"
            End If

        Else
            resultado = ""
        End If

        'TABLA HMEXPLORACION
        strsql = "SELECT folioconsulta FROM hmexploracion WHERE folioconsulta ='" & Hffolioconsulta & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                strsql = "UPDATE hmexploracion SET PadecimientoActual='" & PadecimientoActual & "' " & _
                         ", TensionArterial='" & TensionArterial & "', FrecuenciaCardiaca='" & FrecuenciaCardiaca & "', Peso='" & Peso & "', Estatura='" & Estatura & "', IndiceMasaCorporal='" & IMC & "' " & _
                         ", Exploracion='" & Exploracion & "', planseguir='" & Plan & "' WHERE folioconsulta='" & Hffolioconsulta & "'"
                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = -1 Then
                    'Error
                End If
            Else
                strsql = "INSERT INTO hmexploracion (" & _
                "folioconsulta, PadecimientoActual, Exploracion, PlanSeguir, farmRecibidos, codigoExtremidad, TensionArterial, " & _
                "FrecuenciaCardiaca, Peso, Estatura, IndiceMasaCorporal, CodigoUsuario," & _
                "CodigoEmpresa, Status, FechaActualizacion) VALUES (" & _
                 "'" & Hffolioconsulta & "', '" & PadecimientoActual & "', " & _
                 "'" & Exploracion & "', '" & Plan & "', '', '', '" & TensionArterial & "', '" & FrecuenciaCardiaca & "', '" & Peso & "', '" & Estatura & "', '" & IMC & "'," & _
                 "'" & context.Session("codigoUsuario") & "', '" & context.Session("codigoEmpresa") & "', 1," & _
                 " getdate())"

                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = -1 Then
                    'Error
                End If
                strsql = "UPDATE AgAgenda SET CodigoUsuarioAtiende='" & context.Session("codigoUsuario") & "' WHERE folioconsulta='" & Hffolioconsulta & "'"
                clsdatos.cargaComando(strsql)
                clsdatos.ejecutar()

            End If
        Else
            'Error
        End If
        'TABLA HMANTECEDENTES MEDICOS
        strsql = "select am.codigopaciente from agagenda a "
        strsql += vbNewLine & " left join hmantecedentesmedicos am on a.codigopaciente = am.codigopaciente "
        strsql += vbNewLine & "  where a.folioconsulta = '" & Hffolioconsulta & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0).Item("codigopaciente")) Then
                    strsql = " INSERT INTO [dbo].[HmAntecedentesMedicos]([CodigoPaciente],[AntecedentesPatologicos]" & _
                     " ,[AntecedentesNoPatologicos],[AlergiasComunes],[CodigoUsuario],[CodigoEmpresa],[FechaActualizacion])" & _
                     " VALUES('" & HFCodPaciente & "','" & Patologicos.Trim & "','" & NoPatologicos.Trim & "'" & _
                     " ,'" & Alergias.Trim & "','" & context.Session("codigoUsuario") & "','" & context.Session("codigoEmpresa") & "',getdate())"
                    clsdatos.cargaComando(strsql)
                    clsdatos.ejecutar()
                Else
                    strsql = " UPDATE hm SET hm.antecedentespatologicos = '" & Patologicos.Trim & "', hm.AntecedentesNoPatologicos = '" & NoPatologicos.Trim & "'," & _
                     " hm.alergiascomunes = '" & Alergias.Trim & "', codigousuario = '" & context.Session("codigoUsuario") & "', fechaactualizacion = getdate()" & _
                     " FROM hmantecedentesmedicos hm" & _
                     " WHERE hm.codigopaciente = '" & HFCodPaciente & "'"
                    clsdatos.cargaComando(strsql)
                    clsdatos.ejecutar()
                End If
            Else
                'Error
            End If
        End If


        context.Response.ContentType = "application/json; charset=utf-8"
        'context.Response.Write("{""data"" : ""1""}")
        context.Response.Write("{""imc"":""" & IMC & """,""resultado"":""" & resultado & """}")

        'Justificacion -------------------------------------------------------------------------------------------------------------
      
        'strsql = "select FolioConsulta,DJustificacion FROM HmJustificaciones " & _
        '                " WHERE FolioConsulta ='" & Hffolioconsulta & "' and folioJustificacion=" & HFfolioJustificacion & " "
        'If clsdatos.cargatabla(strsql, dt) = 0 Then
        '    If dt.Rows.Count > 0 Then
        '        strsql = "  update HmJustificaciones set Cjustificacion='" & Justificacion & "' " & _
        '                 "where FolioJustificacion ='" & HFfolioJustificacion & "' "
        '        clsdatos.cargaComando(strsql)
        '        If clsdatos.ejecutar() = 0 Then

        '        Else
        '            'error
        '        End If
        '    Else
        '        strsql = "INSERT INTO HmJustificaciones (FolioConsulta, DJustificacion, CJustificacion, FechaJustificacion,CodigoUsuario,CodigoEmpresa,status,FechaActualizacion "
        '        strsql = strsql & ") VALUES ('" & Hffolioconsulta & "','getdate()','" & Justificacion & "','getdate()', "
        '        strsql = strsql & context.Session("codigoUsuario") & ",1,1 ,GETDATE())"
        '        clsdatos.cargaComando(strsql)
        '        If clsdatos.ejecutar() = 0 Then

        '        Else
        '            'error
        '        End If
        '    End If
        'End If



    End Sub

 

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

End Class