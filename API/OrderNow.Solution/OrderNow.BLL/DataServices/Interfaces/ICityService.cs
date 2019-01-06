using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ICityService:IService<City>
    {
        PagedResultsDto GetAllCities(long regionId, int page, int pageSize, int tenantId);
    }
}
