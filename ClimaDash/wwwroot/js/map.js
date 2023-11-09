function initializeMap(layer, zoomLevel, x, y) {
    var map = L.map('map').setView([x, y], zoomLevel);

    L.tileLayer(`https://tile.openweathermap.org/map/${layer}/{zoomLevel}/{x}/{y}.png?appid=2b71d84695ee6bd55a61a5c20029c677`, {
        maxZoom: 18,
        attribution: 'Your attribution here'
    }).addTo(map);
}