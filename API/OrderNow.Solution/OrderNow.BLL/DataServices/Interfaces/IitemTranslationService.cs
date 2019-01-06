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
    public interface IitemTranslationService:IService<ItemTranslation>
    {
        bool CheckItemNameExistForCategory(string itemName, string language, long itemId, long categoryId, long restaurantId);
        PagedResultsDto GetAllItemsByCategoryId(string language, long categoryId, int page, int pageSize);
        List<ItemNamesDto> GetAllItemNamesByCategoryId(string language, long categoryId);
        PagedResultsDto GetActivatedItemsByCategoryId(string language, long categoryId, int page, int pageSize);
    }
}
