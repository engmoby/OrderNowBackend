(function () {
    'use strict';

    angular
        .module('home')
        .controller('CityController', ['$rootScope', 'blockUI', '$scope', '$filter', '$translate',
            '$state', 'CityResource', 'CitiesPrepService', '$stateParams', 'appCONSTANTS',
            'ToastService', 'RegionByIdPrepService', CityController]);


    function CityController($rootScope, blockUI, $scope, $filter, $translate,
        $state, CityResource, CitiesPrepService, $stateParams, appCONSTANTS, ToastService, RegionByIdPrepService) {

        $('.pmd-sidebar-nav>li>a').removeClass("active")
        $($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")

      //   

        var vm = this;
        $scope.totalCount = CitiesPrepService.totalCount;
        $scope.Cities = CitiesPrepService;
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        function refreshCities() {

         //    

            var k = CityResource.getAllCities({ regionId: $stateParams.regionId, page: vm.currentPage }).$promise.then(function (results) {
                $scope.Cities = results
                 

            },
                function (data, status) {
                     

                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }

        vm.currentPage = 1;
        $scope.changePage = function (page) {
            vm.currentPage = page;
            refreshCities();
        }
         

    }

})();
