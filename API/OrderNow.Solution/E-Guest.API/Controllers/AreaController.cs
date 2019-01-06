using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using OperationSurvey.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class AreaController : BaseApiController
    { 
        private readonly IAreaFacade _areaFacade;
        public AreaController(IAreaFacade areaFacade)
        {
            _areaFacade = areaFacade; 
        }

        [Route("api/Cities/{cityId:long}/Areas/GetAllAreas", Name = "GetAllAreas")]
        [HttpGet]
        public IHttpActionResult GetAllAreas(long cityId, int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto areaObj = _areaFacade.GetAllAreas(cityId,page, pagesize, TenantId);
            var data = Mapper.Map<List<AreaModel>>(areaObj.Data);
            return PagedResponse("GetAllAreas", page, pagesize, areaObj.TotalCount, data);
        }


        [Route("api/Areas", Name = "CreateArea")]
        [HttpPost]
        public IHttpActionResult CreateArea([FromBody] AreaModel areaModel)
        {
            var reurnArea = _areaFacade.CreateArea(Mapper.Map<AreaDto>(areaModel),UserId, TenantId);

            return Ok(reurnArea);
        }


        [Route("api/Areas/EditArea", Name = "EditArea")]
        [HttpPost]
        public IHttpActionResult EditArea([FromBody] AreaModel areaModel)
        {
            var reurnArea = _areaFacade.EditArea(Mapper.Map<AreaDto>(areaModel), UserId, TenantId);

            return Ok(reurnArea);
        }


        [Route("api/Areas/GetAreaById", Name = "GetAreaById")]
        [HttpGet]
        public IHttpActionResult GetAreaById(long areaId)
        {
            var reurnArea = _areaFacade.GetArea(areaId, TenantId);
            return Ok(reurnArea);
        }

        
        
    }

}