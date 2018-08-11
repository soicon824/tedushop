(function (app) {
    app.controller('loginController', loginController);
    loginController.$inject = ['$scope','apiService','notificationService','$state']
    function loginController($scope, apiService, notificationService, $state) {
        $scope.loginSubmit = function () {
            $state.go('home');
        }
    }
})(angular.module('tedushop'));