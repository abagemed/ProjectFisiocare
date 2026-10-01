<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="AltaEmpresa.aspx.vb" Inherits="AgeMED.AltaEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 34%; 
        }
        .auto-style2 {
            height: 0.6em;
        }
        .auto-style3 {
            height: 0.4em;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <link href="dist/css/registro.css" rel="stylesheet" />

    <script>
            function ValSoloLetras(e) {
                tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) return true;
                patron = /[A-Za-z]/;
                te = String.fromCharCode(tecla);
                return patron.test(te);
            }
        </script>
    <script>
        function ValSoloNumCP(e) {
            tecla = (document.all) ? e.keyCode : e.which;
            if (tecla == 8) return true;
            patron = /[0-9]/;
            te = String.fromCharCode(tecla);
            return patron.test(te);
        }
        </script>
        <script>
            function ValSoloLetrasEspacioBlanco(e) {
                tecla = (document.all) ? e.keyCode : e.which;
                if (tecla == 8) return true;
                patron = /[A-Za-z ]/;
                te = String.fromCharCode(tecla);
                return patron.test(te);
            }
        </script>
 
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate >
    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Datos de la empresa</h3>
        </div>
              
        <!-- inicia Div Datos de la empresa formulario -->
            <div class="box-body">
                <table class="TablaRegistro">
                    <tr>
                        <td>
                            <asp:Label ID="lbNombre" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NOMBRE DE EMPRESA"></asp:Label>
                            <asp:RequiredFieldValidator ID="rfNombre" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
                                ErrorMessage="DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px">DATO OBLIGATORIO!</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="lbCalle" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: calle"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCalle" Display="Dynamic"
                                ErrorMessage="  DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="lbNumExterior" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: No exterior"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="TxtNumExterior" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <input id="txtNombre" type="text" runat="server" class="form-control" style="text-transform:uppercase ; font-family: Calibri; width: 90%;"
                                maxlength="30" onkeypress="return ValSoloLetrasEspacioBlanco(event)" />&nbsp;
                        </td>
                        <td class="auto-style2">
                            <input id="txtCalle" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" autocomplete="on" />&nbsp;
                        </td>
                        <td class="auto-style1">
                            <input id="TxtNumExterior" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="LbNumInterior" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: No interior"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="TxtNumInterior" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                         </td>
                        <td class="auto-style1">
                            <asp:Label ID="LbColonia" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: Colonia"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="TxtColonia" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="LbPais" runat="server" CssClass="control-label" Font-Names="Calibri" Text="PAIS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <input id="TxtNumInterior" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                        <td class="auto-style1">
                            <input id="TxtColonia" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DDPais" class="form-control" runat="server" Style="font-family: Calibri; width: 90%;"></asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style1">
                            <asp:Label ID="LbEstado" runat="server" CssClass="control-label" Font-Names="Calibri" Text="ESTADO"></asp:Label>
                         </td>
                         <td>
                            <asp:Label ID="LbMunicipio" runat="server" CssClass="control-label" Font-Names="Calibri" Text="MUNICIPIO"></asp:Label>
                         </td>
                         <td class="auto-style1">
                            <asp:Label ID="LbCiudad" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CIUDAD"></asp:Label>
                         </td>
                    </tr>
                    <tr>
                        <td runat="server">
                            <asp:DropDownList ID="DDEstado" class="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em" AutoPostBack="true"></asp:DropDownList>&nbsp;
                        </td>
                        <td runat="server">
                            <asp:DropDownList ID="DDMunicipio" class="form-control" runat="server" Font-Names="Calibri" Font-Size="1em" Style="font-family: Calibri; width: 90%;" AutoPostBack="True"></asp:DropDownList>&nbsp;
                        </td>
                        <td runat="server">
                             <asp:DropDownList ID="DDCiudad" class="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em"></asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style1">
                            <asp:Label ID="LbCP" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CODIGO POSTAL"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="LbDescripcion" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DESCRIPCION"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:TextBox ID="TxtCP" class="form-control" runat="server" Font-Names="Calibri" Style="font-family:Calibri; width:90%" MaxLength="5" onkeypress="return ValSoloNumCP(event)"></asp:TextBox>
                        </td>
                         <td runat="server" colspan="2" style="width:100%; max-width: initial">
                            <input id="TxtDescripcion" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family: Calibri; width:95%;"
                                maxlength="70" onkeypress="return ValSoloLetrasEspacioBlanco(event)" />
                        </td>
                    </tr>
                </table>
            </div>
    </div>
    <div class="box box-info">
        <div class="box-header with-border">
            <h3 class="box-title">Datos de facturación</h3>
        </div>
            
        <!-- inicia Div Datos de facturacion -->
            <div class="box-body">
                <table class="TablaRegistro">
                    <tr>
                        <td>
                            <asp:Label ID="LbFNombre" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NOMBRE DE EMPRESA"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtNombre" Display="Dynamic"
                                ErrorMessage="DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px">DATO OBLIGATORIO!</asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label2" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: calle"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCalle" Display="Dynamic"
                                ErrorMessage="  DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                        </td>
                        <td class="auto-style1">
                            <asp:Label ID="Label3" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: No exterior"></asp:Label>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="TxtNumExterior" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                            </td>
                    </tr>
                    <tr>
                        <td class="auto-style2">
                            <input id="Text1" type="text" runat="server" class="form-control" style="text-transform:uppercase ; font-family: Calibri; width: 90%;"
                                maxlength="30" onkeypress="return ValSoloLetrasEspacioBlanco(event)" />&nbsp;
                        </td>
                        <td class="auto-style2">
                            <input id="Text2" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" autocomplete="on" />&nbsp;
                        </td>
                        <td class="auto-style1">
                            <input id="Text3" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:Label ID="Label4" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: No interior"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="TxtNumInterior" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                         </td>
                        <td class="auto-style1">
                            <asp:Label ID="Label5" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DIRECCIÓN: Colonia"></asp:Label>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="TxtColonia" Display="Dynamic"
                                ErrorMessage="   DATO OBLIGATORIO!" Font-Bold="False" Font-Italic="False" Font-Names="Calibri" Font-Size="8pt"
                                ForeColor="#C00000" SetFocusOnError="True" ValidationGroup="gDatos" Width="165px"></asp:RequiredFieldValidator>
                        </td>
                        <td>
                            <asp:Label ID="Label6" runat="server" CssClass="control-label" Font-Names="Calibri" Text="PAIS"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <input id="Text4" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                        <td class="auto-style1">
                            <input id="Text5" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family:Calibri; width: 90%;"
                                maxlength="25" onkeypress="return ValSoloLetras(event)" />&nbsp;
                        </td>
                        <td>
                            <asp:DropDownList ID="DropDownList1" class="form-control" runat="server" Style="font-family: Calibri; width: 90%;"></asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style1">
                            <asp:Label ID="Label7" runat="server" CssClass="control-label" Font-Names="Calibri" Text="ESTADO"></asp:Label>
                         </td>
                         <td>
                            <asp:Label ID="Label8" runat="server" CssClass="control-label" Font-Names="Calibri" Text="MUNICIPIO"></asp:Label>
                         </td>
                         <td class="auto-style1">
                            <asp:Label ID="Label9" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CIUDAD"></asp:Label>
                         </td>
                    </tr>
                    <tr>
                        <td runat="server">
                            <asp:DropDownList ID="DropDownList2" class="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em" AutoPostBack="true"></asp:DropDownList>&nbsp;
                        </td>
                        <td runat="server">
                            <asp:DropDownList ID="DropDownList3" class="form-control" runat="server" Font-Names="Calibri" Font-Size="1em" Style="font-family: Calibri; width: 90%;" AutoPostBack="True"></asp:DropDownList>&nbsp;
                        </td>
                        <td runat="server">
                             <asp:DropDownList ID="DropDownList4" class="form-control" runat="server" Font-Names="Calibri" Style="font-family: Calibri; width: 90%;" Font-Size="1em"></asp:DropDownList>&nbsp;
                        </td>
                    </tr>
                    <tr>
                         <td class="auto-style1">
                            <asp:Label ID="Label10" runat="server" CssClass="control-label" Font-Names="Calibri" Text="CODIGO POSTAL"></asp:Label>
                        </td>
                        <td colspan="2">
                            <asp:Label ID="Label11" runat="server" CssClass="control-label" Font-Names="Calibri" Text="DESCRIPCION"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td class="auto-style1">
                            <asp:TextBox ID="TextBox1" class="form-control" runat="server" Font-Names="Calibri" Style="font-family:Calibri; width:90%" MaxLength="5" onkeypress="return ValSoloNumCP(event)"></asp:TextBox>
                        </td>
                         <td runat="server" colspan="2" style="width:100%; max-width: initial">
                            <input id="Text6" type="text" runat="server" class="form-control" style="text-transform :uppercase ; font-family: Calibri; width:95%;"
                                maxlength="70" onkeypress="return ValSoloLetrasEspacioBlanco(event)" />
                        </td>
                    </tr>
                </table>
            </div>
         <!-- Div botones aceptar | cancelar -->
            <div class="box-footer">
                <asp:Button ID="BtnCancelar" runat="server" Text="Cancelar" class="btn btn-default bg-red " />
                <asp:Button ID="BtnGuardar" runat="server" Text="Guardar"  CausesValidation ="true" ValidationGroup ="gDatos" class="btn btn-info pull-right"/>                
            </div>
            <!-- /.box-footer -->
       <%-- </form>--%>
    </div>
            </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
