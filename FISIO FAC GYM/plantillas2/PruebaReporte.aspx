<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Home.Master" CodeBehind="PruebaReporte.aspx.vb" Inherits="AgeMED.PruebaReporte" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .txtAgFecha, .input-group-addon {
            cursor: pointer;
        }

        .chart {
            width: 100%;
            min-height: 450px;
        }
    </style>
    <script type="text/javascript">
        
    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="content-header">
        <h1>Reportes
                <small>Generales</small>
        </h1>
        <ol class="breadcrumb">
            <li><a href="index"><i class="fa fa-home"></i>Inicio</a></li>
            <li class="active"><i class="fa fa-bar-chart"></i>Reportes</li>
        </ol>
    </section>

    <section class="content">
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title">Reportes</h3>
                    </div>
                    <div class="box-body">
                        <!--NOMBRE-->
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <div class="form-group">
                                    <label>Tipo de reporte:</label>
                                    <div class="input-group">
                                        <asp:DropDownList ID="DdConsulta" class="form-control input-lg" runat="server">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                            <div class="col-xs-12 col-sm-12 col-md-6 col-lg-6">
                                <div class="form-group">
                                    <div class="input-group col-md-12">
                                        <asp:DropDownList ID="DdMedico" runat="server" Visible="false"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-lg-6 col-md-6 col-sm-12 col-xs-12">
                                <div class="form-group">
                                    <label>Rango de fechas:</label>
                                    <div class="input-group">
                                        <div class="input-group-addon">
                                            <i class="fa fa-calendar fa-2x"></i>
                                        </div>
                                        <asp:TextBox runat="server" ID="Fechas" CssClass="form-control daterange input-lg"></asp:TextBox>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <asp:Button ID="BtnAceptar" CssClass="btn btn-primary btn-lg btn-block" runat="server" Text="Reporte" Visible="true" />
                            </div>
                        </div>
                        <hr>
                    </div>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-md-12">
                <div class="box box-info">
                    <div class="box-header with-border">
                        <h3 class="box-title" id="resultados_reporte">Resultados</h3>
                    </div>
                    <div class="box-body">
                        <!--NOMBRE-->
                        <div class="row">
                            <div class="col-xs-12 col-sm-12 col-md-12 col-lg-12">
                                <div class="table-responsive">
                                    <asp:GridView ID="GdReporte" runat="server" CssClass="tabla_reportes table table-striped"></asp:GridView>
                                </div>
                                <div class="text-center">
                                    <div id="total_pagado"></div>
                                    <div id="total_pendiente"></div>
                                </div>
                            </div>
                        </div>
                        <hr>
                        <div class="fixed">
                            <div class=" col-md-6">
                                <div id="barchart" class="chart hidden"></div>
                            </div>                           
                            <div class="col-md-6">
                                <div id="donutchart" class="chart" "></div>
                            </div>
                        </div>
                          <div class="fixed">                                                     
                            <div class="col-md-6">
                                <div id="donutchart3" class="chart" "></div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>     
        <asp:HiddenField ID="HdIngresos" runat="server" />
    </section>
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>    
    <script>
        
        $(function () {
            if ($('.tabla_reportes').length > 0) {
                $('html, body').animate({
                    scrollTop: $("#resultados_reporte").offset().top
                }, 2000);
            }
            $('.input-group').find('.fa-calendar').on('click', function () {
                $(this).parent().siblings('#<%=Fechas.ClientID%>').trigger('focus');
            });


            $('.daterange').daterangepicker({
                "locale": {
                    "format": "DD/MM/YYYY",
                    "separator": " - ",
                    "applyLabel": "Aceptar",
                    "cancelLabel": "Cancelar",
                    "fromLabel": "Desde",
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
                        "Augosto",
                        "Septiempre",
                        "Octubre",
                        "Noviembre",
                        "Diciembre"
                    ],
                    "firstDay": 1,
                }
            });
        });
        window.onload = function () {
            switch ($('#<%=DdConsulta.ClientID%>').val()) {
                case '1':
                    {
                        $('#barchart').addClass('hidden');
                        $("#total_pagado").empty;
                        $("#total_pendiente").empty;
                        $('#<%=DdConsulta.ClientID%>').empty;
                        break;
                    }
                case '2':
                    {
                        if ($('.tabla_reportes').length > 0) {
                            google.charts.load('current', { 'packages': ['bar'] });
                            google.charts.setOnLoadCallback(drawChart);
                            google.charts.load('current', { 'packages': ['corechart'] });
                            google.charts.setOnLoadCallback(drawChart2);
                            google.charts.load('current', { 'packages': ['corechart'] });
                            google.charts.setOnLoadCallback(drawChart3);
                            $('#barchart').removeClass('hidden');
                        }

                        $("#total_pagado").empty;
                        $("#total_pendiente").empty;
                        break;
                    }
                case '3':
                    {
                        if ($('.tabla_reportes').length > 0) {
                            var suma_pagado = 0;
                            var suma_pendiente = 0;
                            var valor;
                            var filas = $('table#<%= GdReporte.ClientID%>').find('tbody').find('tr');
                            for (var i = 0; i < filas.length; i++) {
                                valor = parseInt(numeral($(filas[i]).find('td:eq(3)').text().replace("$ ", "")).value(), 10);
                                if ($(filas[i]).find('td:eq(9)').text() == "Pagado") {
                                    suma_pagado += valor;
                                    console.log("suma_pagado = " + suma_pagado)
                                }
                                else if ($(filas[i]).find('td:eq(9)').text() == "Sin Pagar") {
                                    suma_pendiente += valor;
                                    console.log("suma_pendiente = " + suma_pendiente)
                                }
                            }
                            $('#barchart').addClass('hidden');
                            $("#total_pagado").empty;
                            $("#total_pendiente").empty;
                            $("#total_pagado").append('<h3>Total Pagados: <strong>' + numeral(suma_pagado).format('$ 0,0.00') + '</strong></h3>');
                            $("#total_pendiente").append('<h3>Total Sin pagar: <strong>' + numeral(suma_pendiente).format('$ 0,0.00') + '</strong></h3>');
                        }
                        break;
                    }
                case '5':
                    {
                        if ($('.tabla_reportes').length > 0) {
                            google.charts.load('current', { 'packages': ['bar'] });
                            google.charts.setOnLoadCallback(drawChart);
                            $('#barchart').removeClass('hidden');
                        }
                        $("#total_pagado").empty;
                        $("#total_pendiente").empty;
                        break;
                    }
            }


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

            function GetTable() {
                var array = [];
                var headers = [];
                var data = [];
                $('#<%= GdReporte.ClientID%> th').each(function (index, item) {
                    headers[index] = $(item).text();
                });
                data.push(headers);
                $('#<%= GdReporte.ClientID%> tr').has('td').each(function () {
                    var arrayItem = [];
                    $('td', $(this)).each(function (index, item) {
                        if (index != 0) {
                            arrayItem[index] = parseInt($(item).text(), 10);
                        }
                        else {
                            arrayItem[index] = $(item).text();
                        }

                    });
                    data.push(arrayItem);
                });
                console.log(data);
                return data;
            }

            function GetTable2() {
                var array = [];
                var headers = [];
                var data2 = [];
                $('#<%= GdReporte.ClientID%> th').each(function (index, item) {
                    if (index < 2) {
                        headers[index] = $(item).text();
                    }

                });
                data2.push(headers);
                $('#<%= GdReporte.ClientID%> tr').has('td').each(function () {
                    var arrayItem = [];
                    $('td', $(this)).each(function (index, item) {
                        if (index != 0 && index < 2) {
                            arrayItem[index] = parseInt($(item).text(), 10);
                        }
                        else if (index == 0) {
                            arrayItem[index] = $(item).text();
                        }

                    });
                    data2.push(arrayItem);
                });
                console.log(data2);               
                return data2;
            }
         

            function drawChart() {
                var data = google.visualization.arrayToDataTable(GetTable());
                var options = {
                    chart: {
                        title: 'Reporte',
                        subtitle: 'Historial de consultas',
                    },
                    bars: 'horizontal', // Required for Material Bar Charts.
                    vAxis: { format: 'decimal' },
                    height: 400,
                };
                var chart = new google.charts.Bar(document.getElementById('barchart'));
                chart.draw(data, options);
            }


            function drawChart2() {
                var data = google.visualization.arrayToDataTable(GetTable2());

                var options = {
                    title: 'Consultas Realizadas',
                    pieHole: 0.4,
                    responsive: true,
                };

                var chart = new google.visualization.PieChart(document.getElementById('donutchart'));
                chart.draw(data, options);
            }

            function drawChart3() {
                var data = google.visualization.arrayToDataTable(<% =HdIngresos.Value%>);

                var options = {
                    title: 'Ingresos',
                    pieHole: 0.4,
                    responsive: true,
                    colors: ['#e0440e', '#e6693e', '#ec8f6e', '#f3b49f', '#f6c7b6', '#f5c7b2', '#f3c8b7']
                };

                var chart = new google.visualization.PieChart(document.getElementById('donutchart3'));
                chart.draw(data, options);
            }
            $(window).resize(function () {
                drawChart();
                drawChart2();
            });
         
        }
      
        
</script>
</asp:Content>
