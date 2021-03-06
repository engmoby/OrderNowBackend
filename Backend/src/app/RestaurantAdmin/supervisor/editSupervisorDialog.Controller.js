(function () {
    'use strict';

    angular
        .module('home')
        .controller('editSupervisorDialogController2', ['$uibModalInstance', '$timeout', '$filter', '$translate', 'CountryResource', 'RegionResource', 'CityResource',
            'AreaResource', 'SupervisorResource', 'ToastService', 'Supervisor', 'branches', 'callBackFunction', 'selectedLanguage', editSupervisorDialogController2])

    function editSupervisorDialogController2($uibModalInstance, $timeout, $filter, $translate, CountryResource, RegionResource, CityResource, AreaResource, SupervisorResource, ToastService, Supervisor, branches, callBackFunction, selectedLanguage) {
        var vm = this;
        vm.menuName = "";
        vm.Supervisor = Supervisor;
        vm.Supervisor.confirmPassword = Supervisor.password;
        vm.selectedLanguage = selectedLanguage;
        refreshCountries();
        //10 seconds delay
        $timeout(function () {
            init();

        }, 5000);

        function refreshCountries() {
            var k = CountryResource.getAllCountries().$promise.then(function (results) {
                vm.counties = results.results;
                debugger
                console.log(vm.counties);

            },
                function (data, status) {


                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
        }

        function init() {
            //  vm.counties = [];
          //  vm.selectedCountry = { countryId: 0, titleDictionary: { "en-us": "All Countries", "ar-eg": "كل البلاد" } };
           // vm.counties.push(vm.selectedCountry);
            //vm.counties = vm.counties.concat(CountriesPrepService.results)
            vm.selectedRegion = { regionId: 0, titleDictionary: { "en-us": "All Regions", "ar-eg": "كل الأقاليم" } };
            vm.regions = [];
            vm.regions.push(vm.selectedRegion);
            vm.selectedCity = { cityId: 0, titleDictionary: { "en-us": "All Cities", "ar-eg": "كل المدن" } };
            vm.cities = [];
            vm.cities.push(vm.selectedCity);
            vm.selectedArea = { areaId: 0, titleDictionary: { "en-us": "All Areas", "ar-eg": "كل المناطق" } };
            vm.areaList = [];
            vm.areaList.push(vm.selectedArea);
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en-us": "All Branches", "ar-eg": "كل الفروع" } };
            vm.branchList = [];
            vm.branchList.push(vm.selectedBranch);

            console.log(vm.counties);
            var indexCountry = vm.counties.indexOf($filter('filter')(vm.counties, { 'countryId': vm.Supervisor.countryId }, true)[0]);
            vm.selectedCountry = vm.counties[indexCountry];

            funcCountryChange();

        }
        function funcCountryChange() {
            vm.selectedRegion = { regionId: 0, titleDictionary: { "en-us": "All Regions", "ar-eg": "كل الأقاليم" } };
            vm.selectedCity = { cityId: 0, titleDictionary: { "en-us": "All Cities", "ar-eg": "كل المدن" } };
            vm.selectedArea = { areaId: 0, titleDictionary: { "en-us": "All Areas", "ar-eg": "كل المناطق" } };
            vm.regions = [];
            vm.cities = [vm.selectedCity];
            vm.areaList = [vm.selectedArea];
            vm.regions.push(vm.selectedRegion);

            vm.branchList = [];
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en-us": "All Branches", "ar-eg": "كل الفروع" } };
            vm.branchList.push(vm.selectedBranch);


            RegionResource.getAllRegions({ countryId: vm.selectedCountry.countryId, pageSize: 0 }).$promise.then(function (results) {

                vm.regions = vm.regions.concat(results.results);

                var indexregion = vm.regions.indexOf($filter('filter')(vm.regions, { 'regionId': vm.Supervisor.regionId }, true)[0]);
                vm.selectedRegion = vm.regions[indexregion];
                funcregionChange();
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
            // blockUI.stop();
        }


        vm.countryChange = function () {
            funcCountryChange();
        }
        function funcregionChange() {
            if (vm.selectedRegion.regionId != undefined) {
                vm.cities = [];
                vm.areaList = [];
                vm.selectedCity = { cityId: 0, titleDictionary: { "en-us": "All Cities", "ar-eg": "كل المدن" } };
                vm.selectedArea = { areaId: 0, titleDictionary: { "en-us": "All Areas", "ar-eg": "كل المناطق" } };
                vm.cities.push(vm.selectedCity);
                vm.areaList = [vm.selectedArea];

                vm.branchList = [];
                vm.selectedBranch = { branchId: 0, titleDictionary: { "en-us": "All Branches", "ar-eg": "كل الفروع" } };
                vm.branchList.push(vm.selectedBranch);
                CityResource.getAllCities({ regionId: vm.selectedRegion.regionId, pageSize: 0 }).$promise.then(function (results) {

                    vm.cities = vm.cities.concat(results.results);

                    var indexcity = vm.cities.indexOf($filter('filter')(vm.cities, { 'cityId': vm.Supervisor.cityId }, true)[0]);
                    vm.selectedCity = vm.cities[indexcity];
                    funcCityChange();
                },
                    function (data, status) {
                        ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                    });
            }
        }
        vm.regionChange = function () {
            funcregionChange();
        }
        function funcCityChange() {

            if (vm.selectedCity.cityId != undefined) {
                vm.areaList = [];
                vm.selectedArea = { areaId: 0, titleDictionary: { "en-us": "All Areas", "ar-eg": "كل المناطق" } };
                vm.areaList.push(vm.selectedArea);

                vm.branchList = [];
                vm.selectedBranch = { branchId: 0, titleDictionary: { "en-us": "All Branches", "ar-eg": "كل الفروع" } };
                vm.branchList.push(vm.selectedBranch);
                AreaResource.getAllAreas({ cityId: vm.selectedCity.cityId, pageSize: 0 }).$promise.then(function (results) {
                    vm.areaList = vm.areaList.concat(results.results);

                    var indexarea = vm.areaList.indexOf($filter('filter')(vm.areaList, { 'areaId': vm.Supervisor.areaId }, true)[0]);
                    vm.selectedArea = vm.areaList[indexarea];
                    funcAreaChange();

                },
                    function (data, status) {
                        ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                    });
            }
        }
        vm.cityChange = function () {
            funcCityChange();
        }
        function funcAreaChange() {
            vm.branchList = [];
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en-us": "All Branches", "ar-eg": "كل الفروع" } };
            vm.branchList.push(vm.selectedBranch);
            if (vm.selectedArea.areaId > 0)
                vm.branchList = vm.branchList.concat(vm.selectedArea.branches);


            var indexbranch = vm.branchList.indexOf($filter('filter')(vm.branchList, { 'branchId': vm.Supervisor.branchId }, true)[0]);
            vm.selectedBranch = vm.branchList[indexbranch];

        }
        vm.areaChange = function () {
            funcAreaChange();
        }
        vm.close = function () {
            $uibModalInstance.dismiss('cancel');
        }
        vm.Branches = branches;
        if (branches.length > 0) {
            branches.forEach(function (element) {
                if (element.branchId == vm.Supervisor.branchId) {
                    vm.selectedBranch = element;
                }
            }, this);
        }
        vm.updateSupervisor = function () {
            var newSupervisor = new SupervisorResource();
            newSupervisor.userName = vm.Supervisor.userName;
            newSupervisor.name = vm.Supervisor.name;
            newSupervisor.password = vm.Supervisor.password;
            newSupervisor.userId = Supervisor.userId;
            newSupervisor.branchId = vm.selectedBranch.branchId;
            newSupervisor.$update().then(
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", $translate.instant('SupervisorUpdateSuccess'), "success");
                    $uibModalInstance.dismiss('cancel');
                    callBackFunction();
                },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.data.message, "error");
                }
            );
        }
    }
}());
