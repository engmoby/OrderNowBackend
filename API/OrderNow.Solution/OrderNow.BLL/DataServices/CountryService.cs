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
    public class CountryService:Service<Country>,ICountryService
    {
        public CountryService(IRepositoryAsync<Country> repository):base(repository)
        {
            
        }
        public PagedResultsDto GetAllCountries(int page, int pageSize, int restaurantId)
        {
            var query = Queryable().Where(x => !x.IsDeleted &&  x.RestaurantId == restaurantId).OrderBy(x => x.CountryId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            var modelReturn = pageSize > 0
                ? query.OrderBy(x => x.CountryId).Skip((page - 1) * pageSize).Take(pageSize).ToList()
                : query.Where(x => x.Regions.Count > 0 && x.Regions.Any(r=>r.Cities.Any(c => c.Areas.Count > 0 && c.Areas.Any(a => a.Branches.Count > 0)))).OrderBy(x => x.CountryId).ToList();
            results.Data = Mapper.Map<List<Country>, List<CountryDto>>(modelReturn);
            return results;
        }
    }
}
