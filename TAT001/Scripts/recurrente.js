

function llenaCat(vkorg, vtweg, spart, kunnr) {
    document.getElementById("loader").style.display = "initial";
    var soc = document.getElementById("sociedad_id").value;
    $("#select_categoria").find('option').remove().end();
    $.ajax({
        type: "POST",
        url: '../Listas/categoriasCliente',
        dataType: "json",
        data: { vkorg: vkorg, spart: spart, kunnr: kunnr, soc_id: soc },
        success: function (data) {
            $("#select_categoria").find('option').remove().end();

            for (var i = 0; i < data.length; i++) {
                var num = data[i].MATERIALGP_ID;
                var cat = data[i].TXT50;
                $("#select_categoria").append($("<option></option>")
                    .attr("value", num)
                    .text(cat));
            }
            var elem = document.getElementById("select_categoria");
            var instance = M.Select.init(elem, []);
            document.getElementById("loader").style.display = "none";
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            M.toast({ html: httpStatusMessage });
            document.getElementById("loader").style.display = "none";
        },
        async: false
    });
}

$(document).ready(function () {
    $('#table_rec').DataTable({
        "language": {
            "zerorecords": "no hay registros",
            "infoempty": "registros no disponibles",
            "decimal": ".",
            "thousands": ","
        },
        "paging": false,
        //        "ordering": false,
        "info": false,
        "searching": false,
        "columns": [
            {
                "name": 'POS',
                "className": 'POS',
            },
            {
                "name": 'TSOL',
                "className": 'TSOL'
            },
            {
                "name": 'FECHA',
                "className": 'FECHA'
            },
            {
                "name": 'MONTO',
                "className": 'MONTO'
            },
            {
                "name": 'PORCENTAJE',
                "className": 'PORCENTAJE'
            }
        ]
    });
});

function cambiaRec() {
    var campo = document.getElementById("check_recurrente");
    var radio = document.getElementById("btn-peri");
    var table = $('#table_rec').DataTable();
    table.clear().draw(true);
    var tipo = document.getElementById("select_neg").value;
    var montoo = document.getElementById("monto_dis").value;

    if (radio != null) //B20180625 MGC 2018.06.26 Marcaba error, por validación de null
    if (radio.checked)
        if (campo.checked) {
            if (montoo === "") {
                var dist = $('#table_dis').DataTable();
                var montooo = 0.00;
                $('#table_dis > tbody  > tr').each(function () {
                    var montot = $(this).find("td.total input").val();
                    //montoo += parseInt(montot);
                    montooo += parseFloat(montot);
                });
                montoo = montooo;
            }
            if (montoo > 0) {

                //if (montoo > 0) { 
                $(".table_rec").css("display", "table");
                //Add row 
                ////var datei = document.getElementById("fechai_vig").value.split('/');
                ////var datef = document.getElementById("fechaf_vig").value.split('/');
                ////var dateii = new Date(datei[2], datei[1] - 1, datei[0]);
                ////var dateff = new Date(datef[2], datef[1] - 1, datef[0]);
                var pe1 = document.getElementById("periodoi_id").value;
                var pe2 = document.getElementById("periodof_id").value;
                var ej1 = document.getElementById("anioi_id").value;
                var ej2 = document.getElementById("aniof_id").value;

                var anios = ej2 - ej1;
                ////var resdate = dateff - dateii;

                var meses = 1 + (pe2 - pe1) + (anios * 12);
                if (meses > 1 & montoo > 0) {
                    for (var i = 1; i <= meses; i++) {
                        var date = "";
                        var monto = "";
                        if (i === 1) {
                            if (tipo !== "P") {
                                ////date = document.getElementById("fechai_vig").value;
                                ////monto = montoo;
                                //////addRowRec(table, i, date, monto, tipo);
                                ////primerDiaT(table, i, datei, monto, tipo);
                            } else {
                                ////var dates = new Date(datei[2], datei[1] - 1 + i, 1);
                                //////date = date.addDays(-1);
                                ////dates.setDate(dates.getDate() - 1);
                                ////date = dates.getDate() + "/" + (dates.getMonth() + 1) + "/" + dates.getFullYear();
                                monto = montoo;
                                //////addRowRec(table, i, date, monto, tipo);
                                ////ultimoDiaT(table, i, datei, monto, tipo);
                                ultimoDiaT(table, i, pe1, ej1, monto, tipo);
                            }
                        }
                        else {
                            if (tipo !== "P") {
                                //////var dates = new Date(datei[2], datei[1] - 2 + i, 1);
                                //////date = dates.getDate() + "/" + (dates.getMonth() + 1) + "/" + dates.getFullYear();
                                ////monto = montoo;
                                ////primerDiaT(table, i, datei, monto, tipo);
                            } else {
                                ////var dates = new Date(datei[2], datei[1] - 1 + i, 1);
                                //////date = date.addDays(-1);
                                ////dates.setDate(dates.getDate() - 1);
                                ////date = dates.getDate() + "/" + (dates.getMonth() + 1) + "/" + dates.getFullYear();
                                monto = montoo;
                                //////addRowRec(table, i, date, monto, tipo);
                                ////ultimoDiaT(table, i, datei, monto, tipo);
                                ultimoDiaT(table, i, pe1, ej1, monto, tipo);
                            }
                        }
                    }

                }
            }
            else {
                $(".table_rec").css("display", "none");
                //campo.checked = false;
            }
        } else {
            $(".table_rec").css("display", "none");
        }


}

function addRowRec(t, num, date, monto, tipo) {
    if (tipo !== "P") {
        addRowRecl(
            t,
            num, //POS
            document.getElementById("tsol_id").value,
            date,
            monto,
            0.00
            //"<input class=\"PORCENTAJE input_rec numberd input_dc \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
        );
    } else {
        if (num !== 1) {
            addRowRecl(
                t,
                num, //POS
                document.getElementById("tsol_id").value,
                date,
                "<input class=\"MONTO input_rec numberd input_dc monto \" style=\"font-size:12px;height:2rem;\" type=\"text\" id=\"\" name=\"\" value=\"" + monto + "\">",
                "<input class=\"PORCENTAJE input_rec numberd input_dc\" style=\"font-size:12px;height:2rem;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
            );
        } else {
            addRowRecl(
                t,
                num, //POS
                document.getElementById("tsol_id").value,
                date,
                monto,
                //0.00
                //"<input class=\"MONTO input_rec numberd input_dc monto \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + monto + "\">",
                "<input class=\"PORCENTAJE input_rec numberd input_dc\" style=\"font-size:12px;height:2rem;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
            );
        }
    }
}

function addRowRecl(t, pos, tsol, fecha, monto, porc) {
    //var t = $('#table_rec').DataTable();

    t.row.add([
        pos
        , tsol
        , fecha
        , monto
        , porc
    ]).draw(false);
}

function enviaRec(borrador) { //B20180625 MGC 2018.07.03

    var lengthT = $("table#table_rec tbody tr[role='row']").length;
    var tipo = document.getElementById("select_neg").value;

    if (lengthT > 0) {
        //Obtener los valores de la tabla para agregarlos a la tabla oculta y agregarlos al json
        //Se tiene que jugar con los index porque las columnas (ocultas) en vista son diferentes a las del plugin
        var indext = 0;
        jsonObjDocs = [];
        var i = 1;
        var vol = "";
        var sol = $("#tsol_id").val();
        var mostrar = isFactura(sol);
        //if (sol == "NC" | sol == "NCI" | sol == "OP") {
        if (mostrar) {
            vol = "real";
        } else {
            vol = "estimado";
        }

        var poss = 0;
        $("#table_rec > tbody  > tr[role='row']").each(function () { //B20180625 MGC 2018.07.03
            poss++;

            var pos = $(this).find("td.POS").text();
            var tsol = $(this).find("td.TSOL").text();
            var fecha = $(this).find("td.FECHA").text();
            var monto = "";
            if (tipo === "P") {
                if (poss === 1) {
                    monto = $(this).find("td.MONTO").text();
                } else {
                    monto = $(this).find("td.MONTO input").val();
                }
                var porcentaje = $(this).find("td.PORCENTAJE input").val();
            } else {
                monto = $(this).find("td.MONTO").text();
                var porcentaje = $(this).find("td.PORCENTAJE").text();
            }


            //Obtener el id de la categoría            
            var t = $('#table_rec').DataTable();
            var tr = $(this);

            var item = {};

            item["NUM_DOC"] = 0;
            item["POS"] = pos;
            item["TSOL"] = tsol;
            item["FECHAF"] = fecha + " 12:00:00 p.m.";
            item["MONTO_BASE"] = monto;
            item["PORC"] = porcentaje;

            jsonObjDocs.push(item);
            i++;
            item = "";

            //$(this).addClass('selected');
            if (borrador != "X") { //B20180625 MGC 2018.07.03
                $(this).addClass('selected');
            }

        });

        docsenviar = JSON.stringify({ 'docs': jsonObjDocs });

        $.ajax({
            type: "POST",
            url: 'getPartialRec',
            contentType: "application/json; charset=UTF-8",
            data: docsenviar,
            success: function (data) {

                if (data !== null || data !== "") {

                    $("table#table_rech tbody").append(data);
                    if (borrador != "X") { //B20180625 MGC 2018.07.03
                        $('#delRow').click();
                    }
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
            },
            async: false
        });
    }

}

function copiarTableVistaRec() {

    var lengthT = $("table#table_rech tbody tr").length;
    var tipo = document.getElementById("select_neg").value;

    if (lengthT > 0) {
        //Obtener los valores de la tabla para agregarlos a la tabla de la vista en información
        //Se tiene que jugar con los index porque las columnas (ocultas) en vista son diferentes a las del plugin
        //$('#check_recurrente').trigger('change');
        document.getElementById("check_recurrente").checked = true;
        $(".table_rec").css("display", "table");
        var rowsn = 0;

        var tsol = "";
        var sol = $("#tsol_id").val();

        var i = 1;
        $('#table_rech > tbody  > tr').each(function () {

            //var pos = $(this).find("td.POS").text();
            var pos = $(this).find("td:eq(1)").text();
            //var fecha = $(this).find("td.FECHA").text();
            var fecha = $(this).find("td:eq(2)").text().trim();

            var ffecha = fecha.split(' ');

            //var prov = $(this).find("td.PROVEEDOR").text();
            var monto = $(this).find("td:eq(3)").text();
            //var control = $(this).find("td.CONTROL").text();
            var porc = $(this).find("td:eq(4)").text();
            // var autorizacion = $(this).find("td.AUTORIZACION").text();

            //if ($("#check_factura").is(':checked')) {

            //    factura = "<input class=\"FACTURA input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + factura + "\">";
            //    ffecha[0] = "<input class=\"FECHA input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + ffecha[0] + "\">";
            //    prov = "<input class=\"PROVEEDOR input_sop_f input_proveedor\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + prov + "\">";
            //    control = "<input class=\"CONTROL input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + control + "\">";
            //    autorizacion = "<input class=\"AUTORIZACION input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + autorizacion + "\">";
            //    vven[0] = "<input class=\"VENCIMIENTO input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + vven[0] + "\">";
            //    facturak = "<input class=\"FACTURAK input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + facturak + "\">";
            //    ejerciciok = "<input class=\"EJERCICIOK input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + ejerciciok + "\">";
            //    bill_doc = "<input class=\"BILL_DOC input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + bill_doc + "\">";
            //    belnr = "<input class=\"BELNR input_sop_f\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + belnr + "\">"

            //}
            if (tipo === "P") {
                if (pos.trim() === "1") {
                    monto = monto.trim();
                } else {
                    monto = "<input class=\"MONTO input_rec numberd input_dc monto \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + monto.trim() + "\">";
                }

                porc = "<input class=\"PORCENTAJE input_rec numberd input_dc\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + porc.trim() + "\">";
            } else {
                monto = monto.trim();
                porc = porc.trim();
            }
            var t = $('#table_rec').DataTable();

            addRowRecl(t, pos.trim(), sol.trim(), ffecha[0], monto, porc);

            //Quitar el row
            $(this).remove();
            if (i > rowsn) {

            }
        });
        ////Hide columns
        //ocultarColumnasTablaSoporteDatos();
        //$('.input_sop_f').trigger('focusout');
    }

    //var sol = $("#tsol_id").val();

    //selectTsol(sol);
}

function primerDiaT(t, num, date, monto, tipo) {
    document.getElementById("loader").style.display = "initial";


    $.ajax({
        type: "POST",
        url: '../Listas/getPrimerDia',
        dataType: "json",
        data: { ejercicio: date[2], periodo: (date[1] - 1 + num) },
        success: function (data) {
            document.getElementById("loader").style.display = "none";
            var dd = data.split('/');
            var dates = new Date(dd[2], dd[1], dd[0]);
            datee = dates.getDate() + "/" + (dates.getMonth()) + "/" + dates.getFullYear();

            addRowRec(t, num, datee, monto, tipo);
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            M.toast({ html: httpStatusMessage });
            document.getElementById("loader").style.display = "none";
        },
        async: false
    });
}

//function ultimoDiaT(t, num, date, monto, tipo) {
function ultimoDiaT(t, num, periodo, ejercicio, monto, tipo) {
    document.getElementById("loader").style.display = "initial";


    $.ajax({
        type: "POST",
        url: '../Listas/getUltimoDia',
        dataType: "json",
        data: { ejercicio: ejercicio, periodo: (periodo - 1 + num) },
        success: function (data) {
            document.getElementById("loader").style.display = "none";
            var dd = data.split('/');
            var dates = new Date(dd[2], dd[1], dd[0]);
            datee = dates.getDate() + "/" + (dates.getMonth()) + "/" + dates.getFullYear();

            addRowRec(t, num, datee, monto, tipo);
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            M.toast({ html: httpStatusMessage });
            document.getElementById("loader").style.display = "none";
        },
        async: false
    });
}

function setDates(tipo) {
    if (tipo == "date") {
        document.getElementById('fechai_vig').value = document.getElementById('fechai_vig2').value;
        document.getElementById('fechaf_vig').value = document.getElementById('fechaf_vig2').value;
    } else {
        document.getElementById("loader").style.display = "initial";
        var ej1 = document.getElementById('anioi_id').value;
        var pe1 = document.getElementById('periodoi_id').value;
        var ej2 = document.getElementById('aniof_id').value;
        var pe2 = document.getElementById('periodof_id').value;

        $.ajax({
            type: "POST",
            url: '../Listas/getPrimerDia',
            dataType: "json",
            data: { ejercicio: ej1, periodo: pe1 },
            success: function (data) {
                document.getElementById("loader").style.display = "none";
                var dd = data.split('/');
                var dates = new Date(dd[2], dd[1], dd[0]);
                datee = dates.getDate() + "/" + (dates.getMonth()) + "/" + dates.getFullYear();

                document.getElementById('fechai_vig').value = datee;
            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
                document.getElementById("loader").style.display = "none";
            },
            async: false
        });
        $.ajax({
            type: "POST",
            url: '../Listas/getUltimoDia',
            dataType: "json",
            data: { ejercicio: ej2, periodo: pe2 },
            success: function (data) {
                document.getElementById("loader").style.display = "none";
                var dd = data.split('/');
                var dates = new Date(dd[2], dd[1], dd[0]);
                datee = dates.getDate() + "/" + (dates.getMonth()) + "/" + dates.getFullYear();

                document.getElementById('fechaf_vig').value = datee;
            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
                document.getElementById("loader").style.display = "none";
            },
            async: false
        });

        //var firstDay = new Date(document.getElementById('anioi_id').value, document.getElementById('periodoi_id').value - 1, 1);
        //var lastDay = new Date(document.getElementById('aniof_id').value, document.getElementById('periodof_id').value, 0);

        //var fechai = firstDay.getDate() + "/" + (firstDay.getMonth() + 1) + "/" + firstDay.getFullYear();
        //var fechaf = lastDay.getDate() + "/" + (lastDay.getMonth() + 1) + "/" + lastDay.getFullYear();

        //document.getElementById('fechai_vig').value = fechai;
        //document.getElementById('fechaf_vig').value = fechaf;
    }
    pickerFecha2(".format_date");
}


//Evaluar la extensión y tamaño del archivo a cargar
function changeFile(campo) {
    var length = $(campo).length;
    var message = "";
    var namefile = "";
    if (length > 0) {
        //Validar tamaño y extensión
        var file = $(campo).get(0).files;
        if (file.length > 0) {
            var sizefile = file[0].size;
            namefile = file[0].name;
            if (sizefile > 20971520) {
                message = 'Error! Tamaño máximo del archivo 20 M --> Archivo ' + namefile + " sobrepasa el tamaño";

            }

            if (!evaluarExtSoporte(namefile)) {
                //message = "Error! Tipos de archivos aceptados 'xlsx', 'doc', 'pdf', 'png', 'msg', 'zip', 'jpg', 'docs' --> Archivo " + namefile + " no es compatible";
                message = "Error! Tipos de archivos rechazados 'exe', 'bak' --> Archivo " + namefile + " no es compatible";

            }
        }
    } else {
        message = "No selecciono archivo";
    }

    if (message != "") {
        $(campo).val("");
        M.toast({ html: message });
    } else {
        //Verificar los nombres
        var id = $(campo).attr('id');
        var res = evaluarFilesName(id, namefile);

        if (res) {
            //Nombre duplicado
            M.toast({ html: 'Ya existe un archivo con ese mismo nombre' });
        }
    }
}