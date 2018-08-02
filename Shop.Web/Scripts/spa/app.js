var myApp = angular.module('myModule', []);
myApp.controller("schoolController", schoolController);
myApp.controller("studentController", studentController);
myApp.controller("teacherController", teacherController);

myApp.$inject = ['$scope'];
function schoolController($scope) {
    $scope.message = "This is my message from controller";
}

function studentController($scope) {
    $scope.message = "This is my studentController";
}

function teacherController($scope) {
    //$scope.message = "This is my teacherController";
}