(function () {
    'use strict';

    angular
        .module('home')
        .controller('dashboardController', ['blockUI', '$scope', '$state',
            '$translate', 'dashboardResource', 'RequestDashboardPrepService', '$filter', 'CountriesPrepService',
            'RegionResource', 'CityResource', 'AreaResource', 'ItemDashboardPrepService', 'requestsPrepService', 'LatestItemsPrepService', dashboardController]);

    function dashboardController(blockUI, $scope, $state,
        $translate, dashboardResource, RequestDashboardPrepService, $filter, CountriesPrepService,
        RegionResource, CityResource, AreaResource, ItemDashboardPrepService, requestsPrepService, LatestItemsPrepService) {

        // $('.pmd-sidebar-nav>li>a').removeClass("active")
        // $($('.pmd-sidebar-nav').children()[10].children[0]).addClass("active")
        blockUI.start("Loading...");

        var vm = this;
        vm.orderCounts = RequestDashboardPrepService;
        vm.ItemCount = ItemDashboardPrepService;
        vm.requests = requestsPrepService;
        vm.items = LatestItemsPrepService; 
        //   vm.clientCounts = ClientPrepService;
        function init() {
            vm.requestsFilter = [
                {
                    name: $translate.instant('Country'),
                    value: "country"
                },
                {
                    name: $translate.instant('Region'),
                    value: "region"
                },
                {
                    name: $translate.instant('City'),
                    value: "city"
                },
                {
                    name: $translate.instant('Area'),
                    value: "area"
                },
                {
                    name: $translate.instant('Branch'),
                    value: "branch"
                },
            ]
            vm.selectedRequestFilter = "branch"
            vm.options = {
                chart: {
                    type: 'multiBarChart',
                    height: 300,
                    margin: {
                        top: 20,
                        right: 20,
                        bottom: 65,
                        left: 50
                    },
                    stacked: true,

                    duration: 100,
                    xAxis: {
                        // axisLabel: 'X Axis',
                        rotateLabels: 30
                    },
                    yAxis: {
                        axisLabel: $translate.instant('requestsCount'),
                        axisLabelDistance: -10,

                    }
                }
            };
            loadRequestDashboard(RequestDashboardPrepService);
            //  vm.questionList = AnswerQuestionPrepService.results;


            //init requests filter
            vm.counties = [];
            vm.selectedCountry = { countryId: 0, titleDictionary: { "en": "All Countries", "ar": "كل البلاد" } };
            vm.counties.push(vm.selectedCountry);
            vm.counties = vm.counties.concat(CountriesPrepService.results)

            vm.selectedRegion = { regionId: 0, titleDictionary: { "en": "All Regions", "ar": "كل الأقاليم" } };
            vm.regions = [];
            vm.regions.push(vm.selectedRegion);
            vm.selectedCity = { cityId: 0, titleDictionary: { "en": "All Cities", "ar": "كل المدن" } };
            vm.cities = [];
            vm.cities.push(vm.selectedCity);
            vm.selectedArea = { areaId: 0, titleDictionary: { "en": "All Areas", "ar": "كل المناطق" } };
            vm.areaList = [];
            vm.areaList.push(vm.selectedArea);
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en": "All Branches", "ar": "كل الفروع" } };
            vm.branchList = [];
            vm.branchList.push(vm.selectedBranch);


        }
        //start request filter functions
        vm.countryChange = function () {
            vm.selectedRegion = { regionId: 0, titleDictionary: { "en": "All Regions", "ar": "كل الأقاليم" } };
            vm.selectedCity = { cityId: 0, titleDictionary: { "en": "All Cities", "ar": "كل المدن" } };
            vm.selectedArea = { areaId: 0, titleDictionary: { "en": "All Areas", "ar": "كل المناطق" } };
            vm.regions = [];
            vm.cities = [vm.selectedCity];
            vm.areaList = [vm.selectedArea];
            vm.regions.push(vm.selectedRegion);

            vm.branchList = [];
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en": "All Branches", "ar": "كل الفروع" } };
            vm.branchList.push(vm.selectedBranch);
            RegionResource.getAllRegions({ countryId: vm.selectedCountry.countryId, pageSize: 0 }).$promise.then(function (results) {

                vm.regions = vm.regions.concat(results.results);
            },
                function (data, status) {
                    ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                });
            blockUI.stop();
        }
        vm.regionChange = function () {
            if (vm.selectedRegion.regionId != undefined) {
                /*for (var i = vm.regions.length - 1; i >= 0; i--) {
                    if (vm.regions[i].regionId == 0) {
                        vm.regions.splice(i, 1);
                    }
                }*/
                vm.cities = [];
                vm.areaList = [];
                vm.selectedCity = { cityId: 0, titleDictionary: { "en": "All Cities", "ar": "كل المدن" } };
                vm.selectedArea = { areaId: 0, titleDictionary: { "en": "All Areas", "ar": "كل المناطق" } };
                vm.cities.push(vm.selectedCity);
                vm.areaList = [vm.selectedArea];

                vm.branchList = [];
                vm.selectedBranch = { branchId: 0, titleDictionary: { "en": "All Branches", "ar": "كل الفروع" } };
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
                /*for (var i = vm.cities.length - 1; i >= 0; i--) {
                    if (vm.cities[i].cityId == 0) {
                        vm.cities.splice(i, 1);
                    }
                }*/
                vm.areaList = [];
                vm.selectedArea = { areaId: 0, titleDictionary: { "en": "All Areas", "ar": "كل المناطق" } };
                vm.areaList.push(vm.selectedArea);

                vm.branchList = [];
                vm.selectedBranch = { branchId: 0, titleDictionary: { "en": "All Branches", "ar": "كل الفروع" } };
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
            // vm.areaList.splice(0, 1);
            vm.branchList = [];
            vm.selectedBranch = { branchId: 0, titleDictionary: { "en": "All Branches", "ar": "كل الفروع" } };
            vm.branchList.push(vm.selectedBranch);
            if (vm.selectedArea.areaId > 0)
                vm.branchList = vm.branchList.concat(vm.selectedArea.branches);
        }

        vm.departmentChange = function () {
            // vm.areaList.splice(0, 1);

            vm.categories = [];
            vm.selectedCategory = { categoryId: 0, titleDictionary: { "en": "All Categories", "ar": "كل الفئات" } };
            vm.categories.push(vm.selectedCategory);
            if (vm.selectedDepartment.departmentId > 0)
                vm.categories = vm.categories.concat(vm.selectedDepartment.categories);
        }
        vm.countryId = 0;
        vm.regionId = 0;
        vm.cityId = 0;
        vm.areaId = 0;
        vm.branchId = 0;

        vm.from = "";
        vm.to = "";

        vm.applyFilter = function () {
            vm.from = ""
            if ($('#fromdate').val() != "") {
                var fromDate = $('#fromdate').val().split('/')
                vm.from = (new Date(fromDate[1] + "/" + fromDate[0] + "/" + fromDate[2])).toISOString().replace('Z', '');
            }
            vm.to = ""
            if ($('#todate').val() != "") {
                var toDate = $('#todate').val().split('/')
                vm.to = (new Date(toDate[1] + "/" + toDate[0] + "/" + toDate[2])).toISOString().replace('Z', '');
            }
            vm.countryId = vm.selectedCountry.countryId;
            vm.regionId = vm.selectedRegion.regionId;
            vm.cityId = vm.selectedCity.cityId;
            vm.areaId = vm.selectedArea.areaId;
            vm.branchId = vm.selectedBranch.branchId;
            vm.status = vm.selectedStatus;
            vm.requestFilterChange()
        }
        function loadRequestDashboard(requests) {
            debugger;
            var Approved = [];
            var Pending = [];
            var Rejected = [];
            requests.forEach(function (element) {


                Pending.push({
                    x: element.xaxisName[$scope.selectedLanguage],
                    y: element.pendingCount
                })
                Rejected.push({
                    x: element.xaxisName[$scope.selectedLanguage],
                    y: element.rejectedCount
                })
                Approved.push({
                    x: element.xaxisName[$scope.selectedLanguage],
                    y: element.approvedCount
                })
            }, this);
            vm.data = [

                {
                    "key": $translate.instant('Pending'),
                    "values": Pending
                },
                {
                    "key": $translate.instant('RejectedStatus'),
                    "values": Rejected
                },
                {
                    "key": $translate.instant('ApprovedStatus'),
                    "values": Approved
                },
            ];
        }
        init();

        vm.requestFilterChange = function () {
            dashboardResource.getRequestsDashboard({
                xaxis: vm.selectedRequestFilter,
                countryId: vm.countryId, regionId: vm.regionId, cityId: vm.cityId,
                areaId: vm.areaId, branchId: vm.branchId, from: vm.from, to: vm.to,
                status: vm.status
            }).$promise
                .then(function (results) {
                    loadRequestDashboard(results)
                },
                    function (data, status) {
                        blockUI.stop();

                        ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
                    });
        }
        //end request filter

        blockUI.stop();

        
        
    }

}());