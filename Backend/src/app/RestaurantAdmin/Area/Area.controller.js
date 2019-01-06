(function () {
    'use strict';

    angular
        .module('home')
        .controller('AreaController', ['$rootScope', 'blockUI', '$scope', '$filter', '$translate',
            '$state', 'AreaResource', 'AreaPrepService', '$localStorage',
            'authorizationService', 'appCONSTANTS',
            'ToastService', 'CityByIdPrepService', 'RegionByIdPrepService', '$stateParams', AreaController]);


    function AreaController($rootScope, blockUI, $scope, $filter, $translate,
        $state, AreaResource, AreaPrepService, $localStorage, authorizationService,
        appCONSTANTS, ToastService, CityByIdPrepService, RegionByIdPrepService, $stateParams) {

        $('.pmd-sidebar-nav>li>a').removeClass("active")
        $($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")

      //   

        var vm = this;
        $scope.totalCount = AreaPrepService.totalCount;
        $scope.AreaList = AreaPrepService;
        console.log($scope.AreaList);
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.cityName = CityByIdPrepService.titleDictionary[$scope.selectedLanguage];
        function refreshAreas() {

             

            var k = AreaResource.getAllAreas({ cityId: $stateParams.cityId, page: vm.currentPage }).$promise.then(function (results) {
                $scope.AreaList = results
                 

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
