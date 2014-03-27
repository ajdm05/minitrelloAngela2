'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')
    // Path: /login
   .controller('CardController', ['$scope', '$location', '$window', '$stateParams', 'CardServices', function ($scope, $location, $window, $stateParams, CardServices) {



       $scope.boardDetailId = $stateParams.boardId;
       $scope.CreateNewBoardModel = { Title: '' };
        console.log($scope.boardDetailId);
        $scope.cards = [];

        $scope.getCardsForLoggedUser = function () {
            console.log("ParamCards");
            console.log($stateParams.laneId);
            CardServices
                .getCardsForLoggedUser($stateParams.laneId)
              .success(function (data, status, headers, config) {
                  $scope.cards = data;
                  console.log(data);
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
        };

        $scope.createNewCardForLoggedUser = function () {
            console.log("ParamCreateCard");
            console.log($stateParams.laneId);
            CardServices
                .createNewCardForLoggedUser($scope.CreateNewCardModel, $stateParams.laneId)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.boards.push(data);

              })
              .error(function (data, status, headers, config) {
                  console.log(data);
              });
        };


    /*if ($scope.boardDetailId > 0)
    {
       //
    }
    else
    {*/
        $scope.getCardsForLoggedUser();
    //}
    

       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);