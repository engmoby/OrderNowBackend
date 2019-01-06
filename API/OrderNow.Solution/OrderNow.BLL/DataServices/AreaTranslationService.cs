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
    public class AreaTranslationService : Service<AreaTranslation>, IAreaTranslationService
    {
        public AreaTranslationService(IRepositoryAsync<AreaTranslation> repository) : base(repository)
        {
            _repository = repository;
        }
        public PagedResultsDto GetAllAreas()
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Area.IsDeleted ).Select(x => x.Area).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Area.IsDeleted).Select().ToList();
            var Areas = _repository.Query(x => !x.Area.IsDeleted ).Select(x => x.Area)
                .OrderBy(x => x.AreaId).ToList();
            results.Data = Mapper.Map<List<Area>, List<AreaDto>>(Areas);
            return results;
        }
        public PagedResultsDto GetAllAreasTranslation(string language)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Area).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Areas = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Area)
                .OrderBy(x => x.AreaId).ToList();
            results.Data = Mapper.Map<List<Area>, List<AreaDto>>(Areas, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Area Area in src)
                        {
                            Area.AreaTranslations = Area.AreaTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
        public PagedResultsDto GetAreaTranslationByAreaId(string language,long AreaId)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()  && x.AreaId == AreaId).Select(x => x.Area).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Areas = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower() && x.AreaId == AreaId).Select(x => x.Area)
                .OrderBy(x => x.AreaId).ToList();
            results.Data = Mapper.Map<List<Area>, List<AreaDto>>(Areas, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Area Area in src)
                        {
                            Area.AreaTranslations = Area.AreaTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
        public AreaDto AreaTranslationByAreaId(string language, long AreaId)
        {
            var aaax = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Areas = _repository.Query(x => !x.Area.IsDeleted && x.Language.ToLower() == language.ToLower() && x.AreaId == AreaId).Select(x => x.Area)
                .OrderBy(x => x.AreaId).FirstOrDefault();
            var results = Mapper.Map<Area, AreaDto>(Areas, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {

                        src.AreaTranslations = src.AreaTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();


                    }
                );
            });
            return results;
        }
        public bool CheckNameExist(string objName, string language, long recordId, long tenantId, long cityId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() && x.Title.ToLower() == objName.ToLower() &&
                          x.AreaId != recordId && x.Area.TenantId == tenantId && !x.Area.IsDeleted && x.Area.CityId == cityId);
        }

    }
}