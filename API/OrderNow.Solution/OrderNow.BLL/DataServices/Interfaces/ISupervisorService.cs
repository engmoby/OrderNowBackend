using System.Collections.Generic;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface ISupervisorService:IService<Supervisor>
    {
        List<Supervisor> GetAllSupervisors(long restaurantId, int page, int pageSize);
        bool CheckUserNameDuplicated(string userName, long userId, long restaurantId);
        bool CheckClientUserNameDuplicated(string userName, long userId, string phone);
    }
}
