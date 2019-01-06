using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using OrderNow.Common.CustomException;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class CityFacade:BaseFacade,ICityFacade
    {

        private ICityTranslationService _cityTranslationService;
        private ICityService _cityService;
        private IUserService _userService;
        public CityFacade(IUnitOfWorkAsync unitOfWork, ICityTranslationService cityTranslationService, ICityService cityService, IUserService userService) : base(unitOfWork)
        {
            _cityTranslationService = cityTranslationService;
            _cityService = cityService;
            _userService = userService;
        }
        public CityDto GetCity(long cityId, int tenantId)
        {
            return Mapper.Map<CityDto>(_cityService.Query(x => x.CityId == cityId).Select().FirstOrDefault());
        }
        private void ValidateCity(CityDto cityDto, long tenantId, long regionId)
        {
            foreach (var name in cityDto.TitleDictionary)
            {
                if (name.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.MenuNameExceedLength);

                if (_cityTranslationService.CheckNameExist(name.Value, name.Key, cityDto.CityId, tenantId,regionId))
                    throw new ValidationException(ErrorCodes.NameIsExist);
            }
        }
        public CityDto CreateCity(CityDto cityDto, int userId, int tenantId)
        {
            if (GetCity(cityDto.CityId, tenantId) != null)
            {
                return EditCity(cityDto, userId, tenantId);
            }
            ValidateCity(cityDto, tenantId,cityDto.RegionId);
            var city = Mapper.Map<City>(cityDto);
            foreach (var name in cityDto.TitleDictionary)
            {
                city.CityTranslations.Add(new CityTranslation
                {
                    Title = name.Value,
                    Language = name.Key
                });
            }

            city.CreationTime = Strings.CurrentDateTime;
            city.CreatorUserId = userId;
            _cityTranslationService.InsertRange(city.CityTranslations);
            _cityService.Insert(city);
            SaveChanges();
            return cityDto;
        }

        public CityDto EditCity(CityDto cityDto, int userId, int tenantId)
        {
            var city = _cityService.Query(x => x.CityId == cityDto.CityId).Select().FirstOrDefault();
            if (city == null) throw new NotFoundException(ErrorCodes.CountryNotFound);
            ValidateCity(cityDto, tenantId, cityDto.RegionId);
            foreach (var name in cityDto.TitleDictionary)
            {
                var cityTranslation = city.CityTranslations.FirstOrDefault(x => x.Language.ToLower() == name.Key.ToLower()
                                                                                      && x.CityId == cityDto.CityId);
                if (cityTranslation == null)
                {
                    city.CityTranslations.Add(new CityTranslation
                    {
                        Title = name.Value,
                        Language = name.Key
                    });
                }
                else
                    cityTranslation.Title = name.Value;
            }

            city.LastModificationTime = Strings.CurrentDateTime;
            city.LastModifierUserId = userId;
            city.IsDeleted = cityDto.IsDeleted;
            _cityService.Update(city);
            SaveChanges();
            return cityDto;

        }
           
        public PagedResultsDto GetAllCities(long regionId, int page, int pageSize, int tenantId)
        {
            return _cityService.GetAllCities(regionId, page, pageSize, tenantId);
        }
        public PagedResultsDto GetAllCitiesForUser(long userId)
        {
            var user = _userService.Find(userId);
            return null;// user.AreaId.HasValue ? _cityService.GetAllCities(user.Area.City.RegionId, 1, 0, user.TenantId) : new PagedResultsDto();
        }
    }
}
