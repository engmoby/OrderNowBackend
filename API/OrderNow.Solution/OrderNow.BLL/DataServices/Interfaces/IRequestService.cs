using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IRequestService : IService<Request>
    {
        List<Request> GetAllRequestsByAdmin(long adminId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsBySupervisor(long userId, int page, int pageSize, long roomId, long featureId, DateTime fromDateTime, DateTime toDateTime);
        List<Request> GetAllRequestsByWaiter(long restaurantId, long statusId, Enums.RequestStatus status, int page, int pageSize, long roomId, DateTime fromDateTime, DateTime toDateTime);

        List<Request> GetAllRequestsByClient(long userId, long statusId, Enums.RequestStatus status, int page,
            int pageSize, DateTime fromDateTime, DateTime toDateTime);


        List<Request> GetDashboardDto(long resturantId, string xAxis,
            long countryId, long regionId, long cityId, long areaId, long branchId, DateTime fromDateTime,
            DateTime toDateTime,
            Enums.RequestStatus statusFilter, string status);
    }
}
