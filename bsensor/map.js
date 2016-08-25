var mapOptions = {
    center: new google.maps.LatLng(45.50934, -122.68149),
    zoom: 13,
    mapTypeId: google.maps.MapTypeId.ROADMAP
};

var map = new google.maps.Map(document.getElementById('map'), mapOptions);

var cursor_marker = null;

var locations = [
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 },
    { lat: 45.52755001, lng: -122.6755583 }];

var bus_stops = [];

for (var i = 0; i < locations.length; i++)
{
    addMarker(locations[i]);
}

// Adds a marker to the map and push to the array.
function addMarker(location) {
    var marker = new google.maps.Marker( { position: location, map: map } );
    bus_stops.push(marker);
}

var markerOptions = {
    position: new google.maps.LatLng({0},{1}), title:'Start', label: 'S'{2}", lat0, lng0, "};
    var marker = new google.maps.Marker(markerOptions);
    marker.setMap(map);

    markerOptions = {
position: new google.maps.LatLng({0},{1}), title:'Finish', label:'F'{2}", lat1, lng1, "};
marker = new google.maps.Marker(markerOptions);
marker.setMap(map);

