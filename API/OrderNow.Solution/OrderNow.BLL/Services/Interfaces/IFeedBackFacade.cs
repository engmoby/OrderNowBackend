using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IFeedBackFacade
    {
        void AddFeedBack(FeedBackDto feedBackDto, long userId);
        PagedResultsDto GetAllFeedBack(long userId,long restaurantId, int page, int pageSize, string userRole);
    }
}
