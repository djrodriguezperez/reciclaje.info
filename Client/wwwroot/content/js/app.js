
///Project Reciclaje.info v1.0 
///TFG 2022
///


//// Global Script Functions 
(function () {
    var map;
    function inicializarMapa() {
        var divMap = document.getElementById('map');

        if (divMap != undefined) {
            map = new L.map(divMap, {
                center: { lat: 40.5, lng: -3.7 },
                zoom: 11,
            });

            L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                attribution: 'Map data &copy; <a href="https://openstreetmap.org">OpenStreetMap</a>, Reciclaje.info © 2020',
                maxZoom: 19
            }).addTo(map);
        } else {
            divMap.innerText='Opps!! No ha sido posible cargar el mapa de geolocalización, actualice la aplicación o espere unos minutos y vuelva a intentarlo.'
        }




        // Obtener ubicación dispositivo

        //loadLatLong(txtLatitud.GetValue(), txtLongitud.GetValue(), false);

    }




    window.onInicializarMapa = inicializarMapa;

})();

function csvJSON(csv) {

    var lines = csv.split(";");

    var result = [];

    // NOTE: If your columns contain commas in their values, you'll need
    // to deal with those before doing the next step 
    // (you might convert them to &&& or something, then covert them back later)
    // jsfiddle showing the issue https://jsfiddle.net/
    var headers = lines[0].split(",");

    for (var i = 1; i < lines.length; i++) {

        var obj = {};
        var currentline = lines[i].split(",");

        for (var j = 0; j < headers.length; j++) {
            obj[headers[j]] = currentline[j];
        }

        result.push(obj);

    }

    //return result; //JavaScript object
    return JSON.stringify(result); //JSON
}




