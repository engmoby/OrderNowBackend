(function () {
    'use strict';

    angular
        .module('home')
        .controller('createTableDialogController', ['$scope', '$http', '$state', 'appCONSTANTS', '$translate',
            'TableResource', 'ToastService', '$stateParams', 'AreaByIdPrepService', 'BranchByIdPrepService', 'CityByIdPrepService', 'RegionByIdPrepService', createTableDialogController])

    function createTableDialogController($scope, $http, $state, appCONSTANTS, $translate, TableResource,
        ToastService, $stateParams, AreaByIdPrepService, BranchByIdPrepService, CityByIdPrepService, RegionByIdPrepService) {
        var vm = this;
        vm.Branch = BranchByIdPrepService;
        vm.language = appCONSTANTS.supportedLanguage;
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.cityName = CityByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.branchName = BranchByIdPrepService.titleDictionary[$scope.selectedLanguage];
        vm.close = function () {
            $state.go('Branchs', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId, areaId: $stateParams.areaId });
        } 
        vm.AddNewTable = function () {
            var newObj = new TableResource();
            newObj.branchId = vm.Branch.branchId;
            newObj.tableName = vm.tableName;
            newObj.$create().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success");
                    $state.go('Branchs', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId, areaId: $stateParams.areaId }, { reload: true });

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }

    }
}());
