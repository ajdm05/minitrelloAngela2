'use strict';

angular.module('app.services').factory('OrganizationServices', ['$http', '$window', function ($http, $window) {

    var organization = {};

    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:1416";
    var baseUrl = baseRemoteUrl;

    organization.getOrganizationsForLoggedUser = function () {
        return $http.get(baseUrl + '/organizations/' + $window.sessionStorage.token);
    };

    organization.createNewOrganizationForLoggedUser = function (model) {
        console.log(baseUrl + '/organizations/create/' + $window.sessionStorage.token);
        console.log("model:" + model);
        return $http.post(baseUrl + '/organizations/create/' + $window.sessionStorage.token, model);
    };

    organization.archiveOrganizationForLoggedUser = function (idOrganization) {
        console.log("ParamOrgaDele");
        console.log(idOrganization);
        console.log("LinkDeleteOrg")
        console.log(baseUrl + '/organization/delete/' + idOrganization + '/' + $window.sessionStorage.token);
        return $http.delete(baseUrl + '/organization/delete/' + idOrganization + '/' + $window.sessionStorage.token);
    };
    return organization;

}]);
