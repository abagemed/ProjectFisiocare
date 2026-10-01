
Imports System.Net
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Web
Imports CrystalDecisions.Shared
Imports CrystalDecisions.ReportAppServer
Imports CrystalDecisions.ReportSource
Imports CrystalDecisions.Reporting
Imports System.Text
Imports System.IO
Imports System.Data

Partial Class expedientemedico
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
       
        If Not (Page.IsPostBack) Then
            'realizado.Visible = True
            'cancelado.Visible = False
            'misbotones.Visible = True
            'cmbTipoterapia.Visible = True
            'chkcierre.Visible = True
            'modatos.Visible = False
            If (Context.Items("elidCita") <> "") Then
                lblIdcita.Text = Context.Items("elidCita").ToString.Trim
                hfposicion.Text = Context.Items("posicion").ToString.Trim
                hfturno.Text = Context.Items("turno").ToString.Trim
                hfFechaAgenda.Text = Session("FechaAgenda")

            ElseIf (Context.Request("elidCita") <> "") Then
                lblIdcita.Text = Context.Request("elidCita")
                hfposicion.Text = Context.Request("posicion")
                hfturno.Text = Context.Request("turno")
                hfFechaAgenda.Text = Context.Request("fecha")

            End If
           
            ''lblIdcliente.Text =
           


        End If

        'Dim handlerUrl As String = "http://107.161.180.154/admonmaster/expedientemedico.ashx?name=expedientemedico"
        'Dim response As String = (New WebClient()).DownloadString(handlerUrl)

    End Sub
    Protected Sub cmdimprimirHistorialexp_Click(sender As Object, e As EventArgs)

        ''// Inserta imagen en CR AB 08/08/2018

        Dim mireporte As New ReportDocument
        Dim rpDatos As New CrystalDecisions.Shared.ParameterValues
        Dim Mivar As New CrystalDecisions.Shared.ParameterDiscreteValue
        Dim imgrpt As New CrystalDecisions.Shared.ParameterFields
        Dim dt As New DataTable
        Dim dtDL As New DataTable
        Dim clsdatos As New ClaseDatos
        Dim strsql As String
        Dim strsqlDL As String

        'Dim DirectorioLogo As String = ""

        'strsqlDL = "  select codigoEmpresa, directoriologo from CatEmpresas" & _
        '            " where codigoEmpresa = '" & Session("codigoEmpresa") & "'"
        'If clsdatos.cargatabla(strsqlDL, dtDL) = 0 Then
        '    If dtDL.Rows.Count > 0 Then
        '        DirectorioLogo = dtDL.Rows(0).Item("directoriologo")
        '    Else
        '        'LblMensajeCritico.Text = clsdatos.MensajeError
        '        'PanelCritico.Visible = True
        '    End If

        'End If

        mireporte.Load(Server.MapPath("/reportes/ExpedienteHistorialMedico.rpt"))


        'Dim columna As New DataColumn("codigoEmpresa")
        'dt.Columns.Add(columna)
        'Dim col2 As New DataColumn("descripcion")
        'dt.Columns.Add(col2)
        'Dim col3 As New DataColumn("razonSocial")
        'dt.Columns.Add(col3)
        'Dim col4 As New DataColumn("rfc")
        'dt.Columns.Add(col4)
        'dt.Columns.Add(New DataColumn("img", GetType(Byte())))



        'Dim fs As FileStream = New FileStream(Server.MapPath(DirectorioLogo), FileMode.Open)
        'Dim br As BinaryReader = New BinaryReader(fs)
        'Dim imagen(CInt(fs.Length)) As Byte

        'br.Read(imagen, 0, CInt(fs.Length))
        'br.Close()
        'fs.Close()

        'Dim Empresas As Boolean
        'Dim dtEmpresas As DataTable
        'Dim contador As Int32 = 0
        'Dim cuenta As Int32 = 0

        'If ConsultasEmpresa(dtEmpresas) Then
        '    Empresas = True
        'Else
        '    Empresas = False
        'End If

        'Do While contador = cuenta

        '    If Empresas = True Then

        '        For Each fila As DataRow In dtEmpresas.Rows
        '            Dim concepto1 As New Concepto()
        '            Dim nFila As DataRow
        '            nFila = dt.NewRow
        '            nFila(0) = fila.Item("codigoEmpresa")
        '            nFila(1) = fila.Item("descripcion")
        '            nFila(2) = fila.Item("razonSocial")
        '            nFila(3) = fila.Item("rfc")
        '            nFila(4) = imagen
        '            dt.Rows.Add(nFila)
        '        Next

        '    End If

        '    contador = contador + 1
        'Loop

        'mireporte.SetDataSource(dt.DefaultView)

        ''//Fin Modificaion AB 08/08/2018

        'strsql = " select C.codigoConsultorio, (U.nombre +' '+U.primerApellido+' '+U.segundoApellido) as descripcionConsultorio,U.cedula,U.especialidad " & _
        '          "from  [" & clsdatos.BaseDatos & "].[dbo].CatConsultorios as C " & _
        '          "inner join [" & clsdatos.BaseDatos & "].[dbo].[CatUsuarios] as U on C.idDoctorCabecera=U.codigoUsuario " & _
        '          " where codigoConsultorio = '" & Session("codigoConsultorio") & "' and C.codigoEmpresa=" & Session("codigoEmpresa") & ""
        'If clsdatos.cargatabla(strsql, dt) = 0 Then
        '    If dt.Rows.Count > 0 Then
        '        Mivar.Value = dt.Rows(0).Item("descripcionConsultorio")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("descripcionc").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()
        '        Mivar.Value = dt.Rows(0).Item("cedula")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("cedulaProfesional").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()
        '        Mivar.Value = dt.Rows(0).Item("especialidad")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("especialidad").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()
        '    Else
        '        'LblMensajeCritico.Text = clsdatos.MensajeError
        '        'PanelCritico.Visible = True
        '        'PanelCritico.Focus()
        '    End If

        'End If

        'strsql = "  select codigoEmpresa, descripcion, razonSocial, rfc, calle, NoExterior, NoInterior, colonia, " & _
        '            " municipio, ciudad, estado, codigoPostal, pais from CatEmpresas" & _
        '            " where codigoEmpresa = '" & Session("codigoEmpresa") & "'"
        'If clsdatos.cargatabla(strsql, dt) = 0 Then
        '    If dt.Rows.Count > 0 Then

        '        Mivar.Value = dt.Rows(0).Item("razonSocial")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("descripcione").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("rfc")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("rfce").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("calle")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("callee").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("NoExterior")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("nexteriore").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("colonia")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("coloniae").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("codigoPostal")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("cpe").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("municipio")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("municipioe").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("estado")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("estadoe").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '        Mivar.Value = dt.Rows(0).Item("pais")
        '        rpDatos.Add(Mivar)
        '        mireporte.DataDefinition.ParameterFields("paise").ApplyCurrentValues(rpDatos)
        '        rpDatos.Clear()

        '    Else
        '        LblMensajeCritico.Text = clsdatos.MensajeError
        '        PanelCritico.Visible = True
        '        PanelCritico.Focus()
        '    End If

        'End If



        Dim sindatos As String
        sindatos = " "

        'strsql = " select d.descripcion as descripciond,IdHmd,h.FechaActualizacion  " & _
        '  " from agagenda a " & _
        '  " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa and tipoDiagnostico='PREDEFINIDO' " & _
        '  " inner join CatDiagnosticosDet d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
        '  " d.codigolistaDiagnostico = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
        '  " where a.codigoPaciente=" & Session("codigoPaciente") & " and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1 " & _
        '  " union all" & _
        '  " select d.descripcion as descripciond,IdHmd,h.FechaActualizacion  " & _
        '  " from agagenda a " & _
        '  " inner join hmdiagnosticosdet h on h.folioconsulta = a.folioconsulta and h.codigoempresa = a.CodigoEmpresa and tipoDiagnostico='PERSONAL' " & _
        '  " inner join CatDiagnosticosPersonales d on d.codigoDiagnostico = h.codigodiagnostico and  " & _
        '  " d.codigoConsultorio = h.codigolistadiagnostico and (d.codigoempresa = h.codigoempresa or d.codigoempresa = 0)" & _
        '  " where a.codigoPaciente=" & Session("codigoPaciente") & " and a.codigoempresa = '" & Session("codigoEmpresa") & "' and h.status = 1 " & _
        '" order by IdHmD desc"

        'strsql = " select d.descripcion as descripciond,IdHDiagnostico,h.FechaActualizacion " & _
        '   "from agenda a " & _
        '   "inner join HMdiagnosticos h on h.idcita = a.idcita " & _
        '   "inner join CatDiagnosticos d on d.idDiagnostico = h.idDiagnostico " & _
        '   "where a.idcliente=6806 and h.status = 'True' " & _
        '   "order by IdHDiagnostico desc "

        strsql = " select HD.IdHDiagnostico as IdHDiagnostico ,HD.IdDiagnostico as IdDiagnostico , CD.Descripcion as descripciond , " & _
"isnull(FORMAT(HD.FechaActualizacion , 'dd MMMM yyyy'),FORMAT(A.Fecha , 'dd MMMM yyyy')) as fechaActualizacion " & _
"from HMDiagnosticos HD " & _
"left join agenda A  on A.IdCita = HD.idCita " & _
"inner join CatDiagnosticos CD on CD.IdDiagnostico = HD.IdDiagnostico " & _
"where HD.idCliente  = 6806 and HD.Status = 'True'"


        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                If dt.Rows.Count > 1 Then
                    Dim diagnosticos As String = ""
                    Dim diagnosticoTipiado As String = ""
                    For Each i As DataRow In dt.Rows
                        diagnosticoTipiado = i.Item("descripciond")
                        diagnosticos += String.Format("{0,-50}", diagnosticoTipiado) + i.Item("fechaActualizacion") + vbLf
                    Next
                    Mivar.Value = diagnosticos
                    rpDatos.Add(Mivar)
                    mireporte.DataDefinition.ParameterFields("descripciond").ApplyCurrentValues(rpDatos)
                    rpDatos.Clear()
                Else
                    Mivar.Value = dt.Rows(0).Item("descripciond")
                    rpDatos.Add(Mivar)
                    mireporte.DataDefinition.ParameterFields("descripciond").ApplyCurrentValues(rpDatos)
                    rpDatos.Clear()
                End If

            Else
                Mivar.Value = sindatos
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("descripciond").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            End If
        End If



        
        strsql = " select P.idcliente as Id,(P.paterno+' '+P.materno+' '+P.nombre) as paciente,P.edad as edad,p.domicilio as direccion, p.ciudadorigen as origen, CO.descripcion as ocupacion,p.fechanacimiento as fechanacimiento " & _
           "from agenda A " & _
           "inner join clientes P on P.idcliente=A.idcliente " & _
           "left join CatOcupaciones CO on CO.IdOcupacion=P.idocupacion " & _
           "where A.idcita='118535' "

        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then


                Mivar.Value = dt.Rows(0).Item("paciente")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("paciente").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("edad")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("edad").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("direccion")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("direccion").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("ocupacion")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("ocupacion").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("fechanacimiento")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("fnacimiento").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()

                Mivar.Value = dt.Rows(0).Item("origen")
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("origen").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()



            Else
                'LblMensajeCritico.Text = clsdatos.MensajeError
                'PanelCritico.Visible = True
                'PanelCritico.Focus()
            End If

        End If





        

        

        ' strsql = "select FechaFinalizacion FROM [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] " &
        '" where folioconsulta = '" & Hffolioconsulta.Value & "' and codigoempresa = '" & Session("codigoEmpresa") & "'  "
        ' If clsdatos.cargatabla(strsql, dt) = 0 Then
        '     If dt.Rows.Count > 0 Then
        '         Mivar.Value = dt.Rows(0).Item("FechaFinalizacion")
        '         rpDatos.Add(Mivar)
        '         mireporte.DataDefinition.ParameterFields("fconsulta").ApplyCurrentValues(rpDatos)
        '         rpDatos.Clear()
        '     Else
        '         Mivar.Value = lblfagenda.Text.Trim
        '         rpDatos.Add(Mivar)
        '         mireporte.DataDefinition.ParameterFields("fconsulta").ApplyCurrentValues(rpDatos)
        '         rpDatos.Clear()
        '     End If
        ' Else
        '     Mivar.Value = lblfagenda.Text.Trim
        '     rpDatos.Add(Mivar)
        '     mireporte.DataDefinition.ParameterFields("fconsulta").ApplyCurrentValues(rpDatos)
        '     rpDatos.Clear()
        ' End If
        '
       

        'Mivar.Value = Lblfolioconsulta.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("folioc").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = TxtTension.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("tension").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = Txtfrecuencia.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("frecuencia").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = Txtpeso.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("peso").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = txttalla.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("talla").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = TxtImc.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("imc").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()



        'Mivar.Value = txtPadecimientoActual.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("padecimientos").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = txtPatologicos.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("patologicos").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = txtNoPatologicos.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("nopatologicos").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Mivar.Value = txtAlergias.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("alergias").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()


        'Mivar.Value = txtExploracion.Text.Trim
        'rpDatos.Add(Mivar)
        'mireporte.DataDefinition.ParameterFields("exploracion").ApplyCurrentValues(rpDatos)
        'rpDatos.Clear()

        'Prueba historial cosnsulta
        'strsql = "SELECT A.FechaFinalizacion as fechaAgenda,E.padecimientoActual,E.exploracion,E.planseguir,P.pronostico  FROM [" & clsdatos.BaseDatos & "].[dbo].[AgAgenda] as A " & _
        '            "inner join [" & clsdatos.BaseDatos & "].[dbo].[HmExploracion] as E on A.folioConsulta=E.folioConsulta and A.CodigoEmpresa=E.CodigoEmpresa " & _
        '            "left join [" & clsdatos.BaseDatos & "].[dbo].[catPronosticos] as P on E.pronostico=P.id " & _
        '            "where A.codigoPAciente=" & Session("codigoPaciente") & " and A.codigoEtapa=6 and A.codigoempresa = '" & Session("codigoEmpresa") & "'" & _
        '             "order by fechaAgenda desc"



        strsql = " select A.Fecha as FechaAgenda, " & _
         "isnull(HT.exploracion_fisica,'') as exploracion_fisica ," & _
         "isnull(iif(HT.exploracion_analgesia= '1','SI','NO'),'NO') as exploracion_analgesia ," & _
         "isnull(iif(HT.exploracion_propiocepsion= '1','SI','NO'),'NO') as exploracion_propiocepsion ," & _
          "isnull(iif(HT.exploracion_desinflamacion= '1','SI','NO'),'NO') as exploracion_desinflamacion ," & _
         "isnull(iif(HT.exploracion_habilidades_manuales= '1','SI','NO'),'NO') as exploracion_habilidades_manuales ," & _
         "isnull(iif(HT.exploracion_fortalecimiento= '1','SI','NO'),'NO') as exploracion_fortalecimiento ," & _
         "isnull(iif(HT.exploracion_aumentar_rangos= '1','SI','NO'),'NO') as exploracion_aumentar_rangos ," & _
         "isnull(iif(HT.exploracion_reduccion_marcha= '1','SI','NO'),'NO') as exploracion_reduccion_marcha ," & _
         "isnull(iif(HT.exploracion_reintegracion_deportiva= '1','SI','NO'),'NO') as exploracion_reintegracion_deportiva ," & _
         "isnull(iif(HT.analgesia_laser= '1','SI','NO'),'NO') as analgesia_laser ," & _
         "isnull(iif(HT.analgesia_ultrasonido= '1','SI','NO'),'NO') as analgesia_ultrasonido ," & _
         "isnull(iif(HT.analgesia_traccion_cervical= '1','SI','NO'),'NO') as analgesia_traccion_cervical ," & _
         "isnull(iif(HT.analgesia_electroterapia= '1','SI','NO'),'NO') as analgesia_electroterapia ," & _
         "isnull(iif(HT.analgesia_masaje= '1','SI','NO'),'NO') as analgesia_masaje ," & _
         "isnull(iif(HT.analgesia_traccion_lumbar= '1','SI','NO'),'NO') as analgesia_traccion_lumbar ," & _
         "isnull(iif(HT.analgesia_tape= '1','SI','NO'),'NO') as analgesia_tape ," & _
         "isnull(iif(HT.analgesia_chc= '1','SI','NO'),'NO') as analgesia_chc ," & _
         "isnull(iif(HT.analgesia_parafina= '1','SI','NO'),'NO') as analgesia_parafina ," & _
         "isnull(iif(HT.analgesia_magneto= '1','SI','NO'),'NO') as analgesia_magneto ," & _
         "isnull(iif(HT.analgesia_crio= '1','SI','NO'),'NO') as analgesia_crio ," & _
         "isnull(iif(HT.analgesia_diatermia= '1','SI','NO'),'NO') as analgesia_diatermia ," & _
         "isnull(iif(HT.analgesia_ondas= '1','SI','NO'),'NO') as analgesia_ondas ," & _
         "isnull(iif(HT.analgesia_hidroterapia= '1','SI','NO'),'NO') as analgesia_hidroterapia ," & _
         "isnull(iif(HT.analgesia_terapia_manual= '1','SI','NO'),'NO') as analgesia_terapia_manual ," & _
         "isnull(iif(HT.analgesia_banios_contraste= '1','SI','NO'),'NO') as analgesia_banios_contraste ," & _
         "isnull(iif(HT.analgesia_cf= '1','SI','NO'),'NO') as analgesia_cf ," & _
         "isnull(iif(HT.analgesia_ejercicio= '1','SI','NO'),'NO') as analgesia_ejercicio ," & _
         "isnull(iif(HT.analgesia_gimnasia= '1','SI','NO'),'NO') as analgesia_gimnasia ," & _
         "isnull(iif(HT.estiramiento_sel= '1','SI','NO'),'NO') as estiramiento_sel ," & _
         "isnull(iif(HT.miembro_superior= '1','SI','NO'),'NO') as miembro_superior ," & _
         "isnull(iif(HT.columna= '1','SI','NO'),'NO') as columna ," & _
         "isnull(iif(HT.miembro_inferior= '1','SI','NO'),'NO') as miembro_inferior ," & _
         "isnull(iif(HT.estiramiento_activo_sup= '1','SI','NO'),'NO') as estiramiento_activo_sup ," & _
         "isnull(iif(HT.estiramiento_pasivo_sup= '1','SI','NO'),'NO') as estiramiento_pasivo_sup ," & _
         "isnull(iif(HT.estiramiento_cintura_escapular= '1','SI','NO'),'NO') as estiramiento_cintura_escapular ," & _
         "isnull(iif(HT.estiramiento_extensores_muneca= '1','SI','NO'),'NO') as estiramiento_extensores_muneca ," & _
         "isnull(iif(HT.estiramiento_extensores_codo= '1','SI','NO'),'NO') as estiramiento_extensores_codo ," & _
         "isnull(iif(HT.estiramiento_flexores_muneca= '1','SI','NO'),'NO') as estiramiento_flexores_muneca ," & _
         "isnull(iif(HT.estiramiento_flexores_codo= '1','SI','NO'),'NO') as estiramiento_flexores_codo ," & _
         "isnull(iif(HT.estiramiento_maq_mov_pasiva_hombro= '1','SI','NO'),'NO') as estiramiento_maq_mov_pasiva_hombro ," & _
         "isnull(iif(HT.estiramiento_mano_hombro= '1','SI','NO'),'NO') as estiramiento_mano_hombro ," & _
         "isnull(iif(HT.estiramiento_superior_3_5= '1','SI','NO'),'NO') as estiramiento_superior_3_5 ," & _
         "isnull(iif(HT.estiramiento_superior_3_10= '1','SI','NO'),'NO') as estiramiento_superior_3_10 ," & _
         "isnull(iif(HT.estiramiento_superior_3_15= '1','SI','NO'),'NO') as estiramiento_superior_3_15 ," & _
         "isnull(iif(HT.estiramiento_dedos= '1','SI','NO'),'NO') as estiramiento_dedos ," & _
         "isnull(HT.estiramiento_otro_superior,'') as estiramiento_otro_superior ," & _
         "isnull(HT.estiramiento_otro_columna,'') as estiramiento_otro_columna ," & _
         "isnull(HT.estiramiento_otro_inferior,'') as estiramiento_otro_inferior ," & _
         "isnull(iif(HT.estiramiento_activo_columna= '1','SI','NO'),'NO') as estiramiento_activo_columna ," & _
         "isnull(iif(HT.estiramiento_pasivo_columna= '1','SI','NO'),'NO') as estiramiento_pasivo_columna ," & _
         "isnull(iif(HT.estiramiento_paravertebrales_dorsales= '1','SI','NO'),'NO') as estiramiento_paravertebrales_dorsales ," & _
         "isnull(iif(HT.estiramiento_paravertebrales_lumbares= '1','SI','NO'),'NO') as estiramiento_paravertebrales_lumbares ," & _
         "isnull(iif(HT.estiramiento_cervicales= '1','SI','NO'),'NO') as estiramiento_cervicales ," & _
         "isnull(iif(HT.estiramiento_paravertebrales= '1','SI','NO'),'NO') as estiramiento_paravertebrales ," & _
         "isnull(iif(HT.estiramiento_columna_3_5= '1','SI','NO'),'NO') as estiramiento_columna_3_5 ," & _
         "isnull(iif(HT.estiramiento_columna_3_10= '1','SI','NO'),'NO') as estiramiento_columna_3_10 ," & _
         "isnull(iif(HT.estiramiento_columna_3_15= '1','SI','NO'),'NO') as estiramiento_columna_3_15 ," & _
         "isnull(iif(HT.estiramiento_trapecio= '1','SI','NO'),'NO') as estiramiento_trapecio ," & _
         "isnull(iif(HT.estiramiento_activo_inf= '1','SI','NO'),'NO') as estiramiento_activo_inf ," & _
         "isnull(iif(HT.estiramiento_pasivo_inf= '1','SI','NO'),'NO') as estiramiento_pasivo_inf ," & _
         "isnull(iif(HT.estiramiento_cuadriceps= '1','SI','NO'),'NO') as estiramiento_cuadriceps ," & _
         "isnull(iif(HT.estiramiento_tensor_fascia_lata= '1','SI','NO'),'NO') as estiramiento_tensor_fascia_lata ," & _
         "isnull(iif(HT.estiramiento_isquiotibiales= '1','SI','NO'),'NO') as estiramiento_isquiotibiales ," & _
         "isnull(iif(HT.estiramiento_tibial_anterior= '1','SI','NO'),'NO') as estiramiento_tibial_anterior ," & _
         "isnull(iif(HT.estiramiento_aductores= '1','SI','NO'),'NO') as estiramiento_aductores ," & _
         "isnull(iif(HT.estiramiento_tibial_posterior= '1','SI','NO'),'NO') as estiramiento_tibial_posterior ," & _
         "isnull(iif(HT.estiramiento_abductores= '1','SI','NO'),'NO') as estiramiento_abductores ," & _
         "isnull(iif(HT.estiramiento_peroneos= '1','SI','NO'),'NO') as estiramiento_peroneos ," & _
         "isnull(iif(HT.estiramiento_rotadores_cadera= '1','SI','NO'),'NO') as estiramiento_rotadores_cadera ," & _
         "isnull(iif(HT.estiramiento_maq_mov_pasiva_rodilla= '1','SI','NO'),'NO') as estiramiento_maq_mov_pasiva_rodilla ," & _
         "isnull(iif(HT.estiramiento_fascia_plantar= '1','SI','NO'),'NO') as estiramiento_fascia_plantar ," & _
         "isnull(iif(HT.estiramiento_maq_mov_pasiva_tobillo= '1','SI','NO'),'NO') as estiramiento_maq_mov_pasiva_tobillo ," & _
         "isnull(iif(HT.estiramiento_inferior_3_5= '1','SI','NO'),'NO') as estiramiento_inferior_3_5 ," & _
         "isnull(iif(HT.estiramiento_inferior_3_10= '1','SI','NO'),'NO') as estiramiento_inferior_3_10 ," & _
         "isnull(iif(HT.estiramiento_inferior_3_15= '1','SI','NO'),'NO') as estiramiento_inferior_3_15 ," & _
         "isnull(iif(HT.fortalecimiento_sel= '1','SI','NO'),'NO') as fortalecimiento_sel ," & _
         "isnull(iif(HT.fortalecimiento_cintura_escapular= '1','SI','NO'),'NO') as fortalecimiento_cintura_escapular ," & _
         "isnull(iif(HT.fortalecimiento_ligas= '1','SI','NO'),'NO') as fortalecimiento_ligas ," & _
         "isnull(iif(HT.fortalecimiento_polainas= '1','SI','NO'),'NO') as fortalecimiento_polainas ," & _
         "isnull(iif(HT.fortalecimiento_isometricas= '1','SI','NO'),'NO') as fortalecimiento_isometricas ," & _
         "isnull(iif(HT.fortalecimiento_sin_peso= '1','SI','NO'),'NO') as fortalecimiento_sin_peso ," & _
         "isnull(iif(HT.fortalecimiento_extensores_muneca= '1','SI','NO'),'NO') as fortalecimiento_extensores_muneca ," & _
         "isnull(iif(HT.fortalecimiento_extensores_codo= '1','SI','NO'),'NO') as fortalecimiento_extensores_codo ," & _
         "isnull(iif(HT.fortalecimiento_flexores_muneca= '1','SI','NO'),'NO') as fortalecimiento_flexores_muneca ," & _
         "isnull(iif(HT.fortalecimiento_flexores_codo= '1','SI','NO'),'NO') as fortalecimiento_flexores_codo ," & _
         "isnull(iif(HT.fortalecimiento_maq_mov_pasiva_hombro= '1','SI','NO'),'NO') as fortalecimiento_maq_mov_pasiva_hombro ," & _
         "isnull(iif(HT.fortalecimiento_mano_hombro= '1','SI','NO'),'NO') as fortalecimiento_mano_hombro ," & _
         "isnull(iif(HT.fortalecimiento_superior_3_5= '1','SI','NO'),'NO') as fortalecimiento_superior_3_5 ," & _
         "isnull(iif(HT.fortalecimiento_superior_3_10= '1','SI','NO'),'NO') as fortalecimiento_superior_3_10 ," & _
         "isnull(iif(HT.fortalecimiento_superior_3_15= '1','SI','NO'),'NO') as fortalecimiento_superior_3_15 ," & _
         "isnull(iif(HT.fortalecimiento_dedos= '1','SI','NO'),'NO') as fortalecimiento_dedos ," & _
         "isnull(HT.fortalecimiento_otro_superior,'') as fortalecimiento_otro_superior ," & _
         "isnull(HT.fortalecimiento_otro_columna,'') as fortalecimiento_otro_columna ," & _
         "isnull(HT.fortalecimiento_otro_inferior,'') as fortalecimiento_otro_inferior ," & _
         "isnull(iif(HT.fortalecimiento_paravertebrales_dorsales= '1','SI','NO'),'NO') as fortalecimiento_paravertebrales_dorsales ," & _
         "isnull(iif(HT.fortalecimiento_williams= '1','SI','NO'),'NO') as fortalecimiento_williams ," & _
         "isnull(iif(HT.fortalecimiento_mckenzic= '1','SI','NO'),'NO') as fortalecimiento_mckenzic ," & _
         "isnull(iif(HT.fortalecimiento_core= '1','SI','NO'),'NO') as fortalecimiento_core ," & _
         "isnull(iif(HT.fortalecimiento_klapp= '1','SI','NO'),'NO') as fortalecimiento_klapp ," & _
         "isnull(iif(HT.fortalecimiento_paravertebrales_lumbares= '1','SI','NO'),'NO') as fortalecimiento_paravertebrales_lumbares ," & _
         "isnull(iif(HT.fortalecimiento_cervicales= '1','SI','NO'),'NO') as fortalecimiento_cervicales ," & _
         "isnull(iif(HT.fortalecimiento_paravertebrales= '1','SI','NO'),'NO') as fortalecimiento_paravertebrales ," & _
         "isnull(iif(HT.fortalecimiento_columna_3_5= '1','SI','NO'),'NO') as fortalecimiento_columna_3_5 ," & _
         "isnull(iif(HT.fortalecimiento_columna_3_10= '1','SI','NO'),'NO') as fortalecimiento_columna_3_10 ," & _
         "isnull(iif(HT.fortalecimiento_columna_3_15= '1','SI','NO'),'NO') as fortalecimiento_columna_3_15 ," & _
         "isnull(iif(HT.fortalecimiento_trapecio= '1','SI','NO'),'NO') as fortalecimiento_trapecio ," & _
         "isnull(iif(HT.fortalecimiento_ligas_columna= '1','SI','NO'),'NO') as fortalecimiento_ligas_columna ," & _
         "isnull(iif(HT.fortalecimiento_isometricas_columna= '1','SI','NO'),'NO') as fortalecimiento_isometricas_columna ," & _
         "isnull(iif(HT.fortalecimiento_cuadriceps= '1','SI','NO'),'NO') as fortalecimiento_cuadriceps ," & _
         "isnull(iif(HT.fortalecimiento_ligas_inf= '1','SI','NO'),'NO') as fortalecimiento_ligas_inf ," & _
         "isnull(iif(HT.fortalecimiento_isometricas_inf= '1','SI','NO'),'NO') as fortalecimiento_isometricas_inf ," & _
         "isnull(iif(HT.fortalecimiento_tensor_fascia_lata= '1','SI','NO'),'NO') as fortalecimiento_tensor_fascia_lata ," & _
         "isnull(iif(HT.fortalecimiento_isquiotibiales= '1','SI','NO'),'NO') as fortalecimiento_isquiotibiales ," & _
         "isnull(iif(HT.fortalecimiento_tibial_anterior= '1','SI','NO'),'NO') as fortalecimiento_tibial_anterior ," & _
         "isnull(iif(HT.fortalecimiento_aductores= '1','SI','NO'),'NO') as fortalecimiento_aductores ," & _
         "isnull(iif(HT.fortalecimiento_tibial_posterior= '1','SI','NO'),'NO') as fortalecimiento_tibial_posterior ," & _
         "isnull(iif(HT.fortalecimiento_abductores= '1','SI','NO'),'NO') as fortalecimiento_abductores ," & _
         "isnull(iif(HT.fortalecimiento_peroneos= '1','SI','NO'),'NO') as fortalecimiento_peroneos ," & _
         "isnull(iif(HT.fortalecimiento_rotadores_cadera= '1','SI','NO'),'NO') as fortalecimiento_rotadores_cadera ," & _
         "isnull(iif(HT.fortalecimiento_fascia_plantar= '1','SI','NO'),'NO') as fortalecimiento_fascia_plantar ," & _
         "isnull(iif(HT.fortalecimiento_polainas_inf= '1','SI','NO'),'NO') as fortalecimiento_polainas_inf ," & _
         "isnull(iif(HT.fortalecimiento_sin_peso_inf= '1','SI','NO'),'NO') as fortalecimiento_sin_peso_inf ," & _
         "isnull(iif(HT.fortalecimiento_banco= '1','SI','NO'),'NO') as fortalecimiento_banco ," & _
         "isnull(iif(HT.fortalecimiento_trampolin= '1','SI','NO'),'NO') as fortalecimiento_trampolin ," & _
         "isnull(iif(HT.fortalecimiento_bossu= '1','SI','NO'),'NO') as fortalecimiento_bossu ," & _
         "isnull(iif(HT.fortalecimiento_inferior_3_5= '1','SI','NO'),'NO') as fortalecimiento_inferior_3_5 ," & _
         "isnull(iif(HT.fortalecimiento_inferior_3_10= '1','SI','NO'),'NO') as fortalecimiento_inferior_3_10 ," & _
         "isnull(iif(HT.fortalecimiento_inferior_3_15= '1','SI','NO'),'NO') as fortalecimiento_inferior_3_15 ," & _
         "isnull(iif(HT.reacondicionamiento_bici= '1','SI','NO'),'NO') as reacondicionamiento_bici ," & _
         "isnull(iif(HT.reacondicionamiento_tiempo_bici= '1','SI','NO'),'NO') as reacondicionamiento_tiempo_bici ," & _
         "isnull(iif(HT.reacondicionamiento_caminadora= '1','SI','NO'),'NO') as reacondicionamiento_caminadora ," & _
         "isnull(iif(HT.reacondicionamiento_tiempo_caminadora= '1','SI','NO'),'NO') as reacondicionamiento_tiempo_caminadora ," & _
         "isnull(iif(HT.reacondicionamiento_reduccion_marcha= '1','SI','NO'),'NO') as reacondicionamiento_reduccion_marcha ," & _
         "isnull(iif(HT.reacondicionamiento_eliptica= '1','SI','NO'),'NO') as reacondicionamiento_eliptica ," & _
         "isnull(iif(HT.reacondicionamiento_escaleras= '1','SI','NO'),'NO') as reacondicionamiento_escaleras ," & _
         "isnull(iif(HT.reacondicionamiento_pelotas= '1','SI','NO'),'NO') as reacondicionamiento_pelotas ," & _
         "isnull(HT.observaciones,'') as observaciones ," & _
         "isnull(HT.analgesia_laser_otro,'') as analgesia_laser_otro ," & _
         "isnull(HT.analgesia_ultrasonido_otro,'') as analgesia_ultrasonido_otro ," & _
         "isnull(HT.analgesia_electroterapia_otro,'') as analgesia_electroterapia_otro ," & _
         "isnull(TP.Nombre,'') as atendio " & _
           "from HMTratamientos HT " & _
           "left join agenda A on A.idcita=HT.idcita " & _
           "left join Terapistas TP on TP.id=HT.idatendio " & _
           "where HT.idcliente='6806' order by idtratamiento ASC "




        If clsdatos.cargatabla(strsql, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                Dim var As String = ""
                Dim pronostico As String = ""
                Dim sb = New System.Text.StringBuilder()
                For Each row As DataRow In dt.Rows
                    var += "FECHA DE TRATAMIENTO: " + row.Item("FechaAgenda") + vbCr + vbCr
                    var += "EXPLORACION FISICA: " + row.Item("exploracion_fisica") + vbCr + vbCr
                    var += "OBJETIVOS DEL PACIENTE  " + vbCr
                    var += "Analgesía: " + row.Item("exploracion_analgesia") + "       "
                    var += "Propiocepción: " + row.Item("exploracion_propiocepsion") + "       "
                    var += "Desinflamación: " + row.Item("exploracion_desinflamacion") + vbCr
                    var += "H. manuales: " + row.Item("exploracion_habilidades_manuales") + "       "
                    var += "Fortalecimiento: " + row.Item("exploracion_fortalecimiento") + "       "
                    var += "A. rangos de movimiento: " + row.Item("exploracion_aumentar_rangos") + vbCr
                    var += "Reeducación de la marcha: " + row.Item("exploracion_reduccion_marcha") + "       "
                    var += "Reintegración Deportiva: " + row.Item("exploracion_reintegracion_deportiva") + vbCr + vbCr
                    var += "ANALGESÍA  " + vbCr
                    var += "Láser: " + row.Item("analgesia_laser") + "       " + row.Item("analgesia_laser_otro") + "       "
                    var += "Ultrasonido: " + row.Item("analgesia_ultrasonido") + "       " + row.Item("analgesia_ultrasonido_otro") + "       "
                    var += "Tracción cervical: " + row.Item("analgesia_traccion_cervical") + vbCr
                    var += "Electroterapia: " + row.Item("analgesia_electroterapia") + "       " + row.Item("analgesia_electroterapia_otro") + "       "
                    var += "Masaje: " + row.Item("analgesia_masaje") + "       "
                    var += "Tracción lumbar: " + row.Item("analgesia_traccion_lumbar") + vbCr
                    var += "Tape: " + row.Item("analgesia_tape") + "       "
                    var += "CHC: " + row.Item("analgesia_chc") + "       "
                    var += "Parafina: " + row.Item("analgesia_parafina") + vbCr
                    var += "Magneto terapia: " + row.Item("analgesia_magneto") + "       "
                    var += "Crior: " + row.Item("analgesia_crio") + "       "
                    var += "Diatermia: " + row.Item("analgesia_diatermia") + vbCr
                    var += "Ondas de choque: " + row.Item("analgesia_ondas") + "       "
                    var += "Hidroterapia: " + row.Item("analgesia_hidroterapia") + "       "
                    var += "Terapia Manual: " + row.Item("analgesia_terapia_manual") + vbCr
                    var += "Baños de contraste: " + row.Item("analgesia_banios_contraste") + "       "
                    var += "CF: " + row.Item("analgesia_cf") + "       "
                    var += "Ejercicio: " + row.Item("analgesia_ejercicio") + vbCr
                    var += "Gimnasia: " + row.Item("analgesia_gimnasia") + vbCr + vbCr
                    var += "REACONDICIONAMIENTO  " + vbCr
                    var += "Bici: " + row.Item("reacondicionamiento_bici") + "       "
                    var += "Tiempo: " + row.Item("reacondicionamiento_tiempo_bici") + vbCr
                    var += "Caminadora: " + row.Item("reacondicionamiento_caminadora") + "       "
                    var += "Tiempo: " + row.Item("reacondicionamiento_tiempo_caminadora") + vbCr
                    var += "Reducción de la marcha: " + row.Item("reacondicionamiento_reduccion_marcha") + "       "
                    var += "Elíptica: " + row.Item("reacondicionamiento_eliptica") + "       "
                    var += "Escaleras: " + row.Item("reacondicionamiento_escaleras") + vbCr
                    var += "Pelotas: " + row.Item("reacondicionamiento_pelotas") + vbCr + vbCr
                    var += "Observaciones: " + row.Item("observaciones") + vbCr + vbCr
                    var += "Atendio: " + row.Item("atendio") + vbCr + vbCr + vbCr + vbCr


                    'If IsDBNull(row.Item("pronostico")) Then
                    '    pronostico = ""
                    'Else
                    '    pronostico = row.Item("pronostico")
                    'End If
                    'var += "PRONÓSTICO:  " + pronostico + vbCr + vbCr

                Next
                Mivar.Value = var
                rpDatos.Add(Mivar)
                mireporte.DataDefinition.ParameterFields("plan").ApplyCurrentValues(rpDatos)
                rpDatos.Clear()
            Else
                'LblMensajeAdvertencia.Text = "Sin consultas finalizadas"
                'PanelAdvertencia.Visible = True
                Exit Sub
            End If
        End If
        Try
            Dim nomArchivo As String = lblIdcita.Text + Replace(Format(Date.Now, "dd/MM/yyyy").Trim(Format(Date.Now, "dd/MM/yyyy")), "/", "")
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
            If Session("Movil") = 1 Then
                ' Limpiamos la salida
                Response.Clear()
                'Con esto le decimos al browser que la salida sera descargable
                Response.ContentType = "application/octet-stream"
                ' esta linea es opcional, en donde podemos cambiar el nombre del fichero a descargar (para que sea diferente al original)
                Response.AddHeader("Content-Disposition", "attachment; filename=reportes/" & nomArchivo & ".pdf")
                ' Escribimos el fichero a enviar 
                Response.WriteFile("reportes/" & nomArchivo & ".pdf")
                ' volcamos el stream 
                Response.Flush()
                ' Enviamos todo el encabezado ahora
                Response.End()

            Else
                Response.Write("<script type='text/javascript'>detailedresults=window.open('ImpExReporteHF.aspx?doc=" & nomArchivo & ".pdf');</script>")

            End If
            'Response.Write("<script type='text/javascript'>detailedresults=window.open('ImpExReporte.aspx?doc=" & nomArchivo & ".pdf');</script>")
        Catch ex As Exception
            'LblMensajeCritico.Text = "Error 1: " + clsdatos.MensajeError
            'PanelCritico.Visible = True
        End Try
    End Sub
    '********************* NUEVAS FUNCIONES **********

    Protected Sub Pacientes(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("duracion", "na")
        Context.Items.Add("posicion", "na")
        Context.Items.Add("idhorario", "na")
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Context.Items.Add("elhorario", "")
        Context.Items.Add("turno", "M")
        Server.Transfer("clientes.aspx", True)
    End Sub

    Protected Sub Recibos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("pagos.aspx", False)
    End Sub

    Protected Sub Historial(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("hpacientes.aspx", False)
    End Sub

    Protected Sub Catalogos(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("tipoPagos.aspx", False)
    End Sub

    Protected Sub CorteCaja(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("Corte.aspx", False)
    End Sub

    Protected Sub BloqueoCitas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("bloqueocitas.aspx", False)
    End Sub

    Protected Sub FacturasGeneradas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("facGeneradas.aspx", False)
    End Sub

    Protected Sub VerCancelados(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("canceladas.aspx", False)
    End Sub
    Private Sub BtnCerrarSesion_Click(sender As Object, e As EventArgs) Handles BtnCerrarSesion.Click
        Response.Redirect("login.aspx")
    End Sub
    Protected Sub lnkguardar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles lnkguardar.Click
        Server.Transfer("default.aspx", False)
    End Sub

    Protected Sub CTerapeutas(ByVal sender As Object, ByVal e As System.EventArgs)
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("ABCTerapeutas.aspx", False)
    End Sub
End Class
