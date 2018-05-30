

function llenaCat(vkorg, vtweg, spart, kunnr) {
    document.getElementById("loader").style.display = "initial";
    var soc = document.getElementById("sociedad_id").value;
    $.ajax({
        type: "POST",
        url: '../Listas/categoriasCliente',
        dataType: "json",
        data: { vkorg: vkorg, spart: spart, kunnr: kunnr, soc_id: soc },
        success: function (data) {
            $("#select_categoria").find('option').remove().end();

            for (var i = 0; i < data.length; i++) {
                var num = data[i].CATEGORIA_ID;
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

function cambiaRec(campo) {
    var table = $('#table_rec').DataTable();
    table.clear().draw(true);
    if (campo.checked) {

        //if (montoo > 0) {
        $(".table_rec").css("display", "table");
        //Add row 
        var datei = document.getElementById("fechai_vig").value.split('/');
        var datef = document.getElementById("fechaf_vig").value.split('/');
        var dateii = new Date(datei[2], datei[1] - 1, datei[0]);
        var dateff = new Date(datef[2], datef[1] - 1, datef[0]);

        var anios = datef[2] - datei[2];

        var meses = 1 + (datef[1] - datei[1]) + (anios * 12);
        var montoo = document.getElementById("monto_dis").value;
        if (meses > 1 & montoo > 0) {
            for (var i = 1; i <= meses; i++) {
                var date = "";
                var monto = "";
                if (i === 1) {
                    date = document.getElementById("fechai_vig").value;
                    monto = montoo;
                }
                else {
                    var dates = new Date(datei[2], datei[1] - 2 + i, 1);
                    date = dates.getDate() + "/" + (dates.getMonth() + 1) + "/" + dates.getFullYear();
                    monto = "";
                }
                addRowRec(table, i, date, monto);
            }
        }
        else {
            $(".table_rec").css("display", "none");
            campo.checked = false;
        }
    } else {
        $(".table_rec").css("display", "none");
    }

}



function addRowRec(t, num, date, monto) {
    if (monto !== "") {
        addRowRecl(
            t,
            num, //POS
            document.getElementById("tsol_id").value,
            date,
            monto,
            "<input class=\"PORCENTAJE input_rec numberd input_dc \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
        );
    } else {
        addRowRecl(
            t,
            num, //POS
            document.getElementById("tsol_id").value,
            date,
            "<input class=\"MONTO input_rec numberd input_dc monto \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"" + monto + "\">",
            "<input class=\"PORCENTAJE input_rec numberd input_dc\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
        );
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


function enviaRec() {

    var lengthT = $("table#table_rec tbody tr[role='row']").length;

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
        $('#table_rec > tbody  > tr').each(function () {
            poss++;

            var pos = $(this).find("td:eq(" + (0 + indext) + ")").text();
            var tsol = $(this).find("td:eq(" + (1 + indext) + ")").text();
            var fecha = $(this).find("td:eq(" + (2 + indext) + ")").text();
            var monto = "";
            if (poss === 1) {
                monto = $(this).find("td:eq(" + (3 + indext) + ")").text();
            } else {
                monto = $(this).find("td:eq(" + (3 + indext) + ") input").val();
            }
            var porcentaje = $(this).find("td:eq(" + (4 + indext) + ") input").val();


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
                    $('#delRow').click();
                }

            },
            error: function (xhr, httpStatusMessage, customErrorMessage) {
                M.toast({ html: httpStatusMessage });
            },
            async: false
        });
    }

}