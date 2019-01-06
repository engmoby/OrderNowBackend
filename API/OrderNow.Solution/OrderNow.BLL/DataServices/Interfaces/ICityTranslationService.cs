using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ICityTranslationService:IService<CityTranslation>
    {
        bool CheckNameExist(string objName, string language, long recordId, long tenantId, long regionId);
    }
}
