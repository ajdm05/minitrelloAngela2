'use strict';

angular.module('app.services').factory('CardServices', ['$http', '$window', function ($http, $window) {

    var card = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    card.getCardsForLane = function() {
        return $http.get(baseUrl + '/cards/' + $window.sessionStorage.token);
    };

    return card;

}]);