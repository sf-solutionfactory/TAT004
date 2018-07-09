
function asignarSolicitud(num, num2) {
    $.ajax({
        type: "POST",
        url: 'getSolicitud',
        dataType: "json",
        data: { "num": num, "num2": num2 },

        success: function (data) {

            if (data !== null || data !== "") {
                if (data.S_NUM == "") {
                    var num2 = $('#monto_doc_md').val();
                    $('#s_montob').text(num2);
                    $('#s_montop').text("-");
                    $('#s_montoa').text("-");
                    $('#s_rema').text(num2);
                    $('#s_rema').text("-");//RSG 09.07.2018
                    $('#s_impa').text("-");
                    $('#s_impb').text("-");
                    $('#s_impc').text("-");
                    $('#s_ret').text("-");
                    $('#s_total').text(num2);
                }
                else {
                    $('#s_montob').text(data.S_MONTOB);
                    $('#s_montop').text(data.S_MONTOP);
                    $('#s_montoa').text(data.S_MONTOA);
                    if (data.S_REMA < 0) {
                        $('#s_rema').text(data.S_REMA);
                        //$('#s_color').style.color;
                        ////document.getElementById("s_rema").style.color = "red";
                        ////document.getElementById("s_rema2").style.color = "red";
                        document.getElementById("a4").classList.add("red");
                        document.getElementById("a4").classList.add("white-text");
                    }
                    else {
                        $('#s_rema').text(data.S_REMA);
                        document.getElementById("a4").classList.remove("red");
                        document.getElementById("a4").classList.remove("white-text");
                    }
                    $('#s_impa').text(data.S_IMPA);
                    $('#s_impb').text(data.S_IMPB);
                    $('#s_impc').text(data.S_IMPC);
                    $('#s_ret').text(data.S_RET);
                    $('#s_total').text(data.S_TOTAL);
                }
            }
        },
        error: function (xhr, httpStatusMessage, customErrorMessage) {
            M.toast({ html: httpStatusMessage });
        },
        async: false
    });
}
