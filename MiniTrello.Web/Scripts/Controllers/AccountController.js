'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')

  

    // Path: /login
    .controller('AccountController', ['$scope', '$location', '$window', 'AccountServices', function ($scope, $location, $window, AccountServices) {

        $scope.hasError = false;
        $scope.errorMessage = '';

        $scope.isLogged = function() {
            return $window.sessionStorage.token != null;
        };
        
        $scope.loginModel = { Email: '', Password: '' };
        $scope.registerModel = { Email: '', Password: '', FirstName: '', LastName: '', ConfirmPassword: '' };
        $scope.updateProfileModel = { FirstName: '', LastName: '', UserName: '', Bio: '', Initials: ''};
        
        
        // TODO: Authorize a user
        $scope.login = function () {

            AccountServices
                .login($scope.loginModel)
              .success(function (data, status, headers, config) {
                  
                  $window.sessionStorage.token = data.Token;
                  $location.path('/organizations');
              })
              .error(function (data, status, headers, config) {
                // Erase the token if the user fails to log in
                delete $window.sessionStorage.token;

                $scope.errorMessage = 'Error o clave incorrect';
                $scope.hasError = true;
                // Handle login errors here
                $scope.message = 'Error: Invalid user or password';
            });
            //$location.path('/');
        };

        $scope.goToRegister = function() {
            $location.path('/register');
        };

        $scope.goToLogin = function() {
            $location.path('/login');
        };

        $scope.goToForgotPassword = function () {
            $location.path('/forgotPassword');
        };

        $scope.goToRegister = function () {
            $location.path('/updateProfile');
        };

        $scope.register = function() {
            AccountServices
                .register($scope.registerModel)
                .success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.goToLogin();
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.errorMessage = 'Email is already registered';
                    $scope.hasError = true;
                    $scope.message = 'Email is already registered';
                });
        };

        $scope.updateProfile = function () {
            AccountServices
                .updateProfile($scope.updateProfileModel)
                .success(function (data, status, headers, config) {
                    console.log(data);
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                    $scope.errorMessage = 'Could not be updated';
                    $scope.hasError = true;
                    $scope.message = 'Could not be updated';
                });
            $location.path('/organizations');
        };

        $scope.forgotPassword = function () {
            AccountServices
                .register($scope.forgotPasswordModel)
                .success(function (data, status, headers, config) {
                    console.log(data);
                    $scope.goToLogin();
                })
                .error(function (data, status, headers, config) {
                    console.log(data);
                });
        };

        $scope.logOut = function () {
            if ($window.sessionStorage.token != null) {
                $window.sessionStorage.token = null;
                $scope.goToLogin();
            }  
        };

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);