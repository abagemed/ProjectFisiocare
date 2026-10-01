Imports System.Data.SqlClient
Imports System.Data
Partial Class Defzonamed
    Inherits System.Web.UI.Page
    Dim clase As New miclases
    Dim claseGrids As New gridsEnviados
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Dim numdias As Integer = DateTime.DaysInMonth(Now.Year, Now.Month)
            txtNumdias.Text = numdias






        End If
    End Sub
    Protected Sub cmbBocupacion_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbBocupacion.Click
        Try
            lblcampestre.Visible = False
            lblstarmedica.Visible = False
            'lblcampestreE.Visible = False
            'lblstarmedicaE.Visible = False
            lbls.Visible = False
            llenaOcupacion(gridOcupacion, gbaseCP, "rentaszm")
            llenaOcupacion(gridOcupacionSM, gbaseSM, "rentaszm")
            'llenaOcupacion(DataGrid1, DataGrid2, "gym")
            creaEncabezado()
            'llenadoctores()

            'gridOcupacionE = claseGrids.llenaEnviados(gridOcupacionE, txtfecha1.Text.Trim, txtfecha2.Text.Trim, "fisiocarecp", txtNumdias.Text)
            'gridOcupacionSMe = claseGrids.llenaEnviados(gridOcupacionSMe, txtfecha1.Text.Trim, txtfecha2.Text.Trim, "fisiocaresm", txtNumdias.Text)
            'gbaseCPe = claseGrids.llenaEnviados(gbaseCPe, txtfecha1.Text.Trim, txtfecha2.Text.Trim, "fisiocarecp", txtNumdias.Text)
            'gbaseSMe = claseGrids.llenaEnviados(gbaseSMe, txtfecha1.Text.Trim, txtfecha2.Text.Trim, "fisiocaresm", txtNumdias.Text)
            'llenaEncabezadoEnviados()

            'gEncabezadoC = claseGrids.llenacortesias(gEncabezadoC, txtfecha1.Text, txtfecha2.Text, "fisiocarecp")
            ' gbaseEncabezadoC = claseGrids.llenacortesias(gbaseEncabezadoC, txtfecha1.Text, txtfecha2.Text, "fisiocarecp")

            'TextBox1.Text = "http://107.161.180.154/administracion/rptGeneral.aspx?fecha1=" + txtfecha1.Text + "&fecha2=" + txtfecha2.Text + "&numdias=" + txtNumdias.Text + "" & _
            '"&capmaxima=" + txtCapMaxima.Text + "&meta=" + txtmeta.Text + "" & _
            '"&metatx=" + txtmetaTx.Text + "&numterapistas=" + txtnumterapistas.Text + "" & _
            '"&capmaximasm=" + txtcapmaximasm.Text + "&metasm=" + txtmetasm.Text + "&metatxsm=" + txtmetaTxsm.Text + "&numterapistassm=" + txtnumterapistassm.Text + "&metas=" + txtmetas.Text

            'terapiaXdoctor()

            'terapiaIMPXdoctor()

        Catch
            Messagebox1.ShowMessage("DATOS INCORRECTOS FAVOR DE VERIFICAR")
        End Try
    End Sub
    Sub llenaOcupacion(ByVal gridDatos As DataGrid, ByVal gridBase As DataGrid, ByVal miBase As String)

        Dim AUX As Date = txtfecha1.Text
        AUX = txtfecha2.Text
        gridDatos.Visible = True
        Dim conexion As SqlConnection = clase.conecta(miBase)
        Dim comando As SqlCommand = conexion.CreateCommand
        Dim coaseguro As String = " and coaseguro='false' "
        If miBase = "gym" Then
            coaseguro = " "
        End If
        Dim miscampos As String = ""
        Dim miscampos2 As String = ""
        Dim fecha1 As Date = txtfecha1.Text.Trim
        Dim fecha2 As Date = txtfecha2.Text.Trim
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
            Dim fecha As Date = txtfecha1.Text.Trim
            Dim fechax2 As Date = "01/05/2013"
            Dim liga As String = ""
            'If fecha < fechax2 Or miBase = "gym" Then
            '    liga = "select idCosto,id_servicio,isnull(left(nombre,4)," & _
            '    " 'Part')  as descripcion from catcostos left join catfacturas on catcostos.iddatosfac=catfacturas.iddatosfac"
            'Else
            liga = "select CIDPRODUCTO,CIDVALORCLASIFICACION2,isnull(left(admClasificacionesValores.CVALORCLASIFICACION,20)," & _
            " 'SinClasificacion')  as CVALORCLASIFICACION from admProductos left join admClasificacionesValores on admProductos.CIDVALORCLASIFICACION2=admClasificacionesValores.CIDVALORCLASIFICACION"
            'End If
            Dim consulta As String = "select CIDVALORCLASIFICACION,CVALORCLASIFICACION,ctotal," + miscampos + ",0 as TAcumuladoTx, 0.0 as Acumulado$,0.0 as AcumuladoTx,0.0 as Estimado$,0.0 as EstimadoTx, " & _
            "'' as capMax, '' as MetaTx, '' as Meta$, '' as dif_EstimadoTx_MetaTx,'' as Dif_Meta$_Estimado$,'' as #Terapistas,'' as TxEstimadoDiarias,'' as TxEstimadoxTerapista from " & _
            "(SELECT left(convert(varchar(10),CFECHA,103),5) as fecha, ctotal,count(admMovimientos.CIDPRODUCTO) as cuenta," & _
            "admMovimientos.CIDPRODUCTO,cnombrep01,admClasificacionesValores.CIDVALORCLASIFICACION FROM  admMovimientos  inner join (" + liga + ") as admProductos on " & _
            "admMovimientos.CIDPRODUCTO=admProductos.CIDPRODUCTO where CFECHA>='" + txtfecha1.Text.Trim + "' and " & _
            "CFECHA<='" + txtfecha2.Text.Trim + "' and ctotal<>0 " + coaseguro + " group by " & _
            "CFECHA,admMovimientos.CIDPRODUCTO,admMovimientos.ctotal,cnombrep01,admClasificacionesValores.CIDVALORCLASIFICACION) as temp pivot (sum(cuenta) " & _
            "for CFECHA in(" + miscampos2 + ")) as piv order by admClasificacionesValores.CIDVALORCLASIFICACION"

            ' Dim consulta As String = "select id_servicio,descripcion,Importe," + miscampos + ",0 as TAcumuladoTx, 0.0 as Acumulado$,0.0 as AcumuladoTx,0.0 as Estimado$,0.0 as EstimadoTx, " & _
            '"'' as capMax, '' as MetaTx, '' as Meta$, '' as dif_EstimadoTx_MetaTx,'' as Dif_Meta$_Estimado$,'' as #Terapistas,'' as TxEstimadoDiarias,'' as TxEstimadoxTerapista from " & _
            '"(SELECT left(convert(varchar(10),fecha,103),5) as fecha, importe,count(abonos.idcosto) as cuenta," & _
            '"abonos.idcosto,descripcion,id_servicio FROM  abonos  inner join (" + liga + ") as catcostos on " & _
            '"abonos.idcosto=catcostos.idcosto where fecha>='" + txtfecha1.Text.Trim + "' and " & _
            '"fecha<='" + txtfecha2.Text.Trim + "' and importe<>0 " + coaseguro + " group by " & _
            '"fecha,abonos.idcosto,abonos.importe,descripcion,id_servicio) as temp pivot (sum(cuenta) " & _
            '"for fecha in(" + miscampos2 + ")) as piv order by id_servicio"



            Dim odataAdapter As SqlDataAdapter
            odataAdapter = New SqlDataAdapter(consulta, conexion)
            Dim tabla As DataTable
            tabla = New DataTable
            odataAdapter.Fill(tabla)
            Dim filaT2 As DataRow = tabla.NewRow
            Dim filaNuevosPacientes As DataRow = tabla.NewRow

            tabla.TableName = "mitabla"
            gridDatos.DataMember = "mitabla"
            gridDatos.DataSource = tabla.DefaultView
            gridDatos.DataBind()
            gridBase.DataMember = "mitabla"
            gridBase.DataSource = tabla.DefaultView
            gridBase.DataBind()



            Dim auxTabla As String = "clientes"
            If miBase = "gym" Then
                auxTabla = "agenda"
            End If
            comando.CommandText = "select " + miscampos + "  from (select descripcion,fecha," & _
            "count(idcliente) as cuenta from(select distinct left(convert(varchar(10),dtinicio,103),5) " & _
            "as fecha,idCliente,'Pacientes Nuevos' as descripcion from " + auxTabla + " where " & _
            "dtinicio>='" + txtfecha1.Text.Trim + "' and dtinicio<='" + txtfecha2.Text.Trim + "') as cliente " & _
            "group by fecha,descripcion) as temp pivot(sum(cuenta) for fecha in(" + miscampos2 + ")) as piv "
            conexion.Open()
            Dim oLeer As SqlDataReader = comando.ExecuteReader

            If gridDatos.Items.Count > 0 Then
                lblcampestre.Visible = True
                lblstarmedica.Visible = True
                lblcampestreE.Visible = True
                lblstarmedicaE.Visible = True
                lbls.Visible = True
                Dim cuentaF As Integer = gridDatos.Items.Count
                Dim cuentaC As Integer = gridDatos.Items(0).Cells.Count
                Dim sumaF As Double = 0
                Dim columna As Integer = 3
                Dim fila As Integer = 0
                oLeer.Read()
                Do While columna < cuentaC - 12
                    Do While fila < cuentaF
                        sumaF = sumaF + gridDatos.Items(fila).Cells(columna).Text
                        fila = fila + 1
                    Loop
                    filaT2(columna) = sumaF
                    Try
                        filaNuevosPacientes(columna) = oLeer.GetValue(columna - 3).ToString
                    Catch
                        filaNuevosPacientes(columna) = 0
                    End Try
                    fila = 0
                    columna = columna + 1
                    sumaF = 0
                Loop
                oLeer.Close()
                conexion.Close()

                tabla.Rows.Add(filaT2)
                tabla.Rows.Add(filaNuevosPacientes)
                gridDatos.DataMember = "mitabla"
                gridDatos.DataSource = tabla.DefaultView
                gridDatos.DataBind()
                gridBase.DataMember = "mitabla"
                gridBase.DataSource = tabla.DefaultView
                gridBase.DataBind()
                fila = 0
                columna = 3

                ''agrupo costos
                Do While fila < cuentaF
                    Dim tipoTerapia As String = gridDatos.Items(fila).Cells(0).Text
                    Dim costo As String = gridDatos.Items(fila).Cells(2).Text
                    Dim filaaux As Integer = fila
                    If gridDatos.Items(fila).Visible Then
                        Do While filaaux < cuentaF
                            filaaux = filaaux + 1
                            If filaaux < cuentaF Then
                                If tipoTerapia = gridDatos.Items(filaaux).Cells(0).Text And costo = gridDatos.Items(filaaux).Cells(2).Text Then
                                    gridDatos.Items(fila).Cells(1).Text = gridDatos.Items(fila).Cells(1).Text + "," + gridDatos.Items(filaaux).Cells(1).Text
                                    gridBase.Items(fila).Cells(1).Text = gridDatos.Items(fila).Cells(1).Text + "," + gridDatos.Items(filaaux).Cells(1).Text
                                    columna = 3
                                    Do While columna < cuentaC - 8
                                        gridDatos.Items(fila).Cells(columna).Text = Convert.ToDouble(gridDatos.Items(fila).Cells(columna).Text) + Convert.ToDouble(gridDatos.Items(filaaux).Cells(columna).Text)
                                        columna = columna + 1
                                    Loop
                                    gridDatos.Items(filaaux).Visible = False
                                    gridBase.Items(filaaux).Visible = False
                                    If Len(gridBase.Items(fila).Cells(1).Text) > 30 Then
                                        gridBase.Items(fila).Cells(1).Text = Left(gridBase.Items(fila).Cells(1).Text, 30)
                                    End If

                                End If
                            End If
                        Loop
                    End If

                    fila = fila + 1
                Loop
                ''''
                columna = 3
                fila = 0

                sumaF = 0
                Dim totalAcumulado As Integer = 0
                Dim totalTx As Integer = 0
                Dim totalTx2 As Integer = 0
                Dim totalProAcumulado As Integer = 0
                Dim totalProtx As Integer = 0
                Dim sumPacientesNvo As Integer = 0

                Do While fila < cuentaF
                    If gridDatos.Items(fila).Visible Then
                        columna = 3
                        Do While columna < cuentaC - 8
                            sumaF = Convert.ToDouble(sumaF) + Convert.ToDouble(gridDatos.Items(fila).Cells(columna).Text)
                            Select Case Left(gridDatos.Items(fila).Cells(0).Text.Trim, 2)
                                Case "CM"
                                    gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#00B0F0")
                                    gridBase.Items(fila).BackColor = Drawing.ColorTranslator.FromHtml("#00B0F0")
                                Case "TH"
                                    gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#ffc000")
                                    gridBase.Items(fila).BackColor = Drawing.ColorTranslator.FromHtml("#ffc000")
                                Case "TD"
                                    gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#f79646")
                                    gridBase.Items(fila).BackColor = Drawing.ColorTranslator.FromHtml("#f79646")
                                Case "HT"
                                    gridDatos.Items(fila).Cells(columna).BackColor = Drawing.Color.FromArgb(83, 142, 213)
                                    gridBase.Items(fila).BackColor = Drawing.Color.FromArgb(83, 142, 213)
                                Case Else
                                    gridDatos.Items(fila).Cells(columna).BackColor = Drawing.ColorTranslator.FromHtml("#ffff00")
                                    gridBase.Items(fila).BackColor = Drawing.Color.White
                            End Select
                            If fila + 1 = cuentaF Then
                                If columna < cuentaC - 12 Then
                                    sumPacientesNvo = sumPacientesNvo + Convert.ToDecimal(gridDatos.Items(cuentaF + 1).Cells(columna).Text)
                                End If
                            End If
                            columna = columna + 1
                        Loop
                        Dim auxSumaf As Integer = sumaF
                        '' ponderar las terapias menores a 190
                        If Convert.ToDouble(gridDatos.Items(fila).Cells(2).Text) < 0 Then
                            auxSumaf = CInt(sumaF * 0.0)
                        End If
                        '' fin
                        gridDatos.Items(fila).Cells(cuentaC - 13).Text = sumaF
                        gridDatos.Items(fila).Cells(cuentaC - 13).BackColor = Drawing.Color.White
                        gridDatos.Items(fila).Cells(cuentaC - 11).Text = auxSumaf
                        gridDatos.Items(fila).Cells(cuentaC - 11).BackColor = Drawing.Color.White
                        gridDatos.Items(fila).Cells(cuentaC - 12).Text = "$" + Format(Convert.ToDouble(sumaF) * Convert.ToDouble(gridDatos.Items(fila).Cells(2).Text), "###,###,###0.00").ToString
                        gridDatos.Items(fila).Cells(cuentaC - 12).BackColor = Drawing.Color.White
                        gridDatos.Items(fila).Cells(cuentaC - 9).Text = Math.Round(Math.Round(Convert.ToDouble(gridDatos.Items(fila).Cells(cuentaC - 11).Text / numDias), 2) * txtNumdias.Text.Trim)
                        gridDatos.Items(fila).Cells(cuentaC - 9).BackColor = Drawing.Color.White
                        gridDatos.Items(fila).Cells(cuentaC - 10).Text = "$" + Format(Math.Round(Convert.ToDouble(gridDatos.Items(fila).Cells(cuentaC - 12).Text / numDias) * txtNumdias.Text), "###,###,###0.00").ToString
                        gridDatos.Items(fila).Cells(cuentaC - 10).BackColor = Drawing.Color.White
                        totalAcumulado = totalAcumulado + Convert.ToDouble(sumaF) * Convert.ToDouble(gridDatos.Items(fila).Cells(2).Text)
                        totalTx = totalTx + auxSumaf
                        totalTx2 = totalTx2 + sumaF
                        totalProAcumulado = totalProAcumulado + gridDatos.Items(fila).Cells(cuentaC - 10).Text
                        totalProtx = totalProtx + gridDatos.Items(fila).Cells(cuentaC - 9).Text
                        sumaF = 0
                        If miBase = "gym" Then
                            gridDatos.Items(fila).Cells(cuentaC - 10).Text = ""
                        End If

                        gridDatos.Items(fila).Cells(cuentaC - 12).HorizontalAlign = HorizontalAlign.Right
                        gridDatos.Items(fila).Cells(cuentaC - 10).HorizontalAlign = HorizontalAlign.Right
                    Else
                        ''por si la ultima columna esta oculta pueda sumar los pacientes nuevos
                        If fila + 1 = cuentaF Then
                            columna = 3
                            Do While columna < cuentaC - 12
                                sumPacientesNvo = sumPacientesNvo + Convert.ToDecimal(gridDatos.Items(cuentaF + 1).Cells(columna).Text)
                                columna = columna + 1
                            Loop
                        End If
                    End If
                    fila = fila + 1
                Loop
                gridDatos.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gridDatos.Items(cuentaF + 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gridBase.Items(cuentaF).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gridBase.Items(cuentaF + 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
                gridBase.Items(cuentaF).Cells(1).Text = "TOTAL"
                gridBase.Items(cuentaF + 1).Cells(1).Text = "PACIENTES NUEVOS"
                gridDatos.Items(cuentaF).Cells(cuentaC - 13).Text = Format(totalTx2, "###,###,###") ''totalTX2 sin calculo de precio
                gridDatos.Items(cuentaF).Cells(cuentaC - 11).Text = Format(totalTx, "###,###,###") ''totalTX
                gridDatos.Items(cuentaF).Cells(cuentaC - 12).Text = "$" + Format(totalAcumulado, "###,###,###0.00").ToString ''total$
                gridDatos.Items(cuentaF + 1).Cells(cuentaC - 13).Text = sumPacientesNvo.ToString
                gridDatos.Items(cuentaF).Cells(cuentaC - 10).Text = "$" + Format(totalProAcumulado, "###,###,###0.00").ToString ''proyeccion$
                'proyecccion gym
                If miBase = "gym" Then
                    Dim auxFecha As Date = fecha1.AddMonths(-1)
                    Dim auxProyectaGym As Double = Convert.ToDouble(clase.leerValor("select sum(importe) from abonos where month(fecha)=" + auxFecha.Month.ToString + " and year(fecha)='" + auxFecha.Year.ToString + "'", miBase))
                    If totalAcumulado >= auxProyectaGym Then
                        auxProyectaGym = totalAcumulado
                    End If
                    gridDatos.Items(cuentaF).Cells(cuentaC - 10).Text = "$" + Format(auxProyectaGym, "###,###,###0.00")
                End If
                '*****
                gridDatos.Items(cuentaF).Cells(cuentaC - 9).Text = Format(totalProtx, "###,###,###") ''proyeccion tx
                Select Case miBase
                    Case "fisiocarecp"
                        gridDatos.Items(cuentaF).Cells(cuentaC - 8).Text = Format(Convert.ToDouble(txtCapMaxima.Text.Trim), "###,###,###") ''capMax
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).Text = Format(Convert.ToDouble(txtmetaTx.Text.Trim), "###,###,###") ''metatx
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).Text = "$" + Format(Convert.ToDouble(txtmeta.Text.Trim), "###,###,###0.00") ''meta$
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 3).Text = txtnumterapistas.Text.Trim ''#terapistas
                        gridDatos.Items(cuentaF).Cells(cuentaC - 5).Text = Format(Convert.ToDouble(totalProtx - txtmetaTx.Text.Trim), "###,###,###")
                        gridDatos.Items(cuentaF).Cells(cuentaC - 4).Text = "$" + Format(Convert.ToDouble(totalProAcumulado - txtmeta.Text.Trim), "###,###,###0.00")
                        gridDatos.Items(cuentaF).Cells(cuentaC - 2).Text = Math.Round(totalProtx / txtNumdias.Text, 2)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 1).Text = Math.Round(gridDatos.Items(cuentaF).Cells(cuentaC - 2).Text / txtnumterapistas.Text, 2)
                    Case "fisiocaresm"
                        gridDatos.Items(cuentaF).Cells(cuentaC - 8).Text = Format(Convert.ToDouble(txtcapmaximasm.Text.Trim), "###,###,###") ''capMax
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).Text = Format(Convert.ToDouble(txtmetaTxsm.Text.Trim), "###,###,###") ''metatx
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).Text = "$" + Format(Convert.ToDouble(txtmetasm.Text.Trim), "###,###,###0.00") ''meta$
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 3).Text = txtnumterapistassm.Text.Trim ''#terapistas
                        gridDatos.Items(cuentaF).Cells(cuentaC - 5).Text = Format(Convert.ToDouble(totalProtx - txtmetaTxsm.Text.Trim), "###,###,###")
                        gridDatos.Items(cuentaF).Cells(cuentaC - 4).Text = "$" + Format(Convert.ToDouble(totalProAcumulado - txtmetasm.Text.Trim), "###,###,###0.00")
                        gridDatos.Items(cuentaF).Cells(cuentaC - 2).Text = Math.Round(totalProtx / txtNumdias.Text, 2)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 1).Text = Math.Round(gridDatos.Items(cuentaF).Cells(cuentaC - 2).Text / txtnumterapistassm.Text, 2)
                    Case "gym"
                        gridDatos.Items(cuentaF + 1).Visible = False
                        gridDatos.Items(cuentaF).Cells(cuentaC - 8).Text = 0
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).Text = 0
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 7).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).Text = "$" + Format(Convert.ToDouble(txtmetas.Text.Trim), "###,###,###0.00") ''meta$
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderWidth = 2
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).BorderColor = Drawing.Color.Red
                        gridDatos.Items(cuentaF).Cells(cuentaC - 6).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
                        gridDatos.Items(cuentaF).Cells(cuentaC - 3).Text = 0
                        gridDatos.Items(cuentaF).Cells(cuentaC - 5).Text = 0
                        gridDatos.Items(cuentaF).Cells(cuentaC - 4).Text = "$" + Format(Convert.ToDouble(gridDatos.Items(cuentaF).Cells(cuentaC - 10).Text.Replace("$", "") - txtmetas.Text.Trim), "###,###,###0.00")
                        gridDatos.Items(cuentaF).Cells(cuentaC - 2).Text = 0
                        gridDatos.Items(cuentaF).Cells(cuentaC - 1).Text = 0
                        gridBase.Items(cuentaF + 1).Visible = False
                End Select

                gridDatos.Items(cuentaF).Font.Bold = True
                If gridDatos.Items(cuentaF).Cells(cuentaC - 5).Text < 0 Then
                    gridDatos.Items(cuentaF).Cells(cuentaC - 5).ForeColor = Drawing.Color.Red
                End If
                If Convert.ToDouble(gridDatos.Items(cuentaF).Cells(cuentaC - 4).Text.Replace("$", "")) < 0 Then
                    gridDatos.Items(cuentaF).Cells(cuentaC - 4).ForeColor = Drawing.Color.Red
                End If
            End If
        Else
            gridDatos.Visible = False
        End If

    End Sub
    Sub creaEncabezado()
        Dim tabla As New DataTable
        Dim columna As New DataColumn("Resumen <hr/>")
        tabla.Columns.Add(columna)
        Dim oFila As DataRow = tabla.NewRow
        Dim oFila2 As DataRow = tabla.NewRow
        oFila(0) = "GRAN TOTAL"
        oFila2(0) = "Pacientes Nuevos"
        tabla.Rows.Add(oFila)
        tabla.Rows.Add(oFila2)
        tabla.TableName = "mitabla"
        gbaseEncabezado.DataMember = "mitabla"
        gbaseEncabezado.DataSource = tabla.DefaultView
        gbaseEncabezado.DataBind()

        Dim ultimafilaCP As Integer = gridOcupacion.Items.Count - 2
        Dim ultimafilaSM As Integer = gridOcupacionSM.Items.Count - 2
        Dim numColumas As Integer = 0
        If ultimafilaCP > 0 And ultimafilaSM > 0 Then
            numColumas = gridOcupacion.Items(0).Cells.Count - 3
            Dim cuentaColumnas As Integer = 0
            Do While cuentaColumnas < numColumas
                gEncabezado.Items(0).Cells(cuentaColumnas).Text = Convert.ToDouble(gridOcupacion.Items(ultimafilaCP).Cells(cuentaColumnas + 3).Text.Replace("$", "")) + Convert.ToDouble(gridOcupacionSM.Items(ultimafilaSM).Cells(cuentaColumnas + 3).Text.Replace("$", ""))
                If cuentaColumnas < numColumas - 12 Then
                    gEncabezado.Items(1).Cells(cuentaColumnas).Text = Convert.ToDecimal(gridOcupacion.Items(ultimafilaCP + 1).Cells(cuentaColumnas + 3).Text) + Convert.ToDecimal(gridOcupacionSM.Items(ultimafilaSM + 1).Cells(cuentaColumnas + 3).Text)
                End If
                cuentaColumnas = cuentaColumnas + 1
            Loop
            Dim ultimaFilaSeniors As Int16 = DataGrid1.Items.Count - 2
            If ultimaFilaSeniors > 0 Then
                gEncabezado.Items(0).Cells(numColumas - 6).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 6).Text) + Convert.ToDecimal(DataGrid1.Items(ultimaFilaSeniors).Cells(numColumas - 3).Text.Replace("$", ""))
            Else
                gEncabezado.Items(0).Cells(numColumas - 6).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 6).Text)
            End If
            If ultimaFilaSeniors > 0 Then
                gEncabezado.Items(0).Cells(numColumas - 12).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 12).Text) + Convert.ToDecimal(DataGrid1.Items(ultimaFilaSeniors).Cells(numColumas - 9).Text.Replace("$", ""))
            Else
                gEncabezado.Items(0).Cells(numColumas - 12).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 12).Text)
            End If
            If ultimaFilaSeniors > 0 Then
                gEncabezado.Items(0).Cells(numColumas - 10).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 10).Text) + Convert.ToDecimal(DataGrid1.Items(ultimaFilaSeniors).Cells(numColumas - 7).Text.Replace("$", ""))
            Else
                gEncabezado.Items(0).Cells(numColumas - 10).Text = Convert.ToDecimal(gEncabezado.Items(0).Cells(numColumas - 10).Text)
            End If

            gEncabezado.Items(0).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            gbaseEncabezado.Items(0).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            gEncabezado.Items(1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
            gbaseEncabezado.Items(1).BackColor = Drawing.Color.FromArgb(194, 214, 154)


            gEncabezado.Items(0).Cells(numColumas - 11).Text = Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 11).Text), "###,###,###") ''totalTX
            gEncabezado.Items(0).Cells(numColumas - 12).Text = "$" + Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 12).Text), "###,###,###0.00").ToString ''total$
            gEncabezado.Items(0).Cells(numColumas - 10).Text = "$" + Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 10).Text), "###,###,###0.00").ToString ''proyeccion$
            gEncabezado.Items(0).Cells(numColumas - 9).Text = Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 9).Text), "###,###,###") ''proyeccion tx

            gEncabezado.Items(0).Cells(numColumas - 8).Text = Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 8).Text), "###,###,###") ''capMax
            gEncabezado.Items(0).Cells(numColumas - 7).Text = Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 7).Text), "###,###,###") ''metatx
            gEncabezado.Items(0).Cells(numColumas - 7).BorderWidth = 2
            gEncabezado.Items(0).Cells(numColumas - 7).BorderColor = Drawing.Color.Red
            gEncabezado.Items(0).Cells(numColumas - 7).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
            gEncabezado.Items(0).Cells(numColumas - 6).Text = "$" + Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 6).Text), "###,###,###0.00") ''meta$
            gEncabezado.Items(0).Cells(numColumas - 6).BorderWidth = 2
            gEncabezado.Items(0).Cells(numColumas - 6).BorderColor = Drawing.Color.Red
            gEncabezado.Items(0).Cells(numColumas - 6).ForeColor = Drawing.Color.FromArgb(51, 51, 153)
            'gEncabezado.Items(0).Cells(numColumas - 3).Text = gEncabezado.Items(0).Cells(numColumas - 3).Text ''#terapistas
            gEncabezado.Items(0).Cells(numColumas - 5).Text = Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 5).Text), "###,###,###") ''difmetatx
            gEncabezado.Items(0).Cells(numColumas - 4).Text = "$" + Format(Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 10).Text.Replace("$", "")) - Convert.ToDouble(gEncabezado.Items(0).Cells(numColumas - 6).Text.Replace("$", "")), "###,###,###0.00") ''difmeta$
            gEncabezado.Items(0).Cells(numColumas - 2).Text = Math.Round(gEncabezado.Items(0).Cells(numColumas - 9).Text / txtNumdias.Text, 2) ''txdiario
            gEncabezado.Items(0).Cells(numColumas - 1).Text = Math.Round(gEncabezado.Items(0).Cells(numColumas - 2).Text / gEncabezado.Items(0).Cells(numColumas - 3).Text, 2) ''txXterapista

            gEncabezado.Items(0).Font.Bold = True
            If gEncabezado.Items(0).Cells(numColumas - 5).Text < 0 Then
                gEncabezado.Items(0).Cells(numColumas - 5).ForeColor = Drawing.Color.Red
            End If
            If gEncabezado.Items(0).Cells(numColumas - 4).Text < 0 Then
                gEncabezado.Items(0).Cells(numColumas - 4).ForeColor = Drawing.Color.Red
            End If
        End If


    End Sub
    Protected Sub gEncabezado_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gEncabezado.ItemDataBound

        Dim cColumnas As Integer = e.Item.Cells.Count
        'e.Item.Cells(cColumnas - 12).Visible = False
        e.Item.Cells(cColumnas - 11).Visible = False

    End Sub
    Protected Sub gridOcupacion_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridOcupacion.ItemDataBound
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False

        Dim cColumnas As Integer = e.Item.Cells.Count
        'e.Item.Cells(cColumnas - 12).Visible = False  'oculta las columnas de acumulado
        e.Item.Cells(cColumnas - 11).Visible = False  'oculta las columnas de acumulado

        Dim tabla As New DataTable
        If e.Item.ItemType = ListItemType.Header Then
            Dim cuenta As Integer = 3
            Do While cuenta < e.Item.Cells.Count - 13
                e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
                Dim columna As New DataColumn(e.Item.Cells(cuenta).Text)
                tabla.Columns.Add(columna)
                cuenta = cuenta + 1
            Loop
            Do While cuenta < e.Item.Cells.Count
                Select Case e.Item.Cells.Count - cuenta
                    Case 5
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado Tx <a style='color:red;font-weight:bold'>-</a> Meta Tx"
                    Case 4
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado $ <a style='color:red;font-weight:bold'>-</a> Meta $"
                    Case 2
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:100px' /> Terapias Diarias"
                    Case 1
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:125px' /> Terapias por Terapista"
                End Select
                Dim columna As New DataColumn(e.Item.Cells(cuenta).Text)
                tabla.Columns.Add(columna)
                cuenta = cuenta + 1
            Loop
            cuenta = 0
            Dim oFila As DataRow = tabla.NewRow
            Dim oFila2 As DataRow = tabla.NewRow
            Do While cuenta < e.Item.Cells.Count - 3
                oFila(cuenta) = "0.0"
                cuenta = cuenta + 1
            Loop
            tabla.Rows.Add(oFila)
            tabla.Rows.Add(oFila2)
            tabla.TableName = "mitabla"
            gEncabezado.DataMember = "mitabla"
            gEncabezado.DataSource = tabla.DefaultView
            gEncabezado.DataBind()
        End If

    End Sub

    Protected Sub gridOcupacionSM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridOcupacionSM.ItemDataBound
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False

        Dim cColumnas As Integer = e.Item.Cells.Count
        'e.Item.Cells(cColumnas - 12).Visible = False  'oculta las columnas de acumulado
        e.Item.Cells(cColumnas - 11).Visible = False  'oculta las columnas de acumulado

        If e.Item.ItemType = ListItemType.Header Then
            Dim cuenta As Integer = 3
            Do While cuenta < e.Item.Cells.Count - 13
                e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
                cuenta = cuenta + 1
            Loop
            Do While cuenta < e.Item.Cells.Count
                Select Case e.Item.Cells.Count - cuenta
                    Case 5
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado Tx <a style='color:red;font-weight:bold'>-</a> Meta Tx"
                    Case 4
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado $ <a style='color:red;font-weight:bold'>-</a> Meta $"
                    Case 2
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:100px' /> Terapias Diarias"
                    Case 1
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:125px' /> Terapias por Terapista"
                End Select
                cuenta = cuenta + 1
            Loop
        End If
    End Sub

    Protected Sub gbaseCP_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCP.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(1).Text = e.Item.Cells(1).Text + "<hr/> &nbsp;"
        End If
    End Sub

    Protected Sub gbaseSM_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseSM.ItemDataBound
        If e.Item.ItemType = ListItemType.Header Then
            e.Item.Cells(1).Text = e.Item.Cells(1).Text + "<hr/> &nbsp;"
        End If
    End Sub

    Function eldiadelasemana(ByVal fecha As Date) As String

        Dim miDia As Int16 = fecha.DayOfWeek

        Dim elDia As String = ""
        Select Case miDia
            Case 1
                elDia = "L"
            Case 2
                elDia = "M"
            Case 3
                elDia = "M"
            Case 4
                elDia = "J"
            Case 5
                elDia = "V"
            Case 6
                elDia = "S"
            Case 0
                elDia = "D"
        End Select
        Return elDia
    End Function
    ''Sub llenadoctores()
    ''    Dim conexion As SqlConnection = clase.conecta("fisiocarecp")
    ''    Dim consulta As String = "select nombre,Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic, ene+feb+mar+abr+may+jun+jul+ago+sep+oct+nov+dic as Total from(" & _
    ''    "select nombre,isnull([mes1],0) as Ene,isnull([mes2],0) as Feb,isnull([mes3],0) as Mar,isnull([mes4],0) as Abr," & _
    ''    "isnull([mes5],0) as May,isnull([mes6],0) as Jun,isnull([mes7],0) as Jul, isnull([mes8],0) as Ago, isnull([mes9],0) as Sep," & _
    ''    "isnull([mes10],0) as Oct,isnull([mes11],0) as Nov,isnull([mes12],0) as Dic from ("

    ''    consulta = consulta + "select isnull(nombre,'DR. SIN REMITENTE DESCONOCIDO') as nombre,'mes'+ convert(char(2),mes) as mes,count(idCliente) as cuenta from (" & _
    ''    "select 'DR. '+nombre+' '+paterno+' '+materno as nombre,datepart(month,dtinicio) as mes, idcliente from (" & _
    ''    "select temp.dtinicio,temp.idcliente,isnull(clientes.idDoctor,0) as idDoctor from (" & _
    ''    "select distinct dtinicio,idCliente from fisiocareCP.dbo.clientes where  " & _
    ''    "datepart(year,dtinicio)<>1900 and (dtinicio>='01/01/" + Right(txtfecha2.Text.Trim, 4) + "' and dtinicio<='" + txtfecha2.Text.Trim + "')) as temp left join fisiocareCP.dbo.clientes " & _
    ''    "on fisiocareCP.dbo.clientes.idcliente=temp.idcliente) as temp left join fisiocareCP.dbo.catDoctores on temp.idDoctor=id " & _
    ''    "union all " & _
    ''    "select 'DR. '+nombre+' '+paterno+' '+materno as nombre,datepart(month,dtinicio) as mes, idcliente from (" & _
    ''    "select temp.dtinicio,temp.idcliente,isnull(clientes.idDoctor,0) as idDoctor from (" & _
    ''    "select distinct dtinicio,idCliente from fisiocareSM.dbo.clientes where " & _
    ''    "datepart(year,dtinicio)<>1900 and (dtinicio>='01/01/" + Right(txtfecha2.Text.Trim, 4) + "' and dtinicio<='" + txtfecha2.Text.Trim + "')) as temp left join fisiocareSM.dbo.clientes " & _
    ''    "on fisiocareSM.dbo.clientes.idcliente=temp.idcliente) as temp left join fisiocareSM.dbo.catDoctores on temp.idDoctor=id "

    ''    consulta = consulta + ") as final group by nombre,mes) as total pivot " & _
    ''    "(sum(cuenta) for mes in ([mes1],[mes2],[mes3],[mes4],[mes5],[mes6],[mes7],[mes8],[mes9],[mes10],[mes11],[mes12])) as piv) as resultados order by total desc"

    ''    Dim odataAdapter As SqlDataAdapter
    ''    odataAdapter = New SqlDataAdapter(consulta, conexion)
    ''    Dim tabla As DataTable
    ''    tabla = New DataTable
    ''    odataAdapter.Fill(tabla)
    ''    tabla.TableName = "mitabla"
    ''    gMedicos.DataMember = "mitabla"
    ''    gMedicos.DataSource = tabla.DefaultView
    ''    gMedicos.DataBind()

    ''    If gMedicos.Items.Count > 0 Then
    ''        Dim filaTotal As DataRow = tabla.NewRow
    ''        Dim nFilas As Integer = gMedicos.Items.Count
    ''        Dim nColumnas As Integer = gMedicos.Items(0).Cells.Count
    ''        Dim cFilas As Integer = 0
    ''        Dim cColumnas As Integer = 1
    ''        Do While cColumnas < nColumnas
    ''            Dim sumaNvoPacientes As Integer = 0
    ''            Do While cFilas < nFilas
    ''                sumaNvoPacientes = sumaNvoPacientes + gMedicos.Items(cFilas).Cells(cColumnas).Text
    ''                cFilas = cFilas + 1
    ''            Loop
    ''            filaTotal(cColumnas) = sumaNvoPacientes
    ''            cColumnas = cColumnas + 1
    ''            cFilas = 0
    ''        Loop

    ''        Dim cuentaF As Integer = gMedicos.Items.Count
    ''        Dim cuentaC As Integer = gMedicos.Items(0).Cells.Count
    ''        Dim columna As Integer = 0
    ''        Dim fila As Integer = 0

    ''        Dim cuentaOtros As Integer = 0
    ''        Do While fila < cuentaF
    ''            Dim nomColum As String = gMedicos.Items(fila).Cells(0).Text.Trim
    ''            If nomColum = "DR. FOLLETOS" Or nomColum = "DR. L.R. MIRIAM HERRERA MARRUFO" Or nomColum = "DR. REVISTA PUBLICITARIA" Or nomColum = "DR. HERBE RIVERO ." Or nomColum = "DR. RECOMENDACION DE UN AMIG@" Or nomColum = "DR. SIN REMITENTE DESCONOCIDO" Or nomColum = "DR. RECOMENDACION FAMILIAR     O AMIGO" Or nomColum = "DR. REVSITA PUBLICITARIA" Then
    ''                columna = 0
    ''                Dim filaAux As DataRow = tabla.NewRow
    ''                Do While columna < cuentaC
    ''                    filaAux(columna) = gMedicos.Items(fila).Cells(columna).Text.Trim
    ''                    columna = columna + 1
    ''                Loop
    ''                tabla.Rows(fila).Delete()
    ''                tabla.Rows.Add(filaAux)
    ''                cuentaOtros = cuentaOtros + 1
    ''            End If
    ''            fila = fila + 1
    ''        Loop

    ''        tabla.Rows.Add(filaTotal)
    ''        gMedicos.DataMember = "mitabla"
    ''        gMedicos.DataSource = tabla.DefaultView
    ''        gMedicos.DataBind()
    ''        gMedicos.Items(nFilas).BackColor = Drawing.Color.FromArgb(194, 214, 154)

    ''        cuentaF = gMedicos.Items.Count - (cuentaOtros + 1)
    ''        Do While cuentaF < gMedicos.Items.Count - 1
    ''            'gridDatos.Items(cuentaF).BackColor = Drawing.Color.Beige
    ''            gMedicos.Items(cuentaF).Cells(0).Text = Right(gMedicos.Items(cuentaF).Cells(0).Text.Trim, gMedicos.Items(cuentaF).Cells(0).Text.Trim.Length - 4)
    ''            cuentaF = cuentaF + 1
    ''        Loop
    ''    End If
    ''End Sub

    ''Protected Sub gMedicos_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gMedicos.ItemDataBound
    ''    e.Item.Cells(0).HorizontalAlign = HorizontalAlign.Left
    ''    e.Item.Cells(13).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    ''    If e.Item.ItemType = ListItemType.Header Then
    ''        e.Item.Cells(0).Text = ""
    ''        e.Item.Cells(13).BackColor = Drawing.Color.White
    ''    End If
    ''End Sub

    ''Protected Sub gridOcupacionE_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridOcupacionE.ItemDataBound
    ''    e.Item.Cells(0).Visible = False
    ''    Dim tabla As New DataTable
    ''    If e.Item.ItemType = ListItemType.Header Then
    ''        Dim cuenta As Integer = 1
    ''        Do While cuenta < e.Item.Cells.Count - 2
    ''            e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
    ''            Dim columna As New DataColumn(e.Item.Cells(cuenta).Text)
    ''            tabla.Columns.Add(columna)
    ''            cuenta = cuenta + 1
    ''        Loop
    ''        Do While cuenta < e.Item.Cells.Count
    ''            Dim columna As New DataColumn(e.Item.Cells(cuenta).Text)
    ''            tabla.Columns.Add(columna)
    ''            cuenta = cuenta + 1
    ''        Loop
    ''        cuenta = 0
    ''        Dim oFila As DataRow = tabla.NewRow
    ''        Do While cuenta < e.Item.Cells.Count - 3
    ''            oFila(cuenta) = "0"
    ''            cuenta = cuenta + 1
    ''        Loop
    ''        tabla.Rows.Add(oFila)
    ''        tabla.TableName = "mitabla"
    ''        gEncabezadoE.DataMember = "mitabla"
    ''        gEncabezadoE.DataSource = tabla.DefaultView
    ''        gEncabezadoE.DataBind()
    ''    End If
    ''End Sub
    ''Sub llenaEncabezadoEnviados()
    ''    Dim tabla As New DataTable
    ''    Dim columna As New DataColumn("Resumen <hr/>")
    ''    tabla.Columns.Add(columna)
    ''    Dim oFila2 As DataRow = tabla.NewRow
    ''    oFila2(0) = "Total Pacientes Nuevos"
    ''    tabla.Rows.Add(oFila2)
    ''    tabla.TableName = "mitabla"
    ''    gbaseEncabezadoE.DataMember = "mitabla"
    ''    gbaseEncabezadoE.DataSource = tabla.DefaultView
    ''    gbaseEncabezadoE.DataBind()

    ''    Dim ultimafilaCP As Integer = gridOcupacionE.Items.Count - 1
    ''    Dim ultimafilaSM As Integer = gridOcupacionSMe.Items.Count - 1
    ''    Dim numColumas As Integer = 0
    ''    If ultimafilaCP > 0 And ultimafilaSM > 0 Then
    ''        numColumas = gridOcupacionE.Items(0).Cells.Count - 1
    ''        Dim cuentaColumnas As Integer = 0
    ''        Do While cuentaColumnas < numColumas
    ''            gEncabezadoE.Items(0).Cells(cuentaColumnas).Text = Convert.ToDouble(gridOcupacionE.Items(ultimafilaCP).Cells(cuentaColumnas + 1).Text.Replace("$", "")) + Convert.ToDouble(gridOcupacionSMe.Items(ultimafilaSM).Cells(cuentaColumnas + 1).Text.Replace("$", ""))
    ''            cuentaColumnas = cuentaColumnas + 1
    ''        Loop
    ''        gEncabezadoE.Items(0).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    ''        gbaseEncabezadoE.Items(0).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    ''    End If
    ''End Sub

    ''Protected Sub gridOcupacionSMe_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridOcupacionSMe.ItemDataBound
    ''    e.Item.Cells(0).Visible = False
    ''    If e.Item.ItemType = ListItemType.Header Then
    ''        Dim cuenta As Integer = 1
    ''        Do While cuenta < e.Item.Cells.Count - 2
    ''            e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
    ''            cuenta = cuenta + 1
    ''        Loop
    ''    End If
    ''End Sub

    Protected Sub DataGrid1_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles DataGrid1.ItemDataBound
        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False
        Dim cColumnas As Integer = e.Item.Cells.Count
        e.Item.Cells(cColumnas - 1).Visible = False
        e.Item.Cells(cColumnas - 2).Visible = False
        e.Item.Cells(cColumnas - 3).Visible = False
        e.Item.Cells(cColumnas - 5).Visible = False
        e.Item.Cells(cColumnas - 7).Visible = False
        e.Item.Cells(cColumnas - 8).Visible = False
        e.Item.Cells(cColumnas - 9).Visible = False
        e.Item.Cells(cColumnas - 11).Visible = False
        e.Item.Cells(cColumnas - 13).Visible = False

        e.Item.Cells(0).Visible = False
        e.Item.Cells(1).Visible = False
        e.Item.Cells(2).Visible = False
        If e.Item.ItemType = ListItemType.Header Then
            Dim cuenta As Integer = 3
            Do While cuenta < e.Item.Cells.Count - 13
                e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
                cuenta = cuenta + 1
            Loop
            Do While cuenta < e.Item.Cells.Count
                Select Case e.Item.Cells.Count - cuenta
                    Case 5
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado Tx <a style='color:red;font-weight:bold'>-</a> Meta Tx"
                    Case 4
                        e.Item.Cells(cuenta).Text = "Diferencia<hr style='width:125px' />Estimado $ <a style='color:red;font-weight:bold'>-</a> Meta $"
                    Case 2
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:100px' /> Terapias Diarias"
                    Case 1
                        e.Item.Cells(cuenta).Text = "Estimado <hr style='width:125px' /> Terapias por Terapista"
                End Select
                cuenta = cuenta + 1
            Loop
        End If

    End Sub

    ''Protected Sub gEncabezadoC_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gEncabezadoC.ItemDataBound
    ''    e.Item.Cells(0).Visible = False
    ''    e.Item.Cells(1).Visible = False
    ''    e.Item.Cells(2).Visible = False
    ''    If e.Item.ItemType = ListItemType.Header Then
    ''        Dim cuenta As Integer = 3
    ''        Do While cuenta < e.Item.Cells.Count - 1
    ''            e.Item.Cells(cuenta).Text = e.Item.Cells(cuenta).Text + "<hr/>" + eldiadelasemana(e.Item.Cells(cuenta).Text + "/" + Right(txtfecha1.Text, 4))
    ''            cuenta = cuenta + 1
    ''        Loop
    ''    End If
    ''End Sub

    ''Sub terapiaXdoctor()
    ''    'Dim conexion As SqlConnection = clase.conecta("fisiocarecp")
    ''    Dim consulta As String = "select nomDoctor as Nombre,Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic, " & _
    ''    "ene+feb+mar+abr+may+jun+jul+ago+sep+oct+nov+dic as Total from(select nomDoctor,isnull([mes1],0)" & _
    ''    " as Ene,isnull([mes2],0) as Feb,isnull([mes3],0) as Mar,isnull([mes4],0) as Abr,isnull([mes5],0)" & _
    ''    " as May,isnull([mes6],0) as Jun,isnull([mes7],0) as Jul, isnull([mes8],0) as Ago, isnull([mes9],0)" & _
    ''    " as Sep,isnull([mes10],0) as Oct,isnull([mes11],0) as Nov,isnull([mes12],0) as Dic from("

    ''    consulta = consulta + " select nomDoctor,'mes'+ convert(char(2),mes) as mes,count(idcosto) as cuenta from(" & _
    ''    "select id,(nombre+' '+paterno+' '+materno) nomDoctor,abonos.idcosto,fecha,datepart(month,fecha)" & _
    ''    " as mes,id_servicio from catdoctores left join abonos on abonos.iddoctor=catdoctores.id " & _
    ''    " left join catcostos on catcostos.idcosto=abonos.idcosto where ligaabono=0 and id_servicio<>'CM' " & _
    ''    " AND ID_SERVICIO<>'CMD' AND ID_SERVICIO<>'CT' AND  ID_SERVICIO<>'CTB' and year(fecha)=" + Right(txtfecha2.Text, 4) + ") " & _
    ''    "as principal group by nomDoctor,mes) as general "

    ''    consulta = consulta + " pivot(sum(cuenta) for mes in ([mes1],[mes2],[mes3],[mes4],[mes5],[mes6],[mes7],[mes8],[mes9]," & _
    ''    "[mes10],[mes11],[mes12])) as piv)as resultados order by total desc"

    ''    gTerapiaDocCP = clase.LLenaGrid(consulta, gTerapiaDocCP, "fisiocarecp")
    ''    gTerapiaDocSM = clase.LLenaGrid(consulta, gTerapiaDocSM, "fisiocaresm")
    ''End Sub

    ''Sub terapiaIMPXdoctor()
    ''    'Dim conexion As SqlConnection = clase.conecta("fisiocarecp")
    ''    Dim consulta As String = "select nomDoctor as Nombre,Ene,Feb,Mar,Abr,May,Jun,Jul,Ago,Sep,Oct,Nov,Dic, " & _
    ''    "ene+feb+mar+abr+may+jun+jul+ago+sep+oct+nov+dic as Total from(select nomDoctor,isnull([mes1],0)" & _
    ''    " as Ene,isnull([mes2],0) as Feb,isnull([mes3],0) as Mar,isnull([mes4],0) as Abr,isnull([mes5],0)" & _
    ''    " as May,isnull([mes6],0) as Jun,isnull([mes7],0) as Jul, isnull([mes8],0) as Ago, isnull([mes9],0)" & _
    ''    " as Sep,isnull([mes10],0) as Oct,isnull([mes11],0) as Nov,isnull([mes12],0) as Dic from("

    ''    consulta = consulta + " select nomDoctor,'mes'+ convert(char(2),mes) as mes,sum(importe) as cuenta from(" & _
    ''    "select id,(nombre+' '+paterno+' '+materno) nomDoctor,abonos.idcosto,importe,fecha,datepart(month,fecha)" & _
    ''    " as mes,id_servicio from catdoctores left join abonos on abonos.iddoctor=catdoctores.id " & _
    ''    " left join catcostos on catcostos.idcosto=abonos.idcosto where id_servicio<>'CM' " & _
    ''    " AND ID_SERVICIO<>'CMD' AND ID_SERVICIO<>'CT' AND coaseguro<>'1' AND  ID_SERVICIO<>'CTB' and year(fecha)=" + Right(txtfecha2.Text, 4) + ") " & _
    ''    "as principal group by nomDoctor,mes) as general "

    ''    consulta = consulta + " pivot(sum(cuenta) for mes in ([mes1],[mes2],[mes3],[mes4],[mes5],[mes6],[mes7],[mes8],[mes9]," & _
    ''    "[mes10],[mes11],[mes12])) as piv)as resultados order by total desc"

    ''    gTeraImpoCP = clase.LLenaGrid(consulta, gTeraImpoCP, "fisiocarecp")
    ''    gTeraImpoSM = clase.LLenaGrid(consulta, gTeraImpoSM, "fisiocaresm")
    ''End Sub
End Class