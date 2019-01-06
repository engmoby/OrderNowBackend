using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ICategoryTranslationService:IService<CategoryTranslation>
    {
        bool CheckCategoryNameExist(string categoryName, string language, long categoryId, long restaurantId);
        PagedResultsDto GetAllCategoriesByTable(long tableId);
        PagedResultsDto GetAllCategories(string language, long resturantId, int page, int pageSize);
        bool CheckCategoryByLanguage(long categoryId, string language);
        PagedResultsDto GetActivatedCategories(string language , long resturantId, int page, int pageSize); 
    }
}
