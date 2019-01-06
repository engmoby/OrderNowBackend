(function () {
    'use strict';

    angular
        .module('home')
        .controller('RegionController', ['$rootScope', 'blockUI', '$scope', '$filter', '$translate',
            '$state', 'RegionResource', 'RegionsPrepService', '$stateParams', 'appCONSTANTS',
            'ToastService', 'CountryByIdPrepService', RegionController]);


    function RegionController($rootScope, blockUI, $scope, $filter, $translate,
        $state, RegionResource, RegionsPrepService, $stateParams, appCONSTANTS, ToastService, CountryByIdPrepService) {

        $('.pmd-sidebar-nav>li>a').removeClass("active")
        $($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")

       
        var vm = this;
        $scope.totalCount = RegionsPrepService.totalCount;
        $scope.Regions = RegionsPrepService;
        debugger;
        $scope.countryName = CountryByIdPrepService.titleDictionary[$scope.selectedLanguage];
        function refreshRegions() {



            var k = RegionResource.getAllRegions({ countryId: $stateParams.countryId, page: vm.currentPage }).$promise.then(function (results) {
                $scope.Regions = results


            },
                function (data, status) {


                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }

        vm.currentPage = 1;
        $scope.changePage = function (page) {
            vm.currentPage = page;
            refreshRegions();
        }


    }

})();
