'use strict';

angular.module('app.services',[]).factory('AccountServices', ['$http', function ($http) {

    var account = {};
    
    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    account.login = function (model) {
        return $http.post(baseUrl + '/login', model);
    };

    account.register = function (model) {
        return $http.post(baseUrl + '/register', model);
    };

    account.forgotPassword = function (model) {
        return $http.post(baseUrl + '/forgotPassword', model);
    };

    account.forgotPassword = function (model) {
        return $http.post(baseUrl + '/forgotPassword', model);
    };

    account.updateProfile = function(model) {
        return $http.post(baseUrl + '/updateProfile' + $window.sessionStorage.token, model);
    };

    return account;

}]);