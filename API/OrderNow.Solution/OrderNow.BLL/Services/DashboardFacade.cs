using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.BLL.DTOs;
using OrderNow.BLL.Services.Interfaces;
using OrderNow.Common;
using Repository.Pattern.UnitOfWork;

namespace OrderNow.BLL.Services
{
    public class DashboardFacade : BaseFacade, IDashboardFacade
    {
        private readonly IRequestService _requestService;
        private readonly ITableService _tableService;
        private readonly IItemService _itemService;

        public DashboardFacade(IUnitOfWorkAsync unitOfWork, IRequestService requestService, ITableService tableService, IItemService itemService) : base(unitOfWork)
        {
            _requestService = requestService;
            _tableService = tableService;
            _itemService = itemService;
        }

        public DashboardFacade(IRequestService requestService, ITableService tableService, IItemService itemService)
        {
            _requestService = requestService;
            _tableService = tableService;
            _itemService = itemService;
        }
        public List<DashboardDto> GetDashboardDto(long resturantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId, long branchId, string from, string to, string status)
        {
            List<DashboardDto> dashboardDtos = new List<DashboardDto>();
            DateTime fromDateTime = !String.IsNullOrEmpty(from) ? DateTime.Parse(from) : DateTime.MinValue;
            DateTime toDateTime = !String.IsNullOrEmpty(to) ? DateTime.Parse(to) : DateTime.MaxValue;
            var statusFilter = Enums.RequestStatus.Approved;
            if (status == null) status = "";
            if (status != "")
                statusFilter = (Enums.RequestStatus)Enum.Parse(typeof(Enums.RequestStatus), status, true);
            //var dss = _requestService.Query(x => x.RestaurantId == resturantId && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select().Count();

            //var query = _requestService.Query(x =>
            ////x.RestaurantId == resturantId &&
            //                                      x.CreateTime >= fromDateTime
            //                                      && x.CreateTime <= toDateTime
            //                                      //&& (countryId <= 0 || x.Table.Branch.Area.City.Region.CountryId == countryId)
            //                                      //&& (regionId <= 0 || x.Table.Branch.Area.City.RegionId == regionId)
            //                                      //&& (cityId <= 0 || x.Table.Branch.Area.CityId == cityId)
            //                                      //&& (areaId <= 0 || x.Table.Branch.AreaId == areaId)
            //                                      //&& (branchId <= 0 || x.Table.BranchId == branchId) 
            //                                      && (status == "" || x.Status == statusFilter)
            //                                      ).Select();
            List<RequestDto> requests = new List<RequestDto>();
            var getFromsService =
                Mapper.Map<List<RequestDto>>(_requestService.GetDashboardDto(resturantId, xAxis, countryId, regionId, cityId, areaId, branchId, fromDateTime, toDateTime, statusFilter, status));
            foreach (var requestDto in getFromsService)
            {
                var getTableById = _tableService.Find(requestDto.TableId);
                requestDto.Table = Mapper.Map<TableDto>(getTableById);
                requests.Add(requestDto);
            }
            if (xAxis.ToLower() == "branch")
            {

                dashboardDtos = requests.GroupBy(x => x.Table.BranchId, (k, t) => new { key = k, requests = t.ToList() }).Select(x => x).ToList()
                    .Select(x => new DashboardDto
                    {
                        XaxisName = x.requests.FirstOrDefault().Table.Branch.TitleDictionary,
                        PendingCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Pending),
                        ApprovedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Approved),
                        RejectedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Rejected),
                    }).ToList();
            }
            else if (xAxis.ToLower() == "area")
            {
                dashboardDtos = requests.GroupBy(x => x.Table.Branch.AreaId, (k, t) => new { key = k, requests = t.ToList() }).Select(x => x).ToList()
                    .Select(x => new DashboardDto
                    {
                        XaxisName = x.requests.FirstOrDefault().Table.Branch.Area.TitleDictionary,
                        PendingCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Pending),
                        ApprovedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Approved),
                        RejectedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Rejected),
                    }).ToList();
            }
            else if (xAxis.ToLower() == "city")
            {
                dashboardDtos = requests.GroupBy(x => x.Table.Branch.Area.CityId, (k, t) => new { key = k, requests = t.ToList() }).Select(x => x).ToList()
                    .Select(x => new DashboardDto
                    {
                        XaxisName = x.requests.FirstOrDefault().Table.Branch.Area.City.TitleDictionary,
                        PendingCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Pending),
                        ApprovedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Approved),
                        RejectedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Rejected)
                    }).ToList();
            }
            else if (xAxis.ToLower() == "region")
            {
                dashboardDtos = requests.GroupBy(x => x.Table.Branch.Area.City.RegionId, (k, t) => new { key = k, requests = t.ToList() }).Select(x => x).ToList()
                    .Select(x => new DashboardDto
                    {
                        XaxisName = x.requests.FirstOrDefault().Table.Branch.Area.City.Region.TitleDictionary,
                        PendingCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Pending),
                        ApprovedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Approved),
                        RejectedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Rejected)
                    }).ToList();
            }
            else if (xAxis.ToLower() == "country")
            {
                dashboardDtos = requests.GroupBy(x => x.Table.Branch.Area.City.Region.CountryId, (k, t) => new { key = k, requests = t.ToList() }).Select(x => x).ToList()
                    .Select(x => new DashboardDto
                    {
                        XaxisName = x.requests.FirstOrDefault().Table.Branch.Area.City.Region.Country.TitleDictionary,
                        PendingCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Pending),
                        ApprovedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Approved),
                        RejectedCount = x.requests.Count(t => t.Status == Enums.RequestStatus.Rejected)
                    }).ToList();
            }


            return dashboardDtos;
        }

        public List<SellerItemDto> GetItemCount(long resturantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId, long branchId, string from, string to, string status)
        {
            var returnItem = new List<SellerItemDto>();
            var itemList = _itemService.GetAllItemsByRestuantId(resturantId);

            foreach (var item in itemList)
            {
                returnItem.Add(new SellerItemDto
                {
                    ItemId = item.ItemId,
                    Item = Mapper.Map<ItemDTO>(item),
                    TitleDictionary = item.ItemTranslations.ToDictionary(translation => translation.Language.ToLower(), translation => translation.ItemName),
                    Count = 0
                });
            }

            var getFromsService = _requestService.Query(x => x.RestaurantId == resturantId
                                             && (countryId <= 0 || x.Table.Branch.Area.City.Region.CountryId == countryId)
                                             && (regionId <= 0 || x.Table.Branch.Area.City.RegionId == regionId)
                                             && (cityId <= 0 || x.Table.Branch.Area.CityId == cityId)
                                             && (areaId <= 0 || x.Table.Branch.AreaId == areaId)
                                             && (branchId <= 0 || x.Table.BranchId == branchId)
                                             && (status == "" || x.Status == Enums.RequestStatus.Approved)
            ).Select().ToList();
            foreach (var sellerItemDto in returnItem)
            {
                var count = getFromsService.Count(x => x.RequestDetails.Exists(e => e.ItemSize.ItemId == sellerItemDto.ItemId));
                sellerItemDto.Count = count;
            }
            return new List<SellerItemDto>(returnItem.OrderByDescending(o => o.Count));
        }
    }
}
