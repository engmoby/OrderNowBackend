(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('createCountryDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate',
            'CountryResource', 'ToastService', '$rootScope', createCountryDialogController])

    function createCountryDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, CountryResource,
        ToastService, $rootScope) {
        
     //     
            
		var vm = this;
		vm.language = appCONSTANTS.supportedLanguage;
		vm.close = function(){
			$state.go('Countries');
		} 
		 
		vm.AddNewCountry = function () {
        //      
            
            var newObj = new CountryResource();
            newObj.titleDictionary = vm.titleDictionary; 
            newObj.IsDeleted = false; 
            newObj.IsStatic =false;
            newObj.$create().then(
                function (data, status) { 
        ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success"); 
                    $state.go('Countries');
                              


                },
                function (data, status) {
                        

                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
  
	}	
}());
