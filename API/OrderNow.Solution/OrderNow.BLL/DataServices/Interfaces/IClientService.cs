using System.Collections.Generic;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IClientService:IService<Client>
    {
        List<Client> GetAllClients(long adminId, int page, int pageSize);
        bool CheckUserNameDuplicated(string userName, long userId, long adminId);
        bool CheckClientUserNameDuplicated(string userName, long userId, string phone);
    }
}
