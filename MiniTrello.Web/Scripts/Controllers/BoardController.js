'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')
    // Path: /login
   .controller('BoardController', ['$scope', '$location', '$window', '$stateParams', 'BoardServices', function ($scope, $location, $window, $stateParams, BoardServices) {



       $scope.boardDetailId = $stateParams.boardId;
       $scope.CreateNewBoardModel = { Title: '' };
        console.log($scope.boardDetailId);
        $scope.boards = [];

        $scope.getBoardsForLoggedUser = function () {
            console.log("Param");
            console.log($stateParams.organizationId);
            BoardServices
                .getBoardsForLoggedUser($stateParams.organizationId)
              .success(function (data, status, headers, config) {
                  $scope.boards = data;
                  console.log(data);
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
        };

        $scope.createNewBoardForLoggedUser = function () {
            BoardServices
                .createNewBoardForLoggedUser($scope.CreateNewBoardModel, $stateParams.organizationId)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.boards.push(data);

              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
        };

        $scope.getMembersForBoard = function () {
            BoardServices
                .getMembersForBoard($stateParams.boardId)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.boards.push(data);

              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
        };

        $scope.getBoardsDetails = function () {
            BoardServices
                .getBoardDetails()
              .success(function (data, status, headers, config) {
                  $scope.boards = data;
              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
        };

        $scope.getBoardsForLoggedUser();
    

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);