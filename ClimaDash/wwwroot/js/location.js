function getLocation() {
    return new Promise(function (resolve, reject) {
        if (navigator.geolocation) {
            navigator.geolocation.getCurrentPosition(function (position) {
                var latitude = position.coords.latitude;
                var longitude = position.coords.longitude;
                DotNet.invokeMethodAsync('ClimaDash', 'LocationReceived', latitude, longitude)
                    .then(function (response) {
                        resolve(response);
                    })
                    .catch(function (error) {
                        reject(error);
                    });
            }, function (error) {
                if (error.code === error.PERMISSION_DENIED) {
                    window.alert("PERMISSION DENIED");
                } else {
                    window.alert("An error occurred while trying to get your location.");
                }
            });
        } else {
            window.alert("Geolocation is not supported by your browser");
        }
    });
}