
///Project Reciclaje.info v1.0 
///TFG 2022
///


//// Create global script functions 
(function () {
    let map;
    let currentPosition = null;
    function tryGetCurrentPosition() {

        if ('geolocation' in navigator) {
            window.navigator.geolocation.getCurrentPosition(loadPositionMap, showError);
        } else {
           
        }


        
    }
    function inicializarMapa() {
        try {
            let divMap = document.getElementById('map');

            if (divMap != undefined) {
                map = new L.map(divMap, {
                    center: { lat: position.coords.latitude, lng: position.coords.longitude },
                    zoom: 11,
                });

                L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
                    attribution: 'Map data &copy; <a href="https://openstreetmap.org">OpenStreetMap</a>, Reciclaje.info © 2020',
                    maxZoom: 19
                }).addTo(map);
            } else {
                divMap.innerText = 'Opps!! No ha sido posible cargar el mapa de geolocalización, actualice la aplicación o espere unos minutos y vuelva a intentarlo.'
            }
        } catch (e) {
            alert(e.error);
        }
    }

    function showError(error) {
        switch (error.code) {
            case error.PERMISSION_DENIED:
                x.innerHTML = "User denied the request for Geolocation."
                break;
            case error.POSITION_UNAVAILABLE:
                x.innerHTML = "Location information is unavailable."
                break;
            case error.TIMEOUT:
                x.innerHTML = "The request to get user location timed out."
                break;
            case error.UNKNOWN_ERROR:
                x.innerHTML = "An unknown error occurred."
                break;
        }
    }


    window.tryGetCurrentPosition = tryGetCurrentPosition;
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




