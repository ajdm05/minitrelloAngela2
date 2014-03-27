'use strict';

angular.module('app.services').factory('LaneServices', ['$http', '$window', function ($http, $window) {

    var lane = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    lane.getLanesForLoggedUser = function (boardId) {
        return $http.get(baseUrl + '/lanes/' + boardId + '/' + $window.sessionStorage.token);
    };

    lane.createNewLaneForLoggedUser = function (model, idBoard) {
        console.log("ParamLinkBoard");
        console.log(idBoard);
        return $http.post(baseUrl + '/lanes/create/' + idBoard + '/' + $window.sessionStorage.token, model);
    };

    return lane;

}]);