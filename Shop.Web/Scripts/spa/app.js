var myApp = angular.module('myModule', []);
myApp.controller("myController", myController);
myApp.$inject = ['$scope'];
function myController($scope) {
    $scope.message = "This is my message from controller";
}