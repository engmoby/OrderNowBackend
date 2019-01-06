using System.Web.Http;
using Unity;
using Unity.WebApi;
using Unity.Lifetime;
using System;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.BLL;
using OrderNow.BLL.Services;

namespace E_Guest.API
{
    public static class UnityConfig
    {

        #region Unity Container
        private static Lazy<IUnityContainer> container = new Lazy<IUnityContainer>(() =>
        {
            var container = new UnityContainer();
            ApplyMapping(container, true);
            return container;
        });

        /// <summary>
        /// Gets the configured Unity container.
        /// </summary>
        public static IUnityContainer GetConfiguredContainer()
        {
            return container.Value;
        }
        #endregion
        public static void RegisterTypes(HttpConfiguration config)
        {
            // NOTE: To load from web.config uncomment the line below. Make sure to add a Microsoft.Practices.Unity.Configuration to the using statements.
            // container.LoadConfiguration();
            var container = new UnityContainer();

            // TODO: Register your types here

            ApplyMapping(container, false);


            //GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
            GlobalConfiguration.Configuration.DependencyResolver =
                config.DependencyResolver = new UnityDependencyResolver(container);


        }

        public static void ApplyMapping(IUnityContainer container, bool applyDependencyResolver)
        {
            container.RegisterType<IUserFacade, UserFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRefreshTokenFacade, RefreshTokenFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRequestFacade, RequestFacade>(new PerResolveLifetimeManager())

                .RegisterType<IRestaurantFacade, RestaurantFacade>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryFacade, CategoryFacade>(new PerResolveLifetimeManager())
                .RegisterType<IitemFacade, ItemFacade>(new PerResolveLifetimeManager())
                .RegisterType<ISizeFacade, SizeFacade>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemFacade, SideItemFacade>(new PerResolveLifetimeManager())
                .RegisterType<IBackgroundFacade, BackgroundFacade>(new PerResolveLifetimeManager())
                .RegisterType<IAreaFacade, AreaFacade>(new PerResolveLifetimeManager())
                .RegisterType<IBranchFacade, BranchFacade>(new PerResolveLifetimeManager())
                .RegisterType<ITableFacade, TableFacade>(new PerResolveLifetimeManager())
                .RegisterType<ICountryFacade, CountryFacade>(new PerResolveLifetimeManager())
                .RegisterType<IRegionFacade, RegionFacade>(new PerResolveLifetimeManager())
                .RegisterType<ICityFacade, CityFacade>(new PerResolveLifetimeManager())
                .RegisterType<IDashboardFacade, DashboardFacade>(new PerResolveLifetimeManager())

               .RegisterType<IFeedBackFacade, FeedBackFacade>(new PerResolveLifetimeManager());



            OrderNowBllConfig.RegisterTypes(container);

            if (applyDependencyResolver)
                GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);


        }
    }
}