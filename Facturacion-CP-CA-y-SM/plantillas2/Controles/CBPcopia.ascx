<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="CBPcopia.ascx.vb" Inherits="AgeMED.BuscadorPaciente" %>
<link href="../dist/css/registro.css" rel="stylesheet" />
<div class="box box-warning box-solid">
    <div class="box-header with-border">
        <h3 class="box-title">Busqueda Paciente</h3>

        <div class="box-tools pull-right">
            <button type="button" class="btn btn-box-tool" data-widget="collapse">
                <i class="fa fa-minus"></i>
            </button>
            <button type="button" class="btn btn-box-tool" data-widget="remove">
                <i class="fa fa-remove"></i>
            </button>
        </div>
        <!-- /.box-tools -->
    </div>
    <!-- /.box-header -->
    <div class="box-body">

        <table class="TablaRegistro">
            <tr>
                <td>
                    <asp:Label ID="LblNombres2" runat="server" CssClass="control-label" Font-Names="Calibri" Text="NOMBRE(S)"></asp:Label>
                    <asp:TextBox ID="TxbNombre2" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="LblPaterno2" runat="server" CssClass="control-label" Font-Names="Calibri" Text="A. PATERNO"></asp:Label>
                    <asp:TextBox ID="TxbPaterno2" runat="server" CssClass="form-control"></asp:TextBox>
                    <asp:Label ID="LblMaterno2" runat="server" CssClass="control-label" Font-Names="Calibri" Text="A. MATERNO"></asp:Label>
                    <asp:TextBox ID="TxbMaterno2" runat="server" CssClass="form-control"></asp:TextBox>
                </td>
                <td>
                    <asp:ListBox ID="ListBox12" runat="server" CssClass="form-control" Height="163"></asp:ListBox>
                </td>
            </tr>
        </table>

    </div>

    <!-- /.box-body -->
    <div id="boton" class="box-footer">
        <asp:Button ID="BtnBuscarPaciente" runat="server" Text="Buscar" CssClass="btn btn-info  pull-left " />
    </div>
    <!-- /.box-footer -->



</div>
