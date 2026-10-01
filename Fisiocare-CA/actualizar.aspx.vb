Imports System.Data.SqlClient
Partial Class actualizar
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Sub grabadatosUpdateLocal(ByVal cadena As String)
        Dim conexionL As SqlConnection = funciones.conecta
        Dim comandoL As SqlCommand = conexionL.CreateCommand
        comandoL.CommandText = cadena
        conexionL.Open()
        comandoL.ExecuteNonQuery()
        conexionL.Close()
    End Sub
    Sub remotoLocal()
        '**** copio los catalogos del principal a la base
        Dim srvRemoto As String = funciones.cadenaremoto
        Dim conexionL As SqlConnection = funciones.conecta
        Dim comandoL As SqlCommand = conexionL.CreateCommand
        comandoL.CommandText = "delete from catcostos;delete from catresponsables;delete from catservicios"
        conexionL.Open()
        comandoL.ExecuteNonQuery()

        comandoL.CommandText = "SET IDENTITY_INSERT catCostos on  insert into catCostos(idCosto,id_servicio," & _
        "descripcion,costo,activo) select * from openrowset('sqloledb'," + srvRemoto + " ," & _
        "'select * from catCostos' ) as catCostos SET IDENTITY_INSERT catCostos off "
        comandoL.ExecuteNonQuery()

        comandoL.CommandText = "insert into catResponsables(id_responsable,descripcion) select * from " & _
        "openrowset('sqloledb'," + srvRemoto + " ,'select * from catResponsables' ) as catResponsables "
        comandoL.ExecuteNonQuery()

        comandoL.CommandText = "insert into catServicios(id_servicio,descripcion,activo) select * from " & _
        "openrowset('sqloledb'," + srvRemoto + " ,'select * from catServicios' ) as catServicios "
        comandoL.ExecuteNonQuery()
        conexionL.Close()
        conexionL.Dispose()

        '************+actualizo los movimiento del servidor central
        Dim conexionR As SqlConnection = funciones.conectaR
        Dim comandoR As SqlCommand = conexionR.CreateCommand

        comandoR.CommandText = "select distinct idabono,abono from abonos where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        conexionR.Open()
        Dim leerR As SqlDataReader = comandoR.ExecuteReader
        Dim cActualiza As String = ""
        Do While leerR.Read
            cActualiza = "update abonos set abono='" + leerR.GetValue(1).ToString.Trim + "' " & _
            " where idabono='" + leerR.GetValue(0).ToString.Trim + "'"
            grabadatosUpdateLocal(cActualiza)
        Loop
        leerR.Close()

        comandoR.CommandText = "select idfactura,capcuenta,cancelada,sustituidoX from factura where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leerR = comandoR.ExecuteReader
        cActualiza = ""
        Do While leerR.Read
            cActualiza = "update factura set capcuenta='" + leerR.GetValue(1).ToString.Trim + "', " & _
            "cancelada='" + leerR.GetValue(2).ToString + "', sustituidoX='" + leerR.GetValue(3).ToString.Trim + "' " & _
            " where idfactura='" + leerR.GetValue(0).ToString.Trim + "'"
            grabadatosUpdateLocal(cActualiza)
        Loop
        leerR.Close()

        comandoR.CommandText = "select DISTINCT num_factura from factura where cancelada='TRUE' and datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leerR = comandoR.ExecuteReader
        cActualiza = ""
        Do While leerR.Read
            cActualiza = cActualiza + " num_factura='" + leerR.GetValue(0).ToString.Trim + "' or"
        Loop
        leerR.Close()
        cActualiza = "(" + Left(cActualiza, cActualiza.Length - 2) + ")"
        If cActualiza <> "" Then
            cActualiza = "update abonos set num_factura=NULL,facturado='false' where " + cActualiza + " AND datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        End If
        grabadatosUpdateLocal(cActualiza)

        conexionR.Close()
        conexionR.Dispose()
    End Sub
    Sub localRemoto()
        '*******+ copio los datos del server de la sucursal al principal
        Dim conexionR As SqlConnection = funciones.conectaR
        Dim comandoR As SqlCommand = conexionR.CreateCommand
        comandoR.CommandText = "delete from abonos where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "';" & _
        "delete from agenda where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "';" & _
        "delete from cancelaciones where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "';" & _
        "delete from catdoctores;delete from catfacturas;delete from clientes;delete from consecutivofac;" & _
        "delete from ejercicios;delete from factura where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'" & _
        ";delete from horarios2017;delete from semanasagendar;delete from terapia"
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()
        conexionR.Dispose()

        Dim conexionL As SqlConnection = funciones.conecta
        Dim comandoL As SqlCommand = conexionL.CreateCommand
        Dim leer As SqlDataReader

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select id,nombre,direccion,ciudad,rfc from catfacturas"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT catfacturas on  insert into catfacturas(id,nombre," & _
        "direccion,ciudad,rfc) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
            ",'" + leer.GetValue(2).ToString + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
            ") SET IDENTITY_INSERT catfacturas off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idcita,cancelacion,fecha from cancelaciones where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "insert into cancelaciones(idcita,cancelacion,fecha)values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
                    ",'" + Left(leer.GetValue(2).ToString, 10) + "')"
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        If comandoR.CommandText <> "" Then
            comandoR.ExecuteNonQuery()
        End If
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select id,nombre,paterno,materno from catdoctores"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT catdoctores on  insert into catdoctores(id,nombre," & _
            "paterno,materno) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
            ",'" + leer.GetValue(2).ToString + "','" + leer.GetValue(3).ToString.Trim + "" & _
            "') SET IDENTITY_INSERT catdoctores off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select id,idTerapia,tipo,texto from ejercicios"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        comandoR.Parameters.Add(New SqlParameter("@texto", Data.SqlDbType.Text))
        Do While leer.Read
            comandoR.CommandText = "SET IDENTITY_INSERT ejercicios on  insert into ejercicios(id,idTerapia," & _
        "tipo,texto) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
                   ",'" + leer.GetValue(2).ToString.Trim + "',@texto)" & _
                   " SET IDENTITY_INSERT ejercicios off "
            comandoR.Parameters("@texto").Value = leer.GetValue(3).ToString
            conexionR.Open()
            comandoR.ExecuteNonQuery()
            conexionR.Close()
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idCliente,paterno,materno,nombre,elnombre,domicilio,telefono," & _
                "celular,adeudo,ultimopago,idDoctor,rfc from clientes"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            Dim AUX As String = Left(leer.GetValue(9).ToString, 10)
            If leer.GetValue(9).ToString = " " Then
                AUX = "NULL"
            End If
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT clientes on  insert into clientes(idCliente,paterno," & _
        "materno,nombre,elnombre,domicilio,telefono,celular,adeudo,ultimopago,idDoctor,rfc) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
       ",'" + leer.GetValue(2).ToString + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
       ",'" + leer.GetValue(5).ToString.Trim + "','" + leer.GetValue(6).ToString + "','" + leer.GetValue(7).ToString + "'" & _
       ",'" + leer.GetValue(8).ToString.Trim + "','" + AUX + "','" + leer.GetValue(10).ToString + "'" & _
       ",'" + leer.GetValue(11).ToString + "') SET IDENTITY_INSERT clientes off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idabono,idcliente,fecha,abono,importe,idcita,idcosto,facturado," & _
        "num_factura,reciboPagado,idDoctor from abonos where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT abonos on  insert into abonos(idabono,idcliente,fecha,abono,importe,idcita,idcosto,facturado," & _
        "num_factura,reciboPagado,idDoctor) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
        ",'" + Left(leer.GetValue(2).ToString, 10) + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
        ",'" + leer.GetValue(5).ToString.Trim + "','" + leer.GetValue(6).ToString + "','" + leer.GetValue(7).ToString + "'" & _
        ",'" + leer.GetValue(8).ToString.Trim + "','" + leer.GetValue(9).ToString + "','" + leer.GetValue(10).ToString + "') SET IDENTITY_INSERT abonos off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        If comandoR.CommandText <> "" Then
            comandoR.ExecuteNonQuery()
        End If
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idcliente,idhorario,fecha,fila,posicion,idCita,estado,idDoctor from agenda where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT agenda on  insert into agenda(idcliente,idhorario,fecha,fila," & _
            "posicion,idCita,estado,idDoctor) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
                    ",'" + Left(leer.GetValue(2).ToString, 10) + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
                    ",'" + leer.GetValue(5).ToString.Trim + "','" + leer.GetValue(6).ToString + "','" + leer.GetValue(7).ToString + "'" & _
                    ") SET IDENTITY_INSERT agenda off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        If comandoR.CommandText <> "" Then
            comandoR.ExecuteNonQuery()
        End If
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idFactura,num_factura,fecha,cantidad,idcosto,importe,nombre,direccion" & _
        ",ciudad,rfc,capCuenta,paciente,cancelada,observaciones,sustituidoX from factura where datepart(yyyy, fecha)='" + cmbaño.SelectedValue + "'"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT factura on  insert into factura(idFactura,num_factura,fecha," & _
        "cantidad,idcosto,importe,nombre,direccion,ciudad,rfc,capCuenta,paciente,cancelada,observaciones" & _
        ",sustituidoX) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
        ",'" + Left(leer.GetValue(2).ToString, 10) + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
        ",'" + leer.GetValue(5).ToString.Trim + "','" + leer.GetValue(6).ToString + "','" + leer.GetValue(7).ToString + "'" & _
        ",'" + leer.GetValue(8).ToString.Trim + "','" + leer.GetValue(9).ToString + "','" + leer.GetValue(10).ToString + "'" & _
        ",'" + leer.GetValue(11).ToString.Trim + "','" + leer.GetValue(12).ToString.Trim + "','" + leer.GetValue(13).ToString.Trim + "'" & _
        ",'" + leer.GetValue(14).ToString + "') SET IDENTITY_INSERT factura off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        If comandoR.CommandText <> "" Then
            comandoR.ExecuteNonQuery()
        End If
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select horario,idhorario,habilitado from horarios2017"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "insert into horarios2017(horario,idhorario,habilitado) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
                               ",'" + leer.GetValue(2).ToString + "')"
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select semanas_agendar from semanasAgendar"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "insert into semanasAgendar(semanas_agendar) values('" + leer.GetValue(0).ToString + "')"
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select idterapia,idcliente,poliza" & _
        ",certificado,noSiniestro,edad,responsable,medico,diagnostico,fechainicio,noSesiones,muscular,basica," & _
        "laser,consulta,movimiento,adicional,hospital from terapia"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "SET IDENTITY_INSERT terapia on  insert into terapia(idterapia,idcliente,poliza" & _
        ",certificado,noSiniestro,edad,responsable,medico,diagnostico,fechainicio,noSesiones,muscular,basica," & _
        "laser,consulta,movimiento,adicional,hospital) values('" + leer.GetValue(0).ToString + "','" + leer.GetValue(1).ToString.Trim + "'" & _
                    ",'" + leer.GetValue(2).ToString + "','" + leer.GetValue(3).ToString.Trim + "','" + leer.GetValue(4).ToString + "'" & _
                    ",'" + leer.GetValue(5).ToString.Trim + "','" + leer.GetValue(6).ToString + "','" + leer.GetValue(7).ToString + "'" & _
                    ",'" + leer.GetValue(8).ToString.Trim + "','" + Left(leer.GetValue(9).ToString, 10) + "','" + leer.GetValue(10).ToString + "'" & _
                    ",'" + leer.GetValue(11).ToString.Trim + "','" + leer.GetValue(12).ToString.Trim + "','" + leer.GetValue(13).ToString.Trim + "'" & _
                    ",'" + leer.GetValue(14).ToString + "','" + leer.GetValue(15).ToString + "'," & _
                    "'" + leer.GetValue(16).ToString + "','" + leer.GetValue(17).ToString + "') SET IDENTITY_INSERT terapia off "
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()

        conexionL = funciones.conecta
        conexionL.Open()
        comandoL = conexionL.CreateCommand()
        comandoL.CommandText = "select consecutivo,serie from consecutivofac"
        leer = comandoL.ExecuteReader
        conexionR = funciones.conectaR
        comandoR = conexionR.CreateCommand
        Do While leer.Read
            comandoR.CommandText = comandoR.CommandText + "insert into consecutivofac(consecutivo,serie) values('" + leer.GetValue(0).ToString + "'" & _
                    ",'" + leer.GetValue(1).ToString + "')"
        Loop
        leer.Close()
        conexionL.Close()
        conexionL.Dispose()
        TextBox1.Text = comandoR.CommandText
        conexionR.Open()
        comandoR.ExecuteNonQuery()
        conexionR.Close()
        '***********
    End Sub
    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        'Try
        'TextBox1.Visible = False
        remotoLocal()
        'Catch ex As SqlException
        'Messagebox1.ShowMessage("'ERROR': OCURRIO UN ERROR MIENTAS SE ACTUALIZABA DEL SERVIDOR REMOTO AL LOCAL ....... :(" + ex.Message.ToString)
        'TextBox1.Visible = True
        'End Try
        'Try
        'TextBox1.Visible = False
        localRemoto()
        Messagebox1.ShowMessage("LA BASE DE DATOS HA SIDO ACTUALIZADA GRACIAS ...")
        'Catch ex As Exception
        'Messagebox1.ShowMessage("'ERROR': OCURRIO UN ERROR MIENTAS SE ACTUALIZABA DEL SERVIDOR LOCAL AL REMOTO ....... :(" + ex.Message.ToString)
        'TextBox1.Visible = True
        'End Try
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not (Page.IsPostBack) Then
            Try
                lblfecha.Text = Context.Items("fecha").ToString.Trim
            Catch
                Response.Redirect("default.aspx")
            End Try
        End If

    End Sub

    Protected Sub LinkButton5_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButton5.Click
        Context.Items.Add("fecha", lblfecha.Text.Trim)
        Server.Transfer("default.aspx", False)
    End Sub
End Class
