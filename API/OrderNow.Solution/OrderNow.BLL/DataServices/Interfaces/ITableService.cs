using System.Collections.Generic;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ITableService : IService<Table>
    {
         PagedResultsDto GetAllTables(int page, int pageSize, int tenantId);
        List<TableDto> GetAllTables(long branchId);
    }
}
