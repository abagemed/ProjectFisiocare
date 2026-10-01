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


Partial Public Class BuscarPaciente

    '''<summary>
    '''Control UpdatePanelBusqueda.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelBusqueda As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control PanelAvisos.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAvisos As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeAviso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeAviso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelDesicion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelDesicion As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMostarDecision.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMostarDecision As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelCritico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelCritico As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeCritico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeCritico As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelAdvertencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAdvertencia As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblMensajeAdvertencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblMensajeAdvertencia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control TxbPaterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbPaterno As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxbPaterno_TextBoxWatermarkExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbPaterno_TextBoxWatermarkExtender As Global.AjaxControlToolkit.TextBoxWatermarkExtender

    '''<summary>
    '''Control TxbMaterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbMaterno As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxbMaterno_TextBoxWatermarkExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbMaterno_TextBoxWatermarkExtender As Global.AjaxControlToolkit.TextBoxWatermarkExtender

    '''<summary>
    '''Control TxbNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbNombre As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control TxbNombre_TextBoxWatermarkExtender.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxbNombre_TextBoxWatermarkExtender As Global.AjaxControlToolkit.TextBoxWatermarkExtender

    '''<summary>
    '''Control BotonBuscar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BotonBuscar As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control LstBoxPacientes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LstBoxPacientes As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Control RequiredFieldValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator2 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control BtnVerTexto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnVerTexto As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control HdPregunta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HdPregunta As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control Hdfolio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Hdfolio As Global.System.Web.UI.WebControls.HiddenField
End Class
