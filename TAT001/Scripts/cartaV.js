$('body').on('click', '#imprimir_btn', function (e) {

    //Validar valores correctos en distribución
    updateFooter(false);

    var total = 0;

    total = totalFooter();

    var texto = armarMonto(total);

    //Obtener el monto original
    var monto = $('#monto').val();
    if (total > monto) {
        M.toast({ html: 'Monto de distribución es mayor al monto de la solicitud' });
    } else {
        copiarTableControl();
        $('#submit_btn').click();
    }
});

$('body').on('focusout', '.input_oper', function () {
   
    var tr = $(this).closest('tr'); //Obtener el row 

    //Solo a cantidades
    if ($(this).hasClass("numberd")) {

        //Se dispara el evento desde el total
        if ($(this).hasClass("total")) {
            var total_val = $(this).val();
            //Agregar los valores a 0 y agregar el total
            updateTotalRow(tr, "", "X", total_val);
            //alert("total" + total_val);
        } else {
            //alert("no total");
            updateTotalRow(tr, "", "", 0);
        }

    }
});

function updateTotalRow(tr, tdp_apoyo, totals, total_val) {

    //totals = X cuando nada más se agrega el total

    //Multiplicar costo unitario % por apoyo(dividirlo entre 100)
    //Columnas 8 * 9 res 10
    //Categoría es 7 * 8 = 9  --> -1
    //Material es 6 * 7 = 8   --> -2

    //Validar si las operaciones se hacen por renglón o solo agregar el valor del total
    if (totals != "X") {
        var col3 = tr.find("td:eq(" + (3) + ") input").val();
        var col4 = tr.find("td:eq(" + (4) + ") input").val();

        col4 = convertP(col4);

        if ($.isNumeric(col4)) {
            col4 = col4 / 100;
        }

        var col5 = col3 * col4;
        //Apoyo por pieza
        //Modificar el input
        tr.find("td:eq(" + (5) + ") input").val(col5.toFixed(2));

        //Costo con apoyo
        var col6 = col3 - col5;
        tr.find("td:eq(" + (6) + ")").text(col6.toFixed(2));

        //Estimado apoyo
        var col8 = tr.find("td:eq(" + (8) + ") input").val();
        var col9 = col5 * col8;
        //col14 = col14.toFixed(2);
        tr.find("td:eq(" + (9) + ") input").val(col9.toFixed(2));

        //Agregar nada más el total
    } else {
        total_val = parseFloat(total_val);
        var col9 = total_val.toFixed(2);
        tr.find("td:eq(" + (3) + ") input").val("0.00");
        if (tdp_apoyo != "X") {
            tr.find("td:eq(" + (4) + ") input").val("0.00");
        }
        tr.find("td:eq(" + (5) + ") input").val("0.00");
        tr.find("td:eq(" + (6) + ")").text("0.00");
        tr.find("td:eq(" + (7) + ") input").val("0.00");
        tr.find("td:eq(" + (8) + ") input").val("0.00");
        tr.find("td:eq(" + (9) + ") input").val(col9);
    }

    updateFooter(true);
}

function convertP(i) {
    return typeof i === 'string' ?
        i.replace(/[\$,]/g, '') * 1 :
        typeof i === 'number' ?
            i : 0;
};

function convertI(i) {
    return typeof i === 'string' ?
        i.replace(/[\$,]/g, '') * 1 :
        typeof i === 'number' ?
            i : 0;
};

function updateFooter(flag) {
    resetFooter();
    var total = 0;

    total = totalFooter();

    var texto = armarMonto(total);

    //Obtener el monto original
    if (flag) {
        var monto = $('#monto').val();
        if (total > monto) {
            M.toast({ html: 'Monto de distribución es mayor al monto de la solicitud' });
        }
    }

    $('#lbl_monto').text(texto);
}

function totalFooter() {
    coltotal = (9);
    var total = 0;

    //Obtener las tablas
    var tables = $('.table_mat');

    try {
        for (var i = 0; i < tables.length; i++) {
            var tabname = "#" + tables[i].id;
            $(tabname).find("tr").each(function (index) {
                var col9 = $(this).find("td:eq(" + coltotal + ") input").val();

                col9 = convertI(col9);

                if ($.isNumeric(col9)) {
                    total += col9;
                }

            });
        }
    } catch (error) {

    }

    total = total.toFixed(2);

    return total;
}

function resetFooter() {
    var texto = armarMonto(0);
    $('#lbl_monto').text(texto);
}

function armarMonto(monto) {
    var texto = $('#monto_texto').val();
    //var monto = $('#monto').val();
    var moneda = $('#moneda').val();

    return texto + " " + monto + " " + moneda
}

function copiarTableControl() {

    //var lengthT = $("table#table_dis tbody tr[role='row']").length;
    var tables = $('.table_mat');

    if (tables.length > 0) {
        //Obtener los valores de la tabla para agregarlos a la tabla oculta y agregarlos al json

        jsonObjDocs = [];
        var j = 1;
        var vol = "";
        var mostrar = true;
        mostrar = isFactura();

        //var itemh = {};
        //var token = $('input[name=__RequestVerificationToken]').val();
        //itemh["__RequestVerificationToken"] = token;
        //jsonObjDocs.push(itemh);

        if (mostrar) {
            vol = "real";
        } else {
            vol = "estimado";
        }

        //Obtener las tablas
        var tables = $('.table_mat');
        try {
            for (var i = 0; i < tables.length; i++) {
                var tabdedate = "#aldate_" + tables[i].id;
                var tabaldate = "#dedate_" + tables[i].id;
                var tabname = "#" + tables[i].id + " > tbody  > tr[role='row']";
                
                $(tabname).each(function () {

                    //Multiplicar costo unitario % por apoyo(dividirlo entre 100)
                    //Columnas 8 * 9 res 10
                    //Categoría es 7 * 8 = 9  --> -1
                    //Material es 6 * 7 = 8   --> -2

                    var vigencia_de = $(tabdedate).val();//$(this).find("td:eq(" + (3) + ") input").val();
                    var vigencia_al = $(tabaldate).val();//$(this).find("td:eq(" + (4) + ") input").val();

                    var matnr = "";
                    matnr = $(this).find("td:eq(" + (0) + ")").text();
                    var matkl = $(this).find("td:eq(" + (1) + ")").text();

                    //Obtener el id de la categoría            
                    
                    var matkl_id = '';

                    var costo_unitario = $(this).find("td:eq(" + (3) + ") input").val();
                    var porc_apoyo = $(this).find("td:eq(" + (4) + ") input").val();
                    var monto_apoyo = $(this).find("td:eq(" + (5) + ") input").val();

                    var precio_sug = $(this).find("td:eq(" + (7) + ") input").val();
                    var volumen_est = $(this).find("td:eq(" + (8) + ") input").val();

                    var total = $(this).find("td:eq(" + (9) + ") input").val();

                    var item = {};

                    item["NUM_DOC"] = 0;
                    item["POS"] = j;
                    item["MATNR"] = matnr || "";
                    item["MATKL"] = matkl;
                    item["MATKL_ID"] = matkl_id;
                    item["DESC"] = "";
                    item["CANTIDAD"] = 0; //Siempre 0
                    item["MONTO"] = costo_unitario;
                    item["PORC_APOYO"] = porc_apoyo;
                    item["MONTO_APOYO"] = monto_apoyo;
                    item["VIGENCIA_DE"] = vigencia_de + " 12:00:00 p. m.";
                    item["VIGENCIA_AL"] = vigencia_al + " 12:00:00 p. m.";
                    item["PRECIO_SUG"] = precio_sug;
                    volumen_est = volumen_est || 0
                    total = parseFloat(total);
                    if (vol == "estimado") {
                        item["VOLUMEN_EST"] = volumen_est;
                        item["VOLUMEN_REAL"] = 0;
                        item["APOYO_REAL"] = 0;
                        item["APOYO_EST"] = total;
                    } else {
                        item["VOLUMEN_EST"] = 0;
                        item["VOLUMEN_REAL"] = volumen_est;
                        item["APOYO_REAL"] = total;
                        item["APOYO_EST"] = 0;

                    }

                    jsonObjDocs.push(item);
                    j++;
                    item = "";

                });

            }
        } catch (error) {

        }

        

        docsenviar = JSON.stringify({ 'docs': jsonObjDocs });

        $.ajax({
            type: "POST",
            url: '../../Listas/getPartialMat',
            contentType: "application/json; charset=UTF-8",
            data: docsenviar,
            success: function (data) {

                if (data !== null || data !== "") {

                    $("table#table_dish tbody").append(data);

                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
            },
            async: false
        });
    }

}

function isFactura() {

    var res = false;

   
    var fact = $('#isfactura').val();
           
    try {
        fact = (fact == 'true');
    } catch (error) {
        fact = false;
    }
    res = fact;
    return res;
}