'use strict';

angular.module('app.services',[]).factory('AccountServices', ['$http', function ($http) {

    var account = {};
    
    var baseRemoteUrl = "http://minitrelloapiajdm.apphb.com";
    var baseLocalUrl = "http://localhost:8080";
    var baseUrl = baseRemoteUrl;

    account.login = function (model) {
        return $http.post(baseLocalUrl + '/login', model);
    };

    account.register = function (model) {
        return $http.post(baseLocalUrl + '/register', model);
    };

    account.forgotPassword = function (model) {
        return $http.post(baseLocalUrl + '/forgotPassword' + $window.sessionStorage.token, model);
    };

    account.updateProfile = function(model) {
        return $http.post(baseLocalUrl + '/updateProfile' + $window.sessionStorage.token, model);
    };

    return account;

}]);