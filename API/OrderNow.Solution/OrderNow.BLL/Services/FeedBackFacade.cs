﻿using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using OrderNow.Common.CustomException;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class FeedBackFacade : BaseFacade, IFeedBackFacade
    {
        private IFeedBackService _feedBackService; 
        private IRestaurantService _restaurantService;

        public FeedBackFacade(IFeedBackService feedBackService, IUnitOfWorkAsync unitOfWork, IRestaurantService restaurantService) : base(unitOfWork)
        {
            _feedBackService = feedBackService; 
            _restaurantService = restaurantService;
        }

        public void AddFeedBack(FeedBackDto feedBackDto, long userId)
        {
            //var waiter = _restaurantWaiterService.Find(userId);
            //if (waiter == null) throw new NotFoundException(ErrorCodes.RestaurantAdminNotFound);
            var feedback = Mapper.Map<FeedBack>(feedBackDto);
            //feedback.RestaurantId = waiter.RestaurantId;
            // feedback.CreateTime = DateTime.Now;
            _feedBackService.Insert(feedback);
            SaveChanges();
        }

        public PagedResultsDto GetAllFeedBack(long userId, long restaurantId, int page, int pageSize, string userRole)
        {
            PagedResultsDto results = new PagedResultsDto();
            if (userRole == Enums.RoleType.User.ToString())
            {
                var restaurant = _restaurantService.Find(restaurantId);
                if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
                results = _feedBackService.GetAllFeedBack(restaurantId, page, pageSize);
            }
            else if (userRole == Enums.RoleType.RestaurantAdmin.ToString())
            {
                var restaurant = _restaurantService.GetRestaurantByAdminId(userId);
                if (restaurant == null) throw new NotFoundException(ErrorCodes.RestaurantNotFound);
                results = _feedBackService.GetAllFeedBack(restaurant.RestaurantId, page, pageSize);
            }
            return results;

        }
    }
}
