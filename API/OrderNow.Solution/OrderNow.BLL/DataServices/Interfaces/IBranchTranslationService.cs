using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IBranchTranslationService : IService<BranchTranslation>
    {
        PagedResultsDto GetAllBranchs();
        PagedResultsDto GetAllBranchsTranslation(string language);
        PagedResultsDto GetBranchTranslationByBranchId(string language, long branchId);
        BranchDto BranchTranslationByBranchId(string language, long branchId);
        bool CheckNameExist(string objName, string language, long recordId, long tenantId, long areaId);
    }
}
