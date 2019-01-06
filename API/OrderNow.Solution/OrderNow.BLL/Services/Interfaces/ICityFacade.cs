using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface ICityFacade
    {
        CityDto GetCity(long cityId, int tenantId);
        CityDto CreateCity(CityDto cityDto, int userId, int tenantId);
        CityDto EditCity(CityDto cityDto, int userId, int tenantId);
        PagedResultsDto GetAllCities(long regionId, int page, int pageSize, int tenantId);
        PagedResultsDto GetAllCitiesForUser(long userId);
    }
}
