'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')



    // Path: /login
    .controller('BoardController', ['$scope', '$location', '$window', 'BoardServices','$stateParams', function ($scope, $location, $window, boardServices, $stateParams) {


        $scope.boardDetailId = $stateParams.boardId;
        $scope.IdOrganization = $stateParams.IdOrganization;

        //console.log($location.search().boardId);

        console.log($scope.boardDetailId);

        $scope.boards = [];

       // var board = { Id: 1, Name: 'Myboard1', Description: 'Description1' };
       // var board1 = { Id: 2, Name: 'Myboard2', Description: 'Description2' };
       // $scope.boards.push(board);
       // $scope.boards.push(board1);
        

        $scope.getBoardsForLoggedUser = function () {
            boardServices
                .getBoardsForLoggedUser($scope.IdOrganization)
              .success(function (data, status, headers, config) {
                    $scope.boards = data;
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
            //$location.path('/');
        };

        $scope.getBoardsDetails = function () {

            boardServices
                .getBoardDetails()
              .success(function (data, status, headers, config) {
                  $scope.boards = data;
              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
            //$location.path('/');
        };

    if ($scope.boardDetailId > 0)
    {
       //
    }
    else
    {
        $scope.getBoardsForLoggedUser();
    }
    

       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);