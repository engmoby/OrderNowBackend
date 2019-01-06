using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ICountryTranslationService:IService<CountryTranslation>
    {
        bool CheckNameExist(string objName, string language, long recordId, long tenantId);
    }
}
