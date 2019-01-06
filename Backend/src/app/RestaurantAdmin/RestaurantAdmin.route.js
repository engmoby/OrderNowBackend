(function () {
    'use strict';

    angular
        .module('home')
        .config(function ($stateProvider, $urlRouterProvider) {

            $stateProvider

                .state('Category', {
                    url: '/category',
                    templateUrl: './app/RestaurantAdmin/templates/Category.html',
                    controller: 'categoryController',
                    'controllerAs': 'categoryCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Category'
                    },
                    resolve: {
                        categoriesPrepService: categoriesPrepService
                    }
                })
                .state('newCategory', {
                    url: '/NewCategory',
                    templateUrl: './app/RestaurantAdmin/templates/newCategory.html',
                    controller: 'categoryDialogController',
                    'controllerAs': 'categoryDlCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Category'
                    }
                })
                .state('editCategory', {
                    url: '/Category/:categoryId',
                    templateUrl: './app/RestaurantAdmin/templates/editCategory.html',
                    controller: 'editCategoryDialogController',
                    'controllerAs': 'editCategoryDlCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Category'
                    },
                    resolve: {
                        categoryPrepService: categoryPrepService
                    }
                })
                .state('size', {
                    url: '/size',
                    templateUrl: './app/RestaurantAdmin/templates/size.html',
                    controller: 'sizeController',
                    'controllerAs': 'sizeCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Size'
                    },
                    resolve: {
                        sizesPrepService: sizesPrepService
                    }

                })
                .state('newsize', {
                    url: '/Newsize',
                    templateUrl: './app/RestaurantAdmin/templates/newSize.html',
                    controller: 'sizeDialogController',
                    'controllerAs': 'sizeDlCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Size'
                    }
                })
                .state('editsize', {
                    url: '/size/:sizeId',
                    templateUrl: './app/RestaurantAdmin/templates/editSize.html',
                    controller: 'editSizeDialogController',
                    'controllerAs': 'editSizeDlCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Size'
                    },
                    resolve: {
                        sizePrepService: sizePrepService
                    }
                })
                /*.state('sideItem', {
                      url: '/SideItem',
                      templateUrl: './app/RestaurantAdmin/templates/sideItem.html',
                      controller: 'sideItemController',
                      'controllerAs': 'sideItemCtrl',
                      data: {
                          permissions: {
                              only: ['RestaurantAdmin'],
                             redirectTo: 'root'
                          },
                          displayName: 'SideItem'
                      },
                      resolve: {
                        sideItemPrepService: sideItemPrepService
                      }
                   
                  })*/
                .state('Items', {
                    url: '/Category/:categoryId/Item',
                    templateUrl: './app/RestaurantAdmin/templates/Item.html',
                    controller: 'ItemController',
                    'controllerAs': 'itemCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Category'
                    },
                    resolve: {
                        itemsPrepService: itemsPrepService
                    }
                })
                .state('newItem', {
                    url: '/Category/:categoryId/newItem',
                    templateUrl: './app/RestaurantAdmin/templates/newItem.html',
                    controller: 'newItemController',
                    'controllerAs': 'newItemCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Items'
                    },
                    resolve: {
                        ItemSizePrepService: ItemSizePrepService,
                        ItemSideItemPrepService: ItemSideItemPrepService,
                        defaultItemsPrepService: defaultItemsPrepService,
                    }
                })
                .state('editItem', {
                    url: '/Category/:categoryId/Items/:itemId',
                    templateUrl: './app/RestaurantAdmin/templates/editItem.html',
                    controller: 'editItemController',
                    'controllerAs': 'editItemCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Items'
                    },
                    resolve: {
                        itemPrepService: itemPrepService,
                        ItemSizePrepService: ItemSizePrepService,
                        ItemSideItemPrepService: ItemSideItemPrepService
                    }
                })
                .state('Waiters', {
                    url: '/Waiter',
                    templateUrl: './app/RestaurantAdmin/templates/waiter.html',
                    controller: 'WaiterController',
                    'controllerAs': 'waiterCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Waiters'
                    },
                    resolve: {
                        waitersPrepService: waitersPrepService
                        /*WaitersLimitPrepService:WaitersLimitPrepService*/
                    }
                })
                .state('Supervisors', {
                    url: '/Supervisor',
                    templateUrl: './app/RestaurantAdmin/templates/Supervisor.html',
                    controller: 'SupervisorController',
                    'controllerAs': 'SupervisorCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Supervisors'
                    },
                    resolve: {
                        SupervisorsPrepService: SupervisorsPrepService
                        /*SupervisorsLimitPrepService:SupervisorsLimitPrepService*/
                    }
                })
                .state('Backgrounds', {
                    url: '/Background',
                    templateUrl: './app/RestaurantAdmin/templates/background.html',
                    controller: 'BackgroundController',
                    'controllerAs': 'backgroundCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Backgrounds'
                    },
                    resolve: {
                        backgroundsPrepService: backgroundsPrepService
                    }
                })

                .state('Area', {

                    url: '/:cityId/Area',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/Area/templates/Area.html',
                            controller: 'AreaController',
                            'controllerAs': 'AreaCtrl',
                        }
                    },
                    resolve: {
                        AreaPrepService: AreaPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                        RegionByIdPrepService: RegionByIdPrepService,

                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Cities",
                    ncyBreadcrumb: {
                        label: '{{cityName}}'
                    }

                })
                .state('newArea', {
                    url: '/:cityId/newArea',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/Area/templates/new.html',
                            controller: 'createAreaDialogController',
                            'controllerAs': 'newAreaCtrl',
                        }
                    },
                    resolve: {
                        CityByIdPrepService: CityByIdPrepService,
                        RegionByIdPrepService: RegionByIdPrepService,

                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Cities",
                    ncyBreadcrumb: {
                        label: '{{cityName}}'
                    }

                })
                .state('editArea', {
                    url: '/:cityId/editArea/:areaId',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/Area/templates/edit.html',
                            controller: 'editAreaDialogController',
                            'controllerAs': 'editAreaCtrl',
                        }
                    },
                    resolve: {
                        AreaByIdPrepService: AreaByIdPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                        RegionByIdPrepService: RegionByIdPrepService,

                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Cities",
                    ncyBreadcrumb: {
                        label: '{{cityName}}'
                    }

                })
                .state('Branchs', {
                    url: '/:areaId/Branchs',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/branch/templates/branch.html',
                            controller: 'BranchController',
                            'controllerAs': 'BranchCtrl',
                        }
                    },
                    resolve: {
                        //  BranchPrepService: BranchPrepService,
                        AreaPrepService: AreaPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                        // RegionByIdPrepService: RegionByIdPrepService,

                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Area",
                    ncyBreadcrumb: {
                        label: '{{areaName}}'
                    }

                })
                .state('newBranch', {
                    url: '/:areaId/newBranch',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/branch/templates/new.html',
                            controller: 'createBranchDialogController',
                            'controllerAs': 'newBranchCtrl',
                        }
                    },
                    resolve: {
                        AreaByIdPrepService: AreaByIdPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Area",
                    ncyBreadcrumb: {
                        label: '{{areaName}}'
                    }

                })
                .state('editBranch', {
                    url: '/:areaId/editBranch/:branchId',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/branch/templates/edit.html',
                            controller: 'editBranchDialogController',
                            'controllerAs': 'editBranchCtrl',
                        }
                    },
                    resolve: {
                        BranchByIdPrepService: BranchByIdPrepService,
                        AreaByIdPrepService: AreaByIdPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Area",
                    ncyBreadcrumb: {
                        label: '{{areaName}}'
                    }
                })
                .state('newTable', {
                    url: '/:branchId/newTable',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/table/templates/new.html',
                            controller: 'createTableDialogController',
                            'controllerAs': 'newTableCtrl',
                        }
                    },
                    resolve: {
                        BranchByIdPrepService: BranchByIdPrepService,
                        AreaByIdPrepService: AreaByIdPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                        RegionByIdPrepService: RegionByIdPrepService,
                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Branchs",
                    ncyBreadcrumb: {
                        label: '{{branchName}}'
                    }

                })
                .state('editTable', {
                    url: '/:branchId/editTable/:tableId',
                    views: {
                        '@': {
                            templateUrl: './app/RestaurantAdmin/table/templates/edit.html',
                            controller: 'editTableDialogController',
                            'controllerAs': 'editTableCtrl',
                        }
                    },
                    resolve: {
                        TableByIdPrepService: TableByIdPrepService,
                        BranchByIdPrepService: BranchByIdPrepService,
                        AreaByIdPrepService: AreaByIdPrepService,
                        CityByIdPrepService: CityByIdPrepService,
                        RegionByIdPrepService: RegionByIdPrepService,
                    },
                    data: {
                        permissions: {
                            only: ['4'],
                            redirectTo: 'root'
                        }
                    },
                    parent: "Branchs",
                    ncyBreadcrumb: {
                        label: '{{branchName}}'
                    }
                })

                .state('itemOrder', {
                    url: '/order',
                    templateUrl: './app/RestaurantAdmin/templates/itemOrder.html',
                    controller: 'itemOrderController',
                    'controllerAs': 'itemOrderDlCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'itemOrder'
                    },
                    resolve: {
                        allMenuPrepService: allMenuPrepService
                    }
                })
                .state('feedBacks', {
                    url: '/feedback',
                    templateUrl: './app/RestaurantAdmin/templates/feedbacks.html',
                    controller: 'feedBackController',
                    'controllerAs': 'feedBackCtrl',
                    data: {
                        permissions: {
                            only: ['RestaurantAdmin'],
                            redirectTo: 'root'
                        },
                        displayName: 'Menu'
                    },
                    resolve: {
                        feedBacksPrepService: feedBacksPrepService
                    }

                })


                .state('Dashboard', {
                    url: '/Dashboard',
                    templateUrl: './app/RestaurantAdmin/dashboard/templates/dashboard.html',
                    controller: 'dashboardController',
                    'controllerAs': 'dashboardCtrl',
                    resolve: {
                        requestsPrepService: requestsPrepService,
                        RequestDashboardPrepService: RequestDashboardPrepService,
                        ItemDashboardPrepService: ItemDashboardPrepService,
                        CountriesPrepService: CountriesPrepService,
                        LatestItemsPrepService: LatestItemsPrepService
                    }

                })

        });

    menusPrepService.$inject = ['MenuResource']
    function menusPrepService(MenuResource) {
        return MenuResource.getAllMenus().$promise;
    }

    menuPrepService.$inject = ['MenuResource', '$stateParams']
    function menuPrepService(MenuResource, $stateParams) {
        return MenuResource.getMenu({ menuId: $stateParams.menuId }).$promise;
    }

    categoriesPrepService.$inject = ['GetCategoriesResource', '$stateParams']
    function categoriesPrepService(GetCategoriesResource, $stateParams) {
        return GetCategoriesResource.getAllCategories({ MenuId: $stateParams.menuId }).$promise;
    }

    categoryPrepService.$inject = ['CategoryResource', '$stateParams']
    function categoryPrepService(CategoryResource, $stateParams) {
        return CategoryResource.getCategory({ categoryId: $stateParams.categoryId }).$promise;
    }

    sizesPrepService.$inject = ['SizeResource']
    function sizesPrepService(SizeResource) {
        return SizeResource.getAllSizes().$promise;
    }

    sizePrepService.$inject = ['SizeResource', '$stateParams']
    function sizePrepService(SizeResource, $stateParams) {
        return SizeResource.getSize({ sizeId: $stateParams.sizeId }).$promise;
    }

    sideItemPrepService.$inject = ['SideItemResource']
    function sideItemPrepService(SideItemResource) {
        return SideItemResource.getAllSideItems().$promise;
    }

    itemsPrepService.$inject = ['GetItemsResource', '$stateParams']
    function itemsPrepService(GetItemsResource, $stateParams) {
        return GetItemsResource.getAllItems({ CategoryId: $stateParams.categoryId }).$promise;
    }

    ItemSizePrepService.$inject = ['SizeResource']
    function ItemSizePrepService(SizeResource) {
        return SizeResource.getAllSizes({ pagesize: 0 }).$promise;
    }

    ItemSideItemPrepService.$inject = ['SideItemResource']
    function ItemSideItemPrepService(SideItemResource) {
        return SideItemResource.getAllSideItems({ pagesize: 0 }).$promise;
    }

    itemPrepService.$inject = ['ItemResource', '$stateParams']
    function itemPrepService(ItemResource, $stateParams) {
        return ItemResource.getItem({ itemId: $stateParams.itemId }).$promise;
    }

    defaultItemsPrepService.$inject = ['GetItemNamesResource', '$stateParams', '$localStorage', 'appCONSTANTS']
    function defaultItemsPrepService(GetItemNamesResource, $stateParams, $localStorage, appCONSTANTS) {
        if ($localStorage.language != appCONSTANTS.defaultLanguage) {
            return GetItemNamesResource.getAllItemNames({ CategoryId: $stateParams.categoryId, lang: appCONSTANTS.defaultLanguage }).$promise;
        }
        else
            return null;
    }

    RestaurantIsReadyPrepService.$inject = ['CheckRestaurantReadyResource']
    function RestaurantIsReadyPrepService(CheckRestaurantReadyResource) {
        return CheckRestaurantReadyResource.IsReady().$promise;
    }

    waitersPrepService.$inject = ['WaiterResource']
    function waitersPrepService(WaiterResource) {
        return WaiterResource.getAllWaiters().$promise;
    }
    SupervisorsPrepService.$inject = ['SupervisorResource']
    function SupervisorsPrepService(SupervisorResource) {
        return SupervisorResource.getAllSupervisors().$promise;
    }
    backgroundsPrepService.$inject = ['BackgroundResource']
    function backgroundsPrepService(BackgroundResource) {
        return BackgroundResource.getAllBackgrounds().$promise;
    }


    templatesPrepService.$inject = ['TemplateResource']
    function templatesPrepService(TemplateResource) {
        return TemplateResource.getTemplates().$promise;
    }

    allMenuPrepService.$inject = ['ActivatedMenuResource']
    function allMenuPrepService(ActivatedMenuResource) {
        return ActivatedMenuResource.getAllMenusName().$promise;
    }

    branchsPrepService.$inject = ['BranchResource']
    function branchsPrepService(BranchResource) {
        return BranchResource.getAllBranches().$promise;
    }

    branchPrepService.$inject = ['BranchResource', '$stateParams']
    function branchPrepService(BranchResource, $stateParams) {
        return BranchResource.getBranch({ branchId: $stateParams.branchId }).$promise;
    }



    tablesPrepService.$inject = ['TableResource']
    function tablesPrepService(TableResource) {
        return TableResource.getAllTables().$promise;
    }

    tablePrepService.$inject = ['TableResource', '$stateParams']
    function tablePrepService(TableResource, $stateParams) {
        return TableResource.getTable({ tableId: $stateParams.tableId }).$promise;
    }


    TableByIdPrepService.$inject = ['TableResource', '$stateParams']
    function TableByIdPrepService(TableResource, $stateParams) {
        return TableResource.getTable({ tableId: $stateParams.tableId }).$promise;
    }


    WaitersLimitPrepService.$inject = ['WaitersLimitResource']
    function WaitersLimitPrepService(WaitersLimitResource) {
        return WaitersLimitResource.getWaitersLimit().$promise;
    }
    feedBacksPrepService.$inject = ['FeedBackResource']
    function feedBacksPrepService(FeedBackResource) {
        return FeedBackResource.getAllFeedBack().$promise;
    }
    /*Area */
    AreaPrepService.$inject = ['AreaResource', '$stateParams']
    function AreaPrepService(AreaResource, $stateParams) {
        return AreaResource.getAllAreas({ cityId: $stateParams.cityId }).$promise;
    }

    AllAreaPrepService.$inject = ['AreaResource']
    function AllAreaPrepService(AreaResource) {
        return AreaResource.getAllAreas({ pageSize: 0 }).$promise;
    }

    AreaByIdPrepService.$inject = ['AreaResource', '$stateParams']
    function AreaByIdPrepService(AreaResource, $stateParams) {
        return AreaResource.getArea({ areaId: $stateParams.areaId }).$promise;
    }


    /*Branch */
    BranchPrepService.$inject = ['BranchResource']
    function BranchPrepService(BranchResource) {
        return BranchResource.getAllBranchs().$promise;
    }

    BranchByIdPrepService.$inject = ['BranchResource', '$stateParams']
    function BranchByIdPrepService(BranchResource, $stateParams) {
        return BranchResource.getBranch({ branchId: $stateParams.branchId }).$promise;
    }



    CityByIdPrepService.$inject = ['CityResource', '$stateParams']
    function CityByIdPrepService(CityResource, $stateParams) {
        return CityResource.getCity({ cityId: $stateParams.cityId }).$promise;
    }

    RegionByIdPrepService.$inject = ['RegionResource', '$stateParams']
    function RegionByIdPrepService(RegionResource, $stateParams) {
        return RegionResource.getRegion({ regionId: $stateParams.regionId }).$promise;
    }
    CountriesPrepService.$inject = ['CountryResource']
    function CountriesPrepService(CountryResource) {
        return CountryResource.getAllCountries({ pageSize: 0 }).$promise;
    }

    RegionsForUserPrepService.$inject = ['RegionResource', '$stateParams']
    function RegionsForUserPrepService(RegionResource, $stateParams) {
        return RegionResource.getAllRegionsForUser({ userId: $stateParams.userId }).$promise;
    }
    CitiesForUserPrepService.$inject = ['CityResource', '$stateParams']
    function CitiesForUserPrepService(CityResource, $stateParams) {
        return CityResource.getAllCitiesForUser({ userId: $stateParams.userId }).$promise;
    }
    AreasForUserPrepService.$inject = ['AreaResource', '$stateParams']
    function AreasForUserPrepService(AreaResource, $stateParams) {
        return AreaResource.getAllAreasForUser({ userId: $stateParams.userId }).$promise;
    }
    BranchManagerPrepService.$inject = ['UserResource']
    function BranchManagerPrepService(UserResource) {
        return UserResource.getUserByTypeName({ type: 'branchmanager' }).$promise;
    }



    RequestDashboardPrepService.$inject = ['dashboardResource']
    function RequestDashboardPrepService(dashboardResource) {
        return dashboardResource.getRequestsDashboard().$promise;
    }
    ClientPrepService.$inject = ['ClientResource']
    function ClientPrepService(ClientResource) {
        return ClientResource.getClients().$promise;
    }

    requestsPrepService.$inject = ['RequestResource']
    function requestsPrepService(RequestResource) {
        return RequestResource.getAllRequest({ page: 0, pagesize: 5, statusId:1}).$promise;
    }
    
    LatestItemsPrepService.$inject = ['LatestItemsResource']
    function LatestItemsPrepService(LatestItemsResource) {
      //  return LatestItemsResource.GetLatestItems({ page: 0, pagesize: 1}).$promise;
    }

    ItemDashboardPrepService.$inject = ['dashboardResource']
    function ItemDashboardPrepService(dashboardResource) {
        return dashboardResource.GetItemCount().$promise;
    }
}());
