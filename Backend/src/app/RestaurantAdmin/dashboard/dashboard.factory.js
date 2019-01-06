(function () {
    angular
        .module('home')
        .factory('dashboardResource', ['$resource', 'appCONSTANTS', dashboardResource])

    function dashboardResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Dashboard/', {}, {
            getRequestsDashboard: { method: 'GET', url: appCONSTANTS.API_URL + 'Dashboard', useToken: true, isArray: true },
            GetItemCount: { method: 'GET', url: appCONSTANTS.API_URL + 'ItemCount', useToken: true, isArray: true,  params: { status: 'Approved' } },
        })
    }

}());
