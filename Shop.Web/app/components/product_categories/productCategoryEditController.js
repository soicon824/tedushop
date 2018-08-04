(function (app) {
    app.controller('productCategoryEditController', productCategoryEditController);
    productCategoryEditController.$inject = ['$scope', 'apiService', 'notificationService', '$state', '$stateParams','commonService']
    function productCategoryEditController($scope, apiService, notificationService, $state, $stateParams, commonService) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        $scope.loadProductCategoryDetail = function () {
            apiService.get('/api/productcategory/getbyid/' + $stateParams.id,null,
                function (result) {
                    $scope.productCategory = result.data;
                },function (error) {
                    notificationService.displayError(error.data);
                });
        }
        $scope.UpdateProductCategory = function () {
            apiService.put('/api/productcategory/update', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + ' updated');
                    $state.go('product_categories');
                },
                function (error) {
                    notificationService.displayError('Update fail');

                });
        }
        $scope.getSeoTitle = function () {
            $scope.productCategory.Alias = commonService.getSeoTitle($scope.productCategory.Name);
        }
        function loadParentCategories() {
            apiService.get('/api/productcategory/getallparents', null,
                function (result) {
                    $scope.parentCategories = result.data;
                },
                function (result) {
                    console.log("cannot get list parent");
                });
        }
        loadParentCategories();
        $scope.loadProductCategoryDetail();
    }
})(angular.module('tedushop.product_categories'));