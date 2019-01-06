using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IRestaurantService:IService<Restaurant>
    {
        PagedResultsDto GetAllRestaurant(string language, int page, int pageSize);
        Restaurant GetRestaurantByAdminId(long adminId);
        Restaurant GetRestaurantByWaiterId(long waiterId);
        //int GetAllResturantsLimits(long userId);
        List<Restaurant> GetRestaurantsName(long adminId);
    }
}
