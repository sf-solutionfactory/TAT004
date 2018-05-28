﻿
$('body').on('keydown.autocomplete', '.input_material', function () {
    var tr = $(this).closest('tr'); //Obtener el row
    auto(this).autocomplete({
        source: function (request, response) {
            auto.ajax({
                type: "POST",
                url: 'materiales',
                dataType: "json",
                data: { "Prefix": request.term },
                success: function (data) {
                    response(auto.map(data, function (item) {
                        return { label: item.ID + " - " + item.MAKTX, value: item.ID };
                    }))
                }
            })
        },
        messages: {
            noResults: '',
            results: function (resultsCount) { }
        },
        change: function (e, ui) {
            if (!(ui.item)) {
                e.target.value = "";
            }
        },
        select: function (event, ui) {

            var label = ui.item.label;
            var value = ui.item.value;
            var desc = label.split('-')
            selectMaterial(value, desc[1], tr);
        }
    });
});

function selectMaterial(val, desc, tr) {
    var index = getIndex();
    desc = $.trim(desc);
    //Categoría
    var cat = getCategoria(val);
    tr.find("td:eq(" + (6 + index) + ")").text(cat.TXT50);
    //Descripción
    tr.find("td:eq(" + (7 + index) + ")").text(desc);

    //Remove background a celda de material
    tr.find('td').eq((5 + index)).removeClass("errorMaterial");
}

$('body').on('keydown.autocomplete', '.input_proveedor', function () {
    var tr = $(this).closest('tr'); //Obtener el row
    auto(this).autocomplete({
        source: function (request, response) {
            auto.ajax({
                type: "POST",
                url: 'proveedores',
                dataType: "json",
                data: { "Prefix": request.term },
                success: function (data) {
                    response(auto.map(data, function (item) {
                        return { label: item.ID + " - " + item.NOMBRE, value: item.ID };
                    }))
                }
            })
        },
        messages: {
            noResults: '',
            results: function (resultsCount) { }
        },
        change: function (e, ui) {
            if (!(ui.item)) {
                e.target.value = "";
            }
        },
        select(event, ui) {

            var label = ui.item.label;
            var value = ui.item.value;
            var desc = label.split('-')
            selectProveedor(value, desc[1], tr);
        }
    });
});

function selectProveedor(val, desc, tr) {

    desc = $.trim(desc);
    //Descripción
    tr.find("td.PROVEEDOR_TXT").text(desc);

    //Remove background a celda de proveedor
    tr.find("td.PROVEEDOR").removeClass("errorProveedor");
}