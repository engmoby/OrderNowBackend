(function() {
    'use strict';

    angular
        .module('home')
        .config(function($stateProvider, $urlRouterProvider) {

            $stateProvider
            .state('Countries', {
                url: '/Country',
                templateUrl: './app/RestaurantAdmin/Country/templates/Countries.html',
                controller: 'CountryController',
                'controllerAs': 'CountryCtrl',
                resolve: {
                    CountriesPrepService: CountriesPrepService
                },
                data: {
                    permissions: {
                        only: ['4'],
                        redirectTo: 'root'
                    }
                }

            })
            .state('newCountry', {
                url: '/newCountry',
                templateUrl: './app/RestaurantAdmin/Country/templates/new.html',
                controller: 'createCountryDialogController',
                'controllerAs': 'newCountryCtrl',
                data: {
                    permissions: {
                        only: ['4'],
                        redirectTo: 'root'
                    }
                }

            })
            .state('editCountry', {
                url: '/editCountry/:countryId',
                templateUrl: './app/RestaurantAdmin/Country/templates/edit.html',
                controller: 'editCountryDialogController',
                'controllerAs': 'editCountryCtrl',
                resolve: {
                    CountryByIdPrepService: CountryByIdPrepService
                },
                data: {
                    permissions: {
                        only: ['4'],
                        redirectTo: 'root'
                    }
                }

            })
        });
        
        CountriesPrepService.$inject = ['CountryResource']
        function CountriesPrepService(CountryResource) {
            return CountryResource.getAllCountries().$promise;
        }

        CountryByIdPrepService.$inject = ['CountryResource', '$stateParams']
        function CountryByIdPrepService(CountryResource, $stateParams) {
            return CountryResource.getCountry({ countryId: $stateParams.countryId }).$promise;
        }

}());
