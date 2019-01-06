using OrderNow.DAL.Entities;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.DataContext;
using Repository.Pattern.Ef6;
using Repository.Pattern.Ef6.Factories;
using Repository.Pattern.Repositories;
using Repository.Pattern.UnitOfWork;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace OrderNow.DAL
{
    public static class OrderNowDalConfig
    {
        public static void RegisterTypes(IUnityContainer container)
        {
            container
                .RegisterType<IDataContextAsync, OrderNowContext>(new PerResolveLifetimeManager())
                .RegisterType<IUnitOfWorkAsync, UnitOfWork>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryProvider, RepositoryProvider>(
                    new PerResolveLifetimeManager(), new InjectionConstructor(new object[] { new RepositoryFactories() })
                )
                .RegisterType<IRepositoryAsync<User>, Repository<User>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RefreshToken>, Repository<RefreshToken>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Supervisor>, Repository<Supervisor>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Client>, Repository<Client>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Request>, Repository<Request>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RequestDetail>, Repository<RequestDetail>>(new PerResolveLifetimeManager())
                
                .RegisterType<IRepositoryAsync<SupervisorFeature>, Repository<SupervisorFeature>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Package>, Repository<Package>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Admin>, Repository<Admin>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantAdmin>, Repository<RestaurantAdmin>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantWaiter>, Repository<RestaurantWaiter>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Category>, Repository<Category>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<CategoryTranslation>, Repository<CategoryTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Item>, Repository<Item>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemTranslation>, Repository<ItemTranslation>>(new PerResolveLifetimeManager()) 
                .RegisterType<IRepositoryAsync<Table>, Repository<Table>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Restaurant>, Repository<Restaurant>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RestaurantTranslation>, Repository<RestaurantTranslation>>(new PerResolveLifetimeManager()) 
                .RegisterType<IRepositoryAsync<Size>, Repository<Size>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SizeTranslation>, Repository<SizeTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItem>, Repository<SideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<SideItemTranslation>, Repository<SideItemTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSideItem>, Repository<ItemSideItem>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<ItemSize>, Repository<ItemSize>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Background>, Repository<Background>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Country>, Repository<Country>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<CountryTranslation>, Repository<CountryTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Region>, Repository<Region>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<RegionTranslation>, Repository<RegionTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<City>, Repository<City>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<CityTranslation>, Repository<CityTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Area>, Repository<Area>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<AreaTranslation>, Repository<AreaTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Branch>, Repository<Branch>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<Table>, Repository<Table>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<BranchTranslation>, Repository<BranchTranslation>>(new PerResolveLifetimeManager())
                .RegisterType<IRepositoryAsync<FeedBack>, Repository<FeedBack>>(new PerResolveLifetimeManager());

        }

    }
}
