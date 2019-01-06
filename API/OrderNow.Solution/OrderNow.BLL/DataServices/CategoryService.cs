using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class CategoryService : Service<Category>, ICategoryService
    {
        private ITableService _tableService;
        public CategoryService(IRepositoryAsync<Category> repository, ITableService tableService) : base(repository)
        {
            _tableService = tableService;
        }


        public PagedResultsDto GetAllCategories(string language, int page, int pageSize)
        {
            var query = Queryable();
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(query.OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList(), opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Category category in src)
                        {
                            category.CategoryTranslations = category.CategoryTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
        public PagedResultsDto GetAllCategoriesByTable(long tableId)
        {
            var getResturentId = _tableService.Find(tableId).Branch.Area.City.Region.Country.RestaurantId;
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.IsDeleted && x.RestaurantId == getResturentId && x.IsActive).Select(x => x).Count(x => !x.IsDeleted);

            var categories = _repository.Query(x => !x.IsDeleted && x.RestaurantId == getResturentId  && x.IsActive ).Select(x => x).OrderBy(x => x.CategoryId).ToList();

            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return results;
        }
    }
}
