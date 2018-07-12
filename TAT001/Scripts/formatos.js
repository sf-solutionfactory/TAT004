

function toNum(string) {
    if (string !== "" && string != undefined) {
        var _miles = $("#miles").val();
        var _decimales = $("#dec").val();
        string = string.replace('$', '');
        string = string.replace('%', '');
        string = string.replace(_miles, '');
        string = string.replace(_decimales, '.');
    } else {
        string = "0.00";
    }
    return string;
}

function toShow(string) {
    var _miles = $("#miles").val();
    var _decimales = $("#dec").val();
    var xx = parseFloat(string).toFixed(2);
    xx = xx.replace('.', _decimales);
    //string = xx.toString().replace(/\B(?=(\d{3})+(?!\d))/g, _miles) + '%';
    if (xx != '') {
        if (_decimales === '.') {
            //Hace la conversion a 2 decimales
            var _xv = xx.replace(',', '');
            xx = _xv;
            string = ("$" + parseFloat(xx).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        } else if (_decimales === ',') {
            var _xv = xx.replace('.', '');
            xx = _xv.replace(',', '.');
            var _xpf = parseFloat(xx.replace(',', '.')).toFixed(2);
            _xpf = _xpf.replace('.', ',');
            string = ("$" + _xpf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, "."));
        }
    }
    else {
        $(this).val("$ 0" + _decimales + "00");
    }
    return string;
}

function toShowPorc(string) {
    var _miles = $("#miles").val();
    var _decimales = $("#dec").val();
    var xx = parseFloat(string).toFixed(2);
    xx = xx.replace('.', _decimales);
    //string = xx.toString().replace(/\B(?=(\d{3})+(?!\d))/g, _miles) + '%';
    if (xx != '') {
        if (_decimales === '.') {
            //Hace la conversion a 2 decimales
            var _xv = xx.replace(',', '');
            xx = _xv;
            string = (parseFloat(xx).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",") + '%');
        } else if (_decimales === ',') {
            var _xv = xx.replace('.', '');
            xx = _xv.replace(',', '.');
            var _xpf = parseFloat(xx.replace(',', '.')).toFixed(2);
            _xpf = _xpf.replace('.', ',');
            string = (_xpf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ".") + '%');
        }
    }
    else {
        $(this).val("$ 0" + _decimales + "00");
    }
    return string;
}

function toShowNum(string) {
    var _miles = $("#miles").val();
    var _decimales = $("#dec").val();
    var xx = parseFloat(string).toFixed(2);
    xx = xx.replace('.', _decimales);
    //string = xx.toString().replace(/\B(?=(\d{3})+(?!\d))/g, _miles) + '%';
    if (xx != '') {
        if (_decimales === '.') {
            //Hace la conversion a 2 decimales
            var _xv = xx.replace(',', '');
            xx = _xv;
            string = (parseFloat(xx).toFixed(2).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        } else if (_decimales === ',') {
            var _xv = xx.replace('.', '');
            xx = _xv.replace(',', '.');
            var _xpf = parseFloat(xx.replace(',', '.')).toFixed(2);
            _xpf = _xpf.replace('.', ',');
            string = (_xpf.toString().replace(/\B(?=(\d{3})+(?!\d))/g, "."));
        }
    }
    else {
        $(this).val("$ 0" + _decimales + "00");
    }
    return string;
}