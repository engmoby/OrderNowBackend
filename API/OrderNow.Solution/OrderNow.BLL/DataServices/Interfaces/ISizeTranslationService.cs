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
    public  interface ISizeTranslationService:IService<SizeTranslation>
    {
        PagedResultsDto GetAllSizes(string language,long userId, int page, int pageSize);
        bool CheckSizeNameExist(string sizeName, string language, long sizeId,long restaurantAdminId);
        bool CheckSizeNameTranslated(string language, long sizeId);
    }
}
