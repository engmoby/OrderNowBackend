using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class CityService:Service<City>,ICityService
    {
        public CityService(IRepositoryAsync<City> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllCities(long regionId, int page, int pageSize, int tenantId)
        {
            var query = Queryable().Where(x => !x.IsDeleted && (x.RegionId == regionId)).OrderBy(x => x.CityId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            var modelReturn = pageSize > 0
                ? query.OrderBy(x => x.CityId).Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : query.Where(x => x.Areas.Count > 0 &&  x.Areas.Any(a => a.Branches.Count > 0)).OrderBy(x => x.CityId).ToList();
            results.Data = Mapper.Map<List<City>, List<CityDto>>(modelReturn);
            return results;
        }
    }
}
