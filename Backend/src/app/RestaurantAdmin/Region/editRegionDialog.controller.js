(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editRegionDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate', 'RegionResource', 'ToastService',
            'RegionByIdPrepService','$stateParams','CountryByIdPrepService', editRegionDialogController])

    function editRegionDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, RegionResource, ToastService, 
        RegionByIdPrepService,$stateParams,CountryByIdPrepService) {
      //    
        
        var vm = this; 
		vm.language = appCONSTANTS.supportedLanguage;
        vm.Region = RegionByIdPrepService; 
        $scope.countryName = CountryByIdPrepService.titleDictionary[$scope.selectedLanguage];
        
        vm.Close = function () {
            $state.go('Regions',{countryId: $stateParams.countryId });
        }
        vm.UpdateRegion  = function () { 
           //   
            
            var updateObj = new RegionResource();
            updateObj.regionId = vm.Region.regionId;
            updateObj.countryId= $stateParams.countryId;
            updateObj.titleDictionary = vm.Region.titleDictionary;
		    updateObj.IsDeleted = false;
		    updateObj.IsStatic = false;
		    updateObj.$update().then(
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('Editeduccessfully'), "success");
 
                    $state.go('Regions',{countryId: $stateParams.countryId },{ reload: true });

                },
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
        
	}	
}());
