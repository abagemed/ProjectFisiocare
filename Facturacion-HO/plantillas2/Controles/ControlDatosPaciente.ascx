<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ControlDatosPaciente.ascx.vb" Inherits="AgeMED.BuscadorPaciente" %>
<link href="dist/css/registro.css" rel="stylesheet" />
 <div class="box box-warning box-solid">
    <div class="box-header with-border">
       
            <h3 class="box-title">Datos del paciente</h3>
            <div class="box-tools pull-right">
                    <button type="button" class="btn btn-box-tool" data-widget="collapse">
                        <i class="fa fa-minus"></i>
                    </button>
                  <button type="button" class="btn btn-box-tool" data-widget="remove">
                        <i class="fa fa-remove"></i></button>
                </div>
        </div>
        <!-- /.box-header -->
        <!-- form start -->
      
            <div class="box-body">
                                <table class="TablaRegistro">
                    <tr>
                        <td>
                            <asp:Label ID="lbNombres" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NOMBRE(S)"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfNombre" runat="server" ControlToValidate="txtNombres" Display="Dynamic"
                                ErrorMessage="DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px">DATO OBLIGATORIO!</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lpapPaterno" runat="server" CssClass="control-label" Font-Names="Calibri" Text="APELLIDO PATERNO"></asp:Label></td>
                        <td>
                            <asp:Label ID="lbapMaterno" runat="server" CssClass="control-label" Font-Names="Calibri" Text="APELLIDO MATERNO"></asp:Label></td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <input id="txtNombres" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="50" onkeypress="return val(event)" />&nbsp;</td>
                        <td class="auto-style2">
                            <input id="TxtPapellido" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="50" onkeypress="return val(event)" />&nbsp;</td>
                        <td class="auto-style2">
                            <input id="TxtSapellido" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="50" onkeypress="return val(event)" />&nbsp;</td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Lblnacionalidad" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NACIONALIDAD"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="LblEdoNacimiento" runat="server" CssClass="control-label" Font-Names="Calibri" Text="EDO. DE NACIMIENTO"></asp:Label></td>
                        <td>
                            <asp:Label ID="LblFechaNacimeinto" runat="server" CssClass="control-label" Font-Names="Calibri" Text="FECHA DE NACIMIENTO"></asp:Label></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="DDNacionalidad" Cssclass="form-control" runat="server" Style="font-family: Calibri; width: 90%;"></asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="DDEdoNacimiento" Cssclass="form-control" runat="server" Style="font-family: Calibri; width: 90%;"></asp:DropDownList></td>
                        <td>
                            <table style="width: 100%">
                                <tr>
                                    <td class="fnetiqueta" style="width: 10%;">
                                        <label id="lbldia" class="control-label" style="font-family: Calibri; vertical-align: middle;">Dia  </label>
                                    </td>
                                    <td class="fncontrol" style="width: 21%;">
                                        <asp:DropDownList ID="DDdiasfecha" Cssclass="form-control" Style="padding: 6px 8px; font-family: Calibri; width: 90%;" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="fnetiqueta" style="width: 10%;">
                                        <label id="lblmes" class="control-label" style="font-family: Calibri; vertical-align: middle;">Mes  </label>
                                    </td>
                                    <td class="fncontrol" style="width: 21%;">
                                        <asp:DropDownList ID="DDmesesFecha" Cssclass="form-control" Style="padding: 6px 8px; font-family: Calibri; width: 90%;" runat="server"></asp:DropDownList>
                                    </td>
                                    <td class="fnetiqueta" style="width: 10%;">
                                        <label id="lblaño" class="control-label" style="font-family: Calibri; vertical-align: middle;">Año  </label>
                                    </td>
                                    <td class="fncontrol" style="width: 28%;">
                                        <asp:DropDownList ID="DDañosFecha" Cssclass="form-control" Style="padding: 6px 8px; font-family: Calibri; width: 90%;" runat="server"></asp:DropDownList></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblcurp" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CURP"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="Txtcurp" Display="Dynamic"
                                ErrorMessage="DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px">DATO OBLIGATORIO!</asp:RequiredFieldValidator>

                        </td>
                        <td>
                            <asp:Label ID="lblGenero" runat="server" CssClass="control-label" Font-Names="Calibri" Text="GENERO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Lbldireccion" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCION"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDireccion" Display="Dynamic"
                                ErrorMessage="DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px">DATO OBLIGATORIO!</asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <input id="Txtcurp" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;" maxlength="18" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:RadioButton ID="rbhombre" runat="server" GroupName="Genero" Text="HOMBRE " Checked="True" />
                            <asp:RadioButton ID="rbrdmujer" runat="server" GroupName="Genero" Text="MUJER" Style="margin-left: 1em;" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDireccion" Cssclass="form-control" runat="server" Font-Names="Calibri"
                                MaxLength="150" Rows="2" Style="font-family: Calibri; width: 90%;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Lblestado" runat="server" CssClass="control-label" Font-Names="Calibri" Text="ESTADO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lblmunicipio" runat="server" CssClass="control-label" Font-Names="Calibri" Text="MUNICIPIO"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="lbllocalidad" runat="server" CssClass="control-label" Font-Names="Calibri" Text="LOCALIDAD"></asp:Label>
                        </td>
                    </tr>
                    <tr runat="server" id="Regoficial">
                        <td runat="server">
                            <asp:DropDownList ID="DDestadosoficial" Cssclass="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em" AutoPostBack="True"></asp:DropDownList></td>
                        <td runat="server">
                            <asp:DropDownList ID="DDmunicipios" Cssclass="form-control" runat="server" Font-Names="Calibri" Font-Size="1em" Style="font-family: Calibri; width: 90%;" AutoPostBack="True"></asp:DropDownList></td>
                        <td runat="server">
                            <asp:DropDownList ID="DDlocalidades" Cssclass="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em"></asp:DropDownList></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="Lblcodigopostal" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CODIGO POSTAL"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Lblestadocivil" runat="server" CssClass="control-label" Font-Names="Calibri" Text="ESTADO CIVIL"></asp:Label>
                        </td>
                        <td>
                            <asp:Label ID="Lblocupacion" runat="server" CssClass="control-label" Font-Names="Calibri" Text="OCUPACION"></asp:Label>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <asp:TextBox ID="Txtcodigopostal" Cssclass="form-control" runat="server" Font-Names="Calibri"
                                MaxLength="5" Style="font-family: Calibri; width: 90%;"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="DDEstadoCivil" Cssclass="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em">
                            </asp:DropDownList></td>
                        <td>
                            <asp:DropDownList ID="DDocupacion" CssClass="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em">
                            </asp:DropDownList></td>
                    </tr>

                    <tr>
                        <td>
                            <asp:Label ID="lblTelefonoFijo" runat="server" CssClass="control-label" Font-Names="Calibri" Text="TELEFONO FIJO"></asp:Label></td>
                        <td>
                            <asp:Label ID="lbTelefonomovil" runat="server" CssClass="control-label" Font-Names="Calibri" Text="TELEFONO MOVIL"></asp:Label></td>
                        <td>
                            <asp:Label ID="lbmail" runat="server" CssClass="control-label" Font-Names="Calibri" Text="EMAIL"></asp:Label>
                            <asp:RegularExpressionValidator ID="rfEmail" runat="server" ControlToValidate="txtEmail"
                                Display="Dynamic" ErrorMessage="FORMATO INCORRECTO PARA DIRECCION ELECTRONICA"
                                Font-Italic="False" Font-Names="Calibri" Font-Size="8pt" ForeColor="#C00000"
                                ValidationExpression="\w+([-+.]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" ValidationGroup="gDatos" Width="311px">FORMATO INCORRECTO PARA DIRECCION ELECTRONICA</asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <input id="txtTelefonoFijo" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="10" onkeypress="return num(event)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <input id="txtTelefonoMovil" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="10" onkeypress="return num(event)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <asp:TextBox ID="txtEmail" runat="server" Cssclass="form-control" Font-Names="Calibri" Width="90%" MaxLength="50"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="Lblcontacto" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NOMBRE DEL CONTACTO"></asp:Label></td>
                        <td>
                            <asp:Label ID="Lbltelefonocontacto" runat="server" CssClass="control-label" Font-Names="Calibri" Text="TELEFONO DEL CONTACTO"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="width: 66%; max-width: 66%">
                            <input id="TxtNombreContacto" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="100" onkeypress="return val(event)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        <td>
                            <input id="TxtTelefonoContacto" type="text" runat="server" class="form-control" style="font-family: Calibri; width: 90%;"
                                maxlength="10" onkeypress="return num(event)" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                    </tr>

                </table>
            </div>
            <!-- /.box-body -->
            <div class="box-footer">
                <button type="submit" class="btn btn-default bg-red ">Cancelar</button>
                <button type="submit" class="btn btn-info pull-right">Guardar</button>
            </div>
            <!-- /.box-footer -->
       
    </div>




