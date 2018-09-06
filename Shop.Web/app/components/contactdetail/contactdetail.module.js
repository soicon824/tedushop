(function () {
    angular.module('tedushop.contactdetail', ['tedushop.common']).config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider'];
    function config($stateProvider, $urlRouterProvider) {
        $stateProvider.state('contactdetail', {
            url: "/contactdetail",
            parent:'base',
            templateUrl: "/app/components/contactdetail/contactdetailListView.html",
            controller: "contactdetailListController"
        }).state('add_contactdetail', {
            url: "/add_contactdetail",
            parent:'base',
            templateUrl: "/app/components/contactdetail/contactdetailAddView.html",
            controller: "contactdetailAddController"
        }).state('edit_contactdetail', {
            url: "/edit_contactdetail/:id",
            parent:'base',
            templateUrl: "/app/components/contactdetail/contactdetailEditView.html",
            controller: "contactdetailEditController"
        })
    }
})();