(function () {
    var login_app = angular.module('login_app', []);
    login_app.controller('loginControl', function ($scope, $http) {
        $scope.login = 'SIGN IN';

        $scope.doLogin = function () {
            $scope.login = 'Checking..';
            $http({
                url: '/Login/UserLogin',
                method: 'POST',
                data: $scope.loginDetails
            }).success(function (data) {
                $scope.login = 'SIGN IN';
                if (data.isRedirect == true) {
                    $scope.login_msg = data.sucs_msg;
                    window.location.href = data.redirectUrl;  
                }
                else {
                    $scope.login_msg = data;
                }
            })
        };
    });


})();