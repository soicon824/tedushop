(function (app) {
    app.controller('productAddController', productAddController);
    productAddController.$inject = ['$scope', 'apiService', 'notificationService', '$state']
    function productAddController($scope, apiService, notificationService, $state) {
        $scope.product = {
            CreatedDate: new Date(),
            Status: true
        }
        $scope.moreImages = [];
        $scope.chooseMoreImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.moreImages.push(fileUrl);
                })
            }
            finder.popup();
        }
        $scope.ckeditorOptions = {
            language: 'vi',
            height:'200px'
        }
        $scope.addProduct = function () {
            $scope.product.MoreImages = JSON.stringify($scope.moreImages);
            apiService.post('/api/product/create', $scope.product,
                function (result) {
                    notificationService.displaySuccess(result.data.Name + 'Added');
                    $state.go('products');
                },
                function (error) {
                    notificationService.displayError('Add fail');

                });
        }
        $scope.chooseImage = function () {
            var finder = new CKFinder();
            finder.selectActionFunction = function (fileUrl) {
                $scope.$apply(function () {
                    $scope.product.Image = fileUrl;
                })
            }
            finder.popup();
        }
        function loadCategories() {
            apiService.get('/api/productcategory/getallparents', null,
                function (result) {
                    $scope.categories = result.data;
                },
                function (result) {
                    console.log("cannot get category");
                });
        }
        loadCategories();
    }
})(angular.module('tedushop.products'));