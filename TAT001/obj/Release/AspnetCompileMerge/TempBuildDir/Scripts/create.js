$(document).ready(function () {

    $('#cargar_xls').click(function () {
        
        var filenum = $('#file_dis').get(0).files.length;
        if (filenum > 0) { 
            var file = document.getElementById("file_dis").files[0];
            var filename = file.name;
            if (evaluarExt(filename)) {
                M.toast({ html: 'Uploading' + filename });
                loadExcel(file);
            } else {
                M.toast({ html: 'Tipo de archivo incorrecto: ' + filename });
            }
        } else {
            M.toast({ html: 'Seleccione un archivo' });
        }
    });


    $('#table_dis tbody').on('click', 'tr', function () {
        $(this).toggleClass('selected');
    });

    $('#delRow').click(function () {
        alert("del");
        table.rows('.selected').remove().draw(false);
    });

    //Archivos de soporte
    $('#btnsoportes').click(function () {
        //loadFilesf();
        //var file_carta = $('#file_carta').get(0).files.length;

        //var file_carta = $('#file_soporte').get().files;
        //var file_contratos = $('#file_contratos').get(0).files.length;
        //var file_factura = $('#file_factura').get(0).files.length;
        //var file_jbp = $('#file_jbp').get(0).files.length;

        //if (file_carta > 0)//&& file_contratos > 0 && file_factura > 0 && file_jbp > 0) {
        //{
            //var f_carta = document.getElementById("file_carta").files[0];
            //var f_contratos = document.getElementById("file_contratos").files[0];
            //var f_factura = document.getElementById("file_factura").files[0];
            //var f_jbp = document.getElementById("file_jbp").files[0];

            //var filename = f_carta.name;            
                //M.toast({ html: 'Uploading' + filename });
                //loadFile(f_carta)//, f_contratos, f_factura, f_jbp);   
            
        //} else {
            //M.toast({ html: 'Seleccione un archivo' });
        //}        
        //var files = $('.file_soporte');
        //var message = "";

        //for (var i = 0; i < files.length; i++) {
        //    //var file = $(files[i]).get(0).files;
        //    var className = $(files[i]).attr("class");
        //    //Valida si el archivo es obligatorio
        //    if (className.indexOf('nec') >= 0) {
        //        //Validar archivo en archivo obligatorio
        //        var nfile = $(files[i]).get(0).files.length;
        //        if (!nfile > 0) {
        //            var lbltext = $(files[i]).closest('td').prev().children().eq(0).html();
        //            //var parenttd = $(files[i]).closest('td').prev().children().eq(0).html();
        //            //var sitd = $(parenttd).prev().children().eq(0).html();
        //            //var labeltext = $(sitd).children().eq(0).html();
        //            //M.toast({ html: 'Error! Archivo Obligatorio: ' + lbltext });
        //            message = 'Error! Archivo Obligatorio: ' + lbltext;
        //            break;
        //        }
        //    }
        //}

        //if (message == "") {
        //    loadFiles(files)
        //} else {
        //    M.toast({ html: message });
        //}
    });

    $('#tabs').tabs();

    //$('select').Select();
    var elem = document.querySelectorAll('select');
    var instance = M.Select.init(elem, []);

    $('#tab_temp').on("click", function (e) {

        evalInfoTab(false, e);
    });

    $('#tab_soporte').on("click", function (e) {

        evalTempTab(false, e);

    });

    $('#tab_fin').on("click", function (e) {

        evalSoporteTab(false, e);

    });

        //$('#cargar').click(function () {
        //    M.toast({ html: 'Load' })
        //    loadExcel();
        //});
    //Enter en el monto
    $('#monto_doc_md').keypress(keypressHandler);

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
            var iNum = parseFloat(tipo_cambio.replace(',', '.')).toFixed(2);
            
            if (iNum > 0) {
                var num = "" + iNum;
                num = num.replace('.', ',');
                var numexp = num;//* 60000000000;
                //$('#tipo_cambio').val(numexp);
            } else {
                $('#tipo_cambio').val(0);
            }
            var tipo_cambio = $('#monto_doc_ml2').val();
            var iNum2 = parseFloat(tipo_cambio.replace(',', '.')).toFixed(2);
            //var iNum2 = parseFloat(tipo_cambio.replace('.', ','));
            if (iNum2 > 0) {
                var nums = "" + iNum2;
                nums = nums.replace('.', ',');
                var numexp2 = nums;// * 60000000000;
                $('#monto_doc_ml2').val(numexp2);
            } else {
                $('#monto_doc_ml2').val(0);
            }

            //Monto
            var monto = $('#monto_doc_md').val();
            var numm = parseFloat(monto.replace(',', '.')).toFixed(2);   
            if (numm > 0) {
                var numsm = "" + numm;
                numsm = numsm.replace('.', ',');
                var numexp2m = numsm;// * 60000000000;
                $('#monto_doc_md').val(numexp2m);
            } else {
                $('#monto_doc_md').val(0);
            }
            //Termina provisional
            $('#btn_guardar').click();
        } else {
            M.toast({ html: msg })
        }
        
    });
    
});

function keypressHandler(e) {
    
    if (e.which == 13) {
        e.preventDefault(); //stops default action: submitting form
        //var msg = 'Enter';
        //M.toast({ html: msg })

        var monto_doc_md = $('#monto_doc_md').val()

        if (monto_doc_md > 0){
            //Obtener la moneda en la lista
            var MONEDA_ID = $('#moneda_id').val();

            selectTcambio(MONEDA_ID, monto_doc_md);
            
        }

    }
}

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
    //var totalFiles = document.getElementById("file_dis").files.length;

    //for (var i = 0; i < totalFiles; i++) {
    //    var file = document.getElementById("file_dis").files[i];
    //    var filename = file.name;
        formData.append("FileUpload", file);        
    //}

    var table = $('#table_dis').DataTable();
    $.ajax({
        type: "POST",
        url: 'LoadExcel',
        //data: { "url": url },
        data: formData,
        dataType: "json",
        cache: false,
        contentType: false,
        processData: false,
        success: function (data) {

            if (data !== null || data !== "") {
                //alert("success" + data);
                $.each(data, function (i, dataj) {   

                    var date_de = new Date(parseInt(dataj.VIGENCIA_DE.substr(6)));
                    var date_al = new Date(parseInt(dataj.VIGENCIA_AL.substr(6)));
                    table.row.add([
                        date_de.getDate() + "/" + (date_de.getMonth() + 1) + "/" + date_de.getFullYear(),
                        date_al.getDate() + "/" + (date_al.getMonth() + 1) + "/" + date_al.getFullYear(),     
                        dataj.MATNR,
                        "Cereales",
                        "Corn Flakes 200gr"
                        ]).draw(false);
                });
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            alert("Request couldn't be processed. Please try again later. the reason        " + xhr.status + " : " + httpStatusMessage + " : " + customErrorMessage);
        }
    });

}

function loadFile(f_carta) {//, f_contratos, f_factura, f_jbp) {

    var formData = new FormData();
    //var totalFiles = document.getElementById("file_dis").files.length;

    //for (var i = 0; i < totalFiles; i++) {
    //    var file = document.getElementById("file_dis").files[i];
    //    var filename = file.name;
    formData.append("f_carta", f_carta);
    //formData.append("f_contratos", f_contratos);
    //formData.append("f_factura", f_factura);
    //formData.append("f_jbp", f_jbp);
    //}

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
        }
    });

}

function loadFiles(files) {//, f_contratos, f_factura, f_jbp) {

    var formData = new FormData();
    //var totalFiles = document.getElementById("file_dis").files.length;

    //for (var i = 0; i < totalFiles; i++) {
    //    var file = document.getElementById("file_dis").files[i];
    //    var filename = file.name;
    //formData.append("f_carta", f_carta);
    //formData.append("f_contratos", f_contratos);
    //formData.append("f_factura", f_factura);
    //formData.append("f_jbp", f_jbp);
    //}
    var count = 1;
    for (var i = 0; i < files.length; i++) {
        var file = $(files[i]).get(0);
        //var name = "f_carta" + count;
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
        }
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
    }

    if (message == "") {
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
        var iNum = parseFloat(v.replace(',', '.'))
        if (iNum > 0) {
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
                    $('<option>', {
                        text: "--Seleccione--"
                    }).html("--Seleccione--").appendTo($("#tall_id"));
                    $.each(data, function (i, optiondata) {
                        $('<option>', {
                            value: optiondata.ID,
                            text: optiondata.TEXT
                        }).html(optiondata.NAME).appendTo($("#tall_id"));
                    });
                    
                    var elem = document.getElementById('tall_id');
                    var instance = M.Select.init(elem, []);
                }
            },
            error: function (data) {
                alert("Request couldn't be processed. Please try again later. the reason        " + data);
            }
        });
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
            }
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
                    $('#vkorg').val(data.VKORG).focus();
                    $('#stcd1').val(data.STCD1);
                    $("label[for='stcd1']").addClass("active");
                    $('#vtweg').val(data.VTWEG);
                    $('#payer_nombre').val(data.PAYER_NOMBRE);
                    $('#payer_email').val(data.PAYER_EMAIL);
                }

            },
            error: function (data) {
                $('#vkorg').val("").focus();
                $('#stcd1').val("");
                $("label[for='stcd1']").addClass("active");
                $('#vtweg').val("");
                $('#payer_nombre').val("");
                $('#payer_email').val("");
            }
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
                        }
                    }

                },
                error: function (data) {
                    alert("Error tipo de cambio        " + data);
                }
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
            data: { "moneda_id": MONEDA_ID, "monto_doc_md": monto_doc_md},

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
            }
        });
    }

}

    //function loadExcel() {

    //    var url = "C:\Users\matias\Desktop\prueba.xlsx";
    //    var sim = "@@n\\";
    //    var url2 = url.replace('n\\', sim));

    //    $.ajax({
    //        type: "POST",
    //        url: 'LoadExcel',
    //        data: { "url": url },
    //        dataType: "json",
    //        success: function (data) {

    //            if (data !== null || data !== "") {
    //                alert("success" + data);
    //            }
    //        },
    //        error: function (data) {
    //            alert("Request couldn't be processed. Please try again later. the reason        " + data);
    //        }
    //    });

    //}