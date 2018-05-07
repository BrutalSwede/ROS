var geocoder;
var map;
var marker; // Define marker here so we don't have more than one.

function initialize() {
    geocoder = new google.maps.Geocoder();
    var latlng = new google.maps.LatLng(57.678691, 12.001445);
    var mapOptions = {
        zoom: 8,
        center: latlng
    }
    map = new google.maps.Map(document.getElementById('map'), mapOptions);

    // Enable autocomplete
    var input = document.getElementById('Address');
    var options = {
        types: [],
        componentRestrictions: { country: 'se' } // only suggest places in Sweden
    };

    var autocomplete = new google.maps.places.Autocomplete(input, options);

    // Override the form submit when you press 'Enter' whilst in the 'Address' input field
    input.addEventListener('keydown', function (e) {

        var key = e.which || e.key;

        if (key == 13) { // Key# 13 is the 'Enter' key
            e.preventDefault();
            codeAddress();
        }
    });
}

function codeAddress() {
    var address = document.getElementById('Address').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);
            map.setZoom(15);

            // Only allow one marker to be on the map at any one time
            if (marker) {
                marker.setPosition(results[0].geometry.location);
            }
            else {
                marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location
                });
            }

        } else {
            alert('Geocode was not successful for the following reason: ' + status);
        }
    });
}
