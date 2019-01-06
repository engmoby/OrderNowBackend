using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface ITableFacade
    {
        TableDto GetTable(long tableId);
        TableDto GetTable(long TableId, int tenantId);
        TableDto CreateTable(TableDto userDto, int userId, int tenantId);
        TableDto EditTable(TableDto userDto, int userId, int tenantId);
        PagedResultsDto GetAllTables(int page, int pageSize, int tenantId);
    }
}
