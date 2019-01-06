using System.Collections.Generic;
using System.Linq;
using System.Threading;
using AutoMapper;
using AutoMapper.Configuration;
using Unity;
using Unity.Lifetime;
using OrderNow.BLL.DataServices;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.ManageStorage;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using OrderNow.BLL;
using OrderNow.DAL;

namespace OrderNow.BLL
{
    public static class OrderNowBllConfig
    {
        public static void RegisterMappings(MapperConfigurationExpression mapperConfiguration)
        {
            mapperConfiguration.CreateMap<RefreshToken, RefreshTokenDto>().ReverseMap();

            mapperConfiguration.CreateMap<Supervisor, SupervisorDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)));
            mapperConfiguration.CreateMap<SupervisorDto, Supervisor>();

            mapperConfiguration.CreateMap<Client, ClientDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)));
            mapperConfiguration.CreateMap<ClientDto, Client>();

            mapperConfiguration.CreateMap<RequestDto, Request>()
                .ForMember(dto => dto.Table, m => m.MapFrom(src => src.Table))
                .ForMember(dto => dto.ModifiedBy, m => m.Ignore());
            mapperConfiguration.CreateMap<Request, RequestDto>()
                    .ForMember(dto => dto.RestaurantName,
                    m => m.MapFrom(src => src.Restaurant.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantName)
                        ))

                .ForMember(dto => dto.Table, m => m.MapFrom(src => src.Table))
                .ForMember(dto => dto.TableId, m => m.MapFrom(src => src.Creater.TableId))
                .ForMember(dto => dto.TableName, m => m.MapFrom(src => src.Creater.TableName))
                .ForMember(dto => dto.CreateTime, m => m.MapFrom(src => src.CreateTime))
                .ForMember(dto => dto.ModifyTime, m => m.MapFrom(src => src.ModifyTime))
                .ForMember(dto => dto.Modifier, m => m.MapFrom(src => src.Modifier.UserName))
                .ForMember(dto => dto.RequestDetails, m => m.MapFrom(src => src.RequestDetails));


            mapperConfiguration.CreateMap<RequestDetail, RequestDetailDto>()
                .ForMember(dto => dto.DescriptionDictionary,
                    m => m.MapFrom(src => src.ItemSize.Item.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemName)))
                .ForMember(dto => dto.Price, m => m.MapFrom(src => src.Price))
                .ForMember(dto => dto.ItemId, m => m.MapFrom(src => src.ItemSize.ItemId));
            mapperConfiguration.CreateMap<RequestDetailDto, RequestDetail>();



            mapperConfiguration.CreateMap<Restaurant, RestaurantDTO>()
                .ForMember(dto => dto.RestaurantName,
                    m => m.MapFrom(src => src.RestaurantTranslations.FirstOrDefault().RestaurantName))
                .ForMember(dto => dto.RestaurantDescription,
                    m => m.MapFrom(src => src.RestaurantTranslations.FirstOrDefault().RestaurantDescription))

                .ForMember(dto => dto.RestaurantAdminPassword, m => m.MapFrom(src => PasswordHelper.Decrypt(src.RestaurantAdmin.Password)))
                .ForMember(dto => dto.Image, m => m.Ignore())
                .ForMember(dto => dto.RestaurantNameDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantName)))
                .ForMember(dto => dto.RestaurantDescriptionDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantDescription)));

            //mapperConfiguration.CreateMap<Restaurant, RestaurantDTO>()
            //    .ForMember(dto => dto.RestaurantNameDictionary, m => m.MapFrom(src => src.RestaurantTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.RestaurantName)));

            mapperConfiguration.CreateMap<CategoryDTO, Category>();
            mapperConfiguration.CreateMap<Category, CategoryDTO>()
                .ForMember(dest => dest.Items, m => m.MapFrom(src => src.Items.Where(x => x.IsDeleted)))
                .ForMember(dest => dest.CategoryName, m => m.MapFrom(src => src.CategoryTranslations.FirstOrDefault().CategoryName))
                .ForMember(dto => dto.CategoryNameDictionary, m => m.MapFrom(src => src.CategoryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.CategoryName)));


            mapperConfiguration.CreateMap<ItemSize, SizeDto>()
                .ForMember(dest => dest.SizeName, m => m.MapFrom(src => src.Size.SizeTranslations.FirstOrDefault(x => x.Language.ToLower() == Thread.CurrentThread.CurrentCulture.Name.ToLower()).SizeName))
                .ForMember(dto => dto.SizeNameDictionary, m => m.MapFrom(src => src.Size.SizeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.SizeName)));
            mapperConfiguration.CreateMap<ItemSideItem, SideItemDTO>()
                .ForMember(dest => dest.SideItemName, m => m.MapFrom(src => src.SideItem.SideItemTranslations.FirstOrDefault(x => x.Language.ToLower() == Thread.CurrentThread.CurrentCulture.Name.ToLower()).SideItemName))
                .ForMember(dest => dest.Value, m => m.MapFrom(src => src.SideItem.Value))
                .ForAllMembers(opts => opts.Condition(src =>
                {
                    var sideItemTranslation = src.SideItem.SideItemTranslations
                        .FirstOrDefault(x => x.Language.ToLower() ==
                                             Thread.CurrentThread.CurrentCulture.Name.ToLower());
                    return sideItemTranslation != null && sideItemTranslation
                                   .SideItemName != null;
                }));
            mapperConfiguration.CreateMap<ItemDTO, Item>();
            mapperConfiguration.CreateMap<Item, ItemDTO>()
                .ForMember(dest => dest.ItemName, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemName))
                .ForMember(dest => dest.ItemDescription, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemDescription))
                .ForMember(dest => dest.MenuId, m => m.MapFrom(src => src.Category))
                .ForMember(dest => dest.RestaurantId, m => m.MapFrom(src => src.Category.RestaurantId))
                .ForMember(dest => dest.Sizes, m => m.MapFrom(src => src.ItemSizes.Where(x => !x.Size.IsDeleted)))
                .ForMember(dest => dest.SideItems, m => m.MapFrom(src => src.ItemSideItems.Where(x => !x.SideItem.IsDeleted)))
                .ForMember(dto => dto.ItemNameDictionary, m => m.MapFrom(src => src.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemName)))
                .ForMember(dto => dto.ItemDescriptionDictionary, m => m.MapFrom(src => src.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemDescription)));

            mapperConfiguration.CreateMap<SizeDto, Size>();
            mapperConfiguration.CreateMap<Size, SizeDto>()
                .ForMember(dest => dest.SizeName, m => m.MapFrom(src => src.SizeTranslations.FirstOrDefault().SizeName))
                .ForMember(dto => dto.SizeNameDictionary, m => m.MapFrom(src => src.SizeTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.SizeName)));

            mapperConfiguration.CreateMap<SideItemDTO, SideItem>();
            mapperConfiguration.CreateMap<SideItem, SideItemDTO>()
                .ForMember(dest => dest.SideItemName, m => m.MapFrom(src => src.SideItemTranslations.FirstOrDefault().SideItemName));

            mapperConfiguration.CreateMap<Item, ItemNamesDto>()
                .ForMember(dest => dest.ItemName, m => m.MapFrom(src => src.ItemTranslations.FirstOrDefault().ItemName));

            mapperConfiguration.CreateMap<RestaurantWaiterDto, RestaurantWaiter>();
            mapperConfiguration.CreateMap<RestaurantWaiter, RestaurantWaiterDto>()
                .ForMember(dto => dto.Password, m => m.MapFrom(src => PasswordHelper.Decrypt(src.Password)))
                .ForMember(dto => dto.BranchTitleDictionary, m => m.MapFrom(src => src.Branch.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)));
            //.ForMember(dto => dto.Start, m => m.MapFrom(src => src.Package.Start.Date.ToShortDateString()))
            //.ForMember(dto => dto.End, m => m.MapFrom(src => src.Package.End.Date.ToShortDateString()));

            mapperConfiguration.CreateMap<Background, BackgroundDto>();
            mapperConfiguration.CreateMap<BackgroundDto, Background>();
            mapperConfiguration.CreateMap<Category, CategoryDTO>()
                //  .ForMember(dest => dest.ItemCount, m => m.MapFrom(src => src.Items.Count(x => x.IsActive)))
                .ForMember(dto => dto.CategoryNameDictionary, m => m.MapFrom(src => src.CategoryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.CategoryName)));

            mapperConfiguration.CreateMap<AreaDto, Area>();
            //  .ForMember(dto => dto.Branches, m => m.Ignore());
            mapperConfiguration.CreateMap<Area, AreaDto>()
                .ForMember(dto => dto.TitleDictionary, m => m.MapFrom(src => src.AreaTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)));

            mapperConfiguration.CreateMap<BranchDto, Branch>();
            mapperConfiguration.CreateMap<Branch, BranchDto>()
                .ForMember(dto => dto.TitleDictionary,
                    m => m.MapFrom(src => src.BranchTranslations.ToDictionary(
                        translation => translation.Language.ToLower(), translation => translation.Title)));
            //.ForMember(dest => dest.BranchId, m => m.MapFrom(src => src.BranchTranslations.Where(x => !x.Branch.IsDeleted)));

            mapperConfiguration.CreateMap<TableDto, Table>();
            mapperConfiguration.CreateMap<Table, TableDto>();

            mapperConfiguration.CreateMap<CountryDto, Country>();
            mapperConfiguration.CreateMap<Country, CountryDto>()
                .ForMember(dto => dto.TitleDictionary, m => m.MapFrom(src => src.CountryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)));


            mapperConfiguration.CreateMap<RegionDto, Region>();
            mapperConfiguration.CreateMap<Region, RegionDto>()
                .ForMember(dto => dto.TitleDictionary, m => m.MapFrom(src => src.RegionTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)))
                .ForMember(dto => dto.CountryNameDictionary, m => m.MapFrom(src => src.Country.CountryTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)));

            mapperConfiguration.CreateMap<CityDto, City>();
            mapperConfiguration.CreateMap<City, CityDto>()
                .ForMember(dto => dto.TitleDictionary, m => m.MapFrom(src => src.CityTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)))
                .ForMember(dto => dto.RegionNameDictionary, m => m.MapFrom(src => src.Region.RegionTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.Title)));


            //mapperConfiguration.CreateMap<BranchDto, Branch>();
            //mapperConfiguration.CreateMap<Branch, BranchDto>()
            //    .ForMember(dest => dest.BranchTitle, m => m.MapFrom(src => src.BranchTranslations.FirstOrDefault().BranchTitle))
            //    .ForMember(dest => dest.BranchAddress, m => m.MapFrom(src => src.BranchTranslations.FirstOrDefault().BranchAddress))
            //    .ForMember(dto => dto.BranchTitleDictionary, m => m.MapFrom(src => src.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.BranchTitle)))
            //    .ForMember(dto => dto.BranchAddressDictionary, m => m.MapFrom(src => src.BranchTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.BranchAddress)));

            mapperConfiguration.CreateMap<FeedBackDto, FeedBack>();
            mapperConfiguration.CreateMap<FeedBack, FeedBackDto>();
            //.ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.Creater.UserName));

            mapperConfiguration.CreateMap<Request, RequestStatusDto>()
                //.ForMember(dto => dto.RoomName, m => m.MapFrom(src => src.Creater.TableName))
                //.ForMember(dto => dto.RoomId, m => m.MapFrom(src => src.Creater.TableId))
                .ForMember(dto => dto.CreateTime, m => m.MapFrom(src => src.CreateTime))
                .ForMember(dto => dto.ModifyTime, m => m.MapFrom(src => src.ModifyTime));

            Mapper.Initialize(mapperConfiguration);

        }

        public static void RegisterTypes(IUnityContainer container)
        {
            OrderNowDalConfig.RegisterTypes(container);
            container.RegisterType<IUserService, UserService>(new PerResolveLifetimeManager())
                .RegisterType<IRefreshTokenService, RefreshTokenService>(new PerResolveLifetimeManager())
                .RegisterType<ISupervisorService, SupervisorService>(new PerResolveLifetimeManager())
                .RegisterType<IClientService, ClientService>(new PerResolveLifetimeManager())
                .RegisterType<ISupervisorFeatureService, SupervisorFeatureService>(new PerResolveLifetimeManager())
                .RegisterType<IManageStorage, ManageStorage>(new PerResolveLifetimeManager())
                .RegisterType<IRequestService, RequestService>(new PerResolveLifetimeManager())
                .RegisterType<IRequestDetailService, RequestDetailService>(new PerResolveLifetimeManager())
                .RegisterType<IAdminService, AdminService>(new PerResolveLifetimeManager())
                .RegisterType<IPackageService, PackageService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantService, RestaurantService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantTranslationService, RestaurantTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryService, CategoryService>(new PerResolveLifetimeManager())
                .RegisterType<ICategoryTranslationService, CategoryTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IItemService, ItemService>(new PerResolveLifetimeManager())
                .RegisterType<IitemTranslationService, ItemTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantAdminService, RestaurantAdminService>(new PerResolveLifetimeManager())
                .RegisterType<ISizeService, SizeService>(new PerResolveLifetimeManager())
                .RegisterType<ISizeTranslationService, SizeTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemService, SideItemService>(new PerResolveLifetimeManager())
                .RegisterType<ISideItemTranslationService, SideItemTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IItemSideItemService, ItemSideItemService>(new PerResolveLifetimeManager())
                .RegisterType<IItemSizeService, ItemSizeService>(new PerResolveLifetimeManager())
                .RegisterType<IRestaurantWaiterService, RestaurantWaiterService>(new PerResolveLifetimeManager())
                .RegisterType<IBackgroundService, BackgroundService>(new PerResolveLifetimeManager())
                .RegisterType<ICountryService, CountryService>(new PerResolveLifetimeManager())
                .RegisterType<ICountryTranslationService, CountryTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IRegionService, RegionService>(new PerResolveLifetimeManager())
                .RegisterType<IRegionTranslationService, RegionTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ICityService, CityService>(new PerResolveLifetimeManager())
                .RegisterType<ICityTranslationService, CityTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IAreaService, AreaService>(new PerResolveLifetimeManager())
                .RegisterType<IAreaTranslationService, AreaTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<ITableService, TableService>(new PerResolveLifetimeManager())
                .RegisterType<IBranchService, BranchService>(new PerResolveLifetimeManager())
                .RegisterType<IBranchTranslationService, BranchTranslationService>(new PerResolveLifetimeManager())
                .RegisterType<IFeedBackService, FeedBackService>(new PerResolveLifetimeManager());
        }
    }
}
