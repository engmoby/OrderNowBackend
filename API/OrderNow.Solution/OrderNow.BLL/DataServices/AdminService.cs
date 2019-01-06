using System;
using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class AdminService:Service<Admin>,IAdminService
    {
        public AdminService(IRepositoryAsync<Admin> repository):base(repository)
        {
            
        }
        public Admin GetAdminByAccountId(Guid userAccountId)
        {
            return _repository.Query(x => x.UserAccountId == userAccountId).Select().FirstOrDefault();
        }
        public Admin GetAdminByUserId(long userId)
        {
            return _repository.Query(x => x.UserId == userId).Select().FirstOrDefault();
        }
    }
}
