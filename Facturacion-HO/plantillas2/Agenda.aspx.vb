'Nombre: Gerardo G. valdez
'Fecha: 05/08/2016
'Agenda principal de AGEMED

Imports System.Drawing
Imports System.Drawing.ImageFormatConverter
Imports System.Windows
Imports System.Security.Cryptography.X509Certificates
Imports System.Collections.Generic
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml
Imports AgeMED.WSFEL.RespuestaTFD
Imports AgeMED.WSFEL
Imports ThoughtWorks.QRCode.Codec
Imports CFDI
Imports System.Net.Mail
Imports System.Net
Imports CrystalDecisions.Web
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Public Class Agenda
    Inherits System.Web.UI.Page
    Dim bloqueo() As String
    Private fromIndex As Integer
    Private dragIndex As Integer
    Private dragRect As Rectangle
    Dim bandera_factura As Integer
    Private bandera_facturaCredito As Integer
    Dim fechapdf As String




    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Dim miscookies As New cookies
        PanelCritico.Visible = False
        miscookies.mirequest = Page.Request
        miscookies.miresponse = Page.Response
        Session("modalidad") = ""

        If Not IsPostBack Then

            If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
                Response.Redirect("login.aspx")
            End If
            ' es esto creo
            TxtAgFecha.Text = Session("FechaAgenda")
            cargaAgenda()
            llenaEtapas()
            contadorEstadosAgenda()
            PorcentajeDeMeta()
            cargarConsultorios()

        End If
    End Sub
    Sub cargaAgenda()
        Dim strSQL As String
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Dim BlnConBloqueo As Boolean
        Dim fechaHoraActual As DateTime
        strSQL = " eXECUTE  [" & clsdatos.BaseDatos & "].[dbo].[genhorarios] " & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",N'G',1,N'" & CDate(TxtAgFecha.Text) & "',0"
        If clsdatos.cargatabla(strSQL, dt) = 0 Then
            DGAgenda.DataSource = dt
            DGAgenda.DataBind()
            'carga primero los boqueos
            BlnConBloqueo = Funbloqueos(Session("codigoConsultorio"), TxtAgFecha.Text)
            If BlnConBloqueo = True Then
                For Each i As String In bloqueo
                    For Each turno1 As DataGridItem In DGAgenda.Items
                        If turno1.Cells(0).Text = i Then
                            CType(turno1.Cells(2).Controls(1), LinkButton).Text = "BLOQUEADO"
                            CType(turno1.Cells(2).Controls(1), LinkButton).Enabled = False
                            CType(turno1.Cells(2).Controls(1), LinkButton).CssClass = "btn btn-default btn-block btn-lg disabled text-red"
                            turno1.Cells(2).BackColor = Drawing.Color.Gray
                        End If
                    Next
                Next
            End If
            DGAgenda.Columns(6).Visible = True
            DGAgenda.Columns(7).Visible = False
            DGAgenda.Columns(8).Visible = False
            '////////////////////////////////////////// Permisos Para tipo de Usuario, Habilitar o desa-habilitar la vista de columnas de la Agenda \\\\\\\\\\\\\\\
            If Session("rol") = "RE" Then
                DGAgenda.Columns(6).Visible = True
                DGAgenda.Columns(7).Visible = True
                DGAgenda.Columns(8).Visible = True
            End If
            '///////////////////////////////////////////    Permisos para carga de paciente según de Agenda RX   \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
            If Session("rol") = "RX" Then
                strSQL = "select  row_number() over(order by numeroTurno) as NumeroTurno,A.codigoEtapa ,E.imagen,E.colorFondoRGB,E.colorTexto,A.codigoPaciente,A.FolioConsulta,A.FechaAgenda, '0.00' as costo,A.horallegada, M.descripcion as modalidad,PApellido+' '+Sapellido+' '+P.nombres as paciente  " & _
                     "from [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] as A inner join [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] as P on A.CodigoPaciente=P.CodigoPaciente and A.codigoEmpresa=P.CodigoEmpresa " & _
                     "inner join  [" & clsdatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E on A.CodigoEtapa=E.codigoEtapa and A.codigoEmpresa=E.CodigoEmpresa " & _
                     "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatModalidades] as M on A.CodigoModalidad=M.codigoModalidad and A.codigoEmpresa=M.CodigoEmpresa " & _
                     "where A.fechaAgenda='" & TxtAgFecha.Text & "'  and A.CodigoEtapa='5' and A.codigoEmpresa='" & Session("codigoEmpresa") & "' "
            Else
                ' Catalogo pacientes, en caso de que la empresa solo pueda ver sus pacientes se le agregra "and A.codigoEmpresa=P.CodigoEmpresa" en caso de tener pacientes compartidos, no se valida empresa.

                strSQL = "select  E.imagen,E.colorFondoRGB,E.colorTexto,A.codigoPaciente,A.FolioConsulta,A.FechaAgenda," & _
                    "format(isnull(IA.ImportePago,0),'N','en-us') as costo,isnull(IA.pagoRealizado,0) as pagoRealizado,isnull(IA.facturado,0) as facturado, A.horallegada, A.codigoEtapa, " & _
                    "A.numeroTurno, M.descripcion as modalidad,PApellido+' '+Sapellido+' '+P.nombres as paciente " & _
                    "from [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] as P " & _
                    "on A.CodigoPaciente=P.CodigoPaciente   " & _
                    "inner join  [" & clsdatos.BaseDatos & "].[dbo].[CatEtapasAgenda] as E " & _
                    "on A.CodigoEtapa=E.codigoEtapa and A.codigoEmpresa=E.CodigoEmpresa " & _
                    "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatModalidades] as M  " & _
                    "on A.CodigoModalidad=M.codigoModalidad and A.codigoEmpresa=M.CodigoEmpresa " & _
                    "left join [" & clsdatos.BaseDatos & "].[dbo].[ingresosCaja]  as IA " & _
                    "on A.folioConsulta=IA.folioConsulta and IA.status=1 and IA.codigoEmpresa=" & Session("codigoEmpresa") & " " & _
                    "where A.codigoConsultorio='" & Session("codigoConsultorio") & "' and A.fechaAgenda='" & TxtAgFecha.Text & "' and A.CodigoEtapa<>'4' " & _
                    "and A.codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                    "  order by A.numeroTurno"

            End If
            If clsdatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    For Each fila As DataRow In dt.Rows
                        For Each turno As DataGridItem In DGAgenda.Items
                            If turno.Cells(0).Text = fila.Item("numeroTurno").ToString Then
                                CType(turno.Cells(2).Controls(1), LinkButton).Text = fila.Item("paciente") 'Nombre del paciente
                                CType(turno.Cells(2).Controls(1), LinkButton).ForeColor = Drawing.Color.White
                                CType(turno.Cells(2).Controls(3), Label).Text = fila.Item("codigoPaciente")
                                CType(turno.Cells(3).Controls(1), LinkButton).Text = "<i class='fa fa-2x fa-" & fila.Item("imagen") & "' style='color:#000000; padding:10px 0 0 10px'></i>"
                                '00000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000
                                ' If BlnEstudioRealizado = True Then
                                'CType(turno.Cells(3).Controls(7), ImageButton).ImageUrl = "../Resource/Estados/" & fila.Item("imagen") & ".png"
                                'CType(turno.Cells(3).Controls(7), ImageButton).Visible = True
                                'BlnEstudioRealizado = False
                                'End If

                                CType(turno.Cells(3).Controls(1), LinkButton).Visible = True
                                CType(turno.Cells(3).Controls(3), Label).Text = fila.Item("codigoEtapa")
                                CType(turno.Cells(4).Controls(1), Label).Text = fila.Item("modalidad")
                                If fila.Item("codigoEtapa") = 6 And Session("rol") = "RE" Then
                                    CType(turno.Cells(8).Controls(1), Button).Visible = True
                                End If
                                CType(turno.Cells(5).Controls(1), Label).Text = fila.Item("folioconsulta")
                                Select Case fila.Item("codigoEtapa")
                                    Case 2
                                        fechaHoraActual = DateTime.Now
                                        '---------------------------------------Calculo de tiempo en confirmado------------------------------------------------
                                        Dim horas, minutos As Integer
                                        horas = DateDiff(DateInterval.Hour, fila.Item("horaLlegada"), fechaHoraActual)
                                        minutos = DateDiff(DateInterval.Minute, fila.Item("horaLlegada"), fechaHoraActual)
                                        If horas > 0 Then
                                            minutos = minutos - (horas * 60)
                                        End If
                                        If horas > 24 Then
                                            horas = 24
                                            minutos = 60
                                        End If
                                        If horas > 1 Then
                                            CType(turno.Cells(3).Controls(5), Label).ForeColor = Color.Red
                                        End If
                                        CType(turno.Cells(3).Controls(5), Label).Text = horas.ToString("D2") + ":" + minutos.ToString("D2") + " Min"
                                        CType(turno.Cells(3).Controls(5), Label).Visible = True
                                    Case 3
                                        fechaHoraActual = DateTime.Now
                                        '---------------------------------------Calculo de tiempo en espera------------------------------------------------
                                        Dim horas, minutos As Integer
                                        horas = DateDiff(DateInterval.Hour, fila.Item("horaLlegada"), fechaHoraActual)
                                        minutos = DateDiff(DateInterval.Minute, fila.Item("horaLlegada"), fechaHoraActual)
                                        If horas > 0 Then
                                            minutos = minutos - (horas * 60)
                                        End If
                                        If horas > 24 Then
                                            horas = 24
                                            minutos = 60
                                        End If
                                        If horas > 1 Then
                                            CType(turno.Cells(3).Controls(5), Label).ForeColor = Color.Red
                                        End If
                                        CType(turno.Cells(3).Controls(5), Label).Text = horas.ToString("D2") + ":" + minutos.ToString("D2") + " Min"
                                        CType(turno.Cells(3).Controls(5), Label).Visible = True
                                        'Agrego la opcion de poder cobrar y facturar desde el estado en espera ------------------
                                        'CType(turno.Cells(3).Controls(1), LinkButton).Enabled = False
                                        CType(turno.Cells(6).Controls(1), LinkButton).Text = "$ " & fila.Item("costo")
                                        CType(turno.Cells(6).Controls(1), LinkButton).ForeColor = Color.SteelBlue
                                        CType(turno.Cells(6).Controls(1), LinkButton).Font.Bold = True
                                        CType(turno.Cells(7).Controls(1), ImageButton).Visible = True
                                        If fila.Item("pagoRealizado") = 0 Then
                                            CType(turno.Cells(6).Controls(1), LinkButton).ForeColor = Color.DarkRed
                                        Else
                                        End If
                                        If fila.Item("facturado") = 1 Then
                                            CType(turno.Cells(7).Controls(1), ImageButton).ImageUrl = "../Resource/impresion.png"
                                            CType(turno.Cells(7).Controls(1), ImageButton).Enabled = False
                                            'CType(turno.Cells(7).Controls(1), ImageButton).Visible = False


                                        End If
                                        '----------------------------------------------------------------------------------------

                                    Case 6
                                        CType(turno.Cells(3).Controls(1), LinkButton).Enabled = False
                                        CType(turno.Cells(6).Controls(1), LinkButton).Text = "$ " & fila.Item("costo")
                                        CType(turno.Cells(6).Controls(1), LinkButton).ForeColor = Color.SteelBlue
                                        CType(turno.Cells(6).Controls(1), LinkButton).Font.Bold = True
                                        CType(turno.Cells(7).Controls(1), ImageButton).Visible = True
                                        If fila.Item("pagoRealizado") = 0 Then
                                            CType(turno.Cells(6).Controls(1), LinkButton).ForeColor = Color.DarkRed
                                        Else
                                        End If
                                        If fila.Item("facturado") = 1 Then
                                            CType(turno.Cells(7).Controls(1), ImageButton).ImageUrl = "../Resource/impresion.png"
                                            CType(turno.Cells(7).Controls(1), ImageButton).Enabled = False
                                            'CType(turno.Cells(7).Controls(1), ImageButton).Visible = False


                                        End If

                                End Select
                                CType(turno.Cells(2).Controls(1), LinkButton).CssClass = "btn btn-block btn-lg " & fila.Item("colorfondorgb")
                                'turno.Cells(2).BackColor = Drawing.ColorTranslator.FromHtml(fila.Item("colorfondorgb"))
                                Exit For
                            End If
                        Next
                    Next
                End If
            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()

        End If
        'If Session("rol") <> "RE" Then
        '    For Each turno As DataGridItem In DGAgenda.Items
        '        If CType(turno.Cells(2).Controls(1), LinkButton).Text = "DISPONIBLE" Then
        '            CType(turno.Cells(2).Controls(1), LinkButton).Enabled = False
        '        End If
        '    Next
        'End If

    End Sub
    

    Sub llenaEtapas()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        '///////////////////////// Permisos de carga de agenda segun rol  \\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\\
        If Session("rol") = "RX" Then
            strsql = "select  * from [" & clsdatos.BaseDatos & "].[dbo].[CatEtapasAgenda] " & _
                         "where codigoEtapa in (3,5) and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1' "
        Else
            strsql = "select  * from [" & clsdatos.BaseDatos & "].[dbo].[CatEtapasAgenda] " & _
                          "where codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1' and codigoEtapa<>2 "
        End If
        '------------------------------------------------------------------------------------------------------------------
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            dgEtapas.DataSource = dt
            dgEtapas.DataBind()
            For Each dgRow As DataGridItem In dgEtapas.Items
                CType(dgRow.Cells(0).Controls(1), LinkButton).Text = "<i class='fa fa-2x fa-" & dgRow.Cells(3).Text & "' style='color:#000000; padding:6px 0'></i>"
                CType(dgRow.Cells(1).Controls(1), LinkButton).Text = dgRow.Cells(4).Text
                CType(dgRow.Cells(1).Controls(1), LinkButton).CssClass = "btn btn-block btn-lg " & dgRow.Cells(5).Text
            Next
        Else
            'CType(Master.FindControl("LblMensajeError"), Label).Text = clsdatos.MensajeError
            'CType(Master.FindControl("panelError"), Panel).Visible = True
        End If
    End Sub
    Function Funbloqueos(ByRef consultorio As String, ByRef fecha As Date)
        Dim strSQL As String
        Dim dt As New DataTable
        Dim a() As String
        Funbloqueos = False
        Dim clsDatos As New ClaseDatos
        strSQL = "select turnos as bloqueos from  [" & clsDatos.BaseDatos & "].[dbo].[AgBloqueos] " & _
                 "where codigoConsultorio='" & consultorio & "' and fecha='" & TxtAgFecha.Text & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                a = Split(dt.Rows(0).Item("bloqueos"), "|", -1)
                bloqueo = a
                Funbloqueos = True
            End If
        End If
    End Function
    Private Sub cargarConsultorios()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        strSQL = "SELECT CF.CodigoConsultorio, (u.nombre+' '+primerApellido+' '+segundoApellido) as DocCabecera " & _
               "FROM [" & clsDatos.BaseDatos & "].[dbo].[CnfConsultoriosUsuarios] as CF " & _
               "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
               "on C.codigoConsultorio=CF.CodigoConsultorio " & _
               "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U " & _
               "on C.idDoctorCabecera=U.codigoUsuario and C.CodigoEmpresa=U.CodigoEmpresa " & _
               "where CF.CodigoUsuarioEnlazado='" & Session("codigoUsuario") & "'  and CF.CodigoEmpresa='" & Session("codigoEmpresa") & "' "
        If funciones.llenadropdown(strSQL, DDAgendasDoctores) = 0 Then
            If DDAgendasDoctores.Items.Count > 1 Then
                DDAgendasDoctores.Visible = True
                DDAgendasDoctores.SelectedValue = Session("codigoConsultorio")
            End If
        End If

    End Sub
    Protected Sub TxtAgFecha_TextChanged(sender As Object, e As EventArgs) Handles TxtAgFecha.TextChanged
        'antonio
        'LnkAgFecha.Text = Format(CDate(TxtAgFecha.Text.ToUpper), "dd MMMM yyyy")
        '____________________________________________

        Session("FechaAgenda") = TxtAgFecha.Text
        PanelCritico.Visible = False
        cargaAgenda()
        llenaEtapas()
        contadorEstadosAgenda()
        PorcentajeDeMeta()
        UpdatePanel1.Update()

    End Sub

    Public Sub contadorEstadosAgenda()
        Dim strSQL As String
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Master.borrarListaPacientesCancelados()
        strSQL = "Select " & _
                "COUNT (CASE WHEN CodigoEtapa ='4' THEN CodigoEtapa end) as canceladas, " & _
                "COUNT(CASE WHEN CodigoEtapa='1' THEN CodigoEtapa END) as agendadas, " & _
                "COUNT (CASE WHEN CodigoEtapa='3' THEN CodigoEtapa END ) as enEspera,  " & _
                "COUNT (CASE WHEN CodigoEtapa='2'THEN CodigoEtapa END) as confirmadas, " & _
                "COUNT (CASE WHEN CodigoEtapa='5'THEN CodigoEtapa END ) as enEstudios, " & _
                "COUNT (CASE WHEN CodigoEtapa=6 THEN CodigoEtapa END) as terminadas,  " & _
                "COUNT (CASE WHEN CodigoEtapa='7' THEN CodigoEtapa END) as noAcudio " & _
                "from [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                "Where FechaAgenda='" & TxtAgFecha.Text & "' and CodigoEmpresa='" & Session("codigoEmpresa") & "' and codigoConsultorio='" & Session("codigoConsultorio") & "'"
        If clsdatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Master.contConfirmadas = dt.Rows(0).Item("confirmadas")
                Master.contEnEspera = dt.Rows(0).Item("enEspera")
                Master.contFinalizados = dt.Rows(0).Item("terminadas")
                Master.conEnEstudio = dt.Rows(0).Item("enEstudios")
                Master.contAgendados = dt.Rows(0).Item("agendadas")
            End If
        End If
        strSQL = "SELECT count(*) as canceladas  FROM[" & clsdatos.BaseDatos & "].[dbo].[AgAgendaCanceladas] as AC " & _
                    "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] as p on  AC.codigoPaciente=P.codigoPAciente " & _
                    "where FechaAgenda='" & TxtAgFecha.Text & "' and AC.CodigoEmpresa='" & Session("codigoEmpresa") & "' and codigoConsultorio='" & Session("codigoConsultorio") & "'"
        If clsdatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Master.contCanceladas = dt.Rows(0).Item("canceladas")
            End If
        End If
        strSQL = "SELECT (P.PApellido+' '+P.Sapellido+' '+P.nombres) as Paciente  FROM [" & clsdatos.BaseDatos & "].[dbo].[AgAgendaCanceladas] as AC " & _
                    "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatPacientes] as p on  AC.codigoPaciente=P.codigoPAciente " & _
                    "where FechaAgenda='" & TxtAgFecha.Text & "' and AC.CodigoEmpresa='" & Session("codigoEmpresa") & "' and codigoConsultorio='" & Session("codigoConsultorio") & "'"
        If clsdatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim listaCancelados As String = ""
                For fila As Integer = 0 To dt.Rows.Count - 1
                    listaCancelados = " - " + dt.Rows(fila).Item("Paciente") + "    "
                    Master.PacientesCancelados = listaCancelados
                Next

            End If
        End If
        strSQL = "SELECT  " & _
                   " '$ '+convert(varchar(17),cast(sum(CASE WHEN P.servicio='CONS' THEN costo ELSE 0 end) as money),1) as honorarios, " & _
                   " '$ '+convert(varchar(17),cast(sum(CASE WHEN P.servicio='OTROS' THEN costo ELSE 0 end) as money),1) as insumos " & _
                    "FROM [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                    "inner join [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] as P on p.folioConsulta=A.folioConsulta and A.codigoEmpresa=P.codigoEmpresa and P.status=1  " & _
                     " where  A.codigoConsultorio=" & Session("codigoConsultorio") & " and fechaAgenda='" & TxtAgFecha.Text & "' and A.codigoEmpresa=" & Session("codigoEmpresa") & " "

        If clsdatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Master.sumaHonorarios = " " + dt.Rows(0).Item("honorarios").ToString
                Master.sumaInsumos = " " + dt.Rows(0).Item("insumos").ToString
            Else
                Master.sumaHonorarios = "0"
                Master.sumaInsumos = "0"
            End If
        End If

        'Dim i As Integer = 0
        'If clsdatos.cargatabla(strSQL, dt) = 0 Then
        '    If dt.Rows.Count > 0 Then
        '        Dim datos As String
        '        Dim ingresos As String
        '        datos = "[['Médico','Ingreso']"
        '        i = i + 1
        '        For Each fila As DataRow In dt.Rows
        '            datos += ",['" + fila.Item("Medico").ToString + "'," + fila.Item("Importe").ToString + "]"
        '            i = i + 1
        '        Next
        '        Master._Ingresos = datos + "]"
        '    End If
        'End If

    End Sub

    Sub PorcentajeDeMeta()
        Dim strSql As String
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim calcula As Double
        Master._porcentaje = "0%"
        strSql = "Select count(*) as NumConsultasAlMes from [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                " where Codigoconsultorio='" & Session("codigoConsultorio") & "' and CodigoEmpresa ='" & Session("codigoEmpresa") & "' and FechaAgenda between '01/06/16' and '30/06/16' and CodigoEtapa='6'"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                calcula = dt.Rows(0).Item("NumConsultasalMes") / 250
                Master._porcentaje = FormatPercent(calcula)
                Master.porcentajeMedicoMeta = "250"
                Master.porcentajeCompletadoMedico = Master._porcentaje
                Master.porcentajeBarra = Master._porcentaje
            End If
        End If
    End Sub

    Protected Sub DDAgendasDoctores_SelectedIndexChanged(sender As Object, e As EventArgs) Handles DDAgendasDoctores.SelectedIndexChanged
        Session("codigoConsultorio") = DDAgendasDoctores.SelectedValue
        PanelCritico.Visible = False
        cargaAgenda()
        llenaEtapas()
        contadorEstadosAgenda()
        PorcentajeDeMeta()
        UpdatePanel1.Update()

    End Sub
    Protected Sub BtnReAgendar_Click(sender As Object, e As EventArgs)
    End Sub

    Protected Sub lnkpaciente_Click(sender As Object, e As EventArgs)
    End Sub

    Private Sub DGAgenda_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles DGAgenda.ItemCommand
        If e.CommandName.ToString = "BtnReAgendar" Then
            Session("codigoPaciente") = CType(e.Item.Cells(2).Controls(3), Label).Text
            Session("nombrePaciente") = CType(e.Item.Cells(2).Controls(1), LinkButton).Text
            Response.Redirect("ReAgendarPaciente.aspx")
            Exit Sub
        End If
        If e.CommandName.ToString = "LnkCosto" Then
            Session("folioConsulta") = CType(e.Item.Cells(5).Controls(1), Label).Text
            Session("codigoPaciente") = CType(e.Item.Cells(2).Controls(3), Label).Text
            Session("nombrePaciente") = CType(e.Item.Cells(2).Controls(1), LinkButton).Text
            Response.Redirect("pagos.aspx")
            Exit Sub

        End If
        '*******CLICK BOTÓN FACTURAR

        If e.CommandName.ToString = "BtnFacturar" Then
            'Cargar datos y abrir Modal
            bandera.Value = 0
            div_input_razon_social.Visible = False
            Session("folioConsulta") = CType(e.Item.Cells(5).Controls(1), Label).Text
            folio_consulta.Value = CType(e.Item.Cells(5).Controls(1), Label).Text
            fconsulta.Value = CType(e.Item.Cells(5).Controls(1), Label).Text
            Session("codigoPaciente") = CType(e.Item.Cells(2).Controls(3), Label).Text
            Session("nombrePaciente") = CType(e.Item.Cells(2).Controls(1), LinkButton).Text
            CargarDatosFacturacion(CType(e.Item.Cells(2).Controls(3), Label).Text)
            LlenarComboRazonSocial(CType(e.Item.Cells(2).Controls(3), Label).Text)
            buscarConsultasAfacturar(CType(e.Item.Cells(2).Controls(3), Label).Text, CType(e.Item.Cells(5).Controls(1), Label).Text)
            
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#datos_facturacion').modal('show');</script>", False)

            Exit Sub

        End If

        If e.CommandName.ToString = "BtnEstado" Then
            Session("folioConsulta") = CType(e.Item.Cells(5).Controls(1), Label).Text
            numEtapa.Value = CType(e.Item.Cells(3).Controls(3), Label).Text
            Timer1.Enabled = False
            mpupetapas.Show()
            Timer1.Enabled = True
            Exit Sub
        End If
        If e.CommandName.ToString = "LnkPaciente" Then
            If CType(e.Item.Cells(2).Controls(1), LinkButton).Text = "DISPONIBLE" Then
                Session.Add("numeroTurno", e.Item.Cells(0).Text)
                Session("modalidad") = "AG"
                Response.Redirect("BuscarPaciente.aspx")
            Else
                If Session("rol") = "RE" Or Session("rol") = "DR" Then
                    If CType(e.Item.Cells(3).Controls(3), Label).Text > "1" Then
                        If Session("rol") = "DR" Or Session("rol") = "RE" Then
                            Session.Add("folioConsulta", CType(e.Item.Cells(5).Controls(1), Label).Text)
                            Response.Redirect("ConsultaEx.aspx")
                        Else
                            Session("modalidad") = "AG"
                            Response.Redirect("DatosPaciente.aspx?codigoPaciente=" + CType(e.Item.Cells(2).Controls(3), Label).Text)
                        End If
                    Else
                        Session("modalidad") = "AG"
                        Response.Redirect("DatosPaciente.aspx?codigoPaciente=" + CType(e.Item.Cells(2).Controls(3), Label).Text)
                    End If
                End If
                If Session("rol") = "RX" Then
                    If CType(e.Item.Cells(3).Controls(3), Label).Text > "1" Then
                        Session.Add("folioConsulta", CType(e.Item.Cells(5).Controls(1), Label).Text)
                        Response.Redirect("EstudiosRX.aspx")

                    End If
                End If
            End If
        End If
    End Sub


    Sub dgEtapas_ItemCommand(source As Object, e As DataGridCommandEventArgs) Handles dgEtapas.ItemCommand
        Dim strSql As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "AGENDADO" Then
            If numEtapa.Value <> "1" Then
                strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='1',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                        ",FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                        "where folioConsulta='" & Session("folioConsulta") & "'"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                cargaAgenda()
                UpdatePanel1.Update()

            End If
            Exit Sub
        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "CONFIRMADO" Then
            If numEtapa.Value <> "2" Then
                strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='2',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                         ",HoraLlegada=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20),FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20) " & _
                         "where folioConsulta='" & Session("folioConsulta") & "'"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                cargaAgenda()
                UpdatePanel1.Update()

            End If
            Exit Sub
        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "EN ESPERA" Then
            If numEtapa.Value <> "3" Then
                strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='3',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                        ",HoraLlegada=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd H:mm:ss") & "',20),FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                        "where folioConsulta='" & Session("folioConsulta") & "'"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo
                GuardarIngresoCajas()
                cargaAgenda()
                UpdatePanel1.Update()
            End If
            Exit Sub
        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "CANCELADO" Then
            If numEtapa.Value <> "4" Then
                'strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='4',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                '           ",FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                '           "where folioConsulta='" & Session("folioConsulta") & "'"
                strSql = "insert into [" & clsDatos.BaseDatos & "].[dbo].[AgAgendaCanceladas] " & _
                        "select * from [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] where folioConsulta='" & Session("folioConsulta") & "' " & _
                        "delete from [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] where folioConsulta='" & Session("folioConsulta") & "' "
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                cargaAgenda()
                UpdatePanel1.Update()
            End If
            Exit Sub
        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "EN ESTUDIO GAB." Then
            If numEtapa.Value <> "5" Then
                strSql = "SELECT consecutivo  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] " & _
                                    " where codigoFoliador='2'  and codigoEmpresa='" & Session("codigoEmpresa") & "'"
                If clsDatos.cargatabla(strSql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        Dim IncrementoFoliador As Integer
                        IncrementoFoliador = dt.Rows(0).Item("consecutivo") + 1
                        strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[CatFoliadoresDet] set consecutivo='" & IncrementoFoliador & "' " & _
                                 "where codigoFoliador='2' and codigoEmpresa='" & Session("codigoEmpresa") & "' " & _
                        "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='5',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                         ",FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                         "where folioConsulta='" & Session("folioConsulta") & "'" & _
                         "insert into  [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] " & _
                         "(folioEstudio,folioConsulta,tipoSolicitud,CodigoEstudioGabinete,CodigoPaciente," & _
                         "FechaSolicitud,FechaAutorizacion,UsuarioSolicita,usuarioAutoriza,CodigoEstudio,status,codigoEmpresa,codigoUsuario," & _
                         "FechaActualizacion) " & _
                         "values('" & IncrementoFoliador & "','" & Session("folioConsulta") & "','1','1','" & Session("codigoPaciente") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & _
                         "convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)," & Session("codigoUsuario") & "," & Session("codigoUsuario") & ",'1','1'," & Session("codigoEmpresa") & "," & Session("codigoUsuario") & ",convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20))"
                        clsDatos.cargaComando(strSql)
                        If clsDatos.ejecutar() = -1 Then
                            LblMensajeCritico.Text = clsDatos.MensajeError
                            PanelCritico.Visible = True
                            PanelCritico.Focus()
                            Exit Sub
                        End If
                        cargaAgenda()
                        UpdatePanel1.Update()
                    End If
                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If

            End If
            Exit Sub

        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "FINALIZADO" Then
            If numEtapa.Value <> "6" Then
                strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='6',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                         ",fechaFinalizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20),FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                         "where folioConsulta='" & Session("folioConsulta") & "'"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                GuardarIngresoCajas()
                cargaAgenda()
                UpdatePanel1.Update()
            End If
            Exit Sub
        End If
        If CType(e.Item.Cells(1).Controls(1), LinkButton).Text = "NO ACUDIO" Then
            If numEtapa.Value <> "7" Then
                strSql = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set CodigoEtapa='7',CodigoUsuario='" & Session("codigoUsuario") & "' " & _
                        ",FechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20) " & _
                        "where folioConsulta='" & hffolioconsulta.Value & "'"
                clsDatos.cargaComando(strSql)
                If clsDatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                    Exit Sub
                End If
                cargaAgenda()
                UpdatePanel1.Update()
            End If
            Exit Sub
        End If
    End Sub

    Protected Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        cargaAgenda()
        UpdatePanel1.Update()
        PanelCritico.Visible = False
    End Sub


    '--------------------------  DRAG and DROP Agenda, arrastrar pacientes a distinto horario  ------------------
    Private Sub BtnActualizar_Click(sender As Object, e As EventArgs) Handles BtnActualizar.Click
        Dim strSQL As String
        Dim dt, dt2 As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim rowIni, rowFin As String
        rowIni = rowInicia.Value
        rowIni = rowIni.Substring(41) + 1
        rowFin = rowFinal.Value
        rowFin = rowFin.Substring(41) + 1
        strSQL = "SELECT *  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                 "where numeroTurno='" & rowIni & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEtapa in (6,4) and status='1' and fechaAgenda='" & Session("fechaAgenda") & "'  "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Exit Sub
            End If
        End If
        strSQL = "SELECT numeroTurno FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] " & _
                 "where numeroturno='" & rowFin & "' and codigoConsultorio='" & Session("codigoConsultorio") & "' and fechaAgenda='" & Session("fechaAgenda") & "' and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1'  and codigoEtapa<> '4' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Exit Sub
            End If
            If Funbloqueos(Session("codigoConsultorio"), Session("fechaAgenda")) Then
                For Each RowBloqueo As String In bloqueo
                    If rowFin = RowBloqueo Then
                        Exit Sub
                    End If
                Next
            End If
            strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] set codigoUsuario='" & Session("codigoUsuario") & "',numeroTurno='" & rowFin & "', fechaActualizacion=convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "',20)  " & _
                     " where numeroturno='" & rowIni & "' and codigoConsultorio='" & Session("codigoConsultorio") & "' and fechaAgenda='" & Session("fechaAgenda") & "'  and codigoEmpresa='" & Session("codigoEmpresa") & "' and status='1' "
            clsDatos.cargaComando(strSQL)
            If clsDatos.ejecutar = -1 Then
                LblMensajeCritico.Text = clsDatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
        cargaAgenda()
        UpdatePanel1.Update()
    End Sub

    '**********************Facturas********************************************
    Sub CargarDatosFacturacion(CodigoPaciente)
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        input_razon_social.Value = ""
        rfc.Value = ""
        correo.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""
        codigo_paciente.Value = CodigoPaciente
        strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                input_razon_social.Value = dt.Rows(0).Item("razonsocial")
                rfc.Value = dt.Rows(0).Item("rfc")
                correo.Value = dt.Rows(0).Item("email")
                direccion.Value = dt.Rows(0).Item("direccion")
                cp.Value = dt.Rows(0).Item("cp")
                ciudad.Value = dt.Rows(0).Item("ciudad")
                estado.Value = dt.Rows(0).Item("estado")
                pais.Value = dt.Rows(0).Item("pais")
                codigo_paciente.Value = dt.Rows(0).Item("CodigoPaciente")
                id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
            Else
                pais.Value = "MÉXICO"
                estado.Value = "YUCATÁN"
                ciudad.Value = "MÉRIDA"
            End If
        End If
    End Sub

    Sub LlenarComboRazonSocial(CodigoPaciente)
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        codigo_paciente.Value = CodigoPaciente
        strSQL = "SELECT idRazonSocial, razonsocial FROM [" & clsDatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE CodigoPaciente =" + CodigoPaciente + " ORDER BY idRazonSocial"
        If funciones.llenadropdown(strSQL, select_razon_social) = -1 Then
            'Errorbandera_facturaCredito
            div_input_razon_social.Visible = True
        Else
            'Se realizó la función, verificar si hay datos
            If select_razon_social.Items.Count = 0 Then
                div_input_razon_social.Visible = True
                div_select_razon_social.Visible = False
                
                'Habilitar bandera para dar de alta el nuevo
                bandera.Value = 1
            Else
                div_input_razon_social.Visible = False
                div_select_razon_social.Visible = True
                select_razon_social.Items.Add("NUEVO")
                bandera.Value = 0
            End If


        End If
    End Sub

    Sub CambioSelectRazonSocial()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'Limpiar Campos
        input_razon_social.Value = ""
        rfc.Value = ""
        correo.Value = ""
        direccion.Value = ""
        cp.Value = ""
        ciudad.Value = ""
        estado.Value = ""
        pais.Value = ""
        If select_razon_social.SelectedValue = "NUEVO" Then
            'Agregar Nuevo
            div_input_razon_social.Visible = True
            div_select_razon_social.Visible = False
            'Habilitar bandera para dar de alta el nuevo
            pais.Value = "MÉXICO"
            estado.Value = "YUCATÁN"
            ciudad.Value = "MÉRIDA"
            bandera.Value = 1
        Else
            strsql = "SELECT * FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] WHERE idRazonSocial =" + select_razon_social.SelectedValue
            If clsdatos.cargatabla(strsql, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    input_razon_social.Value = dt.Rows(0).Item("razonsocial")
                    rfc.Value = dt.Rows(0).Item("rfc")
                    correo.Value = dt.Rows(0).Item("email")
                    direccion.Value = dt.Rows(0).Item("direccion")
                    cp.Value = dt.Rows(0).Item("cp")
                    ciudad.Value = dt.Rows(0).Item("ciudad")
                    estado.Value = dt.Rows(0).Item("estado")
                    pais.Value = dt.Rows(0).Item("pais")
                    id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
                End If
            End If
        End If
    End Sub

    Sub Facturar()


        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If bandera.Value = 1 Then
            'Dar de alta la razón social
            strsql = "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] (CodigoPaciente, razonSocial, rfc, email, direccion, cp, pais, estado, ciudad )VALUES('" & codigo_paciente.Value & "', '" & input_razon_social.Value & "', '" & rfc.Value & "', '" & correo.Value & "', '" & direccion.Value & "', '" & cp.Value & "', '" & pais.Value & "', '" & estado.Value & "', '" & ciudad.Value & "'  )"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                strsql = "SELECT idRazonSocial, razonsocial FROM [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] " & _
                          "WHERE CodigoPaciente = '" & codigo_paciente.Value & "' and razonSocial='" & input_razon_social.Value & "' and rfc='" & rfc.Value & "'  and email='" & correo.Value & "' "
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    id_razon_social.Value = dt.Rows(0).Item("idRazonSocial")
                    Timbrado()
                    cargaAgenda()
                    UpdatePanel1.Update()
                End If

            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        Else
            'Actualizar la razón social
            strsql = "UPDATE [" & clsdatos.BaseDatos & "].[dbo].[CatPacientesDatosFacturacion] SET razonSocial='" & input_razon_social.Value & "', rfc='" & rfc.Value & "', email='" & correo.Value & "', direccion='" & direccion.Value & "', cp='" & cp.Value & "', pais='" & pais.Value & "', estado='" & estado.Value & "', ciudad= '" & ciudad.Value & "' WHERE idRazonSocial = " + id_razon_social.Value
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                Timbrado()
                'LblMensajeAviso.Text = "La factura se ha cancelado"
                'PanelAvisos.Visible = True
                'PanelAvisos.Focus()
                cargaAgenda()
                UpdatePanel1.Update()
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
    End Sub


    Sub enviarFac(ByRef folioFac As String)
        Dim folioFactura As String = folioFac
        'Dim estadoFac As String = estado_factura.Value
        Dim Path As String
        Dim mail As New MailMessage
        'If estadoFac = "Vigente" Then
        Path = Server.MapPath("/Facturas/" + folioFactura)
        'Else
        'Path = Server.MapPath("/Facturas/Canceladas/" + folioFactura)
        'End If

        mail.From = New MailAddress("facturas@stargrupoortopedico.com")

        mail.To.Add(correo.Value)

        mail.Subject = "Factura de servicios de Ortopedia Star"
        mail.Body = "Facturas"
        Try
            Dim FilePath1 As String = Path + ".xml"
            Dim FilePath2 As String = Path + ".pdf"

            'el archivo se adjunta indicándole la ruta 
            mail.Attachments.Add(New Attachment(FilePath1))
            mail.Attachments.Add(New Attachment(FilePath2))

            Dim mailClient As New SmtpClient()

            Dim basicAuthenticationInfo As New NetworkCredential("facturas@stargrupoortopedico.com", "fac2017*")

            mailClient.Host = "mail.stargrupoortopedico.com"

            mailClient.UseDefaultCredentials = True
            mailClient.Credentials = basicAuthenticationInfo
            mailClient.Port = 587

            mailClient.Send(mail)
            LblMensajeAviso.Text = "Se ha enviado los archivos de la factura (xml y pdf)"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
        Catch ex As Exception
            LblMensajeCritico.Text = ex.Message
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End Try

    End Sub


    '********* TIMBRADO

    Function Timbrado()
        Dim banderaError As Boolean = False
        ' Dim fechapdf As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim folio As String = ""
        Dim serie As String = ""
        Dim cuenta As Int32 = 0
        Dim contador As Int32 = 0


        strsql = " SELECT CAST(consecutivo + 1 AS varchar) as consecutivo, serie  from ConsecutivoFac where CodigoCF='1'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                folio = dt.Rows(0).Item("consecutivo")
                serie = dt.Rows(0).Item("serie")
            Else
                LblMensajeCritico.Text = "Error en el consecutivo de folio"
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If


        '********************** FACTURACION ******************************

        '*************  -DATOS DEL EMISOR- ******************
        Dim emisor As New Emisor()
        emisor.rfc = "OST141127IEA"
        emisor.nombre = "ORTOPEDIA STAR, S.C.P."
        emisor.domicilioFiscal = New DomicilioFiscal
        emisor.domicilioFiscal.calle = "26"
        emisor.domicilioFiscal.noExterior = "299"
        emisor.domicilioFiscal.noInterior = "928"
        emisor.domicilioFiscal.colonia = "FRACC. ALTABRISA"
        emisor.domicilioFiscal.localidad = "MÉRIDA"
        emisor.domicilioFiscal.municipio = "MÉRIDA"
        emisor.domicilioFiscal.estado = "YUCATÁN"
        emisor.domicilioFiscal.pais = "MEXICO"
        emisor.domicilioFiscal.codigoPostal = "97133"
        emisor.regimenFiscal = New RegimenFiscal
        emisor.regimenFiscal.regimen = "REGIMEN GENERAL DE LEY PERSONAS MORALES"

        '************* - DATOS DEL RECEPTOR- ****************
        Dim receptor As New Receptor()
        receptor.rfc = rfc.Value
        receptor.nombre = input_razon_social.Value
        receptor.domicilio = New Domicilio()
        receptor.domicilio.calle = direccion.Value
        receptor.domicilio.localidad = ciudad.Value
        receptor.domicilio.codigoPostal = cp.Value
        receptor.domicilio.municipio = ciudad.Value
        receptor.domicilio.estado = estado.Value
        receptor.domicilio.pais = pais.Value

        '************** -DATOS DEL COMPROBANTE- ************
        Dim hoy As DateTime = DateTime.Now
        Dim comp As New Comprobante()
        Dim total, formaPago As String
        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim listafolios, listaFoliosSQL As String


        formaPago = ""
        total = ""
        '++++++++++++++++++++++++  Validar si es del grid +++++++++++++++++++++++++++++++ 
        cargardatosFac(formaPago, total)
        If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
            total = dtTotal.Rows(0).Item("costoTotal")
            variasFacturas = True
        Else
            variasFacturas = False
        End If

        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
        comp.fecha = String.Format("{0}T{1}", hoy.ToString("yyyy-MM-dd"), hoy.ToString("HH:mm:00"))
        fechapdf = comp.fecha
        comp.folio = folio
        comp.serie = serie
        comp.formaDePago = "PAGO EN UNA SOLA EXHIBICION"
        comp.subTotal = total
        comp.total = total
        comp.tipoDeComprobante = "ingreso"
        comp.moneda = "MXP"
        comp.tipoCambio = "1.0"
        comp.metodoDePago = formaPago
        comp.lugarExpedicion = "MÉRIDA,YUCATÁN"

        '************ -CONCEPTO- *******************

        comp.conceptos = New List(Of Concepto)()
        Do While contador = cuenta

            If variasFacturas = True Then

                For Each fila As DataRow In dtConceptos.Rows
                    Dim concepto1 As New Concepto()
                    concepto1.cantidad = fila.Item("cantidad")
                    concepto1.unidad = "No aplica"
                    concepto1.noIdentificacion = "noIdentificacion"
                    concepto1.descripcion = fila.Item("descripcion")
                    concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
                    concepto1.valorUnitario = fila.Item("costo")
                    comp.conceptos.Add(concepto1)

                Next

            Else
                strsql = " SELECT codigoConcepto,descripcion,convert(varchar,convert(decimal(8,2),costo)) as costo,cantidad FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
               "WHERE folioConsulta='" & fconsulta.Value & "' AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1"
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    For Each fila As DataRow In dt.Rows
                        Dim concepto1 As New Concepto()
                        concepto1.cantidad = fila.Item("cantidad")
                        concepto1.unidad = "No aplica"
                        concepto1.noIdentificacion = "noIdentificacion"
                        concepto1.descripcion = fila.Item("descripcion")
                        concepto1.importe = fila.Item("costo") * fila.Item("cantidad") 'Multiplicar con la cantidad
                        concepto1.valorUnitario = fila.Item("costo")
                        comp.conceptos.Add(concepto1)

                    Next
                Else
                    'Error en la consulta
                End If
            End If

            contador = contador + 1
        Loop

        '*************** - CANTIDAD EN LETRAS- *****************
        Dim funciones2 As New FuncionesGenerales

        Dim tletras As String = " "



        tletras = funciones2.monto(Convert.ToDouble(comp.total))

        '*************** - IMPUESTOS- *****************
        Dim iva As New Traslado()
        iva.impuesto = "IVA"
        iva.importe = "0.0000"
        iva.tasa = "0"

        Dim impuestos As New Impuestos()
        impuestos.traslados = New List(Of Traslado)()
        impuestos.totalImpuestosTrasladados = "0.0000"
        impuestos.totalImpuestosRetenidos = "0.0000"
        impuestos.traslados.Add(iva)




        '************************** TIMBRADO Y RESPUESTA****************************
        'Envio al Web Service

        comp.emisor = emisor
        comp.receptor = receptor
        comp.impuestos = impuestos

        Dim Xml
        Dim cer
        Try
            cer = New X509Certificate2(Server.MapPath("/OST14112729122014.pfx"), "orto2014", X509KeyStorageFlags.MachineKeySet)
            Xml = CFDIv32.Serializar(comp, False)

        Catch ex As Exception
            banderaError = True
            LblMensajeCritico.Text = "Error en el sellado del XML para timbrar favor de llamar a informática " + vbNewLine + ex.Message.ToString
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End Try

        If banderaError = False Then
            Dim cadenaOriginal = CFDIv32.CadenaOriginalSellado(Xml)

            Try
                Dim ar = cer.GetSerialNumber
                Array.Reverse(ar)
                comp.noCertificado = Encoding.UTF8.GetString(ar)
                comp.certificado = Convert.ToBase64String(cer.RawData) 'Certificado igual para la cancelación del cfdi
                comp.sello = CFDIv32.Sello(cadenaOriginal, cer)

                Xml = CFDIv32.Serializar(comp, True)
                CFDIv32.Validar(Xml, True)
                File.WriteAllText(Server.MapPath("/mandatorioSGO.xml"), Xml)
            Catch ex As Exception
                banderaError = True
                LblMensajeCritico.Text = "Error en el XML para timbrar favor de llamar a informática" + ex.Message.ToString
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End Try

            Dim uuid, sellocfd, nocertificadoSat, selloSat, FechaTimbrado As String
            If banderaError = False Then
                Try

                    Dim timbrarFac As New AgeMED.WSFEL.WSTFDClient
                    Dim timbreFac As New RespuestaTFD



                    timbreFac = timbrarFac.TimbrarCFDI("OST141127IEA", "kBMEtFvuTrr#", Xml, "agemed_" + comp.folio.ToString)


                    Xml = timbreFac.XMLResultado

                    If timbreFac.MensajeError.Trim = "" And timbreFac.OperacionExitosa = True Then
                        CFDIv32.Validar(Xml, True)
                        uuid = timbreFac.Timbre.UUID
                        sellocfd = timbreFac.Timbre.SelloCFD
                        nocertificadoSat = timbreFac.Timbre.NumeroCertificadoSAT
                        selloSat = timbreFac.Timbre.SelloSAT
                        FechaTimbrado = timbreFac.Timbre.FechaTimbrado


                        '''''' Guarda la factura
                        Dim foliosConsulta As String
                        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++
                        If variasFacturas = True Then
                            foliosConsulta = listaFoliosSQL
                        Else

                            foliosConsulta = "'" + fconsulta.Value + "'"
                        End If

                        '++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

                        strsql = " update [" & clsdatos.BaseDatos & "].[dbo].[IngresosCaja] set facturado=1 " & _
                                " where folioconsulta in (" & listafolios & ")  "
                        strsql += "INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[DetFacturas] (FolioFactura,Serie,rfc,TipoFactura,"
                        strsql += vbNewLine & " OrigenFactura,subtotal,ImporteIva,Total ,"
                        strsql += vbNewLine & " IdRazonSocial,CodigoPaciente,Status,fechaFactura,xml,uuid,numerocertificadosat,sellocfd,sellosat,fechatimbrado,estado,IdEmpleado,codigoEmpresa,FolioConsulta,tipoDePago,conciliado,Saldo) VALUES  "
                        strsql += vbNewLine & " ('" & folio & "','" & serie & "','" & rfc.Value & "', '1', '1','" & comp.subTotal & "',"
                        strsql += vbNewLine & " '" & iva.importe & "','" & comp.total & "','" & id_razon_social.Value & "','" & codigo_paciente.Value & "','1', getdate() , '" & Xml & "', '" & uuid & "',"
                        strsql += vbNewLine & " '" & nocertificadoSat & "','" & sellocfd & "','" & selloSat & "','" & FechaTimbrado & "','" & timbreFac.Timbre.Estado & "','" & Session("codigoUsuario") & "','" & Session("codigoEmpresa") & "','" & foliosConsulta & "'," & formaPago & ",'0','" & comp.total & "')"
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() <> 0 Then
                            LblMensajeAdvertencia.Text = "Error al guardar SQL: " + clsdatos.MensajeError
                            PanelAdvertencia.Visible = True
                        End If

                       

                        '''''' Actualiza el consecutivo 

                        strsql = "update ConsecutivoFac set consecutivo = consecutivo + 1 where CodigoCF in (1,2) "
                        clsdatos.cargaComando(strsql)
                        clsdatos.ejecutar()


                    Else
                        folio = ""
                        banderaError = True
                        LblMensajeCritico.Text = "No cerrar; Error en el XML de timbrado favor de llamar a informática; " + timbreFac.MensajeErrorDetallado.ToString.Trim
                        PanelCritico.Visible = True
                        PanelCritico.Focus()
                    End If
                Catch ex As Exception
                    banderaError = True
                    LblMensajeCritico.Text = "Error en el timbrado favor de llamar a informática " + ex.Message.ToString
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End Try
            End If

            Dim nameFile As String = ""
            If banderaError = False Then
                Try
                    Dim doc As XmlDocument = New XmlDocument()
                    doc.LoadXml(Xml)
                    nameFile = (Server.MapPath("/Facturas/" + comp.folio.ToString + ".xml"))
                    doc.Save(nameFile)
                    CFDIv32.Validar(Xml, True)
                Catch ex As Exception
                    banderaError = True
                    LblMensajeCritico.Text = "Eror en el guradado del XML favor de llamar a informática"
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End Try
            End If

            If banderaError = False Then
                Try
                    Dim cadenaOriComplemento As String = CFDIv32.CadenaOriginalImpresa(Xml)

                    Dim QRCodeEncoder As New QRCodeEncoder
                    QRCodeEncoder.QRCodeScale = "3"
                    QRCodeEncoder.QRCodeVersion = "7"
                    QRCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M

                    Dim imagen As Bitmap
                    Dim auxcad = Split(comp.total, ".")
                    Dim enteros As String = auxcad(0).ToString.Trim.PadLeft(10, "0")
                    Dim decimales As String = auxcad(1).ToString.Trim.PadRight(6, "0")
                    Dim datos As String = "?re=" + emisor.rfc.Trim + "&rr=" + receptor.rfc.Trim + "&tt=" + enteros + "." + decimales + "&id=" + uuid
                    imagen = QRCodeEncoder.Encode(datos)
                    imagen.Save(Server.MapPath("/Facturas/" + comp.folio.ToString + ".jpeg"), Imaging.ImageFormat.Jpeg)

                    imprimirFacturaCfdi(listafolios.ToString, comp.folio.ToString, comp.serie.ToString, receptor.domicilio.calle.ToString, receptor.domicilio.municipio, receptor.domicilio.codigoPostal, receptor.domicilio.localidad, receptor.domicilio.estado, comp.total, tletras, comp.formaDePago, comp.metodoDePago, comp.subTotal, iva.importe, FechaTimbrado, comp.noCertificado.ToString, nocertificadoSat, uuid, cadenaOriComplemento, selloSat, sellocfd)


                   
                    enviarFac(comp.folio.ToString)
                    'aqui modifico GERARDO
                    cargaAgenda()
                    UpdatePanel1.Update()


                  


                    'VisualizarFac(comp.folio.ToString)
                   


                    'LblMensajeAviso.Text = "Factura enviada "
                    'PanelAvisos.Visible = True
                    'PanelAvisos.Focus()
                    'Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & comp.folio.ToString & ".pdf');</script>")


                Catch ex As Exception
                    LblMensajeAdvertencia.Text = "Error en la impresión del PDF llamar a informática " + ex.Message.ToString
                    PanelAdvertencia.Visible = True
                    'PanelAdvertencia.Focus()

                End Try

            End If
        End If

        Return banderaError

    End Function

    Sub imprimirFacturaCfdi(ByVal lfolioconsulta As String, ByVal queimprimo As String, ByVal serie As String, ByVal domicilio As String, ByVal municipio As String, ByVal cp As String, ByVal localidad As String, ByVal estado As String, ByVal totalfac As String, ByVal totalletras As String, ByVal formadepago As String, ByVal metodopago As String, ByVal subtotalfac As String, ByVal iva As String, ByVal fechacer As String, ByVal ceremisor As String, ByVal cersat As String, ByVal uuid As String, ByVal cadoriginal As String, ByVal sellosat As String, ByVal selloCFD As String)
        Dim mireporte As New ReportDocument
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String


        mireporte.Load(Server.MapPath("/Facturas/Reporte/FacturaCFDI.rpt"))


        '++++++++++++++++++++ Detalles de la Factura

        Dim columna As New DataColumn("cantidad")
        dt.Columns.Add(columna)
        Dim col2 As New DataColumn("descripcion")
        dt.Columns.Add(col2)
        Dim col3 As New DataColumn("importe")
        dt.Columns.Add(col3)
        Dim col4 As New DataColumn("precio")
        dt.Columns.Add(col4)
        dt.Columns.Add(New DataColumn("img", GetType(Byte())))


        Dim fs As FileStream = New FileStream(Server.MapPath("/Facturas/" + queimprimo.ToString + ".jpeg"), FileMode.Open)
        Dim br As BinaryReader = New BinaryReader(fs)
        Dim imagen(CInt(fs.Length)) As Byte

        br.Read(imagen, 0, CInt(fs.Length))
        br.Close()
        fs.Close()

        Dim variasFacturas As Boolean
        Dim dtConceptos, dtTotal As DataTable
        Dim listafolios, listaFoliosSQL As String
        Dim contador As Int32 = 0
        Dim cuenta As Int32 = 0

        If consultasAfacturar(dtConceptos, dtTotal, listafolios, listaFoliosSQL) Then
            variasFacturas = True
        Else
            variasFacturas = False
        End If

        Do While contador = cuenta

            If variasFacturas = True Then

                For Each fila As DataRow In dtConceptos.Rows
                    Dim concepto1 As New Concepto()
                    Dim nFila As DataRow
                    nFila = dt.NewRow
                    nFila(0) = fila.Item("cantidad")
                    nFila(1) = fila.Item("descripcion")
                    nFila(2) = fila.Item("costo")
                    nFila(3) = fila.Item("costo") * fila.Item("cantidad")
                    nFila(4) = imagen
                    dt.Rows.Add(nFila)
                Next

            End If

            contador = contador + 1
        Loop

        mireporte.SetDataSource(dt.DefaultView)


        ' -------------- datos folio/serie factura
        Mivar.Value = serie + " " + queimprimo
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("recibo").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '--------------datos facturacion
        Mivar.Value = input_razon_social.Value
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("nombre").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = domicilio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("domicilio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        'Mivar.Value = colonia
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("colonia").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        Mivar.Value = municipio
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("municipio").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cp
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cp").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = localidad
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("localidad").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = estado
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("estado").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = pais.Value
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("paisfac").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = rfc.Value
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("rfc").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Session("nombrePaciente")
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = fechapdf
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fecha").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        strsql = "  select descripcion " & _
                  " from  CatFormasDePago" & _
                  " where codigoFormaPago = '" & metodopago & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Mivar.Value = dt.Rows(0).Item("descripcion")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("formapago").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
            End If

        End If


        strsql = "  select referencia " & _
                 " from  IngresosPagosCajas" & _
                 " where folioConsulta =  " & lfolioconsulta & " "

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("referencia") = "0" Then
                    Mivar.Value = ""
                    rpDatos.Add(Mivar)
                    mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
                    rpDatos.Clear()
                Else
                    Mivar.Value = dt.Rows(0).Item("referencia")
                    rpDatos.Add(Mivar)
                    mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
                    rpDatos.Clear()
                End If
            Else
                Mivar.Value = ""
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("creferencia").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            End If

        End If

        '-----Observaciones de la Factura

        Mivar.Value = observacionesf.Value
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("observacionesfac").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        Mivar.Value = metodopago
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ClaveMP").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '-------------------------importes
        Mivar.Value = subtotalfac
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("importe").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = iva
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("iva").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = totalfac
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("total").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = totalletras
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cantidadletras").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        Mivar.Value = fechacer
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fechacer").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = ceremisor
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ceremisor").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cersat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cersat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = uuid
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("uuid").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = cadoriginal
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("cadenaoriginal").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = sellosat
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("sellosat").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = selloCFD
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("selloCFD").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        '--------------------------------------------------


        Try
            Dim nomArchivo As String = queimprimo.ToString.Trim
            nomArchivo.Replace(" ", "")
            Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
            Dim o As CrystalDecisions.Shared.ExportOptions
            o = New CrystalDecisions.Shared.ExportOptions
            o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
            o.ExportDestinationOptions = filedest.Clone
            mireporte.Export(o)
            filedest = Nothing
            o = Nothing

            'Dim Nombre As String
            'Nombre = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"

            'Response.Clear()
            'Response.ContentType = "application/pdf"
            'Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
            'Response.WriteFile(Nombre)
            'Response.Flush()
            'Response.Close()

            mireporte.Close()
            Response.Write("<script type='text/javascript'>detailedresults=window.open('Impfactura.aspx?doc=" & nomArchivo & ".pdf');</script>")
        Catch ex As Exception
            LblMensajeCritico.Text = "Error 1: " + clsdatos.MensajeError
            PanelCritico.Visible = True
        End Try

        'visor_reporte.DataBind()

        'Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
        'Dim o As CrystalDecisions.Shared.ExportOptions
        'o = New CrystalDecisions.Shared.ExportOptions
        'o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
        'o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
        'filedest.DiskFileName = Server.MapPath("Facturas") & "\" + queimprimo.ToString.Trim + ".pdf"
        'o.ExportDestinationOptions = filedest.Clone
        'mireporte.Export(o)
        'filedest = Nothing
        'o = Nothing






        'Dim Nombre As String

        'Nombre = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".pdf"))
        'Dim xmlArchivo As String = (Server.MapPath("/Facturas/" + queimprimo.ToString.Trim + ".xml"))


        'Response.Clear()
        'Response.ContentType = "application/pdf"
        'Response.AddHeader("Content-disposition", "attachment; filename=" & Nombre)
        'Response.WriteFile(Nombre)
        'Response.Flush()
        'Response.Close()

    End Sub
    Sub cargardatosFac(ByRef formaPago As String, ByRef importe As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT   convert(varchar,convert(decimal(8,2),importePago)) as importePago ,clave FROM [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatFormasDePago] as CF on IC.codigoFormaPago=CF.codigoFormaPago " & _
                   "where folioconsulta='" & fconsulta.Value & "' and IC.status=1 and codigoEmpresa=" & Session("codigoEmpresa") & ""
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                formaPago = dt.Rows(0).Item("clave")
                importe = dt.Rows(0).Item("importePago")
            End If

        End If

    End Sub
    Sub GuardarIngresoCajas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim pago As String = cargarTotalPagos()
        strSQL = "select * from [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                 "where folioConsulta='" & Session("folioConsulta") & "' "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set importePago=" & pago & "" & _
                        "where folioConsulta='" & Session("folioConsulta") & "'"
            Else
                strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado,abono) " & _
                "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,99," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0,0)"
            End If
        End If
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
        Else
            LblMensajeAdvertencia.Text = "Error 1278: " + clsDatos.MensajeError
            PanelAdvertencia.Visible = True
            PanelAdvertencia.Focus()
        End If
    End Sub
    Function cargarTotalPagos() As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT sum(cantidad * costo) as Total " & _
                 " FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                   "where folioConsulta='" & Session("folioConsulta") & "' and codigoempresa = " & Session("codigoEmpresa") & "  and status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Total")) Then
                Return dt.Rows(0).Item("Total").ToString
            Else
                Return "0.0"
            End If

        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            Return "0.0"
        End If
    End Function

    Sub buscarConsultasAfacturar(ByRef codigoPAciente As String, ByRef folioConsulta As String)
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim dt2 As New DataTable

        strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
            "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
            "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
            "where A.codigoPaciente='" & codigoPAciente & "' and  IC.Facturado=0  and IC.PagoRealizado=0 and A.folioConsulta='" & Session("folioConsulta") & "'" & _
            "order by fechaAgenda desc "
        bandera_facturaCredito = 1 ' si es credito
        If clsDatos.cargatabla(strSQL, dt2) = 0 Then
            If dt2.Rows.Count = 0 Then
                bandera_facturaCredito = 0 ' no es credito
                strSQL = "SELECT A.folioConsulta,convert(varchar,A.FechaAgenda,106) as FechaAgenda,format(IC.importePago,'N','en-us') as importePago  " & _
            "FROM  [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
            "inner join [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] as IC " & _
            "on A.folioConsulta=IC.folioConsulta and IC.status=1 and A.codigoEmpresa=IC.codigoEmpresa " & _
            "where A.codigoPaciente='" & codigoPAciente & "' and IC.PagoRealizado=1 and IC.Facturado=0 " & _
            "order by fechaAgenda desc "
            End If
        End If
        If clsDatos.cargatabla(strSQL, dt2) = 0 Then
            If dt2.Rows.Count > 0 Then
                GdConsultasXpagar.DataSource = dt2
                GdConsultasXpagar.DataBind()
                For Each fila As GridViewRow In GdConsultasXpagar.Rows
                    If fila.Cells(0).Text = folioConsulta Then
                        CType(fila.FindControl("chk"), CheckBox).Checked = True
                        CType(fila.FindControl("chk"), CheckBox).Enabled = False
                        If bandera_facturaCredito = 1 Then
                            fila.BackColor = Color.Salmon
                        Else
                            fila.BackColor = Color.MediumTurquoise
                        End If

                    End If
                Next
            End If
        Else
            LblMensajeAdvertencia.Text = clsDatos.MensajeError
            PanelAdvertencia.Visible = True
        End If
    End Sub
    Function consultasAfacturar(ByRef dt1 As DataTable, ByRef dt2 As DataTable, ByRef listaFolios As String, ByRef listaFoliosSQL As String) As Boolean
        Dim clsDatos As New ClaseDatos
        Dim strSQL As String
        Dim listaFac As String = ""
        Dim count As Integer
        Dim listaSQL As String = ""
        count = 0
        For Each row As GridViewRow In GdConsultasXpagar.Rows
            If CType(row.FindControl("chk"), CheckBox).Checked Then
                If count > 0 Then
                    listaFac += ","
                    listaSQL += ","
                End If
                listaFac += "'" + row.Cells(0).Text + "'"
                listaSQL += row.Cells(0).Text
                count = count + 1
            End If
        Next
        If listaFac = "" Then
            Return False
        Else
            strSQL = "select sum(cantidad) as cantidad,descripcion,cast((costo) AS decimal(16,2)) as costo FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                    "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 " & _
                    "group by descripcion,costo"
            If clsDatos.cargatabla(strSQL, dt1) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            strSQL = "select CAST(SUM(costo*cantidad) AS decimal(16,2)) AS costoTotal FROM [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                    "where folioConsulta in (" & listaFac & ") AND codigoempresa = '" & Session("codigoEmpresa") & "' AND status=1 "
            If clsDatos.cargatabla(strSQL, dt2) <> 0 Then
                LblMensajeAdvertencia.Text = clsDatos.MensajeError
                PanelAdvertencia.Visible = True
            End If
            listaFolios = listaFac
            listaFoliosSQL = listaSQL
            Return True
        End If

    End Function

   
End Class