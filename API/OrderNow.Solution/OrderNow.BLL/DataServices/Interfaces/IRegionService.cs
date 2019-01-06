using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IRegionService:IService<Region>
    {
        PagedResultsDto GetAllRegions(long countryId,int page, int pageSize, int tenantId);
    }
}
