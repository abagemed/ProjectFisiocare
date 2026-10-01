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


Partial Public Class facturas

    '''<summary>
    '''Control UpdatePanel1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents UpdatePanel1 As Global.System.Web.UI.UpdatePanel

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
    '''Control BtnSiGFac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnSiGFac As Global.System.Web.UI.WebControls.Button

    '''<summary>
    '''Control BtnNoGFac.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents BtnNoGFac As Global.System.Web.UI.WebControls.Button

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
    '''Control Fechas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Fechas As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control tipo_factura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents tipo_factura As Global.System.Web.UI.HtmlControls.HtmlSelect

    '''<summary>
    '''Control GdFacturas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdFacturas As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control Hdgfacturas.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Hdgfacturas As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control HdIndex.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HdIndex As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control HFolioF.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HFolioF As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control HdTotal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HdTotal As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control correo_confirmar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents correo_confirmar As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control folio_factura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents folio_factura As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control estado_factura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents estado_factura As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control div_select_razon_social.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_select_razon_social As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control select_razon_social.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents select_razon_social As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control div_input_razon_social.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents div_input_razon_social As Global.System.Web.UI.HtmlControls.HtmlGenericControl

    '''<summary>
    '''Control input_razon_social.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents input_razon_social As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control rfc.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfc As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control rfRFC.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfRFC As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control correo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents correo As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control rfEmail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfEmail As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control direccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents direccion As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control cp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents cp As Global.System.Web.UI.HtmlControls.HtmlInputGenericControl

    '''<summary>
    '''Control ciudad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents ciudad As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control estado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents estado As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control pais.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents pais As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control GdConsultasXpagar.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents GdConsultasXpagar As Global.System.Web.UI.WebControls.GridView

    '''<summary>
    '''Control observacionesf.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents observacionesf As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control codigo_paciente.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents codigo_paciente As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control id_razon_social.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents id_razon_social As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control folio_consulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents folio_consulta As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control bandera.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents bandera As Global.System.Web.UI.HtmlControls.HtmlInputHidden

    '''<summary>
    '''Control fconsulta.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents fconsulta As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Control HD_IdFactura.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents HD_IdFactura As Global.System.Web.UI.WebControls.HiddenField

    '''<summary>
    '''Propiedad Master.
    '''</summary>
    '''<remarks>
    '''Propiedad generada automáticamente.
    '''</remarks>
    Public Shadows ReadOnly Property Master() As AgeMED.Site1
        Get
            Return CType(MyBase.Master, AgeMED.Site1)
        End Get
    End Property
End Class
