using System;
using OrderNow.DAL.Entities.Model;
using Service.Pattern;

namespace OrderNow.BLL.DataServices.Interfaces
{
    public interface IAdminService:IService<Admin>
    {
        Admin GetAdminByAccountId(Guid userAccountId);
        Admin GetAdminByUserId(long userId);
    }
}
