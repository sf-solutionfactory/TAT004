
function soportes(tsol, spras) {
    var soci = document.getElementById("sociedad_id").value;
    var pais = document.getElementById("pais_id").value;
    //alert(tsol + soci + pais);
    $.ajax({
        url: "../../Listas/Soportes",
        type: "POST",
        async: false,
        timeout: 30000,
        dataType: "json",
        data: { bukrs: soci, land: pais, tsol: tsol, spras: spras },
        success: function (data) {
            var pp = ($.map(data, function (item) {
                return { tsoporte: item.TSOPORTE_ID, oblig: item.OBLIGATORIO, txt50: item.TXT50 };
            }))
            $("#div_soportes").empty();
            for (var i = 0; i < pp.length; i++) {
                var input = '<label name="labels_soporte2" class="col s12">';
                if (pp[i].oblig) {
                    input += '* ';
                }
                input += pp[i].txt50 + '</label><label class"lbl_nec"></label>' +
                    '<input type="text" value="' + pp[i].txt50 + '" name="labels_soporte" hidden />' +
                    '<div class="file-field input-field col s12">' +
                    '<div class="btn-small" style="float:left;"> ' +
                    '<span>Examinar</span > ' +
                    '<input class="file_soporte';
                if (pp[i].oblig) {
                    input += ' nec';
                }
                input += 'name="files_soporte" id="file_' + pp[i].tsoporte + '" type= "file"> ' +
                    '</div>' +
                    '<div class="file-path-wrapper"> ' +
                    '<input class="file-path validate" type="text"> ' +
                    '</div>' +
                    '</div>';

                $("#div_soportes").append(input);
            }
        }
    });
}