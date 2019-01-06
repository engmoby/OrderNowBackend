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
    public class SideItemsController : BaseApiController
    {
        private ISideItemFacade _sideItemFacade;

        public SideItemsController(ISideItemFacade sideItemFacade)
        {
            _sideItemFacade = sideItemFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "AddSideItem")]
        [HttpPost]
        public IHttpActionResult AddSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.AddSideItem(Mapper.Map<SideItemDTO>(sideItemModel),UserId, Language);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/", Name = "GetAllSideItems")]
        [HttpGet]
        [ResponseType(typeof(List<SideItemModel>))]
        public IHttpActionResult GetAllSideItems(int page = Page, int pagesize = PageSize)
        {
            var sideItems = _sideItemFacade.GetAllSideItems(Language,UserId, page, pagesize);
            return PagedResponse("GetAllSideItems", page, pagesize, sideItems.TotalCount, Mapper.Map<List<SideItemModel>>(sideItems.Data));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems/{sideItemId:long}", Name = "DeleteSideItem")]
        [HttpDelete]
        public IHttpActionResult DeleteSideItem(long sideItemId)
        {
            _sideItemFacade.DeleteSideItem(sideItemId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/SideItems", Name = "UpdateSideItem")]
        [HttpPut]
        public IHttpActionResult UpdateSideItem([FromBody] SideItemModel sideItemModel)
        {
            _sideItemFacade.UpdateSideItem(Mapper.Map<SideItemDTO>(sideItemModel), UserId, Language);
            return Ok();
        }
    }
}
