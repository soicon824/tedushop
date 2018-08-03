(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.getListProductCategory = getListProductCategory

        function getListProductCategory() {
            apiService.get('/api/productcategory/getall', null,
                function (result) {
                    $scope.productCategories = result.data;
                },
                function () {
                    console.log('Load Product category failed');
                }
            )
        }
        $scope.getListProductCategory();
    }
})(angular.module('tedushop.product_categories'));