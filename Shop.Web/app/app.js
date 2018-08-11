/// <reference path="../assets/admin/libs/angular/angular.js" />

(function () {
    angular.module('tedushop',
        [
            'tedushop.products',
            'tedushop.product_categories',
            'tedushop.common'
        ])
        .config(config);
    config.$inject = ['$stateProvider', '$urlRouterProvider', '$httpProvider'];
    function config($stateProvider, $urlRouterProvider, $httpProvider) {
        $stateProvider.state('base', {
            url: "",
            templateUrl: "/app/shared/views/baseView.html",
            controller: "homeController",
            abstract: true
        }).state('home', {
            url: "/admin",
            parent: 'base',
            templateUrl: "/app/components/home/homeView.html",
            controller: "homeController"
        }).state('login', {
            url: "/login",
            templateUrl: "/app/components/login/loginView.html",
            controller: "loginController",
        })
        $urlRouterProvider.otherwise('/admin');
    }
})();