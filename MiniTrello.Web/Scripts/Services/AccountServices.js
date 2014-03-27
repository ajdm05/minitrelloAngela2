'use strict';

angular.module('app.services', []).factory('AccountServices', ['$http', '$window', function ($http, $window) {

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

    account.updateProfile = function (model) {
        console.log("ModelUpdate");
        console.log(model);
        return $http.put(baseUrl + '/updateProfile/' + $window.sessionStorage.token, model);
    };

    return account;

}]);