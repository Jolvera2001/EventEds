let map;
let marker;

window.initializeMap = function(elementId, accessToken, lat, lng) {
    mapboxgl.accessToken = accessToken;
    
    map = new mapboxgl.Map({
        container: elementId,
        style: 'mapbox://styles/mapbox/streets-v11',
        center: [lat, lng],
        zoom: 15,
    });
    
    marker = new mapboxgl.Marker({
        draggable: true,
    })
        .setLngLat([lat, lng])
        .addTo(map);

    marker.on('dragend', () => {
        const lngLat = marker.getLngLat();
        DotNet.invokeMethodAsync('EventEds.Client', 'OnLocationChanged', lngLat.lat, lngLat.lng);
    });

    return true;
};

window.updateMarker = function (lat, lng) {
    if (marker) {
        marker.setLngLat([lng, lat]);
        map.setCenter([lng, lat]);
    }
};