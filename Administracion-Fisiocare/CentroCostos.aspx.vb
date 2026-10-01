Imports System.Data.SqlClient
Partial Class CentroCostos
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Dim db As String = "fisiocaresm" ''utilizado en el llenagrid
    Sub llenaCatcostos()
        Dim consulta As String = "select idcosto,id_servicio,descripcion,costo,iddatosfac,'Campestre' as tabla  from ( " & _
        "select idCosto,id_servicio,descripcion,costo,iddatosfac from fisiocarecp.dbo.catcostos where activo='true' " & _
        "except select idCosto,id_servicio,descripcion,costo,iddatosfac from fisiocaresm.dbo.catcostos where activo='true') as primera " & _
        "union all select idcosto,id_servicio,descripcion,costo,iddatosfac,'starMedica' as tabla from ( " & _
        "Select idCosto,id_servicio,descripcion,costo,iddatosfac from fisiocaresm.dbo.catcostos where activo='true' " & _
        "except select idCosto,id_servicio,descripcion,costo,iddatosfac from fisiocarecp.dbo.catcostos where activo='true') as segunda "

        Dim validaDatos As String = funciones.leerValor(consulta, "fisiocaresm")
        If validaDatos = "0" Then
            cmbInstituciones = funciones.llenacombos(cmbInstituciones, "select idResponsable,descripcion from catresponsables where activo='true' order by descripcion", "fisiocaresm")
            gridCostos.Columns(5).Visible = True
        Else
            Messagebox1.ShowMessage("DATOS INCONCISTENTES EN LOS COSTOS DE LAS SUCURSALES FAVOR DE VERIFICA")
            consulta = "select t.idcosto,t.id_servicio,t.descripcion,t.costo,t.iddatosfac,catResponsables.descripcion as responsable from ( " + consulta + ") " & _
            "as t left join catResponsables on catResponsables.idresponsable=t.iddatosfac"
            gridCostos = funciones.LLenaGrid(consulta, gridCostos, db)
            btnNcosto.Visible = False
        End If

        consulta = "select id_servicio,descripcion,'Campestre' as tabla  from (select id_servicio,descripcion from fisiocarecp.dbo.catservicios where activo='true' " & _
        "except select id_servicio,descripcion from fisiocaresm.dbo.catservicios where activo='true') as primera " & _
        "union all select id_servicio,descripcion,'starMedica' as tabla from (select id_servicio,descripcion from fisiocaresm.dbo.catservicios where activo='true' " & _
        "except select id_servicio,descripcion from fisiocarecp.dbo.catservicios where activo='true') as segunda"

        validaDatos = funciones.leerValor(consulta, "fisiocaresm")
        If validaDatos = "0" Then
            gridServicios = funciones.LLenaGrid("select id_servicio,descripcion,ClaveProdServ as codigoSat,ClaveUnidad as tipoCosto  from catServicios where activo='true' order by id_servicio", gridServicios, db)
        Else
            Messagebox1.ShowMessage("DATOS INCONSISTENTES EN LE CATALOGO DE LOS TIPOS DE TERAPIAS DE LAS SUCURSALES")
            gridServicios = funciones.LLenaGrid(consulta, gridServicios, db)
            btnNterapia.Visible = False
        End If

        consulta = "select idresponsable,abreviacion,descripcion,'Campestre' as tabla  from (select idresponsable,abreviacion,descripcion from fisiocarecp.dbo.catresponsables where activo='true' " & _
       "except select idresponsable,abreviacion,descripcion from fisiocaresm.dbo.catresponsables where activo='true') as primera " & _
       "union all select idresponsable,abreviacion,descripcion, 'starMedica' as tabla from (select idresponsable,abreviacion,descripcion from fisiocaresm.dbo.catresponsables where activo='true' " & _
       "except select idresponsable,abreviacion,descripcion  from fisiocarecp.dbo.catresponsables where activo='true') as segunda"

        validaDatos = funciones.leerValor(consulta, "fisiocaresm")
        If validaDatos = "0" Then
            gridInstituciones = funciones.LLenaGrid("select idresponsable,abreviacion,descripcion from catResponsables where activo='true' order by abreviacion", gridInstituciones, db)
        Else
            Messagebox1.ShowMessage("DATOS INCONSISTENTES EN LE CATALOGO DE INTITUCIONES DE LAS SUCURSALES")
            gridInstituciones = funciones.LLenaGrid(consulta, gridInstituciones, db)
            btnNaseguradora.Visible = False
        End If
    End Sub
    Protected Sub gridCostos_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCostos.CancelCommand
        gridCostos.EditItemIndex = -1
        llenaGridCostos()
    End Sub
    Protected Sub gridCostos_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCostos.EditCommand
        gridCostos.EditItemIndex = e.Item.ItemIndex
        llenaGridCostos()
    End Sub

    Protected Sub gridCostos_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCostos.UpdateCommand
        'Try
        Dim cantidad As Double = CType(e.Item.Cells(3).Controls(0), TextBox).Text
        Dim id As String = gridCostos.DataKeys.Item(e.Item.ItemIndex).ToString
        Dim descrip As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
        Dim activo As String = CType(e.Item.Cells(4).Controls(0), TextBox).Text
        'MsgBox("Id: " & id & "  cantidad: " & Str(cantidad) & "  Responsable: " & responsable)

        funciones.grabaDatos("update catCostos set  descripcion='" + descrip + "', costo='" + cantidad.ToString + "' , activo='" + activo + "'" & _
        " where idCosto='" + id + "'", "fisiocaresm")
        funciones.grabaDatos("update catCostos set  descripcion='" + descrip + "', costo='" + cantidad.ToString + "', activo='" + activo + "'" & _
        " where idCosto='" + id + "'", "fisiocarecp")
        gridCostos.EditItemIndex = -1
        llenaGridCostos()
        'Catch
        'Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        'End Try
    End Sub

    Protected Sub gridInstituciones_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridInstituciones.CancelCommand
        gridInstituciones.EditItemIndex = -1
        gridInstituciones = funciones.LLenaGrid("select idresponsable,abreviacion,descripcion from catResponsables where activo='true' order by abreviacion", gridInstituciones, db)
    End Sub

    Protected Sub gridInstituciones_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridInstituciones.EditCommand
        gridInstituciones.EditItemIndex = e.Item.ItemIndex
        gridInstituciones = funciones.LLenaGrid("select idresponsable,abreviacion,descripcion from catResponsables where activo='true' order by abreviacion", gridInstituciones, db)
    End Sub
    Protected Sub gridInstituciones_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridInstituciones.UpdateCommand
        Dim abreviacion As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text.ToUpper
        Dim aseguradora As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text.ToUpper
        Dim id As String = gridInstituciones.Items(e.Item.ItemIndex).Cells(0).Text
        

        funciones.grabaDatos("update catResponsables set descripcion='" + aseguradora + "',abreviacion='" + abreviacion + "'  where idresponsable='" + id + "'", "fisiocaresm")
        funciones.grabaDatos("update catResponsables set descripcion='" + aseguradora + "',abreviacion='" + abreviacion + "'  where idresponsable='" + id + "'", "fisiocarecp")
        gridInstituciones.EditItemIndex = -1
        gridInstituciones = funciones.LLenaGrid("select idresponsable,abreviacion,descripcion from catResponsables where activo='true' order by abreviacion", gridInstituciones, db)
    End Sub

    Protected Sub gridServicios_CancelCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridServicios.CancelCommand
        gridServicios.EditItemIndex = -1
        gridServicios = funciones.LLenaGrid("select id_servicio,descripcion,ClaveProdServ as codigoSat,ClaveUnidad as tipoCosto  from catServicios where activo='true' ", gridServicios, db)

    End Sub

    Protected Sub gridServicios_EditCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridServicios.EditCommand
        gridServicios.EditItemIndex = e.Item.ItemIndex
        gridServicios = funciones.LLenaGrid("select id_servicio,descripcion,ClaveProdServ as codigoSat,ClaveUnidad as tipoCosto  from catServicios where activo='true' ", gridServicios, db)
    End Sub

    Protected Sub gridServicios_UpdateCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridServicios.UpdateCommand
        Dim descrip As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text.ToUpper
        Dim id As String = gridServicios.Items(e.Item.ItemIndex).Cells(0).Text
        Dim codigoSat As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text.ToUpper
        Dim tipoCosto As String = CType(e.Item.Cells(3).Controls(0), TextBox).Text.ToUpper

        funciones.grabaDatos("update catservicios set descripcion='" + descrip + "',ClaveProdServ='" + codigoSat + "',ClaveUnidad='" + tipoCosto + "' where id_servicio='" + id + "'", "fisiocaresm")
        funciones.grabaDatos("update catservicios set descripcion='" + descrip + "',ClaveProdServ='" + codigoSat + "',ClaveUnidad='" + tipoCosto + "' where id_servicio='" + id + "'", "fisiocarecp")
        gridServicios.EditItemIndex = -1
        gridServicios = funciones.LLenaGrid("select id_servicio,descripcion,ClaveProdServ as codigoSat,ClaveUnidad as tipoCosto  from catServicios where activo='true' ", gridServicios, db)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            llenaCatcostos()
            cmbterapias = funciones.llenacombos(cmbterapias, "select id_servicio,descripcion from catServicios where activo='true' order by descripcion", db)
            cmbaseguradoras = funciones.llenacombos(cmbaseguradoras, "select idresponsable,descripcion from catResponsables where activo='true' order by descripcion", db)
        End If
    End Sub
    Sub llenaGridCostos()
        gridCostos = funciones.LLenaGrid("select catcostos.id_servicio,iddatosfac,catcostos.descripcion,costo,idcosto,(case catcostos.activo  when 'true' then 'Activo' else 'Inactivo' end)  as Status," & _
           "catResponsables.descripcion as responsable from catCostos left join catResponsables on " & _
           "catCostos.iddatosfac=catResponsables.idresponsable where iddatosfac='" + cmbInstituciones.SelectedValue + "' order by catcostos.descripcion", gridCostos, db)
        'catcostos.activo='true' and
    End Sub

    Protected Sub Button4_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button4.Click
        If cmbterapias.SelectedValue <> "0" And cmbaseguradoras.SelectedValue <> "0" And txtcosto.Text.Trim <> "" Then
            Try
                Dim cantidad As Decimal = Convert.ToDouble(txtcosto.Text.Trim)
                Dim comentario As String = ""
                If txtcomentario.Text.Trim <> "" Then
                    comentario = " (" + txtcomentario.Text.Trim + ")"
                End If
                Dim descripcion As String = cmbterapias.SelectedValue + " - cod:" + cmbaseguradoras.SelectedValue + " - $" + cantidad.ToString.Trim + comentario
                funciones.grabaDatos("insert into catCostos(id_servicio,descripcion,costo,iddatosfac,activo,cambioprecio, descuentoc, Apoyo) values ('" + cmbterapias.SelectedValue + "'," & _
                "'" + descripcion + "','" + cantidad.ToString.Trim + "','" + cmbaseguradoras.SelectedValue + "','" + cmbActivo.SelectedValue + "','" + cmbcambiop.SelectedValue + "','0.00','0.00')", "fisiocaresm")
                funciones.grabaDatos("insert into catCostos(id_servicio,descripcion,costo,iddatosfac,activo,cambioprecio, descuentoc, Apoyo) values ('" + cmbterapias.SelectedValue + "'," & _
                "'" + descripcion + "','" + cantidad.ToString.Trim + "','" + cmbaseguradoras.SelectedValue + "','" + cmbActivo.SelectedValue + "','" + cmbcambiop.SelectedValue + "','0.00','0.00')", "fisiocarecp")

            Catch
                Messagebox1.ShowMessage("DATOS INCORECTOS FAVOR DE VERIFICAR")
            End Try
        Else
            Messagebox1.ShowMessage("DATOS INCORECTOS FAVOR DE VERIFICAR")
        End If
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNcosto.Click
        pnlcostos.Visible = True
        pnlterapias.Visible = False
        pnlAseguradora.Visible = False
        pnlPrincipal.Visible = False
    End Sub

    Protected Sub Button1_Click1(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        If txtabrevia.Text.Trim <> "" And txtservicio.Text.Trim <> "" Then
            Dim aux As String = funciones.leerValor("select id_servicio from catservicios where id_servicio='" + txtabrevia.Text.Trim.ToUpper + "'", "fisiocaresm")
            If aux = "0" Then
                funciones.grabaDatos("insert into catservicios(id_servicio,descripcion,ClaveProdServ, ClaveUnidad, Unidad, NoIdentificacion, tasaIVA,ObjetoImp) values" & _
                "('" + txtabrevia.Text.Trim.ToUpper + "','" + txtservicio.Text.Trim.ToUpper + "','" + txtclaveservicio.Text.Trim + "','" + txtclaveunidad.Text.Trim + "','" + txtunidad.Text.Trim + "','0','" + cmbiva.SelectedValue + "','02')", "fisiocaresm")
                funciones.grabaDatos("insert into catservicios(id_servicio,descripcion,ClaveProdServ, ClaveUnidad, Unidad, NoIdentificacion, tasaIVA,ObjetoImp) values" & _
                "('" + txtabrevia.Text.Trim.ToUpper + "','" + txtservicio.Text.Trim.ToUpper + "','" + txtclaveservicio.Text.Trim + "','" + txtclaveunidad.Text.Trim + "','" + txtunidad.Text.Trim + "','0','" + cmbiva.SelectedValue + "','02')", "fisiocarecp")
                Messagebox1.ShowMessage("SERVICIO GUARDADO CORRECTAMENTE")
            Else
                Messagebox1.ShowMessage("LA ABREVIATURA YA EXISTE")
            End If
        Else
            Messagebox1.ShowMessage("DATOS INCOMPLETOS")
        End If
    End Sub

    Protected Sub Button2_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button2.Click
        If txtabreviaAseguradora.Text.Trim <> "" And txtdescripAseguradora.Text.Trim <> "" Then
            Dim aux As String = funciones.leerValor("select descripcion from catresponsables where descripcion='" + txtabreviaAseguradora.Text.Trim.ToUpper + "'", "fisiocaresm")
            If aux = "0" Then
                funciones.grabaDatos("insert into catresponsables(abreviacion,descripcion,activo) values" & _
                "('" + txtabreviaAseguradora.Text.ToUpper.Trim + "','" + txtdescripAseguradora.Text.Trim + "','true')", "fisiocaresm")
                funciones.grabaDatos("insert into catresponsables(abreviacion,descripcion,activo) values" & _
                "('" + txtabreviaAseguradora.Text.ToUpper.Trim + "','" + txtdescripAseguradora.Text.Trim + "','true')", "fisiocarecp")
            Else
                Messagebox1.ShowMessage("LA ABREVIATURA YA EXISTE")
            End If
        Else
            Messagebox1.ShowMessage("DATOS INCOMPLETOS")
        End If
    End Sub

    Protected Sub btnNterapia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNterapia.Click
        pnlcostos.Visible = False
        pnlterapias.Visible = True
        pnlAseguradora.Visible = False
        pnlPrincipal.Visible = False
    End Sub

    Protected Sub btnNaseguradora_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnNaseguradora.Click
        pnlcostos.Visible = False
        pnlterapias.Visible = False
        pnlAseguradora.Visible = True
        pnlPrincipal.Visible = False
    End Sub

    Protected Sub Button3_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button3.Click
        pnlcostos.Visible = False
        pnlterapias.Visible = False
        pnlAseguradora.Visible = False
        pnlPrincipal.Visible = True
        llenaGridCostos()
        llenaCatcostos()
        cmbterapias = funciones.llenacombos(cmbterapias, "select id_servicio,descripcion from catServicios where activo='true'", db)
        cmbaseguradoras = funciones.llenacombos(cmbaseguradoras, "select idresponsable,descripcion from catResponsables where activo='true'", db)
    End Sub

    Protected Sub Button5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button5.Click
        pnlcostos.Visible = False
        pnlterapias.Visible = False
        pnlAseguradora.Visible = False
        pnlPrincipal.Visible = True
        llenaCatcostos()
        cmbterapias = funciones.llenacombos(cmbterapias, "select id_servicio,descripcion from catServicios where activo='true'", db)
        cmbaseguradoras = funciones.llenacombos(cmbaseguradoras, "select idresponsable,descripcion from catResponsables where activo='true'", db)
    End Sub

    Protected Sub Button6_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button6.Click
        pnlcostos.Visible = False
        pnlterapias.Visible = False
        pnlAseguradora.Visible = False
        pnlPrincipal.Visible = True
        llenaCatcostos()
        cmbterapias = funciones.llenacombos(cmbterapias, "select id_servicio,descripcion from catServicios where activo='true'", db)
        cmbaseguradoras = funciones.llenacombos(cmbaseguradoras, "select idresponsable,descripcion from catResponsables where activo='true'", db)
    End Sub

    Protected Sub cmbInstituciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbInstituciones.SelectedIndexChanged
        llenaGridCostos()
    End Sub

    Protected Sub Menu1_MenuItemClick(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.MenuEventArgs) Handles Menu1.MenuItemClick
        Dim Valor As String = e.Item.Value.ToString.Trim
        Select Case Valor
            Case "0"
                Server.Transfer("Defadmon.aspx", True)
            Case "1"
                Server.Transfer("centroCostos.aspx", False)
            Case "2"
                Server.Transfer("admUsuarios.aspx", False)
            Case "3"
                Server.Transfer("camaras.aspx", False)
            Case "4"
                Server.Transfer("terapistas.aspx", False)
            Case "5"
                Server.Transfer("admRecibos.aspx", False)
            Case Else
                Server.Transfer(Valor, False)
        End Select
    End Sub
    Protected Sub gridcostos_ItemCommand(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCostos.ItemCommand
        If e.CommandName = "cmdEditarT" Then

            Dim id As String = gridCostos.DataKeys.Item(e.Item.ItemIndex).ToString
            Dim conexion As SqlConnection = funciones.conecta("fisiocareSM")
            Dim comando As SqlCommand = conexion.CreateCommand
            comando.CommandText = "select catcostos.id_servicio,iddatosfac,catcostos.descripcion,costo,idcosto,(case catcostos.activo  when 'true' then 'Activo' else 'Inactivo' end)  as Status," & _
                       "catResponsables.descripcion as responsable,(case catcostos.cambioprecio  when 'true' then 'Activo' else 'Inactivo' end)  as cambiarprecio,descuentoc,Apoyo from catCostos left join catResponsables on " & _
                       "catCostos.iddatosfac=catResponsables.idresponsable where idcosto='" + id + "'"
            conexion.Open()
            Dim sqlread As SqlDataReader = comando.ExecuteReader
            If sqlread.Read Then
                txtmterapia.Text = sqlread.GetValue(0).ToString.Trim
                txtmcosto.Text = sqlread.GetValue(3).ToString.Trim
                txtmdescripcion.Text = sqlread.GetValue(2).ToString.Trim
                txtmresponsables.Text = sqlread.GetValue(6).ToString.Trim
                txtidcosto.Text = sqlread.GetValue(4).ToString.Trim
                cbomactivo.SelectedValue = sqlread.GetValue(5).ToString.Trim
                cbomcambiarp.SelectedValue = sqlread.GetValue(7).ToString.Trim
                txtmdescuento.Text = sqlread.GetValue(8).ToString.Trim
                txtmapoyo.Text = sqlread.GetValue(9).ToString.Trim

                txtmterapia.Enabled = False
                txtmresponsables.Enabled = False
                txtidcosto.Visible = False
            End If
            sqlread.Close()
            conexion.Close()
            hfBandera.Value = "0"
            ModalPopupExtendermt.Show()


        End If

        gridCostos.Visible = False
    End Sub
    Protected Sub bgmcancelar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bgmcancelar.Click
        gridCostos.EditItemIndex = -1
        llenaGridCostos()
        gridCostos.Visible = True
    End Sub
    Protected Sub bgmterapia_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles bgmterapia.Click

        ''Protected Sub gridCostos_bgmterapia(ByVal source As Object, ByVal e As System.Web.UI.WebControls.DataGridCommandEventArgs) Handles gridCostos.bgmterapia
        ''    'Try


        Dim conexion As SqlConnection = funciones.conecta("fisiocareSM")
        Dim comando As SqlCommand = conexion.CreateCommand


        Dim cantidad As Double = txtmcosto.Text
        Dim id As String = txtidcosto.Text
        Dim descrip As String = txtmdescripcion.Text
        Dim activo As String = cbomactivo.SelectedValue
        Dim cambiarp As String = cbomcambiarp.SelectedValue
        Dim descuento As String = txtmdescuento.Text
        Dim apoyo As String = txtmapoyo.Text
        'MsgBox("Id: " & id & "  cantidad: " & Str(cantidad) & "  Responsable: " & responsable)

        funciones.grabaDatos("update catCostos set  descripcion='" + descrip + "', costo='" + cantidad.ToString + "' , activo='" + activo + "', cambioprecio='" + cambiarp + "',descuentoc='" + descuento + "',Apoyo='" + apoyo + "'" & _
        " where idCosto='" + id + "'", "fisiocaresm")
        funciones.grabaDatos("update catCostos set  descripcion='" + descrip + "', costo='" + cantidad.ToString + "', activo='" + activo + "', cambioprecio='" + cambiarp + "',descuentoc='" + descuento + "',Apoyo='" + apoyo + "'" & _
        " where idCosto='" + id + "'", "fisiocarecp")
        gridCostos.EditItemIndex = -1
        llenaGridCostos()
        
        gridCostos.Visible = True

    End Sub
End Class
