(function (app) {
    app.controller('contactdetailAddController', contactdetailAddController);
    contactdetailAddController.$inject = ['$scope','apiService','notificationService','$state']
    function contactdetailAddController($scope, apiService, notificationService, $state) {
        $scope.contactdetail = {
            Status: true
        }
        $scope.Addcontactdetail = function () {
            apiService.post('/api/contactdetail/create', $scope.contactdetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'Added');
                    $state.go('contactdetail');
                },
                function (error) {
                    notificationService.displayError('Add fail');

                });
        }
    }
})(angular.module('tedushop.contactdetail'));