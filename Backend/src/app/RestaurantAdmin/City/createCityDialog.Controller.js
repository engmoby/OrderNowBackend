(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('createCityDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate',
            'CityResource', 'ToastService', '$stateParams','RegionByIdPrepService', createCityDialogController])

    function createCityDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, CityResource,
        ToastService, $stateParams,RegionByIdPrepService) {
        
     //     
            
		var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        vm.close = function(){
			$state.go('Cities',{countryId: $stateParams.countryId ,regionId: $stateParams.regionId});
		} 
		 
		vm.AddNewCity = function () {
        //      
            
            var newObj = new CityResource();
            newObj.regionId= $stateParams.regionId;            
            newObj.titleDictionary = vm.titleDictionary; 
            newObj.IsDeleted = false; 
            newObj.IsStatic =false;
            newObj.$create().then(
                function (data, status) { 
        ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success"); 
                    $state.go('Cities',{countryId: $stateParams.countryId ,regionId: $stateParams.regionId},{ reload: true });
                              


                },
                function (data, status) {
                        

                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
  
	}	
}());
