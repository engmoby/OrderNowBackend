using System.Collections.Generic;
using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IDashboardFacade
    {
        List<DashboardDto> GetDashboardDto(long tenantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId,   long branchId, string from, string to,  string status);
        List<SellerItemDto> GetItemCount(long tenantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId, long branchId, string from, string to, string status);
    }
}
