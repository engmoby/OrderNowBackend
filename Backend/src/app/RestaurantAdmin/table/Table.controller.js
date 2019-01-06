(function () {
    'use strict';

    angular
        .module('home')
        .controller('TableController', ['$rootScope', '$scope', '$filter', '$translate',
            '$state', 'TableResource',   '$localStorage',
            'authorizationService', 'appCONSTANTS',
            'ToastService', TableController]);


    function TableController($rootScope, $scope, $filter, $translate,
        $state, TableResource,  $localStorage, authorizationService,
        appCONSTANTS, ToastService) {
 debugger;
        refreshTables();

        function refreshTables() {
         //    
            
            var k = TableResource.getAllTables().$promise.then(function (results) {
                $scope.TableList = results;
                 
                
            },
            function (data, status) {
                 
                
                ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
            });
        }

    }

})();
