using System;
using AutoMapper.Configuration;
using OrderNow.API.Models;
using OrderNow.BLL;
using OrderNow.BLL.DTOs;
using OrderNow.Common;

namespace OrderNow.API
{
    public class AutoMapperConfig
    {
        public static void RegisterMappings()
        {

            var mapperConfiguration = new MapperConfigurationExpression();

            

            mapperConfiguration.CreateMap<RequestModel, RequestDto>()
                .ForMember(dto => dto.Status, m => m.MapFrom(src => Enum.Parse(typeof(Enums.RoleType),src.Status)))
                .ForMember(dto => dto.Type, m => m.MapFrom(src => Enum.Parse(typeof(Enums.RoleType), src.Type)));
            mapperConfiguration.CreateMap<RequestDto, RequestModel>()
                .ForMember(dto => dto.Status, m => m.MapFrom(src => src.Status.ToString()))
                .ForMember(dto => dto.Type, m => m.MapFrom(src => src.Type.ToString()));


             
            mapperConfiguration.CreateMap<RestaurantModel, RestaurantDTO>();
            mapperConfiguration.CreateMap<RestaurantDTO, RestaurantModel>();
              
            mapperConfiguration.CreateMap<CategoryModel, CategoryDTO>();
            mapperConfiguration.CreateMap<CategoryDTO, CategoryModel>();

            mapperConfiguration.CreateMap<SizeModel, SizeDto>();
            mapperConfiguration.CreateMap<SizeDto, SizeModel>();

            mapperConfiguration.CreateMap<SideItemModel, SideItemDTO>();
            mapperConfiguration.CreateMap<SideItemDTO, SideItemModel>();


            mapperConfiguration.CreateMap<ItemModel, ItemDTO>();
            mapperConfiguration.CreateMap<ItemDTO, ItemModel>();

            mapperConfiguration.CreateMap<ItemSizeDto, ItemSizeModel>();
            mapperConfiguration.CreateMap<ItemSizeModel, ItemSizeDto>();


            mapperConfiguration.CreateMap<ItemNamesDto, ItemNameModel>();

            mapperConfiguration.CreateMap<RestaurantWaiterModel, RestaurantWaiterDto>();
            mapperConfiguration.CreateMap<RestaurantWaiterDto, RestaurantWaiterModel>();


            mapperConfiguration.CreateMap<BackgroundModel, BackgroundDto>();
            mapperConfiguration.CreateMap<BackgroundDto, BackgroundModel>();

            mapperConfiguration.CreateMap<ResturantInfoModel, ResturantInfoDto>();
            mapperConfiguration.CreateMap<ResturantInfoDto, ResturantInfoModel>();

             

            mapperConfiguration.CreateMap<BranchModel, BranchDto>();
            mapperConfiguration.CreateMap<BranchDto, BranchModel>();

            mapperConfiguration.CreateMap<FeedBackModel, FeedBackDto>();
            mapperConfiguration.CreateMap<FeedBackDto, FeedBackModel>();  
             

            mapperConfiguration.CreateMap<RequestStatusDto, RequestStatusModel>()
               .ForMember(dto => dto.Status, m => m.MapFrom(src => src.Status.ToString()));

            OrderNowBllConfig.RegisterMappings(mapperConfiguration);

            //Mapper.Initialize(m =>
            //{
            //    m.CreateMap<RestaurantTypeModel, RestaurantTypeDto>();
            //    m.CreateProfile("ff",expression => {});
            //    //m.AddProfile(ECatalogBLLConfig);
            //});

        }

    }
}