var myApp = angular.module('myModule', []);
myApp.controller("schoolController", schoolController);
myApp.service('validator', validator)
function schoolController($scope, validator) {
    $scope.checkNumber = function (num) {
        $scope.message = validator.checkNumberMain(num);

    }
    $scope.num = 1;
    
}

function validator($window) {
    return {
        checkNumberMain:checkNumber
    }
    function checkNumber(input) {
        if (input % 2 == 0) {
            return 'This is so chan';
        }
        else {
            return "so le";
        }
    }
}


