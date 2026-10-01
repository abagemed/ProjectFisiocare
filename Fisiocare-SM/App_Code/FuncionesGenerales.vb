Imports System.Data
Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Collections.Generic

Public Class FuncionesGenerales
    Private MenError As String
    Private dropdowns As New List(Of DropDownList)
    Private tablaconsultas As New DataTable
    Public ReadOnly Property MensajeError As String
        Get
            Return Me.MenError
        End Get
    End Property
    ' se crea la columna para almacenar las consultas y llenar los dropdowns
    Sub New()
        tablaconsultas.Columns.Add("consulta")
    End Sub
    '*** llenar un solo dropdown
    Public Function llenadropdown(ByVal strsql As String, ByRef dropdown As DropDownList)
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            Try
                dropdown.DataSource = dt
                dropdown.DataValueField = dt.Columns(0).ColumnName
                dropdown.DataTextField = dt.Columns(1).ColumnName
                dropdown.DataBind()
                Return 0
            Catch ex As Exception
                MenError = ex.Message
                Return -1
            End Try
        Else
            MenError = clsdatos.MensajeError
            Return -1
        End If
    End Function
    '** llena un listbox
    Public Function llenalistbox(ByVal strsql As String, ByRef lista As ListBox)
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        lista.Items.Clear()

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            Try
                lista.DataSource = dt
                If dt.Rows.Count > 0 Then
                    lista.DataValueField = dt.Columns(0).ColumnName
                    lista.DataTextField = dt.Columns(1).ColumnName
                    lista.DataBind()
                    Return 0
                Else
                    Return 1
                End If

            Catch ex As Exception
                MenError = ex.Message
                Return -1
            End Try
        Else
            MenError = clsdatos.MensajeError
            Return -1
        End If
    End Function
    '***agrega datos de varios dropdowns para su llenado
    Public Sub agregarllenadodropdown(ByVal strsql As String, ByRef dropdown As DropDownList)
        dropdowns.Add(dropdown)
        tablaconsultas.Rows.Add(strsql)
    End Sub
    '*** llena varios dropdowns
    Public Function llenardropdownvarios()
        For i As Integer = 0 To tablaconsultas.Rows.Count - 1
            If Not llenadropdown(tablaconsultas.Rows(i).Item(0), dropdowns.Item(i)) = 0 Then
                Return -1
            End If
        Next
        tablaconsultas.Rows.Clear()
        dropdowns.Clear()
        Return 0
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
End Class
