using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.UnitOfWork;
using OrderNow.Common.CustomException;

namespace OrderNow.BLL.Services
{
    public class RequestFacade : BaseFacade, IRequestFacade
    {
        private IRequestService _requestService;
        private IRequestDetailService _requestDetailService;
        private IUserService _userService;
        private ITableService _tableService;
        private IRestaurantWaiterService _restaurantWaiterService;
        private IItemSizeService _itemSizeService;
        public RequestFacade(IUnitOfWorkAsync unitOfWork, IRequestService requestService, IRequestDetailService requestDetailService, IUserService userService, IRestaurantWaiterService restaurantWaiterService, ITableService tableService, IItemSizeService itemSizeService) : base(unitOfWork)
        {
            _requestService = requestService;
            _requestDetailService = requestDetailService;
            _userService = userService;
            _restaurantWaiterService = restaurantWaiterService;
            _tableService = tableService;
            _itemSizeService = itemSizeService;
        }

        public void CreateRequest(RequestDto requestDto)
        {
            Request request = Mapper.Map<Request>(requestDto);

            var user = _userService.Find(requestDto.UserId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);

       //  var table = _tableService.Find(requestDto.TableId);

            request.CreationBy = (long) requestDto.TableId;
            request.ModifiedBy = requestDto.UserId;
            request.CreateTime = DateTime.UtcNow;
            request.Status = Enums.RequestStatus.Pending;
            // request.Table = table; 

            request.RequestDetails = new List<RequestDetail>();
            foreach (var item in requestDto.RequestDetails)
            {
                var getItemSizeId = _itemSizeService.Query(x => x.SizeId == item.ItemSizeId && x.ItemId == item.ItemId)
                    .Select().FirstOrDefault();
                if (getItemSizeId != null)
                    request.RequestDetails.Add(new RequestDetail
                    {
                        ItemSizeId = getItemSizeId.ItemSizeId,
                        Number = item.Number,
                        //RequestId = requestId,
                        Price = item.Price
                    });
            }

            _requestDetailService.InsertRange(request.RequestDetails);
            _requestService.Insert(request);
            SaveChanges();
        }

        public PagedResultsDto GetAllRequests(long userId, int page, int pageSize, string role, long statusId, long featureId, string from, string to)
        {
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            int requestsCount = 0;
            List<RequestDto> requests = new List<RequestDto>();
            var status = Enums.RequestStatus.Pending;
            switch (statusId)
            {
                case 1:
                    status = Enums.RequestStatus.Pending;
                    break;
                case 2:
                    status = Enums.RequestStatus.Approved;
                    break;
                case 3:
                    status = Enums.RequestStatus.Rejected;
                    break;
            }
            if (role == Enums.RoleType.Admin.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                requestsCount = _requestService.Query(x => x.Creater.TableId == userId && (statusId <= 0 || x.Status == status)

                                                           && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select().Count();
                requests = Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByAdmin(userId, page, pageSize, statusId, featureId, fromDateTime, toDateTime));
            }

            else if (role == Enums.RoleType.Supervisor.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
                var waiter = _restaurantWaiterService.Find(userId);

                var tableList = _tableService.GetAllTables(waiter.BranchId);
                foreach (var table in tableList)
                {
                    requestsCount = _requestService.Query(x => x.RestaurantId == waiter.RestaurantId && (statusId <= 0 || x.Status == status) && x.TableId == table.TableId).Select().Count();

                    requests.AddRange(Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByWaiter(waiter.RestaurantId, statusId, status, page, pageSize, table.TableId, fromDateTime, toDateTime)));

                }
            }


            else if (role == Enums.RoleType.User.ToString())
            {
                DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
                DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;


                requestsCount = _requestService.Query(x => x.ModifiedBy == userId && (statusId <= 0 || x.Status == status)).Select().Count();

                requests.AddRange(Mapper.Map<List<RequestDto>>(_requestService.GetAllRequestsByClient(userId, statusId, status, page, pageSize, fromDateTime, toDateTime)));


            }

            PagedResultsDto results = new PagedResultsDto
            {
                TotalCount = requestsCount,
                Data = requests.OrderByDescending(x=>x.RequestId)
            };
            return results;
        }

        public List<RequestDetailDto> GetAllRequestDetailByFeatureId(long featureId)
        {
            return null;
        }

        public void ApproveRequest(long requestId, long userId, List<RequestDetailDto> requestDetailDtos)
        {
            Request request = _requestService.Find(requestId);
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            if (request.Status == Enums.RequestStatus.Pending)
            {

                request.ModifyTime = DateTime.UtcNow;
                request.ModifiedBy = userId;
                request.Status = Enums.RequestStatus.Approved;
                //if (requestDetailDtos != null && request.Feature.Type == Enums.FeatureType.Normal)
                //{
                //    foreach (var requestDetail in requestDetailDtos)
                //    {
                //        var price = _featureDetailService.Find(requestDetail.FeatureDetailId).Price;
                //        request.RequestDetails.Add(new RequestDetail
                //        {
                //            FeatureDetailId = requestDetail.FeatureDetailId,
                //            Number = requestDetail.Number,
                //            RequestId = requestId,
                //            Price = price
                //        });
                //    }
                //    _requestDetailService.InsertRange(request.RequestDetails);
                //}
                _requestService.Update(request);
                SaveChanges();
            }
            else
                throw new ValidationException(ErrorCodes.RequestStatusChanged);
        }

        public void RejectRequest(long requestId, long userId)
        {
            Request request = _requestService.Find(requestId);
            var user = _userService.Find(userId);
            if (user == null)
                throw new ValidationException(ErrorCodes.UserNotFound);
            if (user.IsDeleted)
                throw new ValidationException(ErrorCodes.UserDeleted);
            if (!user.IsActive)
                throw new ValidationException(ErrorCodes.UserDeactivated);
            if (request.Status == Enums.RequestStatus.Pending)
            {
                request.ModifyTime = DateTime.UtcNow;
                request.ModifiedBy = userId;
                request.Status = Enums.RequestStatus.Rejected;
                _requestService.Update(request);
                SaveChanges();
            }
            else
                throw new ValidationException(ErrorCodes.RequestStatusChanged);
        }

        public RequestStatusDto GetLastRequestByFeaturedId(long featureId, long roomId)
        {
            return null;//  Mapper.Map<RequestStatusDto>(_requestService.Query(x => x.FeatureId == featureId && x.CreationBy == roomId).Select().OrderByDescending(x => x.CreateTime).FirstOrDefault());
        }
    }
}
