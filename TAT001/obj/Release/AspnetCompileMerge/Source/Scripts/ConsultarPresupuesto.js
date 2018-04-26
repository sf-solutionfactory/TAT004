$(document).ready(function () {
    var arrFiltr = ['', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', '', ''];


    $('.collapsible').collapsible();
    try {
        var table = $('#table').DataTable({
            //scrollY: "70vh",
            dom: "Bfrtip",
            scrollX: "10vh",
            language: {
                lengthMenu: "Display _MENU_ records per page",
                zeroRecords: "No se encontraron datos",
                info: "Página _PAGE_ de _PAGES_",
                infoEmpty: "No hay datos",
                infoFiltered: "(Filtrado de _MAX_ líneas totales)"
            },
            fixedColumns: true,
            fixedColumns: {
                leftColumns: 6
            },
            columnDefs: [
                {
                    //targets: [0, 1, 2],
                    className: 'mdl-data-table__cell--non-numeric'
                }
            ],

            initComplete: function () {
                this.api().columns([0]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#canalFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([1]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#desCanFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([2]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#totCanFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([3]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#bannerFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([4]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#desBanFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([5]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#totBannerFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([6]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#VVX17Fltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([7]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#CSHDCFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([8]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#RECUNFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([9]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#DSTRBFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([10]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#OTHTAFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([11]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#ADVERFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([12]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#CORPMFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([13]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#POPFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([14]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#PMVARFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([15]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#CONPRFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([16]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#RSRDVFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([17]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#SPAFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                this.api().columns([18]).every(function () {
                    var column = this;
                    //console.log(column);
                    var select = $("#FREEGFltr");
                    column.data().unique().sort().each(function (d, j) {
                        select.append('<option selected value="' + d + '">' + d + '</option>')
                    });
                });
                try {
                    this.api().columns([19]).every(function () {
                        var column = this;
                        //console.log(column);
                        var select = $("#consumidoFltr");
                        column.data().unique().sort().each(function (d, j) {
                            select.append('<option selected value="' + d + '">' + d + '</option>')
                        });
                    });
                    this.api().columns([20]).every(function () {
                        var column = this;
                        //console.log(column);
                        var select = $("#disponibleFltr");
                        column.data().unique().sort().each(function (d, j) {
                            select.append('<option selected value="' + d + '">' + d + '</option>')
                        });
                    });
                } catch (e) {

                }
            },
            //    column: [
            //        { data: "Canal" },
            //        { data: "Total Canal" },
            //        { data: "Banner" },
            //        { data: "PPTO Banner", className: "sum" },
            //        { data: "VVX17 - Commercial Discounts", className: "sum" },
            //        { data: "CSHDC - Cash Discounts", className: "sum" },
            //        { data: "RECUN - Unsaleables", className: "sum" },
            //        { data: "DSTRB - Distribution Commission", className: "sum" },
            //        { data: "OTHTA - Logistic Discount", className: "sum" },
            //        { data: "ADVER - Trade Promotion-Other", className: "sum" },
            //        { data: "CORPM - Booklets and Sponsorship", className: "sum" },
            //        { data: "POP - Store openings and Info Exchange", className: "sum" },
            //        { data: "PMVAR - Growth Program", className: "sum" },
            //        { data: "CONPR - Everyday Low Price", className: "sum" },
            //        { data: "RSRDV - Rollbacks", className: "sum" },
            //        { data: "SPA - Cleareance", className: "sum" },
            //        { data: "FREEG - Free Goods", className: "sum" },
            //        { data: "Consumido", className: "sum" },
            //        { data: "PPTO Disponible", className: "sum" }
            //],
            "footerCallback": function (row, data, start, end, display) {//suma de totales por columna en pie de tabla footer
                var api = this.api();
                var intVal = function (i) {
                    return typeof i === 'string' ?
                        i.replace(/[\$,]/g, '') * 1 :
                        typeof i === 'number' ?
                            i : 0;
                };
                var currency = function (value) {
                    return value.replace(/\D/g, "")
                        .replace(/\B(?=(\d{3})+(?!\d)\.?)/g, ",");
                };
                for (var j = 0; j < 21; j++) {
                    if (j > 4) {
                        try {
                            api.columns([j], { page: 'current' }).every(function () {
                                var sum = this
                                    .data()
                                    .reduce(function (a, b) {
                                        return intVal(a) + intVal(b);
                                    }, 0);
                                $(this.footer()).html("$" + currency(sum.toString()));
                            });
                        } catch (e) {

                        }
                    }
                }
            }
        });

        $('[name="filtro"]').on('change', function () {//filtro de busqueda por columna
            var col = 0;
            col = $(this).attr('col');
            var search = new Array();
            var sech = ""
            $(this, ' option:selected').each(function () {
                search = $(this).val();
            });
            sech = search.join('|');
            table.column(col).search(sech, true, false).draw();
        });
        //$('[class="input-field col s2 f"]').on('dblclick', function () { //marcar y desmarcar selec con doble clic
        //    var id = '#' + $(this).attr('name');
        //    var col = $(id).attr('col');
        //    id += ' option';
        //    if (arrFiltr[col] == '') {
        //        $(id).each(function () {
        //            $(this).removeAttr("selected");
        //        });
        //        arrFiltr[col] = 'X';
        //    } else {
        //        $(id).each(function () {
        //            $(this).attr('selected', '');
        //        });
        //        arrFiltr[col] = '';
        //    }
        //    $('select').select();

        //});
        //$('label').attr('unselectable', 'on') /*quitar seleccion de texto por mouse */
        //     .css({
        //         '-moz-user-select': '-moz-none',
        //         '-moz-user-select': 'none',
        //         '-o-user-select': 'none',
        //         '-khtml-user-select': 'none', /* you could also put this in a class */
        //         '-webkit-user-select': 'none',/* and add the CSS class here instead */
        //         '-ms-user-select': 'none',
        //         'user-select': 'none'
        //     }).bind('selectstart', function () { return false; });

        var a = $('#selecc').val();
        table.page.len(a).draw();
        $('#selecc').on('change', function () {
            table.page.len(this.value).draw();
        });

        $('input.global_filter').on('keyup click', function () {
            filterGlobal();
        });
        $('select').select();
        M.Select.init($('select'), []);

        $('#chkfiltro').on('click', function () {
            if ($(this).is(':checked')) {
                // Hacer algo si el checkbox ha sido seleccionado
                $('[name="filtro"] option').each(function () {
                    $(this).attr('selected', '');
                });
                selectAll();
            } else {
                // Hacer algo si el checkbox ha sido deseleccionado
                $('[name="filtro"] option').each(function () {
                    $(this).removeAttr("selected");
                });
                selectNone();
            }
            var elem = document.getElementsByName("periodocpt")
            //instance = M.Select.init(elem, []);
            //$('select').select();
        });
        function selectNone() {
            $('[name="filtro"] option:selected')
                .not(':disabled')
                .prop('selected', false);
            $('.dropdown-content.multiple-select-dropdown input[type="checkbox"]:checked')
                .not(':disabled')
                .prop('checked', '');
            //$('.dropdown-content.multiple-select-dropdown input[type='checkbox']:checked').not(':disabled').trigger('click');
            var values = $(
                '.dropdown-content.multiple-select-dropdown input[type="checkbox"]:disabled'
            )
                .parent()
                .text();
            $('input.select-dropdown').val(values);
            //console.log($('select').val());
            $('.f > .select-wrapper > .select-dropdown .toggle').toggle();
            $('[name="filtro"]').trigger('change');

        }

        function selectAll() {
            $('[name="filtro"] option:not(:disabled)').each(function () {
                var id = '[name=' + $(this).parent().parent().parent().attr('name') + ']';
                $(id + ' select option:not(:disabled)')
                    .not(':selected')
                    .prop('selected', true);
                $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:not(:checked)'
                )
                    .not(':disabled')
                    .prop('checked', 'checked');
                //$('.dropdown-content.multiple-select-dropdown input[type='checkbox']:not(:checked)').not(':disabled').trigger('click');
                var values = $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:checked'
                )
                    .not(':disabled')
                    .parent()
                    .map(function () {
                        return $(this).text();
                    })
                    .get();
                $(id + ' input.select-dropdown').val(values.join(', '));
                //console.log($('select').val());
                $(id + ' > .select-wrapper > .select-dropdown .toggle').toggle();
            });
            $('[name="filtro"]').trigger('change');
        }
    } catch (e) {

    }
    $("select").select();

    $(".f > .select-wrapper > .select-dropdown").prepend(
        '<li class="toggle selectnone"><span><label></label>Select none</span></li>'
    );
    $(".f > .select-wrapper > .select-dropdown").prepend(
        '<li style="display:none" class="toggle selectall"><span><label></label>Select all</span></li>'
    );
    $(".f > .select-wrapper > .select-dropdown .selectall").on(
        "click",
        function () {
            var id = '[name=' + $(this).parent().parent().parent().attr('name') + ']';
            $(id + ' option:not(:disabled)')
                .not(':selected')
                .prop('selected', true);

            $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:not(:checked)'
            )
                .not(':disabled')
                .prop('checked', 'checked');
            //$('.dropdown-content.multiple-select-dropdown input[type='checkbox']:not(:checked)').not(':disabled').trigger('click');
            var values = $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:checked')
                .not(':disabled')
                .parent()
                .map(function () {
                    return $(this).text();
                })
                .get();
            $(id + ' input.select-dropdown').val(values.join(', '));
            $(id + '> .select-wrapper > .select-dropdown .toggle').toggle();
            $('[name="filtro"]').trigger('change');
        }
    );
    $(".f > .select-wrapper > .select-dropdown .selectnone").on(
        "click",
        function () {
            var id = '[name=' + $(this).parent().parent().parent().attr('name') + ']';
            $(id + ' option:selected')
                .not(':disabled')
                .prop('selected', false);
            $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:checked')
                .not(':disabled')
                .prop('checked', '');
            //$('.dropdown-content.multiple-select-dropdown input[type='checkbox']:checked').not(':disabled').trigger('click');
            var values = $(id + ' .dropdown-content.multiple-select-dropdown input[type="checkbox"]:disabled')
                .parent()
                .text();
            $(id + ' input.select-dropdown').val(values);
            $(id + ' > .select-wrapper > .select-dropdown .toggle').toggle();
            $('[name="filtro"]').trigger('change');
        }
    );
    $('th').css('text-align', 'center');//porque no se deja de otra forma
    $('td').css('text-align', 'center');
});