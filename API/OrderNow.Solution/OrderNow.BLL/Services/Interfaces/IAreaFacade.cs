using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IAreaFacade
    { 
        AreaDto GetArea(long userId, int tenantId); 
        AreaDto CreateArea(AreaDto userDto, int userId, int tenantId); 
        AreaDto EditArea(AreaDto userDto, int userId, int tenantId);
        PagedResultsDto GetAllAreas(long cityId,int page, int pageSize, int tenantId);
        AreaDto GetAllAreasByUserId(long userId);
        PagedResultsDto GetAllAreasForUser(long userId);
    }
}
