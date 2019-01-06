(function () {
    angular
      .module('home')
        .factory('CityResource', ['$resource', 'appCONSTANTS', CityResource]) 

    function CityResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Cities/', {}, {
            getAllCities: { method: 'GET', url: appCONSTANTS.API_URL + 'Regions/:regionId/Cities', useToken: true,  params: { lang: '@lang' } },
            create: { method: 'POST', useToken: true },
            update: { method: 'POST', url: appCONSTANTS.API_URL + 'Cities/EditCity', useToken: true },
            getCity: { method: 'GET', url: appCONSTANTS.API_URL + 'Cities/:cityId', useToken: true },
            getAllCitiesForUser: { method: 'GET', url: appCONSTANTS.API_URL + 'Users/:userId/Cities', useToken: true, isArray:true }
        })
    } 

}());
