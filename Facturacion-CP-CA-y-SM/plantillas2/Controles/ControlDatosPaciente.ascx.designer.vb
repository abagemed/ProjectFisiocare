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


Partial Public Class BuscadorPaciente

    '''<summary>
    '''Control lbNombres.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbNombres As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control rfNombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfNombre As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lpapPaterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lpapPaterno As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbapMaterno.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbapMaterno As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control txtNombres.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtNombres As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control TxtPapellido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtPapellido As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control TxtSapellido.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtSapellido As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control Lblnacionalidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblnacionalidad As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control LblEdoNacimiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblEdoNacimiento As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control LblFechaNacimeinto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents LblFechaNacimeinto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control DDNacionalidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDNacionalidad As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDEdoNacimiento.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDEdoNacimiento As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDdiasfecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDdiasfecha As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDmesesFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDmesesFecha As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDañosFecha.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDañosFecha As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lblcurp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblcurp As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator1.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator1 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control lblGenero.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblGenero As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Lbldireccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lbldireccion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control RequiredFieldValidator2.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents RequiredFieldValidator2 As Global.System.Web.UI.WebControls.RequiredFieldValidator

    '''<summary>
    '''Control Txtcurp.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Txtcurp As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control rbhombre.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rbhombre As Global.System.Web.UI.WebControls.RadioButton

    '''<summary>
    '''Control rbrdmujer.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rbrdmujer As Global.System.Web.UI.WebControls.RadioButton

    '''<summary>
    '''Control txtDireccion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtDireccion As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control Lblestado.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblestado As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lblmunicipio.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblmunicipio As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbllocalidad.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbllocalidad As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Regoficial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Regoficial As Global.System.Web.UI.HtmlControls.HtmlTableRow

    '''<summary>
    '''Control DDestadosoficial.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDestadosoficial As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDmunicipios.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDmunicipios As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDlocalidades.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDlocalidades As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control Lblcodigopostal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblcodigopostal As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Lblestadocivil.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblestadocivil As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Lblocupacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblocupacion As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Txtcodigopostal.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Txtcodigopostal As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control DDEstadoCivil.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDEstadoCivil As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control DDocupacion.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents DDocupacion As Global.System.Web.UI.WebControls.DropDownList

    '''<summary>
    '''Control lblTelefonoFijo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lblTelefonoFijo As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbTelefonomovil.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbTelefonomovil As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control lbmail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents lbmail As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control rfEmail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents rfEmail As Global.System.Web.UI.WebControls.RegularExpressionValidator

    '''<summary>
    '''Control txtTelefonoFijo.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTelefonoFijo As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control txtTelefonoMovil.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtTelefonoMovil As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control txtEmail.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents txtEmail As Global.System.Web.UI.WebControls.TextBox

    '''<summary>
    '''Control Lblcontacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lblcontacto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control Lbltelefonocontacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents Lbltelefonocontacto As Global.System.Web.UI.WebControls.Label

    '''<summary>
    '''Control TxtNombreContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtNombreContacto As Global.System.Web.UI.HtmlControls.HtmlInputText

    '''<summary>
    '''Control TxtTelefonoContacto.
    '''</summary>
    '''<remarks>
    '''Campo generado automáticamente.
    '''Para modificarlo, mueva la declaración del campo del archivo del diseñador al archivo de código subyacente.
    '''</remarks>
    Protected WithEvents TxtTelefonoContacto As Global.System.Web.UI.HtmlControls.HtmlInputText
End Class
