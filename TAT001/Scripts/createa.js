

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
            noResults: "", results: ""
        },
        select(event, ui) {

            var label = ui.item.label;
            var value = ui.item.value;
            var desc = label.split('-')
            selectMaterial(value, desc[1], tr);
        }
    });
});

function selectMaterial(val, desc, tr) {
    //Obtener el index del renglón
    var t = $('#table_dis').DataTable();
    var indexcat = t.row(tr).index();
    //Resetear id cat   
    t.cell(indexcat, 0).data("").draw();
    //Obtener el index de la columna
    var indexr = getIndex();  
    desc = $.trim(desc);    
    //Categoría
    var cat = getCategoria(val);
    tr.find("td:eq(" + (6 + indexr) + ")").text(cat.TXT50);
    //Descripción
    tr.find("td:eq(" + (7 + indexr) + ")").text(desc);
    
    //Agregar id de la categoría    
    t.cell(indexcat, 0).data(cat.CATEGORIA_ID).draw();    

    //Remove background a celda de material
    tr.find('td').eq((5 + indexr)).removeClass("errorMaterial");
}
