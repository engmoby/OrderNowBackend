using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using OperationSurvey.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class CitiesController : BaseApiController
    {
        private ICityFacade _cityFacade;

        public CitiesController(ICityFacade cityFacade)
        {
            _cityFacade = cityFacade;
        }
        [Route("api/Regions/{regionId:long}/Cities", Name = "GetAllCities")]
        [HttpGet]
        public IHttpActionResult GetAllCities(long regionId, int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto allCities = _cityFacade.GetAllCities(regionId, page, pagesize, TenantId);
            var data = Mapper.Map<List<CityModel>>(allCities.Data);
            return PagedResponse("GetAllCities", page, pagesize, allCities.TotalCount, data);
        }


        [Route("api/Cities", Name = "CreateCity")]
        [HttpPost]
        public IHttpActionResult CreateCity([FromBody] CityModel cityModel)
        {
            var city = _cityFacade.CreateCity(Mapper.Map<CityDto>(cityModel), UserId, TenantId);

            return Ok(city);
        }


        [Route("api/Cities/EditCity", Name = "EditCity")]
        [HttpPost]
        public IHttpActionResult EditCity([FromBody] CityModel cityModel)
        {
            var city = _cityFacade.EditCity(Mapper.Map<CityDto>(cityModel), UserId, TenantId);

            return Ok(city);
        }
        [Route("api/Cities/{cityId:long}", Name = "GetCity")]
        [HttpGet]
        public IHttpActionResult GetCity(long cityId)
        {
            var cityModel = Mapper.Map<CityModel>(_cityFacade.GetCity(cityId, TenantId));
            return Ok(cityModel);
        }

        
    }
}
