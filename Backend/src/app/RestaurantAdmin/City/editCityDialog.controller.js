(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editCityDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate', 'CityResource', 'ToastService',
            'CityByIdPrepService','$stateParams','RegionByIdPrepService', editCityDialogController])

    function editCityDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, CityResource, ToastService, 
        CityByIdPrepService,$stateParams,RegionByIdPrepService) {
      //    
        
        var vm = this; 
		vm.language = appCONSTANTS.supportedLanguage;
        vm.City = CityByIdPrepService;
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        
        vm.Close = function () {
            $state.go('Cities',{countryId: $stateParams.countryId,regionId: $stateParams.regionId });
        }
        vm.UpdateCity  = function () { 
          ///    
            
            var updateObj = new CityResource();
            updateObj.cityId = vm.City.cityId;
            updateObj.regionId= $stateParams.regionId;
            updateObj.titleDictionary = vm.City.titleDictionary;
		    updateObj.IsDeleted = false;
		    updateObj.IsStatic = false;
		    updateObj.$update().then(
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('Editeduccessfully'), "success");
 
                    $state.go('Cities',{countryId: $stateParams.countryId,regionId: $stateParams.regionId},{ reload: true });

                },
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
        
	}	
}());
