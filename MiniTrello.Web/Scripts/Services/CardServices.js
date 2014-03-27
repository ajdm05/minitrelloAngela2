'use strict';

angular.module('app.services').factory('CardServices', ['$http', '$window', function ($http, $window) {

    var card = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    card.getCardsForLoggedUser = function (laneId) {
        console.log("ParamCardLink");
        console.log(laneId);
        return $http.get(baseUrl + '/cards/' + laneId + '/' + $window.sessionStorage.token);
    };

    card.createNewCardForLoggedUser = function (model, laneId) {
        console.log("ParamLinkBoard");
        console.log(laneId);
        return $http.post(baseUrl + '/cards/create/' + laneId + '/' + $window.sessionStorage.token, model);
    };

    return card;

}]);