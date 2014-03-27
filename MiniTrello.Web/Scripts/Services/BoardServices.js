'use strict';

angular.module('app.services').factory('BoardServices', ['$http', '$window', function ($http, $window) {

    var board = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    /*board.getBoardsForLoggedUser = function (organizationId) {
        return $http.get(baseUrl + '/boards/' + $window.sessionStorage.organizationID + '/' + $window.sessionStorage.token);
        //return $http.get(baseUrl + '/boards/' + organizationId + '/' + $window.sessionStorage.token);
    };*/

    board.getBoardsForLoggedUser = function (organizationId) {

        return $http.get(baseUrl + '/boards/' + organizationId + '/' + $scope.window.sessionStorage.token);
        //return $http.get(baseUrl + '/boards/' + organizationId + '/' + $window.sessionStorage.token);
    };

    board.getBoardDetails = function () {
        return $http.get(baseUrl + '/boards/' + $window.sessionStorage.id + '/' + $window.sessionStorage.token);
    };

    board.createNewBoardForLoggedUser = function () {
        return $http.post(baseUrl + '/boards/create/' + $window.sessionStorage.token);
    };

    board.deletBoard = function () {
        return $http.delete(baseUrl + '/boards/delete/' + $window.sessionStorage.token);
    };

    return board;

}]);