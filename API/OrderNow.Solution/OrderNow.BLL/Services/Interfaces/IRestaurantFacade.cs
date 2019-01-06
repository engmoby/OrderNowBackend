using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IRestaurantFacade
    { 
        void AddRestaurant(RestaurantDTO restaurantDto,string path, long userId);
        RestaurantDTO GetRestaurant(long restaurantId, string language);
        PagedResultsDto GetAllRestaurant(string language,int page,int pageSize, long userId);
        void ActivateRestaurant(long restaurantId);
        void DeActivateRestaurant(long restaurantId);
        void DeleteRestaurant(long restaurantId);
        void UpdateRestaurant(RestaurantDTO restaurantDto, string path, long userId); 
        RestaurantDTO CheckRestaurantReady(long restaurantAdminId);
        void PublishRestaurant(long restaurantAdminId);
        ResturantInfoDto GetGlobalRestaurantInfo(long userId, string role, string language); 
    }
}
