$('body').on('click', '#imprimir_btn', function () {

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

    $('#table_mat').find("tr").each(function (index) {
        var col9 = $(this).find("td:eq(" + coltotal + ") input").val();

        col9 = convertI(col9);

        if ($.isNumeric(col9)) {
            total += col9;
        }

    });

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