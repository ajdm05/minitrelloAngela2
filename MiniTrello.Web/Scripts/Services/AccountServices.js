'use strict';

angular.module('app.services',[]).factory('AccountServices', ['$http', function ($http) {

    var account = {};

    account.login = function (data) {
        return $http.post('http://minitrelloapiajdm.apphb.com/login', data);
    };


    return account;

}]);