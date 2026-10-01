<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="Reportes.aspx.vb" Inherits="AgeMED.PruebaReporte" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
   
   
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="Label1" runat="server" Text="Reporte de pacientes agendados el dia de hoy"></asp:Label>
    <asp:DropDownList ID="DdMedico" runat="server"></asp:DropDownList>
      
    <asp:Label ID="De" runat="server" Text="Label"></asp:Label>
    <asp:Calendar ID="FechaDE" runat="server"></asp:Calendar>
    
    <div class="row">
        <div class="col-md-12">
            <div class="box box-info">
                <div class="box-header with-border">
                    <h3 class="box-title">Reportes</h3>
                </div>

                <div class="box-body">
                    <!--NOMBRE-->

                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
                            <div class="form-group">
                                <label>Tipo de reporte:</label>
                            </div>
                        </div>
                        
                        <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                            <div class="form-group">
                                <div class="input-group col-md-12">
                                    <asp:DropDownList ID="DDlocalidades" class="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
                            <div class="form-group">
                                <label>Rango de fechas:</label>

                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-4 col-lg-4">
                            <div class="form-group">
                                <div class="input-group date">
                                  <span class="input-group-addon">
                                    <i class="fa fa-calendar"></i>
                                  </span>
                                    <asp:TextBox runat="server" ID="FechaHasta" CssClass="datepicker" ReadOnly="true"></asp:TextBox>
                                </div>
                            </div>
                        </div>
                        <div class="col-xs-12 col-sm-12 col-md-2 col-lg-2">
                            <button type="button" class="btn btn-primary"><i class="fa fa-check-circle"></i> Reporte</button>
                        </div>
                    </div>
                    <hr>
                </div>
            </div>

           
        </div>
    </div>
   
    
    <div class="form-group">
               
              </div>
    <asp:Label ID="Hasta" runat="server" Text="Label"></asp:Label>
    <asp:GridView ID="GdReporte" runat="server"></asp:GridView>
    <asp:Button ID="BtnAceptar" runat="server" Text="Button" />
     <script>
         window.onload = function () {
             $("#<%=FechaHasta.ClientID%>").daterangepicker(
                 {
                 "locale": {
                     "format": "DD/MM/YYYY",
                     "separator": " - ",
                     "applyLabel": "Aceptar",
                     "cancelLabel": "Cancelar",
                     "fromLabel": "De",
                     "toLabel": "Hasta",
                     "customRangeLabel": "Custom",
                     "daysOfWeek": [
                         "Do",
                         "Lu",
                         "Ma",
                         "Mi",
                         "Ju",
                         "Vi",
                         "Sa"
                     ],
                     "monthNames": [
                         "Enero",
                         "Febrero",
                         "Marzo",
                         "Abril",
                         "Mayo",
                         "Junio",
                         "Julio",
                         "Agosto",
                         "Septiembre",
                         "Octobre",
                         "Noviembre",
                         "Deciembre"
                     ],
                     "firstDay": 1
                 },
             });
             

             var grid = document.getElementById('<%= GdReporte.ClientID%>');
             var tbody = grid.getElementsByTagName("tbody")[0]; //gets the first and only tbody
             var firstTr = tbody.getElementsByTagName("tr")[0]; //gets the first tr, hopefully contains the th's

             tbody.removeChild(firstTr); //remove tr's from table

             var newTh = document.createElement('thead'); //creates thead
             newTh.appendChild(firstTr); //puts ths in thead
             grid.insertBefore(newTh, tbody); //puts thead behore tbody
             $(document.getElementById('<%= GdReporte.ClientID%>')).DataTable({
                "language": {
                    "sProcessing": "Procesando...",
                    "sLengthMenu": "Mostrar _MENU_ registros",
                    "sZeroRecords": "No se encontraron resultados",
                    "sEmptyTable": "Ningún dato disponible en esta tabla",
                    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
                    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
                    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
                    "sInfoPostFix": "",
                    "sSearch": "Buscar:",
                    "sUrl": "",
                    "sInfoThousands": ",",
                    "sLoadingRecords": "Cargando...",
                    "oPaginate": {
                        "sFirst": "Primero",
                        "sLast": "Último",
                        "sNext": "Siguiente",
                        "sPrevious": "Anterior"
                    },
                    "oAria": {
                        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
                        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
                    }
                },
                "ordering": true,
                dom: 'Bfrtip',

                buttons: [
					{
					    extend: 'excel',
					    text: '<i class="fa fa-file-excel-o"></i> Excel',
					    className: 'btn btn-success'
					},
					{
					    extend: 'pdf',
					    text: '<i class="fa fa-file-pdf-o"></i> PDF',
					    className: 'btn btn-danger'
					},
					{
					    extend: 'print',
					    text: '<i class="fa fa-print"></i> Imprimir',
					    className: 'btn btn-primary'
					},
                ]
            });
        }
        </script>
</asp:Content>
