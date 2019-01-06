using System.Collections.Generic;
using System.Linq;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.Common;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class ClientService : Service<Client>, IClientService
    {
        public ClientService(IRepositoryAsync<Client> repository) : base(repository)
        {
            _repository = repository;
        }

        public List<Client> GetAllClients(long adminId, int page, int pageSize)
        {
            var clients = _repository
                .Query(x => !x.IsDeleted).Select()
                .OrderBy(x => x.UserId).Skip((page - 1) * pageSize)
                .Take(pageSize).ToList();
            return clients;
        }

        public bool CheckUserNameDuplicated(string userName, long userId, long adminId)
        {
            return _repository.Queryable().Any(u => u.UserName.ToLower() == userName.ToLower() && !u.IsDeleted && u.UserId != userId );
        }

        public bool CheckClientUserNameDuplicated(string userName, long userId, string phone)
        {
            return _repository.Queryable().Any(u => (u.UserName.ToLower() == userName.ToLower() || u.Phone == phone) && !u.IsDeleted && u.UserId != userId
            && u.Role == Enums.RoleType.User );
        }
    }
}
