$(document).ready(function () {
    var ban = 0;
    $('body').on('keydown', '.input_dc', function (e) {
        if (e.keyCode == 110 || e.keyCode == 190) {
            if ($(this).val().indexOf('.') != -1) {
                e.preventDefault();
            }
        }
        else {  // Allow: backspace, delete, tab, escape, enter and .
            if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190]) !== -1 ||
                // Allow: Ctrl+A, Command+A
                (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
                // Allow: home, end, left, right, down, up
                (e.keyCode >= 35 && e.keyCode <= 40)) {
                // let it happen, don't do anything

                return;
            }

            // Ensure that it is a number and stop the keypress
            if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
                e.preventDefault();
            }
        }

    });

    //Para validar los decimales del costo unitario
    $('body').on('focusout', '.input_dc', function () {
        var xx = $(this).val();

        if (xx != '') {
            //Hace la conversion a 2 decimales
            $(this).val(parseFloat(xx).toFixed(2));
        }
        else {
            $(this).val($(this).val());
        }
    });

    // Para que no ingresen letras en la fecha
    $('body').on('keydown', '.input_fe', function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 191]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        //Para slash
        if (e.keyCode === 111 || (e.shiftKey & e.keyCode === 55)) {
            return;
        }

        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }
    });

    //Para validar las fechas
    $('body').on('focusout', '.input_fe', function () {
        var xx = $(this).val();
        if (xx != "") {
            if (isDate(xx) === true) {
                //alert("true");
            } else {
                //alert("false");
                $(this).val("");
            }
        } else {
            return;
        }
    });

    // Para que no ingresen letras en la fecha del checkfactura
    $('body').on('keydown', '.fv', function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110, 190, 191]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        //Para slash
        if (e.keyCode === 111 || (e.shiftKey & e.keyCode === 55)) {
            return;
        }

        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }


    });

    // Para que no ingresen letras en el campo de proveedor
    $('body').on('keydown', '.prv', function (e) {
        // Allow: backspace, delete, tab, escape, enter and .
        if ($.inArray(e.keyCode, [46, 8, 9, 27, 13, 110]) !== -1 ||
            // Allow: Ctrl+A, Command+A
            (e.keyCode === 65 && (e.ctrlKey === true || e.metaKey === true)) ||
            // Allow: home, end, left, right, down, up
            (e.keyCode >= 35 && e.keyCode <= 40)) {
            // let it happen, don't do anything
            return;
        }
        // Ensure that it is a number and stop the keypress
        if ((e.shiftKey || (e.keyCode < 48 || e.keyCode > 57)) && (e.keyCode < 96 || e.keyCode > 105)) {
            e.preventDefault();
        }


    });
    //Para validar las fechas
    $('body').on('focusout', '.fv', function () {
        var xx = $(this).val();
        if (xx != "") {
            if (isDate(xx) === true) {
                //alert("true");
            } else {
                alert("Fecha Erronea");
                //$(this).val("");
            }
        } else {
            return;
        }
    });

    function isDate(xx) {
        var currVal = xx;
        if (currVal == '')
            return false;

        var rxDatePattern = /^(\d{1,2})(\/|-)(\d{1,2})(\/|-)(\d{4})$/; //Declare Regex
        var dtArray = currVal.match(rxDatePattern); // is format OK?

        if (dtArray == null)
            return false;

        //Checks for mm/dd/yyyy format.
        dtMonth = dtArray[3];
        dtDay = dtArray[1];
        dtYear = dtArray[5];

        if (dtMonth < 1 || dtMonth > 12) return false;

        else if (dtDay < 1 || dtDay > 31) return false;
        else if ((dtMonth == 4 || dtMonth == 6 || dtMonth == 9 || dtMonth == 11) && dtDay == 31) return false;
        else if (dtMonth == 2) {
            var isleap = (dtYear % 4 == 0 && (dtYear % 100 != 0 || dtYear % 400 == 0));
            if (dtDay > 29 || (dtDay == 29 && !isleap)) return false;
        }
        return true;
    }
    function parseDMY(value) {
        var date = value.split("/");
        var d = parseInt(date[0], 10),
            m = parseInt(date[1], 10),
            y = parseInt(date[2], 10);
    }
});