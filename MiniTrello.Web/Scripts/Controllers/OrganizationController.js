'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('OrganizationController', ['$scope', '$location', '$window', 'OrganizationServices', '$stateParams', function ($scope, $location, $window, organizationServices, $stateParams) {

        $scope.boardDetailId = $stateParams.boardId;
        $scope.organizationId = $stateParams.organizationId;
        //console.log($location.search().boardId);
        console.log($scope.boardDetailId);
        $scope.CreateNewOrganizationModel = { Title: '', Description: '' };
        $scope.archiveOrganizationModel = { Id: 0 };
        $scope.organizations = [];

        $scope.goToOrganizations = function () {
            $location.path('/organizations');
        };

        $scope.getOrganizationsForLoggedUser = function () {
            organizationServices
                .getOrganizationsForLoggedUser()
              .success(function (data, status, headers, config) {
                  $scope.organizations = data;
                    console.log(data);
                })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
            //$location.path('/');
        };

        $scope.createNewOrganizationForLoggedUser = function () {
            organizationServices
                .createNewOrganizationForLoggedUser($scope.CreateNewOrganizationModel)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.organizations.push(data);
                  $location.path('/organizations');
                })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
            //$location.path('/organizations');
        };

        $scope.archiveOrganizationForLoggedUser = function (idOrganization) {
            organizationServices
                .archiveOrganizationForLoggedUser(idOrganization)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $location.path('/organizations');
              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
        };

        //if ($scope.boardDetailId > 0) {
        //    //get board details
        //}
        //else {
        $scope.getOrganizationsForLoggedUser();
        // }




        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);