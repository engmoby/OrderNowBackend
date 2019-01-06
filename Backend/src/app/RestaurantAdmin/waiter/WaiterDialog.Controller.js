(function () {
	'use strict';

	angular
		.module('home')
		.controller('waiterDialogController', ['$uibModalInstance', '$translate', 'CountryResource', 'RegionResource', 'CityResource', 'AreaResource', 'CountriesPrepService', 'WaiterResource', 'ToastService', 'branches', 'callBackFunction', 'selectedLanguage', '$rootScope', waiterDialogController])

	function waiterDialogController($uibModalInstance, $translate, CountryResource, RegionResource, CityResource, AreaResource, CountriesPrepService, WaiterResource, ToastService, branches, callBackFunction, selectedLanguage, $rootScope) {
		var vm = this;
		vm.close = function () {
			$uibModalInstance.dismiss('cancel');
		}
		vm.Branches = branches;
		vm.selectedLanguage = selectedLanguage;
		if (branches.length > 0) {
			vm.selectedBranch = branches[0];
		}

		refreshCountries();
		 init();
		function init() {
			debugger;
			vm.counties = [];
			vm.selectedCountry = { countryId: 0, titleDictionary: {"en-us": "All Countries", "ar-eg": "كل البلاد" } };
			vm.counties.push(vm.selectedCountry);
			//vm.counties = vm.counties.concat(CountriesPrepService.results)
			console.log(vm.counties);
			vm.selectedRegion = { regionId: 0, titleDictionary: {"en-us": "All Regions", "ar-eg": "كل الأقاليم" } };
			vm.regions = [];
			vm.regions.push(vm.selectedRegion);
			vm.selectedCity = { cityId: 0, titleDictionary: {"en-us": "All Cities", "ar-eg": "كل المدن" } };
			vm.cities = [];
			vm.cities.push(vm.selectedCity);
			vm.selectedArea = { areaId: 0, titleDictionary: {"en-us": "All Areas", "ar-eg": "كل المناطق" } };
			vm.areaList = [];
			vm.areaList.push(vm.selectedArea);
			vm.selectedBranch = { branchId: 0, titleDictionary: {"en-us": "All Branches", "ar-eg": "كل الفروع" } };
			vm.branchList = [];
			vm.branchList.push(vm.selectedBranch);


		}
		vm.countryChange = function () {
			vm.selectedRegion = { regionId: 0, titleDictionary: {"en-us": "All Regions", "ar-eg": "كل الأقاليم" } };
			vm.selectedCity = { cityId: 0, titleDictionary: {"en-us": "All Cities", "ar-eg": "كل المدن" } };
			vm.selectedArea = { areaId: 0, titleDictionary: {"en-us": "All Areas", "ar-eg": "كل المناطق" } };
			vm.regions = [];
			vm.cities = [vm.selectedCity];
			vm.areaList = [vm.selectedArea];
			vm.regions.push(vm.selectedRegion);

			vm.branchList = [];
			vm.selectedBranch = { branchId: 0, titleDictionary: {"en-us": "All Branches", "ar-eg": "كل الفروع" } };
			vm.branchList.push(vm.selectedBranch);
			RegionResource.getAllRegions({ countryId: vm.selectedCountry.countryId, pageSize: 0 }).$promise.then(function (results) {

				vm.regions = vm.regions.concat(results.results);
			},
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				}); 
		}
		vm.regionChange = function () {
			if (vm.selectedRegion.regionId != undefined) {
				vm.cities = [];
				vm.areaList = [];
				vm.selectedCity = { cityId: 0, titleDictionary: {"en-us": "All Cities", "ar-eg": "كل المدن" } };
				vm.selectedArea = { areaId: 0, titleDictionary: {"en-us": "All Areas", "ar-eg": "كل المناطق" } };
				vm.cities.push(vm.selectedCity);
				vm.areaList = [vm.selectedArea];

				vm.branchList = [];
				vm.selectedBranch = { branchId: 0, titleDictionary: {"en-us": "All Branches", "ar-eg": "كل الفروع" } };
				vm.branchList.push(vm.selectedBranch);
				CityResource.getAllCities({ regionId: vm.selectedRegion.regionId, pageSize: 0 }).$promise.then(function (results) {

					vm.cities = vm.cities.concat(results.results);
				},
					function (data, status) {
						ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
					});
			}
		}
		vm.cityChange = function () {
			if (vm.selectedCity.cityId != undefined) {
				vm.areaList = [];
				vm.selectedArea = { areaId: 0, titleDictionary: {"en-us": "All Areas", "ar-eg": "كل المناطق" } };
				vm.areaList.push(vm.selectedArea);

				vm.branchList = [];
				vm.selectedBranch = { branchId: 0, titleDictionary: {"en-us": "All Branches", "ar-eg": "كل الفروع" } };
				vm.branchList.push(vm.selectedBranch);
				AreaResource.getAllAreas({ cityId: vm.selectedCity.cityId, pageSize: 0 }).$promise.then(function (results) {
					vm.areaList = vm.areaList.concat(results.results);
				},
					function (data, status) {
						ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
					});
			}
		}
		vm.areaChange = function () {
			vm.branchList = [];
			vm.selectedBranch = { branchId: 0, titleDictionary: {"en-us": "All Branches", "ar-eg": "كل الفروع" } };
			vm.branchList.push(vm.selectedBranch);
			if (vm.selectedArea.areaId > 0)
				vm.branchList = vm.branchList.concat(vm.selectedArea.branches);
		}
		function refreshCountries() {
			var k = CountryResource.getAllCountries().$promise.then(function (results) {
				vm.counties = results.results;

				console.log(vm.counties);

			},
				function (data, status) {


					ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
				});
		}
	
		vm.AddNewWaiter = function () {
			var newWaiter = new WaiterResource();
			newWaiter.userName = vm.userName;
			newWaiter.name = vm.name;
			newWaiter.password = vm.password;
			newWaiter.branchId = vm.selectedBranch.branchId;
			newWaiter.$create().then(
				function (data, status) {
					ToastService.show("right", "bottom", "fadeInUp", $translate.instant('WaiterAddSuccess'), "success");
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
