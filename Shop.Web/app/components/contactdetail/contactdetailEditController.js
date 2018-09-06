(function (app) {
    app.controller('contactdetailEditController', contactdetailEditController);
    contactdetailEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function contactdetailEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.contactdetail = {
            Status: true
        }
        $scope.loadcontactdetailDetail = function () {
            apiService.get('/api/contactdetail/getbyid/' + $stateParams.id,null,
                function (result) {
                    $scope.contactdetail = result.data;
                },function (error) {
                    notificationService.displayError(error.data);
                });
        }
        $scope.Updatecontactdetail = function () {
            apiService.put('/api/contactdetail/update', $scope.contactdetail,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' updated');
                    $state.go('contactdetail');
                },
                function (error) {
                    notificationService.displayError('Update fail');

                });
        }
        $scope.loadcontactdetailDetail();
    }
})(angular.module('tedushop.contactdetail'));