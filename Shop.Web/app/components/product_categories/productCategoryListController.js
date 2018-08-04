(function (app) {
    app.controller('productCategoryListController', productCategoryListController);
    productCategoryListController.$inject = ['$scope','apiService','notificationService']
    function productCategoryListController($scope, apiService, notificationService) {
        $scope.productCategories = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getListProductCategory = getListProductCategory
        $scope.keyword = '';
        $scope.search = search;

        function search() {
            getListProductCategory();
        }
        function getListProductCategory(page) {
            page = page || 0
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pagesize: 5
                }
            };
            apiService.get('/api/productcategory/getall', config,
                function (result) {
                    if (result.data.TotalCount == 0) {
                        notificationService.displayWarning("Not found by keyword");
                    }
                    else {
                        notificationService.displaySuccess("Founded: " + result.data.TotalCount);
                    }
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