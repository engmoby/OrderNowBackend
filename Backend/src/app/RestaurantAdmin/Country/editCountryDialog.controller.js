(function () {
    'use strict';
	
    angular
        .module('home')
        .controller('editCountryDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate', 'CountryResource', 'ToastService',
            'CountryByIdPrepService', editCountryDialogController])

    function editCountryDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, CountryResource, ToastService, CountryByIdPrepService) {
     //     
        debugger
        var vm = this; 
		vm.language = appCONSTANTS.supportedLanguage;
        vm.Country = CountryByIdPrepService; 
        vm.Close = function () {
            $state.go('Countries');
        }
        vm.UpdateCountry  = function () { 
         //     
            
            var updateObj = new CountryResource();
            updateObj.countryId = vm.Country.countryId;
            updateObj.titleDictionary = vm.Country.titleDictionary;
		    updateObj.IsDeleted = false;
		    updateObj.IsStatic = false;
		    updateObj.$update().then(
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('Editeduccessfully'), "success");
 
                    $state.go('Countries');

                },
                function (data, status) {
                     
                    
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         
        
	}	
}());
