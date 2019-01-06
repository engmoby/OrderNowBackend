(function () {
    'use strict';

    angular
        .module('home')
        .controller('createAreaDialogController', ['$scope', 'blockUI', '$http', '$state', 'appCONSTANTS', '$translate',
            'AreaResource', 'ToastService', '$stateParams', 'CityByIdPrepService', 'RegionByIdPrepService', createAreaDialogController])

    function createAreaDialogController($scope, blockUI, $http, $state, appCONSTANTS, $translate, AreaResource,
        ToastService, $stateParams, CityByIdPrepService, RegionByIdPrepService) {
       //  

        var vm = this;
        vm.language = appCONSTANTS.supportedLanguage;
        $scope.countryName = RegionByIdPrepService.countryNameDictionary[$scope.selectedLanguage];
        $scope.regionName = RegionByIdPrepService.titleDictionary[$scope.selectedLanguage];
        $scope.cityName = CityByIdPrepService.titleDictionary[$scope.selectedLanguage];
        vm.close = function(){
		    $state.go('Area',{ countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId });
		} 

        vm.AddNewArea = function () {
          //   

            var newObj = new AreaResource();
            newObj.cityId = $stateParams.cityId;
            newObj.titleDictionary = vm.titleDictionary;
            newObj.IsDeleted = false;
            newObj.IsStatic = false;
            newObj.$create().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('AddedSuccessfully'), "success");
                    $state.go('Area',{ countryId: $stateParams.countryId, regionId: $stateParams.regionId, cityId: $stateParams.cityId },{ reload: true });
                     


                },
                function (data, status) {
                     

                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
         

    }
}());
