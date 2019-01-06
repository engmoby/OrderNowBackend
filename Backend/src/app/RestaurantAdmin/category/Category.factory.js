(function() {
    angular
      .module('home')
      .factory('CategoryResource', ['$resource', 'appCONSTANTS', CategoryResource])
      .factory('GetCategoriesResource', ['$resource', 'appCONSTANTS', GetCategoriesResource])
      .factory('GetCategoriesNameResource', ['$resource', 'appCONSTANTS', GetCategoriesNameResource])
      .factory('ActivateCategoryResource', ['$resource', 'appCONSTANTS', ActivateCategoryResource])
      .factory('DeactivateCategoryResource', ['$resource', 'appCONSTANTS', DeactivateCategoryResource])
      .factory('LatestItemsResource', ['$resource', 'appCONSTANTS', GetLatestItemsForResturant]);
  
    function CategoryResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Categories/:categoryId', {}, {
        getCategory: { method: 'GET', useToken: true },
        create: { method: 'POST', useToken: true },
        deleteCategory: { method: 'DELETE', useToken: true },
        update: { method: 'PUT', useToken: true }
      })
    }
    function GetCategoriesResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Categories/GetAllCategories', {}, {
          getAllCategories: { method: 'GET', useToken: true, params:{lang:'@lang'} }
        })
    }
    
    function GetLatestItemsForResturant($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Categories/LatestItems', {}, {
        GetLatestItems: { method: 'GET', useToken: true, params:{lang:'@lang'} }
      })
  }
    function GetCategoriesNameResource($resource, appCONSTANTS) {
      return $resource(appCONSTANTS.API_URL + 'Categories/Categories/Name', {}, {
        getAllCategoriesName: { method: 'GET', useToken: true, params:{lang:'@lang'},isArray: true }
      })
  }

    function ActivateCategoryResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Categories/:categoryId/Activate', {}, {
          Activate: { method: 'GET', useToken: true}
        })
    }
    function DeactivateCategoryResource($resource, appCONSTANTS) {
        return $resource(appCONSTANTS.API_URL + 'Categories/:categoryId/DeActivate', {}, {
          Deactivate: { method: 'GET', useToken: true }
        })
    }
}());
  