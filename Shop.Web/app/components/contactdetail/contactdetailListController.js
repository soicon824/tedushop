/// <reference path="../../../assets/admin/libs/ngbootbox/ngbootbox.js" />
(function (app) {
    app.controller('contactdetailListController', contactdetailListController);
    contactdetailListController.$inject = ['$scope','apiService','notificationService','$ngBootbox','$filter']
    function contactdetailListController($scope, apiService, notificationService, $ngBootbox, $filter) {
        $scope.contactdetail = [];
        $scope.page = 0;
        $scope.pageCount = 0;
        $scope.getListcontactdetail = getListcontactdetail
        $scope.keyword = '';
        $scope.search = search;
        $scope.deletecontactdetail = deletecontactdetail;
        $scope.selectAll = selectAll;
        $scope.deleteMultiple = deleteMultiple;
        $scope.$watch("contactdetail", function (n, o) {
            var checked = $filter("filter")(n, { checked: true });
            if (checked.length) {
                $scope.selected = checked;
                $('#btnDelete').removeAttr('disabled');
            }
            else {
                $('#btnDelete').attr('disabled', 'disabled');
            }
        }, true);

        $scope.isAll = false;
        function selectAll() {
            if ($scope.isAll === false) {
                angular.forEach($scope.contactdetail, function (item) {
                    item.checked = true;
                });
                $scope.isAll = true;
            }
            else {
                angular.forEach($scope.contactdetail, function (item) {
                    item.checked = false;
                });
                $scope.isAll = false;
            }
        }

        function deleteMultiple() {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var lstId = [];
                $.each($scope.selected, function (i, item) {
                    lstId.push(item.ID);
                });
                var config = {
                    params: {
                        lstId: JSON.stringify(lstId)
                    }
                }
                apiService.del('api/contactdetail/deletemulti', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }

        function deletecontactdetail(id) {
            $ngBootbox.confirm('Bạn có chắc muốn xóa?').then(function () {
                var config = {
                    params: {
                        id: id
                    }
                }
                apiService.del('api/contactdetail/delete', config, function () {
                    notificationService.displaySuccess('Xóa thành công');
                    search();
                }, function () {
                    notificationService.displayError('Xóa không thành công');
                })
            });
        }
        function search() {
            getListcontactdetail();
        }
        function getListcontactdetail(page) {
            page = page || 0
            var config = {
                params: {
                    keyword: $scope.keyword,
                    page: page,
                    pagesize: 5
                }
            };
            apiService.get('/api/contactdetail/getall', config,
                function (result) {
                    if (result.data.TotalCount === 0) {
                        notificationService.displayWarning("Not found by keyword");
                    }
                    else {
                        notificationService.displaySuccess("Founded: " + result.data.TotalCount);
                    }
                    $scope.contactdetail = result.data.Items;
                    $scope.page = result.data.Page;
                    $scope.pagesCount = result.data.TotalPages;
                    $scope.totalCount = result.data.TotalCount;
                },
                function () {
                    console.log('Load ct failed');
                }
            )
        }
        $scope.getListcontactdetail();
    }
})(angular.module('tedushop.contactdetail'));