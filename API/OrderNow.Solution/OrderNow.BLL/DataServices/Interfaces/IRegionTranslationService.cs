using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IRegionTranslationService:IService<RegionTranslation>
    {
        bool CheckNameExist(string objName, string language, long recordId, long tenantId, long countryId);
    }
}
