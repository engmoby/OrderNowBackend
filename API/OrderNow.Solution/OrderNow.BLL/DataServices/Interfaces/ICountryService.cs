using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ICountryService:IService<Country>
    {
        PagedResultsDto GetAllCountries(int page, int pageSize, int restaurantId);
    }
}
