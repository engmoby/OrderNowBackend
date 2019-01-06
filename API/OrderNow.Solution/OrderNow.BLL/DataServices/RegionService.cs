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
    public class RegionService:Service<Region>,IRegionService
    {
        public RegionService(IRepositoryAsync<Region> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllRegions(long countryId,int page, int pageSize, int tenantId)
        {
            var query = Queryable().Where(x => !x.IsDeleted && (x.CountryId == countryId)).OrderBy(x => x.RegionId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            var modelReturn = pageSize > 0
                ? query.OrderBy(x => x.CountryId).Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : query.Where(x => x.Cities.Count > 0 && x.Cities.Any(c=>c.Areas.Count >0 && c.Areas.Any(a=>a.Branches.Count>0))).OrderBy(x => x.CountryId).ToList();
            results.Data = Mapper.Map<List<Region>, List<RegionDto>>(modelReturn);
            return results;
        }
    }
}
