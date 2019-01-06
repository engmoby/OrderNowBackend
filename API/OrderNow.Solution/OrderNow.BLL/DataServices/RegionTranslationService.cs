using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class RegionTranslationService:Service<RegionTranslation>,IRegionTranslationService
    {
        public RegionTranslationService(IRepositoryAsync<RegionTranslation> repository):base(repository)
        {

        }
        public bool CheckNameExist(string objName, string language, long recordId, long tenantId, long countryId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() && x.Title.ToLower() == objName.ToLower() &&
                          x.RegionId != recordId && x.Region.Country.TenantId == tenantId && !x.Region.IsDeleted && x.Region.CountryId == countryId);
        }
    }
}
