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


Partial Public Class BuscarCitaPaciente

    '''<summary>
    '''Control box_BusquedaPaciente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents box_BusquedaPaciente As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control UpdatePanelCita.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanelCita As Global.System.Web.UI.UpdatePanel

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
    '''Control RequiredFieldValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator2 As Global.System.Web.UI.WebControls.RequiredFieldValidator

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
    '''Control LstBoxPacientes.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LstBoxPacientes As Global.System.Web.UI.WebControls.ListBox

    '''<summary>
    '''Control RequiredFieldValidator3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator3 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control UpdatePanel3.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel3 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control panelError.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents panelError As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblNomPaciente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblNomPaciente As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control PanelAviso2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAviso2 As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblAviso.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblAviso As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelDesicion2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelDesicion2 As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblDesicion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblDesicion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control BtnSi2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnSi2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BtnNo2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnNo2 As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control PanelCritico2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelCritico2 As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control Lblcritico.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblcritico As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control PanelAdvertencia2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents PanelAdvertencia2 As Global.System.Web.UI.WebControls.Panel

    '''<summary>
    '''Control LblAdvertencia.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblAdvertencia As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control dtgCitasProgramadas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dtgCitasProgramadas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control UpdatePanel2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel2 As Global.System.Web.UI.UpdatePanel

    '''<summary>
    '''Control dtgCitas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents dtgCitas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control Hdpregunta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Hdpregunta As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control Hdindex.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Hdindex As Global.System.Web.UI.WebControls.HiddenField
End Class
