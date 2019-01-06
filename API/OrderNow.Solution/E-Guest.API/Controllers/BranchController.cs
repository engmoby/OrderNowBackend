using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class BranchController : BaseApiController
    {
        private readonly IBranchFacade _branchFacade;
        public BranchController(IBranchFacade branchFacade)
        {
            _branchFacade = branchFacade;
        }

        [Route("api/Branchs/{areaId:long}/GetAllBranchs", Name = "GetAllBranchs")]
        [HttpGet]
        public IHttpActionResult GetAllBranchs(long areaId, int page = Page, int pagesize = PageSize)
         {
            PagedResultsDto branchObj = _branchFacade.GetAllBranchs(areaId, page, pagesize, TenantId);
            var data = Mapper.Map<List<BranchModel>>(branchObj.Data);
            return PagedResponse("GetAllBranchs", page, pagesize, branchObj.TotalCount, data);
        }


        [Route("api/Branchs", Name = "CreateBranch")]
        [HttpPost]
        public IHttpActionResult CreateBranch([FromBody] BranchModel branchModel)
        {
            var reurnBranch = _branchFacade.CreateBranch(Mapper.Map<BranchDto>(branchModel), UserId, TenantId);

            return Ok(reurnBranch);
        }


        [Route("api/Branchs/EditBranch", Name = "EditBranch")]
        [HttpPost]
        public IHttpActionResult EditBranch([FromBody] BranchModel BranchModel)
        {
            var reurnBranch = _branchFacade.EditBranch(Mapper.Map<BranchDto>(BranchModel), UserId, TenantId);

            return Ok(reurnBranch);
        }


        [Route("api/Branchs/GetBranchById", Name = "GetBranchById")]
        [HttpGet]
        public IHttpActionResult GetBranchById(long BranchId)
        {
            var reurnBranch = _branchFacade.GetBranch(BranchId, TenantId);
            return Ok(reurnBranch);
        }
    }

}