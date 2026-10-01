Imports System.Data
Imports System.Data.SqlClient

Partial Class consultasbancos
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Protected Sub Btncm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncm.Click
        hfSucursal.Value = cmbSucursal.SelectedValue

        If cmbSucursal.SelectedIndex = "0" Then
            Dim sql, sqlaux, sqltotales As String
            'Dim dias As Integer = DateDiff(DateInterval.Day, CDate(txtfecha1.Text), CDate(txtfecha2.Text))
            ''Dim i = 0
            'Dim fechaaux As String
            'fechaaux = txtfecha1.Text
            sqlaux = ""
            sqltotales = ""
            Dim hfaño As String
            hfaño = cmbaño.SelectedValue
            Dim hfsaldo As String
            hfsaldo = cmbmes.SelectedValue

            ' ingresos
            'base

            sql = "select  isnull(left(cat.Nombre,30), 'SINCATEGORIA') as Ingresos " & _
                    "from Categorias cat " & _
                        "where(cat.id = 1 Or cat.id = 546)" & _
                        "union all " & _
                        "select 'TOTAL' " & _
                        "from Categorias cat " & _
                        "where(cat.id = 1)"
            gbaseingresos = funciones.LLenaGridC(sql, gbaseingresos, hfSucursal.Value)

            ' altabrisa


            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Altabrisa' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('3') or cat.Id IN ('548'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Altabrisa'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('3') or cat.Id IN ('548'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            ialtabrisa = funciones.LLenaGridC(sql, ialtabrisa, hfSucursal.Value)

            'terapia a domicilio

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'TerapiaHospital' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('6') or cat.Id IN ('554'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'TerapiaHospital'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('6') or cat.Id IN ('554'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            ithospital = funciones.LLenaGridC(sql, ithospital, hfSucursal.Value)

            'campestre

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Campestre' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('2') or cat.Id IN ('547'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Campestre'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('2') or cat.Id IN ('547'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            icampestre = funciones.LLenaGridC(sql, icampestre, hfSucursal.Value)

            'gym

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'SGym' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('4') or cat.Id IN ('549'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'SGym'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('4') or cat.Id IN ('549'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            igym = funciones.LLenaGridC(sql, igym, hfSucursal.Value)

            'Hidroterapia

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Hidroterapia' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('5') or cat.Id IN ('550'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Hidroterapia'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('5') or cat.Id IN ('550'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            ihidroterapia = funciones.LLenaGridC(sql, ihidroterapia, hfSucursal.Value)

            'terapia domicilio

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'TerapiaDomicilio' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('7') or cat.Id IN ('555'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'TerapiaDomicilio'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('7') or cat.Id IN ('555'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            itdomicilio = funciones.LLenaGridC(sql, itdomicilio, hfSucursal.Value)

            ' administracion

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Administracion' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('556'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Administracion'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('556'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            iadministracion = funciones.LLenaGridC(sql, iadministracion, hfSucursal.Value)

            ' coorporativo

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Corporativo' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('557'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Corporativo'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('557'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            icorporativo = funciones.LLenaGridC(sql, icorporativo, hfSucursal.Value)

            ' ho

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'HO CruzRoja' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('461') or cat.Id IN ('559'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'HO CruzRoja'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('461') or cat.Id IN ('559'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            iho = funciones.LLenaGridC(sql, iho, hfSucursal.Value)

            'anticanceroso

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Anticanceroso' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('462') or cat.Id IN ('560'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Anticanceroso'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('462') or cat.Id IN ('560'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            ianticanceroso = funciones.LLenaGridC(sql, ianticanceroso, hfSucursal.Value)

            ' pensiones

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Pensiones' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('463') or cat.Id IN ('561'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Pensiones'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('463') or cat.Id IN ('561'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            ipensiones = funciones.LLenaGridC(sql, ipensiones, hfSucursal.Value)

            'socios

            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as 'Socios' " & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('558'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )" & _
                        "union all " & _
            "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Socios'" & _
              "FROM Categorias cat " & _
             "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
            "WHERE (cat.Id IN ('1') or cat.Id IN ('558'))" & _
            "AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
            "AND (sdo.Ejercicio = '" + hfaño + "' " & _
            "OR sdo.Ejercicio IS NULL )"
            isocios = funciones.LLenaGridC(sql, isocios, hfSucursal.Value)

            ' TOTALES
            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'TOTALES' " & _
" FROM Categorias cat LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
" WHERE (cat.Id IN ('2') or cat.Id IN ('3')  or cat.Id IN ('4')  or cat.Id IN ('5')  or cat.Id IN ('6')  or cat.Id IN ('7') " & _
 " or cat.Id IN ('461')  or cat.Id IN ('462')  or cat.Id IN ('463')  or cat.Id IN ('693')  or cat.Id IN ('694')  or cat.Id IN ('695'))AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
" AND (sdo.Ejercicio = '" + hfaño + "' OR sdo.Ejercicio IS NULL )union ALL " & _
" SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Totales' " & _
" FROM Categorias cat LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
" WHERE (cat.Id IN ('547')  or cat.Id IN ('548')  or cat.Id IN ('549')  or cat.Id IN ('550')  or cat.Id IN ('554')  or cat.Id IN ('555')" & _
   " or cat.Id IN ('556')  or cat.Id IN ('557')  or cat.Id IN ('558')  or cat.Id IN ('559')  or cat.Id IN ('560')  or cat.Id IN ('561'))AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
" AND (sdo.Ejercicio = '" + hfaño + "' OR sdo.Ejercicio IS NULL )" & _
" union all SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'TerapiaHospital'" & _
" FROM Categorias cat LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
" WHERE (cat.Id IN ('2') or cat.Id IN ('3')  or cat.Id IN ('4')  or cat.Id IN ('5')  or cat.Id IN ('6')  or cat.Id IN ('7') " & _
 " or cat.Id IN ('461')  or cat.Id IN ('462')  or cat.Id IN ('463')  or cat.Id IN ('693')  or cat.Id IN ('694')  or cat.Id IN ('695')" & _
  " or cat.Id IN ('547')  or cat.Id IN ('548')  or cat.Id IN ('549')  or cat.Id IN ('550')  or cat.Id IN ('554')  or cat.Id IN ('555')" & _
   " or cat.Id IN ('556')  or cat.Id IN ('557')  or cat.Id IN ('558')  or cat.Id IN ('559')  or cat.Id IN ('560')  or cat.Id IN ('561')" & _
" )AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
" AND (sdo.Ejercicio = '" + hfaño + "' OR sdo.Ejercicio IS NULL )"
            itotales = funciones.LLenaGridC(sql, itotales, hfSucursal.Value)

            '''''EGRESOS
            'base
            sql = "select  isnull(left(cat.Nombre,30), 'SINCATEGORIA') as Egresos " & _
                                "from Categorias cat " & _
                                "where(cat.id = 281) " & _
                                "union all " & _
                                "select  isnull(left(cat.Nombre,30), 'SINCATEGORIA') as Egresos " & _
                                "from Categorias cat " & _
                                "where(cat.id = 17 Or cat.id = 35 Or cat.id = 45 Or cat.id = 55 Or cat.id = 65 Or cat.id = 75 Or cat.id = 85 Or cat.id = 95 " & _
" Or cat.id = 105 Or cat.id = 115 Or cat.id = 125 Or cat.id = 135 Or cat.id = 145 Or cat.id = 155 Or cat.id = 165 Or cat.id = 175 Or cat.id = 185 Or cat.id = 195 " & _
" Or cat.id = 205 Or cat.id = 215 Or cat.id = 285 Or cat.id = 286 Or cat.id = 287 Or cat.id = 288 Or cat.id = 326 Or cat.id = 336 Or cat.id = 353 Or cat.id = 365 " & _
" Or cat.id = 377 Or cat.id = 389 Or cat.id = 401 Or cat.id = 411 Or cat.id = 429 Or cat.id = 441 Or cat.id = 449 Or cat.id = 465 Or cat.id = 469 Or cat.id = 481" & _
" Or cat.id = 501 Or cat.id = 529 Or cat.id = 629 Or cat.id = 655)" & _
                                    "union all " & _
                                    "select 'TOTAL' " & _
                                    "from Categorias cat " & _
                                    "where(cat.id = 1)"
            gbaseegresos = funciones.LLenaGridC(sql, gbaseegresos, hfSucursal.Value)

            'ALABRISA


            sql = "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, (sdo." + hfsaldo + ")), 1)) as ' ' " & _
                          "FROM Categorias cat " & _
                         "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
                       " WHERE (cat.Id IN ('9') or cat.Id IN ('19') or cat.Id IN ('37') or cat.Id IN ('47') or cat.Id IN ('57') or cat.Id IN ('67') or cat.Id IN ('77') or cat.Id IN ('87')" & _
" or cat.Id IN ('97') or cat.Id IN ('107') or cat.Id IN ('117') or cat.Id IN ('127') or cat.Id IN ('137') or cat.Id IN ('147') or cat.Id IN ('157')" & _
" or cat.Id IN ('167') or cat.Id IN ('177') or cat.Id IN ('187') or cat.Id IN ('197') or cat.Id IN ('207') or cat.Id IN ('217') or cat.Id IN ('290')" & _
" or cat.Id IN ('300') or cat.Id IN ('309') or cat.Id IN ('318') or cat.Id IN ('328') or cat.Id IN ('338') or cat.Id IN ('355') or cat.Id IN ('367') or cat.Id IN ('379') " & _
" or cat.Id IN ('391') or cat.Id IN ('403') or cat.Id IN ('413') or cat.Id IN ('431') or cat.Id IN ('443') or cat.Id IN ('451') or cat.Id IN ('467') or cat.Id IN ('471') " & _
" or cat.Id IN ('482')  or cat.Id IN ('503') or cat.Id IN ('531') or cat.Id IN ('631') or cat.Id IN ('643'))AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
                        "AND (sdo.Ejercicio = '" + hfaño + "' " & _
                        "OR sdo.Ejercicio IS NULL )" & _
                                    "union all " & _
                        "SELECT concat('$',CONVERT(VARCHAR(30), CONVERT(MONEY, sum(sdo." + hfsaldo + ")), 1)) as 'Altabrisa'" & _
                          "FROM Categorias cat " & _
                         "LEFT JOIN SaldosCategorias sdo ON sdo.idCategoria = cat.Id " & _
                        " WHERE (cat.Id IN ('9') or cat.Id IN ('19') or cat.Id IN ('37') or cat.Id IN ('47') or cat.Id IN ('57') or cat.Id IN ('67') or cat.Id IN ('77') or cat.Id IN ('87')" & _
" or cat.Id IN ('97') or cat.Id IN ('107') or cat.Id IN ('117') or cat.Id IN ('127') or cat.Id IN ('137') or cat.Id IN ('147') or cat.Id IN ('157')" & _
" or cat.Id IN ('167') or cat.Id IN ('177') or cat.Id IN ('187') or cat.Id IN ('197') or cat.Id IN ('207') or cat.Id IN ('217') or cat.Id IN ('290')" & _
" or cat.Id IN ('300') or cat.Id IN ('309') or cat.Id IN ('318') or cat.Id IN ('328') or cat.Id IN ('338') or cat.Id IN ('355') or cat.Id IN ('367') or cat.Id IN ('379')" & _
" or cat.Id IN ('391') or cat.Id IN ('403') or cat.Id IN ('413') or cat.Id IN ('431') or cat.Id IN ('443') or cat.Id IN ('451') or cat.Id IN ('467') or cat.Id IN ('471') " & _
" or cat.Id IN ('482')  or cat.Id IN ('503') or cat.Id IN ('531') or cat.Id IN ('631') or cat.Id IN ('643'))AND LTRIM(RTRIM(cat.CodigoMoneda)) =LTRIM(RTRIM(' 1 ')) " & _
                        "AND (sdo.Ejercicio = '" + hfaño + "' " & _
                        "OR sdo.Ejercicio IS NULL )"
            ealtabrisa = funciones.LLenaGridC(sql, ealtabrisa, hfSucursal.Value)


        Else

        End If

        cmdexcelcm.Visible = True
        ialtabrisa.Visible = True
        'gridventasCMA.Visible = True
        'lblaltabrisa.Visible = True
        'lblcma.Visible = True
        'lbltotales.Visible = True


        'TextBox1.Text = "http://107.161.180.154/administracion/rptzonamedica.aspx?fecha1=" + txtfecha1.Text + "&fecha2=" + txtfecha2.Text + "&numdias=" + txtNumdias.Text + ""

        Dim UltimaFilaB As Integer
        UltimaFilaB = gbaseingresos.Items.Count - 1

        gbaseingresos.Items(UltimaFilaB).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        gbaseingresos.Items(UltimaFilaB).ForeColor = Drawing.Color.Black
        gbaseingresos.Items(UltimaFilaB).Font.Bold = True
        gbaseingresos.Items(UltimaFilaB).Font.Size = 10

        Dim UltimaFilaGA As Integer
        UltimaFilaGA = ialtabrisa.Items.Count - 1

        ialtabrisa.Items(UltimaFilaGA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ialtabrisa.Items(UltimaFilaGA).ForeColor = Drawing.Color.Black
        ialtabrisa.Items(UltimaFilaGA).Font.Bold = True
        ialtabrisa.Items(UltimaFilaGA).Font.Size = 10


        Dim UltimaFilath As Integer
        UltimaFilath = ithospital.Items.Count - 1

        ithospital.Items(UltimaFilath).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ithospital.Items(UltimaFilath).ForeColor = Drawing.Color.Black
        ithospital.Items(UltimaFilath).Font.Bold = True
        ithospital.Items(UltimaFilath).Font.Size = 10


        Dim UltimaFilac As Integer
        UltimaFilac = icampestre.Items.Count - 1

        icampestre.Items(UltimaFilac).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        icampestre.Items(UltimaFilac).ForeColor = Drawing.Color.Black
        icampestre.Items(UltimaFilac).Font.Bold = True
        icampestre.Items(UltimaFilac).Font.Size = 10

        Dim UltimaFilaG As Integer
        UltimaFilaG = igym.Items.Count - 1

        igym.Items(UltimaFilaG).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        igym.Items(UltimaFilaG).ForeColor = Drawing.Color.Black
        igym.Items(UltimaFilaG).Font.Bold = True
        igym.Items(UltimaFilaG).Font.Size = 10

        Dim UltimaFilah As Integer
        UltimaFilah = ihidroterapia.Items.Count - 1

        ihidroterapia.Items(UltimaFilah).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ihidroterapia.Items(UltimaFilah).ForeColor = Drawing.Color.Black
        ihidroterapia.Items(UltimaFilah).Font.Bold = True
        ihidroterapia.Items(UltimaFilah).Font.Size = 10

        Dim UltimaFilatd As Integer
        UltimaFilatd = itdomicilio.Items.Count - 1

        itdomicilio.Items(UltimaFilatd).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        itdomicilio.Items(UltimaFilatd).ForeColor = Drawing.Color.Black
        itdomicilio.Items(UltimaFilatd).Font.Bold = True
        itdomicilio.Items(UltimaFilatd).Font.Size = 10

        Dim UltimaFilaA As Integer
        UltimaFilaA = iadministracion.Items.Count - 1

        iadministracion.Items(UltimaFilaA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        iadministracion.Items(UltimaFilaA).ForeColor = Drawing.Color.Black
        iadministracion.Items(UltimaFilaA).Font.Bold = True
        iadministracion.Items(UltimaFilaA).Font.Size = 10

        Dim UltimaFilaco As Integer
        UltimaFilaco = icorporativo.Items.Count - 1

        icorporativo.Items(UltimaFilaco).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        icorporativo.Items(UltimaFilaco).ForeColor = Drawing.Color.Black
        icorporativo.Items(UltimaFilaco).Font.Bold = True
        icorporativo.Items(UltimaFilaco).Font.Size = 10

        Dim UltimaFilaho As Integer
        UltimaFilaho = iho.Items.Count - 1

        iho.Items(UltimaFilaho).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        iho.Items(UltimaFilaho).ForeColor = Drawing.Color.Black
        iho.Items(UltimaFilaho).Font.Bold = True
        iho.Items(UltimaFilaho).Font.Size = 10

        Dim UltimaFilaat As Integer
        UltimaFilaat = ianticanceroso.Items.Count - 1

        ianticanceroso.Items(UltimaFilaat).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ianticanceroso.Items(UltimaFilaat).ForeColor = Drawing.Color.Black
        ianticanceroso.Items(UltimaFilaat).Font.Bold = True
        ianticanceroso.Items(UltimaFilaat).Font.Size = 10

        Dim UltimaFilap As Integer
        UltimaFilap = ipensiones.Items.Count - 1

        ipensiones.Items(UltimaFilap).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ipensiones.Items(UltimaFilap).ForeColor = Drawing.Color.Black
        ipensiones.Items(UltimaFilap).Font.Bold = True
        ipensiones.Items(UltimaFilap).Font.Size = 10

        Dim UltimaFilaso As Integer
        UltimaFilaso = isocios.Items.Count - 1

        isocios.Items(UltimaFilaso).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        isocios.Items(UltimaFilaso).ForeColor = Drawing.Color.Black
        isocios.Items(UltimaFilaso).Font.Bold = True
        isocios.Items(UltimaFilaso).Font.Size = 10

        Dim UltimaFilat As Integer
        UltimaFilat = itotales.Items.Count - 1

        itotales.Items(UltimaFilat).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        itotales.Items(UltimaFilat).ForeColor = Drawing.Color.Black
        itotales.Items(UltimaFilat).Font.Bold = True
        itotales.Items(UltimaFilat).Font.Size = 10


        'Dim UltimaFilaGC As Integer
        'UltimaFilaGC = gridventasCMA.Items.Count - 1

        'gridventasCMA.Items(UltimaFilaGC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridventasCMA.Items(UltimaFilaGC).ForeColor = Drawing.Color.Black
        'gridventasCMA.Items(UltimaFilaGC).Font.Bold = True
        'gridventasCMA.Items(UltimaFilaGC).Font.Size = 10

        'Dim UltimaFilaGT As Integer
        'UltimaFilaGT = gridtotales.Items.Count - 1

        'gridtotales.Items(UltimaFilaGT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gridtotales.Items(UltimaFilaGT).ForeColor = Drawing.Color.Black
        'gridtotales.Items(UltimaFilaGT).Font.Bold = True
        'gridtotales.Items(UltimaFilaGT).Font.Size = 10

        'Dim UltimaFilaGBA As Integer
        'UltimaFilaGBA = gbaseingresos.Items.Count - 1

        'gbaseingresos.Items(UltimaFilaGBA).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseingresos.Items(UltimaFilaGBA).ForeColor = Drawing.Color.Black
        'gbaseingresos.Items(UltimaFilaGBA).Font.Bold = True
        'gbaseingresos.Items(UltimaFilaGBA).Font.Size = 10
        'gbaseingresos.Items(UltimaFilaGBA).HorizontalAlign = HorizontalAlign.Right

        'Dim UltimaFilaGBC As Integer
        'UltimaFilaGBC = gbaseCMA.Items.Count - 1

        'gbaseCMA.Items(UltimaFilaGBC).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseCMA.Items(UltimaFilaGBC).ForeColor = Drawing.Color.Black
        'gbaseCMA.Items(UltimaFilaGBC).Font.Bold = True
        'gbaseCMA.Items(UltimaFilaGBC).Font.Size = 10
        'gbaseCMA.Items(UltimaFilaGBC).HorizontalAlign = HorizontalAlign.Right


        'Dim UltimaFilaGBT As Integer
        'UltimaFilaGBT = gbaseTOTAL.Items.Count - 1

        'gbaseTOTAL.Items(UltimaFilaGBT).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseTOTAL.Items(UltimaFilaGBT).ForeColor = Drawing.Color.Black
        'gbaseTOTAL.Items(UltimaFilaGBT).Font.Bold = True
        'gbaseTOTAL.Items(UltimaFilaGBT).Font.Size = 10
        'gbaseTOTAL.Items(UltimaFilaGBT).HorizontalAlign = HorizontalAlign.Right

        'Dim UltimaFilaGBT2 As Integer
        'UltimaFilaGBT2 = gbaseTOTAL2.Items.Count - 1

        'gbaseTOTAL2.Items(UltimaFilaGBT2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        ''gbaseTOTAL2.Items(UltimaFilaGBT2).ForeColor = Drawing.Color.Black
        'gbaseTOTAL2.Items(UltimaFilaGBT2).Font.Bold = True
        'gbaseTOTAL2.Items(UltimaFilaGBT2).Font.Size = 10

        'Dim UltimaFilaGBA2 As Integer
        'UltimaFilaGBA2 = gbaseALTA2.Items.Count - 1

        'gbaseALTA2.Items(UltimaFilaGBA2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseALTA2.Items(UltimaFilaGBA2).ForeColor = Drawing.Color.Blue
        'gbaseALTA2.Items(UltimaFilaGBA2).Font.Bold = True
        'gbaseALTA2.Items(UltimaFilaGBA2).Font.Size = 10

        'Dim UltimaFilaGBC2 As Integer
        'UltimaFilaGBC2 = gbaseCMA2.Items.Count - 1

        'gbaseCMA2.Items(UltimaFilaGBC2).BackColor = Drawing.Color.FromArgb(194, 214, 154)
        'gbaseCMA2.Items(UltimaFilaGBC2).ForeColor = Drawing.Color.Blue
        'gbaseCMA2.Items(UltimaFilaGBC2).Font.Bold = True
        'gbaseCMA2.Items(UltimaFilaGBC2).Font.Size = 10


    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            'Dim hoy As DateTime = DateTime.Now()
            'txtfecha1.Text = String.Format("{0:dd/MM/yyyy}", hoy)
            'txtfecha2.Text = String.Format("{0:dd/MM/yyyy}", hoy)


            'Dim numdias As Integer = DateTime.DaysInMonth(Now.Year, Now.Month)
            'txtNumdias.Text = numdias
        End If
    End Sub
    Protected Sub ExportarAExcel()
        'Dim sb As StringBuilder = New StringBuilder()
        'Dim sw As IO.StringWriter = New IO.StringWriter(sb)
        'Dim htw As HtmlTextWriter = New HtmlTextWriter(sw)
        'Dim pagina As Page = New Page
        'Dim form As New HtmlForm
        'lblaltabrisa.EnableViewState = False
        'lblcma.EnableViewState = False
        'lbltotales.EnableViewState = False
        'gridventasALTA.EnableViewState = False
        'gridventasCMA.EnableViewState = False
        'gridtotales.EnableViewState = False
        'pagina.EnableEventValidation = False
        'pagina.DesignerInitialize()
        'pagina.Controls.Add(form)
        'form.Controls.Add(lblaltabrisa)
        'form.Controls.Add(gridventasALTA)
        'form.Controls.Add(lblcma)
        'form.Controls.Add(gridventasCMA)
        'form.Controls.Add(lbltotales)
        'form.Controls.Add(gridtotales)
        'pagina.RenderControl(htw)
        'Response.Clear()
        'Response.Buffer = True
        'Response.ContentType = "application/vnd.ms-excel"
        'Response.AddHeader("Content-Disposition", "attachment;filename=VentasEstimadas.xls")
        'Response.Charset = "UTF-8"
        'Response.ContentEncoding = Encoding.Default
        'Response.Write(sb.ToString())
        'Response.End()

    End Sub
    Protected Sub cmdexcelcm_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmdexcelcm.Click
        ExportarAExcel()
    End Sub
    'Protected Sub gridventasALTA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasALTA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False

    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).Text = Format(cdbl(e.Item.Cells(e.Item.Cells.Count - i - 1).Text, "###,###,###0.00").ToString
    '        'DataGridView1.Columns(0).DefaultCellStyle.Format = "##,##0.00"
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).Text = Format(Convert.ToDouble(e.Item.Cells(e.Item.Cells.Count - i - 1).Text), "###,###,###0.00").ToString ''total$
    '    Next

    '    For t As Integer = 0 To 1

    '        e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
    '    Next
    'End Sub
    'Protected Sub gridventasCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridventasCMA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
    '    Next

    '    For t As Integer = 0 To 1
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
    '    Next
    'End Sub
    'Protected Sub gridtotales_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gridtotales.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).BackColor = Drawing.Color.AliceBlue
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Visible = False
    '    Next

    '    For t As Integer = 0 To 1
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.Black
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).BackColor = Drawing.Color.FromArgb(194, 214, 154)
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Visible = False
    '    Next
    'End Sub
    'Protected Sub gbaseALTA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseALTA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
    '    e.Item.Cells(0).Font.Bold = True
    '    'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    'End Sub
    'Protected Sub gbaseCMA_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
    '    e.Item.Cells(0).Font.Bold = True
    '    'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    'End Sub
    'Protected Sub gbaseTOTAL_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseTOTAL.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.DarkRed
    '    e.Item.Cells(0).Font.Bold = True
    '    'e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    'End Sub
    'Protected Sub gbaseTOTAL2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseTOTAL2.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10
    '    Next

    '    For t As Integer = 0 To 1
    '        'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10


    '    Next
    'End Sub
    'Protected Sub gbaseALTA2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseALTA2.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

    '    Next

    '    For t As Integer = 0 To 1
    '        'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



    '    Next
    'End Sub

    'Protected Sub gbaseCMA2_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles gbaseCMA2.ItemDataBound
    '    e.Item.Cells(0).ForeColor = Drawing.Color.AliceBlue
    '    e.Item.Cells(0).Font.Bold = True
    '    e.Item.Cells(0).BackColor = Drawing.Color.CadetBlue
    '    e.Item.Cells(0).Visible = False

    '    For i As Integer = 0 To 3
    '        'e.Item.Cells(e.Item.Cells.Count - i - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - i - 1).Font.Size = 10

    '    Next

    '    For t As Integer = 0 To 1
    '        'e.Item.Cells(e.Item.Cells.Count - t - 1).ForeColor = Drawing.Color.DarkRed
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Bold = True
    '        e.Item.Cells(e.Item.Cells.Count - t - 1).Font.Size = 10



    '    Next
    'End Sub

End Class
