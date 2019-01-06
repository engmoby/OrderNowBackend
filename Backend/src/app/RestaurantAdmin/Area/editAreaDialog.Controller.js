(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editAreaDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate', 'AreaResource', 'ToastService',
            'AreaByIdPrepService','$stateParams','CityByIdPrepService','RegionByIdPrepService', editAreaDialogController])

    function editAreaDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, AreaResource, ToastService, 
        AreaByIdPrepService, $stateParams, CityByIdPrepService, RegionByIdPrepService) {
       //   
        
        var vm = this; 
		vm.language = appCONSTANTS.supportedLanguage;
        vm.Area = AreaByIdPrepService; 
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.cityName = CityByIdPrepService.titleDictionary[$scope.selectedLanguage];
     
        vm.close = function () {
            $state.go('Area', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId });
        }
        vm.UpdateArea = function () { 
         //     
            
            var updateObj = new AreaResource();
            updateObj.AreaId = vm.Area.areaId;
            updateObj.cityId= $stateParams.cityId;                        
            updateObj.titleDictionary = vm.Area.titleDictionary;
		    updateObj.IsDeleted = false;
		    updateObj.IsStatic = false;
		    updateObj.$update().then(
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('Editeduccessfully'), "success");
                    $state.go('Area', { countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId },{ reload: true });

                },
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
        
	}	
}());
