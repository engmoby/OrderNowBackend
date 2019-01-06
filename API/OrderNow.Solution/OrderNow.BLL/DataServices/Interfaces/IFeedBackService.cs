using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IFeedBackService : IService<FeedBack>
    {
        PagedResultsDto GetAllFeedBack(long restaurantId, int page, int pageSize);
        int GetRestaurantRate(long restaurantId);
    }
}
