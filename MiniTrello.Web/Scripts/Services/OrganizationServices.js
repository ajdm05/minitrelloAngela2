'use strict';

angular.module('app.services').factory('OrganizationServices', ['$http', '$window', function ($http, $window) {

    var organization = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseLocalUrl;

    organization.getOrganizationsForLoggedUser = function () {
        return $http.get(baseRemoteUrl + '/organizations/' + $window.sessionStorage.token);
    };

    return organization;

}]);
