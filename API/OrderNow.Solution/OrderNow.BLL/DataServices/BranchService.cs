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
    public class BranchService : Service<Branch>, IBranchService
    {
        public BranchService(IRepositoryAsync<Branch> repository) : base(repository)
        {
            _repository = repository;
        }

        public PagedResultsDto GetAllBranchs(long areaId, int page, int pageSize, int tenantId)
        {

            var query = Queryable().Where(x => !x.IsDeleted && x.AreaId == areaId).OrderBy(x => x.BranchId);
            PagedResultsDto results = new PagedResultsDto();
            results.TotalCount = query.Select(x => x).Count();

            results.Data = Mapper.Map<List<Branch>, List<BranchDto>>(query.OrderBy(x => x.BranchId).Skip((page - 1) * pageSize).Take(pageSize).ToList());

            return results;

        }

    }
}