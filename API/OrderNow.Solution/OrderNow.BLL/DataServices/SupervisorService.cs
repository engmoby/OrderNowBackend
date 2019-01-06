using System.Collections.Generic;
using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class SupervisorService : Service<Supervisor>, ISupervisorService
    {
        public SupervisorService(IRepositoryAsync<Supervisor> repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Supervisor> GetAllSupervisors(long restaurantId, int page, int pageSize)
        {
            var supervisors = _repository
                .Query(x => !x.IsDeleted && x.RestaurantId == restaurantId).Select()
                .OrderBy(x => x.UserId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            return supervisors;
        }

        public bool CheckUserNameDuplicated(string userName, long userId, long restaurantId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId
            && u.RestaurantId == restaurantId);
        }

        public bool CheckClientUserNameDuplicated(string userName, long userId, string phone)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId
            && u.Role == Enums.RoleType.User && u.Phone == phone);
        }
    }
}
