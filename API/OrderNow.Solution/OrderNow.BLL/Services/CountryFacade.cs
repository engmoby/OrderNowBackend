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
    public class CountryFacade:BaseFacade,ICountryFacade
    {
        private ICountryTranslationService _countryTranslationService;
        private ICountryService _countryService;
        public CountryFacade(IUnitOfWorkAsync unitOfWork, ICountryTranslationService countryTranslationService, ICountryService countryService) : base(unitOfWork)
        {
            _countryTranslationService = countryTranslationService;
            _countryService = countryService;
        }
        public CountryDto GetCountry(long countryId, int tenantId)
        {
            return Mapper.Map<CountryDto>(_countryService.Query(x => x.CountryId == countryId && x.TenantId == tenantId).Select().FirstOrDefault());
        }
        private void ValidateCountry(CountryDto countryDto, long tenantId)
        {
            foreach (var name in countryDto.TitleDictionary)
            {
                if (name.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.MenuNameExceedLength);

                if (_countryTranslationService.CheckNameExist(name.Value, name.Key, countryDto.CountryId, tenantId))
                    throw new ValidationException(ErrorCodes.NameIsExist);
            }
        }
        public CountryDto CreateCountry (CountryDto countryDto, int userId, int tenantId)
        {
            if (GetCountry(countryDto.CountryId, tenantId) != null)
            {
                return EditCountry(countryDto, userId, tenantId);
            }
            ValidateCountry(countryDto, tenantId);
            var country = Mapper.Map<Country>(countryDto);
            foreach (var name in countryDto.TitleDictionary)
            {
                country.CountryTranslations.Add(new CountryTranslation
                {
                    Title = name.Value,
                    Language = name.Key
                });
            }

            country.CreationTime = Strings.CurrentDateTime;
            country.CreatorUserId = userId;
            country.TenantId = (int) countryDto.RestaurantId;
            country.RestaurantId = countryDto.RestaurantId;
            _countryTranslationService.InsertRange(country.CountryTranslations);
            _countryService.Insert(country);
            SaveChanges();
            return countryDto;
        }

        public CountryDto EditCountry(CountryDto countryDto, int userId, int tenantId)
        {
            var country = _countryService.Query(x => x.CountryId == countryDto.CountryId && x.TenantId == tenantId).Select().FirstOrDefault();
            if (country == null) throw new NotFoundException(ErrorCodes.CountryNotFound);
            ValidateCountry(countryDto, tenantId);
            foreach (var areaName in countryDto.TitleDictionary)
            {
                var countryTranslation = country.CountryTranslations.FirstOrDefault(x => x.Language.ToLower() == areaName.Key.ToLower()
                                                                                   && x.CountryId == countryDto.CountryId);
                if (countryTranslation == null)
                {
                    country.CountryTranslations.Add(new CountryTranslation
                    {
                        Title = areaName.Value,
                        Language = areaName.Key
                    });
                }
                else
                    countryTranslation.Title = areaName.Value;
            }

            country.LastModificationTime = Strings.CurrentDateTime;
            country.LastModifierUserId = userId;
            country.IsDeleted = countryDto.IsDeleted;
          //  country.RestaurantId = countryDto.RestaurantId;
            _countryService.Update(country);
            SaveChanges();
            return countryDto;

        }

        public PagedResultsDto GetAllCountries(int page, int pageSize, int tenantId)
        {
            return _countryService.GetAllCountries(page, pageSize, tenantId);
        }
    }
}
