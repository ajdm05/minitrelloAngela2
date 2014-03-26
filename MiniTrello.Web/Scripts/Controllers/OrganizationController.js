'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('OrganizationController', ['$scope', '$location', '$window', 'OrganizationServices', '$stateParams', function ($scope, $location, $window, organizationServices, $stateParams) {


        $scope.boardDetailId = $stateParams.boardId;
        //console.log($location.search().boardId);
        console.log($scope.boardDetailId);
        $scope.addNewOrganizationModel = { Title: '', Description: '' };
        $scope.archiveOrganizationModel = { Id: null };
        $scope.changeTitleOrganizationModel = { Id: 0, Title: '' };
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
                .createNewOrganizationForLoggedUser($scope.createNewOrganizationModel)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.organizations.push(data);
                  $scope.goToOrganizations();
                })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
            //$location.path('/');
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