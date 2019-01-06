using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class CountryTranslationService:Service<CountryTranslation>,ICountryTranslationService
    {
        public CountryTranslationService(IRepositoryAsync<CountryTranslation> repository):base(repository)
        {
            
        }
        public bool CheckNameExist(string objName, string language, long recordId, long tenantId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() && x.Title.ToLower() == objName.ToLower() &&
                          x.CountryId != recordId && x.Country.TenantId == tenantId && !x.Country.IsDeleted);
        }
    }
}
