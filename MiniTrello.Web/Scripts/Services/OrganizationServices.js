'use strict';

angular.module('app.services').factory('OrganizationServices', ['$http', '$window', function ($http, $window) {

    var organization = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseLocalUrl;

    organization.getOrganizationsForLoggedUser = function () {
        return $http.get(baseRemoteUrl + '/organizations/' + $window.sessionStorage.token);
    };

    organization.createNewOrganizationForLoggedUser = function (data) {
        return $http.get(baseRemoteUrl + '/organizations/create/' + $window.sessionStorage.token,data);
    };

    organization.archiveOrganizationForLoggedUser = function () {
        return $http.get(baseRemoteUrl + '/organizations/delete/' + $window.sessionStorage.token);
    };
    return organization;

}]);
