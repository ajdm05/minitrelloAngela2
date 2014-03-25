'use strict';

angular.module('app.services').factory('BoardServices', ['$http', '$window', function ($http, $window) {

    var board = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    board.getBoardsForLoggedUser = function() {
        return $http.get(baseLocalUrl + '/boards/' + $window.sessionStorage.token);
    };

    board.getBoardDetails = function () {
        return $http.get(baseLocalUrl + '/boards/' + $window.sessionStorage.id + '/' + $window.sessionStorage.token);
    };

    board.createBoard = function () {
        return $http.post(baseLocalUrl + '/boards/create/' + $window.sessionStorage.token);
    };

    board.deletBoard = function () {
        return $http.delete(baseLocalUrl + '/boards/create/' + $window.sessionStorage.token);
    };

    return board;

}]);