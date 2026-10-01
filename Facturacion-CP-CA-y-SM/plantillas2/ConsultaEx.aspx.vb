'Bueno
Imports System.Text
Imports System.Windows.Input
Imports System.IO
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Public Class ConsultaEx
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ocultarPaneles()
        ocultarPanelesdiag()
        ocultarPanelesdiagpp()
        ocultarPanelesex()
        ocultarPanelesReceta()
        ocultarPanelesJustificaciones()
        ocultarPanelesPagos()

        If Not IsPostBack Then
            If Session("codigoUsuario") Is Nothing And Session("sesion") Is Nothing Then
                Response.Redirect("login.aspx")
            End If
            Session.Add("UserFolder", Context.Server.MapPath("~/archivos"))
            Hffolioconsulta.Value = Session("FolioConsulta")
            hfFechaAgenda.Value = Session("FechaAgenda")
            cargardatosbase()
            cargarpaciente()
            cargarcita()
            cargarhistorialcitas()
            consultadiagnosticospp()
            listaConceptoPagos()
            Gvdiagnosticosps.DataBind()
            cargardiagnosticos()
            'historialdiagnosticos()
            cargarConceptoPagos()
            cargaextremidades()
            ConsultaMedicamentos()
            ConsultaJustificaciones()
            llenareceta()
            llenaGridJustificaciones()
            llenajustificacion()
            btagregarnj.Visible = True
            cargaListaDeEstudiosRX()
            leeListEstudioRX()
            CargaImgRX()
            CargaVideo()
            folioRX()
            Deshabilitar()
        End If


    End Sub


    'Btn para guardar datos
    'Protected Sub LnkGuardarTodo_Click(sender As Object, e As EventArgs)


    '    GuardarActualizarAM()
    '    GuardarActualizarDatos()

    'End Sub

    'Btn para guardar datos
    'Protected Sub LnkGuardarTodoRegresar_Click(sender As Object, e As EventArgs)


    '    GuardarActualizarAM()
    '    GuardarActualizarDatos()


    '    Dim consultorio, fecha As String
    '    consultorio = Session("codigoConsultorio")
    '    fecha = hfFechaAgenda.Value

    '    Session.Add("CodigoConsultorio", consultorio)
    '    Session.Add("FechaAgenda", fecha)
    '    Response.Redirect("agenda.aspx")

    'End Sub

    ' boton para Finalizar la consulta/expediente
    Protected Sub LnkFinalizar_Click(sender As Object, e As EventArgs)

        LblMostarDecision.Text = "¿Esta seguro que desea finalizar la consulta?"
        HdPreguntas.Value = "1"
        PanelDesicion.Visible = True
        PanelDesicion.Focus()

        'Dim dt As New DataTable
        'Dim clsdatos As New ClaseDatos
        'Dim strsql As String


        'Dim sindatos As String
        'sindatos = " "

        'strsql = " select h.folioconsulta, h.codigodiagnostico, d.descripcion " & _
        '      " from agagenda a " & _
        '      " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
        '      " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
        '      " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
        '      " where a.folioconsulta = '" & Hffolioconsulta.Value & "' and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1"

        'If clsdatos.cargatabla(strsql, dt) = 0 Then
        '    If dt.Rows.Count > 0 Then
        '        LblMostarDecision.Text = "¿Esta seguro que desea finalizar la consulta?"
        '        HdPreguntas.Value = "1"
        '        PanelDesicion.Visible = True
        '        PanelDesicion.Focus()
        '    Else
        '        strsql = " select top 1 h.IdHmD, h.folioconsulta, h.codigodiagnostico, d.descripcion as descripcion " & _
        '" from agagenda a " & _
        '" inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
        '" inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
        '" d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
        '" where a.CodigoPaciente  = (select codigopaciente from agagenda where folioconsulta = '" & Hffolioconsulta.Value & "' " & _
        '" and codigoempresa = '" & Session("codigoEmpresa") & "') and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1" & _
        '" order by h.IdHmD desc "

        '        If clsdatos.cargatabla(strsql, dt) = 0 Then
        '            If dt.Rows.Count > 0 Then
        '                LblMostarDecision.Text = "¿Desea continuar con el Ultimo Diagnostico del paciente :" + " " + dt.Rows(0).Item("descripcion").trim + "? "
        '                HdPreguntas.Value = "1"
        '                PanelDesicion.Visible = True
        '                PanelDesicion.Focus()

        '            Else



        '                LblMensajeAdvertencia.Text = "¿No se puede Finalizar la consulta Sin diagnostico ?"
        '                PanelAdvertencia.Visible = True
        '                PanelAdvertencia.Focus()
        '            End If
        '        End If
        '    End If

        'End If




    End Sub
    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntas.Value
            Case "1"
                GuardarActualizarAM()
                GuardarActualizarDatos()
                GuardarIngresoCajas()
                Dim strsql As String
                Dim dt As New DataTable
                Dim clsdatos As New ClaseDatos

                strsql = "UPDATE AgAgenda SET CodigoEtapa=6 WHERE folioconsulta = '" & Hffolioconsulta.Value & "'"
                clsdatos.cargaComando(strsql)
                clsdatos.ejecutar()
                Response.Redirect("Agenda.aspx")
        End Select
    End Sub
    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        GuardarActualizarAM()
        GuardarActualizarDatos()
    End Sub
    'Guarda/actualiza los datos del historial médico
    Sub GuardarActualizarDatos()
        Dim strsql As String '
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable '
        'correcccion temporal, error de Antonio,   OOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOOO
        If TxtImc.Text = "Índice de Masa Corporal" Then
            TxtImc.Text = ""
        End If
        'oooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooooo

        strsql = "SELECT folioconsulta FROM hmexploracion WHERE folioconsulta ='" & Hffolioconsulta.Value & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then

            If dt.Rows.Count > 0 Then
                strsql = "UPDATE hmexploracion SET  PadecimientoActual='" & txtPadecimientoActual.Text & "' " & _
                         ",TensionArterial='" & TxtTension.Text & "', FrecuenciaCardiaca='" & Txtfrecuencia.Text & "', Peso='" & Txtpeso.Text & "', Estatura='" & txttalla.Text & "', IndiceMasaCorporal='" & TxtImc.Text & "' " & _
                         ",Exploracion='" & txtExploracion.Text & "',planseguir='" & txtPlan.Text & "' WHERE folioconsulta='" & Hffolioconsulta.Value & "'"
                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = clsdatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
                LblMensajeAviso.Text = "  Los datos del Paciente se han Actualizado Correctamente "
                PanelAvisos.Visible = True
                PanelAvisos.Focus()



            Else
               
                strsql = "INSERT INTO hmexploracion (" & _
                "folioconsulta,PadecimientoActual, Exploracion, PlanSeguir, farmRecibidos, codigoExtremidad, TensionArterial, " & _
                "FrecuenciaCardiaca, Peso, Estatura, IndiceMasaCorporal, CodigoUsuario," & _
                 "CodigoEmpresa, Status, FechaActualizacion) VALUES (" & _
                 "'" & Hffolioconsulta.Value & "', '" & txtPadecimientoActual.Text & "', " & _
                 "'" & txtExploracion.Text & "', '" & txtPlan.Text & "',0,0 ," & _
                 "'" & TxtTension.Text & "', '" & Txtfrecuencia.Text & "', '" & Txtpeso.Text & "'," & _
                 "'" & txttalla.Text & "','" & TxtImc.Text & "','" & Session("codigoUsuario") & "', 1, 1," & _
                 " getdate())"
                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = -1 Then
                    LblMensajeCritico.Text = "Error 567: " + clsdatos.MensajeError
                    PanelCritico.Visible = True
                    PanelCritico.Focus()
                End If
                'guarda al finalizar los datos del medico de cabecera gerry
                strsql = "SELECT idDoctorCabecera  FROM [" & clsdatos.BaseDatos & "].[dbo].[CatConsultorios] " & _
                         "where codigoConsultorio='" & Session("codigoConsultorio") & "' and codigoEmpresa=" & Session("codigoEmpresa") & " "
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        strsql = "UPDATE AgAgenda SET  CodigoUsuarioAtiende='" & dt.Rows(0).Item("idDoctorCabecera") & "' WHERE folioconsulta='" & Hffolioconsulta.Value & "'"
                        clsdatos.cargaComando(strsql)
                        If clsdatos.ejecutar() = 0 Then
                            LblMensajeAviso.Text = "Los datos del paciente se han guardado correctamente."
                            PanelAvisos.Visible = True
                        Else
                            LblMensajeCritico.Text = "Error 565: " + clsdatos.MensajeError
                            PanelCritico.Visible = True
                        End If
                    End If
                Else
                    LblMensajeCritico.Text = "Error 566: " + clsdatos.MensajeError
                    PanelCritico.Visible = True
                End If

                ' Guardar al finalizar segun el usuario logeado +++++
                'strsql = "UPDATE AgAgenda SET  CodigoUsuarioAtiende='" & Session("codigoUsuario") & "' WHERE folioconsulta='" & Hffolioconsulta.Value & "'"
                'clsdatos.cargaComando(strsql)
                'clsdatos.ejecutar()
                'LblMensajeAviso.Text = "Los datos del paciente se han guardado correctamente."
                'PanelAvisos.Visible = True
                'PanelAvisos.Focus()

            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If

    End Sub
    'Protected Sub txttalla_TextChanged(sender As Object, e As EventArgs) Handles txttalla.TextChanged
    '    If Val(txttalla.Text) > 0 And (Txtpeso.Text) <> "" Then
    '        TxtImc.Text = String.Format("{0:00.00}", (Val(Txtpeso.Text)) / (Val(txttalla.Text) * Val(txttalla.Text)))
    '        cargaDesIMC()
    '    Else
    '        TxtImc.Text = "Índice de Masa Corporal"
    '    End If


    'End Sub
    'Protected Sub txtpeso_TextChanged(sender As Object, e As EventArgs) Handles Txtpeso.TextChanged
    '    If (txttalla.Text) <> "" And Val(Txtpeso.Text) > 0 Then
    '        TxtImc.Text = String.Format("{0:00.00}", (Val(Txtpeso.Text)) / (Val(txttalla.Text) * Val(txttalla.Text)))
    '        cargaDesIMC()
    '    Else
    '        TxtImc.Text = "Índice de Masa Corporal"
    '    End If
    'End Sub
    ' guarda/actualizar los antecedentes medicos del paciente
    Sub GuardarActualizarAM()

        Dim strsql As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        strsql = "select am.codigopaciente from agagenda a "
        strsql += vbNewLine & " left join hmantecedentesmedicos am on a.codigopaciente = am.codigopaciente "
        strsql += vbNewLine & "  where a.folioconsulta = '" & Hffolioconsulta.Value & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If IsDBNull(dt.Rows(0).Item("codigopaciente")) Then
                    strsql = " INSERT INTO [dbo].[HmAntecedentesMedicos]([CodigoPaciente],[AntecedentesPatologicos]" & _
                     " ,[AntecedentesNoPatologicos],[AlergiasComunes],[CodigoUsuario],[CodigoEmpresa],[FechaActualizacion])" & _
                     " VALUES('" & HFCodPaciente.Value & "','" & txtPatologicos.Text.Trim & "','" & txtNoPatologicos.Text.Trim & "'" & _
                     " ,'" & txtAlergias.Text.Trim & "','" & Session("codigoUsuario") & "',1,getdate())"
                    clsdatos.cargaComando(strsql)
                    clsdatos.ejecutar()
                Else
                    strsql = " UPDATE hm SET hm.antecedentespatologicos = '" & txtPatologicos.Text.Trim & "', hm.AntecedentesNoPatologicos = '" & txtNoPatologicos.Text.Trim & "'," & _
                     " hm.alergiascomunes = '" & txtAlergias.Text.Trim & "', codigousuario = '" & Session("codigoUsuario") & "', fechaactualizacion = getdate()" & _
                     " FROM hmantecedentesmedicos hm" & _
                     " WHERE hm.codigopaciente = '" & HFCodPaciente.Value & "'"
                    clsdatos.cargaComando(strsql)
                    clsdatos.ejecutar()
                End If
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If
        End If
    End Sub
    'Se llena los datos generales de la citaV
    Sub cargarcita()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT A.folioConsulta,A.fechaAgenda,(U2.nombre+' '+U2.primerApellido+' '+U2.segundoApellido) as doctorConectado,(U2.nombre+' '+U2.primerApellido+' '+U2.segundoApellido) as doctorAtiende,(U.nombre+' '+U.primerApellido+' '+U.segundoApellido) as agendo  " & _
                 "FROM [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
                 "inner join [" & clsdatos.BaseDatos & "].[dbo].[catUsuarios] as U on U.codigoUsuario=A.codigoUsuarioAgenda " & _
                 "inner join [" & clsdatos.BaseDatos & "].[dbo].[catConsultorios] as C on C.codigoConsultorio=A.codigoConsultorio " & _
                 "inner join [" & clsdatos.BaseDatos & "].[dbo].[catUsuarios] as U2 on U2.codigoUsuario=C.idDoctorCabecera " & _
                 "where A.folioconsulta ='" & Hffolioconsulta.Value & "'"

        'Antigua Consulta donde guarda segun el usuario loqueado
        'strsql = "  select a.folioconsulta,fechaagenda, isnull(case when a.codigoetapa > 1  then ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido  from catusuarios u" & _
        ' " where u.codigousuario = '" & Session("codigoUsuario") & "' and u.codigoempresa = a.codigoempresa)  else ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido " & _
        ' " from catusuarios u where u.codigousuario = '" & Session("codigoUsuario") & "' and u.codigoempresa = a.codigoempresa and u.rol in ('DR','RE')) " & _
        ' " end , '') as doctorconectado, " & _
        ' " isnull(case when a.codigoetapa > 1  then ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido from catusuarios u" & _
        ' " where u.codigousuario = a.CodigoUsuarioAtiende and u.codigoempresa = a.codigoempresa)  else ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido " & _
        ' " from catusuarios u where u.codigousuario = a.CodigoUsuarioAtiende and u.codigoempresa = a.codigoempresa and u.rol in ('DR','RE')) " & _
        ' " end , '') as doctoratiende," & _
        ' " isnull(case when a.codigoetapa > 1  then ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido   from catusuarios u" & _
        ' " where u.codigousuario = a.CodigoUsuarioAgenda and u.codigoempresa = a.codigoempresa)  else ( select + u.nombre + ' '+ u.primerapellido + ' '+ u.segundoapellido " & _
        ' " from catusuarios u where u.codigousuario = a.CodigoUsuarioAgenda and u.codigoempresa = a.codigoempresa and u.rol in ('DR','RE')) " & _
        ' " end , '') as agendo" & _
        ' " from agagenda a " & _
        ' " where a.folioconsulta = '" & Hffolioconsulta.Value & "'"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Lblfolioconsulta.Text = dt.Rows(0).Item("folioconsulta")
                lblagendo.Text = dt.Rows(0).Item("agendo")
                lblfagenda.text = dt.Rows(0).Item("fechaagenda")
                If dt.Rows(0).Item("doctoratiende") = "" Then
                    LblDrAtiende.Text = dt.Rows(0).Item("doctorconectado")
                    txtdocjustificacion.Text = LblDrAtiende.Text
                Else
                    LblDrAtiende.Text = dt.Rows(0).Item("doctoratiende")
                    txtdocjustificacion.Text = LblDrAtiende.Text
                End If
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                PanelCritico.Focus()
            End If

        End If


    End Sub
    'se llena el combo historial de citasVG
    Sub cargarhistorialcitas()
        Dim strsql As String
        Dim funciones As New FuncionesGenerales

        strsql = "   select folioconsulta, format(fechaagenda,'dd/MM/yyyy') from agagenda" & _
       " where codigopaciente = (select codigopaciente from agagenda where folioconsulta = '" & Hffolioconsulta.Value & "' and codigoempresa = 1   )" & _
       " and codigoetapa in (2,3,5,6) and codigoempresa = 1" & _
       " order by fechaagenda desc"
        If funciones.llenadropdown(strsql, DDHfecha) = -1 Then
            CType(Master.FindControl("LblMensajeError"), Label).Text = funciones.MensajeError

        Else
            DDHfecha.SelectedValue = Session("fechaAgenda")
        End If
    End Sub
    Protected Sub DDHfecha_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles DDHfecha.SelectedIndexChanged
        Session("FechaAgenda") = DDHfecha.Items(DDHfecha.SelectedIndex).Text
        Hffolioconsulta.Value = DDHfecha.SelectedValue
        cargarpaciente()
        cargarcita()
        cargardatosbase()
        consultadiagnosticospp()
        listaConceptoPagos()
        Gvdiagnosticosps.DataBind()
        cargardiagnosticos()
        'historialdiagnosticos()
        cargarConceptoPagos()
        cargaextremidades()
        llenareceta()
        llenaGridJustificaciones()
        llenajustificacion()

    End Sub
    'Llenar el panel de datos del pacienteVG
    Sub cargarpaciente()

        'Reset de datos en Historial

        TxtTension.Text = ""
        Txtfrecuencia.Text = ""
        Txtpeso.Text = ""
        txttalla.Text = ""
        TxtImc.Text = ""
        txtPadecimientoActual.Text = ""
        txtExploracion.Text = ""
        txtPlan.Text = ""

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = " select  p.CodigoPaciente , p.papellido + ' ' + p.sapellido+ ' ' +p.nombres as nombre, concat(iif((iif(DATEPART(dayofyear,p.FechaNacimiento) > DATEPART(dayofyear,getdate())  , " & _
        " datediff(YEAR,p.FechaNacimiento, getdate()) -1 ,datediff(YEAR,p.FechaNacimiento, getdate()))) = 0, '', concat((iif(DATEPART(dayofyear,p.FechaNacimiento) > DATEPART(dayofyear,getdate())  , " & _
         " datediff(YEAR,p.FechaNacimiento, getdate()) -1 ,datediff(YEAR,p.FechaNacimiento, getdate()))) ,' años ')) , " & _
         " concat(FLOOR((CAST(DATEDIFF(day, p.FechaNacimiento, GETDATE()) AS float) / 365 - FLOOR(CAST(DATEDIFF(day, p.FechaNacimiento, " & _
         " GETDATE()) AS float) / 365)) * 12) , ' meses ')) as edad, p.FechaNacimiento, " & _
         " o.descripcion as ocupacion, i.descripcion as estadocivil, iif(p.Genero = 'H','MASCULINO','FEMENINO') as genero, n.nacionalidad, p.municipio + ', ' +  e.nombreentidad as procedencia" & _
         ",C.razonSocial,CT.descripcion as tipoPaciente " & _
         " from [" & clsdatos.BaseDatos & "].[dbo].[agagenda] a " & _
         " inner join [" & clsdatos.BaseDatos & "].[dbo].[catpacientes] p on  p.codigopaciente = a.codigopaciente and p.CodigoEmpresa = a.CodigoEmpresa " & _
         " inner join [" & clsdatos.BaseDatos & "].[dbo].[catentidades] e on e.codigoentidad = p.codigoentidad " & _
         " inner join [" & clsdatos.BaseDatos & "].[dbo].[catocupaciones] o on o.codigoocupacion = p.codigoocupacion and o.CodigoEmpresa = a.CodigoEmpresa " & _
         " inner join [" & clsdatos.BaseDatos & "].[dbo].[CatEstadoCivil] i on i.codigoEstadoCivil = p.CodigoEstadoCivil and i.codigoEmpresa = a.CodigoEmpresa " & _
         " inner join [" & clsdatos.BaseDatos & "].[dbo].[catnacionalidades] n on n.codigonacionalidad = p.codigonacionalidad " & _
          "inner join [" & clsdatos.BaseDatos & "].[dbo].[catClientes] C on C.codigoCliente=a.codigoCliente and C.codigoempresa =a.codigoEmpresa " & _
        "inner join CatTipoPaciente CT on CT.codigoTipo=a.codigoTipo and CT.codigoEmpresa=a.codigoEmpresa " & _
         " where a.folioconsulta = '" & Hffolioconsulta.Value & "'"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Session("codigoPaciente") = dt.Rows(0).Item("CodigoPaciente")
                HFCodPaciente.Value = dt.Rows(0).Item("CodigoPaciente")
                lblpaciente.Text = dt.Rows(0).Item("nombre").trim
                lbledad.Text = dt.Rows(0).Item("edad").trim
                Lblfechanacimiento.Text = Format(dt.Rows(0).Item("FechaNacimiento"), "dd") & " de " & _
                Format(dt.Rows(0).Item("FechaNacimiento"), "MMMM") & " de " & Format(dt.Rows(0).Item("FechaNacimiento"), "yyyy")
                lblocupacion.Text = dt.Rows(0).Item("ocupacion").trim
                lblestadocivil.Text = dt.Rows(0).Item("estadocivil").trim
                Lblgenero.Text = dt.Rows(0).Item("genero").trim
                Lbldireccion.Text = dt.Rows(0).Item("procedencia").trim
                Lblnacionalidad.Text = dt.Rows(0).Item("nacionalidad").trim
                lblNombreReceta.Text = lblpaciente.Text
                lblNombreJusti.Text = lblNombreReceta.Text
                lblEdadReceta.Text = lbledad.Text
                lblEdadJusti.Text = lblEdadReceta.Text
                lblfreceta.Text = Session("FechaAgenda")
                lblfjustificacion.Text = Session("FechaAgenda")
                LblAseguradora.Text = dt.Rows(0).Item("razonSocial")
                LblTipoPaciente.Text = dt.Rows(0).Item("tipoPaciente")


            End If
        Else
            LblMensajeCritico.Text = "Error 21: " + clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If


        strsql = "SELECT CodigoPaciente," & _
              "isnull(AntecedentesPatologicos,'')as AntecedentesPatologicos, isnull(AntecedentesNoPatologicos,'')as AntecedentesNoPatologicos, isnull(AlergiasComunes,'')as AlergiasComunes FROM HmAntecedentesMedicos WHERE codigopaciente= " & HFCodPaciente.Value & ""
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                txtPatologicos.Text = dt.Rows(0).Item("AntecedentesPatologicos").trim
                txtNoPatologicos.Text = dt.Rows(0).Item("AntecedentesNoPatologicos").trim
                txtAlergias.Text = dt.Rows(0).Item("AlergiasComunes").trim
                txtAntecedentes.Enabled = False
                txtTratamientoPrevio.Enabled = False
                txtalergiareceta.Text = txtAlergias.Text
                hftemalergia.Value = txtalergiareceta.Text


            End If
        Else
            LblMensajeCritico.Text = "Error 24: " + clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If
        strsql = "select Exploracion,planseguir from hmexploracion where folioconsulta = "
        strsql = strsql + "(select top 1(folioconsulta) from Agagenda where codigoPaciente='" & HFCodPaciente.Value & "' and folioconsulta < '" & Hffolioconsulta.Value & "' order by folioconsulta desc)"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                txtAntecedentes.Text = dt.Rows(0).Item("Exploracion").trim
                txtTratamientoPrevio.Text = dt.Rows(0).Item("planseguir").trim
                txtAntecedentes.Enabled = False
                txtTratamientoPrevio.Enabled = False
            Else
                txtAntecedentes.Text = ""
                txtTratamientoPrevio.Text = ""
                txtAntecedentes.Enabled = False
                txtTratamientoPrevio.Enabled = False
            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If

        strsql = ""
        strsql = "select TensionArterial,FrecuenciaCardiaca, Peso, Estatura, IndiceMasaCorporal,PadecimientoActual,exploracion,planseguir from ("
        strsql = strsql + " select TensionArterial,FrecuenciaCardiaca, Peso, Estatura, IndiceMasaCorporal,PadecimientoActual,exploracion,planseguir from hmexploracion " & _
        " where folioconsulta='" & Hffolioconsulta.Value & "') hmexploracion "

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                TxtTension.Text = dt.Rows(0).Item("TensionArterial").trim
                Txtfrecuencia.Text = dt.Rows(0).Item("FrecuenciaCardiaca").trim
                Txtpeso.Text = dt.Rows(0).Item("Peso").trim
                txttalla.Text = dt.Rows(0).Item("Estatura").trim
                TxtImc.Text = dt.Rows(0).Item("IndiceMasaCorporal").trim
                txtPadecimientoActual.Text = dt.Rows(0).Item("padecimientoactual").trim
                txtExploracion.Text = dt.Rows(0).Item("Exploracion").trim
                txtPlan.Text = dt.Rows(0).Item("planseguir").trim

            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If


        strsql = ""
        strsql = "SELECT Agagenda.CodigoEtapa as IDEtapaC, CatEtapasAgenda.descripcion as Descripcion, CatEtapasAgenda.imagen as ImagenEC, CatEtapasAgenda.colorFondo AS colorFondo FROM ("
        strsql = strsql + "select CodigoEtapa from Agagenda where folioconsulta='" & Hffolioconsulta.Value & "') Agagenda "
        strsql = strsql + "left join (select CodigoEtapa,descripcion,imagen,colorFondo from CatEtapasAgenda where CodigoEmpresa='" & Session("codigoEmpresa") & "') CatEtapasAgenda on CatEtapasAgenda.CodigoEtapa=Agagenda.CodigoEtapa"


        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                lbEtapaC.Text = dt.Rows(0).Item("Descripcion").trim
                imgEstadoC.Text = "<i class='fa fa-2x fa-" & dt.Rows(0).Item("ImagenEC") & "' style='color:" & dt.Rows(0).Item("colorFondo") & "; padding:6px 0'></i>"
                lbidEtapaC.Text = dt.Rows(0).Item("IDEtapaC")


            End If
        Else
            LblMensajeCritico.Text = clsdatos.MensajeError
            PanelCritico.Visible = True
            PanelCritico.Focus()
        End If

        If lbidEtapaC.Text > "4" Then
            btnEnviarEG.Visible = False
            LstEstudiosRX.Visible = False
            RbtListExtremidades.Visible = False
        Else
            btnEnviarEG.Visible = True
            LstEstudiosRX.Visible = True
            RbtListExtremidades.Visible = True
        End If

    End Sub


    Protected Sub btnEnviarEG_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviarEG.Click



        Dim strsql As String
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos

        strsql = "UPDATE AgAgenda SET CodigoEtapa=5 where folioconsulta = '" & Hffolioconsulta.Value & "'"
        clsdatos.cargaComando(strsql)
        clsdatos.ejecutar()

        GuardarActualizarAM()
        GuardarActualizarDatos()
        insertListEstudioRX()
        Regresar()

    End Sub
    Sub cargardiagnosticos()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        'strsql = " select h.folioconsulta, h.codigodiagnostico, d.codigoexterno, d.descripcion,h.codigolistaDiagnostico " & _
        ' " from agagenda a " & _
        ' " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
        ' " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
        ' " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
        ' " where a.folioconsulta = '" & Hffolioconsulta.Value & "' and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1"
        strsql = " select h.folioconsulta, h.codigodiagnostico, d.codigoexterno, d.descripcion,FORMAT(a.FechaAgenda , 'dd MMMM yyyy') as FechaAgenda " & _
         " from agagenda a " & _
         " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
         " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
         " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
         " where a.CodigoPaciente  = (select codigopaciente from agagenda where folioconsulta = '" & Hffolioconsulta.Value & "' " & _
         " and codigoempresa = '" & Session("codigoEmpresa") & "') and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1" & _
         " order by h.IdHmD desc "
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            Dgdiagnosticos.Columns(0).Visible = True
            Dgdiagnosticos.Columns(1).Visible = True
            Dgdiagnosticos.DataSource = dt
            Dgdiagnosticos.DataBind()
            Dgdiagnosticos.Columns(0).Visible = False
            Dgdiagnosticos.Columns(1).Visible = False
        Else
            LblMensajeCriticodiag.Text = clsdatos.MensajeError
            PanelCriticodiag.Visible = True
            PanelCriticodiag.Focus()
        End If
    End Sub

    'Sub historialdiagnosticos()
    '    Dim dt As New DataTable
    '    Dim clsdatos As New ClaseDatos
    '    Dim strsql As String

    '    strsql = " select top 1 h.IdHmD, h.folioconsulta, h.codigodiagnostico, d.codigoexterno, d.descripcion,h.codigolistaDiagnostico " & _
    '     " from agagenda a " & _
    '     " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
    '     " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
    '     " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
    '     " where a.CodigoPaciente  = (select codigopaciente from agagenda where folioconsulta = '" & Hffolioconsulta.Value & "' " & _
    '     " and codigoempresa = '" & Session("codigoEmpresa") & "') and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1" & _
    '     " order by h.IdHmD desc "
    '    If clsdatos.cargatabla(strsql, dt) = 0 Then
    '        If (dt.Rows.Count > 0) Then
    '            ultimo_diagnostico.InnerHtml = "<h4>Último Diagnóstico del Paciente: <strong >" + dt.Rows(0).Item("descripcion") + "</strong></h4><hr>"
    '        Else
    '            ultimo_diagnostico.InnerHtml = ""
    '        End If
    '        'GvdiagnosticosH.Columns(0).Visible = True
    '        'GvdiagnosticosH.Columns(1).Visible = True
    '        'GvdiagnosticosH.DataSource = dt
    '        'GvdiagnosticosH.DataBind()
    '        'GvdiagnosticosH.Columns(0).Visible = False
    '        'GvdiagnosticosH.Columns(1).Visible = False
    '    Else
    '        LblMensajeCriticodiag.Text = clsdatos.MensajeError
    '        PanelCriticodiag.Visible = True
    '        'PanelCriticodiag.Focus()
    '    End If
    'End Sub

    Sub cargarTotalPagos()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = "SELECT format(sum(costo*cantidad),'N','en-us') as Total " & _
                 " FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                   "where folioConsulta='" & Hffolioconsulta.Value & "' and codigoempresa = " & Session("codigoEmpresa") & "  and status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If Not IsDBNull(dt.Rows(0).Item("Total")) Then
                LblPagoTotal.Text = dt.Rows(0).Item("Total")
            Else
                LblPagoTotal.Text = 0.0
            End If

        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub
    Sub cargarConceptoPagos()
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        strsql = " SELECT codigoConcepto,servicio,cantidad,descripcion,format(costo,'C','en-us') as costo FROM [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                 "where folioConsulta='" & Hffolioconsulta.Value & "' and codigoempresa = '" & Session("codigoEmpresa") & "'  and status=1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            GdvConceptoPagos.Columns(0).Visible = True
            GdvConceptoPagos.DataSource = dt
            GdvConceptoPagos.DataBind()
            GdvConceptoPagos.Columns(0).Visible = False
            cargarTotalPagos()
        Else
            LblMensajeCriticoPagos.Text = clsdatos.MensajeError
            PanelCriticoPagos.Visible = True

        End If
    End Sub
    Private Sub Dgdiagnosticos_RowCommand(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Dgdiagnosticos.RowCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim fila As Integer = e.CommandArgument
        Dim coddiag As Integer = Dgdiagnosticos.Rows.Item(fila).Cells(0).Text()
        If e.CommandName = "borrar" Then
            LblMostarDecisiondiag.Text = "¿Deseas eliminar el diagnóstico?"
            PanelDesiciondiag.Visible = True
            'PanelDesiciondiag.Focus()
            HdPreguntasdiag.Value = "0"
            HdIndex.Value = coddiag
        End If
    End Sub

    Private Sub GdvConceptoPagos_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdvConceptoPagos.RowCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim fila As Integer = e.CommandArgument
        Dim codigoConcepto As Integer = GdvConceptoPagos.Rows.Item(fila).Cells(0).Text
        HdCantidad_a_descontar.Value = GdvConceptoPagos.Rows.Item(fila).Cells(4).Text
        If e.CommandName = "borrarConceptoPago" Then
            LblDecisionPagos.Text = "¿Deseas quitar el concepto de pago?"
            PanelDesicionPagos.Visible = True
            HdPreguntasPagos.Value = "0"
            HdIndexPagos.Value = codigoConcepto
        End If
    End Sub


    Protected Sub BtnSiPagos_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasPagos.Value
            Case "0"
                eliminarConceptoPago()
                ocultarPanelesPagos()
        End Select
    End Sub

    Protected Sub BtnNoPagos_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasPagos.Value
            Case "0"
                ocultarPanelesPagos()
        End Select
    End Sub

    Sub ocultarPanelesPagos()
        PanelAdvertenciaPagos.Visible = False
        PanelCriticoPagos.Visible = False
        PanelDesicionPagos.Visible = False
        PanelAvisosPagos.Visible = False
    End Sub

    Protected Sub BtnSidiag_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasdiag.Value
            Case "0"
                EliminarDiag()
                cargardiagnosticos()
                'historialdiagnosticos()
        End Select
    End Sub
    Private Sub EliminarDiag()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "  update h set status = 0, h.fechaactualizacion = getdate(), h.codigousuario = '" & Session("codigoUsuario") & "'" & _
                    " from hmdiagnosticosdet h where h.codigodiagnostico = '" & HdIndex.Value & "'  and h.folioconsulta = '" & Hffolioconsulta.Value & "' " & _
        " and codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAvisodiag.Text = "El diagnóstico ha sido eliminado."
            PanelAvisosdiag.Visible = True
            'PanelAvisosdiag.Focus()
            Gvdiagnosticosps.DataBind()

        Else
            LblMensajeCriticodiag.Text = clsDatos.MensajeError
            PanelCriticodiag.Visible = True
            'PanelCriticodiag.Focus()
        End If
    End Sub

    Sub eliminarConceptoPago()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        strSQL = "    delete from  [" & clsDatos.BaseDatos & "].[dbo].[HmPagosConceptos] " & _
                     "where folioConsulta='" & Hffolioconsulta.Value & "'  and codigoEmpresa =" & Session("codigoEmpresa") & " and status=1 and codigoConcepto='" & HdIndexPagos.Value & "' "
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            HdEliminaPago.Value = 1
            LblMensajeAvisoPagos.Text = "El concepto de pago se ha borrado."
            PanelAvisosPagos.Visible = True
            GuardarIngresoCajas()
            cargarConceptoPagos()
            'Gvdiagnosticosps.DataBind()
        Else
            LblMensajeCriticoPagos.Text = clsDatos.MensajeError
            PanelCriticoPagos.Visible = True
        End If
    End Sub
    Protected Sub BtnNodiag_Click(sender As Object, e As EventArgs)
        cargardiagnosticos()
        'historialdiagnosticos()
    End Sub

    Sub consultadiagnosticospp()

        Dim funciones As New FuncionesGenerales
        Dim strsql As String
        Lstdiagnosticos.Items.Clear()
        strsql = "  select d.codigodiagnostico, d.codigoexterno + '-' + d.descripcion as diagnostico from catdiagnosticosdet d"
        strsql += vbNewLine & " inner join catdiagnosticoslistas l on l.codigolistadiagnostico = d.codigoListaDiagnostico "
        strsql += vbNewLine & " inner join CnfConsultoriosListasDiagnosticos c on c.codigoListaDiagnostico  = l.codigoListaDiagnostico "
        strsql += vbNewLine & " where c.codigoconsultorio = (select codigoconsultorio from agagenda where folioconsulta = '" & Hffolioconsulta.Value & "'"
        strsql += vbNewLine & " and codigoempresa =  '" & Session("codigoEmpresa") & "' )and c.codigoempresa =  '" & Session("codigoEmpresa") & "' and "
        strsql += vbNewLine & " l.status = 1 and d.status =1 and c.status = 1"
        strsql += vbNewLine & " order by d.descripcion asc "
        If funciones.llenalistbox(strsql, Lstdiagnosticos) = -1 Then
            LblMensajeCriticodiag.Text = funciones.MensajeError
            PanelCriticodiag.Visible = True
            PanelCriticodiag.Focus()
        End If

    End Sub

    ' -------- Lcc. Gerardo G. Valdez 7/2/2017   Lista de pagos ----------------------
    Sub listaConceptoPagos()
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim strsql As String
        LstConceptoPagos.Items.Clear()
        strsql = "SELECT  codigoConcepto,(servicio + '      -      '+ descripcion + '      -     $ ' + convert(varchar,convert(decimal(8,2),costo))) as descripcion   FROM [" & clsDatos.BaseDatos & "].[dbo].[catConceptoPagos] " & _
        "where status = 1 And activomedico = 1 " & _
         "order by descripcion"
        If funciones.llenalistbox(strsql, LstConceptoPagos) = -1 Then
            LblMensajeCriticoPagos.Text = funciones.MensajeError
            PanelCriticoPagos.Visible = True
            'PanelCriticoPagos.Focus()
        End If

    End Sub

    Protected Sub Lstdiagnosticos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles Lstdiagnosticos.SelectedIndexChanged
        Dim dt As New DataTable
        Dim band As Boolean = True
        dt.Columns.Add("codigodiagnostico")
        dt.Columns.Add("codigolistadiagnostico")
        dt.Columns.Add("folioconsulta")
        dt.Columns.Add("codigoexterno")
        dt.Columns.Add("descripcion")
        Gvdiagnosticosps.Columns(0).Visible = True
        Gvdiagnosticosps.Columns(1).Visible = True
        Gvdiagnosticosps.Columns(2).Visible = True
        For Each fila As GridViewRow In Gvdiagnosticosps.Rows
            If fila.Cells(0).Text = Lstdiagnosticos.SelectedValue Then
                band = False
                Exit For
            Else
                band = True
                dt.Rows.Add({fila.Cells(0).Text, fila.Cells(1).Text, fila.Cells(2).Text, fila.Cells(3).Text, fila.Cells(4).Text})
            End If
        Next
        If band Then
            Dim ext, desc As String
            ext = Lstdiagnosticos.SelectedItem.Text.Substring(0, InStr(Lstdiagnosticos.SelectedItem.Text, "-") - 1)
            desc = Lstdiagnosticos.SelectedItem.Text.Substring(InStr(Lstdiagnosticos.SelectedItem.Text, "-"))
            dt.Rows.Add({Lstdiagnosticos.SelectedValue, 0, "", ext, desc})
            Gvdiagnosticosps.DataSource = dt
            Gvdiagnosticosps.DataBind()
            Gvdiagnosticosps.Columns(0).Visible = False
            Gvdiagnosticosps.Columns(1).Visible = False
            Gvdiagnosticosps.Columns(2).Visible = False
            guardardiag()


        End If
        cargardiagnosticos()
        'historialdiagnosticos()

    End Sub

    Private Sub LstConceptoPagos_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstConceptoPagos.SelectedIndexChanged
        Dim dt As New DataTable
        Dim band As Boolean = True
        dt.Columns.Add("codigoConcepto")
        dt.Columns.Add("cantidad")
        dt.Columns.Add("servicio")
        dt.Columns.Add("descripcion")
        dt.Columns.Add("costo")
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.Columns(1).Visible = True
        GdvConceptoPagos.Columns(2).Visible = True
        For Each fila As GridViewRow In GdvConceptoPagos.Rows
            If fila.Cells(0).Text = LstConceptoPagos.SelectedValue Then
                band = False
                GdvConceptoPagos.Columns(0).Visible = False
                Exit For
            Else
                band = True
                dt.Rows.Add({fila.Cells(0).Text, fila.Cells(1).Text, fila.Cells(2).Text, fila.Cells(3).Text, fila.Cells(4).Text.ToString})
            End If
        Next
        If band Then
            Dim costo, desc, servicio As String
            Dim cant As Integer
            Dim datos() As String
            datos = Split(LstConceptoPagos.SelectedItem.Text, "-", -1)
            servicio = datos(0).Trim
            desc = datos(1).Trim
            costo = datos(2).Trim
            cant = cantidad.Value
            dt.Rows.Add({LstConceptoPagos.SelectedValue, cant, servicio, desc, costo})
            GdvConceptoPagos.DataSource = dt
            GdvConceptoPagos.DataBind()
            GdvConceptoPagos.Columns(0).Visible = False
            GdvConceptoPagos.Columns(1).Visible = True
            GdvConceptoPagos.Columns(2).Visible = True
            guardarConceptoPagos()
            GuardarIngresoCajas()
            cantidad.Value = 1
        End If
        'cargarConceptoPagos()
    End Sub

    Sub guardarConceptoPagos()
        Dim clsdatos As New ClaseDatos
        Dim strsql, codigos As String
        ocultarPanelesPagos()
        GdvConceptoPagos.Columns(0).Visible = True
        GdvConceptoPagos.Columns(1).Visible = True
        codigos = ""
        For Each fila As GridViewRow In GdvConceptoPagos.Rows
            codigos += fila.Cells(0).Text & ","
        Next
        If codigos.Length > 0 Then
            codigos = codigos.Substring(0, codigos.Length - 1)
            strsql = "  INSERT INTO [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos] (folioConsulta,codigoConcepto,descripcion,costo,codigoEmpresa,codigoUsuario,fecha,status,cantidad,servicio) " & _
                    " SELECT '" & Hffolioconsulta.Value & "',CP.codigoConcepto,CP.descripcion,CP.costo,codigoEmpresa=" & Session("codigoEmpresa") & ",codigoUsuario=" & Session("codigoUsuario") & ",GETDATE(),1," & cantidad.Value & ", CP.servicio " & _
                    " FROM [" & clsdatos.BaseDatos & "].[dbo].[catConceptoPagos] as CP " & _
                    " WHERE CP.status=1 and CP.activomedico=1 and CP.codigoConcepto in (" & codigos & ") " & _
                    " AND CP.codigoConcepto NOT IN ( SELECT codigoConcepto FROM  [" & clsdatos.BaseDatos & "].[dbo].[HmPagosConceptos]  " & _
                    " WHERE folioConsulta= '" & Hffolioconsulta.Value & "' AND codigoEmpresa=1 AND status=1)"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                cargarConceptoPagos()
                ' GdvConceptoPagos.DataBind()
                GdvConceptoPagos.Columns(0).Visible = False
            Else
                LblMensajeCriticoPagos.Text = clsdatos.MensajeError
                PanelCriticoPagos.Visible = True
                'PanelCriticoPagos.Focus()
            End If

        Else
            LblMensajeAdvertenciaPagos.Text = "Seleccione un concepto"
            PanelAdvertenciaPagos.Visible = True
            'PanelAdvertenciaPagos.Focus()
            Exit Sub
        End If
    End Sub

    Sub guardardiag()
        Dim clsdatos As New ClaseDatos
        Dim strsql, codigos As String
        Gvdiagnosticosps.Columns(0).Visible = True
        Gvdiagnosticosps.Columns(1).Visible = True
        codigos = ""

        For Each fila As GridViewRow In Gvdiagnosticosps.Rows
            codigos += fila.Cells(0).Text & ","
        Next
        If codigos.Length > 0 Then
            codigos = codigos.Substring(0, codigos.Length - 1)

            strsql = "   insert into HMDIAGNOSTICOSDET (FolioConsulta,codigoDiagnostico,codigoListaDiagnostico,codigoempresa," & _
             " status,fechaactualizacion, codigousuario)" & _
             " SELECT '" & Hffolioconsulta.Value & "', CODIGODIAGNOSTICO, CODIGOLISTADIAGNOSTICO,'" & Session("codigoEmpresa") & "', 1, GETDATE(), " & Session("codigoUsuario") & "  " & _
             " FROM CATDIAGNOSTICOSDET WHERE codigoDiagnostico IN (" & codigos & ") AND codigoDiagnostico NOT IN " & _
             " (SELECT CODIGODIAGNOSTICO FROM HMDIAGNOSTICOSDET where folioconsulta = '" & Hffolioconsulta.Value & "' and codigoempresa = 1) "
            clsdatos.cargaComando(strsql)
            strsql = "  update h set status = 1, h.fechaactualizacion = getdate(), h.codigousuario = '" & Session("codigoUsuario") & "'" & _
             " from hmdiagnosticosdet h where h.folioconsulta = '" & Hffolioconsulta.Value & "' and h.codigodiagnostico  in (" & codigos & ") " & _
             " and codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 0"
            clsdatos.cargaComando(strsql)
            If clsdatos.ejecutar() = 0 Then
                cargardiagnosticos()
                Gvdiagnosticosps.DataBind()
                'historialdiagnosticos()
            Else
                LblMensajeCriticodiag.Text = clsdatos.MensajeError
                PanelCriticodiag.Visible = True
                'PanelCriticodiag.Focus()
            End If

        Else
            LblMensajeAdvertenciadiag.Text = "Seleccione un diagnóstico para poder guardar."
            PanelAdvertenciadiag.Visible = True
            ' PanelAdvertenciadiag.Focus()
            Exit Sub
        End If
    End Sub
    'elimina el diagnostico seleccionados selecciona *ventada poppup
    Protected Sub Gvdiagnosticosps_SelectedIndexChanged(sender As Object, e As System.Web.UI.WebControls.GridViewCommandEventArgs) Handles Gvdiagnosticosps.RowCommand
        Dim dt As New DataTable
        dt.Columns.Add("codigodiagnostico")
        dt.Columns.Add("codigolistadiagnostico")
        dt.Columns.Add("folioconsulta")
        dt.Columns.Add("codigoexterno")
        dt.Columns.Add("descripcion")
        Gvdiagnosticosps.Columns(0).Visible = True
        Gvdiagnosticosps.Columns(1).Visible = True
        Gvdiagnosticosps.Columns(2).Visible = True
        For Each filas As GridViewRow In Gvdiagnosticosps.Rows
            If filas.RowIndex <> Convert.ToInt32(e.CommandArgument) Then
                dt.Rows.Add({filas.Cells(0).Text, filas.Cells(1).Text, filas.Cells(2).Text, filas.Cells(3).Text, filas.Cells(4).Text})

            End If
        Next

        Gvdiagnosticosps.DataSource = dt
        Gvdiagnosticosps.DataBind()
        Gvdiagnosticosps.Columns(0).Visible = False
        Gvdiagnosticosps.Columns(1).Visible = False
        Gvdiagnosticosps.Columns(2).Visible = False
    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub
    Private Sub ocultarPanelesdiag()
        PanelAdvertenciadiag.Visible = False
        PanelAvisosdiag.Visible = False
        PanelCriticodiag.Visible = False
        PanelDesiciondiag.Visible = False
    End Sub
    Private Sub ocultarPanelesdiagpp()
        PanelAdvertenciadiag.Visible = False
        PanelAvisosdiag.Visible = False
        PanelCriticodiag.Visible = False
        PanelDesiciondiag.Visible = False
    End Sub
    Protected Sub Lnkbackagenda_Click(sender As Object, e As EventArgs)

        Dim consultorio, fecha As String
        consultorio = Session("codigoConsultorio")
        fecha = hfFechaAgenda.Value

        Session.Add("CodigoConsultorio", consultorio)
        Session.Add("FechaAgenda", fecha)
        Response.Redirect("agenda.aspx")


    End Sub
    'se llena las extremidades dolientes asignadas al pacienteVG
    Public Sub cargaextremidades()

        Dim funciones As New FuncionesGenerales
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strsql As String
        strsql = "select h.codigoextremidad, e.descripcion, e.urlimagen from HmExtremidadesDet h" & _
        " inner join catextremidades e on e.codigoextremidad = h.CodigoExtremidad " & _
        " and e.CodigoEmpresa = h.CodigoEmpresa  where h.folioconsulta = '" & Hffolioconsulta.Value & "' " & _
        " and h.codigoempresa = 1  and h.status = 1 order by e.descripcion"
        If clsdatos.cargatabla(strsql, dt) <> -1 Then
            Dgextremidades.DataSource = dt
            Dgextremidades.DataBind()
            For Each fila As DataGridItem In Dgextremidades.Items
                CType(fila.Cells(3).Controls(1), LinkButton).Text = fila.Cells(1).Text
            Next
            If Dgextremidades.Items.Count > 0 Then
                ImgExtremidades.ImageUrl = "../Resource/extremidades/" & Dgextremidades.Items(0).Cells(2).Text
            Else
                ImgExtremidades.ImageUrl = "../Resource/extremidades/" & DGextremidadesref.Items(0).Cells(2).Text
            End If
            strsql = "select h.Codigosal, c.descripcion from hmalergiasales h" & _
             " inner join catsales c on h.codigosal = c.codigosal" & _
             " where h.codigopaciente = 3 and h.codigoempresa = 1 and h.status = 1"
        Else
            LblMensajeCriticoex.Text = clsdatos.MensajeError
            PanelCriticoex.Visible = True
            'PanelCriticoex.Focus()
        End If
    End Sub
    'carga datos base para el manejo de la consulta
    Public Sub cargardatosbase()

        Dim funciones As New FuncionesGenerales
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strsql As String
        strsql = "select codigoextremidad,descripcion,urlimagen from CatExtremidades where codigoempresa= 1 and status = 1 order by descripcion "
        If funciones.llenalistbox(strsql, LstExtremidades) <> -1 Then
            If clsdatos.cargatabla(strsql, dt) <> -1 Then
                DGextremidadesref.DataSource = dt
                DGextremidadesref.DataBind()
            Else
                LblMensajeCriticoex.Text = clsdatos.MensajeError
                PanelCriticoex.Visible = True
                'PanelCriticoex.Focus()
            End If
        Else
            LblMensajeCriticoex.Text = clsdatos.MensajeError
            PanelCriticoex.Visible = True
            'PanelCriticoex.Focus()
        End If
    End Sub

    'guarda extremidades asignadas
    Function guardarextremidades()

        Dim strsql, codigos As String
        Dim clsdatos As New ClaseDatos
        codigos = ""
        For Each fila As DataGridItem In Dgextremidades.Items
            codigos += fila.Cells(0).Text & ","
        Next
        If codigos.Length > 0 Then
            codigos = codigos.Substring(0, codigos.Length - 1)
        End If

        '''''' Guarda la extremidad nueva
        strsql = "insert into  hmextremidadesdet" & _
        " select '" & Hffolioconsulta.Value & "', codigoextremidad, " & Session("codigoUsuario") & ",codigoempresa,1, getdate()" & _
         " from catextremidades where codigoextremidad in (" & codigos & ") and codigoextremidad not in " & _
         " (select codigoextremidad from HmExtremidadesDet where folioconsulta = '" & Hffolioconsulta.Value & "' and codigoempresa = " & Session("codigoEmpresa") & ")" & _
         " and CodigoEmpresa = " & Session("codigoEmpresa") & " "
        clsdatos.cargaComando(strsql)
        '''''''Activa la extremidad nuevamente
        strsql = " update h set h.status = 1, h.fechaactualizacion = getdate(), codigousuario = " & Session("codigoUsuario") & " " & _
         " from hmextremidadesdet h" & _
         "  where codigoextremidad in (" & codigos & ")  and folioconsulta = '" & Hffolioconsulta.Value & "' " & _
         " and codigoempresa = " & Session("codigoEmpresa") & " and status = 0 "
        clsdatos.cargaComando(strsql)

        If clsdatos.ejecutar = -1 Then
            LblMensajeCriticoex.Text = clsdatos.MensajeError
            PanelCriticoex.Visible = True
            'PanelCriticoex.Focus()
            Return -1
        End If
        Return 0
    End Function

    'muestra la extremidad selecionada 
    Public Sub seleccionarextremidad(ByRef codigoextremidad As Integer)
        For Each fila As DataGridItem In DGextremidadesref.Items
            If fila.Cells(0).Text = codigoextremidad Then
                ImgExtremidades.ImageUrl = "../Resource/extremidades/" & fila.Cells(2).Text
                Exit For
            End If
        Next
    End Sub

    'evento del boton agregar extremidad
    Protected Sub Agregar_Extremidad_Click()
        Dim band As Boolean = True
        Dim dt As New DataTable
        If LstExtremidades.SelectedValue.Trim = "" Then
            LblMensajeAdvertenciaex.Text = "Por favor, seleccione una extemidad."
            PanelAdvertenciaex.Visible = True
            'PanelAdvertenciaex.Focus()
            Exit Sub

        Else

            For Each filaref As DataGridItem In DGextremidadesref.Items
                If filaref.Cells(0).Text = LstExtremidades.SelectedValue Then
                    dt.Columns.Add("codigoextremidad")
                    dt.Columns.Add("descripcion")
                    dt.Columns.Add("urlimagen")
                    For Each fila As DataGridItem In Dgextremidades.Items
                        If fila.Cells(0).Text = LstExtremidades.SelectedValue Then
                            band = False
                            Exit For
                        Else
                            band = True
                            dt.Rows.Add({fila.Cells(0).Text, fila.Cells(1).Text, fila.Cells(2).Text})
                        End If
                    Next
                    If band Then
                        dt.Rows.Add({filaref.Cells(0).Text, filaref.Cells(1).Text, filaref.Cells(2).Text})
                        Dgextremidades.DataSource = dt
                        Dgextremidades.DataBind()
                        For Each fila As DataGridItem In Dgextremidades.Items
                            CType(fila.Cells(3).Controls(1), LinkButton).Text = fila.Cells(1).Text
                        Next

                    End If
                    guardarextremidades()
                    Exit For

                End If
            Next
        End If
    End Sub
    'evento que selecciona la extremidad para mostrarla desde la lista de extremidades
    Protected Sub LstExtremidades_SelectedIndexChanged(sender As Object, e As EventArgs) Handles LstExtremidades.SelectedIndexChanged
        seleccionarextremidad(LstExtremidades.SelectedValue)
    End Sub
    'elimina la extremidad o la despliega, depende del comandoVG

    Protected Sub Dgextremidades_SelectedIndexChanged(sender As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles Dgextremidades.ItemCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        If e.CommandArgument = "" Then
            ImgExtremidades.ImageUrl = "../Resource/extremidades/" & e.Item.Cells(2).Text
        Else
            Dim fila As Integer = e.CommandArgument
            Dim codextre As Integer = Dgextremidades.Items(fila).Cells(0).Text()

            If e.CommandName = "borrarex" Then
                LblMostarDecisionex.Text = "¿Deseas eliminar la extremedidad seleccionada?"
                PanelDesicionex.Visible = True
                'PanelDesicionex.Focus()
                HdPreguntasex.Value = "0"
                HdIndexex.Value = codextre
            End If

        End If

    End Sub
    Protected Sub BtnSiex_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasex.Value
            Case "0"

                Eliminarex()
                cargaextremidades()
        End Select
    End Sub
    Private Sub Eliminarex()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable


        strSQL = " update h set h.status = 0, h.fechaactualizacion = getdate(), codigousuario = " & Session("codigoUsuario") & "  " & _
                    " from hmextremidadesdet h" & _
                    " where codigoextremidad = '" & HdIndexex.Value & "'   and folioconsulta = '" & Hffolioconsulta.Value & "' " & _
                    " and codigoempresa = " & Session("codigoEmpresa") & " and status = 1 "

        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAvisoex.Text = " La extremidad ha sido eliminada"
            PanelAvisosex.Visible = True
            'PanelAvisosex.Focus()

        Else
            LblMensajeCriticoex.Text = clsDatos.MensajeError
            PanelCriticoex.Visible = True
            'PanelCriticoex.Focus()
        End If
    End Sub
    Protected Sub BtnNoex_Click(sender As Object, e As EventArgs)
        cargaextremidades()
    End Sub
    Private Sub ocultarPanelesex()
        PanelAdvertenciaex.Visible = False
        PanelAvisosex.Visible = False
        PanelCriticoex.Visible = False
        PanelDesicionex.Visible = False
    End Sub
    'Proceso de la recetamedica
    'Consulta principal para llenar el list de medicamentos
    Sub ConsultaMedicamentos()
        Dim strsql As String
        Dim funciones As New FuncionesGenerales
        ListMedicamentos.Items.Clear()

        strsql = "select CodigoMedicamento, descripcion, DosisRecomendada, Presentacion from catmedicamentos order by descripcion"


        If funciones.llenalistbox(strsql, ListMedicamentos) = -1 Then
            LblMensajeCriticoReceta.Text = funciones.MensajeError
            PanelCriticoReceta.Visible = True
            PanelCriticoReceta.Focus()

        Else
        End If
    End Sub
    ' Llenar los txt de acuerdo a la seleccion de del list de medicamentos
    Protected Sub ListMedicamentos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListMedicamentos.SelectedIndexChanged

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = "select CodigoMedicamento,cs.descripcion as dsales,cm.descripcion, DosisRecomendada, Presentacion from catmedicamentos cm" & _
        " left join catSales cs on cs.CodigoSal = cm.CodigoSal where CodigoMedicamento =" & _
        " " & ListMedicamentos.SelectedValue

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                txtObservaReceta.Text = dt.Rows(0).Item("DosisRecomendada").trim
                txtsalreceta.Value = dt.Rows(0).Item("dsales").trim
                hfTemporal.Value = dt.Rows(0).Item("dsales").trim & "¬" & dt.Rows(0).Item("DosisRecomendada").trim & "¬" & dt.Rows(0).Item("presentacion").trim
                txtalergiareceta.Text = hftemalergia.Value
            Else
            End If
        End If
    End Sub
    'Boton para Agregar el medicamento en la Receta
    Protected Sub btareceta_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles btareceta.Click

        If ListMedicamentos.SelectedValue.Trim > "" Then
            If Trim(txtUnidades.Value) > "" Then
                If Trim(txtObservaReceta.Text) > "" Then

                    Dim funciones As New FuncionesGenerales
                    Dim miMedicamento As String = ""

                    Dim sSales As String = funciones.entry(1, hfTemporal.Value, "¬")
                    Dim sEmpaque As String = funciones.entry(3, hfTemporal.Value, "¬")


                    miMedicamento = "* " & txtUnidades.Value & "   " & ListMedicamentos.Items(ListMedicamentos.SelectedIndex).Text.Trim & "  ("

                    If sSales.Trim <> "" Then
                        miMedicamento = miMedicamento & " " & sSales & ","
                    End If

                    If sEmpaque.Trim <> "" Then
                        miMedicamento = miMedicamento & "  " & sEmpaque & " --"
                    End If

                    miMedicamento = miMedicamento & "  " & txtObservaReceta.Text & " ) "

                    txtAplicacionMedica.Text = txtAplicacionMedica.Text + Chr(13) + miMedicamento
                    llenaGridMedicamentos(miMedicamento)
                    txtalergiareceta.Text = hftemalergia.Value
                Else
                    LblMensajeAdvertenciaReceta.Text = " Por favor, agregue la dosis en observaciones."
                    PanelAdvertenciaReceta.Visible = True
                    PanelAdvertenciaReceta.Focus()
                    Exit Sub
                End If
            Else
                LblMensajeAdvertenciaReceta.Text = " Por favor, agregue la unidad del medicamento."
                PanelAdvertenciaReceta.Visible = True
                PanelAdvertenciaReceta.Focus()
                Exit Sub

            End If
        Else
            LblMensajeAdvertenciaReceta.Text = " Por favor, seleccione un medicamento."
            PanelAdvertenciaReceta.Visible = True
            PanelAdvertenciaReceta.Focus()
            Exit Sub
        End If
        txtUnidades.Value = 1

    End Sub
    Sub llenaGridMedicamentos(ByVal miObservacion As String)

        Dim funciones As New FuncionesGenerales
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strsql As String


        If Trim(txtUnidades.Value) > "" Then


            If Trim(txtsalreceta.Value) > "" Then


                strsql = ""
                strsql = "INSERT INTO HmRecetaMedica (FolioConsulta, CodigoMedicamento, Cantidad, Indicaciones,status,CodigoUsuario,FechaActualizacion "
                strsql = strsql & ") VALUES ('" & Hffolioconsulta.Value & "', " & ListMedicamentos.SelectedValue & ", "
                strsql = strsql & txtUnidades.Value.ToString.Trim & ", " & "'" & miObservacion.Trim & "' ,1," & Session("codigoUsuario") & " ,GETDATE())"
                clsdatos.cargaComando(strsql)
                clsdatos.ejecutar()


                strsql = "select CodigoMedicamento,Cantidad,Indicaciones as descripcion FROM HmRecetaMedica WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' order by FolioReceta"

                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    gvReceta.Columns(0).Visible = False
                    gvReceta.Columns(1).Visible = False
                    gvReceta.Columns(2).Visible = True
                    gvReceta.DataSource = dt
                    gvReceta.DataBind()

                Else
                    LblMensajeCriticoReceta.Text = clsdatos.MensajeError
                    PanelCriticoReceta.Visible = True
                    PanelCriticoReceta.Focus()
                End If

            Else
                MsgBox("Indique la cantidad.")
            End If
        Else
            MsgBox("Indique la cantidad")
        End If


        txtUnidades.Value = 1
        txtsalreceta.Value = ""
        txtObservaReceta.Text = ""
        txtalergiareceta.Text = ""

    End Sub
    Sub llenareceta()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        strsql = "select FolioReceta,CodigoMedicamento,indicaciones from HmRecetaMedica where FolioConsulta='" & Hffolioconsulta.Value & "' order by FolioReceta"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            txtAplicacionMedica.Text = ""
            If dt.Rows.Count > 0 Then


                For Each fila As DataRow In dt.Rows
                    txtAplicacionMedica.Text = txtAplicacionMedica.Text + Chr(13) + (fila).Item("indicaciones").trim

                Next

            Else
            End If
        End If


        strsql = "select CodigoMedicamento,Cantidad,Indicaciones as descripcion FROM HmRecetaMedica WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' order by FolioReceta"

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            gvReceta.Columns(0).Visible = False
            gvReceta.Columns(1).Visible = False
            gvReceta.Columns(2).Visible = True
            gvReceta.DataSource = dt
            gvReceta.DataBind()

        Else
            LblMensajeCriticoReceta.Text = clsdatos.MensajeError
            PanelCriticoReceta.Visible = True
            'PanelCriticoReceta.Focus()
        End If
    End Sub

    Private Sub ocultarPanelesReceta()
        PanelAdvertenciaReceta.Visible = False
        PanelAvisosReceta.Visible = False
        PanelCriticoReceta.Visible = False
        PanelDesicionReceta.Visible = False
    End Sub

    Protected Sub gvReceta_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gvReceta.ItemCommand

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos

        Dim fila As Integer = e.CommandArgument
        Dim codeditmed As Integer = gvReceta.Items(fila).Cells(0).Text()

        If e.CommandName = "borrarmedicamento" Then
            LblMostarDecisionReceta.Text = " ¿Deseas eliminar el medicamento seleccionado?"
            PanelDesicionReceta.Visible = True
            'PanelDesicionReceta.Focus()
            HdPreguntasRecetaedit.Value = "0"
            HdIndexRecetaedit.Value = codeditmed
        End If

    End Sub
    Protected Sub BtnSiReceta_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasRecetaedit.Value
            Case "0"

                EliminarMedicamento()
                llenareceta()
        End Select
    End Sub
    Private Sub EliminarMedicamento()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable


        strSQL = "delete HmRecetaMedica where FolioConsulta='" & Hffolioconsulta.Value & "' and CodigoMedicamento='" & HdIndexRecetaedit.Value & "'"

        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAvisoReceta.Text = " El medicamento se ha eliminado."
            PanelAvisosReceta.Visible = True
            'PanelAvisosReceta.Focus()
            btagregarnj.Visible = True

        Else
            LblMensajeCriticoReceta.Text = clsDatos.MensajeError
            PanelCriticoReceta.Visible = True
            'PanelCriticoReceta.Focus()
        End If
    End Sub
    Protected Sub BtnNoReceta_Click(sender As Object, e As EventArgs)
        llenareceta()
    End Sub

    ' Inicio de proceso de justificaciones medica
    'Consulta principal para llenar el list de Justificaciones
    Sub ConsultaJustificaciones()
        Dim strsql As String
        Dim funciones As New FuncionesGenerales
        ListJustificacion.Items.Clear()

        strsql = "select CodigoJustificacion, descripcion from CatJustificaciones order by descripcion"


        If funciones.llenalistbox(strsql, ListJustificacion) = -1 Then
            LblMensajeCriticoJusti.Text = funciones.MensajeError
            PanelCriticoJusti.Visible = True
            'PanelCriticoJusti.Focus()

        Else
        End If
    End Sub

    Protected Sub ListJustificacion_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles ListJustificacion.SelectedIndexChanged

        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String

        strsql = "SELECT descripcion,contenido FROM CatJustificaciones WHERE CodigoJustificacion = " & ListJustificacion.SelectedValue

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                txtdescripcionj.Value = dt.Rows(0).Item("descripcion").trim
                txtJustificacion.Text = dt.Rows(0).Item("contenido").trim
                'GuardarJustificacion()
                'llenaGridJustificaciones()
                Updatejustificacion.Visible = False
                gjustificacion.Visible = True
                btnuevojustificacion.Visible = False
                btagregarnj.Visible = False


            Else
            End If
        End If
    End Sub
    Protected Sub gjustificacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles gjustificacion.Click
        GuardarJustificacion()
        llenaGridJustificaciones()
    End Sub

    Sub GuardarJustificacion()

        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If txtdescripcionj.Value = "" Then
            LblMensajeAdvertenciaJusti.Text = " Por favor, agregue la descripción de la nueva justificación."
            PanelAdvertenciaJusti.Visible = True
            PanelAdvertenciaJusti.Focus()
            Exit Sub
        Else
            If txtJustificacion.Text = "" Then
                LblMensajeAdvertenciaJusti.Text = " Por favor, agregue el contenido de la nueva justificación."
                PanelAdvertenciaJusti.Visible = True
                PanelAdvertenciaJusti.Focus()
                Exit Sub

            Else

                strsql = "select FolioConsulta,DJustificacion FROM HmJustificaciones " & _
                        " WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' and DJustificacion = '" & txtdescripcionj.Value & "'"
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        foliocj.Value = dt.Rows(0).Item("FolioConsulta").trim
                        djustificacionc.Value = dt.Rows(0).Item("DJustificacion").trim


                        If djustificacionc.Value = txtdescripcionj.Value Then
                            LblMensajeAdvertenciaJusti.Text = " La justificación que desea agregar ya existe en el catálogo."
                            PanelAdvertenciaJusti.Visible = True
                            PanelAdvertenciaJusti.Focus()
                            Exit Sub
                        Else
                        End If
                    Else


                        strsql = ""
                        strsql = "INSERT INTO HmJustificaciones (FolioConsulta, DJustificacion, CJustificacion, FechaJustificacion,CodigoUsuario,CodigoEmpresa,status,FechaActualizacion "
                        strsql = strsql & ") VALUES ('" & Hffolioconsulta.Value & "','" & txtdescripcionj.Value & "','" & txtJustificacion.Text & "','" & lblfjustificacion.Text & "', "
                        strsql = strsql & Session("codigoUsuario") & ",1,1 ,GETDATE())"
                        clsdatos.cargaComando(strsql)

                        If clsdatos.ejecutar() = 0 Then
                            btagregarnj.Visible = True
                            gjustificacion.Visible = False
                        Else
                            LblMensajeCriticoJusti.Text = clsdatos.MensajeError
                            PanelCriticoJusti.Visible = True
                            PanelCriticoJusti.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub
    Protected Sub btnuevojustificacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnuevojustificacion.Click
        GuardarJustificacionynuevaJustificacion()
        llenaGridJustificaciones()
    End Sub

    Sub GuardarJustificacionynuevaJustificacion()

        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If txtdescripcionj.Value = "" Then
            LblMensajeAdvertenciaJusti.Text = " Por favor, agregue la descripción de la nueva justificación"
            PanelAdvertenciaJusti.Visible = True
            'PanelAdvertenciaJusti.Focus()
            Exit Sub
        Else
            If txtJustificacion.Text = "" Then
                LblMensajeAdvertenciaJusti.Text = " Por favor, agregue el contenido de la nueva justificación."
                PanelAdvertenciaJusti.Visible = True
                PanelAdvertenciaJusti.Focus()
                Exit Sub

            Else

                strsql = "select Descripcion FROM CatJustificaciones " & _
                       " WHERE  Descripcion = '" & txtdescripcionj.Value & "'"
                If clsdatos.cargatabla(strsql, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        djustificacionnuevo.Value = dt.Rows(0).Item("Descripcion").trim


                        If djustificacionnuevo.Value = txtdescripcionj.Value Then
                            LblMensajeAdvertenciaJusti.Text = " La justificación que desea agregar ya existe en el catálogo."
                            PanelAdvertenciaJusti.Visible = True
                            PanelAdvertenciaJusti.Focus()
                            Exit Sub
                        Else
                        End If
                    Else


                        strsql = ""
                        strsql = "INSERT INTO CatJustificaciones (Descripcion, Contenido, status "
                        strsql = strsql & ") VALUES ('" & txtdescripcionj.Value & "','" & txtJustificacion.Text & "',1 )"
                        clsdatos.cargaComando(strsql)
                        strsql = ""
                        strsql = "INSERT INTO HmJustificaciones (FolioConsulta, DJustificacion, CJustificacion, FechaJustificacion,CodigoUsuario,CodigoEmpresa,status,FechaActualizacion "
                        strsql = strsql & ") VALUES ('" & Hffolioconsulta.Value & "', '" & txtdescripcionj.Value & "','" & txtJustificacion.Text & "','" & lblfjustificacion.Text & "', "
                        strsql = strsql & Session("codigoUsuario") & ",1,1 ,GETDATE())"
                        clsdatos.cargaComando(strsql)


                        If clsdatos.ejecutar() = 0 Then
                            ConsultaJustificaciones()
                            gjustificacion.Visible = False
                            Updatejustificacion.Visible = False
                            btnuevojustificacion.Visible = False
                            btagregarnj.Visible = True
                        Else
                            LblMensajeCriticoJusti.Text = clsdatos.MensajeError
                            PanelCriticoJusti.Visible = True
                            PanelCriticoJusti.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub

    Sub llenaGridJustificaciones()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable



        strsql = "select FolioJustificacion, DJustificacion as descripcionj FROM HmJustificaciones " & _
                        " WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' order by foliojustificacion desc"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            DGjustificaciones.Columns(0).Visible = False
            DGjustificaciones.Columns(1).Visible = True
            DGjustificaciones.DataSource = dt
            DGjustificaciones.DataBind()


        Else
            LblMensajeCriticoJusti.Text = clsdatos.MensajeError
            PanelCriticoJusti.Visible = True
            'PanelCriticoJusti.Focus()
        End If
    End Sub

    Sub llenajustificacion()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable


        strsql = "select top 1 FolioJustificacion,DJustificacion, CJustificacion as descripcion, format(FechaJustificacion,'dd/MM/yyyy') as fecha FROM HmJustificaciones " & _
                         " WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' order by foliojustificacion desc"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            txtJustificacion.Text = ""
            If dt.Rows.Count > 0 Then
                txtdescripcionj.Value = dt.Rows(0).Item("DJustificacion").trim
                txtJustificacion.Text = dt.Rows(0).Item("descripcion").trim
                lblfjustificacion.Text = Left(dt.Rows(0).Item("Fecha"), 10)
                HFFolioJustificacion.Value = dt.Rows(0).Item("FolioJustificacion")

            Else
            End If
        End If
    End Sub
    'elimina la Justificacion o la despliega, depende del comandoVG

    Protected Sub DGjustificaciones_SelectedIndexChanged(sender As Object, e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles DGjustificaciones.ItemCommand
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        If e.CommandArgument = "" Then
            llenajustificacion()
        Else
            Dim fila As Integer = e.CommandArgument
            Dim codjustificacion As Integer = DGjustificaciones.Items(fila).Cells(0).Text()
            Select Case e.CommandName

                Case "borrarjusti"

                    If e.CommandName = "borrarjusti" Then
                        LblMostarDecisionJusti.Text = " ¿Deseas eliminar la justificación seleccionada?"
                        PanelDesicionJusti.Visible = True
                        'PanelDesicionJusti.Focus()
                        HdPreguntasJusti.Value = "0"
                        HdIndexJusti.Value = codjustificacion
                        Updatejustificacion.Visible = False
                        gjustificacion.Visible = False
                        btnuevojustificacion.Visible = False

                    End If

                Case "verjusti"
                    strsql = "select FolioJustificacion, DJustificacion ,CJustificacion as descripcion,format(FechaJustificacion,'dd/MM/yyyy') as fecha FROM HmJustificaciones " & _
                            " WHERE FolioConsulta ='" & Hffolioconsulta.Value & "' and FolioJustificacion='" + codjustificacion.ToString + "'"
                    If clsdatos.cargatabla(strsql, dt) = 0 Then
                        If dt.Rows.Count > 0 Then
                            FJustificacionUpdate.Value = dt.Rows(0).Item("FolioJustificacion").ToString
                            txtdescripcionj.Value = dt.Rows(0).Item("DJustificacion").trim
                            txtJustificacion.Text = dt.Rows(0).Item("descripcion").trim
                            lblfjustificacion.Text = Left(dt.Rows(0).Item("Fecha"), 10)

                            Updatejustificacion.Visible = True
                            gjustificacion.Visible = False
                            txtdescripcionj.EnableViewState = False
                            btnuevojustificacion.Visible = False
                            btagregarnj.Visible = False
                        Else
                        End If
                    End If
            End Select
        End If

    End Sub
    Protected Sub BtnSijusti_Click(sender As Object, e As EventArgs)
        Select Case HdPreguntasJusti.Value
            Case "0"

                EliminarJustificacion()
                llenaGridJustificaciones()
        End Select
    End Sub
    Private Sub EliminarJustificacion()
        Dim strSQL As String
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable

        strSQL = "delete HmJustificaciones where FolioConsulta='" & Hffolioconsulta.Value & "' and FolioJustificacion='" & HdIndexJusti.Value & "'"


        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
            LblMensajeAvisoJusti.Text = " La justificación se ha eliminado."
            PanelAvisosJusti.Visible = True
            'PanelAvisosJusti.Focus()
            txtJustificacion.Text = ""
            llenajustificacion()

        Else
            LblMensajeCriticoJusti.Text = clsDatos.MensajeError
            PanelCriticoJusti.Visible = True
            'PanelCriticoJusti.Focus()
        End If
    End Sub
    Protected Sub BtnNojusti_Click(sender As Object, e As EventArgs)
        llenaGridJustificaciones()
        llenajustificacion()
    End Sub
    Protected Sub Updatejustificacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Updatejustificacion.Click
        ActualizarJustificacion()
        llenaGridJustificaciones()
    End Sub

    Sub ActualizarJustificacion()
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim dt As New DataTable

        If txtdescripcionj.Value = "" Then
            LblMensajeAdvertenciaJusti.Text = " Modifique la descripción para actuzalizar la justificación."
            PanelAdvertenciaJusti.Visible = True
            'PanelAdvertenciaJusti.Focus()
            Exit Sub
        Else
            If txtJustificacion.Text = "" Then
                LblMensajeAdvertenciaJusti.Text = " Modifique el contenido para actualizar la justificación."
                PanelAdvertenciaJusti.Visible = True
                'PanelAdvertenciaJusti.Focus()
                Exit Sub

            Else
                strsql = " UPDATE HmJustificaciones SET CJustificacion = '" & txtJustificacion.Text & "', codigousuario = '" & Session("codigoUsuario") & "', fechaActualizacion = getdate()" & _
                    " WHERE FolioJustificacion = '" & FJustificacionUpdate.Value & "'"



                clsdatos.cargaComando(strsql)
                If clsdatos.ejecutar() = 0 Then
                    LblMensajeAvisoJusti.Text = " La justificación ha sido actualizada."
                    PanelAvisosJusti.Visible = True
                    'PanelAvisosJusti.Focus()
                    btagregarnj.Visible = True
                    Updatejustificacion.Visible = False

                Else
                    LblMensajeCriticoJusti.Text = clsdatos.MensajeError
                    PanelCriticoJusti.Visible = True
                    'PanelCriticoJusti.Focus()
                End If
            End If
        End If

    End Sub
    Private Sub ocultarPanelesJustificaciones()
        PanelAdvertenciaJusti.Visible = False
        PanelAvisosJusti.Visible = False
        PanelCriticoJusti.Visible = False
        PanelDesicionJusti.Visible = False
    End Sub

    Protected Sub btagregarnj_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btagregarnj.Click
        txtdescripcionj.Value = ""
        txtJustificacion.Text = ""
        gjustificacion.Visible = False
        Updatejustificacion.Visible = False
        btnuevojustificacion.Visible = True
        btagregarnj.Visible = False
    End Sub

    Sub Regresar()

        Dim consultorio, fecha As String
        consultorio = Session("codigoConsultorio")
        fecha = hfFechaAgenda.Value

        Session.Add("CodigoConsultorio", consultorio)
        Session.Add("FechaAgenda", fecha)
        Response.Redirect("agenda.aspx")
    End Sub



    Protected Sub btnnuevom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnnuevom.Click

        txtUnidades.Visible = False
        fhashtag.Visible = False
        gaddon.Visible = False
        lblcantidadReceta.Visible = False

        ListMedicamentos.Visible = False
        txtnombrenm.Visible = True

        txtsalreceta.Visible = False
        Listsalnm.Visible = True
        ConsultaSales()

        txtObservaReceta.Visible = False
        lblobservacionreceta.Visible = False
        txtdosisnm.Visible = True

        txtalergiareceta.Visible = False
        lblalergiaReceta.Visible = False
        txtpresentacionnm.Visible = True

        btnnuevom.Visible = False
        btareceta.Visible = False
        btnguardarnm.Visible = True
        actualizaReceta.Visible = True



    End Sub
    Protected Sub btnguardarnm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnguardarnm.Click

        Dim funciones As New FuncionesGenerales
        Dim clsdatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strsql As String


        If Trim(txtnombrenm.Value) > "" Then


            If Listsalnm.SelectedValue.Trim > "" Then


                If Trim(txtdosisnm.Value) > "" Then

                    If Trim(txtpresentacionnm.Value) > "" Then


                        strsql = ""
                        strsql = "INSERT INTO CatMedicamentos (CodigoSal, Descripcion, DosisRecomendada, Presentacion, Status, CodigoUsuario, FechaActualizacion "
                        strsql = strsql & ") VALUES ( " & Listsalnm.SelectedValue & ",'" & txtnombrenm.Value & "','" & txtdosisnm.Value & "','" & txtpresentacionnm.Value & "',1," & Session("codigoUsuario") & " ,GETDATE())"
                        clsdatos.cargaComando(strsql)
                        clsdatos.ejecutar()



                        txtUnidades.Visible = True
                        fhashtag.Visible = True
                        gaddon.Visible = True
                        lblcantidadReceta.Visible = True

                        ListMedicamentos.Visible = True
                        txtnombrenm.Visible = False

                        txtsalreceta.Visible = True
                        Listsalnm.Visible = False
                        ConsultaSales()

                        txtObservaReceta.Visible = True
                        lblobservacionreceta.Visible = True
                        txtdosisnm.Visible = False

                        txtalergiareceta.Visible = True
                        lblalergiaReceta.Visible = True
                        txtpresentacionnm.Visible = False

                        btnnuevom.Visible = True
                        btareceta.Visible = True
                        btnguardarnm.Visible = False
                        actualizaReceta.Visible = False

                        ConsultaMedicamentos()

                        '                strsql = "select top 1 CodigoMedicamento,cs.descripcion as dsales,cm.descripcion, DosisRecomendada, Presentacion from catmedicamentos cm" & _
                        '" left join catSales cs on cs.CodigoSal = cm.CodigoSal order by CodigoMedicamento desc"

                        '                If clsdatos.cargatabla(strsql, dt) = 0 Then
                        '                    If dt.Rows.Count > 0 Then
                        '                        ListMedicamentos.SelectedValue = dt.Rows(0).Item("CodigoMedicamento")
                        '                        txtObservaReceta.Text = dt.Rows(0).Item("DosisRecomendada").trim
                        '                        txtsalreceta.Value = dt.Rows(0).Item("dsales").trim
                        '                        hfTemporal.Value = dt.Rows(0).Item("dsales").trim & "¬" & dt.Rows(0).Item("DosisRecomendada").trim & "¬" & dt.Rows(0).Item("presentacion").trim
                        '                        txtalergiareceta.Text = hftemalergia.Value
                        '                    Else
                        '                        LblMensajeCriticoReceta.Text = clsdatos.MensajeError
                        '                        PanelCriticoReceta.Visible = True
                        '                        PanelCriticoReceta.Focus()
                        '                    End If

                        '                End If


                    Else
                        LblMensajeAdvertenciaReceta.Text = " Por favor, escriba la presentación del medicamento."
                        PanelAdvertenciaReceta.Visible = True
                        'PanelAdvertenciaReceta.Focus()

                    End If

                Else
                    LblMensajeAdvertenciaReceta.Text = " Por favor, escriba la dosis recomendada del medicamento."
                    PanelAdvertenciaReceta.Visible = True
                    'PanelAdvertenciaReceta.Focus()

                End If
            Else
                LblMensajeAdvertenciaReceta.Text = " Seleccione la formula correspondiente del medicamento."
                PanelAdvertenciaReceta.Visible = True
                'PanelAdvertenciaReceta.Focus()

            End If
        Else
            LblMensajeAdvertenciaReceta.Text = " Escriba el nombre del medicamento."
            PanelAdvertenciaReceta.Visible = True
            'PanelAdvertenciaReceta.Focus()

        End If






    End Sub

    Sub ConsultaSales()
        Dim strsql As String
        Dim funciones As New FuncionesGenerales
        ListMedicamentos.Items.Clear()

        strsql = "select CodigoSal, Descripcion from CatSales order by descripcion"


        If funciones.llenalistbox(strsql, Listsalnm) = -1 Then
            LblMensajeCriticoReceta.Text = funciones.MensajeError
            PanelCriticoReceta.Visible = True
            'PanelCriticoReceta.Focus()

        Else
        End If
    End Sub
    Protected Sub actualizaReceta_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles actualizaReceta.Click
        txtUnidades.Visible = True
        fhashtag.Visible = True
        gaddon.Visible = True
        lblcantidadReceta.Visible = True

        ListMedicamentos.Visible = True
        txtnombrenm.Visible = False

        txtsalreceta.Visible = True
        Listsalnm.Visible = False
        ConsultaSales()

        txtObservaReceta.Visible = True
        lblobservacionreceta.Visible = True
        txtdosisnm.Visible = False

        txtalergiareceta.Visible = True
        lblalergiaReceta.Visible = True
        txtpresentacionnm.Visible = False

        btnnuevom.Visible = True
        btareceta.Visible = True
        btnguardarnm.Visible = False
        actualizaReceta.Visible = False

        ConsultaMedicamentos()



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
                Lblnacionalidad.Text = dt.Rows(0).Item("nacionalidad")
                lblocupacion.Text = dt.Rows(0).Item("ocupacion")
                lbledad.Text = dt.Rows(0).Item("edad")
                strSQL = "SELECT C.idDoctorCabecera,(U.nombre+' '+U.primerApellido+' '+U.segundoApellido) as Medico  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConsultorios] as C " & _
                         "inner join [" & clsDatos.BaseDatos & "].[dbo].[CatUsuarios] as U on C.idDoctorCabecera=U.codigoUsuario" & _
                         "where C.codigoConsultorio='" & Session("codigoConsultorio") & "' and C.codigoEmpresa=" & Session("codigoEmpresa") & " "
                If clsDatos.cargatabla(strSQL, dt) = 0 Then
                    If dt.Rows.Count > 0 Then
                        LblDrAtiende.Text = dt.Rows(0).Item("Medico")
                    Else
                        LblDrAtiende.Text = "Sin asignación"
                    End If
                End If
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
                "where codigoPaciente='" & HFCodPaciente.Value & "' and E.codigoEmpresa='1' and A.tipoArchivo='Imagen' and E.status='1' " & _
                 "order by folioEstudio desc"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                img += "["
                previus = "["
                For Each fila As DataRow In dt.Rows
                    'img += "'" + "http://107.161.180.154/hortopedia/img/" + fila.Item("url") + "'"
                    img += "'../../archivos/radiografias/" + fila.Item("url") + "'"
                    previus += "{caption: " + Chr(34) + fila.Item("url").ToString + Chr(34) + ",size: " + Chr(34) + getTamFile(Session("UserFolder") + "\radiografias\" + fila.Item("url")) + Chr(34) + ", width: " + Chr(34) + "120px" + Chr(34) + ",url: " + Chr(34) + "Handler.ashx?f=" + fila.Item("url").ToString + "&codigoPaciente=" + HFCodPaciente.Value + "&codigoEmpresa=" + Session("codigoEmpresa").ToString + "&codigoUsuario=" + Session("codigoUsuario").ToString + Chr(34) + ", key: " + i.ToString + "}"
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

    Sub CargaVideo()
        Dim strSql As String
        Dim img As String = ""
        Dim previus As String = ""
        Dim dt As New DataTable
        Dim clsDatos As New ClaseDatos
        Dim i As Integer = 1
        Dim tipoArchivo As String = ""
        Dim type As String = ""
        strSql = "SELECT  A.tipoArchivo, E.folioEstudio,E.folioConsulta,E.CodigoPaciente,A.url,A.Descripcion,A.folioArchivo " & _
                 "FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudios] as E " & _
                "inner join [" & clsDatos.BaseDatos & "].[dbo].[HmArchivosEstudios] as A on A.folioEstudio=E.folioEstudio and A.status='1' " & _
                "where codigoPaciente='" & HFCodPaciente.Value & "' and E.codigoEmpresa='1'  AND A.tipoArchivo IN ('3gp', 'mp4', 'Office', 'pdf', 'doc', 'docx', 'jpg', 'jpeg', 'png', 'gif') AND E.status='1' " & _
                 "order by folioEstudio desc"
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                img += "["
                previus = "["
                For Each fila As DataRow In dt.Rows
                    If fila.Item("tipoArchivo") = "mp4" Then
                        tipoArchivo = "video/mp4"
                        type = "video"
                    End If
                    If fila.Item("tipoArchivo") = "3gp" Then
                        tipoArchivo = "video/3gp"
                        type = "video"
                    End If
                    If fila.Item("tipoArchivo") = "pdf" Then
                        tipoArchivo = "appliacation/pdf"
                        type = "pdf"
                    End If
                    If fila.Item("tipoArchivo") = "docx" Then
                        tipoArchivo = "application/vnd.openxmlformats-officedocument.wordprocessingml.document"
                        type = "application"
                    End If
                    If fila.Item("tipoArchivo") = "doc" Then
                        tipoArchivo = "application/msword"
                        type = "application"
                    End If
                    If fila.Item("tipoArchivo") = "jpg" Or fila.Item("tipoArchivo") = "jpeg" Or fila.Item("tipoArchivo") = "png" Or fila.Item("tipoArchivo") = "gif" Then
                        tipoArchivo = ""
                        type = "image"
                    End If
                    img += "'./archivos/documentos/" + fila.Item("url") + "'"

                    previus += "{type: """ & type & """, filetype: """ & tipoArchivo & """, caption: " + Chr(34) + fila.Item("url").ToString + Chr(34) + ",size: " + Chr(34) + getTamFile(Session("UserFolder") + "\documentos\" + fila.Item("url")) + Chr(34) + ", width: " + Chr(34) + "120px" + Chr(34) + ",url: " + Chr(34) + "HandlerVideo.ashx?f=" + fila.Item("url").ToString + "&codigoPaciente=" + HFCodPaciente.Value + "&codigoEmpresa=" + Session("codigoEmpresa").ToString + "&codigoUsuario=" + Session("codigoUsuario").ToString + Chr(34) + ", key: " + i.ToString + "}"
                    If i < dt.Rows.Count Then
                        img += ","
                        previus += ","
                        i = i + 1
                    End If

                Next
                img += "]"
                previus += "]"
                video.Value = img
                PreviusVideos.Value = previus

            Else
                Video.Value = "[]"
                PreviusVideos.Value = "{}"
            End If
        Else
            MsgBox("Error: " + clsDatos.MensajeError, MsgBoxStyle.Critical)
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
                                  "(FolioEstudio,FolioConsulta,CodigoEstudioGabinete,CodigoPaciente,usuarioSolicita,codigoUsuario,status,codigoEmpresa,codigoEstudio,fechaActualizacion) " & _
                "values(" & folioEstudio & ",'" & Session("folioConsulta") & "',1,'" & Session("CodigoPaciente") & "',0,'" & Session("codigoUsuario") & "',1," & Session("codigoEmpresa") & ",'" & Session("codigoUsuario") & "',convert(datetime,'" & Format(Date.Now, "yyyy-MM-dd hh:mm:ss") & "', 20)) "
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

    '16022017
    Sub cargaListaDeEstudiosRX()  ' ++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim funciones As New FuncionesGenerales
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT codigoEstudio, (zona+' '+estudio) as estudio  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatEstudios] " & _
                 "where codigoEstudioGab = 1 And Empresa = " & Session("codigoEmpresa") & " And status = 1 " & _
                " order by Zona"
        If funciones.llenalistbox(strSql, LstEstudiosRX) = 0 Then

        End If
    End Sub

    Sub insertListEstudioRX()      '+++++++++++++++++++++++++++++++++++++++++++++++++++
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        For Each li As ListItem In LstEstudiosRX.Items
            If li.Selected = True Then
                strSql = "  insert into [" & clsDatos.BaseDatos & "].[dbo].[HmEstudiosDet] " & _
               "(folioConsulta ,CodigoEstudio,codigoEmpresa,extremidad,fechaActualizacion,status) " & _
              " values('" & Session("folioConsulta") & "'," & li.Value & "," & Session("codigoEmpresa") & " ,'" & RbtListExtremidades.SelectedItem.ToString & "',getdate(),1)"
                clsDatos.cargaComando(strSql)
                clsDatos.ejecutar()
            End If
        Next
    End Sub

    Sub leeListEstudioRX()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSql As String
        strSql = "SELECT codigoEstudio,extremidad " & _
                  "FROM [" & clsDatos.BaseDatos & "].[dbo].[HmEstudiosDet] " & _
                   "where folioConsulta='" & Session("folioConsulta") & "' and status=1 and codigoEmpresa=" & Session("codigoEmpresa") & ""
        If clsDatos.cargatabla(strSql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                For x = 0 To dt.Rows.Count - 1
                    For Each li As ListItem In LstEstudiosRX.Items
                        If li.Value = dt.Rows(x).Item("codigoEstudio") Then
                            li.Selected = True
                        End If
                    Next
                Next
            End If
        End If

    End Sub
   Protected Sub cmdimprimirexp_Click(sender As Object, e As EventArgs)

        Dim mireporte As New ReportDocument
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim dt As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String


        mireporte.Load(Server.MapPath("/reportes/ExpedienteMedico.rpt"))


        strsql = "  select codigoConsultorio, descripcionConsultorio " & _
                  " from  CatConsultorios" & _
                  " where codigoConsultorio = '" & Session("codigoConsultorio") & "'"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Mivar.Value = dt.Rows(0).Item("descripcionConsultorio")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("descripcionc").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                'PanelCritico.Focus()
            End If

        End If

        strsql = "  select codigoEmpresa, descripcion, razonSocial, rfc, calle, NoExterior, NoInterior, colonia, " & _
                    " municipio, ciudad, estado, codigoPostal, pais from CatEmpresas" & _
                    " where codigoEmpresa = '" & Session("codigoEmpresa") & "'"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                Mivar.Value = dt.Rows(0).Item("razonSocial")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("descripcione").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("rfc")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("rfce").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("calle")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("callee").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("NoExterior")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("nexteriore").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("colonia")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("coloniae").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("codigoPostal")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("cpe").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("municipio")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("municipioe").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("estado")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("estadoe").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("pais")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("paise").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

            Else
                LblMensajeCritico.Text = clsdatos.MensajeError
                PanelCritico.Visible = True
                'PanelCritico.Focus()
            End If

        End If



        Dim sindatos As String
        sindatos = " "

        strsql = " select d.descripcion as descripciond " & _
          " from agagenda a " & _
          " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa " & _
          " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
          " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
          " where a.folioconsulta = '" & Hffolioconsulta.Value & "' and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1"
        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Mivar.Value = dt.Rows(0).Item("descripciond")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("descripciond").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            Else
                Mivar.Value = sindatos
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("descripciond").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            End If
        End If
        Mivar.Value = lblpaciente.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = lbledad.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("edad").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = lblocupacion.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("ocupacion").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Lblfechanacimiento.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fnacimiento").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = LblDrAtiende.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("doctor").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Lbldireccion.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("direccion").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        Mivar.Value = lblfagenda.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("fconsulta").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Lblfolioconsulta.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("folioc").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = TxtTension.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("tension").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Txtfrecuencia.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("frecuencia").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = Txtpeso.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("peso").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txttalla.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("talla").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = TxtImc.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("imc").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()



        Mivar.Value = txtPadecimientoActual.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("padecimientos").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtPatologicos.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("patologicos").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtNoPatologicos.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("nopatologicos").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtAlergias.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("alergias").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        Mivar.Value = txtExploracion.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("exploracion").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()

        Mivar.Value = txtPlan.Text.Trim
        rpDatos.Add(Mivar)
        mireporte.DataDefinition.ParameterFields("plan").ApplyCurrentValues(rpDatos)
        rpDatos.Clear()


        Try
            Dim nomArchivo As String = lblpaciente.Text + Replace(DDHfecha.Items(DDHfecha.SelectedIndex).Text, "/", "")
            nomArchivo.Replace(" ", "")
            Dim filedest As New CrystalDecisions.Shared.DiskFileDestinationOptions
            Dim o As CrystalDecisions.Shared.ExportOptions
            o = New CrystalDecisions.Shared.ExportOptions
            o.ExportFormatType = CrystalDecisions.Shared.ExportFormatType.PortableDocFormat
            o.ExportDestinationType = CrystalDecisions.Shared.ExportDestinationType.DiskFile
            filedest.DiskFileName = Server.MapPath("reportes") & "\" + nomArchivo + ".pdf"
            o.ExportDestinationOptions = filedest.Clone
            mireporte.Export(o)
            filedest = Nothing
            o = Nothing
            mireporte.Close()
            Response.Write("<script type='text/javascript'>detailedresults=window.open('ImpExReporte.aspx?doc=" & nomArchivo & ".pdf');</script>")
        Catch ex As Exception
            LblMensajeCritico.Text = "Error 1: " + clsdatos.MensajeError
            PanelCritico.Visible = True
        End Try

      
    End Sub
    Sub cargaDesIMC()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        If txttalla.Text <> "" And Txtpeso.Text <> "" Then
            strSQL = " SELECT descripcion  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatIMC] " & _
                " WHERE inicio<=" & TxtImc.Text & " and fin>=" & TxtImc.Text & ""
            If clsDatos.cargatabla(strSQL, dt) = 0 Then
                If dt.Rows.Count > 0 Then
                    imc_resultado.InnerText = dt.Rows(0).Item("descripcion")
                Else
                    imc_resultado.InnerText = ""
                End If
            Else
                imc_resultado.InnerText = ""
            End If
        Else
            imc_resultado.InnerText = ""
        End If
    End Sub

    Sub GuardarIngresoCajas()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim pago As String
        If HdEliminaPago.Value = 1 Then
            pago = CDbl(Replace(LblPagoTotal.Text, ",", "")) - CDbl(HdCantidad_a_descontar.Value)
            HdEliminaPago.Value = 0
        Else
            pago = Replace(LblPagoTotal.Text, ",", "")
        End If

        strSQL = "select * from [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] " & _
                 "where folioConsulta='" & Session("folioConsulta") & "' and codigoEmpresa=" & Session("codigoEmpresa") & " and status=1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] set importePago=" & pago & " " & _
                      "where folioConsulta='" & Session("folioConsulta") & "' and codigoEmpresa=" & Session("codigoEmpresa") & " and status=1 "
            Else
                strSQL = "  INSERT INTO [" & clsDatos.BaseDatos & "].[dbo].[IngresosCaja] (FolioConsulta,codigoUsuario,fechaIngreso,importePago,Referencia,Status,codigoFormaPago,Codigoconsultorio,CodigoEmpresa,conciliado,PagoRealizado,facturado,Abono) " & _
             "VALUES ('" & Session("folioConsulta") & "'," & Session("codigoUsuario") & ",'" & Session("FechaAgenda") & "'," & pago & ",'00000',1,99," & Session("codigoConsultorio") & "," & Session("codigoEmpresa") & ",0,0,0,0)"
            End If
        End If
    
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar() = 0 Then
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If

    End Sub

    Sub DatosPaciente()
        Response.Redirect("DatosPaciente.aspx?modulo=1&codigoPaciente=" + CType(Session("codigoPaciente"), Double).ToString)
    End Sub

    Sub Deshabilitar()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT FolioConsulta, CodigoEtapa  FROM [" & clsDatos.BaseDatos & "].[dbo].[AgAgenda] WHERE FolioConsulta = '" & Hffolioconsulta.Value & "'"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows(0).Item("CodigoEtapa") = "10" Then
                    'Botones
                    finalizar1.Visible = False
                    finalizar2.Visible = False
                    'guardar1.Visible = False
                    'guardar2.Visible = False
                    'guardar_regresar1.Visible = False
                    'guardar_regresar2.Visible = False

                    'Diagnosticos
                    contenedor_diagnosticos.Visible = False
                    Dgdiagnosticos.Columns(4).Visible = False

                    'Expediente
                    txtPadecimientoActual.Enabled = False
                    txtAntecedentes.Enabled = False
                    txtTratamientoPrevio.Enabled = False
                    txtPatologicos.Enabled = False
                    txtNoPatologicos.Enabled = False
                    txtAlergias.Enabled = False
                    txtExploracion.Enabled = False
                    txtPlan.Enabled = False

                    'Mediciones
                    TxtTension.Enabled = False
                    Txtfrecuencia.Enabled = False
                    Txtpeso.Enabled = False
                    txttalla.Enabled = False
                    TxtImc.Enabled = False

                    'Motivo de consulta
                    contenedor_extremidades.Visible = False

                    'Receta Médica
                    'contenedor_receta.Visible = False

                    'Justificaciones
                    'contenedor_justificaciones.Visible = False

                    'Pagos
                    contenedor_pagos_cantidad.Visible = False
                    LstConceptoPagos.Visible = False
                    GdvConceptoPagos.Columns(5).Visible = False

                End If
            Else
                'Error en la consulta
            End If
        End If
    End Sub

End Class