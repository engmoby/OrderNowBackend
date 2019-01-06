(function () {
    'use strict';

    angular
        .module('home')
        .controller('loginController', ['$rootScope', '$scope', '$state', '$localStorage', 'authorizationService', 'appCONSTANTS', loginController]);

    function loginController($rootScope, $scope, $state, $localStorage, authorizationService, appCONSTANTS) {
debugger;
        if ($localStorage.authInfo) {
            if ($localStorage.authInfo.Role == "Admin") {
                $state.go('restaurants');

                // } else if ($localStorage.authInfo.Role == "Room") {
                //     $state.go('clientFeatures');

            } else if ($scope.user.role == "Supervisor") {
                $state.go('adminRequests');

            }
            else if ($scope.user.role == "Receptionist") {
                $state.go('adminRequests');

            }
            else if ($scope.user.role == "Waiter") {
            $state.go('login');
           // $state.go('adminRequests');

            } else if ($localStorage.authInfo.Role == "RestaurantAdmin") {
                $state.go('Backgrounds');

            }
        }
        else {
            $state.go('login');
        }
    }

}())