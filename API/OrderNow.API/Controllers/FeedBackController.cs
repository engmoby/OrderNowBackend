﻿using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using OrderNow.API.Infrastructure;
using OrderNow.API.Models;
using OrderNow.API.Providers;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;

namespace OrderNow.API.Controllers
{
    public class FeedBackController : BaseApiController
    {
        private IFeedBackFacade _feedBackFacade;

        public FeedBackController(IFeedBackFacade feedBackFacade)
        {
            _feedBackFacade = feedBackFacade;
        }

        [AuthorizeRoles(Enums.RoleType.RestaurantAdmin, Enums.RoleType.User)]
        [Route("api/FeedBacks/", Name = "GetAllFeedBack")]
        [HttpGet]
        [ResponseType(typeof(List<FeedBackModel>))]
        public IHttpActionResult GetAllFeedBack(int page = Page, int pagesize = PageSize)
        {
            //var categories = _categoryFacade.GetAllCategoriesByMenuId(Language, menuId, page, pagesize);
            PagedResultsDto feedbacks;
            feedbacks = _feedBackFacade.GetAllFeedBack(UserId,0, page, pagesize, UserRole);
            var data = Mapper.Map<List<FeedBackModel>>(feedbacks.Data);

            return PagedResponse("GetAllFeedBack", page, pagesize, feedbacks.TotalCount, data);
        }

        [AuthorizeRoles(Enums.RoleType.User)]
        [Route("api/FeedBacks", Name = "FeedBacks")]
        [HttpPost]
        public IHttpActionResult AddFeedBack([FromBody]FeedBackModel feedBackModel) 
        {
            feedBackModel.RoomId = UserId;
            _feedBackFacade.AddFeedBack(Mapper.Map<FeedBackDto>(feedBackModel), UserId);
            return Ok();
        }
    }
}
