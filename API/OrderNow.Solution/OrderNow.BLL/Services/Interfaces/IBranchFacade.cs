using OrderNow.BLL.DTOs;

namespace OrderNow.BLL.Services.Interfaces
{
    public interface IBranchFacade
    {
        BranchDto GetBranch(long branchId, int tenantId);
        BranchDto CreateBranch(BranchDto userDto, int userId, int tenantId);
        BranchDto EditBranch(BranchDto userDto, int userId, int tenantId);
        PagedResultsDto GetAllBranchs(long areaId, int page, int pageSize, int tenantId);
    }
}
