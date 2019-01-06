(function () {
    'use strict';

    angular
        .module('home')
        .controller('CountryController', ['$rootScope', 'blockUI', '$scope', '$filter', '$translate',
            '$state', 'CountryResource', 'CountriesPrepService',  '$localStorage', 'appCONSTANTS',
            'ToastService', CountryController]);


    function CountryController($rootScope, blockUI, $scope, $filter, $translate,
        $state, CountryResource, CountriesPrepService, $localStorage, appCONSTANTS, ToastService) { 

        $('.pmd-sidebar-nav>li>a').removeClass("active")
        $($('.pmd-sidebar-nav').children()[3].children[0]).addClass("active")
debugger
            
        var vm = this;
        $scope.totalCount = CountriesPrepService.totalCount;
        $scope.Countries  = CountriesPrepService;
        function refreshCountries() {

          //    
            
            var k = CountryResource.getAllCountries({page:vm.currentPage}).$promise.then(function (results) { 
                $scope.Countries = results  
                 
                
            },
            function (data, status) {
                 
                
                ToastService.show("right", "bottom", "fadeInUp", data.message, "error");
            });
        }
        
        vm.currentPage = 1;
        $scope.changePage = function (page) {
            vm.currentPage = page;
            refreshCountries();
        }
         
        
    }

})();
