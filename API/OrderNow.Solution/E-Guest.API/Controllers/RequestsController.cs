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
    public class RequestsController : BaseApiController
    {
        private IRequestFacade _requestFacade;
        public RequestsController(IRequestFacade requestFacade)
        {
            _requestFacade = requestFacade;
        }

        // [AuthorizeRoles(Enums.RoleType.User)]
        [Route("api/Requests", Name = "CreateRequest")]
        [HttpPost]
        public IHttpActionResult CreateRequest([FromBody] RequestModel requestModel)
        {
            // requestModel.RoomId = UserId;
            _requestFacade.CreateRequest(Mapper.Map<RequestDto>(requestModel));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Admin, Enums.RoleType.Supervisor, Enums.RoleType.Waiter, Enums.RoleType.User)]
        [Route("api/Requests/", Name = "GetAllRequests")]
        [HttpGet]
        [ResponseType(typeof(List<RequestModel>))]
        public IHttpActionResult GetAllRequests(int page = Page, int pagesize = PageSize, long statusId = 0, long featureId = 0, string from = "", string to = "")
        {
            PagedResultsDto requests = _requestFacade.GetAllRequests(UserId, page, pagesize, UserRole, statusId, featureId, from, to);
            var data = Mapper.Map<List<RequestModel>>(requests.Data);
            foreach (var item in data)
            {
                item.LogoURL = Url.Link("RestaurantLogo", new { item.RestaurantId });
            }
            return PagedResponse("GetAllRequests", page, pagesize, requests.TotalCount, data);
        }



        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Requests/{requestId:long}/Approve", Name = "ApproveRequest")]
        [HttpPost]
        public IHttpActionResult ApproveRequest(long requestId, [FromBody] RequestModel requestModel)
        {
            _requestFacade.ApproveRequest(requestId, UserId, Mapper.Map<List<RequestDetailDto>>(requestModel.RequestDetails));
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.Supervisor, Enums.RoleType.Waiter)]
        [Route("api/Requests/{requestId:long}/Reject", Name = "RejectRequest")]
        [HttpGet]
        public IHttpActionResult RejectRequest(long requestId)
        {
            _requestFacade.RejectRequest(requestId, UserId);
            return Ok();
        }
    }
}
