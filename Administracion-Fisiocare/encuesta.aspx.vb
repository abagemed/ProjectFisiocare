
Partial Class encuesta
    Inherits System.Web.UI.Page
    Dim funciones As New miclases
    Sub llenaDatos()
        txtTotalencuesta.Text = funciones.leerValor("select count(id) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "'", cmbSucursal.SelectedValue)
        gPregunta1 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2,  sum(op3) as op3, sum(op4) as op4,  sum(op5) as op5 " & _
        "from (select 'op1'=(case when preg1 = 0 then  1 end ), 'op2'=(case when preg1 = 1 then  1 else 0 end ) ," & _
        "'op3'=(case when preg1 = 2 then  1 else 0 end ), 'op4'=(case when preg1 = 3 then  1 else 0 end ) ," & _
        "'op5'=(case when preg1 = 4 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp", gPregunta1, cmbSucursal.SelectedValue)

        gPregunta2 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2,  sum(op3) as op3, sum(op4) as op4, sum(op5) as op5, sum(op6) as op6, sum(op7) as op7, sum(op8) as op8, sum(op9) as op9, sum(op10) as op10 from (select " & _
        "'op1'=(case when preg2 = 0 then  1 end ), 'op2'=(case when preg2 = 1 then  1 else 0 end ) ," & _
        "'op3'=(case when preg2 = 2 then  1 else 0 end ), 'op4'=(case when preg2 = 3 then  1 else 0 end ) ," & _
        "'op5'=(case when preg2 = 4 then  1 else 0 end ), 'op6'=(case when preg2 = 5 then  1 else 0 end ) ," & _
        "'op7'=(case when preg2 = 6 then  1 else 0 end ), 'op8'=(case when preg2 = 7 then  1 else 0 end ) ," & _
        "'op9'=(case when preg2 = 8 then  1 else 0 end ) ," & _
        "'op10'=(case when preg2 = 9 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp ", gPregunta2, cmbSucursal.SelectedValue)

        gPregunta3 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2,  sum(op3) as op3, sum(op4) as op4, sum(op5) as op5, sum(op6) as op6, sum(op7) as op7, sum(op8) as op8, sum(op9) as op9, sum(op10) as op10 from (select " & _
        "'op1'=(case when preg3 = 0 then  1 end ), 'op2'=(case when preg3 = 1 then  1 else 0 end ) ," & _
        "'op3'=(case when preg3 = 2 then  1 else 0 end ), 'op4'=(case when preg3 = 3 then  1 else 0 end ) ," & _
        "'op5'=(case when preg3 = 4 then  1 else 0 end ), 'op6'=(case when preg3 = 5 then  1 else 0 end ) ," & _
        "'op7'=(case when preg3 = 6 then  1 else 0 end ), 'op8'=(case when preg3 = 7 then  1 else 0 end ) ," & _
        "'op9'=(case when preg3 = 8 then  1 else 0 end ) ," & _
        "'op10'=(case when preg3 = 9 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp", gPregunta3, cmbSucursal.SelectedValue)

        gPregunta4 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2,  sum(op3) as op3, sum(op4) as op4, sum(op5) as op5, sum(op6) as op6, sum(op7) as op7, sum(op8) as op8, sum(op9) as op9, sum(op10) as op10 from (select " & _
        "'op1'=(case when preg4 = 0 then  1 end ), 'op2'=(case when preg4 = 1 then  1 else 0 end ) ," & _
        "'op3'=(case when preg4 = 2 then  1 else 0 end ), 'op4'=(case when preg4 = 3 then  1 else 0 end ) ," & _
        "'op5'=(case when preg4 = 4 then  1 else 0 end ), 'op6'=(case when preg4 = 5 then  1 else 0 end ) ," & _
        "'op7'=(case when preg4 = 6 then  1 else 0 end ), 'op8'=(case when preg4 = 7 then  1 else 0 end ) ," & _
        "'op9'=(case when preg4 = 8 then  1 else 0 end ) ," & _
        "'op10'=(case when preg4 = 9 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp", gPregunta4, cmbSucursal.SelectedValue)

        gPregunta5 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2,  sum(op3) as op3, sum(op4) as op4, sum(op5) as op5, sum(op6) as op6, sum(op7) as op7, sum(op8) as op8, sum(op9) as op9, sum(op10) as op10 from (select " & _
        "'op1'=(case when preg5 = 0 then  1 end ), 'op2'=(case when preg5 = 1 then  1 else 0 end ) ," & _
        "'op3'=(case when preg5 = 2 then  1 else 0 end ), 'op4'=(case when preg5 = 3 then  1 else 0 end ) ," & _
        "'op5'=(case when preg5 = 4 then  1 else 0 end ), 'op6'=(case when preg5 = 5 then  1 else 0 end ) ," & _
        "'op7'=(case when preg5 = 6 then  1 else 0 end ), 'op8'=(case when preg5 = 7 then  1 else 0 end ) ," & _
        "'op9'=(case when preg5 = 8 then  1 else 0 end ) ," & _
        "'op10'=(case when preg5 = 9 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp", gPregunta5, cmbSucursal.SelectedValue)

        gPregunta7 = funciones.LLenaGrid("select sum(op1) as op1, sum(op2) as op2 from (select " & _
        "'op1'=(case when preg7 = 0 then  1 end ) ," & _
        "'op2'=(case when preg7 = 1 then  1 else 0 end ) from encuestafisio where fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "' ) temp", gPregunta7, cmbSucursal.SelectedValue)




        'gPregunta5 = funciones.LLenaGrid("select idpaciente,paciente,preg5 as pregunta5 from encuestafisio where preg5 <> ' ' and fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "'", gPregunta5, cmbSucursal.SelectedValue)

        gPregunta6 = funciones.LLenaGrid("select idpaciente,paciente,correo,preg6 as pregunta6 from encuestafisio where preg6 <> ' ' and fecha>='" + txtfecha1.Text + "' and fecha<='" + txtfecha2.Text + "'", gPregunta6, cmbSucursal.SelectedValue)
    End Sub
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub cmbSucursal_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbSucursal.SelectedIndexChanged
        llenaDatos()
    End Sub

    Protected Sub Button1_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Button1.Click
        llenaDatos()
    End Sub
End Class
