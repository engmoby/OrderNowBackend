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
    public class AreaFacade : BaseFacade, IAreaFacade
    {
        private readonly IAreaService _areaService;
        private readonly IAreaTranslationService _typeTranslationService;
        private readonly IUserService _userService;


        public AreaFacade(IAreaService areaService, IUnitOfWorkAsync unitOfWork,
            IAreaTranslationService typeTranslationService, IUserService userService) : base(unitOfWork)
        {
            _areaService = areaService;
            _typeTranslationService = typeTranslationService;
            _userService = userService;
        }

        public AreaFacade(IAreaService areaService, IAreaTranslationService typeTranslationService,
            IUserService userService)
        {
            _areaService = areaService;
            _typeTranslationService = typeTranslationService;
            _userService = userService;
        }

        public AreaDto GetArea(long areaId, int tenantId)
        {
            return Mapper.Map<AreaDto>(_areaService.Query(x => x.AreaId == areaId && x.TenantId == tenantId).Select()
                .FirstOrDefault());
        }

        public AreaDto CreateArea(AreaDto areaDto, int userId, int tenantId)
        {
            if (GetArea(areaDto.AreaId, tenantId) != null)
            {
                return EditArea(areaDto, userId, tenantId);
            }
            ValidateArea(areaDto, tenantId, areaDto.CityId);
            var areaObj = Mapper.Map<Area>(areaDto);
            foreach (var areaName in areaDto.TitleDictionary)
            {
                areaObj.AreaTranslations.Add(new AreaTranslation
                {
                    Title = areaName.Value,
                    Language = areaName.Key,
                    TenantId = tenantId
                });
            }

            areaObj.CreationTime = Strings.CurrentDateTime;
            areaObj.CreatorUserId = userId;
            areaObj.TenantId = tenantId;
            _typeTranslationService.InsertRange(areaObj.AreaTranslations);
            _areaService.Insert(areaObj);
            SaveChanges();
            return areaDto;
        }

        public AreaDto EditArea(AreaDto areaDto, int userId, int tenantId)
        {
            var areaObj = _areaService.Query(x => x.AreaId == areaDto.AreaId && x.TenantId == tenantId).Select()
                .FirstOrDefault();
            if (areaObj == null) throw new NotFoundException(ErrorCodes.CountryNotFound);
            ValidateArea(areaDto, tenantId,areaDto.CityId);
            foreach (var areaName in areaDto.TitleDictionary)
            {
                var areaTranslation = areaObj.AreaTranslations.FirstOrDefault(
                    x => x.Language.ToLower() == areaName.Key.ToLower()
                         && x.AreaId == areaDto.AreaId);
                if (areaTranslation == null)
                {
                    areaObj.AreaTranslations.Add(new AreaTranslation
                    {
                        Title = areaName.Value,
                        Language = areaName.Key
                    });
                }
                else
                    areaTranslation.Title = areaName.Value;
            }

            areaObj.LastModificationTime = Strings.CurrentDateTime;
            areaObj.LastModifierUserId = userId;
            areaObj.IsDeleted = areaDto.IsDeleted;
            areaObj.IsStatic = areaDto.IsStatic;
            _areaService.Update(areaObj);
            SaveChanges();
            return areaDto;

        }

        public PagedResultsDto GetAllAreas(long cityId, int page, int pageSize, int tenantId)
        {
            return _areaService.GetAllAreas(cityId, page, pageSize, tenantId);
        }

        public AreaDto GetAllAreasByUserId(long userId)
        {
            return Mapper.Map<AreaDto>(_userService.Find(userId));
        }

        private void ValidateArea(AreaDto areaDto, long tenantId, long cityId)
        {
            foreach (var name in areaDto.TitleDictionary)
            {
                if (name.Value.Length > 300)
                    throw new ValidationException(ErrorCodes.MenuNameExceedLength);

                if (_typeTranslationService.CheckNameExist(name.Value, name.Key, areaDto.AreaId, tenantId,cityId))
                    throw new ValidationException(ErrorCodes.NameIsExist);
            }
        }

        public PagedResultsDto GetAllAreasForUser(long userId)
        {
            var user = _userService.Find(userId);
            return null;//user.AreaId.HasValue? _areaService.GetAllAreas(user.Area.CityId.Value, 1, 0, user.TenantId): new PagedResultsDto();

        }
    }
}