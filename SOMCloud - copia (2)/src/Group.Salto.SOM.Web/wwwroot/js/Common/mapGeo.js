var app = app || {};
app.mapGeo = app.mapGeo || {};

app.mapGeo = (function () {
    var settings;
    var inputLatitude;
    var inputLongitude;
    var inputRatio;
    var searchcodeaddress;
    var mapGeoCodeAddressError;
    var mapGeoPosicionBase
    var mapGeoLatitudeIncorrectFormat;
    var mapGeoLongitudeIncorrectFormat
    var mapGeoRadioIncorrectFormat;

    var init = function (options) {
        settings = $.extend({}, options);
        setInitialValues();
        setInitialEvents();
    };

    function setInitialValues() {
        inputLatitude = $("#" + settings.inputLatitude);
        inputLongitude = $("#" + settings.inputLongitude);
        inputRatio = $("#" + settings.inputRatio);
        searchcodeaddress = $("#" + settings.searchcodeaddress);
        mapGeoCodeAddressError = settings.mGeoCodeAddressError;
        mapGeoPosicionBase = settings.mGeoPosicionBase;
        mapGeoLatitudeIncorrectFormat = settings.mGeoLatitudeIncorrectFormat;
        mapGeoLongitudeIncorrectFormat = settings.mGeoLongitudeIncorrectFormat;
        mapGeoRadioIncorrectFormat = settings.mGeoRadioIncorrectFormat;
    }

    function setInitialEvents() {
        inputLatitude.on("change", validateLat);
        inputLongitude.on("change", validateLng);
        inputRatio.on("change", validateRadius);
        searchcodeaddress.on("click", codeAddress);
    }

    var map;
    var circle;
    var marker = new google.maps.Marker();
    var geocoder;

    var checkGeoCoder = function () {
        if (geocoder == undefined) {
            initialize();
        }
    }

    function initialize() {
        geocoder = new window.google.maps.Geocoder();
        var mapOptions = {
            zoom: 8,
            center: new window.google.maps.LatLng(-34.397, 150.644)//TODO:Ponerlo configurable
        };

        map = new window.google.maps.Map(document.getElementById('map-canvas'), mapOptions);
        var basePosition;

        if (inputLatitude.val() !== "" && inputLongitude.val() !== "") {
            basePosition = new window.google.maps.LatLng(inputLatitude.val(), inputLongitude.val());
            placeMarker(basePosition);
            window.google.maps.event.addListener(marker, 'click', toggleBounce);
        }

        window.google.maps.event.addListener(map, 'click', function (event) {
            placeMarker(event.latLng);
            updatePosition(event.latLng);
        });

        if (basePosition == undefined) {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = new window.google.maps.LatLng(position.coords.latitude, position.coords.longitude);
                    map.setCenter(pos);
                }, function () {
                    handleNoGeolocation(true);
                });
            } else {
                handleNoGeolocation(false);
            }
        }
    }

    var codeAddress = function () {
        var address = $('#mapseachaddress').val();
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === window.google.maps.GeocoderStatus.OK) {
                placeMarker(results[0].geometry.location);
                updatePosition(results[0].geometry.location);
                window.google.maps.event.addListener(marker, 'click', toggleBounce);
            } else {
                alert(mapGeoCodeAddressError + ': ' + status);
            }
        });
    }

    function updatePosition(latLng) {
        inputLatitude.val(latLng.lat());
        inputLongitude.val(latLng.lng());
    }

    function placeMarker(location) {
        marker.setMap(null);
        if (circle != undefined) {
            circle.setMap(null);
        }

        var image = {
            url: "../../images/home_icon.png",
            size: new window.google.maps.Size(42, 60),
            origin: new window.google.maps.Point(0, 0),
            anchor: new window.google.maps.Point(21, 59)
        };

        marker = new window.google.maps.Marker({
            position: location,
            draggable: true,
            animation: window.google.maps.Animation.DROP,
            title: mapGeoPosicionBase,
            icon: image,
            map: map
        });

        map.setCenter(location);
        var radius = inputRatio.val();
        if ($.isNumeric(radius)) {
            var circleOptions = {
                strokeColor: "#2ba6cb",
                strokeOpacity: 0.8,
                strokeWeight: 2,
                fillColor: "#2ba6cb",
                fillOpacity: 0.25,
                map: map,
                center: location,
                radius: radius * 1000
            };
            circle = new window.google.maps.Circle(circleOptions);
        }

        window.google.maps.event.addListener(marker, 'drag', function (event) {
            updatePosition(event.latLng);
        });
        window.google.maps.event.addListener(marker, 'dragend', function (event) {
            placeMarker(event.latLng);
        });
    }

    function toggleBounce() {
        if (marker.getAnimation() != null) {
            marker.setAnimation(null);
        } else {
            marker.setAnimation(window.google.maps.Animation.BOUNCE);
        }
    }

    function handleNoGeolocation() {
        map.setCenter(new window.google.maps.LatLng(41.61, 0.676));
    }

    function validateLat() {
        if (inputLatitude.val() !== ""
            && CheckFloatNumber(inputLatitude, mapGeoLatitudeIncorrectFormat)
            && validateLng(true)) {
            var latLng = new window.google.maps.LatLng(inputLatitude.val(), inputLongitude.val());
            placeMarker(latLng);
            map.setCenter(latLng);
        }
        return false;
    }

    function validateLng() {
        if (inputLongitude.val() !== ""
            && CheckFloatNumber(inputLongitude, mapGeoLongitudeIncorrectFormat)
            && validateLng(true)) {
            var latLng = new window.google.maps.LatLng(inputLatitude.val(), inputLongitude.val());
            placeMarker(latLng);
            map.setCenter(latLng);
        }
        return false;
    }

    function validateRadius() {
        if (inputRatio.val() !== ""
            && app.common.validations.CheckFloatNumber(inputRatio, mapGeoRadioIncorrectFormat)
            && marker != undefined) {
            placeMarker(marker.getPosition());
        }
        return false;
    }

    return {
        Init: init,
        CheckGeoCoder: checkGeoCoder,
        CodeAddress: codeAddress
    };
})();