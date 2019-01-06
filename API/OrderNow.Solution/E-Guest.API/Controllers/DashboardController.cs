using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using E_Guest.API.Infrastructure;
using E_Guest.API.Models;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;

namespace E_Guest.API.Controllers
{
    public class DashboardController : BaseApiController
    {
        private readonly IDashboardFacade _dashboardFacade;
        private readonly IUserFacade _userFacade;
        public DashboardController(IDashboardFacade dashboardFacade, IUserFacade userFacade)
        {
            _dashboardFacade = dashboardFacade;
            _userFacade = userFacade;
        }

        [Route("api/Dashboard", Name = "GetRequestDashboard")]
        [HttpGet]
        public IHttpActionResult GetTicketDashboard(string xaxis = "branch",
            long countryId = 0, long regionId = 0, long cityId = 0, long areaId = 0, long branchId = 0, string from = "", string to = "", string status = "")
        {
            var getuserResturant = _userFacade.GetUser(UserId);
            return Ok(_dashboardFacade.GetDashboardDto(getuserResturant.RestaurantId, xaxis, countryId, regionId, cityId, areaId, branchId, from, to, status));
        }

        [Route("api/ItemCount", Name = "GetItemCount")]
        [HttpGet]
        public IHttpActionResult GetItemCount(string xaxis = "branch",
            long countryId = 0, long regionId = 0, long cityId = 0, long areaId = 0, long branchId = 0, string from = "", string to = "", string status = "")
        {
            var getuserResturant = _userFacade.GetUser(UserId);
            var data = _dashboardFacade.GetItemCount(getuserResturant.RestaurantId, xaxis, countryId, regionId, cityId,
                areaId, branchId, from, to, status);
            foreach (var item in data)
            {
                item.ImageUrl = Url.Link("ItemImage", new { getuserResturant.RestaurantId, item.Item.MenuId, item.Item.CategoryId, item.ItemId });
            }
            return Ok(data);
        }
    }

}