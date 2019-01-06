using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.BLL.Services.ManageStorage;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using OrderNow.Common.CustomException;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class CategoryFacade : BaseFacade, ICategoryFacade
    {
        private ICategoryService _categoryService;
        private ICategoryTranslationService _categoryTranslationService;
        private IManageStorage _manageStorage;
        private IRestaurantService _restaurantService;

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IManageStorage manageStorage
            , IRestaurantService restaurantService, IUnitOfWorkAsync unitOfWork) : base(unitOfWork)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _manageStorage = manageStorage;
            _restaurantService = restaurantService;
        }

        public CategoryFacade(ICategoryService categoryService, ICategoryTranslationService categoryTranslationService, IManageStorage manageStorage
        , IRestaurantService restaurantService)
        {
            _categoryService = categoryService;
            _categoryTranslationService = categoryTranslationService;
            _manageStorage = manageStorage;
            _restaurantService = restaurantService;
        }

        public void AddCategory(CategoryDTO categoryDto, string path)
        {
            ValidateCategory(categoryDto);

            var category = Mapper.Map<Category>(categoryDto);
            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                category.CategoryTranslations.Add(new CategoryTranslation
                {
                    CategoryName = categoryName.Value,
                    Language = categoryName.Key
                });
            }
            category.RestaurantId = categoryDto.RestaurantId;
            _categoryTranslationService.InsertRange(category.CategoryTranslations);
            _categoryService.Insert(category);
            SaveChanges();
            _manageStorage.UploadImage(path + "\\" + "Restaurant-" + category.RestaurantId + "\\" + "Category-" + category.CategoryId, categoryDto.Image, category.CategoryId.ToString());
        }

        public CategoryDTO GetCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            if (category.IsDeleted) throw new NotFoundException(ErrorCodes.CategoryDeleted);
            //return Mapper.Map<Category, CategoryDTO>(category, opt =>
            //{
            //    opt.BeforeMap((src, dest) =>
            //        {
            //            src.CategoryTranslations = src.CategoryTranslations
            //                .Where(x => x.Language.ToLower() == language.ToLower())
            //                .ToList();
            //        }
            //    );
            //});
            return Mapper.Map<CategoryDTO>(category);
        }
        private void ValidateCategory(CategoryDTO categoryDto)
        {
            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                if (string.IsNullOrEmpty(categoryName.Value))
                    throw new ValidationException(ErrorCodes.EmptyCategoryName);
                if (categoryName.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.CategoryNameExceedLength);
                if (_categoryTranslationService.CheckCategoryNameExist(categoryName.Value, categoryName.Key, categoryDto.CategoryId, categoryDto.RestaurantId))
                    throw new ValidationException(ErrorCodes.CategoryNameAlreadyExist);
            }
        }


        public void ActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.MenuNotFound);
            if (category.Items.Count(m => m.IsActive) <= 0)
                throw new ValidationException(ErrorCodes.CategoryItemsDoesNotActivated);
            if (Strings.SupportedLanguages.Any(x => !category.CategoryTranslations.Select(m => m.Language.ToLower())
                .Contains(x.ToLower())))
                throw new ValidationException(ErrorCodes.CategoryIsNotTranslated);
            //if(!_categoryService.CategoryHasValidTemplates(categoryId))
            //    throw new ValidationException(ErrorCodes.CategoryTemplatesInvalid);
            category.IsActive = true;
            _categoryService.Update(category);
            SaveChanges();
        }

        public void DeActivateCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsActive = false;
            _categoryService.Update(category);

            SaveChanges();
        }
        public void DeleteCategory(long categoryId)
        {
            var category = _categoryService.Find(categoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            category.IsDeleted = true;
            category.IsActive = false;
            _categoryService.Update(category);

            SaveChanges();
        }
        private void CheckRestaurantHasMenuActivated(Category category)
        {

        }

        public void UpdateCategory(CategoryDTO categoryDto, string path)
        {
            var category = _categoryService.Find(categoryDto.CategoryId);
            if (category == null) throw new NotFoundException(ErrorCodes.CategoryNotFound);
            categoryDto.RestaurantId = category.RestaurantId;
            ValidateCategory(categoryDto);

            foreach (var categoryName in categoryDto.CategoryNameDictionary)
            {
                var categoryTranslation =
                    category.CategoryTranslations.FirstOrDefault(x => x.Language.ToLower() == categoryName.Key.ToLower());
                if (categoryTranslation == null)
                {
                    category.CategoryTranslations.Add(new CategoryTranslation
                    {
                        Language = categoryName.Key.ToLower(),
                        CategoryName = categoryName.Value
                    });
                }
                else
                {
                    categoryTranslation.CategoryName = categoryName.Value;
                }
            }
          //  category.RestaurantId = categoryDto.RestaurantId;
            _categoryService.Update(category);
            SaveChanges();
            if (categoryDto.IsImageChange)
                _manageStorage.UploadImage(path + "\\" + "Restaurant-" + category.RestaurantId + "\\" + "Category-" + category.CategoryId
                    , categoryDto.Image, categoryDto.CategoryId.ToString());
        }
    }
}
