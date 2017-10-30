(function ($) {
    $(function () {

        GetPersonas();

        $("#fecha-nac").datepicker({
            maxDate: 0,
            dateFormat: "d M, y",
            changeMonth: true,
            changeYear: true
        });

        $("#fecha-nac").datepicker($.datepicker.regional["es"]);

        $("#enviar-persona").click(function (e) {
            e.preventDefault();
            $('#form-persona').validator('validate');
            if (!$(this).hasClass("disabled")) {
                if (SetPersona()) {
                    GetPersonas();
                    $('#myModal').modal('hide');
                }
            }
        });

        $("#listado-personas").on("click", ".editar-persona", function (e) {
            e.preventDefault();
            var idPersona = $(this).data("id");
            GetPersona(idPersona);
            $('#myModal').modal('show');
        });

        $("#listado-personas").on("click", ".borrar-persona", function (e) {
            e.preventDefault();
            var idPersona = $(this).data("id");
            DeletePersona(idPersona);
            GetPersonas();
        });

        $('#myModal').on('hide.bs.modal', function () {
            LimpiaForm();
        });

        $('#myModal').on('show.bs.modal', function () {
            $('#form-persona').validator();
        });

    });
})(jQuery);

function LimpiaForm() {
    $('#form-persona').validator('destroy');

    $("#enviar-persona").removeData("id");
    $("#nombre").val(undefined);
    $("#apellido").val(undefined);
    $("#nro-doc").val(undefined);
    $("#fecha-nac").val(undefined);
    $("#calle").val(undefined);
    $("#numero").val(undefined);
}

function GetPersonas() {
    $.ajax({
        method: "POST",
        url: "Personas.aspx/GetPersonas",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (response) {
        if (response.d != null) {
            $("#listado-personas").html(response.d);
        }
    }).fail(function (xhr, status, error) {
        alert("Hubo un error");
    });
}

function GetPersona(idPersona) {
    $.ajax({
        method: "POST",
        url: "Personas.aspx/GetPersona",
        data: "{ idPersona:" + idPersona + " }",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (response) {
        if (response.d != null) {
            var persona = JSON.parse(response.d);
            $("#enviar-persona").data("id", persona.idPersona);
            $("#direccion").data("id", persona.Direccion.idDireccion);
            $("#nombre").val(persona.nombre);
            $("#apellido").val(persona.apellido);
            $("#nro-doc").val(persona.numeroDocumento);
            //$("#fecha-nac").val(persona.fechaNacimiento);
            $("#calle").val(persona.Direccion.calle);
            $("#numero").val(persona.Direccion.numero);

            $("#fecha-nac").val($.datepicker.formatDate('d M, y', new Date(persona.fechaNacimiento)));
        }
    }).fail(function (xhr, status, error) {
        alert("Hubo un error");
    });
}

function SetPersona() {
    var res;
    var persona = new Object();
    var direccion = new Object();
    var idPersona = $("#enviar-persona").data("id") == undefined ? 0 : $("#enviar-persona").data("id");
    var idDireccion = $("#direccion").data("id") == undefined ? 0 : $("#direccion").data("id");

    persona.idPersona = idPersona;
    direccion.idDireccion = idDireccion;
    persona.nombre = $("#nombre").val();
    persona.apellido = $("#apellido").val();
    persona.numeroDocumento = $("#nro-doc").val();
    persona.fechaNacimiento = $("#fecha-nac").val();
    direccion.calle = $("#calle").val();
    direccion.numero = $("#numero").val();
    persona.Direccion = direccion;

    $.ajax({
        method: "POST",
        url: "Personas.aspx/SetPersona",
        data: "{ persona:" + JSON.stringify(persona) + " }",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (response) {
        if (response.d == "") {
            res = true;
        }
        else {
            alert(response.d);
            res = false;
        }
    }).fail(function (xhr, status, error) {
        alert("Hubo un error");
        res = false;
    });

    return res;
}

function DeletePersona(idPersona) {
    $.ajax({
        method: "POST",
        url: "Personas.aspx/DeletePersona",
        data: "{ idPersona:" + idPersona + " }",
        async: false,
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    }).done(function (response) {
        if (response.d != null) {

        }
    }).fail(function (xhr, status, error) {
        alert("Hubo un error");
    });
}