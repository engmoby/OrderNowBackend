using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using OperationSurvey.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class CountryController : BaseApiController
    {
        private readonly ICountryFacade _countryFacade;
        private readonly IUserFacade _userFacade;
        UserDto getResturantIdByUserId;
  public CountryController(ICountryFacade countryFacade, IUserFacade userFacade)
        {
            _countryFacade = countryFacade;
            _userFacade = userFacade;
              getResturantIdByUserId = _userFacade.GetUser(UserId);
        }

        [Route("api/Countries/GetAllCountries", Name = "GetAllCountries")]
        [HttpGet]
        public IHttpActionResult GetAllCountries(int page = Page, int pagesize = PageSize)
        {
            var getResturantIdByUserId = _userFacade.GetUser(UserId);
            PagedResultsDto allCountries = _countryFacade.GetAllCountries(page, pagesize, (int) getResturantIdByUserId.RestaurantId);
            var data = Mapper.Map<List<CountryModel>>(allCountries.Data);
            return PagedResponse("GetAllCountries", page, pagesize, allCountries.TotalCount, data);
        }


        [Route("api/Countries", Name = "CreateCountry")]
        [HttpPost]
        public IHttpActionResult CreateCountry([FromBody] CountryModel countryModel)
        {
         //   var getResturantIdByUserId = _userFacade.GetUser(UserId);
            countryModel.RestaurantId = getResturantIdByUserId.RestaurantId;
            var country = _countryFacade.CreateCountry(Mapper.Map<CountryDto>(countryModel), UserId, (int)getResturantIdByUserId.RestaurantId);

            return Ok(country);
        }


        [Route("api/Countries/EditCountry", Name = "EditCountry")]
        [HttpPost]
        public IHttpActionResult EditCountry([FromBody] CountryModel countryModel)
        {
            var country = _countryFacade.EditCountry(Mapper.Map<CountryDto>(countryModel), UserId, (int)getResturantIdByUserId.RestaurantId);

            return Ok(country);
        }
        [Route("api/Countries/{countryId:long}", Name = "GetCountry")]
        [HttpGet]
        public IHttpActionResult GetCountry(long countryId)
        { 
            var countryModel = Mapper.Map<CountryModel>(_countryFacade.GetCountry(countryId, (int)getResturantIdByUserId.RestaurantId));
            return Ok(countryModel);
        }
    }
}
