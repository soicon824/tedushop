(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService']
    function productCategoryListController($scope, apiService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getListProductCategory = getListProductCategory

        function getListProductCategory(page) {
            page = page || 0
            var config = {
                params: {
                    page: page,
                    pagesize: 5
                }
            };
            apiService.get('/api/productcategory/getall', config,
                function (result) {
                    $scope.productCategories = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log('Load Product category failed');
                }
            )
        }
        $scope.getListProductCategory();
    }
})(angular.module('tedushop.product_categories'));