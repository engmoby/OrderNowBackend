using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class RequestService : Service<Request>, IRequestService
    {
        public RequestService(IRepositoryAsync<Request> repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Request> GetAllRequestsByAdmin(long adminId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            //if (pageSize > 0)
            //    requests = _repository.Query(x => x.Creater.UserId == adminId &&(roomId <= 0 || x.CreationBy==roomId) && (featureId <= 0 || x.FeatureId == featureId)
            //    && x.CreateTime>=fromDateTime && x.CreateTime<=toDateTime).Select()
            //        .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
            //        .Take(pageSize).ToList();
            //else
            //    requests = _repository.Query(x => x.Creater.UserId == adminId && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
            //                                      && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
            //        .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return null;// requests;
        }

        public List<Request> GetAllRequestsBySupervisor(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            //if (pageSize > 0)
            //    requests = _repository.Query(x => x.Feature.SupervisorFeatures.Select(s => s.SupervisorId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
            //                                      && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
            //        .OrderBy(x=>x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
            //        .Take(pageSize).ToList();
            //else
            //    requests = _repository.Query(x => x.Feature.SupervisorFeatures.Select(s => s.SupervisorId).Contains(userId) && (roomId <= 0 || x.CreationBy == roomId) && (featureId <= 0 || x.FeatureId == featureId)
            //                                      && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
            //        .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return null;//requests;
        }


        public List<Request> GetAllRequestsByWaiter(long restaurantId, long statusId, Enums.RequestStatus status, int page, int pageSize, long branchId, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
                requests = _repository.Query(x => x.RestaurantId.Value == restaurantId && (statusId <= 0 || x.Status == status) && (branchId <= 0 || x.TableId == branchId) && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                    .Take(pageSize).ToList();
            else
                requests = _repository.Query(x => x.RestaurantId.Value == restaurantId && (statusId <= 0 || x.Status == status) && (branchId <= 0 || x.TableId == branchId) && x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                    .OrderBy(x => x.Status).ThenByDescending(x => x.CreateTime).ToList();

            return requests;
        }


        public List<Request> GetAllRequestsByClient(long userId, long statusId, Enums.RequestStatus status, int page,
            int pageSize, DateTime fromDateTime, DateTime toDateTime)
        {
            List<Request> requests;
            if (pageSize > 0)
            {
                if (statusId == 4)
                {
                    requests = _repository.Query(x => x.ModifiedBy == userId && x.Status != Enums.RequestStatus.Pending &&
                                                      x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                        .OrderByDescending(x => x.RequestId).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
                else
                {
                    requests = _repository.Query(x => x.ModifiedBy == userId && (statusId <= 0 || x.Status == status) &&
                                                      x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime).Select()
                        .OrderByDescending(x => x.RequestId).ThenByDescending(x => x.CreateTime).Skip((page - 1) * pageSize)
                        .Take(pageSize).ToList();
                }
            }
            else
            {
                if (statusId == 4)
                {
                    requests = _repository.Query(x => x.ModifiedBy == userId && x.Status != Enums.RequestStatus.Pending &&
                                                      x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime)
                        .Select()
                        .OrderByDescending(x => x.RequestId).ThenByDescending(x => x.CreateTime).ToList();
                }
                else
                {
                    requests = _repository.Query(x => x.ModifiedBy == userId && (statusId <= 0 || x.Status != status) &&
                                                      x.CreateTime >= fromDateTime && x.CreateTime <= toDateTime)
                        .Select()
                        .OrderByDescending(x => x.RequestId).ThenByDescending(x => x.CreateTime).ToList();
                }
            }
            return requests;
        }

        public List<Request> GetDashboardDto(long resturantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId, long branchId, DateTime fromDateTime, DateTime toDateTime,
            Enums.RequestStatus statusFilter, string status)
        {
            List<Request> requests;
             
                    requests = _repository.Query(x => x.RestaurantId == resturantId
                                                      && x.CreateTime >= fromDateTime
                                                      && x.CreateTime <= toDateTime
                        && (countryId <= 0 || x.Table.Branch.Area.City.Region.CountryId == countryId)
                        && (regionId <= 0 || x.Table.Branch.Area.City.RegionId == regionId)
                        && (cityId <= 0 || x.Table.Branch.Area.CityId == cityId)
                        && (areaId <= 0 || x.Table.Branch.AreaId == areaId)
                        && (branchId <= 0 || x.Table.BranchId == branchId)
                         && (status == "" || x.Status == statusFilter)
                    ).Select().ToList();
          
             
            return requests;
        }

    }
}
