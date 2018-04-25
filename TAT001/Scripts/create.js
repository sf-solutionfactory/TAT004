$(document).ready(function () {

    //Validar que los labels esten activos
    //Información
    $("label[for='notas_txt']").addClass("active");
    //Nombre    
    if ($('#cli_name').val() != "") {
        $("label[for='cli_name']").addClass("active");
    }
    //Razón social
    if ($('#parvw').val() != "") {
        $("label[for='parvw']").addClass("active");
    }
    //Razón social
    if ($('#vkorg').val() != "") {
        $("label[for='vkorg']").addClass("active");
    }
    //Tax ID
    if ($('#stcd1').val() != "") {
        $("label[for='stcd1']").addClass("active");
    }
    //Canal
    if ($('#vtweg').val() != "") {
        $("label[for='vtweg']").addClass("active");
    }
    //Payer nombre
    if ($('#payer_nombre').val() != "") {
        $("label[for='payer_nombre']").addClass("active");
    }
    //Email nombre
    if ($('#payer_email').val() != "") {
        $("label[for='payer_email']").addClass("active");
    }
    //Soporte
    //Negociación
    if ($('#notas_soporte').val() != "") {
        $("label[for='notas_soporte']").addClass("active");
    }

    //Distribución    
    $('#table_dis').DataTable({
        "language": {
            "zeroRecords": "No hay registros",
            "infoEmpty": "Registros no disponibles",
            "decimal": ".",
            "thousands": ","
        },
        "paging": false,
        //        "ordering": false,
        "info": false,
        "searching": false,
        //"footerCallback": function (row, data, start, end, display) {
        //    var api = this.api(), data;

        //    // Remove the formatting to get integer data for summation
        //    var intVal = function (i) {
        //        return typeof i === 'string' ?
        //            i.replace(/[\$,]/g, '') * 1 :
        //            typeof i === 'number' ?
        //                i : 0;
        //    };

        //    // Total over all pages
        //    total = api
        //        .column(14)
        //        .data()
        //        .reduce(function (a, b) {
        //            return intVal(a) + intVal(b);
        //        }, 0);

        //    // Total over this page
        //    pageTotal = api
        //        .column(14, { page: 'current' })
        //        .data()
        //        .reduce(function (a, b) {
        //            return intVal(a) + intVal(b);
        //        }, 0);

        //    //Fixed 2
        //    var tc = parseFloat(total).toFixed(2);

        //    // Update footer
        //    $(api.column(14).footer()).html(
        //        //'$' + pageTotal + ' ( $' + total + ' total)'
        //        '$' + tc
        //    );
        //},//Termina el callback
        "columns": [
            {
                "className": 'id_row',
                "orderable": false,
                "defaultContent": ''

            },
            {
                "className": 'detail_row',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {
                "className": 'select_row',
                "orderable": false,
                "data": null,
                "defaultContent": ''
            },
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {},
            {}
            
        ]
    });

    $('#table_dis tbody').on('click', 'td.select_row', function () {
        var tr = $(this).closest('tr');
        $(tr).toggleClass('selected');
    });

    $('#delRow').click(function (e) {
        var t = $('#table_dis').DataTable();
        t.rows('.selected').remove().draw(false);
        updateFooter();
        event.returnValue = false;
        event.cancel = true;
    });

    //Mostrar los materiales (detalle) de la categoria 
    $('#table_dis tbody').on('click', 'td.detail_row', function () {
        var t = $('#table_dis').DataTable();
        var tr = $(this).closest('tr');
        var row = t.row(tr);        

        if (row.child.isShown()) {
            row.child.hide();
            tr.removeClass('details');
        }
        else {
            //Obtener el id de la categoría
            var index = t.row(tr).index();
            var catid = t.row(index).data()[0];
            row.child(format(catid)).show();
            tr.addClass('details');
        }
    });

    $('#addRow').on('click', function () {

        //Obtener el tipo de negociación
        var neg = $("#select_neg").val();

        if (neg != "") {
            //Monto
            if (neg == "M") {
                //Obtener la distribución
                var dis = $("#select_dis").val();
                if (dis != "") {
                    var t = $('#table_dis').DataTable();
                    //Distribución por categoría
                    if (dis == "C") {
                        //Obtener la categoría
                        var cat = $('#select_categoria').val();

                        if (cat != "") {                        
                            var opt = $("#select_categoria option:selected").text();
                            t.row.add([
                                cat + "", //col0
                                "", //col1
                                "", ////col2
                                "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">", //col3
                                "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "", //Material
                                opt + "",
                                opt + "",
                                "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "",
                                "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                                "",
                            ]).draw(false);
                        } else {
                            M.toast({ html: 'Seleccione una categoría' });
                        }

                    } else if (dis == "M") {
                        //Distribución por material
                        t.row.add([
                            "",
                            "",
                            "",
                            "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "<input class=\"input_oper input_material number\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "",
                            "",
                            "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "",
                            "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
                            "",
                        ]).draw(false);

                        $('#table_dis').css("font-size", "12px");
                        $('#table_dis').css("display", "table");
                        //$('#tfoot_dis').css("display", "table-footer-group");

                        //if ($('#select_dis').val() == "M") {

                            t.column(0).visible(false);
                            t.column(1).visible(false);
                        //}
                    }
                    updateFooter();
                } else {
                    M.toast({ html: 'Seleccione distribución' });
                }

            }
        } else {
            M.toast({ html: 'Seleccione negociación' });
        }

        event.returnValue = false;
        event.cancel = true;
        
    });

    $('#select_neg').change();
    $('#select_dis').change();
    

    $("#file_dis").change(function () {
        var filenum = $('#file_dis').get(0).files.length;
        if (filenum > 0) {
            var file = document.getElementById("file_dis").files[0];
            var filename = file.name;
            if (evaluarExt(filename)) {
                M.toast({ html: 'Uploading' + filename });
                loadExcel(file);
                updateFooter();
            } else {
                M.toast({ html: 'Tipo de archivo incorrecto: ' + filename });
            }
        } else {
            M.toast({ html: 'Seleccione un archivo' });
        }
    });

    //Temporalidad
    if ($('#monto_doc_md').val() != "") {
        $("label[for='monto_doc_md']").addClass("active");
    }
    
    $('#tabs').tabs();

    var elem = document.querySelectorAll('select');
    var instance = M.Select.init(elem, []);

    $('#tab_temp').on("click", function (e) {
        $('#gall_id').change();
        evalInfoTab(false, e);
    });

    $('#tab_soporte').on("click", function (e) {

        evalTempTab(false, e);

    });

    $('#tab_dis').on("click", function (e) {

        evalSoporteTab(false, e);

    });

    $('#tab_fin').on("click", function (e) {

        var res = evalDistribucionTab(true, e);        
        if (res) {

            //Activar el botón de guardar
            $("#btn_guardarh").removeClass("disabled");

            //Copiar el monto de distribución de la tabla footer al monto financiera
            var total_dis = $('#total_dis').text();
            var basei = convertI(total_dis);

            //Obtiene el id del tipo de negociación, default envía vacío
            var select_neg = $('#select_neg').val();
            //Validar el monto base vs monto tabla
            if (select_neg == "M") {
                //Tiene que tener una moneda
                //Obtener la moneda de distribución y de financiera
                var monedadis_id = $('#monedadis_id').val();
                var monedafin_id = $('#moneda_id').val();

                //Si las monedas son iguales, se pasa el monto
                if (monedadis_id == monedafin_id) {
                    $('#monto_doc_md').val(basei);
                    
                } else {
                    //Realizar conversión de monedas
                    var newMonto = cambioCurr(monedadis_id, monedafin_id, basei);
                    $('#monto_doc_md').val(newMonto);
                    
                }

            } else {
                //Si no es por monto solo se copia la cantidad
                $('#monto_doc_md').val(basei);
                
            }

            //Emular un focus out para actualizar los campos
            $('#monto_doc_md').focusout();
            
            $("label[for='monto_doc_md']").addClass("active");

            //Obtener los valores para asignar persupuesto
            //Obtener canal desc
            var canal = $('#vtweg').val();
            var canal = canal.split('-');
            canal[1] = $.trim(canal[1]);
            $('#p_vtweg').text(canal[1]);
            //Obtener cliente id
            var kunnr = $('#payer_id').val();
            //$('#cli_name').val();
            $('#p_kunnr').text(kunnr);
            
            
            asignarPresupuesto(kunnr);
            
        } else {
            M.toast({ html: 'Verificar valores en los campos de Distribución!' });
            e.preventDefault();
            e.stopPropagation();
            //var active = $('ul.tabs .active').attr('href');
            //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Distribucion_cont');
        }

        
    });

    //Financiera   
    $('#monto_doc_md').focusout(function (e) {

        //e.preventDefault(); //stops default action: submitting form
        //var msg = 'Enter';
        //M.toast({ html: msg })

        var monto_doc_md = $('#monto_doc_md').val();
        var is_num = $.isNumeric(monto_doc_md);
        var mt = parseFloat(monto_doc_md.replace(',', '')).toFixed(2);
        if (mt > 0 & is_num == true) {
            //Obtener la moneda en la lista
            //var MONEDA_ID = $('#moneda_id').val();
            $('#monto_doc_md').val(mt);
           
            //selectTcambio(MONEDA_ID, mt);
            var tipo_cambio = $('#tipo_cambio').val();
            var tc = parseFloat(tipo_cambio.replace(',', '')).toFixed(2);
            //Validar el monto en tipo de cambio
            var is_num2 = $.isNumeric(tipo_cambio);
            if (tc > 0 & is_num2 == true) {
                $('#tipo_cambio').val(tc);
                var monto = mt / tc;
                monto = parseFloat(monto).toFixed(2);
                $('#monto_doc_ml2').val(monto);
                $('#montos_doc_ml2').val(monto);
                $("label[for='montos_doc_ml2']").addClass("active");
            } else {
                $('#monto_doc_ml2').val(monto);
                $('#montos_doc_ml2').val(monto);
                $("label[for='montos_doc_ml2']").addClass("active");
                var msg = 'Tipo de cambio incorrecto';
                M.toast({ html: msg });
                e.preventDefault();
            }

        } else {
            $('#monto_doc_ml2').val(monto_doc_md);
            $('#montos_doc_ml2').val(monto_doc_md);
            $("label[for='montos_doc_ml2']").addClass("active");
            var msg = 'Monto incorrecto';
            M.toast({ html: msg });
            e.preventDefault();
        }

    });
    $('#tipo_cambio').focusout(function (e) {
        var tipo_cambio = $('#tipo_cambio').val();
        var is_num = $.isNumeric(tipo_cambio);
        var tc = parseFloat(tipo_cambio.replace(',', '')).toFixed(2);
        //Validar el monto en tipo de cambio
        if (tc > 0 & is_num == true) {
            //Validar el monto
            $('#tipo_cambio').val(tc)
            var monto_doc_md = $('#monto_doc_md').val();
            var mt = parseFloat(monto_doc_md.replace(',', '')).toFixed(2);
            var is_num2 = $.isNumeric(monto_doc_md);
            if (mt > 0 & is_num2 == true) {
                $('#monto_doc_md').val(mt);
                
                //Validar la moneda                    
                var moneda_id = $('#moneda_id').val();
                if (moneda_id != null && moneda_id != "") {
                    $('#monto_doc_ml2').val();
                    $('#montos_doc_ml2').val();

                    //Los valores son correctos, proceso para generar nuevo monto
                    var monto = mt / tc;
                    monto = parseFloat(monto).toFixed(2);
                    $('#monto_doc_ml2').val(monto);
                    $('#montos_doc_ml2').val(monto);
                    $("label[for='montos_doc_ml2']").addClass("active");

                } else {
                    $('#monto_doc_md').val();
                    
                    $('#monto_doc_ml2').val(monto);
                    $('#montos_doc_ml2').val(monto);
                    var msg = 'Moneda incorrecta';
                    M.toast({ html: msg })
                }

            } else {
                $('#monto_doc_md').val();
                
                $('#tipo_cambio').val("");
                $('#monto_doc_ml2').val(monto);
                $('#montos_doc_ml2').val(monto);
                $("label[for='montos_doc_ml2']").addClass("active");
                var msg = 'Monto incorrecto';
                M.toast({ html: msg });
                e.preventDefault();
            }

        } else {
            $('#monto_doc_ml2').val(monto);
            $('#montos_doc_ml2').val(monto);
            $("label[for='montos_doc_ml2']").addClass("active");
            var msg = 'Tipo de cambio incorrecto';
            M.toast({ html: msg });
            e.preventDefault();
        }
    });

    var monto_doc_md = $('#monto_doc_md').val();
    var is_num = $.isNumeric(monto_doc_md);
    var mt = parseFloat(monto_doc_md.replace(',', '')).toFixed(2);
    if (mt > 0 & is_num == true) {
        //Obtener la moneda en la lista
        //var MONEDA_ID = $('#moneda_id').val();
        $('#monto_doc_md').val(mt);
        
        //selectTcambio(MONEDA_ID, mt);
        var tipo_cambio = $('#tipo_cambio').val();
        var tc = parseFloat(tipo_cambio.replace(',', '')).toFixed(2);
        //Validar el monto en tipo de cambio
        var is_num2 = $.isNumeric(tipo_cambio);
        if (tc > 0 & is_num2 == true) {
            $('#tipo_cambio').val(tc);
            var monto = mt / tc;
            monto = parseFloat(monto).toFixed(2);
            $('#monto_doc_ml2').val(monto);
            $('#montos_doc_ml2').val(monto);
            $("label[for='montos_doc_ml2']").addClass("active");
        } else {
            $('#monto_doc_ml2').val(monto);
            $('#montos_doc_ml2').val(monto);
            $("label[for='montos_doc_ml2']").addClass("active");
        }

    } else {
        $('#monto_doc_ml2').val(monto_doc_md);
        $('#montos_doc_ml2').val(monto_doc_md);
        $("label[for='montos_doc_ml2']").addClass("active");
        //var msg = 'Monto incorrecto';
        //M.toast({ html: msg });
        //e.preventDefault();
    }


    //Termina financiera

    //$('#cargar').click(function () {
    //    M.toast({ html: 'Load' })
    //    loadExcel();
    //});
    //Enter en el monto
    //$('#monto_doc_md').keypress(keypressHandler);

    $('#btn_guardarh').on("click", function (e) {
        var msg = 'Verificar valores en los campos de ';
        var res = true;
        //Evaluar TabInfo values
        var InfoTab = evalInfoTab(true, e);
        if (!InfoTab) {
            msg += 'Información';
            res = InfoTab;
        }
        //Evaluar TempTab values
        var TempTab = evalTempTab(true, e);
        if (!TempTab) {
            msg += ' ,Temporalidad';
            res = TempTab;
        }
        //Evaluar SoporteTab values
        var SoporteTab = evalSoporteTab(true, e);
        if (!SoporteTab) {
            msg += ' ,Soporte';
            res = SoporteTab;
        }

        //Evaluar SoporteTab values
        var FinancieraTab = evalFinancieraTab(true, e);
        if (!FinancieraTab) {
            msg += ' ,Financiera';
            res = FinancieraTab;
        }

        msg += '!';
        if (res) {
            //loadFilesf();
            //Provisional
            var tipo_cambio = $('#tipo_cambio').val();
            //var iNum = parseFloat(tipo_cambio.replace(',', '.')).toFixed(2);
            var iNum = parseFloat(tipo_cambio.replace(',', ''));

            if (iNum > 0) {
                //var num = "" + iNum;
                //num = num.replace('.', ',');
                //var numexp = num;//* 60000000000;
                //$('#tipo_cambio').val(numexp);
            } else {
                $('#tipo_cambio').val(0);
            }
            var tipo_cambio = $('#monto_doc_ml2').val();
            //var iNum2 = parseFloat(tipo_cambio.replace(',', '.')).toFixed(2);
            var iNum2 = parseFloat(tipo_cambio.replace(',', ''));
            //var iNum2 = parseFloat(tipo_cambio.replace('.', ','));
            if (iNum2 > 0) {
                //var nums = "" + iNum2;
                //nums = nums.replace('.', ',');
                //var numexp2 = nums;// * 60000000000;
                //$('#monto_doc_ml2').val(numexp2);
            } else {
                $('#monto_doc_ml2').val(0);
            }

            //Monto
            var monto = $('#monto_doc_md').val();
            //var numm = parseFloat(monto.replace(',', '.')).toFixed(2);   
            var numm = parseFloat(monto.replace(',', ''));
            if (numm > 0) {
                $('#MONTO_DOC_MD').val(numm);
            } else {   
                $('#MONTO_DOC_MD').val(0);
                $('#monto_doc_md').val(0);
            }
            //Termina provisional
            $('#btn_guardar').click();
        } else {
            M.toast({ html: msg })
        }

    });

});

function asignarPresupuesto(kunnr) {

    $.ajax({
        type: "POST",
        url: 'getPresupuesto',
        dataType: "json",
        data: { "kunnr": kunnr },

        success: function (data) {

            if (data !== null || data !== "") {
                $('#p_canal').text(data.P_CANAL);
                $('#p_banner').text(data.P_BANNER);
                $('#pc_c').text(data.PC_C);
                $('#pc_a').text(data.PC_A);
                $('#pc_p').text(data.PC_P);
                $('#pc_t').text(data.PC_T);
            
            }

        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            M.toast({ html: httpStatusMessage });
        },
        async: false
    });

}

$('body').on('focusout', '.input_oper', function () {
    var t = $('#table_dis').DataTable();
    var tr = $(this).closest('tr'); //Obtener el row 


    updateTotalRow(t, tr);

    //Validar si el focusout fue en la columna de material
    if ($(this).hasClass("input_material")) {
        //Validar el material
        var mat = $(this).val();
        var val = valMaterial(mat);
        var index = getIndex();

        if (val.ID == null || val.ID == "") {
            tr.find('td').eq((5 + index)).addClass("errorMaterial");
        } else if (val.ID == mat) {
            
            selectMaterial(val.ID, val.MAKTX, tr);

        } else {
            tr.find('td').eq((5 + index)).addClass("errorMaterial");
        }

    }

});

//Validar el patrón para dos decimales en los campos editables de la tabla
$('body').on('keypress keydown', '.input_oper', function () {

    ////Si está escribiendo en campos de fecha, aceptar numeros y diagonal
    //if ($(this).hasClass("format_date")) {


    //} else if ($(this).hasClass("number")) {
    //    //Si es material, acepta puros numeros
    //} else if ($(this).hasClass("numberd")) {
    //    //Si es cantidad, acepta numeros y punto
    //    $(this).val($(this).val().replace(/[^0-9\.]/g, ''));
    //    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
    //        event.preventDefault();
    //    }  
    //}

});

//Variables globales
var detail = "";
var montocambio = 0;
var categoriamaterial = "";
var materialVal = "";

function updateTotalRow(t, tr) {

    //Add index
    //Se tiene que jugar con los index porque las columnas (ocultas) en vista son diferentes a las del plugin
    //Obtener la distribución
    var index = getIndex();    

    //Multiplicar costo unitario % por apoyo(dividirlo entre 100)
    //Columnas 8 * 9 res 10
    //Categoría es 7 * 8 = 9  --> -1
    //Material es 6 * 7 = 8   --> -2

    var col8 = tr.find("td:eq(" + (8 + index) + ") input").val();
    var col9 = tr.find("td:eq(" + (9 + index) + ") input").val();

    col9 = convertP(col9);

    if ($.isNumeric(col9)) {
        col9 = col9 / 100;
    }

    var col10 = col8 * col9;
    //Apoyo por pieza
    //Modificar el input
    tr.find("td:eq(" + (10 + index) + ") input").val(col10.toFixed(2));

    //Costo con apoyo
    var col11 = col8 - col10;
    //col11 = col11.toFixed(2);
    tr.find("td:eq(" + (11 + index) + ")").text(col11.toFixed(2));

    //Estimado apoyo
    var col13 = tr.find("td:eq(" + (13 + index) + ") input").val();
    var col14 = col10 * col13;
    //col14 = col14.toFixed(2);
    tr.find("td:eq(" + (14 + index) + ")").text("$" + col14.toFixed(2));
    
    updateFooter();
}

function updateTable() {
    var t = $('#table_dis').DataTable();
    $('#table_dis > tbody  > tr').each(function () {
                
        updateTotalRow(t, $(this));
        
    });

    updateFooter();

}

function resetFooter() {
    $('#total_dis').text("$0");
}

function getIndex() {
    var index = 0;
    var dis = $("#select_dis").val();
    if (dis != "") {
        var t = $('#table_dis').DataTable();
        //Distribución por categoría
        if (dis == "C") {
            index = -1;
        } else if (dis == "M") {
            //Distribución por material
            index = -2;
        }
    }

    return index;
}


function updateFooter() {
    resetFooter();
    var index = getIndex();
    coltotal = (14 + index);

    var t = $('#table_dis').DataTable();
    var total = 0;

    $('#table_dis').find("tr").each(function (index) {
        var col4 = $(this).find("td:eq(" + coltotal+")").text();

        col4 = convertI(col4);

        if ($.isNumeric(col4)) {
            total += col4;            
        }

    });

    total = total.toFixed(2);

    $('#total_dis').text("$" + total);
 }

function convertI(i) {
    return typeof i === 'string' ?
        i.replace(/[\$,]/g, '') * 1 :
        typeof i === 'number' ?
            i : 0;
};

function convertP(i) {
    return typeof i === 'string' ?
        i.replace(/[\$,]/g, '') * 1 :
        typeof i === 'number' ?
            i : 0;
};


function format(catid) {

    detail = "";
    var id = parseInt(catid)
    if (catid != "") {

        $.ajax({
            type: "POST",
            url: 'selectMatCat',
            data: { "catid": id },

            success: function (data) {

                if (data !== null || data !== "") {
                   var detaill = '<table class=\"display\" style=\"width:100%\">' +
                        '<tbody>' +
                        '<tr>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td>Tiger</td>' +
                        '<td>Nixon</td>' +
                        '<td>System Architect</td>' +
                        '<td>Edinburgh</td>' +
                        '<td>$320,800</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '</tr>' +
                        '<tr>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td></td>' +
                        '<td>Garrett</td>' +
                        '<td>Winters</td>' +
                        '<td>Accountant</td>' +
                        '<td>Tokyo</td>' +
                        '<td>$170,250</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '<td>Tiger</td>' +
                        '</tr>' +
                        '</tbody>' +
                        '</table>';
                   useReturnData(detaill);
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: msg });
            },
            async: false
        });
    } 

    return detail;
}

function useReturnData(data) {
    detail = data;
};

function evaluarExt(filename) {

    var exts = ['xls', 'xlsx'];
    // split file name at dot
    var get_ext = filename.split('.');
    // reverse name to check extension
    get_ext = get_ext.reverse();
    // check file type is valid as given in 'exts' array
    if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
        return true;
    } else {
        return false;
    }
}

function loadFilesf() {
    var files = $('.file_soporte');
    var message = "";

    for (var i = 0; i < files.length; i++) {
        //var file = $(files[i]).get(0).files;
        var className = $(files[i]).attr("class");
        //Valida si el archivo es obligatorio
        if (className.indexOf('nec') >= 0) {
            //Validar archivo en archivo obligatorio
            var nfile = $(files[i]).get(0).files.length;
            if (!nfile > 0) {
                var lbltext = $(files[i]).closest('td').prev().children().eq(0).html();
                //var parenttd = $(files[i]).closest('td').prev().children().eq(0).html();
                //var sitd = $(parenttd).prev().children().eq(0).html();
                //var labeltext = $(sitd).children().eq(0).html();
                //M.toast({ html: 'Error! Archivo Obligatorio: ' + lbltext });
                message = 'Error! Archivo Obligatorio: ' + lbltext;
                break;
            }
        }
    }

    if (message == "") {
        loadFiles(files)
    } else {
        M.toast({ html: message });
    }

}

function loadExcel(file) {

    var formData = new FormData();

    formData.append("FileUpload", file);

    var table = $('#table_dis').DataTable();
    table.clear().draw();
    $.ajax({
        type: "POST",
        url: 'LoadExcel',
        data: formData,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {

            if (data !== null || data !== "") {
                var index = getIndex();
                $.each(data, function (i, dataj) {

                    var date_de = new Date(parseInt(dataj.VIGENCIA_DE.substr(6)));
                    var date_al = new Date(parseInt(dataj.VIGENCIA_AL.substr(6)));
                    var addedRow = table.row.add([
                                        dataj.POS,
                                        "",
                                        "",
                                        "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + date_de.getDate() + "/" + (date_de.getMonth() + 1) + "/" + date_de.getFullYear() + "\">",
                                        "<input class=\"input_oper format_date\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + date_al.getDate() + "/" + (date_al.getMonth() + 1) + "/" + date_al.getFullYear() + "\">",
                                        "<input class=\"input_oper input_material number\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.MATNR + "\">",
                                        dataj.MATKL,
                                        dataj.DESC,                        
                                        "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.MONTO + "\">",                     
                                        "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.PORC_APOYO + "\">",                        
                                        "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.MONTO_APOYO + "\">",                     
                                        dataj.MONTOC_APOYO,                        
                                        "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.PRECIO_SUG + "\">",                
                                        "<input class=\"input_oper numberd\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + dataj.VOLUMEN_EST + "\">",                      
                                        dataj.PORC_APOYOEST
                    ]).draw(false).node();

                    if (dataj.ACTIVO == false) {
                        $(addedRow).find('td').eq((index + 5)).addClass("errorMaterial");
                    }

                }); //Fin de for
                $('#table_dis').css("font-size", "12px");
                $('#table_dis').css("display", "table");  
                $('#tfoot_dis').css("display", "table-footer-group");

                if ($('#select_dis').val() == "M") {
                    
                    table.column(0).visible(false);
                    table.column(1).visible(false);
                }

                updateTable();
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            alert("Request couldn't be processed. Please try again later. the reason        " + xhr.status + " : " + httpStatusMessage + " : " + customErrorMessage);
        },
        async: false
    });

    //Actualizar los valores en la tabla
    updateTable();

}

function loadFile(f_carta) {//, f_contratos, f_factura, f_jbp) {

    var formData = new FormData();

    formData.append("f_carta", f_carta);


    $.ajax({
        type: "POST",
        url: 'saveFiles',
        data: formData,
        //dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {

            if (data !== null || data !== "") {
                alert("success" + data);

            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            alert("Request couldn't be processed. Please try again later. the reason        " + xhr.status + " : " + httpStatusMessage + " : " + customErrorMessage);
        },
        async: false
    });

}

function loadFiles(files) {

    var formData = new FormData();

    var count = 1;
    for (var i = 0; i < files.length; i++) {
        var file = $(files[i]).get(0);
        if ($(files[i]).get(0).files.length > 0) {
            formData.append(file.files[0].name, file.files[0]);
            count++;
        }
    }

    $.ajax({
        type: "POST",
        url: 'saveFiles',
        data: formData,
        //dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {

            if (data !== null || data !== "") {
                alert("success" + data);

            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            alert("Request couldn't be processed. Please try again later. the reason        " + xhr.status + " : " + httpStatusMessage + " : " + customErrorMessage);
        },
        async: false
    });

}

//Funciones de evaluación
function evalInfoTab(ret, e) {
    var res = true;
    var msg = "";

    if (evaluarInfoTab()) {
        msg = 'siguiente pestaña!';
    } else {
        msg = 'Verificar valores en los campos de Información!';
        res = false;
    }
    //Email
    var payer_email = $('#payer_email').val();
    
    if (!validateEmail(payer_email)) {
        msg = 'Introduzca un email válido!';
        res=  false;
    }

    if (ret == true) {
        return res;
    } else {
        if (!res) {
            M.toast({ html: msg });
            e.preventDefault();
            e.stopPropagation();
            //var active = $('ul.tabs .active').attr('href');
            //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Informacion_cont');
        }
        return "";
    }
}

function evalTempTab(ret, e) {
    var res = true;
    var msg = "";

    if (evaluarTempTab()) {
        msg = 'siguiente pestaña!';
    } else {
        msg = 'Verificar valores en los campos de Temporalidad!';
        res = false;

    }

    if (ret == true) {
        return res;
    } else {
        if (!res) {
            M.toast({ html: msg });
            e.preventDefault();
            e.stopPropagation();
            //    //var active = $('ul.tabs .active').attr('href');
            //    //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Temporalidad_cont');
        }
        return "";
    }

}

function evalSoporteTab(ret, e) {
    var res = true;
    var msg = "";

    if (evaluarSoporteTab()) {
        msg = 'siguiente pestaña!';
    } else {
        msg = 'Verificar valores en los campos de Soporte!';
        res = false;
    }

    if (ret == true) {
        return res;
    } else {
        if (!res) {
            M.toast({ html: msg });
            e.preventDefault();
            e.stopPropagation();
            //var active = $('ul.tabs .active').attr('href');
            //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Soporte_cont');
        }
        return "";
    }
}

function evalDistribucionTab(ret, e) {
    var res = true;
    var msg = "";

    if (evaluarDisTab()) {
        msg = 'siguiente pestaña!';
    } else {
        msg = 'Verificar valores en los campos de Distribución!';
        res = false;
    }

    if (ret == true) {
        return res;
    } else {
        if (!res) {
            M.toast({ html: msg });
            e.preventDefault();
            e.stopPropagation();
            //var active = $('ul.tabs .active').attr('href');
            //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Distribucion_cont');
        }
        return "";
    }
}

function evalFinancieraTab(ret, e) {
    var res = true;
    var msg = "";

    if (evaluarFinancieraTab()) {
        msg = 'siguiente pestaña!';
    } else {
        msg = 'Verificar valores en los campos de Financiera!';
        res = false;
    }

    if (ret == true) {
        return res;
    } else {
        if (!res) {
            M.toast({ html: msg });
            e.preventDefault();
            e.stopPropagation();
            //var active = $('ul.tabs .active').attr('href');
            //$('ul.tabs').tabs('select_tab', active);
            var ell = document.getElementById("tabs");
            var instances = M.Tabs.getInstance(ell);
            instances.select('Financiera_cont');
        }
        return "";
    }
}

//Evaluar los elementos de tab_info
function evaluarInfoTab() {

    var res = true;

    //Obtiene el id de la lista id solicitud, default envía vacío
    var tsol_id = $('#tsol_id').val();

    if (!evaluarVal(tsol_id)) {
        return false;
    }

    //Obtiene el id de la lista id clasificación, default envía vacío
    var tall_id = $('#tall_id').val();

    if (!evaluarVal(tall_id)) {
        return false;
    }

    //Sociedad
    var sociedad_id = $('#sociedad_id').val();

    if (!evaluarVal(sociedad_id)) {
        return false;
    }

    //País
    var pais_id = $('#pais_id').val();

    if (!evaluarVal(pais_id)) {
        return false;
    }

    //Estado
    var state_id = $('#state_id').val();

    if (!evaluarVal(state_id)) {
        return false;
    }

    //Ciudad
    var city_id = $('#city_id').val();

    if (!evaluarVal(city_id)) {
        return false;
    }

    //Fecha
    var fechad = $('#fechad').val();

    if (!evaluarVal(fechad)) {
        return false;
    }

    //Periodo
    var periodo = $('#periodo').val();

    if (!evaluarVal(periodo)) {
        return false;
    }

    //Ejercicio
    var ejercicio = $('#ejercicio').val();

    if (!evaluarVal(ejercicio)) {
        return false;
    }
    //Concepto
    var concepto = $('#concepto').val();

    if (!evaluarVal(concepto)) {
        return false;
    }

    //Obtiene el id de la lista id cliente, default envía vacío
    var payer_id = $('#payer_id').val();

    if (!evaluarVal(payer_id)) {
        return false;
    }

    //Sociedad
    var vkorg = $('#vkorg').val();

    if (!evaluarVal(vkorg)) {
        return false;
    }

    //Taxt ID
    var stcd1 = $('#stcd1').val();

    if (!evaluarVal(stcd1)) {
        return false;
    }

    //Canal
    var vtweg = $('#vtweg').val();

    if (!evaluarVal(vtweg)) {
        return false;
    }

    //Nombre de la persona
    var payer_nombre = $('#payer_nombre').val();

    if (!evaluarVal(payer_nombre)) {
        return false;
    }

    //Email
    var payer_email = $('#payer_email').val();

    if (!evaluarVal(payer_email)) {
        return false;
    }

    return res;
}
function validateEmail(email) {
    var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return re.test(String(email).toLowerCase());
}
//Evaluar los elementos de tab_temp
function evaluarTempTab() {

    var res = true;

    //Obtiene el id de la lista id solicitud, default envía vacío
    var fechai_vig = $('#fechai_vig').val();

    if (!evaluarVal(fechai_vig)) {
        return false;
    }

    //Obtiene el id de la lista id clasificación, default envía vacío
    var fechaf_vig = $('#fechaf_vig').val();

    if (!evaluarVal(fechaf_vig)) {
        return false;
    }

    if (res) {
        var res = validar_fechas(fechai_vig, fechaf_vig);
    }

    return res;
}

function evaluarSoporteTab() {
    //var res = true;

    //var filecarta = "#file_carta";

    //if (!evaluarFile(filecarta)) {
    //    return false;
    //}

    //return res;

    return evaluarFiles();
}

function evaluarDisTab() {

    var res = true;

    //Obtiene el id del tipo de negociación, default envía vacío
    var select_neg = $('#select_neg').val();

    if (!evaluarVal(select_neg)) {
        return false;
    }

    //Obtiene el id de la lista distribución, default envía vacío
    var select_dis = $('#select_dis').val();

    if (!evaluarVal(select_dis)) {
        return false;
    }

    if (res) {
        //Validar los montos tipo de negociación monto
        //Validar el monto base vs monto tabla
        if (select_neg == "M") {
            var monedadis_id = $('#monedadis_id').val();
            var monto_dis = $('#monto_dis').val();
            var total_dis = $('#total_dis').text();

            if ((monto_dis != "" & total_dis != "") & monedadis_id != "") {

                //Base, monto footer
                var res = validar_montos(monto_dis, total_dis);
            } else {
                return false;
            }
            //Validar el porcentaje apoyo monto
        } else if (select_neg == "P"){

        }        
    } 
    return res;
}

function evaluarFinancieraTab() {

    var res = true;

    //Evaluar el monto
    var monto_doc_md = $('#monto_doc_md').val();

    if (!evaluarValInt(monto_doc_md)) {
        return false;
    }

    //Obtiene el id de la lista id clasificación, default envía vacío
    var moneda_id = $('#moneda_id').val();

    if (!evaluarVal(moneda_id)) {
        return false;
    }

    //Obtener el tipo de cambio
    var tipo_cambio = $('#tipo_cambio').val();

    if (!evaluarValInt(tipo_cambio)) {
        return false;
    }

    //Monto en dolares
    var tipo_cambio = $('#montos_doc_ml2').val();

    if (!evaluarValInt(tipo_cambio)) {
        return false;
    }

    return res;

}

function evaluarFile(id) {
    var filenum = $(id).get(0).files.length;

    if (filenum > 0) {
        var file = document.getElementById(id).files[0];
        var filename = file.name;
        return true;
        //if (evaluarExt(filename)) {
        //    M.toast({ html: 'Uploading' + filename });
        //    loadExcel(file);
        //} else {
        //    M.toast({ html: 'Tipo de archivo incorrecto: ' + filename });
        //}
    } else {
        //M.toast({ html: 'Seleccione un archivo' });
        return false;
    }
}

function evaluarFiles() {
    var files = $('.file_soporte');
    var message = "";

    for (var i = 0; i < files.length; i++) {
        //var file = $(files[i]).get(0).files;
        var className = $(files[i]).attr("class");
        //Valida si el archivo es obligatorio
        if (className.indexOf('nec') >= 0) {
            //Validar archivo en archivo obligatorio
            var nfile = $(files[i]).get(0).files.length;
            if (!nfile > 0) {
                var lbltext = $(files[i]).closest('td').prev().children().eq(0).html();
                //var parenttd = $(files[i]).closest('td').prev().children().eq(0).html();
                //var sitd = $(parenttd).prev().children().eq(0).html();
                //var labeltext = $(sitd).children().eq(0).html();
                //M.toast({ html: 'Error! Archivo Obligatorio: ' + lbltext });
                message = 'Error! Archivo Obligatorio: ' + lbltext;
                break;
            }
        }

        //Validar tamaño y extensión
        var file = $(files[i]).get(0).files;
        if (file.length > 0) {
            var sizefile = file[0].size;
            if (sizefile > 20971520) {
                var lbltext = $(files[i]).closest('td').prev().children().eq(0).html();
                message = 'Error! Tamaño máximo del archivo 20 M --> Archivo ' + lbltext + " sobrepasa el tamaño";
                break;
            }

            var namefile = file[0].name;
            if (!evaluarExtSoporte(namefile)) {
                var lbltext = $(files[i]).closest('td').prev().children().eq(0).html();
                message = "Error! Tipos de archivos aceptados 'xlsx', 'doc', 'pdf', 'png', 'msg', 'zip', 'jpg', 'docs' --> Archivo " + lbltext + " no es compatible";
                break;
            }
        }

    }

    if (message == "") {
        return true;
    } else {
        return false;
    }
}

//function evaluarExt(filename) {
function evaluarExtSoporte(filename) {

    var exts = ['xlsx', 'doc', 'pdf', 'png', 'msg', 'zip', 'jpg', 'docs'];
    // split file name at dot
    var get_ext = filename.split('.');
    // reverse name to check extension
    get_ext = get_ext.reverse();
    // check file type is valid as given in 'exts' array
    if ($.inArray(get_ext[0].toLowerCase(), exts) > -1) {
        return true;
    } else {
        return false;
    }
}

function evaluarVal(v) {
    if (v != null && v != "") {
        return true;
    } else {
        return false
    }
}

function evaluarValInt(v) {

    if (v != null && v != "") {
        var is_num = $.isNumeric(v);
        var iNum = parseFloat(v.replace(',', '.'))
        if (iNum > 0 & is_num == true) {
            return true;
        } else {
            return false;
        }
    } else {
        return false
    }
}
function selectTall(valu) {
    if (valu != "") {
        $("#tall_id").empty();
        $.ajax({
            type: "POST",
            url: 'SelectTall',
            data: { "id": valu },
            dataType: "json",
            success: function (data) {

                if (data !== null || data !== "") {
                    //$('<option>', {
                    //    text: "--Seleccione--"
                    //}).html("--Seleccione--").appendTo($("#tall_id"));
                    //$.each(data, function (i, optiondata) {
                    //    $('<option>', {
                    //        value: optiondata.ID,
                    //        text: optiondata.TEXT
                    //    }).html(optiondata.NAME).appendTo($("#tall_id"));
                    //});

                    //var elem = document.getElementById('tall_id');
                    //var instance = M.Select.init(elem, []);
                    $("#tall_id").val(data[0].ID);
                }
            },
            error: function (data) {
                alert("Request couldn't be processed. Please try again later. the reason        " + data);
            },
            async: false
        });
    }
}

function selectDis(val) {    
    resetFooter();
    if (val == "M") {//Monto
        $('#div_apoyobase').css("display", "none");
        $('#div_montobase').css("display", "inherit");
    } else if (val == "P") {//Porcentaje
        M.toast({ html: '¿Desea realizar esta solicitud por porcentaje?' });
        $('#div_montobase').css("display", "none");
        $('#div_apoyobase').css("display", "inherit");
    } else {
        $('#div_montobase').css("display", "none");
        $('#div_apoyobase').css("display", "none");
    }  
    var select_dis = $('#select_dis').val();
    $('#select_dis').val(select_dis).change();
}

function selectMonto(val) {    

    //Siempre inicializar la tabla
    var ta = $('#table_dis').DataTable();
    ta.clear().draw();

    //Obtener la negociación
    var select_neg = $('#select_neg').val();

    //Desactivar el panel de monto
    if (val == "" || select_neg == "") {
        $('#div_montobase').css("display", "none"); 
        $('#div_apoyobase').css("display", "none"); 
        $('#cargar_excel').css("display", "none");
        $('#select_categoria').css("display", "none");
        $('.div_categoria').css("display", "none");     
        $('#table_dis').css("display", "none");
        $('#div_btns_row').css("display", "none");  
        ta.column(0).visible(false);
        ta.column(1).visible(false);
    } else {
    //Activar el panel de monto dependiendo del tipo de negociación
        //$('#div_montobase').css("display", "inherit");
        $('#div_btns_row').css("display", "inherit");
        
        if (select_neg == "M") {//Monto
            $('#div_apoyobase').css("display", "none");
            $('#div_montobase').css("display", "inherit");             
        } else if (select_neg == "P") {//Porcentaje
            $('#div_montobase').css("display", "none");
            $('#div_apoyobase').css("display", "inherit");            
        } else {
            $('#div_montobase').css("display", "none");
            $('#div_apoyobase').css("display", "none"); 
        }
    }
    if (select_neg != "") {
        //Monto
        if (val == "M") {
            $('#cargar_excel').css("display", "inherit");
            $('#select_categoria').css("display", "none");
            $('.div_categoria').css("display", "none");
            ta.column(0).visible(false);
            ta.column(1).visible(false);
        }

        //Categoría
        if (val == "C") {
            $('#cargar_excel').css("display", "none");
            $('.div_categoria').css("display", "inline-block");
            //Mostrar el encabezado de la tabla               
            $('#table_dis').css("font-size", "12px");
            $('#table_dis').css("display", "table");
            ta.column(0).visible(false);
            ta.column(1).visible(true);
        }

        resetFooter();
    } else {
        M.toast({ html: 'Seleccione Negociación' });
    }
}

function selectCity(valu) {
    if (valu != "") {
        $("#city_id").empty();
        $.ajax({
            type: "POST",
            url: 'SelectCity',
            data: { "id": valu },
            dataType: "json",
            success: function (data) {

                if (data !== null || data !== "") {
                    $('<option>', {
                        text: "--Seleccione--"
                    }).html("--Seleccione--").appendTo($("#city_id"));
                    $.each(data, function (i, optiondata) {
                        $('<option>', {
                            value: optiondata.ID,
                            text: optiondata.NAME
                        }).html(optiondata.NAME).appendTo($("#city_id"));
                    });

                    $('#city_id').formSelect();
                }
            },
            error: function (data) {
                alert("Request couldn't be processed. Please try again later. the reason        " + data);
            },
            async: false
        });
    }

}
function asignCity(valu) {
    if (valu != "") {
        var iNum = parseInt(valu);
        $('#citys_id').val(iNum);
    }
}


function selectCliente(valu) {
    if (valu != "") {
        $.ajax({
            type: "POST",
            url: 'SelectCliente',
            data: { "kunnr": valu },

            success: function (data) {

                if (data !== null || data !== "") {
                    $('#cli_name').val(data.NAME1);
                    $("label[for='cli_name']").addClass("active");
                    $('#vkorg').val(data.VKORG).focus();
                    $("label[for='vkorg']").addClass("active");
                    $('#parvw').val(data.PARVW).focus();
                    $("label[for='parvw']").addClass("active");
                    $('#stcd1').val(data.STCD1);
                    $("label[for='stcd1']").addClass("active");
                    $('#vtweg').val(data.VTWEG);
                    $("label[for='vtweg']").addClass("active");
                    $('#payer_nombre').val(data.PAYER_NOMBRE);
                    $("label[for='payer_nombre']").addClass("active");
                    $('#payer_email').val(data.PAYER_EMAIL);
                    $("label[for='payer_email']").addClass("active");
                }

            },
            error: function (data) {
                alert("Request couldn't be processed. Please try again later. the reason        " + data);
            },
            async: false
        });
    }

}

function selectMoneda(valu) {
    $('#monto_doc_ml2').val("");
    $('#montos_doc_ml2').val("");
    $('#tipo_cambio').val("");
    $('#monedas_id').val("");

    if (valu != "") {
        var monto_doc_md = $('#monto_doc_md').val()
        var mt = parseFloat(monto_doc_md.replace(',', '.'))
        if (mt > 0) {

            $('#monedas_id').val(valu);

            $.ajax({
                type: "POST",
                url: 'SelectTcambio',
                data: { "fcurr": valu },

                success: function (data) {

                    if (data !== null || data !== "") {
                        var iNum = parseFloat(data.replace(',', '.')).toFixed(2);
                        if (iNum > 0) {

                            $('#tipo_cambio').val(iNum);

                            var monto_doc_md = $('#monto_doc_md').val()

                            if (monto_doc_md > 0) {
                                //Obtener la moneda en la lista
                                var MONEDA_ID = $('#moneda_id').val();

                                selectTcambio(MONEDA_ID, monto_doc_md);

                            }
                        } else {
                            M.toast({ html: data });
                        }
                    }

                },
                error: function (data) {
                    alert("Error tipo de cambio        " + data);
                },
                async: false
            });

        } else {
            var msg = 'Monto incorrecto';
            M.toast({ html: msg })
        }
    }

}

function selectTcambio(MONEDA_ID, monto_doc_md) {
    $('#monto_doc_ml2').val();
    $('#montos_doc_ml2').val();

    if (MONEDA_ID != "") {

        $.ajax({
            type: "POST",
            url: 'SelectVcambio',
            data: { "moneda_id": MONEDA_ID, "monto_doc_md": monto_doc_md },

            success: function (data) {

                if (data !== null || data !== "") {
                    var iNum = parseFloat(data.replace(',', '.')).toFixed(2);
                    if (iNum > 0) {
                        $('#monto_doc_ml2').val(iNum);
                        $('#montos_doc_ml2').val(iNum);
                        $("label[for='montos_doc_ml2']").addClass("active");

                    } else {
                        M.toast({ html: data });
                    }
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: msg });
            },
            async: false
        });
    }

}

function cambioCurr(fcurr, tcurr, monto) {
    montocambio = 0;
    var localmonto = 0;
    if (fcurr != "" & tcurr != "" & monto != "") {

        $.ajax({
            type: "POST",
            url: 'cambioCurr',
            data: { "fcurr": fcurr, "tcurr": tcurr, "monto": monto },

            success: function (data) {

                if (data !== null || data !== "") {
                                    
                    var iNum = parseFloat(data.replace(',', '.')).toFixed(2);
                    if (iNum > 0) {
                        asignarMonto(data);
                    } else {
                        M.toast({ html: data });
                    }
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: msg });
            },
            async: false
        });
    }

    localmonto = montocambio;
    return localmonto;

}

function asignarMonto(monto) {
    montocambio = monto;
}

function validar_fechas(ini_date, fin_date) {

    var DateToValue = new Date();
    var DateFromValue = new Date();

    var idate = ini_date.split('/');
    DateFromValue.setFullYear(idate[0], idate[1], idate[2], 0, 0, 0, 0);

    var fdate = fin_date.split('/');
    DateToValue.setFullYear(fdate[0], fdate[1], fdate[2], 0, 0, 0, 0);

    if (Date.parse(DateFromValue) <= Date.parse(DateToValue)) {
        return true;
    }
    return false;
}

function validar_montos(base, footer) {

    var basei = convertI(base);
    var footeri = convertI(footer);

    if (basei == footeri) {
        return true;
    }

    return false;
}

function getCategoria(mat) {
    categoriamaterial = "";
    var localcat = "";
    if (mat != "") {
        $.ajax({
            type: "POST",
            url: 'getCategoria',
            data: { "material": mat },

            success: function (data) {

                if (data !== null || data !== "") {
                        asignarCategoria(data);                     
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
            },
            async: false
        });
    }

    localcat = categoriamaterial;
    return localcat;

}

function asignarCategoria(cat) {
    categoriamaterial = cat;
}

function valMaterial(mat) {
    materialVal = "";
    var localval = "";
    if (mat != "") {
        $.ajax({
            type: "POST",
            url: 'getMaterial',
            dataType: "json",
            data: { "mat": mat },

            success: function (data) {

                if (data !== null || data !== "") {
                    asignarValMat(data);
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
            },
            async: false
        });
    }

    localval = materialVal;
    return localval;
}

function asignarValMat(val) {
    materialVal = val;
}

