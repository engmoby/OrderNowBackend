using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class CityTranslationService:Service<CityTranslation>,ICityTranslationService
    {
        public CityTranslationService(IRepositoryAsync<CityTranslation> repository):base(repository)
        {
            
        }
        public bool CheckNameExist(string objName, string language, long recordId, long tenantId, long regionId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() && x.Title.ToLower() == objName.ToLower() &&
                          x.CityId != recordId && x.City.Region.Country.TenantId == tenantId && !x.City.IsDeleted && x.City.RegionId == regionId);
        }
    }
}
