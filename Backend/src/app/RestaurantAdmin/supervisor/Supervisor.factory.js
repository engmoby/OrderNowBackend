(function() {
    angular
      .module('home')
      .factory('SupervisorResource', ['$resource', 'appCONSTANTS', SupervisorResource])
      .factory('SupervisorsLimitResource', ['$resource', 'appCONSTANTS', SupervisorsLimitResource]);
  
    function SupervisorResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Supervisor/:SupervisorId', {}, {
        getAllSupervisors: { method: 'GET', useToken: true, params:{lang:'@lang'} },
        create: { method: 'POST', useToken: true },
        deleteSupervisor: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }

    function SupervisorsLimitResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Supervisors/Limit', {}, {
        getSupervisorsLimit: { method: 'GET', useToken: true ,transformResponse: function (data) {return {SupervisorLimit: angular.fromJson(data)} }},
      })
    }
    
  }());
  