function mensajeError(msg) {
    alert(msg);
}

// Devuelve la cantidad de dias en un mes en un año seleccionados
function diasEnMesAnio(mes, anio)
{
    var esBisiesto = (anio % 4 == 0 && anio % 100 != 0) || (anio % 100 == 0 && anio % 400 == 0);
    return [31, (esBisiesto ? 29 : 28), 31, 30, 31, 30, 31, 31, 30, 31, 30, 31][mes - 1];
}

// Rellena el DropDownList ddlDia con los dias del mes en el año seleccionados
function onCambiaFecha()
{
    var dias = document.getElementById("ddlDia");
    var mes = document.getElementById("ddlMes");
    var anio = document.getElementById("ddlAnio");

    var mesElegido = mes.options[mes.selectedIndex].value;
    var anioElegido = anio.options[anio.selectedIndex].value;
    var nroDias = diasEnMesAnio(mesElegido, anioElegido);

    while (dias.options.length > 0)
    {
        dias.remove(0);
    }

    for (var i = 1; i < nroDias + 1; i++)
    {
        var option = document.createElement('option');
        option.text = option.value = i;
        dias.add(option, 0);
    }
}

