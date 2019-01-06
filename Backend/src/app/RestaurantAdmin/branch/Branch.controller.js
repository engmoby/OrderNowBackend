(function () {
    'use strict';

    angular
        .module('home')
        .controller('BranchController', ['$rootScope', '$scope', '$filter', '$translate',
            '$state', 'BranchResource', '$localStorage',
            'authorizationService', 'blockUI', 'appCONSTANTS',
            'ToastService', '$stateParams', BranchController]);


    function BranchController($rootScope, $scope, $filter, $translate,
        $state, BranchResource, $localStorage, authorizationService, blockUI,
        appCONSTANTS, ToastService, $stateParams) {
 
        var vm = this;
        refreshBranchs();
        function refreshBranchs() {
           //  

            var k = BranchResource.getAllBranchs({ areaId: $stateParams.areaId, page: vm.currentPage }).$promise.then(function (results) {
                $scope.BranchList = results;
                 
                console.log( $scope.BranchList);

            },
                function (data, status) {
                     

                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }
        vm.showMore = function (element) {
            $(element.currentTarget).toggleClass("child-table-collapse");
        }
        vm.currentPage = 1;
        $scope.changePage = function (page) {
            vm.currentPage = page;
            refreshAreas();
        }
         
    }

})();
