$('#btn_agregar').click(function (e) {
    e.preventDefault();

    if ($.trim($('#txt_pctmin').val()) == "" || $.trim($('#txt_pctmax').val()) == "" || $.trim($('#txt_comision').val()) == "") {
        mostarError('Debe ingresar los porcentajes requeridos');
        return;
    }

    comision_regexp = /^-?(?:\d+|\d{1,3}(?:[\s,]\d{3})+)(?:[,]\d+)?$/;

    var comisionVal = $('#txt_comision').val();

    if (!(comision_regexp.test(comisionVal))) {
        mostarError('La comisión ingresada no es válida');
        return;
    }

    var pct_min = $('#txt_pctmin').val(),
        pct_max = $('#txt_pctmax').val(),
        comision = $('#txt_comision').val(),
        detallesBody = $('#tbl_detallesEscala tbody');

    var escalaItem = '<tr><td>' + pct_min + '</td>' +
        '<td>' + pct_max + '</td>' +
        '<td>' + comision + '</td></tr>';

    detallesBody.append(escalaItem);

    limpiar();

});

function limpiar() {
    mostarError('');
    $('#txt_pctmin').val('');
    $('#txt_pctmax').val('');
    $('#txt_comision').val('');
}

function mostarError(error) {
    $('#mensaje_validacion').html(error);
}

function GuardarEscala(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Mantenimientos/Escala/Registrar',
        data: data,
        success: function (response) {
            if (response.success) {
                location.href = '/Mantenimientos/Escala';
            } else {
                mostarError(response.responseText);
            }
        },
        error: function () {
            alert('Ocurrió un error al guardar la escala');
        }
    });
}


$('#btn_guardar').click(function (e) {
    e.preventDefault();

    if ($.trim($('#txt_descripcion').val()) == "") {
        mostarError('Debe ingresar una descripción');
        return;
    }


    var detallesArray = [];
    detallesArray.length = 0;

    $.each($('#tbl_detallesEscala tbody tr'), function () {
        detallesArray.push({
            PCTMinimo: $(this).find('td:eq(0)').html(),
            PCTMaximo: $(this).find('td:eq(1)').html(),
            PCTComision: $(this).find('td:eq(2)').html()
        });
    });

    if (detallesArray.length == 0) {
        mostarError('No hay datos en la escala');
        return;
    }

    mostarError('');
    var data = JSON.stringify({
        descripcion: $('#txt_descripcion').val(),
        detalles: detallesArray
    });

    GuardarEscala(data);

});