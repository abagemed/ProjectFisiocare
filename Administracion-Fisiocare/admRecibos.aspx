<%@ Page Language="VB" AutoEventWireup="false" CodeFile="admRecibos.aspx.vb" Inherits="admRecibos" %>

<%@ Register Assembly="messagebox" Namespace="messagebox" TagPrefix="cc2" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<%--<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">--%>
<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
 <link href="rsc/estilo.css" rel="stylesheet" type="text/css" />
    <title>Administracion Fisiocare</title>
     <!-- google font -->
    <link href="https://fonts.googleapis.com/css?family=Poppins:300,400,500,600,700" rel="stylesheet" type="text/css" />
    <link href="https://stackpath.bootstrapcdn.com/font-awesome/4.7.0/css/font-awesome.min.css" rel="stylesheet" integrity="sha384-wvfXpqpZZVQGK6TAh5PVlGOfQNHSoD2xbE+QkPxCAFlNEevoEH3Sl0sibVcOQVnN" crossorigin="anonymous" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/material-design-iconic-font/2.2.0/css/material-design-iconic-font.min.css" />
    <!-- bootstrap -->
	<link href="assets/plugins/bootstrap/css/bootstrap.min.css" rel="stylesheet" type="text/css" />
    <!-- Material Design Lite CSS -->
	<link href="assets/plugins/material/material.min.css" rel="stylesheet" />
	<link href="assets/css/material_style.css" rel="stylesheet" />
	<!-- Theme Styles -->
    <link href="assets/css/style.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/plugins.min.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/responsive.css" rel="stylesheet" type="text/css" />
	<link href="assets/css/theme-color.css" rel="stylesheet" type="text/css" />
    <link href="assets/css/pages/typography.css" rel="stylesheet" type="text/css" />
    <!-- favicon -->
    <link rel="shortcut icon" href="imagenes/favicon.png" />
    <!-- DataTables -->
	<link rel="stylesheet" type="text/css" href="https://cdn.datatables.net/v/dt/jszip-2.5.0/dt-1.10.16/af-2.2.2/b-1.5.1/b-colvis-1.5.1/b-flash-1.5.1/b-html5-1.5.1/b-print-1.5.1/cr-1.4.1/fc-3.2.4/fh-3.1.3/kt-2.3.2/r-2.2.1/rg-1.0.2/rr-1.2.3/sc-1.4.4/sl-1.2.5/datatables.min.css"/>

    <style type="text/css">
    .FondoAplicacion
    {
        background-color: Gray;
        filter: alpha(opacity=70);
        opacity: 0.7;
    }</style>

</head>
<body><center>
    <form id="form1" runat="server">
   <div style="width:900px; height:870px;border: solid 1px #ff0000; font-family:Calibri; font-size:11pt;" align="center">
        <img src="imagenes/baner1.png" />&nbsp;<asp:Menu ID="Menu1" runat="server" BackColor="#F4F4F4"
            BorderColor="DimGray" BorderStyle="Solid" BorderWidth="1px" Font-Bold="True"
            Font-Names="Calibri" Font-Size="14px" ForeColor="#1A3773" Orientation="Horizontal"
            Width="900px">
            <StaticMenuItemStyle VerticalPadding="3px" Width="100px" />
            <DynamicHoverStyle BackColor="#666666" ForeColor="White" />
            <DynamicMenuStyle BackColor="#E3EAEB" />
            <DynamicSelectedStyle BackColor="#1C5E55" />
            <DynamicMenuItemStyle BackColor="#F4F4F4" BorderColor="White" BorderStyle="Solid"
                BorderWidth="1px" HorizontalPadding="3px" VerticalPadding="4px" Width="250px" />
            <StaticHoverStyle BackColor="#666666" ForeColor="White" />
            <Items>
                <asp:MenuItem NavigateUrl="~/Defadmon.aspx" Text="Reporte de Ocupaci&#243;n" Value="0">
                </asp:MenuItem>
                <asp:MenuItem Selectable="False" Text="Agenda" Value="Agenda">
                    <asp:MenuItem NavigateUrl="~/CentroCostos.aspx" Text="Centro de Costos" Value="1"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/admUsuarios.aspx" Text="Administraci&#243;n Clientes"
                        Value="2"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/admRecibos.aspx" Text="Administrar Recibos" Value="5"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/terapistas.aspx" Text="Terapistas" Value="4"></asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/DefaultSM.aspx" Text="Agenda Star Medica" Value="Agenda Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/DefaultCP.aspx" Text="Agenda Campestre" Value="Agenda Campestre">
                    </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Selectable="False" Text="Contabilidad" Value="Contabilidad">
                    <asp:MenuItem NavigateUrl="~/Movimientos.aspx" Text="Movimientos" Value="bancos.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/conFacturas.aspx" Text="Consultas Facturas" Value="confacturas.aspx">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/consultasmed.aspx" Text="Consultas Médicas" Value="consultasmed.aspx">
                </asp:MenuItem>
                </asp:MenuItem>
                <asp:MenuItem Text="Camaras" Value="Camaras">
                    <asp:MenuItem NavigateUrl="~/camarasStar.aspx" Text="Camaras Star Medica" Value="Camaras Star Medica">
                    </asp:MenuItem>
                    <asp:MenuItem NavigateUrl="~/camarasplaza.aspx" Text="Camaras Star plaza" Value="Camaras Star plaza">
                    </asp:MenuItem>
                </asp:MenuItem>
            </Items>
        </asp:Menu>
       <asp:ScriptManager ID="ScriptManager1" runat="server">
       </asp:ScriptManager>
       <cc2:messagebox ID="Messagebox1" runat="server" />
      
       <!-- start page content -->
            <div ID="Panel1" runat="server">
                <div class="page-content">
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-box">
                                        <div class="card-head">
                                            <header>
                                                <asp:Label ID="lblfecNom" runat="server"></asp:Label>
                                                <asp:Label ID="Label1" runat="server" Text="Label" Visible="False"></asp:Label>
                                               Gestion de Abonos
                                            </header>
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                           <div class="row">
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                    <asp:DropDownList ID="cmbSuc" runat="server" Width="203px" class="form-control">
               <asp:ListItem Value="fisiocaresm">Star Medica</asp:ListItem>
               <asp:ListItem Value="fisiocarecp">Campestre</asp:ListItem>
               <asp:ListItem Value="fisiocareho">HO</asp:ListItem>
               <asp:ListItem Value="fisiocareca">Antincanceroso</asp:ListItem>
               <asp:ListItem Value="gym">Gym</asp:ListItem>
               <asp:ListItem Value="fisiocaream">Amerimed</asp:ListItem>
           </asp:DropDownList>
                                                </div>
                                                <div class="col-lg-4 col-md-3 col-sm-12 col-xs-12">
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
               runat="server" ControlToValidate="txtRecibo" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                <asp:TextBox ID="txtRecibo" runat="server" class="form-control" placeholder="Escribe No. Recibo"></asp:TextBox>
                                                    <cc1:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
               TargetControlID="txtRecibo">
           </cc1:FilteredTextBoxExtender>
                                                </div>
                                            </div>

                                            <div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <asp:Button ID="Button1" runat="server" Text="Modificar Abonos"  class="btn btn-primary btn-block" ForeColor="Red" />
                                                    <%--<button type="button" class="btn btn-primary btn-block" onclick="Control(1)"><i class="fa fa-search"></i> Buscar</button>--%>
                                                </div>
                                            </div>

                                            <%--<div class="row margin-right-10">
                                                <div class="col-md-12">
                                                    <h3>Resultados de la búsqueda</h3>
                                                    <select class="form-control" id="lista_pacientes" size="5">

                                                    </select>
                                                </div>
                                            </div>--%>

                                            <div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <asp:Button ID="btnAgregar" runat="server" Text="Agregar Pagos"  class="btn btn-success btn-block" ForeColor="Red" />
                                                </div>
                                            </div>
                                           
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end page content -->
         <!-- start page content -->
            <div ID="Panel2" runat="server" visible="false">
                <div class="page-content">
                    
                    <div class="row">
                        <div class="col-md-12">
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="card card-box">
                                        <div class="card-head">
                                            <header>
                                                <asp:Label ID="Label2" runat="server"></asp:Label>
                                                <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
                                               Gestion de Abonos
                                            </header>
                                            <div class="tools">
			                                    <a class="t-collapse btn-color fa fa-chevron-down" href="javascript:;"></a>
                                            </div>
                                        </div>
                                        <div class="card-body">
                                           <div class="row">
                                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                   <asp:Label id="Label4"  runat="server" Text="PACIENTE" Font-Bold="True" Font-Size="16px"></asp:Label>
                                                   </div>
                                               <div class="col-lg-4 col-md-4 col-sm-12 col-xs-12">
                                                   <asp:Label id="lblPaciente"  runat="server" Font-Bold="True" Font-Size="16px"></asp:Label>
                                                   </div>
                                               </div>
                                            <div class="row">
                                               <div class="col-lg-12 col-md-12 col-sm-12 col-xs-12">
                                                <asp:DataGrid id="gRecibos" runat="server" Width="870px" DataKeyField="idAbono" AutoGenerateColumns="False">
           <Columns>
               <asp:BoundColumn DataField="fecha" HeaderText="Fecha"></asp:BoundColumn>
               <asp:TemplateColumn HeaderText="CatCosto">
                   <ItemTemplate>
                       <asp:DropDownList ID="DropDownList1" DataSource='<%# catCostos() %>' DataTextField="descripcion" DataValueField="responsable" 
                         runat="server" Width="214px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                       </asp:DropDownList>
                   </ItemTemplate>
               </asp:TemplateColumn>
               <asp:TemplateColumn HeaderText="Tipo Terapia">
                   <ItemTemplate>
                       <asp:DropDownList ID="DropDownList2" runat="server" Width="250px" AutoPostBack="True" OnSelectedIndexChanged="DropDownList2_SelectedIndexChanged">
                       </asp:DropDownList>
                   </ItemTemplate>
               </asp:TemplateColumn>
               <asp:TemplateColumn HeaderText="Abono">
                   <ItemTemplate>
                       <asp:TextBox ID="TextBox2" AutoPostBack="true" OnTextChanged="TextBox2_TextChanged" Style=" text-align:center" Text='<%# Bind("abono") %>' runat="server" Width="75px"></asp:TextBox>
                   </ItemTemplate>
               </asp:TemplateColumn>
               <asp:BoundColumn HeaderText="Importe" DataField="importe"></asp:BoundColumn>
               <asp:TemplateColumn HeaderText="Tipo Pago">
                   <ItemTemplate>
                       <asp:DropDownList ID="ListTpago" runat="server" Width="120px" AutoPostBack="True" OnSelectedIndexChanged="ListTpago_SelectedIndexChanged">
                       </asp:DropDownList>
                   </ItemTemplate>
               </asp:TemplateColumn>
               <asp:TemplateColumn>
                   <ItemTemplate >
                       <asp:LinkButton ID="LinkButton1" runat="server"  CssClass="boton" ForeColor="Red">Eliminar</asp:LinkButton>
                   </ItemTemplate>
               </asp:TemplateColumn>
           </Columns>
       </asp:DataGrid>
                                            </div>
                                               </div>
                                            <div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <asp:Button ID="Button2" runat="server" Text="Terminar" Width="121px"  class="btn btn-success btn-block" ForeColor="Red" />
                                                </div>
                                            </div>



                                            <%--<div class="row margin-right-10">
                                                <div class="col-md-12">
                                                    <h3>Resultados de la búsqueda</h3>
                                                    <select class="form-control" id="lista_pacientes" size="5">

                                                    </select>
                                                </div>
                                            </div>--%>

                                            <%--<div class="row margin-top-10">
                                                <div class="col-md-12">
                                                    <asp:Button ID="Button4" runat="server" Text="Agregar Pagos"  class="btn btn-success btn-block" ForeColor="Red" />
                                                </div>
                                            </div>--%>
                                           
                                        </div>
                                        
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <!-- end page content -->





       <br />
       <br />
       <asp:Panel ID="Panel201" runat="server" Visible="False" Width="899px" HorizontalAlign="Center">
           <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                   <td style="text-align:left">
                   </td>
               <tr>
                   <td style="text-align:center">
                   </td>
               </tr>
               <tr>
                   <td style="text-align:center">
                   </td>
               </tr>
               <tr><TD style="TEXT-ALIGN: left" ></TD></tr>
               <tr><TD style="TEXT-ALIGN: center"></TD></tr>
               <tr >
                   <td style="text-align: center">
                       <br />
                       </td>
               </tr>
           </table>           
           </asp:Panel>
          <%-- <asp:Panel ID="Panel3" runat="server" Height="220px" Width="840px" BackColor="White" BorderColor="Gray" BorderStyle="Dotted">
               <table border="0" cellpadding="7" cellspacing="0" style="width: 100%">
                   <tr>
                       <td style="width: 118px; text-align:left">
               </td>
                       <td style="width: 500px; text-align:left">
               </td>
                   </tr>
               </table>
               <br />
             <table border="0" cellpadding="4" cellspacing="0" style="width: 100%">
                   <tr>
                       <td style="background-color:#f4f4f4; color:#1a3773">
                           CATALOGO COSTOS</td>
                       <td style="background-color:#f4f4f4; color:#1a3773">
                           TERAPIA</td>
                       <td style="background-color:#f4f4f4; color:#1a3773">
                           IMPORTE</td>
                       <td style="background-color:#f4f4f4; color:#1a3773">
                           ABONO</td>
                   </tr>
                   <tr>
                       <td>
               </td>
                       <td>
               </td>
                       <td>
               </td>
                       <td>
               </td>
                   </tr>
                   <tr>
                       <td>
                       </td>
                       <td>
                       </td>
                       <td>
                       </td>
                       <td>
                       </td>
                   </tr>
               </table>
               <br />
               <br />
               <asp:Button ID="Button4" runat="server" Text="Grabar" Width="121px" CssClass="boton" ForeColor="Red" />
               &nbsp; &nbsp; &nbsp;&nbsp;
               <asp:Button ID="Button5" runat="server" Text="Cancelar" Width="121px" CssClass="boton" ForeColor="Red" />

           </asp:Panel>--%>

       <%--nuevo modal para agregar AB14052020--%>
       <ajaxToolkit:ModalPopupExtender ID="mpagregarabono" runat="server" PopupControlID="PanelAB" TargetControlID="hfcitan" CancelControlID="BTNcerrarAB" BackgroundCssClass="FondoAplicacion">
                                                </ajaxToolkit:ModalPopupExtender>
       <asp:HiddenField runat="server" ID="hfcitan" />
        <asp:Panel runat="server" ID="PanelAB" CssClass="panelpp">
                <div class="example-modal">
                    <div class="modal-1">
                        <div class="modal-dialog modal-lg">
                            <div class="modal-content">
                                <div class="modal-header">
                                    <h4 class="modal-title">Agregar Pagos</h4>
                                </div>
                                <div class="modal-body">
                                    <div class="form-group">
                                    <label class="input-group col-md-12">NOMBRE DEL PACIENTE</label>
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-user"></i></span>
                                       <asp:Label ID="lblcliente" runat="server" Font-Size="12pt"></asp:Label>
                                    </div>
                                </div>
                                    <div class="form-group" hidden>
                                    <label class="input-group col-md-12">FECHA</label>
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-calendar"></i></span>
                                      <asp:Label ID="lblfecha" runat="server" Font-Size="12pt"></asp:Label>
                                    </div>
                                </div>
                                    <div class="form-group">
                                    <label class="input-group col-md-12">CENTRO DE COSTO</label>
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                     <asp:DropDownList ID="cmbCatCostoA" runat="server" AutoPostBack="True" Width="300px" class="form-control">
               </asp:DropDownList>
                                    </div>
                                </div>
                                    <div class="form-group">
                                    <label class="input-group col-md-12">TERAPIA</label>
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-search"></i></span>
                                     <asp:DropDownList ID="cmbTipoterapiaA" runat="server" AutoPostBack="True" Width="300px" class="form-control">
               </asp:DropDownList>
                                    </div>
                                </div>
                                    <%--<div class="form-group">
                                    <label class="input-group col-md-12">IMPORTE DE LA TERAPIA</label>
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                                     <asp:Label ID="lblImporte" runat="server" CssClass="form-control"></asp:Label>
                                    </div>
                                    </div>--%>
                                    <%--<div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-dollar"></i></span>
                                     <asp:TextBox ID="TxtAbono" runat="server" CssClass="form-control" placeholder="Importe del Abono"></asp:TextBox>
                                        
                                    </div>
                                    </div>--%>
                                    <br />
                                    <div class="row">
                <div class="col-md-3">
                    <label class="input-group col-md-12">IMPORTE</label>
                </div>
                <div class="col-md-3">
                    <asp:Label ID="lblImporte" runat="server" CssClass="form-control"></asp:Label>
                </div>
                <div class="col-md-3">
                    <label class="input-group col-md-12">ABONO</label>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="TxtAbono" runat="server" CssClass="form-control" placeholder="Importe del Abono"></asp:TextBox>
                </div>
            </div>

                                    <br />
                                    <div class="row">
                <div class="col-md-3">
                    <label class="input-group col-md-12">SUBTOTAL</label>
                   <%-- <asp:Label ID="Label10" runat="server" Text="Subtotal"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtsubtotal" runat="server" ReadOnly="True" CssClass="form-control">0.0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="input-group col-md-12">IMPORTE IVA</label>
                   <%-- <asp:Label ID="Label11" runat="server" Text="Importe IVA"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtiva" runat="server" ReadOnly="True" CssClass="form-control">0</asp:TextBox>
                </div>
            </div>
                                    <br />
             <div class="row">
                <div class="col-md-3">
                    <label class="input-group col-md-12">CLAVE SAT</label>
                  <%--  <asp:Label ID="Label9" runat="server" Text="Clave SAT"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtcsat" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="input-group col-md-12">CLAVE UNIDAD</label>
                  <%--  <asp:Label ID="Label13" runat="server" Text="Clave Unidad"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtcunidad" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                 
            </div>
                                    <br />
            <div class="row">
                <div class="col-md-3">
                    <label class="input-group col-md-12">UNIDAD</label>
                    <%--<asp:Label ID="Label12" runat="server" Text="Unidad"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txtunidad" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
                <div class="col-md-3">
                    <label class="input-group col-md-12">TASA IVA</label>
                    <%--<asp:Label ID="Label14" runat="server" Text="Tasa Iva"></asp:Label>--%>
                </div>
                <div class="col-md-3">
                    <asp:TextBox ID="txttasai" ReadOnly="True" runat="server" CssClass="form-control">0</asp:TextBox>
                </div>
            </div>
                                    <br />
                                    
                                    <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font "></i></span>
                                      <asp:TextBox ID="txtobservacionAB"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Motivo del nuevo pago"></asp:TextBox>
                                    </div>
                                </div>
                                    <div class="form-group">
                                    <div class="input-group col-md-12">
                                      <asp:LinkButton ID="lkb2" Text="Guardar Cambios" CssClass="btn btn-block btn-info" runat="server" Font-Names="Calibri" Font-Size="16px"></asp:LinkButton>
                                     <button id="BTNcerrarAB" type="button" class="btn btn-block btn-secondary" data-dismiss="modal">Cancelar</button>
                                    </div>
                                </div>
                                </div>
                                <div class="modal-footer">
					               
					            </div>
                                <input id="hfidcliente" runat="server" type="hidden" />
               <input id="hfiddoctor" runat="server" type="hidden" />
                                <input id="Nidabono" runat="server" type="hidden" />

                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </div>
                <!-- /.example-modal -->
            </asp:Panel>


<br />
           <%--<cc1:modalpopupextender id="ModalPopupExtender1" runat="server" backgroundcssclass="FondoAplicacion"
               popupcontrolid="Panel3" targetcontrolid="hfmodal"></cc1:modalpopupextender>
       <input id="hfmodal" runat="server" type="hidden" />--%>

       <asp:Panel ID="Panelc" runat="server" BackColor="White" BorderColor="#404040" BorderStyle="Dashed"
             BorderWidth="2px" Height="240px" Width="370px">
             <br />
             <table border="0" cellpadding="5" cellspacing="0" style="width: 350px; text-align:center">
                 <tr>
                     <td style="background-color:#f4f4f4; color:#1a3773; font-size:18pt">
                         NÚMERO DE RECIBO A CANCELAR&nbsp;</td>
                 </tr>
                 <tr>
                     <td style="">
                         <asp:TextBox ID="txtnumcancelar" runat="server" BackColor="White" BorderColor="White" BorderStyle="Solid"
                             BorderWidth="1px" Font-Names="calibri" Font-Size="18pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="163px" Enabled="False"></asp:TextBox>
                         </td>
                 </tr>
                 <tr>
                     <td style="background-color: #f4f4f4; color:#1A3773; font-size:16pt">
                         MOTIVO DE CANCELACION
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtmotivo"
                             ErrorMessage="*" ValidationGroup="cancelar"></asp:RequiredFieldValidator>
                         <asp:TextBox ID="txtmotivo" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200"></asp:TextBox>

                         <asp:TextBox ID="txtidabono" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                          <asp:TextBox ID="txtidcliente" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtfecha" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtabonoc" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtimportec" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtidcosto" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                         <asp:TextBox ID="txtidpago" runat="server" BackColor="White" BorderColor="White"
                             BorderStyle="Solid" BorderWidth="1px" Rows="3" Font-Names="calibri" Font-Size="16pt" Style="border-bottom: #e0e0e0 2px solid;
                             text-align: center" Width="315px" MaxLength="200" Visible="false"></asp:TextBox>
                     </td>
                 </tr>
                 <tr>
                     <td style=" background-color:#f4f4f4">
                         <asp:Button ID="Button6" runat="server" CssClass="boton" ForeColor="#000040" Text="Aceptar" ValidationGroup="cancelar" />
                         &nbsp;
                         <asp:Button ID="Button7" runat="server" CssClass="boton" ForeColor="#000040" Text="Cancelar" /></td>
                 </tr>
             </table>
         </asp:Panel>
       <cc1:modalpopupextender id="ModalPopupExtender3" runat="server" backgroundcssclass="FondoAplicacion"
               popupcontrolid="Panelc" targetcontrolid="hfModalcancelar"></cc1:modalpopupextender>

         
      
         <input id="hfModalcancelar" runat="server" type="hidden" />
        <ajaxToolkit:ModalPopupExtender ID="mpuprolTE" runat="server" PopupControlID="pnlrolTE" TargetControlID="hfcita" CancelControlID="BTNcerrar" BackgroundCssClass="FondoAplicacion">
                                                </ajaxToolkit:ModalPopupExtender>
       <asp:HiddenField runat="server" ID="hfcita" />
       <input id="tipomod" runat="server" type="hidden" />
        <asp:Panel runat="server" ID="pnlrolTE" CssClass="panelpp">
                <div class="example-modal">
                    <div class="modal-1">
                        <div class="modal-dialog">
                            <div class="modal-content" dir="ltr">
                                <div class="modal-header">
                                    <h4 class="modal-title">Modificación de Abono</h4>
                                </div>
                                <div class="modal-body">
                                <div class="form-body">
                                    <div class="input-group col-md-12">
                                        <span class="input-group-addon"><i class="fa fa-font"></i></span>
                                        <asp:TextBox ID="txtObservacionMod"  runat="server" CssClass="form-control" TextMode="MultiLine" Rows="3" placeholder="Motivo de cambio"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="input-group col-md-12">
                                       <asp:LinkButton ID="lkb1" Text="Guardar Cambios" CssClass="btn btn-block btn-info" runat="server" Font-Names="Calibri" Font-Size="16px"></asp:LinkButton>
                                </div>     
                                </div>
                                <div class="modal-footer">
					                <button id="BTNcerrar" type="button" class="btn btn-secondary" data-dismiss="modal">Cerrar</button>
					            </div>

                            </div>
                            <!-- /.modal-content -->
                        </div>
                        <!-- /.modal-dialog -->
                    </div>
                    <!-- /.modal -->
                </div>
                <!-- /.example-modal -->
            </asp:Panel>




   </div>
    </form>
    </center>
</body>
</html>
