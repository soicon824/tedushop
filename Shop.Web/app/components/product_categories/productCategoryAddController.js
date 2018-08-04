(function (app) {
    app.controller('productCategoryAddController', productCategoryAddController);
    productCategoryAddController.$inject = ['$scope','apiService','notificationService','$state']
    function productCategoryAddController($scope, apiService, notificationService, $state) {
        $scope.productCategory = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.parentCategories = [];
        $scope.AddProductCategory = function () {
            apiService.post('/api/productcategory/create', $scope.productCategory,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'Added');
                    $state.go('product_categories');
                },
                function (error) {
                    notificationService.displayError('Add fail');

                });
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
    }
})(angular.module('tedushop.product_categories'));