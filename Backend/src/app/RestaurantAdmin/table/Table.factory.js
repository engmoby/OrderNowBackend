(function () {
    angular
      .module('home')
        .factory('TableResource', ['$resource', 'appCONSTANTS', TableResource]) 

    function TableResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Tables/', {}, {
            getAllTables: { method: 'GET', url: appCONSTANTS.API_URL + 'Tables/GetAllTables', useToken: true,  params: { lang: '@lang' } },
            create: { method: 'POST', useToken: true },
            update: { method: 'POST', url: appCONSTANTS.API_URL + 'Tables/EditTable', useToken: true },
            getTable: { method: 'GET', url: appCONSTANTS.API_URL + 'Tables/GetTableById/:TableId', useToken: true }
        })
    } 

}());
