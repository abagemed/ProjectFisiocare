Imports System.Data.SqlClient
Imports Microsoft.VisualBasic
Imports System.Data
Public Class gridsEnviados
    Dim clase As New miclases
    Function llenaEnviados(ByVal gridDatos As DataGrid, ByVal txtfecha1 As String, ByVal txtfecha2 As String, ByVal miBase As String, ByVal diasProyectar As Integer) As DataGrid
        Dim AUX As Date = txtfecha1
        AUX = txtfecha2
        gridDatos.Visible = True
        Dim conexion As SqlConnection = clase.conecta(miBase)
        Dim comando As SqlCommand = conexion.CreateCommand

        Dim miscampos As String = ""
        Dim miscampos2 As String = ""
        Dim camposSum As String = ""
        Dim fecha1 As Date = txtfecha1.Trim
        Dim fecha2 As Date = txtfecha2.Trim
        Dim numDias As Int64 = 0
        Do While fecha1 <= fecha2
            Dim auxCampo As String = fecha1.Date.Day.ToString.PadLeft(2, "0") + "/" + fecha1.Month.ToString.PadLeft(2, "0")
            miscampos = miscampos + " ,convert(float,isnull([" + auxCampo + "],0)) as '" + auxCampo + "'"
            miscampos2 = miscampos2 + " ,[" + auxCampo + "] "
            camposSum = camposSum + "+isnull([" + auxCampo + "],0)"
            fecha1 = fecha1.AddDays(1)
            numDias = numDias + 1
        Loop
        camposSum = "0" + camposSum
        If miscampos <> "" Then
            miscampos = Right(miscampos, Len(miscampos) - 2)
            miscampos2 = Right(miscampos2, Len(miscampos2) - 2)
            Dim consulta As String = "select nombre," + miscampos + ", (" + camposSum + ") as Acumulado,0 as Estimado from " & _
            "(select nombre,fecha,count(idcliente) as cuenta from (select isnull(" & _
            "'DR. '+nombre+' '+paterno+' '+materno,'DR. SIN REMITENTE DESCONOCIDO') as " & _
            "nombre,left(convert(varchar(10),dtinicio,103),5) as fecha, idcliente from " & _
            "(select temp.dtinicio,temp.idcliente,isnull(clientes.idDoctor,0) as idDoctor from " & _
            "(select distinct dtinicio,idCliente from clientes where " & _
            "(dtinicio>='" + txtfecha1.Trim + "' and dtinicio<='" + txtfecha2.Trim + "')) as temp " & _
            "left join clientes on clientes.idcliente=temp.idcliente) as temp left join catDoctores on " & _
            " temp.idDoctor=id ) as eltemp group by nombre,fecha) as temp pivot (sum(cuenta) " & _
            "for fecha in(" + miscampos2 + ")) as piv order by Acumulado desc "

            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(consulta, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            Dim filaT2 As DataRow = tabla.NewRow

            tabla.TableName = "mitabla"
            gridDatos.DataMember = "mitabla"
            gridDatos.DataSource = tabla.DefaultView
            gridDatos.DataBind()


            If gridDatos.Items.Count > 0 Then

                Dim cuentaF As Integer = gridDatos.Items.Count
                Dim cuentaC As Integer = gridDatos.Items(0).Cells.Count
                Dim sumaF As Double = 0
                Dim columna As Integer = 1
                Dim fila As Integer = 0

                Do While columna < cuentaC - 2
                    Do While fila < cuentaF
                        sumaF = sumaF + gridDatos.Items(fila).Cells(columna).Text
                        fila = fila + 1
                    Loop
                    filaT2(columna) = sumaF
                    fila = 0
                    columna = columna + 1
                    sumaF = 0
                Loop

                fila = 0
                Dim cuentaOtros As Integer = 0
                Do While fila < cuentaF
                    Dim nomColum As String = gridDatos.Items(fila).Cells(0).Text.Trim
                    If nomColum = "DR. FOLLETOS" Or nomColum = "DR. L.R. MIRIAM HERRERA MARRUFO" Or nomColum = "DR. REVISTA PUBLICITARIA" Or nomColum = "DR. HERBE RIVERO ." Or nomColum = "DR. RECOMENDACION DE UN AMIG@" Or nomColum = "DR. SIN REMITENTE DESCONOCIDO" Or nomColum = "DR. RECOMENDACION FAMILIAR     O AMIGO" Or nomColum = "DR. REVSITA PUBLICITARIA" Then
                        columna = 0
                        Dim filaAux As DataRow = tabla.NewRow
                        Do While columna < cuentaC
                            filaAux(columna) = gridDatos.Items(fila).Cells(columna).Text.Trim
                            columna = columna + 1
                        Loop
                        tabla.Rows(fila).Delete()
                        tabla.Rows.Add(filaAux)
                        cuentaOtros = cuentaOtros + 1
                    End If
                    fila = fila + 1
                Loop

                tabla.Rows.Add(filaT2)
                gridDatos.DataMember = "mitabla"
                gridDatos.DataSource = tabla.DefaultView
                gridDatos.DataBind()

                fila = 0
                columna = 1
                sumaF = 0

                Dim totalAcumulado As Integer = 0
                Dim totalTx As Integer = 0
                Dim totalProAcumulado As Integer = 0
                Dim totalProtx As Integer = 0
                Dim sumPacientesNvo As Integer = 0

                Do While fila < cuentaF + 1
                    columna = 1
                    sumaF = 0
                    Do While columna < cuentaC - 2
                        sumaF = Convert.ToDouble(sumaF) + Convert.ToDouble(gridDatos.Items(fila).Cells(columna).Text)
                        If fila = cuentaF Then
                            gridDatos.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                        Else
                            gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#ffff00")
                        End If
                        columna = columna + 1
                    Loop
                    If cuentaC > 1 Then
                        gridDatos.Items(fila).Cells(cuentaC - 2).Text = Int(sumaF)
                        gridDatos.Items(fila).Cells(cuentaC - 1).Text = Math.Round((Int(sumaF) / numDias) * diasProyectar)
                    End If
                    fila = fila + 1
                Loop
                gridDatos.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)

                cuentaF = gridDatos.Items.Count - (cuentaOtros + 1)
                Do While cuentaF < gridDatos.Items.Count - 1
                    'gridDatos.Items(cuentaF).BackColor = Drawing.Color.Beige
                    gridDatos.Items(cuentaF).Cells(0).Text = Right(gridDatos.Items(cuentaF).Cells(0).Text.Trim, gridDatos.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
                    cuentaF = cuentaF + 1
                Loop
            End If
        Else
            gridDatos.Visible = False
        End If
        Return gridDatos
    End Function

    Function llenacortesias(ByVal gridDatos As DataGrid, ByVal txtfecha1 As String, ByVal txtfecha2 As String, ByVal miBase As String) As DataGrid

        Dim AUX As Date = txtfecha1
        AUX = txtfecha2
        gridDatos.Visible = True
        Dim conexion As SqlConnection = clase.conecta(miBase)
        Dim comando As SqlCommand = conexion.CreateCommand

        Dim miscampos As String = ""
        Dim miscampos2 As String = ""
        Dim fecha1 As Date = txtfecha1.Trim
        Dim fecha2 As Date = txtfecha2.Trim
        Dim numDias As Int64 = 0
        Do While fecha1 <= fecha2
            Dim auxCampo As String = fecha1.Date.Day.ToString.PadLeft(2, "0") + "/" + fecha1.Month.ToString.PadLeft(2, "0")
            miscampos = miscampos + " ,convert(float,isnull([" + auxCampo + "],0)) as '" + auxCampo + "'"
            miscampos2 = miscampos2 + " ,[" + auxCampo + "] "
            fecha1 = fecha1.AddDays(1)
            numDias = numDias + 1
        Loop

        If miscampos <> "" Then
            miscampos = Right(miscampos, Len(miscampos) - 2)
            miscampos2 = Right(miscampos2, Len(miscampos2) - 2)
            Dim consulta As String = "select id_servicio,descripcion,Importe," + miscampos + ", 0 as Acumulado " & _
            "from ( select '' as id_servicio,fecha,importe,sum(cuenta) as cuenta,'CAMPESTRE' AS descripcion from( " & _
            "SELECT left(convert(varchar(10),fecha,103),5) as fecha, importe,count(abonos.idcosto) as cuenta,abonos.idcosto," & _
            "descripcion,id_servicio FROM  abonos  inner join (select idCosto,id_servicio,isnull(left(nombre,4),'Part')  as descripcion " & _
            "from catcostos left join catfacturas on catcostos.iddatosfac=catfacturas.iddatosfac) as catcostos on " & _
            "abonos.idcosto=catcostos.idcosto where fecha>='" + txtfecha1.Trim + "' and fecha<='" + txtfecha2.Trim + "' and importe=0 group by " & _
            "fecha,abonos.idcosto,abonos.importe,descripcion,id_servicio) as tempx group by id_servicio,fecha,importe,descripcion) as temp pivot (sum(cuenta) " & _
            "for fecha in(" + miscampos2 + ")) as piv "
            consulta = consulta + " union all select id_servicio,descripcion,Importe," + miscampos + ", 0 as Acumulado " & _
            "from ( select '' as id_servicio,fecha,importe,sum(cuenta) as cuenta,'STAR MEDICA' AS descripcion from( " & _
            "SELECT left(convert(varchar(10),fecha,103),5) as fecha, importe,count(abonos.idcosto) as cuenta,abonos.idcosto," & _
            "descripcion,id_servicio FROM  fisiocaresm.dbo.abonos  inner join (select idCosto,id_servicio,isnull(left(nombre,4),'Part')  as descripcion " & _
            "from fisiocaresm.dbo.catcostos left join fisiocaresm.dbo.catfacturas on catcostos.iddatosfac=catfacturas.iddatosfac) as catcostos on " & _
            "abonos.idcosto=catcostos.idcosto where fecha>='" + txtfecha1.Trim + "' and fecha<='" + txtfecha2.Trim + "' and importe=0 group by " & _
            "fecha,abonos.idcosto,abonos.importe,descripcion,id_servicio) as tempx group by id_servicio,fecha,importe,descripcion) as temp pivot (sum(cuenta) " & _
            "for fecha in(" + miscampos2 + ")) as piv "
            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(consulta, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            Dim filaT2 As DataRow = tabla.NewRow

            tabla.TableName = "mitabla"
            gridDatos.DataMember = "mitabla"
            gridDatos.DataSource = tabla.DefaultView
            gridDatos.DataBind()


            If gridDatos.Items.Count > 0 Then

                Dim cuentaF As Integer = gridDatos.Items.Count
                Dim cuentaC As Integer = gridDatos.Items(0).Cells.Count
                Dim sumaF As Double = 0
                Dim columna As Integer = 3
                Dim fila As Integer = 0

                Do While columna < cuentaC - 1
                    Do While fila < cuentaF
                        sumaF = sumaF + gridDatos.Items(fila).Cells(columna).Text
                        fila = fila + 1
                    Loop
                    filaT2(columna) = sumaF
                    fila = 0
                    columna = columna + 1
                    sumaF = 0
                Loop

                tabla.Rows.Add(filaT2)
                gridDatos.DataMember = "mitabla"
                gridDatos.DataSource = tabla.DefaultView
                gridDatos.DataBind()

                fila = 0
                columna = 3

                sumaF = 0
                Dim totalAcumulado As Integer = 0
                Dim totalTx As Integer = 0
                Dim totalProAcumulado As Integer = 0
                Dim totalProtx As Integer = 0
                Dim sumPacientesNvo As Integer = 0

                Do While fila < cuentaF + 1
                    columna = 3
                    sumaF = 0
                    Do While columna < cuentaC - 1
                        sumaF = Convert.ToDouble(sumaF) + Convert.ToDouble(gridDatos.Items(fila).Cells(columna).Text)
                        If fila = cuentaF Then
                            gridDatos.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                        Else
                            gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#ffff00")
                        End If
                        columna = columna + 1
                    Loop
                    If cuentaC > 3 Then
                        gridDatos.Items(fila).Cells(cuentaC - 1).Text = Int(sumaF)
                    End If
                    fila = fila + 1
                Loop
                gridDatos.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            End If
        Else
                    gridDatos.Visible = False
        End If
        Return gridDatos
    End Function
End Class
