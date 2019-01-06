using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;
using E_Guest.API.Providers;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;

namespace OrderNow.API.Controllers
{
    public class WaitersController : BaseApiController
    {
        private IUserFacade _userFacade;

        public WaitersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "GetAllRestaurantWaiters")]
        [HttpGet]
        [ResponseType(typeof(List<RestaurantWaiterModel>))]
        public IHttpActionResult GetAllRestaurantWaiters(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto waiters;
            waiters = _userFacade.GetAllRestaurantWaiters(UserId, page, pagesize,Language);
            var data = Mapper.Map<List<RestaurantWaiterModel>>(waiters.Data);
            return PagedResponse("GetAllItemsForCategory", page, pagesize, waiters.TotalCount, data);
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "AddRestaurantWaiter")]
        [HttpPost]
        public IHttpActionResult AddRestaurantWaiter([FromBody] RestaurantWaiterModel restaurantWaiterModel)
        { 
            _userFacade.AddRestaurantWaiter(Mapper.Map<RestaurantWaiterDto>(restaurantWaiterModel),UserId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters/{waiterId:long}", Name = "GetRestaurantWaiter")]
        [HttpGet]
        public IHttpActionResult GetRestaurantWaiter(long waiterId)
        {
            _userFacade.GetRestaurantWaiter(waiterId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters/{waiterId:long}", Name = "DeleteRestaurantWaiter")]
        [HttpDelete]
        public IHttpActionResult DeleteRestaurantWaiter(long waiterId)
        {
            _userFacade.DeleteRestaurantWaiter(waiterId);
            return Ok();
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters", Name = "UpdateRestaurantWaiter")]
        [HttpPut]
        public IHttpActionResult UpdateRestaurantWaiter([FromBody] RestaurantWaiterModel restaurantWaiterModel)
        {
            _userFacade.UpdateRestaurantWaiter(Mapper.Map<RestaurantWaiterDto>(restaurantWaiterModel));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Waiters/Limit", Name = "GetRestaurantWaitersLimit")]
        [HttpGet]
        public IHttpActionResult GetRestaurantWaitersLimit()
        {
            return Ok(_userFacade.GetMaxAndConsumedUsers(UserId));
        }
    }
}
