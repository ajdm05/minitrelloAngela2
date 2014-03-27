'use strict';

// Google Analytics Collection APIs Reference:
// https://developers.google.com/analytics/devguides/collection/analyticsjs/

angular.module('app.controllers')
    // Path: /login
   .controller('LaneController', ['$scope', '$location', '$window', '$stateParams', 'LaneServices', function ($scope, $location, $window, $stateParams, LaneServices) {



       $scope.boardDetailId = $stateParams.boardId;
       $scope.CreateNewLaneModel = { Title: '' };
        //$scope.organizationID = $stateParams.organizationID;
        console.log($scope.boardDetailId);
        $scope.lanes = [];

        $scope.getLanesForLoggedUser = function () {
            console.log("Param");
            console.log($stateParams.boardId);
            LaneServices
                .getLanesForLoggedUser($stateParams.boardId)
              .success(function (data, status, headers, config) {
                  $scope.lanes = data;
                  console.log(data);
                })
              .error(function (data, status, headers, config) {
                console.log(data);
            });
            //$location.path('/');
        };

        $scope.createNewLaneForLoggedUser = function () {
            console.log("Param");
            console.log($stateParams.boardId);
            LaneServices
                .createNewLaneForLoggedUser($scope.CreateLaneBoardModel, $stateParams.boardId)
              .success(function (data, status, headers, config) {
                  console.log(data);
                  $scope.lanes.push(data);

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
        $scope.getLanesForLoggedUser();
    //}
    

       

        $scope.$on('$viewContentLoaded', function () {
            $window.ga('send', 'pageview', { 'page': $location.path(), 'title': $scope.$root.title });
        });
    }]);