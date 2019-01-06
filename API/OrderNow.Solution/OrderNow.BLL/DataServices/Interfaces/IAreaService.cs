using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IAreaService : IService<Area>
    {
         PagedResultsDto GetAllAreas(long cityId,int page, int pageSize, int tenantId);
    }
}
