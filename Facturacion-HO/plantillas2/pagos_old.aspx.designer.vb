'------------------------------------------------------------------------------
' <generado automáticamente>
'     Este código fue generado por una herramienta.
'
'     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
'     se vuelve a generar el código. 
' </generado automáticamente>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On


Partial Public Class pagos

    '''<summary>
    '''Control UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control lista_conceptos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lista_conceptos As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control cantidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cantidad As Global.System.Web.UI.HtmlControls.HtmlInputGenericControl

    '''<summary>
    '''Control LstConceptoPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LstConceptoPagos As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Control PanelAvisosPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAvisosPagos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeAvisoPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeAvisoPagos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelDesicionPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelDesicionPagos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblDecisionPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblDecisionPagos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelCriticoPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelCriticoPagos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeCriticoPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeCriticoPagos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelAdvertenciaPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAdvertenciaPagos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeAdvertenciaPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeAdvertenciaPagos As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control GdvConceptoPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdvConceptoPagos As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control mensaje_pago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents mensaje_pago As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control LblPagoTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblPagoTotal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control TxtAbono.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtAbono As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control LblPagoSaldo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblPagoSaldo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control HdPreguntasPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HdPreguntasPagos As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control HdIndexPagos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HdIndexPagos As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control Hffolioconsulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Hffolioconsulta As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control imprimir_ticket_btn.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents imprimir_ticket_btn As Global.System.Web.UI.HtmlControls.HtmlButton

    '''<summary>
    '''Control registrar_pago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents registrar_pago As Global.System.Web.UI.HtmlControls.HtmlButton

    '''<summary>
    '''Control GdvConceptoPagos2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdvConceptoPagos2 As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control LblPagoTotal2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblPagoTotal2 As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control forma_pago.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents forma_pago As Global.System.Web.UI.HtmlControls.HtmlSelect

    '''<summary>
    '''Control referencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents referencia As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control folio_consulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents folio_consulta As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control fecha_ticket.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fecha_ticket As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control paciente_nombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents paciente_nombre As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control paciente_telefono.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents paciente_telefono As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control paciente_direccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents paciente_direccion As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control GdvConceptoPagos3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdvConceptoPagos3 As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control LblPagoTotal3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblPagoTotal3 As Global.System.Web.UI.WebControls.Label
End Class
