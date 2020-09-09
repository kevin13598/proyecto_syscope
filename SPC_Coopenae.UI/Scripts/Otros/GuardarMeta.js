var MetasProductos;

//se crea la pagina y carga los tipos de productos
$(document).ready(function () {
    //toma el div donde se va a poner los tipos
    var div_general = $('#cont_gnrl_prods select');

    var opciones_prods = "";
    for (var i = 0; i < json_tipoProductos.length; i++) {
        opciones_prods += "<option value=" + json_tipoProductos[i].Value + ">" + json_tipoProductos[i].Text + "</option>";
    }

    div_general.append(opciones_prods);

    MetasProductos = [];
});

$('#btn_agregar').click(function (e) {
    e.preventDefault();

    agregarMetaProductos();

});

$('#btn_guardar').click(function (e) {
    e.preventDefault();

    CrearMeta();

});

function mostarError(error) {
    $('#mensaje_validacion').html(error);
}

function mostarErrorProdutos(error) {
    $('#mensaje_validacion_prod').html(error);
}

function agregarMetaProductos() {

    //tomar los valores
    var valores_seleccionados = $('#lista_prods').val();

    var cantidad_productos = $('#txt_cant_Prod').val(),
        valorIDP_productos = $('#txt_IDP_Prod').val();

    //Validacion de campos numeros y que haya uno al menos en la lista
    if ($.trim(cantidad_productos) == "" || $.trim(valorIDP_productos) == "") {
        mostarErrorProdutos('Debe ingresar la cantidad y el porcentaje correspondiente');
        return;
    }

    if (valores_seleccionados.length == 0) {
        mostarErrorProdutos("No ha seleccionado ningun producto");
        return;
    }

    //Valida que la cantidad sea mayor que 0
    if ($('#txt_cant_Prod').val() <= 0) {
        mostarErrorProdutos('La cantidad debe ser mayor que cero');
        return;
    }
    //Valida que el IDP esté entre 0 y 100
    if (($('#txt_IDP_Prod').val() <= 0) || ($('#txt_IDP_Prod').val() > 100)) {
        mostarErrorProdutos('El IDP debe ser de 0 a 100');
        return;
    }

    //Este segmento toma los textos datos para mostrarlos en otra seccion
    var divMostrar = $('#mostrar_meta_prods_agregados');

    var txtMostrar = '<div class="panel panel-default"><div class="panel-body"><table><tr><td><b>Productos:</b></td></tr>';             

    $('#lista_prods :selected').each(function () {
        txtMostrar += "<tr><td>" + $(this).text() + "</td></tr>";
    });

    txtMostrar += '</table><br>' +
                  '<b>Cantidad Meta:</b> ' + cantidad_productos + '<br>' +
                  '<b>Porcentaje de IDP Meta:</b> ' + valorIDP_productos + '%' +
                  '</div></div>';

    divMostrar.append(txtMostrar);
    $("#lista_prods :selected").remove();

    MetasProductos.push({
        MetaCantidad: cantidad_productos,
        ValorIDP: valorIDP_productos,
        TipoProductos: valores_seleccionados
    });
    
    limpiar();
}

function limpiar() {
    mostarError('');
    mostarErrorProdutos('');
    $('#txt_IDP_Prod').val('');
    $('#txt_cant_Prod').val('');
}

function CrearMeta() {

    //Tomar loa valores
    var descripcion = $('#txt_descripcion').val(),
        escala = $('#slt_escala').val(),
        salario = $('#slt_salario').val(),
        metaCreditos = $('#txt_metaCred').val(),
        idpCreditos = $('#txt_IDPCreditos').val(),
        metaCDP = $('#txt_metaCDP').val(),
        idpCDP = $('#txt_IDP_CDP').val();

    //Validar
    if ($.trim(escala) == "" || $.trim(salario) == "" || $.trim(descripcion) == "" || $.trim(metaCreditos) == "" || $.trim(metaCDP) == "" || $.trim(idpCreditos) == "" || $.trim(idpCDP) == "") {
        mostarError('Debe ingresar todos los datos solicitados');
        return;
    }

    //Sumar el IDP
    var totalIDP = 0;
    totalIDP += Number(idpCreditos) + Number(idpCDP);
    if (MetasProductos.length > 0) {
        $.each(MetasProductos, function (index, value) {
            totalIDP += Number(value.ValorIDP);
        });
    }

    if (totalIDP != 100) {
        mostarError('Error. El total de todos los IDP ingresados debe ser 100');
        return;
    }

    mostarError('');

    //Crear objeto a enviar
    var data = JSON.stringify({
        _meta: {
            Descripcion: descripcion,
            Escala: escala,
            Salario: salario
        },
        _metaCredito: {
            MetaColocacion: metaCreditos,
            ValorIDP: idpCreditos
        },
        _metaCDP: {
            Metacdp: metaCDP,
            ValorIDP:idpCDP
        },
        _metaProducto: MetasProductos
    });

    //Enviar a guardaMeta
    guardaMeta(data);

}

function guardaMeta(data) {
    return $.ajax({
        contentType: 'application/json; charset=utf-8',
        dataType: 'json',
        type: 'POST',
        url: '/Mantenimientos/Meta/Registrar',
        data: data,
        success: function (response) {
            if (response.success) {
                location.href = '/Mantenimientos/Meta';
            } else {
                mostarError(response.responseText);
            }
        },
        error: function () {
            alert('Ocurrió un error al guardar la escala');
        }
    });
}