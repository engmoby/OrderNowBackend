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
    public class UsersController : BaseApiController
    {
        private IUserFacade _userFacade;

        public UsersController(IUserFacade userFacade)
        {
            _userFacade = userFacade;
        }

        #region Client

        //  [AuthorizeRoles(Enums.RoleType.User)]
        [Route("api/Users/Client", Name = "AddClient")]
        [HttpPost]
        public IHttpActionResult AddClient([FromBody] ClientModel clientModel)
        {
            var getUser = _userFacade.AddClient(Mapper.Map<ClientDto>(clientModel));
            return Ok(getUser);
        }

        [AuthorizeRoles(Enums.RoleType.User)]
        [Route("api/Users/{waiterId:long}", Name = "GetClient")]
        [HttpGet]
        public IHttpActionResult GetClient(long waiterId)
        {
            _userFacade.GetRestaurantWaiter(waiterId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.User)]
        [Route("api/Users", Name = "UpdateClient")]
        [HttpPut]
        public IHttpActionResult UpdateClient([FromBody] RestaurantWaiterModel restaurantWaiterModel)
        {
            _userFacade.UpdateRestaurantWaiter(Mapper.Map<RestaurantWaiterDto>(restaurantWaiterModel));
            return Ok();
        }

       // [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Client/", Name = "GetAllClients")]
        [HttpGet]
        [ResponseType(typeof(List<ClientModel>))]
        public IHttpActionResult GetAllClients(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto users = _userFacade.GetAllUsers(UserId, page, pagesize, Enums.RoleType.User);
            var data = Mapper.Map<List<ClientModel>>(users.Data);

            return PagedResponse("GetAllClients", page, pagesize, users.TotalCount, data);
        }

        #endregion

        #region Supervisor
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor", Name = "AddSupervisor")]
        [HttpPost]
        public IHttpActionResult AddSupervisor([FromBody] SupervisorModel supervisorModel)
        {
            _userFacade.AddSupervisor(Mapper.Map<SupervisorDto>(supervisorModel), UserId);
            return Ok();
        }


        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor/", Name = "GetAllSupervisors")]
        [HttpGet]
        [ResponseType(typeof(List<SupervisorModel>))]
        public IHttpActionResult GetAllSupervisors(int page = Page, int pagesize = PageSize)
        {
            PagedResultsDto users = _userFacade.GetAllUsers(UserId, page, pagesize, Enums.RoleType.Supervisor);
            var data = Mapper.Map<List<SupervisorModel>>(users.Data);

            return PagedResponse("GetAllSupervisors", page, pagesize, users.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor/{supervisorId:long}", Name = "GetSupervisor")]
        [HttpGet]
        [ResponseType(typeof(SupervisorModel))]
        public IHttpActionResult GetSupervisor(long supervisorId)
        {
            return Ok(Mapper.Map<SupervisorModel>(_userFacade.GetSupervisor(supervisorId)));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor/{supervisorId:long}", Name = "DeleteSupervisor")]
        [HttpDelete]
        public IHttpActionResult DeleteSupervisor(long supervisorId)
        {
            _userFacade.DeleteSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor/{supervisorId:long}/Activate", Name = "ActivateSupervisor")]
        [HttpGet]
        public IHttpActionResult ActivateSupervisor(long supervisorId)
        {
            _userFacade.ActivateSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor/{supervisorId:long}/DeActivate", Name = "DeActivateSupervisor")]
        [HttpGet]
        public IHttpActionResult DeActivateSupervisor(long supervisorId)
        {
            _userFacade.DeActivateSupervisor(supervisorId, UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Users/Supervisor", Name = "UpdateSupervisor")]
        [HttpPut]
        public IHttpActionResult UpdateSupervisor([FromBody] SupervisorModel supervisorModel)
        {
            _userFacade.UpdateSupervisor(Mapper.Map<SupervisorDto>(supervisorModel), UserId);
            return Ok();
        }
        #endregion

        [AuthorizeRoles(Enums.RoleType.Admin)]
        [Route("api/Users/GetMaxAndConUsers", Name = "GetMaxAndConUsers")]
        [HttpGet]
        public IHttpActionResult GetMaxAndConUsers()
        {
            UserConsumedModel maxCon = Mapper.Map<UserConsumedModel>(_userFacade.GetMaxAndConsumedUsers(UserId));

            return Ok(maxCon);

        }

        [Route("api/Users/Register", Name = "Register")]
        [HttpPost]
        public IHttpActionResult Register([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.AddNewGlobalUser(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }

        [Route("api/Users/", Name = "UpdateGlobalUser")]
        [HttpPut]
        public IHttpActionResult UpdateGlobalUser([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.UpdateGlobalUser(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }
        [Route("api/Users/Package", Name = "UpdatePackage")]
        [HttpPut]
        public IHttpActionResult UpdatePackage([FromBody] AdminModel adminModel)
        {
            //if (Request.Headers.Authorization.Scheme == "X-Auth-Token" &&
            //    Request.Headers.Authorization.Parameter == "asdasdas")

            _userFacade.UpdateAdminPackage(Mapper.Map<AdminDto>(adminModel));
            return Ok();
            //return Unauthorized();
        }
    }
}
