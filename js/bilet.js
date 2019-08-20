$(document).ready(function () {
    var lista = Array();
    $("span").click(function () {

        if ($(this).css("color") === "rgb(255, 0, 0)") {
            return;
        }

        if ($(this).css("color") === "rgb(0, 128, 0)") {
            if (lista.length > 4) {
                alert('Możesz zamowic tylko 5 biletow');
                return;
            } 
            

            var numer = $(this).attr("title");
            lista.push(numer);
            $(this).css('color', 'black');
        } else {
            var numer = $(this).attr("title");

            var i = lista.indexOf(numer);

            if (i != -1) {
                lista.splice(i, 1);
            }

            $(this).css('color', 'green');
            delete lista[numer];
        }
    });

    $("#submitit").click(function () {
        if (lista.length === 0) {
            alert('Musisz wybrać miejsce');
            return false;
        }
        var link;
        link += "../../../finalizujBilet"
        link += "?miejsca="
        lista.forEach(function (entry) {
            link += entry + ",";
        });

        var idseansu = ($('#idseansu').val());


        link += '&idseansu=' + idseansu;


        window.location.replace(link);
    });
});