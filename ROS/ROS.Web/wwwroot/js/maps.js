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

    var autocomplete = google.maps.places.Autocomplete(input);

    autocomplete.addListener('place_changed', function () {
        if (marker) {
            marker.setVisible(false);
        } 
        var place = autocomplete.getPlace();
        if (!place.geometry) {
            window.alert("No details for " + place.name + "!");
            return;
        }
    });

    if (place.geometry.viewport) {
        map.fitBounds(place.geometry.viewport);
    } else {
        map.setCenter(place.geometry.location);
        map.setZoom(17);  // Why 17? Because it looks good.
    }
    marker.setPosition(place.geometry.location);
    marker.setVisible(true);

    var address = '';
    if (place.address_components) {
        address = [
            (place.address_components[0] && place.address_components[0].short_name || ''),
            (place.address_components[1] && place.address_components[1].short_name || ''),
            (place.address_components[2] && place.address_components[2].short_name || '')
        ].join(' ');
    }
}

function codeAddress() {
    var address = document.getElementById('Address').value;
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status == 'OK') {
            map.setCenter(results[0].geometry.location);

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
