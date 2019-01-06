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
    public class BranchTranslationService : Service<BranchTranslation>, IBranchTranslationService
    {
        public BranchTranslationService(IRepositoryAsync<BranchTranslation> repository) : base(repository)
        {
            _repository = repository;
        }
        public PagedResultsDto GetAllBranchs()
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Branch.IsDeleted ).Select(x => x.Branch).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Branch.IsDeleted).Select().ToList();
            var Branchs = _repository.Query(x => !x.Branch.IsDeleted ).Select(x => x.Branch)
                .OrderBy(x => x.BranchId).ToList();
            results.Data = Mapper.Map<List<Branch>, List<BranchDto>>(Branchs);
            return results;
        }
        public PagedResultsDto GetAllBranchsTranslation(string language)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Branch).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Branchs = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()).Select(x => x.Branch)
                .OrderBy(x => x.BranchId).ToList();
            results.Data = Mapper.Map<List<Branch>, List<BranchDto>>(Branchs, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Branch Branch in src)
                        {
                            Branch.BranchTranslations = Branch.BranchTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
        public PagedResultsDto GetBranchTranslationByBranchId(string language,long BranchId)
        {
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()  && x.BranchId == BranchId).Select(x => x.Branch).Count(x => !x.IsDeleted);
            var aaax = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Branchs = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower() && x.BranchId == BranchId).Select(x => x.Branch)
                .OrderBy(x => x.BranchId).ToList();
            results.Data = Mapper.Map<List<Branch>, List<BranchDto>>(Branchs, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {
                        foreach (Branch Branch in src)
                        {
                            Branch.BranchTranslations = Branch.BranchTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();
                        }

                    }
                );
            });
            return results;
        }
        public BranchDto BranchTranslationByBranchId(string language, long BranchId)
        {
            var aaax = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower()).Select().ToList();
            var Branchs = _repository.Query(x => !x.Branch.IsDeleted && x.Language.ToLower() == language.ToLower() && x.BranchId == BranchId).Select(x => x.Branch)
                .OrderBy(x => x.BranchId).FirstOrDefault();
            var results = Mapper.Map<Branch, BranchDto>(Branchs, opt =>
            {
                opt.BeforeMap((src, dest) =>
                    {

                        src.BranchTranslations = src.BranchTranslations.Where(x => x.Language.ToLower() == language.ToLower()).ToList();


                    }
                );
            });
            return results;
        }

        public bool CheckNameExist(string objName, string language, long recordId, long tenantId, long areaId)
        {
            return Queryable()
                .Any(x => x.Language.ToLower() == language.ToLower() && x.Title.ToLower() == objName.ToLower() &&
                          x.BranchId != recordId && x.Branch.TenantId == tenantId && !x.Branch.IsDeleted && x.Branch.AreaId == areaId);
        }
    }
}