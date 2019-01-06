using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class CategoryTranslationService : Service<CategoryTranslation>, ICategoryTranslationService
    {
        private ITableService _tableService;
        public CategoryTranslationService(IRepositoryAsync<CategoryTranslation> repository, ITableService tableService) : base(repository)
        {
            _tableService = tableService;
        }

        public bool CheckCategoryNameExist(string categoryName, string language, long categoryId, long restaurantId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() &&
                          x.CategoryName.ToLower() == categoryName.ToLower() && x.Category.RestaurantId == restaurantId &&
                          x.CategoryId != categoryId && !x.Category.IsDeleted);
        }

        public PagedResultsDto GetAllCategories(string language, long resturantId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Language.ToLower() == language.ToLower()).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Language.ToLower() == language.ToLower()).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Language.ToLower() == language.ToLower()).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            //results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            foreach (Category category in src)
            //            {
            //                category.CategoryTranslations = category.CategoryTranslations
            //                    .Where(x => x.Language.ToLower() == language.ToLower()).ToList();
            //            }

            //        }
            //    );
            //});
            return results;
        }

        public PagedResultsDto GetAllCategoriesByTable(long tableId)
        {
            var getResturentId = _tableService.Find(tableId).Branch.Area.City.Region.Country.RestaurantId;
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == getResturentId).Select(x => x.Category).Count(x => !x.IsDeleted);

            var categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == getResturentId).Select(x => x.Category).OrderBy(x => x.CategoryId).ToList();

            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);
            return results;
        }

        public PagedResultsDto GetActivatedCategories(string language, long resturantId, int page, int pageSize)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Category.IsActive && x.Language.ToLower() == language.ToLower()).Select(x => x.Category).Count(x => !x.IsDeleted);
            List<Category> categories;
            if (pageSize > 0)
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Category.IsActive && x.Language.ToLower() == language.ToLower()).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                categories = _repository.Query(x => !x.Category.IsDeleted && x.Category.RestaurantId == resturantId && x.Category.IsActive && x.Language.ToLower() == language.ToLower()).Select(x => x.Category)
                    .OrderBy(x => x.CategoryId).ToList();
            results.Data = Mapper.Map<List<Category>, List<CategoryDTO>>(categories);

            return results;
        }


        public bool CheckCategoryByLanguage(long categoryId, string language)
        {
            return _repository.Query(x => x.CategoryId == categoryId && x.Language.ToLower() == language.ToLower() && !x.Category.IsDeleted).Select()
                .Any();
        }
    }
}
