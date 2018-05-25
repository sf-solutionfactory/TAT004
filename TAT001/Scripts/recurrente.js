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
        $(".table_rec").css("display", "table");
        //Add row 
        var datei = document.getElementById("fechai_vig").value.split('/');
        var datef = document.getElementById("fechaf_vig").value.split('/');
        var dateii = new Date(datei[2], datei[1]-1, datei[0]);
        var dateff = new Date(datef[2], datef[1] - 1, datef[0]);

        var anios = datef[2] - datei[2];

        var meses = 1 + (datef[1] - datei[1]) + (anios * 12);
        for (var i = 1; i <= meses; i++) {
            addRowRec(table, i);
        }

        pickerFecha2(".format_fecha", "2");
        //Hide columns
        //ocultarColumnasTablaSoporteDatos();
    } else {
        $(".table_rec").css("display", "none");
    }

}



function addRowRec(t, num) {
    addRowRecl(
        t,
        num, //POS
        document.getElementById("tsol_id").value,
        "<input class=\"FECHA input_rec format_fecha\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">" ,// RSG 21.05.2018",,
        "<input class=\"MONTO input_rec monto \" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">",
        "<input class=\"PORCENTAJE input_rec\" style=\"font-size:12px;\" type=\"text\" id=\"\" name=\"\" value=\"\">"
    );
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