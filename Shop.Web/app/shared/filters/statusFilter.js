(function (app) {
    app.filter('statusFilter', function () {
        return function (input) {
            if (input == true)
                return "Kich hoat"
            else
                return "Chua k hoat"
        }
    })
})(angular.module('tedushop.common'));