'use strict';

angular.module('app.services').factory('LaneServices', ['$http', '$window', function ($http, $window) {

    var lane = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    lane.getLanesForBoard = function() {
        return $http.get(baseUrl + '/lanes/' + $window.sessionStorage.token);
    };

    return lane;

}]);