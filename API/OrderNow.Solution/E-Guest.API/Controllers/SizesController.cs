﻿using System.Collections.Generic;
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
    public class SizesController : BaseApiController
    {
        private ISizeFacade _sizeFacade;

        public SizesController(ISizeFacade sizeFacade)
        {
            _sizeFacade = sizeFacade;
        }
        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes", Name = "AddSize")]
        [HttpPost]
        public IHttpActionResult AddSize([FromBody] SizeModel sizeModel)
        {
            _sizeFacade.AddSize(Mapper.Map<SizeDto>(sizeModel), UserId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes/", Name = "GetAllSizes")]
        [HttpGet]
        [ResponseType(typeof(List<SizeModel>))]
        public IHttpActionResult GetAllSizes(int page = Page, int pagesize = PageSize)
        {
            var sizes = _sizeFacade.GetAllSizes(Language,UserId, page, pagesize);
            return PagedResponse("GetAllSizes", page, pagesize, sizes.TotalCount, Mapper.Map<List<SizeModel>>(sizes.Data));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes/{sizeId:long}", Name = "DeleteSize")]
        [HttpDelete]
        public IHttpActionResult DeleteSize(long sizeId)
        {
            _sizeFacade.DeleteSize(sizeId);
            return Ok();
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes/{sizeId:long}", Name = "GetSize")]
        [HttpGet]
        public IHttpActionResult GetSize(long sizeId)
        {
            return Ok(Mapper.Map<SizeModel>(_sizeFacade.GetSize(sizeId)));
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin)]
        [Route("api/Sizes", Name = "UpdateSize")]
        [HttpPut]
        public IHttpActionResult UpdateSize([FromBody] SizeModel sizeModel)
        {
            _sizeFacade.UpdateSize(Mapper.Map<SizeDto>(sizeModel),UserId);
            return Ok();
        }
    }
}
