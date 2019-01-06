using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderNow.BLL.DataServices.Interfaces;
using OrderNow.DAL.Entities.Model;
using Repository.Pattern.Repositories;
using Service.Pattern;

namespace OrderNow.BLL.DataServices
{
    public class RefreshTokenService : Service<RefreshToken>, IRefreshTokenService
    {
        private readonly IRepositoryAsync<RefreshToken> _repository;

        public RefreshTokenService(IRepositoryAsync<RefreshToken> repository) : base(repository)
        {
            _repository = repository;
        }
    }
}
