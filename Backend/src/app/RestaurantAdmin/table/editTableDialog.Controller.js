(function () {
    'use strict';

    angular
        .module('home')
        .controller('editTableDialogController', ['$scope', '$http', '$state', 'appCONSTANTS', '$translate', 'TableResource', 'ToastService',
            'TableByIdPrepService','$stateParams','BranchByIdPrepService','CityByIdPrepService', 'RegionByIdPrepService', editTableDialogController])

    function editTableDialogController($scope, $http, $state, appCONSTANTS, $translate, TableResource, ToastService,
         TableByIdPrepService,$stateParams,BranchByIdPrepService,CityByIdPrepService, RegionByIdPrepService) {
        var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        vm.Table = TableByIdPrepService;
        console.log(vm.Table);
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.cityName = CityByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.areaName = BranchByIdPrepService.titleDictionary[$scope.selectedLanguage];
            
        vm.close = function () {
            $state.go('Branchs', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId, areaId: $stateParams.areaId });
        }
        vm.UpdateTable = function () {
            var updateObj = new TableResource();
            updateObj.tableId = vm.Table.tableId;
            updateObj.tableName = vm.Table.tableName;

            updateObj.$update().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('Editeduccessfully'), "success");
                    $state.go('Branchs', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId, areaId: $stateParams.areaId }, { reload: true });

                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }
}());
