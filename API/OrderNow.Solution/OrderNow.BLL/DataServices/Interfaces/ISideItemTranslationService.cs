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
    public interface ISideItemTranslationService:IService<SideItemTranslation>
    {
        PagedResultsDto GetAllSideItems(string language, long userId, int page, int pageSize);
        bool CheckSideItemNameExist(string sideItemName, string language, long sizeId, long restaurantAdminId);
        bool CheckSideItemNameTranslated(string language, long sideItemId);
    }
}
