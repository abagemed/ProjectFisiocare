Public Class ABCCargos
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not IsPostBack Then
            'If Session("Pconfiguracion") <> 1 Then
            '    Response.Redirect("agenda.aspx")
            'End If
            CargarGrdUsuarios()
        End If
    End Sub

    Private Sub CargarGrdUsuarios()
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT codigoConcepto,descripcion,costo,(case status  when '1' then 'SI' else (case status  when '0' then 'NO' else 'SI' end) end) as status " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] where (status=1 or status=0) and codigoEmpresa =1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                GdUsuarios.DataSource = dt
                GdUsuarios.DataBind()
            Else
                LblMensajeAdvertencia.Text = "No se encontró ningún concepto de cobro"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Private Sub bucarDatos(ByRef codigoUsuario As String)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        strSQL = "SELECT descripcion,costo,status,activoMedico,ClaveProdServ,ClaveUnidad,Unidad,tasaIVA,tasaISR " & _
                "FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
                 "where codigoConcepto ='" & codigoUsuario & "' and codigoEmpresa =1 "
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then

                If dt.Rows(0).Item("status") = 0 Then
                    txtdescripcion.Text = dt.Rows(0).Item("descripcion")
                    txtdescripcion.ReadOnly = True
                    txtcosto.Text = dt.Rows(0).Item("costo")
                    txtcosto.ReadOnly = True
                    txtClavesat.Text = dt.Rows(0).Item("ClaveProdServ")
                    txtClavesat.ReadOnly = True
                    txtClaveunidad.Text = dt.Rows(0).Item("ClaveUnidad")
                    txtClaveunidad.ReadOnly = True
                    txtunidad.Text = dt.Rows(0).Item("Unidad")
                    txtunidad.ReadOnly = True
                    txtTIVA.Text = dt.Rows(0).Item("tasaIVA")
                    txtTIVA.ReadOnly = True
                    txtTISR.Text = dt.Rows(0).Item("tasaISR")
                    txtTISR.ReadOnly = True
                    If Not IsDBNull(dt.Rows(0).Item("status")) Then
                        If dt.Rows(0).Item("status") = 0 Then
                            sactivo.Value = 0
                        End If
                        If dt.Rows(0).Item("status") = 1 Then
                            sactivo.Value = 1
                        End If

                    End If
                    If Not IsDBNull(dt.Rows(0).Item("activoMedico")) Then
                        If dt.Rows(0).Item("activoMedico") = 0 Then
                            sactivomed.Value = 0
                        End If
                        If dt.Rows(0).Item("activoMedico") = 1 Then
                            sactivomed.Value = 1
                        End If

                    End If
                Else
                    txtdescripcion.Text = dt.Rows(0).Item("descripcion")
                    txtdescripcion.ReadOnly = False
                    txtcosto.Text = dt.Rows(0).Item("costo")
                    txtcosto.ReadOnly = False
                    txtClavesat.Text = dt.Rows(0).Item("ClaveProdServ")
                    txtClavesat.ReadOnly = False
                    txtClaveunidad.Text = dt.Rows(0).Item("ClaveUnidad")
                    txtClaveunidad.ReadOnly = False
                    txtunidad.Text = dt.Rows(0).Item("Unidad")
                    txtunidad.ReadOnly = False
                    txtTIVA.Text = dt.Rows(0).Item("tasaIVA")
                    txtTIVA.ReadOnly = False
                    txtTISR.Text = dt.Rows(0).Item("tasaISR")
                    txtTISR.ReadOnly = False
                    If Not IsDBNull(dt.Rows(0).Item("status")) Then
                        If dt.Rows(0).Item("status") = 0 Then
                            sactivo.Value = 0
                        End If
                        If dt.Rows(0).Item("status") = 1 Then
                            sactivo.Value = 1
                        End If

                    End If
                    If Not IsDBNull(dt.Rows(0).Item("activoMedico")) Then
                        If dt.Rows(0).Item("activoMedico") = 0 Then
                            sactivomed.Value = 0
                        End If
                        If dt.Rows(0).Item("activoMedico") = 1 Then
                            sactivomed.Value = 1
                        End If

                    End If

                End If

                CargarGrdUsuarios()
                

            Else
                LblMensajeAdvertencia.Text = "No se encontró el concepto de cobro, favor de reportarlo a sistemas"
                PanelAdvertencia.Visible = True
            End If
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnGuardarEdit_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        Dim status As String = ""
        If sactivo.Value = 0 Then
            status = 0
        End If
        If sactivo.Value = 1 Then
            status = 1
        End If
        Dim activomed As String = ""
        If sactivomed.Value = 0 Then
            activomed = 0
        End If
        If sactivomed.Value = 1 Then
            activomed = 1
        End If

        strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
                "set descripcion='" & txtdescripcion.Text & "',costo='" & txtcosto.Text & "',status='" & status & "',activoMedico='" & activomed & "',ClaveProdServ='" & txtClavesat.Text & "',ClaveUnidad='" & txtClaveunidad.Text & "',Unidad='" & txtunidad.Text & "',tasaIVA='" & txtTIVA.Text & "',tasaISR='" & txtTISR.Text & "' " & _
                "where codigoConcepto='" & HdUsuarioEdit.Value & "' and codigoEmpresa =1"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se realizó el cambio correctamente"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()

        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Protected Sub BtnGuardarNuevo_Click(sender As Object, e As EventArgs)
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String
        'Dim statusa As String = ""
        'If sactivoa.Value = 0 Then
        '    statusa = 0
        'End If
        'If sactivoa.Value = 1 Then
        '    statusa = 1
        'End If

        Dim activomeda As String = ""
        If sactivomeda.Value = 0 Then
            activomeda = 0
        End If
        If sactivomeda.Value = 1 Then
            activomeda = 1
        End If


        'SOLO PARA UROSUR
        'Dim servicioa As String = ""
        'If sservicioa.Value = 1 Then
        '    servicioa = "HOURI"
        'End If
        'If sservicioa.Value = 2 Then
        '    servicioa = "ESPAT"
        'End If
        'If sservicioa.Value = 3 Then
        '    servicioa = "INSUM"
        'End If
        'If sservicioa.Value = 4 Then
        '    servicioa = "HOURE"
        'End If
        'If sservicioa.Value = 5 Then
        '    servicioa = "USG"
        'End If
        'If sservicioa.Value = 6 Then
        '    servicioa = "OTROS"
        'End If


        'MULTIEMPRESAS
        Dim servicioa As String = ""
        If sservicioa.Value = 1 Then
            servicioa = "CONSU"
        End If
        If sservicioa.Value = 2 Then
            servicioa = "TERAP"
        End If
        If sservicioa.Value = 3 Then
            servicioa = "OTROS"
        End If
        If sservicioa.Value = 4 Then
            servicioa = "INSUM"
        End If
       




        Dim tasaivaa As String = ""
        If stasaivaa.Value = 0 Then
            tasaivaa = "0.000000"
        End If
        If stasaivaa.Value = 1 Then
            tasaivaa = "0.160000"
        End If

        Dim tasaisra As String = ""
        If stasaisra.Value = 0 Then
            tasaisra = "0.000000"
        End If
        If stasaisra.Value = 1 Then
            tasaisra = "0.100000"
        End If

        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] (servicio, corta, descripcion, costo, status, activoMedico, ClaveProdServ, ClaveUnidad, Unidad, NoIdentificacion, tasaIVA,codigoEmpresa,tasaISR) " & _
                    "values('" & servicioa & "','ORCONS','" & txtdescripciona.Text & "','" & txtcostoa.Text & "','1','" & activomeda & "','" & txtClavesata.Text & "','" & txtClaveunidada.Text & "','" & txtunidada.Text & "',0,'" & tasaivaa & "','1','" & tasaisra & "')"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se agregó el nuevo concepto de cobro correctamente"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub

    Protected Sub BtnCancelar_Click(sender As Object, e As EventArgs)
        Response.Redirect("Agenda.aspx")
    End Sub

    Private Sub GdUsuarios_RowCommand(sender As Object, e As GridViewCommandEventArgs) Handles GdUsuarios.RowCommand
        If e.CommandName = "Editar" Then
            ocultarPaneles()
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType(), "none", "<script>$('#Editar_1').modal('show');</script>", False)
            HdUsuarioEdit.Value = GdUsuarios.Rows(e.CommandArgument).Cells(0).Text
            bucarDatos(GdUsuarios.Rows(e.CommandArgument).Cells(0).Text)
        End If
        If e.CommandName = "Borrar" Then
            ocultarPaneles()
            HdUsuarioEdit.Value = GdUsuarios.Rows(e.CommandArgument).Cells(0).Text
            LblMostarDecision.Text = " ¿Desea eliminar este concepto de cobro: " + GdUsuarios.Rows(e.CommandArgument).Cells(1).Text + "?"
            PanelDesicion.Visible = True
            PanelDesicion.Focus()
            Hdpregunta.Value = "0"
        End If
    End Sub
    Protected Sub BtnSi_Click(sender As Object, e As EventArgs)
        Select Case Hdpregunta.Value
            Case "0"
                Dim strSQL As String
                Dim clsDatos As New ClaseDatos
                strSQL = "update [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
                "set status=2 " & _
                "where codigoConcepto='" & HdUsuarioEdit.Value & "'"
                clsDatos.cargaComando(strSQL)
                If clsDatos.ejecutar = 0 Then
                    ocultarPaneles()
                    CargarGrdUsuarios()
                Else
                    LblMensajeCritico.Text = clsDatos.MensajeError
                    PanelCritico.Visible = True
                End If

        End Select
    End Sub

    Protected Sub BtnNo_Click(sender As Object, e As EventArgs)
        ocultarPaneles()
        Exit Sub
    End Sub
    Private Sub ocultarPaneles()
        PanelAdvertencia.Visible = False
        PanelAvisos.Visible = False
        PanelCritico.Visible = False
        PanelDesicion.Visible = False
    End Sub

    Protected Sub btnGuardar2_Click(sender As Object, e As EventArgs) Handles btnGuardar2.Click
        Dim clsDatos As New ClaseDatos
        Dim dt As New DataTable
        Dim strSQL As String

        'Dim statusa As String = ""
        'If sactivoa.Value = 0 Then
        '    statusa = 0
        'End If
        'If sactivoa.Value = 1 Then
        '    statusa = 1
        'End If

        Dim activomeda As String = ""
        If sactivomeda.Value = 0 Then
            activomeda = 0
        End If
        If sactivomeda.Value = 1 Then
            activomeda = 1
        End If


        'SOLO UROSUR
        'Dim servicioa As String = ""
        'If sservicioa.Value = 1 Then
        '    servicioa = "HOURI"
        'End If
        'If sservicioa.Value = 2 Then
        '    servicioa = "ESPAT"
        'End If
        'If sservicioa.Value = 3 Then
        '    servicioa = "INSUM"
        'End If
        'If sservicioa.Value = 4 Then
        '    servicioa = "HOURE"
        'End If
        'If sservicioa.Value = 5 Then
        '    servicioa = "USG"
        'End If
        'If sservicioa.Value = 6 Then
        '    servicioa = "OTROS"
        'End If

        'MULTIEMPRESAS
        Dim servicioa As String = ""
        If sservicioa.Value = 1 Then
            servicioa = "CONSU"
        End If
        If sservicioa.Value = 2 Then
            servicioa = "TERAP"
        End If
        If sservicioa.Value = 3 Then
            servicioa = "OTROS"
        End If
        ''ab08042020
        If sservicioa.Value = 4 Then
            servicioa = "INSUM"
        End If







        Dim tasaivaa As String = ""
        If stasaivaa.Value = 0 Then
            tasaivaa = "0.000000"
        End If
        If stasaivaa.Value = 1 Then
            tasaivaa = "0.160000"
        End If

        Dim tasaisra As String = ""
        If stasaisra.Value = 0 Then
            tasaisra = "0.000000"
        End If
        If stasaisra.Value = 1 Then
            tasaisra = "0.100000"
        End If


      
        strSQL = "SELECT codigoConcepto  FROM [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] " & _
                    "where descripcion='" & txtdescripciona.Text & "' AND (status=1 OR status=0) and codigoEmpresa =1"
        If clsDatos.cargatabla(strSQL, dt) = 0 Then
            If dt.Rows.Count > 0 Then
                LblMensajeAviso.Text = "El nombre del concepto de cobro ya existe"
                PanelAvisos.Visible = True
                PanelAvisos.Focus()
                Exit Sub
            End If
        End If
        strSQL = "insert into [" & clsDatos.BaseDatos & "].[dbo].[CatConceptoPagos] (servicio, corta, descripcion, costo, status, activoMedico, ClaveProdServ, ClaveUnidad, Unidad, NoIdentificacion, tasaIVA,codigoEmpresa,tasaISR) " & _
                  "values('" & servicioa & "','ORCONS','" & txtdescripciona.Text & "','" & txtcostoa.Text & "','1','" & activomeda & "','" & txtClavesata.Text & "','" & txtClaveunidada.Text & "','" & txtunidada.Text & "',0,'" & tasaivaa & "','1','" & tasaisra & "')"
        clsDatos.cargaComando(strSQL)
        If clsDatos.ejecutar = 0 Then
            LblMensajeAviso.Text = "Se agregó el nuevo concepto de cobro correctamente"
            PanelAvisos.Visible = True
            PanelAvisos.Focus()
            CargarGrdUsuarios()
            limpiarvariables()
        Else
            LblMensajeCritico.Text = clsDatos.MensajeError
            PanelCritico.Visible = True
        End If
    End Sub
    Protected Sub limpiarvariables()

        txtdescripciona.Text = ""
        txtcostoa.Text = ""
        sactivomeda.Value = 0
        'sactivoa.Value = 0
        stasaivaa.Value = 0
        sservicioa.Value = 0
    End Sub
End Class