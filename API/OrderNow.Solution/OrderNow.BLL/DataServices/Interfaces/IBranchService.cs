using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IBranchService : IService<Branch>
    {
         PagedResultsDto GetAllBranchs(long areaId, int page, int pageSize, int tenantId);
    }
}
